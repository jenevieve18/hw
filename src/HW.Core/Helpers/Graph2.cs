using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using HW.Core.Helpers;

namespace HW.Core.Helpers
{
	public class Graph2
	{
		Graphics g;
		Bitmap b;
		
		public Graphics Graphics {
			get { return g; }
		}
		
		public int Width { get; set; }
		public int Height { get; set; }
		
		public Bitmap Bitmap {
			get { return b; }
		}
		
		public Margin Margin { get; set; }
		
		public Graph2(int w, int h, string color)
		{
			b = new Bitmap(w, h, PixelFormat.Format24bppRgb);
			g = Graphics.FromImage(b);
			g.Clear(ColorTranslator.FromHtml(color));
			
			this.Width = w;
			this.Height = h;
			Margin = new Margin();
			Series = new List<Series>();
			
			Type = new LineGraphType();
		}
		
		public int InnerWidth {
			get { return Width - (Margin.Right + Margin.Left); }
		}
		
		public int InnerHeight {
			get { return Height - (Margin.Top + Margin.Bottom); }
		}
		
		public List<Series> Series { get; set; }
		
		
		
		public void Draw()
		{
			Type.Draw(this);
		}
		
		public IGraphType2 Type { get; set; }
		
		abstract class BaseGraphType : IGraphType2
		{
			protected Font normalFont = new Font("Tahoma", 7f);
			protected Font smallFont = new Font("Tahoma", 6.5f);
			protected Font bigFont = new Font("Tahoma", 8f);
			protected Pen blackPen = new Pen(ColorTranslator.FromHtml("#000000"));
			
			Graph2 graph = null;
			
			public Graph2 Graph {
				get { return graph; }
			}
			
			public virtual void Draw(Graph2 graph)
			{
				this.graph = graph;
			}
			
			protected void DrawCircle(Pen pen, int x, int y, int radius)
			{
				int diameter = radius * 2;
				graph.Graphics.DrawEllipse(pen, x - radius, y - radius, diameter, diameter);
			}
		}
		
		class LineGraphType : BaseGraphType
		{
			public Rectangle ChartRectangle {
				get {
					return new Rectangle(Graph.Margin.Left, Graph.Margin.Top, Graph.InnerWidth - 1, Graph.InnerHeight - 1);
				}
			}
			
			public override void Draw(Graph2 graph)
			{
				base.Draw(graph);
				var g = graph.Graphics;
				g.DrawRectangle(blackPen, ChartRectangle);
				int yAxis = graph.InnerHeight / (10 + 1);
				for (int i = 1; i < 11; i++) {
					int y1 = graph.Margin.Top + (yAxis * i);
					g.DrawLine(blackPen, graph.Margin.Left, y1, graph.Width - graph.Margin.Right - 1, y1);
				}
				
				int xAxis = graph.InnerWidth / (graph.Series[0].Points.Count + 1);
				int j = 1;
				foreach (var s in graph.Series) {
					foreach (var p in s.Points) {
						int x1 = graph.Margin.Left + (xAxis * j++);
						int y1 = graph.Margin.Top + (int)p.Y;
						DrawCircle(blackPen, x1, y1, 5);
					}
				}
			}
		}
		
		class BoxPlotGraphType : BaseGraphType
		{
			public override void Draw(Graph2 g)
			{
			}
		}
	}
	
	public interface IGraphType2
	{
		void Draw(Graph2 graph);
	}
	
	public class Margin
	{
		public int Top { get; set; }
		public int Right { get; set; }
		public int Bottom { get; set; }
		public int Left { get; set; }
		
		public Margin() : this(0, 0, 0, 0)
		{
		}
		
		public Margin(int top, int right, int bottom, int left)
		{
			this.Top = top;
			this.Right = right;
			this.Bottom = bottom;
			this.Left = left;
		}
	}
}
