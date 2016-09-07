using System;
	
namespace HW.Core2.Models
{
	public class UserSponsorProject
	{
		public int UserSponsorProjectID { get; set; }
		public int UserID { get; set; }
		public int SponsorProjectID { get; set; }
		public DateTime? ConsentDT { get; set; }

		public UserSponsorProject()
		{
		}
	}
}
