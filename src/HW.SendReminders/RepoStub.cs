/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 5/31/2016
 * Time: 9:56 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace HW.SendReminders
{
	public class SmtpStub : ISmtp
	{
		public void Send(MailMessage mail)
		{
		}
	}
	
	public class RepoStub : IRepo
	{
		public SystemSettings GetSystemSettings()
		{
			var s = new SystemSettings {
				ReminderEmail = "reminder@healthwatch.se",
			};
			
			s.Languages.Add(
				new SystemSettingsLang {
					ReminderMessage = @"Här kommer en påminnelse om att logga in på HealthWatch.
	
	<LINK/>
	
	Om du inte vill ha fler påminnelser så kan du stänga av dessa genom att logga in och välja Påminnelser på menyn längst upp.
	
	Med vänliga hälsningar,
	HealthWatch",
					ReminderSubject = "Påminnelse från HealthWatch",
					ReminderAutoLogin = "Tips! Du ställa in så att länken ovan loggar in dig automatiskt utan att ange användarnamn och lösenord. Denna inställning hittar du också under menyalternativet Påminnelser efter att du loggat in.",
				}
			);
			
			s.Languages.Add(
				new SystemSettingsLang {
					ReminderMessage = @"This is a reminder to log on to HealthWatch.
	
	<LINK/>
	
	If you want no further reminders, please log in and turn them off under Reminders on the top menu.
	
	Best regards,
	HealthWatch",
					ReminderSubject = "Reminder from HealthWatch",
					ReminderAutoLogin = "Please note! The link above can be customized to log you on automatically. This is also done in the Reminders section after login."
				}
			);
			return s;
		}
		
		public List<User> ggg()
		{
			var users = new List<User>();
			return users;
		}
		
		public List<User> GetUsers()
		{
			var users = new List<User>();
			users.Add(
				new User {
					ID = 17429,
					UserKey = "d4469cf8-a9c1-474f-a5c3-148f107e432d".Replace("-", "").Substring(0, 12),
					ReminderLink = 2,
					Email = "customer@localhost.com",
					LID = 1,
					ReminderSettings = "17:1:3"
				});
			return users;
		}
		
		public void UpdateUserKey()
		{
		}
		
		public void demyo(int userID, string personalReminderMessage, string xxx, string yyy)
		{
		}
		
		public void UpdateEmailFailure(int userID)
		{
		}
		
		public List<Sponsor> otot()
		{
			var sponsors = new List<Sponsor>();
			sponsors.Add(
				new Sponsor {
					LoginTxt = "LoginTxt2",
					LoginSubject = "LoginSubject2"
				}
			);
			return sponsors;
		}
		
		public void hhh(int answerID, int userSponsorExtendedSurveyID)
		{
			throw new NotImplementedException();
		}
		
		public List<User> ototize(int sponsorID, string ddd, int userID)
		{
			var users = new List<User>();
			users.Add(
				new User {
					ID = 17429,
					UserKey = "d4469cf8-a9c1-474f-a5c3-148f107e432d".Replace("-", "").Substring(0, 12),
					Email = "customer@localhost.com",
					ReminderLink = 2,
					LID = 1
				}
			);
			return users;
		}
		
		public List<string> GetUserRegistrationIDs(int userID)
		{
			var ids = new List<string>();
			ids.Add("cLrKoHsz4zw:APA91bFpZnwCmmXhZN0JongzTbZPx2f9OGyLVBMO-joT-MXP0u-U73w6RQPOUXqNDCEcH7XgiaDUNdKQsY3nIToyX4Km6UyU1NgI6YV56eL_xISSPPlHPawRHexbf_Yyyiiev6tygXPE"); // Ian's Android Phone
	//			ids.Add("e4elA3Xu7Eg:APA91bGZ9lzk1XQoucveFngdRVtk0atB4TWvfbhN_Zp3LwjfWuBiDfLjZo4sBVi8-JJIObCvOGz8hgMiNLt9i-ttLBLw3_hr-tE0oqRVIAfU7SyjXGQliIk6sKiIE-bbVXOKPhXBxnBe"); // Nino's Android Phone
	//			ids.Add("djS9ID_BN18:APA91bGCXkzSGaRqaKlQWw5qwZYRlFY1i9AsQvXBG4imBxphoVD7vEv0BlNQVKPedqVJSVT8AEdd9BARzajkANlNSF2fexHaNwhB-hD91yLX_OL_rNVRVgLQC48Ne9la1G6hRcgRiUpi"); // Nino's Apple Phone
			return ids;
		}
		
		public void fff(int userID)
		{
		}
		
		public void eee(int userID, string reminderSubject, string personalReminderMessage)
		{
		}
		
		public void ddd(int userID, string userKey)
		{
			throw new NotImplementedException();
		}
		
		public void ccc(int userID, string userKey)
		{
			throw new NotImplementedException();
		}
		
		public void bbb(Sponsor s)
		{
		}
		
		public void aaa(int sponsorID)
		{
		}
	}
}
