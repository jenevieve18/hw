﻿using System;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Tests.Helpers
{
	[TestFixture]
	public class ConvertHelperTests
	{
		[Test]
		public void TestToInt32()
		{
			Assert.AreEqual(0, ConvertHelper.ToInt32(null));
		}
		
		[Test]
		public void TestToDouble()
		{
			Assert.AreEqual(0, ConvertHelper.ToDouble(null));
			Assert.AreEqual(-1, ConvertHelper.ToDouble("Superman", -1));
		}
	}
}
