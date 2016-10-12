// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.Core;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Core.Tests.Models
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
			var l = new HWList(2, 2, 3, 3, 3);
			Assert.AreEqual(2, l.LowerBox);
			Assert.AreEqual(3, l.UpperBox);
		}
		
		[Test]
		public void TestValues3()
		{
			var l = new HWList(2, 2, 3, 3, 3, 4);
			Assert.AreEqual(2, l.LowerBox);
			Assert.AreEqual(3, l.UpperBox);
		}
		
		[Test]
		public void TestValues4()
		{
			var l = new HWList(18, 20, 20, 23, 23, 23, 24, 27, 29);
			Assert.AreEqual(20, l.LowerBox);
			Assert.AreEqual(25.5, l.UpperBox);
			Assert.AreEqual(18, l.LowerWhisker);
			Assert.AreEqual(29, l.UpperWhisker);
			Assert.AreEqual(18, l.NerdLowerWhisker);
			Assert.AreEqual(29, l.NerdUpperWhisker);
		}
		
		[Test]
		public void TestValues5()
		{
			var l = new HWList(100.00, 75.00, 50.00, 25.00, 50.00, 75.00, 50.00, 25.00, 50.00, 50.00);
			Console.WriteLine("Min: {0}", l.Min);
			Console.WriteLine("Max: {0}", l.Max);
			Console.WriteLine("Mean: {0}", l.Mean);
			Console.WriteLine("Lower Box: {0}", l.LowerBox);
			Console.WriteLine("Upper Box: {0}", l.UpperBox);
		}
	}
}
