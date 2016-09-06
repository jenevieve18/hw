// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Models;
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
			var q = s.ReadQuestion(1689);
			Console.WriteLine("QuestionID: {0}, Question: {1}", q.QuestionID, q.GetLanguage(1).Question);
			foreach (var qo in q.Options) {
				Console.WriteLine("\tOptionID: {0}, Type: {1} Internal: {2}", qo.OptionID, qo.Option.OptionType, qo.Option.Internal);
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
			var questions = s.FindQuestion(new int[] { 1689 }, 13, new int[] { 97 });
			foreach (var q in questions) {
				Console.WriteLine(HighchartsChart.GetHighchartsChart(q.Options[0].Option.OptionType, q.ToChart(true)));
			}
		}
		
		[Test]
		public void c()
		{
			var questions = s.FindQuestion(new int[] { 1690 }, 13, new int[] { 97 });
			foreach (var q in questions) {
				foreach (var c in q.ToCharts(true)) {
					Console.WriteLine(HighchartsChart.GetHighchartsChart(q.Options[0].Option.OptionType, c));
				}
			}
		}
		
		[Test]
		public void d()
		{
			var x = new GroupedQuestions();
			x.Questions.Add(s.ReadQuestion2(1689, 13, new int[] { 97 }));
			x.Questions.Add(s.ReadQuestion2(1690, 13, new int[] { 97 }));
			x.Questions.Add(s.ReadQuestion2(1691, 13, new int[] { 97 }));
			x.Questions.Add(s.ReadQuestion2(1692, 13, new int[] { 97 }));
			
			Console.WriteLine(HighchartsChart.GetHighchartsChart(9, x.ToChart(true)));
		}
		
		[Test]
		public void e()
		{
			var q = s.ReadQuestion2(1689, 13, new int[] { 97 });
			foreach (var x in q.ToQuestions()) {
				Console.WriteLine(HighchartsChart.GetHighchartsChart(q.Options[0].Option.OptionType, x.ToChart(false)));
			}
		}
		
		[Test]
		public void f()
		{
			var painQuestions = new GroupedQuestions();
			painQuestions.Questions.AddRange(s.ReadQuestion2(1689, 13, new int[] { 97 }).ToQuestions());
			painQuestions.Questions.AddRange(s.ReadQuestion2(1690, 13, new int[] { 97 }).ToQuestions());
			painQuestions.Questions.AddRange(s.ReadQuestion2(1691, 13, new int[] { 97 }).ToQuestions());
			painQuestions.Questions.AddRange(s.ReadQuestion2(1692, 13, new int[] { 97 }).ToQuestions());
			
			Console.WriteLine(HighchartsChart.GetHighchartsChart(9, painQuestions.ToChart(true)));
		}
	}
}
