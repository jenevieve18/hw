using System;
	
namespace HW.EForm.Core.Models
{
	public class SurveyQuestionIf
	{
		public SurveyQuestionIf()
		{
		}
		
		public int SurveyQuestionIfID { get; set; }
		public int SurveyID { get; set; }
		public int SurveyQuestionID { get; set; }
		public int QuestionID { get; set; }
		public int OptionID { get; set; }
		public int OptionComponentID { get; set; }
		public int ConditionAnd { get; set; }
	}
}
