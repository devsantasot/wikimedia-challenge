using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

// This project use Dependency Injection, Serilog, Settings

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
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
                await serviceProvider.GetService<IAppService>().RunAsync();
                //Log.Information("Ending service");
                //return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Error running service");
                //return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

    }
}
