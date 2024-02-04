using LawSearchEngine.Application;
using LawSearchEngine.Infrastructure;
using LawSearchEngine.Persistance;
using LawSearchEngine.Persistance.Context;
using LawSearchEngine.Presentation;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.SetupPresentationLayer()
                .SetupPersistanceLayer(builder.Configuration)
                .SetupInfrastructureLayer(builder.Configuration)
                .SetupApplicationLayer(builder.Configuration);

builder.Services.AddCors(o => o.AddPolicy("All", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

await using var scope = app.Services.CreateAsyncScope();
using var db = scope.ServiceProvider.GetRequiredService<LawSearchEngineDbContext>();
await db.Database.MigrateAsync();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("All");

app.SetupEndpoints();

app.Run();
