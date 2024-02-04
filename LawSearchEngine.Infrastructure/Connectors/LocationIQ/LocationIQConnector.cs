using FluentResults;
using LawSearchEngine.Application.Common.Connectors;
using LawSearchEngine.Application.Common.Connectors.Contracts.LocationIQ.GetLocation;
using LawSearchEngine.Infrastructure.Configurations;
using LawSearchEngine.Infrastructure.Connectors.LocationIQ.Contracts.GetLocation;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LawSearchEngine.Infrastructure.Connectors.LocationIQ
{
    public class LocationIQConnector : ILocationIQConnector
    {
        private readonly LocationIQConfiguration _locationIQConfiguration;
        private readonly HttpClient _httpClient;

        public LocationIQConnector(IOptions<LocationIQConfiguration> locationIQConfiguration, HttpClient httpClient)
        {
            _locationIQConfiguration = locationIQConfiguration.Value;
            _httpClient = httpClient;
        }

        public async Task<Result<LocationIQResponse>> GetLocation(string location)
        {
            try
            {
                var requestUri = new UriBuilder(_locationIQConfiguration.Url)
                {
                    Query = $"key={_locationIQConfiguration.ApiKey}&q={location}&format=json",
                }.Uri.AbsoluteUri;

                var response = await _httpClient.GetAsync(requestUri);
                var content = await response.Content.ReadAsStringAsync();
                var serializedResponse = JsonConvert.DeserializeObject<List<GetLocationResponse>>(content);

                if (serializedResponse == null || serializedResponse.Count == 0)
                    return Result.Fail<LocationIQResponse>("Location not found");

                var result = new LocationIQResponse()
                {
                    Latitude = (double)serializedResponse[0].Latitude,
                    Longitude = (double)serializedResponse[0].Longitude,
                };

                if (result == null)
                    return Result.Fail<LocationIQResponse>("Location not found");

                return result;
            }
            catch (Exception)
            {
                return Result.Fail<LocationIQResponse>("Something failed getting the location");
            }
        }
    }
}
