using System;
using System.Globalization;
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
}
