using System;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Tests.Models
{
	[TestFixture]
	public class DbTests
	{
		[Test]
		public void TestIsEmail()
		{
			Assert.IsTrue(Db.isEmail("info13971@eform.se"));
		}
	}
}
