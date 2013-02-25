//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HW.Core;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Tests.Models
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
			g.Type = new BoxPlotGraphType();
			g.setMinMax(0, 100);
			var r =  new Random();
			for (int i = 0; i < 10; i++) {
				var s = new Series { Description = "Series" + i.ToString() };
				var l = new List<double>();
				for (int j = 0; j < 10; j++) {
					l.Add((double)r.Next(1, 100));
				}
				s.Points.Add(new PointV { Values = new HWList(l) });
				g.Series.Add(s);
			}
			g.Draw();
		}
	}
}
