using System;
using System.Globalization;
using System.Web;

namespace HW.Core.Helpers
{
	public static class ConvertHelper
	{
		public static int ToInt32If(bool condition, object val, int def)
		{
			if (condition) {
				return ToInt32(val, def);
			}
			return def;
		}
		
		public static int ToInt32(object val)
		{
			return ToInt32(val, 0);
		}
		
		public static int ToInt32(object val, int def)
		{
			try {
//				return Convert.ToInt32(val);
				return Int32.Parse(val.ToString());
			} catch {
				return def;
			}
		}
		
		public static bool ToBoolean(object val)
		{
			return ToBoolean(val, false);
		}
		
		public static bool ToBoolean(object val, bool def)
		{
			try {
				return bool.Parse(val.ToString());
			} catch {
				return def;
			}
		}
		
		public static DateTime ToDateTime(string val)
		{
			return ToDateTime(val, DateTime.Now);
		}

        public static DateTime ToDateTime(string val, DateTime def)
        {
            return ToDateTime(val, def, "yyyy-MM-dd");
        }
		
		public static DateTime ToDateTime(string val, DateTime def, string format)
		{
			try {
				DateTime dt;
				//if (DateTime.TryParseExact(val.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) {
                if (DateTime.TryParseExact(val.ToString(), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                {
					return dt;
				} else {
					return def;
				}
				
			} catch {
				return def;
			}
		}
		
		public static double ToDouble(object val)
		{
			return ToDouble(val, 0);
		}
		
		public static double ToDouble(bool condition, double x, double y)
		{
			return condition ? x : y;
		}
		
		public static double ToDouble(object val, double def)
		{
			try {
				return Convert.ToDouble(val);
			} catch {
				return def;
			}
		}
		
		public static decimal ToDecimal(object val)
		{
			return ToDecimal(val, 0);
		}
		
		public static decimal ToDecimal(object val, decimal def)
		{
			try {
				return Convert.ToDecimal(val);
//				return decimal.TryParse(val.ToString());
			} catch {
				return def;
			}
		}
		
		public static decimal ToDecimal(object val, decimal def, CultureInfo culture)
		{
			try {
				return Convert.ToDecimal(val, culture);
//				return decimal.TryParse(val.ToString());
			} catch {
				return def;
			}
		}
		
		public static decimal ToDecimal(object val, CultureInfo culture)
		{
			try {
				return Convert.ToDecimal(val, culture);
			} catch {
				return 0;
			}
		}
	}
}
