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
		MemoryStream Export(Invoice invoice, string templateFileName, string calibriFont, bool flatten);
	}
	
	public abstract class AbstractInvoiceExporter : IInvoiceExporter
	{
		public abstract MemoryStream Export(Invoice invoice, string templateFileName, string calibriFont, bool flatten);
	}
}
