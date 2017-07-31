using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace HW.Core.Helpers
{
	public static class DbHelper
	{
		public static string HashMd5(string str)
		{
			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] hashByteArray = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes("HW" + str + "HW"));
			string hash = "";
			for (int i = 0; i < hashByteArray.Length; i++) {
				hash += hashByteArray[i];
			}
			return hash;
		}
		
		public static bool GetBoolean(SqlDataReader reader, int index)
		{
			return reader.IsDBNull(index) ? false : reader.GetBoolean(index);
		}
		
		public static double GetDouble(SqlDataReader reader, int index)
		{
			return GetDouble(reader, index, 0);
		}
		
		public static double GetDouble(SqlDataReader reader, int index, double defaultValue)
		{
			return reader.IsDBNull(index) ? defaultValue : reader.GetDouble(index);
		}
		
		/// <summary>
		/// Returns a date time from a data reader.
		/// </summary>
		/// <param name="reader">SQL data reader</param>
		/// <param name="index">Index to which the data is referred to from a data reader</param>
		/// <returns>Date time value from a data reader given the index</returns>
		public static DateTime GetDateTime(SqlDataReader reader, int index)
		{
			return GetDateTime(reader, index, DateTime.MaxValue);
		}
		
		/// <summary>
		/// Returns a date time from a data reader.
		/// </summary>
		/// <param name="reader">SQL data reader</param>
		/// <param name="index">Index to which the data is referred to from a data reader</param>
		/// <param name="defaultValue">Returns this value if index in the reader is null</param>
		/// <returns>Date time value from a data reader given the index</returns>
		public static DateTime GetDateTime(SqlDataReader reader, int index, DateTime defaultValue)
		{
			return reader.IsDBNull(index) ? defaultValue : reader.GetDateTime(index);
		}
		
		/// <summary>
		/// Returns a GUID field in the SQL data reader.
		/// </summary>
		/// <param name="reader">SQL data reader</param>
		/// <param name="index">Index to which the data is referred to from a data reader</param>
		/// <returns>GUID value from a data reader given the index</returns>
		public static Guid GetGuid(SqlDataReader reader, int index)
		{
			return reader.IsDBNull(index) ? new Guid() : reader.GetGuid(index);
		}
		
		/// <summary>
		/// Returns a GUID field in the SQL data reader.
		/// </summary>
		/// <param name="reader">SQL data reader</param>
		/// <param name="index">Index to which the data is referred to from a data reader</param>
		/// <param name="defaultValue">Default value to be returned when data reader returns null</param>
		/// <returns>Returns GUID value from a data reader given the index</returns>
		public static Guid? GetGuid(SqlDataReader reader, int index, Guid defaultValue)
		{
			return reader.IsDBNull(index) ? defaultValue : reader.GetGuid(index);
		}

		/// <summary>
		/// Get the value of an index in sqldatareader and return as string
		/// </summary>
		/// <param name="rs">SQL data reader object</param>
		/// <param name="index">Field index</param>
		/// <returns>String value from data reader given the index</returns>
		public static string GetString(SqlDataReader rs, int index)
		{
			return GetString(rs, index, null);
		}

		/// <summary>
		/// Get the value of an index in sqldatareader with the default value of (NULL) if field is DBNULL and return as string
		/// </summary>
		/// <param name="rs">SQL data reader object</param>
		/// <param name="index">Field index</param>
		/// <param name="def">Default value</param>
		/// <returns></returns>
		public static string GetString(SqlDataReader rs, int index, string def)
		{
			return rs.IsDBNull(index) ? def : rs.GetString(index);
		}

		/// <summary>
		/// Get the value of an index in sqldatareader with the default value of (NULL) if field is DBNULL and return as string
		/// </summary>
		/// <param name="reader">SQL data reader object</param>
		/// <param name="index">Field index</param>
		/// <param name="check">Checking value</param>
		/// <param name="def">Default value</param>
		/// <returns></returns>
		public static string GetString(SqlDataReader reader, int index, string check, string def)
		{
			bool condition = GetString(reader, index, check) == check;
			return condition ? def : reader.GetString(index);
		}

		/// <summary>
		/// Get the value of an index in sqldatareader and return as integer
		/// </summary>
		/// <param name="reader">SQL data reader object</param>
		/// <param name="index">Field index</param>
		/// <returns></returns>
		public static int GetInt32(SqlDataReader reader, int index)
		{
			return GetInt32(reader, index, 0);
		}

		/// <summary>
		/// Get the value of an index in sqldatareader with the default value of (0) if field is DBNULL and return as integer
		/// </summary>
		/// <param name="reader">SQL data reader object</param>
		/// <param name="index">Field index</param>
		/// <param name="defaultValue">Default value</param>
		/// <returns></returns>
		public static int GetInt32(SqlDataReader reader, int index, int defaultValue)
		{
			return reader.IsDBNull(index) ? defaultValue : reader.GetInt32(index);
		}
		
		/// <summary>
		/// Function to execute query string
		/// </summary>
		/// <param name="query">Query String</param>
		/// <param name="parameters">Sql parameters for the query</param>
		public static void ExecuteNonQuery(string query, params SqlParameter[] parameters)
		{
			ExecuteNonQuery(query, "SqlConnection", parameters);
		}

		/// <summary>
		/// Function to execute query string
		/// </summary>
		/// <param name="query">Query String</param>
		/// <param name="connectionString">Sql Connection</param>
		/// <param name="parameters">SQL parameters for the query</param>
		/// <returns></returns>
		public static void ExecuteNonQuery(string query, string connectionString, params SqlParameter[] parameters)
		{
			var connection = new SqlConnection(ConfigurationManager.AppSettings[connectionString]);
			OpenConnection(connection);
			var cmd = new SqlCommand(query, connection);
			cmd.Parameters.AddRange(parameters);
			cmd.CommandTimeout = 900;
			cmd.ExecuteNonQuery();
			CloseConnection(connection);
		}
		
		/// <summary>
		/// Function to read SQL data reader and returns a pointer to the reader being executed by the query.
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="parameters">SQL parameters</param>
		/// <returns>SQL data reader</returns>
		public static SqlDataReader ExecuteReader(string query, params SqlParameter[] parameters)
		{
			return ExecuteReader(query, "SqlConnection", parameters);
		}
		
		/// <summary>
		/// Function to read SQL data reader and returns a pointer to the reader being executed by the query.
		/// </summary>
		/// <param name="query">Query string</param>
		/// <param name="connectionString">SQL connection string</param>
		/// <param name="parameters">SQL parameters</param>
		/// <returns>SQL data reader</returns>
		public static SqlDataReader ExecuteReader(string query, string connectionString, SqlParameter[] parameters)
		{
			var connection = new SqlConnection(ConfigurationManager.AppSettings[connectionString]);
			OpenConnection(connection);
			var cmd = new SqlCommand(query, connection);
			cmd.Parameters.AddRange(parameters);
			cmd.CommandTimeout = 900;
			var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
			return reader;
		}
		
		/// <summary>
		/// Execute query string and return string value
		/// </summary>
		/// <param name="query">Query String</param>
		/// <param name="parameters">SQL parameters for the query</param>
		/// <returns>Object from the scalar execution of the data reader</returns>
		public static object ExecuteScalar(string query, params SqlParameter[] parameters)
		{
			return ExecuteScalar(query, "SqlConnection", parameters);
		}
		
		/// <summary>
		/// Execute query string and return string value
		/// </summary>
		/// <param name="query">Query String</param>
		/// <param name="connectionString">Sql Connection</param>
		/// <param name="parameters">SQL parameters for the query</param>
		/// <returns>Object from the scalar execution of the data reader</returns>
		public static object ExecuteScalar(string query, string connectionString, params SqlParameter[] parameters)
		{
			var connection = new SqlConnection(ConfigurationManager.AppSettings[connectionString]);
			OpenConnection(connection);
			var cmd = new SqlCommand(query, connection);
			cmd.Parameters.AddRange(parameters);
			object o = cmd.ExecuteScalar();
			CloseConnection(connection);
			return o;
		}
		
		/// <summary>
		/// Function to open the connection for use. Ensures first that the connection is closed before opening.
		/// </summary>
		/// <param name="connection"></param>
		static void OpenConnection(SqlConnection connection)
		{
			if (connection.State == ConnectionState.Closed) {
				connection.Open();
			}
		}
		
		/// <summary>
		/// Closes the SQL connection. Close only a connection that is open.
		/// </summary>
		/// <param name="connection"></param>
		static void CloseConnection(SqlConnection connection)
		{
			if (connection.State == ConnectionState.Open) {
				connection.Close();
				connection.Dispose();
			}
		}

		#region Obsolete methods and functions.
		
		/// <summary>
		/// Execute query string and return sqldatareader value.
		/// </summary>
		/// <param name="sqlString">Query String</param>
		/// <param name="con">Sql Connection</param>
		/// <returns></returns>
		[Obsolete("Please use ExecuteReader function. This method defies the use of Coding Guidelines.")] public static SqlDataReader rs(string sqlString, string con)
		{
			SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[con].ConnectionString);
			dataConnection.Open();
			SqlCommand cmd = new SqlCommand(sqlString, dataConnection);
			cmd.CommandTimeout = 900;
			SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
			return dataReader;
		}

		[Obsolete("Please use ExecuteReader function. This method defies the use of Coding Guidelines.")] public static SqlDataReader rs(string sqlString)
		{
			return rs(sqlString, "SqlConnection");
		}

		/// <summary>
		/// Function to execute query string
		/// </summary>
		/// <param name="sqlString">Query String</param>
		[Obsolete("Please use ExecuteNonQuery function. This method defies the use of Coding Guidelines.")] public static void exec(string sqlString)
		{
			exec(sqlString, "SqlConnection");
		}

		/// <summary>
		/// Function to execute query string
		/// </summary>
		/// <param name="sqlString">Query String</param>
		/// <param name="con">Sql Connection</param>
		/// <returns></returns>
		[Obsolete("Please use ExecuteNonQuery function. This method defies the use of Coding Guidelines.")] public static void exec(string sqlString, string con)
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
		[Obsolete("Please use the generic ExecuteScalar function. This method defies the use of Coding Guidelines.")] public static int execIntScal(string sqlString)
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
		[Obsolete("Please use the generic ExecuteScalar function. This method defies the use of Coding Guidelines.")] public static int execIntScal(string sqlString, string con, int defaultValue)
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
		[Obsolete("Please use the generic ExecuteScalar function. This method defies the use of Coding Guidelines.")] public static string execStrScal(string sqlString)
		{
			return execStrScal(sqlString, "SqlConnection");
		}

		/// <summary>
		/// Execute query string and return string value
		/// </summary>
		/// <param name="sqlString">Query String</param>
		/// <param name="con">Sql Connection</param>
		/// <returns></returns>
		[Obsolete("Please use the generic ExecuteScalar function. This method defies the use of Coding Guidelines.")] public static string execStrScal(string sqlString, string con)
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
		
		#endregion
	}
}
