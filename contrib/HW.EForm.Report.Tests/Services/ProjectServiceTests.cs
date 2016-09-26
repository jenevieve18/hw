// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;
using HW.EForm.Core.Services;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Services
{
	[TestFixture]
	public class ProjectServiceTests
	{
//		ProjectService projectService = new ProjectService();
		ProjectService s = new ProjectService();
		
		[Test]
		public void TestMethod()
		{
		}
		
//		[Test]
//		public void TestSaveProject()
//		{
//			projectService.SaveProject(new Project { Internal = "Project Ian" });
//		}
//		
//		[Test]
//		public void TestReadProject()
//		{
//			var p = projectService.ReadProject(8);
//			Console.WriteLine(p.Internal);
//			foreach (var pr in p.Rounds) {
//				Console.WriteLine("\t" + pr.Internal);
//				foreach (var pru in pr.Units) {
//					Console.WriteLine("\t\t" + pru.Unit);
//				}
//			}
//		}
//		
//		[Test]
//		public void TestReadProjectRound()
//		{
//			var pr = projectService.ReadProjectRound(10);
//			Console.WriteLine("Internal: {0}, FeedbackID: {1}", pr.Internal, pr.FeedbackID);
//			foreach (var pru in pr.Units) {
//				Console.WriteLine("\tUnit: {0}", pru.Unit);
//			}
//		}
//		
//		[Test]
//		public void TestReadProjectRoundUnit()
//		{
//			var pru = projectService.ReadProjectRoundUnit(96);
//			Console.WriteLine("Unit: {0}, Feedback: {1}", pru.Unit, pru.ProjectRound.Feedback.FeedbackText);
//			foreach (var fq in pru.ProjectRound.Feedback.Questions) {
//				Console.WriteLine("\tQuestion: {0}", fq.Question.GetLanguage(1).Question);
//				foreach (var qo in fq.Question.Options) {
//					Console.WriteLine("\t\t" + qo.Option.Internal);
//					foreach (var oc in qo.Option.Components) {
//						Console.WriteLine("\t\t\t" + oc.OptionComponent.Internal);
//					}
//				}
//			}
//		}
	}
}
