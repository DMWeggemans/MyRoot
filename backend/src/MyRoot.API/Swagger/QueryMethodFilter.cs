using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi;
using System.Reflection;
using System.Text.Json.Nodes;

namespace MyRoot.API.Swagger
{
    public class QueryMethodFilter : IDocumentFilter
    {
        private readonly IApiDescriptionGroupCollectionProvider ApiExplorer;

        public QueryMethodFilter(IApiDescriptionGroupCollectionProvider apiExplorer)
        {
            ApiExplorer = apiExplorer;
        }

        public void Apply(OpenApiDocument document, DocumentFilterContext context)
        {
            IEnumerable<ApiDescription> endpoints = ApiExplorer.ApiDescriptionGroups.Items
                .SelectMany(x => x.Items)
                .Where(x => string.Equals(x.HttpMethod, "QUERY", StringComparison.OrdinalIgnoreCase));

            foreach (ApiDescription description in endpoints)
            {
                string relativePath = $"/{description.RelativePath?.Trim('/')}";
                string tagName = description.ActionDescriptor.RouteValues["controller"] ?? "Unknown";

                document.Tags ??= new HashSet<OpenApiTag>();
                if (document.Tags.Any(x => x.Name == tagName))
                {
                    document.Tags.Add(new OpenApiTag()
                    {
                        Name = tagName
                    });
                }

                OpenApiOperation operation = new OpenApiOperation()
                {
                    Responses = new OpenApiResponses()
                    {
                        ["200"] = new OpenApiResponse()
                        {
                            Description = "Success"
                        }
                    },
                    Tags = new HashSet<OpenApiTagReference>()
                    {
                        new OpenApiTagReference(tagName, document)
                    }
                };

                ApiParameterDescription? provider = description.ParameterDescriptions.FirstOrDefault(x => x.Source.Id == "Body");
                if (provider is not null)
                {
                    operation.RequestBody = new OpenApiRequestBody()
                    {
                        Required = true,
                        Content = new Dictionary<string, OpenApiMediaType>()
                        {
                            ["application/json"] = new OpenApiMediaType()
                            {
                                Schema = context.SchemaGenerator.GenerateSchema(provider.Type, context.SchemaRepository)
                            }
                        }
                    };
                }

                operation.AddExtension("X-QUERY", new JsonNodeExtension(JsonValue.Create("QUERY")));

                if (!document.Paths.TryGetValue(relativePath, out IOpenApiPathItem? pathItem))
                {
                    pathItem = new OpenApiPathItem();
                    document.Paths[relativePath] = pathItem;
                }

                if (pathItem.Operations is null)
                {
                    PropertyInfo? property = pathItem.GetType().GetProperty("Operations");
                    if (property is not null && property.CanWrite)
                    {
                        property.SetValue(pathItem, new Dictionary<HttpMethod, OpenApiOperation>());
                    }
                }

                pathItem.Operations?.Add(HttpMethod.Get, operation);

                if (pathItem.Extensions is null)
                {
                    PropertyInfo? property = pathItem.GetType().GetProperty("Extensions");
                    if (property is not null && property.CanWrite)
                    {
                        property.SetValue(pathItem, new Dictionary<string, IOpenApiExtension>());
                    }
                }

                pathItem.Extensions?.Add("X-QUERY", new OpenApiOperationExtension(operation));
            }
        }
    }
}
