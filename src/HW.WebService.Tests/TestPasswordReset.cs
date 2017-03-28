
using System;
using NUnit.Framework;

namespace HW.WebService.Tests
{
    [TestFixture]
    public class TestPasswordReset
    {
        [Test]
        public void TestMethod()
        {
            var s = new Service(new DummyRequest());
            Assert.IsTrue(s.UserResetPassword("test1@localhost.com", 1));
        }
    }
}
