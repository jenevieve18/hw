/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:04 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using NUnit.Framework;

namespace HW.Core.Tests.Helpers
{
	[TestFixture]
	public class ExporterTests
	{
		ReportPart r;
		IAdmin x;
//		ReportService service = new ReportService(
//			new SqlAnswerRepository(),
//			new SqlReportRepository(),
//			new SqlProjectRepository(),
//			new SqlOptionRepository(),
//			new SqlDepartmentRepository(),
//			new SqlQuestionRepository(),
//			new SqlIndexRepository(),
//			new SqlSponsorRepository(),
//			new SqlSponsorAdminRepository()
//		);
		ReportService service = new ReportService();
		
		[SetUpAttribute]
		public void Setup()
		{
			x = service.ReadSponsor(101);
			r = new SqlReportRepository().ReadReportPart(14, 1);
		}
		
		[Test]
		[Ignore("Finding a way to test without the fetch of GRP image from website.")]
		public void TestExcelExporter()
		{
//			ExcelExporter e = new ExcelExporter(service, false, true, null, 550, 440, "#efefef", r, null);
//			using (FileStream f = new FileStream(@"test.xlsx", FileMode.Create)) {
//				MemoryStream s = e.Export(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.Line, "http://localhost:3428/", x.MinUserCountToDisclose, 3, 3) as MemoryStream;
//				s.WriteTo(f);
//			}
		}
		
		[Test]
		[Ignore("Finding a way to test without the fetch of GRP image from website.")]
		public void TestExcelExporter2()
		{
//			var parts = service.FindByProjectAndLanguage(2643, 1);
//			ExcelExporter e = new ExcelExporter(service, false, true, null, 550, 440, "#efefef", parts, null);
//			using (FileStream f = new FileStream(@"test.xlsx", FileMode.Create)) {
//				MemoryStream s = e.Export2(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.Line, "http://localhost:3428/", x.MinUserCountToDisclose, 3, 3) as MemoryStream;
//				s.WriteTo(f);
//			}
		}
		
		[Test]
		[Ignore("Finding a way to test without the fetch of GRP image from website.")]
		public void TestCsvExporter()
		{
//			CsvExporter e = new CsvExporter(service, false, true, null, 550, 440, "#efefef", r, null);
//			using (FileStream f = new FileStream(@"test.csv", FileMode.Create)) {
//				MemoryStream s = e.Export(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.Line, "http://localhost:3428/", x.MinUserCountToDisclose, 3, 3) as MemoryStream;
//				s.WriteTo(f);
//			}
		}
		
		[Test]
		[Ignore("Finding a way to test without the fetch of GRP image from website.")]
		public void TestSpreadsheetDocumentExporter2()
		{
//			SpreadsheetDocumentExporter2 e = new SpreadsheetDocumentExporter2(r);
//			using (FileStream f = new FileStream(@"test.xlsx", FileMode.Create, FileAccess.Write)) {
//				(e.Export(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.Line, "http://localhost:3428/", x.MinUserCountToDisclose, 3, 3) as MemoryStream).WriteTo(f);
//			}
		}
		
		[Test]
		[Ignore("Finding a way to test without the fetch of GRP image from website.")]
		public void TestPresentationDocumentExporter()
		{
//			PresentationDocumentExporter e = new PresentationDocumentExporter(r);
//			using (FileStream f = new FileStream(@"test.pptx", FileMode.Create, FileAccess.Write)) {
//				MemoryStream s = e.Export(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.Line, "http://localhost:3428/", x.MinUserCountToDisclose, 3, 3) as MemoryStream;
//				s.WriteTo(f);
//			}
		}
	}
}
