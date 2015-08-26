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
		IHGFInvoiceExporter ihg;
		HCGFInvoiceExporter hcg;
		
		[SetUp]
		public void Setup()
		{
			i = new SqlInvoiceRepository().Read(52);
			
			ihg = new IHGFInvoiceExporter();
			hcg = new HCGFInvoiceExporter();
		}
		
		[Test]
		public void TestIHGFExporter()
		{
			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
				MemoryStream s = ihg.Export(i, @"IHG faktura MALL Ian without comments.pdf", "calibri.ttf");
				s.WriteTo(f);
			}
			Process.Start("test.pdf");
		}
		
//		[Test]
//		public void TestIHGFExporter2()
//		{
//			ihg.Export2(i, @"IHG faktura MALL Ian without comments.pdf");
//			Process.Start("test.pdf");
//		}
		
		[Test]
		public void TestHCGFExporter()
		{
			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
				MemoryStream s = hcg.Export(i, @"HCG Fakturamall tom without comments.pdf", "calibri.ttf");
				s.WriteTo(f);
			}
			Process.Start("test.pdf");
		}
		
//		[Test]
//		public void TestHCGFExporter2()
//		{
//			hcg.Export2(i, @"HCG Fakturamall tom without comments.pdf", "calibri.ttf");
//			Process.Start("test.pdf");
//		}
	}
}
