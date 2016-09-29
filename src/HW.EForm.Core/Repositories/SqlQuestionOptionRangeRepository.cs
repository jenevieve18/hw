using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlQuestionOptionRangeRepository : BaseSqlRepository<QuestionOptionRange>
	{
		public SqlQuestionOptionRangeRepository()
		{
		}
		
		public override void Save(QuestionOptionRange questionOptionRange)
		{
			string query = @"
INSERT INTO QuestionOptionRange(
	QuestionOptionRangeID, 
	QuestionOptionID, 
	StartDT, 
	EndDT, 
	LowVal, 
	HighVal
)
VALUES(
	@QuestionOptionRangeID, 
	@QuestionOptionID, 
	@StartDT, 
	@EndDT, 
	@LowVal, 
	@HighVal
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionOptionRangeID", questionOptionRange.QuestionOptionRangeID),
				new SqlParameter("@QuestionOptionID", questionOptionRange.QuestionOptionID),
				new SqlParameter("@StartDT", questionOptionRange.StartDT),
				new SqlParameter("@EndDT", questionOptionRange.EndDT),
				new SqlParameter("@LowVal", questionOptionRange.LowVal),
				new SqlParameter("@HighVal", questionOptionRange.HighVal)
			);
		}
		
		public override void Update(QuestionOptionRange questionOptionRange, int id)
		{
			string query = @"
UPDATE QuestionOptionRange SET
	QuestionOptionRangeID = @QuestionOptionRangeID,
	QuestionOptionID = @QuestionOptionID,
	StartDT = @StartDT,
	EndDT = @EndDT,
	LowVal = @LowVal,
	HighVal = @HighVal
WHERE QuestionOptionRangeID = @QuestionOptionRangeID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionOptionRangeID", questionOptionRange.QuestionOptionRangeID),
				new SqlParameter("@QuestionOptionID", questionOptionRange.QuestionOptionID),
				new SqlParameter("@StartDT", questionOptionRange.StartDT),
				new SqlParameter("@EndDT", questionOptionRange.EndDT),
				new SqlParameter("@LowVal", questionOptionRange.LowVal),
				new SqlParameter("@HighVal", questionOptionRange.HighVal)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM QuestionOptionRange
WHERE QuestionOptionRangeID = @QuestionOptionRangeID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionOptionRangeID", id)
			);
		}
		
		public override QuestionOptionRange Read(int id)
		{
			string query = @"
SELECT 	QuestionOptionRangeID, 
	QuestionOptionID, 
	StartDT, 
	EndDT, 
	LowVal, 
	HighVal
FROM QuestionOptionRange
WHERE QuestionOptionRangeID = @QuestionOptionRangeID";
			QuestionOptionRange questionOptionRange = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@QuestionOptionRangeID", id))) {
				if (rs.Read()) {
					questionOptionRange = new QuestionOptionRange {
						QuestionOptionRangeID = GetInt32(rs, 0),
						QuestionOptionID = GetInt32(rs, 1),
						StartDT = GetString(rs, 2),
						EndDT = GetString(rs, 3),
						LowVal = GetDecimal(rs, 4),
						HighVal = GetDecimal(rs, 5)
					};
				}
			}
			return questionOptionRange;
		}
		
		public override IList<QuestionOptionRange> FindAll()
		{
			string query = @"
SELECT 	QuestionOptionRangeID, 
	QuestionOptionID, 
	StartDT, 
	EndDT, 
	LowVal, 
	HighVal
FROM QuestionOptionRange";
			var questionOptionRanges = new List<QuestionOptionRange>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					questionOptionRanges.Add(new QuestionOptionRange {
						QuestionOptionRangeID = GetInt32(rs, 0),
						QuestionOptionID = GetInt32(rs, 1),
						StartDT = GetString(rs, 2),
						EndDT = GetString(rs, 3),
						LowVal = GetDecimal(rs, 4),
						HighVal = GetDecimal(rs, 5)
					});
				}
			}
			return questionOptionRanges;
		}
	}
}
