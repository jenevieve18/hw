using System;
	
namespace HW.Core2.Models
{
	public class MeasureLang
	{
		public int MeasureLangID { get; set; }
		public int MeasureID { get; set; }
		public int LangID { get; set; }
		public string Measure { get; set; }

		public MeasureLang()
		{
		}
	}
}
