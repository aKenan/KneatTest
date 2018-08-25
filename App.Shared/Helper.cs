using System;
using System.Collections.Generic;
using System.Text;
using static App.Shared.Enumerations;

namespace App.Shared
{
    public static class Helper
    {
        const int yearInHours = 8760; //24*356
        const int monthInHours = 720;//24*30
        const int weekInHours = 168; //24*7
        const int dayInHours = 24;
        public static int CalculateValueToHours(int value, TimeEnum timeType)
        {
            switch (timeType)
            {
                case TimeEnum.YEAR:
                    return value * yearInHours;
                case TimeEnum.MONTH:
                    return value * monthInHours;
                case TimeEnum.WEEK:
                    return value * weekInHours;
                case TimeEnum.DAY:
                    return value * dayInHours;
                case TimeEnum.HOUR:
                    return value;
                default:
                    return 0;
            }
        }

        public static string GetErrorMessage(Exception ex, bool includeStackTrace = true)
        {
            var message = string.Empty;
            if(ex.InnerException != null)
            {
                message = $"Error message: ${ex.InnerException.Message}";
                if (includeStackTrace)
                    message += $"STACK TRACE: {ex.InnerException.StackTrace}";
            }
            else
            {
                message = $"Error message: ${ex.Message}";
                if (includeStackTrace)
                    message += $"STACK TRACE: {ex.StackTrace}";
            }

            return message;
        }
    }
}
