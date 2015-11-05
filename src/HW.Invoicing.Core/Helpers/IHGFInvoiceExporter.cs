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
	public class IHGFInvoiceExporter : AbstractInvoiceExporter
	{
		IIHGFInvoicePDFGenerator generator;
		
		public IHGFInvoiceExporter() : this(new IHGFInvoicePDFTemplateGenerator())
		{
		}
		
		public IHGFInvoiceExporter(IIHGFInvoicePDFGenerator generator)
		{
			this.generator = generator;
		}
		
		public override MemoryStream Export(Invoice invoice, string templateFileName, string font, bool flatten)
		{
			return generator.Generate(invoice, templateFileName, font, flatten);
		}
	}
	
	public interface IIHGFInvoicePDFGenerator
	{
		MemoryStream Generate(Invoice invoice, string templateFileName, string font, bool flatten);
	}
	
	public class IHGFInvoicePDFScratchGenerator : IIHGFInvoicePDFGenerator
	{
		Font normalFont = FontFactory.GetFont("Arial", 9, Font.NORMAL, BaseColor.BLACK);
		Font headerFont = FontFactory.GetFont("Arial", 6, Font.BOLD, BaseColor.BLACK);
		Font smallFont = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK);
		
		public MemoryStream Generate(Invoice invoice, string templateFileName, string font, bool flatten)
		{
			byte[] bytes;
			using (var output = new MemoryStream()) {
				using (var doc = new Document(PageSize.LETTER, 50f, 50f, 70f, 70f)) {
					using (var writer = PdfWriter.GetInstance(doc, output)) {
						writer.PageEvent = new PDFFooter(invoice);
						
						doc.Open();
						
						doc.Add(GetInvoiceDetails(invoice, doc));
						
						doc.Add(new Paragraph(" "));
						doc.Add(new Paragraph("Betalningsvillkor: 30 dagar netto. Vid likvid efter förfallodagen debiteras ränta med 2% per månad.", normalFont));
						doc.Add(new Paragraph(" "));
						
						doc.Add(GetInvoiceItems(invoice, doc));
						
//						doc.Add(new Paragraph(" "));
//						doc.Add(GetItemTotal(invoice, doc));
						
						doc.Close();
					}
				}
				bytes = output.ToArray();
			}
			return new MemoryStream(bytes);
		}
		
		PdfPTable GetInvoiceDetails(Invoice invoice, Document document)
		{
			PdfPTable t = new PdfPTable(4) {
				TotalWidth = document.Right - document.Left,
				WidthPercentage = 100
			};
			
			t.AddCell(new PdfPCell() { Colspan = 2, Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase("KUNDNUMMER", normalFont)) { Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase(invoice.Customer.Number, normalFont)) { Border = Rectangle.NO_BORDER });
			
			Image logo = Image.GetInstance(invoice.Company.InvoiceLogo);
			t.AddCell(new PdfPCell(logo) { Colspan = 2, Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase("FAKTURANUMMER", normalFont)) { Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase(invoice.Number, normalFont)) { Border = Rectangle.NO_BORDER });
			
			t.AddCell(new PdfPCell(new Phrase("Beställare/Leveransadress/Faktureringsadress", normalFont)) { Colspan = 2, Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase("FAKTURADATUM", normalFont)) { Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase(invoice.Date.Value.ToString("yyyy-MM-dd"), normalFont)) { Border = Rectangle.NO_BORDER });
			
			t.AddCell(new PdfPCell(new Phrase(invoice.Customer.ToString() + "\n\n" + invoice.Customer.PurchaseOrderNumber, normalFont)) { Colspan = 2, Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase("FÖRFALLODAG", normalFont)) { Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase(invoice.MaturityDate.Value.ToString("yyyy-MM-dd"), normalFont)) { Border = Rectangle.NO_BORDER });
			
			t.AddCell(new PdfPCell(new Phrase("", smallFont)) { Colspan = 2, Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase("Er referens:", smallFont)) { Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase(invoice.Customer.YourReferencePerson, smallFont)) { Border = Rectangle.NO_BORDER });
			
			t.AddCell(new PdfPCell(new Phrase("", smallFont)) { Colspan = 2, Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase("Vår referens:", smallFont)) { Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase(invoice.Customer.OurReferencePerson, smallFont)) { Border = Rectangle.NO_BORDER });
			
			return t;
		}
		
		PdfPTable GetInvoiceItems(Invoice invoice, Document document)
		{
//			PdfPTable t = new PdfPTable(5) {
//				TotalWidth = document.Right - document.Left,
//				WidthPercentage = 100
//			};
			PdfPTable t = new PdfPTable(10) {
				TotalWidth = document.Right - document.Left,
				WidthPercentage = 100
			};
			var w = t.TotalWidth;
//			float x = 14;
//			t.SetWidths(new float[] { w / x * 8, w / x * 1, w / x * 1, w / x * 2, w / x * 2 });
			float x = 10;
//			t.SetWidths(new float[] { w / x * 6, w / x * 1, w / x * 1, w / x * 1, w / x * 1 });
			t.SetWidths(new float[] { w / x * 1, w / x * 1, w / x * 1, w / x * 1, w / x * 1, w / x * 1, w / x * 1, w / x * 1, w / x * 1, w / x * 1 });
			
			t.AddCell(new PdfPCell(new Phrase("SPECIFIKATION", headerFont)) { Colspan = 6, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER });
			t.AddCell(new PdfPCell(new Phrase("ANTAL", headerFont)) { Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER });
			t.AddCell(new PdfPCell(new Phrase("ENHET", headerFont)) { Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER });
			t.AddCell(new PdfPCell(new Phrase("PRIS PER ENHET", headerFont)) { Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER });
			t.AddCell(new PdfPCell(new Phrase("BELOPP", headerFont)) { Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER });
			foreach (var tb in invoice.Timebooks) {
				t.AddCell(new PdfPCell(new Phrase(tb.Timebook.ToString(), normalFont)) { Colspan = 6, Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(tb.Timebook.Quantity.ToString(), normalFont)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(tb.Timebook.Item.Unit.Name, normalFont)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(tb.Timebook.Price.ToString("### ##0.00"), normalFont)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(tb.Timebook.Amount.ToString("### ###0.00"), normalFont)) { Border = Rectangle.NO_BORDER });
			}
			
			t.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 10, Border = Rectangle.NO_BORDER });
			
			t.AddCell(new PdfPCell() { Colspan = 9, Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase("SUBTOTAL, SEK", headerFont)));
			t.AddCell(new PdfPCell() { Colspan = 9, Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase(invoice.SubTotal.ToString("### ##0.00"), normalFont)));
			
			t.AddCell(new PdfPCell() { Colspan = 9 - (invoice.VATs.Count * 2), Border = Rectangle.NO_BORDER });
			foreach (var v in invoice.VATs.Keys) {
				t.AddCell(new PdfPCell(new Phrase("MOMS %", headerFont)));
				t.AddCell(new PdfPCell(new Phrase("MOMS, SEK", headerFont)));
			}
			t.AddCell(new PdfPCell(new Phrase("SUMMA ATT BETALA, SEK", headerFont)));
			
			t.AddCell(new PdfPCell() { Colspan = 9 - (invoice.VATs.Count * 2), Border = Rectangle.BOTTOM_BORDER });
			foreach (var v in invoice.VATs.Keys) {
				t.AddCell(new PdfPCell(new Phrase(v.ToString("0.00"), normalFont)));
				t.AddCell(new PdfPCell(new Phrase(invoice.VATs[v].ToString("### ##0.00"), normalFont)));
			}
			t.AddCell(new PdfPCell(new Phrase(invoice.TotalAmount.ToString("### ##0.00"), normalFont)) { Colspan = 2 });
			
			return t;
		}
		
		PdfPTable GetItemTotal(Invoice invoice, Document document)
		{
			PdfPTable t = new PdfPTable(3 + (invoice.VATs.Count * 2)) {
				TotalWidth = document.Right - document.Left,
				WidthPercentage = 100
			};
			var w = t.TotalWidth;
			float x = 13 + (invoice.VATs.Count * 2);
			
			List<float> widths = new List<float>();
			widths.Add(w / x * 8);
			foreach (var v in invoice.VATs.Keys) {
				widths.Add(w / x * 1);
				widths.Add(w / x * 2);
			}
			widths.Add(w / x * 2);
			widths.Add(w / x * 2);
			
			t.SetWidths(widths.ToArray());
			
			t.AddCell(new PdfPCell() { Colspan = 3 + (invoice.VATs.Count * 2), Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase("SUBTOTAL, SEK", headerFont)));
			
			t.AddCell(new PdfPCell() { Colspan = 3 + (invoice.VATs.Count * 2), Border = Rectangle.NO_BORDER });
			t.AddCell(new PdfPCell(new Phrase(invoice.SubTotal.ToString("### ##0.00"), normalFont)));
			
			t.AddCell(new PdfPCell() { Border = Rectangle.NO_BORDER });
			foreach (var v in invoice.VATs.Keys) {
				t.AddCell(new PdfPCell(new Phrase("MOMS %", headerFont)));
				t.AddCell(new PdfPCell(new Phrase("MOMS, SEK", headerFont)));
			}
			t.AddCell(new PdfPCell(new Phrase("SUMMA ATT BETALA, SEK", headerFont)) { Colspan = 2 });
			
			t.AddCell(new PdfPCell() { Border = Rectangle.NO_BORDER });
			foreach (var v in invoice.VATs.Keys) {
				t.AddCell(new PdfPCell(new Phrase(v.ToString("0.00"), normalFont)));
				t.AddCell(new PdfPCell(new Phrase(invoice.VATs[v].ToString("### ##0.00"), normalFont)));
			}
			t.AddCell(new PdfPCell(new Phrase(invoice.TotalAmount.ToString("### ##0.00"), normalFont)) { Colspan = 2 });
			
			return t;
		}
		
		public class PDFFooter : PdfPageEventHelper
		{
			Font font = FontFactory.GetFont("Arial", 7, Font.NORMAL, BaseColor.BLACK);
			Invoice invoice;
			
			public PDFFooter(Invoice invoice)
			{
				this.invoice = invoice;
			}
			
			public override void OnOpenDocument(PdfWriter writer, Document document)
			{
				base.OnOpenDocument(writer, document);
//				PdfPTable table = new PdfPTable(2);
//				table.SpacingAfter = 10F;
//				table.TotalWidth = 300F;
//				table.AddCell(new PdfPCell(new Phrase("Header")));
//				table.WriteSelectedRows(0, -1, 150, document.Top , writer.DirectContent);
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
				float x = 7;
				t.SetWidths(new float[] { w / x * 1, w / x * 3, w / x * 1, w / x * 2 });
				
				t.AddCell(new PdfPCell(new Phrase(invoice.Company.Name, font)) { Colspan = 2, Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase("Bankgiro", font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(invoice.Company.BankAccountNumber, font)) { Border = Rectangle.NO_BORDER });
				
				t.AddCell(new PdfPCell(new Phrase(" ", font)) { Colspan = 4, Border = Rectangle.NO_BORDER });
				
				t.AddCell(new PdfPCell(new Phrase("Telefon", font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(invoice.Company.Phone, font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase("VAT/Momsreg.nr", font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(invoice.Company.TIN, font)) { Border = Rectangle.NO_BORDER });
				
				t.AddCell(new PdfPCell(new Phrase("Postadress", font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase(invoice.Company.Address, font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase("F-skattebevis", font)) { Border = Rectangle.NO_BORDER });
				t.AddCell(new PdfPCell(new Phrase("", font)) { Border = Rectangle.NO_BORDER });

				t.WriteSelectedRows(0, -1, document.Left, document.Bottom, writer.DirectContent);
			}
			
			public override void OnCloseDocument(PdfWriter writer, Document document)
			{
				base.OnCloseDocument(writer, document);
			}
		}
	}
	
	public class IHGFInvoicePDFTemplateGenerator : IIHGFInvoicePDFGenerator
	{
		public MemoryStream Generate(Invoice invoice, string templateFileName, string font, bool flatten)
		{
			MemoryStream output = new MemoryStream();
			
			var templateFileStream = new FileStream(templateFileName, FileMode.Open);
			var reader = new PdfReader(templateFileStream);
			var stamper = new PdfStamper(reader, output) { };
			var form = stamper.AcroFields;
			var fieldKeys = form.Fields.Keys;

			var f = BaseFont.CreateFont(font, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
			
			float fontSize = 9f;
			SetFieldProperty(form, "Text1", invoice.Customer.Number, f, fontSize);
			SetFieldProperty(form, "Text2", invoice.Number, f, fontSize);
			SetFieldProperty(form, "Text3", invoice.Date.Value.ToString("yyyy-MM-dd"), f, fontSize);
			SetFieldProperty(form, "Text4", invoice.MaturityDate.Value.ToString("yyyy-MM-dd"), f, fontSize);
			
			SetFieldProperty(form, "Text5", invoice.Customer.YourReferencePerson, f, 8f);
			SetFieldProperty(form, "Text6", invoice.Customer.OurReferencePerson, f, 8f);
			
			SetFieldProperty(form, "Text6B", invoice.Customer.ToString() + "\n\n" + invoice.Customer.PurchaseOrderNumber, f, fontSize);
			
			SetFieldProperty(form, "Text10b", invoice.SubTotal.ToString("### ##0.00"), f, fontSize);
			SetFieldProperty(form, "Text13", invoice.TotalAmount.ToString("### ##0.00"), f, fontSize);
			
			string items = "";
			string quantities = "";
			string units = "";
			string prices = "";
			string amounts = "";
			foreach (var t in invoice.Timebooks) {
				items += t.Timebook.ToString() + "\n\n";
				quantities += t.Timebook.Quantity.ToString() + "\n\n";
				units += t.Timebook.Item.Unit.Name + "\n\n";
				prices += t.Timebook.Price.ToString("### ##0.00") + "\n\n";
				amounts += t.Timebook.Amount.ToString("### ##0.00") + "\n\n";
			}
			
			float itemFontSize = 8f;
			
			items += "\n\n" + invoice.Comments;
			
			SetFieldProperty(form, "Text7", items, f, itemFontSize);
			
			SetFieldProperty(form, "Text8", quantities, f, itemFontSize);
			
			SetFieldProperty(form, "Text9", units, f, itemFontSize);
			
			SetFieldProperty(form, "Text9b", prices, f, itemFontSize);
			
			SetFieldProperty(form, "Text9c", amounts, f, itemFontSize);
			
			if (flatten) {
				stamper.FormFlattening = true;
				foreach (var s in form.Fields.Keys) {
					stamper.PartialFormFlattening(s);
				}
			}
			
			var b = new IHGFVATBox(stamper.GetOverContent(1), invoice.VATs, font, form);
			b.Draw();
			
			stamper.Writer.CloseStream = false;
			stamper.Close();
			reader.Close();
			
			return output;
		}
		
		void SetFieldProperty(AcroFields form, string name, string text, BaseFont f, float size)
		{
			form.SetField(name, text);
			form.SetFieldProperty(name, "textfont", f, null);
			form.SetFieldProperty(name, "textsize", size, null);
		}
	}
	
	public class IHGFVATBox
	{
		PdfContentByte cb;
		float y = 87.5f;
		float height = 27f;
		IDictionary<decimal, decimal> vats;
		string calibriFont;
		AcroFields form;
		
		public IHGFVATBox(PdfContentByte cb, IDictionary<decimal, decimal> vats, string calibriFont, AcroFields form)
		{
			this.cb = cb;
			this.vats = vats;
			this.calibriFont = calibriFont;
			this.form = form;
		}
		
		public void Draw()
		{
			float x = 359f;
			int i = 0;
			
			var keys = vats.Keys.ToList().OrderByDescending(j => j);
			
			foreach (var v in keys) {
				if (i == 0) {
					form.SetField("Text11b", v.ToString());
					form.SetField("Text12", vats[v].ToString("### ##0.00"));
				} else {
					x -= 64f;
					
					DrawRectangle(x, y, 64f, height);
					SetTexts("MOMS, SEK", vats[v].ToString("### ##0.00"), x + 3, y + 19, y + 4.5f);
					
					x -= 41.5f;
					DrawRectangle(x, y, 41.5f, height);
					SetTexts("MOMS %", v.ToString(), x + 3, y + 19, y + 4.5f);
				}
				i++;
			}
		}
		
		void DrawRectangle(float x, float y, float width, float height)
		{
			cb.SetLineWidth(0.3);
			cb.SetColorStroke(BaseColor.DARK_GRAY);
			cb.Rectangle(x, y, width, height);
			cb.SaveState();
			cb.SetColorFill(new BaseColor(241, 241, 242));
			cb.Fill();
			cb.RestoreState();
			
			cb.Rectangle(x, y, width, height);
			cb.Stroke();
		}
		
		void SetTexts(string label, string val, float x, float y, float y2)
		{
			BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
			cb.SetColorFill(BaseColor.DARK_GRAY);
			cb.SetFontAndSize(bf, 6);

			cb.BeginText();
			cb.ShowTextAligned(Element.ALIGN_LEFT, label, x, y, 0);
			cb.EndText();
			
			bf = BaseFont.CreateFont(calibriFont, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
			cb.SetFontAndSize(bf, 10);
			cb.BeginText();
			cb.ShowTextAligned(Element.ALIGN_LEFT, val, x + 1, y2, 0);
			cb.EndText();
		}
	}
}
