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
		SqlProjectSurveyRepository psr = new SqlProjectSurveyRepository();
		SqlSurveyRepository sr = new SqlSurveyRepository();
		SqlProjectRoundUnitRepository prur = new SqlProjectRoundUnitRepository();
		
		public ProjectService()
		{
		}
		
		public Project ReadProject(int projectID)
		{
			var p = pr.Read(projectID);
			p.Rounds = prr.FindByProject(p.ProjectID);
			foreach (var r in p.Rounds) {
				r.Survey = sr.Read(r.SurveyID);
				r.Units = prur.FindByProjectRound(r.ProjectRoundID);
			}
			return p;
		}
		
		public ProjectRound ReadProjectRound(int projectRoundID)
		{
			var r = prr.Read(projectRoundID);
			r.Units = prur.FindByProjectRound(projectRoundID);
			foreach (var u in r.Units) {
				u.Survey = sr.Read(u.SurveyID);
			}
			return r;
		}
		
		public IList<Project> FindAllProjects()
		{
			var projects = pr.FindAll();
			return projects;
		}
	}
}
