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
	public class IndexTests
	{
		[Test]
		public void TestMethod()
		{
			var i =  new Index {};
			
			var q1 = new Question {};
			var q2 = new Question {};
			var q3 = new Question {};
			var q4 = new Question {};
			var q5 = new Question {};
			var q6 = new Question {};
			
			i.AddPart(new IndexPart { Question = q1 });
			i.AddPart(new IndexPart { Question = q2 });
			i.AddPart(new IndexPart { Question = q3 });
			i.AddPart(new IndexPart { Question = q4 });
			i.AddPart(new IndexPart { Question = q5 });
			i.AddPart(new IndexPart { Question = q6 });
			
			Assert.IsTrue(i.Parts[0].HasQuestion);
			
			Assert.AreEqual(6, i.Parts.Count);
		}
	}
}
