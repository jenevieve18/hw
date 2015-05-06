using System;
using System.Web;

namespace HW.Core.Helpers
{
	public static class SessionHelper
	{
		public static object Get(string name)
		{
			return HttpContext.Current.Session[name];
		}
		
		public static void RemoveIf(bool condition, string name)
		{
			if (condition) {
				HttpContext.Current.Session.Remove(name);
			}
		}
		
		public static void AddIf(bool condition, string name, object val)
		{
			if (condition) {
				HttpContext.Current.Session[name] = val;
			}
		}
	}
	
	public static class ConvertHelper
	{
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
		
		public static double ToDouble(object val)
		{
			return ToDouble(val, 0);
		}
		
		public static double ToDouble(object val, double def)
		{
			try {
				return Convert.ToDouble(val);
			} catch {
				return def;
			}
		}
	}
}
