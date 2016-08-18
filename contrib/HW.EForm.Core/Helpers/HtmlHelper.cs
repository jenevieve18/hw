// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;

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
	}
}
