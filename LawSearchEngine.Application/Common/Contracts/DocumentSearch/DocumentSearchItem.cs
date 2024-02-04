using FluentResults;
using LawSearchEngine.Application.Common.Behaviors.Validation;
using LawSearchEngine.Domain.Common.ObjectTypes;
using System.Text.RegularExpressions;

namespace LawSearchEngine.Application.Common.Contracts.DocumentSearch
{
    public class DocumentSearchItem : Value
    {
        public DocumentSearchOperation? Operation { get; private set; }
        public string? Field { get; private set; }
        public string? Value { get; private set; }

        public bool IsOperation => Operation.HasValue;
        public bool IsSearch => Field != null && Value != null;

        protected DocumentSearchItem()
        {

        }

        public static Result<DocumentSearchItem> Create(string search)
        {
            if (CheckIfElementIsAOperationParameter(search))
            {
                return new DocumentSearchItem
                {
                    Operation = Enum.Parse<DocumentSearchOperation>(search)
                };
            }

            if (!CheckIfElementIsASearchParameter(search))
                return Result.Fail(new ValidationError("Validation Failed", new()
                {
                    new Error("Search").CausedBy("Search parameter is not valid.")
                }));

            var searchParameter = search.Split(':');
            var field = searchParameter[0];
            var value = searchParameter[1].Trim('\'');

            return new DocumentSearchItem
            {
                Field = field,
                Value = value
            };
        }
        private static bool CheckIfElementIsASearchParameter(string element)
        {
            if (string.IsNullOrEmpty(element))
                return false;

            var pattern = @"^\w+:'(.*?)'$";
            return Regex.IsMatch(element, pattern);
        }

        private static bool CheckIfElementIsAOperationParameter(string element)
        {
            return Enum.TryParse<DocumentSearchOperation>(element, out var operation);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Field;
            yield return Value;
        }
    }
}
