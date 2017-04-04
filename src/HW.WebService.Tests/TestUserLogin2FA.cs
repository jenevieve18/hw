
using System;
using System.Threading;
using NUnit.Framework;

namespace HW.WebService.Tests
{
    [TestFixture]
    public class TestUserLogin2FA
    {
        Service s;
        Service s2;
        
        [SetUpAttribute]
        public void Setup()
        {
            s = new Service(new DummyRequest());
            s2 = new Service(new DummyRequest2());
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
            
            Assert.IsNull(ud.UserData.token);
            Assert.AreEqual(new DateTime(), ud.UserData.tokenExpires);
            Assert.AreEqual(0, ud.UserData.languageID);
            
            ud = s2.UserLogin2FA("test1", "password", 10);
            
            Assert.IsNull(ud.secretKey);
            Assert.IsNull(ud.resourceID);
            Assert.IsTrue(ud.activeLoginAttempt);
        }
        
        [Test]
        public void TestLoginCorrect2FA()
        {
            var u = s.UserLogin("test1", "password", 10);
            Assert.IsTrue(s.UserDisable2FA(u.token, 10));
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
            
            u = s.UserHolding(ud.resourceID);
            Assert.IsNull(u.token);
        }
        
        [Test]
        public void TestLoginCorrectNo2FA()
        {
//        	var u = s.UserLogin("test1", "password", 10);
//        	Assert.IsTrue(s.UserDisable2FA(u.token, 10));
        	
        	var ud = s.UserLogin2FA("test1", "password", 10);
        	Assert.IsNull(ud.secretKey);
        	Assert.IsNull(ud.resourceID);
        }
        
        [Test]
        public void TestGetLoginAttempts()
        {
        	var u = s.UserLogin("test1", "password", 10);
            Assert.IsTrue(s.UserDisable2FA(u.token, 10));
            Assert.IsTrue(s.UserEnable2FA(u.token, 10));
            
            var ud = s.UserLogin2FA("test1", "password", 10);
            
            Assert.IsNull(ud.UserData.token);
            Assert.AreEqual(new DateTime(), ud.UserData.tokenExpires);
            Assert.AreEqual(0, ud.UserData.languageID);
            
            string secretKey = ud.secretKey;
            string resourceID = ud.resourceID;
        	
            Assert.IsFalse(ud.activeLoginAttempt);
        	Assert.IsNotNull(ud.secretKey);
        	Assert.IsNotNull(resourceID);
            
            ud = s2.UserLogin2FA("test1", "password", 10);
            
            Assert.IsNull(ud.secretKey);
            Assert.IsNull(ud.resourceID);
            Assert.IsTrue(ud.activeLoginAttempt);
            
            Assert.IsTrue(s.UserSubmitSecretKey(secretKey));
            u = s.UserHolding(resourceID);
            
            Assert.IsNotNull(u.token);
            Assert.AreNotEqual(new DateTime(), u.tokenExpires);
            Assert.AreNotEqual(0, u.languageID);
            
        	Assert.IsFalse(s.UserGetLoginAttempts(u.token).webservice);
        	Assert.IsFalse(s.UserGetLoginAttempts(u.token).website);
        }
        
        [Test]
        public void a()
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
            
            Assert.IsTrue(s.UserSubmitSecretKey(ud.secretKey));
            
            u = s.UserHolding(ud.resourceID);
            Assert.AreNotEqual(new DateTime(), u.tokenExpires);
            Assert.IsNotEmpty(u.token);
            
            ud = s.UserLogin2FA("test1", "password", 10);
            Assert.IsNull(ud.UserData.token);
            Assert.AreEqual(new DateTime(), ud.UserData.tokenExpires);
            Assert.AreEqual(0, ud.UserData.languageID);
        }
        
        [Test]
        public void TestUserExtendToken()
        {
            var u = s.UserLogin("test1", "password", 10);
            Assert.IsTrue(s.UserDisable2FA(u.token, 10));
            Assert.IsTrue(s.UserEnable2FA(u.token, 10));
            
            Assert.IsFalse(s.UserExtendToken(u.token, 10));
        }
    }
    
    public class DummyRequest : Service.IRequest
    {
        public string UserHostAddress {
            get { return "test"; }
        }
    }
    
    public class DummyRequest2 : Service.IRequest
    {
        public string UserHostAddress {
            get { return "test2"; }
        }
    }
}
