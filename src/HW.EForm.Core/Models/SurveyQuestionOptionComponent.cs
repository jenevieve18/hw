using System;
	
namespace HW.EForm.Core.Models
{
	public class SurveyQuestionOptionComponent
	{
		public SurveyQuestionOptionComponent()
		{
		}
		
		public int SurveyQuestionOptionComponentID { get; set; }
		public int SurveyQuestionOptionID { get; set; }
		public int OptionComponentID { get; set; }
		public int Hide { get; set; }
		public string OnClick { get; set; }

	}
}
