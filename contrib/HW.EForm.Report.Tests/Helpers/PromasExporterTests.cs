﻿// <file>
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

namespace HW.EForm.Report.Tests.Helpers
{
	[TestFixture]
	public class PromasExporterTests
	{
		FeedbackService s = new FeedbackService(new SqlFeedbackRepository(),
		                                        new SqlFeedbackQuestionRepository(),
		                                        new SqlQuestionRepository(),
		                                        new SqlQuestionOptionRepository(),
		                                        new SqlQuestionLangRepository(),
		                                        new SqlWeightedQuestionOptionRepository(),
		                                        new SqlOptionRepository(),
		                                        new SqlOptionComponentsRepository(),
		                                        new SqlOptionComponentRepository(),
		                                        new SqlOptionComponentLangRepository(),
		                                        new SqlProjectRoundUnitRepository(),
		                                        new SqlAnswerValueRepository());
		
		[Test]
		public void TestMethod()
		{
//			var x = new PromasExporter();
//			
//			var f = s.ReadFeedbackWithAnswers(6, 13, new int[] { 97 });
//			using (var fs = new FileStream("test.pptx", FileMode.Create)) {
//				var m = x.Export(f) as MemoryStream;
//				m.WriteTo(fs);
//			}
//			Process.Start("test.pptx");
		}
	}
}