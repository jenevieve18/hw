using System;
	
namespace HW.EForm.Core.Models
{
	public class SurveyQuestionLang
	{
		public SurveyQuestionLang()
		{
		}
		
		public int SurveyQuestionLangID { get; set; }
		public int SurveyQuestionID { get; set; }
		public int LangID { get; set; }
		public string Question { get; set; }
		public string QuestionJapaneseUnicode { get; set; }

	}
}
