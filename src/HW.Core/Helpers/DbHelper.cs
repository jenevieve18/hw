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
}
