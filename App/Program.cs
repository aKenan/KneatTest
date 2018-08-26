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
            Config(); //configurate .net CORE

            //basic data
            Console.Title = "StarWars";            
            Console.WriteLine("Welcome");
            Console.WriteLine("This application calculates number of required stops for each starship in starwars for inserted number of megalights.");

            int mgltInt = int.MinValue;

            Console.WriteLine("Enter number of megalights(MGLT) or enter 'q' to quit: ");
            var mgltInput =  Console.ReadLine();//input value

            while(mgltInput.Trim().ToLower() != "q")
            {
                //validations
                if (!int.TryParse(mgltInput, out mgltInt)) //check if input is integer number
                {
                    Console.WriteLine("Incorrect input, MGLT must be number! Try again or enter 'q' to quit: ");
                    mgltInput = Console.ReadLine();
                }
                else if (mgltInt < 0)//check if number is greather than 0
                {
                    Console.WriteLine("MGLT must be greather than 0! Try again or enter 'q' to quit: ");
                    mgltInput = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Please wait...");
                    try
                    {
                        var calculations = diProvider.GetRequiredService<Worker>().CalculateNumberOfStopsForEachStarship(mgltInt).Result; //calculate

                        Console.WriteLine("-------------------------------------------------------");

                        foreach (var calc in calculations) // write all records
                        {
                            Console.WriteLine($"{calc.Item1}: {calc.Item2}"); 
                        }

                        Console.WriteLine("-------------------------------------------------------");
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message); //error has occured
                    }
                    finally
                    {
                        Console.WriteLine("PRESS ANY KEY TO EXIT");
                        Console.ReadLine();
                    }

                    return;//end
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
