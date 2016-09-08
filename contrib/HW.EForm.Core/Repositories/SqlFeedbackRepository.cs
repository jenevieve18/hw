using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HW.EForm.Core.Repositories
{
	public interface IFeedbackRepository : IBaseRepository<Feedback>
	{
	}
	
	public class SqlFeedbackRepository : BaseSqlRepository<Feedback>, IFeedbackRepository
	{
		public SqlFeedbackRepository()
		{
		}
		
		public override void Save(Feedback feedback)
		{
			string query = @"
INSERT INTO Feedback(
	Feedback,
	SurveyID,
	Compare,
	FeedbackTemplateID,
	NoHardcodedIdxs
)
VALUES(
	@Feedback,
	@SurveyID,
	@Compare,
	@FeedbackTemplateID,
	@NoHardcodedIdxs
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@Feedback", feedback.FeedbackText),
				new SqlParameter("@SurveyID", feedback.SurveyID),
				new SqlParameter("@Compare", feedback.Compare),
				new SqlParameter("@FeedbackTemplateID", feedback.FeedbackTemplateID),
				new SqlParameter("@NoHardcodedIdxs", feedback.NoHardcodedIdxs)
			);
		}
		
		public override void Update(Feedback feedback, int id)
		{
			string query = @"
UPDATE Feedback SET
	FeedbackID = @FeedbackID,
	Feedback = @Feedback,
	SurveyID = @SurveyID,
	Compare = @Compare,
	FeedbackTemplateID = @FeedbackTemplateID,
	NoHardcodedIdxs = @NoHardcodedIdxs
WHERE FeedbackID = @FeedbackID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackID", feedback.FeedbackID),
				new SqlParameter("@Feedback", feedback.FeedbackText),
				new SqlParameter("@SurveyID", feedback.SurveyID),
				new SqlParameter("@Compare", feedback.Compare),
				new SqlParameter("@FeedbackTemplateID", feedback.FeedbackTemplateID),
				new SqlParameter("@NoHardcodedIdxs", feedback.NoHardcodedIdxs)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Feedback
WHERE FeedbackID = @FeedbackID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackID", id)
			);
		}
		
		public override Feedback Read(int id)
		{
			string query = @"
SELECT 	FeedbackID,
	Feedback,
	SurveyID,
	Compare,
	FeedbackTemplateID,
	NoHardcodedIdxs
FROM Feedback
WHERE FeedbackID = @FeedbackID";
			Feedback feedback = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@FeedbackID", id))) {
				if (rs.Read()) {
					feedback = new Feedback {
						FeedbackID = GetInt32(rs, 0),
						FeedbackText = GetString(rs, 1),
						SurveyID = GetInt32(rs, 2),
						FeedbackTemplateID = GetInt32(rs, 3),
						NoHardcodedIdxs = GetInt32(rs, 4)
					};
				}
			}
			return feedback;
		}
		
		public override IList<Feedback> FindAll()
		{
			string query = @"
SELECT 	FeedbackID,
	Feedback,
	SurveyID,
	Compare,
	FeedbackTemplateID,
	NoHardcodedIdxs
FROM Feedback";
			var feedbacks = new List<Feedback>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					feedbacks.Add(
						new Feedback {
							FeedbackID = GetInt32(rs, 0),
							FeedbackText = GetString(rs, 1),
							SurveyID = GetInt32(rs, 2),
							FeedbackTemplateID = GetInt32(rs, 3),
							NoHardcodedIdxs = GetInt32(rs, 4)
						}
					);
				}
			}
			return feedbacks;
		}
	}
}
