using System;
using HW.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core.Repositories.Sql
{
	public class SqlProjectRoundUnitRepository : BaseSqlRepository<ProjectRoundUnit>, IProjectRoundUnitRepository
	{
		public SqlProjectRoundUnitRepository()
		{
		}
		
		public override void Save(ProjectRoundUnit projectRoundUnit)
		{
			string query = @"
INSERT INTO ProjectRoundUnit(
	ProjectRoundUnitID, 
	ProjectRoundID, 
	Unit, 
	ID, 
	ParentProjectRoundUnitID, 
	SortOrder, 
	SortString, 
	SurveyID, 
	LangID, 
	UnitKey, 
	UserCount, 
	UnitCategoryID, 
	CanHaveUsers, 
	ReportID, 
	Timeframe, 
	Yellow, 
	Green, 
	SurveyIntro, 
	Terminated, 
	IndividualReportID, 
	UniqueID, 
	RequiredAnswerCount, 
	SortStringLength
)
VALUES(
	@ProjectRoundUnitID, 
	@ProjectRoundID, 
	@Unit, 
	@ID, 
	@ParentProjectRoundUnitID, 
	@SortOrder, 
	@SortString, 
	@SurveyID, 
	@LangID, 
	@UnitKey, 
	@UserCount, 
	@UnitCategoryID, 
	@CanHaveUsers, 
	@ReportID, 
	@Timeframe, 
	@Yellow, 
	@Green, 
	@SurveyIntro, 
	@Terminated, 
	@IndividualReportID, 
	@UniqueID, 
	@RequiredAnswerCount, 
	@SortStringLength
)";
			ExecuteNonQuery(
				query,
				"eFormSqlConnection",
				new SqlParameter("@ProjectRoundUnitID", projectRoundUnit.ProjectRoundUnitID),
				new SqlParameter("@ProjectRoundID", projectRoundUnit.ProjectRoundID),
				new SqlParameter("@Unit", projectRoundUnit.Name),
				new SqlParameter("@ID", projectRoundUnit.ID),
				new SqlParameter("@ParentProjectRoundUnitID", projectRoundUnit.ParentProjectRoundUnitID),
				new SqlParameter("@SortOrder", projectRoundUnit.SortOrder),
				new SqlParameter("@SortString", projectRoundUnit.SortString),
				new SqlParameter("@SurveyID", projectRoundUnit.SurveyID),
				new SqlParameter("@LangID", projectRoundUnit.LangID),
				new SqlParameter("@UnitKey", projectRoundUnit.UnitKey),
				new SqlParameter("@UserCount", projectRoundUnit.UserCount),
				new SqlParameter("@UnitCategoryID", projectRoundUnit.UnitCategoryID),
				new SqlParameter("@CanHaveUsers", projectRoundUnit.CanHaveUsers),
				new SqlParameter("@ReportID", projectRoundUnit.ReportID),
				new SqlParameter("@Timeframe", projectRoundUnit.Timeframe),
				new SqlParameter("@Yellow", projectRoundUnit.Yellow),
				new SqlParameter("@Green", projectRoundUnit.Green),
				new SqlParameter("@SurveyIntro", projectRoundUnit.SurveyIntro),
				new SqlParameter("@Terminated", projectRoundUnit.Terminated),
				new SqlParameter("@IndividualReportID", projectRoundUnit.IndividualReportID),
				new SqlParameter("@UniqueID", projectRoundUnit.UniqueID),
				new SqlParameter("@RequiredAnswerCount", projectRoundUnit.RequiredAnswerCount),
				new SqlParameter("@SortStringLength", projectRoundUnit.SortStringLength)
			);
		}
		
		public override void Update(ProjectRoundUnit projectRoundUnit, int id)
		{
			string query = @"
UPDATE ProjectRoundUnit SET
	ProjectRoundUnitID = @ProjectRoundUnitID,
	ProjectRoundID = @ProjectRoundID,
	Unit = @Unit,
	ID = @ID,
	ParentProjectRoundUnitID = @ParentProjectRoundUnitID,
	SortOrder = @SortOrder,
	SortString = @SortString,
	SurveyID = @SurveyID,
	LangID = @LangID,
	UnitKey = @UnitKey,
	UserCount = @UserCount,
	UnitCategoryID = @UnitCategoryID,
	CanHaveUsers = @CanHaveUsers,
	ReportID = @ReportID,
	Timeframe = @Timeframe,
	Yellow = @Yellow,
	Green = @Green,
	SurveyIntro = @SurveyIntro,
	Terminated = @Terminated,
	IndividualReportID = @IndividualReportID,
	UniqueID = @UniqueID,
	RequiredAnswerCount = @RequiredAnswerCount,
	SortStringLength = @SortStringLength
WHERE ProjectRoundUnitID = @ProjectRoundUnitID";
			ExecuteNonQuery(
				query,
				"eFormSqlConnection",
				new SqlParameter("@ProjectRoundUnitID", projectRoundUnit.ProjectRoundUnitID),
				new SqlParameter("@ProjectRoundID", projectRoundUnit.ProjectRoundID),
				new SqlParameter("@Unit", projectRoundUnit.Name),
				new SqlParameter("@ID", projectRoundUnit.ID),
				new SqlParameter("@ParentProjectRoundUnitID", projectRoundUnit.ParentProjectRoundUnitID),
				new SqlParameter("@SortOrder", projectRoundUnit.SortOrder),
				new SqlParameter("@SortString", projectRoundUnit.SortString),
				new SqlParameter("@SurveyID", projectRoundUnit.SurveyID),
				new SqlParameter("@LangID", projectRoundUnit.LangID),
				new SqlParameter("@UnitKey", projectRoundUnit.UnitKey),
				new SqlParameter("@UserCount", projectRoundUnit.UserCount),
				new SqlParameter("@UnitCategoryID", projectRoundUnit.UnitCategoryID),
				new SqlParameter("@CanHaveUsers", projectRoundUnit.CanHaveUsers),
				new SqlParameter("@ReportID", projectRoundUnit.ReportID),
				new SqlParameter("@Timeframe", projectRoundUnit.Timeframe),
				new SqlParameter("@Yellow", projectRoundUnit.Yellow),
				new SqlParameter("@Green", projectRoundUnit.Green),
				new SqlParameter("@SurveyIntro", projectRoundUnit.SurveyIntro),
				new SqlParameter("@Terminated", projectRoundUnit.Terminated),
				new SqlParameter("@IndividualReportID", projectRoundUnit.IndividualReportID),
				new SqlParameter("@UniqueID", projectRoundUnit.UniqueID),
				new SqlParameter("@RequiredAnswerCount", projectRoundUnit.RequiredAnswerCount),
				new SqlParameter("@SortStringLength", projectRoundUnit.SortStringLength)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ProjectRoundUnit
WHERE ProjectRoundUnitID = @ProjectRoundUnitID";
			ExecuteNonQuery(
				query,
				"eFormSqlConnection",
				new SqlParameter("@ProjectRoundUnitID", id)
			);
		}
		
		public override ProjectRoundUnit Read(int id)
		{
			string query = @"
SELECT 	ProjectRoundUnitID, 
	ProjectRoundID, 
	Unit, 
	ID, 
	ParentProjectRoundUnitID, 
	SortOrder, 
	SortString, 
	SurveyID, 
	LangID, 
	UnitKey, 
	UserCount, 
	UnitCategoryID, 
	CanHaveUsers, 
	ReportID, 
	Timeframe, 
	Yellow, 
	Green, 
	SurveyIntro, 
	Terminated, 
	IndividualReportID, 
	UniqueID, 
	RequiredAnswerCount, 
	SortStringLength
FROM ProjectRoundUnit
WHERE ProjectRoundUnitID = @ProjectRoundUnitID";
			ProjectRoundUnit projectRoundUnit = null;
			using (var rs = ExecuteReader(query, "eFormSqlConnection", new SqlParameter("@ProjectRoundUnitID", id))) {
				if (rs.Read()) {
					projectRoundUnit = new ProjectRoundUnit {
						ProjectRoundUnitID = GetInt32(rs, 0),
						ProjectRoundID = GetInt32(rs, 1),
						Name = GetString(rs, 2),
						ID = GetString(rs, 3),
						ParentProjectRoundUnitID = GetInt32(rs, 4),
						SortOrder = GetInt32(rs, 5),
						SortString = GetString(rs, 6),
						SurveyID = GetInt32(rs, 7),
						LangID = GetInt32(rs, 8),
						UnitKey = GetGuid(rs, 9),
						UserCount = GetInt32(rs, 10),
						UnitCategoryID = GetInt32(rs, 11),
						CanHaveUsers = GetBoolean(rs, 12),
						ReportID = GetInt32(rs, 13),
						Timeframe = GetInt32(rs, 14),
						Yellow = GetInt32(rs, 15),
						Green = GetInt32(rs, 16),
						SurveyIntro = GetString(rs, 17),
						Terminated = GetBoolean(rs, 18),
						IndividualReportID = GetInt32(rs, 19),
						UniqueID = GetString(rs, 20),
						RequiredAnswerCount = GetInt32(rs, 21),
						SortStringLength = GetInt32(rs, 22)
					};
				}
			}
			return projectRoundUnit;
		}
		
		public override IList<ProjectRoundUnit> FindAll()
		{
			string query = @"
SELECT 	ProjectRoundUnitID, 
	ProjectRoundID, 
	Unit, 
	ID, 
	ParentProjectRoundUnitID, 
	SortOrder, 
	SortString, 
	SurveyID, 
	LangID, 
	UnitKey, 
	UserCount, 
	UnitCategoryID, 
	CanHaveUsers, 
	ReportID, 
	Timeframe, 
	Yellow, 
	Green, 
	SurveyIntro, 
	Terminated, 
	IndividualReportID, 
	UniqueID, 
	RequiredAnswerCount, 
	SortStringLength
FROM ProjectRoundUnit";
			var projectRoundUnits = new List<ProjectRoundUnit>();
			using (var rs = ExecuteReader(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					projectRoundUnits.Add(new ProjectRoundUnit {
						ProjectRoundUnitID = GetInt32(rs, 0),
						ProjectRoundID = GetInt32(rs, 1),
						Name = GetString(rs, 2),
						ID = GetString(rs, 3),
						ParentProjectRoundUnitID = GetInt32(rs, 4),
						SortOrder = GetInt32(rs, 5),
						SortString = GetString(rs, 6),
						SurveyID = GetInt32(rs, 7),
						LangID = GetInt32(rs, 8),
						UnitKey = GetGuid(rs, 9),
						UserCount = GetInt32(rs, 10),
						UnitCategoryID = GetInt32(rs, 11),
						CanHaveUsers = GetBoolean(rs, 12),
						ReportID = GetInt32(rs, 13),
						Timeframe = GetInt32(rs, 14),
						Yellow = GetInt32(rs, 15),
						Green = GetInt32(rs, 16),
						SurveyIntro = GetString(rs, 17),
						Terminated = GetBoolean(rs, 18),
						IndividualReportID = GetInt32(rs, 19),
						UniqueID = GetString(rs, 20),
						RequiredAnswerCount = GetInt32(rs, 21),
						SortStringLength = GetInt32(rs, 22)
					});
				}
			}
			return projectRoundUnits;
		}
	}
}
