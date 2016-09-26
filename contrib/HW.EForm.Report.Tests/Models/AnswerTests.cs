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
	public class AnswerTests
	{
		[Test]
		public void TestMethod()
		{
			var u1 = new ProjectRoundUser { Name = "User1" };
			
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
			a1.AddAnswerValue(new AnswerValue { Question = q1, Option = o1, ValueInt = 0 });
			a1.AddAnswerValue(new AnswerValue { Question = q2, Option = o2, ValueInt = 3 });
			a1.AddAnswerValue(new AnswerValue { Question = q3, Option = o3, ValueInt = 8 });
		}
	}
}
