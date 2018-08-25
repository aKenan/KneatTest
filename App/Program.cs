using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;
using System;
using Services;

namespace App
{
    class Program
    {
        static IServiceProvider diProvider;
        static void Main(string[] args)
        {            
            Config();

            int mgltInt = int.MinValue;

            Console.WriteLine("Enter number of megalights(MGLT) or enter 'q' to quit: ");
            var mgltInput =  Console.ReadLine();

            while(mgltInput.Trim().ToLower() != "q")
            {
                if (!int.TryParse(mgltInput, out mgltInt))
                {
                    Console.WriteLine("Incorrect input, MGLT must be number! Try again or enter 'q' to quit: ");
                    mgltInput = Console.ReadLine();
                }
                else if (mgltInt < 0)
                {
                    Console.WriteLine("MGLT must be greather than 0! Try again or enter 'q' to quit: ");
                    mgltInput = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Please wait...");
                    try
                    {
                        var calculations = diProvider.GetRequiredService<Worker>().CalculateNumberOfStopsForEachStarship(mgltInt).Result;

                        Console.WriteLine("-------------------------------------------------------");

                        foreach (var calc in calculations)
                        {
                            Console.WriteLine(calc);
                        }

                        Console.WriteLine("-------------------------------------------------------");
                        Console.WriteLine("PRESS ANY KEY TO EXIT");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                    }
                    finally
                    {
                        Console.ReadLine();
                    }

                    return;
                }
            }
        }

        #region config

        private static void Config()
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            diProvider = services.BuildServiceProvider();

            var loggerFactory = diProvider.GetRequiredService<ILoggerFactory>();

            loggerFactory.AddNLog();
            loggerFactory.ConfigureNLog("nlog.config");
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false);

            IConfiguration config = configuration.Build();

            serviceCollection.AddSingleton(config);

            serviceCollection
                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddLogging(builder => builder.SetMinimumLevel(LogLevel.Trace))
                .AddScoped<Worker>()
                .AddTransient(s => new StarshipService(config.GetSection("API:URL").Value));
            
        }

#endregion
    }
}
