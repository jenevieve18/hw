using System;
using System.Globalization;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Core.Tests.Helpers
{
	[TestFixture]
	public class ConvertHelperTests
	{
		[Test]
		public void TestToBoolean()
		{
			Assert.IsFalse(ConvertHelper.ToBoolean(null));
		}
		
		[Test]
		public void TestToInt32()
		{
			Assert.AreEqual(0, ConvertHelper.ToInt32(null));
		}
		
		[Test]
		public void TestToDecimal()
		{
			Assert.AreEqual(3.0, ConvertHelper.ToDecimal("3.00", new CultureInfo("en-US")));
		}
		
		[Test]
		public void TestToDouble()
		{
			Assert.AreEqual(0, ConvertHelper.ToDouble(null));
			Assert.AreEqual(-1, ConvertHelper.ToDouble("Superman", -1));
		}
	}
}
