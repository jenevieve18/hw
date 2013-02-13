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
using System.Text;
using System.Threading;
using System.Web;
using MySql.Data.MySqlClient;

namespace HW.Core
{
	public class ExtendedGraph : Graph
	{
		IGraphType type;
		
		public List<IExplanation> Explanations { get; set; }
		
		public List<Series> Series { get; set; }
		
		public IGraphType Type {
			get { return type; }
			set { type = value; type.Graph = this; }
		}
		
		public ExtendedGraph(int width, int height, string color) : base(width, height, color)
		{
			Series = new List<Series>();
			Explanations = new List<IExplanation>();
			
			Type = new LineGraphType(false, 2); // TODO: Why default to line graph?
		}
		
		public void DrawDeviation(int color, int cx, int newVal, int newStd)
		{
			Line l1 = new Line {
				Color = color,
				X1 = cx * steping - 10,
				Y1 = Convert.ToInt32(maxH - ((newVal - newStd) - minVal) / (maxVal - minVal) * maxH),
				X2 = cx * steping + 10,
				Y2 = Convert.ToInt32(maxH - ((newVal - newStd) - minVal) / (maxVal - minVal) * maxH),
				T = 1
			};
			drawLine(l1);
			Line l2 = new Line {
				Color = 20,
				X1 = cx * steping,
				Y1 = Convert.ToInt32(maxH - ((newVal - newStd) - minVal) / (maxVal - minVal) * maxH),
				X2 = cx * steping,
				Y2 = Convert.ToInt32(maxH - ((newVal + newStd) - minVal) / (maxVal - minVal) * maxH),
				T = 1
			};
			drawLine(l2);
			Line l3 = new Line {
				Color = color,
				X1 = cx * steping - 10,
				Y1 = Convert.ToInt32(maxH - ((newVal + newStd) - minVal) / (maxVal - minVal) * maxH),
				X2 = cx * steping + 10,
				Y2 = Convert.ToInt32(maxH - ((newVal + newStd) - minVal) / (maxVal - minVal) * maxH),
				T = 1
			};
			drawLine(l3);
		}
		
		public void DrawWhiskers(int cx, int upperWhisker, int lowerWhisker)
		{
			Line l1 = new Line {
				Color = 20,
				X1 = cx * steping - 10,
				Y1 = Convert.ToInt32(maxH - (upperWhisker - minVal) / (maxVal - minVal) * maxH),
				X2 = cx * steping + 10,
				Y2 = Convert.ToInt32(maxH - (upperWhisker - minVal) / (maxVal - minVal) * maxH),
				T = 1
			};
			drawLine(l1);
			Line l2 = new Line {
				Color = 20,
				X1 = cx * steping - 10,
				Y1 = Convert.ToInt32(maxH - (lowerWhisker - minVal) / (maxVal - minVal) * maxH),
				X2 = cx * steping + 10,
				Y2 = Convert.ToInt32(maxH - (lowerWhisker - minVal) / (maxVal - minVal) * maxH),
				T = 1
			};
			drawLine(l2);
			Line l3 = new Line {
				Color = 20,
				X1 = cx * steping,
				Y1 = Convert.ToInt32(maxH - (upperWhisker - minVal) / (maxVal - minVal) * maxH),
				X2 = cx * steping,
				Y2 = Convert.ToInt32(maxH - (lowerWhisker - minVal) / (maxVal - minVal) * maxH),
				T = 1
			};
			drawLine(l3);
		}
		
		public void DrawMedian(int cx, int mean)
		{
			Line l1 = new Line {
				Color = 20,
				X1 = cx * steping - 23,
				Y1 = Convert.ToInt32(maxH - (mean - minVal) / (maxVal - minVal) * maxH),
				X2 = cx * steping + 23,
				Y2 = Convert.ToInt32(maxH - (mean - minVal) / (maxVal - minVal) * maxH),
				T = 1
			};
			drawLine(l1);
		}
		
		public void DrawBar2(int color, int i, float lalala, float v)
		{
			DrawBar2(color, i, lalala, v, steping, barW, 1, 0, 100, false, false);
		}
		
		public void DrawBar2(int color, int i, float lalala, float v, int s, int b, int barDivision, int align, int occupy, bool writeN, bool percent)
		{
			float mMaxH = maxH * occupy / 100;
			float dMaxH = maxH - mMaxH;
			
			float width = b / barDivision;
			float height = (v - minVal) / (maxVal - minVal) * mMaxH;
			float lh = (lalala - minVal) / (maxVal - minVal) * mMaxH;
			height -= lh;
			
			int atH = Convert.ToInt32(mMaxH - height);
			
			float x = leftSpacing + (i * s) - (b / 2) + (b / 2 * align);
			float y = topSpacing + dMaxH + atH - lh;
			objGraphics.DrawRectangle(blackPen, x, y, width, height);
			float x2 = x + 1;
			float y2 = y + 1;
			float width2 = width - 1;
			float height2 = height - 2;
			objGraphics.FillRectangle(GetSolidBrush(colors[color]), x2, y2, width2, height2);
			if (barDivision <= 2) {
				float x3 = leftSpacing + b / barDivision + 1 + i * s - b / 2 + b / 2 * align;
				float y3 = y + 2;
				float height3 = height - 2;
				objGraphics.FillRectangle(shadowBrush, x3, y3, 2, height3);
			}
			if (writeN && b / barDivision > 30) {
				objGraphics.DrawString(v.ToString() + (percent ? "%" : ""), smallFont, solidBlackBrush, leftSpacing + 1 + i * s - b / 2 + b / 2 * align + (b / barDivision - 1) / 2, topSpacing + 2 + dMaxH + (atH > mMaxH / 5 ? atH - 15 : atH + 15), drawFormatCenter);
			}
		}
		
		public void DrawExplanations()
		{
			foreach (var e in Explanations) {
				drawAxis(e.HasAxis);
				drawAxisExpl(e.Description, e.Color, e.Right, e.Box);
			}
		}
		
		public void Draw()
		{
			DrawWiggle();
			Type.Draw(Series);
		}
		
		public void DrawBars(object disabled, int steps, List<Bar> bars)
		{
			DrawBars(disabled, steps, 0, bars, new List<int>());
		}
		
		public void DrawBars(object disabled, int steps, List<Bar> bars, List<int> referenceLines)
		{
			DrawBars(disabled, steps, 0, bars, referenceLines);
		}
		
		public void DrawBars(object disabled, int steps, decimal tot, List<Bar> bars)
		{
			DrawBars(disabled, steps, tot, bars, new List<int>());
		}
		
		public void DrawBars(object disabled, int steps, decimal tot, List<Bar> bars, List<int> referenceLines)
		{
			setMinMax(0f, 100f);
			
			steps += 2;
			
			computeSteping((steps <= 1 ? 2 : steps));
			drawOutlines(11);
			drawAxis(disabled);
			
			int i = 0;
			decimal sum = 0;
			foreach (Bar b in bars) {
				i++;
				sum += (decimal)b.Value;
				drawBar(b.Color, i, b.Value);
				drawBottomString(b.Description, i, true);
				if (b.HasReference) {
					drawReference(i, b.Reference);
				}
			}
			foreach (int l in referenceLines) {
				drawReferenceLine(l, " = riktvärde");
			}
			if (tot > 0) {
				drawBar(4, ++i, Convert.ToInt32(Math.Round((tot - sum) / tot * 100M, 0)));
				drawBottomString("Inget svar", i, true);
			}
		}
		
		public void DrawBackgroundFromIndexes(List<IIndex> indexes)
		{
			foreach (var i in indexes) {
				if (i.YellowLow > 0) {
					drawBgFromString(minVal, Math.Min(maxVal, (float)Convert.ToDouble(i.YellowLow)), "FFA8A8");                             // red
				}
				if (i.YellowLow < 100 && i.GreenLow > 0) {
					drawBgFromString(Math.Max(minVal, (float)Convert.ToDouble(i.YellowLow)), Math.Min(maxVal, (float)Convert.ToDouble(i.GreenLow)), "FFFEBE");    // yellow
				}
				if (i.GreenLow < 100 && i.GreenHigh > 0) {
					drawBgFromString(Math.Max(minVal, (float)Convert.ToDouble(i.GreenLow)), Math.Min(maxVal, (float)Convert.ToDouble(i.GreenHigh)), "CCFFBB");   // green
				}
				if (i.GreenHigh < 100 && i.YellowHigh > 0) {
					drawBgFromString(Math.Max(minVal, (float)Convert.ToDouble(i.GreenHigh)), Math.Min(maxVal, (float)Convert.ToDouble(i.YellowHigh)), "FFFEBE"); // yellow
				}
				if (i.YellowHigh < 100) {
					drawBgFromString(Math.Max(minVal, (float)Convert.ToDouble(i.YellowHigh)), maxVal, "FFA8A8");                           // red
				}
			}
		}
		
		public void DrawBackgroundFromIndexes2(List<IIndex> indexes)
		{
			foreach (var i in indexes) {
				if (i.YellowLow > 0) {
					drawBgFromString2(minVal, Math.Min(maxVal, (float)Convert.ToDouble(i.YellowLow)), "FFA8A8", "Unhealthy");                             // red
				}
				if (i.YellowLow < 100 && i.GreenLow > 0) {
					drawBgFromString2(Math.Max(minVal, (float)Convert.ToDouble(i.YellowLow)), Math.Min(maxVal, (float)Convert.ToDouble(i.GreenLow)), "FFFEBE", "Improvement Needed");    // yellow
				}
				if (i.GreenLow < 100 && i.GreenHigh > 0) {
					drawBgFromString2(Math.Max(minVal, (float)Convert.ToDouble(i.GreenLow)), Math.Min(maxVal, (float)Convert.ToDouble(i.GreenHigh)), "CCFFBB", "Healthy");   // green
				}
				if (i.GreenHigh < 100 && i.YellowHigh > 0) {
					drawBgFromString2(Math.Max(minVal, (float)Convert.ToDouble(i.GreenHigh)), Math.Min(maxVal, (float)Convert.ToDouble(i.YellowHigh)), "FFFEBE", "Improvement Needed"); // yellow
				}
				if (i.YellowHigh < 100) {
					drawBgFromString2(Math.Max(minVal, (float)Convert.ToDouble(i.YellowHigh)), maxVal, "FFA8A8", "Unhealthy");                           // red
				}
			}
		}
		
		public void SetMinMaxes(List<IMinMax> minMaxes)
		{
			for (int i = 0; i < minMaxes.Count; i++) {
				setMinMax(minMaxes[i].Min, minMaxes[i].Max);
				roundMinMax();
			}
		}
		
		void DrawWiggle()
		{
			if (minVal != 0f) {
				// Crunched graph sign
				drawLine(20, 0, (int)maxH + 20, 0, (int)maxH + 23, 1);
				drawLine(20, 0, (int)maxH + 23, -5, (int)maxH + 26, 1);
				drawLine(20, -5, (int)maxH + 26, 5, (int)maxH + 32, 1);
				drawLine(20, 5, (int)maxH + 32, 0, (int)maxH + 35, 1);
				drawLine(20, 0, (int)maxH + 35, 0, (int)maxH + 38, 1);
			}
		}
		
		public void DrawComputingSteps(object disabled, int steps)
		{
			computeSteping((steps <= 1 ? 2 : steps));
			drawOutlines(11);
			drawAxis(disabled);
		}
		
		public void DrawBottomString(int minDT, int maxDT, int groupBy)
		{
			int j = 0;
			for (int i = minDT; i <= maxDT; i++) {
				j++;
				DrawBottomString(groupBy, i, j, "");
			}
		}
		
		public void DrawBottomString(int groupBy, int i, int dx, string str)
		{
			switch (groupBy) {
				case 1:
					{
						int d = i;
						int w = d % 52;
						if (w == 0) {
							w = 52;
						}
						//						string v = "v" + w + ", " + (d / 52) + str;
						string v = string.Format("v{0}, {1}{2}", w, d / 52, str);
						drawBottomString(v, dx, true);
						break;
					}
				case 2:
					{
						int d = i * 2;
						int w = d % 52;
						if (w == 0) {
							w = 52;
						}
						//						string v = "v" + (w - 1) + "-" + w + ", " + (d - ((d - 1) % 52)) / 52 + str;
						string v = string.Format("v{0}-{1}, {2}{3}", w - 2, w, (d - ((d - 1) % 52)) / 52, str);
						drawBottomString(v, dx, true);
						break;
					}
				case 3:
					{
						int d = i;
						int w = d % 12;
						if (w == 0) {
							w = 12;
						}
						string v = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[w - 1] + ", " + ((d - w) / 12) + str;
						drawBottomString(v, dx, true);
						break;
					}
				case 4:
					{
						int d = i * 3;
						int w = d % 12;
						if (w == 0) {
							w = 12;
						}
						//						string v = "Q" + (w / 3) + ", " + ((d - w) / 12) + str;
						string v = string.Format("Q{0}, {1}{2}", w / 3, (d - w) / 12, str);
						drawBottomString(v, dx, true);
						break;
					}
				case 5:
					{
						int d = i * 6;
						int w = d % 12;
						if (w == 0) {
							w = 12;
						}
						//						string v = ((d - w) / 12) + "/" + (w / 6) + str;
						string v = string.Format("{0}/{1}{2}", (d - w) / 12, w / 6, str);
						drawBottomString(v, dx, true);
						break;
					}
				case 6:
					{
						drawBottomString(i.ToString() + str, dx, true);
						break;
					}
				case 7:
					{
						int d = i * 2;
						int w = d % 52;
						if (w == 0) {
							w = 52;
						}
						string v = "v" + w + "-" + ((w == 52 ? 0 : w) + 1) + ", " + ((d + 1) - (d % 52)) / 52 + str;
						drawBottomString(v, dx, true);
						break;
					}
			}
		}
	}
	
	public interface IGraphFactory
	{
		ExtendedGraph CreateGraph(string key, int rpid, int langID, int PRUID, int type, int fy, int ty, int cx, int rac, int o, int q, int GB, bool stdev, bool hasGrouping, string plot, int width, int height, string bg, int GRPNG, int SPONS, int SID, string GID, object disabled);
		
		string CreateGraph2(string key, int rpid, int langID, int PRUID, int type, int fy, int ty, int cx, int rac, int o, int q, int GB, bool stdev, bool hasGrouping, string plot, int width, int height, string bg, int GRPNG, int SPONS, int SID, string GID, object disabled);
	}
	
	public class UserLevelGraphFactory : IGraphFactory
	{
		IAnswerRepository answerRepository;
		IReportRepository reportRepository;
		
		public UserLevelGraphFactory(IAnswerRepository answerRepository, IReportRepository reportRepository)
		{
			this.answerRepository = answerRepository;
			this.reportRepository = reportRepository;
		}
		
		public ExtendedGraph CreateGraph(string key, int rpid, int langID, int PRUID, int type, int fy, int ty, int cx, int rac, int o, int q, int GB, bool stdev, bool hasGrouping, string plot, int width, int height, string bg, int GRPNG, int SPONS, int SID, string GID, object disabled)
		{
			int answerID = 0;
			int projectRoundUserID = 0;
			Answer a = answerRepository.ReadByKey(key);
			if (a != null) {
				answerID = a.Id;
				if (langID == 0) {
					langID = a.Language.Id;
				}
				projectRoundUserID = a.ProjectRoundUser.Id;
			}
			LanguageFactory.SetCurrentCulture(langID);

			ExtendedGraph g = new ExtendedGraph(width, height, bg);
			g.setMinMax(0f, 100f);

			if (type == 8) {
				int dx = answerRepository.CountByProject(projectRoundUserID, fy, ty);
				if (dx == 1) {
					type = 9;
				} else {
					cx = dx;
				}
			}
			if (type == 8) {
				g.computeSteping(cx);
				g.drawOutlines(11);

				int bx = 0;
				foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID)) {
					if (bx == 0) {
						g.drawAxisExpl(c.QuestionOption.Languages[0].Question, bx + 4, false, true);
						g.drawAxis(false);
					} else {
						g.drawAxisExpl(c.QuestionOption.Languages[0].Question, bx + 4, true, true);
						g.drawAxis(true);
					}
					float lastVal = -1f;
					int lastCX = 0;
					cx = 0;
					foreach (Answer aa in answerRepository.FindByQuestionAndOptionWithYearSpan(c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty)) {
						if (bx == 0) {
							g.drawBottomString(aa.SomeString, cx);
						}
						float newVal = aa.Average;
						if (lastVal != -1f && newVal != -1f) {
							g.drawStepLine(bx + 4, lastCX, lastVal, cx, newVal);
							lastCX = cx;
						}
						cx++;
						lastVal = newVal;
					}
					bx++;
				}
			} else if (type == 9) {
				g.computeSteping(cx + 2);
				g.drawOutlines(11);
				g.drawAxis();

				cx = 0;

				bool hasReference = false;

				foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID)) {
					a = answerRepository.ReadByQuestionAndOption(answerID, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id);
					if (a != null) {
						int color = IndexFactory.GetColor(c.QuestionOption, a.Values[0].ValueInt);
						g.drawBar(color, ++cx, a.Values[0].ValueInt);
						if (c.QuestionOption.TargetValue != 0) {
							hasReference = true;
							g.drawReference(cx, c.QuestionOption.TargetValue);
						}
						g.drawBottomString(c.QuestionOption.Languages[0].Question, cx, true);
					}
				}

				// g.drawAxisExpl("poäng", 0, false, false);

				if (hasReference) {
					g.drawReference(450, 25, " = riktvärde");
				}

				g.drawColorExplBox("Hälsosam nivå", 0, 100, 30);
				g.drawColorExplBox("Förbättringsbehov", 1, 250, 30);
				g.drawColorExplBox("Ohälsosam nivå", 2, 400, 30);
			}
			return g;
		}
		
		public string CreateGraph2(string key, int rpid, int langID, int PRUID, int type, int fy, int ty, int cx, int rac, int o, int q, int GB, bool stdev, bool hasGrouping, string plot, int width, int height, string bg, int GRPNG, int SPONS, int SID, string GID, object disabled)
		{
			int answerID = 0;
			int projectRoundUserID = 0;
			Answer a = answerRepository.ReadByKey(key);
			if (a != null) {
				answerID = a.Id;
				if (langID == 0) {
					langID = a.Language.Id;
				}
				projectRoundUserID = a.ProjectRoundUser.Id;
			}
			LanguageFactory.SetCurrentCulture(langID);

			StringBuilder s = new StringBuilder();
//			ExtendedGraph g = new ExtendedGraph(width, height, bg);
//			g.setMinMax(0f, 100f);

			if (type == 8) {
				int dx = answerRepository.CountByProject(projectRoundUserID, fy, ty);
				if (dx == 1) {
					type = 9;
				} else {
					cx = dx;
				}
			}
			if (type == 8) {
//				g.computeSteping(cx);
//				g.drawOutlines(11);

				int bx = 0;
				foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID)) {
					if (bx == 0) {
//						g.drawAxisExpl(c.QuestionOption.Languages[0].Question, bx + 4, false, true);
//						g.drawAxis(false);
					} else {
//						g.drawAxisExpl(c.QuestionOption.Languages[0].Question, bx + 4, true, true);
//						g.drawAxis(true);
					}
					float lastVal = -1f;
					int lastCX = 0;
					cx = 0;
					foreach (Answer aa in answerRepository.FindByQuestionAndOptionWithYearSpan(c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty)) {
						if (bx == 0) {
//							g.drawBottomString(aa.SomeString, cx);
						}
						float newVal = aa.Average;
						if (lastVal != -1f && newVal != -1f) {
//							g.drawStepLine(bx + 4, lastCX, lastVal, cx, newVal);
							lastCX = cx;
						}
						cx++;
						lastVal = newVal;
					}
					bx++;
				}
			} else if (type == 9) {
//				g.computeSteping(cx + 2);
//				g.drawOutlines(11);
//				g.drawAxis();

				cx = 0;

				bool hasReference = false;

				foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID)) {
					a = answerRepository.ReadByQuestionAndOption(answerID, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id);
					if (a != null) {
						int color = IndexFactory.GetColor(c.QuestionOption, a.Values[0].ValueInt);
//						g.drawBar(color, ++cx, a.Values[0].ValueInt);
						if (c.QuestionOption.TargetValue != 0) {
							hasReference = true;
//							g.drawReference(cx, c.QuestionOption.TargetValue);
						}
//						g.drawBottomString(c.QuestionOption.Languages[0].Question, cx, true);
					}
				}

				if (hasReference) {
//					g.drawReference(450, 25, " = riktvärde");
				}

//				g.drawColorExplBox("Hälsosam nivå", 0, 100, 30);
//				g.drawColorExplBox("Förbättringsbehov", 1, 250, 30);
//				g.drawColorExplBox("Ohälsosam nivå", 2, 400, 30);
			}
//			return g;
			return s.ToString();
		}
	}
	
	public class GroupStatsGraphFactory : IGraphFactory
	{
		IProjectRepository projectRepository;
		IAnswerRepository answerRepository;
		IOptionRepository optionRepository;
		IReportRepository reportRepository;
		IIndexRepository indexRepository;
		IQuestionRepository questionRepository;
		IDepartmentRepository departmentRepository;
		
		int lastCount = 0;
		float lastVal = 0;
		string lastDesc = "";
		System.Collections.Hashtable res = new System.Collections.Hashtable();
		System.Collections.Hashtable cnt = new System.Collections.Hashtable();
		
		public GroupStatsGraphFactory(IAnswerRepository answerRepository, IReportRepository reportRepository, IProjectRepository projectRepository, IOptionRepository optionRepository, IIndexRepository indexRepository, IQuestionRepository questionRepository, IDepartmentRepository departmentRepository)
		{
			this.projectRepository = projectRepository;
			this.answerRepository = answerRepository;
			this.optionRepository = optionRepository;
			this.reportRepository = reportRepository;
			this.indexRepository = indexRepository;
			this.questionRepository = questionRepository;
			this.departmentRepository = departmentRepository;
		}
		
		public ExtendedGraph CreateGraph(string key, int rpid, int langID, int PRUID, int type, int fy, int ty, int cx, int rac, int o, int q, int GB, bool stdev, bool hasGrouping, string plot, int width, int height, string bg, int GRPNG, int SPONS, int SID, string GID, object disabled)
		{
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

			if (type == 1) {
				decimal tot = answerRepository.CountByDate(fy, ty, sortString);

				if (rac > Convert.ToInt32(tot)) {
					g = new ExtendedGraph(895, 50, "#FFFFFF");
					g.drawStringInGraph("Ingen återkoppling pga otillräckligt underlag", 300, -30);
				} else {
					g = new ExtendedGraph(895, 550, "#FFFFFF");
					List<Bar> bars = new List<Bar>();
					foreach (OptionComponentLanguage c in optionRepository.FindComponentsByLanguage(o, langID)) {
						int x = answerRepository.CountByValueWithDateOptionAndQuestion(c.Component.Id, fy, ty, o, q, sortString);
						var b = new Bar {
							Description = c.Text,
							Value = Convert.ToInt32(Math.Round(Convert.ToDecimal(x) / tot * 100M, 0)),
							Color = 5
						};
						bars.Add(b);
					}
					cx = optionRepository.CountByOption(o);
					g.DrawBars(disabled, cx, tot, bars);
					g.drawAxisExpl(string.Format("% (n = {0})", tot), 5, false, false);
				}
			} else if (type == 3) {
				g = new ExtendedGraph(895, 550, "#FFFFFF");
				List<Bar> bars = new List<Bar>();
				List<int> referenceLines = new List<int>();
				
				foreach (ReportPartComponent c in reportRepository.FindComponents(rpid)) {
					System.Collections.SortedList all = new System.Collections.SortedList();

					foreach (ProjectRoundUnit u in projectRepository.FindRoundUnitsBySortString(sortString)) {
						res = new System.Collections.Hashtable();

						if (c.Index.Parts.Count == 0) {
							GetIdxVal(c.Index.Id, u.SortString, langID, fy, ty);
						} else {
							GetOtherIdxVal(c.Index.Id, u.SortString, langID, fy, ty);
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
				g.DrawBars(disabled, cx, bars, referenceLines);
				g.drawAxisExpl("poäng", 0, false, false);
			} else if (type == 2) {
				g = new ExtendedGraph(895, 550, "#FFFFFF");
				List<Bar> bars = new List<Bar>();
				foreach (ReportPartComponent c in reportRepository.FindComponents(rpid)) {
					if (c.Index.Parts.Count == 0) {
						GetIdxVal(c.Index.Id, sortString, langID, fy, ty);
					} else {
						GetOtherIdxVal(c.Index.Id, sortString, langID, fy, ty);
					}
					int color = IndexFactory.GetColor(c.Index, lastVal);
					bars.Add(new Bar { Value = lastVal, Color = color, Description = lastDesc, Reference = c.Index.TargetValue });
				}
				g.DrawBars(disabled, cx, bars);
				g.drawAxisExpl("poäng", 0, false, false);
				g.drawReference(780, 25, " = riktvärde");
			} else if (type == 8) {
				if (GB == 0) {
					GB = 2;
				}
				
				string groupBy = GroupFactory.GetGroupBy(GB);
				g = new ExtendedGraph(895, 440, "#FFFFFF");

				int t = 2 + (!stdev ? 1 : 0);
				if (plot == "BoxPlot") {
					g.Type = new BoxPlotGraphType();
				} else {
					g.Type = new LineGraphType(stdev, t);
				}
				Answer answer = answerRepository.ReadByGroup(groupBy, fy, ty, sortString);
				if (answer != null) {
					cx = answer.DummyValue1 + 3;
					minDT = answer.DummyValue2;
					maxDT = answer.DummyValue3;
				}

				List<IIndex> indexes = new List<IIndex>();
				List<IMinMax> minMaxes = new List<IMinMax>();
				foreach (ReportPartComponent c in reportRepository.FindComponentsByPart(rpid)) {
					if (!hasGrouping) {
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
				g.DrawBackgroundFromIndexes2(indexes);
				g.DrawComputingSteps(disabled, cx);

				cx = 0;
				
				g.DrawBottomString(minDT, maxDT, GB);
				
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
								while (lastDT + 1 < a.SomeInteger) {
									lastDT++;
									cx++;
								}
								if (a.Values.Count >= rac) {
									if (COUNT == 1) {
										g.DrawBottomString(GB, a.SomeInteger, cx, (COUNT == 1 ? ", n = " + a.Values.Count : ""));
									}
									s.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
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
					foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID)) {
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
							while (lastDT + 1 < a.SomeInteger) {
								lastDT++;
								cx++;
							}

							if (a.CountV >= rac) {
								g.DrawBottomString(GB, a.SomeInteger, cx, ", n = " + a.CountV);
								s.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
							}
							lastDT = a.SomeInteger;
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
		
		public string CreateGraph2(string key, int rpid, int langID, int PRUID, int type, int fy, int ty, int cx, int rac, int o, int q, int GB, bool stdev, bool hasGrouping, string plot, int width, int height, string bg, int GRPNG, int SPONS, int SID, string GID, object disabled)
		{
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
			StringBuilder ss = new StringBuilder();
//			ExtendedGraph g = null;

			LanguageFactory.SetCurrentCulture(langID);

			if (type == 1) {
				decimal tot = answerRepository.CountByDate(fy, ty, sortString);

				if (rac > Convert.ToInt32(tot)) {
//					g = new ExtendedGraph(895, 50, "#FFFFFF");
//					g.drawStringInGraph("Ingen återkoppling pga otillräckligt underlag", 300, -30);
				} else {
//					g = new ExtendedGraph(895, 550, "#FFFFFF");
//					List<Bar> bars = new List<Bar>();
					foreach (OptionComponentLanguage c in optionRepository.FindComponentsByLanguage(o, langID)) {
						int x = answerRepository.CountByValueWithDateOptionAndQuestion(c.Component.Id, fy, ty, o, q, sortString);
//						var b = new Bar {
//							Description = c.Text,
//							Value = Convert.ToInt32(Math.Round(Convert.ToDecimal(x) / tot * 100M, 0)),
//							Color = 5
//						};
//						bars.Add(b);
					}
					cx = optionRepository.CountByOption(o);
//					g.DrawBars(disabled, cx, tot, bars);
//					g.drawAxisExpl(string.Format("% (n = {0})", tot), 5, false, false);
				}
			} else if (type == 3) {
//				g = new ExtendedGraph(895, 550, "#FFFFFF");
//				List<Bar> bars = new List<Bar>();
//				List<int> referenceLines = new List<int>();
				
				foreach (ReportPartComponent c in reportRepository.FindComponents(rpid)) {
					System.Collections.SortedList all = new System.Collections.SortedList();

					foreach (ProjectRoundUnit u in projectRepository.FindRoundUnitsBySortString(sortString)) {
						res = new System.Collections.Hashtable();

						if (c.Index.Parts.Count == 0) {
							GetIdxVal(c.Index.Id, u.SortString, langID, fy, ty);
						} else {
							GetOtherIdxVal(c.Index.Id, u.SortString, langID, fy, ty);
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
//							bars.Add(new Bar { Color = color, Value = Convert.ToInt32(all.GetKey(i)), Description = s });
						}
					}
//					referenceLines.Add(c.Index.TargetValue);
				}
//				g.DrawBars(disabled, cx, bars, referenceLines);
//				g.drawAxisExpl("poäng", 0, false, false);
			} else if (type == 2) {
//				g = new ExtendedGraph(895, 550, "#FFFFFF");
//				List<Bar> bars = new List<Bar>();
				foreach (ReportPartComponent c in reportRepository.FindComponents(rpid)) {
					if (c.Index.Parts.Count == 0) {
						GetIdxVal(c.Index.Id, sortString, langID, fy, ty);
					} else {
						GetOtherIdxVal(c.Index.Id, sortString, langID, fy, ty);
					}
					int color = IndexFactory.GetColor(c.Index, lastVal);
//					bars.Add(new Bar { Value = lastVal, Color = color, Description = lastDesc, Reference = c.Index.TargetValue });
				}
//				g.DrawBars(disabled, cx, bars);
//				g.drawAxisExpl("poäng", 0, false, false);
//				g.drawReference(780, 25, " = riktvärde");
			} else if (type == 8) {
				if (GB == 0) {
					GB = 2;
				}
				
				string groupBy = GroupFactory.GetGroupBy(GB);
//				g = new ExtendedGraph(895, 440, "#FFFFFF");

				int t = 2 + (!stdev ? 1 : 0);
				if (plot == "BoxPlot") {
//					g.Type = new BoxPlotGraphType();
				} else {
//					g.Type = new LineGraphType(stdev, t);
				}
				Answer answer = answerRepository.ReadByGroup(groupBy, fy, ty, sortString);
				if (answer != null) {
					cx = answer.DummyValue1 + 3;
					minDT = answer.DummyValue2;
					maxDT = answer.DummyValue3;
				}

				List<IIndex> indexes = new List<IIndex>();
				List<IMinMax> minMaxes = new List<IMinMax>();
				foreach (ReportPartComponent c in reportRepository.FindComponentsByPart(rpid)) {
					if (!hasGrouping) {
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
//				g.SetMinMaxes(minMaxes);
//				g.DrawBackgroundFromIndexes2(indexes);
//				g.DrawComputingSteps(disabled, cx);

				cx = 0;
				
//				g.DrawBottomString(minDT, maxDT, GB);
				
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
					
//					g.Explanations.Add(
//						new Explanation {
//							Description = (extraDesc != "" ? extraDesc + "\n" : "") + LanguageFactory.GetMeanText(langID) + (stdev ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
//							Color = 0,
//							Right = false,
//							Box = false,
//							HasAxis = false
//						}
//					);
					ReportPartComponent c = reportRepository.ReadComponentByPartAndLanguage(rpid, langID);
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
							var answers = answerRepository.FindByQuestionAndOptionJoinedAndGrouped2(join[i].ToString(), groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty);
//							Series s = new Series {
//								Description = (string)desc[i],
//								Color = bx + 4,
//								X = 130 + (int)((bx % breaker) * itemWidth),
//								Y = 20 + (int)Math.Floor((double)bx / breaker) * 15
//							};
							foreach (Answer a in answers) {
								while (lastDT + 1 < a.SomeInteger) {
									lastDT++;
									cx++;
								}
								if (a.Values.Count >= rac) {
									if (COUNT == 1) {
//										g.DrawBottomString(GB, a.SomeInteger, cx, (COUNT == 1 ? ", n = " + a.Values.Count : ""));
									}
//									s.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
									foreach (var v in a.Values) {
										ss.Append(v.ValueInt.ToString());
										ss.Append(",");
									}
								}
								ss.Append(Environment.NewLine);
								lastDT = a.SomeInteger;
								cx++;
							}
//							g.Series.Add(s);
							bx++;
						}
					}
				} else {
					int bx = 0;
					foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID)) {
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
						foreach (Answer a in answerRepository.FindByQuestionAndOptionGrouped(groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty, sortString)) {
							while (lastDT + 1 < a.SomeInteger) {
								lastDT++;
								cx++;
							}

							if (a.CountV >= rac) {
//								g.DrawBottomString(GB, a.SomeInteger, cx, ", n = " + a.CountV);
//								s.Points.Add(new PointV { X = cx, Values = a.GetIntValues() });
							}
							lastDT = a.SomeInteger;
							cx++;
						}
//						g.Series.Add(s);
						bx++;
					}
				}
//				g.Draw();
			}
//			return g;
			return ss.ToString();
		}
		
		void GetIdxVal(int idx, string sortString, int langID, int fy, int ty)
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

		void GetOtherIdxVal(int idx, string sortString, int langID, int fy, int ty)
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
						GetIdxVal(p.OtherIndex.Id, sortString, langID, fy, ty);
						tot += lastVal * p.Multiple;
						minCnt = Math.Min(lastCount, minCnt);
					}
				}
			}
			lastVal = 100 * tot / max;
			lastCount = minCnt;
		}
	}
	
	public class GraphFactory
	{
		public static IGraphFactory CreateFactory(bool hasAnswerKey, IAnswerRepository answerRepository, IReportRepository reportRepository, IProjectRepository projectRepository, IOptionRepository optionRepository, IDepartmentRepository departmentRepository, IQuestionRepository questionRepository, IIndexRepository indexRepository)
		{
			if (hasAnswerKey) {
				return new UserLevelGraphFactory(answerRepository, reportRepository);
			} else {
				return new GroupStatsGraphFactory(answerRepository, reportRepository, projectRepository, optionRepository, indexRepository, questionRepository, departmentRepository);
			}
		}
	}
}
