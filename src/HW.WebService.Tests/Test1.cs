
using System;
using NUnit.Framework;

namespace HW.WebService.Tests
{
	[TestFixture]
	public class TestServiceRegression
	{
	    Service s;
	    
	    [SetUp]
	    public void Setup()
	    {
	        s = new Service(new DummyRequest());
	    }
	    
		[Test]
		public void TestUserCreate()
		{
//			s.UserCreate("test1", "password", "test1@localhost.com", "", true, 2, 0, 0, 10);
		}
		
		[Test]
		public void TestMultipleRequests()
		{
		    for (int i = 0; i < 100; i++) {
		        s.Hello("Dong");
		    }
		}
	}
}
