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
			var r = s.ReadReport(1);
			Console.WriteLine("{0}: {1}", r.ReportID, r.Internal);
			foreach (var rp in r.Parts) {
				Console.WriteLine("\t{0}: {1}", rp.ReportPartID, rp.Internal);
			}
		}
	}
}
