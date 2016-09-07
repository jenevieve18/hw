using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlUserProjectRoundUserAnswerRepository : BaseSqlRepository<UserProjectRoundUserAnswer>
	{
		public SqlUserProjectRoundUserAnswerRepository()
		{
		}
		
		public override void Save(UserProjectRoundUserAnswer userProjectRoundUserAnswer)
		{
			string query = @"
INSERT INTO UserProjectRoundUserAnswer(
	UserProjectRoundUserAnswerID, 
	ProjectRoundUserID, 
	AnswerKey, 
	DT, 
	UserProfileID, 
	AnswerID
)
VALUES(
	@UserProjectRoundUserAnswerID, 
	@ProjectRoundUserID, 
	@AnswerKey, 
	@DT, 
	@UserProfileID, 
	@AnswerID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserProjectRoundUserAnswerID", userProjectRoundUserAnswer.UserProjectRoundUserAnswerID),
				new SqlParameter("@ProjectRoundUserID", userProjectRoundUserAnswer.ProjectRoundUserID),
				new SqlParameter("@AnswerKey", userProjectRoundUserAnswer.AnswerKey),
				new SqlParameter("@DT", userProjectRoundUserAnswer.DT),
				new SqlParameter("@UserProfileID", userProjectRoundUserAnswer.UserProfileID),
				new SqlParameter("@AnswerID", userProjectRoundUserAnswer.AnswerID)
			);
		}
		
		public override void Update(UserProjectRoundUserAnswer userProjectRoundUserAnswer, int id)
		{
			string query = @"
UPDATE UserProjectRoundUserAnswer SET
	UserProjectRoundUserAnswerID = @UserProjectRoundUserAnswerID,
	ProjectRoundUserID = @ProjectRoundUserID,
	AnswerKey = @AnswerKey,
	DT = @DT,
	UserProfileID = @UserProfileID,
	AnswerID = @AnswerID
WHERE UserProjectRoundUserAnswerID = @UserProjectRoundUserAnswerID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserProjectRoundUserAnswerID", userProjectRoundUserAnswer.UserProjectRoundUserAnswerID),
				new SqlParameter("@ProjectRoundUserID", userProjectRoundUserAnswer.ProjectRoundUserID),
				new SqlParameter("@AnswerKey", userProjectRoundUserAnswer.AnswerKey),
				new SqlParameter("@DT", userProjectRoundUserAnswer.DT),
				new SqlParameter("@UserProfileID", userProjectRoundUserAnswer.UserProfileID),
				new SqlParameter("@AnswerID", userProjectRoundUserAnswer.AnswerID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UserProjectRoundUserAnswer
WHERE UserProjectRoundUserAnswerID = @UserProjectRoundUserAnswerID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserProjectRoundUserAnswerID", id)
			);
		}
		
		public override UserProjectRoundUserAnswer Read(int id)
		{
			string query = @"
SELECT 	UserProjectRoundUserAnswerID, 
	ProjectRoundUserID, 
	AnswerKey, 
	DT, 
	UserProfileID, 
	AnswerID
FROM UserProjectRoundUserAnswer
WHERE UserProjectRoundUserAnswerID = @UserProjectRoundUserAnswerID";
			UserProjectRoundUserAnswer userProjectRoundUserAnswer = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserProjectRoundUserAnswerID", id))) {
				if (rs.Read()) {
					userProjectRoundUserAnswer = new UserProjectRoundUserAnswer {
						UserProjectRoundUserAnswerID = GetInt32(rs, 0),
						ProjectRoundUserID = GetInt32(rs, 1),
						AnswerKey = GetString(rs, 2),
						DT = GetDateTime(rs, 3),
						UserProfileID = GetInt32(rs, 4),
						AnswerID = GetInt32(rs, 5)
					};
				}
			}
			return userProjectRoundUserAnswer;
		}
		
		public override IList<UserProjectRoundUserAnswer> FindAll()
		{
			string query = @"
SELECT 	UserProjectRoundUserAnswerID, 
	ProjectRoundUserID, 
	AnswerKey, 
	DT, 
	UserProfileID, 
	AnswerID
FROM UserProjectRoundUserAnswer";
			var userProjectRoundUserAnswers = new List<UserProjectRoundUserAnswer>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					userProjectRoundUserAnswers.Add(new UserProjectRoundUserAnswer {
						UserProjectRoundUserAnswerID = GetInt32(rs, 0),
						ProjectRoundUserID = GetInt32(rs, 1),
						AnswerKey = GetString(rs, 2),
						DT = GetDateTime(rs, 3),
						UserProfileID = GetInt32(rs, 4),
						AnswerID = GetInt32(rs, 5)
					});
				}
			}
			return userProjectRoundUserAnswers;
		}
	}
}
