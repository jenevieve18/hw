// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Diagnostics;
using System.IO;
using HW.Core.Models;
using HW.Core.Services;
using HW.Core.Util.Exporters;
using NUnit.Framework;

namespace HW.Grp.Tests.Util
{
	[TestFixture]
	public class ExportTests
	{
		ReportService service = new ReportService();
		
		[Test]
		public void TestEverythingExcel()
		{
			int groupBy = 7;
			
			DateTime dateFrom = new DateTime(2012, 1, 1);
			DateTime dateTo = new DateTime(2013, 1, 1);
			
			int langID = 2;

			int reportPartID = 14;
            int projectRoundUnitID = 2643;
			
			int grouping = 2;
			int sponsorAdminID = -1;
			int sponsorID = 83;
			string departmentIDs = "0,927,929,930";
			int plot = PlotType.Line;
//			string type = "xls";
			
			bool hasGrouping = true;
			
			bool hasAnswerKey = false;
			
			IAdmin sponsor = service.ReadSponsor(sponsorID);

			var reportPart = service.ReadReportPart(reportPartID);
            reportPart.SelectedReportPartLangID = langID;
			
			ProjectRoundUnit projectRoundUnit = service.ReadProjectRoundUnit(projectRoundUnitID);
			var sponsorAdmin = service.ReadSponsorAdmin(sponsorAdminID);
			
			var exporter = new ExcelStatsExporter(service, hasAnswerKey, hasGrouping, reportPart);
			exporter.CellWrite += delegate(object sender2, ExcelCellEventArgs e2) {
				e2.ExcelCell.Value = R.Str(langID, e2.ValueKey, "");
				e2.Writer.WriteCell(e2.ExcelCell);
			};
			
			string url = "http://localhost:19882/reportImage.aspx?LangID=2&FY=2012&TY=2013&FM=1&TM=1&SAID=-1&SID=83&GB=7&RPID=14&PRUID=2643&GID=0,928,929,930&GRPNG=2&PLOT=7";
			using (var o = exporter.Export(url, langID, projectRoundUnit, dateFrom, dateTo, groupBy, plot, grouping, sponsorAdmin, sponsor as Sponsor, departmentIDs) as MemoryStream) {
				using (var f = new FileStream("test.xls", FileMode.Create, System.IO.FileAccess.Write)) {
					o.WriteTo(f);
				}
			}
			
			Process.Start("test.xls");
		}
		
		[Test]
		public void TestEverythingExcelX()
		{
			int groupBy = 7;
			
			DateTime dateFrom = new DateTime(2012, 1, 1);
			DateTime dateTo = new DateTime(2012, 1, 1);
			
			int langID = 2;

//			int reportPartID = 14;
            int projectRoundUnitID = 2643;
			
			int grouping = 2;
			int sponsorAdminID = -1;
			int sponsorID = 83;
//			string departmentIDs = "0,928,929,930";
			string departmentIDs = "0,928";
			int plot = PlotType.Verbose;
//			string type = "xls";
			
			bool hasGrouping = true;
			
			bool hasAnswerKey = false;
			
			IAdmin sponsor = service.ReadSponsor(sponsorID);

			var reportParts = service.FindByProjectAndLanguage(projectRoundUnitID, langID);
			
			ProjectRoundUnit projectRoundUnit = service.ReadProjectRoundUnit(projectRoundUnitID);
			var sponsorAdmin = service.ReadSponsorAdmin(sponsorAdminID);
			
			var exporter = new ExcelStatsExporter(service, hasAnswerKey, hasGrouping, reportParts);
			exporter.CellWrite += delegate(object sender2, ExcelCellEventArgs e2) {
				e2.ExcelCell.Value = R.Str(langID, e2.ValueKey, "");
				e2.Writer.WriteCell(e2.ExcelCell);
			};
			
			using (var o = exporter.ExportAll(langID, projectRoundUnit, dateFrom, dateTo, groupBy, plot, grouping, sponsorAdmin, sponsor as Sponsor, departmentIDs) as MemoryStream) {
				using (var f = new FileStream("test.xls", FileMode.Create, System.IO.FileAccess.Write)) {
					o.WriteTo(f);
				}
			}
			
			Process.Start("test.xls");
		}
	}
}
