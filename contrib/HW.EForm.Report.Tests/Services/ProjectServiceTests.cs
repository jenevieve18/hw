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
	public class ProjectServiceTests
	{
		ProjectService ps = new ProjectService();
		
		[Test]
		public void TestMethod()
		{
			var pru = ps.ReadProjectRoundUnit(97);
			var s = pru.Survey;
			Console.WriteLine(s.ToString());
			foreach (var sq in s.Questions) {
				Console.WriteLine(sq.Question.Internal);
				foreach (var sqo in sq.Question.Options) {
					foreach (var oc in sqo.Option.Components) {
						Console.WriteLine("  " + oc.Component.CurrentLanguage.Text);
					}
				}
				Console.WriteLine();
			}
		}
	}
}
