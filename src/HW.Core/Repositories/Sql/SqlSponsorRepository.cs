using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlSponsorRepository : BaseSqlRepository<Sponsor>//, ISponsorRepository
	{
		public void UpdateSponsorLastLoginSent(int sponsorId)
		{
			string query = string.Format(
				@"
UPDATE Sponsor SET LoginLastSent = GETDATE()
WHERE SponsorID = {0}",
				sponsorId
			);
			Db.exec(query, "healthWatchSqlConnection");
		}

		public void UpdateLastAllMessageSent(int sponsorId)
		{
			string query = string.Format(
				@"
UPDATE Sponsor SET AllMessageLastSent = GETDATE()
WHERE SponsorID = {0}",
				sponsorId
			);
			Db.exec(query, "healthWatchSqlConnection");
		}

		public void UpdateSponsorLastInviteReminderSent(int sponsorId)
		{
			string query = string.Format(
				@"
UPDATE Sponsor SET InviteReminderLastSent = GETDATE()
WHERE SponsorID = {0}",
				sponsorId
			);
			Db.exec(query, "healthWatchSqlConnection"); // TODO: move to department???
		}

		public void UpdateSponsorLastInviteSent(int sponsorId)
		{
			string query = string.Format(
				@"
UPDATE Sponsor SET InviteLastSent = GETDATE()
WHERE SponsorID = {0}",
				sponsorId
			);
			Db.exec(query, "healthWatchSqlConnection"); // TODO: move to department???
		}
		
//		public void UpdateSponsorInviteSent(int userId, int sponsorInviteId)
//		{
//			string query = string.Format(
//				@"
		//UPDATE SponsorInvite SET UserID = {0}, Sent = GETDATE()
		//WHERE SponsorInviteID = {1}",
//				userId,
//				sponsorInviteId
//			);
//			Db.exec(query, "healthWatchSqlConnection");
//		}

//		public void UpdateNullUserForUserInvite(int userId)
//		{
//			string query = string.Format(
//				@"
		//UPDATE SponsorInvite SET UserID = NULL
		//WHERE UserID = {0}",
//				userId
//			);
//			Db.exec(query, "healthWatchSqlConnection");
//		}

		public void UpdateExtendedSurveyLastEmailSent(int sponsorExtendedSurveyId)
		{
			string query = string.Format(
				@"
UPDATE SponsorExtendedSurvey SET EmailLastSent = GETDATE()
WHERE SponsorExtendedSurveyID = {0}",
				sponsorExtendedSurveyId
			);
			Db.exec(query, "healthWatchSqlConnection");
		}

		public void UpdateExtendedSurveyLastFinishedSent(int sponsorExtendedSurveyId)
		{
			string query = string.Format(
				@"
UPDATE SponsorExtendedSurvey SET FinishedLastSent = GETDATE()
WHERE SponsorExtendedSurveyID = {0}",
				sponsorExtendedSurveyId
			);
			Db.exec(query, "healthWatchSqlConnection");
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
		
		public SponsorInvite SaveSponsorInvite(int sponsorID, string unit, string email, DateTime stopped, int stoppedReason)
		{
			string query = string.Format(
				@"
SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
BEGIN TRAN;
INSERT INTO SponsorInvite (SponsorID,DepartmentID,Email,Stopped, StoppedReason )
VALUES ({0},{1},'{2}',{3},{4});
SELECT SponsorInviteID FROM [SponsorInvite] WHERE SponsorID={0} AND Email = '{2}' ORDER BY SponsorInviteID DESC;
COMMIT;",
				sponsorID,
				unit,
				email,
				(stopped != DateTime.MinValue ? "'" + stopped.ToString("yyyy-MM-dd") + "'" : "NULL"),
				(stoppedReason != 0 ? stoppedReason.ToString() : "NULL")
			);
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					return new SponsorInvite {
						Id = GetInt32(rs, 0)
					};
				}
			}
			return null;
		}
		
		public void Save(int sponsorInviteID, string bqID, int baID)
		{
			string query = string.Format(
				@"
INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,BAID)
VALUES ({0},{1},{2})",
				sponsorInviteID,
				bqID,
				baID
			);
			Db.exec(query);
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

		public void DeleteSponsorAdminFunction(int sponsorAdminId)
		{
			Db.exec("DELETE FROM SponsorAdminFunction WHERE SponsorAdminID = " + sponsorAdminId,
			        "healthWatchSqlConnection");
		}
		
		public void UpdatePreviewExtendedSurveys(string flip, int pessiid)
		{
			string query = string.Format(
				@"
UPDATE SponsorInvite SET PreviewExtendedSurveys = {0}
WHERE SponsorInviteID = {1}",
				flip == "1" ? "1" : "NULL",
				pessiid
			);
			Db.exec(query);
		}

		public void UpdateDeletedAdmin(int sponsorId, int sponsorAdminId)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdmin
	SET SponsorID = -ABS(SponsorID), Usr = Usr + 'DELETED'
WHERE SponsorAdminID = {1} AND SponsorID = {0}",
				sponsorId,
				sponsorAdminId
			);
			Db.exec(query, "healthWatchSqlConnection");
		}

		public int CountSentInvitesBySponsor(int sponsorId, DateTime dateSent)
		{
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM SponsorInvite AS si
LEFT OUTER JOIN [User] AS u ON si.UserID = u.UserID
WHERE (si.SponsorID = {0})
AND (ISNULL(u.Created, si.Sent) < '{1}')
OR (si.SponsorID = {0}) AND (si.Sent < '{1}')",
				sponsorId,
				dateSent.ToString("yyyy-MM-dd")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}

		public int CountCreatedInvitesBySponsor(int sponsorId, DateTime dateCreated)
		{
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM SponsorInvite si
INNER JOIN [User] u ON si.UserID = u.UserID
WHERE si.SponsorID = {0} AND u.Created < '{1}'",
				sponsorId,
				dateCreated.ToString("yyyy-MM-dd")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}

		public SponsorInvite ReadSponsorInviteByUser(int userId)
		{
			string query = string.Format(
				@"
SELECT Email,
	DepartmentID,
	StoppedReason,
	Stopped,
	UserID
FROM SponsorInvite
WHERE SponsorInviteID = " + userId
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var i = new SponsorInvite {
						Email = GetString(rs, 0),
						Department = new Department {Id = GetInt32(rs, 1)},
						StoppedReason = GetInt32(rs, 2),
						Stopped = GetDateTime(rs, 3),
						User = new User {Id = GetInt32(rs, 4)}
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
	s.LoginSubject,
	s.SponsorID
FROM Sponsor s
INNER JOIN SponsorInvite si ON s.SponsorID = si.SponsorID
LEFT OUTER JOIN [User] u ON u.UserID = si.UserID
WHERE s.SponsorID = {0} AND si.SponsorInviteID = {1}",
				sponsorID,
				inviteID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var i = new SponsorInvite {
						Email = GetString(rs, 2),
						InvitationKey = GetString(rs, 3),
						User = rs.IsDBNull(4) ? null
							: new User {
							Id = GetInt32(rs, 4),
							ReminderLink = GetInt32(rs, 5),
							UserKey = GetString(rs, 6)
						},
						Sponsor = new Sponsor {
							InviteText = GetString(rs, 0),
							InviteSubject = GetString(rs, 1),
							LoginText = GetString(rs, 7),
							LoginSubject = GetString(rs, 8),
							Id = GetInt32(rs, 9)
						}
					};
					return i;
				}
			}
			return null;
		}
		
		public SponsorInvite ReadSponsorInvite(int sponsorID, int sponsorInviteID)
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
WHERE s.SponsorID = {0} AND si.SponsorInviteID = {1}",
				sponsorID,
				sponsorInviteID
			);
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					var s = new Sponsor { InviteText = GetString(rs, 0), InviteSubject = GetString(rs, 1), LoginText = GetString(rs, 7), LoginSubject = GetString(rs, 8) };
					return new SponsorInvite {
						Sponsor = s,
						Email = GetString(rs, 2),
						InvitationKey = GetString(rs, 3),
						User = rs.IsDBNull(4) ? null : new User { Id = GetInt32(rs, 4), ReminderLink = GetInt32(rs, 5), UserKey = GetString(rs, 6) }
					};
				}
			}
			return null;
		}
		
		public SponsorInvite ReadSponsorInvite2(int sponsorInviteID)
		{
			string query = string.Format(
				@"
SELECT Email,
	DepartmentID,
	StoppedReason,
	Stopped
FROM SponsorInvite
WHERE SponsorInviteID = {0}",
				sponsorInviteID
			);
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					return new SponsorInvite {
						Email = GetString(rs, 0),
						Department = rs.IsDBNull(1) ? null : new Department { Id = GetInt32(rs, 1) },
						StoppedReason = GetInt32(rs, 2),
						Stopped = GetDateTime(rs, 3)
					};
				}
			}
			return null;
		}

		public SponsorInvite ReadSponsorInvite(int sponsorInviteId)
		{
			string query = string.Format(
				@"
SELECT SponsorID,
	Email,
	DepartmentID,
FROM SponsorInvite
WHERE UserID IS NULL
WHERE SponsorInviteID = {0}",
				sponsorInviteId
			);
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					var i = new SponsorInvite {
						Sponsor = new Sponsor { Id = GetInt32(rs, 0) },
						Email = GetString(rs, 1),
						Department = rs.IsDBNull(2) ? null : new Department { Id = GetInt32(rs, 2) }
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
SELECT SponsorInviteID,
	Stopped,
	StoppedReason
FROM SponsorInvite
WHERE Email = '{0}'
AND SponsorID = {1}",
				email,
				sponsorID
			);
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					var i = new SponsorInvite {
						Id = rs.GetInt32(0),
						Stopped = GetDateTime(rs, 1),
						StoppedReason = GetInt32(rs, 2)
					};
					return i;
				}
			}
			return null;
		}

		public SponsorInviteBackgroundQuestion ReadSponsorInviteBackgroundQuestion(int sponsorId, int userId, int bqId)
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
				sponsorId,
				userId,
				bqId
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var i = new SponsorInvite {
						User = new User {
							Profile = new UserProfile {Id = GetInt32(rs, 5)}
						}
					};
					var sib = new SponsorInviteBackgroundQuestion {
						BackgroundAnswer = new BackgroundAnswer {Id = rs.GetInt32(0)},
						ValueInt = GetInt32(rs, 1),
						ValueText = GetString(rs, 2),
						ValueDate = GetDateTime(rs, 3),
						BackgroundQuestion = new BackgroundQuestion {Type = GetInt32(rs, 4)},
						Invite = i
					};
					return sib;
				}
			}
			return null;
		}

		public Sponsor ReadSponsor2(int sponsorId)
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
				sponsorId
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var u = new SuperSponsor {
						Id = GetInt32(rs, 1),
						Languages = new List<SuperSponsorLanguage>(
							new [] {
								new SuperSponsorLanguage {Header = GetString(rs, 2)}
							}
						)
					};
					var s = new Sponsor {
						Name = GetString(rs, 0),
						SuperSponsor = u
					};
					return s;
				}
			}
			return null;
		}

		public Sponsor ReadSponsor(int sponsorId)
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
	s.AllMessageLastSent,
	s.Sponsor,
	s.Application,
	CAST(s.SponsorKey AS VARCHAR(64)),
	s.TreatmentOffer,
	s.TreatmentOfferText,
	s.TreatmentOfferEmail,
	s.TreatmentOfferIfNeededText,
	s.TreatmentOfferBQ,
	s.TreatmentOfferBQfn,
	s.TreatmentOfferBQmorethan,
	s.InfoText,
	s.ConsentText,
	s.SuperSponsorID,
	s.AlternativeTreatmentOfferText,
	s.AlternativeTreatmentOfferEmail,
	s.LID,
	s.MinUserCountToDisclose,
	s.ProjectRoundUnitID,
	s.EmailFrom
FROM Sponsor s
WHERE s.SponsorID = {0}",
				sponsorId
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
						Name = GetString(rs, 14),
						Application = GetString(rs, 15),
						SponsorKey = GetString(rs, 16),
						TreatmentOffer = GetInt32(rs, 17),
						TreatmentOfferText = GetString(rs, 18),
						TreatmentOfferEmail = GetString(rs, 19),
						TreatmentOfferIfNeededText = GetString(rs, 20),
						TreatmentOfferBQ = GetInt32(rs, 21),
						TreatmentOfferBQfn = GetInt32(rs, 22),
						TreatmentOfferBQmorethan = GetInt32(rs, 23),
						InfoText = GetString(rs, 24),
						ConsentText = GetString(rs, 25),
						SuperSponsor = new SuperSponsor {Id = GetInt32(rs, 26)},
						AlternativeTreatmentOfferText = GetString(rs, 27),
						AlternativeTreatmentOfferEmail = GetString(rs, 28),
						Language = new Language {Id = GetInt32(rs, 29)},
						MinUserCountToDisclose = GetInt32(rs, 30, 10),
						ProjectRoundUnit = new ProjectRoundUnit { Id = GetInt32(rs, 31) },
						EmailFrom = GetString(rs, 32, "", "info@healthwatch.se")
					};
					return s;
				}
			}
			return null;
		}

		public bool SponsorAdminExists(int sponsorAdminId, string usr)
		{
			string query = string.Format(
				@"
SELECT SponsorAdminID FROM SponsorAdmin WHERE Usr = '{0}' {1}",
				usr.Replace("'", ""),
				(sponsorAdminId != 0 ? " AND SponsorAdminID != " + sponsorAdminId : "")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return true;
				}
			}
			return false;
		}

		public SponsorAdmin ReadSponsorAdmin(int sponsorId, int sponsorAdminId, int said)
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
				sponsorId,
				sponsorAdminId,
				said
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

		public SponsorAdmin ReadSponsorAdmin(int sponsorAdminId)
		{
			string query = string.Format(
				@"
SELECT SuperUser FROM SponsorAdmin WHERE SponsorAdminID = {0}",
				sponsorAdminId
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

		public SponsorAdmin ReadSponsorAdmin(int sponsorId, int sponsorAdminId, string password)
		{
			string query = string.Format(
				@"
SELECT SponsorAdminID
FROM SponsorAdmin
WHERE SponsorID = {0}
AND SponsorAdminID = {1}
AND Pas = '{2}'",
				sponsorId,
				sponsorAdminId,
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

		public SponsorAdmin ReadSponsorAdmin(int sponsorAdminId, string usr)
		{
			string query = string.Format(
				@"
SELECT SponsorAdminID FROM SponsorAdmin WHERE SponsorID = {0} AND Usr = '{1}'",
				sponsorAdminId,
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

		public SponsorAdmin ReadSponsorAdmin(string skey, string sakey, string sa, string said, string anv, string los)
		{
			string s1 = (skey == null ? "sa.SponsorAdminID, " : "-1, ");
			string s3 = (skey == null ? "sa.Anonymized, " : "NULL, ");
			string s4 = (skey == null ? "sa.SeeUsers, " : (sa != null ? "sas.SeeUsers, " : "1, "));
			string s6 = (skey == null ? "sa.ReadOnly, " : "NULL, ");
			string s7 = (skey == null ? "ISNULL(sa.Name,sa.Usr) " : "'Internal administrator' ");
			string j = (
				anv != null && los != null || sakey != null
				? "INNER JOIN SponsorAdmin sa ON sa.SponsorID = s.SponsorID " + (
					sakey != null
					? "WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),sa.SponsorAdminKey),'-',''),8) = '" +
					sakey.Substring(0, 8).Replace("'", "") + "' " +
					"AND s.SponsorID = " + sakey.Substring(8).Replace("'", "")
					: "WHERE sa.Usr = '" + anv.Replace("'", "") + "' " +
					"AND sa.Pas = '" + los.Replace("'", "") + "'")
				: (
					sa != null
					? "INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID AND sas.SuperAdminID = " +
					Convert.ToInt32(said) + " "
					: ""
				) +
				"WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),s.SponsorKey),'-',''),8) = '" +
				skey.Substring(0, 8).Replace("'", "") + "' " +
				"AND s.SponsorID = " + skey.Substring(8).Replace("'", "")
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
			if (skey == null && sakey == null) {
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
					anv.Replace("'", ""),
					los.Replace("'", "")
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
						Sponsor = new Sponsor {Id = GetInt32(rs, 0), Name = GetString(rs, 2)},
						Anonymized = GetInt32(rs, 3) == 1, //GetBoolean(rs, 3),
						SeeUsers = GetInt32(rs, 4) == 1, //GetBoolean(rs, 4),
						SuperAdminId = GetInt32(rs, 5),
						ReadOnly = GetInt32(rs, 6) == 1, //GetBoolean(rs, 6),
						Name = GetString(rs, 7)
					};
					return a;
				}
			}
			return null;
		}

		public IList<SponsorProjectRoundUnit> FindSponsorProjectRoundUnitsBySponsor(int sponsorId)
		{
			string query = string.Format(
				@"
SELECT spru.ProjectRoundUnitID,
	spru.SurveyID
FROM SponsorProjectRoundUnit spru
WHERE spru.SponsorID = {0}",
				sponsorId
			);
			var units = new List<SponsorProjectRoundUnit>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var u = new SponsorProjectRoundUnit {
						ProjectRoundUnit = new ProjectRoundUnit { Id = GetInt32(rs, 0) },
						Survey = new Survey { Id = GetInt32(rs, 1) }
					};
					units.Add(u);
				}
			}
			return units;
		}

		public IList<SponsorInviteBackgroundQuestion> FindInviteBackgroundQuestionsByUser(int userId)
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
WHERE s.SponsorInviteID = " + userId
			);
			var invites = new List<SponsorInviteBackgroundQuestion>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var i = new SponsorInviteBackgroundQuestion {
						BackgroundQuestion = new BackgroundQuestion {
							Id = rs.GetInt32(0),
							Type = GetInt32(rs, 2),
							Restricted = GetInt32(rs, 6) == 1
						},
						BackgroundAnswer = rs.IsDBNull(1) ? null : new BackgroundAnswer { Id = rs.GetInt32(1) },
						ValueInt = GetInt32(rs, 3),
						ValueDate = GetDateTime(rs, 4),
						ValueText = GetString(rs, 5)
					};
					invites.Add(i);
				}
			}
			return invites;
		}

		public IList<SponsorInvite> FindInvitesBySponsor(int sponsorId, int sponsorAdminId)
		{
			string j = sponsorAdminId != -1
				? string.Format(
					@"INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ",
					sponsorAdminId)
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
				sponsorId,
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

		public IList<SponsorInvite> FindSentInvitesBySponsor(int sponsorId, int sponsorAdminId)
		{
			string j = sponsorAdminId != -1
				? string.Format(
					"INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ",
					sponsorAdminId)
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
				sponsorId,
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

		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySponsorAdmin(int sponsorId, int sponsorAdminId)
		{
			string w = sponsorAdminId != -1
				? string.Format(
					@"INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID
WHERE sad.SponsorAdminID = {0}
AND ", sponsorAdminId)
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
				sponsorId,
				w
			);
			var surveys = new List<SponsorExtendedSurvey>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new SponsorExtendedSurvey {
						ProjectRound = new ProjectRound {Id = rs.GetInt32(0)},
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

		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySponsor(int sponsorId)
		{
			string query = string.Format(
				@"
SELECT ses.ProjectRoundID,
	ses.Internal,
	ses.RoundText,
	ses.IndividualFeedbackEmailSubject,
	ses.IndividualFeedbackEmailBody,
	ses.SponsorExtendedSurveyID
FROM SponsorExtendedSurvey ses
WHERE ses.SponsorID = {0}
ORDER BY ses.SponsorExtendedSurveyID",
				sponsorId
			);
			var surveys = new List<SponsorExtendedSurvey>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new SponsorExtendedSurvey {
						ProjectRound = new ProjectRound {Id = rs.GetInt32(0)},
						Internal = rs.GetString(1),
						RoundText = rs.GetString(2),
						IndividualFeedbackEmailSubject = rs.GetString(3),
						IndividualFeedbackEmailBody = rs.GetString(4),
						Id = GetInt32(rs, 5)
					};
					surveys.Add(s);
				}
			}
			return surveys;
		}

		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySuperAdmin(int superAdminId)
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
				superAdminId
			);
			var surveys = new List<SponsorExtendedSurvey>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new SponsorExtendedSurvey {
						Sponsor = new Sponsor {Name = GetString(rs, 0)},
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

		public IList<SponsorAdminDepartment> FindAdminDepartmentBySponsorAdmin(int sponsorAdminId)
		{
			string query = string.Format(
				@"
SELECT DepartmentID
FROM SponsorAdminDepartment
WHERE SponsorAdminID = {0}",
				sponsorAdminId
			);
			var departments = new List<SponsorAdminDepartment>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new SponsorAdminDepartment {
						Department = new Department {Id = rs.GetInt32(0)}
					};
					departments.Add(d);
				}
			}
			return departments;
		}

		public IList<SponsorAdminFunction> FindAdminFunctionBySponsorAdmin(int sponsorAdminId)
		{
			string query = string.Format(
				@"
SELECT ManagerFunctionID
FROM SponsorAdminFunction
WHERE SponsorAdminID = {0}",
				sponsorAdminId
			);
			var functions = new List<SponsorAdminFunction>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var f = new SponsorAdminFunction {
						Function = new ManagerFunction {Id = rs.GetInt32(0)}
					};
					functions.Add(f);
				}
			}
			return functions;
		}

		public IList<SponsorAdmin> FindAdminBySponsor(int sponsorId, int sponsorAdminId)
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
AND sa.SponsorID = {0}
ORDER BY sa.Name",
				sponsorId,
				sponsorAdminId,
				sponsorAdminId != -1 ? "AND ((SELECT COUNT(*) FROM SponsorAdminDepartment sad WHERE sad.SponsorAdminID = sa.SponsorAdminID) = 0 OR (SELECT COUNT(*) FROM SponsorAdminDepartment sad INNER JOIN SponsorAdminDepartment sad2 ON sad.DepartmentID = sad2.DepartmentID WHERE sad.SponsorAdminID = sa.SponsorAdminID AND sad2.SponsorAdminID = " + sponsorAdminId + ") > 0) " : ""
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

		public IList<SponsorBackgroundQuestion> FindBySponsor(int sponsorId)
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
				sponsorId
			);
			var sponsors = new List<SponsorBackgroundQuestion>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new SponsorBackgroundQuestion {
						Id = rs.GetInt32(0),
						BackgroundQuestion = new BackgroundQuestion { Internal = rs.GetString(1) }
					};
					sponsors.Add(s);
				}
			}
			return sponsors;
		}
		
		public IList<SponsorInvite> FindInvites(string select, string ESuserSelect, string join, string ESuserJoin, int sponsorID, int deptID)
		{
			string query = string.Format(
				@"
SELECT s.SponsorInviteID,
	s.Email,
	s.Sent,
	s.UserID,
	u.ReminderLink,
	LOWER(LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)),
	s.PreviewExtendedSurveys,
	s.Stopped,
	s.StoppedReason
	{0}{1}
FROM SponsorInvite s{2}{3}
LEFT OUTER JOIN [User] u ON s.UserID = u.UserID
WHERE s.SponsorID = {4}
AND s.DepartmentID = {5}
ORDER BY s.Email",
				select,
				ESuserSelect,
				join,
				ESuserJoin,
				sponsorID,
				deptID
			);
			var invites = new List<SponsorInvite>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var i = new SponsorInvite {
						Id = GetInt32(rs, 0),
						Email = GetString(rs, 1),
						Sent = GetDateTime(rs, 2),
						User = new User {
							Id = GetInt32(rs, 3),
							ReminderLink = GetInt32(rs, 4),
							UserKey = GetString(rs, 5)
						},
						PreviewExtendedSurveys = GetInt32(rs, 6),
						Stopped = GetDateTime(rs, 7),
						StoppedReason = GetInt32(rs, 8)
					};
					invites.Add(i);
				}
			}
			return invites;
		}
		
		public IList<SponsorInvite> FindInvites(string hiddenBqJoin, string hiddenBqWhere, int sponsorAdminID, int sponsorID, string email)
		{
			string query = string.Format(
				@"
SELECT si.SponsorInviteID,
	si.DepartmentID,
	dbo.cf_departmentTree(si.DepartmentID,' » ') + ' » ' + si.Email
FROM SponsorInvite si
{0}
{1}
si.SponsorID = {2}
AND (si.Email LIKE '%{3}%'{4})",
				hiddenBqJoin,
				(sponsorAdminID != -1 ? "INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + sponsorAdminID + " AND " : "WHERE "),
				sponsorID,
				email.Replace("'", ""),
				hiddenBqWhere.Replace("[x]", "'%" + email.Replace("'", "") + "%'")
			);
			var invites = new List<SponsorInvite>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var i = new SponsorInvite {
						Id = GetInt32(rs, 0),
						Department = rs.IsDBNull(1) ? null : new Department {
							Id = GetInt32(rs, 1),
							TreeName = GetString(rs, 2)
						}
					};
					invites.Add(i);
				}
			}
			return invites;
		}
		
		public IList<UserSponsorExtendedSurvey> Find3(int sponsorExtendedSurveyID, int bqID, int sponsorID, int valueInt)
		{
			string query = string.Format(
				@"
SELECT usesX.AnswerID
FROM SponsorInvite si
INNER JOIN [User] u ON si.UserID = u.UserID
INNER JOIN UserSponsorExtendedSurvey usesX ON u.UserID = usesX.UserID AND usesX.SponsorExtendedSurveyID = {0}
LEFT OUTER JOIN SponsorInviteBQ sib ON si.SponsorInviteID = sib.SponsorInviteID AND sib.BQID = {1}
LEFT OUTER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID
LEFT OUTER JOIN UserProfileBQ upb ON up.UserProfileID = upb.UserProfileID AND upb.BQID = {2}
WHERE usesX.AnswerID IS NOT NULL AND si.SponsorID = {3} AND ISNULL(sib.BAID,upb.ValueInt) = {4}",
				sponsorExtendedSurveyID,
				bqID,
				bqID,
				sponsorID,
				valueInt
			);
			var surveys = new List<UserSponsorExtendedSurvey>();
			SqlDataReader rs = Db.rs(query);
			while (rs.Read()) {
				var s = new UserSponsorExtendedSurvey {
					Answer = new Answer { Id = GetInt32(rs, 0) }
				};
				surveys.Add(s);
			}
			return surveys;
		}
		
		public IList<SponsorExtendedSurvey> Find2(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT ses.SponsorExtendedSurveyID,
	ses.Internal,
	ses.ProjectRoundID,
	ses.EformFeedbackID,
	ses.RequiredUserCount,
	ses.PreviousProjectRoundID,
	ses.RoundText,
	ses2.RoundText,
	pr.Started,
	pr.Closed,
	ses.WarnIfMissingQID,
	ses.ExtraEmailSubject
FROM SponsorExtendedSurvey ses
LEFT OUTER JOIN SponsorExtendedSurvey ses2 ON ses.SponsorID = ses2.SponsorID AND ses.PreviousProjectRoundID = ses2.ProjectRoundID
LEFT OUTER JOIN eform..ProjectRound pr ON ses.ProjectRoundID = pr.ProjectRoundID
WHERE ses.SponsorID = {0}
ORDER BY ses.SponsorExtendedSurveyID",
				sponsorID
			);
			var surveys = new List<SponsorExtendedSurvey>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var s = new SponsorExtendedSurvey {
						Id = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						ProjectRound = rs.IsDBNull(2) ? null : new ProjectRound {
							Id = GetInt32(rs, 2),
							Started = GetDateTime(rs, 8),
							Closed = GetDateTime(rs, 9)
						},
						Feedback = rs.IsDBNull(3) ? null : new Feedback { Id = GetInt32(rs, 3) },
						RequiredUserCount = GetInt32(rs, 4, 10),
						PreviousProjectRound = rs.IsDBNull(5) ? null : new ProjectRound { Id = GetInt32(rs, 5) },
						RoundText = GetString(rs, 6),
						RoundText2 = GetString(rs, 7),
						WarnIfMissingQID = GetInt32(rs, 10),
						ExtraEmailSubject = GetString(rs, 11)
					};
					surveys.Add(s);
				}
			}
			return surveys;
		}
		
		public IList<SponsorBackgroundQuestion> Find(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT BQ.Internal,
	BQ.BQID,
	BQ.Type,
	sbq.Hidden,
	sbq.InGrpAdmin,
	sbq.Fn,
	BQ.InternalAggregate,
	BQ.Restricted
FROM SponsorBQ sbq
INNER JOIN BQ ON sbq.BQID = BQ.BQID
WHERE sbq.SponsorID = {0} AND (sbq.Hidden = 1 OR sbq.InGrpAdmin = 1)
ORDER BY sbq.SortOrder",
				sponsorID
			);
			var questions = new List<SponsorBackgroundQuestion>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var q = new SponsorBackgroundQuestion {
						BackgroundQuestion = new BackgroundQuestion {
							Internal = GetString(rs, 0),
							Id = GetInt32(rs, 1),
							Type = GetInt32(rs, 2),
							InternalAggregate = GetString(rs, 6),
							Restricted = GetInt32(rs, 7) == 1
						},
						Hidden = GetInt32(rs, 3),
						InGrpAdmin = GetInt32(rs, 4),
						Fn = GetInt32(rs, 5)
					};
					questions.Add(q);
				}
			}
			return questions;
		}

		// TODO: Is this a duplicate of SqlQuestionRepository.FindBackgroundQuestions()?
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
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var q = new SponsorBackgroundQuestion {
						BackgroundQuestion = new BackgroundQuestion {
							Id = GetInt32(rs, 0),
							Type = GetInt32(rs, 1)
						}
					};
					questions.Add(q);
				}
			}
			return questions;
		}

		public IList<SponsorProjectRoundUnit> FindBySponsorAndLanguage(int sponsorId, int langId)
		{
			string query = string.Format(
				@"
SELECT ISNULL(sprul.Nav, '?'),
	spru.ProjectRoundUnitID
FROM SponsorProjectRoundUnit spru
LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID
WHERE spru.SponsorID = {0}
AND ISNULL(sprul.LangID, 1) = {1}",
				sponsorId,
				langId
			);
			var projects = new List<SponsorProjectRoundUnit>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var p = new SponsorProjectRoundUnit {
						Navigation = rs.GetString(0),
						ProjectRoundUnit = new ProjectRoundUnit {Id = rs.GetInt32(1)}
					};
					projects.Add(p);
				}
			}
			return projects;
		}

		public IList<SponsorProjectRoundUnit> FindDistinctRoundUnitsWithReportBySuperAdmin(int superAdminId)
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
				superAdminId,
				DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01"),
				DateTime.Now.ToString("yyyy-MM-01")
			);
			var units = new List<SponsorProjectRoundUnit>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var u = new SponsorProjectRoundUnit {
						Sponsor = new Sponsor {Name = rs.GetString(0)},
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

		public IList<Sponsor> FindAndCountDetailsBySuperAdmin(int superAdminId)
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
				superAdminId
			);
			var sponsors = new List<Sponsor>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new Sponsor {
						Id = rs.GetInt32(0),
						Name = rs.GetString(1),
						SponsorKey = rs.GetString(2),
						ClosedAt = GetDateTime(rs, 9),
						MinimumInviteDate = GetDateTime(rs, 6),
						ExtendedSurveys = new List<SponsorExtendedSurvey>(GetInt32(rs, 3)),
						SentInvites = new List<SponsorInvite>(GetInt32(rs, 4)),
						ActiveInvites = new List<SponsorInvite>(GetInt32(rs, 5)),
						SuperAdminSponsors = new List<SuperAdminSponsor>(
							new [] {
								new SuperAdminSponsor {SeeUsers = GetInt32(rs, 7) == 1}
							}
						),
						Invites = new List<SponsorInvite>(GetInt32(rs, 8))
					};
					sponsors.Add(s);
				}
			}
			return sponsors;
		}

//		public IList<Sponsor> Y(int sponsorID)
//		{
//			string query = string.Format(
//				@"
		//SELECT DISTINCT s.Sponsor,
//	ses.ProjectRoundUnitID,
//	ses.Nav,
//	rep.ReportID,
//	rep.Internal,
//	(
//		SELECT COUNT(DISTINCT a.ProjectRoundUserID)
//		FROM eform.dbo.Answer AS a
//		WHERE (a.ProjectRoundUnitID = r.ProjectRoundUnitID)
//			AND (a.EndDT >= '{1}')
//			AND (a.EndDT < '{2}')
//	) AS CX
		//FROM Sponsor AS s
		//INNER JOIN SponsorProjectRoundUnit AS ses ON ses.SponsorID = s.SponsorID
		//INNER JOIN eform.dbo.ProjectRoundUnit AS r ON ses.ProjectRoundUnitID = r.ProjectRoundUnitID
		//INNER JOIN eform.dbo.Report AS rep ON rep.ReportID = r.ReportID
		//INNER JOIN SuperAdminSponsor AS sas ON s.SponsorID = sas.SponsorID
		//WHERE (s.Deleted IS NULL) AND (sas.SuperAdminID = {0})
		//ORDER BY s.Sponsor, ses.Nav",
//				sponsorID,
//				DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01"),
//				DateTime.Now.ToString("yyyy-MM-01")
//			);
//			var sponsors = new List<Sponsor>();
//			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
//				while (rs.Read()) {
//					var s = new Sponsor();
//					s.Name = rs.GetString(0);
//					s.ProjectRoundUnit = new ProjectRoundUnit {
//						Report = new Report { Id = rs.GetInt32(3), Internal = rs.GetString(4) }
//					};
//				}
//			}
//			return sponsors;
//		}

		public SponsorBackgroundQuestion ReadSponsorBackgroundQuestion(int sponsorBqId)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateSponsorInvite2(int sponsorInviteID)
		{
			string query = string.Format(
				@"
UPDATE SponsorInvite SET SponsorID = -ABS(SponsorID),
DepartmentID = -ABS(DepartmentID),
UserID = -ABS(UserID)
WHERE SponsorInviteID = {0}", sponsorInviteID
			);
			Db.exec(query);
		}
		
		public void UpdateSponsorInvite(string departmentID, DateTime stopped, int stoppedReason, int sponsorInviteID)
		{
			string query = string.Format(
				@"
UPDATE SponsorInvite SET DepartmentID = {0},
Stopped = {1},
StoppedReason = {2}
WHERE SponsorInviteID = {3}",
				departmentID,
				(stopped != DateTime.MinValue ? "'" + stopped.ToString("yyyy-MM-dd") + "'" : "NULL"),
				(stoppedReason != 0 ? stoppedReason.ToString() : "NULL"),
				sponsorInviteID
			);
			Db.exec(query);
		}
		
		public void UpdateSponsorInviteAndDepartment(int sponsorId, int userId, int sponsorInviteId, int departmentId)
		{
			Db.exec(string.Format("UPDATE SponsorInvite SET UserID = NULL WHERE UserID = {0}", userId));
			Db.exec(string.Format("UPDATE SponsorInvite SET UserID = {0}, Sent = GETDATE() WHERE SponsorInviteID = {1}", userId, sponsorInviteId));
			Db.exec(string.Format("UPDATE [User] SET DepartmentID = {0}, SponsorID = {1} WHERE UserID = {2}", departmentId, sponsorId, userId));
			Db.exec(string.Format("UPDATE UserProfile SET DepartmentID = {0}, SponsorID = {1} WHERE UserID = {2}", departmentId, sponsorId, userId));
		}

		public void UpdateSponsorInviteSent(int sponsorInviteId)
		{
			string query = string.Format(
				@"
UPDATE SponsorInvite SET Sent = GETDATE()
WHERE SponsorInviteID = {0}",
				sponsorInviteId
			);
			Db.exec(query, "healthWatchSqlConnection");
		}

		public void UpdateSponsor(int sponsorId)
		{
			string query = string.Format(
				@"
UPDATE Sponsor SET AllMessageLastSent = GETDATE()
WHERE SponsorID = {0}",
				sponsorId
			);
			Db.exec(query, "healthWatchSqlConnection"); // TODO: move to department???
		}

		public void UpdateSponsorAdminPassword(string password, int sponsorAdminId)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdmin SET Pas = '{0}' WHERE SponsorAdminID = {1}",
				password,
				sponsorAdminId
			);
			Db.exec(query);
		}

		public void UpdateSponsorAdminSession(int sponsorAdminSessionId, DateTime date)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdminSession SET EndDT = '{0}'
WHERE SponsorAdminSessionID = {1}",
				date,
				sponsorAdminSessionId
			);
			Db.exec(query);
		}

		public void SaveSponsorAdminSession(int sponsorAdminId, DateTime date)
		{
			string query = string.Format(
				@"
INSERT INTO SponsorAdminSession(SponsorAdminID, DT)
VALUES({0}, '{1}')",
				sponsorAdminId,
				date
			);
			Db.exec(query);
		}

		public void SaveSponsorAdminSessionFunction(int sessionId, int functionId, DateTime date)
		{
			string query = string.Format(
				@"
INSERT INTO SponsorAdminSessionFunction(SponsorAdminSessionID, ManagerFunctionID, DT)
VALUES({0}, {1}, '{2}')",
				sessionId,
				functionId,
				date
			);
			Db.exec(query);
		}

		public void SaveSponsorAdminDepartment(SponsorAdminDepartment d)
		{
			string query = string.Format(
				@"
INSERT INTO SponsorAdminDepartment (SponsorAdminID,DepartmentID)
VALUES ({0},{1})",
				d.Id,
				d.Department.Id
			);
			Db.exec(query);
		}

		public int CountExtendedSurveyBySponsor(int sponsorId)
		{
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM SponsorExtendedSurvey ses
WHERE ses.SponsorID = {0}",
				sponsorId
			);
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}

		public int CountSentInvitesBySponsor3(int sponsorId, DateTime dt)
		{
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM SponsorInvite si INNER JOIN [User] u ON si.UserID = u.UserID
WHERE si.SponsorID = {0} AND u.Created < '{1}'",
				sponsorId,
				dt.ToString("yyyy-MM-dd")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}

		public int CountSentInvitesBySponsor2(int sponsorId, DateTime dt)
		{
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM SponsorInvite si
LEFT OUTER JOIN [User] u ON si.UserID = u.UserID
WHERE si.SponsorID = {0}
AND (ISNULL(u.Created, si.Sent) < '{1}' OR si.Sent < '{1}')",
				sponsorId,
				dt.ToString("yyyy-MM-dd")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public UserProfileBackgroundQuestion Read3(int bqID, int sponsorID)
		{
			string query = string.Format(
				@"
SELECT AVG(DATEDIFF(year, upbq.ValueDate, GETDATE())),
	COUNT(upbq.ValueDate)
FROM SponsorInvite si
INNER JOIN [User] u ON si.UserID = u.UserID
INNER JOIN UserProfileBQ upbq ON u.UserProfileID = upbq.UserProfileID AND upbq.BQID = {0}
WHERE si.SponsorID = {1}",
				bqID,
				sponsorID
			);
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					return new UserProfileBackgroundQuestion {
						Average = GetDouble(rs, 0),
						Count = GetInt32(rs, 1)
					};
				}
			}
			return null;
		}
		
		public SponsorInvite Read2(int deleteUserID)
		{
			string query = string.Format(
				@"
SELECT si.UserID
FROM SponsorInvite si
WHERE si.SponsorInviteID = {0}",
				deleteUserID
			);
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					return new SponsorInvite {
						User = rs.IsDBNull(0) ? null : new User { Id = GetInt32(rs, 0) }
					};
				}
			}
			return null;
		}

		public int ReadLastSponsorAdminSession()
		{
			string query = string.Format(
				@"
SELECT TOP 1 * FROM SponsorAdminSession ORDER BY SponsorAdminSessionID DESC"
			);
			int sessionId = 0;
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					sessionId = GetInt32(rs, 0);
				}
			}
			return sessionId;
		}

		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySuperAdmin2(int superAdminId)
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
--AND s.Closed IS NOT NULL
ORDER BY s.Sponsor, ses.Internal, ses.RoundText",
				superAdminId
			);
			var surveys = new List<SponsorExtendedSurvey>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new SponsorExtendedSurvey {
						Sponsor = new Sponsor {Name = GetString(rs, 0)},
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

		public IList<SuperSponsor> FindSuperSponsors()
		{
			string query = string.Format(
				@"
SELECT SuperSponsorID,
SuperSponsor
FROM SuperSponsor"
			);
			var supers = new List<SuperSponsor>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var s = new SuperSponsor {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1)
					};
					supers.Add(s);
				}
			}
			return supers;
		}

		public IList<SuperAdminSponsor> FindSuperAdminSponsors(int superAdminId)
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
				superAdminId
			);
			var admins = new List<SuperAdminSponsor>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var a = new SuperAdminSponsor {
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
//						SeeUsers = GetBoolean(rs, 7)
						SeeUsers = GetInt32(rs, 7) == 1
					};
					admins.Add(a);
				}
			}
			return admins;
		}

		public IList<SponsorProjectRoundUnit> FindSponsorProjectRoundUnits(int superAdminId)
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
				superAdminId
			);
			var units = new List<SponsorProjectRoundUnit>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var u = new SponsorProjectRoundUnit {
						Sponsor = new Sponsor {Name = GetString(rs, 0)},
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
	}
}