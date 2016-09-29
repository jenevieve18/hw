using System;
	
namespace HW.EForm.Core.Models
{
	public class UserCategoryLang
	{
		public UserCategoryLang()
		{
		}
		
		public int UserCategoryLangID { get; set; }
		public int UserCategoryID { get; set; }
		public int LangID { get; set; }
		public string Category { get; set; }
		public string CategoryJapaneseUnicode { get; set; }

	}
}
