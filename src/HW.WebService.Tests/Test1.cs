
using System;
using NUnit.Framework;

namespace HW.WebService.Tests
{
	[TestFixture]
	public class Test1
	{
		Service s;
		int langID;
		string username;
		string email;
		string password;
		string token;
		
		[SetUpAttribute]
		public void Setup()
		{
		}
		
		[Test]
		public void TestUserCreate()
		{
			s = new Service(new DummyRequest());
			
			langID = 2; // English
			username = "testuser" + Guid.NewGuid().ToString();
			password = "Start123!!!";
			email = username + "@localhost.com";
			
			Assert.IsNotNull(s.TodaysWordsOfWisdom(langID));
			
			var u = s.UserCreate(username, password, email, email, true, langID, 0, 0, 10);
			Assert.IsNotNull(u.tokenExpires);
			token = u.token;
			
			Assert.IsFalse(s.UserGet2FAStatus(token, 10).user2FAEnabled);
			Assert.IsFalse(s.UserGet2FAStatus(token, 10).sponsor2FAEnabled);
			
			s.UserEnable2FA(token, 10);
			
			Assert.IsFalse(s.UserExtendToken(token, 10));
			
			var ud = s.UserLogin2FA(username, password, 10);
			Assert.AreEqual(32, ud.secretKey.Length);
			
			ud = s.UserLogin2FA(username, password, 10);
			Assert.IsNotEmpty(ud.resourceID);
		}
		
		class DummyRequest : Service.IRequest
		{
			public string UserHostAddress {
				get { return "test"; }
			}
		}
	}
}
