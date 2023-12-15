using FluentResults;
using FluentValidation;
using LawSearchEngine.Application.Common.Behaviors.Validation;
using MediatR;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LawSearchEngine.Application.Common.Behaviors.Logging
{
    internal class LoggingBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : ResultBase, new()
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request,
                                      RequestHandlerDelegate<TResponse> next,
                                      CancellationToken cancellationToken)
        {
            string requestName = request.GetType().Name;

            _logger.LogInformation("Handling {Request} with data {Data}", requestName, JsonSerializer.Serialize(request));

            TResponse response;

            response = await next();

            if (response.IsFailed)
                response.Errors.ForEach(error => LogExpectedErrors(requestName, error));

            _logger.LogInformation("Finish handling {Request}", requestName);

            return response;
        }

        private void LogExpectedErrors(string requestName, IError error)
        {
            if (error is ValidationError validationError)
            {
                _logger.LogWarning("{Request} failed with validation errors {Errors}", requestName, JsonSerializer.Serialize(validationError.MapValidationErrors()));
            }
            else
            {
                _logger.LogWarning("{Request} failed with error {Error}", requestName, JsonSerializer.Serialize(error));
            }
        }
    }
}
