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
	public class InvoiceExporterFactory
	{
		public const int IHGF = 1;
		public const int HCGF = 2;
		
		public static IInvoiceExporter GetExporter(int companyId)
		{
			switch (companyId) {
					case IHGF: return new IHGInvoiceExporter(new IHGInvoicePDFScratchGenerator());
//					case HCGF: return new HCGInvoiceExporter(new HCGInvoicePDFScratchGenerator());
//					case HCGF: return new HCGInvoiceExporter(new BaseInvoicePDFScratchGenerator());
//					case HCGF: return new HCGInvoiceExporter();
					case HCGF: return new DefaultInvoiceExporter();
					default: throw new NotSupportedException();
			}
		}
		
		public static IInvoiceExporter GetExporter2(int index)
		{
			var exporters = GetExporters();
			if (exporters.Count > index) {
				return exporters[index];
			} else {
				return new DefaultInvoiceExporter();
			}
//			return GetExporters()[index];
		}
		
		public static IList<IInvoiceExporter> GetExporters()
		{
			return new List<IInvoiceExporter>(
				new IInvoiceExporter[] {
					new IHGInvoiceExporter(),
//					new HCGInvoiceExporter(),
//					new VCInvoiceExporter()
					new DefaultInvoiceExporter()
				}
			);
		}
	}
}
