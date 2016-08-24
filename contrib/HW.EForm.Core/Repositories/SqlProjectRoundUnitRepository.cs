using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlProjectRoundUnitRepository : BaseSqlRepository<ProjectRoundUnit>
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
	RequiredAnswerCount
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
	@RequiredAnswerCount
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundUnitID", projectRoundUnit.ProjectRoundUnitID),
				new SqlParameter("@ProjectRoundID", projectRoundUnit.ProjectRoundID),
				new SqlParameter("@Unit", projectRoundUnit.Unit),
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
				new SqlParameter("@RequiredAnswerCount", projectRoundUnit.RequiredAnswerCount)
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
	RequiredAnswerCount = @RequiredAnswerCount
WHERE ProjectRoundUnitID = @ProjectRoundUnitID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundUnitID", projectRoundUnit.ProjectRoundUnitID),
				new SqlParameter("@ProjectRoundID", projectRoundUnit.ProjectRoundID),
				new SqlParameter("@Unit", projectRoundUnit.Unit),
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
				new SqlParameter("@RequiredAnswerCount", projectRoundUnit.RequiredAnswerCount)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ProjectRoundUnit
WHERE ProjectRoundUnitID = @ProjectRoundUnitID";
			ExecuteNonQuery(
				query,
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
	RequiredAnswerCount
FROM ProjectRoundUnit
WHERE ProjectRoundUnitID = @ProjectRoundUnitID";
			ProjectRoundUnit projectRoundUnit = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ProjectRoundUnitID", id))) {
				if (rs.Read()) {
					projectRoundUnit = new ProjectRoundUnit {
						ProjectRoundUnitID = GetInt32(rs, 0),
						ProjectRoundID = GetInt32(rs, 1),
						Unit = GetString(rs, 2),
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
						RequiredAnswerCount = GetInt32(rs, 21)
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
	RequiredAnswerCount
FROM ProjectRoundUnit";
			var projectRoundUnits = new List<ProjectRoundUnit>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					projectRoundUnits.Add(new ProjectRoundUnit {
						ProjectRoundUnitID = GetInt32(rs, 0),
						ProjectRoundID = GetInt32(rs, 1),
						Unit = GetString(rs, 2),
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
						RequiredAnswerCount = GetInt32(rs, 21)
					});
				}
			}
			return projectRoundUnits;
		}
		
		public IList<ProjectRoundUnit> FindByProjectRound(int projectRoundID)
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
	RequiredAnswerCount
FROM ProjectRoundUnit
WHERE ProjectRoundID = @ProjectRoundID";
			var projectRoundUnits = new List<ProjectRoundUnit>();
			using (var rs = ExecuteReader(query, new SqlParameter("@ProjectRoundID", projectRoundID))) {
				while (rs.Read()) {
					projectRoundUnits.Add(new ProjectRoundUnit {
						ProjectRoundUnitID = GetInt32(rs, 0),
						ProjectRoundID = GetInt32(rs, 1),
						Unit = GetString(rs, 2),
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
						RequiredAnswerCount = GetInt32(rs, 21)
					});
				}
			}
			return projectRoundUnits;
		}
		
		public IList<ProjectRoundUnit> FindByProjectRoundAndManager(int projectRoundID, int managerID)
		{
			string query = @"
SELECT 	pru.ProjectRoundUnitID, 
	pru.ProjectRoundID, 
	pru.Unit, 
	pru.ID, 
	pru.ParentProjectRoundUnitID, 
	pru.SortOrder, 
	pru.SortString, 
	pru.SurveyID, 
	pru.LangID, 
	pru.UnitKey, 
	pru.UserCount, 
	pru.UnitCategoryID, 
	pru.CanHaveUsers, 
	pru.ReportID, 
	pru.Timeframe, 
	pru.Yellow, 
	pru.Green, 
	pru.SurveyIntro, 
	pru.Terminated, 
	pru.IndividualReportID, 
	pru.UniqueID, 
	pru.RequiredAnswerCount
FROM ProjectRoundUnit pru
INNER JOIN ManagerProjectRoundUnit mpru ON mpru.ProjectRoundUnitID = pru.ProjectRoundUnitID
INNER JOIN ManagerProjectRound mpr ON mpr.ProjectRoundID = mpru.ProjectRoundID AND mpru.ManagerID = @ManagerID
WHERE pru.ProjectRoundID = @ProjectRoundID";
			var projectRoundUnits = new List<ProjectRoundUnit>();
			using (var rs = ExecuteReader(query, new SqlParameter("@ProjectRoundID", projectRoundID), new SqlParameter("@ManagerID", managerID))) {
				while (rs.Read()) {
					projectRoundUnits.Add(new ProjectRoundUnit {
						ProjectRoundUnitID = GetInt32(rs, 0),
						ProjectRoundID = GetInt32(rs, 1),
						Unit = GetString(rs, 2),
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
						RequiredAnswerCount = GetInt32(rs, 21)
					});
				}
			}
			return projectRoundUnits;
		}
	}
}
