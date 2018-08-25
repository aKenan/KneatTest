using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.StarshipModels
{
    public class StarshipModel
    {
        /// <summary>
        /// The Maximum number of Megalights this starship can travel in a standard hour. A "Megalight" is a standard unit of distance and has never been defined before within the Star Wars universe. 
        /// This figure is only really useful for measuring the difference in speed of starships. We can assume it is similar to AU, the distance between our Sun (Sol) and Earth.
        /// </summary>
        [JsonProperty("MGLT")]
        public string MGLT {get; set;}

        /// <summary>
        /// The maximum length of time that this starship can provide consumables for its entire crew without having to resupply.
        /// </summary>
        [JsonProperty("consumables")]
        public string Consumables { get; set; }
        /// <summary>
        /// The name of this starship
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
