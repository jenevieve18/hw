﻿using System;
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
		
		#region Obsolete methods
		
		[Obsolete("Please use DbHelper's ExecuteNonQuery method from now on. This is efficient since it creates only one connection at a time.")]
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

        [Obsolete("Please use DbHelper's ExecuteScalar method from now on. This is efficient since it creates only one connection at a time.")]
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
		
		[Obsolete("Please use DbHelper's ExecuteReader method from now on. This is efficient since it creates only one connection at a time.")]
		protected SqlDataReader ExecuteReader(string query)
		{
			return ExecuteReader(query, "SqlConnection");
		}
		
		[Obsolete("Please use DbHelper's ExecuteReader method from now on. This is efficient since it creates only one connection at a time.")]
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

        [Obsolete("Please use DbHelper's GetDateTime method from now on.")]
		protected DateTime? GetDateTime(SqlDataReader rs, int index, DateTime? def)
        {
            return rs.IsDBNull(index) ? def : (DateTime?)rs.GetDateTime(index);
        }
		
		[Obsolete("Please use DbHelper's GetDateTime method from now on.")]
		protected DateTime? GetDateTime(SqlDataReader rs, int index)
		{
            return GetDateTime(rs, index, null);
		}
		
		[Obsolete()]
		protected void SetDateTime(DateTime date, SqlDataReader rs, int index)
		{
			if (rs.IsDBNull(index)) {
				date = rs.GetDateTime(index);
			}
		}
		
		[Obsolete("Please use DbHelper's GetString method from now on.")]
		protected string GetString(SqlDataReader rs, int index)
		{
			return GetString(rs, index, null);
		}
		
		[Obsolete("Please use DbHelper's GetString method from now on.")]
		protected string GetString(SqlDataReader rs, int index, string def)
		{
			return rs.IsDBNull(index) ? def : rs.GetString(index);
		}
		
		[Obsolete("Please use DbHelper's GetString method from now on.")]
		protected string GetString(SqlDataReader rs, int index, string check, string def)
		{
			bool condition = GetString(rs, index, check) == check;
			return condition ? def : rs.GetString(index);
		}
		
		[Obsolete("Please use DbHelper's GetBoolean method from now on.")]
		protected bool GetBoolean(SqlDataReader rs, int index)
		{
			return rs.IsDBNull(index) ? false : rs.GetBoolean(index);
		}
		
		[Obsolete("Please use DbHelper's GetFloat method from now on.")]
		protected float GetFloat(SqlDataReader rs, int index)
		{
			return GetFloat(rs, index, 0);
		}
		
		[Obsolete("Please use DbHelper's GetFloat method from now on.")]
		protected float GetFloat(SqlDataReader rs, int index, float def)
		{
			return rs.IsDBNull(index) ? def : rs.GetFloat(index);
		}
		
		[Obsolete("Please use DbHelper's GetObject method from now on.")]
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
		
		[Obsolete("Please use DbHelper's GetInt32 method from now on.")]
		protected int GetInt32(SqlDataReader rs, int index)
		{
			return GetInt32(rs, index, 0);
		}
		
		[Obsolete("Please use DbHelper's GetInt32 method from now on.")]
		protected int GetInt32(SqlDataReader rs, int index, int def)
		{
			return rs.IsDBNull(index) ? def : rs.GetInt32(index);
		}
		
		[Obsolete("Please use DbHelper's GetGuid method from now on.")]
		protected Guid? GetGuid(SqlDataReader rs, int index)
		{
			return GetGuid(rs, index, null);
		}
		
		[Obsolete("Please use DbHelper's GetGuid method from now on.")]
		protected Guid? GetGuid(SqlDataReader rs, int index, Guid? def)
		{
			return rs.IsDBNull(index) ? def : rs.GetGuid(index);
		}
		
		[Obsolete()]
		protected int? GetInt32Nullable(SqlDataReader rs, int index)
		{
			return GetInt32Nullable(rs, index, null);
		}
		
		[Obsolete()]
		protected int? GetInt32Nullable(SqlDataReader rs, int index, int? def)
		{
			return rs.IsDBNull(index) ? def : rs.GetInt32(index);
		}
		
		[Obsolete("Please use DbHelper's GetDouble method from now on.")]
		protected double GetDouble(SqlDataReader rs, int index)
		{
			return GetDouble(rs, index, 0);
		}
		
		[Obsolete("Please use DbHelper's GetDouble method from now on.")]
		protected double GetDouble(SqlDataReader rs, int index, double def)
		{
			return rs.IsDBNull(index) ? def : rs.GetDouble(index);
		}
		
		[Obsolete("Please use DbHelper's GetDecimal method from now on.")]
		protected decimal GetDecimal(SqlDataReader rs, int index)
		{
			return GetDecimal(rs, index, 0);
		}
		
		[Obsolete("Please use DbHelper's GetDecimal method from now on.")]
		protected decimal GetDecimal(SqlDataReader rs, int index, decimal def)
		{
			return rs.IsDBNull(index) ? def : rs.GetDecimal(index);
		}
		
		[Obsolete("Please use DbHelper's private static method CloseConnection. Only to be used inside the DbHelper class.")]
		protected void CloseConnection()
		{
			if (con != null && con.State == ConnectionState.Open) {
				con.Close();
			}
		}
		
		[Obsolete("Please use DbHelper's private static method OpenConnection. Only to be used inside the DbHelper class.")]
		protected void OpenConnection()
		{
			if (con != null && con.State == ConnectionState.Closed) {
				con.Open();
			}
		}
		
		[Obsolete()]
		SqlConnection CreateConnection(string connectionName)
		{
			if (ConfigurationManager.ConnectionStrings[connectionName] != null) {
				con = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString);
			} else {
				con = new SqlConnection(ConfigurationManager.AppSettings[connectionName]);
			}
			return con;
		}
		
		#endregion
	}
}
