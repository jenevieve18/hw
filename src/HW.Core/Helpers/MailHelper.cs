using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Configuration;

namespace HW.Core.Helpers
{
	public class MailHelper
	{
		public MailHelper()
		{
		}
		
		public static void SendMail(string to, string subject, string body)
		{
			SendMail("info@healthwatch.se", to, subject, body);
		}
		
		public static void SendMail(string from, string to, string subject, string body)
		{
			SendMail(from, to, subject, body, "");
		}

        public static void SendMail(string from, string to, string subject, string body, string attachmentFilename)
        {
            SendMail(from, to, subject, body, attachmentFilename, "", "", ConfigurationManager.AppSettings["SmtpServer"]);
        }
		
		public static void SendMail(string from, string to, string subject, string body, string attachmentFilename, string username, string password, string server)
		{
			SmtpClient smtpClient = new SmtpClient();
//			NetworkCredential basicCredential = new NetworkCredential(username, password);
//			MailMessage message = new MailMessage();
			MailMessage message = new MailMessage(from, to, subject, body);
//			MailAddress fromAddress = new MailAddress(username);

			// Setup up the host, increase the timeout to 5 minutes
			smtpClient.Host = server;
//			smtpClient.UseDefaultCredentials = false;
//			smtpClient.Credentials = basicCredential;
//			smtpClient.Timeout = (60 * 5 * 1000);

//			message.From = fromAddress;
//			message.Subject = subject;
//			message.IsBodyHtml = false;
//			message.Body = body;
//			message.To.Add(to);

			if (attachmentFilename != null) {
//				message.Attachments.Add(new Attachment(attachmentFilename));
				Attachment attachment = new Attachment(attachmentFilename, MediaTypeNames.Application.Octet);
				ContentDisposition disposition = attachment.ContentDisposition;
				disposition.CreationDate = File.GetCreationTime(attachmentFilename);
				disposition.ModificationDate = File.GetLastWriteTime(attachmentFilename);
				disposition.ReadDate = File.GetLastAccessTime(attachmentFilename);
				disposition.FileName = Path.GetFileName(attachmentFilename);
				disposition.Size = new FileInfo(attachmentFilename).Length;
				disposition.DispositionType = DispositionTypeNames.Attachment;
				message.Attachments.Add(attachment);
			}

			smtpClient.Send(message);
		}
	}
}
