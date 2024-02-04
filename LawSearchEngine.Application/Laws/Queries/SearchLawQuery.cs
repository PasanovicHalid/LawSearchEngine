using Elastic.Clients.Elasticsearch.Core.Search;
using FluentResults;
using FluentValidation;
using LawSearchEngine.Application.Common.Connectors;
using LawSearchEngine.Application.Common.Contracts;
using LawSearchEngine.Application.Common.Contracts.DocumentSearch;
using LawSearchEngine.Application.Common.RequestTypes;
using LawSearchEngine.Domain.Indexes;
using MediatR;

namespace LawSearchEngine.Application.Laws.Queries
{
    public class SearchLawQuery : DocumentSearchRequest<LawIndex>, IQuery<Result<List<SearchResponse>>>
    {
        public override HighlightDescriptor<LawIndex> Highlight => new HighlightDescriptor<LawIndex>().Fields(f =>
                                                               f.Add("law", new HighlightField
                                                               {
                                                                   PreTags = new List<string> { "<b>" },
                                                                   PostTags = new List<string> { "</b>" },
                                                                   FragmentSize = 150,
                                                                   RequireFieldMatch = false,
                                                               })
                                                         );
    }

    public class SearchLawQueryValidator : AbstractValidator<SearchLawQuery>
    {
        public SearchLawQueryValidator()
        {
            Include(new DocumentSearchRequestValidator<LawIndex>());
        }
    }

    public class SearchLawQueryHandler : IRequestHandler<SearchLawQuery, Result<List<SearchResponse>>>
    {
        private readonly IElasticSearchConnector _connector;

        public SearchLawQueryHandler(IElasticSearchConnector connector)
        {
            _connector = connector;
        }

        public async Task<Result<List<SearchResponse>>> Handle(SearchLawQuery request, CancellationToken cancellationToken)
        {
            var result = new List<SearchResponse>();

            var response = await _connector.SearchAsync(request);

            foreach (var hit in response)
            {
                result.Add(new SearchResponse
                {
                    FilePath = hit.Source?.LawPath ?? "",
                    Highlights = hit.Highlight ?? new Dictionary<string, IReadOnlyCollection<string>>(),
                });
            }

            return result;
        }
    }
}
