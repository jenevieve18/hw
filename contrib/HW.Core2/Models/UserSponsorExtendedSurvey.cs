using System;
	
namespace HW.Core2.Models
{
	public class UserSponsorExtendedSurvey
	{
		public int UserSponsorExtendedSurveyID { get; set; }
		public int UserID { get; set; }
		public int SponsorExtendedSurveyID { get; set; }
		public int ProjectRoundUserID { get; set; }
		public int AnswerID { get; set; }
		public DateTime? FinishedEmail { get; set; }
		public DateTime? ContactRequestDT { get; set; }
		public string ContactRequest { get; set; }

		public UserSponsorExtendedSurvey()
		{
		}
	}
}
