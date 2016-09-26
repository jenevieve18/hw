// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using NUnit.Framework;
using System.Collections.Generic;
using HW.EForm.Core.Helpers;

namespace HW.EForm.Report.Tests.Models
{
	[TestFixture]
	public class ProjectRoundUnitTests
	{
		[Test]
		public void TestMethod()
		{
			var pru1 = new ProjectRoundUnit { Unit = "Department1" };
			var pru2 = new ProjectRoundUnit { Unit = "Department2" };
			
			var u1 = new ProjectRoundUser { Name = "User1" };
			var u2 = new ProjectRoundUser { Name = "User2" };
			var u3 = new ProjectRoundUser { Name = "User3" };
			var u4 = new ProjectRoundUser { Name = "User4" };
			var u5 = new ProjectRoundUser { Name = "User5" };
			
//			var u6 = new ProjectRoundUser { Name = "User6" };
//			var u7 = new ProjectRoundUser { Name = "User7" };
			
			var q1 = new Question { QuestionID = 1, Internal = "How do you feel right now?" };
			var o1 = new Option { Internal = "Option1", OptionType = OptionTypes.SingleChoice };
			o1.AddComponent(new OptionComponent { Internal = "Bad", OptionComponentID = 0, ExportValue = 0 });
			o1.AddComponent(new OptionComponent { Internal = "So so", OptionComponentID = 1, ExportValue = 1 });
			o1.AddComponent(new OptionComponent { Internal = "Good", OptionComponentID = 2, ExportValue = 2 });
			q1.AddOption(o1);
			
			var q2 = new Question { QuestionID = 2, Internal = "How is your lovelife?" };
			var o2 = new Option { Internal = "Option1", OptionType = OptionTypes.SingleChoice };
			o2.AddComponent(new OptionComponent { Internal = "Very Bad", OptionComponentID = 3, ExportValue = 0 });
			o2.AddComponent(new OptionComponent { Internal = "Bad", OptionComponentID = 4, ExportValue = 1 });
			o2.AddComponent(new OptionComponent { Internal = "OK", OptionComponentID = 5, ExportValue = 2 });
			o2.AddComponent(new OptionComponent { Internal = "Good", OptionComponentID = 6, ExportValue = 3 });
			o2.AddComponent(new OptionComponent { Internal = "Very Good", OptionComponentID = 7, ExportValue = 4 });
			q2.AddOption(o2);
			
			var q3 = new Question { QuestionID = 3, Internal = "How do you feel living in this world?" };
			var o3 = new Option { Internal = "Option1", OptionType = OptionTypes.SingleChoice };
			o3.AddComponent(new OptionComponent { Internal = "Very Bad", OptionComponentID = 8, ExportValue = 0 });
			o3.AddComponent(new OptionComponent { Internal = "Bad", OptionComponentID = 9, ExportValue = 1 });
			o3.AddComponent(new OptionComponent { Internal = "Good", OptionComponentID = 10, ExportValue = 2 });
			o3.AddComponent(new OptionComponent { Internal = "Very Good", OptionComponentID = 11, ExportValue = 3 });
			q3.AddOption(o3);
			
			var a1 = new Answer { ProjectRoundUser = u1 };
			var av11 = new AnswerValue { Question = q1, Option = o1, OptionComponent = o1.Components[0].OptionComponent };
			var av12 = new AnswerValue { Question = q2, Option = o2, OptionComponent = o2.Components[0].OptionComponent };
			var av13 = new AnswerValue { Question = q3, Option = o3, OptionComponent = o3.Components[0].OptionComponent };
			a1.AddAnswerValue(av11);
			a1.AddAnswerValue(av12);
			a1.AddAnswerValue(av13);
			
			var a2 = new Answer { ProjectRoundUser = u2 };
			var av21 = new AnswerValue { Question = q1, Option = o1, OptionComponent = o1.Components[1].OptionComponent };
			var av22 = new AnswerValue { Question = q2, Option = o2, OptionComponent = o2.Components[1].OptionComponent };
			var av23 = new AnswerValue { Question = q3, Option = o3, OptionComponent = o3.Components[1].OptionComponent };
			a2.AddAnswerValue(av21);
			a2.AddAnswerValue(av22);
			a2.AddAnswerValue(av23);
			
			var a3 = new Answer { ProjectRoundUser = u3 };
			var av31 = new AnswerValue { Question = q1, Option = o1, OptionComponent = o1.Components[2].OptionComponent };
			var av32 = new AnswerValue { Question = q2, Option = o2, OptionComponent = o2.Components[2].OptionComponent };
			var av33 = new AnswerValue { Question = q3, Option = o3, OptionComponent = o3.Components[2].OptionComponent };
			a3.AddAnswerValue(av31);
			a3.AddAnswerValue(av32);
			a3.AddAnswerValue(av33);
			
			var a4 = new Answer { ProjectRoundUser = u4 };
			var av42 = new AnswerValue { Question = q2, Option = o2, OptionComponent = o2.Components[3].OptionComponent };
			var av43 = new AnswerValue { Question = q3, Option = o3, OptionComponent = o3.Components[3].OptionComponent };
			a4.AddAnswerValue(av42);
			a4.AddAnswerValue(av43);
			
			var a5 = new Answer { ProjectRoundUser = u5 };
			var av52 = new AnswerValue { Question = q2, Option = o2, OptionComponent = o2.Components[4].OptionComponent };
			a5.AddAnswerValue(av52);
			
			pru1.AddAnswer(a1);
			pru1.AddAnswer(a2);
			pru1.AddAnswer(a3);
			pru1.AddAnswer(a4);
			pru1.AddAnswer(a5);
			
			var i = new Index { MaxVal = 20 };
			var ip1 = new IndexPart { Multiple = 2, Question = q1, Option = o1 };
			var ip2 = new IndexPart { Multiple = 1, Question = q2, Option = o2 };
			var ip3 = new IndexPart { Multiple = 1, Question = q3, Option = o3 };
			i.AddPart(ip1);
			i.AddPart(ip2);
			i.AddPart(ip3);
			
			var answersForQ1 = new List<AnswerValue>();
			answersForQ1.Add(av11);
			answersForQ1.Add(av21);
			answersForQ1.Add(av31);
			
//			Console.WriteLine("Mean: {0}, Max: {1}", new Answer(answersForQ1).GetMean(), i.MaxVal);
			
			var answersForQ2 = new List<AnswerValue>();
			answersForQ2.Add(av12);
			answersForQ2.Add(av22);
			answersForQ2.Add(av32);
			answersForQ2.Add(av42);
			answersForQ2.Add(av52);
			
//			Console.WriteLine("Mean: {0}, Max: {1}", new Answer(answersForQ2).GetMean(), i.MaxVal);
			
			var answersForQ3 = new List<AnswerValue>();
			answersForQ3.Add(av13);
			answersForQ3.Add(av23);
			answersForQ3.Add(av33);
			answersForQ3.Add(av43);
			
//			Console.WriteLine("Mean: {0}, Max: {1}", new Answer(answersForQ3).GetMean(), i.MaxVal);
		}
	}
}
