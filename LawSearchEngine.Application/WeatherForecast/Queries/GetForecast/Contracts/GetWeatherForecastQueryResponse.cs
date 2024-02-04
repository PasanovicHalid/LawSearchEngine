using LawSearchEngine.Application.WeatherForecast.Common.Contracts;

namespace LawSearchEngine.Application.WeatherForecast.Queries.GetForecast.Contracts
{
    public class GetWeatherForecastQueryResponse
    {
        public List<WeatherForecastApplicationResponse> WeatherForecasts { get; set; } = new List<WeatherForecastApplicationResponse>();
    }
}
