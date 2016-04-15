using System;
using NUnit.Framework;
using HW.Core.Helpers;

namespace HW.Tests.Helpers
{
	[TestFixture]
	public class ExtensionMethodsTests
	{
		[Test]
		public void TestRoundOff()
		{
			Assert.AreEqual(20000, 11000.Ceiling());
			Assert.AreEqual(40000, 36983.Ceiling());
			Assert.AreEqual(200000, 136000.Ceiling());
		}
	}
}
