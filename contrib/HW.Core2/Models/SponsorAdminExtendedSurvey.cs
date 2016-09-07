using System;
	
namespace HW.Core2.Models
{
	public class SponsorAdminExtendedSurvey
	{
		public int SponsorAdminExtendedSurveyID { get; set; }
		public int SponsorAdminID { get; set; }
		public string EmailSubject { get; set; }
		public string EmailBody { get; set; }
		public string FinishedEmailSubject { get; set; }
		public string FinishedEmailBody { get; set; }
		public DateTime? EmailLastSent { get; set; }
		public DateTime? FinishedLastSent { get; set; }
		public int SponsorExtendedSurveyID { get; set; }

		public SponsorAdminExtendedSurvey()
		{
		}
	}
}
