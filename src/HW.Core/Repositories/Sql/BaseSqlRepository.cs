using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HW.Core.Repositories.Sql
{
	public class BaseSqlRepository<T> : IBaseRepository<T>
	{
		SqlConnection con;
		
		public BaseSqlRepository()
		{
		}
		
		public virtual void SaveOrUpdate(T t)
		{
			throw new NotImplementedException();
		}
		
		public virtual void SaveOrUpdate<U>(U t)
		{
			throw new NotImplementedException();
		}
		
		public void Delete(T t)
		{
			throw new NotImplementedException();
		}
		
		public virtual T Read(int id)
		{
			throw new NotImplementedException();
		}
		
		public virtual IList<T> FindAll()
		{
			throw new NotImplementedException();
		}
		
		protected void ExecuteNonQuery(string query, string connectionName, params SqlParameter[] parameters)
		{
			con = new SqlConnection(ConfigurationManager.AppSettings[connectionName]);
			OpenConnection();
			SqlCommand cmd = new SqlCommand(query, con);
			foreach (var p in parameters) {
				if (p.Value == null) {
					p.Value = DBNull.Value;
				}
				cmd.Parameters.Add(p);
			}
			cmd.ExecuteNonQuery();
		}
		
		protected SqlDataReader ExecuteReader(string query)
		{
			return ExecuteReader(query, "SqlConnection");
		}
		
		protected SqlDataReader ExecuteReader(string query, string connectionName)
		{
			con = new SqlConnection(ConfigurationManager.AppSettings[connectionName]);
			OpenConnection();
			SqlCommand cmd = new SqlCommand(query, con);
			return cmd.ExecuteReader(CommandBehavior.CloseConnection);
		}
		
		protected DateTime? GetDateTime(SqlDataReader rs, int index)
		{
			return rs.IsDBNull(index) ? null : (DateTime?)rs.GetDateTime(index);
		}
		
		protected void SetDateTime(DateTime date, SqlDataReader rs, int index)
		{
			if (rs.IsDBNull(index)) {
				date = rs.GetDateTime(index);
			}
		}
		
		protected string GetString(SqlDataReader rs, int index)
		{
			return rs.IsDBNull(index) ? null : rs.GetString(index);
		}
		
		protected bool GetBoolean(SqlDataReader rs, int index)
		{
			return rs.IsDBNull(index) ? false : rs.GetBoolean(index);
		}
		
		protected int GetInt32(SqlDataReader rs, int index)
		{
			return GetInt32(rs, index, 0);
		}
		
		protected int GetInt32(SqlDataReader rs, int index, int def)
		{
			return rs.IsDBNull(index) ? def : rs.GetInt32(index);
		}
		
		protected double GetDouble(SqlDataReader rs, int index)
		{
			return rs.IsDBNull(index) ? 0 : rs.GetDouble(index);
		}
		
		void CloseConnection()
		{
			if (con.State == ConnectionState.Open) {
				con.Close();
			}
		}
		
		void OpenConnection()
		{
			if (con.State == ConnectionState.Closed) {
				con.Open();
			}
		}
		
		public void Delete<U>(U t)
		{
			throw new NotImplementedException();
		}
	}
}
