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
		IAnswerRepository ar = new AnswerRepositoryStub();
		IAnswerValueRepository avr = new AnswerValueRepositoryStub();
		
		[Test]
		public void TestMethod()
		{
			Assert.AreEqual(10, ar.FindAll().Count);
		}
	}
	
	public class AnswerRepositoryStub : BaseRepositoryStub<Answer>, IAnswerRepository
	{
		public AnswerRepositoryStub()
		{
			items.Add(new Answer { AnswerID = 1, ProjectRoundUnitID = 1, ProjectRoundUserID = 1 });
			items.Add(new Answer { AnswerID = 2, ProjectRoundUnitID = 1, ProjectRoundUserID = 2 });
			items.Add(new Answer { AnswerID = 3, ProjectRoundUnitID = 1, ProjectRoundUserID = 3 });
			items.Add(new Answer { AnswerID = 4, ProjectRoundUnitID = 1, ProjectRoundUserID = 4 });
			items.Add(new Answer { AnswerID = 5, ProjectRoundUnitID = 1, ProjectRoundUserID = 5 });
			items.Add(new Answer { AnswerID = 6, ProjectRoundUnitID = 1, ProjectRoundUserID = 6 });
			items.Add(new Answer { AnswerID = 7, ProjectRoundUnitID = 1, ProjectRoundUserID = 7 });
			items.Add(new Answer { AnswerID = 8, ProjectRoundUnitID = 1, ProjectRoundUserID = 8 });
			items.Add(new Answer { AnswerID = 9, ProjectRoundUnitID = 1, ProjectRoundUserID = 9 });
			items.Add(new Answer { AnswerID = 10, ProjectRoundUnitID = 1, ProjectRoundUserID = 10 });
		}
	}
	
	public class AnswerValueRepositoryStub : BaseRepositoryStub<AnswerValue>, IAnswerValueRepository
	{
		public AnswerValueRepositoryStub()
		{
			items.Add(new AnswerValue { AnswerID = 1, QuestionID = 1, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 1, QuestionID = 2, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 1, QuestionID = 3, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 1, QuestionID = 4, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 1, QuestionID = 5, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 1, QuestionID = 6, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 1, QuestionID = 7, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 1, QuestionID = 8, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 1, QuestionID = 9, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 1, QuestionID = 10, OptionID = 1, ValueInt = 5 });
			
			items.Add(new AnswerValue { AnswerID = 2, QuestionID = 1, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 2, QuestionID = 2, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 2, QuestionID = 3, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 2, QuestionID = 4, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 2, QuestionID = 5, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 2, QuestionID = 6, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 2, QuestionID = 7, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 2, QuestionID = 8, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 2, QuestionID = 9, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 2, QuestionID = 10, OptionID = 1, ValueInt = 5 });
			
			items.Add(new AnswerValue { AnswerID = 3, QuestionID = 1, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 3, QuestionID = 2, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 3, QuestionID = 3, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 3, QuestionID = 4, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 3, QuestionID = 5, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 3, QuestionID = 6, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 3, QuestionID = 7, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 3, QuestionID = 8, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 3, QuestionID = 9, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 3, QuestionID = 10, OptionID = 1, ValueInt = 5 });
			
			items.Add(new AnswerValue { AnswerID = 4, QuestionID = 1, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 4, QuestionID = 2, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 4, QuestionID = 3, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 4, QuestionID = 4, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 4, QuestionID = 5, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 4, QuestionID = 6, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 4, QuestionID = 7, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 4, QuestionID = 8, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 4, QuestionID = 9, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 4, QuestionID = 10, OptionID = 1, ValueInt = 5 });
			
			items.Add(new AnswerValue { AnswerID = 5, QuestionID = 1, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 5, QuestionID = 2, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 5, QuestionID = 3, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 5, QuestionID = 4, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 5, QuestionID = 5, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 5, QuestionID = 6, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 5, QuestionID = 7, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 5, QuestionID = 8, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 5, QuestionID = 9, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 5, QuestionID = 10, OptionID = 1, ValueInt = 3 });
			
			items.Add(new AnswerValue { AnswerID = 6, QuestionID = 1, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 6, QuestionID = 2, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 6, QuestionID = 3, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 6, QuestionID = 4, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 6, QuestionID = 5, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 6, QuestionID = 6, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 6, QuestionID = 7, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 6, QuestionID = 8, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 6, QuestionID = 9, OptionID = 1, ValueInt = 3 });
			items.Add(new AnswerValue { AnswerID = 6, QuestionID = 10, OptionID = 1, ValueInt = 3 });
			
			items.Add(new AnswerValue { AnswerID = 7, QuestionID = 1, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 7, QuestionID = 2, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 7, QuestionID = 3, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 7, QuestionID = 4, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 7, QuestionID = 5, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 7, QuestionID = 6, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 7, QuestionID = 7, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 7, QuestionID = 8, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 7, QuestionID = 9, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 7, QuestionID = 10, OptionID = 1, ValueInt = 2 });
			
			items.Add(new AnswerValue { AnswerID = 8, QuestionID = 1, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 8, QuestionID = 2, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 8, QuestionID = 3, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 8, QuestionID = 4, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 8, QuestionID = 5, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 8, QuestionID = 6, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 8, QuestionID = 7, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 8, QuestionID = 8, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 8, QuestionID = 9, OptionID = 1, ValueInt = 2 });
			items.Add(new AnswerValue { AnswerID = 8, QuestionID = 10, OptionID = 1, ValueInt = 2 });
			
			items.Add(new AnswerValue { AnswerID = 9, QuestionID = 1, OptionID = 1, ValueInt = 1 });
			items.Add(new AnswerValue { AnswerID = 9, QuestionID = 2, OptionID = 1, ValueInt = 1 });
			items.Add(new AnswerValue { AnswerID = 9, QuestionID = 3, OptionID = 1, ValueInt = 1 });
			items.Add(new AnswerValue { AnswerID = 9, QuestionID = 4, OptionID = 1, ValueInt = 1 });
			items.Add(new AnswerValue { AnswerID = 9, QuestionID = 5, OptionID = 1, ValueInt = 1 });
			items.Add(new AnswerValue { AnswerID = 9, QuestionID = 6, OptionID = 1, ValueInt = 1 });
			items.Add(new AnswerValue { AnswerID = 9, QuestionID = 7, OptionID = 1, ValueInt = 1 });
			items.Add(new AnswerValue { AnswerID = 9, QuestionID = 8, OptionID = 1, ValueInt = 1 });
			items.Add(new AnswerValue { AnswerID = 9, QuestionID = 9, OptionID = 1, ValueInt = 1 });
			items.Add(new AnswerValue { AnswerID = 9, QuestionID = 10, OptionID = 1, ValueInt = 1 });
			
			items.Add(new AnswerValue { AnswerID = 10, QuestionID = 1, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 10, QuestionID = 2, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 10, QuestionID = 3, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 10, QuestionID = 4, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 10, QuestionID = 5, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 10, QuestionID = 6, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 10, QuestionID = 7, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 10, QuestionID = 8, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 10, QuestionID = 9, OptionID = 1, ValueInt = 5 });
			items.Add(new AnswerValue { AnswerID = 10, QuestionID = 10, OptionID = 1, ValueInt = 5 });
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
