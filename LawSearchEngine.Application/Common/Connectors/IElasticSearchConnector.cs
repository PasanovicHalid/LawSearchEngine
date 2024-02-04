using Elastic.Clients.Elasticsearch.Core.Search;
using LawSearchEngine.Application.Common.Contracts.DocumentSearch;
using LawSearchEngine.Domain.Indexes;

namespace LawSearchEngine.Application.Common.Connectors
{
    public interface IElasticSearchConnector
    {
        public bool IndexContract(ContractIndex index);
        public bool IndexLaw(LawIndex law);
        public Task<IReadOnlyCollection<Hit<T>>> SearchAsync<T>(DocumentSearchRequest<T> request) where T : class;

    }
}
