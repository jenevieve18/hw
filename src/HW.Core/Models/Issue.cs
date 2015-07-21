using System;

namespace HW.Core.Models
{
	public class Issue : BaseModel
	{
		public string Title { get; set; }
		public DateTime? Date { get; set; }
		public string Description { get; set; }
		public User User { get; set; }
		
		public Issue()
		{
		}
	}
}
