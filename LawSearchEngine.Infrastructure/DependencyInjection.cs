using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;

namespace LawSearchEngine.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection SetupInfrastructureLayer(this IServiceCollection services)
        {
            services.AddOpenTelemetry()
                .WithTracing(builder =>
                {
                    builder.AddAspNetCoreInstrumentation()
                           .AddHttpClientInstrumentation();
                });

            return services;
        }
    }
}