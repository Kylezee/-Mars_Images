using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using kfe.Infrastructure.Configuration.Models;
using System;
using System.Collections.Generic;

namespace kfe.Infrastructure.Swagger
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(
            this IServiceCollection services,
            IHostingEnvironment hostingEnvironment)
        {
            services.AddSwaggerGen(
                    options =>
                    {



                        var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                        foreach (var description in provider.ApiVersionDescriptions)
                        {
                            var info = SwaggerInfo.GetSwaggerInfo(description);
                            info.Description = description.IsDeprecated ? ": Deprecated" : string.Empty;

                            options.SwaggerDoc(description.GroupName, info);

                        }
                        options.OperationFilter<SwaggerDefaultValues>();

                        var xmlDocPath = Path.Combine(AppContext.BaseDirectory, $"{hostingEnvironment.ApplicationName}.xml");


                        if (File.Exists(xmlDocPath))
                        {
                            options.IncludeXmlComments(xmlDocPath);
                        }

                        options.DescribeAllEnumsAsStrings();

                        var security = new Dictionary<string, IEnumerable<string>>
                        {
                            { "Bearer", new string[] { }},
                        };

                        options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                        {
                            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                            Name = "Authorization",
                            In = "header",
                            Type = "apiKey"
                        });
                        options.AddSecurityRequirement(security);

                    });

            return services;
        }
        public static IApplicationBuilder UseSwaggerDocumentation(
            this IApplicationBuilder app,
            IApiVersionDescriptionProvider provider,
            IOptions<SwaggerConfiguration> swaggerSettings
            )
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            return app;
        }

    }
}
