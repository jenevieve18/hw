// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using NUnit.Framework;
using HW.EForm.Core.Helpers;

namespace HW.EForm.Report.Tests.Helpers
{
	[TestFixture]
	public class QuestionHelperTests
	{
		[Test]
		public void TestMethod()
		{
			var q = new Question();
			q.AddLanguage(1, "Gender");
			
			var o = new Option { OptionID = 1, Internal = "Gender" };
			o.AddComponent(new OptionComponents(1, "Male", 1, "Male") { ExportValue = 1 });
			o.AddComponent(new OptionComponents(2, "Female", 1, "Female") { ExportValue = 2 });
			
			q.AddOption(new QuestionOption { Option = o });
			
			var a = new Answer { ProjectRoundUnit = new ProjectRoundUnit { Unit = "Department 1" } };
			q.AddAnswerValue(a, 1, 1);
			q.AddAnswerValue(a, 1, 1);
			q.AddAnswerValue(a, 1, 1);
			q.AddAnswerValue(a, 1, 1);
			q.AddAnswerValue(a, 1, 1);
			q.AddAnswerValue(a, 1, 1);
			q.AddAnswerValue(a, 1, 1);
			q.AddAnswerValue(a, 1, 1);
			q.AddAnswerValue(a, 1, 2);
			q.AddAnswerValue(a, 1, 2);
			
			a = new Answer { ProjectRoundUnit = new ProjectRoundUnit { Unit = "Department 2" } };
			q.AddAnswerValue(a, 1, 1);
			q.AddAnswerValue(a, 1, 1);
			q.AddAnswerValue(a, 1, 1);
			q.AddAnswerValue(a, 1, 1);
			q.AddAnswerValue(a, 1, 1);
			q.AddAnswerValue(a, 1, 1);
			q.AddAnswerValue(a, 1, 2);
			q.AddAnswerValue(a, 1, 2);
			q.AddAnswerValue(a, 1, 2);
			q.AddAnswerValue(a, 1, 2);
			
//			Console.WriteLine(q.GetLanguage(1).Question);
//			foreach (var qo in q.Options) {
//				Console.WriteLine("\t" + qo.Option.Internal);
//				foreach (var oc in qo.Option.Components) {
//					Console.WriteLine("\t\t" + oc.OptionComponent.Internal);
//				}
//			}
			
			q.SetAnswerValuesForComponents();
			foreach (var qo in q.Options) {
				foreach (var oc in qo.Option.Components) {
					Console.WriteLine("{0}: {1}", oc.OptionComponent.Internal, oc.OptionComponent.AnswerValues.Count);
				}
			}
			
			Console.WriteLine(q.ToChart());
		}
	}
}
