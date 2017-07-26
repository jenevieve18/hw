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
using System.Configuration;
using System.Data;

namespace HW.Core.Helpers
{
	public static class DbHelper
	{
		//public static int GetInt32(SqlDataReader rs, int index, int def)
		//{
		//	return rs.IsDBNull(index) ? def : rs.GetInt32(index);
		//}
		
		//public static string GetString(SqlDataReader rs, int index, string def)
		//{
		//	return rs.IsDBNull(index) ? def : rs.GetString(index);
		//}
		
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


        /// <summary>
        /// Get the value of an index in sqldatareader and return as string
        /// </summary>
        /// <param name="rs">Sqldatareader object</param>
        /// <param name="index">field index</param>
        /// <returns></returns>
        public static string GetString(SqlDataReader rs, int index)
        {
            return GetString(rs, index, null);
        }

        /// <summary>
        /// Get the value of an index in sqldatareader with the default value of (NULL) if field is DBNULL and return as string
        /// </summary>
        /// <param name="rs">Sqldatareader object</param>
        /// <param name="index">field index</param>
        /// <param name="def">Default value</param>
        /// <returns></returns>
        public static string GetString(SqlDataReader rs, int index, string def)
        {
            return rs.IsDBNull(index) ? def : rs.GetString(index);
        }

        /// <summary>
        /// Get the value of an index in sqldatareader with the default value of (NULL) if field is DBNULL and return as string
        /// </summary>
        /// <param name="rs">Sqldatareader object</param>
        /// <param name="index">field index</param>
        /// <param name="check">checking value</param>
        /// <param name="def">default value</param>
        /// <returns></returns>
        public static string GetString(SqlDataReader rs, int index, string check, string def)
        {
            bool condition = GetString(rs, index, check) == check;
            return condition ? def : rs.GetString(index);
        }

        /// <summary>
        /// Get the value of an index in sqldatareader and return as integer
        /// </summary>
        /// <param name="rs">Sqldatareader object</param>
        /// <param name="index">field index</param>
        /// <returns></returns>
        public static int GetInt32(SqlDataReader rs, int index)
        {
            return GetInt32(rs, index, 0);
        }

        /// <summary>
        /// Get the value of an index in sqldatareader with the default value of (0) if field is DBNULL and return as integer
        /// </summary>
        /// <param name="rs">Sqldatareader object</param>
        /// <param name="index">field index</param>
        /// <param name="def">default value</param>
        /// <returns></returns>
        public static int GetInt32(SqlDataReader rs, int index, int def)
        {
            return rs.IsDBNull(index) ? def : rs.GetInt32(index);
        }

        /// <summary>
        /// Execute query string and return sqldatareader value.
        /// </summary>
        /// <param name="sqlString">Query String</param>
        /// <param name="con">Sql Connection</param>
        /// <returns></returns>
        public static SqlDataReader rs(string sqlString, string con)
        {
            SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[con].ConnectionString);
            dataConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlString, dataConnection);
            cmd.CommandTimeout = 900;
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dataReader;
        }

        public static SqlDataReader rs(string sqlString)
        {
            return rs(sqlString, "SqlConnection");
        }

        /// <summary>
        /// Function to execute query string
        /// </summary>
        /// <param name="sqlString">Query String</param>
        public static void exec(string sqlString)
        {
            exec(sqlString, "SqlConnection");
        }

        /// <summary>
        /// Function to execute query string 
        /// </summary>
        /// <param name="sqlString">Query String</param>
        /// <param name="con">Sql Connection</param>
        /// <returns></returns>
        public static void exec(string sqlString, string con)
        {
            SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[con].ConnectionString);
            dataConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlString, dataConnection);
            cmd.CommandTimeout = 900;
            cmd.ExecuteNonQuery();
            dataConnection.Close();
            dataConnection.Dispose();
        }

        /// <summary>
        /// Execute query string and return integer value with default value of (0)
        /// </summary>
        /// <param name="sqlString">Query String</param>
        /// <returns></returns>
        public static int execIntScal(string sqlString)
        {
            return execIntScal(sqlString, "SqlConnection", 0);
        }

        /// <summary>
        /// Execute query string and return integer value with default value of (default value is based on given value of defaultValue parameter)
        /// </summary>
        /// <param name="sqlString">Query String</param>
        /// <param name="con">Sql Connection</param>
        /// <param name="defaultValue">default value</param>
        /// <returns></returns>
        public static int execIntScal(string sqlString, string con, int defaultValue)
        {
            SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[con].ConnectionString);
            dataConnection.Open();
            SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
            object o = dataCommand.ExecuteScalar();
            //			int ret = 0;
            int ret = defaultValue;
            if (o != null)
                ret = Convert.ToInt32(o.ToString());
            dataConnection.Close();
            dataConnection.Dispose();
            return ret;
        }

        /// <summary>
        /// Execute query string and return string value 
        /// </summary>
        /// <param name="sqlString">Query String</param>
        /// <returns></returns>
        public static string execStrScal(string sqlString)
        {
            return execStrScal(sqlString, "SqlConnection");
        }

        /// <summary>
        /// Execute query string and return string value 
        /// </summary>
        /// <param name="sqlString">Query String</param>
        /// <param name="con">Sql Connection</param>
        /// <returns></returns>
        public static string execStrScal(string sqlString, string con)
        {
            SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[con].ConnectionString);
            dataConnection.Open();
            SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
            object o = dataCommand.ExecuteScalar();
            string ret = "";
            if (o != null)
                ret = o.ToString();
            dataConnection.Close();
            dataConnection.Dispose();
            return ret;
        }

    }

}
