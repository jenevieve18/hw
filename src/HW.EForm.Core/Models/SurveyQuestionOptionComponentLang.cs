using System;
	
namespace HW.EForm.Core.Models
{
	public class SurveyQuestionOptionComponentLang
	{
		public SurveyQuestionOptionComponentLang()
		{
		}
		
		public int SurveyQuestionOptionComponentLangID { get; set; }
		public int SurveyQuestionOptionID { get; set; }
		public int OptionComponentID { get; set; }
		public int LangID { get; set; }
		public string Text { get; set; }
		public string OnClick { get; set; }
		public string TextJapaneseUnicode { get; set; }
		public string OnClickJapaneseUnicode { get; set; }

	}
}
