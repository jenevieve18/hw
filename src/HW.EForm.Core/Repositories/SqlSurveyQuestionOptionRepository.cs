using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlSurveyQuestionOptionRepository : BaseSqlRepository<SurveyQuestionOption>
	{
		public SqlSurveyQuestionOptionRepository()
		{
		}
		
		public override void Save(SurveyQuestionOption surveyQuestionOption)
		{
			string query = @"
INSERT INTO SurveyQuestionOption(
	SurveyQuestionOptionID, 
	SurveyQuestionID, 
	QuestionOptionID, 
	OptionPlacement, 
	Variablename, 
	Forced, 
	SortOrder, 
	Warn, 
	Height, 
	Width
)
VALUES(
	@SurveyQuestionOptionID, 
	@SurveyQuestionID, 
	@QuestionOptionID, 
	@OptionPlacement, 
	@Variablename, 
	@Forced, 
	@SortOrder, 
	@Warn, 
	@Height, 
	@Width
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionOptionID", surveyQuestionOption.SurveyQuestionOptionID),
				new SqlParameter("@SurveyQuestionID", surveyQuestionOption.SurveyQuestionID),
				new SqlParameter("@QuestionOptionID", surveyQuestionOption.QuestionOptionID),
				new SqlParameter("@OptionPlacement", surveyQuestionOption.OptionPlacement),
				new SqlParameter("@Variablename", surveyQuestionOption.Variablename),
				new SqlParameter("@Forced", surveyQuestionOption.Forced),
				new SqlParameter("@SortOrder", surveyQuestionOption.SortOrder),
				new SqlParameter("@Warn", surveyQuestionOption.Warn),
				new SqlParameter("@Height", surveyQuestionOption.Height),
				new SqlParameter("@Width", surveyQuestionOption.Width)
			);
		}
		
		public override void Update(SurveyQuestionOption surveyQuestionOption, int id)
		{
			string query = @"
UPDATE SurveyQuestionOption SET
	SurveyQuestionOptionID = @SurveyQuestionOptionID,
	SurveyQuestionID = @SurveyQuestionID,
	QuestionOptionID = @QuestionOptionID,
	OptionPlacement = @OptionPlacement,
	Variablename = @Variablename,
	Forced = @Forced,
	SortOrder = @SortOrder,
	Warn = @Warn,
	Height = @Height,
	Width = @Width
WHERE SurveyQuestionOptionID = @SurveyQuestionOptionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionOptionID", surveyQuestionOption.SurveyQuestionOptionID),
				new SqlParameter("@SurveyQuestionID", surveyQuestionOption.SurveyQuestionID),
				new SqlParameter("@QuestionOptionID", surveyQuestionOption.QuestionOptionID),
				new SqlParameter("@OptionPlacement", surveyQuestionOption.OptionPlacement),
				new SqlParameter("@Variablename", surveyQuestionOption.Variablename),
				new SqlParameter("@Forced", surveyQuestionOption.Forced),
				new SqlParameter("@SortOrder", surveyQuestionOption.SortOrder),
				new SqlParameter("@Warn", surveyQuestionOption.Warn),
				new SqlParameter("@Height", surveyQuestionOption.Height),
				new SqlParameter("@Width", surveyQuestionOption.Width)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SurveyQuestionOption
WHERE SurveyQuestionOptionID = @SurveyQuestionOptionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionOptionID", id)
			);
		}
		
		public override SurveyQuestionOption Read(int id)
		{
			string query = @"
SELECT 	SurveyQuestionOptionID, 
	SurveyQuestionID, 
	QuestionOptionID, 
	OptionPlacement, 
	Variablename, 
	Forced, 
	SortOrder, 
	Warn, 
	Height, 
	Width
FROM SurveyQuestionOption
WHERE SurveyQuestionOptionID = @SurveyQuestionOptionID";
			SurveyQuestionOption surveyQuestionOption = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SurveyQuestionOptionID", id))) {
				if (rs.Read()) {
					surveyQuestionOption = new SurveyQuestionOption {
						SurveyQuestionOptionID = GetInt32(rs, 0),
						SurveyQuestionID = GetInt32(rs, 1),
						QuestionOptionID = GetInt32(rs, 2),
						OptionPlacement = GetInt32(rs, 3),
						Variablename = GetString(rs, 4),
						Forced = GetInt32(rs, 5),
						SortOrder = GetInt32(rs, 6),
						Warn = GetInt32(rs, 7),
						Height = GetInt32(rs, 8),
						Width = GetInt32(rs, 9)
					};
				}
			}
			return surveyQuestionOption;
		}
		
		public override IList<SurveyQuestionOption> FindAll()
		{
			string query = @"
SELECT 	SurveyQuestionOptionID, 
	SurveyQuestionID, 
	QuestionOptionID, 
	OptionPlacement, 
	Variablename, 
	Forced, 
	SortOrder, 
	Warn, 
	Height, 
	Width
FROM SurveyQuestionOption";
			var surveyQuestionOptions = new List<SurveyQuestionOption>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					surveyQuestionOptions.Add(new SurveyQuestionOption {
						SurveyQuestionOptionID = GetInt32(rs, 0),
						SurveyQuestionID = GetInt32(rs, 1),
						QuestionOptionID = GetInt32(rs, 2),
						OptionPlacement = GetInt32(rs, 3),
						Variablename = GetString(rs, 4),
						Forced = GetInt32(rs, 5),
						SortOrder = GetInt32(rs, 6),
						Warn = GetInt32(rs, 7),
						Height = GetInt32(rs, 8),
						Width = GetInt32(rs, 9)
					});
				}
			}
			return surveyQuestionOptions;
		}
	}
}
