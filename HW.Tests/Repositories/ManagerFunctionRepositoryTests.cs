//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core;
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
		public void TestFindBySponsorAdmin2()
		{
			r.FindBySponsorAdmin2(742);
		}
		
		[Test]
		public void TestFindBySponsorAdmin()
		{
			r.ReadFirstFunctionBySponsorAdmin(742);
		}
	}
}
