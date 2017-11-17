using System;
using HW.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core.Repositories.Sql
{
	public class SqlSponsorBQRepository : BaseSqlRepository<SponsorBackgroundQuestion>
	{
		public SqlSponsorBQRepository()
		{
		}
		
		public override void Save(SponsorBackgroundQuestion sponsorBQ)
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
				"healthWatchSqlConnection",
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
		
		public override void Update(SponsorBackgroundQuestion sponsorBQ, int id)
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
				"healthWatchSqlConnection",
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
				"healthWatchSqlConnection",
				new SqlParameter("@SponsorBQID", id)
			);
		}
		
		public override SponsorBackgroundQuestion Read(int id)
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
			SponsorBackgroundQuestion sponsorBQ = null;
			using (var rs = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@SponsorBQID", id))) {
				if (rs.Read()) {
					sponsorBQ = new SponsorBackgroundQuestion {
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
		
		public override IList<SponsorBackgroundQuestion> FindAll()
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
			var sponsorBQs = new List<SponsorBackgroundQuestion>();
			using (var rs = ExecuteReader(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					sponsorBQs.Add(new SponsorBackgroundQuestion {
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

		public IList<SponsorBackgroundQuestion> FindBySponsor(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT sbq.BQID,
        BQ.Internal
FROM SponsorBQ sbq
INNER JOIN BQ ON BQ.BQID = sbq.BQID
WHERE (BQ.Comparison = 1 OR sbq.Hidden = 1)
AND BQ.Type IN (1, 7)
AND sbq.SponsorID = {0}",
				sponsorID
			);
			var questions = new List<SponsorBackgroundQuestion>();
			using (SqlDataReader rs = ExecuteReader(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var sbq = new SponsorBackgroundQuestion {
						Id = rs.GetInt32(0),
						BackgroundQuestion = new BackgroundQuestion { Internal = rs.GetString(1) }
					};
					questions.Add(sbq);
				}
			}
			return questions;
		}
    }
}
