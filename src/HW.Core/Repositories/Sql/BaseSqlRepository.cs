using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using HW.Core.Models;
using HW.Core.Services;

namespace HW.Core.Repositories.Sql
{
	public class BaseSqlRepository<T> // : IBaseRepository<T>
	{
		SqlConnection con;
		
		public BaseSqlRepository()
		{
		}
		
		public virtual void Save(T t)
		{
			throw new NotImplementedException();
		}
		
		public virtual void Update(T t, int id)
		{
			throw new NotImplementedException();
		}
		
		public virtual void SaveOrUpdate(T t)
		{
			throw new NotImplementedException();
		}
		
		public virtual void SaveOrUpdate<U>(U t)
		{
			throw new NotImplementedException();
		}
		
		public virtual void Delete(int id)
		{
			throw new NotImplementedException();
		}
		
		public virtual void Delete(T t)
		{
			throw new NotImplementedException();
		}
		
		public virtual void Delete<U>(U t)
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
			con = CreateConnection(connectionName);
			OpenConnection();
			SqlCommand cmd = new SqlCommand(query, con);
			foreach (var p in parameters) {
				if (p.Value == null) {
					p.Value = DBNull.Value;
				}
				cmd.Parameters.Add(p);
			}
			cmd.ExecuteNonQuery();
			CloseConnection();
		}

        protected object ExecuteScalar(string query, string connectionName, params SqlParameter[] parameters)
        {
            con = CreateConnection(connectionName);
            OpenConnection();
            SqlCommand cmd = new SqlCommand(query, con);
            foreach (var p in parameters)
            {
                if (p.Value == null)
                {
                    p.Value = DBNull.Value;
                }
                cmd.Parameters.Add(p);
            }
            object obj = cmd.ExecuteScalar();
            CloseConnection();
            return obj;
        }
		
		protected SqlDataReader ExecuteReader(string query)
		{
			return ExecuteReader(query, "SqlConnection");
		}
		
		protected SqlDataReader ExecuteReader(string query, string connectionName, params SqlParameter[] parameters)
		{
			con = CreateConnection(connectionName);
			OpenConnection();
			SqlCommand cmd = new SqlCommand(query, con);
			foreach (var p in parameters) {
				if (p.Value == null) {
					p.Value = DBNull.Value;
				}
				cmd.Parameters.Add(p);
			}
			return cmd.ExecuteReader(CommandBehavior.CloseConnection);
		}

        protected DateTime? GetDateTime(SqlDataReader rs, int index, DateTime? def)
        {
            return rs.IsDBNull(index) ? def : (DateTime?)rs.GetDateTime(index);
        }
		
		protected DateTime? GetDateTime(SqlDataReader rs, int index)
		{
			//return rs.IsDBNull(index) ? null : (DateTime?)rs.GetDateTime(index);
            return GetDateTime(rs, index, null);
		}
		
		protected void SetDateTime(DateTime date, SqlDataReader rs, int index)
		{
			if (rs.IsDBNull(index)) {
				date = rs.GetDateTime(index);
			}
		}
		
		protected string GetString(SqlDataReader rs, int index)
		{
			return GetString(rs, index, null);
		}
		
		protected string GetString(SqlDataReader rs, int index, string def)
		{
			return rs.IsDBNull(index) ? def : rs.GetString(index);
		}
		
		protected string GetString(SqlDataReader rs, int index, string check, string def)
		{
			bool condition = GetString(rs, index, check) == check;
			return condition ? def : rs.GetString(index);
		}
		
		protected bool GetBoolean(SqlDataReader rs, int index)
		{
			return rs.IsDBNull(index) ? false : rs.GetBoolean(index);
		}
		
		protected float GetFloat(SqlDataReader rs, int index)
		{
			return GetFloat(rs, index, 0);
		}
		
		protected float GetFloat(SqlDataReader rs, int index, float def)
		{
			return rs.IsDBNull(index) ? def : rs.GetFloat(index);
		}
		
		protected T GetObject<T>(SqlDataReader rs, int index) where T : BaseModel, new()
		{
			if (!rs.IsDBNull(index)) {
				T m = new T();
				m.Id = GetInt32(rs, index);
				return m;
			} else {
				return null;
			}
		}
		
//		protected object GetObject(SqlDataReader rs, int index, Type type)
//		{
//			if (!rs.IsDBNull(index)) {
//				BaseModel m = (BaseModel)Activator.CreateInstance(type);
//				m.Id = GetInt32(rs, index);
//				return m;
//			} else {
//				return null;
//			}
//		}
//
		protected int GetInt32(SqlDataReader rs, int index)
		{
			return GetInt32(rs, index, 0);
		}
		
		protected int GetInt32(SqlDataReader rs, int index, int def)
		{
			return rs.IsDBNull(index) ? def : rs.GetInt32(index);
		}
		
		protected Guid? GetGuid(SqlDataReader rs, int index)
		{
			return GetGuid(rs, index, null);
		}
		
		protected Guid? GetGuid(SqlDataReader rs, int index, Guid? def)
		{
			return rs.IsDBNull(index) ? def : rs.GetGuid(index);
		}
		
		protected int? GetInt32Nullable(SqlDataReader rs, int index)
		{
			return GetInt32Nullable(rs, index, null);
		}
		
		protected int? GetInt32Nullable(SqlDataReader rs, int index, int? def)
		{
			return rs.IsDBNull(index) ? def : rs.GetInt32(index);
		}
		
		protected double GetDouble(SqlDataReader rs, int index)
		{
			return GetDouble(rs, index, 0);
		}
		
		protected double GetDouble(SqlDataReader rs, int index, double def)
		{
			return rs.IsDBNull(index) ? def : rs.GetDouble(index);
		}
		
		protected decimal GetDecimal(SqlDataReader rs, int index)
		{
			return GetDecimal(rs, index, 0);
		}
		
		protected decimal GetDecimal(SqlDataReader rs, int index, decimal def)
		{
			return rs.IsDBNull(index) ? def : rs.GetDecimal(index);
		}
		
		protected void CloseConnection()
		{
			if (con != null && con.State == ConnectionState.Open) {
				con.Close();
			}
		}
		
		protected void OpenConnection()
		{
			if (con != null && con.State == ConnectionState.Closed) {
				con.Open();
			}
		}
		
		SqlConnection CreateConnection(string connectionName)
		{
			if (ConfigurationManager.ConnectionStrings[connectionName] != null) {
				con = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString);
			} else {
				con = new SqlConnection(ConfigurationManager.AppSettings[connectionName]);
			}
			return con;
		}
	}
}
