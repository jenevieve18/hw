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

namespace HW.EForm.Report.Tests.Helpers
{
	[TestFixture]
	public class HighchartsTests
	{
		QuestionService s = new QuestionService();
		
		[SetUpAttribute]
		public void Setup()
		{
		}
		
		[Test]
		public void TestVAS()
		{
			var q = s.ReadQuestionWithAnswers(238, 62, new int[] { 1183, 1184 });
			var chart = new HighchartsBoxplot(q.ToChart());
			using (var w = new StreamWriter("chart.html")) {
				Console.WriteLine(chart);
				w.WriteLine(chart);
			}
			Process.Start("chart.html");
		}
		
		[Test]
		public void TestSingleChoice()
		{
			var q = s.ReadQuestionWithAnswers(331, 62, new int[] { 1183 });
			var chart = new HighchartsColumnChart(q.ToChart());
			using (var w = new StreamWriter("chart.html")) {
				Console.WriteLine(chart);
				w.WriteLine(chart);
			}
			Process.Start("chart.html");
		}
		
		[Test]
		public void TestSingleChoice2()
		{
			var q = s.ReadQuestionWithAnswers(331, 62, new int[] { 1183, 1185 });
			var chart = new HighchartsLineChart(q.ToChart());
			using (var w = new StreamWriter("chart.html")) {
				Console.WriteLine(chart);
				w.WriteLine(chart);
			}
			Process.Start("chart.html");
		}
	}
}
