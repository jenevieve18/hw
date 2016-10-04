// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Windows.Forms;
using HW.Core.Helpers;
using HW.Core.Models;
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
		public void TestGetWeightedQuestionOptionReportPartGraph2()
		{
//			g = f.CreateGraph(
//				null,
//				new SqlReportRepository().ReadReportPart(14, 2),
//				2,
//				2643,
//				2012,
//				2013,
//				7,
//				true,
//				0,
//				550,
//				440,
//				"#EFEFEF",
//				0,
//				514,
//				83,
//				"0",
//				null,
//				0,
//				10,
//				9,
//				9
//			);
			
			var reportPart = new SqlReportRepository().ReadReportPart(14, 2);
			var p = new {
				reportPart = reportPart,
				langID = 2,
				point = 0,
				hasGrouping = true,
				sponsorID = 83,
				sponsorAdminID = 514,
				sponsorMinUserCountToDisclose = 10,
				departmentIDs = "0",
				grouping = 0,
				groupBy = 0,
				plot = 0,
				cx = reportPart.Components.Capacity,
				projectRoundUnit = new SqlProjectRepository().ReadRoundUnit(2643),
				dateFrom = new DateTime(2012, 9, 1),
				dateTo = new DateTime(2013, 9, 1),
			};
			
			g = f.GetWeightedQuestionOptionReportPartGraph(
				p.reportPart,
				p.langID,
				p.point,
				p.hasGrouping,
				p.sponsorID,
				p.sponsorMinUserCountToDisclose,
				p.departmentIDs,
				p.projectRoundUnit.Id,
				p.sponsorAdminID,
				p.grouping,
				p.groupBy != 0 ? p.groupBy : GroupBy.TwoWeeksStartWithOdd,
				p.plot,
				p.cx,
				p.projectRoundUnit.SortString,
				p.dateFrom,
				p.dateTo
			);
		}
		
		[TestAttribute]
		public void TestGetWeightedQuestionOptionReportPartGraph()
		{
			var s = new SponsorService();
			
			var sponsorAdmin = s.ReadSponsorAdmin(514);
			
			var sponsor = s.ReadSponsor(83);
			
			int langID = 2;
			var reportPart = new SqlReportRepository().ReadReportPart(14, langID);
			
			var projectRoundUnit = new SqlProjectRepository().ReadRoundUnit(2643);
			
			var dateFrom = new DateTime(2012, 9, 1);
			var dateTo = new DateTime(2013, 9, 1);
			
			g = f.CreateGraph2(
				reportPart,
				projectRoundUnit,
				langID,
				sponsorAdmin,
				sponsor,
				dateFrom,
				dateTo,
				GroupBy.TwoWeeksStartWithEven,
				true,
				0,
				Grouping.None,
				"0",
				0
			);
		}
		
		[TestAttribute]
		public void TestGetIndexReportPartGraph()
		{
			var sponsorService = new SponsorService();
			var reportService = new ReportService3();
			
			var sponsorAdmin = sponsorService.ReadSponsorAdmin(-1);
			
			var sponsor = sponsorService.ReadSponsor(1);
			
			int langID = 2;
//			var reportPart = new SqlReportRepository().ReadReportPart(114, langID);
			var reportPart = reportService.ReadReportPart(114);
			reportPart.SelectedReportPartLangID = langID;
			
			var projectRoundUnit = new SqlProjectRepository().ReadRoundUnit(3476);
			
			var dateFrom = new DateTime(2015, 9, 1);
			var dateTo = new DateTime(2016, 9, 1);
			
			g = f.CreateGraph2(
				reportPart,
				projectRoundUnit,
				langID,
				sponsorAdmin,
				sponsor,
				dateFrom,
				dateTo,
				GroupBy.TwoWeeksStartWithEven,
				true,
				PlotType.Bar,
				Grouping.None,
				"0",
				0
			);
		}
		
		[TestAttribute]
		public void TestGetIndexReportPartGraph2a()
		{
			var sponsorService = new SponsorService();
			var reportService = new ReportService3();
			var projectService = new ProjectService();
			
			var sponsorAdmin = sponsorService.ReadSponsorAdmin(-1);
			
			var sponsor = sponsorService.ReadSponsor(1);
			
			int langID = 2;
			
			var reportPart2 = reportService.ReadReportPart(114);
			reportPart2.SelectedReportPartLangID = langID;
			
			var projectRoundUnit = new SqlProjectRepository().ReadRoundUnit(3476);
			var projectRoundUnit2 = projectService.ReadProjectRoundUnit(3476);
			
			var dateFrom = new DateTime(2015, 9, 1);
			var dateTo = new DateTime(2016, 9, 1);
			
			g = f.GetIndexReportPartGraph2(
				reportPart2,
				langID,
				0,
				true,
				sponsorAdmin,
				sponsor,
				"0,1251",
				projectRoundUnit,
				Grouping.None,
				GroupBy.TwoWeeksStartWithEven,
				PlotType.Bar,
				dateFrom,
				dateTo
			);
		}
		
		[Test]
		public void TestGetIndexReportPartGraph2()
		{
			g = f.CreateGraph(
				null,
				new SqlReportRepository().ReadReportPart(114, 2),
				2,
				3476,
				new DateTime(2015, 9, 1),
				new DateTime(2016, 9, 1),
				7,
				true,
				0,
				550,
				440,
				"#EFEFEF",
				0,
				-1,
				1,
				"0",
				null,
				0,
				10
			);
		}
		
//		[TestAttribute]
//		public void TestGetIndexReportPartGraph2b()
//		{
//			var s = new SponsorService();
//			
//			var sponsorAdmin = s.ReadSponsorAdmin(-1);
//			
//			var sponsor = s.ReadSponsor(1);
//			
//			int langID = 2;
//			var reportPart = new SqlReportRepository().ReadReportPart(114, langID);
//			
//			var projectRoundUnit = new SqlProjectRepository().ReadRoundUnit(3476);
//			
//			var dateFrom = new DateTime(2015, 9, 1);
//			var dateTo = new DateTime(2016, 9, 1);
//			
//			g = f.GetIndexReportPartGraph2(
//				reportPart,
//				projectRoundUnit,
//				langID,
//				dateFrom,
//				dateTo
//			);
//		}
		
		[TearDownAttribute]
		public void Teardown()
		{
			var form = new Form();
			form.Size = new System.Drawing.Size(900, 600);

			var p = new PictureBox();
			p.Image = g.objBitmap;
			p.Dock = DockStyle.Fill;

			form.Controls.Add(p);
			form.ShowDialog();
		}
	}
}
