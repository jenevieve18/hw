
using System;
using NUnit.Framework;

namespace HW.WebService.Tests
{
	[TestFixture]
	public class Test1
	{
		Service s;
		
		[SetUpAttribute]
		public void Setup()
		{
			s = new Service(new DummyRequest());
		}
		
		class DummyRequest : Service.IRequest
		{
			public string UserHostAddress {
				get { return "test"; }
			}
		}
		
		[Test]
		public void TestMethod()
		{
			var u = s.UserLogin("test1", "password", 10);
			
			var token = u.token;
			
			Assert.IsTrue(s.UserDisable2FA(token, 10));
			
			Assert.IsTrue(s.UserEnable2FA(token, 10));
		}
		
		[Test]
		public void a()
		{
			var u = s.UserLogin("test1", "password", 10);
			Console.WriteLine(s.UserGenerateSecret(u.token, 10));
		}
	}
}
