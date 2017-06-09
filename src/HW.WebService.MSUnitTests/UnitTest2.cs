using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HW.WebService.Tests2
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            var s = new Service(new DummyRequest());
            var u = s.UserLogin("test1", "password", 10);
            var f = s.FormEnum(u.token, 2, 10);
            s.UserGetFormInstanceFeedback(u.token, f[0].formKey, "", 2, 10);
        }
    }
}
