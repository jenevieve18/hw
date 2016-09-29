using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlFeedbackRunRowRepository : BaseSqlRepository<FeedbackRunRow>
	{
		public SqlFeedbackRunRowRepository()
		{
		}
		
		public override void Save(FeedbackRunRow feedbackRunRow)
		{
			string query = @"
INSERT INTO FeedbackRunRow(
	FeedbackRunRowID, 
	FeedbackRunID, 
	URL, 
	Area, 
	Header, 
	Description
)
VALUES(
	@FeedbackRunRowID, 
	@FeedbackRunID, 
	@URL, 
	@Area, 
	@Header, 
	@Description
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackRunRowID", feedbackRunRow.FeedbackRunRowID),
				new SqlParameter("@FeedbackRunID", feedbackRunRow.FeedbackRunID),
				new SqlParameter("@URL", feedbackRunRow.URL),
				new SqlParameter("@Area", feedbackRunRow.Area),
				new SqlParameter("@Header", feedbackRunRow.Header),
				new SqlParameter("@Description", feedbackRunRow.Description)
			);
		}
		
		public override void Update(FeedbackRunRow feedbackRunRow, int id)
		{
			string query = @"
UPDATE FeedbackRunRow SET
	FeedbackRunRowID = @FeedbackRunRowID,
	FeedbackRunID = @FeedbackRunID,
	URL = @URL,
	Area = @Area,
	Header = @Header,
	Description = @Description
WHERE FeedbackRunRowID = @FeedbackRunRowID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackRunRowID", feedbackRunRow.FeedbackRunRowID),
				new SqlParameter("@FeedbackRunID", feedbackRunRow.FeedbackRunID),
				new SqlParameter("@URL", feedbackRunRow.URL),
				new SqlParameter("@Area", feedbackRunRow.Area),
				new SqlParameter("@Header", feedbackRunRow.Header),
				new SqlParameter("@Description", feedbackRunRow.Description)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM FeedbackRunRow
WHERE FeedbackRunRowID = @FeedbackRunRowID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackRunRowID", id)
			);
		}
		
		public override FeedbackRunRow Read(int id)
		{
			string query = @"
SELECT 	FeedbackRunRowID, 
	FeedbackRunID, 
	URL, 
	Area, 
	Header, 
	Description
FROM FeedbackRunRow
WHERE FeedbackRunRowID = @FeedbackRunRowID";
			FeedbackRunRow feedbackRunRow = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@FeedbackRunRowID", id))) {
				if (rs.Read()) {
					feedbackRunRow = new FeedbackRunRow {
						FeedbackRunRowID = GetInt32(rs, 0),
						FeedbackRunID = GetInt32(rs, 1),
						URL = GetString(rs, 2),
						Area = GetString(rs, 3),
						Header = GetString(rs, 4),
						Description = GetString(rs, 5)
					};
				}
			}
			return feedbackRunRow;
		}
		
		public override IList<FeedbackRunRow> FindAll()
		{
			string query = @"
SELECT 	FeedbackRunRowID, 
	FeedbackRunID, 
	URL, 
	Area, 
	Header, 
	Description
FROM FeedbackRunRow";
			var feedbackRunRows = new List<FeedbackRunRow>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					feedbackRunRows.Add(new FeedbackRunRow {
						FeedbackRunRowID = GetInt32(rs, 0),
						FeedbackRunID = GetInt32(rs, 1),
						URL = GetString(rs, 2),
						Area = GetString(rs, 3),
						Header = GetString(rs, 4),
						Description = GetString(rs, 5)
					});
				}
			}
			return feedbackRunRows;
		}
	}
}
