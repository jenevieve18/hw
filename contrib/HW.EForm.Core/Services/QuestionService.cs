// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;

namespace HW.EForm.Core.Services
{
	public class QuestionService
	{
		SqlQuestionRepository qr = new SqlQuestionRepository();
		SqlQuestionLangRepository qlr = new SqlQuestionLangRepository();
		SqlQuestionOptionRepository qor = new SqlQuestionOptionRepository();
		SqlOptionRepository or = new SqlOptionRepository();
		
		public QuestionService()
		{
		}
		
		public Question ReadQuestion(int questionID)
		{
			var q = qr.Read(questionID);
			q.Options = qor.FindByQuestion(questionID);
			foreach (var o in q.Options) {
				o.Option = or.Read(o.OptionID);
			}
			return q;
		}
	}
}
