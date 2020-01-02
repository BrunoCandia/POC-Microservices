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

            swaggerDoc.Paths.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

            var version = swaggerDoc.Info.Version;

            foreach (var pathItem in swaggerDoc.Paths.Values)
            {
                TryAddVersionParamTo2(pathItem, version);
                //TryAddVersionParamTo2(pathItem, version);
                //TryAddVersionParamTo2(pathItem, version);
                //TryAddVersionParamTo2(pathItem, version);
            }

            //foreach (var pathItem in swaggerDoc.Paths.Values)
            //{
            //    TryAddVersionParamTo(pathItem.Get, version);
            //    TryAddVersionParamTo(pathItem.Post, version);
            //    TryAddVersionParamTo(pathItem.Put, version);
            //    TryAddVersionParamTo(pathItem.Delete, version);
            //}
        }

        //private void TryAddVersionParamTo(Operation operation, string version)
        //{
        //    if (operation == null) return;

        //    if (operation.Parameters == null)
        //        operation.Parameters = new List<IParameter>();

        //    operation.Parameters.Add(new NonBodyParameter
        //    {
        //        Name = "api-version",
        //        In = "header",
        //        Type = "string",
        //        Default = version,
        //    });
        //}

        private void TryAddVersionParamTo2(OpenApiPathItem openApiPathItem, string version)
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
