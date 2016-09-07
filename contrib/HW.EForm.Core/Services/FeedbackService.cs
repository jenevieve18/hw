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
	public class FeedbackService
	{
		SqlFeedbackRepository feedbackRepo = new SqlFeedbackRepository();
		SqlFeedbackQuestionRepository feedbackQuestionRepo = new SqlFeedbackQuestionRepository();
		
		SqlQuestionRepository questionRepo = new SqlQuestionRepository();
		SqlQuestionOptionRepository questionOptionRepo = new SqlQuestionOptionRepository();
		SqlQuestionLangRepository questionLangRepo = new SqlQuestionLangRepository();
		
		SqlWeightedQuestionOptionRepository weightedQuestionOptionRepo = new SqlWeightedQuestionOptionRepository();
		
		SqlOptionRepository optionRepo = new SqlOptionRepository();
		SqlOptionComponentsRepository optionComponentsRepo = new SqlOptionComponentsRepository();
		SqlOptionComponentRepository optionComponentRepo = new SqlOptionComponentRepository();
		SqlOptionComponentLangRepository optionComponentLangRepo = new SqlOptionComponentLangRepository();
		
		SqlProjectRoundUnitRepository projectRoundUnitRepo = new SqlProjectRoundUnitRepository();
		
		SqlAnswerValueRepository answerValueRepo = new SqlAnswerValueRepository();
		
		public FeedbackService()
		{
		}
		
		public void SaveFeedback(Feedback f)
		{
			feedbackRepo.Save(f);
		}

        public void SaveFeedbackQuestion(FeedbackQuestion fq)
        {
            feedbackQuestionRepo.Save(fq);
        }
		
		public Feedback ReadFeedback(int feedbackID)
		{
			var f = feedbackRepo.Read(feedbackID);
			if (f != null) {
				f.Questions = feedbackQuestionRepo.FindByFeedback(feedbackID);
				foreach (var fq in f.Questions) {
					fq.Question = questionRepo.Read(fq.QuestionID);
					fq.Question.Languages = questionLangRepo.FindByQuestion(fq.QuestionID);
					fq.Question.Options = questionOptionRepo.FindByQuestion(fq.QuestionID);
					foreach (var qo in fq.Question.Options) {
						qo.Option = optionRepo.Read(qo.OptionID);
						qo.Option.Components = optionComponentsRepo.FindByOption(qo.OptionID);
						foreach (var oc in qo.Option.Components) {
							oc.OptionComponent = optionComponentRepo.Read(oc.OptionComponentID);
						}
					}
				}
			}
			return f;
		}
		
		public Feedback ReadFeedbackWithAnswers(int feedbackID, int projectRoundID, int[] projectRoundUnitIDs)
		{
			var f = feedbackRepo.Read(feedbackID);
			if (f != null) {
				f.Questions = feedbackQuestionRepo.FindByFeedback(f.FeedbackID);
				foreach (var fq in f.Questions) {
					fq.Question = questionRepo.Read(fq.QuestionID);
					fq.Question.WeightedQuestionOption = weightedQuestionOptionRepo.ReadByQuestion(fq.QuestionID);
					fq.Question.Languages = questionLangRepo.FindByQuestion(fq.QuestionID);
//					fq.Question.Options = questionOptionRepo.FindByQuestion(fq.QuestionID, projectRoundUnitIDs);
					fq.Question.Options = questionOptionRepo.FindByQuestion(fq.QuestionID);
					foreach (var qo in fq.Question.Options) {
						qo.Option = optionRepo.Read(qo.OptionID);
						qo.Option.Components = optionComponentsRepo.FindByOption(qo.OptionID);
						foreach (var oc in qo.Option.Components) {
							oc.OptionComponent = optionComponentRepo.Read(oc.OptionComponentID);
							oc.OptionComponent.Languages = optionComponentLangRepo.FindByOptionComponent(oc.OptionComponentID);
						}
					}
//					fq.ProjectRoundUnits = projectRoundUnitRepo.FindProjectRoundUnits(projectRoundUnitIDs);
					fq.Question.ProjectRoundUnits = projectRoundUnitRepo.FindProjectRoundUnits(projectRoundUnitIDs);
//					foreach (var pru in fq.ProjectRoundUnits) {
					foreach (var pru in fq.Question.ProjectRoundUnits) {
						pru.Options = fq.Question.Options;
						pru.AnswerValues = answerValueRepo.FindByQuestionOptionsAndUnit(fq.QuestionID, fq.Question.Options, projectRoundID, pru.ProjectRoundUnitID);
					}
				}
			}
			return f;
		}
		
		public IList<Feedback> FindAllFeedbacks()
		{
			var feedbacks = feedbackRepo.FindAll();
			foreach (var f in feedbacks) {
				f.Questions = feedbackQuestionRepo.FindByFeedback(f.FeedbackID);
				foreach (var fq in f.Questions) {
					fq.Question = questionRepo.Read(fq.QuestionID);
					fq.Question.Languages = questionLangRepo.FindByQuestion(fq.QuestionID);
					fq.Question.Options = questionOptionRepo.FindByQuestion(fq.QuestionID);
					foreach (var qo in fq.Question.Options) {
						qo.Option = optionRepo.Read(qo.OptionID);
						qo.Option.Components = optionComponentsRepo.FindByOption(qo.OptionID);
						foreach (var oc in qo.Option.Components) {
							oc.OptionComponent = optionComponentRepo.Read(oc.OptionComponentID);
						}
					}
				}
			}
			return feedbacks;
		}
		
		public IList<FeedbackQuestion> FindFeedbackQuestions(int feedbackID, int[] questionIDs)
		{
			var questions = feedbackQuestionRepo.FindByQuestions(feedbackID, questionIDs);
			foreach (var fq in questions) {
				fq.Question = questionRepo.Read(fq.QuestionID);
				fq.Question.Languages = questionLangRepo.FindByQuestion(fq.QuestionID);
			}
			return questions;
		}
	}
}
