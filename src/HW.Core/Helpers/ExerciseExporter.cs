/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/7/2016
 * Time: 10:46 PM
 * 
 */
using System;
using System.IO;
using HW.Core.Models;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Collections.Generic;

namespace HW.Core.Helpers
{
	public class ExerciseExporter
	{
		Font normalFont = FontFactory.GetFont("Arial", 9, Font.NORMAL, BaseColor.BLACK);
		Font titleFont = FontFactory.GetFont("Arial", 16, Font.NORMAL, BaseColor.BLACK);
		Font boldFont = FontFactory.GetFont("Arial", 9, Font.BOLD, BaseColor.BLACK);
		Font headerFont = FontFactory.GetFont("Arial", 6, Font.BOLD, BaseColor.BLACK);
		Font headerNormalFont = FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK);
		Font smallFont = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK);
		Font smallestFont = FontFactory.GetFont("Arial", 3, Font.NORMAL, BaseColor.BLACK);
		ExerciseVariantLanguage variant;
		
		public ExerciseExporter()
		{
		}
		
		public bool HasContentDisposition(string file)
		{
			return GetContentDisposition(file).Length > 0;
		}
		
		public string GetContentDisposition(string file)
		{
			return "";
		}
		
		public string Type {
			get { return "application/pdf"; }
		}
		
//		int LangId {
//			get { return exercise.Customer.Language != null ? exercise.Customer.Language.Id : 1; }
//		}
//
//		string Currency {
//			get { return exercise.Customer.Currency != null ? ", " + exercise.Customer.Currency.ShortName : ""; }
//		}
		
		public MemoryStream Export(ExerciseVariantLanguage variant)
		{
			this.variant = variant;
			
			byte[] bytes;
			using (var output = new MemoryStream()) {
//				var htmlText = "<h1>Hello world</h1><p>The quick brown fox jumps over the lazy dog.</p>";
				var htmlText = variant.Content;
				TextReader txtReader = new StringReader(htmlText);
				using (var doc = new Document(PageSize.A4, 50f, 50f, 50f, 50f)) {
					using (var writer = PdfWriter.GetInstance(doc, output)) {
						writer.PageEvent = new PDFFooter(variant);
						
//						HTMLWorker htmlWorker = new HTMLWorker(doc);
						
						var styles = new StyleSheet();
						styles.LoadStyle("body", "font-size", "3px");
						styles.LoadStyle("h2", "font-size", "3px");
						styles.LoadStyle("p", "font-size", "3px");
						
//						htmlWorker.SetStyleSheet(style);
						
						doc.Open();
						
//						htmlWorker.StartDocument();
//						htmlWorker.Parse(txtReader);
						
						List<IElement> objects = HTMLWorker.ParseToList(new StringReader(htmlText), styles);
						foreach (IElement element in objects) {
							doc.Add(element);
						}
						
//						doc.Add(GetInvoiceDetails(exercise, doc));
						
//						doc.Add(new Paragraph("hello world "));
//						doc.Add(new Paragraph(R.Str(LangId, "invoice.terms", "Betalningsvillkor: 30 dagar netto. Vid likvid efter förfallodagen debiteras ränta med 2% per månad."), smallFont));
//						doc.Add(new Paragraph(" ", smallestFont));
						
//						doc.Add(GetInvoiceItems(exercise, doc));
//						doc.Add(GetInvoiceTotal(exercise, doc));
						
//						htmlWorker.EndDocument();
//						htmlWorker.Close();
						
						doc.Close();
					}
				}
				bytes = output.ToArray();
			}
			return new MemoryStream(bytes);
		}
		
//		public MemoryStream Export(ExerciseVariantLanguage variant)
//		{
//			this.variant = variant;
//
//			byte[] bytes;
//			using (var output = new MemoryStream()) {
//				using (var doc = new Document(PageSize.A4, 30f, 30f, 30f, 70f)) {
//					using (var writer = PdfWriter.GetInstance(doc, output)) {
//						writer.PageEvent = new PDFFooter(variant);
//
//						doc.Open();
//
		////						doc.Add(GetInvoiceDetails(exercise, doc));
//
//						doc.Add(new Paragraph(" "));
		////						doc.Add(new Paragraph(R.Str(LangId, "invoice.terms", "Betalningsvillkor: 30 dagar netto. Vid likvid efter förfallodagen debiteras ränta med 2% per månad."), smallFont));
//						doc.Add(new Paragraph(" ", smallestFont));
//
		////						doc.Add(GetInvoiceItems(exercise, doc));
		////						doc.Add(GetInvoiceTotal(exercise, doc));
//
//						doc.Close();
//					}
//				}
//				bytes = output.ToArray();
//			}
//			return new MemoryStream(bytes);
//		}
		
//		PdfPTable GetInvoiceDetails(Exercise invoice, Document document)
//		{
//			PdfPTable t = new PdfPTable(2) {
//				TotalWidth = document.Right - document.Left,
//				WidthPercentage = 100
//			};
//			var w = t.TotalWidth;
//			float x = 10;
//			t.SetWidths(new float[] { w / x * 6, w / x * 4 });
//
//			PdfPTable t2 = new PdfPTable(1);
//			try {
//				Image logo = Image.GetInstance(invoice.Company.InvoiceLogo);
//				logo.ScalePercent((float)invoice.Company.InvoiceLogoPercentage);
//				t2.AddCell(new PdfPCell(logo) { Border = Rectangle.NO_BORDER });
//			} catch {
//				t2.AddCell(new PdfPCell(new Phrase(" ")) { Border = Rectangle.NO_BORDER });
//			}
//			t2.AddCell(new PdfPCell(new Phrase(" ", normalFont)) { Border = Rectangle.NO_BORDER });
//			t2.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "customer", "Beställare/Leveransadress/Faktureringsadress"), normalFont)) { Border = Rectangle.NO_BORDER });
//			t2.AddCell(new PdfPCell(new Phrase(invoice.Customer != null ? invoice.Customer.ToString() + "\n\n" + invoice.GetContactReferenceNumber() : "", normalFont)) { Border = Rectangle.NO_BORDER });
//
//			PdfPTable t3 = new PdfPTable(2);
//			t3.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "invoice.label", "FAKTURA"), titleFont)) { Border = Rectangle.NO_BORDER, Colspan = 2, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingRight = 3, PaddingBottom = 20 });
//
//			t3.AddCell(C(" ", smallestFont, Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER, 0, 2));
//			t3.AddCell(C(R.Str(LangId, "customer.number", "KUNDNUMMER"), normalFont, 3));
//			t3.AddCell(C(invoice.Customer != null ? invoice.Customer.Number : "", normalFont, 3));
//			t3.AddCell(C(" ", smallestFont, Rectangle.RIGHT_BORDER, 0, 2));
//
//			t3.AddCell(C(" ", smallestFont, Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER, 0, 2));
//			t3.AddCell(C(R.Str(LangId, "invoice.number", "FAKTURANUMMER"), normalFont, 3));
//			t3.AddCell(C(invoice.Number, normalFont, 3));
//			t3.AddCell(C(" ", smallestFont, Rectangle.RIGHT_BORDER, 0, 2));
//
//			t3.AddCell(C(" ", smallestFont, Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER, 0, 2));
//			t3.AddCell(C(R.Str(LangId, "invoice.date", "FAKTURADATUM"), normalFont, 3));
//			t3.AddCell(C(invoice.Date.Value.ToString("yyyy-MM-dd"), normalFont, 3));
//			t3.AddCell(C(" ", smallestFont, Rectangle.RIGHT_BORDER, 0, 2));
//
//			t3.AddCell(C(" ", smallestFont, Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER, 0, 2));
//			t3.AddCell(C(R.Str(LangId, "invoice.date.maturity", "FÖRFALLODAG"), normalFont, 3));
//			t3.AddCell(C(invoice.MaturityDate.Value.ToString("yyyy-MM-dd"), normalFont, 3));
//			t3.AddCell(C(" ", smallestFont, Rectangle.RIGHT_BORDER, 0, 2));
//
//			t3.AddCell(C(" ", smallestFont, Rectangle.TOP_BORDER, 0, 2));
//			t3.AddCell(C(R.Str(LangId, "invoice.reference.your", "Er referens:"), smallFont, 3));
//			t3.AddCell(C(invoice.GetContactName(), smallFont, 3));
//
//			t3.AddCell(C(" ", smallestFont, Rectangle.NO_BORDER, 0, 2));
//			t3.AddCell(C(R.Str(LangId, "invoice.reference.our", "Vår referens:"), smallFont, 3));
//			t3.AddCell(C(invoice.OurReferencePerson, smallFont, 3));
//
//			t.AddCell(new PdfPCell(t2) { Border = Rectangle.NO_BORDER });
//			t.AddCell(new PdfPCell(t3) { Border = Rectangle.NO_BORDER });
//
//			return t;
//		}
		
//		PdfPTable GetInvoiceItems(Exercise exercise, Document document)
//		{
//			PdfPTable t = new PdfPTable(16) {
//				TotalWidth = document.Right - document.Left,
//				WidthPercentage = 100
//			};
//			var w = t.TotalWidth;
//			float x = 16;
//			var widths = new List<float>();
//			for (int i = 0; i < 16; i++) {
//				widths.Add(w / x * 1);
//			}
//			t.SetWidths(widths.ToArray());
//
//			t.AddCell(C(" ", smallestFont, Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER, 0, 13));
//			t.AddCell(C(" ", smallestFont, Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER, 0, 3));
//
//			t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "item", "SPECIFIKATION"), headerFont)) { Colspan = 13, Border = Rectangle.NO_BORDER });
//			t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "item.price", "PRIS"), headerFont)) { Colspan = 3, Border = Rectangle.LEFT_BORDER });
//
//			int j = 0;
//			foreach (var tb in exercise.Timebooks) {
//				if (tb.Timebook.IsHeader) {
//					if (j > 0) {
//						t.AddCell(new PdfPCell(new Phrase(" ", normalFont)) { Colspan = 13, VerticalAlignment = Element.ALIGN_CENTER, Border = Rectangle.NO_BORDER });
//						t.AddCell(new PdfPCell(new Phrase("", normalFont)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_CENTER, Border = Rectangle.LEFT_BORDER });
//						t.AddCell(new PdfPCell(new Phrase(" ", normalFont)) { Colspan = 13, VerticalAlignment = Element.ALIGN_CENTER, Border = Rectangle.NO_BORDER });
//						t.AddCell(new PdfPCell(new Phrase("", normalFont)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_CENTER, Border = Rectangle.LEFT_BORDER });
//					}
//
//					t.AddCell(new PdfPCell(new Phrase(tb.Timebook.ToString(), normalFont)) { Colspan = 13, Border = Rectangle.NO_BORDER });
//					t.AddCell(new PdfPCell(new Phrase("", normalFont)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, Border = Rectangle.LEFT_BORDER });
//				} else {
//					t.AddCell(new PdfPCell(new Phrase(tb.Timebook.ToString(), normalFont)) { Colspan = 13, Border = Rectangle.NO_BORDER });
//					t.AddCell(new PdfPCell(new Phrase(tb.Timebook.Amount.ToString("### ### ##0.00"), normalFont)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, Border = Rectangle.LEFT_BORDER });
//				}
//				j++;
//			}
//
//			t.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 16, Border = Rectangle.NO_BORDER });
//			t.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 16, Border = Rectangle.NO_BORDER });
//			t.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 16, Border = Rectangle.NO_BORDER });
//
//			t.AddCell(new PdfPCell(new Phrase(exercise.Comments, normalFont)) { Colspan = 16, Border = Rectangle.NO_BORDER });
//
//			return t;
//		}
		
//		PdfPTable GetInvoiceTotal(Exercise exercise, Document document)
//		{
//			PdfPTable t = new PdfPTable(16) {
//				TotalWidth = document.Right - document.Left,
//				WidthPercentage = 100
//			};
//			var w = t.TotalWidth;
//			float x = 16;
//			var widths = new List<float>();
//			for (int i = 0; i < 16; i++) {
//				widths.Add(w / x * 1);
//			}
//			t.SetWidths(widths.ToArray());
//
//			t.KeepTogether = true;
//
//			t.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 16, Border = Rectangle.NO_BORDER });
//
//			t.AddCell(new PdfPCell() { Colspan = 13, Border = Rectangle.NO_BORDER });
//			t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "invoice.subtotal", "SUBTOTAL") + Currency, headerNormalFont)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER });
//
//			t.AddCell(new PdfPCell() { Colspan = 13, Border = Rectangle.NO_BORDER });
//			t.AddCell(C(exercise.SubTotal.ToString("### ### ##0.00"), normalFont, Rectangle.BODY | Rectangle.RIGHT_BORDER, 5, 3, Element.ALIGN_CENTER));
//
//			t.AddCell(new PdfPCell() { Colspan = 13 - (exercise.VATs.Count * 3), Border = Rectangle.NO_BORDER });
//			foreach (var v in exercise.VATs.Keys) {
//				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "invoice.vat", "MOMS %"), headerNormalFont)) { Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER });
//				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "invoice.vat.amount", "MOMS") + ", " + Currency, headerNormalFont)) { Colspan = 2, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER });
//			}
//			t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "invoice.total", "SUMMA ATT BETALA"), headerFont)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER });
//
//			t.AddCell(new PdfPCell() { Colspan = 13 - (exercise.VATs.Count * 3), Border = Rectangle.BOTTOM_BORDER });
//			foreach (var v in exercise.VATs.Keys) {
//				t.AddCell(C(v.ToString(), normalFont, Rectangle.BODY, 5));
//				t.AddCell(C(exercise.VATs[v].ToString("### ### ##0.00"), normalFont, Rectangle.BODY, 5, 2));
//			}
//			t.AddCell(C(exercise.TotalAmount.ToString("### ### ##0.00"), boldFont, Rectangle.BODY | Rectangle.RIGHT_BORDER, 5, 3, Element.ALIGN_CENTER));
//
//			return t;
//		}
		
		public class PDFFooter : PdfPageEventHelper
		{
			Font font = FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK);
			Font boldFont = FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK);
			Font font2 = FontFactory.GetFont("Arial", 3, Font.NORMAL, BaseColor.BLACK);
			ExerciseVariantLanguage exercise;
			
			public PDFFooter(ExerciseVariantLanguage variant)
			{
				this.exercise = variant;
			}
			
//			int LangId {
//				get { return exercise.Customer.Language != null ? exercise.Customer.Language.Id : 1; }
//			}
			
			public override void OnOpenDocument(PdfWriter writer, Document document)
			{
				base.OnOpenDocument(writer, document);
			}

			public override void OnStartPage(PdfWriter writer, Document document)
			{
				base.OnStartPage(writer, document);
			}

			public override void OnEndPage(PdfWriter writer, Document document)
			{
				base.OnEndPage(writer, document);
				
//				PdfPTable t = new PdfPTable(4) {
//					TotalWidth = document.Right - document.Left
//				};
//				t.DefaultCell.Border = Rectangle.NO_BORDER;
//				var w = t.TotalWidth;
//				float x = 10;
//				t.SetWidths(new float[] { w / x * 1.5f, w / x * 5, w / x * 1.5f, w / x * 2 });
//
//				t.AddCell(new PdfPCell(new Phrase(exercise.Company.Name, boldFont)) { Colspan = 2, Border = Rectangle.NO_BORDER });
//				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "company.bank", "Bankgiro") + " " + exercise.Company.BankAccountNumber, boldFont)) { Colspan = 2, Border = Rectangle.NO_BORDER });
//
//				t.AddCell(new PdfPCell(new Phrase(" ", font2)) { Colspan = 4, Border = Rectangle.NO_BORDER, Padding = 0 });
//
//				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "company.address.postal", "Postadress"), font)) { Border = Rectangle.NO_BORDER });
//				t.AddCell(new PdfPCell(new Phrase(exercise.Company.Address.Clean("\n", "\r"), font)) { Border = Rectangle.NO_BORDER });
//				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "company.vat", "VAT/Momsreg.nr") + " " + exercise.Company.TIN, font)) { Colspan = 2, Border = Rectangle.NO_BORDER });
//
//				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "company.phone", "Telefon"), font)) { Border = Rectangle.NO_BORDER });
//				t.AddCell(new PdfPCell(new Phrase(exercise.Company.Phone, font)) { Border = Rectangle.NO_BORDER });
//				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "company.tax", "F-skattebevis"), font)) { Border = Rectangle.NO_BORDER });
//				t.AddCell(new PdfPCell(new Phrase("", font)) { Border = Rectangle.NO_BORDER });
//
//				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "company.email", "Email"), font)) { Border = Rectangle.NO_BORDER });
//				t.AddCell(new PdfPCell(new Phrase(exercise.Company.Email, font)) { Border = Rectangle.NO_BORDER });
//				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "company.org", "Org nr") + " " + exercise.Company.OrganizationNumber, font)) { Colspan = 2, Border = Rectangle.NO_BORDER });
//
//				t.WriteSelectedRows(0, -1, document.Left, document.Bottom, writer.DirectContent);
			}
			
			public override void OnCloseDocument(PdfWriter writer, Document document)
			{
				base.OnCloseDocument(writer, document);
			}
		}
	}
}
