// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.Core.Repositories;
using NUnit.Framework;

namespace HW.Grp.Tests.Repositories
{
	[TestFixture]
	public class ReportRepositoryTests
	{
		ReportRepositoryStub reportRepo = new ReportRepositoryStub();
		
		[Test]
		public void TestMethod()
		{
			var r = reportRepo.Read(1);
		}
	}
}
