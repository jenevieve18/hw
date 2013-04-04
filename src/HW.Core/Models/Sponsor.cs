//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class Sponsor : BaseModel
	{
		public string Name { get; set; }
		public string Application { get; set; }
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public string LoginText { get; set; }
		public DateTime? ClosedAt { get; set; }
		public DateTime DeletedAt { get; set; }
		public string ConsentText { get; set; }
		public SuperSponsor SuperSponsor { get; set; }
		public IList<SponsorProjectRoundUnit> RoundUnits { get; set; }
		public IList<SponsorInvite> Invites { get; set; }
		public IList<SponsorExtendedSurvey> ExtendedSurveys { get; set; }
		public IList<SuperAdminSponsor> SuperAdminSponsors { get; set; }
		public string InviteText { get; set; }
		public string InviteReminderText { get; set; }
		public string InviteSubject { get; set; }
		public string InviteReminderSubject { get; set; }
		public string LoginSubject { get; set; }
		public DateTime? InviteLastSent { get; set; }
		public DateTime? InviteReminderLastSent { get; set; }
		public DateTime? LoginLastSent { get; set; }
		public int LoginDays { get; set; }
		public int LoginWeekday { get; set; }
		public string AllMessageSubject { get; set; }
		public string AllMessageBody { get; set; }
		public DateTime? AllMessageLastSent { get; set; }
		public string SponsorKey { get; set; }
		
		public DateTime? MinimumInviteDate { get; set; }
		public bool Closed { get { return ClosedAt != null; } }
		public IList<SponsorInvite> SentInvites { get; set; }
		public IList<SponsorInvite> ActiveInvites { get; set; }
	}
	
	public class SponsorAdmin : BaseModel
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
		
		public bool SuperAdmin { get; set; } // FIXME: Used with Default to determine whether it's a SuperAdmin who logs in.
	}
	
	public class SponsorExtendedSurvey : BaseModel
	{
		public Sponsor Sponsor { get; set; }
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public string Internal { get; set; }
		public string RoundText { get; set; }
		public string IndividualFeedbackEmailSubject { get; set; }
		public string IndividualFeedbackEmailBody { get; set; }
		public DateTime EmailLastSent { get; set; }
		public string EmailSubject { get; set; }
		public string EmailBody { get; set; }
		public string FinishedEmailSubject { get; set; }
		public string FinishedEmailBody { get; set; }
		public string ExtraEmailSubject { get; set; }
		public string ExtraEmailBody { get; set; }
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
		public BackgroundQuestion Question { get; set; }
	}
	
	public class SponsorInvite : BaseModel
	{
		public Sponsor Sponsor { get; set; }
		public Department Department { get; set; }
		public string Email { get; set; }
		public User User { get; set; }
		public int StoppedReason { get; set; }
		public DateTime? Stopped { get; set; }
		public IList<SponsorInviteBackgroundQuestion> BackgroundQuestions { get; set; }
		
		public string InvitationKey { get; set; }
	}
	
	public class SponsorInviteBackgroundQuestion : BaseModel
	{
		public SponsorInvite Invite { get; set; }
		public BackgroundQuestion Question { get; set; }
		public BackgroundAnswer Answer { get; set; }
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
	
	public class SuperAdmin : BaseModel
	{
		public string Name { get; set; }
		public string Password { get; set; }
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
