using System;
	
namespace HW.EForm.Core.Models
{
	public class ProjectRoundUser
	{
		public ProjectRoundUser()
		{
		}
		
		public int ProjectRoundUserID { get; set; }
		public int ProjectRoundID { get; set; }
		public int ProjectRoundUnitID { get; set; }
		public Guid UserKey { get; set; }
		public string Email { get; set; }
		public DateTime? LastSent { get; set; }
		public int SendCount { get; set; }
		public int ReminderCount { get; set; }
		public int UserCategoryID { get; set; }
		public string Name { get; set; }
		public DateTime? Created { get; set; }
		public int Extended { get; set; }
		public string Extra { get; set; }
		public int ExternalID { get; set; }
		public int NoSend { get; set; }
		public int Terminated { get; set; }
		public int FollowupSendCount { get; set; }
		public int GroupID { get; set; }
		public int ExtendedTag { get; set; }
	}
}
