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
		SqlProjectRepository spr = new SqlProjectRepository();
		SqlProjectRoundRepository sprr = new SqlProjectRoundRepository();
		SqlProjectRoundLangRepository sprlr = new SqlProjectRoundLangRepository();
		SqlProjectRoundUnitRepository sprur = new SqlProjectRoundUnitRepository();
		SqlProjectRoundUnitManagerRepository sprumr = new SqlProjectRoundUnitManagerRepository();
		SqlProjectRoundUserRepository sprur2 = new SqlProjectRoundUserRepository();
		SqlProjectSurveyRepository spsr = new SqlProjectSurveyRepository();
		
		SqlFeedbackRepository sfr = new SqlFeedbackRepository();
		SqlFeedbackQuestionRepository sfqr = new SqlFeedbackQuestionRepository();
		
		SqlSurveyRepository ssr = new SqlSurveyRepository();
		SqlSurveyQuestionRepository ssqr = new SqlSurveyQuestionRepository();
		
		SqlReportRepository srr = new SqlReportRepository();
		SqlReportPartRepository srpr = new SqlReportPartRepository();
		
		SqlQuestionRepository sqr = new SqlQuestionRepository();
		SqlQuestionOptionRepository sqor = new SqlQuestionOptionRepository();
		SqlQuestionLangRepository sqlr = new SqlQuestionLangRepository();
		
		SqlOptionRepository sor = new SqlOptionRepository();
		SqlOptionComponentsRepository socsr = new SqlOptionComponentsRepository();
		SqlOptionComponentRepository socr = new SqlOptionComponentRepository();
		SqlOptionComponentLangRepository soclr = new SqlOptionComponentLangRepository();
		
		SqlAnswerRepository sar = new SqlAnswerRepository();
		SqlAnswerValueRepository savr = new SqlAnswerValueRepository();
		
		public ProjectService()
		{
		}
		
		public IList<Project> FindAllProjects()
		{
			return spr.FindAll();
		}
		
		public Project ReadProject(int projectID)
		{
			var p = spr.Read(projectID);
			if (p != null) {
				p.Surveys = spsr.FindByProject(projectID);
				foreach (var s in p.Surveys) {
					s.Survey = ssr.Read(s.SurveyID);
				}
			}
			return p;
		}
		
		public Project ReadProject(int projectID, int managerID)
		{
			var p = spr.Read(projectID);
			if (p != null) {
				p.Rounds = sprr.FindByProjectAndManager(p.ProjectID, managerID);
				foreach (var pr in p.Rounds) {
					pr.Survey = ssr.Read(pr.SurveyID);
					pr.Units = sprur.FindByProjectRoundAndManager(pr.ProjectRoundID, managerID);
					pr.Languages = sprlr.FindByProjectRound(pr.ProjectRoundID);
				}
				p.Surveys = spsr.FindByProject(projectID);
				foreach (var ps in p.Surveys) {
					ps.Survey = ssr.Read(ps.SurveyID);
				}
			}
			return p;
		}
		
		public ProjectRound ReadProjectRound(int projectRoundID, int managerID)
		{
			var pr = sprr.Read(projectRoundID);
			if (pr != null) {
				pr.Survey = ReadSurvey(pr.SurveyID);
				pr.Units = sprur.FindByProjectRoundAndManager(projectRoundID, managerID);
				foreach (var pru in pr.Units) {
					pru.ProjectRound = pr;
					pru.Survey = ReadSurvey(pru.SurveyID);
					pru.Managers = sprumr.FindByProjectRoundUnit(pru.ProjectRoundUnitID);
					foreach (var prum in pru.Managers) {
						prum.User = sprur2.Read(prum.ProjectRoundUserID);
					}
				}
			}
			return pr;
		}
		
		public ProjectRound ReadProjectRound2(int projectRoundID, int projectRoundUnitID)
		{
			var pr = sprr.Read(projectRoundID);
			if (pr != null) {
				pr.Survey = ReadSurvey(pr.SurveyID);
				pr.Units = sprur.FindByProjectRound(projectRoundID);
				pr.Feedback = ReadFeedback(pr.FeedbackID, projectRoundID, projectRoundUnitID);
				foreach (var pru in pr.Units) {
					pru.Survey = ReadSurvey(pru.SurveyID);
					pru.Managers = sprumr.FindByProjectRoundUnit(pru.ProjectRoundUnitID);
					foreach (var prum in pru.Managers) {
						prum.User = sprur2.Read(prum.ProjectRoundUserID);
					}
				}
			}
			return pr;
		}
		
		public Feedback ReadFeedback(int feedbackID, int projectRoundID, int projectRoundUnitID)
		{
			var f = sfr.Read(feedbackID);
			if (f != null) {
				f.Questions = sfqr.FindByFeedback(feedbackID);
				foreach (var fq in f.Questions) {
					fq.Question = ReadQuestion2(fq.QuestionID, projectRoundID, projectRoundUnitID);
				}
			}
			return f;
		}
		
		public ProjectRoundUnit ReadProjectRoundUnit(int projectRoundUnitID)
		{
			var pru = sprur.Read(projectRoundUnitID);
			if (pru != null) {
				pru.ProjectRound = ReadProjectRound2(pru.ProjectRoundID, projectRoundUnitID);
				pru.Survey = ReadSurvey(pru.SurveyID);
				pru.Managers = sprumr.FindByProjectRoundUnit(pru.ProjectRoundUnitID);
				foreach (var prum in pru.Managers) {
					prum.User = sprur2.Read(prum.ProjectRoundUserID);
				}
			}
			return pru;
		}
		
		public ProjectRoundUnit ReadProjectRoundUnit2(int projectRoundUnitID)
		{
			var pru = sprur.Read(projectRoundUnitID);
			if (pru != null) {
				var pr = sprr.Read(pru.ProjectRoundID);
				if (pr != null) {
					pr.Survey = ssr.Read(pr.SurveyID);
					var f = sfr.Read(pr.FeedbackID);
					if (f != null) {
						f.Questions = sfqr.FindByFeedback(pr.FeedbackID);
						foreach (var fq in f.Questions) {
							var q = sqr.Read(fq.QuestionID);
							if (q != null) {
								q.Languages = sqlr.FindByQuestion(q.QuestionID);
								q.Options = sqor.FindByQuestion(q.QuestionID);
								foreach (var qo in q.Options) {
									var o = sor.Read(qo.OptionID);
									o.AnswerValues = savr.FindByQuestionOption(q.QuestionID, o.OptionID, pr.ProjectRoundID, projectRoundUnitID);
									if (o != null) {
										o.Components = socsr.FindByOption(o.OptionID);
										foreach (var ocs in o.Components) {
											var oc = socr.Read(ocs.OptionComponentID);
											if (oc != null) {
												oc.Languages = soclr.FindByOptionComponent(oc.OptionComponentID);
												ocs.OptionComponent = oc;
											}
										}
										qo.Option = o;
									}
								}
//								q.AnswerValues = savr.FindByQuestionOptions(q.QuestionID, q.Options, pr.ProjectRoundID, projectRoundUnitID);
								fq.Question = q;
							}
						}
						pr.Feedback = f;
						pru.ProjectRound = pr;
						pru.Survey = ssr.Read(pru.SurveyID);
					}
				}
			}
			return pru;
		}
		
		public Survey ReadSurvey(int surveyID)
		{
			var s = ssr.Read(surveyID);
			if (s != null) {
				s.Questions = ssqr.FindBySurvey(surveyID);
				foreach (var sq in s.Questions) {
					sq.Question = ReadQuestion(sq.QuestionID);
				}
			}
			return s;
		}
		
		public Question ReadQuestion(int questionID)
		{
			var q = sqr.Read(questionID);
			if (q != null) {
				q.Options = sqor.FindByQuestion(questionID);
				foreach (var qo in q.Options) {
					qo.Option = ReadOption(qo.OptionID);
				}
			}
			return q;
		}
		
		public Question ReadQuestion2(int questionID, int projectRoundID, int projectRoundUnitID)
		{
			var q = sqr.Read(questionID);
			if (q != null) {
				q.Options = sqor.FindByQuestion(questionID);
//				q.AnswerValues = savr.FindByQuestionOptions(questionID, q.Options, projectRoundID, projectRoundUnitID);
			}
			return q;
		}
		
		Option ReadOption(int optionID)
		{
			var o = sor.Read(optionID);
			if (o != null) {
				o.Components = socsr.FindByOption(o.OptionID);
				foreach (var oc in o.Components) {
					oc.OptionComponent = ReadOptionComponent(oc.OptionComponentID);
				}
			}
			return o;
		}
		
		OptionComponent ReadOptionComponent(int optionComponentID)
		{
			var oc = socr.Read(optionComponentID);
			if (oc != null) {
//				oc.CurrentLanguage = soclr.ReadByLang(oc.OptionComponentID, 1);
				oc.Languages = soclr.FindByOptionComponent(oc.OptionComponentID);
			}
			return oc;
		}
		
		public Report ReadReport(int reportID)
		{
			var r = srr.Read(reportID);
			if (r != null) {
				r.Parts = srpr.FindByReport(reportID);
				foreach (var p in r.Parts) {
					p.Question = sqr.Read(p.QuestionID);
				}
			}
			return r;
		}
		
		public IList<Project> FindByManager(int managerID)
		{
			var projects = spr.FindByManager(managerID);
			return projects;
		}
	}
}
