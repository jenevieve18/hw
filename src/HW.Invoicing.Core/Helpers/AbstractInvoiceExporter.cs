using System;
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
	public interface IInvoiceExporter
	{
		int Id { get; }
		
		string Name { get; }
		
		MemoryStream Export(Invoice invoice);
	}
	
	public interface IInvoicePDFGenerator
	{
		MemoryStream Generate(Invoice invoice);
	}
	
	public abstract class AbstractInvoiceExporter : IInvoiceExporter
	{
		public abstract int Id { get; }
		
		public abstract string Name { get; }
		
		public abstract MemoryStream Export(Invoice invoice);
	}
	
	public class DefaultInvoiceExporter : AbstractInvoiceExporter
	{
		IInvoicePDFGenerator generator;
		
		public override int Id {
			get {
				return InvoiceExporterFactory.DEFAULT;
			}
		}
		
		public override string Name {
			get {
				return "Default Invoice Exporter";
			}
		}
		
		public DefaultInvoiceExporter() : this(new BaseInvoicePDFScratchGenerator())
		{
		}
		
		public DefaultInvoiceExporter(IInvoicePDFGenerator generator)
		{
			this.generator = generator;
		}
		
		public override MemoryStream Export(Invoice invoice)
		{
			return generator.Generate(invoice);
		}
	}
	
	public abstract class AbstractInvoicePDFGenerator : IInvoicePDFGenerator
	{
		public abstract MemoryStream Generate(Invoice invoice);
		
		protected PdfPCell B(Font font, int colspan)
		{
			return new PdfPCell(new Phrase(" ", font)) {
				Colspan = colspan,
				Border = Rectangle.NO_BORDER
			};
		}
		
		protected PdfPCell C(string text, Font font, int padding)
		{
			return C(text, font, Rectangle.NO_BORDER, padding);
		}
		
		protected PdfPCell C(string text, Font font, int border, int padding)
		{
			return C(text, font, border, padding, 1, Element.ALIGN_LEFT);
		}
		
		protected PdfPCell C(string text, Font font, int border, int padding, int colspan)
		{
			return C(text, font, border, padding, colspan, Element.ALIGN_LEFT);
		}
		
		protected PdfPCell C(string text, Font font, int border, int padding, int colspan, int align)
		{
			return new PdfPCell(new Phrase(text, font)) {
				Border = border,
				Padding = padding,
				Colspan = colspan,
				HorizontalAlignment = align
			};
		}
	}
	
	public class BaseInvoicePDFScratchGenerator : AbstractInvoicePDFGenerator
	{
		Font normalFont = FontFactory.GetFont("Arial", 9, Font.NORMAL, BaseColor.BLACK);
		Font titleFont = FontFactory.GetFont("Arial", 16, Font.NORMAL, BaseColor.BLACK);
		Font boldFont = FontFactory.GetFont("Arial", 9, Font.BOLD, BaseColor.BLACK);
		Font headerFont = FontFactory.GetFont("Arial", 6, Font.BOLD, BaseColor.BLACK);
		Font headerNormalFont = FontFactory.GetFont("Arial", 6, Font.NORMAL, BaseColor.BLACK);
		Font smallFont = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK);
		Font smallestFont = FontFactory.GetFont("Arial", 3, Font.NORMAL, BaseColor.BLACK);
		Invoice invoice;
		
		int LangId {
			get { return invoice.Customer.Language != null ? invoice.Customer.Language.Id : 1; }
		}
		
		string Currency {
			get { return invoice.Customer.Currency != null ? ", " + invoice.Customer.Currency.ShortName : ""; }
		}
		
		public override MemoryStream Generate(Invoice invoice)
		{
			this.invoice = invoice;
			
			byte[] bytes;
			using (var output = new MemoryStream()) {
				using (var doc = new Document(PageSize.A4, 30f, 30f, 30f, 70f)) {
					using (var writer = PdfWriter.GetInstance(doc, output)) {
						writer.PageEvent = new PDFFooter(invoice);
						
						doc.Open();
						
						doc.Add(GetInvoiceDetails(invoice, doc));
						
						doc.Add(new Paragraph(" "));
						doc.Add(new Paragraph(R.Str(LangId, "invoice.terms", "Betalningsvillkor: 30 dagar netto. Vid likvid efter förfallodagen debiteras ränta med 2% per månad."), smallFont));
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
//			int langId = invoice.Customer.Language.Id;
			
			PdfPTable t = new PdfPTable(2) {
				TotalWidth = document.Right - document.Left,
				WidthPercentage = 100
			};
			var w = t.TotalWidth;
			float x = 10;
			t.SetWidths(new float[] { w / x * 6, w / x * 4 });
			
			PdfPTable t2 = new PdfPTable(1);
			try {
				Image logo = Image.GetInstance(invoice.Company.InvoiceLogo);
				//logo.ScalePercent(55);
				logo.ScalePercent((float)invoice.Company.InvoiceLogoPercentage);
				t2.AddCell(new PdfPCell(logo) { Border = Rectangle.NO_BORDER });
			} catch {
				t2.AddCell(new PdfPCell(new Phrase(" ")) { Border = Rectangle.NO_BORDER });
			}
			t2.AddCell(new PdfPCell(new Phrase(" ", normalFont)) { Border = Rectangle.NO_BORDER });
			t2.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "customer", "Beställare/Leveransadress/Faktureringsadress"), normalFont)) { Border = Rectangle.NO_BORDER });
//			t2.AddCell(new PdfPCell(new Phrase(invoice.Customer != null ? invoice.Customer.ToString() + "\n\n" + invoice.Customer.PrimaryContactReferenceNumber : "", normalFont)) { Border = Rectangle.NO_BORDER });
			t2.AddCell(new PdfPCell(new Phrase(invoice.Customer != null ? invoice.Customer.ToString() + "\n\n" + invoice.Customer.GetPrimaryContactReferenceNumber() : "", normalFont)) { Border = Rectangle.NO_BORDER });
			
			PdfPTable t3 = new PdfPTable(2);
			t3.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "invoice.label", "FAKTURA"), titleFont)) { Border = Rectangle.NO_BORDER, Colspan = 2, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingRight = 3, PaddingBottom = 20 });
			
			t3.AddCell(C(" ", smallestFont, Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER, 0, 2));
			t3.AddCell(C(R.Str(LangId, "customer.number", "KUNDNUMMER"), normalFont, 3));
			t3.AddCell(C(invoice.Customer != null ? invoice.Customer.Number : "", normalFont, 3));
			t3.AddCell(C(" ", smallestFont, Rectangle.RIGHT_BORDER, 0, 2));
			
			t3.AddCell(C(" ", smallestFont, Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER, 0, 2));
			t3.AddCell(C(R.Str(LangId, "invoice.number", "FAKTURANUMMER"), normalFont, 3));
			t3.AddCell(C(invoice.Number, normalFont, 3));
			t3.AddCell(C(" ", smallestFont, Rectangle.RIGHT_BORDER, 0, 2));
			
			t3.AddCell(C(" ", smallestFont, Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER, 0, 2));
			t3.AddCell(C(R.Str(LangId, "invoice.date", "FAKTURADATUM"), normalFont, 3));
			t3.AddCell(C(invoice.Date.Value.ToString("yyyy-MM-dd"), normalFont, 3));
			t3.AddCell(C(" ", smallestFont, Rectangle.RIGHT_BORDER, 0, 2));
			
			t3.AddCell(C(" ", smallestFont, Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER, 0, 2));
			t3.AddCell(C(R.Str(LangId, "invoice.date.maturity", "FÖRFALLODAG"), normalFont, 3));
			t3.AddCell(C(invoice.MaturityDate.Value.ToString("yyyy-MM-dd"), normalFont, 3));
			t3.AddCell(C(" ", smallestFont, Rectangle.RIGHT_BORDER, 0, 2));
			
			t3.AddCell(C(" ", smallestFont, Rectangle.TOP_BORDER, 0, 2));
			t3.AddCell(C(R.Str(LangId, "invoice.reference.your", "Er referens:"), smallFont, 3));
//			t3.AddCell(C(invoice.Customer != null && invoice.Customer.ContactPerson != null ? invoice.Customer.ContactPerson.Name : "", smallFont, 3));
			t3.AddCell(C(invoice.Customer != null && invoice.CustomerContact != null ? invoice.CustomerContact.Name : "", smallFont, 3));
			
			t3.AddCell(C(" ", smallestFont, Rectangle.NO_BORDER, 0, 2));
			t3.AddCell(C(R.Str(LangId, "invoice.reference.our", "Vår referens:"), smallFont, 3));
			t3.AddCell(C(invoice.Customer != null ? invoice.Customer.OurReferencePerson : "", smallFont, 3));
			
			t.AddCell(new PdfPCell(t2) { Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(t3) { Border = Rectangle.NO_BORDER });
			
			return t;
		}
		
		PdfPTable GetInvoiceItems(Invoice invoice, Document document)
		{
//			int langId = invoice.Customer.Language.Id;
			
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
			
			t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "item", "SPECIFIKATION"), headerFont)) { Colspan = 13, Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "item.price", "PRIS"), headerFont)) { Colspan = 3, Border = Rectangle.LEFT_BORDER });
			
			int j = 0;
			foreach (var tb in invoice.Timebooks) {
				if (tb.Timebook.IsHeader) {
					if (j > 0) {
						t.AddCell(new PdfPCell(new Phrase(" ", normalFont)) { Colspan = 13, VerticalAlignment = Element.ALIGN_CENTER, Border = Rectangle.NO_BORDER });
						t.AddCell(new PdfPCell(new Phrase("", normalFont)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_CENTER, Border = Rectangle.LEFT_BORDER });
						t.AddCell(new PdfPCell(new Phrase(" ", normalFont)) { Colspan = 13, VerticalAlignment = Element.ALIGN_CENTER, Border = Rectangle.NO_BORDER });
						t.AddCell(new PdfPCell(new Phrase("", normalFont)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_CENTER, Border = Rectangle.LEFT_BORDER });
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
//			int langId = invoice.Customer.Language.Id;
//			string currency = invoice.Customer.Currency.ShortName;
			
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
			t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "invoice.subtotal", "SUBTOTAL") + Currency, headerNormalFont)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER });
			
			t.AddCell(new PdfPCell() { Colspan = 13, Border = Rectangle.NO_BORDER });
			t.AddCell(C(invoice.SubTotal.ToString("### ### ##0.00"), normalFont, Rectangle.BODY | Rectangle.RIGHT_BORDER, 5, 3, Element.ALIGN_CENTER));

			t.AddCell(new PdfPCell() { Colspan = 13 - (invoice.VATs.Count * 3), Border = Rectangle.NO_BORDER });
			foreach (var v in invoice.VATs.Keys) {
				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "invoice.vat", "MOMS %"), headerNormalFont)) { Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER });
				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "invoice.vat.amount", "MOMS") + ", " + Currency, headerNormalFont)) { Colspan = 2, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER });
			}
			t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "invoice.total", "SUMMA ATT BETALA"), headerFont)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER });

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
			
			int LangId {
				get { return invoice.Customer.Language != null ? invoice.Customer.Language.Id : 1; }
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
				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "company.bank", "Bankgiro") + " " + invoice.Company.BankAccountNumber, boldFont)) { Colspan = 2, Border = Rectangle.NO_BORDER });
				
				t.AddCell(new PdfPCell(new Phrase(" ", font2)) { Colspan = 4, Border = Rectangle.NO_BORDER, Padding = 0 });
				
				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "company.address.postal", "Postadress"), font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(invoice.Company.Address.Clean("\n", "\r"), font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "company.vat", "VAT/Momsreg.nr") + " " + invoice.Company.TIN, font)) { Colspan = 2, Border = Rectangle.NO_BORDER });

				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "company.phone", "Telefon"), font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(invoice.Company.Phone, font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "company.tax", "F-skattebevis"), font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase("", font)) { Border = Rectangle.NO_BORDER });

				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "company.email", "Email"), font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(invoice.Company.Email, font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(R.Str(LangId, "company.org", "Org nr") + " " + invoice.Company.OrganizationNumber, font)) { Colspan = 2, Border = Rectangle.NO_BORDER });
				
				t.WriteSelectedRows(0, -1, document.Left, document.Bottom, writer.DirectContent);
			}
			
			public override void OnCloseDocument(PdfWriter writer, Document document)
			{
				base.OnCloseDocument(writer, document);
			}
		}
	}
}
