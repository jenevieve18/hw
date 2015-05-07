
using System;
using HW.Invoicing.Core.Repositories;
using NUnit.Framework;

namespace HW.Tests.Invoicing
{
	[TestFixture]
	public class UsersTests
	{
		[Test]
		public void TestIndex()
		{
			var v = new HW.Invoicing.Users(new UserRepositoryStub());
			v.Index();
		}
		
		[Test]
		public void TestAdd()
		{
			var v = new HW.Invoicing.UserAdd(new UserRepositoryStub());
			v.Add();
		}
		
		[Test]
		public void TestEdit()
		{
			var v = new HW.Invoicing.UserEdit(new UserRepositoryStub());
			v.Edit(1);
		}
		
		[Test]
		public void TestDelete()
		{
			var v = new HW.Invoicing.UserDelete(new UserRepositoryStub());
			v.Delete(1);
		}
	}
}
