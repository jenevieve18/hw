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
		SqlAnswerRepository sar = new SqlAnswerRepository();
		SqlAnswerValueRepository savr = new SqlAnswerValueRepository();
		
		SqlQuestionRepository sqr = new SqlQuestionRepository();
		SqlOptionRepository sor = new SqlOptionRepository();
		
		public AnswerService()
		{
		}
		
		public Answer ReadAnswer(int answerID)
		{
			var a = sar.Read(answerID);
			if (a != null) {
				a.Values = savr.FindByAnswer(answerID);
				foreach (var av in a.Values) {
					av.Question = sqr.Read(av.QuestionID);
					av.Option = sor.Read(av.OptionID);
				}
			}
			return a;
		}
		
		public Answer ReadAnswerByProjectRound(int projectRoundID, int projectRoundUnitID)
		{
			var a = sar.ReadByProjectRound(projectRoundID, projectRoundUnitID);
			if (a != null) {
				a.Values = savr.FindByAnswer(a.AnswerID);
				foreach (var av in a.Values) {
					av.Question = sqr.Read(av.QuestionID);
					av.Option = sor.Read(av.OptionID);
				}
			}
			return a;
		}
		
		public IList<AnswerValue> FindByQuestion(int questionID)
		{
			return savr.FindByQuestion(questionID);
		}
	}
}
