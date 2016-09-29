using System;
	
namespace HW.EForm.Core.Models
{
	public class SurveyQuestionOption
	{
		public SurveyQuestionOption()
		{
		}
		
		public int SurveyQuestionOptionID { get; set; }
		public int SurveyQuestionID { get; set; }
		public int QuestionOptionID { get; set; }
		public int OptionPlacement { get; set; }
		public string Variablename { get; set; }
		public int Forced { get; set; }
		public int SortOrder { get; set; }
		public int Warn { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }

	}
}
