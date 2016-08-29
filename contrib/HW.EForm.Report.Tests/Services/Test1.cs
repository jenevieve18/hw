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
	public class ProjectServiceTests2
	{
		ProjectService2 s = new ProjectService2();
		
		[Test]
		public void TestReadProjectRoundUnit()
		{
			var pru = s.ReadProjectRoundUnit(96);
			Console.WriteLine("Unit: {0}, Feedback: {1}", pru.Unit, pru.ProjectRound.Feedback.FeedbackText);
			foreach (var fq in pru.ProjectRound.Feedback.Questions) {
				Console.WriteLine("\tQuestion: {0}", fq.Question.GetLanguage(1).Question);
				foreach (var qo in fq.Question.Options) {
					Console.WriteLine("\t\t" + qo.Option.Internal);
					foreach (var oc in qo.Option.Components) {
						Console.WriteLine("\t\t\t" + oc.OptionComponent.Internal);
					}
				}
			}
		}
		
		[Test]
		public void TestReadProject()
		{
			var p = s.ReadProject(8);
			Console.WriteLine(p.Internal);
			foreach (var pr in p.Rounds) {
				Console.WriteLine("\t" + pr.Internal);
				foreach (var pru in pr.Units) {
					Console.WriteLine("\t\t" + pru.Unit);
				}
			}
		}
	}
}
