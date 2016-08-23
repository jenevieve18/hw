using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlSurveyQuestionRepository : BaseSqlRepository<SurveyQuestion>
	{
		public SqlSurveyQuestionRepository()
		{
		}
		
		public override void Save(SurveyQuestion surveyQuestion)
		{
			string query = @"
INSERT INTO SurveyQuestion(
	SurveyQuestionID, 
	SurveyID, 
	QuestionID, 
	OptionsPlacement, 
	Variablename, 
	SortOrder, 
	NoCount, 
	RestartCount, 
	ExtendedFirst, 
	NoBreak, 
	BreakAfterQuestion, 
	PageBreakBeforeQuestion, 
	FontSize
)
VALUES(
	@SurveyQuestionID, 
	@SurveyID, 
	@QuestionID, 
	@OptionsPlacement, 
	@Variablename, 
	@SortOrder, 
	@NoCount, 
	@RestartCount, 
	@ExtendedFirst, 
	@NoBreak, 
	@BreakAfterQuestion, 
	@PageBreakBeforeQuestion, 
	@FontSize
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionID", surveyQuestion.SurveyQuestionID),
				new SqlParameter("@SurveyID", surveyQuestion.SurveyID),
				new SqlParameter("@QuestionID", surveyQuestion.QuestionID),
				new SqlParameter("@OptionsPlacement", surveyQuestion.OptionsPlacement),
				new SqlParameter("@Variablename", surveyQuestion.Variablename),
				new SqlParameter("@SortOrder", surveyQuestion.SortOrder),
				new SqlParameter("@NoCount", surveyQuestion.NoCount),
				new SqlParameter("@RestartCount", surveyQuestion.RestartCount),
				new SqlParameter("@ExtendedFirst", surveyQuestion.ExtendedFirst),
				new SqlParameter("@NoBreak", surveyQuestion.NoBreak),
				new SqlParameter("@BreakAfterQuestion", surveyQuestion.BreakAfterQuestion),
				new SqlParameter("@PageBreakBeforeQuestion", surveyQuestion.PageBreakBeforeQuestion),
				new SqlParameter("@FontSize", surveyQuestion.FontSize)
			);
		}
		
		public override void Update(SurveyQuestion surveyQuestion, int id)
		{
			string query = @"
UPDATE SurveyQuestion SET
	SurveyQuestionID = @SurveyQuestionID,
	SurveyID = @SurveyID,
	QuestionID = @QuestionID,
	OptionsPlacement = @OptionsPlacement,
	Variablename = @Variablename,
	SortOrder = @SortOrder,
	NoCount = @NoCount,
	RestartCount = @RestartCount,
	ExtendedFirst = @ExtendedFirst,
	NoBreak = @NoBreak,
	BreakAfterQuestion = @BreakAfterQuestion,
	PageBreakBeforeQuestion = @PageBreakBeforeQuestion,
	FontSize = @FontSize
WHERE SurveyQuestionID = @SurveyQuestionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionID", surveyQuestion.SurveyQuestionID),
				new SqlParameter("@SurveyID", surveyQuestion.SurveyID),
				new SqlParameter("@QuestionID", surveyQuestion.QuestionID),
				new SqlParameter("@OptionsPlacement", surveyQuestion.OptionsPlacement),
				new SqlParameter("@Variablename", surveyQuestion.Variablename),
				new SqlParameter("@SortOrder", surveyQuestion.SortOrder),
				new SqlParameter("@NoCount", surveyQuestion.NoCount),
				new SqlParameter("@RestartCount", surveyQuestion.RestartCount),
				new SqlParameter("@ExtendedFirst", surveyQuestion.ExtendedFirst),
				new SqlParameter("@NoBreak", surveyQuestion.NoBreak),
				new SqlParameter("@BreakAfterQuestion", surveyQuestion.BreakAfterQuestion),
				new SqlParameter("@PageBreakBeforeQuestion", surveyQuestion.PageBreakBeforeQuestion),
				new SqlParameter("@FontSize", surveyQuestion.FontSize)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SurveyQuestion
WHERE SurveyQuestionID = @SurveyQuestionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionID", id)
			);
		}
		
		public override SurveyQuestion Read(int id)
		{
			string query = @"
SELECT 	SurveyQuestionID, 
	SurveyID, 
	QuestionID, 
	OptionsPlacement, 
	Variablename, 
	SortOrder, 
	NoCount, 
	RestartCount, 
	ExtendedFirst, 
	NoBreak, 
	BreakAfterQuestion, 
	PageBreakBeforeQuestion, 
	FontSize
FROM SurveyQuestion
WHERE SurveyQuestionID = @SurveyQuestionID";
			SurveyQuestion surveyQuestion = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SurveyQuestionID", id))) {
				if (rs.Read()) {
					surveyQuestion = new SurveyQuestion {
						SurveyQuestionID = GetInt32(rs, 0),
						SurveyID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionsPlacement = GetInt32(rs, 3),
						Variablename = GetString(rs, 4),
						SortOrder = GetInt32(rs, 5),
						NoCount = GetInt32(rs, 6),
						RestartCount = GetInt32(rs, 7),
						ExtendedFirst = GetInt32(rs, 8),
						NoBreak = GetInt32(rs, 9),
						BreakAfterQuestion = GetInt32(rs, 10),
						PageBreakBeforeQuestion = GetInt32(rs, 11),
						FontSize = GetInt32(rs, 12)
					};
				}
			}
			return surveyQuestion;
		}
		
		public override IList<SurveyQuestion> FindAll()
		{
			string query = @"
SELECT 	SurveyQuestionID, 
	SurveyID, 
	QuestionID, 
	OptionsPlacement, 
	Variablename, 
	SortOrder, 
	NoCount, 
	RestartCount, 
	ExtendedFirst, 
	NoBreak, 
	BreakAfterQuestion, 
	PageBreakBeforeQuestion, 
	FontSize
FROM SurveyQuestion";
			var surveyQuestions = new List<SurveyQuestion>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					surveyQuestions.Add(new SurveyQuestion {
						SurveyQuestionID = GetInt32(rs, 0),
						SurveyID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionsPlacement = GetInt32(rs, 3),
						Variablename = GetString(rs, 4),
						SortOrder = GetInt32(rs, 5),
						NoCount = GetInt32(rs, 6),
						RestartCount = GetInt32(rs, 7),
						ExtendedFirst = GetInt32(rs, 8),
						NoBreak = GetInt32(rs, 9),
						BreakAfterQuestion = GetInt32(rs, 10),
						PageBreakBeforeQuestion = GetInt32(rs, 11),
						FontSize = GetInt32(rs, 12)
					});
				}
			}
			return surveyQuestions;
		}
		
		public IList<SurveyQuestion> FindBySurvey(int surveyID)
		{
			string query = @"
SELECT 	SurveyQuestionID, 
	SurveyID, 
	QuestionID, 
	OptionsPlacement, 
	Variablename, 
	SortOrder, 
	NoCount, 
	RestartCount, 
	ExtendedFirst, 
	NoBreak, 
	BreakAfterQuestion, 
	PageBreakBeforeQuestion, 
	FontSize
FROM SurveyQuestion
WHERE SurveyID = @SurveyID";
			var surveyQuestions = new List<SurveyQuestion>();
			using (var rs = ExecuteReader(query, new SqlParameter("@SurveyID", surveyID))) {
				while (rs.Read()) {
					surveyQuestions.Add(new SurveyQuestion {
						SurveyQuestionID = GetInt32(rs, 0),
						SurveyID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionsPlacement = GetInt32(rs, 3),
						Variablename = GetString(rs, 4),
						SortOrder = GetInt32(rs, 5),
						NoCount = GetInt32(rs, 6),
						RestartCount = GetInt32(rs, 7),
						ExtendedFirst = GetInt32(rs, 8),
						NoBreak = GetInt32(rs, 9),
						BreakAfterQuestion = GetInt32(rs, 10),
						PageBreakBeforeQuestion = GetInt32(rs, 11),
						FontSize = GetInt32(rs, 12)
					});
				}
			}
			return surveyQuestions;
		}
	}
}
