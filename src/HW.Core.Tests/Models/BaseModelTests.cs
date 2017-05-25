/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 5/25/2017
 * Time: 2:16 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using HW.Core.Helpers;
using HW.Core.Models;
using NUnit.Framework;

namespace HW.Core.Tests.Models
{
	[TestFixture]
	public class BaseModelTests
	{
		[Test]
		public void TestMethod()
		{
			var b = new BaseModel();
			b.AddErrorIf(true, "hello world");
			Assert.AreEqual(1, b.Errors.Count);
			
			Assert.Equals(0, ConvertHelper.ToInt32("hello"));
		}
	}
}
