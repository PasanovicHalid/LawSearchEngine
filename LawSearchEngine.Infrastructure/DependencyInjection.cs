﻿using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using LawSearchEngine.Application.Common.Connectors;
using LawSearchEngine.Domain.Indexes;
using LawSearchEngine.Infrastructure.Configurations;
using LawSearchEngine.Infrastructure.Connectors;
using LawSearchEngine.Infrastructure.Connectors.LocationIQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;

namespace LawSearchEngine.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection SetupInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ElasticsearchConfiguration>(configuration.GetSection("Elasticsearch"));
            services.Configure<LocationIQConfiguration>(configuration.GetSection("LocationIQ"));

            services.SetupElasticsearch(configuration);

            services.AddOpenTelemetry()
                .WithTracing(builder =>
                {
                    builder.AddAspNetCoreInstrumentation()
                           .AddHttpClientInstrumentation();
                });

            services.AddScoped<ILocationIQConnector, LocationIQConnector>();

            services.AddHttpClient();

            return services;
        }

        public static IServiceCollection SetupElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            ElasticsearchConfiguration elasticsearchConfiguration = configuration.GetRequiredSection("Elasticsearch").Get<ElasticsearchConfiguration>()!;


            var settings = new ElasticsearchClientSettings(new Uri(elasticsearchConfiguration.Url))
                .DefaultMappingFor<LawIndex>(m => m
                                                  .IndexName("laws")
                                                  .IdProperty(x => x.Id)
                )
                .DefaultMappingFor<ContractIndex>(m => m
                                                       .IndexName("contracts")
                                                       .IdProperty(x => x.Id)
                )
                .ServerCertificateValidationCallback((sender, certificate, chain, errors) => true)
                .CertificateFingerprint(elasticsearchConfiguration.CertificateThumbprint)
                .EnableDebugMode()
                .Authentication(new BasicAuthentication(elasticsearchConfiguration.BasicAuthUsername, elasticsearchConfiguration.BasicAuthPassword));

            var elasticsearchConnector = new ElasticSearchConnector(settings);


            services.AddSingleton<IElasticSearchConnector>(elasticsearchConnector);

            return services;
        }
    }
}