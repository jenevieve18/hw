using System;
using System.Diagnostics;
using System.IO;
using HW.Invoicing.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Helpers
{
	[TestFixture]
	public class AgreementExporterTests
	{
		HCGEAgreementExporter hcg;
		SqlCustomerRepository r = new SqlCustomerRepository();
		CustomerAgreement a;
		
		[SetUp]
		public void Setup()
		{
			hcg = new HCGEAgreementExporter();
			a = r.ReadAgreement(1);
			a.Customer = r.Read(a.Customer.Id);
		}
		
		[Test]
		public void TestExport()
		{
			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
				MemoryStream s = hcg.Export(a, @"HCG Avtal HCGE-PDF form example without comments.pdf", "calibri.ttf");
				s.WriteTo(f);
			}
			Process.Start("test.pdf");
		}
	}
}
