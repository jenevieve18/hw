// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>
using System;
using HW.EForm.Core.Models;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Models
{
	[TestFixture]
	public class ProjectTests
	{
		[Test]
		public void TestMethod()
		{
			var p = new Project { Name = "DEG Health Project" };
			var s = new Survey {};
			p.AddSurvey(s);
			
			var pr = new ProjectRound { Internal = "Q1 (Jan-Mar)" };
			pr.AddUnit("DEG IT");
			pr.AddUnit("DEG Customer Support");
			pr.AddUnit("DEG Graphics Team");
			p.AddRound(pr);
		}
	}
}
