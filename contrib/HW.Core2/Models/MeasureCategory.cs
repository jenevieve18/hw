using System;
	
namespace HW.Core2.Models
{
	public class MeasureCategory
	{
		public int MeasureCategoryID { get; set; }
		public string Category { get; set; }
		public int MeasureTypeID { get; set; }
		public int SortOrder { get; set; }
		public int SPRUID { get; set; }

		public MeasureCategory()
		{
		}
	}
}
