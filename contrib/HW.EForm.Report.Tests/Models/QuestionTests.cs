﻿// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Models
{
	[TestFixture]
	public class QuestionTests
	{
		[Test]
		public void TestMethod()
		{
			var q = new Question {
				Internal = "Question 1"
			};
		}
	}
}
