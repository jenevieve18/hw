
using System;
using HW.Invoicing.Core.Repositories;
using HW.Invoicing.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Invoicing
{
	[TestFixture]
	public class UsersTests
	{
		[SetUp]
		public void Setup()
		{
			var v = new HW.Invoicing.Users(new SqlUserRepository());
		}
		
		[Test]
		public void TestIndex()
		{
			var v = new HW.Invoicing.Users(new UserRepositoryStub());
			v.Index();
		}
		
		[Test]
		public void TestLogout()
		{
			var v = new HW.Invoicing.Logout();
			v.LogOff();
		}
		
		[Test]
		public void TestDashboard()
		{
			var v = new HW.Invoicing.Dashboard(new UserRepositoryStub());
		}
		
		[Test]
		public void TestLogin()
		{
			var v = new HW.Invoicing.Default(new UserRepositoryStub());
			v.Login();
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
