namespace LawSearchEngine.Presentation.Contracts.SearchContract
{
    public class SearchContractRequest
    {
        public List<string> Search { get; set; } = new List<string>();
        public string Address { get; set; } = string.Empty;
        public double? Radius { get; set; }
    }
}
