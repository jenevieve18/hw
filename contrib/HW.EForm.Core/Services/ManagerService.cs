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
		SqlManagerRepository managerRepo = new SqlManagerRepository();
		SqlManagerProjectRoundRepository managerProjectRoundRepo = new SqlManagerProjectRoundRepository();
		SqlManagerProjectRoundUnitRepository managerProjectRoundUnitRepo = new SqlManagerProjectRoundUnitRepository();
		
		SqlProjectRepository projectRepo = new SqlProjectRepository();
		SqlProjectRoundRepository projectRoundRepo = new SqlProjectRoundRepository();
		SqlProjectRoundUnitRepository projectRoundUnitRepo = new SqlProjectRoundUnitRepository();
		
		SqlReportRepository reportRepo = new SqlReportRepository();
		SqlReportPartRepository reportPartRepo = new SqlReportPartRepository();
		
		SqlSurveyRepository surveyRepo = new SqlSurveyRepository();
		SqlSurveyQuestionRepository surveyQuestionRepo = new SqlSurveyQuestionRepository();
		
		SqlQuestionRepository questionRepo = new SqlQuestionRepository();
		
		public ManagerService()
		{
		}
		
		public void SaveManager(Manager m)
		{
			managerRepo.Save(m);
		}
		
		public void UpdateManager(Manager m, int managerID)
		{
			managerRepo.Update(m, managerID);
		}
		
		public IList<Manager> FindAllManagers()
		{
			return managerRepo.FindAll();
		}
		
		public Manager ReadByEmailAndPassword(string name, string password)
		{
			return managerRepo.ReadByEmailAndPassword(name, password);
		}
		
		public Manager ReadManager(int managerID)
		{
			var m = managerRepo.Read(managerID);
			if (m != null) {
				m.ProjectRounds = managerProjectRoundRepo.FindByManager(managerID);
				foreach (var mpr in m.ProjectRounds) {
					mpr.ProjectRound = projectRoundRepo.Read(mpr.ProjectRoundID);
					mpr.ProjectRound.Project = projectRepo.Read(mpr.ProjectRound.ProjectID);
					mpr.Units = managerProjectRoundUnitRepo.FindByProjectRound(mpr.ProjectRoundID);
					foreach (var mpru in mpr.Units) {
//						mpru.ProjectRoundUnit = ReadProjectRoundUnit(mpru.ProjectRoundUnitID);
						mpru.ProjectRoundUnit = projectRoundUnitRepo.Read(mpru.ProjectRoundUnitID);
					}
				}
			}
			return m;
		}
		
//		ProjectRoundUnit ReadProjectRoundUnit(int projectRoundUnitID)
//		{
//			var pru = projectRoundUnitRepo.Read(projectRoundUnitID);
//			if (pru != null) {
//				pru.Survey = ReadSurvey(pru.SurveyID);
//				pru.Report = ReadReport(pru.ReportID);
//			}
//			return pru;
//		}
//		
//		Report ReadReport(int reportID)
//		{
//			var r = reportRepo.Read(reportID);
//			if (r != null) {
//				r.Parts = reportPartRepo.FindByReport(r.ReportID);
//			}
//			return r;
//		}
//		
//		Survey ReadSurvey(int surveyID)
//		{
//			var s = surveyRepo.Read(surveyID);
//			if (s != null) {
//				s.Questions = surveyQuestionRepo.FindBySurvey(surveyID);
//				foreach (var sq in s.Questions) {
//					sq.Question = ReadQuestion(sq.QuestionID);
//				}
//			}
//			return s;
//		}
//		
//		Question ReadQuestion(int questionID)
//		{
//			var q = questionRepo.Read(questionID);
//			return q;
//		}
	}
}
