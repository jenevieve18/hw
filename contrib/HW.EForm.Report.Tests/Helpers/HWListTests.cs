// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Helpers;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Helpers
{
	[TestFixture]
	public class HWListTests
	{
		[Test]
		public void TestMethod()
		{
			var l = new HWList(13, 13, 13, 13, 14, 14, 16, 18, 21);
			Assert.AreEqual(14, l.Median);
			
			l = new HWList(8, 9, 10, 10, 10, 11, 11, 11, 12, 13);
			Assert.AreEqual(10.5, l.Median);
		}
	}
}
