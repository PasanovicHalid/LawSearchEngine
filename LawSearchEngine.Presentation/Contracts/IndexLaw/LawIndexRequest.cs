using Microsoft.AspNetCore.Http;

namespace LawSearchEngine.Presentation.Contracts.IndexLaw
{
    public class LawIndexRequest
    {
        public IFormFile Law { get; set; } = null!;
    }
}
