using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Damacon.DAL.Utils
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }

        public static List<Tuple<int, int>> GetWeeksBetweenDateRange(DateTime startDate, DateTime endDate)
        {
            List<Tuple<int, int>> weekToYear = new List<Tuple<int, int>>();
            for (var currentDate = startDate; currentDate < endDate; currentDate = currentDate.AddDays(1))
            {
                var weekNo = GetWeekFromDate(currentDate);
                int year = currentDate.Month == 1 && weekNo > 50 ? currentDate.Year - 1 : currentDate.Year;
                if (!weekToYear.Any(x => x.Item1 == weekNo && x.Item2 == currentDate.Year))
                {
                    weekToYear.Add(new Tuple<int, int>(weekNo, year));
                }
            }
            return weekToYear;
        }

        public static List<Tuple<int, int>> GetWeeksOfTheMonth(int year, int month)
        {
            DateTime fromDate = new DateTime(year, month, 1);
            DateTime toDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            if (fromDate.DayOfWeek == DayOfWeek.Friday || fromDate.DayOfWeek == DayOfWeek.Saturday || fromDate.DayOfWeek == DayOfWeek.Sunday)
            {
                while (fromDate.DayOfWeek != DayOfWeek.Monday)
                {
                    fromDate = fromDate.AddDays(1);
                }
            }

            if (toDate.DayOfWeek < DayOfWeek.Thursday)
            {
                while (toDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    toDate = toDate.AddDays(-1);
                }
            }

            List<Tuple<int, int>> weekToYear = GetWeeksBetweenDateRange(fromDate, toDate);
            return weekToYear;
        }

        public static int GetWeekFromDate(DateTime dateTime)
        {
            var currentCulture = CultureInfo.CurrentCulture;
            return currentCulture.Calendar.GetWeekOfYear(
                                      dateTime,
                                      currentCulture.DateTimeFormat.CalendarWeekRule,
                                      DayOfWeek.Monday);
        }
    }
}
