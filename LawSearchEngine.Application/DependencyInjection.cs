using FluentValidation;
using LawSearchEngine.Application.Common.Behaviors.Logging;
using LawSearchEngine.Application.Common.Behaviors.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LawSearchEngine.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection SetupApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(conf =>
            {
                conf.Lifetime = ServiceLifetime.Scoped;
                conf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly)
                .AddOpenBehavior(typeof(LoggingBehavior<,>), ServiceLifetime.Scoped)
                .AddOpenBehavior(typeof(ValidationBehavior<,>), ServiceLifetime.Scoped);
            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, ServiceLifetime.Singleton);

            return services;
        }
    }
}