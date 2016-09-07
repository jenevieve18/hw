using System;
	
namespace HW.Core2.Models
{
	public class MeasureComponentPart
	{
		public int MeasureComponentPartID { get; set; }
		public int MeasureComponentID { get; set; }
		public int ComponentPart { get; set; }
		public int SortOrder { get; set; }

		public MeasureComponentPart()
		{
		}
	}
}
