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
	public class ManagerServiceTests
	{
		ManagerService ms = new ManagerService();
		
		[Test]
		public void TestReadManager()
		{
			var m = ms.ReadManager(1);
			foreach (var mpr in m.ProjectRounds) {
				Console.WriteLine("  {0} >> {1}", mpr.ProjectRound.Project.Internal, mpr.ProjectRound.Internal);
				foreach (var mpru in mpr.Units) {
					Console.WriteLine("    ID: {1}, Unit: {0}", mpru.ProjectRoundUnit.Unit, mpru.ProjectRoundUnit.ProjectRoundUnitID);
					var s = mpru.ProjectRoundUnit.Survey != null ? mpru.ProjectRoundUnit.Survey : mpr.ProjectRound.Survey;
					if (s != null) {
						Console.WriteLine("    Survey: {0}", s.Internal);
						foreach (var sq in s.Questions) {
							Console.WriteLine("      {0}", sq.Question.Internal);
						}
					}
					var r = mpru.ProjectRoundUnit.Report;
					Console.WriteLine("    Report: {0}", r.Internal);
					foreach (var rp in r.Parts) {
						Console.WriteLine("      {0}", rp.Internal);
					}
				}
			}
		}
	}
}
