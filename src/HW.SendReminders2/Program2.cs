using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
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
	class Program2
	{
		static void Main(string[] args)
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
							string personalLink = "https://www.healthwatch.se";
							if (u.ReminderLink > 0) {
								personalLink += "/c/" + u.UserKey.ToLower() + u.ID.ToString();
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
							
							var registrationIds = repo.GetUserRegistrationIDs(u.ID);
							Helper.sendGcmNotification(repo, registrationIds, apiKey, senderId, message, u.ID, u.UserKey);

							repo.demyo(
								u.ID,
								personalReminderMessage,
								Helper.nextReminderSend(u.ReminderType, u.ReminderSettings.Split(':'), u.DT, DateTime.Now),
								settings.Languages[u.LID - 1].ReminderSubject.Replace("'", "''")
							);

							Console.WriteLine(u.Email);
						} catch (Exception) {
							badEmail = true;
						}
					} else {
						badEmail = true;
					}
					if (badEmail) {
						repo.UpdateEmailFailure(u.ID);
					}
				}
			}

			if (DateTime.Now.Hour == 8 && Helper.isEmail(settings.ReminderEmail)) {
				Console.WriteLine("Sending company reminders");

				foreach (var s in repo.otot()) {
					string reminderMessage = s.LoginTxt;
					string reminderSubject = s.LoginSubject;

					if (
						s.LoginWeekday == 0
						||
						s.LoginWeekday == 1 && DateTime.Now.DayOfWeek == DayOfWeek.Monday
						||
						s.LoginWeekday == 2 && DateTime.Now.DayOfWeek == DayOfWeek.Tuesday
						||
						s.LoginWeekday == 3 && DateTime.Now.DayOfWeek == DayOfWeek.Wednesday
						||
						s.LoginWeekday == 4 && DateTime.Now.DayOfWeek == DayOfWeek.Thursday
						||
						s.LoginWeekday == 5 && DateTime.Now.DayOfWeek == DayOfWeek.Friday
					) {
						repo.aaa(s.ID);

						repo.bbb(s);

						foreach (var u in repo.ototize(s.ID, (s.LoginWeekday == 0 && s.LoginDays != 1 ? "7" : "1"), s.LoginDays)) {
							bool badEmail = false;
							if (Helper.isEmail(u.Email)) {
								try {
									string personalReminderMessage = reminderMessage;
									string personalLink = "https://www.healthwatch.se";
									if (u.ReminderLink > 0) {
										personalLink += "/c/" + u.UserKey.ToLower() + u.ID.ToString();
									}
									if (personalReminderMessage.IndexOf("<LINK/>") >= 0) {
										personalReminderMessage = personalReminderMessage.Replace("<LINK/>", personalLink);
									} else {
										personalReminderMessage += "\r\n\r\n" + personalLink;
									}
									if (u.ReminderLink == 0) {
										personalReminderMessage += "\r\n\r\n" + settings.Languages[u.ID - 1];
									}
									MailMessage mail = new MailMessage(settings.ReminderEmail, u.Email, reminderSubject, personalReminderMessage);
									smtp.Send(mail);
									
									var registrationIds = repo.GetUserRegistrationIDs(u.ID);
									Helper.sendGcmNotification(repo, registrationIds, apiKey, senderId, message, u.ID, u.UserKey);

									repo.eee(u.ID, reminderSubject, personalReminderMessage);

									Console.WriteLine(u.Email);
								} catch (Exception) {
									badEmail = true;
								}
							} else {
								badEmail = true;
							}
							if (badEmail) {
								repo.fff(u.ID);
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
}