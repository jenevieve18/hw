// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.Core.Helpers;
using HW.Core.Util.Exporters;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Util.Graphs;
using NUnit.Framework;

namespace HW.Grp.Tests.Helpers
{
	[TestFixture]
	public class UserLevelGraphFactoryTests
	{
		UserLevelGraphFactory f;
		
		[Test]
		public void TestMethod()
		{
			f = new UserLevelGraphFactory(new SqlAnswerRepository(), new SqlReportRepository());
			var g = f.CreateGraph("", new ReportPart(), 1, 1, new DateTime(2016, 1, 1), new DateTime(2016, 1, 1), 1, true, 1, 100, 100, "#efefef", 1, 1, 1, "0", null, 0, 10);
		}
		
		[Test]
		public void TestMethod2()
		{
			int i = 0;
			f.CreateGraphForExcelWriter(new ReportPart(), 1, new ProjectRoundUnit(), DateTime.Now, DateTime.Now, 1, true, 0, 1, new SponsorAdmin(), new Sponsor(), "0", new ExcelWriter(null), ref i);
		}
	}
}
