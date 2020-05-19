﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HashSharpCore.Swagger
{

    public class UnauthorizedResponsesOperationFilter : IOperationFilter
    {
        private readonly bool includeUnauthorizedAndForbiddenResponses;
        private readonly string schemeName;

        public UnauthorizedResponsesOperationFilter(bool includeUnauthorizedAndForbiddenResponses, string schemeName = "Bearer")
        {
            this.includeUnauthorizedAndForbiddenResponses = includeUnauthorizedAndForbiddenResponses;
            this.schemeName = schemeName;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var filters = context.ApiDescription.ActionDescriptor.FilterDescriptors;

            var hasAnynomousEndPoint = context.ApiDescription.ActionDescriptor.EndpointMetadata.Any(e =>
                e.GetType() == typeof(Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute));

            //var hasAnonymous = filters.Any(p => p.Filter is AllowAnonymousFilter);
            if (hasAnynomousEndPoint)
                return;

            /*var hasAuthorize = filters.Any(p => p.Filter is AuthorizeFilter);
            if (!hasAuthorize)
                return;*/

            if (includeUnauthorizedAndForbiddenResponses)
            {
                operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });
            }

            operation.Security.Add(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}

                }
            });
        }
    }
}
