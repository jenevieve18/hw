using System;
	
namespace HW.EForm.Core.Models
{
	public class SponsorReminder
	{
		public SponsorReminder()
		{
		}
		
		public int SponsorReminderID { get; set; }
		public int SponsorID { get; set; }
		public string Reminder { get; set; }
		public string FromEmail { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }

	}
}
