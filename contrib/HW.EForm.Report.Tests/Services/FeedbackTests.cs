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
	public class FeedbackTests
	{
		FeedbackService s = new FeedbackService();
		
		[Test]
		public void TestReadFeedback()
		{
			var f = s.ReadFeedback(6);
			Console.WriteLine("FeedbackID: {0}, Feedback: {1}", f.FeedbackID, f.FeedbackText);
			foreach (var fq in f.Questions) {
				Console.WriteLine("\tQuestionID: {0}, Question: {1}", fq.QuestionID, fq.Question.GetLanguage(1).Question);
				foreach (var qo in fq.Question.Options) {
					Console.WriteLine("\t\tOptionID: {0}, Option: {1}", qo.OptionID, qo.Option.Internal);
					foreach (var oc in qo.Option.Components) {
						Console.WriteLine("\t\t\tOptionComponentID: {0}, Component: {1}", oc.OptionComponentID, oc.OptionComponent.Internal);
					}
				}
			}
		}
		
		[Test]
		public void TestFindAllFeedbacks()
		{
			foreach (var f in s.FindAllFeedbacks()) {
				Console.WriteLine("FeedbackID: {0}, Feedback: {1}", f.FeedbackID, f.FeedbackText);
				foreach (var fq in f.Questions) {
					Console.WriteLine("\tQuestionID: {0}, Question: {1}", fq.QuestionID, fq.Question.GetLanguage(1).Question);
					foreach (var qo in fq.Question.Options) {
						Console.WriteLine("\t\tOptionID: {0}, Option: {1}", qo.OptionID, qo.Option.Internal);
						foreach (var oc in qo.Option.Components) {
							Console.WriteLine("\t\t\tOptionComponentID: {0}, Component: {1}", oc.OptionComponentID, oc.OptionComponent.Internal);
						}
					}
				}
			}
		}
		
		[Test]
		public void TestReadFeedbackWithAnswers()
		{
			var f = s.ReadFeedbackWithAnswers(8, 13, new int[] { 97 });
			foreach (var fq in f.Questions) {
				Console.WriteLine("QuestionID: {0}, Question: {1}", fq.QuestionID, fq.Question.Internal);
				foreach (var qo in fq.Question.Options) {
					Console.WriteLine("\tOptionID: {0}, Internal: {1}", qo.OptionID, qo.Option.Internal);
					foreach (var oc in qo.Option.Components) {
						Console.WriteLine("\t\tOptionComponentID: {0}, Internal: {1}", oc.OptionComponentID, oc.OptionComponent.Internal);
					}
				}
				foreach (var pru in fq.Question.ProjectRoundUnits) {
					Console.WriteLine("\tProjectRoundUnitID: {0}, Unit: {1}, Total Answers: {2}", pru.ProjectRoundUnitID, pru.Unit, pru.AnswerValues.Count);
					foreach (var qo in pru.Options) {
						foreach (var oc in qo.Option.Components) {
							Console.WriteLine("\t\tInternal: {0} Answers: {1}, Percentage: {2}%", oc.OptionComponent.Internal, oc.OptionComponent.AnswerValues.Count, oc.OptionComponent.AnswerValues.Count / (double)pru.AnswerValues.Count * 100);
						}
					}
				}
//				Console.WriteLine(new HighchartsColumnChart(fq.ToChart()));
//				Console.WriteLine(new HighchartsLineChart(fq.ToChart()));
				Console.WriteLine(new HighchartsBoxplot(fq.Question.ToChart(true)));
			}
		}
		
		[Test]
		public void aa()
		{
			var f = s.ReadFeedbackWithAnswers(8, 13, new int[] { 97 });
			var questions = f.lalala(new int[] { 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248 });
			foreach (var q in questions) {
				Console.WriteLine(q.Question.GetLanguage(2).Question);
			}
		}
		
		[Test]
		public void b()
		{
			var f = s.ReadFeedbackWithAnswers(6, 13, new int[] { 97 });
			Console.WriteLine(new HighchartsBoxplot(f.ToChart(false)));
		}
		
		[Test]
		public void TestFindFeedbackQuestions()
		{
			var x = s.FindFeedbackQuestions(8, new int[] { 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248 });
			foreach (var y in x) {
				Console.WriteLine(y.Question.GetLanguage(2).Question);
			}
		}
	}
}
