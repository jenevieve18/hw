// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Diagnostics;
using System.IO;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;
using HW.EForm.Core.Services;
using HW.EForm.Report.Tests.Repositories;
using System.Collections.Generic;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Services
{
	[TestFixture]
	public class FeedbackServiceTests
	{
		FeedbackService s;
		
		[SetUp]
		public void Setup()
		{
			s = ServiceFactory.CreateFeedbackService();
		}
		
		[Test]
		public void TestSaveFeedback()
		{
			var f = s.ReadFeedback(8);
			if (f == null) {
				f = new Feedback { FeedbackID = 8, FeedbackText = "TEST FOR PROMAS" };
				for (int i = 238; i <= 248; i++) {
					f.AddQuestion(new FeedbackQuestion { QuestionID = i });
				}
				for (int i = 1614; i <= 1616; i++) {
					f.AddQuestion(new FeedbackQuestion { QuestionID = i });
				}
				s.SaveFeedback(f);
			}
		}
		
		[Test]
		public void TestReadFeedbackWithAnswers()
		{
			var f = s.ReadFeedbackWithAnswers(8, 62, new int[] { 1183 }, 1);
			foreach (var fq in f.Questions) {
				Console.WriteLine("{0}: {1}", fq.QuestionID, fq.Question.SelectedQuestionLang.Question);
				foreach (var qo in fq.Question.Options) {
					Console.WriteLine("\t{0}: {1}", qo.OptionID, qo.Option.Internal);
					foreach (var qc in qo.Option.Components) {
						Console.WriteLine("\t\t" + qc.OptionComponent.SelectedOptionComponentLang.Text);
					}
				}
			}
		}
		
		[Test]
		public void b()
		{
			var fq = s.ReadFeedbackQuestion(194, 62, new int[] { 1183 }, 1);
			var c = fq.Question.ToChart();
			using (var w = new StreamWriter("chart.html")) {
				w.WriteLine(@"<html>
<head>
<meta charset='utf-8'>
<script src='https://code.jquery.com/jquery-2.2.4.min.js'></script>
<script src='https://code.highcharts.com/highcharts.js'></script>
<script src='https://code.highcharts.com/highcharts-more.js'></script>
<script src='https://code.highcharts.com/modules/exporting.js'></script>
</head>
<body>");
				var chart = HighchartsBoxplot.GetHighchartsChart(c.Type, c, true);
				Console.WriteLine(chart);
				w.WriteLine(chart);
				w.WriteLine("</body>");
			}
			Process.Start("chart.html");
		}
		
//		[Test]
//		public void a()
//		{
//			var f = s.ReadFeedbackWithAnswers(8, 62, new int[] { 1183 }, 1);
//			var c = f.GetQuestions(1).ToChart();
//			using (var w = new StreamWriter("charts.html")) {
//				w.WriteLine(@"<html>
		//<head>
		//<meta charset='utf-8'>
		//<script src='https://code.jquery.com/jquery-2.2.4.min.js'></script>
		//<script src='https://code.highcharts.com/highcharts.js'></script>
		//<script src='https://code.highcharts.com/highcharts-more.js'></script>
		//<script src='https://code.highcharts.com/modules/exporting.js'></script>
		//</head>
		//<body>");
//				var chart = HighchartsBoxplot.GetHighchartsChart(c.Type, c, true);
//				Console.WriteLine(chart);
//				w.WriteLine(chart);
//				w.WriteLine("</body>");
//			}
//			Process.Start("charts.html");
//		}

		[Test]
		public void TestReadFeedbackWithAnswers2()
		{
			var f = s.ReadFeedbackWithAnswers(8, 62, new int[] { 1183 }, 1);
			var charts = new List<Chart>();
			foreach (var fq in f.Questions) {
				if (fq.IsPartOfChart && !f.HasGroupedChart(fq.PartOfChart)) {
					charts.Add(f.GetQuestions(fq.PartOfChart).ToChart());
				} else if (!fq.IsPartOfChart) {
					charts.Add(fq.Question.ToChart());
				}
			}
			
			using (var w = new StreamWriter("charts.html")) {
				w.WriteLine(@"<html>
<head>
<meta charset='utf-8'>
<script src='https://code.jquery.com/jquery-2.2.4.min.js'></script>
<script src='https://code.highcharts.com/highcharts.js'></script>
<script src='https://code.highcharts.com/highcharts-more.js'></script>
<script src='https://code.highcharts.com/modules/exporting.js'></script>
</head>
<body>");
				foreach (var c in charts) {
					var chart = HighchartsBoxplot.GetHighchartsChart(c.Type, c, true);
					Console.WriteLine(chart);
					w.WriteLine(chart);
				}
				w.WriteLine("</body>");
			}
			Process.Start("charts.html");
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
	}
}
