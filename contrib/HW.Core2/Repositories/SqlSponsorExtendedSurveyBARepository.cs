using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorExtendedSurveyBARepository : BaseSqlRepository<SponsorExtendedSurveyBA>
	{
		public SqlSponsorExtendedSurveyBARepository()
		{
		}
		
		public override void Save(SponsorExtendedSurveyBA sponsorExtendedSurveyBA)
		{
			string query = @"
INSERT INTO SponsorExtendedSurveyBA(
	SponsorExtendedSurveyBAID, 
	SponsorExtendedSurveyID, 
	ProjectRoundQOID, 
	OptionComponentID, 
	BAID
)
VALUES(
	@SponsorExtendedSurveyBAID, 
	@SponsorExtendedSurveyID, 
	@ProjectRoundQOID, 
	@OptionComponentID, 
	@BAID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorExtendedSurveyBAID", sponsorExtendedSurveyBA.SponsorExtendedSurveyBAID),
				new SqlParameter("@SponsorExtendedSurveyID", sponsorExtendedSurveyBA.SponsorExtendedSurveyID),
				new SqlParameter("@ProjectRoundQOID", sponsorExtendedSurveyBA.ProjectRoundQOID),
				new SqlParameter("@OptionComponentID", sponsorExtendedSurveyBA.OptionComponentID),
				new SqlParameter("@BAID", sponsorExtendedSurveyBA.BAID)
			);
		}
		
		public override void Update(SponsorExtendedSurveyBA sponsorExtendedSurveyBA, int id)
		{
			string query = @"
UPDATE SponsorExtendedSurveyBA SET
	SponsorExtendedSurveyBAID = @SponsorExtendedSurveyBAID,
	SponsorExtendedSurveyID = @SponsorExtendedSurveyID,
	ProjectRoundQOID = @ProjectRoundQOID,
	OptionComponentID = @OptionComponentID,
	BAID = @BAID
WHERE SponsorExtendedSurveyBAID = @SponsorExtendedSurveyBAID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorExtendedSurveyBAID", sponsorExtendedSurveyBA.SponsorExtendedSurveyBAID),
				new SqlParameter("@SponsorExtendedSurveyID", sponsorExtendedSurveyBA.SponsorExtendedSurveyID),
				new SqlParameter("@ProjectRoundQOID", sponsorExtendedSurveyBA.ProjectRoundQOID),
				new SqlParameter("@OptionComponentID", sponsorExtendedSurveyBA.OptionComponentID),
				new SqlParameter("@BAID", sponsorExtendedSurveyBA.BAID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorExtendedSurveyBA
WHERE SponsorExtendedSurveyBAID = @SponsorExtendedSurveyBAID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorExtendedSurveyBAID", id)
			);
		}
		
		public override SponsorExtendedSurveyBA Read(int id)
		{
			string query = @"
SELECT 	SponsorExtendedSurveyBAID, 
	SponsorExtendedSurveyID, 
	ProjectRoundQOID, 
	OptionComponentID, 
	BAID
FROM SponsorExtendedSurveyBA
WHERE SponsorExtendedSurveyBAID = @SponsorExtendedSurveyBAID";
			SponsorExtendedSurveyBA sponsorExtendedSurveyBA = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorExtendedSurveyBAID", id))) {
				if (rs.Read()) {
					sponsorExtendedSurveyBA = new SponsorExtendedSurveyBA {
						SponsorExtendedSurveyBAID = GetInt32(rs, 0),
						SponsorExtendedSurveyID = GetInt32(rs, 1),
						ProjectRoundQOID = GetInt32(rs, 2),
						OptionComponentID = GetInt32(rs, 3),
						BAID = GetInt32(rs, 4)
					};
				}
			}
			return sponsorExtendedSurveyBA;
		}
		
		public override IList<SponsorExtendedSurveyBA> FindAll()
		{
			string query = @"
SELECT 	SponsorExtendedSurveyBAID, 
	SponsorExtendedSurveyID, 
	ProjectRoundQOID, 
	OptionComponentID, 
	BAID
FROM SponsorExtendedSurveyBA";
			var sponsorExtendedSurveyBAs = new List<SponsorExtendedSurveyBA>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorExtendedSurveyBAs.Add(new SponsorExtendedSurveyBA {
						SponsorExtendedSurveyBAID = GetInt32(rs, 0),
						SponsorExtendedSurveyID = GetInt32(rs, 1),
						ProjectRoundQOID = GetInt32(rs, 2),
						OptionComponentID = GetInt32(rs, 3),
						BAID = GetInt32(rs, 4)
					});
				}
			}
			return sponsorExtendedSurveyBAs;
		}
	}
}
