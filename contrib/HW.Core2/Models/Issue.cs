using System;
	
namespace HW.Core2.Models
{
	public class Issue
	{
		public int IssueID { get; set; }
		public DateTime? IssueDate { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int UserID { get; set; }
		public int Status { get; set; }

		public Issue()
		{
		}
	}
}
