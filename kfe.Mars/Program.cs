using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace kfe.Mars
{
    /// <summary>
    /// Entry Point
    /// </summary>
    public class Program
    {

        /// <summary>
        /// Sub Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
               .Enrich.FromLogContext()
               .CreateLogger();


            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Setup webHost
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls($"http://*:5000")
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Verbose()
                        .WriteTo.RollingFile("/var/log/Mars/codex.log").CreateLogger();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddSerilog(dispose: true);
                    logging.AddDebug();
                })
                .UseKestrel(options => options.AddServerHeader = false)
                .Build();
    }
}
