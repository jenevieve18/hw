//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Web;
using MySql.Data.MySqlClient;

namespace HW.Core
{
	public class BaseModel
	{
		public int Id { get; set; }
	}
	
	public class Affiliate : BaseModel
	{
		public string Name { get; set; }
	}
	
	public class BackgroundAnswer : BaseModel
	{
		public string Internal { get; set; }
		public int SortOrder { get; set; }
		public int Value { get; set; }
		public IList<BackgroundAnswerLanguage> Languages { get; set; }
	}
	
	public class BackgroundAnswerLanguage : BaseModel
	{
		public Language Language { get; set; }
		public BackgroundAnswer Answer { get; set; }
	}
	
	public class BackgroundQuestion : BaseModel
	{
		public string Internal { get; set; }
		public int Type { get; set; }
		public string DefaultValue { get; set; }
		public int Comparison { get; set; }
		public string Variable { get; set; }
		public IList<BackgroundQuestionLanguage> Languages { get; set; }
		public int Restricted { get; set; }
	}
	
	public class BackgroundQuestionLanguage : BaseModel
	{
		public Language Language { get; set; }
		public BackgroundQuestion Question { get; set; }
	}
	
	public class BackgroundQuestionVisibility : BaseModel
	{
		public BackgroundQuestion Question { get; set; }
		public BackgroundAnswer Answer { get; set; }
		public BackgroundQuestion Child { get; set; }
	}
	
	public class CX : BaseModel
	{
	}
	
	public class Department : BaseModel
	{
		public Sponsor Sponsor { get; set; }
		public string Name { get; set; }
		public Department Parent { get; set; }
		public int SortOrder { get; set; }
		public string SortString { get; set; }
		public string ShortName { get; set; }
		public string AnonymizedName { get; set; }
		
		public int Depth { get; set; }
		public int Siblings { get; set; }
		public string TreeName { get; set; }
	}
	
	public class Diary : BaseModel
	{
		public string Note { get; set; }
		public DateTime Date { get; set; }
		public User User { get; set; }
		public DateTime Created { get; set; }
		public DateTime Deleted { get; set; }
		public int Mood { get; set; }
	}
	
	public class Exercise : BaseModel
	{
		public string Image { get; set; }
		public ExerciseCategory Category { get; set; }
		public ExerciseArea Area { get; set; }
		public int SortOrder { get; set; }
		public int Minutes { get; set; }
		public int RequiredUserLevel { get; set; }
		public string ReplacementHead { get; set; }
		public IList<ExerciseLanguage> Languages { get; set; }
		
		public ExerciseLanguage CurrentLanguage { get; set; }
		public ExerciseAreaLanguage CurrentArea { get; set; }
		public ExerciseCategoryLanguage CurrentCategory { get; set; }
		public ExerciseVariantLanguage CurrentVariant { get; set; }
		public ExerciseTypeLanguage CurrentType { get; set; }
	}
	
	public class ExerciseArea : BaseModel
	{
		public string Image { get; set; }
		public int SortOrder { get; set; }
		public IList<ExerciseAreaLanguage> Languages { get; set; }
	}
	
	public class ExerciseAreaLanguage : BaseModel
	{
		public ExerciseArea Area { get; set; }
		public Language Language { get; set; }
		public string AreaName { get; set; }
	}
	
	public class ExerciseCategory : BaseModel
	{
		public int SortOrder { get; set; }
		public IList<ExerciseCategoryLanguage> Languages { get; set; }
	}
	
	public class ExerciseCategoryLanguage : BaseModel
	{
		public Language Language { get; set; }
		public ExerciseCategory Category { get; set; }
		public string CategoryName { get; set; }
	}
	
	public class ExerciseLanguage : BaseModel
	{
		public Exercise Exercise { get; set; }
		public string ExerciseName { get; set; }
		public string Time { get; set; }
		public string Teaser { get; set; }
		public Language Language { get; set; }
		public bool IsNew { get; set; }
	}
	
	public class ExerciseMiracle : BaseModel
	{
		public User User { get; set; }
		public DateTime Time { get; set; }
		public DateTime TimeChanged { get; set; }
		public string MiracleDescription { get; set; }
		public bool AllowPublished { get; set; }
		public bool Published { get; set; }
	}
	
	public class ExerciseStats : BaseModel
	{
		public User User { get; set; }
		public ExerciseVariantLanguage VariantLanguage { get; set; }
		public DateTime Date { get; set; }
		public UserProfile UserProfile { get; set; }
	}
	
	public class ExerciseType : BaseModel
	{
		public int SortOrder { get; set; }
		public IList<ExerciseTypeLanguage> Languages { get; set; }
	}
	
	public class ExerciseTypeLanguage : BaseModel
	{
		public ExerciseType Type { get; set; }
		public Language Language { get; set; }
		public string TypeName { get; set; }
		public string SubTypeName { get; set; }
	}
	
	public class ExerciseVariant : BaseModel
	{
		public Exercise Exercise { get; set; }
		public ExerciseType Type { get; set; }
		public IList<ExerciseVariantLanguage> Languages { get; set; }
	}
	
	public class ExerciseVariantLanguage : BaseModel
	{
		public ExerciseVariant Variant { get; set; }
		public string File { get; set; }
		public int Size { get; set; }
		public string Content { get; set; }
		public int ExerciseWindowX { get; set; }
		public int ExerciseWindowY { get; set; }
	}
	
	public class Language : BaseModel
	{
		public string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
	
	public interface IGraphType
	{
		ExtendedGraph Graph { get; set; }
		void Draw(List<Series> series);
	}
	
//	public class BoxPlot : IHWList, IGraphType
//	{
//		public double Mean { get; set; }
//		public double LowerWhisker { get; set;  }
//		public double UpperWhisker { get; set; }
//		public double LowerBox { get; set; }
//		public double UpperBox { get; set; }
//		public double Median { get; set; }
//		public ExtendedGraph Graph { get; set; }
//
//		public BoxPlot(IHWList plot)
//		{
//			this.Mean = plot.Mean;
//		}
//
//		public void Draw(List<Series> series)
//		{
//		}
//	}
//
	public class PointV
	{
		public int X { get; set; }
		public float Y { get; set; }
		public HWList Values { get; set; }
		public string Description { get; set; }
		
//		public int T { get; set; }
//		public float Deviation { get; set; }
//		public double LowerWhisker { get; set; }
//		public double UpperWhisker { get; set; }
//		public double LowerBox { get; set; }
//		public double UpperBox { get; set; }
//		public double Median { get; set; }
	}
	
	public class LineGraphType : IGraphType
	{
		public ExtendedGraph Graph { get; set; }
//		bool stdev;
		int point;
		int t;
		
//		public LineGraphType() : this(false, 2)
		public LineGraphType() : this(0, 2) // TODO: Map this point value to ExtraPoint class.
		{
		}
		
//		public LineGraphType(bool stdev, int t)
		public LineGraphType(int point, int t)
		{
//			this.stdev = stdev;
			this.point = point;
			this.t = t;
		}
		
		public void Draw(List<Series> series)
		{
			Graph.DrawExplanations();
			foreach (Series s in series) {
				Graph.drawColorExplBox(s.Description, s.Color, s.X, s.Y);
				for (int i = 0; i < s.Points.Count; i++) {
					PointV p = s.Points[i];
					HWList l = p.Values;
//					if (stdev) {
					if (point == 1) { // TODO: Map this to ExtraPoint class.
						Graph.DrawDeviation(s.Color, (int)p.X, (int)l.Mean, (int)l.StandardDeviation);
					} else if (point == 2) { // TODO: Map this to ExtraPoint class.
						Graph.DrawDeviation(s.Color, (int)p.X, (int)l.Mean, (int)l.ConfidenceInterval);
					}
					Graph.drawCircle((int)p.X, (int)l.Mean, 4);
					if (i > 0) {
						PointV pp = s.Points[i -1];
						HWList ll = pp.Values;
						Graph.drawStepLine(s.Color, (int)p.X, (int)l.Mean, (int)pp.X, (int)ll.Mean, t);
					}
				}
			}
		}
	}
	
	public class BarGraphTYpe : IGraphType
	{
		public ExtendedGraph Graph { get; set; }
		
		public void Draw(List<Series> series)
		{
//			Graph.DrawBars(new object(), 10, tot, bars);
			int steps = 10;
			
			Graph.setMinMax(0f, 100f);

			steps += 2;
			int tot = 2000;
			
			Graph.computeSteping((steps <= 1 ? 2 : steps));
			Graph.drawOutlines(11);
//			Graph.drawAxis(disabled);
			Graph.drawAxis(new object());

			int i = 0;
			decimal sum = 0;
			Series s = series[0];
//			foreach (Bar b in bars) {
			foreach (var p in s.Points) {
				i++;
				sum += (decimal)p.Y;
				Graph.drawBar(s.Color, i, p.Y);
				Graph.drawBottomString(p.Description, i, true);
//				if (b.HasReference) {
//					Graph.drawReference(i, b.Reference);
//				}
				Graph.drawReference(i, 12);
			}
//			foreach (int l in referenceLines) {
//				drawReferenceLine(l, " = riktvärde");
//			}
			if (tot > 0) {
				Graph.drawBar(4, ++i, Convert.ToInt32(Math.Round((tot - sum) / tot * 100M, 0)));
				Graph.drawBottomString("Inget svar", i, true);
			}
		}
	}
	
	public class BoxPlotGraphType : IGraphType
	{
		public ExtendedGraph Graph { get; set; }
		
		public void Draw(List<Series> series)
		{
			Series s = series[0];
			Graph.drawColorExplBox(s.Description, s.Color, s.X, s.Y);
			foreach (PointV p in s.Points) {
				HWList l = p.Values;
				Graph.DrawWhiskers((int)p.X, (int)l.UpperWhisker, (int)l.LowerWhisker);
				Graph.DrawBar2(s.Color, (int)p.X, (int)l.LowerBox, (int)l.UpperBox);
				Graph.DrawMedian((int)p.X, (int)l.Median);
			}
		}
	}
	
	public class Series
	{
		public List<PointV> Points { get; set; }
		public int Color { get; set; }
		public string Description { get; set; }
		
		public bool Right { get; set; }
		public bool Box { get; set; }
		public bool HasAxis { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		
		public Series()
		{
			Points = new List<PointV>();
		}
	}
	
	public class Bar
	{
		int reference = -1;
		
		public int Reference {
			get { return reference; }
			set { reference = value; }
		}
		public float Value { get; set; }
		public string Description { get; set; }
		public int Color { get; set; }
		public bool HasReference {
			get { return Reference >= 0 && Reference <= 100; }
		}
	}
	
	public class LanguageFactory
	{
		public static string GetMeanText(int lid)
		{
			return lid == 1 ? "medelvärde" : "mean value";
		}
		
		public static string GetGroupExercise(int lid)
		{
			switch (lid) {
					case 0: return "Grupp-<br/>övningar";
					case 1: return "Group-<br/>exercises";
					default: throw new NotSupportedException();
			}
		}
		
		public static string GetChooseArea(int lid)
		{
			// FIXME: Why 0 on Swedish? See setculture where Swedish is 1.
			switch (lid) {
					case 0: return "Välj område";
					case 1: return "Choose area";
					default: throw new NotSupportedException();
			}
		}
		
		public static string GetChooseCategory(int lid)
		{
			// FIXME: Why 0 on Swedish? See setculture where Swedish is 1.
			switch (lid) {
					case 0: return "Välj kategori";
					case 1: return "Choose category";
					default: throw new NotSupportedException();
			}
		}
		
		public static string GetSortingOrder(int lid, int bx)
		{
			// FIXME: Why 0 on Swedish? See setculture where Swedish is 1.
			switch (lid) {
					case 0: return bx + " övningar - Sortering:";
					case 1: return bx + " exercises - Order:";
					default: throw new NotSupportedException();
			}
		}
		
		public static string GetLegend(int lid)
		{
			// FIXME: Why 0 on Swedish? See setculture where Swedish is 1.
			switch (lid) {
					case 0: return ""; // TODO: Why?
					case 1: return ""; // TODO: Why?
					default: throw new NotSupportedException();
			}
		}
		
		public static void SetCurrentCulture(int lid)
		{
			switch (lid) {
					case 1: Thread.CurrentThread.CurrentCulture = new CultureInfo("sv-SE"); break;
					case 2: Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US"); break;
					default: throw new NotSupportedException();
			}
		}
	}
	
	public class ManagerFunction : BaseModel
	{
		public string Function { get; set; }
		public string URL { get; set; }
		public string Expl { get; set; }
	}
	
	public class Measure : BaseModel
	{
		public string Name { get; set; }
		public MeasureCategory Category { get; set; }
		public int SortOrder { get; set; }
		public string Description { get; set; }
	}
	
	public class MeasureCategory : BaseModel
	{
		public string Name { get; set; }
		public MeasureType Type { get; set; }
		public int SortOrder { get; set; }
		public IList<MeasureCategoryLanguage> Languages { get; set; }
	}
	
	public class MeasureCategoryLanguage : BaseModel
	{
		public MeasureCategory Category { get; set; }
		public Measure Measure { get; set; }
		public Language Language { get; set; }
	}
	
	public class MeasureComponent : BaseModel
	{
		public Measure Measure { get; set; }
		public string Component { get; set; }
		public IList<MeasureComponentLanguage> Languages { get; set; }
	}
	
	public class MeasureComponentLanguage : BaseModel
	{
		public MeasureComponent Component { get; set; }
		public Language Language { get; set; }
		public string ComponentName { get; set; }
		public string Unit { get; set; }
	}
	
	public class MeasureComponentPart : BaseModel
	{
		public MeasureComponent Component { get; set; }
		public int Part { get; set; }
		public int SortOrder { get; set; }
	}
	
	public class MeasureLanguage : BaseModel
	{
		public Measure Measure { get; set; }
		public Language Language { get; set; }
		public string MeasureName { get; set; }
	}
	
	public class MeasureType : BaseModel
	{
		public string Name { get; set; }
		public bool Active { get; set; }
		public int SortOrder { get; set; }
	}
	
	public class MeasureTypeLanguage : BaseModel
	{
		public Measure Measure { get; set; }
		public Language Language { get; set; }
		public string TypeName { get; set; }
	}
	
	public class ProfileComparison : BaseModel
	{
		public string Hash { get; set; }
	}
	
	public class ProfileComparisonBackgroundQuestion : BaseModel
	{
		public ProfileComparison Comparison { get; set; }
		public BackgroundQuestion Question { get; set; }
		public int Value { get; set; }
	}
	
	public class Reminder : BaseModel
	{
		public User User { get; set; }
		public DateTime Date { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
	}
	
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
	
	// FIXME: This has conflict with eForm's User class. Verify!
	public class User : BaseModel
	{
		public string Name { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public Department Department { get; set; }
		public UserProfile Profile { get; set; }
		public Sponsor Sponsor { get; set; }
		public string AltEmail { get; set; }
		public int ReminderLink { get; set; }
		public string UserKey { get; set; }
	}
	
	public class UserMeasure : BaseModel
	{
		public User User { get; set; }
		public DateTime Created { get; set; }
		public DateTime Deleted { get; set; }
		public UserProfile UserProfile { get; set; }
	}
	
	public class UserMeasureComponent : BaseModel
	{
		public UserMeasure Measure { get; set; }
		public MeasureComponent Component { get; set; }
		public int IntegerValue { get; set; }
		public decimal DecimalValue { get; set; }
		public string StringValue { get; set; }
	}
	
	public class UserProfile : BaseModel
	{
		public User User { get; set; }
		public Sponsor Sponsor { get; set; }
		public Department Department { get; set; }
		public ProfileComparison Comparison { get; set; }
		public DateTime Created { get; set; }
	}
	
	public class UserProfileBackgroundQuestion : BaseModel
	{
		public UserProfile Profile { get; set; }
		public BackgroundQuestion Question { get; set; }
		public int ValueInt { get; set; }
		public string ValueText { get; set; }
		public DateTime? ValueDate { get; set; }
	}
	
	public class UserProjectRoundUser : BaseModel
	{
		public User User { get; set; }
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public ProjectRoundUser ProjectRoundUser { get; set; }
	}
	
	public class UserProjectRoundUserAnswer : BaseModel
	{
		public DateTime Date { get; set; }
		public UserProfile Profile { get; set; }
		public Answer Answer { get; set; }
//		public ProjectRoundUser ProjectRoundUser { get; set; }
		public UserProjectRoundUser ProjectRoundUser { get; set; }
	}
	
	public class UserToken : BaseModel
	{
		public string Token { get; set; }
		public User Owner { get; set; }
		public DateTime Expiry { get; set; }
	}
	
	public class Wise : BaseModel
	{
		public DateTime LastShown { get; set; }
		public IList<WiseLanguage> Languages { get; set; }
	}
	
	public class WiseLanguage : BaseModel
	{
		public Wise Wise { get; set; }
		public Language Language { get; set; }
		public string WiseName { get; set; }
		public string Owner { get; set; }
	}
}