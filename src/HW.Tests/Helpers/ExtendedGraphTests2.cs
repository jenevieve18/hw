using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Helpers
{
	[TestFixture]
	public class ExtendedGraphTests2
	{
		ExtendedGraph g;
		Form f;
		PictureBox p;
		
		[SetUp]
		public void Setup()
		{
			g = new ExtendedGraph(800, 600, "#FFFFFF");
			
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
		public void TestBoxPlot()
		{
			g.SetMinMax(0, 100);
			g.DrawBackgroundFromIndex(new BaseIndex(0, 40, 60, 101, 101));
			g.DrawComputingSteps(null, 24);
			
			g.DrawBottomString(52313, 52334, 7);
//			g.SetXAxis(GroupStatsGraphFactory.GetBottomStrings(52313, 52334, 7).ToArray());
			
			g.Explanations.Add(
				new Explanation {
					Description = "mean value",
					Color = 0,
					Right = false,
					Box = false,
					HasAxis = false
				}
			);
			
			var r =  new Random();
			
			int breaker = 3;
			int itemWidth = 240;
			
			for (int i = 0; i < 5; i++) {
				var s = new Series {
					Description = "Series " + i.ToString(),
					Color = i + 4,
					X = 130 + (int)((i % breaker) * itemWidth),
					Y = 20 + (int)Math.Floor((double)i / breaker) * 15
				};
				
				for (int j = 0; j < 22; j++) {
					
					var l = new List<double>();
					for (int k = 0; k < 10; k++) {
						l.Add((double)r.Next(1, 100));
					}
					
					s.Points.Add(new PointV { X = j + 1, Values = new HWList(l) });
				}
				g.Series.Add(s);
			}
			
//			g.Type = new LineGraphType(0, 2);
			g.Type = new BoxPlotGraphType();
			g.Draw();
		}
		
		[Test]
		public void TestBoxPlotUnitMeasure()
		{
			g.SetMinMax(0, 100);
			g.DrawBackgroundFromIndex(new BaseIndex(0, 40, 60, 101, 101));
			g.DrawComputingSteps(null, 24);
			
			g.DrawBottomString(52313, 52334, 7);
			
			g.Explanations.Add(
				new Explanation {
					Description = "mean value",
					Color = 0,
					Right = false,
					Box = false,
					HasAxis = false
				}
			);
			
			var r =  new Random();
			
			var mr = new SqlMeasureRepository();
			
			int breaker = 3;
			int itemWidth = 240;
			
			for (int i = 0; i < 5; i++) {
				var s = new Series {
					Description = "Series " + i.ToString(),
					Color = i + 4,
					X = 130 + (int)((i % breaker) * itemWidth),
					Y = 20 + (int)Math.Floor((double)i / breaker) * 15
				};
				
//				for (int j = 0; j < 22; j++) {
				int j = 0;
				var measures = mr.FindUserMeasures();
				foreach (var a in measures) {
					
//					var l = new List<double>();
//					for (int k = 0; k < 10; k++) {
//						l.Add((double)r.Next(1, 100));
//					}
//					
//					s.Points.Add(new PointV { X = j + 1, Values = new HWList(l) });
					
					s.Points.Add(new PointV { X = j + 1, Values = a.GetDecimalValues() });
				}
				g.Series.Add(s);
			}
			
			g.Type = new LineGraphType(0, 2);
//			g.Type = new BoxPlotGraphType();
			g.Draw();
		}
	}
}
