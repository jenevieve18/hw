using System;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using HW.Core.Models;

namespace HW.Core.Helpers
{
	public static class DbHelper
	{
		public static int GetInt32(SqlDataReader rs, int index)
		{
			return GetInt32(rs, index, 0);
		}
		
		public static int GetInt32(SqlDataReader rs, int index, int def)
		{
			return rs.IsDBNull(index) ? def : rs.GetInt32(index);
		}
		
		public static string GetString(SqlDataReader rs, int index)
		{
			return GetString(rs, index, "");
		}
		
		public static string GetString(SqlDataReader rs, int index, string def)
		{
			return rs.IsDBNull(index) ? def : rs.GetString(index);
		}
		
		public static DateTime GetDateTime(SqlDataReader rs, int index)
		{
			return GetDateTime(rs, index, DateTime.MaxValue);
		}
		
		public static DateTime GetDateTime(SqlDataReader rs, int index, DateTime def)
		{
			return rs.IsDBNull(index) ? def : rs.GetDateTime(index);
		}
		
		public static Guid GetGuid(SqlDataReader rs, int index)
		{
			return rs.IsDBNull(index) ? new Guid() : rs.GetGuid(index);
		}
		
		public static Guid? GetGuid(SqlDataReader rs, int index, Guid def)
		{
			return rs.IsDBNull(index) ? def : rs.GetGuid(index);
		}
	}
	
	public static class StrHelper
	{
		public static string Str(bool condition, string x, string y)
		{
			return condition ? x : y;
		}

        public static string Str2(string s, int length)
        {
            if (s != null && s != "" && s.Length > length) {
                return s.Substring(0, length) + "...";
            } else {
                return s;
            }
        }
        
        public static string Str3(string x, string y)
        {
        	return x != null ? x : y;
        }
		
		public static string Clean(this string s, params string[] x)
		{
			string y = s;
			foreach (var z in x) {
				y = y.Replace(z, "");
			}
			return y;
		}
		
		public static string MD5Hash(string text)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			
			// Compute hash from the bytes of text
			md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
			
			// Get hash result after compute it
			byte[] result = md5.Hash;
			
			StringBuilder strBuilder = new StringBuilder();
			for (int i = 0; i < result.Length; i++) {
				// Change it into 2 hexadecimal digits
				// for each byte
				strBuilder.Append(result[i].ToString("x2"));
			}
			
			return strBuilder.ToString();
		}
	}
	
	public static class HtmlHelper
	{
		public static void SetTextIfEmpty(TextBox textBox, string text)
		{
			if (textBox.Text == "") {
				textBox.Text = text;
			}
		}
		
		public static string ToHtml(string text)
		{
			return text.Replace("<", "&lt;").Replace(">", "&gt;");
		}
		
		public static string Anchor(string text, string url)
		{
			return Anchor(text, url, "");
		}
		
		public static string Anchor(string text, string url, string attributes)
		{
			return Anchor(text, url, attributes, false);
		}
		
		public static string Anchor(string text, string url, string attributes, bool random)
		{
			if (random) {
				url = string.Format("{0}?Rnd={1}", url, GetRandomInt());
				return string.Format("<a href='{0}' {1}>{2}</a>", url, attributes, text);
			} else {
				return string.Format("<a href='{0}' {1}>{2}</a>", url, attributes, text);
			}
		}
		
		public static string AnchorSpan(string text, string url)
		{
			return AnchorSpan(text, url, "");
		}
		
		public static string AnchorSpan(string text, string url, string attributes)
		{
			return AnchorSpan(text, url, attributes, false);
		}
		
		public static string AnchorSpan(string text, string url, string attributes, bool random)
		{
			if (random) {
				url = string.Format("{0}?Rnd={1}", url, GetRandomInt());
				return string.Format("<a href='{0}' {1}><span>{2}</span></a>", url, attributes, text);
			} else {
				return string.Format("<a href='{0}' {1}><span>{2}</span></a>", url, attributes, text);
			}
		}
		
		public static string AnchorImage(string url, string image)
		{
			return AnchorImage(url, image, false);
		}

		public static string AnchorImage(string url, string image, string attributes)
		{
			return string.Format("<a href='{0}' {2}><img src='{1}'></a>", url, image, attributes);
		}
		
		public static string AnchorImage(string url, string image, bool random)
		{
			if (random) {
				return string.Format("<a href='{0}&Rnd={2}'><img src='{1}'></a>", url, image, GetRandomInt());
			}
			return string.Format("<a href='{0}'><img src='{1}'></a>", url, image);
		}
		
		public static string Image(string url)
		{
			return Image(url, "");
		}
		
		public static string Image(string url, string alt)
		{
			return string.Format("<img src='{0}' alt='{1}'/>", url, alt);
		}
		
		public static string Image(string url, string alt, int width, int height)
		{
			return string.Format("<img src='{0}' width='{1}' height='{2}' alt='{3}'/>", url, width, height, alt);
		}
		
		public static string DropDownList<T>(IList<T> lists)
		{
			StringBuilder s = new StringBuilder();
			s.AppendLine("<ul>");
			foreach (var l in lists) {
				s.AppendLine(string.Format("<li>{0}</li>", l));
			}
			s.AppendLine("</ul>");
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

		public static void Redirect(string url, bool random)
		{
			if (random) {
				HttpContext.Current.Response.Redirect(string.Format("{0}?Rnd={1}", url, GetRandomInt()), true);
			} else {
				HttpContext.Current.Response.Redirect(url, true);
			}
		}

		public static void Write(object obj, HttpResponse reponse)
		{
			if (obj is MemoryStream) {
				reponse.BinaryWrite(((MemoryStream)obj).ToArray());
				reponse.End();
			} else if (obj is string) {
				reponse.Write((string)obj);
			}
		}
		
		public static void AddHeaderIf(bool condition, string name, string value, HttpResponse response)
		{
			if (condition) {
				response.AddHeader(name, value);
			}
		}
	}
	
	public class FormHelper
	{
		public static string OpenForm(string action)
		{
			return string.Format("<form method='post' action='{0}'>", action);
		}
		
		public static string DropdownList<T>(string name, IList<T> lists)
		{
			return DropdownList<T>(name, lists, "");
		}
		
		public static string DropdownList<T>(string name, IList<T> lists, string clas)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(string.Format("<select name='{0}' class='{1}'>", name, clas));
			foreach (var l in lists) {
				sb.AppendLine(string.Format("<option value='{0}'>{1}</option>", (l as BaseModel).Id, l));
			}
			sb.AppendLine("</select>");
			return sb.ToString();
		}
		
		public static string CheckBox(string name, string value, bool disabled)
		{
			return string.Format("<input type='checkbox' name='{0}' value='{1}' {2}/>", name, value, disabled ? "disabled" : "");
		}
		
		public static string Input(string name)
		{
			return Input(name, "");
		}
		
		public static string Input(string name, string value)
		{
			return Input(name, value, "");
		}

		public static string Input(string name, string value, string attributes)
		{
			return string.Format("<input type='text' name='{0}' id='{0}' value='{1}' {2}/>", name, value, attributes);
		}
		
		//public static string Input(string name, string value, string placeholder)
		//{
		//    return Input(name, value, placeholder, "");
		//}
		
		//public static string Input(string name, string value, string placeholder, string clas)
		//{
		//    return string.Format("<input type='text' name='{0}' id='{0}' value='{1}' class='{3}' placeholder='{2}'/>", name, value, placeholder, clas);
		//}
		
		public static string TextArea(string name, string value)
		{
			return TextArea(name, value, "");
		}
		
		public static string TextArea(string name, string value, string clas)
		{
			return string.Format("<textarea name='{0}' id='{0}' class='{2}'>{1}</textarea>", name, value, clas);
		}
		
		public static string Password(string name, string value)
		{
			return Password(name, value, "");
		}

		public static string Password(string name, string value, string attributes)
		{
			return string.Format("<input type='password' name='{0}' id='{0}' value='{1}' {2}/>", name, value, attributes);
		}
		
		//public static string Password(string name, string value, string placeholder)
		//{
		//    return Password(name, value, placeholder, "");
		//}
		
		//public static string Password(string name, string value, string placeholder, string clas)
		//{
		//    return string.Format("<input type='password' name='{0}' id='{0}' value='{1}' class='{3}' placeholder='{2}'/>", name, value, placeholder, clas);
		//}
		
		public static string Hidden(string id, string val)
		{
			return string.Format("<input type='hidden' id='{0}' value='{1}'/>", id, val);
		}
		
		public static string Submit(string name, string value)
		{
			return string.Format("<input type='submit' id='{0}' name='{0}' value='{1}'/>", name, value);
		}
	}
	
	public class P
	{
		public string Name { get; set; }
		public string Path { get; set; }
		public Q Q { get; set; }
		
		public P(string name) : this("", name)
		{
		}
		
		public P(string path, string name)
		{
			this.Path = path;
			this.Name = name;
			this.Q = new Q();
		}
		
		public override string ToString()
		{
			StringBuilder s = new StringBuilder();
			s.Append(Path);
			s.Append(Name);
			s.Append("?");
			s.Append(Q.ToString());
			return s.ToString();
		}
		
		public void Add(Q q)
		{
			foreach (var k in q.Keys) {
				Q.Add(k, q[k]);
			}
		}
	}
	
	public class Q : Dictionary<string, object>
	{
		public Q()
		{
		}
		
		public void AddIf(bool condition, string key, object val)
		{
			if (condition) {
				Add(key, val);
			}
		}
		
		public override string ToString()
		{
			StringBuilder s = new StringBuilder();
			int i = 1;
			foreach (var k in Keys) {
				s.Append(string.Format("{0}={1}", k, this[k]));
				if (i++ < this.Count) {
					s.Append("&");
				}
			}
			return s.ToString();
		}
	}
}
