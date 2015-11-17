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
INSERT INTO [User](Username, [Password], Color, Name)
VALUES(@Username, @Password, @Color, @Name)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Username", u.Username),
                new SqlParameter("@Password", u.Password),
                new SqlParameter("@Color", u.Color),
                new SqlParameter("@Name", u.Name)
			);
		}
		
		public void SelectCompany(int userId, int selectedCompany)
		{
			string query = string.Format(
				@"
UPDATE [User] SET SelectedCompany = @SelectedCompany
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@SelectedCompany", selectedCompany),
                new SqlParameter("@Id", userId)
			);
		}
		
		public override void Update(User u, int id)
		{
			string query = string.Format(
				@"
UPDATE [User] SET Username = @Username,
    [Password] = @Password,
    Color = @Color,
    Name = @Name
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Username", u.Username),
				new SqlParameter("@Password", u.Password),
                new SqlParameter("@Color", u.Color),
                new SqlParameter("@Id", id),
                new SqlParameter("@Name", u.Name)
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
SELECT Id, Username, [Password], Color, Name
FROM [User]
ORDER BY Username"
			);
			IList<User> users = new List<User>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing")) {
				while (rs.Read()) {
					users.Add(
						new User {
							Id = GetInt32(rs, 0),
							Username = GetString(rs, 1),
							Password = GetString(rs, 2),
                            Color = GetString(rs, 3),
                            Name = GetString(rs, 4)
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
SELECT Id, Username, [Password], Color, Name
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
                        Color = GetString(rs, 3),
                        Name = GetString(rs, 4)
					};
				}
			}
			return u;
		}
		
		public User ReadByNameAndPassword(string name, string password)
		{
			string query = string.Format(
				@"
SELECT Id, Username, [Password], Name, SelectedCompany
FROM [User] u
WHERE Username = @Username
AND [Password] = @Password"
			);
			User u = null;
			using (SqlDataReader rs = ExecuteReader(
				query,
				"invoicing",
				new SqlParameter("@Username", name),
				new SqlParameter("@Password", password))) {
				
				if (rs.Read()) {
					u = new User {
						Id = GetInt32(rs, 0),
						Username = GetString(rs, 1),
						Password = GetString(rs, 2),
                        Name = GetString(rs, 3),
                        SelectedCompany = new Company {
                        	Id = GetInt32(rs, 4)
                        }
					};
				}
			}
			return u;
		}
	}
}
