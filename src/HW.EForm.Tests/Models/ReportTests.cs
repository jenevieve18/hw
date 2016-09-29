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
	public class ReportTests
	{
		[Test]
		public void TestMethod()
		{
			var r = new Report { Internal = "Report1" };
			
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
			
			r.AddPart(new ReportPart { QuestionOption = q1.FirstOption });
			r.AddPart(new ReportPart { QuestionOption = q2.FirstOption });
			r.AddPart(new ReportPart { QuestionOption = q3.FirstOption });
			r.AddPart(new ReportPart { QuestionOption = q4.FirstOption });
			r.AddPart(new ReportPart { QuestionOption = q5.FirstOption });
			r.AddPart(new ReportPart { QuestionOption = q6.FirstOption });
			r.AddPart(new ReportPart { QuestionOption = q7.FirstOption });
			r.AddPart(new ReportPart { QuestionOption = q8.FirstOption });
			r.AddPart(new ReportPart { QuestionOption = q9.FirstOption });
			
			var i1 = new Index {};
			i1.AddPart(new IndexPart { QuestionOption = q1.FirstOption });
			
			var i2 = new Index {};
			i2.AddPart(new IndexPart { QuestionOption = q2.FirstOption });
			
			var i3 = new Index {};
			i3.AddPart(new IndexPart { QuestionOption = q3.FirstOption });
			
			var rp1 = new ReportPart {};
			rp1.AddComponent(new ReportPartComponent { Index = i1 });
			
			var rp2 = new ReportPart {};
			rp2.AddComponent(new ReportPartComponent { Index = i2 });
			
			var rp3 = new ReportPart {};
			rp3.AddComponent(new ReportPartComponent { Index = i3 });
			
			r.AddPart(rp1);
			r.AddPart(rp2);
			r.AddPart(rp3);
			
			var rp4 = new ReportPart {};
			rp4.AddComponent(new ReportPartComponent { WeightedQuestionOption = new WeightedQuestionOption {}});
			
			r.AddPart(rp4);
			
			Assert.AreEqual(9, r.CountQuestions());
			
			Assert.AreEqual(3, r.CountIndexes());
			
			Assert.AreEqual(1, r.CountWeightedQuestionOptions());
		}
	}
}
