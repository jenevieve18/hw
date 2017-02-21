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
		public static string GcmAPIKey {
			get { return ConfigurationManager.AppSettings["GcmAPIKey"]; }
		}
		
		public static string GcmSenderId {
			get { return ConfigurationManager.AppSettings["GcmSenderId"]; }
		}
		
		public static string GcmMessage {
			get { return ConfigurationManager.AppSettings["GcmMessage"]; }
		}
		
		public static void sendGcmNotification(IRepo repo, List<string> registrationIds, string apiKey, string senderId, string title, string body, int userId, string userKey)
		{
			if (registrationIds.Count > 0) {
				userKey = userKey.Length >= 12 ? userKey.Substring(0, 12) : userKey;
				string keyAndUserID = userKey + userId.ToString();
				
				var config = new GcmConfiguration(senderId, apiKey, null);

				var gcmBroker = new GcmServiceBroker(config);

				gcmBroker.OnNotificationFailed += (notification, aggregateEx) => {

					aggregateEx.Handle(
						ex => {

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

//							Console.WriteLine("Device RegistrationId Expired: {0}", oldId);
								Console.WriteLine("Device Registration ID expired.");
								
//							Console.WriteLine("Removing Registration ID {0} from the database...", oldId);
								Console.WriteLine("Removing Registration ID from the database...");

//							repo.UpdateUserRegistrationID(userId, userKey);
								repo.RemoveUserRegistrationID(userId, oldId);
								
								if (!string.IsNullOrWhiteSpace(newId)) {
									Console.WriteLine("Device RegistrationId Changed To: {0}", newId);
									
									Console.WriteLine("Update Registration ID from {0} to {1}...", oldId, newId);
									
									repo.ddd(userId, userKey);
								}
							} else if (ex is RetryAfterException) {
								var retryException = (RetryAfterException)ex;
								Console.WriteLine("GCM Rate Limited, don't send more until after {0}", retryException.RetryAfterUtc);
							} else {
								Console.WriteLine("GCM Notification Failed for some unknown reason");
							}

							return true;
						}
					);
				};

				gcmBroker.OnNotificationSucceeded += (notification) => {
					Console.WriteLine("GCM Notification Sent!");
				};

				gcmBroker.Start();


//			foreach (var registrationId in registrationIds) {
//				gcmBroker.QueueNotification(
//					new GcmNotification {
//						RegistrationIds = new List<string> {
//							registrationId
//						},
//						Notification = JObject.Parse("{ 'sound': 'default', 'badge': '1', 'title': '" + title + "', 'body': '" + body + "', 'click_action': 'se.healthwatch.HealthWatch.NotificationClick' }"),
//						Priority = GcmNotificationPriority.High,
//						Data = JObject.Parse("{ 'userKey': '" + keyAndUserID + "' }")
//					}
//				);
//			}
				
				gcmBroker.QueueNotification(
					new GcmNotification {
						RegistrationIds = registrationIds,
						Notification = JObject.Parse("{ 'sound': 'default', 'badge': '1', 'title': '" + title + "', 'body': '" + body + "', 'click_action': 'se.healthwatch.HealthWatch.NotificationClick' }"),
						Priority = GcmNotificationPriority.High,
						Data = JObject.Parse("{ 'userKey': '" + keyAndUserID + "' }")
					}
				);

				gcmBroker.Stop();
			}
		}
	}
}
