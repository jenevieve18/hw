// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using NUnit.Framework;

namespace HW.EForm2.Tests.Models
{
	[TestFixture]
	public class ManagerTests
	{
		[Test]
		public void TestMethod()
		{
			var m = new Manager {};
			Assert.IsNull(m.Email);
			
			var pr = new ProjectRound {};
			pr.AddUnit("Unit1");
			pr.AddUnit("Unit2");
			pr.AddUnit("Unit3");
			pr.AddUnit("Unit4");
			pr.AddUnit("Unit5");
			
			Assert.AreEqual(5, pr.Units.Count);
			
			var mpru = new ManagerProjectRound {};
			mpru.AddUnit(pr.Units[0]);
			mpru.AddUnit(pr.Units[2]);
			mpru.AddUnit(pr.Units[4]);
			
			m.AddProjectRound(mpru);
			
			Assert.AreEqual(3, m.ProjectRounds[0].Units.Count);
		}
	}
}
