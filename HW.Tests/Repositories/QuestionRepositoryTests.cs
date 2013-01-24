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
