using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlUserRepository : BaseSqlRepository<User>//, IUserRepository
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
				new SqlParameter("@BQID", s.BackgroundQuestion.Id),
				new SqlParameter("@ValueInt", s.ValueInt),
				new SqlParameter("@ValueText", s.ValueText),
				new SqlParameter("@ValueDate", s.ValueDate)
			);
		}
		
		public void SaveUserProfileBackgroundQuestion(int profileID, int bqID, int valueInt, string valueText, DateTime? valueDate)
		{
			string query = string.Format(
				@"
INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt,ValueText,ValueDate)
VALUES ({0},{1},{2},{3},{4})",
				profileID,
				bqID,
				(valueInt == 0 ? "NULL" : valueInt.ToString()),
				(valueText == "" ? "NULL" : "'" + valueText.Replace("'", "") + "'"),
				(valueDate == null ? "NULL" : "'" + valueDate.Value.ToString("yyyy-MM-dd") + "'")
			);
			Db.exec(query);
		}
		
		public void SaveUserProfile(int userID, int profileComparisonID)
		{
			string query = string.Format(
				@"
INSERT INTO UserProfile (UserID,SponsorID,DepartmentID,ProfileComparisonID,Created)
VALUES ({0},1,NULL,{1},GETDATE())",
				userID,
				profileComparisonID
			);
			Db.exec(query);
		}
		
		public void UpdateProjectRoundUser(int projectRoundUserID, int projectRoundUnitID)
		{
			string query = string.Format(
				@"
UPDATE UserProjectRoundUser SET ProjectRoundUnitID = {0}
WHERE UserProjectRoundUserID = {1}",
				projectRoundUnitID,
				projectRoundUserID
			);
			Db.exec(query);
			query = string.Format(
				@"
UPDATE [eform]..[ProjectRoundUser] SET ProjectRoundUnitID = {0}
WHERE ProjectRoundUserID = {1}",
				projectRoundUnitID,
				projectRoundUserID
			);
			Db.exec(query);
			query = string.Format(
				@"
UPDATE [eform]..[Answer] SET ProjectRoundUnitID = {0}
WHERE ProjectRoundUserID = {1}",
				projectRoundUnitID,
				projectRoundUserID
			);
			Db.exec(query);
		}
		
//		public void UpdateUser(int userID, int sponsorID, int departmentID)
//		{
//			string query = "UPDATE [User] SET DepartmentID = " + departmentID + ", SponsorID = " + sponsorID + " WHERE UserID = " + userID;
//			Db.exec(query, "healthWatchSqlConnection");
//		}
		
//		public void UpdateUserProfile(int userID, int sponsorID, int departmentID)
//		{
//			string query = "UPDATE UserProfile SET DepartmentID = " + departmentID + ", SponsorID = " + sponsorID + " WHERE UserID = " + userID;
//			Db.exec(query, "healthWatchSqlConnection");
//		}
		
//		public void UpdateUserProjectRoundUser(int projectRoundUnitID, int userProjectRoundUserID)
//		{
//			string query = string.Format(
//				@"
		//UPDATE UserProjectRoundUser SET ProjectRoundUnitID = {0}
		//WHERE UserProjectRoundUserID = {1}",
//				projectRoundUnitID,
//				userProjectRoundUserID
//			);
//			Db.exec(query, "healthWatchSqlConnection");
//		}
		
		public void UpdateWithDepartment(string unit, int userID, int sponsorID)
		{
			string query = string.Format("UPDATE [User] SET DepartmentID = {0} WHERE UserID = {1} AND SponsorID = {2}", unit, userID, sponsorID);
			Db.exec(query);
			query = string.Format("UPDATE UserProfile SET DepartmentID = {0} WHERE UserID = {1} AND SponsorID = {2}", unit, userID, sponsorID);
			Db.exec(query);
		}
		
		public void Update3(int profileID, int userID)
		{
			string query = string.Format(
				@"
UPDATE [User] SET UserProfileID = {0} WHERE UserID = {1}",
				profileID,
				userID
			);
			Db.exec(query);
		}
		
		public void Update2(int userID, int sponsorID)
		{
			string query = string.Format("UPDATE [User] SET DepartmentID = NULL, SponsorID = 1 WHERE UserID = " + userID + " AND SponsorID = " + sponsorID);
			Db.exec(query);
		}
		
		public void Update(int userID, int sponsorID)
		{
			string query = string.Format("UPDATE [User] SET DepartmentID = NULL, SponsorID = 1 WHERE UserID = " + userID + " AND SponsorID = " + sponsorID);
			Db.exec(query);
			query = string.Format("UPDATE UserProfile SET DepartmentID = NULL, SponsorID = 1 WHERE UserID = " + userID + " AND SponsorID = " + sponsorID);
			Db.exec(query);
		}
		
		public void Update(Department d, int sponsorID, int deleteDepartmentID)
		{
			string query = string.Format("UPDATE [User] SET DepartmentID = " + (d.Parent == null ? "NULL" : d.Parent.Id.ToString()) + " WHERE SponsorID = " + sponsorID + " AND DepartmentID = " + deleteDepartmentID);
			Db.exec(query);
			query = string.Format("UPDATE UserProfile SET DepartmentID = " + (d.Parent == null ? "NULL" : d.Parent.Id.ToString()) + " WHERE SponsorID = " + sponsorID + " AND DepartmentID = " + deleteDepartmentID);
			Db.exec(query);
			query = string.Format("UPDATE Department SET ParentDepartmentID = " + (d.Parent == null ? "NULL" : d.Parent.Id.ToString()) + " WHERE SponsorID = " + sponsorID + " AND ParentDepartmentID = " + deleteDepartmentID);
			Db.exec(query);
		}
		
		public void UpdateEmailFailure(int userID)
		{
			string query = "UPDATE [User] SET EmailFailure = GETDATE() WHERE UserID = " + userID;
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateLastReminderSent(int userID)
		{
			string query = string.Format("UPDATE [User] SET ReminderLastSent = GETDATE() WHERE UserID = {0}", userID);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		// TODO: Check the relationship between user and sponsor!
		public SponsorExtendedSurvey ReadByIdAndSponsorExtendedSurvey2(int userID, int sponsorExtendedSurveyID)
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
					var u = new User {
						Sponsor = s,
						Email = rs.GetString(2),
						Id = rs.GetInt32(3),
						ReminderLink = GetInt32(rs, 4),
						UserKey = rs.GetString(5)
					};
					var e = new SponsorExtendedSurvey {
						Sponsor = s,
						ExtraEmailBody = rs.GetString(0),
						ExtraEmailSubject = rs.GetString(1)
					};
					return e;
				}
			}
			return null;
		}
		
//		public User ReadByIdAndSponsorExtendedSurvey(int userID, int sponsorExtendedSurveyID)
//		{
//			string query = string.Format(
//				@"
		//SELECT ses.ExtraEmailBody,
//	ses.ExtraEmailSubject,
//	u.Email,
//	u.UserID,
//	u.ReminderLink,
//	LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)
		//FROM [User] u
		//INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = {1}
		//WHERE u.UserID = {0}",
//				userID,
//				sponsorExtendedSurveyID
//			);
//			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
//				if (rs.Read()) {
//					var s = new Sponsor();
//					s.ExtendedSurveys = new List<SponsorExtendedSurvey>(
//						new SponsorExtendedSurvey[] {
//							new SponsorExtendedSurvey { ExtraEmailBody = rs.GetString(0), ExtraEmailSubject = rs.GetString(1) }
//						}
//					);
//					var u = new User {
//						Sponsor = s,
//						Email = rs.GetString(2),
//						Id = rs.GetInt32(3),
//						ReminderLink = GetInt32(rs, 4),
//						UserKey = rs.GetString(5)
//					};
//					return u;
//				}
//			}
//			return null;
//		}
		
		public void lalala3(int projectRoundUserId, int answerId)
		{
			Db.exec(string.Format("UPDATE UserSponsorExtendedSurvey SET AnswerID = {0} WHERE AnswerID IS NULL AND ProjectRoundUserID = {1}", answerId, projectRoundUserId));
			Db.exec(string.Format("UPDATE Answer SET EndDT = GETDATE() WHERE ProjectRoundUserID = {0} AND AnswerID = {1}", projectRoundUserId, answerId), "eFormSqlConnection");
		}
		
		public void lalala2(int projectRoundUserId, int answerId)
		{
			Db.exec(string.Format("UPDATE UserSponsorExtendedSurvey SET AnswerID = NULL WHERE ProjectRoundUserID = {0} AND AnswerID = {1}", projectRoundUserId, answerId));
			Db.exec(string.Format("UPDATE Answer SET EndDT = NULL WHERE ProjectRoundUserID = {0} AND AnswerID = {1}", projectRoundUserId, answerId), "eFormSqlConnection");
		}
		
		public void lalala(int sponsorInviteID, int sponsorID, string email, int departmentID)
		{
			string query = string.Format(
				@"
SELECT u2.UserID,
	u2.SponsorID
FROM [User] u2
LEFT OUTER JOIN SponsorInvite si ON u2.UserID = si.UserID
WHERE u2.Email = '{0}' OR si.Email = '{0}'",
				email.Replace("'", "''")
			);
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					int userID = rs.GetInt32(0);
					int fromSponsorID = rs.GetInt32(1);

					Db.rewritePRU(fromSponsorID, sponsorID, userID);
					Db.exec("UPDATE SponsorInvite SET SponsorID = -ABS(SponsorID), DepartmentID = -ABS(DepartmentID), UserID = -ABS(UserID) WHERE UserID = " + userID);
					Db.exec("UPDATE SponsorInvite SET UserID = " + userID + ", Sent = GETDATE() WHERE SponsorInviteID = " + sponsorInviteID);
					Db.exec("UPDATE [User] SET DepartmentID = " + departmentID + ", SponsorID = " + sponsorID + " WHERE UserID = " + userID);
					Db.exec("UPDATE UserProfile SET DepartmentID = " + departmentID + ", SponsorID = " + sponsorID + " WHERE UserID = " + userID);
					
					while (rs.Read()) {
						userID = rs.GetInt32(0);
						Db.exec("UPDATE SponsorInvite SET SponsorID = -ABS(SponsorID), DepartmentID = -ABS(DepartmentID), UserID = -ABS(UserID) WHERE UserID = " + userID);
						Db.exec("UPDATE [User] SET DepartmentID = NULL, SponsorID = 1 WHERE UserID = " + userID);
						Db.exec("UPDATE UserProfile SET DepartmentID = NULL, SponsorID = 1 WHERE UserID = " + userID);
					}
				}
				Db.exec("UPDATE SponsorInvite SET SponsorID = -ABS(SponsorID), DepartmentID = -ABS(DepartmentID), UserID = -ABS(UserID) WHERE Email = '" + email.Replace("'", "") + "' AND SponsorInviteID <> " + sponsorInviteID);
			}
		}
		
		public UserProfile ReadUserProfile(int userID)
		{
			string query = string.Format(
				@"
SELECT TOP 1 UserProfileID
FROM UserProfile
WHERE UserID = {1} ORDER BY UserProfileID DESC",
				userID
			);
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					return new UserProfile {
						Id = GetInt32(rs, 0)
					};
				}
			}
			return null;
		}
		
		public User Read2(string bqID, int userID)
		{
			string query = string.Format(
				@"
SELECT b.UserBQID
FROM [User] u
INNER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID
INNER JOIN UserProfileBQ b ON up.UserProfileID = b.UserProfileID AND b.BQID = {0}
WHERE up.UserID = {1}",
				bqID,
				userID
			);
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					return new User {
						Profile = new UserProfile {
						}
					};
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
		
		public IList<User> Find(string email)
		{
			string query = string.Format(
				@"
SELECT u.UserID,
	s.Sponsor
FROM [User] u
LEFT OUTER JOIN Sponsor s ON u.SponsorID = s.SponsorID
WHERE u.Email = '{0}'",
				email.Replace("'", "''")
			);
			var users = new List<User>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var u = new User {
						Id = GetInt32(rs, 0),
						Sponsor = rs.IsDBNull(1) ? null : new Sponsor { Id = GetInt32(rs, 1) }
					};
					users.Add(u);
				}
			}
			return users;
		}
		
		public IList<User> Find(int userID, string email)
		{
			string query = string.Format(
				@"
SELECT u.UserID,
	s.Sponsor
FROM [User] u
LEFT OUTER JOIN Sponsor s ON u.SponsorID = s.SponsorID
WHERE u.UserID <> {0} AND u.Email = '{1}'",
				userID,
				email.Replace("'", "''")
			);
			var users = new List<User>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var u = new User {
						Id = GetInt32(rs, 0),
						Sponsor = rs.IsDBNull(1) ? null : new Sponsor { Id = GetInt32(rs, 1) }
					};
					users.Add(u);
				}
			}
			return users;
		}
		
		public IList<User> Find(int userID)
		{
			string query = string.Format(
				@"
SELECT u.UserProfileID,
	up.ProfileComparisonID
FROM [User] u
INNER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID
WHERE u.UserID = {0}",
				userID
			);
			var users = new List<User>();
			using (SqlDataReader rs2 = Db.rs(query)) {
				while (rs2.Read()) {
					var u = new User {
						Profile = new UserProfile {
							Id = GetInt32(rs2, 0),
							ProfileComparison = new ProfileComparison { Id =  GetInt32(rs2, 1) }
						}
					};
					users.Add(u);
				}
			}
			return users;
		}
		
		public IList<User> FindBySponsorInvite(int sponsorInviteID)
		{
			string query = string.Format(
				@"
SELECT u.UserID
FROM [User] u
INNER JOIN SponsorInvite si ON u.UserID = si.UserID
WHERE si.SponsorInviteID = {0}",
				sponsorInviteID
			);
			var users = new List<User>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					users.Add(new User { Id = GetInt32(rs, 0) });
				}
			}
			return users;
		}
		
		public IList<UserProfileBackgroundQuestion> FindUserProfileBackgroundQuestions(int userProfileID)
		{
			string query = string.Format(
				@"
SELECT BQID,
	ValueInt,
	ValueText,
	ValueDate
FROM UserProfileBQ
WHERE UserProfileID = {0}",
				userProfileID
			);
			var questions = new List<UserProfileBackgroundQuestion>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var q = new UserProfileBackgroundQuestion {
						BackgroundQuestion = new BackgroundQuestion { Id = GetInt32(rs, 0) },
						ValueInt = GetInt32(rs, 1),
						ValueText = GetString(rs, 2),
						ValueDate = GetDateTime(rs, 3)
					};
					questions.Add(q);
				}
			}
			return questions;
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
