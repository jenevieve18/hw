using System;
using HW.Core;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Repositories
{
	[TestFixture]
	public class ReportRepositoryTests
	{
		SqlReportRepository r;
		
		[SetUp]
		public void Setup()
		{
			r = new SqlReportRepository();
		}
		
		[Test]
		public void TestReadReportPart()
		{
			var p = r.ReadReportPart(1, 1);
		}
	}
}
