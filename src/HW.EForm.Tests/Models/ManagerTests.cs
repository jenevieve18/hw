// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using NUnit.Framework;

namespace HW.EForm.Tests.Models
{
	[TestFixture]
	public class ManagerTests
	{
		[Test]
		public void TestMethod()
		{
			var m = new Manager { Name = "Manager1", Email = "info@localhost.com", Password = "password", Phone = "091234567890" };
			
			var p = new Project { Name = "Project1" };
			var pr = new ProjectRound { Internal = "Round1" };
			pr.AddUnit("Unit1");
			pr.AddUnit("Unit2");
			pr.AddUnit("Unit3");
			pr.AddUnit("Unit4");
			pr.AddUnit("Unit5");
			p.AddRound(pr);
			
			var mpr = new ManagerProjectRound {};
			mpr.AddUnit(pr.Units[0]);
			mpr.AddUnit(pr.Units[2]);
			mpr.AddUnit(pr.Units[4]);
			
			m.AddRound(mpr);
			
			Assert.AreEqual(1, m.Rounds.Count);
			Assert.AreEqual(3, m.Rounds[0].Units.Count);
		}
	}
}
