using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlFeedbackRepository : BaseSqlRepository<Feedback>
	{
		public SqlFeedbackRepository()
		{
		}
		
		public override void Save(Feedback feedback)
		{
			string query = @"
INSERT INTO Feedback(
	FeedbackID, 
	Feedback
)
VALUES(
	@FeedbackID, 
	@Feedback
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackID", feedback.FeedbackID),
				new SqlParameter("@Feedback", feedback.FeedbackText)
			);
		}
		
		public override void Update(Feedback feedback, int id)
		{
			string query = @"
UPDATE Feedback SET
	FeedbackID = @FeedbackID,
	Feedback = @Feedback
WHERE FeedbackID = @FeedbackID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackID", feedback.FeedbackID),
				new SqlParameter("@Feedback", feedback.FeedbackText)
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
	Feedback
FROM Feedback
WHERE FeedbackID = @FeedbackID";
			Feedback feedback = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@FeedbackID", id))) {
				if (rs.Read()) {
					feedback = new Feedback {
						FeedbackID = GetInt32(rs, 0),
						FeedbackText = GetString(rs, 1)
					};
				}
			}
			return feedback;
		}
		
		public override IList<Feedback> FindAll()
		{
			string query = @"
SELECT 	FeedbackID, 
	Feedback
FROM Feedback";
			var feedbacks = new List<Feedback>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					feedbacks.Add(new Feedback {
						FeedbackID = GetInt32(rs, 0),
						FeedbackText = GetString(rs, 1)
					});
				}
			}
			return feedbacks;
		}
	}
}
