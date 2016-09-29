using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlTempReportComponentAnswerRepository : BaseSqlRepository<TempReportComponentAnswer>
	{
		public SqlTempReportComponentAnswerRepository()
		{
		}
		
		public override void Save(TempReportComponentAnswer tempReportComponentAnswer)
		{
			string query = @"
INSERT INTO TempReportComponentAnswer(
	TempReportComponentAnswerID, 
	TempReportComponentID, 
	AnswerID
)
VALUES(
	@TempReportComponentAnswerID, 
	@TempReportComponentID, 
	@AnswerID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@TempReportComponentAnswerID", tempReportComponentAnswer.TempReportComponentAnswerID),
				new SqlParameter("@TempReportComponentID", tempReportComponentAnswer.TempReportComponentID),
				new SqlParameter("@AnswerID", tempReportComponentAnswer.AnswerID)
			);
		}
		
		public override void Update(TempReportComponentAnswer tempReportComponentAnswer, int id)
		{
			string query = @"
UPDATE TempReportComponentAnswer SET
	TempReportComponentAnswerID = @TempReportComponentAnswerID,
	TempReportComponentID = @TempReportComponentID,
	AnswerID = @AnswerID
WHERE TempReportComponentAnswerID = @TempReportComponentAnswerID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@TempReportComponentAnswerID", tempReportComponentAnswer.TempReportComponentAnswerID),
				new SqlParameter("@TempReportComponentID", tempReportComponentAnswer.TempReportComponentID),
				new SqlParameter("@AnswerID", tempReportComponentAnswer.AnswerID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM TempReportComponentAnswer
WHERE TempReportComponentAnswerID = @TempReportComponentAnswerID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@TempReportComponentAnswerID", id)
			);
		}
		
		public override TempReportComponentAnswer Read(int id)
		{
			string query = @"
SELECT 	TempReportComponentAnswerID, 
	TempReportComponentID, 
	AnswerID
FROM TempReportComponentAnswer
WHERE TempReportComponentAnswerID = @TempReportComponentAnswerID";
			TempReportComponentAnswer tempReportComponentAnswer = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@TempReportComponentAnswerID", id))) {
				if (rs.Read()) {
					tempReportComponentAnswer = new TempReportComponentAnswer {
						TempReportComponentAnswerID = GetInt32(rs, 0),
						TempReportComponentID = GetInt32(rs, 1),
						AnswerID = GetInt32(rs, 2)
					};
				}
			}
			return tempReportComponentAnswer;
		}
		
		public override IList<TempReportComponentAnswer> FindAll()
		{
			string query = @"
SELECT 	TempReportComponentAnswerID, 
	TempReportComponentID, 
	AnswerID
FROM TempReportComponentAnswer";
			var tempReportComponentAnswers = new List<TempReportComponentAnswer>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					tempReportComponentAnswers.Add(new TempReportComponentAnswer {
						TempReportComponentAnswerID = GetInt32(rs, 0),
						TempReportComponentID = GetInt32(rs, 1),
						AnswerID = GetInt32(rs, 2)
					});
				}
			}
			return tempReportComponentAnswers;
		}
	}
}
