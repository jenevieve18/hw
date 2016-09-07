using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSuperAdminRepository : BaseSqlRepository<SuperAdmin>
	{
		public SqlSuperAdminRepository()
		{
		}
		
		public override void Save(SuperAdmin superAdmin)
		{
			string query = @"
INSERT INTO SuperAdmin(
	SuperAdminID, 
	Username, 
	Password, 
	HideClosedSponsors
)
VALUES(
	@SuperAdminID, 
	@Username, 
	@Password, 
	@HideClosedSponsors
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SuperAdminID", superAdmin.SuperAdminID),
				new SqlParameter("@Username", superAdmin.Username),
				new SqlParameter("@Password", superAdmin.Password),
				new SqlParameter("@HideClosedSponsors", superAdmin.HideClosedSponsors)
			);
		}
		
		public override void Update(SuperAdmin superAdmin, int id)
		{
			string query = @"
UPDATE SuperAdmin SET
	SuperAdminID = @SuperAdminID,
	Username = @Username,
	Password = @Password,
	HideClosedSponsors = @HideClosedSponsors
WHERE SuperAdminID = @SuperAdminID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SuperAdminID", superAdmin.SuperAdminID),
				new SqlParameter("@Username", superAdmin.Username),
				new SqlParameter("@Password", superAdmin.Password),
				new SqlParameter("@HideClosedSponsors", superAdmin.HideClosedSponsors)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SuperAdmin
WHERE SuperAdminID = @SuperAdminID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SuperAdminID", id)
			);
		}
		
		public override SuperAdmin Read(int id)
		{
			string query = @"
SELECT 	SuperAdminID, 
	Username, 
	Password, 
	HideClosedSponsors
FROM SuperAdmin
WHERE SuperAdminID = @SuperAdminID";
			SuperAdmin superAdmin = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SuperAdminID", id))) {
				if (rs.Read()) {
					superAdmin = new SuperAdmin {
						SuperAdminID = GetInt32(rs, 0),
						Username = GetString(rs, 1),
						Password = GetString(rs, 2),
						HideClosedSponsors = GetInt32(rs, 3)
					};
				}
			}
			return superAdmin;
		}
		
		public override IList<SuperAdmin> FindAll()
		{
			string query = @"
SELECT 	SuperAdminID, 
	Username, 
	Password, 
	HideClosedSponsors
FROM SuperAdmin";
			var superAdmins = new List<SuperAdmin>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					superAdmins.Add(new SuperAdmin {
						SuperAdminID = GetInt32(rs, 0),
						Username = GetString(rs, 1),
						Password = GetString(rs, 2),
						HideClosedSponsors = GetInt32(rs, 3)
					});
				}
			}
			return superAdmins;
		}
	}
}
