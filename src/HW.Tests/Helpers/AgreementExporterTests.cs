﻿using System;
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
		public void TestExport2()
		{
			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
				var x = new CustomerAgreement {
					Id = 1,
					Customer = new Customer {
						Name = "Customer1",
						InvoiceAddress = @"Customer 1
Rörstrandsgatan 36, 113 40 Stockholm
Org.nr 556757-0568"
					},
					LectureTitle = "Detta är bara för att du ska få testa funktionaliteten...Fyll gärna i och tryck skicka.",
					Contact = "Ian Escarro",
					Mobile = "09228545058",
					Email = "ian.escarro@gmail.com",
					Compensation = 124.56M,
					OtherInformation = "",
					ContactPlaceSigned = "Cebu City",
					ContactDateSigned = DateTime.Now,
					ContactName = "Ian Escarro",
					ContactTitle = "Programmer",
					ContactCompany = "ABC Corporation",
					DateSigned = DateTime.Now,
					DateTimeAndPlaces = new System.Collections.Generic.List<CustomerAgreementDateTimeAndPlace>(
						new CustomerAgreementDateTimeAndPlace[] {
							new CustomerAgreementDateTimeAndPlace { Date = DateTime.Now, TimeFrom = "11:00", TimeTo = "12:00", Address = "Compostela Cebu 6003 Philippines" },
							new CustomerAgreementDateTimeAndPlace { Date = DateTime.Now, TimeFrom = "11:00", TimeTo = "12:00", Address = "Compostela Cebu 6003 Philippines" },
							new CustomerAgreementDateTimeAndPlace { Date = DateTime.Now, TimeFrom = "11:00", TimeTo = "12:00", Address = "Compostela Cebu 6003 Philippines" },
							new CustomerAgreementDateTimeAndPlace { Date = DateTime.Now, TimeFrom = "11:00", TimeTo = "12:00", Address = "Compostela Cebu 6003 Philippines" }
						}
					)
				};
				MemoryStream s = hcg.Export(x, @"HCG Avtalsmall Latest.pdf", "calibri.ttf");
				s.WriteTo(f);
			}
			Process.Start("test.pdf");
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
