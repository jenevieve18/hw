// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

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
	public class PresentationDocumentExporterTests
	{
		ReportPart r;
		ReportService service = new ReportService(
			new SqlAnswerRepository(),
			new SqlReportRepository(),
			new SqlProjectRepository(),
			new SqlOptionRepository(),
			new SqlDepartmentRepository(),
			new SqlQuestionRepository(),
			new SqlIndexRepository()
		);
		ExcelExporter e;
		ExcelExporter e2;
		
		[SetUpAttribute]
		public void Setup()
		{
			r = new SqlReportRepository().ReadReportPart(14, 1);
			e = new ExcelExporter(service, false, true, null, 550, 440, "#efefef", r, null);
			
			var parts = service.FindByProjectAndLanguage(2643, 1);
			e2 = new ExcelExporter(service, false, true, null, 550, 440, "#efefef", parts, null);
		}
		
		[Test]
		public void TestPresentationDocumentExporter()
		{
			PresentationDocumentExporter e = new PresentationDocumentExporter(r);
			using (FileStream f = new FileStream(@"C:\Users\ultra\Downloads\test.pptx", FileMode.Create, FileAccess.Write)) {
				MemoryStream s = e.Export(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", "LinePlot", "http://localhost:3428/") as MemoryStream;
				s.WriteTo(f);
			}
		}
		
		[Test]
		public void TestPresentationDocumentExporter2()
		{
			PresentationDocumentExporter e = new PresentationDocumentExporter(r);
			using (FileStream f = new FileStream(@"C:\Users\ultra\Downloads\test2.pptx", FileMode.Create, FileAccess.Write)) {
				MemoryStream s = e2.Export2(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", "LinePlot", "http://localhost:3428/") as MemoryStream;
				s.WriteTo(f);
			}
		}
	}
}
