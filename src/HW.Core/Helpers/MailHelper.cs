using System;
using System.Net;
using System.Net.Mail;

namespace HW.Core.Helpers
{
	public class MailHelper
	{
		public MailHelper()
		{
		}
		
		public static void SendMail(string recipient, string subject, string body, string attachmentFilename, string username, string password, string server)
		{
			SmtpClient smtpClient = new SmtpClient();
			NetworkCredential basicCredential = new NetworkCredential(username, password);
			MailMessage message = new MailMessage();
			MailAddress fromAddress = new MailAddress(username);

			// Setup up the host, increase the timeout to 5 minutes
			smtpClient.Host = server;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Credentials = basicCredential;
			smtpClient.Timeout = (60 * 5 * 1000);

			message.From = fromAddress;
			message.Subject = subject;
			message.IsBodyHtml = false;
			message.Body = body;
			message.To.Add(recipient);

			if (attachmentFilename != null) {
				message.Attachments.Add(new Attachment(attachmentFilename));
			}

			smtpClient.Send(message);
		}
	}
}
