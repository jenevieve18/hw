using System;
using System.Diagnostics;
using System.IO;
using HW.Invoicing.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;
using iTextSharp.text.pdf;
using NUnit.Framework;

namespace HW.Tests.Helpers
{
	[TestFixture]
	public class InvoiceExporterTests
	{
		Invoice i;
		IHGFInvoiceExporter e;
		
		[SetUp]
		public void Setup()
		{
			i = new SqlInvoiceRepository().Read(52);
			e = new IHGFInvoiceExporter();
		}
		
		[Test]
		public void TestExport()
		{
			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
				MemoryStream s = e.Export(i, @"IHG faktura MALL Ian without comments.pdf", @"calibri.ttf");
				s.WriteTo(f);
			}
			Process.Start("test.pdf");
		}
		
		[Test]
		public void TestExport2()
		{
			e.Export2(i, @"IHG faktura MALL Ian without comments.pdf");
			Process.Start("test.pdf");
		}
		
		[Test]
		public void a()
		{
			var x = new HCGFInvoiceExporter();
			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
				MemoryStream s = x.Export(i, @"HCG Fakturamall tom without comments.pdf", @"calibri.ttf");
				s.WriteTo(f);
			}
			Process.Start("test.pdf");
		}
	}
}
