using System;
	
namespace HW.Core2.Models
{
	public class MeasureComponentLang
	{
		public int MeasureComponentLangID { get; set; }
		public int MeasureComponentID { get; set; }
		public int LangID { get; set; }
		public string MeasureComponent { get; set; }
		public string Unit { get; set; }

		public MeasureComponentLang()
		{
		}
	}
}
