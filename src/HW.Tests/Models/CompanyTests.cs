using System;
using HW.Invoicing.Core.Models;
using NUnit.Framework;

namespace HW.Tests.Models
{
	[TestFixture]
	public class CompanyTests
	{
		[Test]
		public void TestMethod()
		{
			var c = new Company {
				Website = "",
				Email = "ian.escarro@gmail.com"
			};
			Assert.AreEqual("ian.escarro@gmail.com", c.GetWebsiteAndEmail());
			c = new Company {
				Website = "http://www.ian.com/",
				Email = "ian.escarro@gmail.com"
			};
			Assert.AreEqual("http://www.ian.com/, ian.escarro@gmail.com", c.GetWebsiteAndEmail());
			c = new Company {
				Website = "http://www.ian.com/",
				Email = ""
			};
			Assert.AreEqual("http://www.ian.com/", c.GetWebsiteAndEmail());
		}
	}
}
