using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlSponsorPRURepository : BaseSqlRepository<SponsorPRU>
	{
		public SqlSponsorPRURepository()
		{
		}
		
		public override void Save(SponsorPRU sponsorPRU)
		{
			string query = @"
INSERT INTO SponsorPRU(
	SponsorPRUID, 
	PRUID, 
	NoLogout, 
	SponsorID, 
	NoSend
)
VALUES(
	@SponsorPRUID, 
	@PRUID, 
	@NoLogout, 
	@SponsorID, 
	@NoSend
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorPRUID", sponsorPRU.SponsorPRUID),
				new SqlParameter("@PRUID", sponsorPRU.PRUID),
				new SqlParameter("@NoLogout", sponsorPRU.NoLogout),
				new SqlParameter("@SponsorID", sponsorPRU.SponsorID),
				new SqlParameter("@NoSend", sponsorPRU.NoSend)
			);
		}
		
		public override void Update(SponsorPRU sponsorPRU, int id)
		{
			string query = @"
UPDATE SponsorPRU SET
	SponsorPRUID = @SponsorPRUID,
	PRUID = @PRUID,
	NoLogout = @NoLogout,
	SponsorID = @SponsorID,
	NoSend = @NoSend
WHERE SponsorPRUID = @SponsorPRUID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorPRUID", sponsorPRU.SponsorPRUID),
				new SqlParameter("@PRUID", sponsorPRU.PRUID),
				new SqlParameter("@NoLogout", sponsorPRU.NoLogout),
				new SqlParameter("@SponsorID", sponsorPRU.SponsorID),
				new SqlParameter("@NoSend", sponsorPRU.NoSend)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorPRU
WHERE SponsorPRUID = @SponsorPRUID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorPRUID", id)
			);
		}
		
		public override SponsorPRU Read(int id)
		{
			string query = @"
SELECT 	SponsorPRUID, 
	PRUID, 
	NoLogout, 
	SponsorID, 
	NoSend
FROM SponsorPRU
WHERE SponsorPRUID = @SponsorPRUID";
			SponsorPRU sponsorPRU = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorPRUID", id))) {
				if (rs.Read()) {
					sponsorPRU = new SponsorPRU {
						SponsorPRUID = GetInt32(rs, 0),
						PRUID = GetInt32(rs, 1),
						NoLogout = GetInt32(rs, 2),
						SponsorID = GetInt32(rs, 3),
						NoSend = GetInt32(rs, 4)
					};
				}
			}
			return sponsorPRU;
		}
		
		public override IList<SponsorPRU> FindAll()
		{
			string query = @"
SELECT 	SponsorPRUID, 
	PRUID, 
	NoLogout, 
	SponsorID, 
	NoSend
FROM SponsorPRU";
			var sponsorPRUs = new List<SponsorPRU>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorPRUs.Add(new SponsorPRU {
						SponsorPRUID = GetInt32(rs, 0),
						PRUID = GetInt32(rs, 1),
						NoLogout = GetInt32(rs, 2),
						SponsorID = GetInt32(rs, 3),
						NoSend = GetInt32(rs, 4)
					});
				}
			}
			return sponsorPRUs;
		}
	}
}
