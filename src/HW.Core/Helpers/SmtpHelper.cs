/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/28/2016
 * Time: 1:47 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace HW.Core.Helpers
{
	public static class SmtpHelper
	{
		static SmtpClient smtp;
		static SmtpClient backupSmtp;
		
		static SmtpHelper()
		{
			string backupServer = "";
			ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

//			SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"], (ConfigurationManager.AppSettings["SmtpPort"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]) : 25));
			smtp = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"], (ConfigurationManager.AppSettings["SmtpPort"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]) : 25));
			if (ConfigurationManager.AppSettings["SmtpUsername"] != null && ConfigurationManager.AppSettings["SmtpPassword"] != null) {
				smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpUsername"], ConfigurationManager.AppSettings["SmtpPassword"]);
			}
			if (ConfigurationManager.AppSettings["SmtpSSL"] != null && Convert.ToBoolean(ConfigurationManager.AppSettings["SmtpSSL"])) {
				smtp.EnableSsl = true;
			}
			if (ConfigurationManager.AppSettings["BackupSmtpServer"] != null) {
				backupServer = "Backup";
			}
			
//			SmtpClient backupSmtp = new SmtpClient(ConfigurationManager.AppSettings[backupServer + "SmtpServer"], (ConfigurationManager.AppSettings[backupServer + "SmtpPort"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings[backupServer + "SmtpPort"]) : 25));
			backupSmtp = new SmtpClient(ConfigurationManager.AppSettings[backupServer + "SmtpServer"], (ConfigurationManager.AppSettings[backupServer + "SmtpPort"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings[backupServer + "SmtpPort"]) : 25));
			if (ConfigurationManager.AppSettings[backupServer + "SmtpUsername"] != null && ConfigurationManager.AppSettings[backupServer + "SmtpPassword"] != null) {
				backupSmtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings[backupServer + "SmtpUsername"], ConfigurationManager.AppSettings[backupServer + "SmtpPassword"]);
			}
			if (ConfigurationManager.AppSettings[backupServer + "SmtpSSL"] != null && Convert.ToBoolean(ConfigurationManager.AppSettings[backupServer + "SmtpSSL"])) {
				backupSmtp.EnableSsl = true;
			}
		}
		
		public static bool Send(string from, string to, string subject, string body)
		{
			MailMessage mail = new MailMessage(from, to, subject, body);
			bool success = true;
			try {
				smtp.Send(mail);
			} catch (Exception ex) {
				try {
					backupSmtp.Send(mail);
				} catch (Exception ex2) {
					success = false;
				}
			}
			return success;
		}
	}
}
