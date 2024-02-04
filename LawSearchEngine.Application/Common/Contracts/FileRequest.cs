using FluentValidation;
using LawSearchEngine.Domain.Common.ObjectTypes;
using Newtonsoft.Json;

namespace LawSearchEngine.Application.Common.Contracts
{
    public class FileRequest : Value
    {
        public string FileName { get; private set; } = string.Empty;
        public string FileExtension { get; private set; } = string.Empty;
        [JsonIgnore]
        public byte[] FileContent { get; private set; } = Array.Empty<byte>();

        public FileRequest(string fileName, string fileExtension, byte[] fileContent)
        {
            FileName = fileName;
            FileExtension = fileExtension;
            FileContent = fileContent;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FileName;
            yield return FileExtension;
            yield return FileContent;
        }
    }

    public class FileRequestValidator : AbstractValidator<FileRequest>
    {
        public FileRequestValidator()
        {
            RuleFor(x => x.FileName).NotEmpty().WithMessage("File name cannot be empty.");
            RuleFor(x => x.FileExtension).NotEmpty().WithMessage("File extension cannot be empty.");
            RuleFor(x => x.FileContent).NotEmpty().WithMessage("File content cannot be empty.");
        }
    }
}
