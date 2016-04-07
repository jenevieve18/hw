using System;
using System.Drawing;
using System.Windows.Forms;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Grp;
using NUnit.Framework;

namespace HW.Tests.Helpers
{
	[TestFixture]
	public class GraphTests
	{
		Form f;
		PictureBox p;
		Graph2 g;
		
		[SetUp]
		public void Setup()
		{
			g = new Graph2(895, 440, "#FFFFFF");
			g.Margin = new Margin(50, 50, 50, 50);
			
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
			p.Image = g.Bitmap;
			f.Controls.Add(p);
			f.ShowDialog();
		}
		
		[Test]
		public void a()
		{
			var s = new Series();
			var r = new Random();
			for (int i = 0; i < 10; i++) {
				s.Points.Add(new PointV() { Y = r.Next(1, 100) });
			}
			g.Series.Add(s);
			g.Draw();
		}
		
		[Test]
		public void b()
		{
			var s = new Series();
			var r = new Random();
			var mr = new SqlMeasureRepository();
			foreach (var x in mr.FindUserMeasures()) {
				var l = new HWList();
				for (int i = 0; i < 100; i++) {
					l.Add(r.Next(1, 100));
				}
				s.Points.Add(new PointV { Values = l });
			}
			g.Series.Add(s);
			g.Draw();
		}
		
//		[Test]
//		public void b()
//		{
//			var x = new SuperReportImage();
//			var i = x.CreateGraph(new LineGraphType(1, 2), "0", "", "", "", "", "", "1113,3101,3238,3241,1149,1161,1215,1225,1543,1551,1575,1577,1620,1622,509,1703,1752,1790,911,1889,2059,1053,2238,2256,2258,2260,2275,1096,2614,2663,2759,2775,1111", 47, "2012-02-01", "2016-03-01", "Measure 1", "Measure 2", null);
//			using (var d = new Form()) {
//				d.Size = new Size(1000, 600);
//				
//				var p = new PictureBox();
//				p.Image = i.objBitmap;
//				p.Dock = DockStyle.Fill;
//				
//				d.Controls.Add(p);
//				d.ShowDialog();
//			}
//		}
//		
//		[Test]
//		public void c()
//		{
//			var x = new SuperReportImage();
//			var i = x.CreateGraph(new BoxPlotMinMaxGraphType(), "0", "", "", "", "", "", "1113,3101,3238,3241,1149,1161,1215,1225,1543,1551,1575,1577,1620,1622,509,1703,1752,1790,911,1889,2059,1053,2238,2256,2258,2260,2275,1096,2614,2663,2759,2775,1111", 47, "2012-02-01", "2016-03-01", "Measure 1", "Measure 2", null);
//			using (var d = new Form()) {
//				d.Size = new Size(1000, 600);
//				
//				var p = new PictureBox();
//				p.Image = i.objBitmap;
//				p.Dock = DockStyle.Fill;
//				
//				d.Controls.Add(p);
//				d.ShowDialog();
//			}
//		}
	}
}
