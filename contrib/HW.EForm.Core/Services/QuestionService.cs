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
	public class QuestionService
	{
		SqlQuestionRepository sqr = new SqlQuestionRepository();
		SqlQuestionLangRepository sqlr = new SqlQuestionLangRepository();
		SqlQuestionOptionRepository sqor = new SqlQuestionOptionRepository();

		SqlQuestionContainerRepository sqcr = new SqlQuestionContainerRepository();

		SqlOptionRepository sor = new SqlOptionRepository();
		SqlOptionComponentRepository socr = new SqlOptionComponentRepository();
		
		public QuestionService()
		{
		}

        public IList<OptionComponent> FindAllComponents()
        {
            return socr.FindAll();
        }

        public IList<Question> FindAllQuestions()
        {
            return sqr.FindAll();
        }

        public IList<Option> FindAllOptions()
        {
            return sor.FindAll();
        }

        public IList<QuestionContainer> FindAllContainers()
        {
            return sqcr.FindAll();
        }
		
		public Question ReadQuestion(int questionID)
		{
			var q = sqr.Read(questionID);
			q.Options = sqor.FindByQuestion(questionID);
			foreach (var o in q.Options) {
				o.Option = sor.Read(o.OptionID);
			}
			return q;
		}
		
		public Option ReadOption(int optionID)
		{
			var o = sor.Read(optionID);
			return o;
		}
	}
}
