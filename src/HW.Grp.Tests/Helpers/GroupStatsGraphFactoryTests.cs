// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Windows.Forms;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using NUnit.Framework;

namespace HW.Grp.Tests.Helpers
{
	[TestFixture]
	public class GroupStatsGraphFactoryTests
	{
		GroupStatsGraphFactory f = new GroupStatsGraphFactory(
			new SqlAnswerRepository(),
			new SqlReportRepository(),
			new SqlProjectRepository(),
			new SqlOptionRepository(),
			new SqlIndexRepository(),
			new SqlQuestionRepository(),
			new SqlDepartmentRepository()
		);
		ExtendedGraph g;
		
		[Test]
		public void TestMethod()
		{
			g = f.CreateGraph(
				null,
				new SqlReportRepository().ReadReportPart(14, 2),
				2,
				2643,
				2012,
				2013,
				7,
				true,
				0,
				550,
				440,
				"#EFEFEF",
				0,
				514,
				83,
				"0",
				null,
				0,
				10,
				9,
				9
			);
		}
		
		[TestAttribute]
		public void TestMethod2()
		{
			var s = new SponsorService();
			var sponsorAdmin = s.ReadSponsorAdmin(514);
			
			var dateFrom = new DateTime(2012, 9, 1);
			var dateTo = new DateTime(2013, 9, 1);
			
			g = f.CreateGraphXXX(
				new SqlReportRepository().ReadReportPart(14, 2),
				new SqlProjectRepository().ReadRoundUnit(2643),
				dateFrom, //2012,
				dateTo, //2013,
				7,
				true,
				0,
				0,
				sponsorAdmin,
				"0",
				0 //,
//				9,
//				9
			);
		}
		
		[TearDownAttribute]
		public void Teardown()
		{
			var x = new Form();
			x.Size = new System.Drawing.Size(900, 600);
			var p = new PictureBox();
			p.Image = g.objBitmap;
			p.Dock = DockStyle.Fill;
			x.Controls.Add(p);
			x.ShowDialog();
		}
	}
}
