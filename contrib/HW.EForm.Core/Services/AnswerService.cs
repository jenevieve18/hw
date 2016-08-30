// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;

namespace HW.EForm.Core.Services
{
	public class AnswerService
	{
		SqlAnswerRepository answerRepo = new SqlAnswerRepository();
		SqlAnswerValueRepository answerValueRepo = new SqlAnswerValueRepository();
		
		SqlQuestionRepository questionRepo = new SqlQuestionRepository();
		SqlOptionRepository optionRepo = new SqlOptionRepository();
		
		public AnswerService()
		{
		}
		
		public Answer ReadAnswer(int answerID)
		{
			var a = answerRepo.Read(answerID);
			if (a != null) {
				a.Values = answerValueRepo.FindByAnswer(answerID);
				foreach (var av in a.Values) {
					av.Question = questionRepo.Read(av.QuestionID);
					av.Option = optionRepo.Read(av.OptionID);
				}
			}
			return a;
		}
		
		public Answer ReadAnswerByProjectRound(int projectRoundID, int projectRoundUnitID)
		{
			var a = answerRepo.ReadByProjectRound(projectRoundID, projectRoundUnitID);
			if (a != null) {
				a.Values = answerValueRepo.FindByAnswer(a.AnswerID);
				foreach (var av in a.Values) {
					av.Question = questionRepo.Read(av.QuestionID);
					av.Option = optionRepo.Read(av.OptionID);
				}
			}
			return a;
		}
		
		public IList<AnswerValue> FindByQuestion(int questionID)
		{
			return answerValueRepo.FindByQuestion(questionID);
		}
		
		public IList<AnswerValue> FindByQuestionOptionsAndUnits(int questionID, IList<QuestionOption> options, int projectRoundID, IList<ProjectRoundUnit> projectRoundUnits)
		{
			var av = answerValueRepo.FindByQuestionOptionsAndUnits(questionID, options, projectRoundID, projectRoundUnits);
			return av;
		}
		
		public IList<AnswerValue> FindByQuestionOptionAndUnit(int questionID, int optionID, int projectRoundID, int projectRoundUnitID)
		{
			var av = answerValueRepo.FindByQuestionOptionAndUnit(questionID, optionID, projectRoundID, projectRoundUnitID);
			return av;
		}
	}
}
