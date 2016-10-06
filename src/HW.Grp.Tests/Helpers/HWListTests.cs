// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Grp.Tests.Helpers
{
	[TestFixture]
	public class HWListTests
	{
		[Test]
		public void TestMethod()
		{
			var l = new HWList(100, 75, 50, 25, 0, 25, 50, 75, 16.66667, 58.33333);
			Assert.AreEqual(100, l.Max);
			Assert.AreEqual(0, l.Min);
			Assert.AreEqual(47.5, l.Mean);
			Assert.AreEqual(50, l.Median);
		}
	}
}
