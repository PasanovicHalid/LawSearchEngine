using Elastic.Clients.Elasticsearch;
using LawSearchEngine.Application.Common.Behaviors.Validation;
using LawSearchEngine.Application.Common.Connectors;
using LawSearchEngine.Application.Common.Contracts;
using LawSearchEngine.Application.Common.Contracts.DocumentSearch;
using LawSearchEngine.Application.Contracts.Commands;
using LawSearchEngine.Application.Contracts.Queries;
using LawSearchEngine.Presentation.Common.Contracts;
using LawSearchEngine.Presentation.Common.Extensions;
using LawSearchEngine.Presentation.Contracts.GetDocument;
using LawSearchEngine.Presentation.Contracts.SearchContract;
using LawSearchEngine.Presentation.Contracts.UploadContract;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;
using System.Text;

namespace LawSearchEngine.Presentation.Endpoints
{
    internal static class DocumentEndpoints
    {
        public static WebApplication SetupDocumentEndpointsEndpoints(this WebApplication app)
        {
            var documentApiBase = app.MapGroup("/api/documents")
                                    .WithTags("Documents")
                                    .WithOpenApi();

            documentApiBase.MapGet("/get", GetDocument)
                     .WithName("GetDocument")
                     .WithDisplayName("Get Document")
                     .WithDescription("Get a document");


            return app;
        }

        public static async Task<Results<FileContentHttpResult, BadRequest>> GetDocument([AsParameters] GetDocumentRequest request, IMinioClient minioClient)
        {
            var parameters = request.DocumentPath.Split('/');
            var fileLoaded = new byte[0];
            await minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(parameters[1])
                .WithObject(parameters[2])
                .WithCallbackStream((stream) =>
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        fileLoaded = ms.ToArray();
                    }
                }));

            return TypedResults.File(fileLoaded, "application/pdf", parameters[2]);
        }
    }
}
