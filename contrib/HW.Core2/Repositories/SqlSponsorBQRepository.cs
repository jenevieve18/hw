using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorBQRepository : BaseSqlRepository<SponsorBQ>
	{
		public SqlSponsorBQRepository()
		{
		}
		
		public override void Save(SponsorBQ sponsorBQ)
		{
			string query = @"
INSERT INTO SponsorBQ(
	SponsorBQID, 
	SponsorID, 
	BQID, 
	Forced, 
	SortOrder, 
	Hidden, 
	Fn, 
	InGrpAdmin, 
	IncludeInTreatmentReq, 
	Organize
)
VALUES(
	@SponsorBQID, 
	@SponsorID, 
	@BQID, 
	@Forced, 
	@SortOrder, 
	@Hidden, 
	@Fn, 
	@InGrpAdmin, 
	@IncludeInTreatmentReq, 
	@Organize
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorBQID", sponsorBQ.SponsorBQID),
				new SqlParameter("@SponsorID", sponsorBQ.SponsorID),
				new SqlParameter("@BQID", sponsorBQ.BQID),
				new SqlParameter("@Forced", sponsorBQ.Forced),
				new SqlParameter("@SortOrder", sponsorBQ.SortOrder),
				new SqlParameter("@Hidden", sponsorBQ.Hidden),
				new SqlParameter("@Fn", sponsorBQ.Fn),
				new SqlParameter("@InGrpAdmin", sponsorBQ.InGrpAdmin),
				new SqlParameter("@IncludeInTreatmentReq", sponsorBQ.IncludeInTreatmentReq),
				new SqlParameter("@Organize", sponsorBQ.Organize)
			);
		}
		
		public override void Update(SponsorBQ sponsorBQ, int id)
		{
			string query = @"
UPDATE SponsorBQ SET
	SponsorBQID = @SponsorBQID,
	SponsorID = @SponsorID,
	BQID = @BQID,
	Forced = @Forced,
	SortOrder = @SortOrder,
	Hidden = @Hidden,
	Fn = @Fn,
	InGrpAdmin = @InGrpAdmin,
	IncludeInTreatmentReq = @IncludeInTreatmentReq,
	Organize = @Organize
WHERE SponsorBQID = @SponsorBQID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorBQID", sponsorBQ.SponsorBQID),
				new SqlParameter("@SponsorID", sponsorBQ.SponsorID),
				new SqlParameter("@BQID", sponsorBQ.BQID),
				new SqlParameter("@Forced", sponsorBQ.Forced),
				new SqlParameter("@SortOrder", sponsorBQ.SortOrder),
				new SqlParameter("@Hidden", sponsorBQ.Hidden),
				new SqlParameter("@Fn", sponsorBQ.Fn),
				new SqlParameter("@InGrpAdmin", sponsorBQ.InGrpAdmin),
				new SqlParameter("@IncludeInTreatmentReq", sponsorBQ.IncludeInTreatmentReq),
				new SqlParameter("@Organize", sponsorBQ.Organize)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorBQ
WHERE SponsorBQID = @SponsorBQID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorBQID", id)
			);
		}
		
		public override SponsorBQ Read(int id)
		{
			string query = @"
SELECT 	SponsorBQID, 
	SponsorID, 
	BQID, 
	Forced, 
	SortOrder, 
	Hidden, 
	Fn, 
	InGrpAdmin, 
	IncludeInTreatmentReq, 
	Organize
FROM SponsorBQ
WHERE SponsorBQID = @SponsorBQID";
			SponsorBQ sponsorBQ = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorBQID", id))) {
				if (rs.Read()) {
					sponsorBQ = new SponsorBQ {
						SponsorBQID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						BQID = GetInt32(rs, 2),
						Forced = GetInt32(rs, 3),
						SortOrder = GetInt32(rs, 4),
						Hidden = GetInt32(rs, 5),
						Fn = GetInt32(rs, 6),
						InGrpAdmin = GetInt32(rs, 7),
						IncludeInTreatmentReq = GetInt32(rs, 8),
						Organize = GetInt32(rs, 9)
					};
				}
			}
			return sponsorBQ;
		}
		
		public override IList<SponsorBQ> FindAll()
		{
			string query = @"
SELECT 	SponsorBQID, 
	SponsorID, 
	BQID, 
	Forced, 
	SortOrder, 
	Hidden, 
	Fn, 
	InGrpAdmin, 
	IncludeInTreatmentReq, 
	Organize
FROM SponsorBQ";
			var sponsorBQs = new List<SponsorBQ>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorBQs.Add(new SponsorBQ {
						SponsorBQID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						BQID = GetInt32(rs, 2),
						Forced = GetInt32(rs, 3),
						SortOrder = GetInt32(rs, 4),
						Hidden = GetInt32(rs, 5),
						Fn = GetInt32(rs, 6),
						InGrpAdmin = GetInt32(rs, 7),
						IncludeInTreatmentReq = GetInt32(rs, 8),
						Organize = GetInt32(rs, 9)
					});
				}
			}
			return sponsorBQs;
		}
	}
}
