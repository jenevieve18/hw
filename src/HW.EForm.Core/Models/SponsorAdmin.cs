using System;
	
namespace HW.EForm.Core.Models
{
	public class SponsorAdmin
	{
		public SponsorAdmin()
		{
		}
		
		public int SponsorAdminID { get; set; }
		public int SponsorID { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public int Restricted { get; set; }

	}
}
