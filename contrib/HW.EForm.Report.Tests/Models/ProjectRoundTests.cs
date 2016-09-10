// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace HW.EForm.Report.Tests.Models
{
	[TestFixture]
	public class ProjectRoundTests
	{
		[Test]
		public void TestMethod()
		{
			var pru = new ProjectRoundUnit {};
			pru.AddOption(new QuestionOption { QuestionID = 1, OptionID = 1 });
			pru.AddOption(new QuestionOption { QuestionID = 1, OptionID = 2 });
			
			var av = new List<AnswerValue>();
			av.Add(new AnswerValue { QuestionID = 1, OptionID = 1, ValueInt = 1 });
			av.Add(new AnswerValue { QuestionID = 1, OptionID = 1, ValueInt = 1 });
			av.Add(new AnswerValue { QuestionID = 1, OptionID = 1, ValueInt = 1 });
			av.Add(new AnswerValue { QuestionID = 1, OptionID = 1, ValueInt = 1 });
			av.Add(new AnswerValue { QuestionID = 1, OptionID = 1, ValueInt = 1 });
			av.Add(new AnswerValue { QuestionID = 1, OptionID = 1, ValueInt = 1 });
			av.Add(new AnswerValue { QuestionID = 1, OptionID = 1, ValueInt = 1 });
			av.Add(new AnswerValue { QuestionID = 1, OptionID = 2, ValueInt = 1 });
			av.Add(new AnswerValue { QuestionID = 1, OptionID = 2, ValueInt = 1 });
			av.Add(new AnswerValue { QuestionID = 1, OptionID = 2, ValueInt = 1 });
			pru.AnswerValues = av;
		}
	}
}
