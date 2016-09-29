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
	public class WeightedQuestionOptionTests
	{
		[Test]
		public void TestMethod()
		{
			var q = new Question {};
			q.AddOption(new Option {});
			
			var w = new WeightedQuestionOption { Internal = "WeightedQuestion1" };
			w.QuestionOption = q.FirstOption;
		}
	}
}
