using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Util;

namespace HW.Core.Util.Graphs
{
	public interface IGraphType
	{
		ExtendedGraph Graph { get; set; }
		void Draw(List<Series> series);
	}
	
	public class LineGraphType : IGraphType
	{
		int point;
		int t;
		
		public ExtendedGraph Graph { get; set; }
		
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
						PointV pp = s.Points[i - 1];
						HWList ll = pp.Values;
						Graph.drawStepLine(s.Color, (int)p.X, (int)l.Mean, (int)pp.X, (int)ll.Mean, t);
					}
				}
			}
		}
	}
	
	public class BarGraphType : IGraphType
	{
		public ExtendedGraph Graph { get; set; }
		
		public void Draw(List<Series> series)
		{
//			Graph.DrawBars(new object(), 10, tot, bars);
//			int steps = 10;
			
//			Graph.setMinMax(0f, 100f);

//			steps += 2;
//			int tot = 2000;
			
//			Graph.computeSteping((steps <= 1 ? 2 : steps));
//			Graph.drawOutlines(11);
//			Graph.drawAxis(disabled);
//			Graph.drawAxis(new object());

//			int i = 0;
//			decimal sum = 0;
			Series s = series[0];
//			foreach (Bar b in bars) {
			foreach (var p in s.Points) {
//				i++;
//				sum += (decimal)p.Y;
//				Graph.drawBar(s.Color, i, p.Y);
//				Graph.drawBottomString(p.Description, i, true);
				Graph.drawColorExplBox(s.Description, s.Color, s.X, s.Y);
				Graph.drawBar(s.Color, p.X, (float)p.Values.Mean);
//				Graph.drawBottomString(p.Description, p.X, true);
//				if (b.HasReference) {
//					Graph.drawReference(i, b.Reference);
//				}
//				Graph.drawReference(i, 12);
			}
//			foreach (int l in referenceLines) {
//				drawReferenceLine(l, " = riktvärde");
//			}
//			if (tot > 0) {
//				Graph.drawBar(4, ++i, Convert.ToInt32(Math.Round((tot - sum) / tot * 100M, 0)));
//				Graph.drawBottomString("Inget svar", i, true);
//			}
		}
	}
	
	public class BoxPlotMinMaxGraphType : IGraphType
	{
		public ExtendedGraph Graph { get; set; }
		
		public void Draw(List<Series> series)
		{
            if (series.Count > 0)
            {
                Series s = series[0];
                Graph.drawColorExplBox(s.Description, s.Color, s.X, s.Y);
                foreach (PointV p in s.Points)
                {
                    HWList l = p.Values;
                    Graph.DrawWhiskers2(s.Color, (int)p.X, (int)l.UpperWhisker, (int)l.LowerWhisker);
                    Graph.DrawBar2(s.Color, (int)p.X, (int)l.LowerBox, (int)l.UpperBox);
                    Graph.DrawMedian((int)p.X, (int)l.Median);
                }
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
				Graph.DrawWhiskers2(s.Color, (int)p.X, (int)l.NerdUpperWhisker, (int)l.NerdLowerWhisker);
				Graph.DrawBar2(s.Color, (int)p.X, (int)l.LowerBox, (int)l.UpperBox);
				Graph.DrawMedian((int)p.X, (int)l.Median);
			}
		}
	}
}
