
using System;
using NUnit.Framework;

namespace HW.WebService.Tests
{
	[TestFixture]
	public class TestUserLogin2FA
	{
		Service s;
		
		[SetUpAttribute]
		public void Setup()
		{
		    s = new Service(new DummyRequest());
		}
		
		[Test]
		public void TestUserLogin2FAInvalid()
		{
		    var u = s.UserLogin2FA("lalala", "lalala", 10);
		    Assert.IsNull(u.UserData.token);
		}
		
		[Test]
		public void TestLoginWithout2FA()
		{
		    var u = s.UserLogin("test1", "password", 10);
		    Assert.IsTrue(s.UserDisable2FA(u.token, 10));
		    
		    var ud = s.UserLogin2FA("test1", "password", 10);
		    Assert.IsNotEmpty(ud.UserData.token);
		    Assert.AreNotEqual(new DateTime(), ud.UserData.tokenExpires);
		    Assert.AreEqual(2, ud.UserData.languageID);
		}
		
		[Test]
		public void TestLoginWithActiveAttempt()
		{
		    var u = s.UserLogin("test1", "password", 10);
		    Assert.IsTrue(s.UserDisable2FA(u.token, 10));
		    Assert.IsTrue(s.UserEnable2FA(u.token, 10));
		    
		    var ud = s.UserLogin2FA("test1", "password", 10);
		    
		    ud = s.UserLogin2FA("test1", "password", 10);
		    Assert.IsNull(ud.UserData.token);
		    Assert.AreEqual(new DateTime(), ud.UserData.tokenExpires);
		    Assert.AreEqual(0, ud.UserData.languageID);
		    
		    Assert.IsNull(ud.secretKey);
		    Assert.IsNull(ud.resourceID);
		    Assert.IsFalse(ud.activeLoginAttempt);
		}
		
		[Test]
		public void TestLoginCorrect2FA()
		{
		    var u = s.UserLogin("test1", "password", 10);
		    Assert.IsTrue(s.UserEnable2FA(u.token, 10));
		    
		    var ud = s.UserLogin2FA("test1", "password", 10);
		    
		    Assert.IsNull(ud.UserData.token);
		    Assert.AreEqual(new DateTime(), ud.UserData.tokenExpires);
		    Assert.AreEqual(0, ud.UserData.languageID);
		    
		    Assert.IsNotEmpty(ud.resourceID);
		}
		
		[Test]
		public void TestLogin2FAFirstTime()
		{
		    var u = s.UserLogin("test1", "password", 10);
		    Assert.IsTrue(s.UserDisable2FA(u.token, 10));
		    Assert.IsTrue(s.UserEnable2FA(u.token, 10));
		    
		    var ud = s.UserLogin2FA("test1", "password", 10);
		    
		    Assert.IsNull(ud.UserData.token);
		    Assert.AreEqual(new DateTime(), ud.UserData.tokenExpires);
		    Assert.AreEqual(0, ud.UserData.languageID);
		    
		    Assert.IsNotEmpty(ud.secretKey);
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
