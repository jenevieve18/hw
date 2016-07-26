/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 5/31/2016
 * Time: 9:51 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using PushSharp.Core;
using PushSharp.Google;

namespace HW.SendReminders
{
	public static class Helper
	{
		public const string API_KEY = "AIzaSyB3ne08mvULbQX8HalX-qRGQtP1Ih9bqDY";
		public const string SENDER_ID = "59929247886";
		public const string Message = "Reminder";
		
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
}
