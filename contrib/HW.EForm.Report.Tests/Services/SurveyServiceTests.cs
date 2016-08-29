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
			Console.WriteLine("Survey: {0}", s.Internal);
			foreach (var sq in s.Questions) {
				Console.WriteLine("\tQuestion: {0}", sq.Question.Internal);
				foreach (var sqo in sq.Options) {
					Console.WriteLine("\t\tOption: {0}", sqo.QuestionOption.Option.Internal);
					foreach (var oc in sqo.QuestionOption.Option.Components) {
						Console.WriteLine("\t\t\t Component: {0}", oc.OptionComponent.Internal);
					}
				}
			}
		}
	}
}
