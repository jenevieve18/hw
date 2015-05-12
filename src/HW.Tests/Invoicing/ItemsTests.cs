using System;
using HW.Invoicing.Core.Repositories;
using HW.Invoicing.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Invoicing
{
	[TestFixture]
	public class ItemsTests
	{
		[Test]
		public void TestIndex()
		{
			var v = new HW.Invoicing.Items();
			
			v = new HW.Invoicing.Items(new ItemRepositoryStub());
			v.Index();
		}
		
		[Test]
		public void TestAdd()
		{
			var v = new HW.Invoicing.ItemAdd();
			
			v = new HW.Invoicing.ItemAdd(new ItemRepositoryStub());
			v.Add();
		}
		
		[Test]
		public void TestEdit()
		{
			var v = new HW.Invoicing.ItemEdit();
			
			v = new HW.Invoicing.ItemEdit(new ItemRepositoryStub());
			v.Edit(1);
		}
		
		[Test]
		public void TestDelete()
		{
			var v = new HW.Invoicing.ItemDelete();
			
			v = new HW.Invoicing.ItemDelete(new ItemRepositoryStub());
			v.Delete(1);
		}
	}
}
