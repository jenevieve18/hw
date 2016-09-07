using System;
	
namespace HW.Core2.Models
{
	public class Sponsor
	{
		public int SponsorID { get; set; }
		public string SponsorName { get; set; }
		public string Application { get; set; }
		public int ProjectRoundUnitID { get; set; }
		public Guid SponsorKey { get; set; }
		public string InviteTxt { get; set; }
		public string InviteReminderTxt { get; set; }
		public string LoginTxt { get; set; }
		public DateTime? InviteLastSent { get; set; }
		public DateTime? InviteReminderLastSent { get; set; }
		public DateTime? LoginLastSent { get; set; }
		public string InviteSubject { get; set; }
		public string InviteReminderSubject { get; set; }
		public string LoginSubject { get; set; }
		public int LoginDays { get; set; }
		public int LoginWeekday { get; set; }
		public int LID { get; set; }
		public int TreatmentOffer { get; set; }
		public string TreatmentOfferText { get; set; }
		public string TreatmentOfferEmail { get; set; }
		public string TreatmentOfferIfNeededText { get; set; }
		public int TreatmentOfferBQ { get; set; }
		public int TreatmentOfferBQfn { get; set; }
		public int TreatmentOfferBQmorethan { get; set; }
		public string InfoText { get; set; }
		public string ConsentText { get; set; }
		public string Closed { get; set; }
		public string Deleted { get; set; }
		public int SuperSponsorID { get; set; }
		public string AlternativeTreatmentOfferText { get; set; }
		public string AlternativeTreatmentOfferEmail { get; set; }
		public Guid SponsorApiKey { get; set; }
		public string AllMessageSubject { get; set; }
		public string AllMessageBody { get; set; }
		public DateTime? AllMessageLastSent { get; set; }
		public int ForceLID { get; set; }
		public int MinUserCountToDisclose { get; set; }
		public string EmailFrom { get; set; }
		public string Comment { get; set; }

		public Sponsor()
		{
		}
	}
}
