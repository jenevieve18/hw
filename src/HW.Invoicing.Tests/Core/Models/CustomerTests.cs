using System;
using HW.Invoicing.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Models
{
	[TestFixture]
	public class CustomerTests
	{
		[Test]
		public void TestMethod()
		{
			var r = new SqlCustomerRepository();
			var customers = r.FindActiveSubscribersByCompany(1, DateTime.Now, DateTime.Now.AddMonths(1).AddDays(-1));
			foreach (var c in customers) {
				Console.WriteLine(c.HasOpenSubscriptionTimebooks);
			}
		}
	}
}
