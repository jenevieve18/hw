using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlFeedbackQuestionRepository : BaseSqlRepository<FeedbackQuestion>
	{
		public SqlFeedbackQuestionRepository()
		{
		}
		
		public override void Save(FeedbackQuestion feedbackQuestion)
		{
			string query = @"
INSERT INTO FeedbackQuestion(
	FeedbackQuestionID, 
	FeedbackID, 
	QuestionID, 
	Additional, 
	OptionID, 
	PartOfChart, 
	FeedbackTemplatePageID, 
	IdxID, 
	HardcodedIdx
)
VALUES(
	@FeedbackQuestionID, 
	@FeedbackID, 
	@QuestionID, 
	@Additional, 
	@OptionID, 
	@PartOfChart, 
	@FeedbackTemplatePageID, 
	@IdxID, 
	@HardcodedIdx
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackQuestionID", feedbackQuestion.FeedbackQuestionID),
				new SqlParameter("@FeedbackID", feedbackQuestion.FeedbackID),
				new SqlParameter("@QuestionID", feedbackQuestion.QuestionID),
				new SqlParameter("@Additional", feedbackQuestion.Additional),
				new SqlParameter("@OptionID", feedbackQuestion.OptionID),
				new SqlParameter("@PartOfChart", feedbackQuestion.PartOfChart),
				new SqlParameter("@FeedbackTemplatePageID", feedbackQuestion.FeedbackTemplatePageID),
				new SqlParameter("@IdxID", feedbackQuestion.IdxID),
				new SqlParameter("@HardcodedIdx", feedbackQuestion.HardcodedIdx)
			);
		}
		
		public override void Update(FeedbackQuestion feedbackQuestion, int id)
		{
			string query = @"
UPDATE FeedbackQuestion SET
	FeedbackQuestionID = @FeedbackQuestionID,
	FeedbackID = @FeedbackID,
	QuestionID = @QuestionID,
	Additional = @Additional,
	OptionID = @OptionID,
	PartOfChart = @PartOfChart,
	FeedbackTemplatePageID = @FeedbackTemplatePageID,
	IdxID = @IdxID,
	HardcodedIdx = @HardcodedIdx
WHERE FeedbackQuestionID = @FeedbackQuestionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackQuestionID", feedbackQuestion.FeedbackQuestionID),
				new SqlParameter("@FeedbackID", feedbackQuestion.FeedbackID),
				new SqlParameter("@QuestionID", feedbackQuestion.QuestionID),
				new SqlParameter("@Additional", feedbackQuestion.Additional),
				new SqlParameter("@OptionID", feedbackQuestion.OptionID),
				new SqlParameter("@PartOfChart", feedbackQuestion.PartOfChart),
				new SqlParameter("@FeedbackTemplatePageID", feedbackQuestion.FeedbackTemplatePageID),
				new SqlParameter("@IdxID", feedbackQuestion.IdxID),
				new SqlParameter("@HardcodedIdx", feedbackQuestion.HardcodedIdx)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM FeedbackQuestion
WHERE FeedbackQuestionID = @FeedbackQuestionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackQuestionID", id)
			);
		}
		
		public override FeedbackQuestion Read(int id)
		{
			string query = @"
SELECT 	FeedbackQuestionID, 
	FeedbackID, 
	QuestionID, 
	Additional, 
	OptionID, 
	PartOfChart, 
	FeedbackTemplatePageID, 
	IdxID, 
	HardcodedIdx
FROM FeedbackQuestion
WHERE FeedbackQuestionID = @FeedbackQuestionID";
			FeedbackQuestion feedbackQuestion = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@FeedbackQuestionID", id))) {
				if (rs.Read()) {
					feedbackQuestion = new FeedbackQuestion {
						FeedbackQuestionID = GetInt32(rs, 0),
						FeedbackID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2),
						Additional = GetInt32(rs, 3),
						OptionID = GetInt32(rs, 4),
						PartOfChart = GetInt32(rs, 5),
						FeedbackTemplatePageID = GetInt32(rs, 6),
						IdxID = GetInt32(rs, 7),
						HardcodedIdx = GetInt32(rs, 8)
					};
				}
			}
			return feedbackQuestion;
		}
		
		public override IList<FeedbackQuestion> FindAll()
		{
			string query = @"
SELECT 	FeedbackQuestionID, 
	FeedbackID, 
	QuestionID, 
	Additional, 
	OptionID, 
	PartOfChart, 
	FeedbackTemplatePageID, 
	IdxID, 
	HardcodedIdx
FROM FeedbackQuestion";
			var feedbackQuestions = new List<FeedbackQuestion>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					feedbackQuestions.Add(new FeedbackQuestion {
						FeedbackQuestionID = GetInt32(rs, 0),
						FeedbackID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2),
						Additional = GetInt32(rs, 3),
						OptionID = GetInt32(rs, 4),
						PartOfChart = GetInt32(rs, 5),
						FeedbackTemplatePageID = GetInt32(rs, 6),
						IdxID = GetInt32(rs, 7),
						HardcodedIdx = GetInt32(rs, 8)
					});
				}
			}
			return feedbackQuestions;
		}
	}
}
