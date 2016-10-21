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
using HW.Core.Util.Graphs;
using NUnit.Framework;

namespace HW.Grp.Tests.Util
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
		public void TestGetWeightedQuestionOptionReportPartGraph()
		{
			var dateFrom = new DateTime(2012, 9, 1);
			var dateTo = new DateTime(2013, 9, 1);
			
			int langID = 2;
			
			var reportPart = new SqlReportRepository().ReadReportPart(14, 2);
			var projectRoundUnit = new SqlProjectRepository().ReadRoundUnit(2643);
			
			bool hasGrouping = true;
			
			var sponsor = new SqlSponsorRepository().Read(83);
			var sponsorAdmin = new SqlSponsorAdminRepository().Read(514);
			
			int groupBy = GroupBy.TwoWeeksStartWithEven;
			int grouping = Grouping.None;
			
			string departmentIDs = "0";
			
			int cx = reportPart.Components.Capacity;
			
			int point = 0;
			
			g = f.GetWeightedQuestionOptionReportPartGraph(
				reportPart,
				langID,
				point,
				hasGrouping,
				sponsorAdmin,
				sponsor,
				departmentIDs,
				projectRoundUnit,
				grouping,
				groupBy != 0 ? groupBy : GroupBy.TwoWeeksStartWithOdd,
				PlotType.Bar,
				dateFrom,
				dateTo
			);
		}
		
		[Test]
		public void TestGetWeightedQuestionOptionReportPartGraph2()
		{
			var dateFrom = new DateTime(2012, 10, 1);
			var dateTo = new DateTime(2013, 10, 1);
			
			int langID = 2;

			int reportPartID = 14;
			int projectRoundUnitID = 2643;
			
			bool hasGrouping = true;
			
			int plot = 0;
			
			int groupBy = GroupBy.TwoWeeksStartWithEven;
			int grouping = Grouping.BackgroundVariable;
			
			int sponsorAdminID = 514;
			int sponsorID = 83;
			
			string departmentIDs = "0,2,7";
			
			int point = 0;
			
			ReportService service = new ReportService();
			
			var reportPart = service.ReadReportPart(reportPartID);
			reportPart.SelectedReportPartLangID = langID;

			var projectRoundUnit = service.ReadProjectRoundUnit(projectRoundUnitID);
			var sponsorAdmin = service.ReadSponsorAdmin(sponsorAdminID);
			
			var sponsor = service.ReadSponsor(sponsorID) as Sponsor;
			g = f.CreateGraph(reportPart, projectRoundUnit, langID, sponsorAdmin, sponsor, dateFrom, dateTo, groupBy, hasGrouping, plot, grouping, departmentIDs, point);
		}
		
		[TestAttribute]
		public void TestGetIndexReportPartGraph()
		{
			var sponsorService = new SponsorService();
			var reportService = new ReportService();
			var projectService = new ProjectService();
			
			var sponsorAdmin = sponsorService.ReadSponsorAdmin(-1);
			
			var sponsor = sponsorService.ReadSponsor(1);
			
			int langID = 2;
			
			var reportPart = reportService.ReadReportPart(114);
			reportPart.SelectedReportPartLangID = langID;
			
//			var projectRoundUnit = new SqlProjectRepository().ReadRoundUnit(3476);
			var projectRoundUnit = projectService.ReadProjectRoundUnit(3476);
			
			var dateFrom = new DateTime(2015, 10, 1);
			var dateTo = new DateTime(2016, 10, 1);
			
			g = f.GetIndexReportPartGraph(
				reportPart,
				langID,
				0,
				true,
				sponsorAdmin,
				sponsor,
				"0,1251",
				projectRoundUnit,
				Grouping.None,
				GroupBy.OneYear,
				PlotType.Bar,
				dateFrom,
				dateTo
			);
		}
		
		[TestAttribute]
		public void TestGetIndexReportPartGraph2()
		{
			var sponsorService = new SponsorService();
			var reportService = new ReportService();
			var projectService = new ProjectService();
			
			var sponsor = sponsorService.ReadSponsor(1);
			var sponsorAdmin = sponsorService.ReadSponsorAdmin(-1);
			
			int langID = 2;
			
			var reportPart = reportService.ReadReportPart(114);
			reportPart.SelectedReportPartLangID = langID;
			
			var projectRoundUnit = projectService.ReadProjectRoundUnit(3476);
			
			var dateFrom = new DateTime(2015, 10, 1);
			var dateTo = new DateTime(2016, 10, 1);
			
			g = f.lalala(
				reportPart,
				langID,
				0,
				sponsorAdmin,
				sponsor,
				"0",
				projectRoundUnit,
				Grouping.None,
				GroupBy.OneYear,
				PlotType.BoxPlot,
				dateFrom,
				dateTo
			);
		}
		
		[Test]
		public void TestGetQuestionReportPartGraph()
		{
			var sponsorService = new SponsorService();
			var reportService = new ReportService();
			var projectService = new ProjectService();
			
			var sponsorAdmin = sponsorService.ReadSponsorAdmin(-1);
			var sponsor = sponsorService.ReadSponsor(1);
			
			int langID = 2;
			
			var reportPart = reportService.ReadReportPart(117);
			reportPart.SelectedReportPartLangID = langID;
			
			var projectRoundUnit = projectService.ReadProjectRoundUnit(3476);
			
			var dateFrom = new DateTime(2015, 9, 1);
			var dateTo = new DateTime(2016, 9, 1);
			
			g = f.GetQuestionReportPartGraph(
				reportPart,
				projectRoundUnit,
				langID,
				dateFrom,
				dateTo,
				PlotType.Bar
			);
		}
		
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
