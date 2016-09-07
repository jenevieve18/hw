using System;
	
namespace HW.Core2.Models
{
	public class UserProjectRoundUser
	{
		public int UserProjectRoundUserID { get; set; }
		public int UserID { get; set; }
		public int ProjectRoundUnitID { get; set; }
		public int ProjectRoundUserID { get; set; }

		public UserProjectRoundUser()
		{
		}
	}
}
