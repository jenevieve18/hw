// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Diagnostics;
using System.IO;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace HW.EForm.Report.Tests.Helpers
{
	[TestFixture]
	public class ChartTests
	{
		[SetUp]
		public void Setup()
		{
		}
		
		[Test]
		public void TestMethod()
		{
			var c = new Chart();
			c.Categories.Add("Male");
			c.Categories.Add("Female");
			
			var d = new List<double>(new double[] {});
		}
		
		[Test]
		public void TestMethod2()
		{
			var q = new Question { Internal = "How are you feeling right now?" };
			var o = new Option { Internal = "" };
			o.AddComponent(new OptionComponent { OptionComponentID = 0, Internal = "Bad", ExportValue = 0});
			o.AddComponent(new OptionComponent { OptionComponentID = 1, Internal = "OK", ExportValue = 1});
			o.AddComponent(new OptionComponent { OptionComponentID = 2, Internal = "Good", ExportValue = 2});
			q.AddOption(o);
			
			IList<ProjectRoundUnit> prus = new List<ProjectRoundUnit>();
			var pru = new ProjectRoundUnit { ProjectRoundUnitID = 1, Unit = "Department1" };
			prus.Add(pru);
			
			var a1 = new Answer { ProjectRoundUnit = pru, ProjectRoundUser = new ProjectRoundUser { Email = "info@eform.se" } };
			a1.AddValue(new AnswerValue { Question = q, Option = o, OptionComponent = o.Components[0].OptionComponent });
			
			var a2 = new Answer { ProjectRoundUnit = pru, ProjectRoundUser = new ProjectRoundUser { Email = "info@eform.se" } };
			a2.AddValue(new AnswerValue { Question = q, Option = o, OptionComponent = o.Components[1].OptionComponent });
			
			var a3 = new Answer { ProjectRoundUnit = pru, ProjectRoundUser = new ProjectRoundUser { Email = "info@eform.se" } };
			a3.AddValue(new AnswerValue { Question = q, Option = o, OptionComponent = o.Components[2].OptionComponent });
			
			var a4 = new Answer { ProjectRoundUnit = pru, ProjectRoundUser = new ProjectRoundUser { Email = "info@eform.se" } };
			a4.AddValue(new AnswerValue { Question = q, Option = o, OptionComponent = o.Components[2].OptionComponent });
			
			var a5 = new Answer { ProjectRoundUnit = pru, ProjectRoundUser = new ProjectRoundUser { Email = "info@eform.se" } };
			a5.AddValue(new AnswerValue { Question = q, Option = o, OptionComponent = o.Components[2].OptionComponent });
			
			var qo = q.FirstOption;
			qo.AddAnswerValue(a1.Values[0]);
			qo.AddAnswerValue(a2.Values[0]);
			qo.AddAnswerValue(a3.Values[0]);
			qo.AddAnswerValue(a4.Values[0]);
			qo.AddAnswerValue(a5.Values[0]);
			
			Assert.AreEqual(1, qo.GetAnswerValues(1, 0).Count);
			Assert.AreEqual(1, qo.GetAnswerValues(1, 1).Count);
			Assert.AreEqual(3, qo.GetAnswerValues(1, 2).Count);
			
			var c = ChartHelper.ToChart(prus, qo);
			
			using (var w = new StreamWriter("test.html")) {
				w.WriteLine(new HighchartsColumnChart(c).ToString());
			}
			Process.Start("test.html");
		}
	}
	
	class X
	{
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public Question Question { get; set; }
	}
}
