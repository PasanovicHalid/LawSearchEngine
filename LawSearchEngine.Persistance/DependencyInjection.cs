using Microsoft.Extensions.DependencyInjection;

namespace LawSearchEngine.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection SetupPersistanceLayer(this IServiceCollection services)
        {
            return services;
        }
    }
}