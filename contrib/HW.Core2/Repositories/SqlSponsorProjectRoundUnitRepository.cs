using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorProjectRoundUnitRepository : BaseSqlRepository<SponsorProjectRoundUnit>
	{
		public SqlSponsorProjectRoundUnitRepository()
		{
		}
		
		public override void Save(SponsorProjectRoundUnit sponsorProjectRoundUnit)
		{
			string query = @"
INSERT INTO SponsorProjectRoundUnit(
	SponsorProjectRoundUnitID, 
	SponsorID, 
	ProjectRoundUnitID, 
	Nav, 
	SurveyKey, 
	SortOrder, 
	Feedback, 
	Ext, 
	SurveyID, 
	OnlyEveryDays, 
	GoToStatistics
)
VALUES(
	@SponsorProjectRoundUnitID, 
	@SponsorID, 
	@ProjectRoundUnitID, 
	@Nav, 
	@SurveyKey, 
	@SortOrder, 
	@Feedback, 
	@Ext, 
	@SurveyID, 
	@OnlyEveryDays, 
	@GoToStatistics
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorProjectRoundUnitID", sponsorProjectRoundUnit.SponsorProjectRoundUnitID),
				new SqlParameter("@SponsorID", sponsorProjectRoundUnit.SponsorID),
				new SqlParameter("@ProjectRoundUnitID", sponsorProjectRoundUnit.ProjectRoundUnitID),
				new SqlParameter("@Nav", sponsorProjectRoundUnit.Nav),
				new SqlParameter("@SurveyKey", sponsorProjectRoundUnit.SurveyKey),
				new SqlParameter("@SortOrder", sponsorProjectRoundUnit.SortOrder),
				new SqlParameter("@Feedback", sponsorProjectRoundUnit.Feedback),
				new SqlParameter("@Ext", sponsorProjectRoundUnit.Ext),
				new SqlParameter("@SurveyID", sponsorProjectRoundUnit.SurveyID),
				new SqlParameter("@OnlyEveryDays", sponsorProjectRoundUnit.OnlyEveryDays),
				new SqlParameter("@GoToStatistics", sponsorProjectRoundUnit.GoToStatistics)
			);
		}
		
		public override void Update(SponsorProjectRoundUnit sponsorProjectRoundUnit, int id)
		{
			string query = @"
UPDATE SponsorProjectRoundUnit SET
	SponsorProjectRoundUnitID = @SponsorProjectRoundUnitID,
	SponsorID = @SponsorID,
	ProjectRoundUnitID = @ProjectRoundUnitID,
	Nav = @Nav,
	SurveyKey = @SurveyKey,
	SortOrder = @SortOrder,
	Feedback = @Feedback,
	Ext = @Ext,
	SurveyID = @SurveyID,
	OnlyEveryDays = @OnlyEveryDays,
	GoToStatistics = @GoToStatistics
WHERE SponsorProjectRoundUnitID = @SponsorProjectRoundUnitID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorProjectRoundUnitID", sponsorProjectRoundUnit.SponsorProjectRoundUnitID),
				new SqlParameter("@SponsorID", sponsorProjectRoundUnit.SponsorID),
				new SqlParameter("@ProjectRoundUnitID", sponsorProjectRoundUnit.ProjectRoundUnitID),
				new SqlParameter("@Nav", sponsorProjectRoundUnit.Nav),
				new SqlParameter("@SurveyKey", sponsorProjectRoundUnit.SurveyKey),
				new SqlParameter("@SortOrder", sponsorProjectRoundUnit.SortOrder),
				new SqlParameter("@Feedback", sponsorProjectRoundUnit.Feedback),
				new SqlParameter("@Ext", sponsorProjectRoundUnit.Ext),
				new SqlParameter("@SurveyID", sponsorProjectRoundUnit.SurveyID),
				new SqlParameter("@OnlyEveryDays", sponsorProjectRoundUnit.OnlyEveryDays),
				new SqlParameter("@GoToStatistics", sponsorProjectRoundUnit.GoToStatistics)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorProjectRoundUnit
WHERE SponsorProjectRoundUnitID = @SponsorProjectRoundUnitID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorProjectRoundUnitID", id)
			);
		}
		
		public override SponsorProjectRoundUnit Read(int id)
		{
			string query = @"
SELECT 	SponsorProjectRoundUnitID, 
	SponsorID, 
	ProjectRoundUnitID, 
	Nav, 
	SurveyKey, 
	SortOrder, 
	Feedback, 
	Ext, 
	SurveyID, 
	OnlyEveryDays, 
	GoToStatistics
FROM SponsorProjectRoundUnit
WHERE SponsorProjectRoundUnitID = @SponsorProjectRoundUnitID";
			SponsorProjectRoundUnit sponsorProjectRoundUnit = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorProjectRoundUnitID", id))) {
				if (rs.Read()) {
					sponsorProjectRoundUnit = new SponsorProjectRoundUnit {
						SponsorProjectRoundUnitID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						ProjectRoundUnitID = GetInt32(rs, 2),
						Nav = GetString(rs, 3),
						SurveyKey = GetGuid(rs, 4),
						SortOrder = GetInt32(rs, 5),
						Feedback = GetString(rs, 6),
						Ext = GetInt32(rs, 7),
						SurveyID = GetInt32(rs, 8),
						OnlyEveryDays = GetInt32(rs, 9),
						GoToStatistics = GetInt32(rs, 10)
					};
				}
			}
			return sponsorProjectRoundUnit;
		}
		
		public override IList<SponsorProjectRoundUnit> FindAll()
		{
			string query = @"
SELECT 	SponsorProjectRoundUnitID, 
	SponsorID, 
	ProjectRoundUnitID, 
	Nav, 
	SurveyKey, 
	SortOrder, 
	Feedback, 
	Ext, 
	SurveyID, 
	OnlyEveryDays, 
	GoToStatistics
FROM SponsorProjectRoundUnit";
			var sponsorProjectRoundUnits = new List<SponsorProjectRoundUnit>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorProjectRoundUnits.Add(new SponsorProjectRoundUnit {
						SponsorProjectRoundUnitID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						ProjectRoundUnitID = GetInt32(rs, 2),
						Nav = GetString(rs, 3),
						SurveyKey = GetGuid(rs, 4),
						SortOrder = GetInt32(rs, 5),
						Feedback = GetString(rs, 6),
						Ext = GetInt32(rs, 7),
						SurveyID = GetInt32(rs, 8),
						OnlyEveryDays = GetInt32(rs, 9),
						GoToStatistics = GetInt32(rs, 10)
					});
				}
			}
			return sponsorProjectRoundUnits;
		}
	}
}
