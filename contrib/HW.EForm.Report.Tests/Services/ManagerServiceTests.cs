// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using HW.EForm.Core.Services;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Services
{
	[TestFixture]
	public class ManagerServiceTests
	{
		ManagerService s = new ManagerService();
		
		[Test]
		public void TestReadManager()
		{
			var m = s.ReadManager(1);
			Console.WriteLine(m.Name);
			foreach (var mpr in m.ProjectRounds) {
				Console.WriteLine("\t" + mpr.ProjectRound.Internal);
				foreach (var mpru in mpr.Units) {
					Console.WriteLine("\t\t" + mpru.ProjectRoundUnit.Unit);
				}
			}
		}
		
		[Test]
		public void TestSaveManager()
		{
			s.SaveManager(new Manager { Name = "ian", Email = "ian.escarro@gmail.com" });
		}
	}
}
