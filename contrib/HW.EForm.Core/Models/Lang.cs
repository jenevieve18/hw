using System;
	
namespace HW.EForm.Core.Models
{
	public class Language
	{
		public const int Swedish = 1;
		public const int English = 2;
		public const int Japanese = 3;
		public const int German = 4;
		
		public Language()
		{
		}
		
		public int LangID { get; set; }
		public string LanguageName { get; set; }
		public string LangJapaneseUnicode { get; set; }
	}
}
