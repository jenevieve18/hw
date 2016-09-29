using System;
	
namespace HW.EForm.Core.Models
{
	public class QuestionOptionRange
	{
		public QuestionOptionRange()
		{
		}
		
		public int QuestionOptionRangeID { get; set; }
		public int QuestionOptionID { get; set; }
		public string StartDT { get; set; }
		public string EndDT { get; set; }
		public decimal LowVal { get; set; }
		public decimal HighVal { get; set; }

	}
}
