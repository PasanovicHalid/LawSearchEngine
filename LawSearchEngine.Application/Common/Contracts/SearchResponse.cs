namespace LawSearchEngine.Application.Common.Contracts
{
    public class SearchResponse
    {
        public string FilePath { get; set; } = string.Empty;
        public IReadOnlyDictionary<string, IReadOnlyCollection<string>> Highlights { get; set; } = new Dictionary<string, IReadOnlyCollection<string>>();
    }
}
