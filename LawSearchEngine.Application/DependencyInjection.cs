using FluentValidation;
using LawSearchEngine.Application.Common.Behaviors.Logging;
using LawSearchEngine.Application.Common.Behaviors.Validation;
using LawSearchEngine.Application.Common.Services.Implementations;
using LawSearchEngine.Application.Common.Services.Interfaces;
using LawSearchEngine.Application.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace LawSearchEngine.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection SetupApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(conf =>
            {
                conf.Lifetime = ServiceLifetime.Scoped;
                conf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly)
                .AddOpenBehavior(typeof(LoggingBehavior<,>), ServiceLifetime.Scoped)
                .AddOpenBehavior(typeof(ValidationBehavior<,>), ServiceLifetime.Scoped);
            });

            services.AddScoped<IDocumentReader, DocumentReader>();

            SetupMinio(services, configuration);

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, ServiceLifetime.Singleton);

            return services;
        }

        private static void SetupMinio(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MinIOConfiguration>(configuration.GetSection("Minio"));

            MinIOConfiguration minIOConfiguration = configuration.GetSection("Minio").Get<MinIOConfiguration>()!;

            services.AddMinio(cs => cs.WithEndpoint(minIOConfiguration.Url)
                                      .WithSSL(secure: false)
                                      .WithCredentials(minIOConfiguration.AccessKey, minIOConfiguration.SecretKey));
        }
    }
}