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
	public class IndexTests
	{
		[Test]
		public void TestMethod()
		{
			var q1 = new Question {};
			q1.AddOption(new Option {});
			
			var q2 = new Question {};
			q2.AddOption(new Option {});
			
			var q3 = new Question {};
			q3.AddOption(new Option {});
			
			var q4 = new Question {};
			q4.AddOption(new Option {});
			
			var q5 = new Question {};
			q5.AddOption(new Option {});
			
			var q6 = new Question {};
			q6.AddOption(new Option {});
			
			var q7 = new Question {};
			q7.AddOption(new Option {});
			
			var q8 = new Question {};
			q8.AddOption(new Option {});
			
			var q9 = new Question {};
			q9.AddOption(new Option {});
			
			var i1 = new Index {};
			i1.AddPart(new IndexPart { QuestionOption = q1.FirstOption });
			i1.AddPart(new IndexPart { QuestionOption = q2.FirstOption });
			i1.AddPart(new IndexPart { QuestionOption = q3.FirstOption });
			
			var i2 = new Index {};
			i2.AddPart(new IndexPart { QuestionOption = q4.FirstOption });
			i2.AddPart(new IndexPart { QuestionOption = q5.FirstOption });
			i2.AddPart(new IndexPart { QuestionOption = q6.FirstOption });
			
			var i3 = new Index {};
			i3.AddPart(new IndexPart { QuestionOption = q7.FirstOption });
			i3.AddPart(new IndexPart { QuestionOption = q8.FirstOption });
			i3.AddPart(new IndexPart { QuestionOption = q9.FirstOption });
		}
	}
}
