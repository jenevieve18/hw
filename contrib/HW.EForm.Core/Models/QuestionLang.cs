using System;
	
namespace HW.EForm.Core.Models
{
	public class QuestionLang
	{
		public int QuestionLangID { get; set; }
		public int QuestionID { get; set; }
		public int LangID { get; set; }
		public string Question { get; set; }
		public string QuestionShort { get; set; }
		public string QuestionArea { get; set; }
		public string QuestionJapaneseUnicode { get; set; }
		public string QuestionShortJapaneseUnicode { get; set; }
		public string QuestionAreaJapaneseUnicode { get; set; }

		public QuestionLang()
		{
		}
		
		public QuestionLang(int langID, string question)
		{
			this.LangID = langID;
			this.Question = question;
		}
	}
}
