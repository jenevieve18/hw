using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HW.Core.Helpers
{
	public class PdfExporter : AbstractExporter
	{
		ReportPart r;
		IList<ReportPartLanguage> parts;
		ReportService service;
		
		public PdfExporter(ReportPart r)
		{
			this.r = r;
		}
		
		public PdfExporter(ReportService service, IList<ReportPartLanguage> parts)
		{
			this.service = service;
			this.parts = parts;
		}
		
		public override string Type {
			get { return "application/pdf"; }
		}
		
		public override string GetContentDisposition(string file)
		{
			return "";
		}
		
		public override string ContentDisposition2 {
			get { return ""; }
		}
		
		public void Save(string file, MemoryStream output)
		{
			byte[] content = output.ToArray();
			using (FileStream f = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None)) {
				f.Write(content, 0, content.Length);
			}
			output.Close();
		}
		
		public MemoryStream Export(ExerciseVariantLanguage evl)
		{
			Font h1 = FontFactory.GetFont(FontFactory.HELVETICA, 28, Font.NORMAL, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#1c73a8")));
			Font strong = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, Font.BOLD, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#666")));
			Font p = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#666")));
			
			MemoryStream output = new MemoryStream();
			Document doc = new Document();
			PdfWriter writer = PdfWriter.GetInstance(doc, output);
			writer.PageEvent = new PdfHeaderFooter(evl.Language.Id);
			
			doc.Open();
			
			Image jpg = Image.GetInstance(@"img/hwlogosmall.gif");
			doc.Add(jpg);
			
			var header = new Paragraph(evl.Variant.Exercise.FirstLanguage.ExerciseName, h1);
			header.SpacingAfter = 10;
			doc.Add(header);
			
			if (evl.Variant.Exercise.ReplacementHead != null) {
				var replacementHead = new Paragraph(evl.Variant.Exercise.ReplacementHead, strong);
				doc.Add(replacementHead);
			}
			
			if (evl.Content != null) {
				var content = CreateSimpleHTMLParagraph(evl.Content, p);
//				content.Font = p;
				doc.Add(content);
			}
			
			doc.Close();
			return output;
		}
		
		Paragraph CreateSimpleHTMLParagraph(string text, Font f)
		{
			Paragraph p = new Paragraph();
			using (StringReader r = new StringReader(text)) {
				List<IElement> elements = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(r, null);
				foreach (IElement e in elements) {
					p.Add(e);
					p.Add(new Paragraph());
				}
			}
			return p;
		}
		
		public override object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm)
		{
			Document doc = new Document();
			var output = new MemoryStream();
			PdfWriter writer = PdfWriter.GetInstance(doc, output);
			doc.Open();
			
			string url = GetUrl(path, langID, fy, ty, spons, sid, gb, r.Id, pruid, gid, grpng, plot, fm, tm);
			doc.Add(new Chunk(r.CurrentLanguage.Subject));
			iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(new Uri(url));
			jpg.ScaleToFit(500f, 500f);
			doc.Add(jpg);
			doc.Close();
			return output;
		}
		
		public override object Export2(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm)
		{
			Document doc = new Document();
			var output = new MemoryStream();
			PdfWriter writer = PdfWriter.GetInstance(doc, output);
			doc.Open();
			
			int i = 0;
			foreach (var p in parts) {
				string url = GetUrl(path, langID, fy, ty, spons, sid, gb, p.ReportPart.Id, pruid, gid, grpng, plot, fm, tm);
				ReportPart r = service.ReadReportPart(p.ReportPart.Id, langID);
				doc.Add(new Chunk(r.CurrentLanguage.Subject));
				iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(new Uri(url));
				jpg.ScaleToFit(500f, 500f);
				doc.Add(jpg);
				
				if (i++ < parts.Count - 1) {
					doc.NewPage();
				}
			}
			doc.Close();
			return output;
		}
	}
	
	public class PdfHeaderFooter : PdfPageEventHelper
	{
		int pageNumber = 1;
		
		public PdfHeaderFooter(int langID)
		{
			this.LangID = langID;
		}
		
		public override void OnOpenDocument(PdfWriter writer, Document document)
		{
			base.OnOpenDocument(writer, document);
//			PdfPTable table = new PdfPTable(new float[] { 1F });
//			table.TotalWidth = document.PageSize.Width - (document.LeftMargin + document.RightMargin);
//
//			Image jpg = Image.GetInstance(@"img/hwlogosmall.gif");
//
//			PdfPCell cell = new PdfPCell(jpg);
//			cell.Border = 0;
//			table.AddCell(cell);
//
//			table.WriteSelectedRows(0, -1, document.LeftMargin, document.Top, writer.DirectContent);
		}
		
		public override void OnChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title)
		{
			base.OnChapter(writer, document, paragraphPosition, title);
		}

		public override void OnStartPage(PdfWriter writer, Document document)
		{
			base.OnStartPage(writer, document);
		}
		
		public int LangID { get; set; }
		
		public override void OnEndPage(PdfWriter writer, Document document)
		{
			base.OnEndPage(writer, document);
			PdfPTable table = new PdfPTable(new float[] { 1F });
			table.TotalWidth = document.PageSize.Width;
			
			Font f = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#ffffff")));
			PdfPCell cell;
			if (pageNumber == 1) {
				string s = LangID == 1
					? "© Copyright. This exercise can not be copied or spread without written permission from the author/originator."
					: "© Copyright. Denna övning får inte kopieras eller spridas utan medgivande från upphovsmännen. info@healthwatch.se";
				cell = new PdfPCell(new Phrase(new Chunk(s, f)));
			} else {
				cell = new PdfPCell(new Phrase(new Chunk("© Copyright. www.healthwatch.se", f)));
			}
			cell.HorizontalAlignment = Element.ALIGN_CENTER;
			cell.Border = 0;
			cell.Padding = document.BottomMargin / 2 - 5;
			cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#B1E1ED"));
			
			table.AddCell(cell);
			table.WriteSelectedRows(0, -1, 0, document.Bottom, writer.DirectContent);
			
			pageNumber++;
		}

		public override void OnCloseDocument(PdfWriter writer, Document document)
		{
			base.OnCloseDocument(writer, document);
		}
	}
}
