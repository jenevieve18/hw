/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Core.Tests.Helpers
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
