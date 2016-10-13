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
}
