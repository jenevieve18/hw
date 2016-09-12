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
		SqlProjectRoundUnitManagerRepository projectRoundUnitManagerRepo = new SqlProjectRoundUnitManagerRepository();

		SqlAnswerValueRepository answerValueRepo = new SqlAnswerValueRepository();
		
//		IFeedbackRepository feedbackRepo;
//		IFeedbackQuestionRepository feedbackQuestionRepo;
//
//		IQuestionRepository questionRepo;
//		IQuestionOptionRepository questionOptionRepo;
//		IQuestionLangRepository questionLangRepo;
//
//		IWeightedQuestionOptionRepository weightedQuestionOptionRepo;
//
//		IOptionRepository optionRepo;
//		IOptionComponentsRepository optionComponentsRepo;
//		IOptionComponentRepository optionComponentRepo;
//		IOptionComponentLangRepository optionComponentLangRepo;
//
//		IProjectRoundUnitRepository projectRoundUnitRepo;
//
//		IAnswerValueRepository answerValueRepo;
//
		public FeedbackService()
		{
		}
		
//		public FeedbackService(IFeedbackRepository feedbackRepo,
//		                       IFeedbackQuestionRepository feedbackQuestionRepo,
//		                       IQuestionRepository questionRepo,
//		                       IQuestionOptionRepository questionOptionRepo,
//		                       IQuestionLangRepository questionLangRepo,
//		                       IWeightedQuestionOptionRepository weightedQuestionOptionRepo,
//		                       IOptionRepository optionRepo,
//		                       IOptionComponentsRepository optionComponentsRepo,
//		                       IOptionComponentRepository optionComponentRepo,
//		                       IOptionComponentLangRepository optionComponentLangRepo,
//		                       IProjectRoundUnitRepository projectRoundUnitRepo,
//		                       IAnswerValueRepository answerValueRepo)
//		{
//			this.feedbackRepo = feedbackRepo;
//			this.feedbackQuestionRepo = feedbackQuestionRepo;
//
//			this.questionRepo = questionRepo;
//			this.questionOptionRepo = questionOptionRepo;
//			this.questionLangRepo = questionLangRepo;
//
//			this.weightedQuestionOptionRepo = weightedQuestionOptionRepo;
//
//			this.optionRepo = optionRepo;
//			this.optionComponentsRepo = optionComponentsRepo;
//			this.optionComponentRepo = optionComponentRepo;
//			this.optionComponentLangRepo = optionComponentLangRepo;
//
//			this.projectRoundUnitRepo = projectRoundUnitRepo;
//
//			this.answerValueRepo = answerValueRepo;
//		}
//
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
		
		public Feedback ReadFeedbackWithAnswers(int feedbackID, int projectRoundID, int[] projectRoundUnitIDs, int langID)
		{
			var f = feedbackRepo.Read(feedbackID);
			if (f != null) {
				f.Questions = feedbackQuestionRepo.FindByFeedback(f.FeedbackID);
				foreach (var fq in f.Questions) {
					fq.Question = ReadQuestion(fq.QuestionID, fq.OptionID, projectRoundID, projectRoundUnitIDs, langID);
				}
			}
			return f;
		}
		
		public FeedbackQuestion ReadFeedbackQuestion(int feedbackQuestionID, int projectRoundID, int[] projectRoundUnitIDs, int langID)
		{
			var fq = feedbackQuestionRepo.Read(feedbackQuestionID);
			fq.Question = ReadQuestion(fq.QuestionID, fq.OptionID, projectRoundID, projectRoundUnitIDs, langID);
			return fq;
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
		
		Question ReadQuestion(int questionID, int optionID, int projectRoundID, int[] projectRoundUnitIDs, int langID)
		{
			var q = questionRepo.Read(questionID);
			q.Languages = questionLangRepo.FindByQuestion(questionID);
			q.SelectedQuestionLangID = langID;
			q.Options = FindQuestionOptions(questionID, optionID, langID);
			q.ProjectRoundUnits = projectRoundUnitRepo.FindProjectRoundUnits(projectRoundUnitIDs);
			foreach (var pru in q.ProjectRoundUnits) {
//				pru.Managers = projectRoundUnitManagerRepo.FindByProjectRoundUnit(pru.ProjectRoundUnitID);
				pru.Options = FindQuestionOptions(questionID, optionID, langID);
				pru.AnswerValues = answerValueRepo.FindByQuestionOptionsAndUnit(questionID, q.Options, projectRoundID, pru.ProjectRoundUnitID);
			}
			return q;
		}
		
		IList<QuestionOption> FindQuestionOptions(int questionID, int optionID, int langID)
		{
			var questionOptions = questionOptionRepo.FindByQuestionAndOption(questionID, optionID);
			foreach (var qo in questionOptions) {
				qo.Option = optionRepo.Read(qo.OptionID);
				qo.WeightedQuestionOption = weightedQuestionOptionRepo.ReadByQuestionAndOption(questionID, optionID);
				qo.Option.Components = optionComponentsRepo.FindByOption(qo.OptionID);
				foreach (var oc in qo.Option.Components) {
					oc.OptionComponent = optionComponentRepo.Read(oc.OptionComponentID);
					oc.OptionComponent.Languages = optionComponentLangRepo.FindByOptionComponent(oc.OptionComponentID);
					oc.OptionComponent.SelectedOptionComponentLangID = langID;
				}
			}
			return questionOptions;
		}
	}
}
