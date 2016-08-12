using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IProjectRepository : IBaseRepository<Project>
	{
	}
	
	public class ProjectRepositoryStub : BaseRepositoryStub<Project>, IProjectRepository
	{
		public override IList<Project> FindAll()
		{
			return new List<Project>(
				new [] {
					new Project { Name = "Project 1" }
				}
			);
		}
	}
}
