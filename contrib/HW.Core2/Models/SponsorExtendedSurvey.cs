using System;
	
namespace HW.Core2.Models
{
	public class SponsorExtendedSurvey
	{
		public int SponsorExtendedSurveyID { get; set; }
		public int SponsorID { get; set; }
		public int ProjectRoundID { get; set; }
		public string Internal { get; set; }
		public int EformFeedbackID { get; set; }
		public int RequiredUserCount { get; set; }
		public int PreviousProjectRoundID { get; set; }
		public string RoundText { get; set; }
		public string EmailSubject { get; set; }
		public string EmailBody { get; set; }
		public string EmailLastSent { get; set; }
		public int IndividualFeedbackID { get; set; }
		public string IndividualFeedbackEmailSubject { get; set; }
		public string IndividualFeedbackEmailBody { get; set; }
		public int WarnIfMissingQID { get; set; }
		public string ExtraEmailSubject { get; set; }
		public string ExtraEmailBody { get; set; }
		public string FinishedEmailSubject { get; set; }
		public string FinishedEmailBody { get; set; }
		public string FinishedLastSent { get; set; }
		public int Total { get; set; }
		public int Answers { get; set; }

		public SponsorExtendedSurvey()
		{
		}
	}
}
