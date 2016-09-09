using System;
	
namespace HW.EForm.Core.Models
{
	public class ReportPart
	{
		public ReportPart()
		{
		}
		
		public int ReportPartID { get; set; }
		public int ReportID { get; set; }
		public string Internal { get; set; }
		public int Type { get; set; }
		public int QuestionID { get; set; }
		public int OptionID { get; set; }
		public int RequiredAnswerCount { get; set; }
		public int SortOrder { get; set; }
		public int PartLevel { get; set; }
		public int GroupingQuestionID { get; set; }
		public int GroupingOptionID { get; set; }

		public Question Question { get; set; }
	}
}
