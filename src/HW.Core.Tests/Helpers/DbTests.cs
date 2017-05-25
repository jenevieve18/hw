using System;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Core.Tests.Helpers
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
			Db.sendMail2("ian.escarro@gmail.com", "", "");
		}
		
		[Test]
		public void TestHasMD5()
		{
			Console.WriteLine(Db.HashMd5("iiiii"));
		}
	}
}
