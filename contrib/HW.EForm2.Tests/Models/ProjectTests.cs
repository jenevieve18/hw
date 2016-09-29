// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace HW.EForm2.Tests.Models
{
	[TestFixture]
	public class ProjectTests
	{
		[Test]
		public void TestAddProject()
		{
			var p = new Project { Internal = "Project1" };
			
			var s1 = new Survey {};
			var s2 = new Survey {};
			var s3 = new Survey {};
			var s4 = new Survey {};
			var s5 = new Survey {};
			
			p.AddSurvey(s1);
			p.AddSurvey(s2);
			p.AddSurvey(s3);
			p.AddSurvey(s4);
			p.AddSurvey(s5);
			
			Assert.AreEqual(5, p.Surveys.Count);
			
			var pr1 = new ProjectRound { };
			Assert.IsFalse(pr1.HasStarted);
			Assert.IsFalse(pr1.IsClosed);
			pr1.AddUnit("Unit11");
			pr1.AddUnit("Unit12");
			
			Assert.AreEqual(2, pr1.Units.Count);
			
			p.AddRound(pr1);
			
			var pr2 = new ProjectRound {};
			pr2.AddUnits(new List<ProjectRoundUnit>(
				new[] {
					new ProjectRoundUnit { ID = "U21", Unit = "Unit21" },
					new ProjectRoundUnit { ID = "U22", Unit = "Unit22" },
					new ProjectRoundUnit { ID = "U32", Unit = "Unit23" }
				}
			));
			p.AddRound(pr2);
			Assert.AreEqual(3, pr2.Units.Count);
			
			p.AddRound(new ProjectRound {});
			
			Assert.AreEqual(3, p.Rounds.Count);
		}
	}
}
