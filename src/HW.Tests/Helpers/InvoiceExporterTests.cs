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
		Invoice invoice;
		
		IHGInvoiceExporter ihg;
		DefaultInvoiceExporter def;
		
		[SetUp]
		public void Setup()
		{
			invoice = new Invoice {
				Customer = new Customer {
					Number = "XXXYY-0123",
					Name = "Carl T. Escalante",
					PurchaseOrderNumber = "XXX-01234",
					ContactPerson = new CustomerContact { Name  = "Gerald S. Hicks" },
					OurReferencePerson = "Michael K. Smith",
//					Language = Language.GetLanguage(1),
//					Currency = Currency.GetCurrency(1)
					InvoiceAddress = @"Carl T. Escalante
37 East Avenue
Scottsdale, AZ 85256"
				},
				Number = "IHGF-001",
				Date = DateTime.Now,
				MaturityDate = DateTime.Now,
				Comments = "The invisible co creates great life. Your heart is entangled in unbridled actions.",
				Timebooks = new System.Collections.Generic.List<InvoiceTimebook>(
					new InvoiceTimebook[] {
						new InvoiceTimebook {
							Timebook = new CustomerTimebook {
								Comments = "ABB",
								IsHeader = true
							}
						},
						new InvoiceTimebook {
							Timebook = new CustomerTimebook {
								Item = new Item {
									Name = "Programming",
									Unit = new Unit { Name = "month" }
								},
								Date = DateTime.Now,
								Quantity = 1234,
								Price = 100,
								VAT = 16,
								Consultant = "Debbie G. Jackson",
								Comments = "Programmering av ny WebbQPS baserat på önskemål från chefen på RIA"
							}
						},
						new InvoiceTimebook {
							Timebook = new CustomerTimebook {
								Item = new Item {
									Name = "Programming",
									Unit = new Unit { Name = "month" }
								},
								Date = DateTime.Now,
								Quantity = 1,
								Price = 100,
								VAT = 25,
								Consultant = "Debbie G. Jackson",
								Comments = "Framtagande av medelvärden från RIAs senaste Webb-QPS samt kontrastering av värden mot de värden som finns i QPS-Nordics manual."
							}
						},
						new InvoiceTimebook {
							Timebook = new CustomerTimebook {
								Item = new Item {
									Name = "Programming",
									Unit = new Unit { Name = "month" }
								},
								Quantity = 1,
								Price = 100,
								VAT = 10,
								Consultant = "Debbie G. Jackson",
								Comments = "The cosmos is rooted in visible excellence"
							}
						},
						new InvoiceTimebook {
							Timebook = new CustomerTimebook {
								Comments = "ABC",
								IsHeader = true
							}
						},
						new InvoiceTimebook {
							Timebook = new CustomerTimebook {
								Item = new Item {
									Name = "Programming",
									Unit = new Unit { Name = "month" }
								},
								Quantity = 1,
								Price = 100,
								VAT = 10,
								Consultant = "Debbie G. Jackson",
								Comments = "The cosmos is rooted in visible excellence"
							}
						}
					}
				)
			};
			
			ihg = new IHGInvoiceExporter();
			def = new DefaultInvoiceExporter();
		}
		
		[Test]
		public void TestIHGExporter()
		{
			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
				invoice.Company = new Company {
					Name = "Interactive Health Group in Stockholm AB",
					Address = @"Rörstrandsgatan 36, 113 40
Stockholm, Sweden",
					BankAccountNumber = "5091 – 8853",
					OrganizationNumber = "556757-0568",
					Phone = "+46-70-7284298",
					TIN = "SE556712369901",
					InvoiceLogo = "ihg.png",
					Website = "www.healthwatch.se",
					Email = "dan.hasson@healthwatch.se"
				};
				MemoryStream s = ihg.Export(invoice);
				s.WriteTo(f);
			}
			Process.Start("test.pdf");
		}
		
		[Test]
		public void TestIHGExporterWithNullCustomer()
		{
			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
				invoice.Company = new Company {
					Name = "Interactive Health Group in Stockholm AB",
					Address = @"Rörstrandsgatan 36, 113 40
Stockholm, Sweden",
					BankAccountNumber = "5091 – 8853",
					OrganizationNumber = "556757-0568",
					Phone = "+46-70-7284298",
					TIN = "SE556712369901",
					InvoiceLogo = "ihg.png",
					Website = "www.healthwatch.se",
					Email = "dan.hasson@healthwatch.se"
				};
				invoice.Customer = null;
				MemoryStream s = ihg.Export(invoice);
				s.WriteTo(f);
			}
			Process.Start("test.pdf");
		}
		
		[Test]
		public void TestIHGExporterWithNullCustomerContact()
		{
			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
				invoice.Company = new Company {
					Name = "Interactive Health Group in Stockholm AB",
					Address = @"Rörstrandsgatan 36, 113 40
Stockholm, Sweden",
					BankAccountNumber = "5091 – 8853",
					OrganizationNumber = "556757-0568",
					Phone = "+46-70-7284298",
					TIN = "SE556712369901",
					InvoiceLogo = "ihg.png",
					Website = "www.healthwatch.se",
					Email = "dan.hasson@healthwatch.se"
				};
				invoice.Customer.ContactPerson = null;
				MemoryStream s = ihg.Export(invoice);
				s.WriteTo(f);
			}
			Process.Start("test.pdf");
		}
		
		[Test]
		public void TestDefaultExporter()
		{
			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
				invoice.Company = new Company {
					Name = "Interactive Health Group in Stockholm AB",
					Address = @"Rörstrandsgatan 36, 113 40
Stockholm, Sweden",
					BankAccountNumber = "5091 – 8853",
					Phone = "+46-70-7284298",
					TIN = "SE556712369901",
					InvoiceLogo = "hcg.png",
					Website = "www.danhasson.se",
					Email = "info@danhasson.se",
					OrganizationNumber = "556757-0568"
				};
				MemoryStream s = def.Export(invoice);
				s.WriteTo(f);
			}
			Process.Start("test.pdf");
		}
		
		[Test]
		public void TestDefaultExporterWithNullCustomer()
		{
			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
				invoice.Company = new Company {
					Name = "Interactive Health Group in Stockholm AB",
					Address = @"Rörstrandsgatan 36, 113 40
Stockholm, Sweden",
					BankAccountNumber = "5091 – 8853",
					Phone = "+46-70-7284298",
					TIN = "SE556712369901",
					InvoiceLogo = "hcg.png",
					Website = "www.danhasson.se",
					Email = "info@danhasson.se",
					OrganizationNumber = "556757-0568"
				};
				invoice.Customer = null;
				MemoryStream s = def.Export(invoice);
				s.WriteTo(f);
			}
			Process.Start("test.pdf");
		}
		
		[Test]
		public void TestDefaultExporterWithNullCustomerContact()
		{
			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
				invoice.Company = new Company {
					Name = "Interactive Health Group in Stockholm AB",
					Address = @"Rörstrandsgatan 36, 113 40
Stockholm, Sweden",
					BankAccountNumber = "5091 – 8853",
					Phone = "+46-70-7284298",
					TIN = "SE556712369901",
					InvoiceLogo = "hcg.png",
					Website = "www.danhasson.se",
					Email = "info@danhasson.se",
					OrganizationNumber = "556757-0568"
				};
				invoice.Customer.ContactPerson = null;
				MemoryStream s = def.Export(invoice);
				s.WriteTo(f);
			}
			Process.Start("test.pdf");
		}
		
//		[Test]
//		public void TestVCExporter()
//		{
//			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
//				invoice.Company = new Company {
//					Name = "Villaume Consulting AB",
//					Address = "Storholmsvägen 382, 132 52 Saltsjö-Boo",
//					BankAccountNumber = "425 – 7291",
//					Phone = "+46 70 241 56 90",
//					TIN = "SE556942889801",
//					InvoiceLogo = "vc.png",
//					Email = "karin.villaume@gmail.com",
//					OrganizationNumber = "556942-8898"
//				};
//				MemoryStream s = vc.Export(invoice, @"HCG Fakturamall tom without comments.pdf", "calibri.ttf", true);
//				s.WriteTo(f);
//			}
//			Process.Start("test.pdf");
//		}
//		
//		[Test]
//		public void TestVCExporterWithNullCustomer()
//		{
//			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
//				invoice.Company = new Company {
//					Name = "Villaume Consulting AB",
//					Address = "Storholmsvägen 382, 132 52 Saltsjö-Boo",
//					BankAccountNumber = "425 – 7291",
//					Phone = "+46 70 241 56 90",
//					TIN = "SE556942889801",
//					InvoiceLogo = "vc.png",
//					Email = "karin.villaume@gmail.com",
//					OrganizationNumber = "556942-8898"
//				};
//				invoice.Customer = null;
//				MemoryStream s = vc.Export(invoice, @"HCG Fakturamall tom without comments.pdf", "calibri.ttf", true);
//				s.WriteTo(f);
//			}
//			Process.Start("test.pdf");
//		}
//		
//		[Test]
//		public void TestVCExporterWithNullCustomerContact()
//		{
//			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
//				invoice.Company = new Company {
//					Name = "Villaume Consulting AB",
//					Address = "Storholmsvägen 382, 132 52 Saltsjö-Boo",
//					BankAccountNumber = "425 – 7291",
//					Phone = "+46 70 241 56 90",
//					TIN = "SE556942889801",
//					InvoiceLogo = "vc.png",
//					Email = "karin.villaume@gmail.com",
//					OrganizationNumber = "556942-8898"
//				};
//				invoice.Customer.ContactPerson = null;
//				MemoryStream s = vc.Export(invoice, @"HCG Fakturamall tom without comments.pdf", "calibri.ttf", true);
//				s.WriteTo(f);
//			}
//			Process.Start("test.pdf");
//		}
	}
}
