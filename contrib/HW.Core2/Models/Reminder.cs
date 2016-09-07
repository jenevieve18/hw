using System;
	
namespace HW.Core2.Models
{
	public class Reminder
	{
		public int ReminderID { get; set; }
		public int UserID { get; set; }
		public DateTime? DT { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }

		public Reminder()
		{
		}
	}
}
