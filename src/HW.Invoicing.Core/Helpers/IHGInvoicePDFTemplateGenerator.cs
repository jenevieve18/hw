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
	public class IHGInvoicePDFTemplateGenerator : IInvoicePDFGenerator
	{
	//		public MemoryStream Generate(Invoice invoice, string templateFileName, string font, bool flatten)
		public MemoryStream Generate(Invoice invoice, string templateFileName, string font)
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
			
	//			SetFieldProperty(form, "Text5", invoice.Customer.YourReferencePerson, f, 8f);
			SetFieldProperty(form, "Text5", invoice.Customer.ContactPerson.Name, f, 8f);
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
			
	//			if (flatten) {
	//				stamper.FormFlattening = true;
	//				foreach (var s in form.Fields.Keys) {
	//					stamper.PartialFormFlattening(s);
	//				}
	//			}
			
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
}
