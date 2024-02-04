using Elastic.Clients.Elasticsearch;
using LawSearchEngine.Application.Common.Behaviors.Validation;
using LawSearchEngine.Application.Common.Connectors;
using LawSearchEngine.Application.Common.Contracts;
using LawSearchEngine.Application.Common.Contracts.DocumentSearch;
using LawSearchEngine.Application.Contracts.Commands;
using LawSearchEngine.Application.Contracts.Queries;
using LawSearchEngine.Presentation.Common.Contracts;
using LawSearchEngine.Presentation.Common.Extensions;
using LawSearchEngine.Presentation.Contracts.SearchContract;
using LawSearchEngine.Presentation.Contracts.UploadContract;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace LawSearchEngine.Presentation.Endpoints
{
    internal static class ContractSearchEndpoints
    {
        public static WebApplication SetupContractSearchEndpoints(this WebApplication app)
        {
            var contractApiBase = app.MapGroup("/api/contracts")
                                    .WithTags("Contracts")
                                    .WithOpenApi();

            contractApiBase.MapPost("/new", UploadNewContract)
                   .DisableAntiforgery()
                   .WithName("UploadNewContract")
                   .WithDisplayName("Upload New Contract")
                   .WithDescription("Upload a new contract");

            contractApiBase.MapPost("/search", SearchContracts)
                     .WithName("SearchContracts")
                     .WithDisplayName("Search Contracts")
                     .WithDescription("Search contracts");


            return app;
        }

        public static async Task<Results<Ok, ValidationProblem, BadRequest<BasicErrorResponse>>> UploadNewContract([FromForm] UploadContractRequest request, ISender sender)
        {
            var response = await sender.Send(new UploadNewContractCommand
            {
                ContractFile = new FileRequest
                (
                    fileName: request.Contract.FileName,
                    fileExtension: request.Contract.FileName.Split('.').Last(),
                    fileContent: await request.Contract.GetBytes()
                )
            });

            if (response.IsFailed)
            {
                if (response.Errors[0] is ValidationError validationError)
                {
                    return TypedResults.ValidationProblem(validationError.MapValidationErrors(), title: "Upload New Contract Request was invalid");
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

        public static async Task<Results<Ok<List<SearchResponse>>, ValidationProblem, BadRequest<BasicErrorResponse>>> SearchContracts(SearchContractRequest request, ISender sender, ILocationIQConnector locationIqConnector)
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

            var searchContractsRequest = new SearchContractQuery
            {
                Search = searchParameters,
            };

            if (!string.IsNullOrEmpty(request.Address) && request.Radius.HasValue)
            {
                var location = await locationIqConnector.GetLocation(request.Address);

                if (location.IsFailed)
                    return TypedResults.BadRequest(new BasicErrorResponse()
                    {
                        Title = location.Errors[0].Message,
                    });

                searchContractsRequest.Location = GeoLocation.Coordinates([location.Value.Longitude, location.Value.Latitude]);
                searchContractsRequest.Radius = request.Radius;
            }

            var response = await sender.Send(searchContractsRequest);

            if (response.IsFailed)
            {
                ValidationError validationError = (ValidationError)response.Errors[0];
                return TypedResults.ValidationProblem(validationError.MapValidationErrors(), title: "Upload New Contract Request was invalid");
            }

            return TypedResults.Ok(response.Value);
        }
    }
}
