using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSuperAdminSponsorRepository : BaseSqlRepository<SuperAdminSponsor>
	{
		public SqlSuperAdminSponsorRepository()
		{
		}
		
		public override void Save(SuperAdminSponsor superAdminSponsor)
		{
			string query = @"
INSERT INTO SuperAdminSponsor(
	SuperAdminSponsorID, 
	SuperAdminID, 
	SponsorID, 
	SeeUsers
)
VALUES(
	@SuperAdminSponsorID, 
	@SuperAdminID, 
	@SponsorID, 
	@SeeUsers
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SuperAdminSponsorID", superAdminSponsor.SuperAdminSponsorID),
				new SqlParameter("@SuperAdminID", superAdminSponsor.SuperAdminID),
				new SqlParameter("@SponsorID", superAdminSponsor.SponsorID),
				new SqlParameter("@SeeUsers", superAdminSponsor.SeeUsers)
			);
		}
		
		public override void Update(SuperAdminSponsor superAdminSponsor, int id)
		{
			string query = @"
UPDATE SuperAdminSponsor SET
	SuperAdminSponsorID = @SuperAdminSponsorID,
	SuperAdminID = @SuperAdminID,
	SponsorID = @SponsorID,
	SeeUsers = @SeeUsers
WHERE SuperAdminSponsorID = @SuperAdminSponsorID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SuperAdminSponsorID", superAdminSponsor.SuperAdminSponsorID),
				new SqlParameter("@SuperAdminID", superAdminSponsor.SuperAdminID),
				new SqlParameter("@SponsorID", superAdminSponsor.SponsorID),
				new SqlParameter("@SeeUsers", superAdminSponsor.SeeUsers)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SuperAdminSponsor
WHERE SuperAdminSponsorID = @SuperAdminSponsorID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SuperAdminSponsorID", id)
			);
		}
		
		public override SuperAdminSponsor Read(int id)
		{
			string query = @"
SELECT 	SuperAdminSponsorID, 
	SuperAdminID, 
	SponsorID, 
	SeeUsers
FROM SuperAdminSponsor
WHERE SuperAdminSponsorID = @SuperAdminSponsorID";
			SuperAdminSponsor superAdminSponsor = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SuperAdminSponsorID", id))) {
				if (rs.Read()) {
					superAdminSponsor = new SuperAdminSponsor {
						SuperAdminSponsorID = GetInt32(rs, 0),
						SuperAdminID = GetInt32(rs, 1),
						SponsorID = GetInt32(rs, 2),
						SeeUsers = GetInt32(rs, 3)
					};
				}
			}
			return superAdminSponsor;
		}
		
		public override IList<SuperAdminSponsor> FindAll()
		{
			string query = @"
SELECT 	SuperAdminSponsorID, 
	SuperAdminID, 
	SponsorID, 
	SeeUsers
FROM SuperAdminSponsor";
			var superAdminSponsors = new List<SuperAdminSponsor>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					superAdminSponsors.Add(new SuperAdminSponsor {
						SuperAdminSponsorID = GetInt32(rs, 0),
						SuperAdminID = GetInt32(rs, 1),
						SponsorID = GetInt32(rs, 2),
						SeeUsers = GetInt32(rs, 3)
					});
				}
			}
			return superAdminSponsors;
		}
	}
}
