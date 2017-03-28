
using System;
using NUnit.Framework;

namespace HW.WebService.Tests
{
	[TestFixture]
	public class Test1
	{
	    Service s;
	    
	    [SetUp]
	    public void Setup()
	    {
	        s = new Service(new DummyRequest());
	    }
	    
		[Test]
		public void TestMethod()
		{
//			s.UserCreate("test1", "password", "test1@localhost.com", "", true, 2, 0, 0, 10);
		}
		
		[Test]
		public void a()
		{
		    for (int i = 0; i < 100; i++) {
		        s.Hello("Dong");
		    }
		}
	}
}
