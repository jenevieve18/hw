//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core;
using NUnit.Framework;

namespace HW.Tests.Views
{
	[TestFixture]
	public class ReportImageTests
	{
		SqlAnswerRepository ar;
		
		[SetUp]
		public void Setup()
		{
			ar = new SqlAnswerRepository();
		}
		
		[Test]
		public void TestMethod()
		{
			string GID = "0,8";
			string groupBy = "dbo.cf_year2WeekEven";
		}
		
		[Test]
		public void TestType8LinePlot()
		{
			ar.ReadByGroup("dbo.cf_year2WeekEven", 2012, 2013, "");
		}
		
		[Test]
		public void a()
		{
			var x = new SqlReportRepository().FindComponentsByPart(0);
		}
		
		[Test]
		public void b()
		{
			var x = new SqlDepartmentRepository().FindBySponsorWithSponsorAdmin(1, 1);
		}
		
		[Test]
		public void c()
		{
			var x = new SqlReportRepository().ReadComponentByPartAndLanguage(0, 1);
		}
	}
}
