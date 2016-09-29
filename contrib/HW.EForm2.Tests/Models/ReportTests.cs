// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using NUnit.Framework;

namespace HW.EForm2.Tests.Models
{
	[TestFixture]
	public class ReportTests
	{
		[Test]
		public void TestMethod()
		{
			var r = new Report { Internal = "Report1" };
			Assert.AreEqual("Report1", r.Internal);
			
			r.AddPart(new ReportPart { Question = new Question {} });
			Assert.IsTrue(r.Parts[0].HasQuestion);
			
			var rp = new ReportPart {};
			
			var i = new Index {};
			i.AddPart(new IndexPart {});
			i.AddPart(new IndexPart {});
			i.AddPart(new IndexPart {});
			
			Assert.AreEqual(3, i.Parts.Count);
			
			var rpc = new ReportPartComponent { Index = i };
			Assert.IsTrue(rpc.HasIndex);
			
			rp.AddComponent(rpc);
			Assert.IsTrue(rp.HasComponents);
			
			r.AddPart(rp);
			r.AddPart(new ReportPart {});
		}
	}
}
