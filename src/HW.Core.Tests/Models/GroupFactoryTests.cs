// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections;
using System.Collections.Generic;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Core.Tests.Models
{
	[TestFixture]
	public class GroupFactoryTests
	{
		[Test]
		public void TestGetGroupByQuery()
		{
			Assert.AreEqual("dbo.cf_yearWeek", GroupFactory.GetGroupByQuery(GroupBy.OneWeek));
			Assert.AreEqual("dbo.cf_year2Week", GroupFactory.GetGroupByQuery(GroupBy.TwoWeeksStartWithOdd));
			Assert.AreEqual("dbo.cf_yearMonth", GroupFactory.GetGroupByQuery(GroupBy.OneMonth));
			Assert.AreEqual("dbo.cf_year3Month", GroupFactory.GetGroupByQuery(GroupBy.ThreeMonths));
			Assert.AreEqual("dbo.cf_year6Month", GroupFactory.GetGroupByQuery(GroupBy.SixMonths));
			Assert.AreEqual("YEAR", GroupFactory.GetGroupByQuery(GroupBy.OneYear));
			Assert.AreEqual("dbo.cf_year2WeekEven", GroupFactory.GetGroupByQuery(GroupBy.TwoWeeksStartWithEven));
		}
	}
}
