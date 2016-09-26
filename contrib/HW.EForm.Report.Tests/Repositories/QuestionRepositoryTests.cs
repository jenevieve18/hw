// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Repositories
{
	[TestFixture]
	public class QuestionRepositoryTests
	{
		QuestionRepositoryStub r = new QuestionRepositoryStub();
		
		[Test]
		public void TestMethod()
		{
			var q = r.Read(1);
			Assert.AreEqual("My work is meaningful", q.Internal);
		}
	}
	
	public class QuestionRepositoryStub : BaseRepositoryStub<Question>, IQuestionRepository
	{
//		List<Question> questions = new List<Question>();
//		
//		public QuestionRepositoryStub()
//		{
//			var oc1 = new OptionComponent { OptionComponentID = 0, Internal = "Completely agree", ExportValue = 1 };
//			var oc2 = new OptionComponent { OptionComponentID = 1, Internal = "Agree", ExportValue = 2 };
//			var oc3 = new OptionComponent { OptionComponentID = 2, Internal = "Neither agree nor disagree", ExportValue = 3 };
//			var oc4 = new OptionComponent { OptionComponentID = 3, Internal = "Disagree", ExportValue = 4 };
//			var oc5 = new OptionComponent { OptionComponentID = 4, Internal = "Complete disagree", ExportValue = 5 };
//
//			var q1 = new Question { QuestionID = 1, Internal = "My work is meaningful" };
//			var o1 = new Option {};
//			
//			o1.AddComponent(oc1);
//			o1.AddComponent(oc2);
//			o1.AddComponent(oc3);
//			o1.AddComponent(oc4);
//			o1.AddComponent(oc5);
//			q1.AddOption(o1);
//			
//			var q2 = new Question { QuestionID = 2, Internal = "I learn new things and develop in my daily work" };
//			var o2 = new Option {};
//			o2.AddComponent(oc1);
//			o2.AddComponent(oc2);
//			o2.AddComponent(oc3);
//			o2.AddComponent(oc4);
//			o2.AddComponent(oc5);
//			q2.AddOption(o2);
//			
//			var q3 = new Question { QuestionID = 3, Internal = "I look forward to going to work" };
//			var o3 = new Option {};
//			o3.AddComponent(oc1);
//			o3.AddComponent(oc2);
//			o3.AddComponent(oc3);
//			o3.AddComponent(oc4);
//			o3.AddComponent(oc5);
//			q3.AddOption(o3);
//			
//			var q4 = new Question { QuestionID = 4, Internal = "My immediate superior shows appreciation for my efforts" };
//			var o4 = new Option {};
//			o4.AddComponent(oc1);
//			o4.AddComponent(oc2);
//			o4.AddComponent(oc3);
//			o4.AddComponent(oc4);
//			o4.AddComponent(oc5);
//			q4.AddOption(o4);
//			
//			var q5 = new Question { QuestionID = 5, Internal = "My immediate superior shows confidence in me as an employee" };
//			var o5 = new Option {};
//			o5.AddComponent(oc1);
//			o5.AddComponent(oc2);
//			o5.AddComponent(oc3);
//			o5.AddComponent(oc4);
//			o5.AddComponent(oc5);
//			q5.AddOption(o5);
//			
//			var q6 = new Question { QuestionID = 6, Internal = "My immediate superior gives me the ability to take responsibility in my work" };
//			var o6 = new Option {};
//			o6.AddComponent(oc1);
//			o6.AddComponent(oc2);
//			o6.AddComponent(oc3);
//			o6.AddComponent(oc4);
//			o6.AddComponent(oc5);
//			q6.AddOption(o6);
//			
//			var q7 = new Question { QuestionID = 7, Internal = "I am familiar with the goals at my workplace" };
//			var o7 = new Option {};
//			o7.AddComponent(oc1);
//			o7.AddComponent(oc2);
//			o7.AddComponent(oc3);
//			o7.AddComponent(oc4);
//			o7.AddComponent(oc5);
//			q7.AddOption(o7);
//			
//			var q8 = new Question { QuestionID = 8, Internal = "The goals at my workplace are monitored and evaluated in a good way" };
//			var o8 = new Option {};
//			o8.AddComponent(oc1);
//			o8.AddComponent(oc2);
//			o8.AddComponent(oc3);
//			o8.AddComponent(oc4);
//			o8.AddComponent(oc5);
//			q8.AddOption(o8);
//			
//			var q9 = new Question { QuestionID = 9, Internal = "I know what is expected from me in my work" };
//			var o9 = new Option {};
//			o9.AddComponent(oc1);
//			o9.AddComponent(oc2);
//			o9.AddComponent(oc3);
//			o9.AddComponent(oc4);
//			o9.AddComponent(oc5);
//			q9.AddOption(o9);
//			
//			questions.Add(q1);
//			questions.Add(q2);
//			questions.Add(q3);
//			questions.Add(q4);
//			questions.Add(q5);
//			questions.Add(q6);
//			questions.Add(q7);
//			questions.Add(q8);
//			questions.Add(q9);
//		}
		
		public QuestionRepositoryStub()
		{
			items.Add(new Question { QuestionID = 1, Internal = "My work is meaningful" });
			items.Add(new Question { QuestionID = 2, Internal = "I learn new things and develop in my daily work" });
			items.Add(new Question { QuestionID = 3, Internal = "I look forward to going to work" });
			items.Add(new Question { QuestionID = 4, Internal = "My immediate superior shows appreciation for my efforts" });
			items.Add(new Question { QuestionID = 5, Internal = "My immediate superior shows confidence in me as an employee " });
			items.Add(new Question { QuestionID = 6, Internal = "My immediate superior gives me the ability to take responsibility in my work " });
			items.Add(new Question { QuestionID = 7, Internal = "I am familiar with the goals at my workplace" });
			items.Add(new Question { QuestionID = 8, Internal = "The goals at my workplace are monitored and evaluated in a good way" });
			items.Add(new Question { QuestionID = 9, Internal = "I know what is expected from me in my work " });
		}
		
		public override Question Read(int id)
		{
			return items.Find(x => x.QuestionID == id);
		}
	}
	
	public class QuestionOptionRepositoryStub : BaseRepositoryStub<QuestionOption>, IQuestionOptionRepository
	{
		public QuestionOptionRepositoryStub()
		{
			items.Add(new QuestionOption { QuestionID = 1, OptionID = 1 });
			items.Add(new QuestionOption { QuestionID = 2, OptionID = 1 });
			items.Add(new QuestionOption { QuestionID = 3, OptionID = 1 });
			items.Add(new QuestionOption { QuestionID = 4, OptionID = 1 });
			items.Add(new QuestionOption { QuestionID = 5, OptionID = 1 });
			items.Add(new QuestionOption { QuestionID = 6, OptionID = 1 });
			items.Add(new QuestionOption { QuestionID = 7, OptionID = 1 });
			items.Add(new QuestionOption { QuestionID = 8, OptionID = 1 });
			items.Add(new QuestionOption { QuestionID = 9, OptionID = 1 });
		}
		
		public IList<QuestionOption> FindByQuestion(int questionID)
		{
			return items.FindAll(x => x.QuestionID == questionID);
		}
		
		public IList<QuestionOption> FindByQuestionAndOption(int questionID, int optionID)
		{
			return items.FindAll(x => x.QuestionID == questionID && x.OptionID == optionID);
		}
	}
	
	public class QuestionLangRepositoryStub : BaseRepositoryStub<QuestionLang>, IQuestionLangRepository
	{
		public QuestionLangRepositoryStub()
		{
//			for (int i = 1; i <= 100; i++) {
//				for (int j = 1; j <= 2; j++) {
//					items.Add(new QuestionLang { QuestionID = i, LangID = j, Question = "Question" + i + "Lang" + j });
//				}
//			}
		}
		
		public IList<QuestionLang> FindByQuestion(int questionID)
		{
			return items.FindAll(x => x.QuestionID == questionID);
		}
	}
}
