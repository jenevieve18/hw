/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:08 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Core.Tests.Helpers
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
