using AutoMapper;
using kfe.Infrastructure.Configuration.Models;
using kfe.Infrastructure.Swagger;
using kfe.Mars.Repositories;
using kfe.Mars.Repositories.Interfaces;
using kfe.Mars.Services;
using kfe.Mars.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace kfe.Mars
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfigurationBuilder _builder;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        private static IConfigurationRoot Configuration;

        /// <summary>
        /// Startup Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="hostingEnvironment"></param>
        /// <param name="logger"></param>
        public Startup(IConfiguration configuration,
            IHostingEnvironment hostingEnvironment,
            Microsoft.Extensions.Logging.ILogger<Startup> logger
            )
        {
            _hostingEnvironment = hostingEnvironment;

            _logger = logger;

            _logger.LogInformation($"Start {nameof(Startup)}");

            try
            {

                _builder = new ConfigurationBuilder()
                    .SetBasePath(_hostingEnvironment.ContentRootPath)
                    .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true);

            }
            catch (Exception exception)
            {
                _logger.LogError($"Exception in {nameof(Startup)}:{exception.Message}");
            }

            _logger.LogInformation($"Exit {nameof(Startup)}");
            Configuration = _builder.Build();
        }

        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            _logger.LogInformation($"Starting {nameof(ConfigureServices)}");

            try
            {
                services.Configure<SwaggerConfiguration>
                    (options => Configuration.GetSection("Swagger").Bind(options));

                services.AddMvc();

                services.AddMvcCore().AddVersionedApiExplorer(
                    options =>
                    {
                        options.GroupNameFormat = "'v'VVV";
                        options.SubstituteApiVersionInUrl = true;
                    });

                services.AddApiVersioning(o => o.ReportApiVersions = true);


                services.AddMvc();

                services.AddSwaggerDocumentation(_hostingEnvironment);


                services.AddMvc();

                // this where the dependency injection will go.

                #region Transient Injections
                services.AddTransient<IMarsRepository, MarsRepository>();
                services.AddTransient<IImagingServices, ImagingServices>();

                #endregion


                services.AddAutoMapper();

                services.AddMvc();

            }
            catch (Exception exception)
            {
                _logger.LogError($"Exception in {nameof(ConfigureServices)}:{exception.Message}");
            }

            _logger.LogInformation($"Exit {nameof(ConfigureServices)}");
        }




        /// <summary>
        /// Configure Stuff
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="swaggerSettings"></param>
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IApiVersionDescriptionProvider provider,
            ILoggerFactory loggerFactory,
            IOptions<SwaggerConfiguration> swaggerSettings)
        {
            _logger.LogInformation($"Starting {nameof(Configure)}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            if (swaggerSettings.Value.Enabled)
            {

                app.UseSwaggerDocumentation(provider, swaggerSettings);

                app.UseMvc();
            }

            _logger.LogInformation($"Exit {nameof(Configure)}");
        }
    }
}
