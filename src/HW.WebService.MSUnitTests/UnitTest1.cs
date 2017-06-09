using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HW.WebService.Tests2
{
    [TestClass]
    public class UnitTest1
    {
        Service s = new Service(new DummyRequest());

        [TestMethod]
        public void TestUserCreate()
        {
            //s.UserCreate("test1", "password", "test1@localhost.com", "", true, 2, 0, 0, 10);
        }

        [TestMethod]
        public void TestMultipleRequests()
        {
            for (int i = 0; i < 100; i++)
            {
                //s.Hello("Dong");
            }
        }
    }
    
    public class DummyRequest : Service.IRequest
    {
        public string UserHostAddress
        {
            get { return "test"; }
        }
    }

    public class DummyRequest2 : Service.IRequest
    {
        public string UserHostAddress
        {
            get { return "test2"; }
        }
    }
}
