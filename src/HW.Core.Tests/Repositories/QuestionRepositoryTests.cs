using System;
using HW.Core;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Core.Tests.Repositories
{
	[TestFixture]
	public class QuestionRepositoryTests
	{
		SqlQuestionRepository r;
		
		[SetUp]
		public void Setup()
		{
			r = new SqlQuestionRepository();
		}
		
		[Test]
		public void TestFindBackgroundQuestions()
		{
			r.FindBackgroundQuestions(100);
		}
		
		[Test]
		public void TestFindLikeBackgroundQuestions()
		{
			r.FindLikeBackgroundQuestions("1");
		}
	}
}
