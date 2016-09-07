using System;
	
namespace HW.Core2.Models
{
	public class SystemSettingsLang
	{
		public int SystemSettingsLangID { get; set; }
		public int SystemID { get; set; }
		public int LID { get; set; }
		public string ReminderMessage { get; set; }
		public string ReminderSubject { get; set; }
		public string ReminderAutoLogin { get; set; }

		public SystemSettingsLang()
		{
		}
	}
}
