// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Services;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Services
{
	[TestFixture]
	public class ReportServiceTests
	{
		ReportService s = new ReportService();
		
		[Test]
		public void TestReadReport()
		{
			var r = s.ReadReport(3);
			Console.WriteLine("Report: {0}", r.Internal);
			foreach (var rp in r.Parts) {
				Console.WriteLine("  Part: {0}, Question: {1}", rp.Internal, rp.Question.Internal);
			}
		}
	}
}
