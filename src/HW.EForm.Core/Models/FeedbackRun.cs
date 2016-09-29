using System;
	
namespace HW.EForm.Core.Models
{
	public class FeedbackRun
	{
		public FeedbackRun()
		{
		}
		
		public int FeedbackRunID { get; set; }
		public Guid FeedbackRunKey { get; set; }
		public int Total { get; set; }
		public int Answer { get; set; }

	}
}
