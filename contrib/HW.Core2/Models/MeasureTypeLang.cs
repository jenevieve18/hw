using System;
	
namespace HW.Core2.Models
{
	public class MeasureTypeLang
	{
		public int MeasureTypeLangID { get; set; }
		public int MeasureTypeID { get; set; }
		public int LangID { get; set; }
		public string MeasureType { get; set; }

		public MeasureTypeLang()
		{
		}
	}
}
