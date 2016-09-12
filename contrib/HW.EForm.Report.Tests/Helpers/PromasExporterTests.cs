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
using System.Collections.Generic;

namespace HW.EForm.Report.Tests.Helpers
{
	[TestFixture]
	public class PromasExporterTests
	{
		FeedbackService s = ServiceFactory.CreateFeedbackService();
		
		[Test]
		public void TestExport()
		{
			var x = new PromasExporter();
			var f = s.ReadFeedbackWithAnswers(8, 62, new int[] { 1183 }, 1);
//			var charts = new List<Chart>();
//			foreach (var fq in f.Questions) {
//				if (fq.IsPartOfChart && !f.HasGroupedChart(fq.PartOfChart)) {
//					charts.Add(f.GetQuestions(fq.PartOfChart).ToChart());
//				} else if (!fq.IsPartOfChart) {
//					charts.Add(fq.Question.ToChart());
//				}
//			}
			
			using (var fs = new FileStream("test.pptx", FileMode.Create)) {
				var m = x.Export(f.Charts) as MemoryStream;
				m.WriteTo(fs);
			}
			Process.Start("test.pptx");
		}
	}
}
