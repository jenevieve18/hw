// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;
using NUnit.Framework;
using System.Collections.Generic;

namespace HW.EForm.Report.Tests.Repositories
{
	[TestFixture]
	public class AnswerRepositoryTests
	{
		[Test]
		public void TestMethod()
		{
		}
	}
	
	public class AnswerValueRepositoryStub : BaseRepositoryStub<AnswerValue>, IAnswerValueRepository
	{
		public IList<AnswerValue> FindByQuestionOptionsAndUnit(int questionID, IList<QuestionOption> options, int projectRoundID, int projectRoundUnitID)
		{
			throw new NotImplementedException();
		}
	}
}
