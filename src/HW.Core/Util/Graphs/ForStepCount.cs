using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using HW.Core.Helpers;
using HW.Core.Util.Exporters;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Services;

namespace HW.Core.Util.Graphs
{
	public class ForStepCount
	{
		SqlProjectRepository projectRepo;
		SqlAnswerRepository answerRepo;
		SqlOptionRepository optionRepo;
		SqlReportRepository reportRepo;
		SqlIndexRepository indexRepo;
		SqlQuestionRepository questionRepo;
		SqlDepartmentRepository departmentRepo;
		SqlMeasureRepository measureRepo;
		
//		int lastCount = 0;
//		float lastVal = 0;
//		string lastDesc = "";
//		System.Collections.Hashtable res = new System.Collections.Hashtable();
//		System.Collections.Hashtable cnt = new System.Collections.Hashtable();
		
		public ForStepCount(SqlAnswerRepository answerRepo, SqlReportRepository reportRepo, SqlProjectRepository projectRepo, SqlOptionRepository optionRepo, SqlIndexRepository indexRepo, SqlQuestionRepository questionRepo, SqlDepartmentRepository departmentRepo, SqlMeasureRepository measureRepo)
		{
			this.projectRepo = projectRepo;
			this.answerRepo = answerRepo;
			this.optionRepo = optionRepo;
			this.reportRepo = reportRepo;
			this.indexRepo = indexRepo;
			this.questionRepo = questionRepo;
			this.departmentRepo = departmentRepo;
			this.measureRepo = measureRepo;
		}
		
		IGraphType GetGraphType(int plot, int t)
		{
			if (plot == PlotType.BoxPlotMinMax) {
				return new BoxPlotMinMaxGraphType();
			} else if (plot == PlotType.BoxPlot) {
				return new BoxPlotGraphType();
			} else if (plot == PlotType.LineSDWithCI) {
				return new LineGraphType(2, t);
			} else if (plot == PlotType.LineSD) {
				return new LineGraphType(1, t);
			} else {
				return new LineGraphType(0, t);
			}
		}
		
		public ExtendedGraph CreateGraph(SponsorProject p, int langID, DateTime dateFrom, DateTime dateTo, int GB, bool hasGrouping, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs, object disabled, int point)
		{
			int differenceDate = 0;
			string sortString = "";
			int startDate = 0;
			int endDate = 0;
			ExtendedGraph g = null;
			
			LanguageFactory.SetCurrentCulture(langID);
			
			if (GB == 0) {
				GB = 2;
			}
			
			string groupByQuery = GroupFactory.GetGroupBy(GB);
			g = new ExtendedGraph(895, 440, "#FFFFFF");
			
			g.Type = GetGraphType(plot, 2);
			var answer = measureRepo.ReadByGroup(groupByQuery, dateFrom.Year, dateTo.Year, sortString, dateFrom.Month, dateTo.Month);
			if (answer != null) {
				differenceDate = answer.DummyValue1 + 3;
				startDate = answer.DummyValue2;
				endDate = answer.DummyValue3;
			}
			
			if (hasGrouping) {
				string extraDesc = "";
				
				var departments = GroupFactory.GetDepartmentsWithJoinQueryForStepCount(grouping, sponsorAdmin, sponsor, departmentIDs, ref extraDesc, departmentRepo, questionRepo);
				
				int aggregation = GetAggregationByGroupBy(GB);
				
				var minMaxes = new List<IMinMax>();
				foreach (var d in departments) {
					var minMax = measureRepo.ReadMinMax2(groupByQuery, dateFrom.Year, dateTo.Year, dateFrom.Month, dateTo.Month, aggregation, departmentIDs, sponsor.Id, d.Query);
					minMax.Max = (float)(((int)minMax.Max).Ceiling());
					minMaxes.Add(minMax);
				}
				g.SetMinMaxes(minMaxes);
				g.DrawBackgroundFromIndex(new BaseIndex());
				g.DrawComputingSteps(disabled, differenceDate);
				
				differenceDate = 0;
				
				g.DrawBottomString(startDate, endDate, GB);
				
				int breaker = 6;
				int itemWidth = 120;
				if (departments.Count < 6) {
					breaker = 4;
					itemWidth = 180;
				}
				if (departments.Count < 4) {
					breaker = 3;
					itemWidth = 240;
				}
				
				g.Explanations.Add(
					new Explanation {
						Description = (extraDesc != "" ? extraDesc + "\n" : "") + LanguageFactory.GetMeanText(langID) + (point == Distribution.StandardDeviation ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
						Color = 0,
						Right = false,
						Box = false,
						HasAxis = false
					}
				);
				int bx = 0;
				foreach(var i in departments) {
					differenceDate = 1;
					int lastDT = startDate - 1;
					var measures = measureRepo.FindByQuestionAndOptionJoinedAndGrouped2(i.Query, groupByQuery, dateFrom.Year, dateTo.Year, dateFrom.Month, dateTo.Month, aggregation, sponsor.Id);
					Series s = new Series {
						Description = i.Name,
						Color = bx + 4,
						X = 130 + (int)((bx % breaker) * itemWidth),
						Y = 20 + (int)Math.Floor((double)bx / breaker) * 15
					};
					foreach (var a in measures) {
						if (a.DT < startDate) {
							continue;
						}
						while (lastDT + 1 < a.DT) {
							lastDT++;
							differenceDate++;
						}
						if (a.Values.Count >= i.MinUserCountToDisclose) {
							if (departments.Count == 1) {
								string v = BaseGraphFactory.GetBottomString(GB, a.DT, differenceDate, (departments.Count == 1 ? ", n = " + a.Values.Count : ""));
								g.DrawBottomString(v, differenceDate);
							}
							s.Points.Add(new PointV { X = differenceDate, Values = a.GetDoubleValues() });
						}
						lastDT = a.DT;
						differenceDate++;
					}
					g.Series.Add(s);
					bx++;
				}
			} else {
				int bx = 0;
				var components = reportRepo.FindComponentsByPartAndLanguage2(p.Id, langID);
				foreach (ReportPartComponent c in components) {
					g.Explanations.Add(
						new Explanation {
							Description = c.WeightedQuestionOption.Languages[0].Question + ", " + LanguageFactory.GetMeanText(langID) + (point == Distribution.StandardDeviation ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
							Color = bx + 4,
							Right = bx == 0 ? false : true,
							Box = bx == 0 ? true : false,
							HasAxis = bx == 0 ? false : true
						}
					);
					differenceDate = 1;
					int lastDT = startDate - 1;
					Series s = new Series { Color = bx + 4 };
					var answers = answerRepo.FindByQuestionAndOptionGroupedX(groupByQuery, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, dateFrom.Year, dateTo.Year, sortString, dateFrom.Month, dateTo.Month);
					foreach (Answer a in answers) {
						if (a.DT < startDate) {
							continue;
						}
						while (lastDT + 1 < a.DT) {
							lastDT++;
							differenceDate++;
						}
						string v = BaseGraphFactory.GetBottomString(GB, a.DT, differenceDate, ", n = " + a.CountV);
						g.DrawBottomString(v, differenceDate);
						s.Points.Add(new PointV { X = differenceDate, Values = a.GetDoubleValues() });
						lastDT = a.DT;
						differenceDate++;
					}
					g.Series.Add(s);
					bx++;
				}
			}
			g.Draw();
			return g;
		}
		
		int GetAggregationByGroupBy(int groupBy)
		{
			switch (groupBy) {
					case 1: return 7;
					case 7: return 14;
					case 2: return 14;
					case 3: return 30;
					case 4: return 120;
					case 5: return 180;
					default: return 365;
			}
		}

		public void CreateGraphForExcelWriter(ReportPart p, int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, bool hasGrouping, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs, ExcelWriter writer, ref int index)
		{
			int cx;
			string sortString = "";
			int minDT = 0;
			int maxDT = 0;
			
			Dictionary<string, List<IAnswer>> weeks = new Dictionary<string, List<IAnswer>>();
			string extraDesc = "";
			
			var departments = GroupFactory.GetDepartmentsWithJoinQueryForStepCount(grouping, sponsorAdmin, sponsor, departmentIDs, ref extraDesc, departmentRepo, questionRepo);

			LanguageFactory.SetCurrentCulture(langID);

			if (groupBy == 0) {
				groupBy = GroupBy.TwoWeeksStartWithOdd;
			}

			string groupByQuery = GroupFactory.GetGroupBy(groupBy);

			var answer = measureRepo.ReadByGroup(groupByQuery, dateFrom.Year, dateTo.Year, sortString, dateFrom.Month, dateTo.Month);
			if (answer != null) {
				minDT = answer.DummyValue2;
				maxDT = answer.DummyValue3;
			}

			weeks = GetWeeks(minDT, maxDT, groupBy);

			if (hasGrouping) {
				int aggregation = GetAggregationByGroupBy(groupBy);
				
				int bx = 0;
				foreach(var i in departments) {
					cx = 1;
					int lastDT = minDT - 1;
					var measures = measureRepo.FindByQuestionAndOptionJoinedAndGrouped2(i.Query, groupByQuery, dateFrom.Year, dateTo.Year, dateFrom.Month, dateTo.Month, aggregation, sponsor.Id);
					foreach (var a in measures) {
						if (a.DT < minDT) {
							continue;
						}
						while (lastDT + 1 < a.DT) {
							lastDT++;
							cx++;
						}
						if (a.Values.Count >= i.MinUserCountToDisclose) {
							if (departments.Count == 1) {
							}
							weeks[GroupStatsGraphFactory.GetBottomString(groupBy, a.DT, cx, "")].Add(a);
						}
						lastDT = a.DT;
						cx++;
					}
					bx++;
				}
			} else {
				int bx = 0;
				foreach (ReportPartComponent c in reportRepo.FindComponentsByPartAndLanguage2(p.Id, langID)) {
					cx = 1;
					int lastDT = minDT - 1;
					var answers = answerRepo.FindByQuestionAndOptionGrouped(groupByQuery, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, dateFrom.Year, dateTo.Year, sortString, dateFrom.Month, dateTo.Month);
					foreach (Answer a in answers) {
						if (a.DT < minDT) {
							continue;
						}
						while (lastDT + 1 < a.DT) {
							lastDT++;
							cx++;
						}

						if (a.CountV >= p.RequiredAnswerCount) {
						}
						lastDT = a.DT;
						cx++;
					}
					bx++;
				}
			}
			
			var plotter = GetPlotter(plot);
			plotter.ForMerge += delegate(object sender, MergeEventArgs e) { OnForMerge(e); };
			plotter.ToExcel(departments, weeks, writer, ref index);
		}
		
		AbstractExcel GetPlotter(int plot)
		{
			if (plot == PlotType.BoxPlotMinMax) {
				return new BoxPlotExcel();
			} else if (plot == PlotType.LineSDWithCI) {
				return new ConfidenceIntervalLineExcel();
			} else if (plot == PlotType.LineSD) {
				return new StandardDeviationLineExcel();
			} else if (plot == PlotType.Verbose) {
				return new EverythingExcel();
			} else {
				return new LineExcel();
			}
		}
		
		Dictionary<string, List<IAnswer>> GetWeeks(int minDT, int maxDT, int groupBy)
		{
			int j = 0;
			Dictionary<string, List<IAnswer>> weeks = new Dictionary<string, List<IAnswer>>();
			for (int i = minDT; i <= maxDT; i++) {
				j++;
				string w = GroupStatsGraphFactory.GetBottomString(groupBy, i, j, "");
				if (!weeks.ContainsKey(w)) {
					weeks.Add(w, new List<IAnswer>());
				}
			}
			return weeks;
		}
		
		public event EventHandler<MergeEventArgs> ForMerge;

		protected virtual void OnForMerge(MergeEventArgs e)
		{
			if (ForMerge != null) {
				ForMerge(this, e);
			}
		}
		
//		void GetIdxVal(int indexID, string sortString, int langID, int yearFrom, int yearTo, int monthFrom, int monthTo)
//		{
//			foreach (Index i in indexRepo.FindByLanguage(indexID, langID, yearFrom, yearTo, sortString, monthFrom, monthTo)) {
//				lastCount = i.CountDX;
//				lastVal = i.AverageAX;
//				lastDesc = i.Languages[0].IndexName;
//				if (!res.Contains(i.Id)) {
//					res.Add(i.Id, lastVal);
//				}
//				if (!cnt.Contains(i.Id)) {
//					cnt.Add(i.Id, lastCount);
//				}
//			}
//		}
//		
//		void GetOtherIdxVal(int indexID, string sortString, int langID, int yearFrom, int yearTo, int monthFrom, int monthTo)
//		{
//			float tot = 0;
//			int max = 0;
//			int minCnt = Int32.MaxValue;
//			Index index = indexRepo.ReadByIdAndLanguage(indexID, langID);
//			if (index != null) {
//				lastDesc = index.Languages[0].IndexName;
//				foreach (IndexPart p in index.Parts) {
//					max += 100 * p.Multiple;
//					if (res.Contains(p.OtherIndex.Id)) {
//						tot += (float)res[p.OtherIndex.Id] * p.Multiple;
//						minCnt = Math.Min((int)cnt[p.OtherIndex.Id], minCnt);
//					} else {
//						GetIdxVal(p.OtherIndex.Id, sortString, langID, yearFrom, yearTo, monthFrom, monthTo);
//						tot += lastVal * p.Multiple;
//						minCnt = Math.Min(lastCount, minCnt);
//					}
//				}
//			}
//			lastVal = 100 * tot / max;
//			lastCount = minCnt;
//		}
	}
}
