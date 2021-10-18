using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Extensions.Logging;

namespace NgTemplate.API
{
    public class Program
    {
        private static readonly LoggerProviderCollection Providers = new LoggerProviderCollection();
        public static void Main(string[] args)
        {
            var environmentName =
              Environment.GetEnvironmentVariable("ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Environment.CurrentDirectory)
                                    .AddJsonFile("appsettings.json", false)
                                    .AddJsonFile($"appsettings.{environmentName}.json", true)
                                    .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Providers(Providers)
                .CreateLogger();

            try
            {
                Log.Information($"Starting up in {environmentName}");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
              Host.CreateDefaultBuilder(args)
                .UseSerilog(providers: Providers)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
