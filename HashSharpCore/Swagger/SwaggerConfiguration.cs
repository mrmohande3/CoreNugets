using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace HashSharpCore.Swagger
{

    public static class SwaggerConfiguration
    {
        public static void AddCustomSwagger(this IServiceCollection services)
        {

            services.AddSwaggerGen(options =>
            {
                //var xmlDuc = Path.Combine(AppContext.BaseDirectory, "swaggerApi.xml");
                //options.IncludeXmlComments(xmlDuc,true);
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "iGarson Api Dacument",
                        Description = "iGarson api for clients that wana use",
                        License = new OpenApiLicense { Name = "Vira Safir Fanavar " },
                        Contact = new OpenApiContact
                        {
                            Name = "Amir Hossein Khademi",
                            Email = "avvampier@gmail.com",
                            Url = new Uri("http://amir-khademi.ir/")
                        }
                    });
                options.SwaggerDoc("v2",
                    new OpenApiInfo
                    {
                        Version = "v2",
                        Title = "iGarson Api Dacument",
                        Description = "iGarson api for clients that wana use",
                        License = new OpenApiLicense { Name = "Vira Safir Fanavar " },
                        Contact = new OpenApiContact
                        {
                            Name = "Amir Hossein Khademi",
                            Email = "avvampier@gmail.com",
                            Url = new Uri("http://amir-khademi.ir/")
                        }
                    });
                options.EnableAnnotations();
                options.DescribeAllParametersInCamelCase();
                options.IgnoreObsoleteActions();

                #region Versioning
                // Remove version parameter from all Operations
                options.OperationFilter<RemoveVersionParameters>();

                //set version "api/v{version}/[controller]" from current swagger doc verion
                options.DocumentFilter<SetVersionInPaths>();

                //Seperate and categorize end-points by doc version
                options.DocInclusionPredicate((version, desc) =>
                {

                    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions)
                        .ToList();

                    return versions.Any(v => $"v{v.ToString()}" == version);
                });
                #endregion

                #region Security

                string url = $"http://{Initializer.SiteSettings.BaseUrl}/api/v1/user/LoginSwagger";
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Scheme = "Bearer",
                    Name = "Bearer",
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri(url),
                        }
                    }
                });
                options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "Bearer");

                #endregion

                #region Customize

                options.OperationFilter<ApplySummariesOperationFilter>();

                #endregion

            });
        }

        public static void UseCustomSwaager(this IApplicationBuilder app)
        {

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.DocExpansion(DocExpansion.None);
                // Display
                options.DefaultModelExpandDepth(2);
                options.DefaultModelRendering(ModelRendering.Model);
                options.DefaultModelsExpandDepth(-1);
                options.DisplayOperationId();
                options.DisplayRequestDuration();
                options.EnableDeepLinking();
                options.EnableFilter();
                options.MaxDisplayedTags(5);
                options.ShowExtensions();

                options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "V2 Docs");
            });
        }
    }
}
