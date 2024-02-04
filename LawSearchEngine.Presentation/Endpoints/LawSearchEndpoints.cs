using LawSearchEngine.Application.Common.Behaviors.Validation;
using LawSearchEngine.Application.Common.Contracts;
using LawSearchEngine.Application.Common.Contracts.DocumentSearch;
using LawSearchEngine.Application.Laws.Commands;
using LawSearchEngine.Application.Laws.Queries;
using LawSearchEngine.Presentation.Common.Contracts;
using LawSearchEngine.Presentation.Common.Extensions;
using LawSearchEngine.Presentation.Contracts.SearchLaw;
using LawSearchEngine.Presentation.Contracts.UploadLaw;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace LawSearchEngine.Presentation.Endpoints
{
    internal static class LawSearchEndpoints
    {
        public static WebApplication SetupLawSearchEndpoints(this WebApplication app)
        {
            var lawsApiBase = app.MapGroup("/api/laws")
                                    .WithTags("Laws")
                                    .WithOpenApi();

            lawsApiBase.MapPost("/new", UploadNewLaw)
                   .DisableAntiforgery()
                   .WithName("UploadNewLaw")
                   .WithDisplayName("Upload New Law")
                   .WithDescription("Upload a new law");

            lawsApiBase.MapPost("/search", SearchLaws)
                     .WithName("SearchLaws")
                     .WithDisplayName("Search Laws")
                     .WithDescription("Search laws");

            return app;
        }

        public static async Task<Results<Ok, ValidationProblem, BadRequest<BasicErrorResponse>>> UploadNewLaw([FromForm] UploadLawRequest request, ISender sender)
        {
            var response = await sender.Send(new UploadNewLawCommand
            {
                LawFile = new FileRequest
                (
                    fileName: request.Law.FileName,
                    fileExtension: request.Law.FileName.Split('.').Last(),
                    fileContent: await request.Law.GetBytes()
                )
            });

            if (response.IsFailed)
            {
                if (response.Errors[0] is ValidationError validationError)
                {
                    return TypedResults.ValidationProblem(validationError.MapValidationErrors(), title: "Upload New Law Request was invalid");
                }
                else
                {
                    return TypedResults.BadRequest(new BasicErrorResponse()
                    {
                        Title = response.Errors[0].Message,
                    });
                }

            }

            return TypedResults.Ok();
        }

        public static async Task<Results<Ok<List<SearchResponse>>, ValidationProblem, BadRequest<BasicErrorResponse>>> SearchLaws(SearchLawRequest request, ISender sender)
        {
            List<DocumentSearchItem> searchParameters = new();

            foreach (var item in request.Search)
            {
                var creationResult = DocumentSearchItem.Create(item);

                if (creationResult.IsFailed)
                {
                    ValidationError validationError = (ValidationError)creationResult.Errors[0];
                    return TypedResults.ValidationProblem(validationError.MapValidationErrors(), title: "Search Law Request was invalid");
                }

                searchParameters.Add(creationResult.Value);
            }

            var searchLawsRequest = new SearchLawQuery
            {
                Search = searchParameters,
            };

            var response = await sender.Send(searchLawsRequest);

            if (response.IsFailed)
            {
                ValidationError validationError = (ValidationError)response.Errors[0];
                return TypedResults.ValidationProblem(validationError.MapValidationErrors(), title: "Upload New law Request was invalid");
            }

            return TypedResults.Ok(response.Value);
        }
    }
}
