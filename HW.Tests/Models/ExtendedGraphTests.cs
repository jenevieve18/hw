//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using HW.Core;
using NUnit.Framework;

namespace HW.Tests.Models
{
	[TestFixture]
	public class ExtendedGraphTests
	{
		ExtendedGraph g;
		Form f;
		PictureBox p;
		OptionRepositoryStub optionRepository;
		AnswerRepositoryStub answerRepository;
		ReportRepositoryStub reportRepository;
		ProjectRepositoryStub projectRepository;
		IndexRepositoryStub indexRepository;
		DepartmentRepositoryStub departmentRepository;
		QuestionRepositoryStub questionRepository;
		
		[SetUp]
		public void Setup()
		{
			optionRepository = new OptionRepositoryStub();
			answerRepository = new AnswerRepositoryStub();
			reportRepository = new ReportRepositoryStub();
			projectRepository = new ProjectRepositoryStub();
			indexRepository = new IndexRepositoryStub();
			departmentRepository = new DepartmentRepositoryStub();
			questionRepository = new QuestionRepositoryStub();
			
			f = new Form();
			f.Size = new Size(1000, 650);
			f.KeyDown += delegate(object sender, KeyEventArgs e) {
				if (e.KeyCode == Keys.Escape) {
					f.Close();
				}
			};
			
			p = new PictureBox();
			p.Dock = DockStyle.Fill;
		}
		
		[TearDown]
		public void Teardown()
		{
			p.Image = g.objBitmap;
			f.Controls.Add(p);
			f.ShowDialog();
		}
		
		[Test]
		public void TestMinimumValue()
		{
			int steps = 10;
			
			g = new ExtendedGraph(895, 550, "#FFFFFF");
			g.setMinMax(0f, 100f);
			g.computeSteping(steps + 1 + 2);
			g.drawOutlines(11);
			g.drawAxis(new object());
			
			g.drawLine(20, 0, (int)g.maxH + 20, 0, (int)g.maxH + 23, 1);
			g.drawLine(20, 0, (int)g.maxH + 23, -5, (int)g.maxH + 26, 1);
			g.drawLine(20, -5, (int)g.maxH + 26, 5, (int)g.maxH + 32, 1);
			g.drawLine(20, 5, (int)g.maxH + 32, 0, (int)g.maxH + 35, 1);
			g.drawLine(20, 0, (int)g.maxH + 35, 0, (int)g.maxH + 38, 1);
		}
		
		[Test]
		public void TestType1()
		{
			List<Bar> bars = new List<Bar>();
			Series s = new Series { Color = 5 };
			int tot = 10;
			foreach (OptionComponentLanguage c in optionRepository.FindComponentsByLanguage(1, 1)) {
				var x = answerRepository.CountByValueWithDateOptionAndQuestion(c.Component.Id, 2011, 2012, 1, 1, "");
				var b = new Bar {
					Description = c.Text,
					Value = Convert.ToInt32(Math.Round(Convert.ToDecimal(x) / tot * 100M, 0)),
					Color = 5
				};
				bars.Add(b);
			}
			g = new ExtendedGraph(895, 550, "#FFFFFF");
			g.DrawBars(new object(), 10, tot, bars);
			g.Draw();
		}
		
		[Test]
		public void TestType1b()
		{
			Series s = new Series { Color = 5 };
			int tot = 10;
			foreach (OptionComponentLanguage c in optionRepository.FindComponentsByLanguage(1, 1)) {
				var x = answerRepository.CountByValueWithDateOptionAndQuestion(c.Component.Id, 2011, 2012, 1, 1, "");
				s.Points.Add(
					new PointV {
						Description = c.Text,
						Y = Convert.ToInt32(Math.Round(Convert.ToDecimal(x) / tot * 100M, 0))
					}
				);
			}
			g = new ExtendedGraph(895, 550, "#FFFFFF");
			g.Series.Add(s);
			g.Type = new BarGraphTYpe();
			g.Draw();
		}
		
		[Test]
		public void TestType1InsufficientEvidence()
		{
			g = new ExtendedGraph(895, 50, "#FFFFFF");
			g.drawStringInGraph("Ingen återkoppling pga otillräckligt underlag", 300, -30);
		}
		
		[Test]
		public void TestType3()
		{
			g = new ExtendedGraph(895, 550, "#FFFFFF");
			
			int rpid = 1;
			string sortString = "";
			int langID = 1;
			
			List<Bar> bars = new List<Bar>();
			List<int> referenceLines = new List<int>();
			
			foreach (ReportPartComponent c in reportRepository.FindComponents(rpid)) {
				System.Collections.SortedList all = new System.Collections.SortedList();

				foreach (ProjectRoundUnit u in projectRepository.FindRoundUnitsBySortString(sortString)) {
					res = new System.Collections.Hashtable();

					if (c.Index.Parts.Count == 0) {
						getIdxVal(c.Index.Id, u.SortString, langID, 2011, 2012);
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
//					int color = IndexFactory.GetColor(c.Index, Convert.ToInt32(all.GetKey(i)));
					int color = c.Index.GetColor(Convert.ToInt32(all.GetKey(i)));
					
					string[] u = all.GetByIndex(i).ToString().Split(',');

					foreach (string s in u) {
						bars.Add(new Bar { Color = color, Value = Convert.ToInt32(all.GetKey(i)), Description = s });
					}
				}
				referenceLines.Add(c.Index.TargetValue);
			}
			g.DrawBars(new object(), 10, bars, referenceLines);
		}
		
		float lastVal = 0;
		string lastDesc = "";
		System.Collections.Hashtable res = new System.Collections.Hashtable();
		System.Collections.Hashtable cnt = new System.Collections.Hashtable();
		int lastCount = 0;
		
		void getIdxVal(int idx, string sortString, int langID, int fy, int ty)
		{
			foreach (Index i in indexRepository.FindByLanguage(idx, langID, fy, ty, sortString)) {
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
						getIdxVal(p.OtherIndex.Id, sortString, langID, 2011, 2012);
					}
				}
			}
			lastVal = 100 * tot / max;
			lastCount = minCnt;
		}
		
		[Test]
		public void TestType8()
		{
			int steps = 10;
			int[] xValues = new int[] { 10, 40, 50, 10, 3, 45, 22 };
			string[] explanations = new string[] { "Department 0", "Department 1", "Department 2", "Department 3", "Department 4", "Department 5", "Department 6" };
			
			g = new ExtendedGraph(895, 440, "#FFFFFF");
			g.setMinMax(0f, 100f);
//			g.drawBgFromString(g.minVal, Math.Min(g.maxVal, (float)Convert.ToDouble(40)), "FFA8A8");
//			g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(40)), Math.Min(g.maxVal, (float)Convert.ToDouble(60)), "FFFEBE");
//			g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(60)), Math.Min(g.maxVal, (float)Convert.ToDouble(101)), "CCFFBB");
			g.drawBgFromString2(g.minVal, Math.Min(g.maxVal, (float)Convert.ToDouble(40)), "FFA8A8", "");
			g.drawBgFromString2(Math.Max(g.minVal, (float)Convert.ToDouble(40)), Math.Min(g.maxVal, (float)Convert.ToDouble(60)), "FFFEBE", "");
			g.drawBgFromString2(Math.Max(g.minVal, (float)Convert.ToDouble(60)), Math.Min(g.maxVal, (float)Convert.ToDouble(101)), "CCFFBB", "");
			
			g.computeSteping(steps + 1 + 2);
			g.drawOutlines(11);
			g.drawAxis(new object());
			
			g.drawAxis(false);
			g.drawAxisExpl(string.Format("% (n = {0})", 10), 5, false, false);
			
			int cx = 0;
			int breaker = 6;
			foreach (int x in xValues) {
				g.drawColorExplBox(explanations[cx], cx + 4, 130 + (int)((cx % breaker) * 120), 20 + (int)Math.Floor((double)cx / breaker) * 15);
				g.drawCircle(cx + 1, x, 4);
				g.drawStepLine(cx + 4, 1, 10, 1, 10, 2 + 0);
				cx++;
			}
		}
		
		[Test]
		public void TestType9()
		{
			g = new ExtendedGraph(895, 550, "#FFFFFF"); // TODO:
		}
		
		[Test]
		public void TestType8HasKey()
		{
			string[] explanations = new string[] { "Question 0", "Question 1" };
			string[] xLabels = new string[] { "X0", "X1", "X2", "X3", "X4" };
			int[] xValues = new int[] { 40, 50, 30, 60, 10 };
			int steps = 10;
			
			g = new ExtendedGraph(550, 440, "#FFFFFF");
			g.setMinMax(0f, 100f);
			g.computeSteping(steps);
			g.drawOutlines(11);
			
			int bx = 0;
			foreach (string s in explanations) {
				if (bx == 0) {
					g.drawAxisExpl(s, bx + 4, false, true);
					g.drawAxis(false);
				} else {
					g.drawAxisExpl(s, bx + 4, true, true);
					g.drawAxis(true);
				}
				float lastVal = -1f;
				int lastCX = 0;
				int cx = 0;
				foreach (int x in xValues) {
					if (bx == 0) {
						g.drawBottomString(xLabels[cx], cx);
					}
					float newVal = x;
					if (lastVal != -1f && newVal != -1f) {
						g.drawStepLine(bx + 4, lastCX, lastVal, cx, newVal);
						lastCX = cx;
					}
					cx++;
					lastVal = newVal;
				}
				bx++;
			}
		}
		
		[Test]
		public void TestType2()
		{
			g = new ExtendedGraph(895, 550, "#FFFFFF");
			
			int rpid = 1;
			string sortString = "";
			int langID = 1;
			
			List<Bar> bars = new List<Bar>();
			
			foreach (ReportPartComponent c in reportRepository.FindComponents(rpid)) {
				if (c.Index.Parts.Count == 0) {
					getIdxVal(c.Index.Id, sortString, langID, 2011, 2012);
				} else {
					getOtherIdxVal(c.Index.Id, sortString, langID);
				}
//				int color = IndexFactory.GetColor(c.Index, lastVal);
				int color = c.Index.GetColor(lastVal);
				Bar b = new Bar { Color = color, Description = lastDesc, Reference = c.Index.TargetValue, Value = lastVal };
				bars.Add(b);
			}
			g.DrawBars(new object(), 10, bars);
		}
		
		bool HasGrouping {
			get { return true; }
//			get { return false; }
		}
		
		bool IsStandardDeviation {
//			get { return HttpContext.Current.Request.QueryString["STDEV"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1; }
			get { return false; }
		}
		
		[Test]
		public void b()
		{
			g = new ExtendedGraph(895, 440, "#FFFFFF");
			X2(g, new object());
		}
		
		void X2(ExtendedGraph g, object disabled)
		{
			int GB = 0;
			string groupBy = "";
			int fy = 2011;
			int ty = 2012;
			int minDT = 0;
			int maxDT = 0;
			string sortString = "";
			int rpid = 1;
			int cx = 0;
//			int steps = 0;
			
			int GRPNG = 1;
			int PRUID = 1;
			int SPONS = 1;
			int SID = 1;
			string GID = "0,1";
			
			int langID = 1;
			int rac = 10;
			
			bool stdev = false;
			bool hasGrouping = true;
			
			if (GB == 0) {
				GB = 2;
			}
			
			groupBy = GroupFactory.GetGroupBy(GB);
//			var g = new Graph(895, 440, "#FFFFFF");

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
						minMaxes.Add(new Answer { Min = a.Min < 0 ? 0 : a.Min, Max = a.Max > 100 ? 100 : a.Max });
					} else {
						minMaxes.Add(new Answer { Min = 0, Max = 100 });
					}
				} else {
					minMaxes.Add(new Answer { Min = 0, Max = 100 });
				}
				indexes.Add(c.QuestionOption);
			}
			g.SetMinMaxes(minMaxes);
			g.DrawBackgroundFromIndexes(indexes);
//			g.DrawWiggle();
			g.DrawComputingSteps(disabled, cx);

			cx = 0;
			
			g.DrawBottomString(minDT, maxDT, GB);
			
			List<IExplanation> explanations = new List<IExplanation>();
			List<IExplanation> explanationBoxes = new List<IExplanation>();
			List<ILine> lines = new List<ILine>();
			List<ICircle> circles = new List<ICircle>();
			
			if (hasGrouping) {
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
				
//				explanations.Add(
//					new Explanation {
//						Description = (extraDesc != "" ? extraDesc + "\n" : "") + LanguageFactory.GetMeanText(langID) + (stdev ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
//						Color = 0,
//						Right = false,
//						Box = false,
//						HasAxis = false
//					}
//				);
				g.Explanations.Add(
					new Explanation {
						Description = (extraDesc != "" ? extraDesc + "\n" : "") + LanguageFactory.GetMeanText(langID) + (stdev ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
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
						var answers = answerRepository.FindByQuestionAndOptionJoinedAndGrouped2(join[i].ToString(), groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty);
						Series s = new Series {
							Description = (string)desc[i],
							Color = bx + 4,
							X = 130 + (int)((bx % breaker) * itemWidth),
							Y = 20 + (int)Math.Floor((double)bx / breaker) * 15
						};
						foreach (Answer a in answers) {
							while (lastDT + 1 < a.DT) {
								lastDT++;
								cx++;
							}
							if (a.Values.Count >= rac) {
								if (COUNT == 1) {
									g.DrawBottomString(GB, a.DT, cx, (COUNT == 1 ? ", n = " + a.Values.Count : ""));
								}
//								List<double> n = new List<double>();
//								foreach (var v in a.Values) {
//									n.Add((double)v.ValueInt);
//								}
//								HWList l = new HWList(n);
//								s.Points.Add(new PointV { X = cx, Y = (float)l.Mean, Deviation = (float)l.StandardDeviation, T = 2 + (!stdev ? 1 : 0) });
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
				foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID)) {
//					explanations.Add(
//						new Explanation {
//							Description = c.QuestionOption.Languages[0].Question + ", " + LanguageFactory.GetMeanText(langID) + (stdev ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
//							Color = bx + 4,
//							Right = bx == 0 ? false : true,
//							Box = bx == 0 ? true : false,
//							HasAxis = bx == 0 ? false : true
//						}
//					);
					g.Explanations.Add(
						new Explanation {
							Description = c.QuestionOption.Languages[0].Question + ", " + LanguageFactory.GetMeanText(langID) + (stdev ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
							Color = bx + 4,
							Right = bx == 0 ? false : true,
							Box = bx == 0 ? true : false,
							HasAxis = bx == 0 ? false : true
						}
					);
					cx = 1;
					int lastDT = minDT - 1;
					Series s = new Series { Color = bx + 4 };
					foreach (Answer a in answerRepository.FindByQuestionAndOptionGrouped(groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty, sortString)) {
						while (lastDT + 1 < a.DT) {
							lastDT++;
							cx++;
						}

						if (a.CountV >= rac) {
							g.DrawBottomString(GB, a.DT, cx, ", n = " + a.CountV);
//							s.Points.Add(new PointV { X = cx, Y = a.AverageV, Deviation = a.StandardDeviation, T = 3 });
							s.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
						}
						lastDT = a.DT;
						cx++;
					}
					g.Series.Add(s);
					bx++;
				}
			}
//			g.DrawExplanations(explanations);
//			g.DrawExplanationBoxes(explanationBoxes);
			g.Draw();
		}
		
		[Test]
		public void c()
		{
			g = new ExtendedGraph(895, 440, "#FFFFFF");
			X3(g, new object());
		}
		
		void X3(ExtendedGraph g, object disabled)
		{
			int GB = 0;
			string groupBy = "";
			int fy = 2011;
			int ty = 2012;
			int minDT = 0;
			int maxDT = 0;
			string sortString = "";
			int rpid = 1;
			int cx = 0;
//			int steps = 0;
			
			int GRPNG = 1;
			int PRUID = 1;
			int SPONS = 1;
			int SID = 1;
			string GID = "0,1";
			
			int langID = 1;
			int rac = 10;
			
			bool stdev = true;
			bool hasGrouping = true;
			
			if (GB == 0) {
				GB = 2;
			}
			
			groupBy = GroupFactory.GetGroupBy(GB);
//			var g = new Graph(895, 440, "#FFFFFF");

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
			g.DrawComputingSteps(disabled, cx);
			g.Type = new BoxPlotGraphType();

			cx = 0;
			
			g.DrawBottomString(minDT, maxDT, GB);
			
			List<IExplanation> explanations = new List<IExplanation>();
			List<IExplanation> explanationBoxes = new List<IExplanation>();
			
			if (hasGrouping) {
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
				
				g.Explanations.Add(
					new Explanation {
						Description = (extraDesc != "" ? extraDesc + "\n" : "") + LanguageFactory.GetMeanText(langID) + (stdev ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
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
						var answers = answerRepository.FindByQuestionAndOptionJoinedAndGrouped2(join[i].ToString(), groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty);
						Series s = new Series {
							Description = (string)desc[i],
							Color = bx + 4,
							X = 130 + (int)((bx % breaker) * itemWidth),
							Y = 20 + (int)Math.Floor((double)bx / breaker) * 15
						};
						foreach (Answer a in answers) {
							while (lastDT + 1 < a.DT) {
								lastDT++;
								cx++;
							}
							if (a.Values.Count >= rac) {
								if (COUNT == 1) {
									g.DrawBottomString(GB, a.DT, cx, (COUNT == 1 ? ", n = " + a.Values.Count : ""));
								}
//								List<double> n = new List<double>();
//								foreach (var v in a.Values) {
//									n.Add((double)v.ValueInt);
//								}
//								HWList l = new HWList(n);
//								s.Points.Add(new PointV { X = cx, Y = (float)l.Median, UpperWhisker = l.UpperWhisker, LowerWhisker = l.LowerWhisker, UpperBox = l.UpperBox, LowerBox = l.LowerBox });
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
				foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID)) {
//					explanations.Add(
//						new Explanation {
//							Description = c.QuestionOption.Languages[0].Question + ", " + LanguageFactory.GetMeanText(langID) + (stdev ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
//							Color = bx + 4,
//							Right = bx == 0 ? false : true,
//							Box = bx == 0 ? true : false,
//							HasAxis = bx == 0 ? false : true
//						}
//					);
					g.Explanations.Add(
						new Explanation {
							Description = c.QuestionOption.Languages[0].Question + ", " + LanguageFactory.GetMeanText(langID) + (stdev ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
							Color = bx + 4,
							Right = bx == 0 ? false : true,
							Box = bx == 0 ? true : false,
							HasAxis = bx == 0 ? false : true
						}
					);
					cx = 1;
					int lastDT = minDT - 1;
					Series s = new Series { Color = bx + 4 };
					foreach (Answer a in answerRepository.FindByQuestionAndOptionGrouped(groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty, sortString)) {
						while (lastDT + 1 < a.DT) {
							lastDT++;
							cx++;
						}

						if (a.CountV >= rac) {
							g.DrawBottomString(GB, a.DT, cx, ", n = " + a.CountV);
//							s.Points.Add(new PointV { X = cx, Y = a.AverageV, Deviation = a.StandardDeviation, T = 3 });
							s.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
						}
						lastDT = a.DT;
						cx++;
					}
					g.Series.Add(s);
					bx++;
				}
			}
//			g.DrawExplanations(explanations);
//			g.DrawExplanationBoxes(explanationBoxes);
			g.Draw();
		}
	}
}