using System;
using System.Collections.Generic;
using System.Linq;

namespace Maya.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime AddBusinessDays(this DateTime date, int days)
        {
            return date.GetDates(days < 0).Where(newDate => !newDate.IsWeekend()).Take(Math.Abs(days)).Last();
        }

        private static IEnumerable<DateTime> GetDates(this DateTime date, bool isForward)
        {
            while (true)
            {
                date = date.AddDays(isForward ? -1.0 : 1.0);
                yield return date;
            }
        }

        public static bool IsWeekend(this DateTime dateTime)
        {
            if (dateTime.DayOfWeek != DayOfWeek.Saturday)
                return dateTime.DayOfWeek == DayOfWeek.Sunday;
            return true;
        }

        public static bool IsHoliday(this DateTime date)
        {
            return GetHolidays(DateTime.Now.Year).Contains(date);
        }

        public static HashSet<DateTime> GetHolidays(int year)
        {
            var dateTimeSet = new HashSet<DateTime>();
            dateTimeSet.Add(AdjustForWeekendHoliday(new DateTime(year, 1, 1)));

            var dateTime1 = new DateTime(year, 5, 31);
            for (var dayOfWeek = dateTime1.DayOfWeek; dayOfWeek != DayOfWeek.Monday; dayOfWeek = dateTime1.DayOfWeek)
                dateTime1 = dateTime1.AddDays(-1.0);

            dateTimeSet.Add(dateTime1);

            var dateTime2 = AdjustForWeekendHoliday(new DateTime(year, 7, 4));
            dateTimeSet.Add(dateTime2);

            var dateTime3 = new DateTime(year, 9, 1);
            for (var dayOfWeek = dateTime3.DayOfWeek; dayOfWeek != DayOfWeek.Monday; dayOfWeek = dateTime3.DayOfWeek)
                dateTime3 = dateTime3.AddDays(1.0);

            dateTimeSet.Add(dateTime3);

            var day1 = Enumerable.Range(1, 30).Where(day => new DateTime(year, 11, day).DayOfWeek == DayOfWeek.Thursday).ElementAt(3);

            var dateTime4 = new DateTime(year, 11, day1);
            dateTimeSet.Add(dateTime4);

            var dateTime5 = AdjustForWeekendHoliday(new DateTime(year, 12, 25));
            dateTimeSet.Add(dateTime5);

            var dateTime6 = AdjustForWeekendHoliday(new DateTime(year + 1, 1, 1));
            if (dateTime6.Year == year)
                dateTimeSet.Add(dateTime6);

            return dateTimeSet;
        }

        private static DateTime AdjustForWeekendHoliday(DateTime holiday)
        {
            switch (holiday.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return holiday.AddDays(1.0);
                case DayOfWeek.Saturday:
                    return holiday.AddDays(-1.0);
                default:
                    return holiday;
            }
        }
    }
}
