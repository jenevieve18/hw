using System;
	
namespace HW.Core2.Models
{
	public class MeasureComponent
	{
		public int MeasureComponentID { get; set; }
		public int MeasureID { get; set; }
		public string Component { get; set; }
		public int Type { get; set; }
		public int Required { get; set; }
		public int SortOrder { get; set; }
		public string Unit { get; set; }
		public int Decimals { get; set; }
		public int ShowInList { get; set; }
		public int ShowUnitInList { get; set; }
		public int ShowInGraph { get; set; }
		public int Inherit { get; set; }
		public string AutoScript { get; set; }

		public MeasureComponent()
		{
		}
	}
}
