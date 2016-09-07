using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorPRUBQmapValRepository : BaseSqlRepository<SponsorPRUBQmapVal>
	{
		public SqlSponsorPRUBQmapValRepository()
		{
		}
		
		public override void Save(SponsorPRUBQmapVal sponsorPRUBQmapVal)
		{
			string query = @"
INSERT INTO SponsorPRUBQmapVal(
	SponsorPRUBQmapValID, 
	SponsorPRUBQmapID, 
	BAID, 
	OCID
)
VALUES(
	@SponsorPRUBQmapValID, 
	@SponsorPRUBQmapID, 
	@BAID, 
	@OCID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorPRUBQmapValID", sponsorPRUBQmapVal.SponsorPRUBQmapValID),
				new SqlParameter("@SponsorPRUBQmapID", sponsorPRUBQmapVal.SponsorPRUBQmapID),
				new SqlParameter("@BAID", sponsorPRUBQmapVal.BAID),
				new SqlParameter("@OCID", sponsorPRUBQmapVal.OCID)
			);
		}
		
		public override void Update(SponsorPRUBQmapVal sponsorPRUBQmapVal, int id)
		{
			string query = @"
UPDATE SponsorPRUBQmapVal SET
	SponsorPRUBQmapValID = @SponsorPRUBQmapValID,
	SponsorPRUBQmapID = @SponsorPRUBQmapID,
	BAID = @BAID,
	OCID = @OCID
WHERE SponsorPRUBQmapValID = @SponsorPRUBQmapValID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorPRUBQmapValID", sponsorPRUBQmapVal.SponsorPRUBQmapValID),
				new SqlParameter("@SponsorPRUBQmapID", sponsorPRUBQmapVal.SponsorPRUBQmapID),
				new SqlParameter("@BAID", sponsorPRUBQmapVal.BAID),
				new SqlParameter("@OCID", sponsorPRUBQmapVal.OCID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorPRUBQmapVal
WHERE SponsorPRUBQmapValID = @SponsorPRUBQmapValID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorPRUBQmapValID", id)
			);
		}
		
		public override SponsorPRUBQmapVal Read(int id)
		{
			string query = @"
SELECT 	SponsorPRUBQmapValID, 
	SponsorPRUBQmapID, 
	BAID, 
	OCID
FROM SponsorPRUBQmapVal
WHERE SponsorPRUBQmapValID = @SponsorPRUBQmapValID";
			SponsorPRUBQmapVal sponsorPRUBQmapVal = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorPRUBQmapValID", id))) {
				if (rs.Read()) {
					sponsorPRUBQmapVal = new SponsorPRUBQmapVal {
						SponsorPRUBQmapValID = GetInt32(rs, 0),
						SponsorPRUBQmapID = GetInt32(rs, 1),
						BAID = GetInt32(rs, 2),
						OCID = GetInt32(rs, 3)
					};
				}
			}
			return sponsorPRUBQmapVal;
		}
		
		public override IList<SponsorPRUBQmapVal> FindAll()
		{
			string query = @"
SELECT 	SponsorPRUBQmapValID, 
	SponsorPRUBQmapID, 
	BAID, 
	OCID
FROM SponsorPRUBQmapVal";
			var sponsorPRUBQmapVals = new List<SponsorPRUBQmapVal>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorPRUBQmapVals.Add(new SponsorPRUBQmapVal {
						SponsorPRUBQmapValID = GetInt32(rs, 0),
						SponsorPRUBQmapID = GetInt32(rs, 1),
						BAID = GetInt32(rs, 2),
						OCID = GetInt32(rs, 3)
					});
				}
			}
			return sponsorPRUBQmapVals;
		}
	}
}
