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
	public class GroupStatsGraphFactory : IGraphFactory
	{
		SqlProjectRepository projectRepository;
		SqlAnswerRepository answerRepository;
		SqlOptionRepository optionRepository;
		SqlReportRepository reportRepository;
		SqlIndexRepository indexRepository;
		SqlQuestionRepository questionRepository;
		SqlDepartmentRepository departmentRepository;
		
		int lastCount = 0;
		float lastVal = 0;
		string lastDesc = "";
		System.Collections.Hashtable res = new System.Collections.Hashtable();
		System.Collections.Hashtable cnt = new System.Collections.Hashtable();
		
		public GroupStatsGraphFactory(SqlAnswerRepository answerRepository, SqlReportRepository reportRepository, SqlProjectRepository projectRepository, SqlOptionRepository optionRepository, SqlIndexRepository indexRepository, SqlQuestionRepository questionRepository, SqlDepartmentRepository departmentRepository)
		{
			this.projectRepository = projectRepository;
			this.answerRepository = answerRepository;
			this.optionRepository = optionRepository;
			this.reportRepository = reportRepository;
			this.indexRepository = indexRepository;
			this.questionRepository = questionRepository;
			this.departmentRepository = departmentRepository;
		}
		
		public ExtendedGraph CreateGraph(string key, ReportPart p, int langID, int PRUID, int fy, int ty, int GB, bool hasGrouping, int plot, int width, int height, string bg, int GRPNG, int sponsorAdminID, int SID, string GID, object disabled, int point, int sponsorMinUserCountToDisclose, int fm, int tm)
		{
			int cx = p.Components.Capacity;
			string sortString = "";
			int minDT = 0;
			int maxDT = 0;
			ProjectRoundUnit roundUnit = projectRepository.ReadRoundUnit(PRUID);
			if (roundUnit != null) {
				sortString = roundUnit.SortString;
				if (langID == 0) {
					langID = roundUnit.Language.Id;
				}
			}
			ExtendedGraph g = null;
	
			LanguageFactory.SetCurrentCulture(langID);
	
			if (p.Type == 1) {
				decimal tot = answerRepository.CountByDate(fy, ty, sortString, fm, tm);
	
				if (p.RequiredAnswerCount > Convert.ToInt32(tot)) {
					g = new ExtendedGraph(895, 50, "#FFFFFF");
					g.drawStringInGraph("Ingen återkoppling pga otillräckligt underlag", 300, -30);
				} else {
					g = new ExtendedGraph(895, 550, "#FFFFFF");
					List<Bar> bars = new List<Bar>();
					foreach (OptionComponents c in optionRepository.FindComponentsByLanguage(p.Option.Id, langID)) {
						int x = answerRepository.CountByValueWithDateOptionAndQuestion(c.Component.Id, fy, ty, p.Option.Id, p.Question.Id, sortString, fm, tm);
						var b = new Bar {
							Description = c.Component.CurrentLanguage.Text,
							Value = Convert.ToInt32(Math.Round(Convert.ToDecimal(x) / tot * 100M, 0)),
							Color = 5
						};
						bars.Add(b);
					}
					cx = optionRepository.CountByOption(p.Option.Id);
					g.DrawBars(disabled, cx, tot, bars);
					g.drawAxisExpl(string.Format("% (n = {0})", tot), 5, false, false);
				}
			} else if (p.Type == 3) {
				g = new ExtendedGraph(895, 550, "#FFFFFF");
				List<Bar> bars = new List<Bar>();
				List<int> referenceLines = new List<int>();
				
				foreach (ReportPartComponent c in reportRepository.FindComponents(p.Id)) {
					System.Collections.SortedList all = new System.Collections.SortedList();
	
					foreach (ProjectRoundUnit u in projectRepository.FindRoundUnitsBySortString(sortString)) {
						res = new System.Collections.Hashtable();
	
						if (c.Index.Parts.Capacity == 0) {
							GetIdxVal(c.Index.Id, u.SortString, langID, fy, ty, fm, tm);
						} else {
							GetOtherIdxVal(c.Index.Id, u.SortString, langID, fy, ty, fm, tm);
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
					referenceLines.Add(c.Index.TargetValue);
				}
				g.DrawBars(disabled, cx, bars, referenceLines);
				g.drawAxisExpl("poäng", 0, false, false);
			} else if (p.Type == 2) {
				g = new ExtendedGraph(895, 550, "#FFFFFF");
				List<Bar> bars = new List<Bar>();
				foreach (ReportPartComponent c in reportRepository.FindComponents(p.Id)) {
					if (c.Index.Parts.Capacity == 0) {
						GetIdxVal(c.Index.Id, sortString, langID, fy, ty, fm, tm);
					} else {
						GetOtherIdxVal(c.Index.Id, sortString, langID, fy, ty, fm, tm);
					}
					int color = c.Index.GetColor(lastVal);
					bars.Add(new Bar { Value = lastVal, Color = color, Description = lastDesc, Reference = c.Index.TargetValue });
				}
				g.DrawBars(disabled, cx, bars);
				g.drawAxisExpl("poäng", 0, false, false);
				g.drawReference(780, 25, " = riktvärde");
			} else if (p.Type == 8) {
				if (GB == 0) {
					GB = 2;
				}
				
				string groupBy = GroupFactory.GetGroupBy(GB);
				g = new ExtendedGraph(895, 440, "#FFFFFF");
	
				int t = 2;
				if (plot == PlotType.BoxPlotMinMax) {
					g.Type = new BoxPlotMinMaxGraphType();
				} else if (plot == PlotType.BoxPlot) {
					g.Type = new BoxPlotGraphType();
				} else if (plot == PlotType.LineSDWithCI) {
					g.Type = new LineGraphType(2, t);
				} else if (plot == PlotType.LineSD) {
					g.Type = new LineGraphType(1, t);
				} else {
					g.Type = new LineGraphType(0, t);
				}
				Answer answer = answerRepository.ReadByGroup(groupBy, fy, ty, sortString, fm, tm);
				if (answer != null) {
					cx = answer.DummyValue1 + 3;
					minDT = answer.DummyValue2;
					maxDT = answer.DummyValue3;
				}
	
				List<IIndex> indexes = new List<IIndex>();
				List<IMinMax> minMaxes = new List<IMinMax>();
				foreach (ReportPartComponent c in reportRepository.FindComponentsByPart(p.Id)) {
					if (!hasGrouping) {
						Answer a = answerRepository.ReadMinMax(groupBy, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, fy, ty, sortString, fm, tm);
						if (a != null) {
							minMaxes.Add(a);
						} else {
							minMaxes.Add(new Answer());
						}
					} else {
						minMaxes.Add(new Answer());
					}
					indexes.Add(c.WeightedQuestionOption);
				}
				g.SetMinMaxes(minMaxes);
				g.DrawBackgroundFromIndexes(indexes);
	//				g.DrawBackgroundFromIndexes2(indexes);
				g.DrawComputingSteps(disabled, cx);
	
				cx = 0;
				
				g.DrawBottomString(minDT, maxDT, GB);
				
				List<IExplanation> explanationBoxes = new List<IExplanation>();
				
				if (hasGrouping) {
					int count = 0;
					Dictionary<string, string> desc = new Dictionary<string, string>();
					Dictionary<string, string> join =  new Dictionary<string, string>();
					List<string> item = new List<string>();
					Dictionary<string, int> mins = new Dictionary<string, int>();
					string extraDesc = "";
					
					count = GroupFactory.GetCount(GRPNG, sponsorAdminID, SID, PRUID, GID, ref extraDesc, desc, join, item, mins, departmentRepository, questionRepository, sponsorMinUserCountToDisclose);
					
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
					ReportPartComponent c = reportRepository.ReadComponentByPartAndLanguage(p.Id, langID);
					if (c != null) {
						int bx = 0;
						foreach(string i in item) {
							explanationBoxes.Add(
								new Explanation {
									Description = (string)desc[i],
									Color = bx + 4,
									X = 130 + (int)((bx % breaker) * itemWidth),
									Y = 20 + (int)Math.Floor((double)bx / breaker) * 15
								}
							);
							cx = 1;
							int lastDT = minDT - 1;
							var answers = answerRepository.FindByQuestionAndOptionJoinedAndGrouped2(join[i].ToString(), groupBy, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, fy, ty, fm, tm);
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
										string v = GetBottomString(GB, a.DT, cx, (count == 1 ? ", n = " + a.Values.Count : ""));
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
						cx = 1;
						int lastDT = minDT - 1;
						Series s = new Series { Color = bx + 4 };
						var answers = answerRepository.FindByQuestionAndOptionGrouped(groupBy, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, fy, ty, sortString, fm, tm);
						foreach (Answer a in answers) {
							if (a.DT < minDT) {
								continue;
							}
							while (lastDT + 1 < a.DT) {
								lastDT++;
								cx++;
							}
	
							if (a.CountV >= p.RequiredAnswerCount) {
								string v = GetBottomString(GB, a.DT, cx, ", n = " + a.CountV);
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
			}
			return g;
		}
		
		public string CreateGraph2(string key, ReportPart p, int langID, int PRUID, int fy, int ty, int GB, bool hasGrouping, int plot, int GRPNG, int sponsorAdminID, int SID, string GID, object disabled, int sponsorMinUserCountToDisclose, int fm, int tm)
		{
			int cx = p.Components.Capacity;
			string sortString = "";
			int minDT = 0;
			int maxDT = 0;
			ProjectRoundUnit roundUnit = projectRepository.ReadRoundUnit(PRUID);
			if (roundUnit != null) {
				sortString = roundUnit.SortString;
				if (langID == 0) {
					langID = roundUnit.Language.Id;
				}
			}
			Dictionary<string, List<Answer>> weeks = new Dictionary<string, List<Answer>>();
			List<Answer> week =  new List<Answer>();
			List<Department> departments = new List<Department>();
	
			LanguageFactory.SetCurrentCulture(langID);
	
			if (p.Type == 1) {
				decimal tot = answerRepository.CountByDate(fy, ty, sortString, fm, tm);
	
				if (p.RequiredAnswerCount > Convert.ToInt32(tot)) {
	//					g = new ExtendedGraph(895, 50, "#FFFFFF");
	//					g.drawStringInGraph("Ingen återkoppling pga otillräckligt underlag", 300, -30);
				} else {
	//					g = new ExtendedGraph(895, 550, "#FFFFFF");
	//					List<Bar> bars = new List<Bar>();
					foreach (OptionComponents c in optionRepository.FindComponentsByLanguage(p.Option.Id, langID)) {
						int x = answerRepository.CountByValueWithDateOptionAndQuestion(c.Component.Id, fy, ty, p.Option.Id, p.Question.Id, sortString, fm, tm);
	//						var b = new Bar {
	//							Description = c.Text,
	//							Value = Convert.ToInt32(Math.Round(Convert.ToDecimal(x) / tot * 100M, 0)),
	//							Color = 5
	//						};
	//						bars.Add(b);
					}
					cx = optionRepository.CountByOption(p.Option.Id);
	//					g.DrawBars(disabled, cx, tot, bars);
	//					g.drawAxisExpl(string.Format("% (n = {0})", tot), 5, false, false);
				}
			} else if (p.Type == 3) {
	//				g = new ExtendedGraph(895, 550, "#FFFFFF");
	//				List<Bar> bars = new List<Bar>();
	//				List<int> referenceLines = new List<int>();
				
				foreach (ReportPartComponent c in reportRepository.FindComponents(p.Id)) {
					SortedList all = new SortedList();
	
					foreach (ProjectRoundUnit u in projectRepository.FindRoundUnitsBySortString(sortString)) {
						res = new System.Collections.Hashtable();
	
						if (c.Index.Parts.Capacity == 0) {
							GetIdxVal(c.Index.Id, u.SortString, langID, fy, ty, fm, tm);
						} else {
							GetOtherIdxVal(c.Index.Id, u.SortString, langID, fy, ty, fm, tm);
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
	//							bars.Add(new Bar { Color = color, Value = Convert.ToInt32(all.GetKey(i)), Description = s });
						}
					}
	//					referenceLines.Add(c.Index.TargetValue);
				}
	//				g.DrawBars(disabled, cx, bars, referenceLines);
	//				g.drawAxisExpl("poäng", 0, false, false);
			} else if (p.Type == 2) {
	//				g = new ExtendedGraph(895, 550, "#FFFFFF");
	//				List<Bar> bars = new List<Bar>();
				foreach (ReportPartComponent c in reportRepository.FindComponents(p.Id)) {
					if (c.Index.Parts.Capacity == 0) {
						GetIdxVal(c.Index.Id, sortString, langID, fy, ty, fm, tm);
					} else {
						GetOtherIdxVal(c.Index.Id, sortString, langID, fy, ty, fm, tm);
					}
					int color = c.Index.GetColor(lastVal);
	//					bars.Add(new Bar { Value = lastVal, Color = color, Description = lastDesc, Reference = c.Index.TargetValue });
				}
	//				g.DrawBars(disabled, cx, bars);
	//				g.drawAxisExpl("poäng", 0, false, false);
	//				g.drawReference(780, 25, " = riktvärde");
			} else if (p.Type == 8) {
				if (GB == 0) {
					GB = 2;
				}
				
				string groupBy = GroupFactory.GetGroupBy(GB);
	//				g = new ExtendedGraph(895, 440, "#FFFFFF");
	
	//				int t = 2 + (!stdev ? 1 : 0);
	//				if (plot == "BOXPLOT") {
				if (plot == PlotType.BoxPlotMinMax) {
	//					g.Type = new BoxPlotGraphType();
				} else {
	//					g.Type = new LineGraphType(stdev, t);
				}
				Answer answer = answerRepository.ReadByGroup(groupBy, fy, ty, sortString, fm, tm);
				if (answer != null) {
					cx = answer.DummyValue1 + 3;
					minDT = answer.DummyValue2;
					maxDT = answer.DummyValue3;
				}
	
	//				List<IIndex> indexes = new List<IIndex>();
	//				List<IMinMax> minMaxes = new List<IMinMax>();
	//				foreach (ReportPartComponent c in reportRepository.FindComponentsByPart(rpid)) {
	//					if (!hasGrouping) {
	//						Answer a = answerRepository.ReadMinMax(groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty, sortString);
	//						if (a != null) {
	//							minMaxes.Add(a);
	//						} else {
	//							minMaxes.Add(new Answer());
	//						}
	//					} else {
	//						minMaxes.Add(new Answer());
	//					}
	//					indexes.Add(c.QuestionOption);
	//				}
	//				g.SetMinMaxes(minMaxes);
	//				g.DrawBackgroundFromIndexes2(indexes);
	//				g.DrawComputingSteps(disabled, cx);
	
				cx = 0;
				
				weeks = GetWeeks(minDT, maxDT, GB);
				
	//				g.DrawBottomString(minDT, maxDT, GB);
	//
	//				List<IExplanation> explanationBoxes = new List<IExplanation>();
				
				if (hasGrouping) {
					int count = 0;
					Dictionary<string, string> desc = new Dictionary<string, string>();
					Dictionary<string, string> join = new Dictionary<string, string>();
					List<string> item = new List<string>();
					Dictionary<string, int> mins = new Dictionary<string, int>();
					string extraDesc = "";
					
					count = GroupFactory.GetCount(GRPNG, sponsorAdminID, SID, PRUID, GID, ref extraDesc, desc, join, item, mins, departmentRepository, questionRepository, sponsorMinUserCountToDisclose);
					
	//					int breaker = 6, itemWidth = 120;
	//					if (COUNT < 6) {
	//						breaker = 4;
	//						itemWidth = 180;
	//					}
	//					if (COUNT < 4) {
	//						breaker = 3;
	//						itemWidth = 240;
	//					}
	//
	//					g.Explanations.Add(
	//						new Explanation {
	//							Description = (extraDesc != "" ? extraDesc + "\n" : "") + LanguageFactory.GetMeanText(langID) + (stdev ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
	//							Color = 0,
	//							Right = false,
	//							Box = false,
	//							HasAxis = false
	//						}
	//					);
					ReportPartComponent c = reportRepository.ReadComponentByPartAndLanguage(p.Id, langID);
					if (c != null) {
						int bx = 0;
						foreach(string i in item) {
	//							explanationBoxes.Add(
	//								new Explanation {
	//									Description = (string)desc[i],
	//									Color = bx + 4,
	//									X = 130 + (int)((bx % breaker) * itemWidth),
	//									Y = 20 + (int)Math.Floor((double)bx / breaker) * 15
	//								}
	//							);
							cx = 1;
							int lastDT = minDT - 1;
							var answers = answerRepository.FindByQuestionAndOptionJoinedAndGrouped2(join[i].ToString(), groupBy, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, fy, ty, fm, tm);
	//							Series s = new Series {
	//								Description = (string)desc[i],
	//								Color = bx + 4,
	//								X = 130 + (int)((bx % breaker) * itemWidth),
	//								Y = 20 + (int)Math.Floor((double)bx / breaker) * 15
	//							};
							departments.Add(new Department { Name = (string)desc[i]});
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
	//										g.DrawBottomString(GB, a.SomeInteger, cx, (COUNT == 1 ? ", n = " + a.Values.Count : ""));
									}
	//									s.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
									weeks[GetBottomString(GB, a.DT, cx, "")].Add(a);
								}
								lastDT = a.DT;
								cx++;
							}
	//							g.Series.Add(s);
							bx++;
						}
					}
				} else {
					int bx = 0;
					foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(p.Id, langID)) {
	//						g.Explanations.Add(
	//							new Explanation {
	//								Description = c.QuestionOption.Languages[0].Question + ", " + LanguageFactory.GetMeanText(langID) + (stdev ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
	//								Color = bx + 4,
	//								Right = bx == 0 ? false : true,
	//								Box = bx == 0 ? true : false,
	//								HasAxis = bx == 0 ? false : true
	//							}
	//						);
						cx = 1;
						int lastDT = minDT - 1;
	//						Series s = new Series { Color = bx + 4 };
						var answers = answerRepository.FindByQuestionAndOptionGrouped(groupBy, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, fy, ty, sortString, fm, tm);
						foreach (Answer a in answers) {
							if (a.DT < minDT) {
								continue;
							}
							while (lastDT + 1 < a.DT) {
								lastDT++;
								cx++;
							}
	
							if (a.CountV >= p.RequiredAnswerCount) {
	//								g.DrawBottomString(GB, a.SomeInteger, cx, ", n = " + a.CountV);
	//								s.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
							}
							lastDT = a.DT;
							cx++;
						}
	//						g.Series.Add(s);
						bx++;
					}
				}
	//				g.Draw();
			}
	//			return g;
			
	//			if (plot == "BOXPLOT") {
	//				return new BoxPlotCsv().ToCsv(departments, weeks);
	//			} else {
	//				if (point == Distribution.None) {
	//					return new LineCsv().ToCsv(departments, weeks);
	//				} else if (point == Distribution.StandardDeviation) {
	//					return new StandardDeviationLineCsv().ToCsv(departments, weeks);
	//				} else {
	//					return new ConfidenceIntervalLineCsv().ToCsv(departments, weeks);
	//				}
	//			}
			
			if (plot == PlotType.BoxPlotMinMax) {
				return new BoxPlotCsv().ToCsv(departments, weeks);
			} else if (plot == PlotType.LineSDWithCI) {
				return new ConfidenceIntervalLineCsv().ToCsv(departments, weeks);
			} else if (plot == PlotType.LineSD) {
				return new StandardDeviationLineCsv().ToCsv(departments, weeks);
			} else {
				return new LineCsv().ToCsv(departments, weeks);
			}
		}
		
		Dictionary<string, List<Answer>> GetWeeks(int minDT, int maxDT, int groupBy)
		{
			int j = 0;
			Dictionary<string, List<Answer>> weeks = new Dictionary<string, List<Answer>>();
			for (int i = minDT; i <= maxDT; i++) {
				j++;
				string w = GetBottomString(groupBy, i, j, "");
				if (!weeks.ContainsKey(w)) {
					weeks.Add(w, new List<Answer>());
				}
			}
			return weeks;
		}
		
		public void CreateGraph3(string key, ReportPart p, int langID, int PRUID, int fy, int ty, int GB, bool hasGrouping, int plot, int GRPNG, int sponsorAdminID, int SID, string GID, object disabled, ExcelWriter writer, ref int index, int sponsorMinUserCountToDisclose, int fm, int tm)
		{
			int cx = p.Components.Capacity;
			string sortString = "";
			int minDT = 0;
			int maxDT = 0;
			ProjectRoundUnit roundUnit = projectRepository.ReadRoundUnit(PRUID);
			if (roundUnit != null) {
				sortString = roundUnit.SortString;
				if (langID == 0) {
					langID = roundUnit.Language.Id;
				}
			}
			Dictionary<string, List<Answer>> weeks = new Dictionary<string, List<Answer>>();
			List<Answer> week =  new List<Answer>();
			List<Department> departments = new List<Department>();
	
			LanguageFactory.SetCurrentCulture(langID);
	
			if (p.Type == 1) {
				decimal tot = answerRepository.CountByDate(fy, ty, sortString, fm, tm);
	
				if (p.RequiredAnswerCount > Convert.ToInt32(tot)) {
				} else {
					foreach (OptionComponents c in optionRepository.FindComponentsByLanguage(p.Option.Id, langID)) {
						int x = answerRepository.CountByValueWithDateOptionAndQuestion(c.Component.Id, fy, ty, p.Option.Id, p.Question.Id, sortString, fm, tm);
					}
					cx = optionRepository.CountByOption(p.Option.Id);
				}
			} else if (p.Type == 3) {
				foreach (ReportPartComponent c in reportRepository.FindComponents(p.Id)) {
					SortedList all = new SortedList();
	
					foreach (ProjectRoundUnit u in projectRepository.FindRoundUnitsBySortString(sortString)) {
						res = new System.Collections.Hashtable();
	
						if (c.Index.Parts.Capacity == 0) {
							GetIdxVal(c.Index.Id, u.SortString, langID, fy, ty, fm, tm);
						} else {
							GetOtherIdxVal(c.Index.Id, u.SortString, langID, fy, ty, fm, tm);
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
			} else if (p.Type == 2) {
				foreach (ReportPartComponent c in reportRepository.FindComponents(p.Id)) {
					if (c.Index.Parts.Capacity == 0) {
						GetIdxVal(c.Index.Id, sortString, langID, fy, ty, fm, tm);
					} else {
						GetOtherIdxVal(c.Index.Id, sortString, langID, fy, ty, fm, tm);
					}
					int color = c.Index.GetColor(lastVal);
				}
			} else if (p.Type == 8) {
				if (GB == 0) {
					GB = 2;
				}
				
				string groupBy = GroupFactory.GetGroupBy(GB);
	
				if (plot == PlotType.BoxPlotMinMax) {
				} else {
				}
				Answer answer = answerRepository.ReadByGroup(groupBy, fy, ty, sortString, fm, tm);
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
					
					count = GroupFactory.GetCount(GRPNG, sponsorAdminID, SID, PRUID, GID, ref extraDesc, desc, join, item, mins, departmentRepository, questionRepository, sponsorMinUserCountToDisclose);
					
					ReportPartComponent c = reportRepository.ReadComponentByPartAndLanguage(p.Id, langID);
					if (c != null) {
						int bx = 0;
						foreach(string i in item) {
							cx = 1;
							int lastDT = minDT - 1;
							var answers = answerRepository.FindByQuestionAndOptionJoinedAndGrouped2(join[i].ToString(), groupBy, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, fy, ty, fm, tm);
							departments.Add(new Department { Name = (string)desc[i] });
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
					foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(p.Id, langID)) {
						cx = 1;
						int lastDT = minDT - 1;
						var answers = answerRepository.FindByQuestionAndOptionGrouped(groupBy, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, fy, ty, sortString, fm, tm);
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
			}
			
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
		
		public event EventHandler<MergeEventArgs> ForMerge;
		
		protected virtual void OnForMerge(MergeEventArgs e)
		{
			if (ForMerge != null) {
				ForMerge(this, e);
			}
		}
		
		public static string GetBottomString(int groupBy, int i, int dx, string str)
		{
			switch (groupBy) {
				case 1:
					{
						int d = i;
						int w = d % 52;
						if (w == 0) {
							w = 52;
						}
						string v = string.Format("v{0}, {1}{2}", w, d / 52, str);
						return v;
					}
				case 2:
					{
						int d = i * 2;
						int w = d % 52;
						if (w == 0) {
							w = 52;
						}
						string v = string.Format("v{0}-{1}, {2}{3}", w - 1, w, (d - ((d - 1) % 52)) / 52, str);
						return v;
					}
				case 3:
					{
						int d = i;
						int w = d % 12;
						if (w == 0) {
							w = 12;
						}
						string v = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[w - 1] + ", " + ((d - w) / 12) + str;
						return v;
					}
				case 4:
					{
						int d = i * 3;
						int w = d % 12;
						if (w == 0) {
							w = 12;
						}
						string v = string.Format("Q{0}, {1}{2}", w / 3, (d - w) / 12, str);
						return v;
					}
				case 5:
					{
						int d = i * 6;
						int w = d % 12;
						if (w == 0) {
							w = 12;
						}
						string v = string.Format("{0}/{1}{2}", (d - w) / 12, w / 6, str);
						return v;
					}
				case 6:
					{
						string v = i.ToString() + str;
						return v;
					}
				case 7:
					{
						int d = i * 2;
						int w = d % 52;
						if (w == 0) {
							w = 52;
						}
						string v = "v" + w + "-" + ((w == 52 ? 0 : w) + 1) + ", " + ((d + 1) - (d % 52)) / 52 + str;
						return v;
					}
				default:
					throw new NotSupportedException();
			}
		}
		
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