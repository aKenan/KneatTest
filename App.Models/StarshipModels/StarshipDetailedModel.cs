using System;
using System.Collections.Generic;
using System.Text;
using static App.Shared.Enumerations;
using static App.Shared.Helper;

namespace App.Models.StarshipModels
{
    public class StarshipDetailedModel : StarshipModel
    {
        /// <summary>
        /// Consumables value
        /// </summary>
        public int? ConsumablesValue { get; set; }  
        /// <summary>
        /// Consumables time type: year, month...
        /// </summary>
        public TimeEnum? ConsumablesTimeType { get {
                if (Consumables.Contains("year"))
                    return TimeEnum.YEAR;
                else if (Consumables.Contains("month"))
                    return TimeEnum.MONTH;
                else if (Consumables.Contains("week"))
                    return TimeEnum.WEEK;
                else if (Consumables.Contains("day"))
                    return TimeEnum.DAY;
                else if (Consumables.Contains("hour"))
                    return TimeEnum.HOUR;
                else return TimeEnum.YEAR;
            }
        }

        /// <summary>
        /// Consumables value converted to hours
        /// </summary>
        public int? ConsumablesValueInHours { get {
                if (!this.ConsumablesValue.HasValue)
                    return null;
                return CalculateValueToHours(this.ConsumablesValue.Value, this.ConsumablesTimeType.Value);
            }
        }        
    }

    public static partial class Extensions
    {
        /// <summary>
        /// Maps StarshipModel to StarshipDetailedModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static StarshipDetailedModel ToStarshipDetailedModel(this StarshipModel model)
        {
            return new StarshipDetailedModel()
            {
                MGLT = model.MGLT,
                Name = model.Name,
                Consumables = model.Consumables,
                ConsumablesValue = !string.IsNullOrEmpty(model.Consumables) && !string.Equals(model.Consumables, "unknown") && model.Consumables.Split(' ').Length >= 1 ? Convert.ToInt32(model.Consumables.Split(' ')[0]) : (int?)null
            };
        }

        /// <summary>
        /// Maps List<StarshipModel> to List<StarshipDetailedModel>
        /// </summary>
        /// <param name="listModel"></param>
        /// <returns></returns>
        public static List<StarshipDetailedModel> ToListStarshipDetailedModel(this List<StarshipModel> listModel)
        {
            var retList = new List<StarshipDetailedModel>();

            foreach(var lm in listModel)
            {
                retList.Add(lm.ToStarshipDetailedModel());
            }

            return retList;
        }
    }
}
