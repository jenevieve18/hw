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

		SqlProjectSurveyRepository spsr = new SqlProjectSurveyRepository();
		SqlSurveyRepository ssr = new SqlSurveyRepository();
		
		SqlReportRepository srr = new SqlReportRepository();
		SqlReportPartRepository srpr = new SqlReportPartRepository();
		
		SqlQuestionRepository sqr = new SqlQuestionRepository();
		
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
			p.Surveys = spsr.FindByProject(projectID);
			foreach (var s in p.Surveys) {
				s.Survey = ssr.Read(s.SurveyID);
			}
			return p;
		}
		
		public Project ReadProject(int projectID, int managerID)
		{
			var p = spr.Read(projectID);
			p.Rounds = sprr.FindByProjectAndManager(p.ProjectID, managerID);
			foreach (var r in p.Rounds) {
				r.Survey = ssr.Read(r.SurveyID);
				r.Units = sprur.FindByProjectRoundAndManager(r.ProjectRoundID, managerID);
				r.Languages = sprlr.FindByProjectRound(r.ProjectRoundID);
			}
			p.Surveys = spsr.FindByProject(projectID);
			foreach (var s in p.Surveys) {
				s.Survey = ssr.Read(s.SurveyID);
			}
			return p;
		}
		
		public ProjectRound ReadProjectRound(int projectRoundID, int managerID)
		{
			var pr = sprr.Read(projectRoundID);
			pr.Survey = ssr.Read(pr.SurveyID);
			pr.Units = sprur.FindByProjectRoundAndManager(projectRoundID, managerID);
			foreach (var u in pr.Units) {
				var s = ssr.Read(u.SurveyID);
				if (s == null) {
					s = new Survey(pr.Survey);
					s.Internal += " *";
				}
				u.Survey = s;
			}
			return pr;
		}
		
		public ProjectRoundUnit ReadProjectRoundUnit(int projectRoundUnitID)
		{
			var pru = sprur.Read(projectRoundUnitID);
			pru.Report = ReadReport(pru.ReportID);
			return pru;
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
