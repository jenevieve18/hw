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
		SqlProjectRepository pr = new SqlProjectRepository();
		SqlProjectRoundRepository prr = new SqlProjectRoundRepository();
		SqlProjectRoundLangRepository prlr = new SqlProjectRoundLangRepository();
		SqlProjectRoundUnitRepository prur = new SqlProjectRoundUnitRepository();

		SqlProjectSurveyRepository psr = new SqlProjectSurveyRepository();
		SqlSurveyRepository sr = new SqlSurveyRepository();
		
		SqlReportRepository rr = new SqlReportRepository();
		SqlReportPartRepository rpr = new SqlReportPartRepository();
		
		SqlQuestionRepository qr = new SqlQuestionRepository();
		
		ReportService rs = new ReportService();
		
		public ProjectService()
		{
		}
		
		public Project ReadProject(int projectID, int managerID)
		{
			var p = pr.Read(projectID);
			p.Rounds = prr.FindByProjectAndManager(p.ProjectID, managerID);
			foreach (var r in p.Rounds) {
				r.Survey = sr.Read(r.SurveyID);
				r.Units = prur.FindByProjectRoundAndManager(r.ProjectRoundID, managerID);
				r.Languages = prlr.FindByProjectRound(r.ProjectRoundID);
			}
			return p;
		}
		
		public ProjectRound ReadProjectRound(int projectRoundID, int managerID)
		{
			var pr = prr.Read(projectRoundID);
			pr.Survey = sr.Read(pr.SurveyID);
			pr.Units = prur.FindByProjectRoundAndManager(projectRoundID, managerID);
			foreach (var u in pr.Units) {
				var s = sr.Read(u.SurveyID);
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
			var pru = prur.Read(projectRoundUnitID);
			pru.Report = ReadReport(pru.ReportID);
			return pru;
		}
		
		public Report ReadReport(int reportID)
		{
			var r = rr.Read(reportID);
			if (r != null) {
				r.Parts = rpr.FindByReport(reportID);
				foreach (var p in r.Parts) {
					p.Question = qr.Read(p.QuestionID);
				}
			}
			return r;
		}
		
		public IList<Project> FindByManager(int managerID)
		{
			var projects = pr.FindByManager(managerID);
			return projects;
		}
	}
}
