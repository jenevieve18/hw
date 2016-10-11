// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

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
