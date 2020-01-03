using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Users.API.Filters
{
    public class AddVersionHeader : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (swaggerDoc == null)
            {
                throw new ArgumentNullException(nameof(swaggerDoc));
            }

            swaggerDoc.Paths.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

            var version = swaggerDoc.Info.Version;

            foreach (var pathItem in swaggerDoc.Paths.Values)
            {
                TryAddVersionParamToDifferentOperations(pathItem, version);                
            }            
        }        

        private void TryAddVersionParamToDifferentOperations(OpenApiPathItem openApiPathItem, string version)
        {
            foreach (var operations in openApiPathItem.Operations.Keys)
            {
                switch (operations)
                {
                    case OperationType.Get:
                        TryAddVersionParamToSpecificOperation(openApiPathItem, version);
                        break;

                    case OperationType.Put:
                        TryAddVersionParamToSpecificOperation(openApiPathItem, version);
                        break;

                    case OperationType.Post:
                        TryAddVersionParamToSpecificOperation(openApiPathItem, version);
                        break;

                    case OperationType.Delete:
                        TryAddVersionParamToSpecificOperation(openApiPathItem, version);
                        break;

                    default:
                        throw new NotImplementedException("OperationType not implemented");
                }                                
            }            
        }

        private void TryAddVersionParamToSpecificOperation(OpenApiPathItem openApiPathItem, string version)
        {
            openApiPathItem.Parameters.Add(new OpenApiParameter
            {
                Name = "x-api-version",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString(version)                    
                }
            });
        }
    }
}
