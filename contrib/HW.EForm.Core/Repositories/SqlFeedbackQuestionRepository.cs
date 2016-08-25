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
	QuestionID
)
VALUES(
	@FeedbackQuestionID, 
	@FeedbackID, 
	@QuestionID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackQuestionID", feedbackQuestion.FeedbackQuestionID),
				new SqlParameter("@FeedbackID", feedbackQuestion.FeedbackID),
				new SqlParameter("@QuestionID", feedbackQuestion.QuestionID)
			);
		}
		
		public override void Update(FeedbackQuestion feedbackQuestion, int id)
		{
			string query = @"
UPDATE FeedbackQuestion SET
	FeedbackQuestionID = @FeedbackQuestionID,
	FeedbackID = @FeedbackID,
	QuestionID = @QuestionID
WHERE FeedbackQuestionID = @FeedbackQuestionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackQuestionID", feedbackQuestion.FeedbackQuestionID),
				new SqlParameter("@FeedbackID", feedbackQuestion.FeedbackID),
				new SqlParameter("@QuestionID", feedbackQuestion.QuestionID)
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
	QuestionID
FROM FeedbackQuestion
WHERE FeedbackQuestionID = @FeedbackQuestionID";
			FeedbackQuestion feedbackQuestion = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@FeedbackQuestionID", id))) {
				if (rs.Read()) {
					feedbackQuestion = new FeedbackQuestion {
						FeedbackQuestionID = GetInt32(rs, 0),
						FeedbackID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2)
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
	QuestionID
FROM FeedbackQuestion";
			var feedbackQuestions = new List<FeedbackQuestion>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					feedbackQuestions.Add(new FeedbackQuestion {
						FeedbackQuestionID = GetInt32(rs, 0),
						FeedbackID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2)
					});
				}
			}
			return feedbackQuestions;
		}
		
		public IList<FeedbackQuestion> FindByFeedback(int feedbackID)
		{
			string query = @"
SELECT 	FeedbackQuestionID, 
	FeedbackID, 
	QuestionID
FROM FeedbackQuestion
WHERE FeedbackID = @FeedbackID";
			var feedbackQuestions = new List<FeedbackQuestion>();
			using (var rs = ExecuteReader(query, new SqlParameter("@FeedbackID", feedbackID))) {
				while (rs.Read()) {
					feedbackQuestions.Add(new FeedbackQuestion {
						FeedbackQuestionID = GetInt32(rs, 0),
						FeedbackID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2)
					});
				}
			}
			return feedbackQuestions;
		}
	}
}
