using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HW.EForm.Core.Repositories
{
	public interface IProjectRepository : IBaseRepository<Project>
	{
		Project Read(int projectID, int managerID);
		IList<Project> FindByManager(int managerID);
	}
	
	public class ProjectRepositoryStub : BaseRepositoryStub<Project>, IProjectRepository
	{
		public ProjectRepositoryStub()
		{
			data.Add(new Project { ProjectID = 1, Internal = "Project1" });
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
}
