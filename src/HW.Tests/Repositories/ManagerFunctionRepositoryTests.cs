using System;
using HW.Core;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Repositories
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
		
		[Test]
		public void TestFindBySponsorAdmin()
		{
			r.FindBySponsorAdmin(5);
		}
		
		[Test]
		public void TestReadFirstFunctionBySponsorAdmin()
		{
			r.ReadFirstFunctionBySponsorAdmin(742);
		}
	}
}
