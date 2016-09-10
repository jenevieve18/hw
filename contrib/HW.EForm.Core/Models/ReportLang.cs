using System;
	
namespace HW.EForm.Core.Models
{
	public class ReportLang
	{
		public ReportLang()
		{
		}
		
		public int ReportLangID { get; set; }
		public int ReportID { get; set; }
		public int LangID { get; set; }
		public string Feedback { get; set; }
		public string FeedbackJapaneseUnicode { get; set; }
	}
}
