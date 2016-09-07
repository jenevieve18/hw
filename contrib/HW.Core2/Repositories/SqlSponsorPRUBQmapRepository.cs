using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorPRUBQmapRepository : BaseSqlRepository<SponsorPRUBQmap>
	{
		public SqlSponsorPRUBQmapRepository()
		{
		}
		
		public override void Save(SponsorPRUBQmap sponsorPRUBQmap)
		{
			string query = @"
INSERT INTO SponsorPRUBQmap(
	SponsorPRUBQmapID, 
	SponsorProjectRoundUnitID, 
	BQID, 
	QID, 
	OID, 
	FN
)
VALUES(
	@SponsorPRUBQmapID, 
	@SponsorProjectRoundUnitID, 
	@BQID, 
	@QID, 
	@OID, 
	@FN
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorPRUBQmapID", sponsorPRUBQmap.SponsorPRUBQmapID),
				new SqlParameter("@SponsorProjectRoundUnitID", sponsorPRUBQmap.SponsorProjectRoundUnitID),
				new SqlParameter("@BQID", sponsorPRUBQmap.BQID),
				new SqlParameter("@QID", sponsorPRUBQmap.QID),
				new SqlParameter("@OID", sponsorPRUBQmap.OID),
				new SqlParameter("@FN", sponsorPRUBQmap.FN)
			);
		}
		
		public override void Update(SponsorPRUBQmap sponsorPRUBQmap, int id)
		{
			string query = @"
UPDATE SponsorPRUBQmap SET
	SponsorPRUBQmapID = @SponsorPRUBQmapID,
	SponsorProjectRoundUnitID = @SponsorProjectRoundUnitID,
	BQID = @BQID,
	QID = @QID,
	OID = @OID,
	FN = @FN
WHERE SponsorPRUBQmapID = @SponsorPRUBQmapID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorPRUBQmapID", sponsorPRUBQmap.SponsorPRUBQmapID),
				new SqlParameter("@SponsorProjectRoundUnitID", sponsorPRUBQmap.SponsorProjectRoundUnitID),
				new SqlParameter("@BQID", sponsorPRUBQmap.BQID),
				new SqlParameter("@QID", sponsorPRUBQmap.QID),
				new SqlParameter("@OID", sponsorPRUBQmap.OID),
				new SqlParameter("@FN", sponsorPRUBQmap.FN)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorPRUBQmap
WHERE SponsorPRUBQmapID = @SponsorPRUBQmapID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorPRUBQmapID", id)
			);
		}
		
		public override SponsorPRUBQmap Read(int id)
		{
			string query = @"
SELECT 	SponsorPRUBQmapID, 
	SponsorProjectRoundUnitID, 
	BQID, 
	QID, 
	OID, 
	FN
FROM SponsorPRUBQmap
WHERE SponsorPRUBQmapID = @SponsorPRUBQmapID";
			SponsorPRUBQmap sponsorPRUBQmap = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorPRUBQmapID", id))) {
				if (rs.Read()) {
					sponsorPRUBQmap = new SponsorPRUBQmap {
						SponsorPRUBQmapID = GetInt32(rs, 0),
						SponsorProjectRoundUnitID = GetInt32(rs, 1),
						BQID = GetInt32(rs, 2),
						QID = GetInt32(rs, 3),
						OID = GetInt32(rs, 4),
						FN = GetInt32(rs, 5)
					};
				}
			}
			return sponsorPRUBQmap;
		}
		
		public override IList<SponsorPRUBQmap> FindAll()
		{
			string query = @"
SELECT 	SponsorPRUBQmapID, 
	SponsorProjectRoundUnitID, 
	BQID, 
	QID, 
	OID, 
	FN
FROM SponsorPRUBQmap";
			var sponsorPRUBQmaps = new List<SponsorPRUBQmap>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorPRUBQmaps.Add(new SponsorPRUBQmap {
						SponsorPRUBQmapID = GetInt32(rs, 0),
						SponsorProjectRoundUnitID = GetInt32(rs, 1),
						BQID = GetInt32(rs, 2),
						QID = GetInt32(rs, 3),
						OID = GetInt32(rs, 4),
						FN = GetInt32(rs, 5)
					});
				}
			}
			return sponsorPRUBQmaps;
		}
	}
}
