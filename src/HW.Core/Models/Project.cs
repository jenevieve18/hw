using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public interface IHasLanguage
	{
		Language Language { get; set; }
	}
	
	public class Project : BaseModel
	{
		public Project()
		{
			Rounds = new List<ProjectRound>();
		}
		
		public string Internal { get; set; }
		public string Name { get; set; }
		public string AppURL { get; set; }
		public IList<ProjectRound> Rounds { get; set; }
		
		public void AddRound(ProjectRound pr)
		{
			pr.Project = this;
			Rounds.Add(pr);
		}
	}
	
	public class ProjectRound : BaseModel
	{
		public int ProjectRoundID { get; set; }
		public int ProjectID { get; set; }
		public string Internal { get; set; }
		public DateTime? Started { get; set; }
		public DateTime? Closed { get; set; }
		public int TransparencyLevel { get; set; }
		public int RepeatedEntry { get; set; }
		public int SurveyID { get; set; }
		public int LangID { get; set; }
//		public Guid? RoundKey { get; set; }
		public string RoundKey { get; set; }
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
		public int FeedBackID { get; set; }
		
		public Project Project { get; set; }
		public Report Report { get; set; }
		public IList<ProjectRoundLang> Languages { get; set; }
		public IList<ProjectRoundUnit> Units { get; set; }
		public Survey Survey { get; set; }
		public List<Answer> Answers { get; set; }
		
		public bool IsOpen {
			get { return Closed == null || Closed >= DateTime.Now; }
		}
		
		public string ToPeriodString()
		{
			return (Started ==  null ? "?" : Started.Value.ToString("yyyy-MM-dd")) + "--" + (Closed == null ? "?" : Closed.Value.ToString("yyyy-MM-dd"));
		}
	}
	
	public class ProjectRoundLang : BaseModel
	{
		public ProjectRound Round { get; set; }
		public Language Language { get; set; }
		public string InvitationSubject { get; set; }
	}
	
	public class ProjectRoundUnit : BaseModel, IHasLanguage
	{
		public int ProjectRoundUnitID { get; set; }
		public int ProjectRoundID { get; set; }
		public string Name { get; set; }
		public string ID { get; set; }
		public int ParentProjectRoundUnitID { get; set; }
		public int SortOrder { get; set; }
		public string SortString { get; set; }
		public int SurveyID { get; set; }
		public int LangID { get; set; }
		public Guid? UnitKey { get; set; }
		public int UserCount { get; set; }
		public int UnitCategoryID { get; set; }
		public bool CanHaveUsers { get; set; }
		public int ReportID { get; set; }
		public int Timeframe { get; set; }
		public int Yellow { get; set; }
		public int Green { get; set; }
		public string SurveyIntro { get; set; }
		public bool Terminated { get; set; }
		public int IndividualReportID { get; set; }
		public string UniqueID { get; set; }
		public int RequiredAnswerCount { get; set; }
		public int SortStringLength { get; set; }
		
		public Report Report { get; set; }
		public Language Language { get; set; }
		public List<Answer> Answers { get; set; }
		public Survey Survey { get; set; }
		public ProjectRound ProjectRound { get; set; }
		public IList<ProjectRoundUser> ProjectRoundUsers { get; set; }
		
		public string TreeString { get; set; } // TODO: This comes from cf_projectUnitTree function.
	}
	
	public class ProjectRoundUnitManager : BaseModel
	{
		public int ProjectRoundUnitManagerID { get; set; }
		public int ProjectRoundUnitID { get; set; }
		public int ProjectRoundUserID { get; set; }
		
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public ProjectRoundUser ProjectRoundUser { get; set; }
	}
	
	public class ProjectRoundUser : BaseModel
	{
		public int ProjectRoundUserID { get; set; }
		public int ProjectRoundID { get; set; }
		public int ProjectRoundUnitID { get; set; }
		public Guid? UserKey { get; set; }
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
		
		public ProjectRound ProjectRound { get; set; }
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public UserCategory UserCategory { get; set; }
		
		public string SomeText { get; set; }
	}
	
	public class ProjectUnitCategory : BaseModel
	{
		public int ProjectUnitCategoryID { get; set; }
		public int ProjectID { get; set; }
		public int UnitCategoryID { get; set; }
		
		public Project Project { get; set; }
	}
	
	public class ProjectUserCategory : BaseModel
	{
		public int ProjectUserCategoryID { get; set; }
		public int ProjectID { get; set; }
		public int UserCategoryID { get; set; }
	}
	
	public class ProjectSurvey : BaseModel
	{
		public int ProjectSurveyID { get; set; }
		public int ProjectID { get; set; }
		public int SurveyID { get; set; }
		
		public Project Project { get; set; }
		public Survey Survey { get; set; }
	}
}
