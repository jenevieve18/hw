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
	public class FeedbackTests
	{
		[Test]
		public void TestMethod()
		{
			var f = new Feedback { FeedbackText = "Feedback1" };
			
			var q1 = new Question {};
			q1.AddOption(new Option {});
			
			var q2 = new Question {};
			q2.AddOption(new Option {});
			
			var q3 = new Question {};
			q3.AddOption(new Option {});
			
			f.AddQuestion(q1);
			f.AddQuestion(q2);
			f.AddQuestion(q3);
			f.AddQuestion(new Question {});
			f.AddQuestion(new Question {});
			f.AddQuestion(new Question {});
			f.AddQuestion(new Question {});
			f.AddQuestion(new Question {});
			f.AddQuestion(new Question {});
			
			var i1 = new Index {};
			i1.AddPart(new IndexPart { Question = q1 });
			i1.AddPart(new IndexPart { Question = q2 });
			i1.AddPart(new IndexPart { Question = q3 });
			
			var i2 = new Index {};
			i2.AddPart(new IndexPart { Question = q1 });
			i2.AddPart(new IndexPart { Question = q2 });
			i2.AddPart(new IndexPart { Question = q3 });
			
			var i3 = new Index {};
			i3.AddPart(new IndexPart { Question = q1 });
			i3.AddPart(new IndexPart { Question = q2 });
			i3.AddPart(new IndexPart { Question = q3 });
			
			f.AddQuestion(i1);
			f.AddQuestion(i2);
			f.AddQuestion(i3);
			
			Assert.AreEqual(9, f.CountQuestions());
			
			Assert.AreEqual(3, f.CountIndexes());
		}
	}
}
