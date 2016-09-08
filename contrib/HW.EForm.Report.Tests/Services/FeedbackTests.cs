// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Repositories;
using HW.EForm.Core.Services;
using HW.EForm.Report.Tests.Repositories;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Services
{
	[TestFixture]
	public class FeedbackTests
	{
		FeedbackService s = new FeedbackService(new FeedbackRepositoryStub(),
		                                        new FeedbackQuestionRepositoryStub(),
		                                        new QuestionRepositoryStub(),
		                                        new QuestionOptionRepositoryStub(),
		                                        new QuestionLangRepositoryStub(),
		                                        new WeightedQuestionOptionRepositoryStub(),
		                                        new OptionRepositoryStub(),
		                                        new OptionComponentsRepositoryStub(),
		                                        new OptionComponentRepositoryStub(),
		                                        new OptionComponentLangRepositoryStub(),
		                                        new ProjectRoundUnitRepositoryStub(),
		                                        new AnswerValueRepositoryStub());
		
		[Test]
		public void TestMethod()
		{
//			var f = s.ReadFeedbackWithAnswers2(8, 62, new int[] { 1183 }, 1);
			var f = s.ReadFeedbackWithAnswers2(1, 1, new int[] { 1 }, 1);
		}
		
//		[Test]
//		public void TestReadFeedback()
//		{
//			var f = s.ReadFeedback(6);
//			Console.WriteLine("FeedbackID: {0}, Feedback: {1}", f.FeedbackID, f.FeedbackText);
//			foreach (var fq in f.Questions) {
//				Console.WriteLine("\tQuestionID: {0}, Question: {1}", fq.QuestionID, fq.Question.GetLanguage(1).Question);
//				foreach (var qo in fq.Question.Options) {
//					Console.WriteLine("\t\tOptionID: {0}, Option: {1}", qo.OptionID, qo.Option.Internal);
//					foreach (var oc in qo.Option.Components) {
//						Console.WriteLine("\t\t\tOptionComponentID: {0}, Component: {1}", oc.OptionComponentID, oc.OptionComponent.Internal);
//					}
//				}
//			}
//		}
//		
//		[Test]
//		public void TestFindAllFeedbacks()
//		{
//			foreach (var f in s.FindAllFeedbacks()) {
//				Console.WriteLine("FeedbackID: {0}, Feedback: {1}", f.FeedbackID, f.FeedbackText);
//				foreach (var fq in f.Questions) {
//					Console.WriteLine("\tQuestionID: {0}, Question: {1}", fq.QuestionID, fq.Question.GetLanguage(1).Question);
//					foreach (var qo in fq.Question.Options) {
//						Console.WriteLine("\t\tOptionID: {0}, Option: {1}", qo.OptionID, qo.Option.Internal);
//						foreach (var oc in qo.Option.Components) {
//							Console.WriteLine("\t\t\tOptionComponentID: {0}, Component: {1}", oc.OptionComponentID, oc.OptionComponent.Internal);
//						}
//					}
//				}
//			}
//		}
		
		[Test]
		public void TestReadFeedbackWithAnswers()
		{
//			var f = s.ReadFeedbackWithAnswers(8, 13, new int[] { 97 });
			var f = s.ReadFeedbackWithAnswers(8, 62, new int[] { 1183 }, 1);
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
		
//		[Test]
//		public void a()
//		{
//			var f = s.ReadFeedbackWithAnswers(8, 62, new int[] { 1183 }, 2);
//			foreach (var fq in f.Questions) {
//				Console.WriteLine("QuestionID: {0}, Question: {1}", fq.QuestionID, fq.Question.SelectedQuestionLang.Question);
//				foreach (var qo in fq.Question.Options) {
//					Console.WriteLine("\t" + qo.Option.Internal);
//					foreach (var oc in qo.Option.Components) {
//						Console.WriteLine("\t\t" + oc.OptionComponent.SelectedOptionComponentLang.Text);
//					}
//				}
//			}
//		}
//		
//		[Test]
//		public void b()
//		{
//			var f = s.ReadFeedbackWithAnswers(8, 62, new int[] { 1183 }, 2);
//			var questions = f.GetGroupedQuestions();
//			foreach (var keys in questions.Keys) {
//				Console.WriteLine(keys);
//				foreach (var fq in questions[keys]) {
//					Console.WriteLine("\t" + fq.Question.SelectedQuestionLang.Question);
//				}
//			}
//		}
	}
}
