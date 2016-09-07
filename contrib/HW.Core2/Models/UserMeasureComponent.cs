using System;
	
namespace HW.Core2.Models
{
	public class UserMeasureComponent
	{
		public int UserMeasureComponentID { get; set; }
		public int UserMeasureID { get; set; }
		public int MeasureComponentID { get; set; }
		public int ValInt { get; set; }
		public decimal ValDec { get; set; }
		public string ValTxt { get; set; }

		public UserMeasureComponent()
		{
		}
	}
}
