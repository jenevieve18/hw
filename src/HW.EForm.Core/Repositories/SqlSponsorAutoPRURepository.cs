using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlSponsorAutoPRURepository : BaseSqlRepository<SponsorAutoPRU>
	{
		public SqlSponsorAutoPRURepository()
		{
		}
		
		public override void Save(SponsorAutoPRU sponsorAutoPRU)
		{
			string query = @"
INSERT INTO SponsorAutoPRU(
	SponsorAutoPRUID, 
	SponsorID, 
	PRUID, 
	Note
)
VALUES(
	@SponsorAutoPRUID, 
	@SponsorID, 
	@PRUID, 
	@Note
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAutoPRUID", sponsorAutoPRU.SponsorAutoPRUID),
				new SqlParameter("@SponsorID", sponsorAutoPRU.SponsorID),
				new SqlParameter("@PRUID", sponsorAutoPRU.PRUID),
				new SqlParameter("@Note", sponsorAutoPRU.Note)
			);
		}
		
		public override void Update(SponsorAutoPRU sponsorAutoPRU, int id)
		{
			string query = @"
UPDATE SponsorAutoPRU SET
	SponsorAutoPRUID = @SponsorAutoPRUID,
	SponsorID = @SponsorID,
	PRUID = @PRUID,
	Note = @Note
WHERE SponsorAutoPRUID = @SponsorAutoPRUID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAutoPRUID", sponsorAutoPRU.SponsorAutoPRUID),
				new SqlParameter("@SponsorID", sponsorAutoPRU.SponsorID),
				new SqlParameter("@PRUID", sponsorAutoPRU.PRUID),
				new SqlParameter("@Note", sponsorAutoPRU.Note)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorAutoPRU
WHERE SponsorAutoPRUID = @SponsorAutoPRUID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAutoPRUID", id)
			);
		}
		
		public override SponsorAutoPRU Read(int id)
		{
			string query = @"
SELECT 	SponsorAutoPRUID, 
	SponsorID, 
	PRUID, 
	Note
FROM SponsorAutoPRU
WHERE SponsorAutoPRUID = @SponsorAutoPRUID";
			SponsorAutoPRU sponsorAutoPRU = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorAutoPRUID", id))) {
				if (rs.Read()) {
					sponsorAutoPRU = new SponsorAutoPRU {
						SponsorAutoPRUID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						PRUID = GetInt32(rs, 2),
						Note = GetString(rs, 3)
					};
				}
			}
			return sponsorAutoPRU;
		}
		
		public override IList<SponsorAutoPRU> FindAll()
		{
			string query = @"
SELECT 	SponsorAutoPRUID, 
	SponsorID, 
	PRUID, 
	Note
FROM SponsorAutoPRU";
			var sponsorAutoPRUs = new List<SponsorAutoPRU>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorAutoPRUs.Add(new SponsorAutoPRU {
						SponsorAutoPRUID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						PRUID = GetInt32(rs, 2),
						Note = GetString(rs, 3)
					});
				}
			}
			return sponsorAutoPRUs;
		}
	}
}
