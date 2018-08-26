using System;

namespace App.Shared
{
    public class Enumerations
    {
        /// <summary>
        /// Time enum: year, month...
        /// </summary>
        public enum TimeEnum
        {
            YEAR,
            MONTH,
            WEEK,
            DAY,
            HOUR,
            MINUTE,
            SECOND
        }

        /// <summary>
        /// contains how many hours contains one time type
        /// </summary>
        public enum TimeInHours
        {
            YEAR = 8760, //24*365
            MONTH = 720, //24*30
            WEEK = 168, //24*7
            DAY = 24
        }

    }
}
