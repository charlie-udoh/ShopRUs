using System;

namespace ShopRUs.Core.Helpers
{
    public static class DateHelper
    {
        public static double GetDateDifferenceInYears(DateTime date1, DateTime date2)
        {
            var days = (date2 - date1).Days;
            var years = days / 365.0;
            return years;
        }
    }
}
