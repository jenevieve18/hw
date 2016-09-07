using System;
	
namespace HW.Core2.Models
{
	public class UserMeasure
	{
		public int UserMeasureID { get; set; }
		public int UserID { get; set; }
		public DateTime? DT { get; set; }
		public DateTime? CreatedDT { get; set; }
		public DateTime? DeletedDT { get; set; }
		public int UserProfileID { get; set; }

		public UserMeasure()
		{
		}
	}
}
