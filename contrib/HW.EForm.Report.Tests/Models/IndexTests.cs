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
	public class IndexTests
	{
		[Test]
		public void TestMethod()
		{
			var q = new Question { Internal = "NI-U / Kärleksfull och generös mot andra?" };
			var o = new Option { Internal = "NI-U / Inte alls - hela tiden" };
			var oc1 = new OptionComponent { ExportValue = 1 };
			var oc2 = new OptionComponent { ExportValue = 2 };
			var oc3 = new OptionComponent { ExportValue = 3 };
			var oc4 = new OptionComponent { ExportValue = 4 };
			o.AddComponent(oc1);
			o.AddComponent(oc2);
			o.AddComponent(oc3);
			o.AddComponent(oc4);
			q.AddOption(o);
			
			var i = new Index { Internal = "test" };
			var ip = new IndexPart { Question = q, Option = o };
			ip.Components.Add(new IndexPartComponent { OptionComponent = oc1, Val = 1 });
			ip.Components.Add(new IndexPartComponent { OptionComponent = oc2, Val = 2 });
			ip.Components.Add(new IndexPartComponent { OptionComponent = oc3, Val = 3 });
			ip.Components.Add(new IndexPartComponent { OptionComponent = oc4, Val = 4 });
			i.Parts.Add(ip);
			
			var u1 = new ProjectRoundUser { Name = "User1" };
			var u2 = new ProjectRoundUser { Name = "User2" };
			
			var a1 = new Answer { ProjectRoundUser = u1 };
			var av11 = new AnswerValue { Answer = a1, Question = q, Option = o, ValueInt = 1 };
			var av12 = new AnswerValue { Answer = a1, Question = q, Option = o, ValueInt = 2 };
			var av13 = new AnswerValue { Answer = a1, Question = q, Option = o, ValueInt = 3 };
			var av14 = new AnswerValue { Answer = a1, Question = q, Option = o, ValueInt = 4 };
			a1.AddAnswerValue(av11);
			a1.AddAnswerValue(av12);
			a1.AddAnswerValue(av13);
			a1.AddAnswerValue(av14);
		
			var a2 = new Answer { ProjectRoundUser = u2 };
			var av21 = new AnswerValue { Answer = a2, Question = q, Option = o, ValueInt = 1 };
			var av22 = new AnswerValue { Answer = a2, Question = q, Option = o, ValueInt = 2 };
			var av23 = new AnswerValue { Answer = a2, Question = q, Option = o, ValueInt = 3 };
			var av24 = new AnswerValue { Answer = a2, Question = q, Option = o, ValueInt = 4 };
			a2.AddAnswerValue(av21);
			a2.AddAnswerValue(av22);
			a2.AddAnswerValue(av23);
			a2.AddAnswerValue(av24);
		}
	}
}
