//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core;
using NUnit.Framework;

namespace HW.Tests.Views
{
	[TestFixture]
	public class DefaultTests
	{
		SqlSponsorRepository sponsorRepository;
		SqlManagerFunctionRepository functionRepository;
		
		[SetUp]
		public void Setup()
		{
			sponsorRepository = new SqlSponsorRepository();
			functionRepository = new SqlManagerFunctionRepository();
		}
		
		[Test]
		public void TestLogin()
		{
			SponsorAdmin s = sponsorRepository.ReadSponsorAdmin(null, null, null, "", "Usr188", "Pas188");
			ManagerFunction f = functionRepository.ReadFirstFunctionBySponsorAdmin(s.Id);
		}
	}
}
