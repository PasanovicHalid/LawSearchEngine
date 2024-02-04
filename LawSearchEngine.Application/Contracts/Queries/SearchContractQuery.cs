using Elastic.Clients.Elasticsearch.Core.Search;
using FluentResults;
using FluentValidation;
using LawSearchEngine.Application.Common.Connectors;
using LawSearchEngine.Application.Common.Contracts;
using LawSearchEngine.Application.Common.Contracts.DocumentSearch;
using LawSearchEngine.Application.Common.RequestTypes;
using LawSearchEngine.Domain.Indexes;
using MediatR;

namespace LawSearchEngine.Application.Contracts.Queries
{
    public class SearchContractQuery : DocumentSearchWithLocationRequest<ContractIndex>, IQuery<Result<List<SearchResponse>>>
    {
        public override HighlightDescriptor<ContractIndex> Highlight => new HighlightDescriptor<ContractIndex>().Fields(f =>
                                                               f.Add("contract", new HighlightField
                                                               {
                                                                   PreTags = new List<string> { "<b>" },
                                                                   PostTags = new List<string> { "</b>" },
                                                                   FragmentSize = 150,
                                                                   RequireFieldMatch = false
                                                               })
                                                         );
    }

    public class SearchContractQueryValidator : AbstractValidator<SearchContractQuery>
    {
        public SearchContractQueryValidator()
        {
            Include(new DocumentSearchWithLocationRequestValidator<ContractIndex>());
        }
    }

    public class SearchContractQueryHandler : IRequestHandler<SearchContractQuery, Result<List<SearchResponse>>>
    {
        private readonly IElasticSearchConnector _connector;

        public SearchContractQueryHandler(IElasticSearchConnector connector)
        {
            _connector = connector;
        }

        public async Task<Result<List<SearchResponse>>> Handle(SearchContractQuery request, CancellationToken cancellationToken)
        {
            var result = new List<SearchResponse>();

            var response = await _connector.SearchAsync(request);

            foreach (var hit in response)
            {
                result.Add(new SearchResponse
                {
                    FilePath = hit.Source?.ContractPath ?? "",
                    Highlights = hit.Highlight ?? new Dictionary<string, IReadOnlyCollection<string>>(),
                });
            }

            return result;
        }
    }
}
