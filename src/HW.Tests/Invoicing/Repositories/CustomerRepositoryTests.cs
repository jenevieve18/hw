using System;
using HW.Invoicing.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Invoicing.Repositories
{
	[TestFixture]
	public class CustomerRepositoryTests
	{
		SqlCustomerRepository r;
		
		[SetUp]
		public void Setup()
		{
			r = new SqlCustomerRepository();
		}
		
		[Test]
		public void TestFindAll()
		{
			foreach (var c in r.FindAll()) {
				Console.WriteLine(c.Name);
			}
		}
	}
}
