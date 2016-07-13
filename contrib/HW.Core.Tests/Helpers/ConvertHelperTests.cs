/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 6:44 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
		public void TestToDateTime()
		{
			Assert.AreEqual(DateTime.Now, ConvertHelper.ToDateTime(null));
			Assert.AreEqual(new DateTime(2016, 1, 1), ConvertHelper.ToDateTime(null, new DateTime(2016, 1, 1)));
		}
		
		[Test]
		public void TestToDecimal()
		{
			Assert.AreEqual(0, ConvertHelper.ToDecimal(null));
		}
		
		[Test]
		public void TestToDouble()
		{
			Assert.AreEqual(0, ConvertHelper.ToDouble(null));
		}
		
		[Test]
		public void TestToInt32()
		{
			Assert.AreEqual(0, ConvertHelper.ToInt32(null));
		}
	}
}
