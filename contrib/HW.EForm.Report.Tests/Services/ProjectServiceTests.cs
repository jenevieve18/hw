// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Diagnostics;
using System.IO;
using HW.EForm.Core.Services;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Services
{
	[TestFixture]
	public class ProjectServiceTests
	{
		ProjectService2 ps = new ProjectService2();
		
//		[Test]
//		public void TestMethod()
//		{
//			var pru = ps.ReadProjectRoundUnit2(96);
//			var f = pru.ProjectRound.Feedback;
//			Console.WriteLine("FeedbackID: {0}, Feedback: {1}", f.FeedbackID, f.FeedbackText);
//			foreach (var fq in f.Questions) {
//				Console.WriteLine("\tQuestionID: {0}, Question: {1}", fq.Question.QuestionID, fq.Question.GetLanguage(1).Question);
//				foreach (var fqo in fq.Question.Options) {
//					Console.WriteLine("\t\tOptionID: {1}, OptionType: {0}", fqo.Option.OptionType, fqo.Option.OptionID);
//					foreach (var oc in fqo.Option.Components) {
//						Console.WriteLine("\t\t\tComponent: {0}", oc.Component.GetLanguage(1).Text);
//					}
//					Console.WriteLine("\t\tANSWERS ({0})", fqo.Option.AnswerValues.Count);
//					foreach (var av in fqo.Option.AnswerValues) {
//						Console.WriteLine(
//							"\t\tUser: {0}, ValueInt: {1}, OptionTypeID: {2}, OptionType: {3}",
//							av.Answer.ProjectRoundUser != null ? av.Answer.ProjectRoundUser.ProjectRoundUserID.ToString() : "<null>",
//							av.GetValueInt(),
//							av.Option.OptionID,
//							av.Option.OptionType
//						);
//					}
//				}
//				Console.WriteLine();
//			}
//		}
//		
//		[Test]
//		public void TestMethod2()
//		{
//			var pru = ps.ReadProjectRoundUnit2(96);
//			var f = pru.ProjectRound.Feedback;
//			foreach (var fq in f.Questions) {
//				Console.WriteLine(fq.Question);
//			}
//		}
		
		[Test]
		public void TestMethod3()
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
			
//			var pru = ps.ReadProjectRoundUnit2(96);
//			var f = pru.ProjectRound.Feedback;
//			foreach (var fq in f.Questions) {
//				template = template.Replace("__SCRIPT__", fq.Question.ToChart().ToString());
//				Console.WriteLine(template);
//				using (var w = new StreamWriter("test.html")) {
//					w.WriteLine(template);
//				}
//				Process.Start("test.html");
//			}
		}
	}
}
