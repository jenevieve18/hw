using System;
using HW.Invoicing.Core.Repositories.Sql;
using NUnit.Framework;
using System.Linq;

namespace HW.Tests.Invoicing.Repositories
{
	[TestFixture]
	public class InvoiceRepositoryTests
	{
		SqlInvoiceRepository r;
		
		[SetUp]
		public void Setup()
		{
			r = new SqlInvoiceRepository();
		}
		
		[Test]
		public void TestFindAll()
		{
			foreach (var i in r.FindAll()) {
				Console.WriteLine(i.Date);
			}
		}
	}
}
