using System;
using HW.Core.Models;
using NUnit.Framework;

namespace HW.Core.Tests.Models
{
	[TestFixture]
	public class ReportTests
	{
		[Test]
		public void TestMethod()
		{
			var r = new Report {};
			r.AddPart(new ReportPart {});
			r.AddPart(new ReportPart {});
			r.AddPart(new ReportPart {});
			r.AddPart(new ReportPart {});
			
			Assert.AreEqual(4, r.Parts.Count);
		}
	}
}
