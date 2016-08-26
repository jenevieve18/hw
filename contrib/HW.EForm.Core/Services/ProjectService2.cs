// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;

namespace HW.EForm.Core.Services
{
	public class ProjectService2
	{
		SqlProjectRepository projectRepo = new SqlProjectRepository();
		SqlProjectRoundRepository projectRoundRepo = new SqlProjectRoundRepository();
		SqlProjectRoundLangRepository projectRoundLangRepo = new SqlProjectRoundLangRepository();
		SqlProjectRoundUnitRepository projectRoundUnitRepo = new SqlProjectRoundUnitRepository();
		SqlProjectRoundUnitManagerRepository projectRoundUnitManagerRepo = new SqlProjectRoundUnitManagerRepository();
		SqlProjectRoundUserRepository projectRoundUserRepo = new SqlProjectRoundUserRepository();
		SqlProjectSurveyRepository projectSurveyRepo = new SqlProjectSurveyRepository();
		
		SqlFeedbackRepository feedbackRepo = new SqlFeedbackRepository();
		SqlFeedbackQuestionRepository feedbackQuestionRepo = new SqlFeedbackQuestionRepository();
		
		SqlSurveyRepository surveyRepo = new SqlSurveyRepository();
		SqlSurveyQuestionRepository surveyQuestionRepo = new SqlSurveyQuestionRepository();
		
		SqlQuestionRepository questionRepo = new SqlQuestionRepository();
		SqlQuestionOptionRepository questionOptionRepo = new SqlQuestionOptionRepository();
		SqlQuestionLangRepository questionLangRepo = new SqlQuestionLangRepository();
		
		SqlOptionRepository optionRepo = new SqlOptionRepository();
		SqlOptionComponentsRepository optionComponentsRepo = new SqlOptionComponentsRepository();
		SqlOptionComponentRepository optionComponentRepo = new SqlOptionComponentRepository();
		SqlOptionComponentLangRepository optionComponentLangRepo = new SqlOptionComponentLangRepository();
		
		SqlAnswerRepository answerRepo = new SqlAnswerRepository();
		SqlAnswerValueRepository answerValueRepo = new SqlAnswerValueRepository();
		
		public ProjectService2()
		{
		}
		
		public ProjectRoundUnit ReadProjectRoundUnit3(int projectRoundUnitID)
		{
			var pru = projectRoundUnitRepo.Read(projectRoundUnitID);
			var pr = projectRoundRepo.Read(pru.ProjectRoundID);
			var f = feedbackRepo.Read(pr.FeedbackID);
			f.Questions = feedbackQuestionRepo.FindByFeedback(f.FeedbackID);
			foreach (var fq in f.Questions) {
				fq.Question = questionRepo.Read(fq.QuestionID);
				fq.Question.Languages = questionLangRepo.FindByQuestion(fq.QuestionID);
				fq.Question.AnswerValues = answerValueRepo.FindByQuestionOptions(fq.QuestionID, fq.Question.Options, pru.ProjectRoundID, pru.ProjectRoundUnitID);
			}
			pr.Feedback = f;
			
			pru.ProjectRound = pr;
			return pru;
		}
		
//		public ProjectRoundUnit ReadProjectRoundUnit2(int projectRoundUnitID)
//		{
//			var pru = projectRoundUnitRepo.Read(projectRoundUnitID);
//			var pr = projectRoundRepo.Read(pru.ProjectRoundID);
//			if (pr != null){
//				pr.Feedback = ReadFeedback(pr.FeedbackID, pr.ProjectRoundID, projectRoundUnitID);
//			}
//			pru.ProjectRound = pr;
//			return pru;
//		}
//		
//		Feedback ReadFeedback(int feedbackID, int projectRoundID, int projectRoundUnitID)
//		{
//			var f = feedbackRepo.Read(feedbackID);
//			if (f != null) {
//				f.Questions = feedbackQuestionRepo.FindByFeedback(feedbackID);
//				foreach (var fq in f.Questions) {
//					fq.Question = questionRepo.Read(fq.QuestionID);
//					fq.Question.Languages = questionLangRepo.FindByQuestion(fq.QuestionID);
//					fq.Question.Options = questionOptionRepo.FindByQuestion(fq.QuestionID);
//					foreach (var qo in fq.Question.Options) {
//						qo.Option = optionRepo.Read(qo.OptionID);
//						qo.Option.Components = optionComponentsRepo.FindByOption(qo.OptionID);
//						foreach (var oc in qo.Option.Components) {
//							oc.Component = optionComponentRepo.Read(oc.OptionComponentID);
//							oc.Component.Languages = optionComponentLangRepo.FindByOptionComponent(oc.OptionComponentID);
//						}
//						qo.Option.AnswerValues = answerValueRepo.FindByQuestionOption(fq.QuestionID, qo.OptionID, projectRoundID, projectRoundUnitID);
//						foreach (var av in qo.Option.AnswerValues) {
//							av.Answer = answerRepo.Read(av.AnswerID);
//							av.Option = optionRepo.Read(av.OptionID);
//							av.Option.Components = optionComponentsRepo.FindByOption(av.OptionID);
//							foreach (var oc in av.Option.Components) {
//								oc.Component = optionComponentRepo.Read(oc.OptionComponentID);
//								oc.Component.Languages = optionComponentLangRepo.FindByOptionComponent(oc.OptionComponentID);
//							}
//							av.Answer.ProjectRoundUser = projectRoundUserRepo.Read(av.Answer.ProjectRoundUserID);
//						}
//					}
//				}
//			}
//			return f;
//		}
	}
}
