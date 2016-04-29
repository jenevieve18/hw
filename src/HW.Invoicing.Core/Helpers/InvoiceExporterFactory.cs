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
		public const int DEFAULT = 2;
		
		public static IInvoiceExporter GetExporter(int exporter)
		{
			switch (exporter) {
					case IHGF: return new IHGInvoiceExporter(new IHGInvoicePDFScratchGenerator());
					case DEFAULT: return new DefaultInvoiceExporter();
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
		}
		
		public static IList<IInvoiceExporter> GetExporters()
		{
			return new List<IInvoiceExporter>(
				new IInvoiceExporter[] {
					new IHGInvoiceExporter(),
					new DefaultInvoiceExporter()
				}
			);
		}
	}
}
