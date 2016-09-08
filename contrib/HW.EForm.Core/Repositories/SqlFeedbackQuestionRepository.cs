using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HW.EForm.Core.Repositories
{
	public interface IFeedbackQuestionRepository : IBaseRepository<FeedbackQuestion>
	{
		IList<FeedbackQuestion> FindByFeedback(int feedbackID);
		IList<FeedbackQuestion> FindByQuestions(int feedbackID, int[] questionIDs);
	}
	
	public class SqlFeedbackQuestionRepository : BaseSqlRepository<FeedbackQuestion>, IFeedbackQuestionRepository
	{
		public SqlFeedbackQuestionRepository()
		{
		}
		
		public override void Save(FeedbackQuestion feedbackQuestion)
		{
			string query = @"
INSERT INTO FeedbackQuestion(
	FeedbackID,
	QuestionID,
	Additional,
	OptionID,
	PartOfChart
)
VALUES(
	@FeedbackID,
	@QuestionID,
	@Additional,
	@OptionID,
	@PartOfChart
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackID", feedbackQuestion.FeedbackID),
				new SqlParameter("@QuestionID", feedbackQuestion.QuestionID),
				new SqlParameter("@Additional", feedbackQuestion.Additional),
				new SqlParameter("@OptionID", feedbackQuestion.OptionID),
				new SqlParameter("@PartOfChart", feedbackQuestion.PartOfChart)
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
	PartOfChart = @PartOfChart
WHERE FeedbackQuestionID = @FeedbackQuestionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackQuestionID", id),
				new SqlParameter("@FeedbackID", feedbackQuestion.FeedbackID),
				new SqlParameter("@QuestionID", feedbackQuestion.QuestionID),
				new SqlParameter("@Additional", feedbackQuestion.Additional),
				new SqlParameter("@OptionID", feedbackQuestion.OptionID),
				new SqlParameter("@PartOfChart", feedbackQuestion.PartOfChart)
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
	PartOfChart
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
						PartOfChart = GetInt32(rs, 5)
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
	PartOfChart
FROM FeedbackQuestion";
			var feedbackQuestions = new List<FeedbackQuestion>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					feedbackQuestions.Add(
						new FeedbackQuestion {
							FeedbackQuestionID = GetInt32(rs, 0),
							FeedbackID = GetInt32(rs, 1),
							QuestionID = GetInt32(rs, 2),
							Additional = GetInt32(rs, 3),
							OptionID = GetInt32(rs, 4),
							PartOfChart = GetInt32(rs, 5)
						}
					);
				}
			}
			return feedbackQuestions;
		}
		
		public IList<FeedbackQuestion> FindByQuestions(int feedbackID, int[] questionIDs)
		{
			string questions = "";
			var parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@FeedbackID", feedbackID));
			if (questionIDs.Length > 0) {
				questions = "AND QuestionID IN (";
				int i = 1;
				foreach (var questionID in questionIDs) {
					questions += "@QuestionID" + questionID;
					questions += i++ < questionIDs.Length ? ", " : "";
					parameters.Add(new SqlParameter("@QuestionID" + questionID, questionID));
				}
				questions += ")";
			}
			string query = string.Format(@"
SELECT 	FeedbackQuestionID,
	FeedbackID,
	QuestionID,
	Additional,
	OptionID,
	PartOfChart
FROM FeedbackQuestion
WHERE FeedbackID = @FeedbackID
{0}", questions);
			var feedbackQuestions = new List<FeedbackQuestion>();
			using (var rs = ExecuteReader(query, parameters.ToArray())) {
				while (rs.Read()) {
					feedbackQuestions.Add(
						new FeedbackQuestion {
							FeedbackQuestionID = GetInt32(rs, 0),
							FeedbackID = GetInt32(rs, 1),
							QuestionID = GetInt32(rs, 2),
							Additional = GetInt32(rs, 3),
							OptionID = GetInt32(rs, 4),
							PartOfChart = GetInt32(rs, 5)
						}
					);
				}
			}
			return feedbackQuestions;
		}
		
		public IList<FeedbackQuestion> FindByFeedback(int feedbackID)
		{
			string query = @"
SELECT 	FeedbackQuestionID,
	FeedbackID,
	QuestionID,
	Additional,
	OptionID,
	PartOfChart
FROM FeedbackQuestion
WHERE FeedbackID = @FeedbackID";
			var feedbackQuestions = new List<FeedbackQuestion>();
			using (var rs = ExecuteReader(query, new SqlParameter("@FeedbackID", feedbackID))) {
				while (rs.Read()) {
					feedbackQuestions.Add(
						new FeedbackQuestion {
							FeedbackQuestionID = GetInt32(rs, 0),
							FeedbackID = GetInt32(rs, 1),
							QuestionID = GetInt32(rs, 2),
							Additional = GetInt32(rs, 3),
							OptionID = GetInt32(rs, 4),
							PartOfChart = GetInt32(rs, 5)
						}
					);
				}
			}
			return feedbackQuestions;
		}
	}
}
