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
VALUES(@Username, @Password, @Color, @Name);
SELECT CAST(scope_identity() AS int)"
			);
			int userId = (int)ExecuteScalar(
				query,
				"invoicing",
				new SqlParameter("@Username", u.Username),
				new SqlParameter("@Password", StrHelper.MD5Hash(u.Password)),
				new SqlParameter("@Color", u.Color),
				new SqlParameter("@Name", u.Name)
			);
//			SaveLinks(userId, u.Links);
			SaveCompanyLinks(userId, u.SelectedCompany);
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
			string p = u.Password != "" ? "[Password] = @Password," : "";
			string c = u.Color != "" ? "Color = @Color," : "";
			string query = string.Format(
				@"
UPDATE [User] SET Username = @Username,
    {0}
    {1}
    Name = @Name
WHERE Id = @Id",
				p,
				c
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Username", u.Username),
				new SqlParameter("@Password", StrHelper.MD5Hash(u.Password)),
				new SqlParameter("@Color", u.Color),
				new SqlParameter("@Id", id),
				new SqlParameter("@Name", u.Name)
			);
			SaveCompanyLinks(id, u.SelectedCompany);
		}
		
//		public override void UpdateProfile(User u, int id)
//		{
//			string p = u.Password != "" ? "[Password] = @Password," : "";
//			string query = string.Format(
//				@"
		//UPDATE [User] SET Username = @Username,
		//    {0}
		//    Color = @Color,
		//    Name = @Name
		//WHERE Id = @Id",
//				p
//			);
//			ExecuteNonQuery(
//				query,
//				"invoicing",
//				new SqlParameter("@Username", u.Username),
//				new SqlParameter("@Password", StrHelper.MD5Hash(u.Password)),
//				new SqlParameter("@Color", u.Color),
//				new SqlParameter("@Id", id),
//				new SqlParameter("@Name", u.Name)
//			);
//		}
		
//		public void SaveLinks(int userId, IList<UserLink> links)
//		{
//			string query = string.Format(
//				@"
		//DELETE FROM UserLink
		//WHERE UserId = @UserId"
//			);
//			ExecuteNonQuery(query, "invoicing", new SqlParameter("@UserId", userId));
//
//			query = string.Format(
//				@"
		//INSERT INTO UserLink(UserId, Link)
		//VALUES(@UserId, @Link)"
//			);
//			foreach (var l in links) {
//				ExecuteNonQuery(query, "invoicing", new SqlParameter("@UserId", userId), new SqlParameter("@Link", l.Link.Id));
//			}
//		}
		
		public void SaveCompanyLinks(int userId, Company company)
		{
			if (company != null) {
				string query = string.Format(
					@"
SELECT 1
FROM UserCompany uc
RIGHT OUTER JOIN Company c ON c.Id = uc.CompanyId
WHERE c.UserId = @UserId
AND c.Id = @CompanyId"
				);
				bool userCompanyFound = false;
				using (var rs = ExecuteReader(query, "invoicing", new SqlParameter("@UserId", userId), new SqlParameter("@CompanyId", company.Id))) {
					if (rs.Read()) {
						userCompanyFound = true;
					}
				}
				if (!userCompanyFound) {
					query = string.Format(
						@"
INSERT INTO UserCompany(UserId, CompanyId)
VALUES(@UserId, @CompanyId)"
					);
					ExecuteNonQuery(query, "invoicing", new SqlParameter(), new SqlParameter("@UserId", userId), new SqlParameter("@CompanyId", company.Id));
				}
				query = string.Format(
					@"
DELETE FROM UserCompanyLink
WHERE UserId = @UserId"
				);
				ExecuteNonQuery(
					query,
					"invoicing",
					new SqlParameter("@UserId", userId)
				);
				
				query = string.Format(
					@"
INSERT INTO UserCompanyLink(UserId, CompanyId, Link)
VALUES(@UserId, @CompanyId, @Link)"
				);
				foreach (var l in company.Links) {
					ExecuteNonQuery(
						query, "invoicing",
						new SqlParameter("@UserId", userId),
						new SqlParameter("@CompanyId", company.Id),
						new SqlParameter("@Link", l.Id)
					);
				}
			}
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
		
		public IList<User> FindCollaborators(int companyId)
		{
			string query = string.Format(
				@"
SELECT Id,
	Username,
	[Password],
	Color,
	Name
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
		
		public IList<UserLink> FindLinks(int userId)
		{
			string query = string.Format(
				@"
SELECT Id,
	UserId,
	Link
FROM UserLink
WHERE UserId = @UserId"
			);
			IList<UserLink> links = new List<UserLink>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@UserId", userId))) {
				while (rs.Read()) {
					links.Add(
						new UserLink {
							Id = GetInt32(rs, 0),
							User = new User { Id = GetInt32(rs, 1) },
							Link = Link.GetLink(GetInt32(rs, 2, 1))
						}
					);
				}
			}
			return links;
		}
		
		public IList<Link> FindLinks(int userId, int companyId)
		{
			string query = string.Format(
				@"
SELECT ucl.Link
FROM UserCompanyLink ucl
WHERE ucl.UserId = @UserId
AND ucl.CompanyId = @CompanyId"
			);
			IList<Link> links = new List<Link>();
			using (SqlDataReader rs = ExecuteReader(
				query,
				"invoicing",
				new SqlParameter("@UserId", userId),
				new SqlParameter("@CompanyId", companyId)
			)) {
				while (rs.Read()) {
					links.Add(Link.GetLink(GetInt32(rs, 0)));
				}
			}
			return links;
		}
		
		public override IList<User> FindAll()
		{
			string query = string.Format(
				@"
SELECT Id,
	Username,
	[Password],
	Color,
	Name
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

		public Company ReadSelectedCompany(User u)
		{
			var cr = new SqlCompanyRepository();
			if (u.HasSelectedCompany) {
				return cr.Read(u.SelectedCompany.Id);
			} else {
				var companies = cr.FindCompanies(u.Id);
				foreach (var c in cr.FindCompaniesThatHasAccessTo(u.Id)) {
					companies.Add(c.Company);
				}
				if (companies.Count > 0) {
					return companies[0];
				} else {
					return null;
				}
			}
		}
		
		public override User Read(int id)
		{
			string query = string.Format(
				@"
SELECT Id,
	Username,
	[Password],
	Color,
	Name
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
			//            if (u != null)
			//            {
			//                u.AddLinks(FindLinks(id));
			//            }
			return u;
		}
		
		public User ReadByNameAndPassword(string name, string password)
		{
			string query = string.Format(
				@"
SELECT Id,
	Username,
	[Password],
	Name,
	SelectedCompany
FROM [User] u
WHERE Username = @Username
AND ([Password] = @Password OR [Password] = @HashedPassword)"
			);
			User u = null;
			using (SqlDataReader rs = ExecuteReader(
				query,
				"invoicing",
				new SqlParameter("@Username", name),
				new SqlParameter("@Password", password),
				new SqlParameter("@HashedPassword", StrHelper.MD5Hash(password)))) {
				
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
