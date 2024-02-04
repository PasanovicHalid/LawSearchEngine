using Microsoft.AspNetCore.Http;

namespace LawSearchEngine.Presentation.Contracts.IndexContract
{
    public class ContractIndexRequest
    {
        public string SignerName { get; set; } = string.Empty;
        public string SignerSurname { get; set; } = string.Empty;
        public string GovernmentName { get; set; } = string.Empty;
        public string LevelOfGovernment { get; set; } = string.Empty;
        public IFormFile Contract { get; set; } = null!;
        public double Latitude { get; set; } = 0;
        public double Longitude { get; set; } = 0;
    }
}
