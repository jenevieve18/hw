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
	public class InvoiceExporter
	{
//		public string Type {
//			get { return "application/pdf"; }
//		}
//
//		public string GetContentDisposition(string file)
//		{
//			return string.Format("attachment;filename=\"{0}.pdf\";", file);
//		}
//
//		public bool HasContentDisposition(string file)
//		{
//			return GetContentDisposition(file).Length > 0;
//		}
//
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
		
		public MemoryStream Export(Invoice invoice, string templateFileName)
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
			
			stamper.FormFlattening = true;
			foreach (var s in form.Fields.Keys)
			{
				stamper.PartialFormFlattening(s);
			}
			
//			PdfContentByte cb = stamper.GetOverContent(1);
//			float y = 87.5f;
//			float height = 27f;
//			cb.Rectangle(400, y, 64, height);cb.Stroke();
//			cb.Rectangle(358.5, y, 41.5, height);cb.Stroke();
			
			var b = new VATBox(stamper.GetOverContent(1), invoice.VATs);
			b.Draw();
			
			stamper.Writer.CloseStream = false;
			stamper.Close();
			reader.Close();
			
			return output;
		}
		
		byte[] FlattenPdfFormToBytes(PdfReader reader)
		{
			var output = new MemoryStream();
			var stamper = new PdfStamper(reader, output) { FormFlattening = true };
			stamper.Close();
			return output.ToArray();
		}
		
		class VATBox
		{
			PdfContentByte cb;
			float y = 87.5f;
			float height = 27f;
			IDictionary<decimal, decimal> vats;
			
			public VATBox(PdfContentByte cb, IDictionary<decimal, decimal> vats)
			{
				this.cb = cb;
				this.vats = vats;
			}
			
			public void Draw()
			{
				vats.Remove(25);
				
				float x = 358.5f;
				foreach (var v in vats.Keys) {
					x -= 64f;
					cb.Rectangle(x, y, 64, height);cb.Stroke();
					SetTexts("MOMS, SEK", vats[v].ToString(), x + 20, y + 19, y + 5);
					
					x -= 41.5f;
					cb.Rectangle(x, y, 41.5, height);cb.Stroke();
					SetTexts("MOMS %", v.ToString(), x + 17, y + 19, y + 5);
					
//					cb.Rectangle(400, y, 64, height);cb.Stroke();
//					cb.Rectangle(358.5, y, 41.5, height);cb.Stroke();
				}
			}
			
			void SetTexts(string label, string val, float x, float y, float y2)
			{
				BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252,BaseFont.NOT_EMBEDDED);
				cb.SetColorFill(BaseColor.DARK_GRAY);
				cb.SetFontAndSize(bf, 6);

				cb.BeginText();
				cb.ShowTextAligned(1, label, x, y, 0);
				cb.EndText();
				
//				bf = BaseFont.CreateFont("Calibri", BaseFont.CP1252,BaseFont.NOT_EMBEDDED);
				cb.SetFontAndSize(bf, 9);
				cb.BeginText();
				cb.ShowTextAligned(1, val, x, y2, 0);
				cb.EndText();
			}
		}
	}
}
