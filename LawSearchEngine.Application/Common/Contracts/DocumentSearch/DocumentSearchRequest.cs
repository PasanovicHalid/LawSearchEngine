using Elastic.Clients.Elasticsearch.Core.Search;
using Elastic.Clients.Elasticsearch.QueryDsl;
using FluentValidation;

namespace LawSearchEngine.Application.Common.Contracts.DocumentSearch
{
    public abstract class DocumentSearchRequest<T>
    {
        public List<DocumentSearchItem> Search { get; set; } = new();

        public virtual QueryDescriptor<T> Query
        {
            get
            {
                QueryDescriptor<T> result = new();

                result.Bool(GenerateBooleanQueries);

                return result;
            }
        }

        protected void GenerateBooleanQueries(BoolQueryDescriptor<T> booleanQuery)
        {
            Dictionary<DocumentSearchOperation, List<DocumentSearchItem>> searchItems = ExtractAllSearchItems();

            foreach (var item in searchItems)
            {

                switch (item.Key)
                {
                    case DocumentSearchOperation.AND:
                        List<Action<QueryDescriptor<T>>> andActions = new();
                        foreach (var searchItem in item.Value)
                        {
                            andActions.Add(m =>
                            {
                                m.MatchPhrase(mp => mp.Field(searchItem.Field!).Query(searchItem.Value!));
                            });
                        }

                        booleanQuery.Must(andActions.ToArray());
                        continue;
                    case DocumentSearchOperation.OR:
                        List<Action<QueryDescriptor<T>>> orActions = new();
                        foreach (var searchItem in item.Value)
                        {
                            orActions.Add(m =>
                            {
                                m.MatchPhrase(mp => mp.Field(searchItem.Field!).Query(searchItem.Value!));
                            });
                        }

                        booleanQuery.Should(orActions.ToArray());
                        continue;
                    case DocumentSearchOperation.NOT:
                        List<Action<QueryDescriptor<T>>> notActions = new();
                        foreach (var searchItem in item.Value)
                        {
                            notActions.Add(m =>
                            {
                                m.MatchPhrase(mp => mp.Field(searchItem.Field!).Query(searchItem.Value!));
                            });
                        }

                        booleanQuery.MustNot(notActions.ToArray());
                        continue;
                }
            }
        }

        private Dictionary<DocumentSearchOperation, List<DocumentSearchItem>> ExtractAllSearchItems()
        {
            DocumentSearchOperation? operation = null;
            Dictionary<DocumentSearchOperation, List<DocumentSearchItem>> searchItems = new();
            foreach (var item in Search)
            {
                if (operation == null)
                {
                    searchItems.Add(DocumentSearchOperation.AND, new()
                            {
                                item,
                            });
                    operation = DocumentSearchOperation.AND;
                    continue;
                }

                if (item.IsOperation)
                {
                    operation = item.Operation;
                    continue;
                }

                if (item.IsSearch)
                {
                    if (!searchItems.TryGetValue(operation.Value, out var searchItem))
                    {
                        searchItems.Add(operation.Value, new()
                                {
                                    item,
                                });
                        continue;
                    }
                    searchItems[operation.Value].Add(item);
                }

            }

            return searchItems;
        }

        public abstract HighlightDescriptor<T> Highlight { get; }
    }

    public class DocumentSearchRequestValidator<T> : AbstractValidator<DocumentSearchRequest<T>>
    {
        public DocumentSearchRequestValidator()
        {
            RuleFor(x => x.Search).NotEmpty()
                              .WithMessage("Search should not be empty")
                                  .Must(CheckifSearchIsValid)
                              .WithMessage("Search needs to contain search request and operators between");
        }

        private static bool CheckifSearchIsValid(List<DocumentSearchItem> search)
        {
            for (var i = 0; i < search.Count; i++)
            {
                if (CheckIfElementIsNotASearchParameter(search[i], i))
                    return false;

                if (CheckIfElementIsNotAnOperation(search[i], i))
                    return false;
            }

            return true;
        }

        private static bool CheckIfElementIsNotAnOperation(DocumentSearchItem element, int position)
        {
            return position % 2 != 0 && !element.IsOperation;
        }

        private static bool CheckIfElementIsNotASearchParameter(DocumentSearchItem element, int position)
        {
            return position % 2 == 0 && !element.IsSearch;
        }
    }

}
