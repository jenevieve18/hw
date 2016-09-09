using System;
	
namespace HW.EForm.Core.Models
{
	public class QuestionLang
	{
		public QuestionLang()
		{
		}
		
		public QuestionLang(int langID, string question)
		{
			this.LangID = langID;
			this.Question = question;
		}
		
		public int QuestionLangID { get; set; }
		public int QuestionID { get; set; }
		public int LangID { get; set; }
		public string Question { get; set; }
		public string QuestionShort { get; set; }
		public string QuestionArea { get; set; }
		public string QuestionJapaneseUnicode { get; set; }
		public string QuestionShortJapaneseUnicode { get; set; }
		public string QuestionAreaJapaneseUnicode { get; set; }

		public override string ToString()
		{
			return string.Format("[QuestionLang QuestionLangID={0}, QuestionID={1}, LangID={2}, Question={3}, QuestionShort={4}, QuestionArea={5}, QuestionJapaneseUnicode={6}, QuestionShortJapaneseUnicode={7}, QuestionAreaJapaneseUnicode={8}]", QuestionLangID, QuestionID, LangID, Question, QuestionShort, QuestionArea, QuestionJapaneseUnicode, QuestionShortJapaneseUnicode, QuestionAreaJapaneseUnicode);
		}
	}
}
