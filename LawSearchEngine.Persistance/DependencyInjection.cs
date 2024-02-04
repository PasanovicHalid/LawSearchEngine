using LawSearchEngine.Application.Common.Repositories.Authentification;
using LawSearchEngine.Application.Common.Repositories;
using LawSearchEngine.Persistance.Context;
using LawSearchEngine.Persistance.Repositories.Authentification;
using LawSearchEngine.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LawSearchEngine.Application.Common.Repositories.Agencies;
using LawSearchEngine.Persistance.Repositories.Agencies;

namespace LawSearchEngine.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection SetupPersistanceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LawSearchEngineDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("LawSearchEngineDB"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGovernmentRepository, GovernmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();

            return services;
        }
    }
}