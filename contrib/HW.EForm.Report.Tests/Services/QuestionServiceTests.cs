// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Models;
using HW.EForm.Core.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace HW.EForm.Report.Tests.Services
{
	[TestFixture]
	public class QuestionServiceTests
	{
		QuestionService s = new QuestionService();
		
		[Test]
		public void TestSingleChoice()
		{
			var q = s.ReadQuestion(62);
			q.SelectedQuestionLangID = Language.Swedish;
			Assert.AreEqual("Utbildning", q.SelectedQuestionLang.Question);
			Assert.IsTrue(q.HasOnlyOneOption);
			Assert.IsTrue(q.FirstOption.Option.IsSingleChoice);
			Assert.AreEqual(3, q.FirstOption.Option.Components.Count);
			Assert.AreEqual("NLP Practitioner", q.FirstOption.Option.Components[0].OptionComponent.SelectedOptionComponentLang.Text);
			Assert.AreEqual(1, q.FirstOption.Option.Components[0].OptionComponent.ExportValue);
			Assert.AreEqual("NLP Master", q.FirstOption.Option.Components[1].OptionComponent.SelectedOptionComponentLang.Text);
			Assert.AreEqual(2, q.FirstOption.Option.Components[1].OptionComponent.ExportValue);
			Assert.AreEqual("NLP och Hälsa", q.FirstOption.Option.Components[2].OptionComponent.SelectedOptionComponentLang.Text);
			Assert.AreEqual(3, q.FirstOption.Option.Components[2].OptionComponent.ExportValue);
			
			q = s.ReadQuestion(387);
			q.SelectedQuestionLangID = Language.English;
			Assert.AreEqual("Felt sad or down?", q.SelectedQuestionLang.Question);
			Assert.IsTrue(q.HasOnlyOneOption);
			Assert.IsTrue(q.FirstOption.Option.IsSingleChoice);
			Assert.AreEqual(4, q.FirstOption.Option.Components.Count);
			Assert.AreEqual("Not at all", q.FirstOption.Option.Components[0].OptionComponent.SelectedOptionComponentLang.Text);
			Assert.AreEqual(1, q.FirstOption.Option.Components[0].OptionComponent.ExportValue);
			Assert.AreEqual("All the time", q.FirstOption.Option.Components[1].OptionComponent.SelectedOptionComponentLang.Text);
			Assert.AreEqual(3, q.FirstOption.Option.Components[1].OptionComponent.ExportValue);
			Assert.AreEqual("Most of the time", q.FirstOption.Option.Components[2].OptionComponent.SelectedOptionComponentLang.Text);
			Assert.AreEqual(2, q.FirstOption.Option.Components[2].OptionComponent.ExportValue);
			Assert.AreEqual("Sometimes", q.FirstOption.Option.Components[3].OptionComponent.SelectedOptionComponentLang.Text);
			Assert.AreEqual(1, q.FirstOption.Option.Components[3].OptionComponent.ExportValue);
		}
		
		[Test]
		public void TestFreeText()
		{
			var q = s.ReadQuestion(67);
			Assert.IsTrue(q.HasOnlyOneOption);
			Assert.IsTrue(q.FirstOption.Option.IsFreeText);
		}
		
		[Test]
		public void TestMultiChoice()
		{
			var q = s.ReadQuestion(646);
			q.SelectedQuestionLangID = Language.Swedish;
			Assert.AreEqual("", q.SelectedQuestionLang.Question);
			Assert.IsTrue(q.HasOnlyOneOption);
			Assert.IsTrue(q.FirstOption.Option.IsMultiChoice);
			Assert.AreEqual(1, q.FirstOption.Option.Components.Count);
			Assert.AreEqual("Jag har tagit del av informationen om KI-LIME projektet och samtycker till att delta. Jag är medveten om att mitt deltagande är frivilligt, samt att jag när som helst kan avbryta min medverkan. Jag godkänner att data från Webb-QPS medarbetarenkät får användas i forskningssyfte.", q.FirstOption.Option.Components[0].OptionComponent.SelectedOptionComponentLang.Text);
			Assert.AreEqual(1, q.FirstOption.Option.Components[0].OptionComponent.ExportValue);
			
			q = s.ReadQuestion(1690); // Pain
			q.SelectedQuestionLangID = Language.English;
			Assert.AreEqual("", q.SelectedQuestionLang.Question);
			Assert.IsTrue(q.HasMultipleOptions);
			Assert.IsTrue(q.FirstOption.Option.IsMultiChoice);
			Assert.AreEqual(1, q.FirstOption.Option.Components.Count);
			Assert.AreEqual(1, q.Options[1].Option.Components.Count);
			
			Assert.AreEqual("Shoulders", q.FirstOption.Option.Components[0].OptionComponent.SelectedOptionComponentLang.Text);
			Assert.AreEqual(3, q.FirstOption.Option.Components[0].OptionComponent.ExportValue);
			
			Assert.AreEqual("Arms/hands", q.Options[1].Option.Components[0].OptionComponent.SelectedOptionComponentLang.Text);
			Assert.AreEqual(4, q.Options[1].Option.Components[0].OptionComponent.ExportValue);
		}
		
		[Test]
		public void TestNumeric()
		{
			var q = s.ReadQuestion(2591);
			Assert.IsTrue(q.HasOnlyOneOption);
			Assert.IsTrue(q.FirstOption.Option.IsNumeric);
		}
		
		[Test]
		public void TestVAS()
		{
			var q = s.ReadQuestion(73);
			Assert.IsTrue(q.HasOnlyOneOption);
			Assert.IsTrue(q.FirstOption.Option.IsVAS);
		}
		
		[Test]
		public void TestReadQuestion()
		{
			var q = s.ReadQuestion(1695);
			Console.WriteLine("{0}: {1}", q.QuestionID, q.GetLanguage(Language.English).Question);
			foreach (var qo in q.Options) {
				Console.WriteLine("\tOptionID: {0}, Type: {1} Internal: {2}", qo.OptionID, qo.Option.OptionType, qo.Option.Internal);
				foreach (var oc in qo.Option.Components) {
					Console.WriteLine("\t\tOptionComponentID: {0}, Component: {1}", oc.OptionComponentID, oc.OptionComponent.GetLanguage(Language.English).Text);
				}
			}
		}
		
		[Test]
		public void TestFindAllQuestions()
		{
			var questions = s.FindAllQuestions();
			foreach (var q in questions) {
				bool sameOptions = q.AllOptionsAreTheSame;
				if (!sameOptions) {
//					Console.WriteLine(q.QuestionID);
				}
				if (q.HasMultipleOptions && !q.AllOptionsAreTheSame) {
					Console.WriteLine(q.QuestionID);
				}
//				Assert.IsTrue(sameOptions);
			}
		}
		
//		[Test]
//		public void f()
//		{
//			var painQuestions = new GroupedQuestions();
//			painQuestions.Questions.AddRange(s.ReadQuestion2(1689, 13, new int[] { 97 }).ToQuestions());
//			painQuestions.Questions.AddRange(s.ReadQuestion2(1690, 13, new int[] { 97 }).ToQuestions());
//			painQuestions.Questions.AddRange(s.ReadQuestion2(1691, 13, new int[] { 97 }).ToQuestions());
//			painQuestions.Questions.AddRange(s.ReadQuestion2(1692, 13, new int[] { 97 }).ToQuestions());
//			
//			Console.WriteLine(HighchartsChart.GetHighchartsChart(9, painQuestions.ToChart(true)));
//		}
	}
}
