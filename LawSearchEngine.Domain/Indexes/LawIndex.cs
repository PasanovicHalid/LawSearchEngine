namespace LawSearchEngine.Domain.Indexes
{
    public class LawIndex
    {
        public Guid Id { get; set; }
        public string Law { get; set; } = string.Empty;
        public string LawPath { get; set; } = string.Empty;
    }
}
