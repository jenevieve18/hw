using System;
	
namespace HW.EForm.Core.Models
{
	public class QuestionCategoryLang
	{
		public QuestionCategoryLang()
		{
		}
		
		public int QuestionCategoryLangID { get; set; }
		public int QuestionCategoryID { get; set; }
		public int LangID { get; set; }
		public string QuestionCategory { get; set; }
		public string QuestionCategoryJapaneseUnicode { get; set; }

	}
}
