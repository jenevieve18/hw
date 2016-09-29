using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlSponsorUserCheckRepository : BaseSqlRepository<SponsorUserCheck>
	{
		public SqlSponsorUserCheckRepository()
		{
		}
		
		public override void Save(SponsorUserCheck sponsorUserCheck)
		{
			string query = @"
INSERT INTO SponsorUserCheck(
	SponsorUserCheckID, 
	UserCheckNr, 
	SponsorID, 
	Txt
)
VALUES(
	@SponsorUserCheckID, 
	@UserCheckNr, 
	@SponsorID, 
	@Txt
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorUserCheckID", sponsorUserCheck.SponsorUserCheckID),
				new SqlParameter("@UserCheckNr", sponsorUserCheck.UserCheckNr),
				new SqlParameter("@SponsorID", sponsorUserCheck.SponsorID),
				new SqlParameter("@Txt", sponsorUserCheck.Txt)
			);
		}
		
		public override void Update(SponsorUserCheck sponsorUserCheck, int id)
		{
			string query = @"
UPDATE SponsorUserCheck SET
	SponsorUserCheckID = @SponsorUserCheckID,
	UserCheckNr = @UserCheckNr,
	SponsorID = @SponsorID,
	Txt = @Txt
WHERE SponsorUserCheckID = @SponsorUserCheckID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorUserCheckID", sponsorUserCheck.SponsorUserCheckID),
				new SqlParameter("@UserCheckNr", sponsorUserCheck.UserCheckNr),
				new SqlParameter("@SponsorID", sponsorUserCheck.SponsorID),
				new SqlParameter("@Txt", sponsorUserCheck.Txt)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorUserCheck
WHERE SponsorUserCheckID = @SponsorUserCheckID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorUserCheckID", id)
			);
		}
		
		public override SponsorUserCheck Read(int id)
		{
			string query = @"
SELECT 	SponsorUserCheckID, 
	UserCheckNr, 
	SponsorID, 
	Txt
FROM SponsorUserCheck
WHERE SponsorUserCheckID = @SponsorUserCheckID";
			SponsorUserCheck sponsorUserCheck = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorUserCheckID", id))) {
				if (rs.Read()) {
					sponsorUserCheck = new SponsorUserCheck {
						SponsorUserCheckID = GetInt32(rs, 0),
						UserCheckNr = GetInt32(rs, 1),
						SponsorID = GetInt32(rs, 2),
						Txt = GetString(rs, 3)
					};
				}
			}
			return sponsorUserCheck;
		}
		
		public override IList<SponsorUserCheck> FindAll()
		{
			string query = @"
SELECT 	SponsorUserCheckID, 
	UserCheckNr, 
	SponsorID, 
	Txt
FROM SponsorUserCheck";
			var sponsorUserChecks = new List<SponsorUserCheck>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorUserChecks.Add(new SponsorUserCheck {
						SponsorUserCheckID = GetInt32(rs, 0),
						UserCheckNr = GetInt32(rs, 1),
						SponsorID = GetInt32(rs, 2),
						Txt = GetString(rs, 3)
					});
				}
			}
			return sponsorUserChecks;
		}
	}
}
