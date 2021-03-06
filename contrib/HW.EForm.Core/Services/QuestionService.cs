﻿// <file>
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
//		SqlQuestionRepository questionRepo = new SqlQuestionRepository();
//		SqlQuestionLangRepository questionLangRepo = new SqlQuestionLangRepository();
//		SqlQuestionOptionRepository questionOptionRepo = new SqlQuestionOptionRepository();
//		SqlQuestionContainerRepository questionContainerRepo = new SqlQuestionContainerRepository();
//
//		SqlOptionRepository optionRepo = new SqlOptionRepository();
//		SqlOptionComponentsRepository optionComponentsRepo = new SqlOptionComponentsRepository();
//		SqlOptionComponentRepository optionComponentRepo = new SqlOptionComponentRepository();
//		SqlOptionComponentLangRepository optionComponentLangRepo = new SqlOptionComponentLangRepository();
//
//		SqlOptionContainerRepository optionContainerRepo = new SqlOptionContainerRepository();
//
//		SqlWeightedQuestionOptionRepository weightedQuestionOptionRepo = new SqlWeightedQuestionOptionRepository();
//
//		SqlProjectRoundUnitRepository projectRoundUnitRepo = new SqlProjectRoundUnitRepository();
//
//		SqlAnswerValueRepository answerValueRepo = new SqlAnswerValueRepository();
		
		IQuestionRepository questionRepo;
		IQuestionLangRepository questionLangRepo;
		IQuestionOptionRepository questionOptionRepo;
		IQuestionContainerRepository questionContainerRepo;
		
		IOptionRepository optionRepo;
		IOptionComponentsRepository optionComponentsRepo;
		IOptionComponentRepository optionComponentRepo;
		IOptionComponentLangRepository optionComponentLangRepo;
		
//		IOptionContainerRepository optionContainerRepo;
		
		IWeightedQuestionOptionRepository weightedQuestionOptionRepo;
		
		IProjectRoundUnitRepository projectRoundUnitRepo;
		
		IAnswerValueRepository answerValueRepo;
		
		public QuestionService()
		{
		}
		
		public QuestionService(IQuestionRepository questionRepo,
		                       IQuestionOptionRepository questionOptionRepo,
		                       IOptionRepository optionRepo,
		                       IOptionComponentsRepository optionComponentsRepo,
		                       IOptionComponentRepository optionComponentRepo)
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
//						oc.OptionComponent.Languages = optionComponentLangRepo.FindByOptionComponent(oc.OptionComponentID);
//					}
//				}
				q.Options = FindQuestionOptions(q.QuestionID);
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
//			q.Options = questionOptionRepo.FindByQuestion(questionID);
//			foreach (var o in q.Options) {
//				o.Option = optionRepo.Read(o.OptionID);
//				o.Option.Components = optionComponentsRepo.FindByOption(o.OptionID);
//				foreach (var oc in o.Option.Components) {
//					oc.OptionComponent = optionComponentRepo.Read(oc.OptionComponentID);
//					oc.OptionComponent.Languages = optionComponentLangRepo.FindByOptionComponent(oc.OptionComponentID);
//				}
//			}
			q.Options = FindQuestionOptions(questionID);
			return q;
		}
		
		public Question ReadQuestionWithAnswers(int questionID, int projectRoundID, int[] projectRoundUnitIDs)
		{
			var q = questionRepo.Read(questionID);
			q.Languages = questionLangRepo.FindByQuestion(questionID);
			q.Options = FindQuestionOptions(questionID);
			q.ProjectRoundUnits = projectRoundUnitRepo.FindProjectRoundUnits(projectRoundUnitIDs);
			foreach (var pru in q.ProjectRoundUnits) {
				pru.Options = FindQuestionOptions(questionID);
				pru.AnswerValues = answerValueRepo.FindByQuestionOptionsAndUnit(q.QuestionID, q.Options, projectRoundID, pru.ProjectRoundUnitID);
			}
			return q;
		}
		
		public IList<Question> FindQuestion(int[] questionIDs, int projectRoundID, int[] projectRoundUnitIDs)
		{
			var questions = new List<Question>();
			foreach (var questionID in questionIDs) {
				var q = questionRepo.Read(questionID);
				q.Languages = questionLangRepo.FindByQuestion(questionID);
//				q.Options = questionOptionRepo.FindByQuestion(questionID);
//				foreach (var o in q.Options) {
//					o.Option = optionRepo.Read(o.OptionID);
//					o.Option.Components = optionComponentsRepo.FindByOption(o.OptionID);
//					foreach (var oc in o.Option.Components) {
//						oc.OptionComponent = optionComponentRepo.Read(oc.OptionComponentID);
//						oc.OptionComponent.Languages = optionComponentLangRepo.FindByOptionComponent(oc.OptionComponentID);
//					}
//				}
				q.Options = FindQuestionOptions(questionID);
				q.ProjectRoundUnits = projectRoundUnitRepo.FindProjectRoundUnits(projectRoundUnitIDs);
				foreach (var pru in q.ProjectRoundUnits) {
					pru.Options = q.Options;
					pru.AnswerValues = answerValueRepo.FindByQuestionOptionsAndUnit(q.QuestionID, q.Options, projectRoundID, pru.ProjectRoundUnitID);
				}
				questions.Add(q);
			}
			return questions;
		}
		
		IList<QuestionOption> FindQuestionOptions(int questionID)
		{
			var questionOptions = questionOptionRepo.FindByQuestion(questionID);
			foreach (var qo in questionOptions) {
				qo.Option = optionRepo.Read(qo.OptionID);
				qo.WeightedQuestionOption = weightedQuestionOptionRepo.ReadByQuestionAndOption(qo.QuestionID, qo.OptionID);
				qo.Option.Components = optionComponentsRepo.FindByOption(qo.OptionID);
				foreach (var oc in qo.Option.Components) {
					oc.OptionComponent = optionComponentRepo.Read(oc.OptionComponentID);
					oc.OptionComponent.Languages = optionComponentLangRepo.FindByOptionComponent(oc.OptionComponentID);
				}
			}
			return questionOptions;
		}
	}
}
