﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Core.Helpers
{
	public class GroupStatsGraphFactory : BaseGraphFactory
	{
		SqlProjectRepository projectRepo;
		SqlAnswerRepository answerRepo;
		SqlOptionRepository optionRepo;
		SqlReportRepository reportRepo;
		SqlIndexRepository indexRepo;
		SqlQuestionRepository questionRepo;
		SqlDepartmentRepository departmentRepo;
		
		int lastCount = 0;
		float lastVal = 0;
		string lastDesc = "";
		System.Collections.Hashtable res = new System.Collections.Hashtable();
		System.Collections.Hashtable cnt = new System.Collections.Hashtable();
		
		public GroupStatsGraphFactory(SqlAnswerRepository answerRepo,
		                              SqlReportRepository reportRepo,
		                              SqlProjectRepository projectRepo,
		                              SqlOptionRepository optionRepo,
		                              SqlIndexRepository indexRepo,
		                              SqlQuestionRepository questionRepo,
		                              SqlDepartmentRepository departmentRepo)
		{
			this.projectRepo = projectRepo;
			this.answerRepo = answerRepo;
			this.optionRepo = optionRepo;
			this.reportRepo = reportRepo;
			this.indexRepo = indexRepo;
			this.questionRepo = questionRepo;
			this.departmentRepo = departmentRepo;
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
			} else if (plot == PlotType.Bar) {
				return new BarGraphType();
			} else {
				return new LineGraphType(0, t);
			}
		}
		
		ExtendedGraph GetGraphForReportPartTypeOne(ReportPart p, int langID, string sortString, int yearFrom, int yearTo, int monthFrom, int monthTo)
		{
			ExtendedGraph g = null;
			decimal tot = answerRepo.CountByDate(yearFrom, yearTo, sortString, monthFrom, monthTo);
			if (p.RequiredAnswerCount > Convert.ToInt32(tot)) {
				g = new ExtendedGraph(895, 50, "#FFFFFF");
				g.drawStringInGraph("Ingen återkoppling pga otillräckligt underlag", 300, -30);
			} else {
				g = new ExtendedGraph(895, 550, "#FFFFFF");
				List<Bar> bars = new List<Bar>();
				foreach (OptionComponents c in optionRepo.FindComponentsByLanguage(p.Option.Id, langID)) {
					int x = answerRepo.CountByValueWithDateOptionAndQuestion(c.Component.Id, yearFrom, yearTo, p.Option.Id, p.Question.Id, sortString, monthFrom, monthTo);
					var b = new Bar {
						Description = c.Component.CurrentLanguage.Text,
						Value = Convert.ToInt32(Math.Round(Convert.ToDecimal(x) / tot * 100M, 0)),
						Color = 5
					};
					bars.Add(b);
				}
				int cx = optionRepo.CountByOption(p.Option.Id);
				g.DrawBars(null, cx, tot, bars);
				g.drawAxisExpl(string.Format("% (n = {0})", tot), 5, false, false);
			}
			return g;
		}
		
		ExtendedGraph GetGraphForReportPartTypeThree(ReportPart p, int langID, int cx, string sortString, int yearFrom, int yearTo, int monthFrom, int monthTo)
		{
			ExtendedGraph g = new ExtendedGraph(895, 550, "#FFFFFF");
			List<Bar> bars = new List<Bar>();
			List<int> referenceLines = new List<int>();
			
			foreach (ReportPartComponent c in reportRepo.FindComponents(p.Id)) {
				System.Collections.SortedList all = new System.Collections.SortedList();
				
				foreach (ProjectRoundUnit u in projectRepo.FindRoundUnitsBySortString(sortString)) {
					res = new System.Collections.Hashtable();
					
					if (c.Index.Parts.Capacity == 0) {
						GetIdxVal(c.Index.Id, u.SortString, langID, yearFrom, yearTo, monthFrom, monthTo);
					} else {
						GetOtherIdxVal(c.Index.Id, u.SortString, langID, yearFrom, yearTo, monthFrom, monthTo);
					}
					
					if (all.Contains(lastVal)) {
						all[lastVal] += "," + u.TreeString;
					} else {
						all.Add(lastVal, u.TreeString);
					}
				}
				
				for (int i = all.Count - 1; i >= 0; i--) {
					int color = c.Index.GetColor(Convert.ToInt32(all.GetKey(i)));
					string[] u = all.GetByIndex(i).ToString().Split(',');
					
					foreach (string s in u) {
						bars.Add(new Bar { Color = color, Value = Convert.ToInt32(all.GetKey(i)), Description = s });
					}
				}
				referenceLines.Add(c.Index.TargetVal);
			}
			g.DrawBars(null, cx, bars, referenceLines);
			g.drawAxisExpl("poäng", 0, false, false);
			return g;
		}
		
		ExtendedGraph GetIndexReportPartGraph(ReportPart p, int langID, int cx, string sortString, int yearFrom, int yearTo, int monthFrom, int monthTo)
		{
			ExtendedGraph g = new ExtendedGraph(895, 550, "#FFFFFF");
			List<Bar> bars = new List<Bar>();
			foreach (ReportPartComponent rpc in reportRepo.FindComponents(p.Id)) {
				if (rpc.Index.Parts.Capacity == 0) {
					GetIdxVal(rpc.Index.Id, sortString, langID, yearFrom, yearTo, monthFrom, monthTo);
				} else {
					GetOtherIdxVal(rpc.Index.Id, sortString, langID, yearFrom, yearTo, monthFrom, monthTo);
				}
				int color = rpc.Index.GetColor(lastVal);
				bars.Add(new Bar { Value = lastVal, Color = color, Description = lastDesc, Reference = rpc.Index.TargetVal });
			}
			g.DrawBars(null, cx, bars);
			g.drawAxisExpl("poäng", 0, false, false);
			g.drawReference(780, 25, " = riktvärde");
			return g;
		}
		
		public ExtendedGraph GetWeightedQuestionOptionReportPartGraph(ReportPart reportPart, int langID, int point, bool hasGrouping, int sponsorID, int sponsorMinUserCountToDisclose, string departmentIDs, int projectRoundUnitID, int sponsorAdminID, int grouping, int groupBy, int plot, int cx, string sortString, DateTime dateFrom, DateTime dateTo)
		{
			string groupByQuery = GroupFactory.GetGroupBy(groupBy);
			int minDT = 0;
			int maxDT = 0;
			ExtendedGraph g = new ExtendedGraph(895, 440, "#FFFFFF");
			g.Type = GetGraphType(plot, 2);
			Answer answer = answerRepo.ReadByGroup(groupByQuery, dateFrom.Year, dateTo.Year, sortString, dateFrom.Month, dateTo.Month);
			if (answer != null) {
				cx = answer.DummyValue1 + 3;
				minDT = answer.DummyValue2;
				maxDT = answer.DummyValue3;
			}
			
			List<IIndex> indexes = new List<IIndex>();
			List<IMinMax> minMaxes = new List<IMinMax>();
			foreach (ReportPartComponent rpc in reportRepo.FindComponentsByPart(reportPart.Id)) {
				if (!hasGrouping) {
					Answer a = answerRepo.ReadMinMax(groupByQuery, rpc.WeightedQuestionOption.Question.Id, rpc.WeightedQuestionOption.Option.Id, dateFrom.Year, dateTo.Year, sortString, dateFrom.Month, dateTo.Month);
					if (a != null) {
						minMaxes.Add(a);
					} else {
						minMaxes.Add(new Answer());
					}
				} else {
					minMaxes.Add(new Answer());
				}
				indexes.Add(rpc.WeightedQuestionOption);
			}
			g.SetMinMaxes(minMaxes);
			g.DrawBackgroundFromIndexes(indexes);
			g.DrawComputingSteps(null, cx);
			
			cx = 0;
			
			g.DrawBottomString(minDT, maxDT, groupBy);
			
			if (hasGrouping) {
				int count = 0;
				Dictionary<string, string> desc = new Dictionary<string, string>();
				Dictionary<string, string> join =  new Dictionary<string, string>();
				List<string> item = new List<string>();
				Dictionary<string, int> mins = new Dictionary<string, int>();
				string extraDesc = "";
				
				count = GroupFactory.GetCount(grouping, sponsorAdminID, sponsorID, projectRoundUnitID, departmentIDs, ref extraDesc, desc, join, item, mins, departmentRepo, questionRepo, sponsorMinUserCountToDisclose);
				
				int breaker = 6, itemWidth = 120;
				if (count < 6) {
					breaker = 4;
					itemWidth = 180;
				}
				if (count < 4) {
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
				ReportPartComponent c = reportRepo.ReadComponentByPartAndLanguage(reportPart.Id, langID);
				if (c != null) {
					int bx = 0;
					foreach(string i in item) {
						cx = 1;
						int lastDT = minDT - 1;
						var answers = answerRepo.FindByQuestionAndOptionJoinedAndGrouped2(join[i].ToString(), groupByQuery, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, dateFrom.Year, dateTo.Year, dateFrom.Month, dateTo.Month);
						Series s = new Series {
							Description = (string)desc[i],
							Color = bx + 4,
							X = 130 + (int)((bx % breaker) * itemWidth),
							Y = 20 + (int)Math.Floor((double)bx / breaker) * 15
						};
						foreach (Answer a in answers) {
							if (a.DT < minDT) {
								continue;
							}
							while (lastDT + 1 < a.DT) {
								lastDT++;
								cx++;
							}
							if (a.Values.Count >= mins[i]) {
								if (count == 1) {
									string v = GetBottomString(groupBy, a.DT, cx, (count == 1 ? ", n = " + a.Values.Count : ""));
									g.DrawBottomString(v, cx);
								}
								s.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
							}
							lastDT = a.DT;
							cx++;
						}
						g.Series.Add(s);
						bx++;
					}
				}
			} else {
				int bx = 0;
				var components = reportRepo.FindComponentsByPartAndLanguage2(reportPart.Id, langID);
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
					cx = 1;
					int lastDT = minDT - 1;
					Series s = new Series { Color = bx + 4 };
					var answers = answerRepo.FindByQuestionAndOptionGroupedX(groupByQuery, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, dateFrom.Year, dateTo.Year, sortString, dateFrom.Month, dateTo.Month);
					foreach (Answer a in answers) {
						if (a.DT < minDT) {
							continue;
						}
						while (lastDT + 1 < a.DT) {
							lastDT++;
							cx++;
						}
						
						if (a.CountV >= reportPart.RequiredAnswerCount) {
							string v = GetBottomString(groupBy, a.DT, cx, ", n = " + a.CountV);
							g.DrawBottomString(v, cx);
							s.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
						}
						lastDT = a.DT;
						cx++;
					}
					g.Series.Add(s);
					bx++;
				}
			}
			g.Draw();
			return g;
		}
		
		ExtendedGraph GetGraphForReportPartTypeOneXXX(ReportPart reportPart, ProjectRoundUnit projectRoundUnit, int langID, DateTime dateFrom, DateTime dateTo)
		{
			ExtendedGraph g = null;
			decimal tot = answerRepo.CountByDate(dateFrom.Year, dateTo.Year, projectRoundUnit.SortString, dateFrom.Month, dateTo.Month);
			if (reportPart.RequiredAnswerCount > Convert.ToInt32(tot)) {
				g = new ExtendedGraph(895, 50, "#FFFFFF");
				g.drawStringInGraph("Ingen återkoppling pga otillräckligt underlag", 300, -30);
			} else {
				g = new ExtendedGraph(895, 550, "#FFFFFF");
				List<Bar> bars = new List<Bar>();
				foreach (OptionComponents c in optionRepo.FindComponentsByLanguage(reportPart.Option.Id, langID != 0 ? langID : projectRoundUnit.Language.Id)) {
					int x = answerRepo.CountByValueWithDateOptionAndQuestion(c.Component.Id, dateFrom.Year, dateTo.Month, reportPart.Option.Id, reportPart.Question.Id, projectRoundUnit.SortString, dateFrom.Month, dateTo.Month);
					var b = new Bar {
						Description = c.Component.CurrentLanguage.Text,
						Value = Convert.ToInt32(Math.Round(Convert.ToDecimal(x) / tot * 100M, 0)),
						Color = 5
					};
					bars.Add(b);
				}
				int cx = optionRepo.CountByOption(reportPart.Option.Id);
				g.DrawBars(null, cx, tot, bars);
				g.drawAxisExpl(string.Format("% (n = {0})", tot), 5, false, false);
			}
			return g;
		}
		
		ExtendedGraph GetGraphForReportPartTypeThreeXXX(ReportPart reportPart, ProjectRoundUnit projectRoundUnit, int langID, DateTime dateFrom, DateTime dateTo)
		{
			ExtendedGraph g = new ExtendedGraph(895, 550, "#FFFFFF");
			List<Bar> bars = new List<Bar>();
			List<int> referenceLines = new List<int>();
			
			foreach (ReportPartComponent c in reportRepo.FindComponents(reportPart.Id)) {
				System.Collections.SortedList all = new System.Collections.SortedList();
				
				foreach (ProjectRoundUnit u in projectRepo.FindRoundUnitsBySortString(projectRoundUnit.SortString)) {
					res = new System.Collections.Hashtable();
					
					if (c.Index.Parts.Capacity == 0) {
						GetIdxVal(c.Index.Id, u.SortString, langID != 0 ? langID : projectRoundUnit.Language.Id, dateFrom.Year, dateTo.Year, dateFrom.Month, dateTo.Month);
					} else {
						GetOtherIdxVal(c.Index.Id, u.SortString, langID != 0 ? langID : projectRoundUnit.Language.Id, dateFrom.Year, dateTo.Year, dateFrom.Month, dateTo.Month);
					}
					
					if (all.Contains(lastVal)) {
						all[lastVal] += "," + u.TreeString;
					} else {
						all.Add(lastVal, u.TreeString);
					}
				}
				
				for (int i = all.Count - 1; i >= 0; i--) {
					int color = c.Index.GetColor(Convert.ToInt32(all.GetKey(i)));
					string[] u = all.GetByIndex(i).ToString().Split(',');
					
					foreach (string s in u) {
						bars.Add(new Bar { Color = color, Value = Convert.ToInt32(all.GetKey(i)), Description = s });
					}
				}
				referenceLines.Add(c.Index.TargetVal);
			}
			g.DrawBars(null, reportPart.Components.Capacity, bars, referenceLines);
			g.drawAxisExpl("poäng", 0, false, false);
			return g;
		}
		
//		public ExtendedGraph GetIndexReportPartGraph2(ReportPart reportPart, ProjectRoundUnit projectRoundUnit, int langID, DateTime dateFrom, DateTime dateTo)
//		{
//			ExtendedGraph g = new ExtendedGraph(895, 550, "#FFFFFF");
//			List<Bar> bars = new List<Bar>();
//			foreach (ReportPartComponent rpc in reportRepo.FindComponents(reportPart.Id)) {
//				if (rpc.Index.Parts.Capacity == 0) {
//					GetIdxVal(rpc.Index.Id, projectRoundUnit.SortString, langID != 0 ? langID : projectRoundUnit.Language.Id, dateFrom.Year, dateTo.Year, dateFrom.Month, dateTo.Month);
//				} else {
//					GetOtherIdxVal(rpc.Index.Id, projectRoundUnit.SortString, langID != 0 ? langID : projectRoundUnit.Language.Id, dateFrom.Year, dateTo.Year, dateFrom.Month, dateTo.Month);
//				}
//				int color = rpc.Index.GetColor(lastVal);
//				bars.Add(new Bar { Value = lastVal, Color = color, Description = lastDesc, Reference = rpc.Index.TargetVal });
//			}
//			g.DrawBars(null, reportPart.Components.Capacity, bars);
//			g.drawAxisExpl("poäng", 0, false, false);
//			g.drawReference(780, 25, " = riktvärde");
//			return g;
//		}
		
//		HWList lalala(int count, float x)
//		{
//			var l = new HWList();
//			for (int i = 0; i < count; i++) {
//				l.Add(x);
//			}
//			return l;
//		}
		
		public ExtendedGraph GetIndexReportPartGraph2(ReportPart reportPart, int langID, int point, bool hasGrouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs, ProjectRoundUnit projectRoundUnit, int grouping, int groupBy, int plot, DateTime dateFrom, DateTime dateTo)
		{
			int cx = reportPart.Components.Capacity;
			
			string groupByQuery = GroupFactory.GetGroupBy(groupBy);
			int minDT = 0;
			int maxDT = 0;
			ExtendedGraph g = new ExtendedGraph(895, 440, "#FFFFFF");
			g.Type = GetGraphType(plot, 2);
			Answer answer = answerRepo.ReadByGroup(groupByQuery, dateFrom.Year, dateTo.Year, projectRoundUnit.SortString, dateFrom.Month, dateTo.Month);
			if (answer != null) {
				cx = answer.DummyValue1 + 3;
				minDT = answer.DummyValue2;
				maxDT = answer.DummyValue3;
			}
//			List<IIndex> indexes = new List<IIndex>();
//			List<IMinMax> minMaxes = new List<IMinMax>();
//			foreach (ReportPartComponent rpc in reportRepo.FindComponentsByPart(reportPart.Id)) {
//				if (!hasGrouping) {
//					Answer a = answerRepo.ReadMinMax(groupByQuery, rpc.WeightedQuestionOption.Question.Id, rpc.WeightedQuestionOption.Option.Id, dateFrom.Year, dateTo.Year, projectRoundUnit.SortString, dateFrom.Month, dateTo.Month);
//					if (a != null) {
//						minMaxes.Add(a);
//					} else {
//						minMaxes.Add(new Answer());
//					}
//				} else {
//					minMaxes.Add(new Answer());
//				}
//				indexes.Add(rpc.WeightedQuestionOption);
//			}
//			g.SetMinMaxes(minMaxes);
//			g.DrawBackgroundFromIndexes(indexes);
			g.SetMinMax(new Answer());
			g.DrawComputingSteps(null, cx);
			
			cx = 0;
			
			g.DrawBottomString(minDT, maxDT, groupBy);
			
			if (hasGrouping) {
				string extraDesc = "";
				
				var departments = GetSponsorAdminSponsorDepartments(grouping, sponsorAdmin, sponsor, departmentIDs, departmentRepo);
				
				var departmentsWithQuery = GroupFactory.GetCount2(grouping, sponsor, projectRoundUnit, departmentIDs, ref extraDesc, questionRepo, departments);
				
				int breaker = 6;
				int itemWidth = 120;
				if (departmentsWithQuery.Count < 6) {
					breaker = 4;
					itemWidth = 180;
				}
				if (departmentsWithQuery.Count < 4) {
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
				var c = reportPart.FirstComponent;
				if (c.HasIndex) {
					int bx = 0;
					foreach(var i in departmentsWithQuery) {
						cx = 1;
						int lastDT = minDT - 1;
						var indexes = indexRepo.FindByLanguage2(i.Query, groupByQuery, c.Index.Id, langID, dateFrom.Year, dateTo.Year, projectRoundUnit.SortString, dateFrom.Month, dateTo.Month);
						Series s = new Series {
							Description = i.Name,
							Color = bx + 4,
							X = 130 + (int)((bx % breaker) * itemWidth),
							Y = 20 + (int)Math.Floor((double)bx / breaker) * 15
						};
						foreach (var a in indexes) {
							if (a.DT < minDT) {
								continue;
							}
							while (lastDT + 1 < a.DT) {
								lastDT++;
								cx++;
							}
							if (a.CountDX >= i.MinUserCountToDisclose) {
								if (departmentsWithQuery.Count == 1) {
									string v = GetBottomString(groupBy, a.DT, cx, (departmentsWithQuery.Count == 1 ? ", n = " + a.CountDX : ""));
									g.DrawBottomString(v, cx);
								}
								s.Points.Add(new PointV { X = cx, Values = new HWList(a.AverageAX) });
							}
							lastDT = a.DT;
							cx++;
						}
						g.Series.Add(s);
						bx++;
					}
				}
			} else {
				int bx = 0;
				var components = reportRepo.FindComponentsByPartAndLanguage2(reportPart.Id, projectRoundUnit.Language.Id);
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
					cx = 1;
					int lastDT = minDT - 1;
					Series s = new Series { Color = bx + 4 };
					var answers = answerRepo.FindByQuestionAndOptionGroupedX(groupByQuery, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, dateFrom.Year, dateTo.Year, projectRoundUnit.SortString, dateFrom.Month, dateTo.Month);
					foreach (Answer a in answers) {
						if (a.DT < minDT) {
							continue;
						}
						while (lastDT + 1 < a.DT) {
							lastDT++;
							cx++;
						}
						
						if (a.CountV >= reportPart.RequiredAnswerCount) {
							string v = GetBottomString(groupBy, a.DT, cx, ", n = " + a.CountV);
							g.DrawBottomString(v, cx);
							s.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
						}
						lastDT = a.DT;
						cx++;
					}
					g.Series.Add(s);
					bx++;
				}
			}
			g.Draw();
			return g;
		}
		
		IList<Department> GetSponsorAdminSponsorDepartments(int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs, SqlDepartmentRepository departmentRepo)
		{
			IList<Department> departments = new List<Department>();
			switch (grouping) {
				case Grouping.None:
					departments = sponsorAdmin != null ? departmentRepo.FindBySponsorWithSponsorAdmin(sponsor.Id, sponsorAdmin.Id, sponsor.MinUserCountToDisclose) : departmentRepo.FindBySponsorOrderedBySortString(sponsor.Id, sponsor.MinUserCountToDisclose);
					break;
				case Grouping.UsersOnUnit:
					departments = sponsorAdmin != null ? departmentRepo.FindBySponsorWithSponsorAdminIn(sponsor.Id, sponsorAdmin.Id, departmentIDs, sponsor.MinUserCountToDisclose) : departmentRepo.FindBySponsorOrderedBySortStringIn(sponsor.Id, departmentIDs, sponsor.MinUserCountToDisclose);
					break;
				case Grouping.UsersOnUnitAndSubUnits:
					departments = sponsorAdmin != null ? departmentRepo.FindBySponsorWithSponsorAdminIn(sponsor.Id, sponsorAdmin.Id, departmentIDs, sponsor.MinUserCountToDisclose) : departmentRepo.FindBySponsorOrderedBySortStringIn(sponsor.Id, departmentIDs, sponsor.MinUserCountToDisclose);
					break;
				case Grouping.BackgroundVariable:
					departments = sponsorAdmin != null ? departmentRepo.FindBySponsorWithSponsorAdmin(sponsor.Id, sponsorAdmin.Id, sponsor.MinUserCountToDisclose) : departmentRepo.FindBySponsorOrderedBySortString(sponsor.Id, sponsor.MinUserCountToDisclose);
					break;
			}
			return departments;
		}
		
		public ExtendedGraph GetWeightedQuestionOptionReportPartGraph2(ReportPart reportPart, int langID, int point, bool hasGrouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs, ProjectRoundUnit projectRoundUnit, int grouping, int groupBy, int plot, DateTime dateFrom, DateTime dateTo)
		{
			int cx = reportPart.Components.Capacity;
			
			string groupByQuery = GroupFactory.GetGroupBy(groupBy);
			int minDT = 0;
			int maxDT = 0;
			ExtendedGraph g = new ExtendedGraph(895, 440, "#FFFFFF");
			g.Type = GetGraphType(plot, 2);
			Answer answer = answerRepo.ReadByGroup(groupByQuery, dateFrom.Year, dateTo.Year, projectRoundUnit.SortString, dateFrom.Month, dateTo.Month);
			if (answer != null) {
				cx = answer.DummyValue1 + 3;
				minDT = answer.DummyValue2;
				maxDT = answer.DummyValue3;
			}
			
			List<IIndex> indexes = new List<IIndex>();
			List<IMinMax> minMaxes = new List<IMinMax>();
			foreach (ReportPartComponent rpc in reportRepo.FindComponentsByPart(reportPart.Id)) {
				if (!hasGrouping) {
					Answer a = answerRepo.ReadMinMax(groupByQuery, rpc.WeightedQuestionOption.Question.Id, rpc.WeightedQuestionOption.Option.Id, dateFrom.Year, dateTo.Year, projectRoundUnit.SortString, dateFrom.Month, dateTo.Month);
					if (a != null) {
						minMaxes.Add(a);
					} else {
						minMaxes.Add(new Answer());
					}
				} else {
					minMaxes.Add(new Answer());
				}
				indexes.Add(rpc.WeightedQuestionOption);
			}
			g.SetMinMaxes(minMaxes);
			g.DrawBackgroundFromIndexes(indexes);
			g.DrawComputingSteps(null, cx);
			
			cx = 0;
			
			g.DrawBottomString(minDT, maxDT, groupBy);
			
			if (hasGrouping) {
				string extraDesc = "";
				
				var departments = GetSponsorAdminSponsorDepartments(grouping, sponsorAdmin, sponsor, departmentIDs, departmentRepo);
				
				var departmentsWithQuery = GroupFactory.GetCount2(grouping, sponsor, projectRoundUnit, departmentIDs, ref extraDesc, questionRepo, departments);
				
				int breaker = 6;
				int itemWidth = 120;
				if (departmentsWithQuery.Count < 6) {
					breaker = 4;
					itemWidth = 180;
				}
				if (departmentsWithQuery.Count < 4) {
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
				ReportPartComponent c = reportRepo.ReadComponentByPartAndLanguage(reportPart.Id, projectRoundUnit.Language.Id);
				if (c != null) {
					int bx = 0;
					foreach(var i in departmentsWithQuery) {
						cx = 1;
						int lastDT = minDT - 1;
						var answers = answerRepo.FindByQuestionAndOptionJoinedAndGrouped2(i.Query, groupByQuery, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, dateFrom.Year, dateTo.Year, dateFrom.Month, dateTo.Month);
						Series s = new Series {
							Description = i.Name,
							Color = bx + 4,
							X = 130 + (int)((bx % breaker) * itemWidth),
							Y = 20 + (int)Math.Floor((double)bx / breaker) * 15
						};
						foreach (Answer a in answers) {
							if (a.DT < minDT) {
								continue;
							}
							while (lastDT + 1 < a.DT) {
								lastDT++;
								cx++;
							}
							if (a.Values.Count >= i.MinUserCountToDisclose) {
								if (departmentsWithQuery.Count == 1) {
									string v = GetBottomString(groupBy, a.DT, cx, (departmentsWithQuery.Count == 1 ? ", n = " + a.Values.Count : ""));
									g.DrawBottomString(v, cx);
								}
								s.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
							}
							lastDT = a.DT;
							cx++;
						}
						g.Series.Add(s);
						bx++;
					}
				}
			} else {
				int bx = 0;
				var components = reportRepo.FindComponentsByPartAndLanguage2(reportPart.Id, projectRoundUnit.Language.Id);
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
					cx = 1;
					int lastDT = minDT - 1;
					Series s = new Series { Color = bx + 4 };
					var answers = answerRepo.FindByQuestionAndOptionGroupedX(groupByQuery, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, dateFrom.Year, dateTo.Year, projectRoundUnit.SortString, dateFrom.Month, dateTo.Month);
					foreach (Answer a in answers) {
						if (a.DT < minDT) {
							continue;
						}
						while (lastDT + 1 < a.DT) {
							lastDT++;
							cx++;
						}
						
						if (a.CountV >= reportPart.RequiredAnswerCount) {
							string v = GetBottomString(groupBy, a.DT, cx, ", n = " + a.CountV);
							g.DrawBottomString(v, cx);
							s.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
						}
						lastDT = a.DT;
						cx++;
					}
					g.Series.Add(s);
					bx++;
				}
			}
			g.Draw();
			return g;
		}
		
		public override ExtendedGraph CreateGraph(string key, ReportPart reportPart, int langID, int projectRoundUnitID, DateTime dateFrom, DateTime dateTo, int groupBy, bool hasGrouping, int plot, int width, int height, string bg, int grouping, int sponsorAdminID, int sponsorID, string departmentIDs, object disabled, int point, int sponsorMinUserCountToDisclose)
		{
			int cx = reportPart.Components.Capacity;
			
			string sortString = "";
			ProjectRoundUnit roundUnit = projectRepo.ReadRoundUnit(projectRoundUnitID);
			if (roundUnit != null) {
				sortString = roundUnit.SortString;
				if (langID == 0) {
					langID = roundUnit.Language.Id;
				}
			}
			
			LanguageFactory.SetCurrentCulture(langID);
			
			ExtendedGraph g = null;
			if (reportPart.Type == 1) {
				g = GetGraphForReportPartTypeOne(reportPart, langID, sortString, dateFrom.Year, dateTo.Year, dateFrom.Month, dateTo.Month);
			} else if (reportPart.Type == 3) {
				g = GetGraphForReportPartTypeThree(reportPart, langID, cx, sortString, dateFrom.Year, dateTo.Year, dateFrom.Month, dateTo.Month);
			} else if (reportPart.Type == 2) {
				g = GetIndexReportPartGraph(reportPart, langID, cx, sortString, dateFrom.Year, dateTo.Year, dateFrom.Month, dateTo.Month);
			} else if (reportPart.Type == 8) {
//				if (groupBy == 0) {
//					groupBy = GroupBy.TwoWeeksStartWithOdd;
//				}
//				g = GetWeightedQuestionOptionReportPartGraph(reportPart, langID, point, hasGrouping, sponsorID, sponsorMinUserCountToDisclose, departmentIDs, projectRoundUnitID, sponsorAdminID, grouping, groupBy != 0 ? groupBy : GroupBy.TwoWeeksStartWithOdd, plot, cx, sortString, dateFrom.Year, dateTo.Year, dateFrom.Month, dateTo.Month);
				g = GetWeightedQuestionOptionReportPartGraph(reportPart, langID, point, hasGrouping, sponsorID, sponsorMinUserCountToDisclose, departmentIDs, projectRoundUnitID, sponsorAdminID, grouping, groupBy != 0 ? groupBy : GroupBy.TwoWeeksStartWithOdd, plot, cx, sortString, dateFrom, dateTo);
			}
			return g;
		}
		
		public ExtendedGraph CreateGraph2(ReportPart reportPart, ProjectRoundUnit projectRoundUnit, int langID, SponsorAdmin sponsorAdmin, Sponsor sponsor, DateTime dateFrom, DateTime dateTo, int groupBy, bool hasGrouping, int plot, int grouping, string departmentIDs, int point)
		{
			LanguageFactory.SetCurrentCulture(langID);
			
			ExtendedGraph g = null;
			if (reportPart.Type == 1) {
				g = GetGraphForReportPartTypeOneXXX(reportPart, projectRoundUnit, langID, dateFrom, dateTo);
			} else if (reportPart.Type == 3) {
				g = GetGraphForReportPartTypeThreeXXX(reportPart, projectRoundUnit, langID, dateFrom, dateTo);
			} else if (reportPart.Type == 2) {
//				g = GetIndexReportPartGraph2(reportPart, projectRoundUnit, langID, dateFrom, dateTo);
				g = GetIndexReportPartGraph2(reportPart, langID, point, hasGrouping, sponsorAdmin, sponsor, departmentIDs, projectRoundUnit, grouping, groupBy, plot, dateFrom, dateTo);
			} else if (reportPart.Type == 8) {
				g = GetWeightedQuestionOptionReportPartGraph2(reportPart, langID, point, hasGrouping, sponsorAdmin, sponsor, departmentIDs, projectRoundUnit, grouping, groupBy != 0 ? groupBy : GroupBy.TwoWeeksStartWithOdd, plot, dateFrom, dateTo);
			}
			return g;
		}
		
		Dictionary<string, List<IAnswer>> GetWeeks(int minDT, int maxDT, int groupBy)
		{
			int j = 0;
			var weeks = new Dictionary<string, List<IAnswer>>();
			for (int i = minDT; i <= maxDT; i++) {
				j++;
				string w = GetBottomString(groupBy, i, j, "");
				if (!weeks.ContainsKey(w)) {
					weeks.Add(w, new List<IAnswer>());
				}
			}
			return weeks;
		}
		
		public override void CreateGraphForExcelWriter(ReportPart rp, int langID, int projectRoundUnitID, int yearFrom, int yearTo, int GB, bool hasGrouping, int plot, int grouping, int sponsorAdminID, int sponsorID, string departmentIDs, ExcelWriter writer, ref int index, int sponsorMinUserCountToDisclose, int monthFrom, int monthTo)
		{
			int cx = rp.Components.Capacity;
			string sortString = "";
			int minDT = 0;
			int maxDT = 0;
			ProjectRoundUnit roundUnit = projectRepo.ReadRoundUnit(projectRoundUnitID);
			if (roundUnit != null) {
				sortString = roundUnit.SortString;
				if (langID == 0) {
					langID = roundUnit.Language.Id;
				}
			}
			var weeks = new Dictionary<string, List<IAnswer>>();
			var departments = new List<IDepartment>();
			
			LanguageFactory.SetCurrentCulture(langID);
			
			if (rp.Type == 1) {
				decimal tot = answerRepo.CountByDate(yearFrom, yearTo, sortString, monthFrom, monthTo);
				
				if (rp.RequiredAnswerCount > Convert.ToInt32(tot)) {
				} else {
					foreach (OptionComponents c in optionRepo.FindComponentsByLanguage(rp.Option.Id, langID)) {
						int x = answerRepo.CountByValueWithDateOptionAndQuestion(c.Component.Id, yearFrom, yearTo, rp.Option.Id, rp.Question.Id, sortString, monthFrom, monthTo);
					}
					cx = optionRepo.CountByOption(rp.Option.Id);
				}
			} else if (rp.Type == 3) {
				foreach (ReportPartComponent c in reportRepo.FindComponents(rp.Id)) {
					SortedList all = new SortedList();
					
					foreach (ProjectRoundUnit u in projectRepo.FindRoundUnitsBySortString(sortString)) {
						res = new System.Collections.Hashtable();
						
						if (c.Index.Parts.Capacity == 0) {
							GetIdxVal(c.Index.Id, u.SortString, langID, yearFrom, yearTo, monthFrom, monthTo);
						} else {
							GetOtherIdxVal(c.Index.Id, u.SortString, langID, yearFrom, yearTo, monthFrom, monthTo);
						}
						
						if (all.Contains(lastVal)) {
							all[lastVal] += "," + u.TreeString;
						} else {
							all.Add(lastVal, u.TreeString);
						}
					}
					
					for (int i = all.Count - 1; i >= 0; i--) {
						int color = c.Index.GetColor(Convert.ToInt32(all.GetKey(i)));
						string[] u = all.GetByIndex(i).ToString().Split(',');
						
						foreach (string s in u) {
						}
					}
				}
			} else if (rp.Type == 2) {
				foreach (ReportPartComponent c in reportRepo.FindComponents(rp.Id)) {
					if (c.Index.Parts.Capacity == 0) {
						GetIdxVal(c.Index.Id, sortString, langID, yearFrom, yearTo, monthFrom, monthTo);
					} else {
						GetOtherIdxVal(c.Index.Id, sortString, langID, yearFrom, yearTo, monthFrom, monthTo);
					}
					int color = c.Index.GetColor(lastVal);
				}
			} else if (rp.Type == 8) {
				if (GB == 0) {
					GB = 2;
				}
				
				string groupBy = GroupFactory.GetGroupBy(GB);
				
				if (plot == PlotType.BoxPlotMinMax) {
				} else {
				}
				Answer answer = answerRepo.ReadByGroup(groupBy, yearFrom, yearTo, sortString, monthFrom, monthTo);
				if (answer != null) {
					cx = answer.DummyValue1 + 3;
					minDT = answer.DummyValue2;
					maxDT = answer.DummyValue3;
				}
				
				cx = 0;
				
				weeks = GetWeeks(minDT, maxDT, GB);
				
				if (hasGrouping) {
					int count = 0;
					Dictionary<string, string> desc = new Dictionary<string, string>();
					Dictionary<string, string> join = new Dictionary<string, string>();
					List<string> item = new List<string>();
					Dictionary<string, int> mins = new Dictionary<string, int>();
					string extraDesc = "";
					
					count = GroupFactory.GetCount(grouping, sponsorAdminID, sponsorID, projectRoundUnitID, departmentIDs, ref extraDesc, desc, join, item, mins, departmentRepo, questionRepo, sponsorMinUserCountToDisclose);
					
					ReportPartComponent c = reportRepo.ReadComponentByPartAndLanguage(rp.Id, langID);
					if (c != null) {
						int bx = 0;
						foreach(string i in item) {
							cx = 1;
							int lastDT = minDT - 1;
							var answers = answerRepo.FindByQuestionAndOptionJoinedAndGrouped2(join[i].ToString(), groupBy, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, yearFrom, yearTo, monthFrom, monthTo);
							departments.Add(new Department { Name = (string)desc[i] });
							foreach (var a in answers) {
								if (a.DT < minDT) {
									continue;
								}
								while (lastDT + 1 < a.DT) {
									lastDT++;
									cx++;
								}
								if (a.Values.Count >= mins[i]) {
									if (count == 1) {
									}
									weeks[GetBottomString(GB, a.DT, cx, "")].Add(a);
								}
								lastDT = a.DT;
								cx++;
							}
							bx++;
						}
					}
				} else {
					int bx = 0;
					foreach (ReportPartComponent c in reportRepo.FindComponentsByPartAndLanguage2(rp.Id, langID)) {
						cx = 1;
						int lastDT = minDT - 1;
						var answers = answerRepo.FindByQuestionAndOptionGrouped(groupBy, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, yearFrom, yearTo, sortString, monthFrom, monthTo);
						foreach (Answer a in answers) {
							if (a.DT < minDT) {
								continue;
							}
							while (lastDT + 1 < a.DT) {
								lastDT++;
								cx++;
							}
							
							if (a.CountV >= rp.RequiredAnswerCount) {
							}
							lastDT = a.DT;
							cx++;
						}
						bx++;
					}
				}
			}
			
			var plotter = GetPlotter(plot);
			plotter.ForMerge += delegate(object sender, MergeEventArgs e) { OnForMerge(e); };
			plotter.CellWrite += delegate(object sender, ExcelCellEventArgs e) { OnCellWrite(e); };
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
		
		public void CreateSuperGraphForExcelWriter(string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, int rpid, string yearFrom, string yearTo, string r1, string r2, int langID, ref int index, int plot, ExcelWriter writer)
		{
			// For min/max
			string rnds = (rnds1 == "0" || rnds2 == "0" ? "" : " AND pru.ProjectRoundUnitID IN (" + rnds1 + (rnds2 != "" ? "," + rnds2 : "") + ")");

			if (rnds1 == "0") {
				rnds1 = " AND pru.ProjectRoundUnitID NOT IN (" + n + ")";
			} else {
				if (rnds1 != "") {
					rnds1 = " AND (pru.ProjectRoundUnitID IN (" + rnds1 + ")";
					if (pid1 != "") {
						rnds1 += " OR a.ProjectRoundUserID IN (" + pid1 + ")";
					}
					rnds1 += ")";
				} else {
					rnds1 = " AND a.ProjectRoundUserID IN (" + pid1 + ")";
				}
			}
			if (rnds2 == "") {
				rnds2 = "";
			} else {
				if (rnds2 == "0") {
					rnds2 = " AND pru.ProjectRoundUnitID NOT IN (" + n + ")";
				} else {
					if (rnds2 != "") {
						rnds2 = " AND (pru.ProjectRoundUnitID IN (" + rnds2 + ")";
						if (pid2 != "") {
							rnds2 += " OR a.ProjectRoundUserID IN (" + pid2 + ")";
						}
						rnds2 += ")";
					} else {
						rnds2 = " AND a.ProjectRoundUserID IN (" + pid2 + ")";
					}
				}
			}

			string join1 = "";
			if (rndsd1 != "") {
				foreach (var d in departmentRepo.FindIn(rndsd1)) {
					join1 += string.Format(
						@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString,{0}) = '{1}' ",
						d.SortString.Length,
						d.SortString
					);
				}
			}
			string join2 = "";
			if (rndsd2 != "") {
				foreach (var d in departmentRepo.FindIn(rndsd2)) {
					join2 += string.Format(
						@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString,{0}) = '{1}' ",
						d.SortString.Length,
						d.SortString
					);
				}
			}

			int cx = 0;
			int type = 0;
			int pl = 0;

			int GB = 3;
			bool stdev = (rnds2 == "");
			string groupBy = "";

			var r = reportRepo.ReadReportPart(rpid);
			if (r != null) {
				type = r.Type;
				cx = r.Components.Capacity;
				pl = r.PartLevel;
			}

			int minDT = 0;
			int maxDT = 0;
			
			var weeks = new Dictionary<string, List<IAnswer>>();
			var departments = new List<IDepartment>();
			
			if (type == 8) {
				groupBy = GroupFactory.GetGroupBy(GB);

				Answer answer = answerRepo.ReadByGroup(groupBy, yearFrom, yearTo, rnds);
				if (answer != null) {
					cx = answer.DummyValue1 + 3;
					minDT = answer.DummyValue2;
					maxDT = answer.DummyValue3;
				}
				
				weeks = GetWeeks(minDT, maxDT, GB);
				
				List<IIndex> indexes = new List<IIndex>();
				List<IMinMax> minMaxes = new List<IMinMax>();
				foreach (var p in reportRepo.FindComponentsByPart(rpid)) {
					Answer a = answerRepo.ReadMinMax(groupBy, p.WeightedQuestionOption.Question.Id, p.WeightedQuestionOption.Option.Id, yearFrom, yearTo, rnds);
					if (a != null) {
						minMaxes.Add(a);
					} else {
						minMaxes.Add(new Answer());
					}
					indexes.Add(p.WeightedQuestionOption);
				}
			} else {
				cx += 2;
			}
			
			cx = 0;

			if (type == 8) {
				int bx = 0;
				var p = reportRepo.ReadComponentByPartAndLanguage(rpid, langID);
				if (p != null) {
					Series s1 = new Series { Description = r1, Color = 4, X = 300, Y = 20 };
					
					departments.Add(new Department { Name = r1 });
					
					cx = 1;
					int lastDT = minDT - 1;
					
					foreach (var a in answerRepo.FindByQuestionAndOptionGrouped4(groupBy, p.WeightedQuestionOption.Question.Id, p.WeightedQuestionOption.Option.Id, join1, yearFrom, yearTo, rnds1)) {
						while (lastDT + 1 < a.DT) {
							lastDT++;
							cx++;
						}
						if (a.Values.Count >= r.RequiredAnswerCount) {
							s1.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
							weeks[GetBottomString(GB, a.DT, cx, "")].Add(a);
						}
						lastDT = a.DT;
						cx++;
					}

					if (rnds2 != "") {
						Series s2 = new Series { Description = r2, Color = 5, X = 600, Y = 20 };
						
						departments.Add(new Department { Name = r2 });

						cx = 1;
						lastDT = minDT - 1;
						foreach (var a in answerRepo.FindByQuestionAndOptionGrouped4(groupBy, p.WeightedQuestionOption.Question.Id, p.WeightedQuestionOption.Option.Id, join2, yearFrom, yearTo, rnds2)) {
							while (lastDT + 1 < a.DT) {
								lastDT++;
								cx++;
							}
							if (a.Values.Count >= r.RequiredAnswerCount) {
								s2.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
								weeks[GetBottomString(GB, a.DT, cx, "")].Add(a);
							}
							lastDT = a.DT;
							cx++;
						}
					}
					bx++;
				}
			}
			
			var plotter = GetPlotter(plot);
			plotter.ForMerge += delegate(object sender, MergeEventArgs e) { OnForMerge(e); };
			plotter.CellWrite += delegate(object sender, ExcelCellEventArgs e) { OnCellWrite(e); };
			plotter.ToExcel(departments, weeks, writer, ref index);
		}
		
		void GetIdxVal(int indexID, string sortString, int langID, int yearFrom, int yearTo, int monthFrom, int monthTo)
		{
			foreach (Index i in indexRepo.FindByLanguage(indexID, langID, yearFrom, yearTo, sortString, monthFrom, monthTo)) {
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
		
		void GetIdxVal2(int indexID, string sortString, int langID, int yearFrom, int yearTo, int monthFrom, int monthTo)
		{
//			foreach (Index i in indexRepo.FindByLanguage2(indexID, langID, yearFrom, yearTo, sortString, monthFrom, monthTo)) {
			foreach (Index i in indexRepo.FindByLanguage(indexID, langID, yearFrom, yearTo, sortString, monthFrom, monthTo)) {
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
			Index index = indexRepo.ReadByIdAndLanguage(idx, langID);
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
