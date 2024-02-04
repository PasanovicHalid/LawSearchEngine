using Elastic.Clients.Elasticsearch;

namespace LawSearchEngine.Domain.Indexes.Common
{
    public abstract class IndexWithLocation
    {
        public GeoLocation Location { get; set; } = GeoLocation.Coordinates([0, 0]);
    }
}
