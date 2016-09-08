// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Repositories
{
	[TestFixture]
	public class ManagerRepositoryTests
	{
		[Test]
		public void TestMethod()
		{
		}
	}
	
	public class ManagerRepositoryStub : BaseRepositoryStub<Manager>, IManagerRepository
	{
	}
}
