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
	public class ProjectService
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
		
		public ProjectService()
		{
		}
		
		public void SaveProject(Project p)
		{
			projectRepo.Save(p);
		}
		
		public Project ReadProject(int projectID)
		{
			var p = projectRepo.Read(projectID);
			p.Rounds = projectRoundRepo.FindByProject(projectID);
			foreach (var pr in p.Rounds) {
				pr.Units = projectRoundUnitRepo.FindByProjectRound(pr.ProjectRoundID);
			}
			return p;
		}
		
		public Project ReadProject(int projectID, int managerID)
		{
			var p = projectRepo.Read(projectID, managerID);
			p.Rounds = projectRoundRepo.FindByProject(projectID, managerID);
			foreach (var pr in p.Rounds) {
				pr.Survey = surveyRepo.Read(pr.SurveyID);
				pr.Languages = projectRoundLangRepo.FindByProjectRound(pr.ProjectRoundID);
				pr.Units = projectRoundUnitRepo.FindByProjectRound(pr.ProjectRoundID, managerID);
			}
			return p;
		}
		
		public ProjectRound ReadProjectRound(int projectRoundID)
		{
			var pr = projectRoundRepo.Read(projectRoundID);
			pr.Units = projectRoundUnitRepo.FindByProjectRound(projectRoundID);
			return pr;
		}
		
		public ProjectRound ReadProjectRound(int projectRoundID, int managerID)
		{
			var pr = projectRoundRepo.Read(projectRoundID, managerID);
			pr.Survey = surveyRepo.Read(pr.SurveyID);
			pr.Units = projectRoundUnitRepo.FindByProjectRound(projectRoundID, managerID);
			foreach (var pru in pr.Units) {
				pru.ProjectRound = pr;
				pru.Survey = surveyRepo.Read(pru.SurveyID);
			}
			return pr;
		}
		
		public ProjectRoundUnit ReadProjectRoundUnit(int projectRoundUnitID)
		{
			var pru = projectRoundUnitRepo.Read(projectRoundUnitID);
			pru.ProjectRound = lalala(pru.ProjectRoundID, pru.ProjectRoundUnitID);
			return pru;
		}
		
		public IList<Project> FindByManager(int managerID)
		{
			return projectRepo.FindByManager(managerID);
		}
		
		public IList<ProjectRoundUnit> FindProjectRoundUnits(int[] projectRoundUnitIDs)
		{
			var units = projectRoundUnitRepo.FindProjectRoundUnits(projectRoundUnitIDs);
			foreach (var pru in units) {
				pru.ProjectRound = lalala(pru.ProjectRoundID, pru.ProjectRoundUnitID);
			}
			return units;
		}
		
		// FIXME: Refer to feedback service
		ProjectRound lalala(int projectRoundID, int projectRoundUnitID)
		{
			var pr = projectRoundRepo.Read(projectRoundID);
			pr.Feedback = lololo(pr.FeedbackID, projectRoundUnitID);
			return pr;
		}
		
		Feedback lololo(int feedbackID, int projectRoundUnitID)
		{
			var f = feedbackRepo.Read(feedbackID);
			f.Questions = feedbackQuestionRepo.FindByFeedback(f.FeedbackID);
			foreach (var fq in f.Questions) {
				fq.Question = questionRepo.Read(fq.QuestionID);
				fq.Question.Languages = questionLangRepo.FindByQuestion(fq.QuestionID);
//				fq.Question.Options = questionOptionRepo.FindByQuestion(fq.QuestionID, projectRoundUnitID);
				foreach (var qo in fq.Question.Options) {
					qo.Option = optionRepo.Read(qo.OptionID);
					qo.Option.Components = optionComponentsRepo.FindByOption(qo.OptionID);
					foreach (var oc in qo.Option.Components) {
						oc.OptionComponent = optionComponentRepo.Read(oc.OptionComponentID);
					}
				}
			}
			return f;
		}
	}
}
