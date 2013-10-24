using System;

namespace HW.Core.Helpers
{
	public class ConvertHelper
	{
		public ConvertHelper()
		{
		}
		
		public static int ToInt32(object val)
		{
			return ToInt32(val, 0);
		}
		
		public static int ToInt32(object val, int def)
		{
			try {
				return Convert.ToInt32(val);
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
