using Microsoft.AspNetCore.Http;

namespace LawSearchEngine.Presentation.Contracts.UploadContract
{
    public class UploadContractRequest
    {
        public IFormFile Contract { get; set; } = null!;
    }
}
