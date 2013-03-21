//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Drawing;
using System.Windows.Forms;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;
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
		
//		[Test]
//		public void a()
//		{
//			var ff = GraphFactory.CreateFactory(
//				false, 
//				new SqlAnswerRepository(),
//				new ReportRepositoryStub(),
//				new ProjectRepositoryStub(),
//				new OptionRepositoryStub(),
//				new DepartmentRepositoryStub(),
//				new QuestionRepositoryStub(),
//				new IndexRepositoryStub()
//			);
//			g = ff.CreateGraph(null, 14, 1, 3101, 8, 2012, 2013, 10, 10, 1, 1, 1, false, "BoxPlot", 550, 440, "#FFFFFF", 1, 1, 1, "", new object(), 0);
//		}
//		
//		[Test]
//		public void b()
//		{
//			var ff = GraphFactory.CreateFactory(
//				false, 
//				new AnswerRepositoryStub(),
//				new ReportRepositoryStub(),
//				new ProjectRepositoryStub(),
//				new OptionRepositoryStub(),
//				new DepartmentRepositoryStub(),
//				new QuestionRepositoryStub(),
//				new IndexRepositoryStub()
//			);
//			string s = ff.CreateGraph2(null, 14, 2, 3101, 8, 2012, 2013, 0, 10, 0, 0, 7, false, "LinePlot", 550, 440, "#FFFFFF", 2, 658, 101, "0,1101,1093", null, 0);
//			Console.WriteLine(s);
//		}
//		
//		[Test]
//		public void bq()
//		{
//			var ff = GraphFactory.CreateFactory(
//				false, 
//				new SqlAnswerRepository(),
//				new SqlReportRepository(),
//				new SqlProjectRepository(),
//				new SqlOptionRepository(),
//				new SqlDepartmentRepository(),
//				new SqlQuestionRepository(),
//				new SqlIndexRepository()
//			);
//			string s = ff.CreateGraph2(null, 14, 2, 3101, 8, 2012, 2013, 0, 10, 0, 0, 7, false, "LinePlot", 550, 440, "#FFFFFF", 2, 658, 101, "0,1101,1093", null, 0);
//			Console.WriteLine(s);
//		}
//		
//		[Test]
//		public void TestHasAnswerKey()
//		{
//			var ff = GraphFactory.CreateFactory(
//				true, 
//				new AnswerRepositoryStub(),
//				new ReportRepositoryStub(),
//				new ProjectRepositoryStub(),
//				new OptionRepositoryStub(),
//				new DepartmentRepositoryStub(),
//				new QuestionRepositoryStub(),
//				new IndexRepositoryStub()
//			);
//			g = ff.CreateGraph("", 1, 1, 1, 8, 2011, 2012, 10, 10, 1, 1, 1, false, "BoxPlot", 550, 440, "#FFFFFF", 1, 1, 1, "", new object(), 0);
//		}
//		
//		[Test]
//		public void TestHasNoAnswerKey()
//		{
//			var ff = GraphFactory.CreateFactory(
//				false, 
//				new AnswerRepositoryStub(),
//				new ReportRepositoryStub(),
//				new ProjectRepositoryStub(),
//				new OptionRepositoryStub(),
//				new DepartmentRepositoryStub(),
//				new QuestionRepositoryStub(),
//				new IndexRepositoryStub()
//			);
//			g = ff.CreateGraph("", 1, 1, 1, 8, 2011, 2012, 10, 10, 1, 1, 1, false, "BoxPlot", 550, 440, "#FFFFFF", 1, 1, 1, "", new object(), 0);
//		}
	}
}
