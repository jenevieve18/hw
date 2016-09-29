using System;
	
namespace HW.EForm.Core.Models
{
	public class SponsorSuperAdmin
	{
		public SponsorSuperAdmin()
		{
		}
		
		public int SponsorSuperAdminID { get; set; }
		public int DefaultSponsorID { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }

	}
}
