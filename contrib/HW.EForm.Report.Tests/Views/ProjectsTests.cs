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
	public class ProjectsTests
	{
		Projects v = new Projects();
		ProjectService s = new ProjectService();
		
		[Test]
		public void TestIndex()
		{
			foreach (var p in s.FindByManager(1)) {
				Console.WriteLine("ProjectID: {0}, Internal: {1}", p.ProjectID, p.Internal);
			}
		}
	}
}
