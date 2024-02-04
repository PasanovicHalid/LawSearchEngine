using FluentResults;
using LawSearchEngine.Application.Common.Behaviors.Validation;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

            _logger.LogInformation("Handling {Request} with data {Data}", requestName, JsonConvert.SerializeObject(request));

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
                _logger.LogWarning("{Request} failed with validation errors {Errors}", requestName, JsonConvert.SerializeObject(validationError.MapValidationErrors()));
            }
            else
            {
                _logger.LogWarning("{Request} failed with error {Error}", requestName, JsonConvert.SerializeObject(error));
            }
        }
    }
}
