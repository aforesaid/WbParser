using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WbParser.Core.Services.Api.WbPublicApi;

namespace WbParser.Core
{
    class Program
    {
        // private static async Task Main()
        // {
        //     try
        //     {
        //         var config = new ConfigurationBuilder()
        //             .AddEnvironmentVariables()
        //             .AddUserSecrets<Program>()
        //             .Build();
        //         var services = new ServiceCollection();
        //
        //         Log.Logger = new LoggerConfiguration()
        //             .Enrich.WithProperty("APP", "WB-PARSER")
        //             .WriteTo.Console()
        //             .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
        //             .MinimumLevel.Override("IdentityServer4", Serilog.Events.LogEventLevel.Error)
        //             .WriteTo.Seq("http://seq:5341", Serilog.Events.LogEventLevel.Information)
        //             .CreateLogger();
        //
        //         services.AddLogging(l => l.AddSerilog());
        //         Log.Information("Starting");
        //         var host = new WbParserServiceHost(services, config);
        //         await host.Start();
        //         Log.Information("Started");
        //         Console.ReadLine();
        //     }
        //     catch (Exception ex)
        //     {
        //         Log.Fatal(ex, "Crashed");
        //         Log.CloseAndFlush();
        //         throw;
        //     }
        // }
            private static int currentCount = 0;
                private static SemaphoreSlim SemaphoreSlim = new(20);
                private static HttpClient _httpClient = new();
                static async Task Main()
                {
                    var modToCount = 0;
                    var request = "маска для сна BEZLLA";
                    var url = WbPublicEndpoints.EmulateQueryByText(request);
                    while (currentCount < 150000)
                    {
                        var tasks = new List<Task>();
                        var api = new WbPublicApiClient();
                        const int startCount = 100;
                        await CreateRequest(url);
                        for (var i = 0; i < startCount; i++)
                        {
                            tasks.Add(Task.Run(async () => await CreateRequest(url)));
                        }
            
                        await Task.WhenAll(tasks);
                        
                        // if (currentCount / 12000 != modToCount)
                        // {
                        //     modToCount++;
                        //     await Task.Delay(new Random().Next(10000, 25000));
                        // }
                        
                        Console.WriteLine(currentCount);
                    }
                }
            
                static async Task CreateRequest(string url)
                {
                    try
                    {
            
                        var request = await _httpClient.GetAsync(url);
                        if (request.IsSuccessStatusCode)
                            currentCount++;
            
            
                        //await Task.Delay(new Random().Next(1000,3000));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error");
                    }
                    finally
                    {
                    }
                }
            
                public void Dispose()
                {
                    _httpClient.Dispose();
                }
            }
            public class ApiResponseError
            {
                public IEnumerable<string> Messages { get; set; }
                public int Code { get; set; }
            
                public ApiResponseError(int code = 1, IEnumerable<string> messages = null)
                {
                    Messages = messages;
                    Code = code;
                }
            }
            
            public class ExcException : Exception
            {
                public ExcException() { }
                public ExcException(string message) : base(message) { }
            }
}
