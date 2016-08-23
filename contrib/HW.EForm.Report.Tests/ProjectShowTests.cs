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
		ProjectService service = new ProjectService();
		
		[Test]
		public void TestShow()
		{
			var p = service.ReadProject(13, 1);
			foreach (var r in p.Rounds) {
				Console.WriteLine(r.Internal);
				foreach (var u in r.Units) {
					Console.WriteLine("    {0}: {1}", u.Unit, u.ID);
				}
			}
		}
	}
}
