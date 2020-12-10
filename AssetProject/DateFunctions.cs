using System;
using System.Collections.Generic;
using System.Text;

namespace EFAssets
{
    public class DateFunctions
    {
        // Returns difference in months between two dates
        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;

            return Math.Abs(monthsApart);
        }

        // Calculates End Of Life date for a given date and lifespan in months
        public static DateTime CalculateEolDate(DateTime startDate, int lifeSpan)
        {
            DateTime eolDate;
            eolDate = startDate.AddMonths(lifeSpan);
            return eolDate;
        }

        // Calculates End Of Life warning date based on startdate and a warning level in months
        public static DateTime CalculateWarningDate(DateTime startDate, int warningLevel)
        {
            DateTime warningDate;
            warningDate = startDate.AddMonths(warningLevel);
            return warningDate;
        }
    }
}
