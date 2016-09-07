// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Diagnostics;
using System.IO;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Models;
using HW.EForm.Core.Services;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Views
{
	[TestFixture]
	public class ProjectRoundUnitShowTests
	{
		ProjectService projectService = new ProjectService();
		AnswerService answerService = new AnswerService();
		
		[Test]
		public void TestShow()
		{
			var pru = projectService.ReadProjectRoundUnit(96);
			Console.WriteLine("ProjectRoundUnitID: {0}, Unit: {1}", pru.ProjectRoundUnitID, pru.Unit);
			Console.WriteLine("FeedbackID: {0}, Feedback: {1}", pru.ProjectRound.FeedbackID, pru.ProjectRound.Feedback.FeedbackText);
			foreach (var fq in pru.ProjectRound.Feedback.Questions) {
				Console.WriteLine("\tQuestion: {0}", fq.Question.GetLanguage(1).Question);
			}
		}
		
		[Test]
		public void TestShow2()
		{
			foreach (var pru in projectService.FindProjectRoundUnits(new int[] { 96 })) {
				Console.WriteLine("ProjectRoundUnitID: {0}, Unit: {1}", pru.ProjectRoundUnitID, pru.Unit);
				Console.WriteLine("FeedbackID: {0}, Feedback: {1}", pru.ProjectRound.FeedbackID, pru.ProjectRound.Feedback.FeedbackText);
				foreach (var fq in pru.ProjectRound.Feedback.Questions) {
					Console.WriteLine("\tQuestion: {0}", fq.Question.GetLanguage(1).Question);
				}
			}
		}
		
//		[Test]
//		public void a()
//		{
//			var projectRoundUnits = projectService.FindProjectRoundUnits(new int[] { 96 });
//			foreach (var pru in projectRoundUnits) {
//				Console.WriteLine("ProjectRoundUnitID: {0}, Unit: {1}", pru.ProjectRoundUnitID, pru.Unit);
//				Console.WriteLine("FeedbackID: {0}, Feedback: {1}", pru.ProjectRound.FeedbackID, pru.ProjectRound.Feedback.FeedbackText);
//				foreach (var fq in pru.ProjectRound.Feedback.Questions) {
//					Console.WriteLine("\tQuestion: {0}", fq.Question.GetLanguage(1).Question);
//					
//					fq.Units = projectRoundUnits;
//					fq.AnswerValues = answerService.FindByQuestionOptionsAndUnits(fq.QuestionID, fq.Question.Options, pru.ProjectRoundID, projectRoundUnits);
//
//					Console.WriteLine(new HighchartsColumnChart(fq.ToChart()).ToString());
//				}
//			}
//		}
		
		[Test]
		public void b()
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
			var projectRoundUnits = projectService.FindProjectRoundUnits(new int[] { 96 });
			foreach (var pru in projectRoundUnits) {
				Console.WriteLine("ProjectRoundUnitID: {0}, Unit: {1}", pru.ProjectRoundUnitID, pru.Unit);
				Console.WriteLine("FeedbackID: {0}, Feedback: {1}", pru.ProjectRound.FeedbackID, pru.ProjectRound.Feedback.FeedbackText);
				foreach (var fq in pru.ProjectRound.Feedback.Questions) {
					Console.WriteLine("\tQuestionID: {0}, Question: {1}", fq.QuestionID, fq.Question.GetLanguage(1).Question);
					
					fq.Question.ProjectRoundUnits = projectRoundUnits;
//					fq.AnswerValues = answerService.FindByQuestionOptionsAndUnits(fq.QuestionID, fq.Question.Options, pru.ProjectRoundID, projectRoundUnits);

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
}
