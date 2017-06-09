using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HW.WebService.Tests2
{
    [TestClass]
    public class TestPasswordReset
    {
        [TestMethod]
        public void TestMethod1()
        {
            var s = new Service(new DummyRequest());
            Assert.IsTrue(s.UserResetPassword("test1@localhost.com", 1));
        }
    }
}
