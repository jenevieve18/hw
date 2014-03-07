using System;
using System.IO;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using NUnit.Framework;

namespace HW.Tests.Helpers
{
	[TestFixture]
	public class DocXExporterTests
	{
		ReportPart r;
		Sponsor x;
		ReportService service = new ReportService(
			new SqlAnswerRepository(),
			new SqlReportRepository(),
			new SqlProjectRepository(),
			new SqlOptionRepository(),
			new SqlDepartmentRepository(),
			new SqlQuestionRepository(),
			new SqlIndexRepository(),
			new SqlSponsorRepository()
		);
		DocXExporter e;
		DocXExporter e2;
		
		[SetUpAttribute]
		public void Setup()
		{
			x = service.ReadSponsor(101);
			r = new SqlReportRepository().ReadReportPart(14, 1);
			e = new DocXExporter(r, "HW template for Word.docx");
			
			var parts = service.FindByProjectAndLanguage(2643, 1);
            e2 = new DocXExporter(service, parts, "HW template for Word.docx");
		}
		
		[Test]
		public void TestLinePlot()
		{
			using (FileStream f = new FileStream(@"C:\Users\ultra\Downloads\test.docx", FileMode.Create)) {
				MemoryStream s = e.Export(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", Plot.Line, "http://localhost:3428/", x.MinUserCountToDisclose) as MemoryStream;
				s.WriteTo(f);
			}
		}
		
		[Test]
		public void TestLinePlot2()
		{
			using (FileStream f = new FileStream(@"C:\Users\ultra\Downloads\test2.docx", FileMode.Create)) {
				MemoryStream s = e2.Export2(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", Plot.Line, "http://localhost:3428/", x.MinUserCountToDisclose) as MemoryStream;
				s.WriteTo(f);
			}
		}
	}
}
