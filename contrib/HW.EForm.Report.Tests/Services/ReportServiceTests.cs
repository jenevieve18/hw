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
			Console.WriteLine(r.Internal);
			foreach (var p in r.Parts) {
				Console.WriteLine("  {0}, Question: {1}", p.Internal, p.Question.Internal);
			}
		}
	}
}
