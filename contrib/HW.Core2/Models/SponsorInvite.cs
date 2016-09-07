using System;
	
namespace HW.Core2.Models
{
	public class SponsorInvite
	{
		public int SponsorInviteID { get; set; }
		public int SponsorID { get; set; }
		public int DepartmentID { get; set; }
		public string Email { get; set; }
		public int UserID { get; set; }
		public DateTime? Sent { get; set; }
		public Guid InvitationKey { get; set; }
		public DateTime? Consent { get; set; }
		public DateTime? Stopped { get; set; }
		public int StoppedReason { get; set; }
		public int PreviewExtendedSurveys { get; set; }
		public int StoppedPercent { get; set; }

		public SponsorInvite()
		{
		}
	}
}
