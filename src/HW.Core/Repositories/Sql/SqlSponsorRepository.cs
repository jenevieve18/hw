using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlSponsorRepository : BaseSqlRepository<SponsorAdmin>, ISponsorRepository
	{
		public void UpdateSponsorLastLoginSent(int sponsorID)
		{
			string query = string.Format(
				@"
UPDATE Sponsor SET LoginLastSent = GETDATE()
WHERE SponsorID = {0}",
				sponsorID
			);
			Db.exec(query, "healthWatchSqlConnection"); // TODO: move to department???
		}
		
		public void UpdateLastAllMessageSent(int sponsorID)
		{
			string query = string.Format(
				@"
UPDATE Sponsor SET AllMessageLastSent = GETDATE()
WHERE SponsorID = {0}",
				sponsorID
			);
			Db.exec(query, "healthWatchSqlConnection"); // TODO: move to department???
		}
		
		public void UpdateSponsorLastInviteReminderSent(int sponsorID)
		{
			string query = string.Format(
				@"
UPDATE Sponsor SET InviteReminderLastSent = GETDATE()
WHERE SponsorID = {0}",
				sponsorID
			);
			Db.exec(query, "healthWatchSqlConnection"); // TODO: move to department???
		}
		
		public void UpdateSponsorLastInviteSent(int sponsorID)
		{
			string query = string.Format(
				@"
UPDATE Sponsor SET InviteLastSent = GETDATE()
WHERE SponsorID = {0}",
				sponsorID
			);
			Db.exec(query, "healthWatchSqlConnection"); // TODO: move to department???
		}
		
		public void Z(int sponsorInviteID, string previewExtendedSurveys)
		{
			string query = string.Format(
				@"
UPDATE SponsorInvite SET PreviewExtendedSurveys = {0}
WHERE SponsorInviteID = {1}",
				previewExtendedSurveys,
				sponsorInviteID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateSponsorInviteSent(int userID, int sponsorInviteID)
		{
			string query = string.Format(
				@"
UPDATE SponsorInvite SET UserID = {0}, Sent = GETDATE()
WHERE SponsorInviteID = {1}",
				userID,
				sponsorInviteID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateSponsorInviteSent(int sponsorInviteID)
		{
			string query = string.Format(
				@"
UPDATE SponsorInvite SET Sent = GETDATE()
WHERE SponsorInviteID = {0}",
				sponsorInviteID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateNullUserForUserInvite(int userID)
		{
			string query = string.Format(
				@"
UPDATE SponsorInvite SET UserID = NULL
WHERE UserID = {0}",
				userID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateExtendedSurveyLastEmailSent(int sponsorExtendedSurveyID)
		{
			string query = string.Format(
				@"
UPDATE SponsorExtendedSurvey SET EmailLastSent = GETDATE()
WHERE SponsorExtendedSurveyID = {0}",
				sponsorExtendedSurveyID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateExtendedSurveyLastFinishedSent(int sponsorExtendedSurveyID)
		{
			string query = string.Format(
				@"
UPDATE SponsorExtendedSurvey SET FinishedLastSent = GETDATE()
WHERE SponsorExtendedSurveyID = {0}",
				sponsorExtendedSurveyID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateSponsor(int sponsorID)
		{
			string query = string.Format(
				@"
UPDATE Sponsor SET AllMessageLastSent = GETDATE()
WHERE SponsorID = {0}",
				sponsorID
			);
			Db.exec(query, "healthWatchSqlConnection"); // TODO: move to department???
		}
		
		public void UpdateSponsorExtendedSurvey(SponsorExtendedSurvey s)
		{
			string query = string.Format(
				@"
UPDATE SponsorExtendedSurvey SET
EmailSubject = @EmailSubject,
EmailBody = @EmailBody,
FinishedEmailSubject = @FinishedEmailSubject,
FinishedEmailBody = @FinishedEmailBody
WHERE SponsorExtendedSurveyID = @SponsorExtendedSurveyID"
			);
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@EmailSubject", s.EmailSubject),
				new SqlParameter("@EmailBody", s.EmailBody),
				new SqlParameter("@FinishedEmailSubject", s.FinishedEmailSubject),
				new SqlParameter("@FinishedEmailBody", s.FinishedEmailBody),
				new SqlParameter("@SponsorExtendedSurveyID", s.Id)
			);
		}
		
		public void UpdateSponsor(Sponsor s)
		{
//			string query = string.Format(
//				@"
			//UPDATE Sponsor SET
//	InviteTxt = @InviteTxt,
//	InviteReminderTxt = @InviteReminderTxt,
//	AllMessageSubject = @AllMessageSubject,
//	LoginTxt = @LoginTxt,
//	InviteSubject = @InviteSubject,
//	InviteReminderSubject = @InviteReminderSubject,
//	AllMessageBody = @AllMessageBody,
//	LoginSubject = @LoginSubject,
//	LoginDays = @LoginDays,
//	LoginWeekday = @LoginWeekday
			//WHERE SponsorID = @SponsorID"
//			);
//			ExecuteNonQuery(
//				query,
//				"healthWatchSqlConnection",
//				new SqlParameter("@InviteTxt", s.InviteText),
//				new SqlParameter("@InviteReminderTxt", s.InviteReminderText),
//				new SqlParameter("@AllMessageSubject", s.AllMessageSubject),
//				new SqlParameter("@LoginTxt", s.LoginText),
//				new SqlParameter("@InviteSubject", s.InviteSubject),
//				new SqlParameter("@InviteReminderSubject", s.InviteReminderSubject),
//				new SqlParameter("@AllMessageBody", s.AllMessageBody),
//				new SqlParameter("@LoginSubject", s.LoginSubject),
//				new SqlParameter("@LoginDays", s.LoginDays),
//				new SqlParameter("@LoginWeekday", s.LoginWeekday),
//				new SqlParameter("@SponsorID", s.Id)
//			);
			string query = string.Format(
				@"
UPDATE Sponsor SET
	InviteTxt = '{0}',
	InviteReminderTxt = '{1}',
	AllMessageSubject = '{2}',
	LoginTxt = '{3}',
	InviteSubject = '{4}',
	InviteReminderSubject = '{5}',
	AllMessageBody = '{6}',
	LoginSubject = '{7}',
	LoginDays = {8},
	LoginWeekday = {9}
WHERE SponsorID = {10}",
				s.InviteText,
				s.InviteReminderText,
				s.AllMessageSubject,
				s.LoginText,
				s.InviteSubject,
				s.InviteReminderSubject,
				s.AllMessageBody,
				s.LoginSubject,
				s.LoginDays,
				s.LoginWeekday,
				s.Id
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateSponsorAdminPassword(string password, int sponsorAdminID)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdmin SET Pas = '{0}' WHERE SponsorAdminID = {1}",
				password,
				sponsorAdminID
			);
			Db2.exec(query);
		}
		
		public void UpdateSponsorAdmin(SponsorAdmin a)
		{
			string p = (a.Password != "Not shown" && a.Password != "")
				? string.Format("Pas = '{0}',", a.Password.Replace("'", "''"))
				: "";
			string query = string.Format(
				@"
UPDATE SponsorAdmin SET ReadOnly = {0},
	Email = '{1}',
	Name = '{2}',
	Usr = '{3}',
	{4}
	SuperUser = {5}
WHERE SponsorAdminID = {6}
AND SponsorID = {7}",
				a.ReadOnly ? "1" : "0",
//				a.ReadOnly,
				a.Email.Replace("'", "''"),
				a.Name.Replace("'", "''"),
				a.Usr.Replace("'", ""),
				p,
				a.SuperUser ? "1" : "0",
//				a.SuperUser,
				a.Id,
				a.Sponsor.Id
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void SaveSponsorAdmin(SponsorAdmin a)
		{
//			string query = string.Format(
//				@"
			//INSERT INTO SponsorAdmin (Email, Name, Usr, Pas, SponsorID, SuperUser, ReadOnly)
			//VALUES ('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6})",
//				a.Email.Replace("'", "''"),
//				a.Name.Replace("'", "''"),
//				a.Usr.Replace("'", "''"),
//				a.Password.Replace("'", "''"),
//				a.Sponsor.Id,
//				a.SuperUser,
//				a.ReadOnly
//			);
//			Db.exec(query, "healthWatchSqlConnection");
			string query = string.Format(
				@"
INSERT INTO SponsorAdmin (Email, Name, Usr, Pas, SponsorID, SuperUser, ReadOnly)
VALUES (@Email, @Name, @Usr, @Pas, @SponsorID, @SuperUser, @ReadOnly)"
			);
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@Email", a.Email),
				new SqlParameter("@Name", a.Name),
				new SqlParameter("@Usr", a.Usr),
				new SqlParameter("@Pas", a.Password),
				new SqlParameter("@SponsorID", a.Sponsor.Id),
				new SqlParameter("@SuperUser", a.SuperUser),
				new SqlParameter("@ReadOnly", a.ReadOnly)
			);
		}
		
		public void SaveSponsorAdminFunction(SponsorAdminFunction f)
		{
//			string query = string.Format(
//				@"
			//INSERT INTO SponsorAdminFunction (SponsorAdminID, ManagerFunctionID)
			//VALUES ({0}, {1})",
//				f.Admin.Id,
//				f.Function.Id
//			);
//			Db.exec(query, "healthWatchSqlConnection");
			string query = string.Format(
				@"
INSERT INTO SponsorAdminFunction (SponsorAdminID, ManagerFunctionID)
VALUES (@SponsorAdminID, @ManagerFunctionID)"
			);
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@SponsorAdminID", f.Admin.Id),
				new SqlParameter("@ManagerFunctionID", f.Function.Id)
			);
		}
		
		public void DeleteSponsorAdminFunction(int sponsorAdminID)
		{
			Db.exec("DELETE FROM SponsorAdminFunction WHERE SponsorAdminID = " + sponsorAdminID, "healthWatchSqlConnection");
		}
		
		public void UpdateDeletedAdmin(int sponsorID, int sponsorAdminID)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdmin
	SET SponsorID = -ABS(SponsorID), Usr = Usr + 'DELETED'
WHERE SponsorAdminID = {1} AND SponsorID = {0}",
				sponsorID,
				sponsorAdminID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public int CountSentInvitesBySponsor3(int sponsorID, DateTime dt)
		{
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM SponsorInvite si INNER JOIN [User] u ON si.UserID = u.UserID
WHERE si.SponsorID = {0} AND u.Created < '{1}'",
				sponsorID,
				dt.ToString("yyyy-MM-dd")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public int CountSentInvitesBySponsor2(int sponsorID, DateTime dt)
		{
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM SponsorInvite si
LEFT OUTER JOIN [User] u ON si.UserID = u.UserID
WHERE si.SponsorID = {0}
AND (ISNULL(u.Created, si.Sent) < '{1}' OR si.Sent < '{1}')",
				sponsorID,
				dt.ToString("yyyy-MM-dd")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public int CountSentInvitesBySponsor(int sponsorID, DateTime dateSent)
		{
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM SponsorInvite AS si
LEFT OUTER JOIN [User] AS u ON si.UserID = u.UserID
WHERE (si.SponsorID = {0})
AND (ISNULL(u.Created, si.Sent) < '{1}')
OR (si.SponsorID = {0}) AND (si.Sent < '{1}')",
				sponsorID,
				dateSent.ToString("yyyy-MM-dd")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public int CountCreatedInvitesBySponsor(int sponsorID, DateTime dateCreated)
		{
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM SponsorInvite si
INNER JOIN [User] u ON si.UserID = u.UserID
WHERE si.SponsorID = {0} AND u.Created < '{1}'",
				sponsorID,
				dateCreated.ToString("yyyy-MM-dd")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public SponsorInvite ReadSponsorInviteByUser(int userID)
		{
			string query = string.Format(
				@"
SELECT Email,
	DepartmentID,
	StoppedReason,
	Stopped,
	UserID
FROM SponsorInvite
WHERE SponsorInviteID = " + userID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var i = new SponsorInvite {
						Email = GetString(rs, 0),
						Department = new Department { Id = GetInt32(rs, 1) },
						StoppedReason = GetInt32(rs, 2),
						Stopped = GetDateTime(rs, 3),
						User = new User { Id = GetInt32(rs, 4) }
					};
					return i;
				}
			}
			return null;
		}
		
		public SponsorInvite ReadSponsorInviteBySponsor(int inviteID, int sponsorID)
		{
			string query = string.Format(
				@"
SELECT s.InviteTxt,
	s.InviteSubject,
	si.Email,
	LEFT(REPLACE(CONVERT(VARCHAR(255),si.InvitationKey),'-',''),8),
	si.UserID,
	u.ReminderLink,
	LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12),
	s.LoginTxt,
	s.LoginSubject
FROM Sponsor s
INNER JOIN SponsorInvite si ON s.SponsorID = si.SponsorID
LEFT OUTER JOIN [User] u ON u.UserID = si.UserID
WHERE s.SponsorID = " + sponsorID + " AND si.SponsorInviteID = " + inviteID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var i = new SponsorInvite {
						Email = rs.GetString(2),
						InvitationKey = rs.GetString(3),
						/*User = new User {
							Id = rs.GetInt32(4),
							ReminderLink = rs.GetInt32(5),
							UserKey = rs.GetString(6)
						},*/
						User = rs.IsDBNull(4) ? null : new User {
							Id = rs.GetInt32(4),
							//ReminderLink = rs.GetInt32(5),
							ReminderLink = GetInt32(rs, 5),
							UserKey = rs.GetString(6)
						},
						Sponsor = new Sponsor {
							InviteText = rs.GetString(0),
							InviteSubject = rs.GetString(1),
							LoginText = rs.GetString(7),
							LoginSubject = rs.GetString(8)
						}
					};
					return i;
				}
			}
			return null;
		}
		
		public SponsorInvite ReadSponsorInvite(int sponsorInviteID)
		{
			string query = string.Format(
				@"
SELECT Email
FROM SponsorInvite
WHERE SponsorInviteID = {0}",
				sponsorInviteID
			);
			using (SqlDataReader rs = Db2.rs(query)) {
				if (rs.Read()) {
					var i = new SponsorInvite {
						Email = rs.GetString(0)
					};
					return i;
				}
			}
			return null;
		}
		
		public SponsorInvite ReadSponsorInvite(string email, int sponsorID)
		{
			string query = string.Format(
				@"
SELECT SponsorInviteID
FROM SponsorInvite
WHERE Email = '{0}'
AND SponsorID = {1}",
				email,
				sponsorID
			);
			using (SqlDataReader rs = Db2.rs(query)) {
				if (rs.Read()) {
					var i = new SponsorInvite {
						Id = rs.GetInt32(0)
					};
				}
			}
			return null;
		}
		
		public SponsorInviteBackgroundQuestion ReadSponsorInviteBackgroundQuestion(int sponsorID, int userID, int bqID)
		{
			string query = string.Format(
				@"
SELECT sib.BAID,
	sib.ValueInt,
	sib.ValueText,
	sib.ValueDate,
	bq.Type,
	up.UserProfileID
FROM SponsorInvite si
INNER JOIN SponsorInviteBQ sib ON si.SponsorInviteID = sib.SponsorInviteID AND sib.BQID = {2}
INNER JOIN bq ON sib.BQID = bq.BQID
INNER JOIN [User] u ON si.UserID = u.UserID
INNER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID
LEFT OUTER JOIN UserProfileBQ upbq ON up.UserProfileID = upbq.UserProfileID AND upbq.BQID = bq.BQID
WHERE upbq.UserBQID IS NULL
AND si.UserID = {1}
AND si.SponsorID = {0}",
				sponsorID,
				userID,
				bqID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var i = new SponsorInvite {
						User = new User {
							Profile = new UserProfile { Id = GetInt32(rs, 5) }
						}
					};
					var sib = new SponsorInviteBackgroundQuestion {
						Answer = new BackgroundAnswer { Id = rs.GetInt32(0) },
						ValueInt = GetInt32(rs, 1),
						ValueText = GetString(rs, 2),
						ValueDate = GetDateTime(rs, 3),
						Question = new BackgroundQuestion { Type = GetInt32(rs, 4) },
						Invite = i
					};
					return sib;
				}
			}
			return null;
		}
		
		public Sponsor X(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT s.Sponsor,
	ss.SuperSponsorID,
	ssl.Header
FROM Sponsor s
LEFT OUTER JOIN SuperSponsor ss ON s.SuperSponsorID = ss.SuperSponsorID
LEFT OUTER JOIN SuperSponsorLang ssl ON ss.SuperSponsorID = ssl.SuperSponsorID AND ssl.LangID = 1
WHERE s.SponsorID = {0}",
				sponsorID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var s = new Sponsor();
					s.Name = GetString(rs, 0);
					var u = new SuperSponsor { Id = GetInt32(rs, 1) };
					s.SuperSponsor = u;
					u.Languages = new List<SuperSponsorLanguage>(
						new SuperSponsorLanguage[] {
							new SuperSponsorLanguage { Header = GetString(rs, 2) }
						}
					);
					return s;
				}
			}
			return null;
		}
		
		public Sponsor ReadSponsor(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT s.InviteTxt,
	s.InviteReminderTxt,
	s.LoginTxt,
	s.InviteSubject,
	s.InviteReminderSubject,
	s.LoginSubject,
	s.InviteLastSent,
	s.InviteReminderLastSent,
	s.LoginLastSent,
	s.LoginDays,
	s.LoginWeekday,
	s.AllMessageSubject,
	s.AllMessageBody,
	s.AllMessageLastSent
FROM Sponsor s
WHERE s.SponsorID = {0}",
				sponsorID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var s = new Sponsor {
						InviteText = GetString(rs, 0),
						InviteReminderText = GetString(rs, 1),
						LoginText = GetString(rs, 2),
						InviteSubject = GetString(rs, 3),
						InviteReminderSubject = GetString(rs, 4),
						LoginSubject = GetString(rs, 5),
						InviteLastSent = GetDateTime(rs, 6),
						InviteReminderLastSent = GetDateTime(rs, 7),
						LoginLastSent = GetDateTime(rs, 8),
						LoginDays = GetInt32(rs, 9),
						LoginWeekday = GetInt32(rs, 10, -1),
						AllMessageSubject = GetString(rs, 11),
						AllMessageBody = GetString(rs, 12),
						AllMessageLastSent = GetDateTime(rs, 13),
					};
					return s;
				}
			}
			return null;
		}
		
		public bool SponsorAdminExists(int sponsorAdminID, string usr)
		{
			string query = string.Format(
				@"
SELECT SponsorAdminID FROM SponsorAdmin WHERE Usr = '{0}' {1}",
				usr.Replace("'", ""),
				(sponsorAdminID != 0 ? " AND SponsorAdminID != " + sponsorAdminID : "")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return true;
				}
			}
			return false;
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorID, int sponsorAdminID, int SAID)
		{
			string query = string.Format(
				@"
SELECT SponsorAdminID,
	Name,
	Usr,
	Email,
	SuperUser,
	ReadOnly
FROM SponsorAdmin
WHERE (SponsorAdminID <> {1} OR SuperUser = 1)
AND SponsorAdminID = {2}
AND SponsorID = {0}",
				sponsorID,
				sponsorAdminID,
				SAID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var a = new SponsorAdmin {
						Id = rs.GetInt32(0),
						Name = rs.GetString(1),
						Usr = rs.GetString(2),
						Email = rs.GetString(3),
						SuperUser = !rs.IsDBNull(4) && rs.GetInt32(4) != 0,
						ReadOnly = !rs.IsDBNull(5) && rs.GetInt32(5) != 0
					};
					return a;
				}
			}
			return null;
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT SuperUser FROM SponsorAdmin WHERE SponsorAdminID = {0}",
				sponsorAdminID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var a = new SponsorAdmin {
						SuperUser = !rs.IsDBNull(0) && rs.GetInt32(0) != 0 //GetBoolean(rs, 0)
					};
					return a;
				}
			}
			return null;
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorID, int sponsorAdminID, string password)
		{
			string query = string.Format(
				@"
SELECT SponsorAdminID
FROM SponsorAdmin
WHERE SponsorID = {0}
AND SponsorAdminID = {1}
AND Pas = '{2}'",
				sponsorID,
				sponsorAdminID,
				password.Replace("'", "''")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var a = new SponsorAdmin {
						Id = rs.GetInt32(0)
					};
					return a;
				}
			}
			return null;
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorAdminID, string usr)
		{
			string query = string.Format(
				@"
SELECT SponsorAdminID FROM SponsorAdmin WHERE SponsorID = {0} AND Usr = '{1}'",
				sponsorAdminID,
				usr.Replace("'", "")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var a = new SponsorAdmin {
						Id = rs.GetInt32(0)
					};
					return a;
				}
			}
			return null;
		}
		
		public SponsorAdmin ReadSponsorAdmin(string SKEY, string SAKEY, string SA, string SAID, string ANV, string LOS)
		{
			string s1 = (SKEY == null ? "sa.SponsorAdminID, " : "-1, ");
			string s3 = (SKEY == null ? "sa.Anonymized, " : "NULL, ");
			string s4 = (SKEY == null ? "sa.SeeUsers, " : (SA != null ? "sas.SeeUsers, " : "1, "));
			string s6 = (SKEY == null ? "sa.ReadOnly, " : "NULL, ");
			string s7 = (SKEY == null ? "ISNULL(sa.Name,sa.Usr) " : "'Internal administrator' ");
			string j = (
				ANV != null && LOS != null || SAKEY != null ?
				"INNER JOIN SponsorAdmin sa ON sa.SponsorID = s.SponsorID " + (
					SAKEY != null ?
					"WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),sa.SponsorAdminKey),'-',''),8) = '" + SAKEY.Substring(0, 8).Replace("'", "") + "' " +
					"AND s.SponsorID = " + SAKEY.Substring(8).Replace("'", "")
					:
					"WHERE sa.Usr = '" + ANV.Replace("'", "") + "' " +
					"AND sa.Pas = '" + LOS.Replace("'", "") + "'")
				: (
					SA != null ?
					"INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID AND sas.SuperAdminID = " + Convert.ToInt32(SAID) + " "
					:
					""
				) +
				"WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),s.SponsorKey),'-',''),8) = '" + SKEY.Substring(0, 8).Replace("'", "") + "' " +
				"AND s.SponsorID = " + SKEY.Substring(8).Replace("'", "")
			);
//			string j = ANV != null && LOS != null || SAKEY != null
//				? string.Format(
//					@"INNER JOIN SponsorAdmin sa ON sa.SponsorID = s.SponsorID {0}",
//					SAKEY != null
//					? string.Format(
//						@"WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),sa.SponsorAdminKey),'-',''),8) = '{0}' AND s.SponsorID = {1}",
//						SAKEY.Substring(0, 8).Replace("'", ""),
//						SAKEY.Substring(8).Replace("'", "")
//					)
//					: string.Format(
//						@"WHERE sa.Usr = '{0}' AND sa.Pas = '{1}'",
//						ANV.Replace("'", ""),
//						LOS.Replace("'", "")
//					)
//				)
//				: string.Format(
//					@"{0}WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),s.SponsorKey),'-',''),8) = '{1}' AND s.SponsorID = {2}",
//					SA != null
//					? string.Format(
//						@"INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID AND sas.SuperAdminID = {0} ",
//						Convert.ToInt32(SAID)
//					)
//					: "",
//					SKEY.Substring(0, 8).Replace("'", ""),
//					SKEY.Substring(8).Replace("'", "")
//				);
//			string j = "";
//			if (ANV != null && LOS != null || SAKEY != null) {
//				j += "INNER JOIN SponsorAdmin sa ON sa.SponsorID = s.SponsorID ";
//				if (SAKEY != null) {
//					j += string.Format(
//						@"WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),sa.SponsorAdminKey),'-',''),8) = '{0}' AND s.SponsorID = {1}",
//						SAKEY.Substring(0, 8).Replace("'", ""),
//						SAKEY.Substring(8).Replace("'", "")
//					);
//				} else {
//					string.Format(
//						@"WHERE sa.Usr = '{0}' AND sa.Pas = '{1}'",
//						ANV.Replace("'", ""),
//						LOS.Replace("'", "")
//					);
//				}
//			} else {
//				if (SA != null) {
//					j += string.Format(
//						@"INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID AND sas.SuperAdminID = {0} ",
//						Convert.ToInt32(SAID)
//					);
//				}
//				j += string.Format(
//					@"WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),s.SponsorKey),'-',''),8) = '{1}' AND s.SponsorID = {2}",
//					SKEY.Substring(0, 8).Replace("'", ""),
//					SKEY.Substring(8).Replace("'", "")
//				);
//			}
			string u = "";
			if (SKEY == null && SAKEY == null) {
				u = string.Format(
					@"
UNION ALL
SELECT NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	sa.SuperAdminID,
	NULL,
	sa.Username
FROM SuperAdmin sa
WHERE sa.Username = '{0}'
AND sa.Password = '{1}'",
					ANV.Replace("'", ""),
					LOS.Replace("'", "")
				);
			}
			string query = string.Format(
				@"
SELECT s.SponsorID,
	{0}
	s.Sponsor,
	{1}
	{2}
	NULL,
	{3}
	{4}
FROM Sponsor s
{5}
{6}",
				s1,
				s3,
				s4,
				s6,
				s7,
				j,
				u
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var a = new SponsorAdmin {
						Id = GetInt32(rs, 1),
						Sponsor = new Sponsor { Id = GetInt32(rs, 0), Name = GetString(rs, 2) },
						Anonymized = GetInt32(rs, 3) == 1, //GetBoolean(rs, 3),
						SeeUsers = GetInt32(rs, 4) == 1, //GetBoolean(rs, 4),
//						SuperAdmin = GetInt32(rs, 5) != 0, // FIXME: Is this really boolean?
						SuperAdminId = GetInt32(rs, 5),
						ReadOnly = GetInt32(rs, 6) == 1, //GetBoolean(rs, 6),
						Name = GetString(rs, 7)
					};
					return a;
				}
			}
			return null;
		}
		
		public SponsorProjectRoundUnit ReadSponsorProjectRoundUnit(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT spru.ProjectRoundUnitID,
	spru.SurveyID
FROM SponsorProjectRoundUnit spru
WHERE spru.SponsorID = {0}",
				sponsorID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var u = new SponsorProjectRoundUnit {
						Id = rs.GetInt32(0),
						Survey = new Survey { Id = rs.GetInt32(1) }
					};
					return u;
				}
			}
			return null;
		}
		
		public IList<SponsorInviteBackgroundQuestion> FindInviteBackgroundQuestionsByUser(int userID)
		{
			string query = string.Format(
				@"
SELECT s.BQID,
	s.BAID,
	BQ.Type,
	s.ValueInt,
	s.ValueDate,
	s.ValueText,
	BQ.Restricted
FROM SponsorInviteBQ s
INNER JOIN BQ ON BQ.BQID = s.BQID
WHERE s.SponsorInviteID = " + userID
			);
			var invites = new List<SponsorInviteBackgroundQuestion>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var i = new SponsorInviteBackgroundQuestion {
						Question = new BackgroundQuestion { Id = rs.GetInt32(0), Type = rs.GetInt32(2), Restricted = rs.GetInt32(6) },
						Answer = new BackgroundAnswer { Id = rs.GetInt32(1) },
						ValueInt = rs.GetInt32(3),
						ValueDate = GetDateTime(rs, 4),
						ValueText = GetString(rs, 5)
					};
					invites.Add(i);
				}
			}
			return invites;
		}
		
		public IList<SponsorInvite> FindInvitesBySponsor(int sponsorID, int sponsorAdminID)
		{
			string j = sponsorAdminID != -1
				? string.Format(@"INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT DISTINCT si.SponsorInviteID,
	si.Email,
	LEFT(REPLACE(CONVERT(VARCHAR(255),si.InvitationKey),'-',''),8)
FROM SponsorInvite si
{1}si.SponsorID = {0}
AND si.UserID IS NULL
AND si.StoppedReason IS NULL
AND si.Sent IS NULL",
				sponsorID,
				j
			);
			var invites = new List<SponsorInvite>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var i = new SponsorInvite {
						Id = rs.GetInt32(0),
						Email = rs.GetString(1),
						InvitationKey = rs.GetString(2)
					};
					invites.Add(i);
				}
			}
			return invites;
		}
		
		public IList<SponsorInvite> FindSentInvitesBySponsor(int sponsorID, int sponsorAdminID)
		{
			string j = sponsorAdminID != -1
				? string.Format("INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT DISTINCT si.SponsorInviteID,
	si.Email,
	LEFT(REPLACE(CONVERT(VARCHAR(255),si.InvitationKey),'-',''),8)
FROM SponsorInvite si
{1}si.SponsorID = {0}
AND si.UserID IS NULL
AND si.StoppedReason IS NULL
AND si.Sent IS NOT NULL
AND DATEADD(hh,1,si.Sent) < GETDATE()",
				sponsorID,
				j
			);
			var invites = new List<SponsorInvite>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var i = new SponsorInvite {
						Id = rs.GetInt32(0),
						Email = rs.GetString(1),
						InvitationKey = rs.GetString(2)
					};
					invites.Add(i);
				}
			}
			return invites;
		}
		
		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySponsorAdmin(int sponsorID, int sponsorAdminID)
		{
			string w = sponsorAdminID != -1
				? string.Format(
					@"INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID
WHERE sad.SponsorAdminID = {0}
AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT ses.ProjectRoundID,
	ses.EmailSubject,
	ses.EmailBody,
	ses.EmailLastSent,
	ses.Internal,
	ses.SponsorExtendedSurveyID,
	ses.FinishedEmailSubject,
	ses.FinishedEmailBody,
	ses.RoundText
FROM SponsorExtendedSurvey ses
INNER JOIN Sponsor s ON ses.SponsorID = s.SponsorID
INNER JOIN Department d ON s.SponsorID = d.SponsorID
LEFT OUTER JOIN SponsorExtendedSurveyDepartment dd ON dd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID
	AND dd.DepartmentID = d.DepartmentID
{1} ses.SponsorID = {0}
AND dd.Hide IS NULL
ORDER BY ses.SponsorExtendedSurveyID DESC",
				sponsorID,
				w
			);
			var surveys = new List<SponsorExtendedSurvey>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new SponsorExtendedSurvey {
						ProjectRound = new ProjectRound { Id = rs.GetInt32(0) },
//						EmailSubject = rs.GetString(1),
//						EmailBody = rs.GetString(2),
//						EmailLastSent = rs.GetDateTime(3),
//						Internal = rs.GetString(4),
						EmailSubject = GetString(rs, 1),
						EmailBody = GetString(rs, 2),
						EmailLastSent = GetDateTime(rs, 3),
						Internal = GetString(rs, 4),
						Id = rs.GetInt32(5),
//						FinishedEmailSubject = rs.GetString(6),
//						FinishedEmailBody = rs.GetString(7),
//						RoundText = rs.GetString(8)
						FinishedEmailSubject = GetString(rs, 6),
						FinishedEmailBody = GetString(rs, 7),
						RoundText = GetString(rs, 8)
					};
					surveys.Add(s);
				}
			}
			return surveys;
		}
		
		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySponsor(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT ses.ProjectRoundID,
	ses.Internal,
	ses.RoundText,
	ses.IndividualFeedbackEmailSubject,
	ses.IndividualFeedbackEmailBody
FROM SponsorExtendedSurvey ses
WHERE ses.SponsorID = {0}
ORDER BY ses.SponsorExtendedSurveyID",
				sponsorID
			);
			var surveys = new List<SponsorExtendedSurvey>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new SponsorExtendedSurvey {
						ProjectRound = new ProjectRound { Id = rs.GetInt32(0) },
						Internal = rs.GetString(1),
						RoundText = rs.GetString(2),
						IndividualFeedbackEmailSubject = rs.GetString(3),
						IndividualFeedbackEmailBody = rs.GetString(4)
					};
					surveys.Add(s);
				}
			}
			return surveys;
		}
		
		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySuperAdmin2(int superAdminID)
		{
			string query = string.Format(
				@"
SELECT s.Sponsor,
	ses.ProjectRoundID,
	ses.Internal,
	ses.RoundText,
	ss.SurveyID,
	ss.Internal
FROM Sponsor s
INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorID = s.SponsorID
INNER JOIN eform..ProjectRound r ON ses.ProjectRoundID = r.ProjectRoundID
INNER JOIN eform..Survey ss ON r.SurveyID = ss.SurveyID
INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID
WHERE sas.SuperAdminID =  {0}
ORDER BY s.Sponsor, ses.Internal, ses.RoundText",
				superAdminID
			);
			var surveys = new List<SponsorExtendedSurvey>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new SponsorExtendedSurvey {
						Sponsor = new Sponsor { Name = GetString(rs, 0) },
						ProjectRound = new ProjectRound {
							Id = GetInt32(rs, 1),
							Survey = new Survey {
								Id = GetInt32(rs, 4),
								Internal = GetString(rs, 5)
							}
						},
						Internal = GetString(rs, 2),
						RoundText = GetString(rs, 3)
					};
					surveys.Add(s);
				}
			}
			return surveys;
			
		}
		
		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySuperAdmin(int superAdminID)
		{
			string query = string.Format(
				@"
SELECT s.Sponsor,
	ses.ProjectRoundID,
	ses.Internal,
	ses.RoundText,
	ss.SurveyID,
	ss.Internal,
	(SELECT COUNT(*) FROM eform..Answer a WHERE a.ProjectRoundID = r.ProjectRoundID AND a.EndDT IS NOT NULL) AS CX
FROM Sponsor s
INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorID = s.SponsorID
INNER JOIN eform..ProjectRound r ON ses.ProjectRoundID = r.ProjectRoundID
INNER JOIN eform..Survey ss ON r.SurveyID = ss.SurveyID
INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID
WHERE s.Deleted IS NULL AND sas.SuperAdminID = {0}
ORDER BY s.Sponsor, ses.Internal, ses.RoundText",
				superAdminID
			);
			var surveys = new List<SponsorExtendedSurvey>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new SponsorExtendedSurvey {
						Sponsor = new Sponsor { Name = GetString(rs, 0) },
						ProjectRound = new ProjectRound {
							Id = GetInt32(rs, 1),
							Survey = new Survey {
								Id = GetInt32(rs, 4),
								Internal = GetString(rs, 5)
							},
							Answers = new List<Answer>(GetInt32(rs, 6))
						},
						Internal = rs.GetString(2),
						RoundText = rs.GetString(3)
					};
					surveys.Add(s);
				}
			}
			return surveys;
		}
		
		public IList<SponsorAdminDepartment> FindAdminDepartmentBySponsorAdmin(int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT DepartmentID
FROM SponsorAdminDepartment
WHERE SponsorAdminID = {0}",
				sponsorAdminID
			);
			var departments = new List<SponsorAdminDepartment>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d =  new SponsorAdminDepartment {
						Department = new Department { Id = rs.GetInt32(0) }
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<SuperAdminSponsor> FindSuperAdminSponsors(int superAdminID)
		{
			string query = string.Format(
				@"
SELECT s.SponsorID,
	s.Sponsor,
	LEFT(REPLACE(CONVERT(VARCHAR(255),s.SponsorKey),'-',''),8),
	(SELECT COUNT(*) FROM SponsorExtendedSurvey ses WHERE ses.SponsorID = s.SponsorID),
	(SELECT COUNT(*) FROM SponsorInvite si WHERE si.Sent IS NOT NULL AND si.SponsorID = s.SponsorID),
	(SELECT COUNT(*) FROM SponsorInvite si INNER JOIN [User] u ON si.UserID = u.UserID WHERE si.SponsorID = s.SponsorID),
	(SELECT MIN(si.Sent) FROM SponsorInvite si WHERE si.SponsorID = s.SponsorID),
	sas.SeeUsers,
	(SELECT COUNT(*) FROM SponsorInvite si WHERE si.SponsorID = s.SponsorID),
	s.Closed
FROM Sponsor s
INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID
WHERE s.Deleted IS NULL AND sas.SuperAdminID = {0}
ORDER BY s.Sponsor",
				superAdminID
			);
			var admins = new List<SuperAdminSponsor>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var a =  new SuperAdminSponsor {
						Sponsor = new Sponsor {
							Id = GetInt32(rs, 0),
							Name = GetString(rs, 1),
							SponsorKey = GetString(rs, 2),
							ExtendedSurveys = new List<SponsorExtendedSurvey>(GetInt32(rs, 3)),
							SentInvites = new List<SponsorInvite>(GetInt32(rs, 4)),
							ActiveInvites = new List<SponsorInvite>(GetInt32(rs, 5)),
							MinimumInviteDate = GetDateTime(rs, 6),
							Invites = new List<SponsorInvite>(GetInt32(rs, 8)),
							ClosedAt = GetDateTime(rs, 9)
						},
						SeeUsers = GetBoolean(rs, 7)
					};
					admins.Add(a);
				}
			}
			return admins;
		}
		
		public IList<SponsorAdminFunction> FindAdminFunctionBySponsorAdmin(int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT ManagerFunctionID
FROM SponsorAdminFunction
WHERE SponsorAdminID = {0}",
				sponsorAdminID
			);
			var functions = new List<SponsorAdminFunction>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var f =  new SponsorAdminFunction {
						Function = new ManagerFunction { Id = rs.GetInt32(0) }
					};
					functions.Add(f);
				}
			}
			return functions;
		}
		
		public IList<SponsorProjectRoundUnit> FindSponsorProjectRoundUnits(int superAdminID)
		{
			string query = string.Format(
				@"
SELECT s.Sponsor,
	ses.ProjectRoundUnitID,
	ISNULL(r.SurveyID, ss.SurveyID),
	ss.Internal
FROM Sponsor s
INNER JOIN SponsorProjectRoundUnit ses ON ses.SponsorID = s.SponsorID
INNER JOIN eform..ProjectRoundUnit r ON ses.ProjectRoundUnitID = r.ProjectRoundUnitID
INNER JOIN eform..ProjectRound rr ON r.ProjectRoundID = rr.ProjectRoundID
INNER JOIN eform..Survey ss ON ISNULL(r.SurveyID, ss.SurveyID) = ss.SurveyID
INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID
WHERE sas.SuperAdminID = {0}
ORDER BY s.Sponsor, ses.Nav",
				superAdminID
			);
			var units = new List<SponsorProjectRoundUnit>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var u = new SponsorProjectRoundUnit {
						Sponsor = new Sponsor { Name = GetString(rs, 0) },
						ProjectRoundUnit = new ProjectRoundUnit {
							Id = GetInt32(rs, 1),
							Survey = new Survey {
								Id = GetInt32(rs, 2),
								Internal = GetString(rs, 3)
							}
						},
					};
					units.Add(u);
				}
			}
			return units;
		}
		
		public IList<SponsorAdmin> FindAdminBySponsor(int sponsorID, int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT sa.SponsorAdminID,
	sa.Usr,
	sa.Name,
	sa.ReadOnly
FROM SponsorAdmin sa
WHERE (sa.SponsorAdminID <> {1} OR sa.SuperUser = 1)
{2}
AND sa.SponsorID = {0}",
				sponsorID,
				sponsorAdminID,
				sponsorAdminID != -1 ? "AND ((SELECT COUNT(*) FROM SponsorAdminDepartment sad WHERE sad.SponsorAdminID = sa.SponsorAdminID) = 0 OR (SELECT COUNT(*) FROM SponsorAdminDepartment sad INNER JOIN SponsorAdminDepartment sad2 ON sad.DepartmentID = sad2.DepartmentID WHERE sad.SponsorAdminID = sa.SponsorAdminID AND sad2.SponsorAdminID = " + sponsorAdminID + ") > 0) " : ""
			);
			var admins = new List<SponsorAdmin>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var a = new SponsorAdmin {
						Id = rs.GetInt32(0),
						Usr = rs.GetString(1),
						Name = rs.GetString(2),
						ReadOnly = GetInt32(rs, 3) == 1
					};
					admins.Add(a);
				}
			}
			return admins;
		}
		
		public IList<SponsorBackgroundQuestion> FindBySponsor(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT sbq.BQID,
	BQ.Internal
FROM SponsorBQ sbq
INNER JOIN BQ ON BQ.BQID = sbq.BQID
WHERE (BQ.Comparison = 1 OR sbq.Hidden = 1)
AND BQ.Type IN (1, 7)
AND sbq.SponsorID = {0}",
				sponsorID
			);
			var sponsors = new List<SponsorBackgroundQuestion>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new SponsorBackgroundQuestion();
					s.Id = rs.GetInt32(0);
					s.BackgroundQuestion = new BackgroundQuestion { Internal = rs.GetString(1) };
					sponsors.Add(s);
				}
			}
			return sponsors;
		}
		
		public IList<SponsorBackgroundQuestion> FindBackgroundQuestions(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT s.BQID,
	b.Type
FROM SponsorBQ s
INNER JOIN BQ b ON s.BQID = b.BQID
WHERE s.Hidden = 1 AND s.SponsorID = {0}
ORDER BY s.SortOrder",
				sponsorID
			);
			var questions = new List<SponsorBackgroundQuestion>();
			using (SqlDataReader rs = Db2.rs(query)) {
				while (rs.Read()) {
					var q = new SponsorBackgroundQuestion {
						Id = rs.GetInt32(0),
						BackgroundQuestion = new BackgroundQuestion { Id = rs.GetInt32(1) }
					};
					questions.Add(q);
				}
			}
			return questions;
		}
		
		public IList<SponsorProjectRoundUnit> FindBySponsorAndLanguage(int sponsorID, int langID)
		{
			string query = string.Format(
				@"
SELECT ISNULL(sprul.Nav, '?'),
	spru.ProjectRoundUnitID
FROM SponsorProjectRoundUnit spru
LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID
WHERE spru.SponsorID = {0}
AND ISNULL(sprul.LangID, 1) = {1}",
				sponsorID,
				langID
			);
			var projects = new List<SponsorProjectRoundUnit>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var p = new SponsorProjectRoundUnit();
					p.Navigation = rs.GetString(0);
					p.ProjectRoundUnit = new ProjectRoundUnit { Id = rs.GetInt32(1) };
					projects.Add(p);
				}
			}
			return projects;
		}
		
		public IList<SponsorProjectRoundUnit> FindDistinctRoundUnitsWithReportBySuperAdmin(int superAdminID)
		{
			string query = string.Format(
				@"
SELECT DISTINCT s.Sponsor,
	ses.ProjectRoundUnitID,
	ses.Nav,
	rep.ReportID,
	rep.Internal,
	(
		SELECT COUNT(DISTINCT a.ProjectRoundUserID)
		FROM eform..Answer a
		WHERE a.ProjectRoundUnitID = r.ProjectRoundUnitID AND a.EndDT >= '{1}' AND a.EndDT < '{2}'
	) AS CX
FROM Sponsor s
INNER JOIN SponsorProjectRoundUnit ses ON ses.SponsorID = s.SponsorID
INNER JOIN eform..ProjectRoundUnit r ON ses.ProjectRoundUnitID = r.ProjectRoundUnitID
INNER JOIN eform..Report rep ON rep.ReportID = r.ReportID
INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID
WHERE s.Deleted IS NULL AND sas.SuperAdminID = {0}
ORDER BY s.Sponsor, ses.Nav",
				superAdminID,
				DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01"),
				DateTime.Now.ToString("yyyy-MM-01")
			);
			var units = new List<SponsorProjectRoundUnit>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var u = new SponsorProjectRoundUnit {
						Sponsor = new Sponsor { Name = rs.GetString(0) },
						ProjectRoundUnit = new ProjectRoundUnit {
							Id = rs.GetInt32(1),
							Report = new Report {
								Id = rs.GetInt32(3),
								Internal = rs.GetString(4)
							},
							Answers = new List<Answer>(rs.GetInt32(5))
						},
						Navigation = rs.GetString(2)
					};
					units.Add(u);
				}
			}
			return units;
		}
		
		public IList<Sponsor> FindAndCountDetailsBySuperAdmin(int superAdminID)
		{
			string query = string.Format(
				@"
SELECT s.SponsorID,
	s.Sponsor,
	LEFT(REPLACE(CONVERT(VARCHAR(255), s.SponsorKey), '-', ''), 8),
	(SELECT COUNT(*) FROM SponsorExtendedSurvey AS ses WHERE (SponsorID = s.SponsorID)),
	(SELECT COUNT(*) FROM SponsorInvite AS si WHERE (Sent IS NOT NULL) AND (SponsorID = s.SponsorID)),
	(SELECT COUNT(*) FROM SponsorInvite AS si INNER JOIN [User] AS u ON si.UserID = u.UserID WHERE (si.SponsorID = s.SponsorID)),
	(SELECT MIN(Sent) FROM SponsorInvite AS si WHERE (SponsorID = s.SponsorID)),
	sas.SeeUsers,
	(SELECT COUNT(*) FROM SponsorInvite AS si WHERE (SponsorID = s.SponsorID)),
	s.Closed
FROM Sponsor AS s
INNER JOIN SuperAdminSponsor AS sas ON s.SponsorID = sas.SponsorID
WHERE (sas.SuperAdminID = {0}) AND (s.Deleted IS NULL)
ORDER BY s.Sponsor",
				superAdminID
			);
			var sponsors = new List<Sponsor>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new Sponsor {
						Id = rs.GetInt32(0),
						Name = rs.GetString(1),
						SponsorKey = rs.GetString(2),
						ClosedAt = GetDateTime(rs, 9),
						MinimumInviteDate = GetDateTime(rs, 6)
					};
					s.ExtendedSurveys = new List<SponsorExtendedSurvey>(GetInt32(rs, 3));
					s.SentInvites = new List<SponsorInvite>(GetInt32(rs, 4));
					s.ActiveInvites = new List<SponsorInvite>(GetInt32(rs, 5));
					s.SuperAdminSponsors = new List<SuperAdminSponsor>(
						new SuperAdminSponsor[] {
							new SuperAdminSponsor { SeeUsers = GetInt32(rs, 7) == 1 }
						}
					);
					s.Invites = new List<SponsorInvite>(GetInt32(rs, 8));
					sponsors.Add(s);
				}
			}
			return sponsors;
		}
		
		public IList<Sponsor> Y(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT DISTINCT s.Sponsor,
	ses.ProjectRoundUnitID,
	ses.Nav,
	rep.ReportID,
	rep.Internal,
	(
		SELECT COUNT(DISTINCT a.ProjectRoundUserID)
		FROM eform.dbo.Answer AS a
		WHERE (a.ProjectRoundUnitID = r.ProjectRoundUnitID)
			AND (a.EndDT >= '{1}')
			AND (a.EndDT < '{2}')
	) AS CX
FROM Sponsor AS s
INNER JOIN SponsorProjectRoundUnit AS ses ON ses.SponsorID = s.SponsorID
INNER JOIN eform.dbo.ProjectRoundUnit AS r ON ses.ProjectRoundUnitID = r.ProjectRoundUnitID
INNER JOIN eform.dbo.Report AS rep ON rep.ReportID = r.ReportID
INNER JOIN SuperAdminSponsor AS sas ON s.SponsorID = sas.SponsorID
WHERE (s.Deleted IS NULL) AND (sas.SuperAdminID = {0})
ORDER BY s.Sponsor, ses.Nav",
				sponsorID,
				DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01"),
				DateTime.Now.ToString("yyyy-MM-01")
			);
			var sponsors = new List<Sponsor>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new Sponsor();
					s.Name = rs.GetString(0);
					s.ProjectRoundUnit = new ProjectRoundUnit {
						Report = new Report { Id = rs.GetInt32(3), Internal = rs.GetString(4) }
					};
				}
			}
			return sponsors;
		}
		
		public void SaveOrUpdate(Sponsor t)
		{
			throw new NotImplementedException();
		}
		
		public void Delete(Sponsor t)
		{
			throw new NotImplementedException();
		}
		
		Sponsor IBaseRepository<Sponsor>.Read(int id)
		{
			throw new NotImplementedException();
		}
		
		IList<Sponsor> IBaseRepository<Sponsor>.FindAll()
		{
			throw new NotImplementedException();
		}
		
		public SponsorBackgroundQuestion ReadSponsorBackgroundQuestion(int sponsorBQID)
		{
			throw new NotImplementedException();
		}
	}
}
