//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IProjectRepository : IBaseRepository<Project>
	{
		void UpdateProjectRoundUser(int projectRoundUnitID, int proejctRoundUserID);
		
		ProjectRound ReadRound(int projectRoundID);
		
		ProjectRoundUnit ReadRoundUnit(int projectRoundUnitID);
		
		IList<ProjectRoundUnit> FindRoundUnitsBySortString(string sortString);
		
		int CountForSortString(string sortString);
	}
	
	public class ProjectRepositoryStub : BaseRepositoryStub<Project>, IProjectRepository
	{
		public void UpdateProjectRoundUser(int projectRoundUnitID, int proejctRoundUserID)
		{
		}
		
		public ProjectRoundUnit ReadRoundUnit(int projectRoundUnitID)
		{
			return new ProjectRoundUnit {
				SortString = "SortString",
				Language = new Language { Id = 1 }
			};
		}
		
		public IList<ProjectRoundUnit> FindRoundUnitsBySortString(string sortString)
		{
			var units = new List<ProjectRoundUnit>();
			for (int i = 0; i < 10; i++) {
				var u = new ProjectRoundUnit() {
					TreeString = ">> Tree " + i,
					SortString = "Sort String " + i
				};
				units.Add(u);
			}
			return units;
		}
		
		public int CountForSortString(string sortString)
		{
			return 10;
		}
		
		public ProjectRound ReadRound(int projectRoundID)
		{
			var p = new ProjectRound {
				Started = DateTime.Now,
				Closed = DateTime.Now
			};
			return p;
		}
	}
}
