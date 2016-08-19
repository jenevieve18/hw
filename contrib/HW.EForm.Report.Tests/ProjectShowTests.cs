// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Services;
using NUnit.Framework;

namespace HW.EForm.Report.Tests
{
	[TestFixture]
	public class ProjectShowTests
	{
		ProjectShow v = new ProjectShow();
		ProjectService s = new ProjectService();
		
		[Test]
		public void TestShow()
		{
			var p = s.ReadProject(13);
			foreach (var r in p.Rounds) {
				Console.WriteLine(r.Internal);
//				Console.WriteLine("  {0}: {1}", r.SurveyID, r.Survey.Internal);
				foreach (var u in r.Units) {
					Console.WriteLine("    {0}: {1}", u.Unit, u.ID);
				}
			}
		}
	}
}
