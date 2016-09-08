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
	public class FeedbackTests
	{
		[Test]
		public void TestProperties()
		{
			Feedback f = new Feedback {
				FeedbackText = "Feedback About Gender"
			};
			var q = new Question { Internal = "Gender" };
			var o = new Option { Internal = "Gender" };
			o.AddComponent(new OptionComponent { Internal = "Male" });
			o.AddComponent(new OptionComponent { Internal = "Female" });
			q.AddOption(o);
			f.AddQuestion(q);
			
			Assert.AreEqual(1, f.Questions.Count);
			Assert.AreEqual(2, q.FirstOption.Option.Components.Count);
		}
	}
}
