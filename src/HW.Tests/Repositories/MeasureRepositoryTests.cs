using System;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Repositories
{
	[TestFixture]
	public class MeasureRepositoryTests
	{
		[Test]
		public void TestMethod()
		{
			var measureRepository = new SqlMeasureRepository();
			string query = @"
INNER JOIN healthwatch..[User] u ON u.UserID = um.UserID
INNER JOIN healthwatch..UserProfile up ON up.UserID = u.UserID
INNER JOIN healthWatch..Department d ON d.DepartmentID = up.DepartmentID AND LEFT(d.SortString, 24) = '000000050000000600000007'";
			var measures = measureRepository.FindByQuestionAndOptionJoinedAndGrouped2(query, "dbo.cf_year2WeekEven", 2015, 2016, 4, 4, Group.GroupBy.TwoWeeksStartWithEven, 3);
			foreach (var m in measures) {
				Console.WriteLine(m.DT);
			}
		}
	}
}
