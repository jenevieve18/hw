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
//		IProjectRepository projectRepo;
//		IProjectRoundUnitRepository projectRoundUnitRepo;
//		IProjectRoundUserRepository projectRoundUserRepo;
//		
//		public ProjectService(IProjectRepository projectRepo, 
//		                      IProjectRoundUnitRepository projectRoundUnitRepo,
//		                     IProjectRoundUserRepository projectRoundUserRepo)
//		{
//			this.projectRepo = projectRepo;
//			this.projectRoundUnitRepo = projectRoundUnitRepo;
//			this.projectRoundUserRepo = projectRoundUserRepo;
//		}
		
		SqlProjectRepository projectRepo = new SqlProjectRepository();
		SqlProjectRoundRepository projectRoundRepo = new SqlProjectRoundRepository();
		SqlProjectRoundUnitRepository projectRoundUnitRepo = new SqlProjectRoundUnitRepository();
		SqlProjectRoundUserRepository projectRoundUserRepo = new SqlProjectRoundUserRepository();
		
//		public ProjectService(IProjectRepository projectRepo, 
//		                      IProjectRoundUnitRepository projectRoundUnitRepo,
//		                     IProjectRoundUserRepository projectRoundUserRepo)
//		{
//			this.projectRepo = projectRepo;
//			this.projectRoundUnitRepo = projectRoundUnitRepo;
//			this.projectRoundUserRepo = projectRoundUserRepo;
//		}
		
		public IList<Project> FindAllProjects()
		{
			return projectRepo.FindAll();
		}
		
		public ProjectRoundUnit ReadProjectRoundUnit(int projectRoundUnitID)
		{
			var pru = projectRoundUnitRepo.Read(projectRoundUnitID);
			pru.ProjectRound = projectRoundRepo.Read(pru.ProjectRoundID);
			pru.ProjectRoundUsers = projectRoundUserRepo.FindByProjectRoundUnit(projectRoundUnitID);
			return pru;
		}
	}
}
