// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.IO;
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

        public static void Write(object obj, HttpResponse reponse)
        {
            if (obj is MemoryStream)
            {
                reponse.BinaryWrite(((MemoryStream)obj).ToArray());
                reponse.End();
            }
            else if (obj is string)
            {
                reponse.Write((string)obj);
            }
        }

        public static void AddHeaderIf(bool condition, string name, string value, HttpResponse response)
        {
            if (condition)
            {
                response.AddHeader(name, value);
            }
        }
	}
}
