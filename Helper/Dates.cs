using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Helper
{
    public class Dates
    {

        public static string[] getNumberOfDaysInMonth()
        {
            int daysInMonth = System.DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month);
            string[] numberOfDaysInMonth = new string[daysInMonth];
            for (int day = 0; day < daysInMonth; day++)
            {
                numberOfDaysInMonth[day] = (day + 1).ToString();
            }

            return numberOfDaysInMonth;
        }

        public static string[] getNumberOfDaysInMonth(int year, int month)
        {
            int daysInMonth = System.DateTime.DaysInMonth(year, month);
            string[] numberOfDaysInMonth = new string[daysInMonth];
            for (int day = 0; day < daysInMonth; day++)
            {
                numberOfDaysInMonth[day] = (day + 1).ToString();
            }

            return numberOfDaysInMonth;
        }

        public static string[] GetNumberOfWeeksInYear()
        {
            int weeksinyear = 52;
            string[] NumberOfWeeksInYear = new string[52];
            for (int week = 0; week < weeksinyear; week++)
            {
                int _week = week + 1;
                NumberOfWeeksInYear[week] = _week.ToString();
            }

            return NumberOfWeeksInYear;
        }



        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public static string getMonthNameFromNumber (int monthNumber)
        {
            return monthNumber.ToString("MMM", CultureInfo.InvariantCulture);
        }



        public static string[] getMonthsInTheYear()
        {
   
            string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthGenitiveNames;

            return monthNames;
        }

        public static int getMonthNumberFromName(string monthName)
        {
            DateTime dt = DateTime.ParseExact(monthName, "MMM", CultureInfo.CurrentCulture);
            int month = dt.Month;
            return month;

        }

        public static string[] getMonthsInTheYearInt()
        {
            throw new NotImplementedException();
        }

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
