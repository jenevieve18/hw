using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections.Generic;

using PushSharp;
using PushSharp.Google;
using PushSharp.Core;
using PushSharp.Apple;
using Newtonsoft.Json.Linq;

namespace HW.SendReminders2
{
	class Program
	{
		static void Main(string[] args)
        {
            string[] reminderMessageLang = new string[2], reminderSubjectLang = new string[2], reminderAutoLoginLang = new string[2];
            string reminderEmail = "";
            
            var apiKey = "AIzaSyB3ne08mvULbQX8HalX-qRGQtP1Ih9bqDY";
            var senderId = "59929247886";
            var message = "Reminder";

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]);

            SqlDataReader rs = recordSet("SELECT " +
                "sl.ReminderMessage, " +
                "sl.ReminderSubject, " +
                "s.ReminderEmail, " +
                "sl.LID, " +
                "sl.ReminderAutoLogin " +
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
                    "ISNULL(u.LID,1) " +
                    "FROM [User] u " +
                    "WHERE u.Email IS NOT NULL " +
                    "AND u.Email <> '' " +
					"AND u.Email NOT LIKE '%DELETED' " +
                    "AND u.Reminder IS NOT NULL " +
                    "AND u.Reminder <> 0 " +
					"AND u.ReminderNextSend IS NOT NULL " +
                    "AND u.ReminderNextSend <= GETDATE()");
                while (rs.Read())
                {
                    bool badEmail = false;
                    if (isEmail(rs.GetString(1)))
                    {
                        try
                        {
                            string personalReminderMessage = reminderMessageLang[rs.GetInt32(7) - 1];
							string personalLink = "https://www.healthwatch.se";
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
                            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(reminderEmail, rs.GetString(1), reminderSubjectLang[rs.GetInt32(7) - 1], personalReminderMessage);
                            smtp.Send(mail);
                            
                            List<string> registrationIds = new List<string>();
                            using (var rs2 = recordSet(
                            	"SELECT UserRegistrationID, " +
                            	"UserID, " +
                            	"RegistrationID " +
                            	"FROM dbo.UserRegistrationID " +
                            	"WHERE UserID = " + rs.GetInt32(0)
                            )) {
                            	while (rs2.Read()) {
                            		if (!rs2.IsDBNull(2)) {
                            			registrationIds.Add(rs2.GetString(2));
                            		}
                            	}
                            }
                            string keyAndUserID = rs.GetString(3).ToLower() + rs.GetInt32(0).ToString();
                            sendGcmNotification(registrationIds, apiKey, senderId, message, keyAndUserID);

                            exec("UPDATE [User] SET ReminderLastSent = GETDATE(), ReminderNextSend = '" + nextReminderSend(rs.GetInt32(4),rs.GetString(5).Split(':'),(rs.IsDBNull(6) ? DateTime.Now : rs.GetDateTime(6)),DateTime.Now) + "' WHERE UserID = " + rs.GetInt32(0));
                            exec("INSERT INTO Reminder (UserID,Subject,Body) VALUES (" + rs.GetInt32(0) + ",'" + reminderSubjectLang[rs.GetInt32(7) - 1].Replace("'", "''") + "','" + personalReminderMessage.Replace("'", "''") + "')");

							Console.WriteLine(rs.GetString(1));
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
                    if (badEmail)
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
						"AND dbo.cf_daysFromLastLogin(UserID) >= " + rs2.GetInt32(2));

						rs = recordSet("SELECT " +
							"u.UserID, " +
							"u.Email, " +
							"u.ReminderLink, " +
                            "LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12), " +
                            "ISNULL(u.LID,1) " +
							"FROM [User] u " +
                            "INNER JOIN SponsorInvite si ON u.UserID = si.UserID " +
							"WHERE u.SponsorID = " + rs2.GetInt32(4) + " " +
							"AND u.Email IS NOT NULL " +
							"AND u.Email <> '' " +
                            "AND si.StoppedReason IS NULL " +
							"AND u.Email NOT LIKE '%DELETED' " +
							"AND (u.ReminderLastSent IS NULL OR DATEADD(d," + (rs2.GetInt32(3) == 0 && rs2.GetInt32(2) != 1 ? "7" : "1") + ",u.ReminderLastSent) <= GETDATE()) " +
							"AND dbo.cf_daysFromLastLogin(u.UserID) >= " + rs2.GetInt32(2));
						while (rs.Read())
						{
							bool badEmail = false;
							if (isEmail(rs.GetString(1)))
							{
								try
								{
									string personalReminderMessage = reminderMessage;
									string personalLink = "https://www.healthwatch.se";
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
									System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(reminderEmail, rs.GetString(1), reminderSubject, personalReminderMessage);
									smtp.Send(mail);
									
									List<string> registrationIds = new List<string>();
		                            using (var rs3 = recordSet(
		                            	"SELECT UserRegistrationID, " +
		                            	"UserID, " +
		                            	"RegistrationID " +
		                            	"FROM dbo.UserRegistrationID " +
		                            	"WHERE UserID = " + rs.GetInt32(0)
		                            )) {
		                            	while (rs3.Read()) {
		                            		if (!rs3.IsDBNull(2)) {
		                            			registrationIds.Add(rs3.GetString(2));
		                            		}
		                            	}
		                            }
									string keyAndUserID = rs.GetString(3).ToLower() + rs.GetInt32(0).ToString();
		                            sendGcmNotification(registrationIds, apiKey, senderId, message, keyAndUserID);

									exec("UPDATE [User] SET ReminderLastSent = GETDATE() WHERE UserID = " + rs.GetInt32(0));
                                    exec("INSERT INTO Reminder (UserID,Subject,Body) VALUES (" + rs.GetInt32(0) + ",'" + reminderSubject.Replace("'", "''") + "','" + personalReminderMessage.Replace("'", "''") + "')");

									Console.WriteLine(rs.GetString(1));
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
							if (badEmail)
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
		
		
//		static void sendGcmNotification(String[] registrationIds, String apiKey, String senderId, String message)
		static void sendGcmNotification(List<string> registrationIds, string apiKey, string senderId, string message, string keyAndUserID)
		{
            // Configuration
            var config = new GcmConfiguration(senderId, apiKey, null);

            // Create a new broker
            var gcmBroker = new GcmServiceBroker(config);

            // Wire up events
            gcmBroker.OnNotificationFailed += (notification, aggregateEx) => {

                aggregateEx.Handle(ex => {

                    // See what kind of exception it was to further diagnose
                    if (ex is GcmNotificationException)
                    {
                        var notificationException = (GcmNotificationException)ex;

                        // Deal with the failed notification
                        var gcmNotification = notificationException.Notification;
                        var description = notificationException.Description;

//                        Console.WriteLine($"GCM Notification Failed: ID={gcmNotification.MessageId}, Desc={description}");
						Console.WriteLine("GCM Notification Failed: ID={0}, Desc={1}", gcmNotification.MessageId, description);
                    }
                    else if (ex is GcmMulticastResultException)
                    {
                        var multicastException = (GcmMulticastResultException)ex;

                        foreach (var succeededNotification in multicastException.Succeeded)
                        {
//                            Console.WriteLine($"GCM Notification Failed: ID={succeededNotification.MessageId}");
							Console.WriteLine("GCM Notification Failed: ID={0}", succeededNotification.MessageId);
                        }

                        foreach (var failedKvp in multicastException.Failed)
                        {
                            var n = failedKvp.Key;
                            var en = failedKvp.Value;

//                            Console.WriteLine($"GCM Notification Failed: ID={n.MessageId}, Desc={en.Data}");
							Console.WriteLine("GCM Notification Failed: ID={0}, Desc={1}", n.MessageId, en.Data);
                        }

                    }
                    else if (ex is DeviceSubscriptionExpiredException)
                    {
                        var expiredException = (DeviceSubscriptionExpiredException)ex;

                        var oldId = expiredException.OldSubscriptionId;
                        var newId = expiredException.NewSubscriptionId;

//                        Console.WriteLine($"Device RegistrationId Expired: {oldId}");
						Console.WriteLine("Device RegistrationId Expired: {0}", oldId);

                        if (!string.IsNullOrWhiteSpace(newId))
                        {
                            // If this value isn't null, our subscription changed and we should update our database
//                            Console.WriteLine($"Device RegistrationId Changed To: {newId}");
							Console.WriteLine("Device RegistrationId Changed To: {0}", newId);
                        }
                    }
                    else if (ex is RetryAfterException)
                    {
                        var retryException = (RetryAfterException)ex;
                        // If you get rate limited, you should stop sending messages until after the RetryAfterUtc date
//                        Console.WriteLine($"GCM Rate Limited, don't send more until after {retryException.RetryAfterUtc}");
						Console.WriteLine("GCM Rate Limited, don't send more until after {0}", retryException.RetryAfterUtc);
                    }
                    else
                    {
                        Console.WriteLine("GCM Notification Failed for some unknown reason");
                    }

                    // Mark it as handled
                    return true;
                });
            };

            gcmBroker.OnNotificationSucceeded += (notification) => {
                Console.WriteLine("GCM Notification Sent!");
            };

            // Start the broker
            gcmBroker.Start();


            foreach (var registrationId in registrationIds)
            {
                // Queue a notification to send
                gcmBroker.QueueNotification(new GcmNotification
                {
                    RegistrationIds = new List<string> {
                                registrationId
                    },
//                    Notification = JObject.Parse("{\"sound\": \"default\",\"title\": \"HealthWatch\",\"body\": \"" + message + "\"}"),
Notification = JObject.Parse("{\"sound\": \"default\",\"title\": \"HealthWatch\",\"body\": \"" + message + "\", 'userKey': '" + keyAndUserID + "'}"),
                    Priority = GcmNotificationPriority.High
//                    Notification = JObject.Parse("{\"sound\": \"default\",\"title\": \"HealthWatch\",\"body\": \"" + message + "\"}")
                    //Notification = JObject.Parse("{\"sound\": \"default\",\"badge\": \"1\",\"title\": \"HealthWatch\",\"body\": \"" + message + "\"}")

                });
            }

            // Stop the broker, wait for it to finish   
            // This isn't done after every message, but after you're
            // done with the broker
            gcmBroker.Stop();
        }
        
	}
}