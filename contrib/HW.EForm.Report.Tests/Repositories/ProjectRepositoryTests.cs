// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;
using NUnit.Framework;
using System.Collections.Generic;

namespace HW.EForm.Report.Tests.Repositories
{
	[TestFixture]
	public class ProjectRepositoryTests
	{
		[Test]
		public void TestMethod()
		{
		}
	}
	
	public class ProjectRoundUnitRepositoryStub : BaseRepositoryStub<ProjectRoundUnit>, IProjectRoundUnitRepository
	{
		public IList<ProjectRoundUnit> FindProjectRoundUnits(int[] projectRoundUnitIDs)
		{
			throw new NotImplementedException();
		}
	}
}
