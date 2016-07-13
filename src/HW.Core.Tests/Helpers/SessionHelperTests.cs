/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:16 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Core.Tests.Helpers
{
	[TestFixture]
	public class SessionHelperTests
	{
		[Test]
		public void TestMethod()
		{
			SessionHelper.AddIf(true, "Hello", "World");
			Assert.AreEqual("World", SessionHelper.Get("Hello").ToString());
			SessionHelper.RemoveIf(true, "Test");
		}
	}
}
