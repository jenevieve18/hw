using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlUserRepository : BaseSqlRepository<User>, IUserRepository
	{
		public SqlUserRepository()
		{
		}
		
		public override void Save(User u)
		{
			string query = string.Format(
				@"
INSERT INTO [User](Name, [Password], Color)
VALUES(@Name, @Password, @Color)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", u.Username),
                new SqlParameter("@Password", u.Password),
                new SqlParameter("@Color", u.Color)
			);
		}
		
		public override void Update(User u, int id)
		{
			string query = string.Format(
				@"
UPDATE [User] SET Name = @Name,
[Password] = @Password,
Color = @Color
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", u.Username),
				new SqlParameter("@Password", u.Password),
                new SqlParameter("@Color", u.Color),
				new SqlParameter("@Id", id)
			);
		}
		
		public override void Delete(int id)
		{
			string query = string.Format(
				@"
DELETE FROM [User]
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Id", id)
			);
		}
		
		public override IList<User> FindAll()
		{
			string query = string.Format(
				@"
SELECT Id, Name, [Password], Color
FROM [User]
ORDER BY Name"
			);
			IList<User> users = new List<User>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing")) {
				while (rs.Read()) {
					users.Add(
						new User {
							Id = GetInt32(rs, 0),
							Username = GetString(rs, 1),
							Password = GetString(rs, 2),
                            Color = GetString(rs, 3)
						}
					);
				}
			}
			return users;
		}
		
		public override User Read(int id)
		{
			string query = string.Format(
				@"
SELECT Id, Name, [Password], Color
FROM [User]
WHERE Id = @Id"
			);
			User u = null;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				if (rs.Read()) {
					u = new User {
						Id = GetInt32(rs, 0),
						Username = GetString(rs, 1),
						Password = GetString(rs, 2),
                        Color = GetString(rs, 3)
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
						Username = GetString(rs, 1),
						Password = GetString(rs, 2)
					};
				}
			}
			return u;
		}
	}
}
