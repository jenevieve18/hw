// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Services;
using NUnit.Framework;

namespace HW.Grp.Tests.Models
{
	[TestFixture]
	public class ReportTests
	{
		ReportService2 s = new ReportService2(
			new ReportRepositoryStub(),
			new ReportPartRepositoryStub(),
			new ReportPartComponentRepositoryStub(),
			new QuestionRepositoryStub(),
			new IndexRepositoryStub()
		);
		
		[Test]
		public void TestMethod()
		{
			var r = s.ReadReport(1);
			
			Assert.AreEqual("HME HW group report", r.Internal);
			Assert.AreEqual(3, r.Parts.Count);
			
			Assert.IsFalse(r.Parts[0].HasQuestion);
			Assert.IsTrue(r.Parts[0].HasComponents);
			Assert.AreEqual("HME Leadership", r.Parts[0].FirstComponent.Index.Internal);
			
			Assert.IsFalse(r.Parts[1].HasQuestion);
			Assert.IsTrue(r.Parts[1].HasComponents);
			Assert.AreEqual("HME Motivation", r.Parts[1].FirstComponent.Index.Internal);
			
			Assert.IsTrue(r.Parts[2].HasQuestion);
		}
	}
}
