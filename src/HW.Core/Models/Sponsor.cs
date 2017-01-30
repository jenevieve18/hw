using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public interface IAdmin
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
		int? LoginWeekday { get; set; }
		DateTime? LoginLastSent { get; set; }
		string EmailFrom { get; set; }
		int MinUserCountToDisclose { get; set; }
		bool SuperUser { get; set; }
	}
	
	public class Sponsor : BaseModel, IAdmin
	{
		public int SponsorID { get; set; }
		public string Name { get; set; }
		public string Application { get; set; }
		public int ProjectRoundUnitID { get; set; }
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public DateTime? Closed { get; set; }
		public DateTime? Deleted { get; set; }
		public int SuperSponsorID { get; set; }
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
		public int ForceLID { get; set; }
		public string LoginSubject { get; set; }
		public string LoginText { get; set; }
		public int? LoginDays { get; set; }
		public int? LoginWeekday { get; set; }
		public int LID { get; set; }
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
		public Guid? SponsorApiKey { get; set; }
		public Language Language { get; set; }
		public int MinUserCountToDisclose { get; set; }
		public string EmailFrom { get; set; }
		public string Comment { get; set; }
		public int DefaultPlotType { get; set; }
		
		public bool HasSuperSponsor {
			get { return SuperSponsor != null; }
		}
		
		// FIXME: These are not necessary properties
		public DateTime? MinimumInviteDate { get; set; }
		public bool IsClosed { get { return Closed != null; } }
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
		
		public int SponsorExtendedSurveyID { get; set; }
		public int SponsorID { get; set; }
		public int ProjectRoundID { get; set; }
		public int EformFeedbackID { get; set; }
		public int PreviousProjectRoundID { get; set; }
		public int IndividualFeedbackID { get; set; }
		public int WarnIfMissingQID { get; set; }
		
		public string RoundText2 { get; set; }
		public int ExtraExtendedSurveyId { get; set; }
	}
	
	public class SponsorAdmin : BaseModel, IAdmin
	{
		public int SponsorAdminID { get; set; }
		public Sponsor Sponsor { get; set; }
		public int SponsorID { get; set; }
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
			ExtendedSurveys = new List<SponsorAdminExtendedSurvey>();
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
//			} else if (LoginDays == -1) {
			} else if (HaveNotLoggedIn) {
				return notOnRecord;
			} else {
				return string.Format("{0} {1}", LoginDays, days);
			}
		}
		
		public bool HaveNotLoggedIn {
			get { return LoginDays == -1; }
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
		public int? LoginWeekday { get; set; }
		public DateTime? LoginLastSent { get; set; }
		public string EmailFrom { get; set; }
		public int MinUserCountToDisclose { get; set; }
		
		public Guid? SponsorAdminKey { get; set; }
		public string InviteTxt { get; set; }
		public string InviteReminderTxt { get; set; }
		public string UniqueKey { get; set; }
		public int UniqueKeyUsed { get; set; }
		
		public override object ToObject()
		{
			return new {
				id = Id
			};
		}
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
		public int SponsorBQID { get; set; }
		public int SponsorID { get; set; }
		public int BQID { get; set; }
		public int Forced { get; set; }
		public int SortOrder { get; set; }
		public int IncludeInTreatmentReq { get; set; }
		public int Organize { get; set; }
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
	
	public class SponsorLang : BaseModel
	{
		public Sponsor Sponsor { get; set; }
		public Language Language { get; set; }
	}
	
	public class SponsorLogo : BaseModel
	{
		public Sponsor Sponsor { get; set; }
	}
	
	public class SponsorProjectMeasure : BaseModel
	{
		public SponsorProject SponsorProject { get; set; }
		public Measure Measure { get; set; }
	}
	
	public class SponsorProject : BaseModel, IReportPart
	{
		public Sponsor Sponsor { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string Subject { get; set; } // Project Name
		public string Header { get; set; }
		public IList<SponsorProjectMeasure> Measures { get; set; }
		
		public ReportPart ReportPart { get; set; }
		
		public SponsorProject()
		{
			Measures = new List<SponsorProjectMeasure>();
		}
		
		public List<ReportPartComponent> Components { get; set; }
		public IList<ReportPartLang> Languages { get; set; }
	}
	
	public class SponsorProjectRoundUnit : BaseModel
	{
		public Sponsor Sponsor { get; set; }
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public string Navigation { get; set; }
		public Survey Survey { get; set; }
		public int DefaultAggregation { get; set; }
		public int SponsorProjectRoundUnitID { get; set; }
		public int SponsorID { get; set; }
		public int ProjectRoundUnitID { get; set; }
//		public Guid SurveyKey { get; set; }
		public string SurveyKey { get; set; }
		public int SortOrder { get; set; }
		public string Feedback { get; set; }
		public int Ext { get; set; }
		public int SurveyID { get; set; }
		public int OnlyEveryDays { get; set; }
		public int GoToStatistics { get; set; }
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
		public bool HasSponsorAdmin { get { return SponsorAdmin != null; }}
		public ExerciseVariantLanguage ExerciseVariantLanguage { get; set; }
		public bool HasExerciseVariantLanguage { get { return ExerciseVariantLanguage != null; } }
		public IList<SponsorAdminExerciseDataInput> Inputs { get; set; }
		
		public SponsorAdminExercise()
		{
			Inputs = new List<SponsorAdminExerciseDataInput>();
		}
		
		public void AddDataInputs(IList<SponsorAdminExerciseDataInput> inputs)
		{
			foreach (var i in inputs) {
				AddDataInput(i);
			}
		}
		
		public void AddDataInput(SponsorAdminExerciseDataInput input)
		{
			input.SponsorAdminExercise = this;
			Inputs.Add(input);
		}
		
		public override object ToObject()
		{
			var inputs = new List<object>();
			foreach (var i in Inputs) {
				inputs.Add(i.ToObject());
			}
			return new {
				id = Id,
				date = Date,
				sponsorAdmin = HasSponsorAdmin ? SponsorAdmin.ToObject() : null,
				exerciseVariantLanguage = HasExerciseVariantLanguage ? ExerciseVariantLanguage.ToObject() : null,
				inputs = inputs
			};
		}
	}
	
	public class SponsorAdminExerciseDataInput : BaseModel
	{
		public SponsorAdminExercise SponsorAdminExercise { get; set; }
		public string ValueText { get; set; }
		public int SortOrder { get; set; }
		public int ValueInt { get; set; }
		public int Type { get; set; }
		public IList<SponsorAdminExerciseDataInputComponent> Components { get; set; }
		
		public SponsorAdminExerciseDataInput()
		{
			Components = new List<SponsorAdminExerciseDataInputComponent>();
		}
		
		public void AddComponents(IList<SponsorAdminExerciseDataInputComponent> components)
		{
			foreach (var c in components) {
				AddComponent(c);
			}
		}
		
		public void AddComponent(SponsorAdminExerciseDataInputComponent component)
		{
			component.SponsorAdminExerciseDataInput = this;
			Components.Add(component);
		}
		
		public override object ToObject()
		{
			var components = new List<object>();
			foreach (var c in Components) {
				components.Add(c.ToObject());
			}
			return new {
				id = Id,
				valueText = ValueText,
				valueInt = ValueInt,
				sorOrder = SortOrder,
				type = Type,
				components = components
			};
		}
	}
	
	public class SponsorAdminExerciseDataInputComponent : BaseModel
	{
		public SponsorAdminExerciseDataInput SponsorAdminExerciseDataInput { get; set; }
		public string ValueText { get; set; }
		public int SortOrder { get; set; }
		public int ValueInt { get; set; }
		public string Class { get; set; }
		
		public override object ToObject()
		{
			return new {
				valueText = ValueText,
				valueInt = ValueInt,
				sortOrder = SortOrder,
				@class = Class
			};
		}
	}
	
//	public class SponsorAdminExerciseDataInputType
//	{
//		public const int SingleChoice = 1;
//		public const int FreeText = 2;
//		public const int MultiChoice = 3;
//		public const int Numeric = 4;
//		public const int VAS = 9;
//	}
//
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
