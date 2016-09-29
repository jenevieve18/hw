using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlFeedbackRunRepository : BaseSqlRepository<FeedbackRun>
	{
		public SqlFeedbackRunRepository()
		{
		}
		
		public override void Save(FeedbackRun feedbackRun)
		{
			string query = @"
INSERT INTO FeedbackRun(
	FeedbackRunID, 
	FeedbackRunKey, 
	Total, 
	Answer
)
VALUES(
	@FeedbackRunID, 
	@FeedbackRunKey, 
	@Total, 
	@Answer
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackRunID", feedbackRun.FeedbackRunID),
				new SqlParameter("@FeedbackRunKey", feedbackRun.FeedbackRunKey),
				new SqlParameter("@Total", feedbackRun.Total),
				new SqlParameter("@Answer", feedbackRun.Answer)
			);
		}
		
		public override void Update(FeedbackRun feedbackRun, int id)
		{
			string query = @"
UPDATE FeedbackRun SET
	FeedbackRunID = @FeedbackRunID,
	FeedbackRunKey = @FeedbackRunKey,
	Total = @Total,
	Answer = @Answer
WHERE FeedbackRunID = @FeedbackRunID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackRunID", feedbackRun.FeedbackRunID),
				new SqlParameter("@FeedbackRunKey", feedbackRun.FeedbackRunKey),
				new SqlParameter("@Total", feedbackRun.Total),
				new SqlParameter("@Answer", feedbackRun.Answer)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM FeedbackRun
WHERE FeedbackRunID = @FeedbackRunID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackRunID", id)
			);
		}
		
		public override FeedbackRun Read(int id)
		{
			string query = @"
SELECT 	FeedbackRunID, 
	FeedbackRunKey, 
	Total, 
	Answer
FROM FeedbackRun
WHERE FeedbackRunID = @FeedbackRunID";
			FeedbackRun feedbackRun = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@FeedbackRunID", id))) {
				if (rs.Read()) {
					feedbackRun = new FeedbackRun {
						FeedbackRunID = GetInt32(rs, 0),
						FeedbackRunKey = GetGuid(rs, 1),
						Total = GetInt32(rs, 2),
						Answer = GetInt32(rs, 3)
					};
				}
			}
			return feedbackRun;
		}
		
		public override IList<FeedbackRun> FindAll()
		{
			string query = @"
SELECT 	FeedbackRunID, 
	FeedbackRunKey, 
	Total, 
	Answer
FROM FeedbackRun";
			var feedbackRuns = new List<FeedbackRun>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					feedbackRuns.Add(new FeedbackRun {
						FeedbackRunID = GetInt32(rs, 0),
						FeedbackRunKey = GetGuid(rs, 1),
						Total = GetInt32(rs, 2),
						Answer = GetInt32(rs, 3)
					});
				}
			}
			return feedbackRuns;
		}
	}
}
