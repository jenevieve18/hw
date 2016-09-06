// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Services;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Services
{
	[TestFixture]
	public class QuestionServiceTests
	{
		QuestionService s = new QuestionService();
		
		[Test]
		public void TestReadQuestion()
		{
			var q = s.ReadQuestion(380);
			Console.WriteLine("QuestionID: {0}, Question: {1}", q.QuestionID, q.Internal);
			foreach (var qo in q.Options) {
				Console.WriteLine("\tOptionID: {0}, Internal: {1}", qo.OptionID, qo.Option.Internal);
				foreach (var oc in qo.Option.Components) {
					Console.WriteLine("\t\tOptionComponentID: {0}, Component: {1}", oc.OptionComponentID, oc.OptionComponent.GetLanguage(2).Text);
				}
			}
		}
		
//		[Test]
//		public void a()
//		{
//			var q = s.ReadQuestion2(380, 13, new int[] { 97 });
//			Console.WriteLine(HighchartsChart.GetHighchartsChart(q.Options[0].Option.OptionType, q.ToChart(true)));
//		}
		
		[Test]
		public void b()
		{
			var questions = s.FindQuestion(new int[] { 380, 381, 382, 383, 384, 459, 460, 461, 462, 463 }, 13, new int[] { 97 });
			foreach (var q in questions) {
				Console.WriteLine(HighchartsChart.GetHighchartsChart(q.Options[0].Option.OptionType, q.ToChart(true)));
			}
		}
	}
}
