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
		SqlQuestionRepository questionRepo = new SqlQuestionRepository();
		SqlQuestionLangRepository questionLangRepo = new SqlQuestionLangRepository();
		SqlQuestionOptionRepository questionOptionRepo = new SqlQuestionOptionRepository();
		SqlQuestionContainerRepository questionContainerRepo = new SqlQuestionContainerRepository();

		SqlOptionRepository optionRepo = new SqlOptionRepository();
		SqlOptionComponentsRepository optionComponentsRepo = new SqlOptionComponentsRepository();
		SqlOptionComponentRepository optionComponentRepo = new SqlOptionComponentRepository();
		
		SqlWeightedQuestionOptionRepository weightedQuestionOptionRepo = new SqlWeightedQuestionOptionRepository();
		
		public QuestionService()
		{
		}
		
		public Option ReadOption(int optionID)
		{
			var o = optionRepo.Read(optionID);
			return o;
		}
		
		public WeightedQuestionOption ReadWeightedQuestionOption(int questionID)
		{
			return weightedQuestionOptionRepo.ReadByQuestion(questionID);
		}

		public IList<OptionComponent> FindAllComponents()
		{
			return optionComponentRepo.FindAll();
		}

		public IList<Question> FindAllQuestions()
		{
			var questions = questionRepo.FindAll();
			foreach (var q in questions) {
				q.Languages = questionLangRepo.FindByQuestion(q.QuestionID);
//				q.Options = questionOptionRepo.FindByQuestion(q.QuestionID);
//				foreach (var o in q.Options) {
//					o.Option = optionRepo.Read(o.OptionID);
//					o.Option.Components = optionComponentsRepo.FindByOption(o.OptionID);
//					foreach (var oc in o.Option.Components) {
//						oc.OptionComponent = optionComponentRepo.Read(oc.OptionComponentID);
//					}
//				}
			}
			return questions;
		}

		public IList<Option> FindAllOptions()
		{
			return optionRepo.FindAll();
		}

		public IList<QuestionContainer> FindAllContainers()
		{
			return questionContainerRepo.FindAll();
		}
		
		public Question ReadQuestion(int questionID)
		{
			var q = questionRepo.Read(questionID);
			q.Languages = questionLangRepo.FindByQuestion(questionID);
			q.Options = questionOptionRepo.FindByQuestion(questionID);
			foreach (var o in q.Options) {
				o.Option = optionRepo.Read(o.OptionID);
				o.Option.Components = optionComponentsRepo.FindByOption(o.OptionID);
				foreach (var oc in o.Option.Components) {
					oc.OptionComponent = optionComponentRepo.Read(oc.OptionComponentID);
				}
			}
			return q;
		}
	}
}
