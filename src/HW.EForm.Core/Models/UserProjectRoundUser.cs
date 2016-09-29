using System;
	
namespace HW.EForm.Core.Models
{
	public class UserProjectRoundUser
	{
		public UserProjectRoundUser()
		{
		}
		
		public int UserProjectRoundUserID { get; set; }
		public int UserID { get; set; }
		public int ProjectRoundUserID { get; set; }
		public string Note { get; set; }

	}
}
