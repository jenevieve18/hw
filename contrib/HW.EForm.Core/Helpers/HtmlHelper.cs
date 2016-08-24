// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Web;

namespace HW.EForm.Core.Helpers
{
	public static class HtmlHelper
	{
		public static string Anchor(string text, string url)
		{
			return Anchor(text, url, "");
		}
		
		public static string Anchor(string text, string url, string attributes)
		{
			return string.Format("<a href='{1}' {2}>{0}</a>", text, url, attributes);
		}
		
		public static void RedirectIf(bool condition, string url)
		{
			if (condition) {
				HttpContext.Current.Response.Redirect(url, true);
			}
		}
	}
}
