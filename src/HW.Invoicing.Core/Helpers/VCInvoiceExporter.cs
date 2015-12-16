﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HW.Core.Models;
using HW.Invoicing.Core.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using HW.Core.Helpers;

namespace HW.Invoicing.Core.Helpers
{
	public class VCInvoiceExporter : AbstractInvoiceExporter
	{
		IInvoicePDFGenerator generator;
		
		public override string Name {
			get {
				return "VC Invoice Exporter";
			}
		}
		
		public VCInvoiceExporter() : this(new VCInvoicePDFScratchGenerator())
		{
		}
		
		public VCInvoiceExporter(IInvoicePDFGenerator generator)
		{
			this.generator = generator;
		}
		
		public override MemoryStream Export(Invoice invoice, string templateFileName, string font, bool flatten)
		{
			return generator.Generate(invoice, templateFileName, font, flatten);
		}
	}
	
	public class VCInvoicePDFScratchGenerator : AbstractInvoicePDFGenerator
	{
		Font normalFont = FontFactory.GetFont("Arial", 9, Font.NORMAL, BaseColor.BLACK);
		Font titleFont = FontFactory.GetFont("Arial", 16, Font.NORMAL, BaseColor.BLACK);
		Font boldFont = FontFactory.GetFont("Arial", 9, Font.BOLD, BaseColor.BLACK);
		Font headerFont = FontFactory.GetFont("Arial", 6, Font.BOLD, BaseColor.BLACK);
		Font headerNormalFont = FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK);
		Font smallFont = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK);
		Font smallestFont = FontFactory.GetFont("Arial", 3, Font.NORMAL, BaseColor.BLACK);
		
		public override MemoryStream Generate(Invoice invoice, string templateFileName, string font, bool flatten)
		{
			byte[] bytes;
			using (var output = new MemoryStream()) {
				using (var doc = new Document(PageSize.LETTER, 50f, 50f, 30f, 90f)) {
					using (var writer = PdfWriter.GetInstance(doc, output)) {
						writer.PageEvent = new PDFFooter(invoice);
						
						doc.Open();
						
						doc.Add(GetInvoiceDetails(invoice, doc));
						
						doc.Add(new Paragraph(" "));
						doc.Add(new Paragraph("Betalningsvillkor: 30 dagar netto. Vid likvid efter förfallodagen debiteras ränta med 2% per månad.", smallFont));
						doc.Add(new Paragraph(" ", smallestFont));
						
						doc.Add(GetInvoiceItems(invoice, doc));
						doc.Add(GetInvoiceTotal(invoice, doc));
						
						doc.Close();
					}
				}
				bytes = output.ToArray();
			}
			return new MemoryStream(bytes);
		}
		
		PdfPTable GetInvoiceDetails(Invoice invoice, Document document)
		{
			PdfPTable t = new PdfPTable(2) {
				TotalWidth = document.Right - document.Left,
				WidthPercentage = 100
			};
			var w = t.TotalWidth;
			float x = 10;
			t.SetWidths(new float[] { w / x * 6, w / x * 4 });
			
			PdfPTable t2 = new PdfPTable(1);
			Image logo = Image.GetInstance(invoice.Company.InvoiceLogo);
			logo.ScalePercent(55);
			t2.AddCell(new PdfPCell(logo) { Border = Rectangle.NO_BORDER });
			t2.AddCell(new PdfPCell(new Phrase(" ", normalFont)) { Border = Rectangle.NO_BORDER });
			t2.AddCell(new PdfPCell(new Phrase("Beställare/Leveransadress/Faktureringsadress", normalFont)) { Border = Rectangle.NO_BORDER });
			t2.AddCell(new PdfPCell(new Phrase(invoice.Customer.ToString() + "\n\n" + invoice.Customer.PurchaseOrderNumber, normalFont)) { Border = Rectangle.NO_BORDER });
			
			PdfPTable t3 = new PdfPTable(2);
			t3.AddCell(new PdfPCell(new Phrase("FAKTURA", titleFont)) { Border = Rectangle.NO_BORDER, Colspan = 2, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingRight = 3, PaddingBottom = 20 });
			
			t3.AddCell(C(" ", smallestFont, Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER, 0, 2));
			t3.AddCell(C("KUNDNUMMER", normalFont, 3));
			t3.AddCell(C(invoice.Customer.Number, normalFont, 3));
			t3.AddCell(C(" ", smallestFont, Rectangle.RIGHT_BORDER, 0, 2));
			
			t3.AddCell(C(" ", smallestFont, Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER, 0, 2));
			t3.AddCell(C("FAKTURANUMMER", normalFont, 3));
			t3.AddCell(C(invoice.Number, normalFont, 3));
			t3.AddCell(C(" ", smallestFont, Rectangle.RIGHT_BORDER, 0, 2));
			
			t3.AddCell(C(" ", smallestFont, Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER, 0, 2));
			t3.AddCell(C("FAKTURADATUM", normalFont, 3));
			t3.AddCell(C(invoice.Date.Value.ToString("yyyy-MM-dd"), normalFont, 3));
			t3.AddCell(C(" ", smallestFont, Rectangle.RIGHT_BORDER, 0, 2));
			
			t3.AddCell(C(" ", smallestFont, Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER, 0, 2));
			t3.AddCell(C("FÖRFALLODAG", normalFont, 3));
			t3.AddCell(C(invoice.MaturityDate.Value.ToString("yyyy-MM-dd"), normalFont, 3));
			t3.AddCell(C(" ", smallestFont, Rectangle.RIGHT_BORDER, 0, 2));
			
			t3.AddCell(C(" ", smallestFont, Rectangle.TOP_BORDER, 0, 2));
			t3.AddCell(C("Er referens:", smallFont, 3));
			t3.AddCell(C(invoice.Customer.YourReferencePerson, smallFont, 3));
			
			t3.AddCell(C(" ", smallestFont, Rectangle.NO_BORDER, 0, 2));
			t3.AddCell(C("Vår referens:", smallFont, 3));
			t3.AddCell(C(invoice.Customer.OurReferencePerson, smallFont, 3));
			
			t.AddCell(new PdfPCell(t2) { Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(t3) { Border = Rectangle.NO_BORDER });
			
			return t;
		}
		
		PdfPTable GetInvoiceItems(Invoice invoice, Document document)
		{
			PdfPTable t = new PdfPTable(16) {
				TotalWidth = document.Right - document.Left,
				WidthPercentage = 100
			};
			var w = t.TotalWidth;
			float x = 16;
			var widths = new List<float>();
			for (int i = 0; i < 16; i++) {
				widths.Add(w / x * 1);
			}
			t.SetWidths(widths.ToArray());
			
			t.AddCell(C(" ", smallestFont, Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER, 0, 13));
			t.AddCell(C(" ", smallestFont, Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER, 0, 3));
			
			t.AddCell(new PdfPCell(new Phrase("SPECIFIKATION", headerFont)) { Colspan = 13, Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase("PRIS", headerFont)) { Colspan = 3, Border = Rectangle.LEFT_BORDER });
			
			int j = 0;
			foreach (var tb in invoice.Timebooks) {
				if (tb.Timebook.IsHeader) {
					if (j > 0) {
						t.AddCell(new PdfPCell(new Phrase(" ", normalFont)) { Colspan = 13, Border = Rectangle.NO_BORDER });
						t.AddCell(new PdfPCell(new Phrase("", normalFont)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, Border = Rectangle.LEFT_BORDER });
						t.AddCell(new PdfPCell(new Phrase(" ", normalFont)) { Colspan = 13, Border = Rectangle.NO_BORDER });
						t.AddCell(new PdfPCell(new Phrase("", normalFont)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, Border = Rectangle.LEFT_BORDER });
					}
					
					t.AddCell(new PdfPCell(new Phrase(tb.Timebook.ToString(), normalFont)) { Colspan = 13, Border = Rectangle.NO_BORDER });
					t.AddCell(new PdfPCell(new Phrase("", normalFont)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, Border = Rectangle.LEFT_BORDER });
				} else {
					t.AddCell(new PdfPCell(new Phrase(tb.Timebook.ToString(), normalFont)) { Colspan = 13, Border = Rectangle.NO_BORDER });
					t.AddCell(new PdfPCell(new Phrase(tb.Timebook.Amount.ToString("### ### ##0.00"), normalFont)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, Border = Rectangle.LEFT_BORDER });
				}
				j++;
			}
			
			t.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 16, Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 16, Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 16, Border = Rectangle.NO_BORDER });
			
			t.AddCell(new PdfPCell(new Phrase(invoice.Comments, normalFont)) { Colspan = 16, Border = Rectangle.NO_BORDER });
			
			return t;
		}
		
		PdfPTable GetInvoiceTotal(Invoice invoice, Document document)
		{
			PdfPTable t = new PdfPTable(16) {
				TotalWidth = document.Right - document.Left,
				WidthPercentage = 100
			};
			var w = t.TotalWidth;
			float x = 16;
			var widths = new List<float>();
			for (int i = 0; i < 16; i++) {
				widths.Add(w / x * 1);
			}
			t.SetWidths(widths.ToArray());
			
			t.KeepTogether = true;
			
			t.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 16, Border = Rectangle.NO_BORDER });

			t.AddCell(new PdfPCell() { Colspan = 13, Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase("SUBTOTAL, SEK", headerNormalFont)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER });
			
			t.AddCell(new PdfPCell() { Colspan = 13, Border = Rectangle.NO_BORDER });
			t.AddCell(C(invoice.SubTotal.ToString("### ### ##0.00"), normalFont, Rectangle.BODY | Rectangle.RIGHT_BORDER, 5, 3, Element.ALIGN_CENTER));

			t.AddCell(new PdfPCell() { Colspan = 13 - (invoice.VATs.Count * 3), Border = Rectangle.NO_BORDER });
			foreach (var v in invoice.VATs.Keys) {
				t.AddCell(new PdfPCell(new Phrase("MOMS %", headerNormalFont)) { Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER });
				t.AddCell(new PdfPCell(new Phrase("MOMS, SEK", headerNormalFont)) { Colspan = 2, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER });
			}
			t.AddCell(new PdfPCell(new Phrase("SUMMA ATT BETALA, SEK", headerFont)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER });

			t.AddCell(new PdfPCell() { Colspan = 13 - (invoice.VATs.Count * 3), Border = Rectangle.BOTTOM_BORDER });
			foreach (var v in invoice.VATs.Keys) {
				t.AddCell(C(v.ToString(), normalFont, Rectangle.BODY, 5));
				t.AddCell(C(invoice.VATs[v].ToString("### ### ##0.00"), normalFont, Rectangle.BODY, 5, 2));
			}
			t.AddCell(C(invoice.TotalAmount.ToString("### ### ##0.00"), boldFont, Rectangle.BODY | Rectangle.RIGHT_BORDER, 5, 3, Element.ALIGN_CENTER));
			
			return t;
		}
		
		public class PDFFooter : PdfPageEventHelper
		{
			Font font = FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK);
			Font boldFont = FontFactory.GetFont("Arial", 7, Font.BOLD, BaseColor.BLACK);
			Font font2 = FontFactory.GetFont("Arial", 3, Font.NORMAL, BaseColor.BLACK);
			Invoice invoice;
			
			public PDFFooter(Invoice invoice)
			{
				this.invoice = invoice;
			}
			
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
				
				PdfPTable t = new PdfPTable(4) {
					TotalWidth = document.Right - document.Left
				};
				t.DefaultCell.Border = Rectangle.NO_BORDER;
				var w = t.TotalWidth;
				float x = 10;
				t.SetWidths(new float[] { w / x * 1.5f, w / x * 5, w / x * 1.5f, w / x * 2 });
				
				t.AddCell(new PdfPCell(new Phrase(invoice.Company.Name, boldFont)) { Colspan = 2, Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase("Bankgiro " + invoice.Company.BankAccountNumber, boldFont)) { Colspan = 2, Border = Rectangle.NO_BORDER });
				
				t.AddCell(new PdfPCell(new Phrase(" ", font2)) { Colspan = 4, Border = Rectangle.NO_BORDER, Padding = 0 });
				
				t.AddCell(new PdfPCell(new Phrase("Postadress", font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(invoice.Company.Address.Clean("\n", "\r"), font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase("VAT/Momsreg.nr " + invoice.Company.TIN, font)) { Colspan = 2, Border = Rectangle.NO_BORDER });

				t.AddCell(new PdfPCell(new Phrase("Telefon", font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(invoice.Company.Phone, font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase("F-skattebevis", font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase("", font)) { Border = Rectangle.NO_BORDER });

				t.AddCell(new PdfPCell(new Phrase("Email", font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(invoice.Company.Email, font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase("Org nr " + invoice.Company.OrganizationNumber, font)) { Colspan = 2, Border = Rectangle.NO_BORDER });

				t.WriteSelectedRows(0, -1, document.Left, document.Bottom, writer.DirectContent);
			}
			
			public override void OnCloseDocument(PdfWriter writer, Document document)
			{
				base.OnCloseDocument(writer, document);
			}
		}
	}
}
