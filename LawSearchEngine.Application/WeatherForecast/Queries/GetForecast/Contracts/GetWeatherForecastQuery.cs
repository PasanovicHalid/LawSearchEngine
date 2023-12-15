using FluentResults;
using FluentValidation;
using LawSearchEngine.Application.Common.RequestTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Application.WeatherForecast.Queries.GetForecast.Contracts
{
    public class GetWeatherForecastQuery : IQuery<Result<GetWeatherForecastQueryResponse>>
    {
        public int Days { get; set; }
        public int BottomTemperature { get; set; }
        public int TopTemperature { get; set; }
    }

    public class GetWeatherForecastQueryValidator : AbstractValidator<GetWeatherForecastQuery>
    {
        public GetWeatherForecastQueryValidator()
        {
            RuleFor(x => x.Days).GreaterThan(0)
                .WithMessage("Number of Days must be greater than zero");
            RuleFor(x => x.BottomTemperature).GreaterThan(-273)
                .WithName("Bottom Temperature")
                .WithMessage("Temperature must be greater than absolute zero");
            RuleFor(x => x.TopTemperature).LessThan(100)
                .WithName("Top Temperature")
                .WithMessage("Temperature must be less than 100 degrees Celsius");
            RuleFor(x => x.BottomTemperature).LessThan(x => x.TopTemperature)
                .WithMessage("Temperature range must be valid");
        }
    }
}
