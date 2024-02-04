using Microsoft.AspNetCore.Http;

namespace LawSearchEngine.Presentation.Contracts.UploadLaw
{
    public class UploadLawRequest
    {
        public IFormFile Law { get; set; } = null!;
    }
}
