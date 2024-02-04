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

namespace LawSearchEngine.Application.Laws.Commands
{
    public class UploadNewLawCommand : ICommand<Result>
    {
        public FileRequest LawFile { get; set; } = null!;
    }

    public class UploadNewLawCommandValidator : AbstractValidator<UploadNewLawCommand>
    {
        public UploadNewLawCommandValidator(FileRequestValidator fileValidator)
        {
            RuleFor(x => x.LawFile).SetValidator(fileValidator);
            RuleFor(x => x.LawFile.FileExtension).Equal("pdf").WithMessage("File extension must be .pdf");
        }
    }

    public class UploadNewLawCommandHandler : IRequestHandler<UploadNewLawCommand, Result>
    {
        private readonly IMinioClient _minioClient;
        private readonly IDocumentReader _documentReader;
        private readonly IElasticSearchConnector _elasticSearchConnector;

        public UploadNewLawCommandHandler(IMinioClient minioClient, IDocumentReader documentReader, IElasticSearchConnector elasticSearchConnector)
        {
            _minioClient = minioClient;
            _documentReader = documentReader;
            _elasticSearchConnector = elasticSearchConnector;
        }

        public async Task<Result> Handle(UploadNewLawCommand request, CancellationToken cancellationToken)
        {
            string pdfTextContent = _documentReader.ReadDocument(request.LawFile);

            LawIndex index = new()
            {
                Law = pdfTextContent,
                LawPath = $"/laws/{request.LawFile.FileName}",
            };

            _elasticSearchConnector.IndexLaw(index);

            await SaveLawToDatabase(request, cancellationToken);

            return Result.Ok();
        }

        private async Task SaveLawToDatabase(UploadNewLawCommand request, CancellationToken cancellationToken)
        {
            var bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket("laws"), cancellationToken);

            if (!bucketExists)
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket("laws"), cancellationToken);

            await _minioClient.PutObjectAsync(new PutObjectArgs()
                                   .WithBucket("laws")
                                   .WithObject(request.LawFile.FileName)
                                   .WithContentType("application/pdf")
                                   .WithObjectSize(request.LawFile.FileContent.Length)
                                   .WithStreamData(new MemoryStream(request.LawFile.FileContent)), cancellationToken);
        }
    }
}
