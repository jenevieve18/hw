using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class SuperReportImage : System.Web.UI.Page
	{
		SqlDepartmentRepository departmentRepository = new SqlDepartmentRepository();
		SqlReportRepository reportRepository = new SqlReportRepository();
		SqlAnswerRepository answerRepository = new SqlAnswerRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			Response.ContentType = "image/gif";
			
			int langID = ConvertHelper.ToInt32(Session["lid"], 2);
			
			var g = CreateGraph(
				GetGraphType(ConvertHelper.ToInt32(Request.QueryString["PLOT"])),
				StrHelper.Str3(Request.QueryString["RNDS1"], ""),
				StrHelper.Str3(Request.QueryString["RNDS2"], ""),
				StrHelper.Str3(Request.QueryString["RNDSD1"], ""),
				StrHelper.Str3(Request.QueryString["RNDSD2"], ""),
				StrHelper.Str3(Request.QueryString["PID1"], ""),
				StrHelper.Str3(Request.QueryString["PID2"], ""),
				StrHelper.Str3(Request.QueryString["N"], ""),
				Convert.ToInt32(Request.QueryString["RPID"]),
				Request.QueryString["FDT"],
				Request.QueryString["TDT"],
				StrHelper.Str3(Request.QueryString["R1"], ""),
				StrHelper.Str3(Request.QueryString["R2"], ""),
				Request.QueryString["DISABLED"],
				langID
			);
			g.render();
		}
		
		IGraphType GetGraphType(int plot)
		{
			int t = 2;
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
		
		public ExtendedGraph CreateGraph(IGraphType gt, string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, int rpid, string yearFrom, string yearTo, string r1, string r2, string disabled, int langID)
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
				foreach (var d in departmentRepository.FindIn(rndsd1)) {
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
				foreach (var d in departmentRepository.FindIn(rndsd2)) {
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
			int q = 0;
			int o = 0;
			int rac = 0;
			int pl = 0;
			ExtendedGraph g;

//			int steps = 0;
			int GB = 3;
			bool stdev = (rnds2 == "");
			string groupBy = "";

			var r = reportRepository.ReadReportPart(rpid);
			if (r != null) {
				type = r.Type;
				cx = r.Components.Capacity;
				q = r.Question.Id;
				o = r.Option.Id;
				rac = r.RequiredAnswerCount;
				pl = r.PartLevel;
			}

			#region group stats

//			int langID = 1;
			int minDT = 0;
			int maxDT = 0;

			if (type == 8) {
				groupBy = GroupFactory.GetGroupBy(GB);
				g = new ExtendedGraph(895, 440, "#FFFFFF");

				Answer answer = answerRepository.ReadByGroup(groupBy, yearFrom, yearTo, rnds);
				if (answer != null) {
					cx = answer.DummyValue1 + 3;
					minDT = answer.DummyValue2;
					maxDT = answer.DummyValue3;
				}
				
				List<IIndex> indexes = new List<IIndex>();
				List<IMinMax> minMaxes = new List<IMinMax>();
				foreach (var p in reportRepository.FindComponentsByPart(rpid)) {
					Answer a = answerRepository.ReadMinMax(groupBy, p.WeightedQuestionOption.Question.Id, p.WeightedQuestionOption.Option.Id, yearFrom, yearTo, rnds);
					if (a != null) {
						minMaxes.Add(a);
					} else {
						minMaxes.Add(new Answer());
					}
					indexes.Add(p.WeightedQuestionOption);
				}
				g.SetMinMaxes(minMaxes);
				g.DrawBackgroundFromIndexes(indexes);

//				foreach (var p in reportRepository.FindComponentsByPart(ConvertHelper.ToInt32(Request.QueryString["RPID"]))) {
//					Answer a = answerRepository.ReadMinMax(groupBy, p.WeightedQuestionOption.Question.Id, p.WeightedQuestionOption.Option.Id, Request.QueryString["FDT"], Request.QueryString["TDT"], rnds);
//					if (a != null) {
//						g.setMinMax((float)Convert.ToDouble(a.Min), (float)Convert.ToDouble(a.Max));
//						g.roundMinMax();
//					} else {
//						g.setMinMax(0f, 100f);
//					}
//
//					if (p.WeightedQuestionOption.YellowLow > 0) {
//						g.drawBgFromString(g.minVal, Math.Min(g.maxVal, (float)Convert.ToDouble(p.WeightedQuestionOption.YellowLow)), "FFA8A8");                             // red
//					}
//					if (p.WeightedQuestionOption.YellowLow < 100 && p.WeightedQuestionOption.GreenLow > 0) {
//						g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(p.WeightedQuestionOption.YellowLow)), Math.Min(g.maxVal, (float)Convert.ToDouble(p.WeightedQuestionOption.GreenLow)), "FFFEBE");    // yellow
//					}
//					if (p.WeightedQuestionOption.GreenLow < 100 && p.WeightedQuestionOption.GreenHigh > 0) {
//						g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(p.WeightedQuestionOption.GreenLow)), Math.Min(g.maxVal, (float)Convert.ToDouble(p.WeightedQuestionOption.GreenHigh)), "CCFFBB");   // green
//					}
//					if (p.WeightedQuestionOption.GreenHigh < 100 && p.WeightedQuestionOption.YellowHigh > 0) {
//						g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(p.WeightedQuestionOption.GreenHigh)), Math.Min(g.maxVal, (float)Convert.ToDouble(p.WeightedQuestionOption.YellowHigh)), "FFFEBE"); // yellow
//					}
//					if (p.WeightedQuestionOption.YellowHigh < 100) {
//						g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(p.WeightedQuestionOption.YellowHigh)), g.maxVal, "FFA8A8");                           // red
//					}
//				}

				g.DrawWiggle();
			} else {
				g = new ExtendedGraph(895, 550, "#FFFFFF");
				g.setMinMax(0f, 100f);

				cx += 2;
			}

//			steps = cx;
//			g.computeSteping(steps);
//			g.drawOutlines(11);
//			g.drawAxis();
			
//			g.Type = new BoxPlotMinMaxGraphType();
//			g.Type = new LineGraphType(1, 2);
			g.Type = gt;
			
			g.DrawComputingSteps(disabled, cx);

			cx = 0;

			if (type == 8) {
				g.DrawBottomString(minDT, maxDT, GB);

				int bx = 0;
				var p = reportRepository.ReadComponentByPartAndLanguage(rpid, langID);
				if (p != null) {
					Series s1 = new Series { Description = r1, Color = 4, X = 300, Y = 20 };
					
					g.Explanations.Add(
						new Explanation {
							Description = p.WeightedQuestionOption.Languages[0].Question + ", " + LanguageFactory.GetMeanText(langID) + (stdev ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
							Color = 0,
							Right = false,
							Box = false,
							HasAxis = false
						}
					);

//					float lastVal = -1f;
//					float lastStd = -1f;
//					int lastCX = 1;
					cx = 1;
					int lastDT = minDT - 1;
					
					foreach (var a in answerRepository.FindByQuestionAndOptionGrouped4(groupBy, p.WeightedQuestionOption.Question.Id, p.WeightedQuestionOption.Option.Id, join1, yearFrom, yearTo, rnds1)) {
						while (lastDT + 1 < a.DT) {
							lastDT++;
							cx++;
						}
						if (a.Values.Count >= rac) {
							s1.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
						}
						lastDT = a.DT;
						cx++;
					}
					g.Series.Add(s1);

					if (rnds2 != "") {
						Series s2 = new Series { Description = r2, Color = 5, X = 600, Y = 20 };

//						lastVal = -1f;
//						lastStd = -1f;
//						lastCX = 1;
						cx = 1;
						lastDT = minDT - 1;
						foreach (var a in answerRepository.FindByQuestionAndOptionGrouped4(groupBy, p.WeightedQuestionOption.Question.Id, p.WeightedQuestionOption.Option.Id, join2, yearFrom, yearTo, rnds2)) {
							while (lastDT + 1 < a.DT) {
								lastDT++;
								cx++;
							}
							if (a.Values.Count >= rac) {
								s2.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
							}
							lastDT = a.DT;
							cx++;
						}
						g.Series.Add(s2);
					}
					
//					g.drawAxisExpl(p.WeightedQuestionOption.Languages[0].Question + ", " + (langID == 1 ? "medelvärde" : "mean value") + (stdev ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""), 20, false, false);
//					g.drawAxis(false);
//					g.drawColorExplBox(Request.QueryString["R1"].ToString(), 4, 300, 20);
					
//					foreach (var a in answerRepository.FindByQuestionAndOptionGrouped3(groupBy, p.WeightedQuestionOption.Question.Id, p.WeightedQuestionOption.Option.Id, join1, yearFrom, yearTo, rnds1)) {
//						while (lastDT + 1 < a.DT) {
//							lastDT++;
//							cx++;
//						}
//
//						if (a.CountV >= rac) {
//							float newVal = (a.AverageV == -1 ? -1f : (float)Convert.ToDouble(a.AverageV));
//							float newStd = (a.StandardDeviation == -1 ? -1f : (float)Convert.ToDouble(a.StandardDeviation));
//
//							if (stdev) {
//								g.drawLine(20, cx * g.steping - 10, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping + 10, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
//								g.drawLine(20, cx * g.steping, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
//								g.drawLine(20, cx * g.steping - 10, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping + 10, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
//							}
//
//							if (newVal != -1f) {
//								if (lastVal != -1f) {
//									g.drawStepLine(4, lastCX, lastVal, cx, newVal, 3);
//								}
//								if (rnds2 == "") {
//									g.drawBottomString("\n\n\n\n\n\nn=" + a.CountV.ToString(), cx, false);
//								}
//								lastCX = cx;
//							}
//							lastVal = newVal;
//							lastStd = newStd;
//
//							g.drawCircle(cx, newVal);
//						}
//						lastDT = a.DT;
//						cx++;
//					}
//
//					if (rnds2 != "") {
//						g.drawColorExplBox(r2, 5, 600, 20);
//
//						lastVal = -1f;
//						lastStd = -1f;
//						lastCX = 1;
//						cx = 1;
//						lastDT = minDT - 1;
//						foreach (var a in answerRepository.FindByQuestionAndOptionGrouped3(groupBy, p.WeightedQuestionOption.Question.Id, p.WeightedQuestionOption.Option.Id, join2, yearFrom, yearTo, rnds2)) {
//							while (lastDT + 1 < a.DT) {
//								lastDT++;
//								cx++;
//							}
//
//							if (a.CountV >= rac) {
//								float newVal = (a.AverageV == -1 ? -1f : (float)Convert.ToDouble(a.AverageV));
//
//								if (newVal != -1f) {
//									if (lastVal != -1f) {
//										g.drawStepLine(5, lastCX, lastVal, cx, newVal, 3);
//									}
//									lastCX = cx;
//								}
//								lastVal = newVal;
//
//								g.drawCircle(cx, newVal);
//							}
//							lastDT = a.DT;
//							cx++;
//						}
//					}
					bx++;
				}
			}

			#endregion
			
			g.Draw();

			// g.printCopyRight();
//			g.render();
			return g;
		}
		
//		public ExtendedGraph CreateGraph2(IGraphType gt, string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, int rpid, string yearFrom, string yearTo, string r1, string r2, string disabled, int langID)
//		{
//			var g = new ExtendedGraph(895, 440, "#FFFFFF");
//			
//			g.SetMinMaxes(minMaxes);
//			g.DrawBackgroundFromIndexes(indexes);
//			g.DrawWiggle();
//			
//			g.Type = gt;
//			
//			g.DrawComputingSteps(disabled, cx);
//			
//			g.DrawBottomString(minDT, maxDT, GB);
//			
//			g.Explanations.Add(explanations);
//			
//			g.Series.AddRange(series);
//			
//			g.Draw();
//			
//			return g;
//		}
		
		public void X(string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, int rpid, string yearFrom, string yearTo, string r1, string r2, string disabled, int langID)
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
				foreach (var d in departmentRepository.FindIn(rndsd1)) {
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
				foreach (var d in departmentRepository.FindIn(rndsd2)) {
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

			var r = reportRepository.ReadReportPart(rpid);
			if (r != null) {
				type = r.Type;
				cx = r.Components.Capacity;
				pl = r.PartLevel;
			}

			int minDT = 0;
			int maxDT = 0;

			if (type == 8) {
				groupBy = GroupFactory.GetGroupBy(GB);

				Answer answer = answerRepository.ReadByGroup(groupBy, yearFrom, yearTo, rnds);
				if (answer != null) {
					cx = answer.DummyValue1 + 3;
					minDT = answer.DummyValue2;
					maxDT = answer.DummyValue3;
				}
				
				List<IIndex> indexes = new List<IIndex>();
				List<IMinMax> minMaxes = new List<IMinMax>();
				foreach (var p in reportRepository.FindComponentsByPart(rpid)) {
					Answer a = answerRepository.ReadMinMax(groupBy, p.WeightedQuestionOption.Question.Id, p.WeightedQuestionOption.Option.Id, yearFrom, yearTo, rnds);
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
				var p = reportRepository.ReadComponentByPartAndLanguage(rpid, langID);
				if (p != null) {
					Series s1 = new Series { Description = r1, Color = 4, X = 300, Y = 20 };
					
//					g.Explanations.Add(
//						new Explanation {
//							Description = p.WeightedQuestionOption.Languages[0].Question + ", " + LanguageFactory.GetMeanText(langID) + (stdev ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
//							Color = 0,
//							Right = false,
//							Box = false,
//							HasAxis = false
//						}
//					);

					cx = 1;
					int lastDT = minDT - 1;
					
					foreach (var a in answerRepository.FindByQuestionAndOptionGrouped4(groupBy, p.WeightedQuestionOption.Question.Id, p.WeightedQuestionOption.Option.Id, join1, yearFrom, yearTo, rnds1)) {
						while (lastDT + 1 < a.DT) {
							lastDT++;
							cx++;
						}
						if (a.Values.Count >= r.RequiredAnswerCount) {
							s1.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
						}
						lastDT = a.DT;
						cx++;
					}

					if (rnds2 != "") {
						Series s2 = new Series { Description = r2, Color = 5, X = 600, Y = 20 };

						cx = 1;
						lastDT = minDT - 1;
						foreach (var a in answerRepository.FindByQuestionAndOptionGrouped4(groupBy, p.WeightedQuestionOption.Question.Id, p.WeightedQuestionOption.Option.Id, join2, yearFrom, yearTo, rnds2)) {
							while (lastDT + 1 < a.DT) {
								lastDT++;
								cx++;
							}
							if (a.Values.Count >= r.RequiredAnswerCount) {
								s2.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
							}
							lastDT = a.DT;
							cx++;
						}
					}
					bx++;
				}
			}
		}
	}
}