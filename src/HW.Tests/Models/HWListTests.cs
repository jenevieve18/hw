using System;
using HW.Core;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Tests.Models
{
	[TestFixture]
	public class HWListTests
	{
		[SetUp]
		public void Setup()
		{
		}
		
		[Test]
		public void TestValues()
		{
			var e = new HWList(0, 1, 2, 3, 4, 5);
			Assert.AreEqual(0, e.LowerWhisker);
			Assert.AreEqual(5, e.UpperWhisker);
			Assert.AreEqual(1, e.LowerBox);
			Assert.AreEqual(4, e.UpperBox);
			Assert.AreEqual(2.5, e.Mean);
			Assert.AreEqual(2.5, e.Median);
			Assert.AreEqual(1.707825127659933, e.StandardDeviation);

			var o = new HWList(0, 1, 2, 3, 4);
			Assert.AreEqual(0, o.LowerWhisker);
			Assert.AreEqual(4, o.UpperWhisker);
			Assert.AreEqual(0.5, o.LowerBox);
			Assert.AreEqual(3.5, o.UpperBox);
			Assert.AreEqual(2, o.Mean);
			Assert.AreEqual(2, o.Median);
			Assert.AreEqual(1.4142135623730952, o.StandardDeviation);
		}
		
		[Test]
		public void TestValues2()
		{
			var e = new HWList(2, 2, 3, 3, 3);
			Assert.AreEqual(2, e.LowerBox);
			Assert.AreEqual(3, e.UpperBox);
		}
		
		[Test]
		public void TestValues3()
		{
			var e = new HWList(2, 2, 3, 3, 3, 4);
			Assert.AreEqual(2.25, e.LowerBox);
			Assert.AreEqual(3, e.UpperBox);
		}
		
		[Test]
		public void TestValues4()
		{
			var e = new HWList(18, 20, 20, 23, 23, 23, 24, 27, 29);
			Assert.AreEqual(20, e.LowerBox);
			Assert.AreEqual(25.5, e.UpperBox);
			Assert.AreEqual(18, e.LowerWhisker);
			Assert.AreEqual(29, e.UpperWhisker);
			Assert.AreEqual(11.75, e.NerdLowerWhisker);
			Assert.AreEqual(33.75, e.NerdUpperWhisker);
		}
	}
}
