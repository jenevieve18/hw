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
			
			ihg = new IHGFInvoiceExporter(new IHGFInvoicePDFScratchGenerator());
			hcg = new HCGFInvoiceExporter();
		}
		
		[Test]
		public void TestIHGFExporter()
		{
			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create, FileAccess.Write)) {
				var x = new Invoice {
					Customer = new Customer {
						Name = "Carl T. Escalante",
						PurchaseOrderNumber = "XXX-01234",
						YourReferencePerson = "Gerald S. Hicks",
						OurReferencePerson = "Michael K. Smith",
						InvoiceAddress = @"Carl T. Escalante
37 East Avenue
Scottsdale, AZ 85256"
					},
					Number = "IHGF-001",
					Date = DateTime.Now,
					MaturityDate = DateTime.Now,
					Company = new Company {
						Name = "Interactive Health Group in Stockholm AB",
						Address = "Rörstrandsgatan 36, 113 40 Stockholm, Sweden",
						BankAccountNumber = "5091 – 8853",
						Phone = "+46-70-7284298",
						TIN = "SE556712369901",
						InvoiceLogo = "ihg.png"
					},
					Timebooks = new System.Collections.Generic.List<InvoiceTimebook>(
						new InvoiceTimebook[] {
							new InvoiceTimebook {
								Timebook = new CustomerTimebook {
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
									},
									Date = DateTime.Now,
									Quantity = 1,
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
										Unit = new Unit { Name = "months" }
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
										Unit = new Unit { Name = "months" }
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
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
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
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
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
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
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
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
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
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
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
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
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
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
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
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
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
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
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
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
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
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
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
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
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
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
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
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
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
				MemoryStream s = ihg.Export(x, @"IHG faktura MALL Ian without comments.pdf", "arial.ttf", true);
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
				var x = new Invoice {
					Customer = new Customer {
						PurchaseOrderNumber = "XXX-001",
						Name = "Carl T. Escalante",
						YourReferencePerson = "Gerald S. Hicks",
						OurReferencePerson = "Michael K. Smith",
						InvoiceAddress = @"37 East Avenue
Scottsdale, AZ 85256"
					},
					Number = "IHGF-001",
					Date = DateTime.Now,
					MaturityDate = DateTime.Now,
					Timebooks = new System.Collections.Generic.List<InvoiceTimebook>(
						new InvoiceTimebook[] {
							new InvoiceTimebook {
								Timebook = new CustomerTimebook {
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
									},
									Quantity = 1,
									Price = 100,
									VAT = 16,
									Consultant = "Debbie G. Jackson",
									Comments = "The cosmos is rooted in visible excellence"
								}
							},
							new InvoiceTimebook {
								Timebook = new CustomerTimebook {
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
									},
									Quantity = 1,
									Price = 100,
									VAT = 25,
									Consultant = "Debbie G. Jackson",
									Comments = "The cosmos is rooted in visible excellence"
								}
							},
							new InvoiceTimebook {
								Timebook = new CustomerTimebook {
									Item = new Item {
										Name = "Programming",
										Unit = new Unit { Name = "months" }
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
				MemoryStream s = hcg.Export(x, @"HCG Fakturamall tom without comments.pdf", "calibri.ttf", true);
//				MemoryStream s = hcg.Export(i, @"HCG Fakturamall tom without comments.pdf", "calibri.ttf");
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
