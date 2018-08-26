using System;
using System.Collections.Generic;
using System.Text;
using static App.Shared.Enumerations;

namespace App.Shared
{
    public static class Helper
    {
        /// <summary>
        /// Calculates time value to hours
        /// </summary>
        /// <param name="value"></param>
        /// <param name="timeType"></param>
        /// <returns></returns>
        public static int CalculateValueToHours(int value, TimeEnum timeType)
        {
            switch (timeType)
            {
                case TimeEnum.YEAR:
                    return value * (int)TimeInHours.YEAR;
                case TimeEnum.MONTH:
                    return value * (int)TimeInHours.MONTH;
                case TimeEnum.WEEK:
                    return value * (int)TimeInHours.WEEK;
                case TimeEnum.DAY:
                    return value * (int)TimeInHours.DAY;
                case TimeEnum.HOUR:
                    return value;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Get error message from exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="includeStackTrace"></param>
        /// <returns></returns>
        public static string GetErrorMessage(Exception ex, bool includeStackTrace = true)
        {
            var message = string.Empty;
            if(ex.InnerException != null) // inner exception EXISTS
            {
                message = $"Error message: ${ex.InnerException.Message}";
                if (includeStackTrace)
                    message += $"STACK TRACE: {ex.InnerException.StackTrace}";
            } 
            else //inner exception DOES NOT exists
            {
                message = $"Error message: ${ex.Message}";
                if (includeStackTrace)
                    message += $"STACK TRACE: {ex.StackTrace}";
            }

            return message;
        }

        /// <summary>
        /// Calculates number of stops for single starship
        /// </summary>
        /// <param name="starship"></param>
        /// <param name="mglt"></param>
        /// <returns></returns>
        public static int CalculateNumberOfStops(int starshipMglt, int consumablesValueHours, int mglt)
        {
            double formulaValue = (double)mglt / (double)(consumablesValueHours * starshipMglt); //formula: inputMGLT / consumable value in hours * starshipMGLT

            return Convert.ToInt32(Math.Floor(formulaValue)); // round to lower value
        }
    }
}
