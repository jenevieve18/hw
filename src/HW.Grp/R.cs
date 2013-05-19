//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace HW.Grp
{
	public class R
	{
		static ResourceManager manager;

		static R()
		{
			manager = new ResourceManager("HW.Grp.Resources", Assembly.GetExecutingAssembly());
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
