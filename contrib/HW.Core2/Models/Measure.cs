using System;
	
namespace HW.Core2.Models
{
	public class Measure
	{
		public int MeasureID { get; set; }
		public string MeasureText { get; set; }
		public int MeasureCategoryID { get; set; }
		public int SortOrder { get; set; }
		public string MoreInfo { get; set; }

		public Measure()
		{
		}
	}
}
