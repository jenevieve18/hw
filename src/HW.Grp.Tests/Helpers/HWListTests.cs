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
		
		[Test]
		public void TestMethod2()
		{
			var l = new HWList(100.00,
75.00,
41.67,
58.33,
58.33,
75.00,
75.00,
50.00,
50.00,
50.00
);
			Console.WriteLine("Min: {0}", l.Min);
			Console.WriteLine("Max: {0}", l.Max);
			Console.WriteLine("Median: {0}", l.Median);
			Console.WriteLine("Median: {0}", l.Median);
		}
	}
}
