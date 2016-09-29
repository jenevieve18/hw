using System;
	
namespace HW.EForm.Core.Models
{
	public class ReportPartLang
	{
		public ReportPartLang()
		{
		}
		
		public int ReportPartLangID { get; set; }
		public int ReportPartID { get; set; }
		public int LangID { get; set; }
		public string Subject { get; set; }
		public string Header { get; set; }
		public string Footer { get; set; }
		public string AltText { get; set; }
		public string SubjectJapaneseUnicode { get; set; }
		public string HeaderJapaneseUnicode { get; set; }
		public string FooterJapaneseUnicode { get; set; }
		public string AltTextJapaneseUnicode { get; set; }

	}
}
