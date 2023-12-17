using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Analysis;
using Elastic.Clients.Elasticsearch.AsyncSearch;
using Elastic.Clients.Elasticsearch.Core.Reindex;
using Elastic.Clients.Elasticsearch.Core.Search;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.Mapping;
using LawSearchEngine.Application.Common.Connectors;
using LawSearchEngine.Domain.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Infrastructure.Connectors
{
    public class ElasticSearchConnector : IElasticSearchConnector
    {
        private readonly ElasticsearchClient _client;

        public ElasticSearchConnector(ElasticsearchClientSettings settings)
        {
            _client = new ElasticsearchClient(settings);

            var result = _client.Indices.Exists("laws");

            if (!result.Exists)
            {
                _client.Indices.Create("laws");
            }
        }

        public Task<bool> IndexContractAsync(string signerName, string signerSurname, string governmentName, string levelOfGovernment, string contract, double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IndexLawAsync(string law)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> SearchLawsAsync(string query)
        {
            var result = _client.Search<LawIndex>(x => x
                                           .Query(q =>
                                                  q.Match(m =>
                                                          m.Field("law")
                                                          .Query(query)
                                                          .Analyzer("serbian")
                                                          )
                                                  )
                                           .Highlight(h =>
                                                      h.Fields(f =>
                                                               f.Add("law", new HighlightField
                                                               {
                                                                   PreTags = new List<string> { "<b>" },
                                                                   PostTags = new List<string> { "</b>" },
                                                                   FragmentSize = 150,
                                                               })
                                                              )
                                                      )
                                           );



            return Task.FromResult(result as dynamic);
        }

        public Task<dynamic> SearchContractsAsync(string signerName, string signerSurname, string governmentName, string levelOfGovernment, string contract, double longitude, double latitude)
        {
            var result = _client.Search<ContractIndex>(x => x
                                           .Query(q =>
                                                  q.Match(m =>
                                                          m.Field("contract")
                                                          .Query(contract)
                                                          .Analyzer("serbian")
                                                          )
                                                  .GeoDistance(g =>
                                                               g.Field(f => f.Location)
                                                               .DistanceType(GeoDistanceType.Arc)
                                                               .Location(GeoLocation.Coordinates([44.7866, 20.4489]))
                                                               .Distance("100km")
                                                               )
                                                  )
                                           .Highlight(h =>
                                                      h.Fields(f =>
                                                               f.Add("contract", new HighlightField
                                                               {
                                                                   PreTags = new List<string> { "<b>" },
                                                                   PostTags = new List<string> { "</b>" },
                                                                   FragmentSize = 150,
                                                               })
                                                              )
                                                      )
                                           );



            return Task.FromResult(result as dynamic);
        }
    }
}
