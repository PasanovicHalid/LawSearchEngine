using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Search;
using Elastic.Clients.Elasticsearch.IndexManagement;
using LawSearchEngine.Application.Common.Connectors;
using LawSearchEngine.Application.Common.Contracts.DocumentSearch;
using LawSearchEngine.Domain.Indexes;

namespace LawSearchEngine.Infrastructure.Connectors
{
    public class ElasticSearchConnector : IElasticSearchConnector
    {
        private readonly ElasticsearchClient _client;

        public ElasticSearchConnector(ElasticsearchClientSettings settings)
        {
            _client = new ElasticsearchClient(settings);

            CreateLawIndex();

            CreateContractIndex();
        }

        public bool IndexContract(ContractIndex index)
        {
            var result = _client.Index(index);
            return result.IsValidResponse;
        }

        public bool IndexLaw(LawIndex index)
        {
            var result = _client.Index(index);
            return result.IsValidResponse;
        }

        public async Task<IReadOnlyCollection<Hit<T>>> SearchAsync<T>(DocumentSearchRequest<T> request) where T : class
        {
            var result = await _client.SearchAsync<T>(x => x.Query(request.Query)
                                                            .Highlight(request.Highlight)
                                                     );

            return result.Hits;
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

        private void CreateContractIndex()
        {
            var contractIndexExists = _client.Indices.Exists("contracts");

            if (!contractIndexExists.Exists)
            {
                var createContractResponse = _client.Indices.Create("contracts", c => c
                                   .Mappings(ms => ms
                                                   .Properties<ContractIndex>(p => p
                                                        .Text(t => t.Contract, c => c.Analyzer("serbian"))
                                                        .Text(t => t.ContractPath)
                                                        .Text(t => t.GovernmentName, c => c.Analyzer("serbian"))
                                                        .Text(t => t.LevelOfGovernment, c => c.Analyzer("serbian"))
                                                        .Text(t => t.SignerName, c => c.Analyzer("serbian"))
                                                        .Text(t => t.SignerSurname, c => c.Analyzer("serbian"))
                                                        .GeoPoint(g => g.Location)
                                                   )
                                   ).Settings(SetupOfSerbianAnalyzer)
               );
            }
        }

        private void CreateLawIndex()
        {
            var lawIndexExists = _client.Indices.Exists("laws");

            if (!lawIndexExists.Exists)
            {
                var response = _client.Indices.Create("laws", c => c
                    .Mappings(ms => ms
                        .Properties<LawIndex>(p => p
                            .Text(t => t.Law, c => c.Analyzer("serbian"))
                            .Text(t => t.LawPath)
                        )
                    ).Settings(SetupOfSerbianAnalyzer)
                );
            }
        }

        private static void SetupOfSerbianAnalyzer(IndexSettingsDescriptor s)
        {
            s.Analysis(a => a
                .Analyzers(an => an
                   .Custom("serbian", c => c
                       .Tokenizer("standard")
                       .Filter([
                          "serbian_cyrillic_to_latinic",
                           "lowercase",
                           "icu_folding"
                       ])
                    )
                 )
                 .TokenFilters(tf => tf
                    .IcuTransform("serbian_cyrillic_to_latinic", c => c
                        .Id("Any-Latin; NFD; [:Nonspacing Mark:] Remove; NFC")
                    )
                 )
            );
        }
    }
}
