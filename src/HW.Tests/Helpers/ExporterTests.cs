﻿using System;
using System.Linq;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Wordprocessing;
using W = DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using M = DocumentFormat.OpenXml.Math;
using Ovml = DocumentFormat.OpenXml.Vml.Office;
using V = DocumentFormat.OpenXml.Vml;
using A = DocumentFormat.OpenXml.Drawing;
using NUnit.Framework;

namespace HW.Tests.Helpers
{
	[TestFixture]
	public class ExporterTests
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
