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
        /// Calculates number of stops for number of megalights for each starship in StarWars, returns list of string
        /// </summary>
        /// <param name="mglt"></param>
        public async Task<List<Tuple<string , string>>> CalculateNumberOfStopsForEachStarship(int mglt)
        {
            #region log
            _logger.LogInformation($"Invoked CalculateNumberOfStopsForEachStarship, mglt: {mglt}");
            #endregion
            List<Tuple<string , string>> retList = new List<Tuple<string, string>>();

            try
            {
                var starships = await _starshipService.GetAllStarships();//get all starships
                #region log
                _logger.LogInformation($"{starships.Count} number of starship found, beggining calculation"); 
                #endregion

                var starshipsDetailed = starships.ToListStarshipDetailedModel();//map to detailed model

                foreach(var starshipDetailed in starshipsDetailed)
                {
                    string numberOfStops = string.IsNullOrEmpty(starshipDetailed.MGLT) || string.Equals(starshipDetailed.MGLT, "unknown") ?
                        "UNKNOWN" :
                        CalculateNumberOfStops(Convert.ToInt32(starshipDetailed.MGLT), starshipDetailed.ConsumablesValueInHours.Value, mglt).ToString();

                    retList.Add(new Tuple<string, string>(starshipDetailed.Name, numberOfStops));//add to return list
                }
                #region log
                _logger.LogInformation($"Successfully calculated!"); 
                #endregion
            }
            catch (Exception ex)
            {
                #region log
                _logger.LogError(GetErrorMessage(ex)); 
                #endregion
                throw ex;                
            }

            return retList;
        }
    }
}
