using System;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Tests.Helpers
{
	[TestFixture]
	public class MailHelperTests
	{
		[Test]
		public void TestMethod()
		{
			MailHelper.SendMail("ian.escarro", "test", "test", null, "", "", "");
		}
	}
}
