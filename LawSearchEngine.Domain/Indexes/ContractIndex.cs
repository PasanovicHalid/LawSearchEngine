using LawSearchEngine.Domain.Indexes.Common;

namespace LawSearchEngine.Domain.Indexes
{
    public class ContractIndex : IndexWithLocation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SignerName { get; set; } = string.Empty;
        public string SignerSurname { get; set; } = string.Empty;
        public string GovernmentName { get; set; } = string.Empty;
        public string LevelOfGovernment { get; set; } = string.Empty;
        public string Contract { get; set; } = string.Empty;
        public string ContractPath { get; set; } = string.Empty;
    }
}
