using System;
using HW.Invoicing.Core.Repositories;
using HW.Invoicing.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Invoicing
{
	[TestFixture]
	public class CustomersTests
	{
		[SetUp]
		public void Setup()
		{
			var v = new HW.Invoicing.Customers(new SqlCustomerRepository());
		}
		
		[Test]
		public void TestIndex()
		{
			var v = new HW.Invoicing.Customers(new CustomerRepositoryStub());
			v.Index();
		}
		
		[Test]
		public void TestAdd()
		{
			var v = new HW.Invoicing.CustomerAdd(new CustomerRepositoryStub());
			v.Add();
		}
		
		[Test]
		public void TestEdit()
		{
			var v = new HW.Invoicing.CustomerEdit(new CustomerRepositoryStub());
			v.Edit(1);
		}
		
		[Test]
		public void TestDelete()
		{
			var v = new HW.Invoicing.CustomerDelete(new CustomerRepositoryStub());
			v.Delete(1);
		}
	}
}
