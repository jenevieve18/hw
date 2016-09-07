using System;
	
namespace HW.Core2.Models
{
	public class UserProfile
	{
		public int UserProfileID { get; set; }
		public int UserID { get; set; }
		public int SponsorID { get; set; }
		public int DepartmentID { get; set; }
		public int ProfileComparisonID { get; set; }
		public DateTime? Created { get; set; }

		public UserProfile()
		{
		}
	}
}
