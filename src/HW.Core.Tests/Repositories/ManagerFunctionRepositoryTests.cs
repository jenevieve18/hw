// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.Core;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Core.Tests.Repositories
{
	[TestFixture]
	public class ManagerFunctionRepositoryTests
	{
		SqlManagerFunctionRepository r;
		
		[SetUp]
		public void Setup()
		{
			r = new SqlManagerFunctionRepository();
		}
		
		[Test]
		public void TestFindAll()
		{
			r.FindAll();
		}
	}
}
