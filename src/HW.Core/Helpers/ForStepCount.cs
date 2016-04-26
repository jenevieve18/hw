﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Services;

namespace HW.Core.Helpers
{
	public class ForStepCount
	{
		SqlProjectRepository projectRepository;
		SqlAnswerRepository answerRepository;
		SqlOptionRepository optionRepository;
		SqlReportRepository reportRepository;
		SqlIndexRepository indexRepository;
		SqlQuestionRepository questionRepository;
		SqlDepartmentRepository departmentRepository;
		SqlMeasureRepository measureRepository;
		
		int lastCount = 0;
		float lastVal = 0;
		string lastDesc = "";
		System.Collections.Hashtable res = new System.Collections.Hashtable();
		System.Collections.Hashtable cnt = new System.Collections.Hashtable();
		
		public ForStepCount(SqlAnswerRepository answerRepository, SqlReportRepository reportRepository, SqlProjectRepository projectRepository, SqlOptionRepository optionRepository, SqlIndexRepository indexRepository, SqlQuestionRepository questionRepository, SqlDepartmentRepository departmentRepository, SqlMeasureRepository measureRepository)
		{
			this.projectRepository = projectRepository;
			this.answerRepository = answerRepository;
			this.optionRepository = optionRepository;
			this.reportRepository = reportRepository;
			this.indexRepository = indexRepository;
			this.questionRepository = questionRepository;
			this.departmentRepository = departmentRepository;
			this.measureRepository = measureRepository;
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
		
		public ExtendedGraph CreateGraph(SponsorProject p, int langID, int yearFrom, int yearTo, int GB, bool hasGrouping, int plot, int grouping, int sponsorAdminID, int sponsorID, string departmentIDs, object disabled, int point, int sponsorMinUserCountToDisclose, int monthFrom, int monthTo)
		{
//			int differenceDate = p.Components.Capacity;
			int differenceDate = 0;
			string sortString = "";
			int startDate = 0;
			int endDate = 0;
//			ProjectRoundUnit roundUnit = projectRepository.ReadRoundUnit(projectRoundUnitID);
//			if (roundUnit != null) {
//				sortString = roundUnit.SortString;
//				if (langID == 0) {
//					langID = roundUnit.Language.Id;
//				}
//			}
			ExtendedGraph g = null;
			
			LanguageFactory.SetCurrentCulture(langID);
			
//			if (p.Type == 1) {
//				decimal tot = answerRepository.CountByDate(yearFrom, yearTo, sortString, monthFrom, monthTo);
//				if (p.RequiredAnswerCount > Convert.ToInt32(tot)) {
//					g = new ExtendedGraph(895, 50, "#FFFFFF");
//					g.drawStringInGraph("Ingen återkoppling pga otillräckligt underlag", 300, -30);
//				} else {
//					g = new ExtendedGraph(895, 550, "#FFFFFF");
//					List<Bar> bars = new List<Bar>();
//					foreach (OptionComponents c in optionRepository.FindComponentsByLanguage(p.Option.Id, langID)) {
//						int x = answerRepository.CountByValueWithDateOptionAndQuestion(c.Component.Id, yearFrom, yearTo, p.Option.Id, p.Question.Id, sortString, monthFrom, monthTo);
//						var b = new Bar {
//							Description = c.Component.CurrentLanguage.Text,
//							Value = Convert.ToInt32(Math.Round(Convert.ToDecimal(x) / tot * 100M, 0)),
//							Color = 5
//						};
//						bars.Add(b);
//					}
//					differenceDate = optionRepository.CountByOption(p.Option.Id);
//					g.DrawBars(disabled, differenceDate, tot, bars);
//					g.drawAxisExpl(string.Format("% (n = {0})", tot), 5, false, false);
//				}
//			} else if (p.Type == 3) {
//				g = new ExtendedGraph(895, 550, "#FFFFFF");
//				List<Bar> bars = new List<Bar>();
//				List<int> referenceLines = new List<int>();
//
//				foreach (ReportPartComponent c in reportRepository.FindComponents(p.Id)) {
//					System.Collections.SortedList all = new System.Collections.SortedList();
//
//					foreach (ProjectRoundUnit u in projectRepository.FindRoundUnitsBySortString(sortString)) {
//						res = new System.Collections.Hashtable();
//
//						if (c.Index.Parts.Capacity == 0) {
//							GetIdxVal(c.Index.Id, u.SortString, langID, yearFrom, yearTo, monthFrom, monthTo);
//						} else {
//							GetOtherIdxVal(c.Index.Id, u.SortString, langID, yearFrom, yearTo, monthFrom, monthTo);
//						}
//
//						if (all.Contains(lastVal)) {
//							all[lastVal] += "," + u.TreeString;
//						} else {
//							all.Add(lastVal, u.TreeString);
//						}
//					}
//
//					for (int i = all.Count - 1; i >= 0; i--) {
//						int color = c.Index.GetColor(Convert.ToInt32(all.GetKey(i)));
//						string[] u = all.GetByIndex(i).ToString().Split(',');
//
//						foreach (string s in u) {
//							bars.Add(new Bar { Color = color, Value = Convert.ToInt32(all.GetKey(i)), Description = s });
//						}
//					}
//					referenceLines.Add(c.Index.TargetValue);
//				}
//				g.DrawBars(disabled, differenceDate, bars, referenceLines);
//				g.drawAxisExpl("poäng", 0, false, false);
//			} else if (p.Type == 2) {
//			if (p.Type == 2) {
//				g = new ExtendedGraph(895, 550, "#FFFFFF");
//				List<Bar> bars = new List<Bar>();
//				foreach (ReportPartComponent c in reportRepository.FindComponents(p.Id)) {
//					if (c.Index.Parts.Capacity == 0) {
//						GetIdxVal(c.Index.Id, sortString, langID, yearFrom, yearTo, monthFrom, monthTo);
//					} else {
//						GetOtherIdxVal(c.Index.Id, sortString, langID, yearFrom, yearTo, monthFrom, monthTo);
//					}
//					int color = c.Index.GetColor(lastVal);
//					bars.Add(new Bar { Value = lastVal, Color = color, Description = lastDesc, Reference = c.Index.TargetValue });
//				}
//				g.DrawBars(disabled, differenceDate, bars);
//				g.drawAxisExpl("poäng", 0, false, false);
//				g.drawReference(780, 25, " = riktvärde");
//			} else if (p.Type == 8) {
			if (GB == 0) {
				GB = 2;
			}
			
			string groupByQuery = GroupFactory.GetGroupBy(GB);
			g = new ExtendedGraph(895, 440, "#FFFFFF");
			
			g.Type = GetGraphType(plot, 2);
			var answer = measureRepository.ReadByGroup(groupByQuery, yearFrom, yearTo, sortString, monthFrom, monthTo);
			if (answer != null) {
				differenceDate = answer.DummyValue1 + 3;
				startDate = answer.DummyValue2;
				endDate = answer.DummyValue3;
			}
			
//				List<IIndex> indexes = new List<IIndex>();
//				List<IMinMax> minMaxes = new List<IMinMax>();
//				foreach (ReportPartComponent c in reportRepository.FindComponentsByPart(p.Id)) {
//					if (!hasGrouping) {
//						Answer a = answerRepository.ReadMinMax(groupBy, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, yearFrom, yearTo, sortString, monthFrom, monthTo);
//						if (a != null) {
//							minMaxes.Add(a);
//						} else {
//							minMaxes.Add(new Answer());
//						}
//					} else {
//						minMaxes.Add(new Answer());
//					}
//					indexes.Add(c.WeightedQuestionOption);
//				}
//				g.SetMinMaxes(minMaxes);
//				g.DrawBackgroundFromIndexes(indexes);
//				g.SetMinMax(0, 500000);
//				var m = measureRepository.ReadMinMax(groupBy, yearFrom, yearTo, sortString, monthFrom, monthTo);
//				g.SetMinMax((int)m.Min, (int)m.Max);
//				int aggregation = GetAggregationByGroupBy(GB);
//
//				var minMax = measureRepository.ReadMinMax(groupByQuery, yearFrom, yearTo, monthFrom, monthTo, aggregation, departmentIDs, sponsorID);
//				g.SetMinMax((int)minMax.Min, ((int)minMax.Max).Ceiling());
//				g.DrawBackgroundFromIndex(new BaseIndex());
//				g.DrawComputingSteps(disabled, differenceDate);
//
//				differenceDate = 0;
//
//				g.DrawBottomString(startDate, endDate, GB);
			
			if (hasGrouping) {
				string extraDesc = "";
				
				var departments = GroupFactory.GetDepartmentsWithJoinQueryForStepCount(grouping, sponsorAdminID, sponsorID, departmentIDs, ref extraDesc, departmentRepository, questionRepository, sponsorMinUserCountToDisclose);
				
				int aggregation = GetAggregationByGroupBy(GB);
				
				var minMaxes = new List<IMinMax>();
				foreach (var d in departments) {
					var minMax = measureRepository.ReadMinMax2(groupByQuery, yearFrom, yearTo, monthFrom, monthTo, aggregation, departmentIDs, sponsorID, d.Query);
					minMax.Max = (float)(((int)minMax.Max).Ceiling());
					minMaxes.Add(minMax);
				}
//				g.SetMinMax((int)minMax.Min, ((int)minMax.Max).Ceiling());
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
//					ReportPartComponent c = reportRepository.ReadComponentByPartAndLanguage(p.Id, langID);
//					if (c != null) {
				int bx = 0;
				foreach(var i in departments) {
					differenceDate = 1;
					int lastDT = startDate - 1;
					var measures = measureRepository.FindByQuestionAndOptionJoinedAndGrouped2(i.Query, groupByQuery, yearFrom, yearTo, monthFrom, monthTo, aggregation, sponsorID);
					Series s = new Series {
						Description = i.Name,
						Color = bx + 4,
						X = 130 + (int)((bx % breaker) * itemWidth),
						Y = 20 + (int)Math.Floor((double)bx / breaker) * 15
					};
					foreach (var a in measures) {
//								if (a.DT < startDate) {
						if (a.DT < startDate) {
							continue;
						}
						while (lastDT + 1 < a.DT) {
							lastDT++;
							differenceDate++;
						}
//						if (a.Components.Count >= i.MinUserCountToDisclose) {
						if (a.Values.Count >= i.MinUserCountToDisclose) {
							if (departments.Count == 1) {
//										string v = GetBottomString(grouping, a.DT, differenceDate, (departments.Count == 1 ? ", n = " + a.Values.Count : ""));
//										string v = BaseGraphFactory.GetBottomString(GB, a.DT, differenceDate, (departments.Count == 1 ? ", n = " + a.Values.Count : ""));
//								string v = BaseGraphFactory.GetBottomString(GB, a.DT, differenceDate, (departments.Count == 1 ? ", n = " + a.Components.Count : ""));
								string v = BaseGraphFactory.GetBottomString(GB, a.DT, differenceDate, (departments.Count == 1 ? ", n = " + a.Values.Count : ""));
								g.DrawBottomString(v, differenceDate);
							}
							s.Points.Add(new PointV { X = differenceDate, Values = a.GetIntValues() });
						}
						lastDT = a.DT;
						differenceDate++;
					}
					g.Series.Add(s);
					bx++;
				}
//					}
			} else {
				int bx = 0;
				var components = reportRepository.FindComponentsByPartAndLanguage2(p.Id, langID);
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
					var answers = answerRepository.FindByQuestionAndOptionGroupedX(groupByQuery, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, yearFrom, yearTo, sortString, monthFrom, monthTo);
					foreach (Answer a in answers) {
						if (a.DT < startDate) {
							continue;
						}
						while (lastDT + 1 < a.DT) {
							lastDT++;
							differenceDate++;
						}
//							if (a.CountV >= p.RequiredAnswerCount) {
//								string v = GetBottomString(grouping, a.DT, differenceDate, ", n = " + a.CountV);
						string v = BaseGraphFactory.GetBottomString(GB, a.DT, differenceDate, ", n = " + a.CountV);
						g.DrawBottomString(v, differenceDate);
						s.Points.Add(new PointV { X = differenceDate, Values = a.GetIntValues() });
//							}
						lastDT = a.DT;
						differenceDate++;
					}
					g.Series.Add(s);
					bx++;
				}
			}
			g.Draw();
//			}
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

		public void CreateGraphForExcelWriter(ReportPart p, int langID, int projectRoundUnitID, int yearFrom, int yearTo, int groupBy, bool hasGrouping, int plot, int grouping, int sponsorAdminID, int sponsorID, string departmentIDs, ExcelWriter writer, ref int index, int sponsorMinUserCountToDisclose, int monthFrom, int monthTo)
		{
//			int cx = p.Components.Capacity;
			int cx;
			string sortString = "";
			int minDT = 0;
			int maxDT = 0;
//			ProjectRoundUnit roundUnit = projectRepository.ReadRoundUnit(PRUID);
//			if (roundUnit != null) {
//				sortString = roundUnit.SortString;
//				if (langID == 0) {
//					langID = roundUnit.Language.Id;
//				}
//			}
//			Dictionary<string, List<Answer>> weeks = new Dictionary<string, List<Answer>>();
//			List<Department> departments = new List<Department>();
			Dictionary<string, List<IAnswer>> weeks = new Dictionary<string, List<IAnswer>>();
			string extraDesc = "";
			
			var departments = GroupFactory.GetDepartmentsWithJoinQueryForStepCount(grouping, sponsorAdminID, sponsorID, departmentIDs, ref extraDesc, departmentRepository, questionRepository, sponsorMinUserCountToDisclose);

			LanguageFactory.SetCurrentCulture(langID);

//			if (p.Type == 1) {
//				decimal tot = answerRepository.CountByDate(yearFrom, yearTo, sortString, monthFrom, monthTo);
//
//				if (p.RequiredAnswerCount > Convert.ToInt32(tot)) {
//				} else {
//					foreach (OptionComponents c in optionRepository.FindComponentsByLanguage(p.Option.Id, langID)) {
//						int x = answerRepository.CountByValueWithDateOptionAndQuestion(c.Component.Id, yearFrom, yearTo, p.Option.Id, p.Question.Id, sortString, monthFrom, monthTo);
//					}
//					cx = optionRepository.CountByOption(p.Option.Id);
//				}
//			} else if (p.Type == 3) {
//				foreach (ReportPartComponent c in reportRepository.FindComponents(p.Id)) {
//					SortedList all = new SortedList();
//
//					foreach (ProjectRoundUnit u in projectRepository.FindRoundUnitsBySortString(sortString)) {
//						res = new System.Collections.Hashtable();
//
//						if (c.Index.Parts.Capacity == 0) {
//							GetIdxVal(c.Index.Id, u.SortString, langID, yearFrom, yearTo, monthFrom, monthTo);
//						} else {
//							GetOtherIdxVal(c.Index.Id, u.SortString, langID, yearFrom, yearTo, monthFrom, monthTo);
//						}
//
//						if (all.Contains(lastVal)) {
//							all[lastVal] += "," + u.TreeString;
//						} else {
//							all.Add(lastVal, u.TreeString);
//						}
//					}
//
//					for (int i = all.Count - 1; i >= 0; i--) {
//						int color = c.Index.GetColor(Convert.ToInt32(all.GetKey(i)));
//						string[] u = all.GetByIndex(i).ToString().Split(',');
//
//						foreach (string s in u) {
//						}
//					}
//				}
//			} else if (p.Type == 2) {
//				foreach (ReportPartComponent c in reportRepository.FindComponents(p.Id)) {
//					if (c.Index.Parts.Capacity == 0) {
//						GetIdxVal(c.Index.Id, sortString, langID, yearFrom, yearTo, monthFrom, monthTo);
//					} else {
//						GetOtherIdxVal(c.Index.Id, sortString, langID, yearFrom, yearTo, monthFrom, monthTo);
//					}
//					int color = c.Index.GetColor(lastVal);
//				}
//			} else if (p.Type == 8) {
				if (groupBy == 0) {
					groupBy = 2;
				}

				string groupByQuery = GroupFactory.GetGroupBy(groupBy);

//				if (plot == PlotType.BoxPlotMinMax) {
//				} else {
//				}
//				Answer answer = answerRepository.ReadByGroup(groupBy, yearFrom, yearTo, sortString, monthFrom, monthTo);
				var answer = measureRepository.ReadByGroup(groupByQuery, yearFrom, yearTo, sortString, monthFrom, monthTo);
				if (answer != null) {
//					cx = answer.DummyValue1 + 3;
					minDT = answer.DummyValue2;
					maxDT = answer.DummyValue3;
				}

//				cx = 0;

				weeks = GetWeeks(minDT, maxDT, groupBy);

				if (hasGrouping) {
//					int count = 0;
					int aggregation = GetAggregationByGroupBy(groupBy);
					
//					Dictionary<string, string> desc = new Dictionary<string, string>();
//					Dictionary<string, string> join = new Dictionary<string, string>();
//					List<string> item = new List<string>();
//					Dictionary<string, int> mins = new Dictionary<string, int>();
//					string extraDesc = "";

//					count = GroupFactory.GetCount(grouping, sponsorAdminID, sponsorID, projectRoundUnitID, departmentIDs, ref extraDesc, desc, join, item, mins, departmentRepository, questionRepository, sponsorMinUserCountToDisclose);

//					ReportPartComponent c = reportRepository.ReadComponentByPartAndLanguage(p.Id, langID);
//					if (c != null) {
						int bx = 0;
//						foreach(string i in item) {
						foreach(var i in departments) {
							cx = 1;
							int lastDT = minDT - 1;
//							var answers = answerRepository.FindByQuestionAndOptionJoinedAndGrouped2(join[i].ToString(), groupBy, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, yearFrom, yearTo, monthFrom, monthTo);
							var answers = measureRepository.FindByQuestionAndOptionJoinedAndGrouped2(i.Query, groupByQuery, yearFrom, yearTo, monthFrom, monthTo, aggregation, sponsorID);
//							departments.Add(new Department { Name = (string)desc[i] });
							foreach (var a in answers) {
								if (a.DT < minDT) {
									continue;
								}
								while (lastDT + 1 < a.DT) {
									lastDT++;
									cx++;
								}
//								if (a.Values.Count >= mins[i]) {
//								if (a.Components.Count >= i.MinUserCountToDisclose) {
								if (a.Values.Count >= i.MinUserCountToDisclose) {
//									if (count == 1) {
									if (departments.Count == 1) {
									}
//									weeks[GroupStatsGraphFactory.GetBottomString(GB, a.DT, cx, "")].Add(a);
									weeks[GroupStatsGraphFactory.GetBottomString(groupBy, a.DT, cx, "")].Add(a);
								}
								lastDT = a.DT;
								cx++;
							}
							bx++;
						}
//					}
				} else {
					int bx = 0;
					foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(p.Id, langID)) {
						cx = 1;
						int lastDT = minDT - 1;
						var answers = answerRepository.FindByQuestionAndOptionGrouped(groupByQuery, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, yearFrom, yearTo, sortString, monthFrom, monthTo);
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
//			}

			if (plot == PlotType.BoxPlotMinMax) {
				var plotter = new BoxPlotExcel();
				plotter.ForMerge += delegate(object sender, MergeEventArgs e) { OnForMerge(e); };
				plotter.ToExcel(departments, weeks, writer, ref index);
			} else if (plot == PlotType.LineSDWithCI) {
				var plotter = new ConfidenceIntervalLineExcel();
				plotter.ForMerge += delegate(object sender, MergeEventArgs e) { OnForMerge(e); };
				plotter.ToExcel(departments, weeks, writer, ref index);
			} else if (plot == PlotType.LineSD) {
				var plotter = new StandardDeviationLineExcel();
				plotter.ForMerge += delegate(object sender, MergeEventArgs e) { OnForMerge(e); };
				plotter.ToExcel(departments, weeks, writer, ref index);
			} else if (plot == PlotType.Verbose) {
				var plotter = new EverythingExcel();
				plotter.ForMerge += delegate(object sender, MergeEventArgs e) { OnForMerge(e); };
				plotter.ToExcel(departments, weeks, writer, ref index);
			} else {
				var plotter = new LineExcel();
				plotter.ForMerge += delegate(object sender, MergeEventArgs e) { OnForMerge(e); };
				plotter.ToExcel(departments, weeks, writer, ref index);
			}
		}
		
//		Dictionary<string, List<Answer>> GetWeeks(int minDT, int maxDT, int groupBy)
		Dictionary<string, List<IAnswer>> GetWeeks(int minDT, int maxDT, int groupBy)
		{
			int j = 0;
//			Dictionary<string, List<Answer>> weeks = new Dictionary<string, List<Answer>>();
			Dictionary<string, List<IAnswer>> weeks = new Dictionary<string, List<IAnswer>>();
			for (int i = minDT; i <= maxDT; i++) {
				j++;
				//string w = GetBottomString(groupBy, i, j, "");
				string w = GroupStatsGraphFactory.GetBottomString(groupBy, i, j, "");
				if (!weeks.ContainsKey(w)) {
					weeks.Add(w, new List<IAnswer>());
				}
			}
			return weeks;
		}
//
//		public static List<string> GetBottomStrings(int minDT, int maxDT, int groupBy)
//		{
//			int j = 0;
//			var strings = new List<string>();
//			for (int i = minDT; i <= maxDT; i++) {
//				j++;
//				string v = GroupStatsGraphFactory.GetBottomString(groupBy, i, j, "");
//				strings.Add(v);
//			}
//			return strings;
//		}
//
		public event EventHandler<MergeEventArgs> ForMerge;

		protected virtual void OnForMerge(MergeEventArgs e)
		{
			if (ForMerge != null) {
				ForMerge(this, e);
			}
		}

//		public void CreateSuperGraphForExcelWriter(string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, int rpid, string yearFrom, string yearTo, string r1, string r2, int langID, ref int index, int plot, ExcelWriter writer)
//		{
//			// For min/max
//			string rnds = (rnds1 == "0" || rnds2 == "0" ? "" : " AND pru.ProjectRoundUnitID IN (" + rnds1 + (rnds2 != "" ? "," + rnds2 : "") + ")");
//
//			if (rnds1 == "0") {
//				rnds1 = " AND pru.ProjectRoundUnitID NOT IN (" + n + ")";
//			} else {
//				if (rnds1 != "") {
//					rnds1 = " AND (pru.ProjectRoundUnitID IN (" + rnds1 + ")";
//					if (pid1 != "") {
//						rnds1 += " OR a.ProjectRoundUserID IN (" + pid1 + ")";
//					}
//					rnds1 += ")";
//				} else {
//					rnds1 = " AND a.ProjectRoundUserID IN (" + pid1 + ")";
//				}
//			}
//			if (rnds2 == "") {
//				rnds2 = "";
//			} else {
//				if (rnds2 == "0") {
//					rnds2 = " AND pru.ProjectRoundUnitID NOT IN (" + n + ")";
//				} else {
//					if (rnds2 != "") {
//						rnds2 = " AND (pru.ProjectRoundUnitID IN (" + rnds2 + ")";
//						if (pid2 != "") {
//							rnds2 += " OR a.ProjectRoundUserID IN (" + pid2 + ")";
//						}
//						rnds2 += ")";
//					} else {
//						rnds2 = " AND a.ProjectRoundUserID IN (" + pid2 + ")";
//					}
//				}
//			}
//
//			string join1 = "";
//			if (rndsd1 != "") {
//				foreach (var d in departmentRepository.FindIn(rndsd1)) {
//					join1 += string.Format(
//						@"
//		INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
//		INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID
//		INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
//		INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString,{0}) = '{1}' ",
//						d.SortString.Length,
//						d.SortString
//					);
//				}
//			}
//			string join2 = "";
//			if (rndsd2 != "") {
//				foreach (var d in departmentRepository.FindIn(rndsd2)) {
//					join2 += string.Format(
//						@"
//		INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
//		INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID
//		INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
//		INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString,{0}) = '{1}' ",
//						d.SortString.Length,
//						d.SortString
//					);
//				}
//			}
//
//			int cx = 0;
//			int type = 0;
//			int pl = 0;
//
//			int GB = 3;
//			bool stdev = (rnds2 == "");
//			string groupBy = "";
//
//			var r = reportRepository.ReadReportPart(rpid);
//			if (r != null) {
//				type = r.Type;
//				cx = r.Components.Capacity;
//				pl = r.PartLevel;
//			}
//
//			int minDT = 0;
//			int maxDT = 0;
//
//			Dictionary<string, List<Answer>> weeks = new Dictionary<string, List<Answer>>();
//			List<Department> departments = new List<Department>();
//
//			if (type == 8) {
//				groupBy = GroupFactory.GetGroupBy(GB);
//
//				Answer answer = answerRepository.ReadByGroup(groupBy, yearFrom, yearTo, rnds);
//				if (answer != null) {
//					cx = answer.DummyValue1 + 3;
//					minDT = answer.DummyValue2;
//					maxDT = answer.DummyValue3;
//				}
//
//				weeks = GetWeeks(minDT, maxDT, GB);
//
//				List<IIndex> indexes = new List<IIndex>();
//				List<IMinMax> minMaxes = new List<IMinMax>();
//				foreach (var p in reportRepository.FindComponentsByPart(rpid)) {
//					Answer a = answerRepository.ReadMinMax(groupBy, p.WeightedQuestionOption.Question.Id, p.WeightedQuestionOption.Option.Id, yearFrom, yearTo, rnds);
//					if (a != null) {
//						minMaxes.Add(a);
//					} else {
//						minMaxes.Add(new Answer());
//					}
//					indexes.Add(p.WeightedQuestionOption);
//				}
//			} else {
//				cx += 2;
//			}
//
//			cx = 0;
//
//			if (type == 8) {
//				int bx = 0;
//				var p = reportRepository.ReadComponentByPartAndLanguage(rpid, langID);
//				if (p != null) {
//					Series s1 = new Series { Description = r1, Color = 4, X = 300, Y = 20 };
//
//					departments.Add(new Department { Name = r1 });
//
//					cx = 1;
//					int lastDT = minDT - 1;
//
//					foreach (var a in answerRepository.FindByQuestionAndOptionGrouped4(groupBy, p.WeightedQuestionOption.Question.Id, p.WeightedQuestionOption.Option.Id, join1, yearFrom, yearTo, rnds1)) {
//						while (lastDT + 1 < a.DT) {
//							lastDT++;
//							cx++;
//						}
//						if (a.Values.Count >= r.RequiredAnswerCount) {
//							s1.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
//							weeks[GetBottomString(GB, a.DT, cx, "")].Add(a);
//						}
//						lastDT = a.DT;
//						cx++;
//					}
//
//					if (rnds2 != "") {
//						Series s2 = new Series { Description = r2, Color = 5, X = 600, Y = 20 };
//
//						departments.Add(new Department { Name = r2 });
//
//						cx = 1;
//						lastDT = minDT - 1;
//						foreach (var a in answerRepository.FindByQuestionAndOptionGrouped4(groupBy, p.WeightedQuestionOption.Question.Id, p.WeightedQuestionOption.Option.Id, join2, yearFrom, yearTo, rnds2)) {
//							while (lastDT + 1 < a.DT) {
//								lastDT++;
//								cx++;
//							}
//							if (a.Values.Count >= r.RequiredAnswerCount) {
//								s2.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
//								weeks[GetBottomString(GB, a.DT, cx, "")].Add(a);
//							}
//							lastDT = a.DT;
//							cx++;
//						}
//					}
//					bx++;
//				}
//			}
//
//			if (plot == PlotType.BoxPlotMinMax) {
//				var plotter = new BoxPlotExcel();
//				plotter.ForMerge += delegate(object sender, MergeEventArgs e) { OnForMerge(e); };
//				plotter.ToExcel(departments, weeks, writer, ref index);
//			} else if (plot == PlotType.LineSDWithCI) {
//				var plotter = new ConfidenceIntervalLineExcel();
//				plotter.ForMerge += delegate(object sender, MergeEventArgs e) { OnForMerge(e); };
//				plotter.ToExcel(departments, weeks, writer, ref index);
//			} else if (plot == PlotType.LineSD) {
//				var plotter = new StandardDeviationLineExcel();
//				plotter.ForMerge += delegate(object sender, MergeEventArgs e) { OnForMerge(e); };
//				plotter.ToExcel(departments, weeks, writer, ref index);
//			} else if (plot == PlotType.Verbose) {
//				var plotter = new EverythingExcel();
//				plotter.ForMerge += delegate(object sender, MergeEventArgs e) { OnForMerge(e); };
//				plotter.ToExcel(departments, weeks, writer, ref index);
//			} else {
//				var plotter = new LineExcel();
//				plotter.ForMerge += delegate(object sender, MergeEventArgs e) { OnForMerge(e); };
//				plotter.ToExcel(departments, weeks, writer, ref index);
//			}
//		}
//
//		public static string GetBottomString(int groupBy, int i, int dx, string str)
//		{
//			switch (groupBy) {
//				case 1:
//					{
//						int d = i;
//						int w = d % 52;
//						if (w == 0) {
//							w = 52;
//						}
//						string v = string.Format("v{0}, {1}{2}", w, d / 52, str);
//						return v;
//					}
//				case 2:
//					{
//						int d = i * 2;
//						int w = d % 52;
//						if (w == 0) {
//							w = 52;
//						}
//						string v = string.Format("v{0}-{1}, {2}{3}", w - 1, w, (d - ((d - 1) % 52)) / 52, str);
//						return v;
//					}
//				case 3:
//					{
//						int d = i;
//						int w = d % 12;
//						if (w == 0) {
//							w = 12;
//						}
//						string v = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[w - 1] + ", " + ((d - w) / 12) + str;
//						return v;
//					}
//				case 4:
//					{
//						int d = i * 3;
//						int w = d % 12;
//						if (w == 0) {
//							w = 12;
//						}
//						string v = string.Format("Q{0}, {1}{2}", w / 3, (d - w) / 12, str);
//						return v;
//					}
//				case 5:
//					{
//						int d = i * 6;
//						int w = d % 12;
//						if (w == 0) {
//							w = 12;
//						}
//						string v = string.Format("{0}/{1}{2}", (d - w) / 12, w / 6, str);
//						return v;
//					}
//				case 6:
//					{
//						string v = i.ToString() + str;
//						return v;
//					}
//				case 7:
//					{
//						int d = i * 2;
//						int w = d % 52;
//						if (w == 0) {
//							w = 52;
//						}
//						string v = "v" + w + "-" + ((w == 52 ? 0 : w) + 1) + ", " + ((d + 1) - (d % 52)) / 52 + str;
//						return v;
//					}
//				default:
//					throw new NotSupportedException();
//			}
//		}
		
		void GetIdxVal(int idx, string sortString, int langID, int fy, int ty, int fm, int tm)
		{
			foreach (Index i in indexRepository.FindByLanguage(idx, langID, fy, ty, sortString, fm, tm)) {
				lastCount = i.CountDX;
				lastVal = i.AverageAX;
				lastDesc = i.Languages[0].IndexName;
				if (!res.Contains(i.Id)) {
					res.Add(i.Id, lastVal);
				}
				if (!cnt.Contains(i.Id)) {
					cnt.Add(i.Id, lastCount);
				}
			}
		}
		
		void GetOtherIdxVal(int idx, string sortString, int langID, int fy, int ty, int fm, int tm)
		{
			float tot = 0;
			int max = 0;
			int minCnt = Int32.MaxValue;
			Index index = indexRepository.ReadByIdAndLanguage(idx, langID);
			if (index != null) {
				lastDesc = index.Languages[0].IndexName;
				foreach (IndexPart p in index.Parts) {
					max += 100 * p.Multiple;
					if (res.Contains(p.OtherIndex.Id)) {
						tot += (float)res[p.OtherIndex.Id] * p.Multiple;
						minCnt = Math.Min((int)cnt[p.OtherIndex.Id], minCnt);
					} else {
						GetIdxVal(p.OtherIndex.Id, sortString, langID, fy, ty, fm, tm);
						tot += lastVal * p.Multiple;
						minCnt = Math.Min(lastCount, minCnt);
					}
				}
			}
			lastVal = 100 * tot / max;
			lastCount = minCnt;
		}
	}
}