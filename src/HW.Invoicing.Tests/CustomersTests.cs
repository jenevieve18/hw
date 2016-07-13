/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:22 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using HW.Invoicing.Core.Repositories;
using NUnit.Framework;

namespace HW.Invoicing.Tests
{
	[TestFixture]
	public class CustomersTests
	{
		[Test]
		public void TestIndex()
		{
			var v = new HW.Invoicing.Customers();
			
			//v = new HW.Invoicing.Customers(new CustomerRepositoryStub());
			//v.Index();
		}
		
		[Test]
		public void TestAdd()
		{
			var v = new HW.Invoicing.CustomerAdd();
			
			//v = new HW.Invoicing.CustomerAdd(new CustomerRepositoryStub());
			//v.Add();
		}
		
		[Test]
		public void TestEdit()
		{
			//var v = new HW.Invoicing.CustomerEdit();
			
//			v = new HW.Invoicing.CustomerEdit(new CustomerRepositoryStub());
//			v.Edit(1);
		}
		
		[Test]
		public void TestDelete()
		{
			var v = new HW.Invoicing.CustomerDelete();
			
			v = new HW.Invoicing.CustomerDelete(new CustomerRepositoryStub());
			v.Delete(1);
		}
	}
}
