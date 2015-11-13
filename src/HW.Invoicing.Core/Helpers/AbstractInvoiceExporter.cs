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
		string Name { get; }
		
		MemoryStream Export(Invoice invoice, string templateFileName, string calibriFont, bool flatten);
	}
	
	public interface IInvoicePDFGenerator
	{
		MemoryStream Generate(Invoice invoice, string templateFileName, string font, bool flatten);
	}
	
	public abstract class AbstractInvoiceExporter : IInvoiceExporter
	{
		public abstract string Name { get; }
		
		public abstract MemoryStream Export(Invoice invoice, string templateFileName, string calibriFont, bool flatten);
	}
	
	public abstract class AbstractInvoicePDFGenerator : IInvoicePDFGenerator
	{
		public abstract MemoryStream Generate(Invoice invoice, string templateFileName, string font, bool flatten);
		
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
}
