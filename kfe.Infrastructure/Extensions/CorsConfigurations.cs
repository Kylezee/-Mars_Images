using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Cors.Infrastructure;
using kfe.Infrastructure.Configuration.Models;
using Microsoft.Extensions.Options;

namespace kfe.Infrastructure.Extensions
{
    public static class CorsConfigurations
    {
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors();
        }

        public static void EnableCorsMiddleware(this IApplicationBuilder app, string[] endPoints)
        {
            app.UseCors(builder =>
            builder.WithOrigins(endPoints)
            .AllowAnyHeader()
            .AllowAnyMethod());
        }

    }
}
