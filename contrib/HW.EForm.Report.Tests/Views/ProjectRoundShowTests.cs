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
	public class ProjectRoundShowTests
	{
		ProjectService s = new ProjectService();
		
		[Test]
		public void TestShow()
		{
			var pr = s.ReadProjectRound(13, 1);
			Console.WriteLine("ProjectRoundID: {0}, Internal: {1}", pr.ProjectRoundID, pr.Internal);
			foreach (var pru in pr.Units) {
				Console.WriteLine("\tProjectRoundUnitID: {0}, Unit: {1}", pru.ProjectRoundUnitID, pru.Unit);
			}
		}
	}
}
