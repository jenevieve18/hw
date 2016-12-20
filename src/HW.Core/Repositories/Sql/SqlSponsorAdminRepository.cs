using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlSponsorAdminRepository : BaseSqlRepository<SponsorAdmin>, IExtendedSurveyRepository, ISponsorAdminRepository
	{
		public void SavePassword(string password, string uid)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdmin SET Pas = @Password,
    UniqueKeyUsed = 1
WHERE UniqueKey = @UniqueKey");
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@Password", password),
				new SqlParameter("@UniqueKey", uid)
			);
		}
		
		public void SaveSponsorAdminExercise(SponsorAdminExercise exercise)
		{
			string query = string.Format(
				@"
INSERT INTO SponsorAdminExercise(SponsorAdminID, ExerciseVariantLangID, Date)
VALUES(@SponsorAdminID, @ExerciseVariantLangID, GETDATE());
SELECT IDENT_CURRENT('SponsorAdminExercise');"
			);
			int sponsorAdminExerciseID = ConvertHelper.ToInt32(
				ExecuteScalar(
					query,
					"healthWatchSqlConnection",
					new SqlParameter("@SponsorAdminID", exercise.SponsorAdmin.Id),
					new SqlParameter("@ExerciseVariantLangID", exercise.ExerciseVariantLanguage.Id)
				)
			);
			query = string.Format(
				@"
INSERT SponsorAdminExerciseDataInput(SponsorAdminExerciseID, ValueText, [Order])
VALUES(@SponsorAdminExerciseID, @ValueText, @Order);
SELECT IDENT_CURRENT('SponsorAdminExerciseDataInput');");
			int i = 0;
			foreach (var data in exercise.Inputs) {
				int sponsorAdminExerciseDataInputID = ConvertHelper.ToInt32(
					ExecuteScalar(
						query,
						"healthWatchSqlConnection",
						new SqlParameter("@SponsorAdminExerciseID", sponsorAdminExerciseID),
						new SqlParameter("@ValueText", data.ValueText),
						new SqlParameter("@Order", i++)
					)
				);
				foreach (var c in data.Components) {
					ExecuteNonQuery(
						@"
INSERT INTO SponsorAdminExerciseDataInputComponent(SponsorAdminExerciseDataInputID, ValueText, SortOrder, ValueInt)
VALUES(@SponsorAdminExerciseDataInputID, @ValueText, @SortOrder, @ValueInt)",
						"healthWatchSqlConnection",
						new SqlParameter("@SponsorAdminExerciseDataInputID", sponsorAdminExerciseDataInputID),
						new SqlParameter("@ValueText", c.ValueText),
						new SqlParameter("@SortOrder", c.SortOrder),
						new SqlParameter("@ValueInt", c.ValueInt)
					);
				}
			}
		}
		
		public void UpdateUniqueKey(string uid, int sponsorAdminID)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdmin SET UniqueKey = @UniqueKey,
UniqueKeyUsed = 0
WHERE SponsorAdminID = @SponsorAdminID");
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@UniqueKey", uid),
				new SqlParameter("@SponsorAdminID", sponsorAdminID)
			);
		}
		
		public void UpdateSponsorLastLoginSent(int sponsorAdminId)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdmin SET LoginLastSent = GETDATE()
WHERE SponsorAdminID = {0}",
				sponsorAdminId
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateLastAllMessageSent(int sponsorAdminExtendedSurveyId)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdmin SET AllMessageLastSent = GETDATE()
WHERE SponsorAdminID = {0}",
				sponsorAdminExtendedSurveyId
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateExtendedSurveyLastFinishedSent(int sponsorAdminExtendedSurveyId)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdminExtendedSurvey SET FinishedLastSent = GETDATE()
WHERE SponsorAdminExtendedSurveyID = {0}",
				sponsorAdminExtendedSurveyId
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateExtendedSurveyLastEmailSent(int sponsorAdminExtendedSurveyId)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdminExtendedSurvey SET EmailLastSent = GETDATE()
WHERE SponsorAdminExtendedSurveyID = {0}",
				sponsorAdminExtendedSurveyId
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateSponsorLastInviteReminderSent(int sponsorAdminExtendedSurveyId)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdmin SET InviteReminderLastSent = GETDATE()
WHERE SponsorAdminID = {0}",
				sponsorAdminExtendedSurveyId
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateSponsorLastInviteSent(int sponsorAdminID)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdmin SET InviteLastSent = GETDATE()
WHERE SponsorAdminID = {0}",
				sponsorAdminID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public bool SponsorAdminEmailExists(string email, int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT Email
FROM SponsorAdmin
WHERE Email = @Email
AND SponsorAdminID != @SponsorAdminID
AND Usr NOT LIKE '%DELETED'"
			);
			bool exists = false;
			using (SqlDataReader r = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@Email", email), new SqlParameter("@SponsorAdminID", sponsorAdminID))) {
				if (r.Read()) {
					exists = true;
				}
			}
			return exists;
		}
		
		public bool SponsorAdminHasAccess(int sponsorAdminID, int function)
		{
			string q = sponsorAdminID != -1 ?
				string.Format(
					@"
INNER JOIN SponsorAdminFunction saf ON saf.ManagerFunctionID = f.ManagerFunctionID
WHERE saf.SponsorAdminID = @SponsorAdminID
AND") :
				"WHERE";
			string query = string.Format(
				@"
SELECT f.ManagerFunctionID
FROM ManagerFunction f
{0} f.ManagerFunctionID = @ManagerFunctionID",
				q
			);
			bool hasAccess = false;
			using (SqlDataReader r = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@SponsorAdminID", sponsorAdminID), new SqlParameter("@ManagerFunctionID", function))) {
				if (r.Read()) {
					hasAccess = true;
				}
			}
			return hasAccess;
		}

		public bool SponsorAdminUniqueKeyUsed(string uid)
		{
			string query = string.Format(
				@"
SELECT UniqueKey, UniqueKeyUsed
FROM SponsorAdmin
WHERE UniqueKey = @UniqueKey"
			);
			bool used = false;
			using (SqlDataReader rs = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@UniqueKey", uid)))
			{
				if (rs.Read())
				{
					used = GetInt32(rs, 1) == 1;
				}
			}
			return used;
		}
		
		public bool SponsorAdminUniqueKeyExists(string uid)
		{
			string query = string.Format(
				@"
SELECT UniqueKey
FROM SponsorAdmin
WHERE UniqueKey = @UniqueKey"
			);
			bool exists = false;
			using (SqlDataReader r = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@UniqueKey", uid))) {
				if (r.Read()) {
					exists = true;
				}
			}
			return exists;
		}

		public SponsorAdmin ReadSponsorByUniqueKey(string uniqueKey)
		{
			string query = @"
SELECT SponsorAdminID
FROM SponsorAdmin
WHERE UniqueKey = @UniqueKey";
			SponsorAdmin a = null;
			using (SqlDataReader rs = ExecuteReader(query, "healthWatchSqlConnection"))
			{
				if (rs.Read())
				{
					a = new SponsorAdmin {
						Id = GetInt32(rs, 0)
					};
				}
			}
			return a;
		}

		public SponsorAdminExercise ReadSponsorAdminExercise(int sponsorAdminExerciseID)
		{
			string query = @"
SELECT SponsorAdminExerciseID,
	Date,
	SponsorAdminID,
	ExerciseVariantLangID
FROM SponsorAdminExercise
WHERE SponsorAdminExerciseID = @SponsorAdminExerciseID";
			SponsorAdminExercise exercise = null;
			using (var rs = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@SponsorAdminExerciseID", sponsorAdminExerciseID))) {
				if (rs.Read()) {
					exercise = new SponsorAdminExercise {
						Id = GetInt32(rs, 0),
						Date = GetDateTime(rs, 1),
						SponsorAdmin = new SponsorAdmin { Id = GetInt32(rs, 2) },
						ExerciseVariantLanguage = new ExerciseVariantLanguage { Id = GetInt32(rs, 3) }
					};
				}
			}
			if (exercise != null) {
				exercise.AddDataInputs(FindSponsorAdminExerciseDataInputs(sponsorAdminExerciseID));
				foreach (var d in exercise.Inputs) {
					d.AddComponents(FindSponsorAdminExerciseDataInputComponents(d.Id));
				}
			}
			return exercise;
		}
		
		IList<SponsorAdminExerciseDataInputComponent> FindSponsorAdminExerciseDataInputComponents(int sponsorAdminExerciseDataInputID)
		{
			string query = @"
SELECT SponsorAdminExerciseDataInputComponentID,
	SponsorAdminExerciseDataInputID,
	ValueText,
	SortOrder,
	ValueInt
FROM dbo.SponsorAdminExerciseDataInputComponent
WHERE SponsorAdminExerciseDataInputID = @SponsorAdminExerciseDataInputID";
			var components = new List<SponsorAdminExerciseDataInputComponent>();
			using (var rs = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@SponsorAdminExerciseDataInputID", sponsorAdminExerciseDataInputID))) {
				while (rs.Read()) {
					components.Add(
						new SponsorAdminExerciseDataInputComponent {
							Id = GetInt32(rs, 0),
							ValueText = GetString(rs, 2),
							SortOrder = GetInt32(rs, 3),
							ValueInt = GetInt32(rs, 4)
						}
					);
				}
			}
			return components;
		}
		
		IList<SponsorAdminExerciseDataInput> FindSponsorAdminExerciseDataInputs(int sponsorAdminExerciseID)
		{
			string query = @"
SELECT SponsorAdminExerciseDataInputID,
	SponsorAdminExerciseID,
	ValueText,
	[Order],
	ValueInt,
	[Type]
FROM dbo.SponsorAdminExerciseDataInput
WHERE SponsorAdminExerciseID = @SponsorAdminExerciseID";
			var inputs = new List<SponsorAdminExerciseDataInput>();
			using (var rs = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@SponsorAdminExerciseID", sponsorAdminExerciseID))) {
				while (rs.Read()) {
					inputs.Add(
						new SponsorAdminExerciseDataInput {
							Id = GetInt32(rs, 0),
							ValueText = GetString(rs, 2),
							Order = GetInt32(rs, 3),
							ValueInt = GetInt32(rs, 4),
							Type = GetInt32(rs, 5)
						}
					);
				}
			}
			return inputs;
		}
		
		public override SponsorAdmin Read(int id)
		{
			string query = @"
SELECT 	SponsorAdminID,
	Usr,
	Pas,
	SponsorID,
	Name,
	Email,
	SuperUser,
	SponsorAdminKey,
	Anonymized,
	SeeUsers,
	ReadOnly,
	LastName,
	PermanentlyDeleteUsers,
	InviteSubject,
	InviteTxt,
	InviteReminderSubject,
	InviteReminderTxt,
	AllMessageSubject,
	AllMessageBody,
	InviteLastSent,
	InviteReminderLastSent,
	AllMessageLastSent,
	LoginLastSent,
	UniqueKey,
	UniqueKeyUsed
FROM SponsorAdmin
WHERE SponsorAdminID = @SponsorAdminID";
			SponsorAdmin sponsorAdmin = null;
			using (var rs = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@SponsorAdminID", id))) {
				if (rs.Read()) {
					sponsorAdmin = new SponsorAdmin {
						SponsorAdminID = GetInt32(rs, 0),
						Id = GetInt32(rs, 0),
						Usr = GetString(rs, 1),
						Password = GetString(rs, 2),
						SponsorID = GetInt32(rs, 3),
						Sponsor = new Sponsor { Id = GetInt32(rs, 3) },
						Name = GetString(rs, 4),
						Email = GetString(rs, 5),
						SuperUser = GetInt32(rs, 6) == 1,
						SponsorAdminKey = GetGuid(rs, 7),
						Anonymized = GetInt32(rs, 8) == 1,
						SeeUsers = GetInt32(rs, 9) == 1,
						ReadOnly = GetInt32(rs, 10) == 1,
						LastName = GetString(rs, 11),
						PermanentlyDeleteUsers = GetInt32(rs, 12) == 1,
						InviteSubject = GetString(rs, 13),
						InviteTxt = GetString(rs, 14),
						InviteReminderSubject = GetString(rs, 15),
						InviteReminderTxt = GetString(rs, 16),
						AllMessageSubject = GetString(rs, 17),
						AllMessageBody = GetString(rs, 18),
						InviteLastSent = GetDateTime(rs, 19),
						InviteReminderLastSent = GetDateTime(rs, 20),
						AllMessageLastSent = GetDateTime(rs, 21),
						LoginLastSent = GetDateTime(rs, 22),
						UniqueKey = GetString(rs, 23),
						UniqueKeyUsed = GetInt32(rs, 24)
					};
				}
			}
			return sponsorAdmin;
		}
		
		public IAdmin ReadSponsor(int sponsorAdminId)
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
	s.LoginLastSent,
	s.EmailFrom
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
						LoginLastSent = GetLaterDate(GetDateTime(rs, 20), GetDateTime(rs, 24)),
						EmailFrom = GetString(rs, 25, "", "info@healthwatch.se")
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
	se.RoundText,
	sae.FinishedLastSent,
	sae.EmailLastSent
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
						RoundText = GetString(rs, 8),
						FinishedLastSent = GetDateTime(rs, 9),
						EmailLastSent = GetDateTime(rs, 10)
					};
					surveys.Add(s);
				}
			}
			return surveys;
		}
		
		public IExtendedSurvey ReadExtendedSurveysBySponsorAdmin(int sponsorID, int sponsorAdminID)
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
	se.RoundText,
	sae.FinishedLastSent,
	sae.EmailLastSent
FROM SponsorAdminExtendedSurvey sae
INNER JOIN SponsorExtendedSurvey se ON sae.SponsorExtendedSurveyID = se.SponsorExtendedSurveyID
INNER JOIN eForm..ProjectRound pr ON se.ProjectRoundID = pr.ProjectRoundID
WHERE SponsorAdminID = {0}
ORDER BY SponsorAdminExtendedSurveyID DESC",
				sponsorAdminID
			);
			SponsorAdminExtendedSurvey s = null;
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					s = new SponsorAdminExtendedSurvey {
						ExtraExtendedSurveyId = GetInt32(rs, 0),
						EmailSubject = GetString(rs, 1),
						EmailBody = GetString(rs, 2),
						FinishedEmailSubject = GetString(rs, 3),
						FinishedEmailBody = GetString(rs, 4),
						Id = GetInt32(rs, 5),
						ProjectRound = rs.IsDBNull(6) ? null : new ProjectRound { Id = GetInt32(rs, 6) },
						Internal = GetString(rs, 7),
						RoundText = GetString(rs, 8),
						FinishedLastSent = GetDateTime(rs, 9),
						EmailLastSent = GetDateTime(rs, 10)
					};
				}
			}
			return s;
		}
		
		public int UpdateEmailTexts(int sponsorExtendedSurveyID, int sponsorAdminID, int sponsorAdminExtendedSurveyID, string emailSubject, string emailBody, string finishedEmailSubject, string finishedEmailBody)
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
				new SqlParameter("@SponsorExtendedSurveyID", sponsorExtendedSurveyID),
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
