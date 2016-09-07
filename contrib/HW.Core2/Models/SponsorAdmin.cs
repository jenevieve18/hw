using System;
	
namespace HW.Core2.Models
{
	public class SponsorAdmin
	{
		public int SponsorAdminID { get; set; }
		public string Usr { get; set; }
		public string Pas { get; set; }
		public int SponsorID { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public int SuperUser { get; set; }
		public Guid SponsorAdminKey { get; set; }
		public int Anonymized { get; set; }
		public int SeeUsers { get; set; }
		public int ReadOnly { get; set; }
		public string LastName { get; set; }
		public int PermanentlyDeleteUsers { get; set; }
		public string InviteSubject { get; set; }
		public string InviteTxt { get; set; }
		public string InviteReminderSubject { get; set; }
		public string InviteReminderTxt { get; set; }
		public string AllMessageSubject { get; set; }
		public string AllMessageBody { get; set; }
		public DateTime? InviteLastSent { get; set; }
		public DateTime? InviteReminderLastSent { get; set; }
		public DateTime? AllMessageLastSent { get; set; }
		public DateTime? LoginLastSent { get; set; }
		public string UniqueKey { get; set; }
		public int UniqueKeyUsed { get; set; }

		public SponsorAdmin()
		{
		}
	}
}
