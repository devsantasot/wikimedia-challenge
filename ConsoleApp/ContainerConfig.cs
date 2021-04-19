using DS_ProgramingChallengeLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace ConsoleApp
{
    public class ContainerConfig
    {
        public static void ConfigureLogger()
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }

        public static IHost ConfigureServices()
        {
            var host = Host.CreateDefaultBuilder()
              .ConfigureServices((context, serviceCollection) =>
              {
                  ConfigureServices(context.Configuration, serviceCollection);
              })
              .UseSerilog()
              .Build();

            return host;
        }


        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsetting.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }

        private static void ConfigureServices(IConfiguration configuration, IServiceCollection serviceCollection)
        {
            // ...
            serviceCollection.AddTransient<IAppService, AppService>();
            serviceCollection.AddTransient<IBusinessLogic, BusinessLogic>();
            serviceCollection.AddTransient<IDownloadHandler, DownloadHandler>();
            serviceCollection.AddTransient<IDecompressorHandler, DecompressorHandler>();
            serviceCollection.AddTransient<IFileParser, FileParser>();
            serviceCollection.AddTransient<IOutputResultParser, OutputResultParser>();
        }
    }
}
