/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/14/2016
 * Time: 2:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data.SqlClient;

namespace HW.Core.Libraries
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
