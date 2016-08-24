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
	public class ManagerService
	{
		SqlManagerRepository smr = new SqlManagerRepository();
		SqlManagerProjectRoundRepository smprr = new SqlManagerProjectRoundRepository();
		SqlManagerProjectRoundUnitRepository smprur = new SqlManagerProjectRoundUnitRepository();
		
		SqlProjectRepository spr = new SqlProjectRepository();
		SqlProjectRoundRepository sprr = new SqlProjectRoundRepository();
		SqlProjectRoundUnitRepository sprur = new SqlProjectRoundUnitRepository();
		
		SqlReportRepository srr = new SqlReportRepository();
		SqlReportPartRepository srpr = new SqlReportPartRepository();
		
		SqlSurveyRepository ssr = new SqlSurveyRepository();
		SqlSurveyQuestionRepository ssqr = new SqlSurveyQuestionRepository();
		
		SqlQuestionRepository sqr = new SqlQuestionRepository();
		
		public ManagerService()
		{
		}
		
		public void UpdateManager(Manager m, int id)
		{
			smr.Update(m, id);
		}
		
		public IList<Manager> FindAllManagers()
		{
			return smr.FindAll();
		}
		
		public Manager ReadByEmailAndPassword(string name, string password)
		{
			return smr.ReadByEmailAndPassword(name, password);
		}
		
		public Manager ReadManager(int managerID)
		{
			var m = smr.Read(managerID);
			m.ProjectRounds = smprr.FindByManager(managerID);
			foreach (var mpr in m.ProjectRounds) {
				mpr.ProjectRound = ReadProjectRound(mpr.ProjectRoundID);
				mpr.Units = smprur.FindByProjectRound(mpr.ProjectRoundID);
				foreach (var mpru in mpr.Units) {
					mpru.ProjectRoundUnit = ReadProjectRoundUnit(mpru.ProjectRoundUnitID);
				}
			}
			return m;
		}
		
		public ProjectRound ReadProjectRound(int projectRoundID)
		{
			var pr = sprr.Read(projectRoundID);
			pr.Project = spr.Read(pr.ProjectID);
			pr.Survey = ReadSurvey(pr.SurveyID);
			return pr;
		}
		
		public ProjectRoundUnit ReadProjectRoundUnit(int projectRoundUnitID)
		{
			var pru = sprur.Read(projectRoundUnitID);
			pru.Survey = ReadSurvey(pru.SurveyID);
			pru.Report = ReadReport(pru.ReportID);
			return pru;
		}
		
		public Report ReadReport(int reportID)
		{
			var r = srr.Read(reportID);
			r.Parts = srpr.FindByReport(r.ReportID);
			return r;
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
		
		public Question ReadQuestion(int x)
		{
			var q = sqr.Read(x);
			return q;
		}
	}
}
