using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace HW.SendReminders
{
	public class BaseRepository
	{
		protected SqlDataReader recordSet(string sqlString)
		{
			SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
			dataConnection.Open();
			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
			SqlDataReader dataReader = dataCommand.ExecuteReader(CommandBehavior.CloseConnection);
			return dataReader;
		}

		protected void exec(string sqlString)
		{
			SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
			dataConnection.Open();
			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
			dataCommand.ExecuteNonQuery();
			dataConnection.Close();
			dataConnection.Dispose();
		}
		
		protected int GetInt32(SqlDataReader rs, int index)
		{
			return rs.IsDBNull(index) ? 0 : rs.GetInt32(index);
		}
		
		protected DateTime GetDateTime(SqlDataReader rs, int index)
		{
			return rs.IsDBNull(index) ? DateTime.Now : rs.GetDateTime(index);
		}
		
		protected string GetString(SqlDataReader rs, int index)
		{
			return rs.IsDBNull(index) ? "" : rs.GetString(index);
		}
	}
	
	public interface ISmtp
	{
		void Send(MailMessage mail);
	}
	
	public class SmtpWrapper : ISmtp
	{
		SmtpClient smtp;
		
		public SmtpWrapper(SmtpClient smtp)
		{
			this.smtp = smtp;
		}
		
		public void Send(MailMessage mail)
		{
			smtp.Send(mail);
		}
	}
	
	public interface IRepo
	{
		SystemSettings GetSystemSettings();
		List<User> ggg();
		List<User> GetUsers();
		void UpdateUserKey();
		void demyo(int userID, string personalReminderMessage, string xxx, string yyy);
		void UpdateEmailFailure(int userID);
		List<Sponsor> otot();
		void hhh(int answerID, int userSponsorExtendedSurveyID);
		List<User> ototize(int sponsorID, string ddd, int userID);
		List<string> GetUserRegistrationIDs(int userID);
		void fff(int userID);
		void eee(int userID, string reminderSubject, string personalReminderMessage);
		void ddd(int userID, string userKey);
		void UpdateUserRegistrationID(int userID, string userKey);
		void bbb(Sponsor s);
		void aaa(int sponsorID);
	}
	
	public class Repo : BaseRepository, IRepo
	{
		public Repo()
		{
		}
		
		public void UpdateEmailFailure(int userID)
		{
//			exec("UPDATE [User] SET EmailFailure = GETDATE() WHERE UserID = " + rs.GetInt32(0));
			exec("UPDATE [User] SET EmailFailure = GETDATE() WHERE UserID = " + userID.ToString());
		}
		
		public List<User> ototize(int sponsorID, string date, int userID)
		{
			var rs = recordSet(
				"SELECT " +
				"u.UserID, " +
				"u.Email, " +
				"u.ReminderLink, " +
				"LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12), " +
				"ISNULL(u.LID,1) " +
				"FROM [User] u " +
				"INNER JOIN SponsorInvite si ON u.UserID = si.UserID " +
				"WHERE u.SponsorID = " + sponsorID + " " +
				"AND u.Email IS NOT NULL " +
				"AND u.Email <> '' " +
				"AND si.StoppedReason IS NULL " +
				"AND u.Email NOT LIKE '%DELETED' " +
//				"AND (u.ReminderLastSent IS NULL OR DATEADD(d," + (rs2.GetInt32(3) == 0 && rs2.GetInt32(2) != 1 ? "7" : "1") + ",u.ReminderLastSent) <= GETDATE()) " +
//				"AND dbo.cf_daysFromLastLogin(u.UserID) >= " + rs2.GetInt32(2));
				"AND (u.ReminderLastSent IS NULL OR DATEADD(d," + date + ",u.ReminderLastSent) <= GETDATE()) " +
				"AND dbo.cf_daysFromLastLogin(u.UserID) >= " + userID);
			var users = new List<User>();
			while (rs.Read()) {
				var u = new User {
					ID = GetInt32(rs, 0),
					Email = GetString(rs, 1),
					ReminderLink = GetInt32(rs, 2),
					UserKey = GetString(rs, 3),
					LID = GetInt32(rs, 4),
				};
				users.Add(u);
			}
			return users;
		}
		
		public List<Sponsor> otot()
		{
			SqlDataReader rs = recordSet(
				"SELECT " +
				"LoginTxt, " +
				"LoginSubject, " +
				"LoginDays, " +
				"LoginWeekday, " +
				"SponsorID " +
				"FROM Sponsor " +
				"WHERE Deleted IS NULL " +
				"AND Closed IS NULL " +
				"AND LoginWeekday IS NOT NULL " +
				"AND LoginDays IS NOT NULL " +
				"AND LoginTxt IS NOT NULL " +
				"AND LoginSubject IS NOT NULL");
			var sponsors = new List<Sponsor>();
			while (rs.Read()) {
				var s = new Sponsor {
					LoginTxt = GetString(rs, 0),
					LoginSubject = GetString(rs, 1),
					LoginDays = GetInt32(rs, 2),
					LoginWeekday = GetInt32(rs, 3),
					ID = GetInt32(rs, 4),
				};
				sponsors.Add(s);
			}
			return sponsors;
		}
		
		public void aaa(int sponsorID)
		{
			exec("UPDATE Sponsor SET LoginLastSent = GETDATE() WHERE SponsorID = " + sponsorID);
		}
		
		public void bbb(Sponsor s)
		{
			exec(
				"UPDATE [User] SET UserKey = NEWID() " +
				"WHERE SponsorID = " + s.ID + " " +
				"AND ReminderLink = 2 " +
				"AND Email IS NOT NULL " +
				"AND Email <> '' " +
				"AND Email NOT LIKE '%DELETED' " +
				"AND (ReminderLastSent IS NULL OR DATEADD(d," +
//				(rs2.GetInt32(3) == 0 && rs2.GetInt32(2) != 1 ? "7" : "1") +	// If check every day (0) and not send every day then push minimum reminder interval to 7 else 1
				(s.LoginWeekday == 0 && s.LoginDays != 1 ? "7" : "1") +	// If check every day (0) and not send every day then push minimum reminder interval to 7 else 1
				",ReminderLastSent) <= GETDATE()) " +
				"AND dbo.cf_daysFromLastLogin(UserID) >= " + s.LoginDays);
		}
		
		public void ddd(int userID, string userKey)
		{
			exec(
				"INSERT INTO dbo.UserRegistrationID(UserID, RegistrationID) " +
				"VALUES(" + userID + ", '" + userKey.Replace("'", "") + "')"
			);
		}
		
		public void UpdateUserRegistrationID(int userID, string userKey)
		{
			exec(
				"UPDATE dbo.UserRegistrationID SET UserID = " + -userID + " " +
				"WHERE UserID = " + userID + " " +
				"AND RegistrationID = '" + userKey.Replace("'", "") + "'"
			);
		}
		
		public void demyo(int userID, string personalReminderMessage, string xxx, string yyy)
		{
//			exec("UPDATE [User] SET ReminderLastSent = GETDATE(), ReminderNextSend = '" + nextReminderSend(rs.GetInt32(4),rs.GetString(5).Split(':'),(rs.IsDBNull(6) ? DateTime.Now : rs.GetDateTime(6)),DateTime.Now) + "' WHERE UserID = " + rs.GetInt32(0));
//			exec("INSERT INTO Reminder (UserID,Subject,Body) VALUES (" + rs.GetInt32(0) + ",'" + reminderSubjectLang[rs.GetInt32(7) - 1].Replace("'", "''") + "','" + personalReminderMessage.Replace("'", "''") + "')");
			exec("UPDATE [User] SET ReminderLastSent = GETDATE(), ReminderNextSend = '" + xxx + "' WHERE UserID = " + userID);
			exec("INSERT INTO Reminder (UserID,Subject,Body) VALUES (" + userID + ",'" + yyy + "','" + personalReminderMessage.Replace("'", "''") + "')");
		}
		
		public void hhh(int answerID, int userSponsorExtendedSurveyID)
		{
			exec("UPDATE UserSponsorExtendedSurvey SET AnswerID = " + answerID + " WHERE UserSponsorExtendedSurveyID = " + userSponsorExtendedSurveyID);
		}
		
		public List<User> ggg()
		{
			var rs = recordSet(
				"SELECT " +
				"s.UserSponsorExtendedSurveyID, " +
				"a.AnswerID, " +         // 1
				"u.Email " +
				"FROM [User] u " +
				"INNER JOIN UserSponsorExtendedSurvey s ON u.UserID = s.UserID " +
				"INNER JOIN eform..Answer a ON s.ProjectRoundUserID = a.ProjectRoundUserID " +
				"WHERE s.AnswerID IS NULL AND a.EndDT IS NOT NULL");
			var users = new List<User>();
			while (rs.Read()) {
				var u = new User {
					UserSponsorExtendedSurveyID = GetInt32(rs, 0),
					AnswerID = GetInt32(rs, 1),
					Email = GetString(rs, 2)
				};
				users.Add(u);
			}
			return users;
		}
		
		public void fff(int userID)
		{
//			exec("UPDATE [User] SET EmailFailure = GETDATE() WHERE UserID = " + rs.GetInt32(0));
			exec("UPDATE [User] SET EmailFailure = GETDATE() WHERE UserID = " + userID);
		}
		
		public void eee(int userID, string reminderSubject, string personalReminderMessage)
		{
//			exec("UPDATE [User] SET ReminderLastSent = GETDATE() WHERE UserID = " + rs.GetInt32(0));
			exec("UPDATE [User] SET ReminderLastSent = GETDATE() WHERE UserID = " + userID);
//			exec("INSERT INTO Reminder (UserID,Subject,Body) VALUES (" + rs.GetInt32(0) + ",'" + reminderSubject.Replace("'", "''") + "','" + personalReminderMessage.Replace("'", "''") + "')");
			exec("INSERT INTO Reminder (UserID,Subject,Body) VALUES (" + userID + ",'" + reminderSubject.Replace("'", "''") + "','" + personalReminderMessage.Replace("'", "''") + "')");
		}
		
		public List<string> GetUserRegistrationIDs(int userID)
		{
			List<string> registrationIds = new List<string>();
			using (var rs2 = recordSet(
				"SELECT UserRegistrationID, " +
				"UserID, " +
				"RegistrationID " +
				"FROM dbo.UserRegistrationID " +
//				"WHERE UserID = " + rs.GetInt32(0)
				"WHERE UserID = " + userID + " " +
				"AND Inactive != 1"
			)) {
				while (rs2.Read()) {
					if (!rs2.IsDBNull(2)) {
						registrationIds.Add(rs2.GetString(2));
					}
				}
			}
			return registrationIds;
		}
		
		public List<User> GetUsers()
		{
			var rs = recordSet(
				@"
SELECT
u.UserID,
u.Email,
u.ReminderLink,
LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12),
u.ReminderType,
u.ReminderSettings,
(SELECT TOP 1 DT FROM Session s WHERE s.UserID = u.UserID ORDER BY SessionID DESC) AS LastLogin,
ISNULL(u.LID,1)
FROM [User] u
WHERE u.Email IS NOT NULL
AND u.Email <> ''
AND u.Email NOT LIKE '%DELETED'
AND u.Reminder IS NOT NULL
AND u.Reminder <> 0
AND u.ReminderNextSend IS NOT NULL
AND u.ReminderNextSend <= GETDATE()
AND u.UserID = 1565"
			);
			var users = new List<User>();
			while (rs.Read()) {
				var u = new User {
					ID = GetInt32(rs, 0),
					Email = GetString(rs, 1),
					ReminderLink = GetInt32(rs, 2),
					UserKey = GetString(rs, 3),
					ReminderType = GetInt32(rs, 4),
					ReminderSettings = GetString(rs, 5),
					DT = GetDateTime(rs, 6),
					LID = GetInt32(rs, 7)
				};
				users.Add(u);
			}
			return users;
		}
		
		public void UpdateUserKey()
		{
			exec(
				@"
UPDATE [User] SET UserKey = NEWID()
WHERE Email IS NOT NULL
AND ReminderLink = 2
AND Email <> ''
AND Email NOT LIKE '%DELETED'
AND Reminder IS NOT NULL
AND Reminder <> 0
AND ReminderNextSend IS NOT NULL
AND ReminderNextSend <= GETDATE()"
			);
		}
		
		public SystemSettings GetSystemSettings()
		{
			SqlDataReader rs = recordSet(
				@"
SELECT
sl.ReminderMessage,
sl.ReminderSubject,
s.ReminderEmail,
sl.LID,
sl.ReminderAutoLogin
FROM SystemSettings s
INNER JOIN SystemSettingsLang sl ON s.SystemID = sl.SystemID
WHERE s.SystemID = 1"
			);
			SystemSettings s = null;
			if (rs.Read()) {
				s.ReminderEmail = GetString(rs, 2);
				do {
					var l = new SystemSettingsLang {
						ReminderMessage = GetString(rs, 0),
						ReminderSubject = GetString(rs, 1),
						ReminderAutoLogin = GetString(rs, 2)
					};
					s.Languages.Add(l);
				} while (rs.Read());
			}
			return s;
		}
	}
	
	public class User
	{
		public int ID { get; set; }
		public string Email { get; set; }
		public int ReminderLink { get; set ;}
		public string UserKey { get; set; }
		public int ReminderType { get; set; }
		public string ReminderSettings { get; set; }
		public DateTime DT { get; set; }
		public int LID { get; set; }
		public int UserSponsorExtendedSurveyID { get; set; }
		public int AnswerID { get; set; }
	}
	
	public class Sponsor
	{
		public string LoginTxt { get; set; }
		public string LoginSubject { get; set; }
		public int LoginDays { get; set; }
		public int LoginWeekday { get; set; }
		public int ID { get; set; }
	}
	
	public class SystemSettings
	{
		public string ReminderEmail { get; set; }
		public List<SystemSettingsLang> Languages { get; set; }
		
		public SystemSettings()
		{
			Languages = new List<SystemSettingsLang>();
		}
	}
	
	public class SystemSettingsLang
	{
		public string ReminderMessage { get; set; }
		public string ReminderSubject { get; set; }
		public string ReminderAutoLogin { get; set; }
	}
}
