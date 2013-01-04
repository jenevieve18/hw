//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Threading;
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
		public float Deviation { get; set; }
		public int T { get; set; }
		
		public double LowerWhisker { get; set; }
		public double UpperWhisker { get; set; }
		public double LowerBox { get; set; }
		public double UpperBox { get; set; }
		public double Median { get; set; }
	}
	
	public class LineGraphType : IGraphType
	{
		public ExtendedGraph Graph { get; set; }
		bool stdev;
		
		public LineGraphType() : this(false)
		{
		}
		
		public LineGraphType(bool stdev)
		{
			this.stdev = stdev;
		}
		
		public void Draw(List<Series> series)
		{
			foreach (Series s in series) {
				for (int i = 0; i < s.Points.Count; i++) {
					PointV p = s.Points[i];
					if (stdev) {
						Graph.DrawDeviation(s.Color, (int)p.X, (int)p.Y, (int)p.Deviation);
					}
					Graph.drawCircle((int)p.X, p.Y, 4);
					if (i > 0) {
						PointV pp = s.Points[i -1];
						Graph.drawStepLine(s.Color, (int)p.X, p.Y, (int)pp.X, pp.Y, p.T);
					}
				}
			}
		}
	}
	
	public class BoxPlotGraphType : IGraphType
	{
		public ExtendedGraph Graph { get; set; }
		
		public void Draw(List<Series> series)
		{
			foreach (Series s in series) {
				PointV p = s.Points[0];
				Graph.DrawWhiskers((int)p.X, (int)p.UpperWhisker, (int)p.LowerWhisker);
				Graph.DrawBar2(s.Color, (int)p.X, (int)p.LowerBox, (int)p.UpperBox);
				Graph.DrawMedian((int)p.X, (int)p.Y);
			}
		}
	}
	
	public class Series
	{
		public List<PointV> Points { get; set; }
		public int Color { get; set; }
		
		public Series()
		{
			Points = new List<PointV>();
		}
	}
	
	public class ExtendedGraph : Graph
	{
		IGraphType type;
		List<Series> series;
		
		public List<Series> Series {
			get { return series; }
			set { series = value; }
		}
		
		public IGraphType Type {
			get { return type; }
			set { type = value; type.Graph = this; }
		}
		
		public ExtendedGraph(int width, int height, string color) : base(width, height, color)
		{
			Series = new List<Series>();
			
			Type = new LineGraphType(false);
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
		
//		public void DrawBox(int color, int i, float min, float v)
//		{
//			drawBar2(color, i, min + 14, v, steping, barW, 1, 0, 100, false, false);
//		}
		
		public void DrawExplanations(List<IExplanation> explanations)
		{
			foreach (var e in explanations) {
				drawAxis(e.HasAxis);
				drawAxisExpl(e.Description, e.Color, e.Right, e.Box);
			}
		}
		
//		public void DrawCircles(List<ICircle> circles)
//		{
//			foreach (var c in circles) {
//				drawCircle(c.CX, c.Value, c.Color);
//			}
//		}
		
		public void Draw()
		{
			DrawWiggle();
			Type.Draw(Series);
		}
		
//		public void DrawLines(List<ILine> lines)
//		{
//			foreach (var l in lines) {
//				drawStepLine(l.Color, l.X1, l.Y1, l.X2, l.Y2, l.T);
//			}
//		}
		
		public void DrawExplanationBoxes(List<IExplanation> explanationBoxes)
		{
			foreach (var e in explanationBoxes) {
				drawColorExplBox(e.Description, e.Color, e.X, e.Y);
			}
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
			switch (lid) {
					case 0: return "Välj område";
					case 1: return "Choose area";
					default: throw new NotSupportedException();
			}
		}
		
		public static string GetChooseCategory(int lid)
		{
			switch (lid) {
					case 0: return "Välj kategori";
					case 1: return "Choose category";
					default: throw new NotSupportedException();
			}
		}
		
		public static string GetSortingOrder(int lid, int bx)
		{
			switch (lid) {
					case 0: return bx + " övningar - Sortering:";
					case 1: return bx + " exercises - Order:";
					default: throw new NotSupportedException();
			}
		}
		
		public static string GetLegend(int lid)
		{
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
		
		public string InvitationKey { get; set; }
	}
	
	public class SponsorInviteBackgroundQuestion : BaseModel
	{
		public SponsorInvite Invite { get; set; }
		public BackgroundQuestion Question { get; set; }
		public BackgroundAnswer Answer { get; set; }
		public int ValueInt { get; set; }
		public DateTime ValueDate { get; set; }
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
		public int IntegerValue { get; set; }
		public string StringValue { get; set; }
		public DateTime DateValue { get; set; }
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
		public ProjectRoundUser ProjectRoundUser { get; set; }
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