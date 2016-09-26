using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.EForm.Core.Models;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlAnswerRepository : BaseSqlRepository<Answer>, IAnswerRepository
	{
		public SqlAnswerRepository()
		{
		}
		
		public override void Save(Answer answer)
		{
			string query = @"
INSERT INTO Answer(
	AnswerID, 
	ProjectRoundID, 
	ProjectRoundUnitID, 
	ProjectRoundUserID, 
	StartDT, 
	EndDT, 
	AnswerKey, 
	ExtendedFirst, 
	CurrentPage, 
	FeedbackAlert
)
VALUES(
	@AnswerID, 
	@ProjectRoundID, 
	@ProjectRoundUnitID, 
	@ProjectRoundUserID, 
	@StartDT, 
	@EndDT, 
	@AnswerKey, 
	@ExtendedFirst, 
	@CurrentPage, 
	@FeedbackAlert
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@AnswerID", answer.AnswerID),
				new SqlParameter("@ProjectRoundID", answer.ProjectRoundID),
				new SqlParameter("@ProjectRoundUnitID", answer.ProjectRoundUnitID),
				new SqlParameter("@ProjectRoundUserID", answer.ProjectRoundUserID),
				new SqlParameter("@StartDT", answer.StartDT),
				new SqlParameter("@EndDT", answer.EndDT),
				new SqlParameter("@AnswerKey", answer.AnswerKey),
				new SqlParameter("@ExtendedFirst", answer.ExtendedFirst),
				new SqlParameter("@CurrentPage", answer.CurrentPage),
				new SqlParameter("@FeedbackAlert", answer.FeedbackAlert)
			);
		}
		
		public override void Update(Answer answer, int id)
		{
			string query = @"
UPDATE Answer SET
	AnswerID = @AnswerID,
	ProjectRoundID = @ProjectRoundID,
	ProjectRoundUnitID = @ProjectRoundUnitID,
	ProjectRoundUserID = @ProjectRoundUserID,
	StartDT = @StartDT,
	EndDT = @EndDT,
	AnswerKey = @AnswerKey,
	ExtendedFirst = @ExtendedFirst,
	CurrentPage = @CurrentPage,
	FeedbackAlert = @FeedbackAlert
WHERE AnswerID = @AnswerID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@AnswerID", answer.AnswerID),
				new SqlParameter("@ProjectRoundID", answer.ProjectRoundID),
				new SqlParameter("@ProjectRoundUnitID", answer.ProjectRoundUnitID),
				new SqlParameter("@ProjectRoundUserID", answer.ProjectRoundUserID),
				new SqlParameter("@StartDT", answer.StartDT),
				new SqlParameter("@EndDT", answer.EndDT),
				new SqlParameter("@AnswerKey", answer.AnswerKey),
				new SqlParameter("@ExtendedFirst", answer.ExtendedFirst),
				new SqlParameter("@CurrentPage", answer.CurrentPage),
				new SqlParameter("@FeedbackAlert", answer.FeedbackAlert)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Answer
WHERE AnswerID = @AnswerID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@AnswerID", id)
			);
		}
		
		public override Answer Read(int id)
		{
			string query = @"
SELECT 	AnswerID, 
	ProjectRoundID, 
	ProjectRoundUnitID, 
	ProjectRoundUserID, 
	StartDT, 
	EndDT, 
	AnswerKey, 
	ExtendedFirst, 
	CurrentPage, 
	FeedbackAlert
FROM Answer
WHERE AnswerID = @AnswerID";
			Answer answer = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@AnswerID", id))) {
				if (rs.Read()) {
					answer = new Answer {
						AnswerID = GetInt32(rs, 0),
						ProjectRoundID = GetInt32(rs, 1),
						ProjectRoundUnitID = GetInt32(rs, 2),
						ProjectRoundUserID = GetInt32(rs, 3),
						StartDT = GetDateTime(rs, 4),
						EndDT = GetDateTime(rs, 5),
						AnswerKey = GetGuid(rs, 6),
						ExtendedFirst = GetInt32(rs, 7),
						CurrentPage = GetInt32(rs, 8),
						FeedbackAlert = GetInt32(rs, 9)
					};
				}
			}
			return answer;
		}
		
		public Answer ReadByProjectRound(int projectRoundID, int projectRoundUnitID)
		{
			string query = @"
SELECT 	AnswerID, 
	ProjectRoundID, 
	ProjectRoundUnitID, 
	ProjectRoundUserID, 
	StartDT, 
	EndDT, 
	AnswerKey, 
	ExtendedFirst, 
	CurrentPage, 
	FeedbackAlert
FROM Answer
WHERE ProjectRoundID = @ProjectRoundID
AND ProjectRoundUnitID = @ProjectRoundUnitID";
			Answer answer = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ProjectRoundID", projectRoundID), new SqlParameter("@ProjectRoundUnitID", projectRoundUnitID))) {
				if (rs.Read()) {
					answer = new Answer {
						AnswerID = GetInt32(rs, 0),
						ProjectRoundID = GetInt32(rs, 1),
						ProjectRoundUnitID = GetInt32(rs, 2),
						ProjectRoundUserID = GetInt32(rs, 3),
						StartDT = GetDateTime(rs, 4),
						EndDT = GetDateTime(rs, 5),
						AnswerKey = GetGuid(rs, 6),
						ExtendedFirst = GetInt32(rs, 7),
						CurrentPage = GetInt32(rs, 8),
						FeedbackAlert = GetInt32(rs, 9)
					};
				}
			}
			return answer;
		}
		
		public override IList<Answer> FindAll()
		{
			string query = @"
SELECT 	AnswerID, 
	ProjectRoundID, 
	ProjectRoundUnitID, 
	ProjectRoundUserID, 
	StartDT, 
	EndDT, 
	AnswerKey, 
	ExtendedFirst, 
	CurrentPage, 
	FeedbackAlert
FROM Answer";
			var answers = new List<Answer>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					answers.Add(new Answer {
						AnswerID = GetInt32(rs, 0),
						ProjectRoundID = GetInt32(rs, 1),
						ProjectRoundUnitID = GetInt32(rs, 2),
						ProjectRoundUserID = GetInt32(rs, 3),
						StartDT = GetDateTime(rs, 4),
						EndDT = GetDateTime(rs, 5),
						AnswerKey = GetGuid(rs, 6),
						ExtendedFirst = GetInt32(rs, 7),
						CurrentPage = GetInt32(rs, 8),
						FeedbackAlert = GetInt32(rs, 9)
					});
				}
			}
			return answers;
		}
	}
}
