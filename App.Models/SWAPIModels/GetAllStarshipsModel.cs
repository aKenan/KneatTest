using App.Models.StarshipModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.SWAPIModels
{
    public class GetAllStarshipsModel
    {
        /// <summary>
        /// Total number of starships
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }
        /// <summary>
        /// Next page API URL
        /// </summary>
        [JsonProperty("next")]
        public string Next { get; set; }
        /// <summary>
        /// Previous page API URL
        /// </summary>
        [JsonProperty("previous")]
        public string Previous { get; set; }

        /// <summary>
        /// Starships
        /// </summary>
        [JsonProperty("results")]
        public List<StarshipModel> Starships { get; set; }
    }
}
