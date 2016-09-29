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
	public class ProjectTests
	{
		[Test]
		public void TestMethod()
		{
			var p = new Project { Internal = "Project1", Name = "Project1" };
			p.AddSurvey(new Survey {});
			p.AddSurvey(new Survey {});
			p.AddSurvey(new Survey {});
			p.AddSurvey(new Survey {});
			p.AddSurvey(new Survey {});
			
			Assert.AreEqual(5, p.Surveys.Count);
			
			var pr3 = new ProjectRound { Internal = "Round3" };
			
			Assert.IsFalse(pr3.HasStarted);
			Assert.IsFalse(pr3.IsClosed);
			
			var pru = new ProjectRoundUnit { Unit = "Unit1" };
			pru.AddUser("info@localhost.com");
			
			pr3.AddUnit(pru);
			pr3.AddUnit("Unit2");
			
			Assert.AreEqual(2, pr3.Units.Count);
			
			p.AddRound(new ProjectRound {});
			p.AddRound(new ProjectRound {});
			p.AddRound(pr3);
			
			Assert.AreEqual(3, p.Rounds.Count);
		}
	}
}
