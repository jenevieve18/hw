using System;
using System.Collections.Generic;
using System.Configuration;
using HW.Core.Helpers;
using HW.Core.Services;
using Newtonsoft.Json.Linq;
using PushSharp.Core;
using PushSharp.Google;

namespace HW.Core.Models
{
	public class ExtendedSurveyMessage : Message
	{
		int sponsorExtendedSurveyID;
		int sponsorAdminExtendedSurveyID;
		
		public int SponsorAdminExtendedSurveyID {
			get { return sponsorAdminExtendedSurveyID; }
			set { sponsorAdminExtendedSurveyID = value; }
		}
		
		public int SponsorExtendedSurveyID {
			get { return sponsorExtendedSurveyID; }
			set { sponsorExtendedSurveyID = value; }
		}
	}
	
	public class LoginReminderMessage : Message
	{
		int loginDays;
		
		public int LoginDays {
			get { return loginDays; }
			set { loginDays = value; }
		}
	}
	
	public class Message
	{
		string _from;
		string to;
		string subject;
		string body;
		int sent;
		int failed;
		
		public int Sent {
			get { return sent; }
			set { sent = value; }
		}
		
		public int Failed {
			get { return failed; }
			set { failed = value; }
		}
		
		public string From {
			get { return _from; }
			set { _from = value; }
		}
		
		public string To {
			get { return to; }
			set { to = value; }
		}
		
		public string Subject {
			get { return subject; }
			set { subject = value; }
		}
		
		public string Body {
			get { return body; }
			set { body = value; }
		}
	}
	
	public class RegistrationSendType : MessageSendType
	{
		public RegistrationSendType(MessageService service) : base(service)
		{
		}
		
		public override void Send(int sponsorID, int sponsorAdminID)
		{
			service.UpdateSponsorLastInviteSent(sponsorID, sponsorAdminID);
			foreach (var i in service.FindInvitesBySponsor(sponsorID, sponsorAdminID)) {
				bool success = Db.sendInvitation(i.Id, i.Email, Message.Subject, Message.Body, i.InvitationKey);
				if (success) {
					Message.Sent++;
				} else {
					Message.Failed++;
				}
			}
		}
	}
	
	public class RegistrationReminderSendType : MessageSendType
	{
		public RegistrationReminderSendType(MessageService repository) : base(repository)
		{
		}
		
		public override void Send(int sponsorID, int sponsorAdminID)
		{
			service.UpdateSponsorLastInviteReminderSent(sponsorID, sponsorAdminID);
			foreach (var i in service.FindSentInvitesBySponsor(sponsorID, sponsorAdminID)) {
				bool success = Db.sendInvitation(i.Id, i.Email, Message.Subject, Message.Body, i.InvitationKey);

				if (success) {
					Message.Sent++;
				} else {
					Message.Failed++;
				}
			}
		}
	}
	
	public class LoginReminderSendType : MessageSendType
	{
		public LoginReminderSendType(MessageService repository) : base(repository)
		{
		}
		
		public override void Send(int sponsorID, int sponsorAdminID)
		{
			var m = Message as LoginReminderMessage;
			service.UpdateSponsorLastLoginSent(sponsorID, sponsorAdminID);
			foreach (var u in service.FindBySponsorWithLoginDays(sponsorID, sponsorAdminID, m.LoginDays)) {
				bool success = false;
				bool badEmail = false;
				if (Db.isEmail(u.Email)) {
					try {
						string body = Message.Body;

						string path = ConfigurationManager.AppSettings["healthWatchURL"];
						string personalLink = "" + path + "";
						if (u.ReminderLink > 0) {
							personalLink += "/c/" + u.UserKey.ToLower() + u.Id.ToString();
						}
						if (body.IndexOf("<LINK/>") >= 0) {
							body = body.Replace("<LINK/>", personalLink);
						} else {
							body += "\r\n\r\n" + personalLink;
						}

						success = Db.sendMail(Message.From, u.Email, Message.Subject, body);
						
						SendPushNotification(u.Id);

						if (success) {
							service.UpdateLastReminderSent(u.Id);
						}
					} catch (Exception) {
						badEmail = true;
					}
				} else {
					badEmail = true;
				}
				if (badEmail) {
					service.UpdateEmailFailure(u.Id);
				}

				if (success) {
					Message.Sent++;
				} else {
					Message.Failed++;
				}
			}
		}
	}
	
	public class ThankYouSendType : MessageSendType
	{
		public ThankYouSendType(MessageService service) : base(service)
		{
		}
		
		public override void Send(int sponsorID, int sponsorAdminID)
		{
			var m = Message as ExtendedSurveyMessage;
			service.UpdateExtendedSurveyLastFinishedSent(m.SponsorExtendedSurveyID, m.SponsorAdminExtendedSurveyID);
			foreach (var u in service.FindBySponsorWithExtendedSurvey(sponsorID, sponsorAdminID, m.SponsorExtendedSurveyID)) {
				bool success = false;
				bool badEmail = false;
				if (Db.isEmail(u.Email)) {
					try {
						string body = Message.Body;

						string path = ConfigurationManager.AppSettings["healthWatchURL"];
						string personalLink = "" + path + "";
						if (u.ReminderLink > 0) {
							personalLink += "c/" + u.UserKey.ToLower() + u.Id.ToString();
						}
						if (body.IndexOf("<LINK/>") >= 0) {
							body = body.Replace("<LINK/>", personalLink);
						} else {
							body += "\r\n\r\n" + personalLink;
						}

						success = Db.sendMail(Message.From, u.Email, Message.Subject, body);
					} catch (Exception) {
						badEmail = true;
					}
				} else {
					badEmail = true;
				}
				if (badEmail) {
					service.UpdateEmailFailure(u.Id);
				}

				if (success) {
					Message.Sent++;
				} else {
					Message.Failed++;
				}
			}
		}
	}
	
	public class AllActivatedUsersSendType : MessageSendType
	{
		public AllActivatedUsersSendType(MessageService service) : base(service)
		{
		}
		
		public override void Send(int sponsorID, int sponsorAdminID)
		{
			service.UpdateLastAllMessageSent(sponsorID, sponsorAdminID);
			foreach (var u in service.Find2(sponsorID, sponsorAdminID)) {
				bool success = false;
				bool badEmail = false;
				if (Db.isEmail(u.Email)) {
					try {
						success = Db.sendMail(u.Email, Message.Subject, Message.Body);
//						SendPushNotification(u.Id);
					} catch (Exception) {
						badEmail = true;
					}
				} else {
					badEmail = true;
				}
				if (badEmail) {
					service.UpdateEmailFailure(u.Id);
				}

				if (success) {
					Message.Sent++;
				} else {
					Message.Failed++;
				}
			}
		}
	}
	
	public class ExtendedSurveySendType : MessageSendType
	{
		public ExtendedSurveySendType(MessageService service) : base(service)
		{
		}
		
		public override void Send(int sponsorID, int sponsorAdminID)
		{
			var m = Message as ExtendedSurveyMessage;
			service.UpdateExtendedSurveyLastEmailSent(m.SponsorExtendedSurveyID, m.SponsorAdminExtendedSurveyID);
			foreach (var u in service.FindBySponsorWithExtendedSurvey2(sponsorID, sponsorAdminID, m.SponsorExtendedSurveyID)) {
				bool success = false;
				bool badEmail = false;
				if (Db.isEmail(u.Email)) {
					try {
						string body = Message.Body;

						string path = ConfigurationManager.AppSettings["healthWatchURL"];
						string personalLink = "" + path + "";
						if (u.ReminderLink > 0) {
							personalLink += "c/" + u.UserKey.ToLower() + u.Id.ToString();
						}
						if (body.IndexOf("<LINK/>") >= 0) {
							body = body.Replace("<LINK/>", personalLink);
						} else {
							body += "\r\n\r\n" + personalLink;
						}

						success = Db.sendMail(Message.From, u.Email, Message.Subject, body);
					} catch (Exception) {
						badEmail = true;
					}
				} else {
					badEmail = true;
				}
				if (badEmail) {
					service.UpdateEmailFailure(u.Id);
				}

				if (success) {
					Message.Sent++;
				} else {
					Message.Failed++;
				}
			}
		}
	}
	
	public abstract class MessageSendType
	{
		protected MessageService service;
		Message message;
		
		public const int RegistrationSendType = 1;
		public const int RegistrationReminderSendType = 2;
		public const int LoginReminderSendType = 3;
		public const int ExtendedSurveySendType = 4;
		public const int ThankYouSendType = 5;
		public const int AllActivatedUsersSendType = 9;
		
		public Message Message {
			get { return message; }
			set { message = value; }
		}
		
		public MessageSendType(MessageService service)
		{
			this.service = service;
		}
		
		public abstract void Send(int sponsorID, int sponsorAdminID);
		
		protected void SendPushNotification(int userID)
		{
			var apiKey = "AIzaSyB3ne08mvULbQX8HalX-qRGQtP1Ih9bqDY";
			var senderId = "59929247886";
			var message = "Reminder";

			var registrationIds = service.GetUserRegistrationIDs(userID);
			SendGcmNotification(registrationIds, apiKey, senderId, message, userID, "");
		}
		
		void SendGcmNotification(List<string> registrationIds, string apiKey, string senderId, string message, int userId, string userKey)
		{
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

							Console.WriteLine("Device RegistrationId Expired: {0}", oldId);
							Console.WriteLine("Removing Registration ID {0} from the database...", oldId);

//						exec(
//							"UPDATE dbo.UserRegistrationID SET UserID = " + -userId + " " +
//							"WHERE UserID = " + userId + " " +
//							"AND RegistrationID = '" + userKey.Replace("'", "") + "'"
//						);
//						repo.ccc(userId, userKey);

							Db.exec(
								"UPDATE dbo.UserRegistrationID SET UserID = " + -userId + " " +
								"WHERE UserID = " + userId + " " +
								"AND RegistrationID = '" + userKey.Replace("'", "") + "'"
							);

							if (!string.IsNullOrWhiteSpace(newId)) {
								Console.WriteLine("Device RegistrationId Changed To: {0}", newId);
								
								Console.WriteLine("Update Registration ID from {0} to {1}...", oldId, newId);
								
//							exec(
//								"INSERT INTO dbo.UserRegistrationID(UserID, RegistrationID) " +
//								"VALUES(" + userId + ", '" + userKey.Replace("'", "") + "')"
//							);
//							repo.ddd(userId, userKey);
								Db.exec(
									"INSERT INTO dbo.UserRegistrationID(UserID, RegistrationID) " +
									"VALUES(" + userId + ", '" + userKey.Replace("'", "") + "')"
								);
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

			foreach (var registrationId in registrationIds) {
				gcmBroker.QueueNotification(
					new GcmNotification {
						RegistrationIds = new List<string> {
							registrationId
						},
						Notification = JObject.Parse("{ 'sound': 'default', 'badge': '1', 'title': 'HealthWatch', 'body': '" + message + "', 'click_action': 'se.healthwatch.HealthWatch.NotificationClick'}"),
						Priority = GcmNotificationPriority.High,
						Data = JObject.Parse("{ 'userKey': '" + keyAndUserID + "' }")
					}
				);
			}

			gcmBroker.Stop();
		}
		
		public static MessageSendType CreateSendType(MessageService service, int sendType)
		{
			switch (sendType) {
					case RegistrationSendType: return new RegistrationSendType(service);
					case RegistrationReminderSendType: return new RegistrationReminderSendType(service);
					case LoginReminderSendType: return new LoginReminderSendType(service);
					case ExtendedSurveySendType: return new ExtendedSurveySendType(service);
					case ThankYouSendType: return new ThankYouSendType(service);
					case AllActivatedUsersSendType: return new AllActivatedUsersSendType(service);
					default: return null;
			}
		}
	}
	
	public class PasswordActivationLink
	{
		public void Send(string to, string uid, string name, string username)
		{
			string body = string.Format(
				@"Dear {0},

A manager account has been set up for you to the HealthWatch group administration interface. Please click the link below to choose a password.

{1}PasswordActivation.aspx?KEY={2}

{3}",
				name,
				ConfigurationManager.AppSettings["grpURL"],
				uid,
				username != "" ? string.Format("Your username is '{0}'", username) : ""
			);
			Db.sendMail("info@healthwatch.se", to, "Your HealthWatch Group Administration account", body);
		}
	}
}
