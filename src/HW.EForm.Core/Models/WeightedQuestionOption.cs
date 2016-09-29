using System;
	
namespace HW.EForm.Core.Models
{
	public class WeightedQuestionOption
	{
		public WeightedQuestionOption()
		{
		}
		
		public int WeightedQuestionOptionID { get; set; }
		public string Internal { get; set; }
		public int QuestionID { get; set; }
		public int OptionID { get; set; }
		public QuestionOption QuestionOption { get; set; }
		public int TargetVal { get; set; }
		public int YellowLow { get; set; }
		public int GreenLow { get; set; }
		public int GreenHigh { get; set; }
		public int YellowHigh { get; set; }
		public int SortOrder { get; set; }

	}
}
