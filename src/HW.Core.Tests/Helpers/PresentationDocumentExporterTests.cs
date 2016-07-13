/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using NUnit.Framework;

namespace HW.Core.Tests.Helpers
{
	[TestFixture]
	public class PresentationDocumentExporterTests
	{
		ReportPart r;
		ISponsor x;
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
		[Ignore("Finding a way to test without the fetch of GRP image from website.")]
		public void TestPresentationDocumentExporter()
		{
//			using (FileStream f = new FileStream(@"test.pptx", FileMode.Create, FileAccess.Write)) {
//				MemoryStream s = e.Export(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.Line, "http://localhost:3428/", x.MinUserCountToDisclose, 3, 3) as MemoryStream;
//				s.WriteTo(f);
//			}
		}
		
		[Test]
		[Ignore("Finding a way to test without the fetch of GRP image from website.")]
		public void TestPresentationDocumentExporter2()
		{
//			using (FileStream f = new FileStream(@"test2.pptx", FileMode.Create, FileAccess.Write)) {
//				MemoryStream s = e2.Export2(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.Line, "http://localhost:3428/", x.MinUserCountToDisclose, 3, 3) as MemoryStream;
//				s.WriteTo(f);
//			}
		}
	}
}
