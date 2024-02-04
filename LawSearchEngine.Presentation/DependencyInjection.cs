using LawSearchEngine.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LawSearchEngine.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection SetupPresentationLayer(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            services.AddAntiforgery();


            return services;
        }

        public static WebApplication SetupEndpoints(this WebApplication app)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async (context) =>
                {
                    await Results.Problem(title: "Something went wrong! Please try again later.")
                                                           .ExecuteAsync(context);
                });
            });

            app.UseAntiforgery();

            app.SetupWeatherForecastEndpoints();
            app.SetupLawSearchEndpoints();
            app.SetupContractSearchEndpoints();
            app.SetupDocumentEndpointsEndpoints();

            return app;
        }
    }
}