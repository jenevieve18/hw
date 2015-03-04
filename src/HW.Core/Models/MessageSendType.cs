using System;
using System.Configuration;
using HW.Core.Helpers;
using HW.Core.Services;

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
		public RegistrationSendType(MessageService repository) : base(repository)
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
}
