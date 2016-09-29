using System;
	
namespace HW.EForm.Core.Models
{
	public class UserSchedule
	{
		public UserSchedule()
		{
		}
		
		public int UserScheduleID { get; set; }
		public int UserID { get; set; }
		public int UserProjectRoundUserID { get; set; }
		public DateTime? DT { get; set; }
		public int SponsorReminderID { get; set; }
		public int Reminder { get; set; }
		public string Note { get; set; }
		public string Email { get; set; }
		public DateTime? SentDT { get; set; }
		public string NoteJapaneseUnicode { get; set; }

	}
}
