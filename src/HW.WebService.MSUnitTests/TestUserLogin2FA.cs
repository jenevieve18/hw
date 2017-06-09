using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HW.WebService.Tests2
{
    [TestClass]
    public class TestUserLogin2FA
    {
        Service s = new Service(new DummyRequest());
        Service s2 = new Service(new DummyRequest2());

        [TestMethod]
        public void TestUserLogin2FAInvalid()
        {
            var u = s.UserLogin2FA("lalala", "lalala", 10);
            Assert.IsNull(u.UserData.token);
        }

        [TestMethod]
        public void TestLoginWithout2FA()
        {
            var u = s.UserLogin("test1", "password", 10);
            Assert.IsTrue(s.UserDisable2FA(u.token, 10));

            var ud = s.UserLogin2FA("test1", "password", 10);
            Assert.IsTrue(ud.UserData.token != "");
            //Assert.IsNotEmpty(ud.UserData.token);
            Assert.AreNotEqual(new DateTime(), ud.UserData.tokenExpires);
            Assert.AreEqual(2, ud.UserData.languageID);
        }

        [TestMethod]
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

        [TestMethod]
        public void TestLoginCorrect2FA()
        {
            var u = s.UserLogin("test1", "password", 10);
            Assert.IsTrue(s.UserDisable2FA(u.token, 10));
            Assert.IsTrue(s.UserEnable2FA(u.token, 10));

            var ud = s.UserLogin2FA("test1", "password", 10);

            Assert.IsNull(ud.UserData.token);
            Assert.AreEqual(new DateTime(), ud.UserData.tokenExpires);
            Assert.AreEqual(0, ud.UserData.languageID);

            //Assert.IsNotEmpty(ud.resourceID);
            Assert.IsTrue(ud.resourceID != "");
        }

        [TestMethod]
        public void TestLogin2FA()
        {
            var u = s.UserLogin("test1", "password", 10);
			Assert.IsTrue(s.UserDisable2FA(u.token, 10));
			Assert.IsTrue(s.UserEnable2FA(u.token, 10));
			
			// First time logging in.
			var ud = s.UserLogin2FA("test1", "password", 10);
			
			Assert.IsNull(ud.UserData.token);
			Assert.AreEqual(new DateTime(), ud.UserData.tokenExpires);
			Assert.AreEqual(0, ud.UserData.languageID);
			
			Assert.IsTrue(ud.secretKey != "");
			Assert.IsTrue(ud.resourceID != "");
			
			// In a way, store secret key locally...
			string secretKey = ud.secretKey;
			
			Assert.IsTrue(s.UserSubmitSecretKey(secretKey));
			u = s.UserHolding(ud.resourceID, "test1");
			Assert.IsNotNull(u.token);
			
			Assert.IsTrue(s.UserLogout(u.token));
			
			
			// 2nd time around when logging in using 2FA.
			ud = s.UserLogin2FA("test1", "password", 10);
			
			Assert.IsNull(ud.UserData.token);
			Assert.AreEqual(new DateTime(), ud.UserData.tokenExpires);
			Assert.AreEqual(0, ud.UserData.languageID);
			
			Assert.IsNull(ud.secretKey);
			Assert.IsTrue(ud.resourceID != "");
			
			// Use local secret key...
			Assert.IsTrue(s.UserSubmitSecretKey(secretKey));
			u = s.UserHolding(ud.resourceID, "test1");
			Assert.IsNotNull(u.token);
        }

        [TestMethod]
        public void TestLoginCorrectNo2FA()
        {
            //var u = s.UserLogin("test1", "password", 10);
            //Assert.IsTrue(s.UserDisable2FA(u.token, 10));

            var ud = s.UserLogin2FA("test1", "password", 10);
            Assert.IsNull(ud.secretKey);
            Assert.IsNull(ud.resourceID);
        }

        [TestMethod]
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
            //u = s.UserHolding(resourceID);
            u = s.UserHolding(resourceID, "test1");

            Assert.IsNotNull(u.token);
            Assert.AreNotEqual(new DateTime(), u.tokenExpires);
            Assert.AreNotEqual(0, u.languageID);

            Assert.IsFalse(s.UserGetLoginAttempts(u.token).webservice);
            Assert.IsFalse(s.UserGetLoginAttempts(u.token).website);
        }

        [TestMethod]
        public void a()
        {
            var u = s.UserLogin("test1", "password", 10);
            Assert.IsTrue(s.UserDisable2FA(u.token, 10));
            Assert.IsTrue(s.UserEnable2FA(u.token, 10));

            var ud = s.UserLogin2FA("test1", "password", 10);

            Assert.IsNull(ud.UserData.token);
            Assert.AreEqual(new DateTime(), ud.UserData.tokenExpires);
            Assert.AreEqual(0, ud.UserData.languageID);

            //Assert.IsNotEmpty(ud.secretKey);
            //Assert.IsNotEmpty(ud.resourceID);
            Assert.IsTrue(ud.secretKey != "");
            Assert.IsTrue(ud.resourceID != "");

            Assert.IsTrue(s.UserSubmitSecretKey(ud.secretKey));

            //u = s.UserHolding(ud.resourceID);
            u = s.UserHolding(ud.resourceID, "test1");
            Assert.AreNotEqual(new DateTime(), u.tokenExpires);
            //Assert.IsNotEmpty(u.token);
            Assert.IsTrue(u.token != "");

            ud = s.UserLogin2FA("test1", "password", 10);
            Assert.IsNull(ud.UserData.token);
            Assert.AreEqual(new DateTime(), ud.UserData.tokenExpires);
            Assert.AreEqual(0, ud.UserData.languageID);
        }

        [TestMethod]
        public void TestUserExtendToken()
        {
            var u = s.UserLogin("test1", "password", 10);
            Assert.IsTrue(s.UserDisable2FA(u.token, 10));
            Assert.IsTrue(s.UserEnable2FA(u.token, 10));

            Assert.IsFalse(s.UserExtendToken(u.token, 10));
        }
    }
}
