using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlSponsorAdminRepository : BaseSqlRepository<SponsorAdmin>
	{
		public SqlSponsorAdminRepository()
		{
		}
		
		public override void Save(SponsorAdmin sponsorAdmin)
		{
			string query = @"
INSERT INTO SponsorAdmin(
	SponsorAdminID, 
	SponsorID, 
	Username, 
	Password, 
	Name, 
	Email, 
	Restricted
)
VALUES(
	@SponsorAdminID, 
	@SponsorID, 
	@Username, 
	@Password, 
	@Name, 
	@Email, 
	@Restricted
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminID", sponsorAdmin.SponsorAdminID),
				new SqlParameter("@SponsorID", sponsorAdmin.SponsorID),
				new SqlParameter("@Username", sponsorAdmin.Username),
				new SqlParameter("@Password", sponsorAdmin.Password),
				new SqlParameter("@Name", sponsorAdmin.Name),
				new SqlParameter("@Email", sponsorAdmin.Email),
				new SqlParameter("@Restricted", sponsorAdmin.Restricted)
			);
		}
		
		public override void Update(SponsorAdmin sponsorAdmin, int id)
		{
			string query = @"
UPDATE SponsorAdmin SET
	SponsorAdminID = @SponsorAdminID,
	SponsorID = @SponsorID,
	Username = @Username,
	Password = @Password,
	Name = @Name,
	Email = @Email,
	Restricted = @Restricted
WHERE SponsorAdminID = @SponsorAdminID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminID", sponsorAdmin.SponsorAdminID),
				new SqlParameter("@SponsorID", sponsorAdmin.SponsorID),
				new SqlParameter("@Username", sponsorAdmin.Username),
				new SqlParameter("@Password", sponsorAdmin.Password),
				new SqlParameter("@Name", sponsorAdmin.Name),
				new SqlParameter("@Email", sponsorAdmin.Email),
				new SqlParameter("@Restricted", sponsorAdmin.Restricted)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorAdmin
WHERE SponsorAdminID = @SponsorAdminID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminID", id)
			);
		}
		
		public override SponsorAdmin Read(int id)
		{
			string query = @"
SELECT 	SponsorAdminID, 
	SponsorID, 
	Username, 
	Password, 
	Name, 
	Email, 
	Restricted
FROM SponsorAdmin
WHERE SponsorAdminID = @SponsorAdminID";
			SponsorAdmin sponsorAdmin = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorAdminID", id))) {
				if (rs.Read()) {
					sponsorAdmin = new SponsorAdmin {
						SponsorAdminID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						Username = GetString(rs, 2),
						Password = GetString(rs, 3),
						Name = GetString(rs, 4),
						Email = GetString(rs, 5),
						Restricted = GetInt32(rs, 6)
					};
				}
			}
			return sponsorAdmin;
		}
		
		public override IList<SponsorAdmin> FindAll()
		{
			string query = @"
SELECT 	SponsorAdminID, 
	SponsorID, 
	Username, 
	Password, 
	Name, 
	Email, 
	Restricted
FROM SponsorAdmin";
			var sponsorAdmins = new List<SponsorAdmin>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorAdmins.Add(new SponsorAdmin {
						SponsorAdminID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						Username = GetString(rs, 2),
						Password = GetString(rs, 3),
						Name = GetString(rs, 4),
						Email = GetString(rs, 5),
						Restricted = GetInt32(rs, 6)
					});
				}
			}
			return sponsorAdmins;
		}
	}
}
