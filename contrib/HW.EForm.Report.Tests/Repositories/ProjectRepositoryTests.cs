// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;
using NUnit.Framework;
using System.Collections.Generic;

namespace HW.EForm.Report.Tests.Repositories
{
	[TestFixture]
	public class ProjectRepositoryTests
	{
		ProjectRepositoryStub r = new ProjectRepositoryStub();
		
		[Test]
		public void TestMethod()
		{
			var p = r.Read(1);
			Assert.AreEqual("Healthwatch", p.Name);
		}
	}
	
	public class ProjectRepositoryStub : BaseRepositoryStub<Project>, IProjectRepository
	{
		public ProjectRepositoryStub()
		{
			items.Add(new Project { Name = "Healthwatch" });
		}
		
		public Project Read(int projectID, int managerID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Project> FindByManager(int managerID)
		{
			throw new NotImplementedException();
		}
	}
	
	public class ProjectRoundRepositoryStub : BaseRepositoryStub<ProjectRound>, IProjectRoundRepository
	{
		public ProjectRoundRepositoryStub()
		{
			items.Add(new ProjectRound { ProjectRoundID = 1, ProjectID = 1, Internal = "HME Index Test" });
		}
		
		public IList<ProjectRound> FindByProject(int projectID)
		{
			return items.FindAll(x => x.ProjectID == projectID);
		}
		
		public IList<ProjectRound> FindByProject(int projectID, int managerID)
		{
			throw new NotImplementedException();
		}
		
		public ProjectRound Read(int projectRoundID, int managerID)
		{
			throw new NotImplementedException();
		}
	}
	
	public class ProjectRoundUnitRepositoryStub : BaseRepositoryStub<ProjectRoundUnit>, IProjectRoundUnitRepository
	{
		public ProjectRoundUnitRepositoryStub()
		{
			items.Add(new ProjectRoundUnit { ProjectRoundUnitID = 1, ProjectRoundID = 1, Unit = "Test" });
		}
		
		public IList<ProjectRoundUnit> FindProjectRoundUnits(int[] projectRoundUnitIDs)
		{
			var units = new List<ProjectRoundUnit>();
			foreach (var i in projectRoundUnitIDs) {
				foreach (var u in items) {
					if (u.ProjectRoundUnitID == i) {
						units.Add(u);
					}
				}
			}
			return units;
		}
		
		public IList<ProjectRoundUnit> FindByProjectRound(int projectRoundID)
		{
			return items.FindAll(x => x.ProjectRoundID == projectRoundID);
		}
		
		public IList<ProjectRoundUnit> FindByProjectRound(int rojectRoundID, int managerID)
		{
			throw new NotImplementedException();
		}
	}
	
	public class ProjectRoundUserRepositoryStub : BaseRepositoryStub<ProjectRoundUser>, IProjectRoundUserRepository
	{
		public ProjectRoundUserRepositoryStub()
		{
			items.Add(new ProjectRoundUser { ProjectRoundUserID = 1, ProjectRoundUnitID = 1, Email = "info@eform.se" });
			items.Add(new ProjectRoundUser { ProjectRoundUserID = 2, ProjectRoundUnitID = 1, Email = "info@eform.se" });
			items.Add(new ProjectRoundUser { ProjectRoundUserID = 3, ProjectRoundUnitID = 1, Email = "info@eform.se" });
			items.Add(new ProjectRoundUser { ProjectRoundUserID = 4, ProjectRoundUnitID = 1, Email = "info@eform.se" });
			items.Add(new ProjectRoundUser { ProjectRoundUserID = 5, ProjectRoundUnitID = 1, Email = "info@eform.se" });
			items.Add(new ProjectRoundUser { ProjectRoundUserID = 6, ProjectRoundUnitID = 1, Email = "info@eform.se" });
			items.Add(new ProjectRoundUser { ProjectRoundUserID = 7, ProjectRoundUnitID = 1, Email = "info@eform.se" });
			items.Add(new ProjectRoundUser { ProjectRoundUserID = 8, ProjectRoundUnitID = 1, Email = "info@eform.se" });
			items.Add(new ProjectRoundUser { ProjectRoundUserID = 9, ProjectRoundUnitID = 1, Email = "info@eform.se" });
			items.Add(new ProjectRoundUser { ProjectRoundUserID = 10, ProjectRoundUnitID = 1, Email = "info@eform.se" });
		}
	}
}
