//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace HW.Grp
{
	public class ReminderHelper
	{
//		static Dictionary<int, string> loginDays = new Dictionary<int, string>();
//		static Dictionary<int, string> loginWeekdays = new Dictionary<int, string>();
		static int langID;
		
		public static void SetLanguageID(int langID)
		{
			ReminderHelper.langID = langID;
		}
		
		static ReminderHelper()
		{
//			loginDays.Add(1, R.Str(langID, "day.everyday", "every day"));
//			loginDays.Add(7, R.Str(langID, "week", "week"));
//			loginDays.Add(14, R.Str(langID, "week.two", "2 weeks"));
//			loginDays.Add(30, R.Str(langID, "month", "month"));
//			loginDays.Add(90, R.Str(langID, "month.three", "3 months"));
//			loginDays.Add(100, R.Str(langID, "month.six", "6 months"));
			
//			loginWeekdays.Add(-1, R.Str(langID, "week.disabled", "< disabled >"));
//			loginWeekdays.Add(0, R.Str(langID, "week.everyday", "< every day >"));
//			loginWeekdays.Add(1, R.Str(langID, "week.monday", "Monday"));
//			loginWeekdays.Add(2, R.Str(langID, "week.tuesday", "Tuesday"));
//			loginWeekdays.Add(3, R.Str(langID, "week.wednesday", "Wednesday"));
//			loginWeekdays.Add(4, R.Str(langID, "week.thursday", "Thursday"));
//			loginWeekdays.Add(5, R.Str(langID, "week.friday", "Friday"));
		}
		
		public static Dictionary<int, string> GetLoginDays()
		{
			Dictionary<int, string> loginDays = new Dictionary<int, string>();
			
			loginDays.Add(1, R.Str(langID, "day.everyday", "every day"));
			loginDays.Add(7, R.Str(langID, "week", "week"));
			loginDays.Add(14, R.Str(langID, "week.two", "2 weeks"));
			loginDays.Add(30, R.Str(langID, "month", "month"));
			loginDays.Add(90, R.Str(langID, "month.three", "3 months"));
			loginDays.Add(100, R.Str(langID, "month.six", "6 months"));
			
			return loginDays;
		}
		
		public static Dictionary<int, string> GetLoginWeekdays()
		{
			Dictionary<int, string> loginWeekdays = new Dictionary<int, string>();
			
			loginWeekdays.Add(-1, R.Str(langID, "week.disabled", "< disabled >"));
			loginWeekdays.Add(0, R.Str(langID, "week.everyday", "< every day >"));
			loginWeekdays.Add(1, R.Str(langID, "week.monday", "Monday"));
			loginWeekdays.Add(2, R.Str(langID, "week.tuesday", "Tuesday"));
			loginWeekdays.Add(3, R.Str(langID, "week.wednesday", "Wednesday"));
			loginWeekdays.Add(4, R.Str(langID, "week.thursday", "Thursday"));
			loginWeekdays.Add(5, R.Str(langID, "week.friday", "Friday"));
			
			return loginWeekdays;
		}
	}
	
	public class R
	{
        static readonly string English = "HW.Grp.Properties.en-US";
        static readonly string Swedish = "HW.Grp.Properties.sv-SE";
        const int EnglishID = 2;
        const int SwedishID = 1;
        const int DefaultID = SwedishID;

        static string GetLangID(int langID)
        {
            switch (langID)
            {
                case EnglishID:
                    return English;
                case SwedishID:
                    return Swedish;
                default:
                    return English;
            }
        }
        
        public static string Str(int langID, string name)
        {
            return Str(langID, name, "");
        }

        public static string Str(int langID, string name, string def)
        {
            try
            {
                ResourceManager resource = new ResourceManager(GetLangID(langID), Assembly.GetExecutingAssembly());
                return resource.GetString(name).ToString();
            }
            catch (Exception ex)
            {
                return def;
            }
        }

        public static string Str(string name, string def)
        {
            return Str(DefaultID, name, def);
        }

        public static string Str(string name)
        {
            return Str(name, "");
        }
	}
}
