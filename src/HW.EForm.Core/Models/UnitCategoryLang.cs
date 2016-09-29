using System;
	
namespace HW.EForm.Core.Models
{
	public class UnitCategoryLang
	{
		public UnitCategoryLang()
		{
		}
		
		public int UnitCategoryLangID { get; set; }
		public int UnitCategoryID { get; set; }
		public int LangID { get; set; }
		public string Category { get; set; }
		public string CategoryJapaneseUnicode { get; set; }

	}
}
