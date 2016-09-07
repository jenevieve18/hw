using System;
	
namespace HW.Core2.Models
{
	public class MeasureType
	{
		public int MeasureTypeID { get; set; }
		public string Type { get; set; }
		public int SortOrder { get; set; }
		public int Active { get; set; }

		public MeasureType()
		{
		}
	}
}
