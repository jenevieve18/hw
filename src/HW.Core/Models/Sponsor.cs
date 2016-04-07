﻿using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public interface ISponsor
	{
		string InviteSubject { get; set; }
		string InviteText { get; set; }
		DateTime? InviteLastSent { get; set; }
		DateTime? InviteReminderLastSent { get; set; }
		string InviteReminderSubject { get; set; }
		string InviteReminderText { get; set; }
		string AllMessageSubject { get; set; }
		string AllMessageBody { get; set; }
		DateTime? AllMessageLastSent { get; set; }
		string LoginSubject { get; set; }
		string LoginText { get; set; }
		int? LoginDays { get; set; }
		int? LoginWeekDay { get; set; }
		DateTime? LoginLastSent { get; set; }
		string EmailFrom { get; set; }
		int MinUserCountToDisclose { get; set; }
		bool SuperUser { get; set; }
	}
	
	public class Sponsor : BaseModel, ISponsor
	{
		public string Name { get; set; }
		public string Application { get; set; }
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public DateTime? ClosedAt { get; set; }
		public DateTime DeletedAt { get; set; }
		public string ConsentText { get; set; }
		public SuperSponsor SuperSponsor { get; set; }
		public string InviteSubject { get; set; }
		public string InviteText { get; set; }
		public DateTime? InviteLastSent { get; set; }
		public DateTime? InviteReminderLastSent { get; set; }
		public string InviteReminderSubject { get; set; }
		public string InviteReminderText { get; set; }
		public string AllMessageSubject { get; set; }
		public string AllMessageBody { get; set; }
		public DateTime? AllMessageLastSent { get; set; }
		public string LoginSubject { get; set; }
		public string LoginText { get; set; }
		public int? LoginDays { get; set; }
		public int? LoginWeekDay { get; set; }
		public DateTime? LoginLastSent { get; set; }
		public string SponsorKey { get; set; }
		public IList<SponsorProjectRoundUnit> RoundUnits { get; set; }
		public List<SponsorInvite> Invites { get; set; }
		public List<SponsorExtendedSurvey> ExtendedSurveys { get; set; }
		public IList<SuperAdminSponsor> SuperAdminSponsors { get; set; }
		public IList<SponsorAdmin> Admins { get; set; }
		public IList<SponsorBackgroundQuestion> BackgroundQuestions { get; set; }
		public int TreatmentOffer { get; set; }
		public string TreatmentOfferText { get; set; }
		public string TreatmentOfferEmail { get; set; }
		public string TreatmentOfferIfNeededText { get; set; }
		public int TreatmentOfferBQ { get; set; }
		public int TreatmentOfferBQfn { get; set; }
		public int TreatmentOfferBQmorethan { get; set; }
		public string InfoText { get; set; }
		public string AlternativeTreatmentOfferText { get; set; }
		public string AlternativeTreatmentOfferEmail { get; set; }
		public Language Language { get; set; }
		public int MinUserCountToDisclose { get; set; }
		public string EmailFrom { get; set; }
		
		public bool HasSuperSponsor {
			get { return SuperSponsor != null; }
		}
		
		// FIXME: These are not necessary properties
		public DateTime? MinimumInviteDate { get; set; }
		public bool Closed { get { return ClosedAt != null; } }
		public List<SponsorInvite> SentInvites { get; set; }
		public List<SponsorInvite> ActiveInvites { get; set; }
		public bool SuperUser { get; set; }
	}
	
	public interface IExtendedSurvey : IBaseModel
	{
		ProjectRound ProjectRound { get; set; }
		string EmailSubject { get; set; }
		string EmailBody { get; set; }
		DateTime? EmailLastSent { get; set; }
		string FinishedEmailSubject { get; set; }
		string FinishedEmailBody { get; set; }
		string ExtraEmailSubject { get; set; }
		string ExtraEmailBody { get; set; }
		string Internal { get; set; }
		string RoundText { get; set; }
		int ExtraExtendedSurveyId { get; set; }
		DateTime? FinishedLastSent { get; set; }
	}
	
	public class SponsorExtendedSurvey : BaseModel, IExtendedSurvey
	{
		public Sponsor Sponsor { get; set; }
		public ProjectRound ProjectRound { get; set; }
		public string Internal { get; set; }
		public string RoundText { get; set; }
		public string IndividualFeedbackEmailSubject { get; set; }
		public string IndividualFeedbackEmailBody { get; set; }
		public DateTime? EmailLastSent { get; set; }
		public string EmailSubject { get; set; }
		public string EmailBody { get; set; }
		public string FinishedEmailSubject { get; set; }
		public string FinishedEmailBody { get; set; }
		public string ExtraEmailSubject { get; set; }
		public string ExtraEmailBody { get; set; }
		public int RequiredUserCount { get; set; }
		public ProjectRound PreviousProjectRound { get; set; }
		public Feedback Feedback { get; set; }
		public DateTime? FinishedLastSent { get; set; }
		public int Answers { get; set; }
		public int Total { get; set; }
		
		public int WarnIfMissingQID { get; set; }
		public string RoundText2 { get; set; }
		public int ExtraExtendedSurveyId { get; set; }
	}
	
	public class SponsorAdmin : BaseModel, ISponsor
	{
		public Sponsor Sponsor { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Usr { get; set; }
		public bool ReadOnly { get; set; }
		public bool SuperUser { get; set; }
		public string Password { get; set; }
		public bool SeeUsers { get; set; }
		public bool Anonymized { get; set; }
		public string LastName { get; set; }
		public bool PermanentlyDeleteUsers { get; set; }
		public string InviteSubject { get; set; }
		public string InviteText { get; set; }
		public DateTime? InviteLastSent { get; set; }
		public DateTime? InviteReminderLastSent { get; set; }
		public string InviteReminderSubject { get; set; }
		public string InviteReminderText { get; set; }
		public string AllMessageSubject { get; set; }
		public string AllMessageBody { get; set; }
		public DateTime? AllMessageLastSent { get; set; }
		public IList<SponsorAdminFunction> Functions { get; set; }
		public IList<SponsorAdminDepartment> Departments { get; set; }
		public IList<SponsorAdminExtendedSurvey> ExtendedSurveys { get; set; }
		
		public SponsorAdmin()
		{
			Functions = new List<SponsorAdminFunction>();
		}
		
		public bool HasExtendedSurveys {
			get { return ExtendedSurveys.Count > 0; }
		}
		
		public void AddFunctions(IList<ManagerFunction> functions)
		{
			foreach (var f in functions) {
				AddFunction(f);
			}
		}
		
		public void AddFunction(ManagerFunction f)
		{
			AddFunction(new SponsorAdminFunction { Function = f });
		}
		
		public void AddFunction(SponsorAdminFunction f)
		{
			f.Admin = this;
			Functions.Add(f);
		}
		
		public string GetLoginDays(string notActivated, string notOnRecord, string days)
		{
			if (Password == null || Password == "") {
				return notActivated;
			} else if (LoginDays == -1) {
				return notOnRecord;
			} else {
				return string.Format("{0} {1}", LoginDays, days);
			}
		}
		
		public override string ToString()
		{
			return Name == "" ? (Usr == "" ? "&gt; empty &lt;" : Usr) : Name;
		}
		
		public override void Validate()
		{
			base.Validate();
		}
		
		public bool SuperAdmin {
			get { return SuperAdminId > 0; }
		}
		public int SuperAdminId { get; set; }
		
		public string LoginSubject { get; set; }
		public string LoginText { get; set; }
		public int? LoginDays { get; set; }
		public int? LoginWeekDay { get; set; }
		public DateTime? LoginLastSent { get; set; }
		public string EmailFrom { get; set; }
		public int MinUserCountToDisclose { get; set; }
	}
	
	public class SponsorAdminExtendedSurvey : BaseModel, IExtendedSurvey
	{
		public SponsorAdmin SponsorAdmin { get; set; }
		public string EmailSubject { get; set; }
		public string EmailBody { get; set; }
		public DateTime? EmailLastSent { get; set; }
		public string FinishedEmailSubject { get; set; }
		public string FinishedEmailBody { get; set; }
		public string ExtraEmailSubject { get; set; }
		public string ExtraEmailBody { get; set; }
		public ProjectRound ProjectRound { get; set; }
		public DateTime? FinishedLastSent { get; set; }
		
		public string Internal { get; set; }
		public string RoundText { get; set; }
		public int ExtraExtendedSurveyId { get; set; }
	}
	
	public class SponsorAdminSession : BaseModel
	{
		public SponsorAdmin Admin { get; set; }
		public DateTime Date { get; set; }
	}
	
	public class SponsorAdminSessionFunction : BaseModel
	{
		public SponsorAdminSession Session { get; set; }
		public ManagerFunction ManagerFunction { get; set; }
		public DateTime Date { get; set; }
	}
	
	public class SponsorAdminDepartment : BaseModel
	{
		public SponsorAdmin Admin { get; set; }
		public Department Department { get; set; }
	}
	
	public class SponsorAdminFunction : BaseModel
	{
		public SponsorAdmin Admin { get; set; }
		public ManagerFunction Function { get; set; }
	}
	
	public class SponsorBackgroundQuestion : BaseModel
	{
		public Sponsor Sponsor { get; set; }
		public BackgroundQuestion BackgroundQuestion { get; set; }
		public int Hidden { get; set; }
		public int InGrpAdmin { get; set; }
		public int Fn { get; set; }
	}
	
	public class SponsorInvite : BaseModel
	{
		public Sponsor Sponsor { get; set; }
		public Department Department { get; set; }
		public string Email { get; set; }
		public User User { get; set; }
		public int StoppedReason { get; set; }
		public DateTime? Stopped { get; set; }
		public IList<SponsorInviteBackgroundQuestion> SponsorInviteBackgroundQuestions { get; set; }
		public string InvitationKey { get; set; }
		public DateTime? Sent { get; set; }
		public int PreviewExtendedSurveys { get; set; }
	}
	
	public class SponsorInviteBackgroundQuestion : BaseModel
	{
		public SponsorInvite Invite { get; set; }
		public BackgroundQuestion BackgroundQuestion { get; set; }
		public BackgroundAnswer BackgroundAnswer { get; set; }
		public int ValueInt { get; set; }
		public DateTime? ValueDate { get; set; }
		public string ValueText { get; set; }
	}
	
	public class SponsorLanguage : BaseModel
	{
		public Sponsor Sponsor { get; set; }
		public Language Language { get; set; }
	}
	
	public class SponsorLogo : BaseModel
	{
		public Sponsor Sponsor { get; set; }
	}
	
	public class SponsorProject : BaseModel
	{
		public Sponsor Sponsor { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string ProjectName { get; set; }
	}
	
	public class SponsorProjectMeasure : BaseModel
	{
		public SponsorProject SponsorProject { get; set; }
		public Measure Measure { get; set; }
	}
	
	public class SponsorProjectRoundUnit : BaseModel
	{
		public Sponsor Sponsor { get; set; }
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public string Navigation { get; set; }
		public Survey Survey { get; set; }
	}
	
	public class SponsorProjectRoundUnitLanguage : BaseModel
	{
		public SponsorProjectRoundUnit SponsorProjectRoundUnit { get; set; }
		public Language Language { get; set; }
		public string Navigation { get; set; }
		
		public override string ToString()
		{
			return Language.ToString();
		}
	}
	
	public class SponsorAdminExercise : BaseModel
	{
		public DateTime? Date { get; set; }
		public SponsorAdmin SponsorAdmin { get; set; }
		public ExerciseVariantLanguage ExerciseVariantLanguage { get; set; }
	}
	
	public class SponsorAdminExerciseDataInput : BaseModel
	{
		public SponsorAdminExercise SponsorAdminExercise { get; set; }
		public string Content { get; set; }
		public int Order { get; set; }
	}
	
//	public class SponsorExerciseDataInput : BaseModel
//	{
//		public Sponsor Sponsor { get; set; }
//		public string Content { get; set; }
//		public int Order { get; set; }
//	}
	
	public class SuperAdmin : BaseModel
	{
		public string Name { get; set; }
		public string Password { get; set; }
		public bool HideClosedSponsors { get; set; }
	}
	
	public class SuperAdminSponsor : BaseModel
	{
		public SuperAdmin Admin { get; set; }
		public Sponsor Sponsor { get; set; }
		public bool SeeUsers { get; set; }
	}
	
	public class SuperSponsor : BaseModel
	{
		public string Name { get; set; }
		public string Logo { get; set; }
		public IList<SuperSponsorLanguage> Languages { get; set; }
	}
	
	public class SuperSponsorLanguage : BaseModel
	{
		public SuperSponsor Sponsor { get; set; }
		public Language Language { get; set; }
		public string Slogan { get; set; }
		public string Header { get; set; }
	}
}
