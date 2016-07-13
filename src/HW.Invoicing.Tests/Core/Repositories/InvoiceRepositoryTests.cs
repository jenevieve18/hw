/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:26 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using HW.Invoicing.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Invoicing.Tests.Core.Repositories
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
