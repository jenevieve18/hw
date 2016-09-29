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
	public class FeedbackTests
	{
		[Test]
		public void TestMethod()
		{
			var f = new Feedback {};
			
			var q1 = new Question(new Option {});
			var q2 = new Question(new Option {});
			var q3 = new Question(new Option {});
			var q4 = new Question(new Option {});
			var q5 = new Question(new Option {});
			var q6 = new Question(new Option {});
			var q7 = new Question(new Option {});
			var q8 = new Question(new Option {});
			var q9 = new Question(new Option {});
			
			f.AddQuestion(q1.FirstOption);
			f.AddQuestion(q2.FirstOption);
			f.AddQuestion(q3.FirstOption);
			f.AddQuestion(q4.FirstOption);
			f.AddQuestion(q5.FirstOption);
			f.AddQuestion(q6.FirstOption);
			f.AddQuestion(q7.FirstOption);
			f.AddQuestion(q8.FirstOption);
			f.AddQuestion(q9.FirstOption);
			
			var i1 = new Index {};
			var i2 = new Index {};
			var i3 = new Index {};
			
			f.AddQuestion(i1);
			f.AddQuestion(i2);
			f.AddQuestion(i3);
		}
	}
}
