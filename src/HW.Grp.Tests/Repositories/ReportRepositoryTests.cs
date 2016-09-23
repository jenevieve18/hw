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
		ReportRepositoryStub r = new ReportRepositoryStub();
		
		[Test]
		public void TestMethod()
		{
			var x = r.Read(1);
		}
	}
}
