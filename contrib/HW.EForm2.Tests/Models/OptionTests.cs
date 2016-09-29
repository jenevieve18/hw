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
	public class OptionTests
	{
		[Test]
		public void TestMethod()
		{
			var o = new Option { Internal = "Option1", OptionType = OptionTypes.SingleChoice, OptionPlacement = OptionPlacements.HorizontalLabelsTop };
			o.AddComponent(new OptionComponent { Internal = "Very bad", ExportValue = 1 });
			o.AddComponent(new OptionComponent { Internal = "Bad", ExportValue = 2 });
			o.AddComponent(new OptionComponent { Internal = "Neither good nor bad", ExportValue = 3 });
			o.AddComponent(new OptionComponent { Internal = "Good", ExportValue = 4 });
			o.AddComponent(new OptionComponent { Internal = "Very good", ExportValue = 5 });
			
			Assert.AreEqual(5, o.Components.Count);
		}
		
		[Test]
		public void TestContainer()
		{
			var oc = new OptionContainer { Container = "Container1" };
			Assert.AreEqual("Container1", oc.Container);
		}
	}
}
