using System;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Tests.Helpers
{
	[TestFixture]
	public class DbTests
	{
		[Test]
		public void TestIsEmail()
		{
			Assert.IsTrue(Db.isEmail("info13971@eform.se"));
		}
		
		[Test]
		public void TestSendMail()
		{
			Db.sendMail("ian.escarro@gmail.com", "", "");
		}
	}
}
