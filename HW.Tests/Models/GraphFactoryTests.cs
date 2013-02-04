//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Drawing;
using System.Windows.Forms;
using HW.Core;
using NUnit.Framework;

namespace HW.Tests.Models
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
		
		[TearDown]
		public void Teardown()
		{
			p.Image = g.objBitmap;
			f.Controls.Add(p);
			f.ShowDialog();
		}
		
		[Test]
		public void TestHasAnswerKey()
		{
			var ff = GraphFactory.CreateFactory(
				true, 
				new AnswerRepositoryStub(),
				new ReportRepositoryStub(),
				new ProjectRepositoryStub(),
				new OptionRepositoryStub(),
				new DepartmentRepositoryStub(),
				new QuestionRepositoryStub(),
				new IndexRepositoryStub()
			);
			g = ff.CreateGraph("", 1, 1, 1, 8, 2011, 2012, 10, 10, 1, 1, 1, false, true, "BoxPlot", 550, 440, "#FFFFFF", 1, 1, 1, "", new object());
		}
		
		[Test]
		public void TestHasNoAnswerKey()
		{
			var ff = GraphFactory.CreateFactory(
				false, 
				new AnswerRepositoryStub(),
				new ReportRepositoryStub(),
				new ProjectRepositoryStub(),
				new OptionRepositoryStub(),
				new DepartmentRepositoryStub(),
				new QuestionRepositoryStub(),
				new IndexRepositoryStub()
			);
			g = ff.CreateGraph("", 1, 1, 1, 8, 2011, 2012, 10, 10, 1, 1, 1, false, true, "BoxPlot", 550, 440, "#FFFFFF", 1, 1, 1, "", new object());
		}
	}
}
