using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Services;
using App.Models.StarshipModels;
using App.Shared;
using System.Threading.Tasks;
using static App.Shared.Helper;

namespace App
{
    public class Worker
    {
        ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private StarshipService _starshipService;
        public Worker(ILogger<Worker> logger, IConfiguration configuration, StarshipService starshipService)
        {
            _logger = logger;
            _configuration = configuration;
            _starshipService = starshipService;
        }

        /// <summary>
        /// Calculates number of stops for number of megalights for each starship in StarWars
        /// </summary>
        /// <param name="mglt"></param>
        public async Task<List<string>> CalculateNumberOfStopsForEachStarship(int mglt)
        {
            _logger.LogInformation($"Invoked CalculateNumberOfStopsForEachStarship, mglt: {mglt}");
            List<string> calculated = new List<string>();

            try
            {
                var starships = await _starshipService.GetAllStarships();
                _logger.LogInformation($"{starships.Count} number of starship found, beggining calculation");

                var starshipsDetailed = starships.ToListStarshipDetailedModel();

                foreach(var starshipDetailed in starshipsDetailed)
                {
                    string numberOfStops = string.IsNullOrEmpty(starshipDetailed.MGLT) || string.Equals(starshipDetailed.MGLT, "unknown") ?
                        "UNKNOWN" :
                        CalculateNumberOfStops(Convert.ToInt32(starshipDetailed.MGLT), starshipDetailed.ConsumablesValueInHours.Value, mglt).ToString();

                    calculated.Add($"{starshipDetailed.Name} : {numberOfStops}");
                }
                _logger.LogInformation($"Successfully calculated!");
            }
            catch (Exception ex)
            {
                _logger.LogError(GetErrorMessage(ex));
                throw ex;                
            }

            return calculated;
        }
    }
}
