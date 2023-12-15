using LawSearchEngine.Application.WeatherForecast.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Application.WeatherForecast.Queries.GetForecast.Contracts
{
    public class GetWeatherForecastQueryResponse
    {
        public List<WeatherForecastApplicationResponse> WeatherForecasts { get; set; } = new List<WeatherForecastApplicationResponse>();
    }
}
