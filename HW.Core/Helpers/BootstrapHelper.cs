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
		
		public static string Anchor(string text, string url)
		{
			return Anchor(text, url, false, "");
		}
		
		public static string Anchor(string text, string url, bool random, string clas)
		{
			if (random) {
				url = string.Format("{0}?Rnd={1}", url, GetRandomInt());
				return string.Format("<a href='{1}'><i class='{2}'></i>{0}</a>", text, url, clas);
			} else {
				return string.Format("<a href='{1}'><i class='{2}'></i>{0}</a>", text, url, clas);
			}
		}
		
		static int GetRandomInt()
		{
			return (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
		}
		
		public static string DropDownList<T>(IList<T> lists, string clas)
		{
			StringBuilder s = new StringBuilder();
			s.Append(string.Format("<ul class='{0}'>", clas));
			foreach (var l in lists) {
				s.Append(string.Format("<li><a href=''>{0}</a></li>", l));
			}
			s.Append("</ul>");
			return s.ToString();
		}
	}
}
