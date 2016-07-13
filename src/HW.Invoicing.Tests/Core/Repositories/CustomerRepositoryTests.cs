/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:25 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using HW.Invoicing.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Invoicing.Tests.Core.Repositories
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
