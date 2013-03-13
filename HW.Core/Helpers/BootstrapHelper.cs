//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;

namespace HW.Core.Helpers
{
	public class BootstrapHelper
	{
		public BootstrapHelper()
		{
		}
		
		public static string Anchor(string text, string url)
		{
			return Anchor(text, url, "");
		}
		
		public static string Anchor(string text, string url, string clas)
		{
			return string.Format("<a href='{1}'><i class='{2}'></i>{0}</a>", text, url, clas);
		}
	}
}
