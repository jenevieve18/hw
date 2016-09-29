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
	public class SurveyTests
	{
		[Test]
		public void TestMethod()
		{
			var s = new Survey {};
			s.AddQuestion(new Question {});
		}
	}
}
