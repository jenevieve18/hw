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
	
	public class ProjectRoundRepositoryStub : BaseRepositoryStub<ProjectRound>, IProjectRoundRepository
	{
	}
	
	public class ProjectRoundUnitRepositoryStub : BaseRepositoryStub<ProjectRoundUnit>, IProjectRoundUnitRepository
	{
		public ProjectRoundUnitRepositoryStub()
		{
			for (int i = 1; i <= 10; i++) {
				items.Add(new ProjectRoundUnit { ProjectRoundID = 1, ProjectRoundUnitID = i, Unit = "Unit" + i });
			}
		}
		
		public IList<ProjectRoundUnit> FindProjectRoundUnits(int[] projectRoundUnitIDs)
		{
			var units = new List<ProjectRoundUnit>();
			foreach (var i in projectRoundUnitIDs) {
				foreach (var u in items) {
					if (u.ProjectRoundUnitID == i) {
						units.Add(u);
					}
				}
			}
			return units;
		}
	}
}
