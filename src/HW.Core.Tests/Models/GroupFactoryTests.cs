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
//		[Test]
//		public void TestGetCount()
//		{
//			Dictionary<string, string> desc = new Dictionary<string, string>();
//			Dictionary<string, string> join = new Dictionary<string, string>();
//			List<string> item = new List<string>();
//			Dictionary<string, int> mins = new Dictionary<string, int>();
//			string extraDesc = "";
//			var count = GroupFactory.GetCount(3, 1, 1, 1, "0,1", ref extraDesc, desc, join, item, mins, new SqlDepartmentRepository(), new SqlQuestionRepository(), 10);
//			Assert.AreEqual(0, count);
//		}
		
		[Test]
		public void TestGetGroupBy()
		{
			Assert.AreEqual("dbo.cf_yearWeek", GroupFactory.GetGroupBy(1));
			Assert.AreEqual("dbo.cf_year2Week", GroupFactory.GetGroupBy(2));
			Assert.AreEqual("dbo.cf_yearMonth", GroupFactory.GetGroupBy(3));
			Assert.AreEqual("dbo.cf_year3Month", GroupFactory.GetGroupBy(4));
			Assert.AreEqual("dbo.cf_year6Month", GroupFactory.GetGroupBy(5));
			Assert.AreEqual("YEAR", GroupFactory.GetGroupBy(6));
			Assert.AreEqual("dbo.cf_year2WeekEven", GroupFactory.GetGroupBy(7));
		}
	}
}
