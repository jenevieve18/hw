/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 8/11/2016
 * Time: 12:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Core.Services
{
	public class ProjectService
	{
		SqlProjectRepository projectRepo = new SqlProjectRepository();
		SqlProjectRoundRepository projectRoundRepo = new SqlProjectRoundRepository();
		SqlProjectRoundUnitRepository projectRoundUnitRepo = new SqlProjectRoundUnitRepository();
		SqlProjectRoundUserRepository projectRoundUserRepo = new SqlProjectRoundUserRepository();
		
		public IList<Project> FindAllProjects()
		{
			return projectRepo.FindAll();
		}
		
		public ProjectRoundUnit ReadProjectRoundUnit(int projectRoundUnitID)
		{
			var pru = projectRoundUnitRepo.Read(projectRoundUnitID);
			if (pru != null) {
				pru.ProjectRound = projectRoundRepo.Read(pru.ProjectRoundID);
				pru.ProjectRoundUsers = projectRoundUserRepo.FindByProjectRoundUnit(projectRoundUnitID);
			}
			return pru;
		}
	}
}
