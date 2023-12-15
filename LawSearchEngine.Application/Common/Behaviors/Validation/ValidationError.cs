using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Application.Common.Behaviors.Validation
{
    public sealed class ValidationError : IError
    {
        public List<IError> Reasons { get; init; }

        public string Message { get; init; }

        public Dictionary<string, object> Metadata { get; init; } = new();

        public ValidationError(string message, List<IError> reasons)
        {
            Message = message;
            Reasons = reasons;
        }

        public ValidationError(string message, List<IError> reasons, Dictionary<string, object> metadata)
        {
            Message = message;
            Reasons = reasons;
            Metadata = metadata;
        }

        public Dictionary<string, string[]> MapValidationErrors()
        {
            var mappedErrors = new Dictionary<string, string[]>();

            foreach (var err in Reasons)
            {
                mappedErrors.Add(err.Message,
                                 err.Reasons.Select(x => x.Message).ToArray());
            }

            return mappedErrors;
        }
    }
}
