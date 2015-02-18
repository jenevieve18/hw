using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlSponsorAdminRepository : BaseSqlRepository<SponsorAdmin>, IExtendedSurveyRepository
	{
		public void UpdateSponsorLastLoginSent(int sponsorAdminId)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdmin SET LoginLastSent = GETDATE() WHERE SponsorAdminID = {0}",
				sponsorAdminId
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateLastAllMessageSent(int sponsorAdminExtendedSurveyId)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdmin SET AllMessageLastSent = GETDATE() WHERE SponsorAdminID = {0}",
				sponsorAdminExtendedSurveyId
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateExtendedSurveyLastFinishedSent(int sponsorAdminExtendedSurveyId)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdminExtendedSurvey SET FinishedLastSent = GETDATE() WHERE SponsorAdminExtendedSurveyID = {0}",
				sponsorAdminExtendedSurveyId
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateExtendedSurveyLastEmailSent(int sponsorAdminExtendedSurveyId)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdminExtendedSurvey SET EmailLastSent = GETDATE() WHERE SponsorAdminExtendedSurveyID = {0}",
				sponsorAdminExtendedSurveyId
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateSponsorLastInviteReminderSent(int sponsorAdminExtendedSurveyId)
		{
//			string query = string.Format(
//				@"
//UPDATE SponsorAdminExtendedSurvey SET InviteReminderLastSent = GETDATE() WHERE SponsorAdminExtendedSurveyID = {0}",
//				sponsorAdminExtendedSurveyId
//			);
			string query = string.Format(
				@"
UPDATE SponsorAdmin SET InviteReminderLastSent = GETDATE() WHERE SponsorAdminID = {0}",
				sponsorAdminExtendedSurveyId
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateSponsorLastInviteSent(int sponsorAdminExtendedSurveyId)
		{
//			string query = string.Format(
//				@"
//UPDATE SponsorAdminExtendedSurvey SET InviteLastSent = GETDATE() WHERE SponsorAdminExtendedSurveyID = {0}",
//				sponsorAdminExtendedSurveyId
//			);
			string query = string.Format(
				@"
UPDATE SponsorAdmin SET InviteLastSent = GETDATE() WHERE SponsorAdminID = {0}",
				sponsorAdminExtendedSurveyId
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public ISponsor ReadSponsor(int sponsorAdminId)
		{
			string query = string.Format(
				@"
SELECT a.SuperUser,
	a.InviteSubject,
	a.InviteTxt,
	a.InviteReminderSubject,
	a.InviteReminderTxt,
	a.AllMessageSubject,
	a.AllMessageBody,
	s.InviteSubject,
	s.InviteTxt,
	s.InviteReminderSubject,
	s.InviteReminderTxt,
	s.AllMessageSubject,
	s.AllMessageBody,
	s.LoginTxt,
	s.LoginSubject,
	s.LoginDays,
	s.LoginWeekday,
	a.InviteLastSent,
	a.InviteReminderLastSent,
	a.AllMessageLastSent,
	a.LoginLastSent,
	s.InviteLastSent,
	s.InviteReminderLastSent,
	s.AllMessageLastSent,
	s.LoginLastSent
FROM SponsorAdmin a,
Sponsor s
WHERE s.SponsorID = a.SponsorID
AND a.SponsorAdminID = {0}",
				sponsorAdminId
			);
			SponsorAdmin a = null;
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					a = new SponsorAdmin {
						SuperUser = GetInt32(rs, 0) != 0,
						InviteSubject = GetString(rs, 1, GetString(rs, 7)),
						InviteText = GetString(rs, 2, GetString(rs, 8)),
						InviteReminderSubject = GetString(rs, 3, GetString(rs, 9)),
						InviteReminderText = GetString(rs, 4, GetString(rs, 10)),
						AllMessageSubject = GetString(rs, 5, GetString(rs, 11)),
						AllMessageBody = GetString(rs, 6, GetString(rs, 12)),
						LoginText = GetString(rs, 13),
						LoginSubject = GetString(rs, 14),
						LoginDays = GetInt32(rs, 15),
						LoginWeekday = GetInt32(rs, 16),
						InviteLastSent = GetLaterDate(GetDateTime(rs, 17), GetDateTime(rs, 21)),
						InviteReminderLastSent = GetLaterDate(GetDateTime(rs, 18), GetDateTime(rs, 22)),
						AllMessageLastSent = GetLaterDate(GetDateTime(rs, 19), GetDateTime(rs, 23)),
						LoginLastSent = GetLaterDate(GetDateTime(rs, 20), GetDateTime(rs, 24))
					};
				}
			}
			return a;
		}
		
		DateTime? GetLaterDate(DateTime? d1, DateTime? d2)
		{
			if (d1 == null) {
				return d2;
			}
			if (d2 > d1) {
				return d2;
			} else {
				return d1;
			}
		}
		
		public IList<IExtendedSurvey> FindExtendedSurveysBySponsorAdmin(int sponsorID, int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT SponsorAdminExtendedSurveyID,
	sae.EmailSubject,
	sae.EmailBody,
	sae.FinishedEmailSubject,
	sae.FinishedEmailBody,
	sae.SponsorExtendedSurveyID,
	pr.ProjectRoundID,
	se.Internal,
	se.RoundText
FROM SponsorAdminExtendedSurvey sae
INNER JOIN SponsorExtendedSurvey se ON sae.SponsorExtendedSurveyID = se.SponsorExtendedSurveyID
INNER JOIN eForm..ProjectRound pr ON se.ProjectRoundID = pr.ProjectRoundID
WHERE SponsorAdminID = {0}
ORDER BY SponsorAdminExtendedSurveyID DESC",
				sponsorAdminID
			);
			var surveys = new List<IExtendedSurvey>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new SponsorAdminExtendedSurvey {
						ExtraExtendedSurveyId = GetInt32(rs, 0),
						EmailSubject = GetString(rs, 1),
						EmailBody = GetString(rs, 2),
						FinishedEmailSubject = GetString(rs, 3),
						FinishedEmailBody = GetString(rs, 4),
						Id = GetInt32(rs, 5),
						ProjectRound = rs.IsDBNull(6) ? null : new ProjectRound { Id = GetInt32(rs, 6) },
						Internal = GetString(rs, 7),
						RoundText = GetString(rs, 8)
					};
					surveys.Add(s);
				}
			}
			return surveys;
		}
		
		public int UpdateEmailTexts(int ID, int sponsorAdminID, int sponsorAdminExtendedSurveyID, string emailSubject, string emailBody, string finishedEmailSubject, string finishedEmailBody)
		{
			string query = string.Format(
				@"
DECLARE @key INT
SELECT @key = SponsorAdminExtendedSurveyID
FROM SponsorAdminExtendedSurvey
WHERE SponsorAdminExtendedSurveyID = @SponsorAdminExtendedSurveyID
IF (@key IS NOT NULL)
    UPDATE SponsorAdminExtendedSurvey SET
	EmailSubject = @EmailSubject,
	EmailBody = @EmailBody,
	FinishedEmailSubject = @FinishedEmailSubject,
	FinishedEmailBody = @FinishedEmailBody,
	SponsorExtendedSurveyID = @SponsorExtendedSurveyID
WHERE SponsorAdminID = @SponsorAdminID
AND SponsorAdminExtendedSurveyID = @SponsorAdminExtendedSurveyID
ELSE
    INSERT SponsorAdminExtendedSurvey(EmailSubject, EmailBody, FinishedEmailSubject, FinishedEmailBody, SponsorExtendedSurveyID, SponsorAdminID)
    VALUES(@EmailSubject, @EmailBody, @FinishedEmailSubject, @FinishedEmailBody, @SponsorExtendedSurveyID, @SponsorAdminID)");
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@EmailSubject", emailSubject),
				new SqlParameter("@EmailBody", emailBody),
				new SqlParameter("@FinishedEmailSubject", finishedEmailSubject),
				new SqlParameter("@FinishedEmailBody", finishedEmailBody),
				new SqlParameter("@SponsorExtendedSurveyID", ID),
				new SqlParameter("@SponsorAdminID", sponsorAdminID),
				new SqlParameter("@SponsorAdminExtendedSurveyID", sponsorAdminExtendedSurveyID)
			);
			if (sponsorAdminExtendedSurveyID == 0) {
				sponsorAdminExtendedSurveyID = ConvertHelper.ToInt32(Db.exes("SELECT IDENT_CURRENT('SponsorAdminExtendedSurvey')"));
			}
			return sponsorAdminExtendedSurveyID;
		}
		
		public void UpdateInviteTexts(int ID, string inviteSubject, string inviteText, string inviteReminderSubject, string inviteReminderText, string allMessageSubject, string allMessageBody)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdmin SET
	InviteSubject = @InviteSubject,
	InviteTxt = @InviteTxt,
	InviteReminderSubject = @InviteReminderSubject,
	InviteReminderTxt = @InviteReminderTxt,
	AllMessageSubject = @AllMessageSubject,
	AllMessageBody = @AllMessageBody
WHERE SponsorAdminID = @SponsorAdminID");
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@InviteSubject", inviteSubject),
				new SqlParameter("@InviteTxt", inviteText),
				new SqlParameter("@InviteReminderSubject", inviteReminderSubject),
				new SqlParameter("@InviteReminderTxt", inviteReminderText),
				new SqlParameter("@AllMessageSubject", allMessageSubject),
				new SqlParameter("@AllMessageBody", allMessageBody),
				new SqlParameter("@SponsorAdminID", ID)
			);
		}
	}
}
