using FluentResults;
using LawSearchEngine.Application.WeatherForecast.Common.Contracts;
using LawSearchEngine.Application.WeatherForecast.Queries.GetForecast.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Application.WeatherForecast.Queries.GetForecast
{
    internal class GetWeatherForecastQueryHandler : IRequestHandler<GetWeatherForecastQuery, Result<GetWeatherForecastQueryResponse>>
    {
        public async Task<Result<GetWeatherForecastQueryResponse>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var forecasts = Enumerable.Range(1, request.Days).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(request.BottomTemperature, request.TopTemperature),
                Summaries[Random.Shared.Next(Summaries.Length)]
            ))
            .ToArray();

            return new GetWeatherForecastQueryResponse
            {
                WeatherForecasts = forecasts.Select(x => new WeatherForecastApplicationResponse
                {
                    Date = x.Date.ToDateTime(TimeOnly.MinValue),
                    Summary = x.Summary,
                    TemperatureC = x.TemperatureC,
                }).ToList(),
            };
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
        {
            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }
}
