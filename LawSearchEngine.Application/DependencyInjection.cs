using FluentValidation;
using LawSearchEngine.Application.Common.Behaviors.Logging;
using LawSearchEngine.Application.Common.Behaviors.Validation;
using LawSearchEngine.Application.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using System;

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

            services.Configure<MinIOConfiguration>(configuration.GetSection("Minio"));

            MinIOConfiguration minIOConfiguration = configuration.GetSection("Minio").Get<MinIOConfiguration>()!;

            //services.AddMinio(cs => cs.WithEndpoint(minIOConfiguration.Url)
            //                          .WithCredentials(minIOConfiguration.AccessKey, minIOConfiguration.SecretKey));

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, ServiceLifetime.Singleton);

            return services;
        }
    }
}