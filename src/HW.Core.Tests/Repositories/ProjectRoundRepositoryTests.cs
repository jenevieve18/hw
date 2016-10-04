// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using NUnit.Framework;

namespace HW.Core.Tests.Repositories
{
	[TestFixture]
	public class ProjectRoundRepositoryTests
	{
//		ProjectService s = new ProjectService(new SqlProjectRepository(),
//		                                      new SqlProjectRoundUnitRepository(),
//		                                      new SqlProjectRoundUserRepository());
		ProjectService s = new ProjectService();
		
		[Test]
		public void TestMethod()
		{
			var x = s.ReadProjectRoundUnit(3476);
			foreach (var y in x.ProjectRoundUsers) {
				Console.WriteLine(y.Email);
			}
		}
	}
}
