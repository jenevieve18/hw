// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using NUnit.Framework;

namespace HW.EForm.Tests.Models
{
	[TestFixture]
	public class QuestionTests
	{
		[Test]
		public void TestMethod()
		{
			var q = new Question { Internal = "Question1" };
			
			var o = new Option {};
			o.AddComponent(new OptionComponent {});
			
			q.AddOption(o);
			
			Assert.IsTrue(q.HasOptions);
			Assert.IsTrue(q.HasOnlyOneOption);
			
			o = new Option {};
			o.AddComponent(new OptionComponent {});
			
			q.AddOption(o);
			
			Assert.IsTrue(q.HasMultipleOptions);
		}
	}
}
