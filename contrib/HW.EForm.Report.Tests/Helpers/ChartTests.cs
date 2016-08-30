// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Diagnostics;
using System.IO;
using HW.EForm.Core.Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace HW.EForm.Report.Tests.Helpers
{
	[TestFixture]
	public class ChartTests
	{
		Chart c;
		string template;
		
//		[SetUp]
//		public void Setup()
//		{
//			template = @"<html>
//<head>
//<meta charset='utf-8'>
//<script src='https://code.jquery.com/jquery-2.2.4.min.js'></script>
//<script src='https://code.highcharts.com/highcharts.js'></script>
//<script src='https://code.highcharts.com/highcharts-more.js'></script>
//<script src='https://code.highcharts.com/modules/exporting.js'></script>
//<script>
//$(function() {
//	$('#container').highcharts(__SCRIPT__);
//});
//</script>
//</head>
//<body>
//<div id=container></div>
//</body.";
//		}
//		
//		[Test]
//		public void TestBoxPlot()
//		{
//			c = new HighchartsBoxplot { Title = "Highcharts Box Plot Example" };
//			c.Categories = new List<string>(new[] { "1", "2", "3", "4", "5" });
//			c.XAxisTitle = "Experiment No.";
//			c.YAxisTitle = "Observations";
//			c.Series.Add(new Series("Observations", new List<double>(new[] { 760.0, 801, 848, 895, 965 })));
//			
//			template = template.Replace("__SCRIPT__", c.ToString());
//			Console.WriteLine(template);
//			using (var w = new StreamWriter("boxplot.html")) {
//				w.WriteLine(template);
//			}
//			Process.Start("boxplot.html");
//		}
//		
//		[Test]
//		public void TestLineChart()
//		{
//			c = new LineChart { Title = "Monthly Average Temperature", Subtitle = "Source: WorldClimate.com" };
//			c.Categories = new List<string>(new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec" });
//			c.YAxisTitle = "Temperature (°C)";
//			c.Series.Add(new Series("Tokyo", new List<double>(new[] { 7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6 })));
//			c.Series.Add(new Series("New York", new List<double>(new[] { -0.2, 0.8, 5.7, 11.3, 17.0, 22.0, 24.8, 24.1, 20.1, 14.1, 8.6, 2.5 })));
//			c.Series.Add(new Series("Berlin", new List<double>(new[] { -0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0 })));
//			c.Series.Add(new Series("London", new List<double>(new[] { 3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8 })));
//			
//			template = template.Replace("__SCRIPT__", c.ToString());
//			Console.WriteLine(template);
//			using (var w = new StreamWriter("boxplot.html")) {
//				w.WriteLine(template);
//			}
//			Process.Start("boxplot.html");
//		}
	}
}
