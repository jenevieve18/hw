using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlUserRepository : BaseSqlRepository<User>, IUserRepository
	{
		public void SaveUserProfileBackgroundQuestion(UserProfileBackgroundQuestion s)
		{
			string query = string.Format(
				@"
INSERT INTO UserProfileBQ (UserProfileID, BQID, ValueInt, ValueText, ValueDate)
VALUES (@UserProfileID, @BQID, @ValueInt, @ValueText, @ValueDate)"
			);
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@UserProfileID", s.Profile.Id),
				new SqlParameter("@BQID", s.Question.Id),
				new SqlParameter("@ValueInt", s.ValueInt),
				new SqlParameter("@ValueText", s.ValueText),
				new SqlParameter("@ValueDate", s.ValueDate)
			);
		}
		
		public void UpdateUser(int userID, int sponsorID, int departmentID)
		{
			string query = "UPDATE [User] SET DepartmentID = " + departmentID + ", SponsorID = " + sponsorID + " WHERE UserID = " + userID;
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateUserProfile(int userID, int sponsorID, int departmentID)
		{
			string query = "UPDATE UserProfile SET DepartmentID = " + departmentID + ", SponsorID = " + sponsorID + " WHERE UserID = " + userID;
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateUserProjectRoundUser(int projectRoundUnitID, int userProjectRoundUserID)
		{
			string query = string.Format(
				@"
UPDATE UserProjectRoundUser SET ProjectRoundUnitID = {0}
WHERE UserProjectRoundUserID = {1}",
				projectRoundUnitID,
				userProjectRoundUserID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateEmailFailure(int userID)
		{
			string query = "UPDATE [User] SET EmailFailure = GETDATE() WHERE UserID = " + userID;
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateLastReminderSent(int userID)
		{
			string query = "UPDATE [User] SET ReminderLastSent = GETDATE() WHERE UserID = " + userID;
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public User ReadByIdAndSponsorExtendedSurvey(int userID, int sponsorExtendedSurveyID)
		{
			string query = string.Format(
				@"
SELECT ses.ExtraEmailBody,
	ses.ExtraEmailSubject,
	u.Email,
	u.UserID,
	u.ReminderLink,
	LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)
FROM [User] u
INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = {1}
WHERE u.UserID = {0}",
				userID,
				sponsorExtendedSurveyID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var s = new Sponsor();
					s.ExtendedSurveys = new List<SponsorExtendedSurvey>(
						new SponsorExtendedSurvey[] {
							new SponsorExtendedSurvey { ExtraEmailBody = rs.GetString(0), ExtraEmailSubject = rs.GetString(1) }
						}
					);
					var u = new User {
						Sponsor = s,
						Email = rs.GetString(2),
						Id = rs.GetInt32(3),
//						ReminderLink = rs.GetInt32(4),
						ReminderLink = GetInt32(rs, 4),
						UserKey = rs.GetString(5)
					};
					return u;
				}
			}
			return null;
		}
		
		public User ReadById(int userID)
		{
			string query = string.Format(
				@"
SELECT UserID,
	SponsorID
FROM [User]
WHERE UserID = {0}",
				userID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var u = new User {
						Id = rs.GetInt32(0),
						Sponsor = new Sponsor { Id = rs.GetInt32(1) }
					};
					return u;
				}
			}
			return null;
		}
		
		public IList<UserProjectRoundUser> FindUserProjectRoundUser(int sponsorID, int surveyID, int userID)
		{
			string query = string.Format(
				@"
SELECT upru.UserProjectRoundUserID,
	upru.ProjectRoundUserID
FROM UserProjectRoundUser upru
INNER JOIN [user] hu ON upru.UserID = hu.UserID
INNER JOIN [eform]..[ProjectRoundUser] pru ON upru.ProjectRoundUserID = pru.ProjectRoundUserID
INNER JOIN [eform]..[ProjectRoundUnit] u ON pru.ProjectRoundUnitID = u.ProjectRoundUnitID
WHERE hu.SponsorID = {0}
AND u.SurveyID = {1}
AND upru.UserID = {2}",
				sponsorID,
				surveyID,
				userID
			);
			var users = new List<UserProjectRoundUser>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var u = new UserProjectRoundUser {
						Id = rs.GetInt32(0),
						ProjectRoundUser = new ProjectRoundUser { Id = rs.GetInt32(1) }
					};
					users.Add(u);
				}
			}
			return users;
		}
		
		public IList<User> Find2(int sponsorID, int sponsorAdminID)
		{
			string j = sponsorAdminID != -1
				? "INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + sponsorAdminID + " AND "
				: "WHERE ";
			string query = string.Format(
				@"
SELECT DISTINCT u.UserID,
	u.Email
FROM [User] u
INNER JOIN SponsorInvite si ON u.UserID = si.UserID
{1}si.SponsorID = {0}
AND u.Email IS NOT NULL
AND u.Email <> ''
AND si.StoppedReason IS NULL
AND u.Email NOT LIKE '%DELETED'",
				sponsorID,
				j
			);
			var users = new List<User>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var u = new User {
						Id = rs.GetInt32(0),
						Email = rs.GetString(1),
					};
					users.Add(u);
				}
			}
			return users;
		}
		
		public IList<User> FindBySponsorWithExtendedSurvey2(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			string j = sponsorAdminID != -1
				? "INNER JOIN SponsorAdminDepartment sad ON u.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + sponsorAdminID + " AND "
				: "WHERE ";
			string query = string.Format(
				@"
SELECT DISTINCT u.UserID,
	u.Email,
	u.ReminderLink,
	LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)
FROM [User] u
INNER JOIN Department d ON u.DepartmentID = d.DepartmentID
INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = {1}
INNER JOIN SponsorInvite si ON u.UserID = si.UserID AND si.SponsorID = ses.SponsorID
INNER JOIN eform..ProjectRound pr ON pr.ProjectRoundID = ses.ProjectRoundID
LEFT OUTER JOIN UserSponsorExtendedSurvey x ON u.UserID = x.UserID AND x.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID AND x.AnswerID IS NOT NULL
LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON si.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID
{2}u.SponsorID = {0}
AND (pr.Started <= GETDATE() OR ISNULL(d.PreviewExtendedSurveys,si.PreviewExtendedSurveys) IS NOT NULL)
AND x.AnswerID IS NULL
AND u.Email IS NOT NULL
AND u.Email <> ''
AND si.StoppedReason IS NULL
AND sesd.Hide IS NULL
AND u.Email NOT LIKE '%DELETED'",
				sponsorID,
//				sponsorAdminID,
				sponsorExtendedSurveyID,
				j
			);
			var users = new List<User>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var u = new User {
						Id = rs.GetInt32(0),
						Email = rs.GetString(1),
//						ReminderLink = rs.GetInt32(2),
						ReminderLink = GetInt32(rs, 2),
						UserKey = rs.GetString(3)
					};
					users.Add(u);
				}
			}
			return users;
		}
		
		public IList<User> FindBySponsorWithExtendedSurvey(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			string j = sponsorAdminID != -1
				? "INNER JOIN SponsorAdminDepartment sad ON u.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + sponsorAdminID + " AND "
				: "WHERE ";
			string query = string.Format(
				@"
SELECT DISTINCT u.UserID,
	u.Email,
	u.ReminderLink,
	LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)
FROM [User] u
INNER JOIN Department d ON u.DepartmentID = d.DepartmentID
INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = {1}
INNER JOIN SponsorInvite si ON u.UserID = si.UserID AND si.SponsorID = ses.SponsorID
INNER JOIN eform..ProjectRound pr ON pr.ProjectRoundID = ses.ProjectRoundID
INNER JOIN UserSponsorExtendedSurvey x ON u.UserID = x.UserID AND x.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID AND x.AnswerID IS NOT NULL
LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON si.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID
{2}u.SponsorID = {0}
AND x.FinishedEmail IS NULL
AND x.AnswerID IS NOT NULL
AND u.Email IS NOT NULL
AND u.Email <> ''
AND si.StoppedReason IS NULL
AND sesd.Hide IS NULL
AND u.Email NOT LIKE '%DELETED'",
				sponsorID,
				sponsorExtendedSurveyID,
				j
			);
			var users = new List<User>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var u = new User {
						Id = rs.GetInt32(0),
						Email = rs.GetString(1),
//						ReminderLink = rs.GetInt32(2),
						ReminderLink = GetInt32(rs, 2),
						UserKey = rs.GetString(3)
					};
					users.Add(u);
				}
			}
			return users;
		}
		
		public IList<User> FindBySponsorWithLoginDays(int sponsorID, int sponsorAdminID, int loginDays)
		{
			string j = sponsorAdminID != -1
				? string.Format("INNER JOIN SponsorAdminDepartment sad ON u.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT DISTINCT u.UserID,
	u.Email,
	u.ReminderLink,
	LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)
FROM [User] u
INNER JOIN SponsorInvite si ON u.UserID = si.UserID
{2}u.SponsorID =  {0}
AND u.Email IS NOT NULL
AND u.Email <> ''
AND si.StoppedReason IS NULL
AND u.Email NOT LIKE '%DELETED'
AND dbo.cf_daysFromLastLogin(u.UserID) >= {1}
AND (u.ReminderLastSent IS NULL OR DATEADD(hh,1,u.ReminderLastSent) < GETDATE())",
				sponsorID,
				loginDays,
				j
			);
			var users = new List<User>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var u = new User {
						Id = rs.GetInt32(0),
						Email = rs.GetString(1),
//						ReminderLink = rs.GetInt32(2),
						ReminderLink = GetInt32(rs, 2),
						UserKey = rs.GetString(3)
					};
					users.Add(u);
				}
			}
			return users;
		}
		
		public int CountBySponsorWithAdminAndExtendedSurvey(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			string j = sponsorAdminID != -1
				? string.Format(@"INNER JOIN SponsorAdminDepartment sad ON u.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM [User] u
INNER JOIN Department d ON u.DepartmentID = d.DepartmentID
INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = {1}
INNER JOIN SponsorInvite si ON u.UserID = si.UserID AND si.SponsorID = ses.SponsorID
INNER JOIN eform..ProjectRound pr ON pr.ProjectRoundID = ses.ProjectRoundID
INNER JOIN UserSponsorExtendedSurvey x ON u.UserID = x.UserID AND x.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID AND x.AnswerID IS NOT NULL
LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON si.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID
{2}u.SponsorID = {0}
AND x.FinishedEmail IS NULL
AND x.AnswerID IS NOT NULL
AND u.Email IS NOT NULL
AND u.Email <> ''
AND si.StoppedReason IS NULL
AND sesd.Hide IS NULL
AND u.Email NOT LIKE '%DELETED'",
				sponsorID,
				sponsorExtendedSurveyID,
				j
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public int CountBySponsorWithAdminAndExtendedSurvey2(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			string j = sponsorAdminID != -1
				? string.Format(@"INNER JOIN SponsorAdminDepartment sad ON u.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM [User] u
INNER JOIN Department d ON u.DepartmentID = d.DepartmentID
INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = {1}
INNER JOIN SponsorInvite si ON u.UserID = si.UserID AND si.SponsorID = ses.SponsorID
INNER JOIN eform..ProjectRound pr ON pr.ProjectRoundID = ses.ProjectRoundID
LEFT OUTER JOIN UserSponsorExtendedSurvey x ON u.UserID = x.UserID AND x.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID AND x.AnswerID IS NOT NULL
LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON si.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID
{2} u.SponsorID = {0}
AND (pr.Started <= GETDATE() OR ISNULL(d.PreviewExtendedSurveys,si.PreviewExtendedSurveys) IS NOT NULL)
AND x.AnswerID IS NULL
AND u.Email IS NOT NULL
AND u.Email <> ''
AND si.StoppedReason IS NULL
AND sesd.Hide IS NULL
AND u.Email NOT LIKE '%DELETED'",
				sponsorID,
				sponsorExtendedSurveyID,
				j
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public int a(int sponsorID, int sponsorAdminID)
		{
			string q = sponsorAdminID != -1
				? string.Format(@"INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM [User] u
INNER JOIN SponsorInvite si ON u.UserID = si.UserID
{1} si.SponsorID = {0}
AND u.Email IS NOT NULL
AND u.Email <> ''
AND si.StoppedReason IS NULL
AND u.Email NOT LIKE '%DELETED'",
				sponsorID,
				q
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
	}
}
