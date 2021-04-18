using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

// This project use Dependency Injection, Serilog, Settings

namespace ConsoleAppTemplate
{
    class Program
    {
        static int Main(string[] args)
        {
            // Initialize serilog logger
            ContainerConfig.ConfigureLogger();

            // Create service collection
            Log.Information("Creating service collection");
            IHost host =  ContainerConfig.ConfigureServices();

            // Create service provider
            Log.Information("Building service provider");
            IServiceProvider serviceProvider = host.Services;

            try
            {
                Log.Information("Starting service");
                serviceProvider.GetService<IAppService>().Run();
                Log.Information("Ending service");
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Error running service");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

    }
}
