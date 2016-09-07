using System;
	
namespace HW.Core2.Models
{
	public class MeasureCategoryLang
	{
		public int MeasureCategoryLangID { get; set; }
		public int MeasureCategoryID { get; set; }
		public int LangID { get; set; }
		public string MeasureCategory { get; set; }

		public MeasureCategoryLang()
		{
		}
	}
}
