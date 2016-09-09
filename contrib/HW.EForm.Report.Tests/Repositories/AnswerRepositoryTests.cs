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
		public AnswerValueRepositoryStub()
		{
			for (int i = 1; i <= 10; i++) {
				items.Add(new AnswerValue { Answer = new Answer { ProjectRoundID = 1, ProjectRoundUnitID = i }, QuestionID = 1, OptionID = 1 });
			}
		}
		
		public IList<AnswerValue> FindByQuestionOptionsAndUnit(int questionID, IList<QuestionOption> options, int projectRoundID, int projectRoundUnitID)
		{
			var values = items.FindAll(x => x.QuestionID == questionID && x.Answer.ProjectRoundID == projectRoundID && x.Answer.ProjectRoundUnitID == projectRoundUnitID);
			var newValues = new List<AnswerValue>();
			foreach (var o in options) {
				newValues.AddRange(values.FindAll(x => x.OptionID == o.OptionID));
			}
			return newValues;
		}
	}
}
