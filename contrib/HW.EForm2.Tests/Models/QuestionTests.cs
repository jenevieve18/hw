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
	public class QuestionTests
	{
		[Test]
		public void TestMethod()
		{
			var q = new Question {};
			var o = new Option {};
			
			o.AddComponent(new OptionComponent { Internal = "Very bad", ExportValue = 1 });
			o.AddComponent(new OptionComponent { Internal = "Bad", ExportValue = 2 });
			o.AddComponent(new OptionComponent { Internal = "Neither good nor bad", ExportValue = 3 });
			o.AddComponent(new OptionComponent { Internal = "Good", ExportValue = 4 });
			o.AddComponent(new OptionComponent { Internal = "Very good", ExportValue = 5 });
		}
		
		[Test]
		public void Test()
		{
			var c = new OptionComponentContainer {};
			var oc = new OptionComponent { Internal = "Very good", ExportValue = 5, Container = c };
		}
	}
}
