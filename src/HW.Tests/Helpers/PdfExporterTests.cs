using System;
using System.Diagnostics;
using System.IO;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NUnit.Framework;

namespace HW.Tests.Helpers
{
	[TestFixture]
	public class PdfExporterTests
	{
		ReportPart r;
		PdfExporter e;
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
		SqlExerciseRepository er;
		
		[SetUp]
		public void Setup()
		{
			x = service.ReadSponsor(101);
			r = new SqlReportRepository().ReadReportPart(14, 1);
			e = new PdfExporter(r);
			er = new SqlExerciseRepository();
		}
		
		[Test]
		public void TestExportExercise()
		{
			string dir = @"pdf";
			CreateDirectory(dir);
			var evl = er.ReadExerciseVariant(1);
			string file = evl.Variant.Exercise.SelectedLanguage.ExerciseName + ".pdf";
			e.Save(Path.Combine(dir, file), e.Export(evl));
			
//			Process.Start(Path.Combine(dir, file));
		}
		
		[Test]
		public void TestExportEnglishExercises()
		{
			string dir = @"pdf\en";
			CreateDirectory(dir);
			foreach (var evl in er.FindExerciseVariants(1)) {
				string file = evl.Variant.Exercise.SelectedLanguage.ExerciseName + ".pdf";
				Console.WriteLine(file);
				e.Save(Path.Combine(dir, file), e.Export(evl));
			}
		}
		
		[Test]
		public void TestExportSwedishExercises()
		{
			string dir = @"pdf\sv";
			CreateDirectory(dir);
			foreach (var evl in er.FindExerciseVariants(0)) {
				string file = evl.Variant.Exercise.SelectedLanguage.ExerciseName + ".pdf";
				Console.WriteLine(file);
				e.Save(Path.Combine(dir, file), e.Export(evl));
			}
		}
		
		void CreateDirectory(string dir)
		{
			if (!Directory.Exists(dir)) {
				Directory.CreateDirectory(dir);
			}
		}
		
		[Test]
		[Ignore("Finding a way to test without the fetch of GRP image from website.")]
		public void TestPdfExporter()
		{
//			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
//				MemoryStream s = e.Export(7, 2012, 2013, 1, 2643, 2, 514, 83, "0,923", PlotType.Line, "http://localhost:25555/", x.MinUserCountToDisclose, 3, 3) as MemoryStream;
//				s.WriteTo(f);
//			}
		}
		
		[Test]
		public void a()
		{
			Document doc = new Document(iTextSharp.text.PageSize.A4);
			FileStream file = new FileStream("test.pdf", FileMode.OpenOrCreate);
			PdfWriter writer = PdfWriter.GetInstance(doc, file);
			// calling PDFFooter class to Include in document
			writer.PageEvent = new PDFFooter();
			doc.Open();
			PdfPTable tab = new PdfPTable(3);
			PdfPCell cell = new PdfPCell(new Phrase("Header", new Font(Font.FontFamily.HELVETICA, 24F)));
			cell.Colspan = 3;
			cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
			//Style
			cell.BorderColor = new BaseColor(System.Drawing.Color.Red);
			cell.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
			cell.BorderWidthBottom = 3f;
			tab.AddCell(cell);
			//row 1
			tab.AddCell("R1C1");
			tab.AddCell("R1C2");
			tab.AddCell("R1C3");
			//row 2
			tab.AddCell("R2C1");
			tab.AddCell("R2C2");
			tab.AddCell("R2C3");
			cell = new PdfPCell();
			cell.Colspan = 3;
			iTextSharp.text.List pdfList = new iTextSharp.text.List(iTextSharp.text.List.UNORDERED);
			pdfList.Add(new iTextSharp.text.ListItem(new Phrase("Unorder List 1")));
			pdfList.Add("Unorder List 2");
			pdfList.Add("Unorder List 3");
			pdfList.Add("Unorder List 4");
			cell.AddElement(pdfList);
			tab.AddCell(cell);
			doc.Add(tab);
			doc.Close();
			file.Close();
		}
		
		public class PDFFooter : PdfPageEventHelper
		{
			public override void OnOpenDocument(PdfWriter writer, Document document)
			{
				base.OnOpenDocument(writer, document);
				PdfPTable tabFot = new PdfPTable(new float[] { 1F });
				tabFot.SpacingAfter = 10F;
				PdfPCell cell;
				tabFot.TotalWidth = 300F;
				cell = new PdfPCell(new Phrase("Header"));
				tabFot.AddCell(cell);
				tabFot.WriteSelectedRows(0, -1, 150, document.Top , writer.DirectContent);
			}

			// write on start of each page
			public override void OnStartPage(PdfWriter writer, Document document)
			{
				base.OnStartPage(writer, document);
			}

			// write on end of each page
			public override void OnEndPage(PdfWriter writer, Document document)
			{
				base.OnEndPage(writer, document);
				PdfPTable tabFot = new PdfPTable(new float[] { 1F });
				PdfPCell cell;
				tabFot.TotalWidth = 300F;
				cell = new PdfPCell(new Phrase("Footer"));
				tabFot.AddCell(cell);
				tabFot.WriteSelectedRows(0, -1, 150, document.Bottom, writer.DirectContent);
			}

			//write on close of document
			public override void OnCloseDocument(PdfWriter writer, Document document)
			{
				base.OnCloseDocument(writer, document);
			}
		}
	}
}
