// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Services;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Views
{
	[TestFixture]
	public class ProjectShowTests
	{
		ProjectShow v = new ProjectShow();
		ProjectService service = new ProjectService();
		
		[Test]
		public void TestShow()
		{
			var p = service.ReadProject(8, 1);
			Console.WriteLine("ProjectID: {0}, Internal: {1}", p.ProjectID, p.Internal);
			foreach (var pr in p.Rounds) {
				Console.WriteLine("\tProjectRoundID: {0}, Internal: {1}", pr.ProjectRoundID, pr.Internal);
				foreach (var pru in pr.Units) {
					Console.WriteLine("\t\tProjectRoundUnitID: {0}: {1}", pru.ProjectRoundUnitID, pru.Unit);
				}
			}
		}
	}
}
