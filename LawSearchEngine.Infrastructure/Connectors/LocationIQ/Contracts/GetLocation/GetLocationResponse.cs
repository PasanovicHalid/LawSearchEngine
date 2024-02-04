using Newtonsoft.Json;

namespace LawSearchEngine.Infrastructure.Connectors.LocationIQ.Contracts.GetLocation
{
    internal class GetLocationResponse
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }
        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }
}
