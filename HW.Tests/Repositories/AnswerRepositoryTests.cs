//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core;
using NUnit.Framework;

namespace HW.Tests.Repositories
{
	[TestFixture]
	public class AnswerRepositoryTests
	{
		SqlAnswerRepository r;
		
		[SetUp]
		public void Setup()
		{
			r = new SqlAnswerRepository();
		}
		
		[Test]
		public void TestRoundByProjectRound()
		{
			r.ReadByProjectRound(123);
		}
		
		[Test]
		public void TestFindByRoundQuestionAndOption()
		{
			r.FindByRoundQuestionAndOption(123, 310, 79);
		}
		
		[Test]
		public void TestReadByKey()
		{
			r.ReadByKey("FAF15E47E0884CC3B6DB2A1334938C42");
		}
		
		[Test]
		public void TestReadMinMax()
		{
			var a = r.ReadMinMax("dbo.cf_yearWeek", 1, 1, 2011, 2012, "");
		}
		
		[Test]
		public void TestFindBackgroundAnswers()
		{
			r.FindBackgroundAnswers(2);
		}
		
		[Test]
		public void TestReadByGroup()
		{
			var a = r.ReadByGroup("dbo.cf_yearWeek", 2011, 2012, "");
		}
		
		[Test]
		public void TestReadByQuestionAndOption()
		{
			r.ReadByQuestionAndOption(784, 310, 79);
		}
		
		[Test]
		public void TestCountByDate()
		{
			r.CountByDate(2011, 2012, "");
		}
		
		[Test]
		public void TestCountByProject()
		{
			r.CountByProject(1, 2011, 2012);
		}
		
		[Test]
		public void TestCountByValueWithDateOptionAndQuestion()
		{
			r.CountByValueWithDateOptionAndQuestion(1, 2011, 2012, 1, 1, "");
		}
		
		[Test]
		public void TestUpdateAnswer()
		{
			r.UpdateAnswer(1, 1);
		}
		
		[Test]
		public void TestG()
		{
			r.g(123);
		}
		
		[Test]
		public void TestF()
		{
			r.f(123);
		}
	}
}
