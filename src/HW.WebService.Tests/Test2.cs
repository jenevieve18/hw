using System;
using NUnit.Framework;

namespace HW.WebService.Tests
{
	[TestFixture]
	public class Test2
	{
		[Test]
		public void TestMethod()
		{
			var s = new Service(new DummyRequest());
			var u = s.UserLogin("test1", "password", 10);
			var f = s.FormEnum(u.token, 2, 10);
			s.UserGetFormInstanceFeedback(u.token, f[0].formKey, "", 2, 10);
		}
	}
}
