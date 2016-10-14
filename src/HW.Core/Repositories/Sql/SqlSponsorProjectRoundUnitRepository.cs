// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core.Repositories.Sql
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
	GoToStatistics, 
	DefaultAggregation
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
	@GoToStatistics, 
	@DefaultAggregation
)";
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@SponsorProjectRoundUnitID", sponsorProjectRoundUnit.SponsorProjectRoundUnitID),
				new SqlParameter("@SponsorID", sponsorProjectRoundUnit.SponsorID),
				new SqlParameter("@ProjectRoundUnitID", sponsorProjectRoundUnit.ProjectRoundUnitID),
				new SqlParameter("@Nav", sponsorProjectRoundUnit.Navigation),
				new SqlParameter("@SurveyKey", sponsorProjectRoundUnit.SurveyKey),
				new SqlParameter("@SortOrder", sponsorProjectRoundUnit.SortOrder),
				new SqlParameter("@Feedback", sponsorProjectRoundUnit.Feedback),
				new SqlParameter("@Ext", sponsorProjectRoundUnit.Ext),
				new SqlParameter("@SurveyID", sponsorProjectRoundUnit.SurveyID),
				new SqlParameter("@OnlyEveryDays", sponsorProjectRoundUnit.OnlyEveryDays),
				new SqlParameter("@GoToStatistics", sponsorProjectRoundUnit.GoToStatistics),
				new SqlParameter("@DefaultAggregation", sponsorProjectRoundUnit.DefaultAggregation)
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
	GoToStatistics = @GoToStatistics,
	DefaultAggregation = @DefaultAggregation
WHERE SponsorProjectRoundUnitID = @SponsorProjectRoundUnitID";
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@SponsorProjectRoundUnitID", sponsorProjectRoundUnit.SponsorProjectRoundUnitID),
				new SqlParameter("@SponsorID", sponsorProjectRoundUnit.SponsorID),
				new SqlParameter("@ProjectRoundUnitID", sponsorProjectRoundUnit.ProjectRoundUnitID),
				new SqlParameter("@Nav", sponsorProjectRoundUnit.Navigation),
				new SqlParameter("@SurveyKey", sponsorProjectRoundUnit.SurveyKey),
				new SqlParameter("@SortOrder", sponsorProjectRoundUnit.SortOrder),
				new SqlParameter("@Feedback", sponsorProjectRoundUnit.Feedback),
				new SqlParameter("@Ext", sponsorProjectRoundUnit.Ext),
				new SqlParameter("@SurveyID", sponsorProjectRoundUnit.SurveyID),
				new SqlParameter("@OnlyEveryDays", sponsorProjectRoundUnit.OnlyEveryDays),
				new SqlParameter("@GoToStatistics", sponsorProjectRoundUnit.GoToStatistics),
				new SqlParameter("@DefaultAggregation", sponsorProjectRoundUnit.DefaultAggregation)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorProjectRoundUnit
WHERE SponsorProjectRoundUnitID = @SponsorProjectRoundUnitID";
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
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
	GoToStatistics, 
	DefaultAggregation
FROM SponsorProjectRoundUnit
WHERE SponsorProjectRoundUnitID = @SponsorProjectRoundUnitID";
			SponsorProjectRoundUnit sponsorProjectRoundUnit = null;
			using (var rs = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@SponsorProjectRoundUnitID", id))) {
				if (rs.Read()) {
					sponsorProjectRoundUnit = new SponsorProjectRoundUnit {
						SponsorProjectRoundUnitID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						ProjectRoundUnitID = GetInt32(rs, 2),
						Navigation = GetString(rs, 3),
//						SurveyKey = GetGuid(rs, 4),
						SurveyKey = GetString(rs, 4),
						SortOrder = GetInt32(rs, 5),
						Feedback = GetString(rs, 6),
						Ext = GetInt32(rs, 7),
						SurveyID = GetInt32(rs, 8),
						OnlyEveryDays = GetInt32(rs, 9),
						GoToStatistics = GetInt32(rs, 10),
						DefaultAggregation = GetInt32(rs, 11)
					};
				}
			}
			return sponsorProjectRoundUnit;
		}
		
		public SponsorProjectRoundUnit ReadByProjectRoundUnit(int projectRoundUnitID)
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
	GoToStatistics, 
	DefaultAggregation
FROM SponsorProjectRoundUnit
WHERE ProjectRoundUnitID = @ProjectRoundUnitID";
			SponsorProjectRoundUnit sponsorProjectRoundUnit = null;
			using (var rs = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@ProjectRoundUnitID", projectRoundUnitID))) {
				if (rs.Read()) {
					sponsorProjectRoundUnit = new SponsorProjectRoundUnit {
						SponsorProjectRoundUnitID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						ProjectRoundUnitID = GetInt32(rs, 2),
						Navigation = GetString(rs, 3),
//						SurveyKey = GetString(rs, 4),
						SortOrder = GetInt32(rs, 5),
						Feedback = GetString(rs, 6),
						Ext = GetInt32(rs, 7),
						SurveyID = GetInt32(rs, 8),
						OnlyEveryDays = GetInt32(rs, 9),
						GoToStatistics = GetInt32(rs, 10),
						DefaultAggregation = GetInt32(rs, 11)
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
	GoToStatistics, 
	DefaultAggregation
FROM SponsorProjectRoundUnit";
			var sponsorProjectRoundUnits = new List<SponsorProjectRoundUnit>();
			using (var rs = ExecuteReader(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					sponsorProjectRoundUnits.Add(new SponsorProjectRoundUnit {
						SponsorProjectRoundUnitID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						ProjectRoundUnitID = GetInt32(rs, 2),
						Navigation = GetString(rs, 3),
//						SurveyKey = GetGuid(rs, 4),
						SurveyKey = GetString(rs, 4),
						SortOrder = GetInt32(rs, 5),
						Feedback = GetString(rs, 6),
						Ext = GetInt32(rs, 7),
						SurveyID = GetInt32(rs, 8),
						OnlyEveryDays = GetInt32(rs, 9),
						GoToStatistics = GetInt32(rs, 10),
						DefaultAggregation = GetInt32(rs, 11)
					});
				}
			}
			return sponsorProjectRoundUnits;
		}
	}
}