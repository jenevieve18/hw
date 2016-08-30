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
		
		SqlOptionRepository optionRepo = new SqlOptionRepository();
		SqlOptionComponentsRepository optionComponentsRepo = new SqlOptionComponentsRepository();
		SqlOptionComponentRepository optionComponentRepo = new SqlOptionComponentRepository();
		
		SqlProjectRoundUnitRepository projectRoundUnitRepo = new SqlProjectRoundUnitRepository();
		
		SqlAnswerValueRepository answerValueRepo = new SqlAnswerValueRepository();
		
		public FeedbackService()
		{
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
		
//		public Feedback ReadFeedback2(int feedbackID, IList<ProjectRoundUnit> units)
//		{
//			var f = feedbackRepo.Read(feedbackID);
//			f.Questions = feedbackQuestionRepo.FindByFeedback(f.FeedbackID);
//			foreach (var fq in f.Questions) {
//				fq.Question = questionRepo.Read(fq.QuestionID);
//				fq.Question.Languages = questionLangRepo.FindByQuestion(fq.QuestionID);
//				fq.Question.Options = questionOptionRepo.FindByQuestion(fq.QuestionID, units);
//				foreach (var qo in fq.Question.Options) {
//					qo.Option = optionRepo.Read(qo.OptionID);
//					qo.Option.Components = optionComponentsRepo.FindByOption(qo.OptionID);
//					foreach (var oc in qo.Option.Components) {
//						oc.OptionComponent = optionComponentRepo.Read(oc.OptionComponentID);
//					}
//				}
//			}
//			return f;
//		}
		
		public Feedback ReadFeedback2(int feedbackID, int projectRoundID, int[] projectRoundUnitIDs)
		{
			var f = feedbackRepo.Read(feedbackID);
			if (f != null) {
				f.Questions = feedbackQuestionRepo.FindByFeedback(f.FeedbackID);
				foreach (var fq in f.Questions) {
					fq.Question = questionRepo.Read(fq.QuestionID);
					fq.Question.Languages = questionLangRepo.FindByQuestion(fq.QuestionID);
					fq.Question.Options = questionOptionRepo.FindByQuestion(fq.QuestionID, projectRoundUnitIDs);
					foreach (var qo in fq.Question.Options) {
						qo.Option = optionRepo.Read(qo.OptionID);
						qo.Option.Components = optionComponentsRepo.FindByOption(qo.OptionID);
						foreach (var oc in qo.Option.Components) {
							oc.OptionComponent = optionComponentRepo.Read(oc.OptionComponentID);
						}
					}
					
					fq.ProjectRoundUnits = projectRoundUnitRepo.FindProjectRoundUnits(projectRoundUnitIDs);
					foreach (var pru in fq.ProjectRoundUnits) {
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
	}
}
