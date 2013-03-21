//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Web;
using HW.Core.Models;

namespace HW.Core.Helpers
{
	public static class HtmlHelper
	{
		public static string Anchor(string text, string url)
		{
			return Anchor(text, url, new Dictionary<string, string>());
		}
		
		public static string Hidden(string id, string val)
		{
			return string.Format("<input type='hidden' id='{0}' value='{1}'/>", id, val);
		}
		
		public static string Anchor(string text, string url, Dictionary<string, string> attributes)
		{
			return Anchor(text, url, false, attributes);
		}
		
		public static string Anchor(string text, string url, bool random, Dictionary<string, string> attributes)
		{
			if (random) {
				url = string.Format("{0}?Rnd={1}", url, GetRandomInt());
				return string.Format("<a href='{1}'{2}>{0}</a>", text, url, GetAttributes(attributes));
			} else {
				return string.Format("<a href='{1}'{2}>{0}</a>", text, url, GetAttributes(attributes));
			}
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
		
		public static string DropDownList<T>(IList<T> lists)
		{
			StringBuilder s = new StringBuilder();
			s.Append("<ul>");
			foreach (var l in lists) {
				s.Append(string.Format("<li>{0}</li>", l));
			}
			s.Append("</ul>");
			return s.ToString();
		}
		
		public static void RedirectIf(bool condition, string url)
		{
			RedirectIf(condition, url, false);
		}
		
		static int GetRandomInt()
		{
			return (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
		}
		
		public static void RedirectIf(bool condition, string url, bool random)
		{
			if (condition) {
				if (random) {
					HttpContext.Current.Response.Redirect(string.Format("{0}?Rnd={1}", url, GetRandomInt()), true);
				} else {
					HttpContext.Current.Response.Redirect(url, true);
				}
			}
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
