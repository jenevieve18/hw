using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlSponsorSuperAdminRepository : BaseSqlRepository<SponsorSuperAdmin>
	{
		public SqlSponsorSuperAdminRepository()
		{
		}
		
		public override void Save(SponsorSuperAdmin sponsorSuperAdmin)
		{
			string query = @"
INSERT INTO SponsorSuperAdmin(
	SponsorSuperAdminID, 
	DefaultSponsorID, 
	Username, 
	Password
)
VALUES(
	@SponsorSuperAdminID, 
	@DefaultSponsorID, 
	@Username, 
	@Password
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorSuperAdminID", sponsorSuperAdmin.SponsorSuperAdminID),
				new SqlParameter("@DefaultSponsorID", sponsorSuperAdmin.DefaultSponsorID),
				new SqlParameter("@Username", sponsorSuperAdmin.Username),
				new SqlParameter("@Password", sponsorSuperAdmin.Password)
			);
		}
		
		public override void Update(SponsorSuperAdmin sponsorSuperAdmin, int id)
		{
			string query = @"
UPDATE SponsorSuperAdmin SET
	SponsorSuperAdminID = @SponsorSuperAdminID,
	DefaultSponsorID = @DefaultSponsorID,
	Username = @Username,
	Password = @Password
WHERE SponsorSuperAdminID = @SponsorSuperAdminID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorSuperAdminID", sponsorSuperAdmin.SponsorSuperAdminID),
				new SqlParameter("@DefaultSponsorID", sponsorSuperAdmin.DefaultSponsorID),
				new SqlParameter("@Username", sponsorSuperAdmin.Username),
				new SqlParameter("@Password", sponsorSuperAdmin.Password)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorSuperAdmin
WHERE SponsorSuperAdminID = @SponsorSuperAdminID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorSuperAdminID", id)
			);
		}
		
		public override SponsorSuperAdmin Read(int id)
		{
			string query = @"
SELECT 	SponsorSuperAdminID, 
	DefaultSponsorID, 
	Username, 
	Password
FROM SponsorSuperAdmin
WHERE SponsorSuperAdminID = @SponsorSuperAdminID";
			SponsorSuperAdmin sponsorSuperAdmin = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorSuperAdminID", id))) {
				if (rs.Read()) {
					sponsorSuperAdmin = new SponsorSuperAdmin {
						SponsorSuperAdminID = GetInt32(rs, 0),
						DefaultSponsorID = GetInt32(rs, 1),
						Username = GetString(rs, 2),
						Password = GetString(rs, 3)
					};
				}
			}
			return sponsorSuperAdmin;
		}
		
		public override IList<SponsorSuperAdmin> FindAll()
		{
			string query = @"
SELECT 	SponsorSuperAdminID, 
	DefaultSponsorID, 
	Username, 
	Password
FROM SponsorSuperAdmin";
			var sponsorSuperAdmins = new List<SponsorSuperAdmin>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorSuperAdmins.Add(new SponsorSuperAdmin {
						SponsorSuperAdminID = GetInt32(rs, 0),
						DefaultSponsorID = GetInt32(rs, 1),
						Username = GetString(rs, 2),
						Password = GetString(rs, 3)
					});
				}
			}
			return sponsorSuperAdmins;
		}
	}
}
