using Elastic.Clients.Elasticsearch;
using FluentResults;
using FluentValidation;
using LawSearchEngine.Application.Common.Connectors;
using LawSearchEngine.Application.Common.Contracts;
using LawSearchEngine.Application.Common.RequestTypes;
using LawSearchEngine.Application.Common.Services.Interfaces;
using LawSearchEngine.Domain.Indexes;
using MediatR;
using Minio;
using Minio.DataModel.Args;
using System.Text.RegularExpressions;

namespace LawSearchEngine.Application.Contracts.Commands
{
    public class UploadNewContractCommand : ICommand<FluentResults.Result>
    {
        public FileRequest ContractFile { get; set; } = null!;
    }

    public class UploadNewContractCommandValidator : AbstractValidator<UploadNewContractCommand>
    {
        public UploadNewContractCommandValidator(FileRequestValidator fileValidator)
        {
            RuleFor(x => x.ContractFile).SetValidator(fileValidator);
            RuleFor(x => x.ContractFile.FileExtension).Equal("pdf").WithMessage("File extension must be .pdf");
        }
    }

    public class UploadNewContractCommandHandler : IRequestHandler<UploadNewContractCommand, FluentResults.Result>
    {
        private readonly IMinioClient _minioClient;
        private readonly IDocumentReader _documentReader;
        private readonly IElasticSearchConnector _elasticSearchConnector;
        private readonly ILocationIQConnector _locationIQConnector;

        public UploadNewContractCommandHandler(IMinioClient minioClient, IDocumentReader documentReader, IElasticSearchConnector elasticSearchConnector, ILocationIQConnector locationIQConnector)
        {
            _minioClient = minioClient;
            _documentReader = documentReader;
            _elasticSearchConnector = elasticSearchConnector;
            _locationIQConnector = locationIQConnector;
        }

        public async Task<FluentResults.Result> Handle(UploadNewContractCommand request, CancellationToken cancellationToken)
        {
            string pdfTextContent = _documentReader.ReadDocument(request.ContractFile);

            var regexForGovernemntInfo = new Regex("Uprava za(.*?)u daljem tekstu klijent");

            var match = regexForGovernemntInfo.Match(pdfTextContent);

            if (!match.Success)
                 return FluentResults.Result.Fail("Government info not found");

            var regexForGovernmentInfos = new Regex("^(.*?), nivo uprave: (.*?), (.*)$");

            var govermentInfosMatch = regexForGovernmentInfos.Match(match.Groups[1].Value);

            if (!govermentInfosMatch.Success)
                return FluentResults.Result.Fail("Government infos not found");

            var govermentName = govermentInfosMatch.Groups[1].Value;
            var govermentLevel = govermentInfosMatch.Groups[2].Value;
            var governmentAddress = govermentInfosMatch.Groups[3].Value;

            var governmentLocation = await _locationIQConnector.GetLocation(governmentAddress);

            if (governmentLocation.IsFailed)
                return FluentResults.Result.Fail("Government Location not found");

            string[] lines = pdfTextContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            string[] lastTwoLines = lines.Skip(Math.Max(0, lines.Length - 4)).Take(2).ToArray();

            var signerGovernment = lastTwoLines[0];

            ContractIndex index = new()
            {
                Contract = pdfTextContent,
                ContractPath = $"/contracts/{request.ContractFile.FileName}",
                GovernmentName = govermentName,
                Id = Guid.NewGuid(),
                LevelOfGovernment = govermentLevel,
                SignerName = signerGovernment,
                SignerSurname = signerGovernment,
                Location = GeoLocation.Coordinates([governmentLocation.Value.Longitude, governmentLocation.Value.Latitude])
            };

            _elasticSearchConnector.IndexContract(index);

            await SaveContractToDatabase(request, cancellationToken);

            return FluentResults.Result.Ok();
        }

        private async Task SaveContractToDatabase(UploadNewContractCommand request, CancellationToken cancellationToken)
        {
            var bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket("contracts"), cancellationToken);

            if (!bucketExists)
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket("contracts"), cancellationToken);

            await _minioClient.PutObjectAsync(new PutObjectArgs()
                                   .WithBucket("contracts")
                                   .WithObject(request.ContractFile.FileName)
                                   .WithContentType("application/pdf")
                                   .WithObjectSize(request.ContractFile.FileContent.Length)
                                   .WithStreamData(new MemoryStream(request.ContractFile.FileContent)), cancellationToken);
        }
    }
}
