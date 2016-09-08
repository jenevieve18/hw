// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Models
{
	[TestFixture]
	public class QuestionTests
	{
		[Test]
		public void TestSingleChoice()
		{
			var q = new Question();
			q.AddLanguage(Language.English, "Sex");
			
			var o = new Option { OptionType = OptionTypes.SingleChoice };
			var oc = new OptionComponent();
			oc.AddLanguage(Language.English, "Woman");
			o.AddComponent(oc);
			
			oc = new OptionComponent();
			oc.AddLanguage(Language.English, "Man");
			o.AddComponent(oc);
			q.AddOption(o);
			
			Assert.IsTrue(q.HasOnlyOneOption);
			Assert.AreEqual("Sex", q.SelectedQuestionLang.Question);
			Assert.IsTrue(q.FirstOption.Option.IsSingleChoice);
			Assert.AreEqual(2, q.FirstOption.Option.Components.Count);
		}
		
		[Test]
		public void TestNumeric()
		{
			var q = new Question();
			q.AddLanguage(Language.English, "Age");
			var o = new Option { OptionType = OptionTypes.Numeric };
			var oc = new OptionComponent();
			oc.AddLanguage(Language.English, "years");
			o.AddComponent(oc);
			q.AddOption(o);
			
			Assert.AreEqual("Age", q.SelectedQuestionLang.Question);
			Assert.IsTrue(q.FirstOption.Option.IsNumeric);
			Assert.AreEqual(1, q.FirstOption.Option.Components.Count);
			
			q = new Question();
			q.AddLanguage(Language.English, "Weight");
			o = new Option { OptionType = OptionTypes.Numeric };
			oc = new OptionComponent();
			oc.AddLanguage(Language.English, "cm");
			o.AddComponent(oc);
			q.AddOption(o);
			
			Assert.IsTrue(q.HasOnlyOneOption);
			Assert.AreEqual("Weight", q.SelectedQuestionLang.Question);
			Assert.IsTrue(q.FirstOption.Option.IsNumeric);
			Assert.AreEqual(1, q.FirstOption.Option.Components.Count);
		}
		
		[Test]
		public void TestFreeText()
		{
			var q = new Question();
			q.AddLanguage(Language.English, "For how long time do you usually use your portable music player at every occasion?");
			var o = new Option { OptionType = OptionTypes.FreeText };
			var oc = new OptionComponent();
			oc.AddLanguage(Language.English, "hours");
			o.AddComponent(oc);
			q.AddOption(o);
			
			Assert.IsTrue(q.HasOnlyOneOption);
			Assert.AreEqual("For how long time do you usually use your portable music player at every occasion?", q.SelectedQuestionLang.Question);
			Assert.IsTrue(q.FirstOption.Option.IsFreeText);
			Assert.AreEqual(1, q.FirstOption.Option.Components.Count);
		}
		
		[Test]
		public void TestMultipleChoice()
		{
			
		}
		
		[Test]
		public void TestVAS()
		{
			var q = new Question();
			q.AddLanguage(Language.English, "How is your financial situation?");
			var o = new Option { OptionType = OptionTypes.VAS };
			var oc = new OptionComponent();
			oc.AddLanguage(Language.English, "Very bad");
			o.AddComponent(oc);
			oc = new OptionComponent();
			oc.AddLanguage(Language.English, "Bad");
			o.AddComponent(oc);
			oc = new OptionComponent();
			oc.AddLanguage(Language.English, "Neither good nor bad");
			o.AddComponent(oc);
			oc = new OptionComponent();
			oc.AddLanguage(Language.English, "Good");
			o.AddComponent(oc);
			oc = new OptionComponent();
			oc.AddLanguage(Language.English, "Very good");
			o.AddComponent(oc);
			q.AddOption(o);
			
			Assert.IsTrue(q.HasOnlyOneOption);
			Assert.AreEqual("How is your financial situation?", q.SelectedQuestionLang.Question);
			Assert.IsTrue(q.FirstOption.Option.IsVAS);
			Assert.AreEqual(5, q.FirstOption.Option.Components.Count);
		}
	}
}
