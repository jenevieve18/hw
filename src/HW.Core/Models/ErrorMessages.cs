using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Core.Models
{
	public class ErrorMessages : List<string>
	{
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var s in this) {
				sb.AppendLine(s);
			}
			return sb.ToString();
		}
		
		public string ToHtmlUl()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("<ul>");
			foreach (var s in this) {
				sb.AppendLine("<li>" + s + "</li>");
			}
			sb.AppendLine("</ul>");
			return sb.ToString();
		}
	}
}
