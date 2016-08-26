// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Services;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Services
{
	[TestFixture]
	public class Test1
	{
		[Test]
		public void TestMethod()
		{
			var s = new ProjectService2();
			var pru = s.ReadProjectRoundUnit3(96);
			
			foreach (var q in pru.ProjectRound.Feedback.Questions) {
				Console.WriteLine("Question: {0}", q.Question.GetLanguage(1).Question);
				foreach (var av in q.Question.AnswerValues) {
					Console.WriteLine();
				}
			}
		}
	}
}
