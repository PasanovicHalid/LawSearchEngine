using LawSearchEngine.Application.Common.Contracts;

namespace LawSearchEngine.Application.Common.Services.Interfaces
{
    public interface IDocumentReader
    {
        string ReadDocument(FileRequest file);
    }
}
