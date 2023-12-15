using FluentResults;
using LawSearchEngine.Application.Common.Behaviors.Validation;
using LawSearchEngine.Application.WeatherForecast.Queries.GetForecast.Contracts;
using LawSearchEngine.Presentation.Contracts.GetWeatherForecast;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Presentation.Endpoints
{
    internal static class WeatherForecastEndpoints
    {
        public static WebApplication SetupWeatherForecastEndpoints(this WebApplication app)
        {
            var weatherApiBase = app.MapGroup("/api/weather")
                                    .WithTags("Weather")
                                    .WithOpenApi();

            weatherApiBase.MapGet("/", GenerateRandomForecast)
                   .WithName("GetWeatherForecast")
                   .WithDisplayName("Get Weather Forecast")
                   .WithDescription("Get a list of weather forecasts");

            return app;
        }

        public static async Task<Results<Ok<GetWeatherForecastQueryResponse>, ValidationProblem>> GenerateRandomForecast([AsParameters] GetWeatherForecastRequest request, ISender sender)
        {
            var response = await sender.Send(new GetWeatherForecastQuery
            {
                Days = request.Days,
                BottomTemperature = request.BottomTemperature,
                TopTemperature = request.TopTemperature,
            });

            if (response.IsFailed)
            {
                ValidationError validationError = (ValidationError)response.Errors[0];
                return TypedResults.ValidationProblem(validationError.MapValidationErrors(), title: "Get Weather forecast request is invalid");
            }

            return TypedResults.Ok(response.Value);
        }

    }
}
