using System;
	
namespace HW.Core2.Models
{
	public class SuperAdminSponsor
	{
		public int SuperAdminSponsorID { get; set; }
		public int SuperAdminID { get; set; }
		public int SponsorID { get; set; }
		public int SeeUsers { get; set; }

		public SuperAdminSponsor()
		{
		}
	}
}
