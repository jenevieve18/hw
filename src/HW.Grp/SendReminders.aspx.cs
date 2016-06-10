﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Models;
using Newtonsoft.Json.Linq;
using PushSharp.Core;
using PushSharp.Google;

namespace HW.Grp
{
    public partial class SendReminders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        	Main();
        }
        
        void Main()
        {
        	var apiKey = "AIzaSyB3ne08mvULbQX8HalX-qRGQtP1Ih9bqDY";
			var senderId = "59929247886";
			var message = "Reminder";

			var smtp = new SmtpWrapper(new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]));

			var repo = new Repo();
			
			var settings = repo.GetSystemSettings();

			if (Helper.isEmail(settings.ReminderEmail)) {
				Console.WriteLine("Updating keys");

				repo.UpdateUserKey();

				Console.WriteLine("Sending personal reminders");

				foreach (var u in repo.GetUsers()) {
					bool badEmail = false;
					if (Helper.isEmail(u.Email)) {
						try {
							string personalReminderMessage = settings.Languages[u.LID - 1].ReminderMessage;
							//string personalLink = "https://www.healthwatch.se";
                            string personalLink = ConfigurationManager.AppSettings["healthWatchURL"];
							if (u.ReminderLink > 0) {
//								personalLink += "/c/" + u.UserKey.ToLower() + u.Id.ToString();
                            	personalLink += "c/" + u.UserKey.ToLower() + u.Id.ToString();
							}
							if (personalReminderMessage.IndexOf("<LINK/>") >= 0) {
								personalReminderMessage = personalReminderMessage.Replace("<LINK/>", personalLink);
							} else {
								personalReminderMessage += "\r\n\r\n" + personalLink;
							}
							if (u.ReminderLink == 0) {
								personalReminderMessage += "\r\n\r\n" + settings.Languages[u.LID - 1].ReminderAutoLogin;
							}
							MailMessage mail = new MailMessage(settings.ReminderEmail, u.Email, settings.Languages[u.LID - 1].ReminderSubject, personalReminderMessage);
							smtp.Send(mail);
							
							var registrationIds = repo.GetUserRegistrationIDs(u.Id);
							Helper.sendGcmNotification(repo, registrationIds, apiKey, senderId, message, u.Id, u.UserKey);

							repo.demyo(
								u.Id,
								personalReminderMessage,
								Helper.nextReminderSend(u.ReminderType, u.ReminderSettings.Split(':'), u.DT, DateTime.Now),
								settings.Languages[u.LID - 1].ReminderSubject.Replace("'", "''")
							);

							Console.WriteLine(u.Email);
						} catch (Exception ex) {
							badEmail = true;
						}
					} else {
						badEmail = true;
					}
					if (badEmail) {
						repo.UpdateEmailFailure(u.Id);
					}
				}
			}

			if (DateTime.Now.Hour == 8 && Helper.isEmail(settings.ReminderEmail)) {
				Console.WriteLine("Sending company reminders");

				foreach (var s in repo.otot()) {
					string reminderMessage = s.LoginText;
					string reminderSubject = s.LoginSubject;

					if (
						s.LoginWeekDay == 0
						||
						s.LoginWeekDay == 1 && DateTime.Now.DayOfWeek == DayOfWeek.Monday
						||
						s.LoginWeekDay == 2 && DateTime.Now.DayOfWeek == DayOfWeek.Tuesday
						||
						s.LoginWeekDay == 3 && DateTime.Now.DayOfWeek == DayOfWeek.Wednesday
						||
						s.LoginWeekDay == 4 && DateTime.Now.DayOfWeek == DayOfWeek.Thursday
						||
						s.LoginWeekDay == 5 && DateTime.Now.DayOfWeek == DayOfWeek.Friday
					) {
						repo.aaa(s.Id);

						repo.bbb(s);

						foreach (var u in repo.ototize(s.Id, (s.LoginWeekDay == 0 && s.LoginDays != 1 ? "7" : "1"), s.LoginDays.Value)) {
							bool badEmail = false;
							if (Helper.isEmail(u.Email)) {
								try {
									string personalReminderMessage = reminderMessage;
									string personalLink = "https://www.healthwatch.se";
									if (u.ReminderLink > 0) {
										personalLink += "/c/" + u.UserKey.ToLower() + u.Id.ToString();
									}
									if (personalReminderMessage.IndexOf("<LINK/>") >= 0) {
										personalReminderMessage = personalReminderMessage.Replace("<LINK/>", personalLink);
									} else {
										personalReminderMessage += "\r\n\r\n" + personalLink;
									}
									if (u.ReminderLink == 0) {
										personalReminderMessage += "\r\n\r\n" + settings.Languages[u.Id - 1];
									}
									MailMessage mail = new MailMessage(settings.ReminderEmail, u.Email, reminderSubject, personalReminderMessage);
									smtp.Send(mail);
									
									var registrationIds = repo.GetUserRegistrationIDs(u.Id);
									Helper.sendGcmNotification(repo, registrationIds, apiKey, senderId, message, u.Id, u.UserKey);

									repo.eee(u.Id, reminderSubject, personalReminderMessage);

									Console.WriteLine(u.Email);
								} catch (Exception) {
									badEmail = true;
								}
							} else {
								badEmail = true;
							}
							if (badEmail) {
								repo.fff(u.Id);
							}
						}
					}
				}
			}

			Console.WriteLine("Check and mark finished extended surveys not marked as finished");
			foreach (var u in repo.ggg()) {
				Console.WriteLine(u.Email + ", " + u.UserSponsorExtendedSurveyID + ", " + u.AnswerID);
				repo.hhh(u.AnswerID, u.UserSponsorExtendedSurveyID);
			}
        }
    }
    
    public class BaseRepository
	{
		protected SqlDataReader recordSet(string sqlString)
		{
            //SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
            SqlConnection dataConnection = new SqlConnection(ConfigurationManager.AppSettings["SqlConnection"]);
			dataConnection.Open();
			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
			SqlDataReader dataReader = dataCommand.ExecuteReader(CommandBehavior.CloseConnection);
			return dataReader;
		}

		protected void exec(string sqlString)
		{
            //SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
            SqlConnection dataConnection = new SqlConnection(ConfigurationManager.AppSettings["SqlConnection"]);
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
		void ccc(int userID, string userKey);
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
					Id = GetInt32(rs, 0),
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
					LoginText = GetString(rs, 0),
					LoginSubject = GetString(rs, 1),
					LoginDays = GetInt32(rs, 2),
					LoginWeekDay = GetInt32(rs, 3),
					Id = GetInt32(rs, 4),
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
				"WHERE SponsorID = " + s.Id + " " +
				"AND ReminderLink = 2 " +
				"AND Email IS NOT NULL " +
				"AND Email <> '' " +
				"AND Email NOT LIKE '%DELETED' " +
				"AND (ReminderLastSent IS NULL OR DATEADD(d," +
//				(rs2.GetInt32(3) == 0 && rs2.GetInt32(2) != 1 ? "7" : "1") +	// If check every day (0) and not send every day then push minimum reminder interval to 7 else 1
				(s.LoginWeekDay == 0 && s.LoginDays != 1 ? "7" : "1") +	// If check every day (0) and not send every day then push minimum reminder interval to 7 else 1
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
		
		public void ccc(int userID, string userKey)
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
				"WHERE UserID = " + userID
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
--AND u.UserID = 1565"
			);
			var users = new List<User>();
			while (rs.Read()) {
				var u = new User {
					Id = GetInt32(rs, 0),
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
                s = new SystemSettings();
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
	
//	public class User
//	{
//		public int ID { get; set; }
//		public string Email { get; set; }
//		public int ReminderLink { get; set ;}
//		public string UserKey { get; set; }
//		public int ReminderType { get; set; }
//		public string ReminderSettings { get; set; }
//		public DateTime DT { get; set; }
//		public int LID { get; set; }
//		public int UserSponsorExtendedSurveyID { get; set; }
//		public int AnswerID { get; set; }
//	}
//	
//	public class Sponsor
//	{
//		public string LoginTxt { get; set; }
//		public string LoginSubject { get; set; }
//		public int LoginDays { get; set; }
//		public int LoginWeekday { get; set; }
//		public int ID { get; set; }
//	}
	
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
	
	public class Helper
	{
		public Helper()
		{
		}

        public static bool isEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return true;
            else
                return false;
        }

		public static string nextReminderSend(int type, string[] settings, DateTime lastLogin, DateTime lastSend)
		{
			DateTime nextPossibleReminderSend = lastSend.Date.AddHours(Convert.ToInt32(settings[0]));
			while (nextPossibleReminderSend <= DateTime.Now.AddMinutes(30)) {
				nextPossibleReminderSend = nextPossibleReminderSend.AddDays(1);
			}
			DateTime nextReminderSend = nextPossibleReminderSend.AddYears(10);

			try {
				switch (type) {
					case 1:
						System.DayOfWeek[] dayOfWeek = { System.DayOfWeek.Monday, System.DayOfWeek.Tuesday, System.DayOfWeek.Wednesday, System.DayOfWeek.Thursday, System.DayOfWeek.Friday, System.DayOfWeek.Saturday, System.DayOfWeek.Sunday };

						switch (Convert.ToInt32(settings[1])) {
							case 1:
								{
									string[] days = settings[2].Split(',');
									foreach (string day in days) {
										DateTime tmp = nextPossibleReminderSend;
										while (tmp.DayOfWeek != dayOfWeek[Convert.ToInt32(day) - 1]) {
											tmp = tmp.AddDays(1);
										}
										if (tmp < nextReminderSend) {
											nextReminderSend = tmp;
										}
									}
									break;
								}
							case 2:
								{
									nextReminderSend = nextPossibleReminderSend.AddDays(7 * (Convert.ToInt32(settings[3]) - 1));
									while (nextReminderSend.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[2]) - 1]) {
										nextReminderSend = nextReminderSend.AddDays(1);
									}
									break;
								}
							case 3:
								{
									DateTime tmp = nextPossibleReminderSend.AddDays(-nextPossibleReminderSend.Day);
									int i = 0;
									while (tmp.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[3]) - 1] || i != Convert.ToInt32(settings[2])) {
										tmp = tmp.AddDays(1);
										if (tmp.DayOfWeek == dayOfWeek[Convert.ToInt32(settings[3]) - 1]) {
											i++;
										}
									}
									nextReminderSend = nextPossibleReminderSend.AddMonths((Convert.ToInt32(settings[4]) - 1));
									if (tmp < nextPossibleReminderSend) {
										// Has allready occurred this month
										nextReminderSend = nextReminderSend.AddMonths(1);
									}
									nextReminderSend = nextReminderSend.AddDays(-nextReminderSend.Day);
									i = 0;
									while (nextReminderSend.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[3]) - 1] || i != Convert.ToInt32(settings[2])) {
										nextReminderSend = nextReminderSend.AddDays(1);
										if (nextReminderSend.DayOfWeek == dayOfWeek[Convert.ToInt32(settings[3]) - 1]) {
											i++;
										}
									}
									break;
								}
						}
						break;
					case 2:
						nextReminderSend = lastLogin.Date.AddHours(Convert.ToInt32(settings[0])).AddDays(Convert.ToInt32(settings[1]) * Convert.ToInt32(settings[2]));
						while (nextReminderSend < nextPossibleReminderSend) {
							nextReminderSend = nextReminderSend.AddDays(7);
						}
						break;
				}
			} catch (Exception) {
				nextReminderSend = nextPossibleReminderSend.AddYears(10);
			}

			return nextReminderSend.ToString("yyyy-MM-dd HH:mm");
		}
		
		public static void sendGcmNotification(IRepo repo, List<string> registrationIds, string apiKey, string senderId, string message, int userId, string userKey)
		{
			userKey = userKey.Length >= 12 ? userKey.Substring(0, 12) : userKey;
			string keyAndUserID = userKey + userId.ToString();
			
            var config = new GcmConfiguration(senderId, apiKey, null);

            var gcmBroker = new GcmServiceBroker(config);

            gcmBroker.OnNotificationFailed += (notification, aggregateEx) => {

                aggregateEx.Handle(ex => {

                    if (ex is GcmNotificationException) {
                        var notificationException = (GcmNotificationException)ex;

                        var gcmNotification = notificationException.Notification;
                        var description = notificationException.Description;

						Console.WriteLine("GCM Notification Failed: ID={0}, Desc={1}", gcmNotification.MessageId, description);
                    } else if (ex is GcmMulticastResultException) {
                        var multicastException = (GcmMulticastResultException)ex;

                        foreach (var succeededNotification in multicastException.Succeeded) {
							Console.WriteLine("GCM Notification Failed: ID={0}", succeededNotification.MessageId);
                        }

                        foreach (var failedKvp in multicastException.Failed) {
                            var n = failedKvp.Key;
                            var en = failedKvp.Value;

							Console.WriteLine("GCM Notification Failed: ID={0}, Desc={1}", n.MessageId, en.Data);
                        }

                    } else if (ex is DeviceSubscriptionExpiredException) {
                        var expiredException = (DeviceSubscriptionExpiredException)ex;

                        var oldId = expiredException.OldSubscriptionId;
                        var newId = expiredException.NewSubscriptionId;

						Console.WriteLine("Device RegistrationId Expired: {0}", oldId);
						
						Console.WriteLine("Removing Registration ID {0} from the database...", oldId);
						
//						exec(
//							"UPDATE dbo.UserRegistrationID SET UserID = " + -userId + " " +
//							"WHERE UserID = " + userId + " " +
//							"AND RegistrationID = '" + userKey.Replace("'", "") + "'"
//						);
						repo.ccc(userId, userKey);

                        if (!string.IsNullOrWhiteSpace(newId)) {
							Console.WriteLine("Device RegistrationId Changed To: {0}", newId);
							
							Console.WriteLine("Update Registration ID from {0} to {1}...", oldId, newId);
							
//							exec(
//								"INSERT INTO dbo.UserRegistrationID(UserID, RegistrationID) " +
//								"VALUES(" + userId + ", '" + userKey.Replace("'", "") + "')"
//							);
							repo.ddd(userId, userKey);
                        }
                    } else if (ex is RetryAfterException) {
                        var retryException = (RetryAfterException)ex;
						Console.WriteLine("GCM Rate Limited, don't send more until after {0}", retryException.RetryAfterUtc);
                    } else {
                        Console.WriteLine("GCM Notification Failed for some unknown reason");
                    }

                    return true;
                });
            };

            gcmBroker.OnNotificationSucceeded += (notification) => {
                Console.WriteLine("GCM Notification Sent!");
            };

            gcmBroker.Start();


            foreach (var registrationId in registrationIds) {
                gcmBroker.QueueNotification(new GcmNotification {
                    RegistrationIds = new List<string> {
                                registrationId
                    },
                	Notification = JObject.Parse("{ 'sound': 'default', 'badge': '1', 'title': 'HealthWatch', 'body': '" + message + "', 'click_action': 'se.healthwatch.HealthWatch.NotificationClick'}"),
                    Priority = GcmNotificationPriority.High,
                    Data = JObject.Parse("{ 'userKey': '" + keyAndUserID + "' }")
                });
            }

            gcmBroker.Stop();
        }
	}
	
	
	
	
	
	
	
	public class SmtpStub : ISmtp
	{
		public SmtpStub(SmtpClient smtp)
		{
		}
		
		public void Send(MailMessage mail)
		{
		}
	}
	
	public class RepoStub : IRepo
	{
		public SystemSettings GetSystemSettings()
		{
			var s = new SystemSettings {
				ReminderEmail = "reminder@healthwatch.se",
			};
			
			s.Languages.Add(
				new SystemSettingsLang {
					ReminderMessage = @"Här kommer en påminnelse om att logga in på HealthWatch.
	
	<LINK/>
	
	Om du inte vill ha fler påminnelser så kan du stänga av dessa genom att logga in och välja Påminnelser på menyn längst upp.
	
	Med vänliga hälsningar,
	HealthWatch",
					ReminderSubject = "Påminnelse från HealthWatch",
					ReminderAutoLogin = "Tips! Du ställa in så att länken ovan loggar in dig automatiskt utan att ange användarnamn och lösenord. Denna inställning hittar du också under menyalternativet Påminnelser efter att du loggat in.",
				}
			);
			
			s.Languages.Add(
				new SystemSettingsLang {
					ReminderMessage = @"This is a reminder to log on to HealthWatch.
	
	<LINK/>
	
	If you want no further reminders, please log in and turn them off under Reminders on the top menu.
	
	Best regards,
	HealthWatch",
					ReminderSubject = "Reminder from HealthWatch",
					ReminderAutoLogin = "Please note! The link above can be customized to log you on automatically. This is also done in the Reminders section after login."
				}
			);
			return s;
		}
		
		public List<User> ggg()
		{
			var users = new List<User>();
			return users;
		}
		
		public List<User> GetUsers()
		{
			var users = new List<User>();
			users.Add(
				new User {
					Id = 17429,
					UserKey = "d4469cf8-a9c1-474f-a5c3-148f107e432d".Replace("-", "").Substring(0, 12),
					ReminderLink = 2,
					Email = "customer@localhost.com",
					LID = 1,
					ReminderSettings = "17:1:3"
				});
			return users;
		}
		
		public void UpdateUserKey()
		{
		}
		
		public void demyo(int userID, string personalReminderMessage, string xxx, string yyy)
		{
		}
		
		public void UpdateEmailFailure(int userID)
		{
		}
		
		public List<Sponsor> otot()
		{
			var sponsors = new List<Sponsor>();
			sponsors.Add(
				new Sponsor {
					LoginText = "LoginTxt2",
					LoginSubject = "LoginSubject2"
				}
			);
			return sponsors;
		}
		
		public void hhh(int answerID, int userSponsorExtendedSurveyID)
		{
			throw new NotImplementedException();
		}
		
		public List<User> ototize(int sponsorID, string ddd, int userID)
		{
			var users = new List<User>();
			users.Add(
				new User {
					Id = 17429,
					UserKey = "d4469cf8-a9c1-474f-a5c3-148f107e432d".Replace("-", "").Substring(0, 12),
					Email = "customer@localhost.com",
					ReminderLink = 2,
					LID = 1
				}
			);
			return users;
		}
		
		public List<string> GetUserRegistrationIDs(int userID)
		{
			var ids = new List<string>();
			ids.Add("cLrKoHsz4zw:APA91bFpZnwCmmXhZN0JongzTbZPx2f9OGyLVBMO-joT-MXP0u-U73w6RQPOUXqNDCEcH7XgiaDUNdKQsY3nIToyX4Km6UyU1NgI6YV56eL_xISSPPlHPawRHexbf_Yyyiiev6tygXPE"); // Ian's Android Phone
	//			ids.Add("e4elA3Xu7Eg:APA91bGZ9lzk1XQoucveFngdRVtk0atB4TWvfbhN_Zp3LwjfWuBiDfLjZo4sBVi8-JJIObCvOGz8hgMiNLt9i-ttLBLw3_hr-tE0oqRVIAfU7SyjXGQliIk6sKiIE-bbVXOKPhXBxnBe"); // Nino's Android Phone
	//			ids.Add("djS9ID_BN18:APA91bGCXkzSGaRqaKlQWw5qwZYRlFY1i9AsQvXBG4imBxphoVD7vEv0BlNQVKPedqVJSVT8AEdd9BARzajkANlNSF2fexHaNwhB-hD91yLX_OL_rNVRVgLQC48Ne9la1G6hRcgRiUpi"); // Nino's Apple Phone
			return ids;
		}
		
		public void fff(int userID)
		{
		}
		
		public void eee(int userID, string reminderSubject, string personalReminderMessage)
		{
		}
		
		public void ddd(int userID, string userKey)
		{
			throw new NotImplementedException();
		}
		
		public void ccc(int userID, string userKey)
		{
			throw new NotImplementedException();
		}
		
		public void bbb(Sponsor s)
		{
		}
		
		public void aaa(int sponsorID)
		{
		}
	}
}