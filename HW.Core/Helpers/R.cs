//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Resources;
using System.Drawing;
using System.Globalization;
using System.Reflection;

namespace HW.Core.Helpers
{
	public static class R
	{
		static ResourceManager manager;

		static R()
		{
		}
		
		public static void SetResource(string resource)
		{
			manager = new ResourceManager(resource, Assembly.GetExecutingAssembly());
		}
		
		public static string Str(string key)
		{
			return Str(key, "");
		}
		
		public static string Str(string key, string def)
		{
			try {
				return manager.GetString(key, CultureInfo.CurrentCulture);
			} catch {
				return def;
			}
		}
		
		public static Image Img(string key)
		{
			return (Bitmap)manager.GetObject(key);
		}
	}
}
