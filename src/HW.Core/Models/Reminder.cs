using System;

namespace HW.Core.Models
{
	public class Reminder : BaseModel
	{
		public User User { get; set; }
		public DateTime Date { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
	}
}
