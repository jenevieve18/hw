using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HW.Core.Models;
using HW.Core.Repositories;

namespace HW.Core.Helpers
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
			
			Type = new LineGraphType(0, 2); // TODO: Why default to line graph? Also map the point value to ExtraPoint class.
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
			float halfWidth = (barW / 1) / 2;
			Line l1 = new Line {
				Color = 20,
				X1 = cx * steping - (int)halfWidth,
				Y1 = Convert.ToInt32(maxH - (mean - minVal) / (maxVal - minVal) * maxH),
				X2 = cx * steping + (int)halfWidth,
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
		
		public void DrawWiggle()
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
				string v = GroupStatsGraphFactory.GetBottomString(groupBy, i, j, "");
				DrawBottomString(v, j);
//				DrawBottomString(groupBy, i, j, "");
			}
		}
		
		public void DrawBottomString(string v, int dx)
		{
			drawBottomString(v, dx, true);
		}
		
//		public void DrawBottomString(int groupBy, int i, int dx, string str)
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
//						drawBottomString(v, dx, true);
//						break;
//					}
//				case 2:
//					{
//						int d = i * 2;
//						int w = d % 52;
//						if (w == 0) {
//							w = 52;
//						}
//						string v = string.Format("v{0}-{1}, {2}{3}", w - 1, w, (d - ((d - 1) % 52)) / 52, str);
//						drawBottomString(v, dx, true);
//						break;
//					}
//				case 3:
//					{
//						int d = i;
//						int w = d % 12;
//						if (w == 0) {
//							w = 12;
//						}
//						string v = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[w - 1] + ", " + ((d - w) / 12) + str;
//						drawBottomString(v, dx, true);
//						break;
//					}
//				case 4:
//					{
//						int d = i * 3;
//						int w = d % 12;
//						if (w == 0) {
//							w = 12;
//						}
//						string v = string.Format("Q{0}, {1}{2}", w / 3, (d - w) / 12, str);
//						drawBottomString(v, dx, true);
//						break;
//					}
//				case 5:
//					{
//						int d = i * 6;
//						int w = d % 12;
//						if (w == 0) {
//							w = 12;
//						}
//						string v = string.Format("{0}/{1}{2}", (d - w) / 12, w / 6, str);
//						drawBottomString(v, dx, true);
//						break;
//					}
//				case 6:
//					{
//						drawBottomString(i.ToString() + str, dx, true);
//						break;
//					}
//				case 7:
//					{
//						int d = i * 2;
//						int w = d % 52;
//						if (w == 0) {
//							w = 52;
//						}
//						string v = "v" + w + "-" + ((w == 52 ? 0 : w) + 1) + ", " + ((d + 1) - (d % 52)) / 52 + str;
//						drawBottomString(v, dx, true);
//						break;
//					}
//			}
//		}
	}
	
	public interface IGraphType
	{
		ExtendedGraph Graph { get; set; }
		void Draw(List<Series> series);
	}
	
	public class PointV
	{
		public int X { get; set; }
		public float Y { get; set; }
		public HWList Values { get; set; }
		public string Description { get; set; }
	}
	
	public class LineGraphType : IGraphType
	{
		public ExtendedGraph Graph { get; set; }
		int point;
		int t;
		
		public LineGraphType() : this(0, 2) // TODO: Map this point value to Distribution class.
		{
		}
		
		public LineGraphType(int point, int t)
		{
			this.point = point;
			this.t = t;
		}
		
		public void Draw(List<Series> series)
		{
			Graph.DrawExplanations();
			foreach (Series s in series) {
				Graph.drawColorExplBox(s.Description, s.Color, s.X, s.Y);
				for (int i = 0; i < s.Points.Count; i++) {
					PointV p = s.Points[i];
					HWList l = p.Values;
					if (point == 1) { // TODO: Map this to ExtraPoint class.
						Graph.DrawDeviation(s.Color, (int)p.X, (int)l.Mean, (int)l.StandardDeviation);
					} else if (point == 2) { // TODO: Map this to ExtraPoint class.
						Graph.DrawDeviation(s.Color, (int)p.X, (int)l.Mean, (int)l.ConfidenceInterval);
					}
					Graph.drawCircle((int)p.X, (int)l.Mean, s.Color);
					if (i > 0) {
						PointV pp = s.Points[i -1];
						HWList ll = pp.Values;
						Graph.drawStepLine(s.Color, (int)p.X, (int)l.Mean, (int)pp.X, (int)ll.Mean, t);
					}
				}
			}
		}
	}
	
	public class BarGraphTYpe : IGraphType
	{
		public ExtendedGraph Graph { get; set; }
		
		public void Draw(List<Series> series)
		{
//			Graph.DrawBars(new object(), 10, tot, bars);
			int steps = 10;
			
			Graph.setMinMax(0f, 100f);

			steps += 2;
			int tot = 2000;
			
			Graph.computeSteping((steps <= 1 ? 2 : steps));
			Graph.drawOutlines(11);
//			Graph.drawAxis(disabled);
			Graph.drawAxis(new object());

			int i = 0;
			decimal sum = 0;
			Series s = series[0];
//			foreach (Bar b in bars) {
			foreach (var p in s.Points) {
				i++;
				sum += (decimal)p.Y;
				Graph.drawBar(s.Color, i, p.Y);
				Graph.drawBottomString(p.Description, i, true);
//				if (b.HasReference) {
//					Graph.drawReference(i, b.Reference);
//				}
				Graph.drawReference(i, 12);
			}
//			foreach (int l in referenceLines) {
//				drawReferenceLine(l, " = riktvärde");
//			}
			if (tot > 0) {
				Graph.drawBar(4, ++i, Convert.ToInt32(Math.Round((tot - sum) / tot * 100M, 0)));
				Graph.drawBottomString("Inget svar", i, true);
			}
		}
	}
	
	public class BoxPlotGraphType : IGraphType
	{
		public ExtendedGraph Graph { get; set; }
		
		public void Draw(List<Series> series)
		{
			Series s = series[0];
			Graph.drawColorExplBox(s.Description, s.Color, s.X, s.Y);
			foreach (PointV p in s.Points) {
				HWList l = p.Values;
//				Graph.DrawWhiskers((int)p.X, (int)l.UpperWhisker, (int)l.LowerWhisker);
				Graph.DrawWhiskers((int)p.X, (int)l.NerdUpperWhisker, (int)l.NerdLowerWhisker);
				Graph.DrawBar2(s.Color, (int)p.X, (int)l.LowerBox, (int)l.UpperBox);
				Graph.DrawMedian((int)p.X, (int)l.Median);
			}
		}
	}
	
	public class Series
	{
		public List<PointV> Points { get; set; }
		public int Color { get; set; }
		public string Description { get; set; }
		
		public bool Right { get; set; }
		public bool Box { get; set; }
		public bool HasAxis { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		
		public Series()
		{
			Points = new List<PointV>();
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
}
