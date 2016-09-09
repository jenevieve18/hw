// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Diagnostics;
using System.IO;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Repositories;
using HW.EForm.Core.Services;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Views
{
	[TestFixture]
	public class FeedbackShowTests
	{
//		FeedbackService feedbackService = new FeedbackService(new SqlFeedbackRepository(),
//		                                        new SqlFeedbackQuestionRepository(),
//		                                        new SqlQuestionRepository(),
//		                                        new SqlQuestionOptionRepository(),
//		                                        new SqlQuestionLangRepository(),
//		                                        new SqlWeightedQuestionOptionRepository(),
//		                                        new SqlOptionRepository(),
//		                                        new SqlOptionComponentsRepository(),
//		                                        new SqlOptionComponentRepository(),
//		                                        new SqlOptionComponentLangRepository(),
//		                                        new SqlProjectRoundUnitRepository(),
//		                                        new SqlAnswerValueRepository());
		FeedbackService feedbackService = ServiceFactory.CreateFeedbackService();
		AnswerService answerService = new AnswerService();
		ProjectService projectService = new ProjectService();
		
		[Test]
		public void TestShow()
		{
			string template = @"<html>
<head>
<meta charset='utf-8'>
<script src='https://code.jquery.com/jquery-2.2.4.min.js'></script>
<script src='https://code.highcharts.com/highcharts.js'></script>
<script src='https://code.highcharts.com/highcharts-more.js'></script>
<script src='https://code.highcharts.com/modules/exporting.js'></script>
<script>
$(function() {
	$('#container').highcharts(__SCRIPT__);
});
</script>
</head>
<body>
<div id=container></div>
</body.";
			var f = feedbackService.ReadFeedbackWithAnswers2(6, 10, new int[] { 96 }, 1);
			
			Console.WriteLine("FeedbackID: {0}, Feedback: {1}", f.FeedbackID, f.FeedbackText);
			foreach (var fq in f.Questions) {
				Console.WriteLine("\tQuestionID: {0}, Question: {1}", fq.QuestionID, fq.Question.GetLanguage(1).Question);
				
				string chart = new HighchartsColumnChart(fq.Question.ToChart(false)).ToString();
				template = template.Replace("__SCRIPT__", chart);
				
				Console.WriteLine(template);
				string path = "Q" + fq.QuestionID + ".html";
				using (var w = new StreamWriter(path)) {
					w.WriteLine(template);
				}
				Process.Start(path);
			}
		}
	}
}
