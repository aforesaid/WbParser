using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace WbParser.Core
{
    class Program
    {
        private static async Task Main()
        {
            try
            {
                var config = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .AddUserSecrets<Program>()
                    .Build();
                var services = new ServiceCollection();

                Log.Logger = new LoggerConfiguration()
                    .Enrich.WithProperty("APP", "WB-PARSER")
                    .WriteTo.Console()
                    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                    .MinimumLevel.Override("IdentityServer4", Serilog.Events.LogEventLevel.Error)
                    .WriteTo.Seq("http://seq:5341", Serilog.Events.LogEventLevel.Information)
                    .CreateLogger();

                services.AddLogging(l => l.AddSerilog());
                Log.Information("Starting");
                var host = new WbParserServiceHost(services, config);
                await host.Start();
                Log.Information("Started");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Crashed");
                Log.CloseAndFlush();
                throw;
            }
        }
    }
}