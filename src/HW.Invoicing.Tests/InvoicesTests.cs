/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:22 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NUnit.Framework;

namespace HW.Invoicing.Tests
{
	[TestFixture]
	public class InvoicesTests
	{
		[Test]
		public void TestIndex()
		{
			var v = new HW.Invoicing.Invoices();
			
			//v = new HW.Invoicing.Invoices(new InvoiceRepositoryStub());
			//v.Index();
		}
		
		[Test]
		public void TestAdd()
		{
			var v = new HW.Invoicing.InvoiceAdd();
			
			//v = new HW.Invoicing.InvoiceAdd(new InvoiceRepositoryStub(), new CustomerRepositoryStub(), new ItemRepositoryStub());
			//v.Add();
		}
		
		[Test]
		public void TestEdit()
		{
//			var v = new HW.Invoicing.InvoiceEdit(new InvoiceRepositoryStub());
//			v.Edit(1);
		}
		
		[Test]
		public void TestDelete()
		{
//			var v = new HW.Invoicing.InvoiceDelete(new InvoiceRepositoryStub());
//			v.Delete(1);
		}
	}
}
