using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class ProjectRound
	{
		public ProjectRound()
		{
			Units = new List<ProjectRoundUnit>();
		}
		
		public int ProjectRoundID { get; set; }
		public int ProjectID { get; set; }
		public string Internal { get; set; }
		public DateTime? Started { get; set; }
		public DateTime? Closed { get; set; }
		public int TransparencyLevel { get; set; }
		public int RepeatedEntry { get; set; }
		public int SurveyID { get; set; }
		public int LangID { get; set; }
		public Guid RoundKey { get; set; }
		public string EmailFromAddress { get; set; }
		public int ReminderInterval { get; set; }
		public int Layout { get; set; }
		public int SelfRegistration { get; set; }
		public int Timeframe { get; set; }
		public int Yellow { get; set; }
		public int Green { get; set; }
		public int IndividualReportID { get; set; }
		public int ExtendedSurveyID { get; set; }
		public int ReportID { get; set; }
		public int Logo { get; set; }
		public int UseCode { get; set; }
		public int ConfidentialIndividualReportID { get; set; }
		public int SendSurveyAsEmail { get; set; }
		public string SFTPhost { get; set; }
		public string SFTPpath { get; set; }
		public string SFTPuser { get; set; }
		public string SFTPpass { get; set; }
		public string SendSurveyAsPdfTo { get; set; }
		public int SendSurveyAsPdfToQ { get; set; }
		public int SendSurveyAsPdfToO { get; set; }
		public int AdHocReportCompareWithParent { get; set; }
		public int FeedbackID { get; set; }

		public Project Project { get; set; }
		public Survey Survey { get; set; }
		public IList<ProjectRoundUnit> Units { get; set; }
		public IList<ProjectRoundLang> Languages { get; set; }
		public Feedback Feedback { get; set; }
		
		public void AddUnit(string unit)
		{
			AddUnit(new ProjectRoundUnit { Unit = unit });
		}
		
		public void AddUnit(ProjectRoundUnit unit)
		{
			Units.Add(unit);
		}
		
		public override string ToString()
		{
			return string.Format("{0} >> {1}", Project.Internal, Internal);
		}
	}
}
