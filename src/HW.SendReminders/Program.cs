using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace HW.SendReminders
{
	class Program
	{
		static void Main(string[] args)
		{
			string[] reminderMessageLang = new string[4], reminderSubjectLang = new string[4], reminderAutoLoginLang = new string[4],
			reminderPushTitleLang = new string[4], reminderPushBodyLang = new string[4];
			string reminderEmail = "", backupServer = "";

			ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

			System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["SmtpServer"], (ConfigurationManager.AppSettings["SmtpPort"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]) : 25));
			if (ConfigurationManager.AppSettings["SmtpUsername"] != null && ConfigurationManager.AppSettings["SmtpPassword"] != null)
			{
				smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpUsername"], ConfigurationManager.AppSettings["SmtpPassword"]);
			}
			if (ConfigurationManager.AppSettings["SmtpSSL"] != null && Convert.ToBoolean(ConfigurationManager.AppSettings["SmtpSSL"]))
			{
				smtp.EnableSsl = true;
			}
			if (ConfigurationManager.AppSettings["BackupSmtpServer"] != null)
			{
				backupServer = "Backup";
			}
			System.Net.Mail.SmtpClient backupSmtp = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings[backupServer + "SmtpServer"], (ConfigurationManager.AppSettings[backupServer + "SmtpPort"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings[backupServer + "SmtpPort"]) : 25));
			if (ConfigurationManager.AppSettings[backupServer + "SmtpUsername"] != null && ConfigurationManager.AppSettings[backupServer + "SmtpPassword"] != null)
			{
				backupSmtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings[backupServer + "SmtpUsername"], ConfigurationManager.AppSettings[backupServer + "SmtpPassword"]);
			}
			if (ConfigurationManager.AppSettings[backupServer + "SmtpSSL"] != null && Convert.ToBoolean(ConfigurationManager.AppSettings[backupServer + "SmtpSSL"]))
			{
				backupSmtp.EnableSsl = true;
			}
			
			var repo = new Repo();

			SqlDataReader rs = recordSet("SELECT " +
			                             "sl.ReminderMessage, " +
			                             "sl.ReminderSubject, " +
			                             "s.ReminderEmail, " +
			                             "sl.LID, " +
			                             "sl.ReminderAutoLogin, " +
			                             "sl.ReminderPushTitle, " +
			                             "sl.ReminderPushBody " +
			                             "FROM SystemSettings s " +
			                             "INNER JOIN SystemSettingsLang sl ON s.SystemID = sl.SystemID " +
			                             "WHERE s.SystemID = 1");
			if (rs.Read() && !rs.IsDBNull(0) && !rs.IsDBNull(1) && !rs.IsDBNull(2))
			{
				reminderEmail = rs.GetString(2);
				do
				{
					reminderMessageLang[rs.GetInt32(3) - 1] = rs.GetString(0);
					reminderSubjectLang[rs.GetInt32(3) - 1] = rs.GetString(1);
					reminderAutoLoginLang[rs.GetInt32(3) - 1] = rs.GetString(4);
					reminderPushTitleLang[rs.GetInt32(3) - 1] = rs.GetString(5);
					reminderPushBodyLang[rs.GetInt32(3) - 1] = rs.GetString(6);
				}
				while (rs.Read());
			}
			rs.Close();

			if (isEmail(reminderEmail))
			{
				#region Updating keys
				Console.WriteLine("Updating keys");

				// Update all keys for users with variable links
				exec("UPDATE [User] SET UserKey = NEWID() " +
				     "WHERE Email IS NOT NULL " +
				     "AND ReminderLink = 2 " +
				     "AND Email <> '' " +
				     "AND Email NOT LIKE '%DELETED' " +
				     "AND Reminder IS NOT NULL " +
				     "AND Reminder <> 0 " +
				     "AND (NULLIF(AltEmail,'') IS NOT NULL OR EmailFailure IS NULL OR DATEADD(MONTH,1,EmailFailure) < GETDATE()) " +
				     "AND ReminderNextSend IS NOT NULL " +
				     "AND ReminderNextSend <= GETDATE()");
				#endregion

				#region Sending personal reminders
				Console.WriteLine("Sending personal reminders");

				rs = recordSet("SELECT " +
				               "u.UserID, " +              // 0
				               "u.Email, " +
				               "u.ReminderLink, " +
				               "LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12), " +
				               "u.ReminderType, " +
				               "u.ReminderSettings, " +    // 5
				               "(SELECT TOP 1 DT FROM Session s WHERE s.UserID = u.UserID ORDER BY SessionID DESC) AS LastLogin, " +
				               "ISNULL(u.LID,1), " +
				               "u.AltEmail, " +			// 8
				               "u.EmailFailure " +			// 9
				               "FROM [User] u " +
				               "WHERE u.Email IS NOT NULL " +
				               "AND u.Email <> '' " +
				               "AND u.Email NOT LIKE '%DELETED' " +
				               "AND u.Reminder IS NOT NULL " +
				               "AND u.Reminder <> 0 " +
				               "AND (NULLIF(u.AltEmail,'') IS NOT NULL OR u.EmailFailure IS NULL OR DATEADD(MONTH,1,u.EmailFailure) < GETDATE()) " +
				               "AND u.ReminderNextSend IS NOT NULL " +
				               "AND u.ReminderNextSend <= GETDATE() " +
				               "AND u.UserID = 17509");
				while (rs.Read())
				{
					bool badEmail = false, usingAlternative = false;
					string emailAddress = rs.GetString(1);
					if ((!isEmail(emailAddress) || !rs.IsDBNull(9) && rs.GetDateTime(9).AddMonths(1) >= DateTime.Now) && !rs.IsDBNull(8))
					{
						// If there is an alternative adress AND:
						//		- the standard email is malformatted, or
						//		- the standard email could not be sent to during the last month
						emailAddress = rs.GetString(8);
						usingAlternative = true;
					}
					if (isEmail(emailAddress))
					{
						try
						{
							string personalReminderMessage = reminderMessageLang[rs.GetInt32(7) - 1];
							string personalLink = ConfigurationManager.AppSettings["LinkUrl"];
							if (!rs.IsDBNull(2) && rs.GetInt32(2) > 0)
							{
								personalLink += "/c/" + rs.GetString(3).ToLower() + rs.GetInt32(0).ToString();
							}
							if (personalReminderMessage.IndexOf("<LINK/>") >= 0)
							{
								personalReminderMessage = personalReminderMessage.Replace("<LINK/>", personalLink);
							}
							else
							{
								personalReminderMessage += "\r\n\r\n" + personalLink;
							}
							if (rs.IsDBNull(2) || rs.GetInt32(2) == 0)
							{
								personalReminderMessage += "\r\n\r\n" + reminderAutoLoginLang[rs.GetInt32(7) - 1];
							}
							System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(reminderEmail, emailAddress, reminderSubjectLang[rs.GetInt32(7) - 1], personalReminderMessage);
							try
							{
								smtp.Send(mail);
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
								backupSmtp.Send(mail);
							}
							
							var registrationIds = repo.GetUserRegistrationIDs(rs.GetInt32(0));
//							Helper.sendGcmNotification(repo, registrationIds, Helper.GcmAPIKey, Helper.GcmSenderId, Helper.GcmMessage, rs.GetInt32(0), rs.GetString(3));
							Helper.sendGcmNotification(repo, registrationIds, Helper.GcmAPIKey, Helper.GcmSenderId, reminderPushTitleLang[rs.GetInt32(7) - 1], reminderPushBodyLang[rs.GetInt32(7) - 1], rs.GetInt32(0), rs.GetString(3));

							exec("UPDATE [User] SET " +
							     (!usingAlternative ? "EmailFailure = NULL, " : "") +
							     "ReminderLastSent = GETDATE(), " +
							     "ReminderNextSend = '" + nextReminderSend(rs.GetInt32(4), rs.GetString(5).Split(':'), (rs.IsDBNull(6) ? DateTime.Now : rs.GetDateTime(6)), DateTime.Now) + "' " +
							     "WHERE UserID = " + rs.GetInt32(0));
							exec("INSERT INTO Reminder (UserID,Subject,Body) VALUES (" + rs.GetInt32(0) + ",'" + reminderSubjectLang[rs.GetInt32(7) - 1].Replace("'", "''") + "','" + personalReminderMessage.Replace("'", "''") + "')");

							Console.WriteLine(rs.GetString(1) + (usingAlternative ? " (" + emailAddress + ")" : ""));
						}
						catch (Exception ex)
						{
							badEmail = true;
						}
					}
					else
					{
						badEmail = true;
					}
					if (badEmail && !usingAlternative)
					{
						exec("UPDATE [User] SET EmailFailure = GETDATE() WHERE UserID = " + rs.GetInt32(0));
					}
				}
				rs.Close();
				#endregion
			}

			if (DateTime.Now.Hour == 8 && isEmail(reminderEmail))
			{
				#region Sending company reminders
				Console.WriteLine("Sending company reminders");

				SqlDataReader rs2 = recordSet("SELECT " +
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
				while (rs2.Read())
				{
					string reminderMessage = rs2.GetString(0);
					string reminderSubject = rs2.GetString(1);

					if (
						rs2.GetInt32(3) == 0
						||
						rs2.GetInt32(3) == 1 && DateTime.Now.DayOfWeek == DayOfWeek.Monday
						||
						rs2.GetInt32(3) == 2 && DateTime.Now.DayOfWeek == DayOfWeek.Tuesday
						||
						rs2.GetInt32(3) == 3 && DateTime.Now.DayOfWeek == DayOfWeek.Wednesday
						||
						rs2.GetInt32(3) == 4 && DateTime.Now.DayOfWeek == DayOfWeek.Thursday
						||
						rs2.GetInt32(3) == 5 && DateTime.Now.DayOfWeek == DayOfWeek.Friday
					)
					{
						exec("UPDATE Sponsor SET LoginLastSent = GETDATE() WHERE SponsorID = " + rs2.GetInt32(4));

						// Update all keys for users with variable links
						exec("UPDATE [User] SET UserKey = NEWID() " +
						     "WHERE SponsorID = " + rs2.GetInt32(4) + " " +
						     "AND ReminderLink = 2 " +
						     "AND Email IS NOT NULL " +
						     "AND Email <> '' " +
						     "AND Email NOT LIKE '%DELETED' " +
						     "AND (ReminderLastSent IS NULL OR DATEADD(d," +
						     (rs2.GetInt32(3) == 0 && rs2.GetInt32(2) != 1 ? "7" : "1") +	// If check every day (0) and not send every day then push minimum reminder interval to 7 else 1
						     ",ReminderLastSent) <= GETDATE()) " +
						     "AND (NULLIF(AltEmail,'') IS NOT NULL OR EmailFailure IS NULL OR DATEADD(MONTH,1,EmailFailure) < GETDATE()) " +
						     "AND dbo.cf_daysFromLastLogin(UserID) >= " + rs2.GetInt32(2));

						rs = recordSet("SELECT " +
						               "u.UserID, " +
						               "u.Email, " +
						               "u.ReminderLink, " +
						               "LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12), " +
						               "ISNULL(u.LID,1), " +
						               "u.AltEmail, " +			// 5
						               "u.EmailFailure " +			// 6
						               "FROM [User] u " +
						               "INNER JOIN SponsorInvite si ON u.UserID = si.UserID AND u.SponsorID = si.SponsorID " +
						               "WHERE u.SponsorID = " + rs2.GetInt32(4) + " " +
						               "AND u.Email IS NOT NULL " +
						               "AND u.Email <> '' " +
						               "AND si.StoppedReason IS NULL " +
						               "AND u.Email NOT LIKE '%DELETED' " +
						               "AND (u.ReminderLastSent IS NULL OR DATEADD(d," + (rs2.GetInt32(3) == 0 && rs2.GetInt32(2) != 1 ? "7" : "1") + ",u.ReminderLastSent) <= GETDATE()) " +
						               "AND (NULLIF(u.AltEmail,'') IS NOT NULL OR u.EmailFailure IS NULL OR DATEADD(MONTH,1,u.EmailFailure) < GETDATE()) " +
						               "AND dbo.cf_daysFromLastLogin(u.UserID) >= " + rs2.GetInt32(2));
						while (rs.Read())
						{
							bool badEmail = false, usingAlternative = false;
							string emailAddress = rs.GetString(1);
							if ((!isEmail(emailAddress) || !rs.IsDBNull(6) && rs.GetDateTime(6).AddMonths(1) >= DateTime.Now) && !rs.IsDBNull(5))
							{
								// If there is an alternative adress AND:
								//		- the standard email is malformatted, or
								//		- the standard email could not be sent to during the last month
								emailAddress = rs.GetString(5);
								usingAlternative = true;
							}
							if (isEmail(emailAddress))
							{
								try
								{
									string personalReminderMessage = reminderMessage;
									string personalLink = ConfigurationManager.AppSettings["LinkUrl"];
									if (!rs.IsDBNull(2) && rs.GetInt32(2) > 0)
									{
										personalLink += "/c/" + rs.GetString(3).ToLower() + rs.GetInt32(0).ToString();
									}
									if (personalReminderMessage.IndexOf("<LINK/>") >= 0)
									{
										personalReminderMessage = personalReminderMessage.Replace("<LINK/>", personalLink);
									}
									else
									{
										personalReminderMessage += "\r\n\r\n" + personalLink;
									}
									if (rs.IsDBNull(2) || rs.GetInt32(2) == 0)
									{
										personalReminderMessage += "\r\n\r\n" + reminderAutoLoginLang[rs.GetInt32(4) - 1];
									}
									System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(reminderEmail, emailAddress, reminderSubject, personalReminderMessage);
									try
									{
										smtp.Send(mail);
									}
									catch (Exception ex)
									{
										Console.WriteLine(ex.Message);
										backupSmtp.Send(mail);
									}
									
									var registrationIds = repo.GetUserRegistrationIDs(rs.GetInt32(0));
//									Helper.sendGcmNotification(repo, registrationIds, Helper.GcmAPIKey, Helper.GcmSenderId, Helper.GcmMessage, rs.GetInt32(0), rs.GetString(3));
									Helper.sendGcmNotification(repo, registrationIds, Helper.GcmAPIKey, Helper.GcmSenderId, reminderPushTitleLang[rs.GetInt32(4) - 1], reminderPushBodyLang[rs.GetInt32(4) - 1], rs.GetInt32(0), rs.GetString(3));

									exec("UPDATE [User] SET " +
									     (!usingAlternative ? "EmailFailure = NULL, " : "") +
									     "ReminderLastSent = GETDATE() " +
									     "WHERE UserID = " + rs.GetInt32(0));
									exec("INSERT INTO Reminder (UserID,Subject,Body) VALUES (" + rs.GetInt32(0) + ",'" + reminderSubject.Replace("'", "''") + "','" + personalReminderMessage.Replace("'", "''") + "')");

									Console.WriteLine(rs.GetString(1) + (usingAlternative ? " (" + emailAddress + ")" : ""));
								}
								catch (Exception)
								{
									badEmail = true;
								}
							}
							else
							{
								badEmail = true;
							}
							if (badEmail && !usingAlternative)
							{
								exec("UPDATE [User] SET EmailFailure = GETDATE() WHERE UserID = " + rs.GetInt32(0));
							}
						}
						rs.Close();
					}
				}
				rs2.Close();
				#endregion
			}

			#region Check and mark finished extended surveys not marked as finished
			Console.WriteLine("Check and mark finished extended surveys not marked as finished");
			rs = recordSet("SELECT " +
			               "s.UserSponsorExtendedSurveyID, " +
			               "a.AnswerID, " +         // 1
			               "u.Email " +
			               "FROM [User] u " +
			               "INNER JOIN UserSponsorExtendedSurvey s ON u.UserID = s.UserID " +
			               "INNER JOIN eform..Answer a ON s.ProjectRoundUserID = a.ProjectRoundUserID " +
			               "WHERE s.AnswerID IS NULL AND a.EndDT IS NOT NULL");
			while (rs.Read())
			{
				Console.WriteLine(rs.GetString(2) + ", " + rs.GetInt32(0) + ", " + rs.GetInt32(1));
				exec("UPDATE UserSponsorExtendedSurvey SET AnswerID = " + rs.GetInt32(1) + " WHERE UserSponsorExtendedSurveyID = " + rs.GetInt32(0));
			}
			rs.Close();
			#endregion
		}

		public static SqlDataReader recordSet(string sqlString)
		{
			SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
			dataConnection.Open();
			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
			SqlDataReader dataReader = dataCommand.ExecuteReader(CommandBehavior.CloseConnection);
			return dataReader;
		}

		public static void exec(string sqlString)
		{
			SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
			dataConnection.Open();
			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
			dataCommand.ExecuteNonQuery();
			dataConnection.Close();
			dataConnection.Dispose();
		}

		public static bool isEmail(string inputEmail)
		{
			//string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
			//    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
			//    @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
			//Regex re = new Regex(strRegex);
			//if (re.IsMatch(inputEmail))
			//    return true;
			//else
			//    return false;
			return (new RegexUtilities()).IsValidEmail(inputEmail.Trim());
		}

		public static string nextReminderSend(int type, string[] settings, DateTime lastLogin, DateTime lastSend)
		{
			DateTime nextPossibleReminderSend = lastSend.Date.AddHours(Convert.ToInt32(settings[0]));
			while (nextPossibleReminderSend <= DateTime.Now.AddMinutes(30))
			{
				nextPossibleReminderSend = nextPossibleReminderSend.AddDays(1);
			}
			DateTime nextReminderSend = nextPossibleReminderSend.AddYears(10);

			try
			{
				switch (type)
				{
					case 1:
						System.DayOfWeek[] dayOfWeek = { System.DayOfWeek.Monday, System.DayOfWeek.Tuesday, System.DayOfWeek.Wednesday, System.DayOfWeek.Thursday, System.DayOfWeek.Friday, System.DayOfWeek.Saturday, System.DayOfWeek.Sunday };

						switch (Convert.ToInt32(settings[1]))
						{
							case 1:
								#region Weekday
								{
									string[] days = settings[2].Split(',');
									foreach (string day in days)
									{
										DateTime tmp = nextPossibleReminderSend;
										while (tmp.DayOfWeek != dayOfWeek[Convert.ToInt32(day) - 1])
										{
											tmp = tmp.AddDays(1);
										}
										if (tmp < nextReminderSend)
										{
											nextReminderSend = tmp;
										}
									}
									break;
								}
								#endregion
							case 2:
								#region Week
								{
									nextReminderSend = nextPossibleReminderSend.AddDays(7 * (Convert.ToInt32(settings[3]) - 1));
									while (nextReminderSend.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[2]) - 1])
									{
										nextReminderSend = nextReminderSend.AddDays(1);
									}
									break;
								}
								#endregion
							case 3:
								#region Month
								{
									DateTime tmp = nextPossibleReminderSend.AddDays(-nextPossibleReminderSend.Day);
									int i = 0;
									while (tmp.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[3]) - 1] || i != Convert.ToInt32(settings[2]))
									{
										tmp = tmp.AddDays(1);
										if (tmp.DayOfWeek == dayOfWeek[Convert.ToInt32(settings[3]) - 1])
										{
											i++;
										}
									}
									nextReminderSend = nextPossibleReminderSend.AddMonths((Convert.ToInt32(settings[4]) - 1));
									if (tmp < nextPossibleReminderSend)
									{
										// Has allready occurred this month
										nextReminderSend = nextReminderSend.AddMonths(1);
									}
									nextReminderSend = nextReminderSend.AddDays(-nextReminderSend.Day);
									i = 0;
									while (nextReminderSend.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[3]) - 1] || i != Convert.ToInt32(settings[2]))
									{
										nextReminderSend = nextReminderSend.AddDays(1);
										if (nextReminderSend.DayOfWeek == dayOfWeek[Convert.ToInt32(settings[3]) - 1])
										{
											i++;
										}
									}
									break;
								}
								#endregion
						}
						break;
					case 2:
						nextReminderSend = lastLogin.Date.AddHours(Convert.ToInt32(settings[0])).AddDays(Convert.ToInt32(settings[1]) * Convert.ToInt32(settings[2]));
						while (nextReminderSend < nextPossibleReminderSend)
						{
							nextReminderSend = nextReminderSend.AddDays(7);
						}
						break;
				}
			}
			catch (Exception)
			{
				nextReminderSend = nextPossibleReminderSend.AddYears(10);
			}

			return nextReminderSend.ToString("yyyy-MM-dd HH:mm");
		}

	}
}