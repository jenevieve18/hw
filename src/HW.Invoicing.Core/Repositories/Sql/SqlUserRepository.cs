﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlUserRepository : BaseSqlRepository<User>
	{
		public SqlUserRepository()
		{
		}
		
		public void Save(User u)
		{
			string query = string.Format(
				@"
INSERT INTO [User](Name, [Password])
VALUES(@Name, @Password)"
			);
			ExecuteNonQuery(query, "invoicing", new SqlParameter("@Name", u.Name), new SqlParameter("@Password", u.Password));
		}
		
		public void Update(User u)
		{
			string query = string.Format(
				@"
UPDATE [User] SET = @Name,
[Password] = @Password
WHERE Id = @Id"
			);
			ExecuteNonQuery(query, "invoicing", new SqlParameter("@Name", u.Name), new SqlParameter("@Password", u.Password), new SqlParameter("@Id", u.Id));
		}
		
		public override IList<User> FindAll()
		{
			string query = string.Format(
				@"
SELECT Id, Name, [Password]
FROM [User]"
			);
			IList<User> users = new List<User>();
			using (SqlDataReader rs = Db.rs(query, "invoicing")) {
				while (rs.Read()) {
					users.Add(
						new User { Id = GetInt32(rs, 0), Name = GetString(rs, 1), Password = GetString(rs, 2) }
					);
				}
			}
			return users;
		}
		
		public override User Read(int id)
		{
			string query = string.Format(
				@"
SELECT Id, Name, [Password]
FROM [User]
WHERE Id = @Id"
			);
			User u = null;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				if (rs.Read()) {
					u = new User {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						Password = GetString(rs, 2)
					};
				}
			}
			return u;
		}
		
		public User ReadByNameAndPassword(string name, string password)
		{
			string query = string.Format(
				@"
SELECT Id, Name, [Password]
FROM [User]
WHERE Name = @Name
AND [Password] = @Password"
			);
			User u = null;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Name", name), new SqlParameter("@Password", password))) {
				if (rs.Read()) {
					u = new User {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						Password = GetString(rs, 2)
					};
				}
			}
			return u;
		}
	}
}