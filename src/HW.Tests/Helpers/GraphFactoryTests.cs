using System;
using System.Drawing;
using System.Windows.Forms;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using NUnit.Framework;

namespace HW.Tests.Helpers
{
	[TestFixture]
	public class GraphFactoryTests
	{
		Form f;
		PictureBox p;
		ExtendedGraph g;
		
		[SetUp]
		public void Setup()
		{
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
		
		[Test]
		public void TestMethod()
		{
		}
		
		[TearDown]
		public void Teardown()
		{
			p.Image = g.objBitmap;
			f.Controls.Add(p);
//			f.ShowDialog();
		}
	}
}
