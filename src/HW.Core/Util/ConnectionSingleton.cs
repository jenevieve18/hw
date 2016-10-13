using System;
using System.Data.SqlClient;

namespace HW.Core.Util
{
	public class ConnectionSingleton
	{
		static SqlConnection instance;
		
		ConnectionSingleton()
		{
		}
		
		public static SqlConnection Instance {
			get {
				if (instance == null) {
					instance = new SqlConnection();
				}
				return instance;
			}
		}
		
		public static void SetConnectionString(string connectionString)
		{
			Instance.ConnectionString = connectionString;
		}
	}
}
