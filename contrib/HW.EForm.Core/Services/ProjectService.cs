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
		
		public ProjectService()
		{
		}
		
		public Project ReadProjectAndManager(int projectID, int managerID)
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
			var r = prr.Read(projectRoundID);
			r.Survey = sr.Read(r.SurveyID);
			r.Units = prur.FindByProjectRoundAndManager(projectRoundID, managerID);
			foreach (var u in r.Units) {
				var s = sr.Read(u.SurveyID);
				if (s == null) {
					s = new Survey(r.Survey);
					s.Internal += " *";
				}
				u.Survey = s;
			}
			return r;
		}
		
		public ProjectRoundUnit ReadProjectRoundUnit(int projectRoundUnitID)
		{
			var u = prur.Read(projectRoundUnitID);
			return u;
		}
		
		public IList<Project> FindByManager(int managerID)
		{
			var projects = pr.FindByManager(managerID);
			return projects;
		}
	}
}
