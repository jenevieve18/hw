using System;
	
namespace HW.EForm.Core.Models
{
	public class OptionComponentLang
	{
		public int OptionComponentLangID { get; set; }
		public int OptionComponentID { get; set; }
		public int LangID { get; set; }
		public string Text { get; set; }
		public string TextJapaneseUnicode { get; set; }

		public OptionComponentLang()
		{
		}
	}
}
