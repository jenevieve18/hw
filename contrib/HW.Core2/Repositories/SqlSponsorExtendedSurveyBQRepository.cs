using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorExtendedSurveyBQRepository : BaseSqlRepository<SponsorExtendedSurveyBQ>
	{
		public SqlSponsorExtendedSurveyBQRepository()
		{
		}
		
		public override void Save(SponsorExtendedSurveyBQ sponsorExtendedSurveyBQ)
		{
			string query = @"
INSERT INTO SponsorExtendedSurveyBQ(
	SponsorExtendedSurveyBQID, 
	SponsorExtendedSurveyID, 
	ProjectRoundQOID, 
	BQID, 
	FN
)
VALUES(
	@SponsorExtendedSurveyBQID, 
	@SponsorExtendedSurveyID, 
	@ProjectRoundQOID, 
	@BQID, 
	@FN
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorExtendedSurveyBQID", sponsorExtendedSurveyBQ.SponsorExtendedSurveyBQID),
				new SqlParameter("@SponsorExtendedSurveyID", sponsorExtendedSurveyBQ.SponsorExtendedSurveyID),
				new SqlParameter("@ProjectRoundQOID", sponsorExtendedSurveyBQ.ProjectRoundQOID),
				new SqlParameter("@BQID", sponsorExtendedSurveyBQ.BQID),
				new SqlParameter("@FN", sponsorExtendedSurveyBQ.FN)
			);
		}
		
		public override void Update(SponsorExtendedSurveyBQ sponsorExtendedSurveyBQ, int id)
		{
			string query = @"
UPDATE SponsorExtendedSurveyBQ SET
	SponsorExtendedSurveyBQID = @SponsorExtendedSurveyBQID,
	SponsorExtendedSurveyID = @SponsorExtendedSurveyID,
	ProjectRoundQOID = @ProjectRoundQOID,
	BQID = @BQID,
	FN = @FN
WHERE SponsorExtendedSurveyBQID = @SponsorExtendedSurveyBQID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorExtendedSurveyBQID", sponsorExtendedSurveyBQ.SponsorExtendedSurveyBQID),
				new SqlParameter("@SponsorExtendedSurveyID", sponsorExtendedSurveyBQ.SponsorExtendedSurveyID),
				new SqlParameter("@ProjectRoundQOID", sponsorExtendedSurveyBQ.ProjectRoundQOID),
				new SqlParameter("@BQID", sponsorExtendedSurveyBQ.BQID),
				new SqlParameter("@FN", sponsorExtendedSurveyBQ.FN)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorExtendedSurveyBQ
WHERE SponsorExtendedSurveyBQID = @SponsorExtendedSurveyBQID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorExtendedSurveyBQID", id)
			);
		}
		
		public override SponsorExtendedSurveyBQ Read(int id)
		{
			string query = @"
SELECT 	SponsorExtendedSurveyBQID, 
	SponsorExtendedSurveyID, 
	ProjectRoundQOID, 
	BQID, 
	FN
FROM SponsorExtendedSurveyBQ
WHERE SponsorExtendedSurveyBQID = @SponsorExtendedSurveyBQID";
			SponsorExtendedSurveyBQ sponsorExtendedSurveyBQ = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorExtendedSurveyBQID", id))) {
				if (rs.Read()) {
					sponsorExtendedSurveyBQ = new SponsorExtendedSurveyBQ {
						SponsorExtendedSurveyBQID = GetInt32(rs, 0),
						SponsorExtendedSurveyID = GetInt32(rs, 1),
						ProjectRoundQOID = GetInt32(rs, 2),
						BQID = GetInt32(rs, 3),
						FN = GetInt32(rs, 4)
					};
				}
			}
			return sponsorExtendedSurveyBQ;
		}
		
		public override IList<SponsorExtendedSurveyBQ> FindAll()
		{
			string query = @"
SELECT 	SponsorExtendedSurveyBQID, 
	SponsorExtendedSurveyID, 
	ProjectRoundQOID, 
	BQID, 
	FN
FROM SponsorExtendedSurveyBQ";
			var sponsorExtendedSurveyBQs = new List<SponsorExtendedSurveyBQ>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorExtendedSurveyBQs.Add(new SponsorExtendedSurveyBQ {
						SponsorExtendedSurveyBQID = GetInt32(rs, 0),
						SponsorExtendedSurveyID = GetInt32(rs, 1),
						ProjectRoundQOID = GetInt32(rs, 2),
						BQID = GetInt32(rs, 3),
						FN = GetInt32(rs, 4)
					});
				}
			}
			return sponsorExtendedSurveyBQs;
		}
	}
}
