using Serilog;
using LawSearchEngine.Infrastructure;
using LawSearchEngine.Application;
using LawSearchEngine.Presentation;
using LawSearchEngine.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.SetupPresentationLayer()
                .SetupPersistanceLayer()
                .SetupInfrastructureLayer()
                .SetupApplicationLayer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.SetupEndpoints();

app.Run();
