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
	public class SurveyServiceTests
	{
		SurveyService ss = new SurveyService();
		
		[Test]
		public void TestReadSurvey()
		{
			var s = ss.ReadSurvey(32);
			Console.WriteLine(s.Internal);
			foreach (var q in s.Questions) {
				Console.WriteLine("  Question: {0}", q.Question.Internal);
				foreach (var o in q.Options) {
					Console.WriteLine("    Option: {0}", o.QuestionOption.Option.Internal);
				}
			}
		}
	}
}
