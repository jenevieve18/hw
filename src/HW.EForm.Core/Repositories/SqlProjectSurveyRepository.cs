using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlProjectSurveyRepository : BaseSqlRepository<ProjectSurvey>
	{
		public SqlProjectSurveyRepository()
		{
		}
		
		public override void Save(ProjectSurvey projectSurvey)
		{
			string query = @"
INSERT INTO ProjectSurvey(
	ProjectSurveyID, 
	ProjectID, 
	SurveyID
)
VALUES(
	@ProjectSurveyID, 
	@ProjectID, 
	@SurveyID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectSurveyID", projectSurvey.ProjectSurveyID),
				new SqlParameter("@ProjectID", projectSurvey.ProjectID),
				new SqlParameter("@SurveyID", projectSurvey.SurveyID)
			);
		}
		
		public override void Update(ProjectSurvey projectSurvey, int id)
		{
			string query = @"
UPDATE ProjectSurvey SET
	ProjectSurveyID = @ProjectSurveyID,
	ProjectID = @ProjectID,
	SurveyID = @SurveyID
WHERE ProjectSurveyID = @ProjectSurveyID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectSurveyID", projectSurvey.ProjectSurveyID),
				new SqlParameter("@ProjectID", projectSurvey.ProjectID),
				new SqlParameter("@SurveyID", projectSurvey.SurveyID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ProjectSurvey
WHERE ProjectSurveyID = @ProjectSurveyID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectSurveyID", id)
			);
		}
		
		public override ProjectSurvey Read(int id)
		{
			string query = @"
SELECT 	ProjectSurveyID, 
	ProjectID, 
	SurveyID
FROM ProjectSurvey
WHERE ProjectSurveyID = @ProjectSurveyID";
			ProjectSurvey projectSurvey = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ProjectSurveyID", id))) {
				if (rs.Read()) {
					projectSurvey = new ProjectSurvey {
						ProjectSurveyID = GetInt32(rs, 0),
						ProjectID = GetInt32(rs, 1),
						SurveyID = GetInt32(rs, 2)
					};
				}
			}
			return projectSurvey;
		}
		
		public override IList<ProjectSurvey> FindAll()
		{
			string query = @"
SELECT 	ProjectSurveyID, 
	ProjectID, 
	SurveyID
FROM ProjectSurvey";
			var projectSurveys = new List<ProjectSurvey>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					projectSurveys.Add(new ProjectSurvey {
						ProjectSurveyID = GetInt32(rs, 0),
						ProjectID = GetInt32(rs, 1),
						SurveyID = GetInt32(rs, 2)
					});
				}
			}
			return projectSurveys;
		}
	}
}
