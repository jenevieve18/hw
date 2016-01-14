using System;

namespace HW.Core.Helpers
{
	public class DateHelper
	{
		public static double MonthDiff(DateTime d1, DateTime d2)
		{
			double months = (d2.Year - d1.Year) * 12;
			months -= d1.Month;
			months += d2.Month;
			months--;
			
			int daysOfMonth1 = GetDaysInMonth(d1.Month, d1.Year);
			double x1 = (daysOfMonth1 - d1.Day + 1) / (double)daysOfMonth1;
			months += x1;
			
			int daysOfMonth2 = GetDaysInMonth(d2.Month, d2.Year);
			double x2 = d2.Day / (double)daysOfMonth2;
			months += x2;
			
			return Math.Round(months * 100.0) / 100.0;
		}
		
		static int GetDaysInMonth(int month, int year)
		{
			//bool isLeapYear = year % 4 == 0 && (year % 100 != 00 && year % 400 == 0);
			int febDays = IsLeapYear(year) ? 29 : 28;
			int[] days = {
				31,
				febDays,
				31,
				30,
				31,
				30,
				31,
				31,
				30,
				31,
				30,
				31
			};
			return days[month - 1];
		}

        static bool IsLeapYear(int year)
        {
            return ((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0);
        }
	}
}
