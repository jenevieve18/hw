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
		SqlAnswerRepository ar = new SqlAnswerRepository();
		SqlAnswerValueRepository avr = new SqlAnswerValueRepository();
		SqlQuestionRepository qr = new SqlQuestionRepository();
		SqlOptionRepository or = new SqlOptionRepository();
		
		public AnswerService()
		{
		}
		
		public Answer ReadAnswer(int answerID)
		{
			var a = ar.Read(answerID);
			a.Values = avr.FindByAnswer(answerID);
			foreach (var v in a.Values) {
				v.Question = qr.Read(v.QuestionID);
				v.Option = or.Read(v.OptionID);
			}
			return a;
		}
	}
}
