using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlSurveyQuestionOptionComponentRepository : BaseSqlRepository<SurveyQuestionOptionComponent>
	{
		public SqlSurveyQuestionOptionComponentRepository()
		{
		}
		
		public override void Save(SurveyQuestionOptionComponent surveyQuestionOptionComponent)
		{
			string query = @"
INSERT INTO SurveyQuestionOptionComponent(
	SurveyQuestionOptionComponentID, 
	SurveyQuestionOptionID, 
	OptionComponentID, 
	Hide, 
	OnClick
)
VALUES(
	@SurveyQuestionOptionComponentID, 
	@SurveyQuestionOptionID, 
	@OptionComponentID, 
	@Hide, 
	@OnClick
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionOptionComponentID", surveyQuestionOptionComponent.SurveyQuestionOptionComponentID),
				new SqlParameter("@SurveyQuestionOptionID", surveyQuestionOptionComponent.SurveyQuestionOptionID),
				new SqlParameter("@OptionComponentID", surveyQuestionOptionComponent.OptionComponentID),
				new SqlParameter("@Hide", surveyQuestionOptionComponent.Hide),
				new SqlParameter("@OnClick", surveyQuestionOptionComponent.OnClick)
			);
		}
		
		public override void Update(SurveyQuestionOptionComponent surveyQuestionOptionComponent, int id)
		{
			string query = @"
UPDATE SurveyQuestionOptionComponent SET
	SurveyQuestionOptionComponentID = @SurveyQuestionOptionComponentID,
	SurveyQuestionOptionID = @SurveyQuestionOptionID,
	OptionComponentID = @OptionComponentID,
	Hide = @Hide,
	OnClick = @OnClick
WHERE SurveyQuestionOptionComponentID = @SurveyQuestionOptionComponentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionOptionComponentID", surveyQuestionOptionComponent.SurveyQuestionOptionComponentID),
				new SqlParameter("@SurveyQuestionOptionID", surveyQuestionOptionComponent.SurveyQuestionOptionID),
				new SqlParameter("@OptionComponentID", surveyQuestionOptionComponent.OptionComponentID),
				new SqlParameter("@Hide", surveyQuestionOptionComponent.Hide),
				new SqlParameter("@OnClick", surveyQuestionOptionComponent.OnClick)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SurveyQuestionOptionComponent
WHERE SurveyQuestionOptionComponentID = @SurveyQuestionOptionComponentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionOptionComponentID", id)
			);
		}
		
		public override SurveyQuestionOptionComponent Read(int id)
		{
			string query = @"
SELECT 	SurveyQuestionOptionComponentID, 
	SurveyQuestionOptionID, 
	OptionComponentID, 
	Hide, 
	OnClick
FROM SurveyQuestionOptionComponent
WHERE SurveyQuestionOptionComponentID = @SurveyQuestionOptionComponentID";
			SurveyQuestionOptionComponent surveyQuestionOptionComponent = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SurveyQuestionOptionComponentID", id))) {
				if (rs.Read()) {
					surveyQuestionOptionComponent = new SurveyQuestionOptionComponent {
						SurveyQuestionOptionComponentID = GetInt32(rs, 0),
						SurveyQuestionOptionID = GetInt32(rs, 1),
						OptionComponentID = GetInt32(rs, 2),
						Hide = GetInt32(rs, 3),
						OnClick = GetString(rs, 4)
					};
				}
			}
			return surveyQuestionOptionComponent;
		}
		
		public override IList<SurveyQuestionOptionComponent> FindAll()
		{
			string query = @"
SELECT 	SurveyQuestionOptionComponentID, 
	SurveyQuestionOptionID, 
	OptionComponentID, 
	Hide, 
	OnClick
FROM SurveyQuestionOptionComponent";
			var surveyQuestionOptionComponents = new List<SurveyQuestionOptionComponent>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					surveyQuestionOptionComponents.Add(new SurveyQuestionOptionComponent {
						SurveyQuestionOptionComponentID = GetInt32(rs, 0),
						SurveyQuestionOptionID = GetInt32(rs, 1),
						OptionComponentID = GetInt32(rs, 2),
						Hide = GetInt32(rs, 3),
						OnClick = GetString(rs, 4)
					});
				}
			}
			return surveyQuestionOptionComponents;
		}
	}
}
