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
		SurveyService service = new SurveyService();
				
		[Test]
		public void TestReadSurvey()
		{
			var s = service.ReadSurvey(32);
			Console.WriteLine("{0}: {1}", s.SurveyID, s.Internal);
			foreach (var sq in s.Questions) {
				Console.WriteLine("\t{0}: {1}", sq.QuestionID, sq.Question.SelectedQuestionLang.Question);
			}
		}
	}
}
