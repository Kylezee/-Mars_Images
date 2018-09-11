using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;

namespace kfe.Infrastructure.Swagger
{
    public static class SwaggerInfo
    {

        public static Info GetSwaggerInfo(ApiVersionDescription description)
        {
            var info = new Info()
            {
                Title = "kfe.Services",
                Version = description.ApiVersion.ToString(),
                Description = $" {description.ApiVersion.ToString()} Documentation",
                Contact = new Contact()
                {
                    Name = "Kyle Foreman",
                    Url = string.Empty,
                    Email = "kyle@kyleforeman.com"
                }
            };

            return info;
        }

    }
}
