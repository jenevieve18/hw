// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using HW.EForm.Report.Tests.Repositories;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Models
{
	[TestFixture]
	public class FeedbackTests
	{
		[Test]
		public void TestMethod()
		{
			Feedback f = new Feedback {
				FeedbackText = "Feedback About Gender"
			};
			var q = new Question { Internal = "Gender" };
			var o = new Option { Internal = "Gender" };
			o.AddComponent(new OptionComponent { Internal = "Male" });
			o.AddComponent(new OptionComponent { Internal = "Female" });
			q.AddOption(o);
			f.AddQuestion(q);
			
			Assert.AreEqual(1, f.Questions.Count);
			Assert.AreEqual(2, q.FirstOption.Option.Components.Count);
		}
		
		[Test]
		public void TestMethod2()
		{
			var f = new Feedback { };
			var q = new Question { Internal = "How are you feeling right now?" };
			var o = new Option { Internal = "" };
			o.AddComponent(new OptionComponent { ExportValue = 0 });
			o.AddComponent(new OptionComponent { ExportValue = 0 });
			o.AddComponent(new OptionComponent { ExportValue = 0 });
			o.AddComponent(new OptionComponent { ExportValue = 0 });
			q.AddOption(o);
			f.AddQuestion(q);
		}
		
		[Test]
		public void a()
		{
			var questions = new QuestionRepositoryStub().FindAll();
			var indexes = new IndexRepositoryStub().FindAll();
			
//			var i1 = new Index { Internal = "HME Motivation" };
//			i1.AddPart(new IndexPart { Question = questions[0], Option = questions[0].FirstOption.Option });
//			i1.AddPart(new IndexPart { Question = questions[1], Option = questions[1].FirstOption.Option });
//			i1.AddPart(new IndexPart { Question = questions[2], Option = questions[2].FirstOption.Option });
//			
//			var i2 = new Index { Internal = "HME Leadership" };
//			i2.AddPart(new IndexPart { Question = questions[3], Option = questions[3].FirstOption.Option });
//			i2.AddPart(new IndexPart { Question = questions[4], Option = questions[4].FirstOption.Option });
//			i2.AddPart(new IndexPart { Question = questions[5], Option = questions[5].FirstOption.Option });
//			
//			var i3= new Index { Internal = "HME Styrning" };
//			i3.AddPart(new IndexPart { Question = questions[6], Option = questions[6].FirstOption.Option });
//			i3.AddPart(new IndexPart { Question = questions[7], Option = questions[7].FirstOption.Option });
//			i3.AddPart(new IndexPart { Question = questions[8], Option = questions[8].FirstOption.Option });
			
			var f = new Feedback { FeedbackText = "test" };
			f.AddQuestion(new FeedbackQuestion { Question = questions[0] });
			f.AddQuestion(new FeedbackQuestion { Question = questions[1] });
			f.AddQuestion(new FeedbackQuestion { Question = questions[2] });
			f.AddQuestion(new FeedbackQuestion { Index = indexes[0] });
			
			foreach (var fq in f.Questions) {
				if (fq.HasQuestion) {
				} else {
					
				}
			}
		}
	}
}
