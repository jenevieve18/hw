using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlSponsorSuperAdminSponsorRepository : BaseSqlRepository<SponsorSuperAdminSponsor>
	{
		public SqlSponsorSuperAdminSponsorRepository()
		{
		}
		
		public override void Save(SponsorSuperAdminSponsor sponsorSuperAdminSponsor)
		{
			string query = @"
INSERT INTO SponsorSuperAdminSponsor(
	SponsorSuperAdminSponsorID, 
	SponsorSuperAdminID, 
	SponsorID
)
VALUES(
	@SponsorSuperAdminSponsorID, 
	@SponsorSuperAdminID, 
	@SponsorID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorSuperAdminSponsorID", sponsorSuperAdminSponsor.SponsorSuperAdminSponsorID),
				new SqlParameter("@SponsorSuperAdminID", sponsorSuperAdminSponsor.SponsorSuperAdminID),
				new SqlParameter("@SponsorID", sponsorSuperAdminSponsor.SponsorID)
			);
		}
		
		public override void Update(SponsorSuperAdminSponsor sponsorSuperAdminSponsor, int id)
		{
			string query = @"
UPDATE SponsorSuperAdminSponsor SET
	SponsorSuperAdminSponsorID = @SponsorSuperAdminSponsorID,
	SponsorSuperAdminID = @SponsorSuperAdminID,
	SponsorID = @SponsorID
WHERE SponsorSuperAdminSponsorID = @SponsorSuperAdminSponsorID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorSuperAdminSponsorID", sponsorSuperAdminSponsor.SponsorSuperAdminSponsorID),
				new SqlParameter("@SponsorSuperAdminID", sponsorSuperAdminSponsor.SponsorSuperAdminID),
				new SqlParameter("@SponsorID", sponsorSuperAdminSponsor.SponsorID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorSuperAdminSponsor
WHERE SponsorSuperAdminSponsorID = @SponsorSuperAdminSponsorID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorSuperAdminSponsorID", id)
			);
		}
		
		public override SponsorSuperAdminSponsor Read(int id)
		{
			string query = @"
SELECT 	SponsorSuperAdminSponsorID, 
	SponsorSuperAdminID, 
	SponsorID
FROM SponsorSuperAdminSponsor
WHERE SponsorSuperAdminSponsorID = @SponsorSuperAdminSponsorID";
			SponsorSuperAdminSponsor sponsorSuperAdminSponsor = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorSuperAdminSponsorID", id))) {
				if (rs.Read()) {
					sponsorSuperAdminSponsor = new SponsorSuperAdminSponsor {
						SponsorSuperAdminSponsorID = GetInt32(rs, 0),
						SponsorSuperAdminID = GetInt32(rs, 1),
						SponsorID = GetInt32(rs, 2)
					};
				}
			}
			return sponsorSuperAdminSponsor;
		}
		
		public override IList<SponsorSuperAdminSponsor> FindAll()
		{
			string query = @"
SELECT 	SponsorSuperAdminSponsorID, 
	SponsorSuperAdminID, 
	SponsorID
FROM SponsorSuperAdminSponsor";
			var sponsorSuperAdminSponsors = new List<SponsorSuperAdminSponsor>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorSuperAdminSponsors.Add(new SponsorSuperAdminSponsor {
						SponsorSuperAdminSponsorID = GetInt32(rs, 0),
						SponsorSuperAdminID = GetInt32(rs, 1),
						SponsorID = GetInt32(rs, 2)
					});
				}
			}
			return sponsorSuperAdminSponsors;
		}
	}
}
