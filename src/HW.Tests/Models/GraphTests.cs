//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Drawing;
using System.Windows.Forms;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Tests.Models
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
	}
}
