//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Text;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Helpers
{
	public class HtmlHelper
	{
		public HtmlHelper()
		{
		}
		
		public static string Anchor(string text, string url)
		{
			return Anchor(text, url, new Dictionary<string, string>());
		}
		
		public static string Anchor(string text, string url, Dictionary<string, string> attributes)
		{
			return string.Format("<a href='{1}'{2}>{0}</a>", text, url, GetAttributes(attributes));
		}
		
		public static string Image(string url)
		{
			return string.Format("<img src='{0}'/>", url);
		}
		
		public static string Image(string url, int width, int height)
		{
			return string.Format("<img src='{0}' width='{1}' height='{2}'/>", url, width, height);
		}
		
		public static string Input(string name, string value, string clas, string placeholder)
		{
			return string.Format("<input type='text' name='{0}' id='{0}' value='{1}' class='{2}' placeholder='{3}'/>", name, value, clas, placeholder);
		}
		
		public static string Password(string name, string value, string clas, string placeholder)
		{
			return string.Format("<input type='password' name='{0}' id='{0}' value='{1}' class='{2}' placeholder='{3}'/>", name, value, clas, placeholder);
		}

		static string GetAttributes(Dictionary<string, string> attributes)
		{
			StringBuilder s = new StringBuilder();
			foreach (var a in attributes.Keys) {
				s.Append(string.Format(" {0}='{1}'", a, attributes[a]));
			}
			return s.ToString();
		}
	}
}
