//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Text;
using HW.Core.Models;

namespace HW.Core.Helpers
{
	public class BootstrapHelper
	{
		public BootstrapHelper()
		{
		}
		
		public static string Anchor(string text, string url, string aclas, string iclas)
		{
			return Anchor(text, url, aclas, iclas, false);
		}
		
		public static string Anchor(string text, string url, string aclas, string iclas, bool random)
		{
			if (random) {
				url = string.Format("{0}?Rnd={1}", url, GetRandomInt());
				return string.Format("<a href='{1}' class='{2}'><i class='{3}'></i>{0}</a>", text, url, aclas, iclas);
			} else {
				return string.Format("<a href='{1}' class='{2}'><i class='{3}'></i>{0}</a>", text, url, aclas, iclas);
			}
		}
		
		public static string DropDownList<T>(IList<T> lists, string clas)
		{
			StringBuilder s = new StringBuilder();
			s.AppendLine(string.Format("<ul class='{0}'>", clas));
			foreach (var l in lists) {
				s.AppendLine(string.Format("<li><a href=''>{0}</a></li>", l));
			}
			s.AppendLine("</ul>");
			return s.ToString();
		}
		
		public static string Button(string name, string value, string bclas, string iclas)
		{
			return string.Format("<button type='submit' id='{0}' name='{0}' class='{2}'><i class='{3}'></i>{1}</button>", name, value, bclas, iclas);
		}
		
		static int GetRandomInt()
		{
			return (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
		}
	}
}
