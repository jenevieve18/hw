using System;
	
namespace HW.Core2.Models
{
	public class SponsorInviteBQ
	{
		public int SponsorInviteBQID { get; set; }
		public int SponsorInviteID { get; set; }
		public int BQID { get; set; }
		public int BAID { get; set; }
		public int ValueInt { get; set; }
		public DateTime? ValueDate { get; set; }
		public string ValueText { get; set; }

		public SponsorInviteBQ()
		{
		}
	}
}
