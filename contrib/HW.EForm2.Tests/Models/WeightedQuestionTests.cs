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
	public class WeightedQuestionTests
	{
		[Test]
		public void TestMethod()
		{
			var w = new WeightedQuestionOption {};
			w.Question = new Question {};
			w.Option = new Option {};
			
			w.YellowLow = 40;
			w.GreenLow = 60;
			w.GreenHigh = 101;
			w.YellowHigh = 101;
			
			w.AddLanguage(new WeightedQuestionOptionLang { LangID = Language.Swedish, Feedback = "" });
			w.AddLanguage(new WeightedQuestionOptionLang { LangID = Language.English, Feedback = "" });
			
			Assert.AreEqual(2, w.Languages.Count);
		}
	}
}
