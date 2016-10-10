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
		public void TestGetWeightedQuestionOptionReportPartGraph()
		{
			var reportPart = new SqlReportRepository().ReadReportPart(14, 2);
			int langID = 2;
			int point = 0;
			bool hasGrouping = true;
			var sponsor = new SqlSponsorRepository().Read(83);
			var sponsorAdmin = new SqlSponsorAdminRepository().Read(514);
			string departmentIDs = "0";
			int grouping = Grouping.None;
			int groupBy = GroupBy.TwoWeeksStartWithEven;
			int cx = reportPart.Components.Capacity;
			var projectRoundUnit = new SqlProjectRepository().ReadRoundUnit(2643);
			var dateFrom = new DateTime(2012, 9, 1);
			var dateTo = new DateTime(2013, 9, 1);
			
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
		
		[TestAttribute]
		public void TestGetIndexReportPartGraph()
		{
			var sponsorService = new SponsorService();
//			var reportService = new ReportService3();
			var reportService = new ReportService();
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
			
			g = f.GetIndexReportPartGraph(
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
		public void TestGetQuestionReportPartGraph()
		{
			var sponsorService = new SponsorService();
//			var reportService = new ReportService3();
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
