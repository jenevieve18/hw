using System;
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
		int LoginDays { get; set; }
		int LoginWeekday { get; set; }
		DateTime? LoginLastSent { get; set; }
		string EmailFrom { get; set; }
		int MinUserCountToDisclose { get; set; }
		bool SuperUser { get; set; }
	}
	
	public class Sponsor : BaseModel, ISponsor
	{
		public virtual string Name { get; set; }
		public virtual string Application { get; set; }
		public virtual ProjectRoundUnit ProjectRoundUnit { get; set; }
		public virtual DateTime? ClosedAt { get; set; }
		public virtual DateTime DeletedAt { get; set; }
		public virtual string ConsentText { get; set; }
		public virtual SuperSponsor SuperSponsor { get; set; }
		public virtual string InviteSubject { get; set; }
		public virtual string InviteText { get; set; }
		public virtual DateTime? InviteLastSent { get; set; }
		public virtual DateTime? InviteReminderLastSent { get; set; }
		public virtual string InviteReminderSubject { get; set; }
		public virtual string InviteReminderText { get; set; }
		public virtual string AllMessageSubject { get; set; }
		public virtual string AllMessageBody { get; set; }
		public virtual DateTime? AllMessageLastSent { get; set; }
		public virtual string LoginSubject { get; set; }
		public virtual string LoginText { get; set; }
		public virtual int LoginDays { get; set; }
		public virtual int LoginWeekday { get; set; }
		public virtual DateTime? LoginLastSent { get; set; }
		public virtual string SponsorKey { get; set; }
		public virtual IList<SponsorProjectRoundUnit> RoundUnits { get; set; }
		public virtual List<SponsorInvite> Invites { get; set; }
		public virtual List<SponsorExtendedSurvey> ExtendedSurveys { get; set; }
		public virtual IList<SuperAdminSponsor> SuperAdminSponsors { get; set; }
		public virtual IList<SponsorAdmin> Admins { get; set; }
		public virtual IList<SponsorBackgroundQuestion> BackgroundQuestions { get; set; }
		public virtual int TreatmentOffer { get; set; }
		public virtual string TreatmentOfferText { get; set; }
		public virtual string TreatmentOfferEmail { get; set; }
		public virtual string TreatmentOfferIfNeededText { get; set; }
		public virtual int TreatmentOfferBQ { get; set; }
		public virtual int TreatmentOfferBQfn { get; set; }
		public virtual int TreatmentOfferBQmorethan { get; set; }
		public virtual string InfoText { get; set; }
		public virtual string AlternativeTreatmentOfferText { get; set; }
		public virtual string AlternativeTreatmentOfferEmail { get; set; }
		public virtual Language Language { get; set; }
		public virtual int MinUserCountToDisclose { get; set; }
		public virtual string EmailFrom { get; set; }
		
		// FIXME: These are not necessary properties
		public virtual DateTime? MinimumInviteDate { get; set; }
		public virtual bool Closed { get { return ClosedAt != null; } }
		public virtual List<SponsorInvite> SentInvites { get; set; }
		public virtual List<SponsorInvite> ActiveInvites { get; set; }
		public virtual bool SuperUser { get; set; }
		
		User user;
		
		// HACK: The reference of this is used by SqlUserRepository.ReadByIdAndSponsorExtendedSurvey2
		public virtual User User {
			get { return user; }
			set { user = value; user.Sponsor = this; }
		}
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
		public virtual Sponsor Sponsor { get; set; }
		public virtual ProjectRound ProjectRound { get; set; }
		public virtual string Internal { get; set; }
		public virtual string RoundText { get; set; }
		public virtual string IndividualFeedbackEmailSubject { get; set; }
		public virtual string IndividualFeedbackEmailBody { get; set; }
		public virtual DateTime? EmailLastSent { get; set; }
		public virtual string EmailSubject { get; set; }
		public virtual string EmailBody { get; set; }
		public virtual string FinishedEmailSubject { get; set; }
		public virtual string FinishedEmailBody { get; set; }
		public virtual string ExtraEmailSubject { get; set; }
		public virtual string ExtraEmailBody { get; set; }
		public virtual int RequiredUserCount { get; set; }
		public virtual ProjectRound PreviousProjectRound { get; set; }
		public virtual Feedback Feedback { get; set; }
		public virtual DateTime? FinishedLastSent { get; set; }
		
		public virtual int WarnIfMissingQID { get; set; }
		public virtual string RoundText2 { get; set; }
		public virtual int ExtraExtendedSurveyId { get; set; }
	}
	
	public class SponsorAdmin : BaseModel, ISponsor
	{
		public virtual Sponsor Sponsor { get; set; }
		public virtual string Name { get; set; }
		public virtual string Email { get; set; }
		public virtual string Usr { get; set; }
		public virtual bool ReadOnly { get; set; }
		public virtual bool SuperUser { get; set; }
		public virtual string Password { get; set; }
		public virtual bool SeeUsers { get; set; }
		public virtual bool Anonymized { get; set; }
		public virtual IList<SponsorAdminFunction> Functions { get; set; }
		public virtual IList<SponsorAdminDepartment> Departments { get; set; }
		public virtual string LastName { get; set; }
		public virtual bool PermanentlyDeleteUsers { get; set; }
		public virtual IList<SponsorAdminExtendedSurvey> ExtendedSurveys { get; set; }
		public virtual string InviteSubject { get; set; }
		public virtual string InviteText { get; set; }
		public virtual DateTime? InviteLastSent { get; set; }
		public virtual DateTime? InviteReminderLastSent { get; set; }
		public virtual string InviteReminderSubject { get; set; }
		public virtual string InviteReminderText { get; set; }
		public virtual string AllMessageSubject { get; set; }
		public virtual string AllMessageBody { get; set; }
		public virtual DateTime? AllMessageLastSent { get; set; }
		
		public bool HasExtendedSurveys {
			get { return ExtendedSurveys.Count > 0; }
		}
		
		public string GetLoginDays()
		{
			if (Password == "") {
				return "Not activated";
			} else if (LoginDays == -1) {
				return "Not on record";
			} else {
				return LoginDays.ToString();
			}
		}
		
		public override string ToString()
		{
			return Name == "" ? (Usr == "" ? "&gt; empty &lt;" : Usr) : Name;
		}
		
		public override void Validate()
		{
			base.Validate();
			AddErrorIf(Name == "", "Sponsor admin name is required.");
			AddErrorIf(Email == "", "Email address name is required.");
			AddErrorIf(Usr == "", "User name is required.");
			AddErrorIf(Password == "", "Password is required.");
		}
		
		public virtual bool SuperAdmin {
			get { return SuperAdminId > 0; }
		}
		public virtual int SuperAdminId { get; set; }
		
		public virtual string LoginSubject { get; set; }
		public virtual string LoginText { get; set; }
		public virtual int LoginDays { get; set; }
		public virtual int LoginWeekday { get; set; }
		public virtual DateTime? LoginLastSent { get; set; }
		public virtual string EmailFrom { get; set; }
		public virtual int MinUserCountToDisclose { get; set; }
	}
	
	public class SponsorAdminExtendedSurvey : BaseModel, IExtendedSurvey
	{
		public virtual SponsorAdmin SponsorAdmin { get; set; }
		public virtual string EmailSubject { get; set; }
		public virtual string EmailBody { get; set; }
		public virtual DateTime? EmailLastSent { get; set; }
		public virtual string FinishedEmailSubject { get; set; }
		public virtual string FinishedEmailBody { get; set; }
		public virtual string ExtraEmailSubject { get; set; }
		public virtual string ExtraEmailBody { get; set; }
		public virtual ProjectRound ProjectRound { get; set; }
		public virtual DateTime? FinishedLastSent { get; set; }
		
		public virtual string Internal { get; set; }
		public virtual string RoundText { get; set; }
		public virtual int ExtraExtendedSurveyId { get; set; }
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
		public virtual SponsorAdmin Admin { get; set; }
		public virtual Department Department { get; set; }
	}
	
	public class SponsorAdminFunction : BaseModel
	{
		public virtual SponsorAdmin Admin { get; set; }
		public virtual ManagerFunction Function { get; set; }
	}
	
	public class SponsorBackgroundQuestion : BaseModel
	{
		public virtual Sponsor Sponsor { get; set; }
		public virtual BackgroundQuestion BackgroundQuestion { get; set; }
		public virtual int Hidden { get; set; }
		public virtual int InGrpAdmin { get; set; }
		public virtual int Fn { get; set; }
	}
	
	public class SponsorInvite : BaseModel
	{
		public virtual Sponsor Sponsor { get; set; }
		public virtual Department Department { get; set; }
		public virtual string Email { get; set; }
		public virtual User User { get; set; }
		public virtual int StoppedReason { get; set; }
		public virtual DateTime? Stopped { get; set; }
		public virtual IList<SponsorInviteBackgroundQuestion> SponsorInviteBackgroundQuestions { get; set; }
		public virtual string InvitationKey { get; set; }
		public virtual DateTime? Sent { get; set; }
		public virtual int PreviewExtendedSurveys { get; set; }
	}
	
	public class SponsorInviteBackgroundQuestion : BaseModel
	{
		public virtual SponsorInvite Invite { get; set; }
		public virtual BackgroundQuestion BackgroundQuestion { get; set; }
		public virtual BackgroundAnswer BackgroundAnswer { get; set; }
		public virtual int ValueInt { get; set; }
		public virtual DateTime? ValueDate { get; set; }
		public virtual string ValueText { get; set; }
	}
	
	public class SponsorLanguage : BaseModel
	{
		public virtual Sponsor Sponsor { get; set; }
		public virtual Language Language { get; set; }
	}
	
	public class SponsorLogo : BaseModel
	{
		public virtual Sponsor Sponsor { get; set; }
	}
	
	public class SponsorProjectRoundUnit : BaseModel
	{
		public virtual Sponsor Sponsor { get; set; }
		public virtual ProjectRoundUnit ProjectRoundUnit { get; set; }
		public virtual string Navigation { get; set; }
		public virtual Survey Survey { get; set; }
	}
	
	public class SponsorProjectRoundUnitLanguage : BaseModel
	{
		public virtual SponsorProjectRoundUnit SponsorProjectRoundUnit { get; set; }
		public virtual Language Language { get; set; }
		public virtual string Navigation { get; set; }
		
		public override string ToString()
		{
			return Language.ToString();
		}
	}
	
	public class SuperAdmin : BaseModel
	{
		public virtual string Name { get; set; }
		public virtual string Password { get; set; }
		public virtual bool HideClosedSponsors { get; set; }
	}
	
	public class SuperAdminSponsor : BaseModel
	{
		public virtual SuperAdmin Admin { get; set; }
		public virtual Sponsor Sponsor { get; set; }
		public virtual bool SeeUsers { get; set; }
	}
	
	public class SuperSponsor : BaseModel
	{
		public virtual string Name { get; set; }
		public virtual string Logo { get; set; }
		public virtual IList<SuperSponsorLanguage> Languages { get; set; }
	}
	
	public class SuperSponsorLanguage : BaseModel
	{
		public virtual SuperSponsor Sponsor { get; set; }
		public virtual Language Language { get; set; }
		public virtual string Slogan { get; set; }
		public virtual string Header { get; set; }
	}
}
