using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace HWgrp.Web
{
	public static class R
	{
		static ResourceManager manager;

		static R()
		{
		}
		
		public static void SetResource(string resource)
		{
			manager = new ResourceManager("HWgrp.Web.Resources", Assembly.GetExecutingAssembly());
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