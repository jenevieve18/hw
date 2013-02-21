//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HW.Core
{
	public class Answer : BaseModel, IMinMax
	{
		public Answer()
		{
			Values = new List<AnswerValue>();
		}
		
		float min = 0;
		float max = 100;
		public ProjectRound ProjectRound { get; set; }
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public ProjectRoundUser ProjectRoundUser { get; set; }
		public Language Language { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public IList<AnswerValue> Values { get; set; }
		
		public HWList GetIntValues()
		{
			List<double> n = new List<double>();
			foreach (var v in Values) {
				n.Add((double)v.ValueInt);
			}
			return new HWList(n);
		}
		
		public float Average { get; set; }
		public int DummyValue1 { get; set; } // TODO: This is used by dbo.cf_yearWeek and related methods
		public int DummyValue2 { get; set; }
		public int DummyValue3 { get; set; }
		public float Max {
			get { return max > 100 ? 100 : max; }
			set { max = value; }
		}
		public float Min {
			get { return min < 0 ? 0 : min; }
			set { min = value; }
		}
		public string SomeString { get; set; } // TODO: From cf_yearMonthDay(a.EndDT) function
		public int DT { get; set; } // TODO: From cf_yearWeek(a.EndDT) function
		public float AverageV { get; set; }
		public int CountV { get; set; }
		public float StandardDeviation { get; set; }
		
		public double LowerWhisker { get; set; }
		public double UpperWhisker { get; set; }
		public double LowerBox { get; set; }
		public double UpperBox { get; set; }
		public double Median { get; set; }
	}
	
	public class AnswerValue : BaseModel
	{
		public Answer Answer { get; set; }
		public Question Question { get; set; }
		public Option Option { get; set; }
		public int ValueInt { get; set; }
		public decimal ValueDecimal { get; set; }
		public DateTime ValueDateTime { get; set; }
		public DateTime Created { get; set; }
		public string ValueText { get; set; }
		public string ValueTextJapaneseUnicode { get; set; }
	}
	
	public class Feedback : BaseModel
	{
		public string Notes { get; set; }
	}
	
	public class FeedbackQuestion : BaseModel
	{
		public Feedback Feedback { get; set; }
		public Question Question { get; set; }
	}
	
	public class Group : BaseModel
	{
		public string Description { get; set; }
		
		public class Grouping // TODO: Make this enum, it's awkward having this constant class!
		{
			public const int None = 0;
			public const int UsersOnUnit = 1;
			public const int UsersOnUnitAndSubUnits = 2;
			public const int BackgroundVariable = 3;
		}
		
		public class GroupBy // TODO: Make this enum, it's awkward having this constant class!
		{
			public const int OneWeek = 1;
			public const int TwoWeeksStartWithEvent = 7;
			public const int TwoWeeksStartWithOdd = 2;
			public const int OneMonth = 3;
			public const int ThreeMonths = 4;
			public const int SixMonths = 5;
			public const int OneYear = 6;
		}
	}
	
	public class GroupFactory
	{
		public static string GetGroupBy(int GB)
		{
//			switch (GB) {
//					case 1: return "dbo.cf_yearWeek";
//					case 2: return "dbo.cf_year2Week";
//					case 3: return "dbo.cf_yearMonth";
//					case 4: return "dbo.cf_year3Month";
//					case 5: return "dbo.cf_year6Month";
//					case 6: return "YEAR";
//					case 7: return "dbo.cf_year2WeekEven";
//					default: throw new NotSupportedException();
//			}
			switch (GB) {
					case Group.GroupBy.OneWeek: return "dbo.cf_yearWeek";
					case Group.GroupBy.TwoWeeksStartWithOdd: return "dbo.cf_year2Week";
					case Group.GroupBy.OneMonth: return "dbo.cf_yearMonth";
					case Group.GroupBy.ThreeMonths: return "dbo.cf_year3Month";
					case Group.GroupBy.SixMonths: return "dbo.cf_year6Month";
					case Group.GroupBy.OneYear: return "YEAR";
					case Group.GroupBy.TwoWeeksStartWithEvent: return "dbo.cf_year2WeekEven";
					default: throw new NotSupportedException();
			}
		}
		
		public static int GetCount(int GRPNG, int SPONS, int SID, int PRUID, string GID, ref string extraDesc, Hashtable desc, Hashtable join, ArrayList item, IDepartmentRepository departmentRepository, IQuestionRepository questionRepository)
		{
			int COUNT = 0;
			switch (GRPNG)
			{
//				case 0:
				case Group.Grouping.None:
					{
						string tmpDesc = "";
						int sslen = 0;
						string tmpSS = "";
						IList<Department> departments = SPONS != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(SID, SPONS) : departmentRepository.FindBySponsorOrderedBySortString(SID);
						foreach (Department d in departments) {
							if (sslen == 0) {
								sslen = d.SortString.Length;
							}
							if (sslen == d.SortString.Length) {
								tmpDesc += (tmpDesc != "" ? ", " : "") + d.Name + "+";
								tmpSS += (tmpSS != "" ? "," : "") + "'" + d.SortString + "'";
							} else {
								break;
							}
						}

						item.Add("1");
						desc.Add("1", tmpDesc);
						join.Add(
							"1",
							string.Format(
								@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) IN ({2}) ",
								PRUID,
								sslen,
								tmpSS
							)
						);
						COUNT++;
						break;
					}
//				case 1:
				case Group.Grouping.UsersOnUnit:
					{
						IList<Department> departments = SPONS != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(SID, SPONS, GID) : departmentRepository.FindBySponsorOrderedBySortStringIn(SID, GID);
						foreach (Department d in departments) {
							item.Add(d.Id.ToString());
							desc.Add(d.Id.ToString(), d.Name);
							join.Add(
								d.Id.ToString(),
								string.Format(
									@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWup.DepartmentID = {1}",
									PRUID,
									d.Id
								)
							);
							COUNT++;
						}
						break;
					}
//				case 2:
				case Group.Grouping.UsersOnUnitAndSubUnits:
					{
						IList<Department> departments = SPONS != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(SID, SPONS, GID) : departmentRepository.FindBySponsorOrderedBySortStringIn(SID, GID);
						foreach (Department d in departments) {
							item.Add(d.Id.ToString());
							desc.Add(d.Id.ToString(), d.Name);
							join.Add(
								d.Id.ToString(),
								string.Format(
									@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) = '{2}'",
									PRUID,
									d.SortString.Length,
									d.SortString
								)
							);
							COUNT++;
						}
						break;
					}
//				case 3:
				case Group.Grouping.BackgroundVariable:
					{
						string tmpSelect = "";
						string tmpJoin = "";
						string tmpOrder = "";

						string tmpDesc = "";
						int sslen = 0;
						string tmpSS = "";

						IList<Department> departments = SPONS != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(SID, SPONS) : departmentRepository.FindBySponsorOrderedBySortString(SID);
						foreach (Department d in departments) {
							if (sslen == 0) {
								sslen = d.SortString.Length;
							}
							if (sslen == d.SortString.Length) {
								tmpDesc += (tmpDesc != "" ? ", " : "") + d.Name + "+";
								tmpSS += (tmpSS != "" ? "," : "") + "'" + d.SortString + "'";
							} else {
								break;
							}
						}
						string bqid = GID.Replace("'", "");
						GID = "";
						foreach (var x in questionRepository.FindLikeBackgroundQuestions(bqid)) {
							GID += (GID != "" ? "," : "") + x.Id;

							extraDesc += (extraDesc != "" ? " / " : "") + x.Internal;

							tmpSelect += (tmpSelect != "" ? " ," : "") + "ba" + x.Id + ".BAID,ba" + x.Id + ".Internal ";
							tmpJoin += (tmpJoin != "" ? "INNER JOIN BA ba" + x.Id + " ON ba" + x.Id + ".BQID = " + x.Id + " " : "FROM BA ba" + x.Id + " ");
							tmpOrder += (tmpOrder != "" ? ", ba" + x.Id + ".SortOrder" : "WHERE ba" + x.Id + ".BQID = " + x.Id + " ORDER BY ba" + x.Id + ".SortOrder");
						}
						string[] GIDS = GID.Split(',');

						SqlDataReader rs2;
						string query = "SELECT " +
							tmpSelect +
							tmpJoin +
							tmpOrder;
						rs2 = Db.rs(query);
						while (rs2.Read()) {
							string key = "";
							string txt = "";
							string sql = string.Format(
								@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) IN ({2})",
								PRUID,
								sslen,
								tmpSS
							);

							for (int i= 0; i < GIDS.Length; i++) {
								key += (key != "" ? "X" : "") + rs2.GetInt32(0 + i * 2);
								txt += (txt != "" ? " / " : "") + rs2.GetString(1 + i * 2);
//								sql += "INNER JOIN healthWatch..UserProfileBQ HWp" + GIDS[i] + " ON HWup.UserProfileID = HWp" + GIDS[i] + ".UserProfileID AND HWp" + GIDS[i] + ".BQID = " + GIDS[i] + " AND HWp" + GIDS[i] + ".ValueInt = " + rs2.GetInt32(0 + i*2);
								sql += string.Format(
									@"
INNER JOIN healthWatch..UserProfileBQ HWp{0} ON HWup.UserProfileID = HWp{0}.UserProfileID AND HWp{0}.BQID = {0} AND HWp{0}.ValueInt = {1}",
									GIDS[i],
									rs2.GetInt32(0 + i*2)
								);
							}
							COUNT++;

							item.Add(key);
							desc.Add(key, txt);
							join.Add(key, sql);
						}
						rs2.Close();
						break;
					}
			}
			return COUNT;
		}
	}
	
	public class Index : BaseIndex
	{
		public int MaxValue { get; set; }
		public IList<IndexPart> Parts { get; set; }
		public IList<IndexLanguage> Languages { get; set; }
		
		public float AverageAX { get; set; }
		public int CountDX { get; set; }
	}
	
	public class IndexPart : BaseModel
	{
		public int Multiple { get; set; }
		public Index OtherIndex { get; set; }
	}
	
	public class IndexLanguage : BaseModel
	{
		public Index Index { get; set; }
		public Language Language { get; set; }
		public string IndexName { get; set;}
	}
	
	public class Manager : BaseModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
	}
	
	public class Navigation : BaseModel
	{
		public string URL { get; set; }
		public string Text { get; set; }
		public int SortOrder { get; set; }
	}
	
	public class Option : BaseModel
	{
		public int Type { get; set; }
		public int Placement { get; set; }
		public string Internal { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public OptionContainer Container { get; set; }
		public string BackgroundColor { get; set; }
		public int InnerWidth { get; set; }
		
		public OptionComponentLanguage CurrentComponent { get; set; }
	}
	
	public class OptionContainer : BaseModel
	{
		public string Container { get; set; }
	}
	
	public class OptionComponent : BaseModel
	{
		public int ExportValue { get; set; }
		public string Internal { get; set; }
		public IList<OptionComponentLanguage> Languages { get; set; }
		public OptionComponentContainer Container { get; set; }
	}
	
	public class OptionComponentContainer : BaseModel
	{
		public string Container { get; set; }
	}
	
	public class OptionComponentLanguage : BaseModel
	{
		public Language Language { get; set; }
		public OptionComponent Component { get; set; }
		public string Text { get; set; }
		public string TextJapaneseUnicode { get; set; }
	}
	
	public class Project : BaseModel
	{
		public string Internal { get; set; }
		public string Name { get; set; }
		public string AppURL { get; set; }
		public IList<ProjectRound> Rounds { get; set; }
	}
	
	public class ProjectRound : BaseModel
	{
		public Project Project { get; set; }
		public string Internal { get; set; }
		public DateTime? Started { get; set; }
		public DateTime? Closed { get; set; }
		public Report Report { get; set; }
		public IList<ProjectRoundLanguage> Languages { get; set; }
	}
	
	public class ProjectRoundLanguage : BaseModel
	{
		public ProjectRound Round { get; set; }
		public Language Language { get; set; }
	}
	
	public interface IHasLanguage
	{
		Language Language { get; set; }
	}
	
	public class ProjectRoundUnit : BaseModel, IHasLanguage
	{
		public string Name { get; set; }
		public Report Report { get; set; }
		public string SortString { get; set; }
		public Language Language { get; set; }
		public IList<Answer> Answers { get; set; }
		public Survey Survey { get; set; }
		
		public string TreeString { get; set; } // TODO: This comes from cf_projectUnitTree function.
	}
	
	public class ProjectRoundUnitManager : BaseModel
	{
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public ProjectRoundUser ProjectRoundUser { get; set; }
	}
	
	public class ProjectRoundUser : BaseModel
	{
		public ProjectRound ProjectRound { get; set; }
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public UserCategory UserCategory { get; set; }
		public string Email { get; set; }
		
		public string SomeText { get; set; }
	}
	
	public class ProjectUnitCategory : BaseModel
	{
		public Project Project { get; set; }
	}
	
	public class ProjectUserCategory : BaseModel
	{
	}
	
	public class Question : BaseModel
	{
		public Question()
		{
			Answers = new List<Answer>();
		}
		
		public QuestionCategory Category { get; set; }
		public string VariableName { get; set; }
		public IList<QuestionLanguage> Languages { get; set; }
		public IList<Answer> Answers { get; set; }
	}
	
	public class QuestionCategory : BaseModel
	{
		public string Internal { get; set; }
	}
	
	public class QuestionCategoryLanguage : BaseModel
	{
		public QuestionCategory Category { get; set; }
		public Language Language { get; set; }
	}
	
	public class QuestionCategoryQuestion : BaseModel
	{
		public QuestionCategory Category { get; set; }
		public Question Question { get; set; }
	}
	
	public class QuestionLanguage : BaseModel
	{
		public Question Question { get; set;}
		public Language Language { get; set; }
	}
	
	public class QuestionOption : BaseModel
	{
		public Question Question { get; set; }
		public Option Option { get; set; }
	}
	
	public class QuestionOptionRange : BaseModel
	{
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public decimal LowValue { get; set; }
		public decimal HighValue { get; set; }
	}
	
	public class Report : BaseModel
	{
		public string Internal { get; set; }
	}
	
	public enum ReportType
	{
		One = 1,
		Two = 2,
		Three = 3,
		Eight = 8,
		Nine = 9
	}
	
	public class ReportLanguage : BaseModel
	{
		public Language Language { get; set; }
		public Option Option { get; set; }
		public Question Question { get; set; }
	}
	
	public class ReportPart : BaseModel
	{
		public Report Report { get; set; }
		public int Type { get; set; }
		public int RequiredAnswerCount { get; set; }
		public int PartLevel { get; set; }
		public Question Question { get; set; }
		public Option Option { get; set; }
		public IList<ReportPartComponent> Components { get; set; }
	}
	
	public class ReportPartComponent : BaseModel
	{
		public ReportPart ReportPart { get; set; }
		public Index Index { get; set; }
		public int SortOrder { get; set; }
		public WeightedQuestionOption QuestionOption { get; set; }
	}
	
	public class ReportPartLanguage : BaseModel
	{
		public ReportPart ReportPart { get; set; }
		public Language Language { get; set; }
		public string Subject { get; set; }
		public string Header { get; set; }
		public string Footer { get; set; }
	}
	
	public class Survey : BaseModel
	{
		public string Name { get; set; }
	}
	
	public class SurveyLanguage : BaseModel
	{
	}
	
	public class XUnit : BaseModel // TODO: XUnit because Unit is a Control in org.aspx
	{
	}
	
	public class UnitCategory : BaseModel
	{
		public string Internal { get; set; }
	}
	
	public class UnitCategoryLanguage : BaseModel
	{
		public UnitCategory Category { get; set; }
		public Language Language { get; set; }
		public string CategoryName { get; set; }
		public string CategoryNameJapaneseUnicode { get; set; }
	}
	
	public class UserCategory : BaseModel
	{
		public string Internal { get; set; }
	}
	
	public class UserNote : BaseModel
	{
		public User User { get; set; }
		public string Note { get; set; }
		public DateTime Date { get; set; }
		public SponsorAdmin SponsorAdmin { get; set; }
	}
	
	public interface ICircle
	{
		int Color { get; set; }
		float Value { get; set; }
		int CX { get; set; }
	}
	
	public class Circle : ICircle
	{
		public int Color { get; set; }
		public float Value { get; set; }
		public int CX { get; set; }
	}
	
	public interface ILine
	{
		int Color { get; set; }
		int X1 { get; set; }
		float Y1 { get; set; }
		int X2 { get; set; }
		float Y2 { get; set; }
		int T { get; set; }
	}
	
	public class Line : ILine
	{
		public int Color { get; set; }
		public int X1 { get; set; }
		public float Y1 { get; set; }
		public int X2 { get; set; }
		public float Y2 { get; set; }
		public int T { get; set; } // TODO: This should be stroke thickness
	}
	
	public interface IExplanation
	{
		int Color { get; set; }
		string Description { get; set; }
		bool Right { get; set; }
		bool Box { get; set; }
		bool HasAxis { get; set; }
		int X { get; set; }
		int Y { get; set; }
	}
	
	public class Explanation : IExplanation
	{
		public int Color { get; set; }
		public string Description { get; set; }
		public bool Right { get; set; }
		public bool Box { get; set; }
		public bool HasAxis { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
	}
	
	public interface IMinMax
	{
		float Min { get; set; }
		float Max { get; set; }
	}
	
	public interface IIndex
	{
		int TargetValue { get; set; }
		int YellowLow { get; set; }
		int GreenLow { get; set; }
		int GreenHigh { get; set; }
		int YellowHigh { get; set; }
	}
	
	public class BaseIndex : BaseModel, IIndex
	{
		public int TargetValue { get; set; }
		public int YellowLow { get; set; }
		public int GreenLow { get; set; }
		public int GreenHigh { get; set; }
		public int YellowHigh { get; set; }
		
		public int GetColor(float x)
		{
			if (YellowLow >= 0 && YellowLow <= 100 && x >= YellowLow) {
				return 1;
			} else if (GreenLow >= 0 && GreenLow <= 100 && x >= GreenLow) {
				return 0;
			} else if (GreenHigh >= 0 && GreenHigh <= 100 && x >= GreenHigh) {
				return 1;
			} else if (YellowHigh >= 0 && YellowHigh <= 100 && x >= YellowHigh) {
				return 2;
			} else {
				return 2;
			}
		}
	}
	
//	public class WeightedQuestionOption : BaseModel, IIndex
	public class WeightedQuestionOption : BaseIndex
	{
		public Question Question { get; set; }
		public Option Option { get; set; }
//		public int TargetValue { get; set; }
//		public int YellowLow { get; set; }
//		public int GreenLow { get; set; }
//		public int GreenHigh { get; set; }
//		public int YellowHigh { get; set; }
		public IList<WeightedQuestionOptionLanguage> Languages { get; set; }
	}
	
	public class WeightedQuestionOptionLanguage : BaseModel
	{
		public WeightedQuestionOption Option { get; set; }
		public Language Language { get; set; }
		public string Question { get; set; }
	}
}
