using System;
	
namespace HW.Core2.Models
{
	public class SystemSetting
	{
		public int SystemID { get; set; }
		public int ProjectRoundUnitID { get; set; }
		public string ReminderMessage { get; set; }
		public string ReminderSubject { get; set; }
		public string ReminderEmail { get; set; }

		public SystemSetting()
		{
		}
	}
}
