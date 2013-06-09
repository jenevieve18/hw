//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Tests.Helpers
{
	[TestFixture]
	public class PageTests
	{
		[Test]
		public void TestQueryString()
		{
			Q q = new Q();
			q.Add("LangID", 1);
			q.Add("PLOT", "BOXPLOT");
			q.Add("PRUID", 1);
			q.Add("TEST", "");
			Assert.AreEqual("LangID=1&PLOT=BOXPLOT&PRUID=1&TEST=", q.ToString());
		}
	}
}
