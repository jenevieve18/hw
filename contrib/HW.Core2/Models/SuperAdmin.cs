using System;
	
namespace HW.Core2.Models
{
	public class SuperAdmin
	{
		public int SuperAdminID { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public int HideClosedSponsors { get; set; }

		public SuperAdmin()
		{
		}
	}
}
