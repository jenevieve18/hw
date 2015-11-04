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
//					case IHGF: return new IHGFInvoiceExporter();
					case IHGF: return new IHGFInvoiceExporter(new IHGFInvoicePDFScratchGenerator());
					case HCGF: return new HCGFInvoiceExporter();
					default: throw new NotSupportedException();
			}
		}
	}
}
