using System;
using System.IO;
using DocumentFormat.OpenXml;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using NUnit.Framework;

namespace HW.Tests.Helpers
{
	[TestFixture]
	public class PresentationDocumentExporterTests
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
		PresentationDocumentExporter e;
		PresentationDocumentExporter e2;
		
		[SetUpAttribute]
		public void Setup()
		{
			x = service.ReadSponsor(101);
			r = new SqlReportRepository().ReadReportPart(14, 1);
			e = new PresentationDocumentExporter(r);
			
			var parts = service.FindByProjectAndLanguage(2643, 1);
			e2 = new PresentationDocumentExporter(service, parts);
		}
		
		[Test]
		public void TestPresentationDocumentExporter()
		{
			using (FileStream f = new FileStream(@"C:\Users\ultra\Downloads\test.pptx", FileMode.Create, FileAccess.Write)) {
				MemoryStream s = e.Export(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.Line, "http://localhost:3428/", x.MinUserCountToDisclose) as MemoryStream;
				s.WriteTo(f);
			}
		}
		
		[Test]
		public void TestPresentationDocumentExporter2()
		{
			using (FileStream f = new FileStream(@"C:\Users\ultra\Downloads\test2.pptx", FileMode.Create, FileAccess.Write)) {
				MemoryStream s = e2.Export2(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.Line, "http://localhost:3428/", x.MinUserCountToDisclose) as MemoryStream;
				s.WriteTo(f);
			}
		}
	}
}
