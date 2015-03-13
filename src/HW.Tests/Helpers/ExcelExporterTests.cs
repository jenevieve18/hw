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
	public class ExcelExporterTests
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
		ExcelExporter e;
		ExcelExporter e2;
		
		[SetUpAttribute]
		public void Setup()
		{
			x = service.ReadSponsor(101);
			r = new SqlReportRepository().ReadReportPart(14, 1);
			e = new ExcelExporter(service, false, true, null, 550, 440, "#efefef", r, null);
			
			var parts = service.FindByProjectAndLanguage(2643, 1);
			e2 = new ExcelExporter(service, false, true, null, 550, 440, "#efefef", parts, null);
		}
		
		[Test]
		public void TestLinePlot()
		{
			using (FileStream f = new FileStream(@"C:\Users\ultra\Downloads\line.xlsx", FileMode.Create)) {
				MemoryStream s = e.Export(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.Line, "http://localhost:3428/", x.MinUserCountToDisclose, 3, 3) as MemoryStream;
				s.WriteTo(f);
			}
		}
		
		[Test]
		public void TestLinePlot2()
		{
			using (FileStream f = new FileStream(@"C:\Users\ultra\Downloads\line2.xlsx", FileMode.Create)) {
				MemoryStream s = e2.Export2(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.Line, "http://localhost:3428/", x.MinUserCountToDisclose, 3, 3) as MemoryStream;
				s.WriteTo(f);
			}
		}
		
		[Test]
		public void TestLineWithSD()
		{
			using (FileStream f = new FileStream(@"C:\Users\ultra\Downloads\linesd.xlsx", FileMode.Create)) {
				MemoryStream s = e.Export(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.LineSD, "http://localhost:3428/", x.MinUserCountToDisclose, 3, 3) as MemoryStream;
				s.WriteTo(f);
			}
		}
		
		[Test]
		public void TestLineWithSD2()
		{
			using (FileStream f = new FileStream(@"C:\Users\ultra\Downloads\linesd2.xlsx", FileMode.Create)) {
				MemoryStream s = e2.Export2(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.LineSD, "http://localhost:3428/", x.MinUserCountToDisclose, 3, 3) as MemoryStream;
				s.WriteTo(f);
			}
		}
		
		[Test]
		public void TestLinePlotWithCI()
		{
			using (FileStream f = new FileStream(@"C:\Users\ultra\Downloads\lineci.xlsx", FileMode.Create)) {
				MemoryStream s = e.Export(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.LineSDWithCI, "http://localhost:3428/", x.MinUserCountToDisclose, 3, 3) as MemoryStream;
				s.WriteTo(f);
			}
		}
		
		[Test]
		public void TestLinePlotWithCI2()
		{
			using (FileStream f = new FileStream(@"C:\Users\ultra\Downloads\lineci2.xlsx", FileMode.Create)) {
				MemoryStream s = e2.Export2(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.LineSDWithCI, "http://localhost:3428/", x.MinUserCountToDisclose, 3, 3) as MemoryStream;
				s.WriteTo(f);
			}
		}
		
		[Test]
		public void TestBoxPlot()
		{
			using (FileStream f = new FileStream(@"C:\Users\ultra\Downloads\boxplot.xlsx", FileMode.Create)) {
				MemoryStream s = e.Export(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.BoxPlotMinMax, "http://localhost:3428/", x.MinUserCountToDisclose, 3, 3) as MemoryStream;
				s.WriteTo(f);
			}
		}
		
		[Test]
		public void TestBoxPlot2()
		{
			using (FileStream f = new FileStream(@"C:\Users\ultra\Downloads\boxplot2.xlsx", FileMode.Create)) {
				MemoryStream s = e2.Export2(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.BoxPlotMinMax, "http://localhost:3428/", x.MinUserCountToDisclose, 3, 3) as MemoryStream;
				s.WriteTo(f);
			}
		}
		
//		[Test]
//		public void TestVerbose()
//		{
//			using (FileStream f = new FileStream(@"C:\Users\ultra\Downloads\verbose.xlsx", FileMode.Create)) {
//				MemoryStream s = e.Export(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923,925", PlotType.Verbose, "http://localhost:3428/", x.MinUserCountToDisclose) as MemoryStream;
//				s.WriteTo(f);
//			}
//		}
//		
//		[Test]
//		public void TestVerbose2()
//		{
//			using (FileStream f = new FileStream(@"C:\Users\ultra\Downloads\verbose2.xlsx", FileMode.Create)) {
//				MemoryStream s = e2.Export2(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923,925", PlotType.Verbose, "http://localhost:3428/", x.MinUserCountToDisclose) as MemoryStream;
//				s.WriteTo(f);
//			}
//		}
	}
}
