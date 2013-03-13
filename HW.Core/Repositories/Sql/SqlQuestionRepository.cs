//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlQuestionRepository : BaseSqlRepository<Question>, IQuestionRepository
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
	}
}
