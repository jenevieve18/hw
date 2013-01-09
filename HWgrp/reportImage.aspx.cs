//	<file>
//		<license></license>
//		<owner name="Jens Pettersson" email=""/>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;

namespace HWgrp
{
	public partial class reportImage : System.Web.UI.Page
	{
		int lastCount = 0;
		float lastVal = 0;
		string lastDesc = "";
		System.Collections.Hashtable res = new System.Collections.Hashtable();
		System.Collections.Hashtable cnt = new System.Collections.Hashtable();
		
		IReportRepository reportRepository = AppContext.GetRepositoryFactory().CreateReportRepository();
		IAnswerRepository answerRepository = AppContext.GetRepositoryFactory().CreateAnswerRepository();
		IProjectRepository projectRepository = AppContext.GetRepositoryFactory().CreateProjectRepository();
		IOptionRepository optionRepository = AppContext.GetRepositoryFactory().CreateOptionRepository();
		IDepartmentRepository departmentRepository = AppContext.GetRepositoryFactory().CreateDepartmentRepository();
		IIndexRepository indexRepository = AppContext.GetRepositoryFactory().CreateIndexRepository();
		IQuestionRepository questionRepository = AppContext.GetRepositoryFactory().CreateQuestionRepository();

		void getIdxVal(int idx, string sortString, int langID)
		{
			int fy = HttpContext.Current.Request.QueryString["FY"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) : 0;
			int ty = HttpContext.Current.Request.QueryString["TY"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) : 0;
			foreach (Index i in indexRepository.FindByLanguage(idx, langID, fy, ty, sortString))
			{
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

		void getOtherIdxVal(int idx, string sortString, int langID)
		{
			float tot = 0;
			//int div = 0;
			int max = 0;
			int minCnt = Int32.MaxValue;
			Index index = indexRepository.ReadByIdAndLanguage(idx, langID);
			if (index != null)
			{
				lastDesc = index.Languages[0].IndexName;
				foreach (IndexPart p in index.Parts)
				{
					max += 100 * p.Multiple;
					if (res.Contains(p.OtherIndex.Id))
					{
						tot += (float)res[p.OtherIndex.Id] * p.Multiple;
						minCnt = Math.Min((int)cnt[p.OtherIndex.Id], minCnt);
					}
					else
					{
						getIdxVal(p.OtherIndex.Id, sortString, langID);
						tot += lastVal * p.Multiple;
						minCnt = Math.Min(lastCount, minCnt);
					}
					//div = rs.GetInt32(2);
				}
			}
			lastVal = 100 * tot / max;
			lastCount = minCnt;
		}
		
		bool HasAnswerKey {
			get { return HttpContext.Current.Request.QueryString["AK"] != null; }
		}
		
		bool HasWidth {
			get { return HttpContext.Current.Request.QueryString["W"] != null; }
		}
		
		bool HasHeight {
			get { return HttpContext.Current.Request.QueryString["H"] != null; }
		}
		
		bool HasBackground {
			get { return HttpContext.Current.Request.QueryString["BG"] != null; }
		}
		
		int Width {
			get {
				if (HasWidth) {
					return Convert.ToInt32(HttpContext.Current.Request.QueryString["W"]);
				} else {
					return 550;
				}
			}
		}
		
		int Height {
			get {
				if (HasHeight) {
					return Convert.ToInt32(HttpContext.Current.Request.QueryString["H"]);
				} else {
					return 440;
				}
			}
		}
		
		string Background {
			get {
				if (HasBackground) {
					return "#" + HttpContext.Current.Request.QueryString["BG"];
				} else {
					return "#EFEFEF";
				}
			}
		}
		
		bool HasGrouping {
//			get { return HttpContext.Current.Request.QueryString["GRPNG"] == null || HttpContext.Current.Request.QueryString["GRPNG"] == "0"; }
			get { return HttpContext.Current.Request.QueryString["GRPNG"] != null || HttpContext.Current.Request.QueryString["GRPNG"] != "0"; }
		}
		
		
		bool IsStandardDeviation {
			get { return HttpContext.Current.Request.QueryString["STDEV"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1; }
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			int cx = 0;
			int type = 0;
			int q = 0;
			int o = 0;
			int rac = 0;
			int pl = 0;
			ExtendedGraph g = null;

			int steps = 0;
			int GB = (HttpContext.Current.Request.QueryString["GB"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["GB"].ToString()) : 0);
			bool stdev = (HttpContext.Current.Request.QueryString["STDEV"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1 : false);
			string groupBy = "";
			
			int fy = HttpContext.Current.Request.QueryString["FY"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) : 0;
			int ty = HttpContext.Current.Request.QueryString["TY"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) : 0;
			
			int langID = (HttpContext.Current.Request.QueryString["LangID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["LangID"]) : 0);

			int rpid = Convert.ToInt32(HttpContext.Current.Request.QueryString["RPID"]);
			ReportPart reportPart = reportRepository.ReadReportPart(rpid);
			if (reportPart != null)
			{
				type = reportPart.Type;
				cx = reportPart.Components.Count;
				q = reportPart.Question.Id;
				o = reportPart.Option.Id;
				rac = reportPart.RequiredAnswerCount;
				pl = reportPart.PartLevel;
			}
			if (HasAnswerKey)
			{
				#region User-level

				int answerID = 0;
				int projectRoundUserID = 0;
				Answer a = answerRepository.ReadByKey(HttpContext.Current.Request.QueryString["AK"]);
				if (a != null)
				{
					answerID = a.Id;
					if (langID == 0)
					{
						langID = a.Language.Id;
					}
					projectRoundUserID = a.ProjectRoundUser.Id;
				}
				LanguageFactory.SetCurrentCulture(langID);

				g = new ExtendedGraph(Width, Height, Background);
				g.setMinMax(0f, 100f);

				if (type == 8)
				{
					int dx = answerRepository.CountByProject(projectRoundUserID, Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]), Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]));
					if (dx == 1)
					{
						type = 9;
					}
					else
					{
						cx = dx;
					}
				}
				if (type == 8)
				{
					g.computeSteping(cx);
					g.drawOutlines(11);

					int bx = 0;
					foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID))
					{
						if (bx == 0)
						{
							g.drawAxisExpl(c.QuestionOption.Languages[0].Question, bx + 4, false, true);
							g.drawAxis(false);
						}
						else
						{
							g.drawAxisExpl(c.QuestionOption.Languages[0].Question, bx + 4, true, true);
							g.drawAxis(true);
						}
						float lastVal = -1f;
						int lastCX = 0;
						cx = 0;
						foreach (Answer aa in answerRepository.FindByQuestionAndOptionWithYearSpan(c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty))
						{
							if (bx == 0)
							{
								g.drawBottomString(aa.SomeString, cx);
							}
							float newVal = aa.Average;
							if (lastVal != -1f && newVal != -1f)
							{
								g.drawStepLine(bx + 4, lastCX, lastVal, cx, newVal);
								lastCX = cx;
							}
							cx++;
							lastVal = newVal;
						}

						bx++;
					}
				}
				else if (type == 9)
				{
					#region Bars
					g.computeSteping(cx + 2);
					g.drawOutlines(11);
					g.drawAxis();

					cx = 0;

					bool hasReference = false;

					foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID))
					{
						a = answerRepository.ReadByQuestionAndOption(answerID, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id);
						if (a != null)
						{
							int color = IndexFactory.GetColor(c.QuestionOption, a.Values[0].ValueInt);
							g.drawBar(color, ++cx, a.Values[0].ValueInt);
							if (c.QuestionOption.TargetValue != 0)
							{
								hasReference = true;
								g.drawReference(cx, c.QuestionOption.TargetValue);
							}
							g.drawBottomString(c.QuestionOption.Languages[0].Question, cx, true);
						}
					}

					//g.drawAxisExpl("poäng", 0, false, false);

					if (hasReference)
					{
						g.drawReference(450, 25, " = riktvärde");
					}

					g.drawColorExplBox("Hälsosam nivå", 0, 100, 30);
					g.drawColorExplBox("Förbättringsbehov", 1, 250, 30);
					g.drawColorExplBox("Ohälsosam nivå", 2, 400, 30);

					#endregion
				}
				#endregion
			}
			else
			{
				#region group stats

				string sortString = "";
				int minDT = 0;
				int maxDT = 0;
				ProjectRoundUnit roundUnit = projectRepository.ReadRoundUnit(Convert.ToInt32(HttpContext.Current.Request.QueryString["PRUID"]));
				if (roundUnit != null)
				{
					sortString = roundUnit.SortString;
					if (langID == 0)
					{
						langID = roundUnit.Language.Id;
					}
				}

				LanguageFactory.SetCurrentCulture(langID);

				if (type == 1)
				{
					#region Question

					decimal tot = answerRepository.CountByDate(fy, ty, sortString);

					if (rac > Convert.ToInt32(tot))
					{
						g = new ExtendedGraph(895, 50, "#FFFFFF");
						g.drawStringInGraph("Ingen återkoppling pga otillräckligt underlag", 300, -30);
					}
					else
					{
						g = new ExtendedGraph(895, 550, "#FFFFFF");
						List<Bar> bars = new List<Bar>();
						foreach (OptionComponentLanguage c in optionRepository.FindComponentsByLanguage(o, langID)) {
							int x = answerRepository.CountByValueWithDateOptionAndQuestion(c.Component.Id, 2011, 2012, 1, 1, "");
							var b = new Bar {
								Description = c.Text,
								Value = Convert.ToInt32(Math.Round(Convert.ToDecimal(x) / tot * 100M, 0)),
								Color = 5
							};
							bars.Add(b);
						}
						cx = optionRepository.CountByOption(o);
						g.DrawBars(HttpContext.Current.Request.QueryString["DISABLED"], cx, tot, bars);
						g.drawAxisExpl(string.Format("% (n = {0})", tot), 5, false, false);
					}
					#endregion
				}
				else if (type == 3)
				{
					#region Benchmark
					g = new ExtendedGraph(895, 550, "#FFFFFF");
					
					List<Bar> bars = new List<Bar>();
					List<int> referenceLines = new List<int>();
					
					foreach (ReportPartComponent c in reportRepository.FindComponents(rpid)) {
						System.Collections.SortedList all = new System.Collections.SortedList();

						foreach (ProjectRoundUnit u in projectRepository.FindRoundUnitsBySortString(sortString)) {
							res = new System.Collections.Hashtable();

							if (c.Index.Parts.Count == 0) {
								getIdxVal(c.Index.Id, u.SortString, langID);
							} else {
								getOtherIdxVal(c.Index.Id, u.SortString, langID);
							}

							if (all.Contains(lastVal)) {
								all[lastVal] += "," + u.TreeString;
							} else {
								all.Add(lastVal, u.TreeString);
							}
						}

						for (int i = all.Count - 1; i >= 0; i--) {
							int color = IndexFactory.GetColor(c.Index, Convert.ToInt32(all.GetKey(i)));
							string[] u = all.GetByIndex(i).ToString().Split(',');

							foreach (string s in u) {
								bars.Add(new Bar { Color = color, Value = Convert.ToInt32(all.GetKey(i)), Description = s });
							}
						}
						referenceLines.Add(c.Index.TargetValue);
					}
					g.DrawBars(HttpContext.Current.Request.QueryString["DISABLED"], cx, bars, referenceLines);
					g.drawAxisExpl("poäng", 0, false, false);
					#endregion
				}
				else if (type == 2)
				{
					#region Index
					g = new ExtendedGraph(895, 550, "#FFFFFF");
					List<Bar> bars = new List<Bar>();
					foreach (ReportPartComponent c in reportRepository.FindComponents(rpid)) {
						if (c.Index.Parts.Count == 0) {
							getIdxVal(c.Index.Id, sortString, langID);
						} else {
							getOtherIdxVal(c.Index.Id, sortString, langID);
						}
						int color = IndexFactory.GetColor(c.Index, lastVal);
						bars.Add(new Bar { Value = lastVal, Color = color, Description = lastDesc, Reference = c.Index.TargetValue });
					}
					g.DrawBars(HttpContext.Current.Request.QueryString["DISABLED"], cx, bars);
					g.drawAxisExpl("poäng", 0, false, false);
					g.drawReference(780, 25, " = riktvärde");
					
					#endregion
				}
				else if (type == 8)
				{
					int GRPNG = Convert.ToInt32(HttpContext.Current.Request.QueryString["GRPNG"]);
					int PRUID = Convert.ToInt32(HttpContext.Current.Request.QueryString["PRUID"]);
					int SPONS = Convert.ToInt32((HttpContext.Current.Request.QueryString["SAID"] != null ? HttpContext.Current.Request.QueryString["SAID"] : HttpContext.Current.Session["SponsorAdminID"]));
					int SID = Convert.ToInt32((HttpContext.Current.Request.QueryString["SID"] != null ? HttpContext.Current.Request.QueryString["SID"] : HttpContext.Current.Session["SponsorID"]));
					string GID = (HttpContext.Current.Request.QueryString["GID"] != null ? HttpContext.Current.Request.QueryString["GID"].ToString().Replace(" ", "") : "");
					
					if (GB == 0) {
						GB = 2;
					}
					
					groupBy = GroupFactory.GetGroupBy(GB);
					g = new ExtendedGraph(895, 440, "#FFFFFF");

					Answer answer = answerRepository.ReadByGroup(groupBy, fy, ty, sortString);
					if (answer != null) {
						cx = answer.DummyValue1 + 3;
						minDT = answer.DummyValue2;
						maxDT = answer.DummyValue3;
					}

					List<IIndex> indexes = new List<IIndex>();
					List<IMinMax> minMaxes = new List<IMinMax>();
					foreach (ReportPartComponent c in reportRepository.FindComponentsByPart(rpid)) {
						if (!HasGrouping) {
							Answer a = answerRepository.ReadMinMax(groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty, sortString);
							if (a != null) {
								minMaxes.Add(a);
							} else {
								minMaxes.Add(new Answer());
							}
						} else {
							minMaxes.Add(new Answer());
						}
						indexes.Add(c.QuestionOption);
					}
					g.SetMinMaxes(minMaxes);
					g.DrawBackgroundFromIndexes(indexes);
					g.DrawComputingSteps(HttpContext.Current.Request.QueryString["DISABLED"], cx);
					g.Type = new BoxPlotGraphType();

					cx = 0;
					
					g.DrawBottomString(minDT, maxDT, GB);
					
					List<IExplanation> explanations = new List<IExplanation>();
					List<IExplanation> explanationBoxes = new List<IExplanation>();
					
					if (HasGrouping) {
						int COUNT = 0;
						Hashtable desc = new Hashtable();
						Hashtable join = new Hashtable();
						ArrayList item = new ArrayList();
						string extraDesc = "";
						
						COUNT = GroupFactory.GetCount(GRPNG, SPONS, SID, PRUID, GID, ref extraDesc, desc, join, item, departmentRepository, questionRepository);
						
						int breaker = 6, itemWidth = 120;
						if (COUNT < 6) {
							breaker = 4;
							itemWidth = 180;
						}
						if (COUNT < 4) {
							breaker = 3;
							itemWidth = 240;
						}
						
						explanations.Add(
							new Explanation {
								Description = (extraDesc != "" ? extraDesc + "\n" : "") + LanguageFactory.GetMeanText(langID) + (IsStandardDeviation ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
								Color = 0,
								Right = false,
								Box = false,
								HasAxis = false
							}
						);
						ReportPartComponent c = reportRepository.ReadComponentByPartAndLanguage(rpid, langID);
						if (c != null) {
							int bx = 0;
							cx = 1;
							foreach(string i in item) {
								explanationBoxes.Add(
									new Explanation {
										Description = (string)desc[i],
										Color = bx + 4,
										X = 130 + (int)((bx % breaker) * itemWidth),
										Y = 20 + (int)Math.Floor((double)bx / breaker) * 15
									}
								);
//						cx = 1;
								int lastDT = minDT - 1;
								Series s = new Series { Color = bx + 4 };
								foreach (var a in answerRepository.FindByQuestionAndOptionJoinedAndGrouped(join[i].ToString(), groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty)) {
									while (lastDT + 1 < a.SomeInteger) {
										lastDT++;
										cx++;
									}
//							if (a.CountV >= rac) {
//								if (COUNT == 1) {
//									g.DrawBottomString(GB, a.SomeInteger, cx, (COUNT == 1 ? ", n = " + a.CountV : ""));
//								}
									List<double> n = new List<double>();
									foreach (var v in a.Values) {
										n.Add((double)v.ValueDecimal);
									}
									HWList l = new HWList(n);
									s.Points.Add(
										new PointV {
											X = cx,
											Y = (float)l.Median,
											UpperWhisker = l.UpperWhisker,
											LowerWhisker = l.LowerWhisker,
											UpperBox = l.UpperBox,
											LowerBox = l.LowerBox
										}
									);
//							}
									lastDT = a.SomeInteger;
//							cx++;
								}
								g.Series.Add(s);
								bx++;
								cx++;
							}
						}
					} else {
						int bx = 0;
						foreach (var c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID)) {
							if (bx == 0) {
								explanations.Add(
									new Explanation {
										Description = c.QuestionOption.Languages[0].Question + ", " + LanguageFactory.GetMeanText(langID) + (IsStandardDeviation ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
										Color = bx + 4,
										Right = false,
										Box = true,
										HasAxis = false
									}
								);
							} else {
								explanations.Add(
									new Explanation {
										Description = c.QuestionOption.Languages[0].Question + ", " + LanguageFactory.GetMeanText(langID) + (IsStandardDeviation ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
										Color = bx + 4,
										Right = true,
										Box = false,
										HasAxis = true
									}
								);
							}
							cx = 1;
							int lastDT = minDT - 1;
							Series s = new Series { Color = bx + 4 };
							foreach (var a in answerRepository.FindByQuestionAndOptionGrouped(groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty, sortString)) {
								while (lastDT + 1 < a.SomeInteger) {
									lastDT++;
									cx++;
								}

								if (a.CountV >= rac) {
									g.DrawBottomString(GB, a.SomeInteger, cx, ", n = " + a.CountV);
									s.Points.Add(new PointV { X = cx, Y = a.AverageV, Deviation = a.StandardDeviation, T = 3 });
								}
								lastDT = a.SomeInteger;
								cx++;
							}
							g.Series.Add(s);
							bx++;
						}
					}
					g.DrawExplanations(explanations);
					g.DrawExplanationBoxes(explanationBoxes);
					g.Draw();
					/*if (GB == 0) {
						GB = 2;
					}
					
					groupBy = GroupFactory.GetGroupBy(GB);
					g = new ExtendedGraph(895, 440, "#FFFFFF");

					Answer answer = answerRepository.ReadByGroup(groupBy, fy, ty, sortString);
					if (answer != null) {
						cx = answer.DummyValue1 + 3;
						minDT = answer.DummyValue2;
						maxDT = answer.DummyValue3;
					}

					List<IIndex> indexes = new List<IIndex>();
					List<IMinMax> minMaxes = new List<IMinMax>();
					foreach (ReportPartComponent c in reportRepository.FindComponentsByPart(rpid)) {
						if (!HasGrouping) {
							Answer a = answerRepository.ReadMinMax(groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty, sortString);
							if (a != null) {
								minMaxes.Add(a);
							} else {
								minMaxes.Add(new Answer());
							}
						} else {
							minMaxes.Add(new Answer());
						}
						indexes.Add(c.QuestionOption);
					}
					g.SetMinMaxes(minMaxes);
					g.DrawBackgroundFromIndexes(indexes);
					g.DrawComputingSteps(HttpContext.Current.Request.QueryString["DISABLED"], cx);
					g.Type = new LineGraphType(stdev);

					cx = 0;
					
					g.DrawBottomString(minDT, maxDT, GB);
					
					List<IExplanation> explanations = new List<IExplanation>();
					List<IExplanation> explanationBoxes = new List<IExplanation>();
					
					if (HasGrouping) {
						int COUNT = 0;
						Hashtable desc = new Hashtable();
						Hashtable join = new Hashtable();
						ArrayList item = new ArrayList();
						string extraDesc = "";
						
						COUNT = GroupFactory.GetCount(GRPNG, SPONS, SID, PRUID, GID, ref extraDesc, desc, join, item, departmentRepository, questionRepository);
						
						int breaker = 6, itemWidth = 120;
						if (COUNT < 6) {
							breaker = 4;
							itemWidth = 180;
						}
						if (COUNT < 4) {
							breaker = 3;
							itemWidth = 240;
						}
						
						explanations.Add(
							new Explanation {
								Description = (extraDesc != "" ? extraDesc + "\n" : "") + LanguageFactory.GetMeanText(langID) + (IsStandardDeviation ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
								Color = 0,
								Right = false,
								Box = false,
								HasAxis = false
							}
						);
						ReportPartComponent c = reportRepository.ReadComponentByPartAndLanguage(rpid, langID);
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
								Series s = new Series { Color = bx + 4 };
								foreach (var a in answerRepository.FindByQuestionAndOptionJoinedAndGrouped(join[i].ToString(), groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty)) {
									while (lastDT + 1 < a.SomeInteger) {
										lastDT++;
										cx++;
									}
									if (a.CountV >= rac) {
										if (COUNT == 1) {
											g.DrawBottomString(GB, a.SomeInteger, cx, (COUNT == 1 ? ", n = " + a.CountV : ""));
										}
										s.Points.Add(new PointV { X = cx, Y = a.AverageV, Deviation = a.StandardDeviation, T = 2 + (!stdev ? 1 : 0) });
									}
									lastDT = a.SomeInteger;
									cx++;
								}
								g.Series.Add(s);
								bx++;
							}
						}
					} else {
						int bx = 0;
						foreach (var c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID)) {
							if (bx == 0) {
								explanations.Add(
									new Explanation {
										Description = c.QuestionOption.Languages[0].Question + ", " + LanguageFactory.GetMeanText(langID) + (IsStandardDeviation ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
										Color = bx + 4,
										Right = false,
										Box = true,
										HasAxis = false
									}
								);
							} else {
								explanations.Add(
									new Explanation {
										Description = c.QuestionOption.Languages[0].Question + ", " + LanguageFactory.GetMeanText(langID) + (IsStandardDeviation ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
										Color = bx + 4,
										Right = true,
										Box = false,
										HasAxis = true
									}
								);
							}
							cx = 1;
							int lastDT = minDT - 1;
							Series s = new Series { Color = bx + 4 };
							foreach (var a in answerRepository.FindByQuestionAndOptionGrouped(groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty, sortString)) {
								while (lastDT + 1 < a.SomeInteger) {
									lastDT++;
									cx++;
								}

								if (a.CountV >= rac) {
									g.DrawBottomString(GB, a.SomeInteger, cx, ", n = " + a.CountV);
									s.Points.Add(new PointV { X = cx, Y = a.AverageV, Deviation = a.StandardDeviation, T = 3 });
								}
								lastDT = a.SomeInteger;
								cx++;
							}
							g.Series.Add(s);
							bx++;
						}
					}
					g.DrawExplanations(explanations);
					g.DrawExplanationBoxes(explanationBoxes);
					g.Draw();*/
					
				}

				#endregion
			}

			// g.printCopyRight();
			g.render();
		}
	}
}