using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlUserSponsorExtendedSurveyRepository : BaseSqlRepository<UserSponsorExtendedSurvey>
	{
		public SqlUserSponsorExtendedSurveyRepository()
		{
		}
		
		public override void Save(UserSponsorExtendedSurvey userSponsorExtendedSurvey)
		{
			string query = @"
INSERT INTO UserSponsorExtendedSurvey(
	UserSponsorExtendedSurveyID, 
	UserID, 
	SponsorExtendedSurveyID, 
	ProjectRoundUserID, 
	AnswerID, 
	FinishedEmail, 
	ContactRequestDT, 
	ContactRequest
)
VALUES(
	@UserSponsorExtendedSurveyID, 
	@UserID, 
	@SponsorExtendedSurveyID, 
	@ProjectRoundUserID, 
	@AnswerID, 
	@FinishedEmail, 
	@ContactRequestDT, 
	@ContactRequest
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserSponsorExtendedSurveyID", userSponsorExtendedSurvey.UserSponsorExtendedSurveyID),
				new SqlParameter("@UserID", userSponsorExtendedSurvey.UserID),
				new SqlParameter("@SponsorExtendedSurveyID", userSponsorExtendedSurvey.SponsorExtendedSurveyID),
				new SqlParameter("@ProjectRoundUserID", userSponsorExtendedSurvey.ProjectRoundUserID),
				new SqlParameter("@AnswerID", userSponsorExtendedSurvey.AnswerID),
				new SqlParameter("@FinishedEmail", userSponsorExtendedSurvey.FinishedEmail),
				new SqlParameter("@ContactRequestDT", userSponsorExtendedSurvey.ContactRequestDT),
				new SqlParameter("@ContactRequest", userSponsorExtendedSurvey.ContactRequest)
			);
		}
		
		public override void Update(UserSponsorExtendedSurvey userSponsorExtendedSurvey, int id)
		{
			string query = @"
UPDATE UserSponsorExtendedSurvey SET
	UserSponsorExtendedSurveyID = @UserSponsorExtendedSurveyID,
	UserID = @UserID,
	SponsorExtendedSurveyID = @SponsorExtendedSurveyID,
	ProjectRoundUserID = @ProjectRoundUserID,
	AnswerID = @AnswerID,
	FinishedEmail = @FinishedEmail,
	ContactRequestDT = @ContactRequestDT,
	ContactRequest = @ContactRequest
WHERE UserSponsorExtendedSurveyID = @UserSponsorExtendedSurveyID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserSponsorExtendedSurveyID", userSponsorExtendedSurvey.UserSponsorExtendedSurveyID),
				new SqlParameter("@UserID", userSponsorExtendedSurvey.UserID),
				new SqlParameter("@SponsorExtendedSurveyID", userSponsorExtendedSurvey.SponsorExtendedSurveyID),
				new SqlParameter("@ProjectRoundUserID", userSponsorExtendedSurvey.ProjectRoundUserID),
				new SqlParameter("@AnswerID", userSponsorExtendedSurvey.AnswerID),
				new SqlParameter("@FinishedEmail", userSponsorExtendedSurvey.FinishedEmail),
				new SqlParameter("@ContactRequestDT", userSponsorExtendedSurvey.ContactRequestDT),
				new SqlParameter("@ContactRequest", userSponsorExtendedSurvey.ContactRequest)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UserSponsorExtendedSurvey
WHERE UserSponsorExtendedSurveyID = @UserSponsorExtendedSurveyID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserSponsorExtendedSurveyID", id)
			);
		}
		
		public override UserSponsorExtendedSurvey Read(int id)
		{
			string query = @"
SELECT 	UserSponsorExtendedSurveyID, 
	UserID, 
	SponsorExtendedSurveyID, 
	ProjectRoundUserID, 
	AnswerID, 
	FinishedEmail, 
	ContactRequestDT, 
	ContactRequest
FROM UserSponsorExtendedSurvey
WHERE UserSponsorExtendedSurveyID = @UserSponsorExtendedSurveyID";
			UserSponsorExtendedSurvey userSponsorExtendedSurvey = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserSponsorExtendedSurveyID", id))) {
				if (rs.Read()) {
					userSponsorExtendedSurvey = new UserSponsorExtendedSurvey {
						UserSponsorExtendedSurveyID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						SponsorExtendedSurveyID = GetInt32(rs, 2),
						ProjectRoundUserID = GetInt32(rs, 3),
						AnswerID = GetInt32(rs, 4),
						FinishedEmail = GetDateTime(rs, 5),
						ContactRequestDT = GetDateTime(rs, 6),
						ContactRequest = GetString(rs, 7)
					};
				}
			}
			return userSponsorExtendedSurvey;
		}
		
		public override IList<UserSponsorExtendedSurvey> FindAll()
		{
			string query = @"
SELECT 	UserSponsorExtendedSurveyID, 
	UserID, 
	SponsorExtendedSurveyID, 
	ProjectRoundUserID, 
	AnswerID, 
	FinishedEmail, 
	ContactRequestDT, 
	ContactRequest
FROM UserSponsorExtendedSurvey";
			var userSponsorExtendedSurveys = new List<UserSponsorExtendedSurvey>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					userSponsorExtendedSurveys.Add(new UserSponsorExtendedSurvey {
						UserSponsorExtendedSurveyID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						SponsorExtendedSurveyID = GetInt32(rs, 2),
						ProjectRoundUserID = GetInt32(rs, 3),
						AnswerID = GetInt32(rs, 4),
						FinishedEmail = GetDateTime(rs, 5),
						ContactRequestDT = GetDateTime(rs, 6),
						ContactRequest = GetString(rs, 7)
					});
				}
			}
			return userSponsorExtendedSurveys;
		}
	}
}
