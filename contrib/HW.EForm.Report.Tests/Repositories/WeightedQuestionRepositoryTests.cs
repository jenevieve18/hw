// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Repositories
{
	[TestFixture]
	public class WeightedQuestionRepositoryTests
	{
		[Test]
		public void TestMethod()
		{
		}
	}
	
	public class WeightedQuestionOptionRepositoryStub : BaseRepositoryStub<WeightedQuestionOption>, IWeightedQuestionOptionRepository
	{
		public WeightedQuestionOptionRepositoryStub()
		{
			items.Add(new WeightedQuestionOption { QuestionID = 1 });
		}
		
		public WeightedQuestionOption ReadByQuestion(int questionID)
		{
			return items.Find(x => x.QuestionID == questionID);
		}
	}
}
