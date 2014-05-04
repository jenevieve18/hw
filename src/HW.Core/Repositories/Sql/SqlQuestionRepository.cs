using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlQuestionRepository : BaseSqlRepository<Question>//, IQuestionRepository
	{
		public IList<BackgroundQuestion> FindBackgroundQuestions(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT BQ.Internal,
	BQ.BQID,
	BQ.Type
FROM SponsorBQ sbq
INNER JOIN BQ ON sbq.BQID = BQ.BQID
WHERE sbq.SponsorID = {0}
AND sbq.Hidden = 1
ORDER BY sbq.SortOrder",
				sponsorID
			);
			var questions = new List<BackgroundQuestion>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var q = new BackgroundQuestion {
						Internal = rs.GetString(0),
						Id = rs.GetInt32(1),
						Type = rs.GetInt32(2)
					};
					questions.Add(q);
				}
			}
			return questions;
		}
		
		public IList<BackgroundQuestion> FindLikeBackgroundQuestions(string bqID)
		{
			string query = string.Format(
				@"
SELECT BQ.BQID, BQ.Internal FROM BQ WHERE BQ.BQID IN ({0})",
				bqID
			);
			var questions = new List<BackgroundQuestion>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var q = new BackgroundQuestion {
						Id = rs.GetInt32(0),
						Internal = rs.GetString(1)
					};
					questions.Add(q);
				}
			}
			return questions;
		}
		
		public IList<BackgroundQuestion> FindBackgroundQuestionsWithAnswers(string query, int count)
		{
			var questions = new List<BackgroundQuestion>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var bq = new BackgroundQuestion { Id = rs.GetInt32(2 + 0 * 3) };
					var answers = new List<BackgroundAnswer>();
					for (int i = 0; i < count; i++) {
						var a = new BackgroundAnswer {
							Id = rs.GetInt32(0 + i * 3),
							Internal = rs.GetString(1 + i * 3)
						};
						answers.Add(a);
					}
					bq.Answers = answers;
					questions.Add(bq);
				}
			}
			return questions;
		}
		
		public IList<BackgroundQuestion> FindAllBackgroundQuestions()
		{
			string query = string.Format(
        		@"
SELECT BQID, 
	Internal, 
	Type, 
	Comparison, 
	Variable 
FROM BQ ORDER BY Internal"
        	);
            var questions = new List<BackgroundQuestion>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var q = new BackgroundQuestion {
						Id = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						Type = GetInt32(rs, 2),
						Comparison = GetInt32(rs, 3),
						Variable = GetString(rs, 4)
					};
            		questions.Add(q);
				}
			}
			return questions;
		}
		
		public BackgroundQuestion ReadBackgroundQuestion(int bqid)
		{
			throw new NotImplementedException();
		}
		
		public void SaveOrUpdateBackgroundQuestion(BackgroundQuestion q)
		{
			throw new NotImplementedException();
		}
	}
}
