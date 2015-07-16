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
		MemoryStream Export(Invoice invoice, string templateFileName, string calibriFont);
	}
	
	public abstract class AbstractInvoiceExporter : IInvoiceExporter
	{
        public abstract MemoryStream Export(Invoice invoice, string templateFileName, string calibriFont);
	}
	
	public class InvoiceExporterFactory
	{
		public const int IHGF = 1;
		public const int HCGF = 2;
		
		public static IInvoiceExporter GetExporter(int companyId)
		{
			switch (companyId) {
					case IHGF: return new IHGFInvoiceExporter();
					case HCGF: return new HCGFInvoiceExporter();
					default: throw new NotSupportedException();
			}
		}
	}
	
	public class HCGFInvoiceExporter : AbstractInvoiceExporter
	{
		public override MemoryStream Export(Invoice invoice, string templateFileName, string calibriFont)
		{
			MemoryStream output = new MemoryStream();
			
			var templateFileStream = new FileStream(templateFileName, FileMode.Open);
			var reader = new PdfReader(templateFileStream);
			var stamper = new PdfStamper(reader, output);
			var form = stamper.AcroFields;
			var fieldKeys = form.Fields.Keys;
			
//			foreach (var k in fieldKeys) {
//				form.SetField(k, k);
//			}
			
			form.SetField("Text1", invoice.Customer.Number);
			form.SetField("Text2", invoice.Number);
			form.SetField("Text3", invoice.Date.Value.ToString("yyyy-MM-dd"));
			form.SetField("Text4", invoice.MaturityDate.Value.ToString("yyyy-MM-dd"));
			
			form.SetField("Text5", invoice.Customer.YourReferencePerson);
			form.SetField("Text6", invoice.Customer.OurReferencePerson);
			
			form.SetField("Text7", invoice.Customer.ToString());
			
			form.SetField("Text10", invoice.SubTotal.ToString("0.00"));
			form.SetField("Text13", invoice.TotalAmount.ToString("0.00"));
			
			string items = "";
//			string quantities = "";
//			string units = "";
//			string prices = "";
			string amounts = "";
			foreach (var t in invoice.Timebooks) {
				items += t.Timebook.ToString() + "\n\n";
//				quantities += t.Timebook.Quantity.ToString() + "\n\n";
//				units += t.Timebook.Item.Unit.Name + "\n\n";
//				prices += t.Timebook.Price.ToString("0.00") + "\n\n";
				amounts += t.Timebook.Amount.ToString("0.00") + "\n\n";
			}
			form.SetField("Text8", items);
//			form.SetField("Text9", prices);
//			form.SetField("Text9", units);
//			form.SetField("Text9b", prices);
			form.SetField("Text9", amounts);
			
			if (invoice.VATs.ContainsKey(25))
			{
				form.SetField("Text11", 25.ToString());
				form.SetField("Text12", invoice.VATs[25].ToString("0.00"));
			}
			
			stamper.FormFlattening = true;
			foreach (var s in form.Fields.Keys)
			{
				stamper.PartialFormFlattening(s);
			}
			
//			var b = new VATBox(stamper.GetOverContent(1), invoice.VATs, calibriFont);
//			b.Draw();
			
			stamper.Writer.CloseStream = false;
			stamper.Close();
			reader.Close();
			
			return output;
		}
	}
	
	public class IHGFInvoiceExporter : AbstractInvoiceExporter
	{
		public void Export2(Invoice invoice, string existingFileName)
		{
			string newFile = @"test.pdf";
			PdfReader reader = new PdfReader(existingFileName);
			PdfStamper stamper = new PdfStamper(reader, new FileStream(newFile, FileMode.Create));
			AcroFields form = stamper.AcroFields;
			var fieldKeys = form.Fields.Keys;
			
			form.SetField("Text1", invoice.Customer.Number);
			form.SetField("Text2", invoice.Number);
			form.SetField("Text3", invoice.Date.Value.ToString("yyyy-MM-dd"));
			form.SetField("Text4", invoice.MaturityDate.Value.ToString("yyyy-MM-dd"));
			
			form.SetField("Text5", invoice.Customer.YourReferencePerson);
			form.SetField("Text6", invoice.Customer.OurReferencePerson);
			
			form.SetField("Text6B", invoice.Customer.ToString());
			
			form.SetField("Text10b", invoice.SubTotal.ToString());
			form.SetField("Text13", invoice.TotalAmount.ToString());
			
			string items = "";
			string quantities = "";
			string units = "";
			string prices = "";
			string amounts = "";
			foreach (var t in invoice.Timebooks) {
				items += t.ToString() + "\n\n";
				quantities += t.Timebook.Quantity.ToString() + "\n\n";
				units += t.Timebook.Item.Unit.Name + "\n\n";
				prices += t.Timebook.Price.ToString() + "\n\n";
				amounts += t.Timebook.Amount.ToString() + "\n\n";
			}
			form.SetField("Text7", items);
			form.SetField("Text8", quantities);
			form.SetField("Text9", units);
			form.SetField("Text9b", prices);
			form.SetField("Text9c", amounts);
			
			if (invoice.VATs.ContainsKey(25))
			{
				form.SetField("Text11b", 25.ToString());
				form.SetField("Text12", invoice.VATs[25].ToString());
			}
			
			// "Flatten" the form so it wont be editable/usable anymore
			stamper.FormFlattening = true;
			
			// You can also specify fields to be flattened, which
			// leaves the rest of the form still be editable/usable
			foreach (var s in form.Fields.Keys)
			{
				stamper.PartialFormFlattening(s);
			}
			
			stamper.Close();
			reader.Close();
		}
		
		public override MemoryStream Export(Invoice invoice, string templateFileName, string calibriFont)
		{
			MemoryStream output = new MemoryStream();
			
			var templateFileStream = new FileStream(templateFileName, FileMode.Open);
			var reader = new PdfReader(templateFileStream);
			var stamper = new PdfStamper(reader, output) {
//				FormFlattening = true,
//				FreeTextFlattening = true
			};
			var form = stamper.AcroFields;
			var fieldKeys = form.Fields.Keys;
			
			form.SetField("Text1", invoice.Customer.Number);
			form.SetField("Text2", invoice.Number);
			form.SetField("Text3", invoice.Date.Value.ToString("yyyy-MM-dd"));
			form.SetField("Text4", invoice.MaturityDate.Value.ToString("yyyy-MM-dd"));
			
			form.SetField("Text5", invoice.Customer.YourReferencePerson);
			form.SetField("Text6", invoice.Customer.OurReferencePerson);
			
			form.SetField("Text6B", invoice.Customer.ToString());
			
			form.SetField("Text10b", invoice.SubTotal.ToString("0.00"));
			form.SetField("Text13", invoice.TotalAmount.ToString("0.00"));
			
			string items = "";
			string quantities = "";
			string units = "";
			string prices = "";
			string amounts = "";
			foreach (var t in invoice.Timebooks) {
				items += t.Timebook.ToString() + "\n\n";
				quantities += t.Timebook.Quantity.ToString() + "\n\n";
				units += t.Timebook.Item.Unit.Name + "\n\n";
				prices += t.Timebook.Price.ToString("0.00") + "\n\n";
				amounts += t.Timebook.Amount.ToString("0.00") + "\n\n";
			}
			form.SetField("Text7", items);
			form.SetField("Text8", quantities);
			form.SetField("Text9", units);
			form.SetField("Text9b", prices);
			form.SetField("Text9c", amounts);
			
			if (invoice.VATs.ContainsKey(25))
			{
				form.SetField("Text11b", 25.ToString());
				form.SetField("Text12", invoice.VATs[25].ToString("0.00"));
			}
			
			stamper.FormFlattening = true;
			foreach (var s in form.Fields.Keys)
			{
				stamper.PartialFormFlattening(s);
			}
			
			var b = new VATBox(stamper.GetOverContent(1), invoice.VATs, calibriFont);
			b.Draw();
			
			stamper.Writer.CloseStream = false;
			stamper.Close();
			reader.Close();
			
			return output;
		}
		
//		byte[] FlattenPdfFormToBytes(PdfReader reader)
//		{
//			var output = new MemoryStream();
//			var stamper = new PdfStamper(reader, output) { FormFlattening = true };
//			stamper.Close();
//			return output.ToArray();
//		}
	}
	
	public class VATBox
	{
		PdfContentByte cb;
		float y = 87.5f;
		float height = 27f;
		IDictionary<decimal, decimal> vats;
		string calibriFont;
		
		public VATBox(PdfContentByte cb, IDictionary<decimal, decimal> vats, string calibriFont)
		{
			this.cb = cb;
			this.vats = vats;
			this.calibriFont = calibriFont;
		}
		
		public void Draw()
		{
			if (vats.ContainsKey(25)) {
				vats.Remove(25);
			}
			
			float x = 358.5f;
			foreach (var v in vats.Keys) {
				x -= 64f;
				DrawRectangle(x, y, 64f, height);
				SetTexts("MOMS, SEK", vats[v].ToString("0.00"), x + 3, y + 19, y + 4.5f);
				
				x -= 41.5f;
				DrawRectangle(x, y, 41.5f, height);
				SetTexts("MOMS %", v.ToString(), x + 3, y + 19, y + 4.5f);
			}
		}
		
		void DrawRectangle(float x, float y, float width, float height)
		{
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
