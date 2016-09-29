using System;
	
namespace HW.EForm.Core.Models
{
	public class FeedbackRunRow
	{
		public FeedbackRunRow()
		{
		}
		
		public int FeedbackRunRowID { get; set; }
		public int FeedbackRunID { get; set; }
		public string URL { get; set; }
		public string Area { get; set; }
		public string Header { get; set; }
		public string Description { get; set; }

	}
}
