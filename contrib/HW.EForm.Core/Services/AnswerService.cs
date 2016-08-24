// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
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
		
//		SqlProjectRoundRepository sprr = new SqlProjectRoundRepository();
//		SqlProjectRoundUnitRepository sprur = new SqlProjectRoundUnitRepository();
		
		public AnswerService()
		{
		}
		
		public Answer ReadAnswer(int answerID)
		{
			var a = sar.Read(answerID);
//			a.ProjectRound = sprr.Read(a.ProjectRoundID);
//			a.ProjectRoundUnitID = sprur.Read(a.ProjectRoundUnitID);
			a.Values = savr.FindByAnswer(answerID);
			foreach (var v in a.Values) {
				v.Question = sqr.Read(v.QuestionID);
				v.Option = sor.Read(v.OptionID);
			}
			return a;
		}
	}
}
