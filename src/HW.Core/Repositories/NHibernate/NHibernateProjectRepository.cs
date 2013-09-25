using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories.NHibernate
{
	public class NHibernateProjectRepository : BaseNHibernateRepository<Project>, IProjectRepository
	{
		public NHibernateProjectRepository()
		{
		}
		
		public void UpdateProjectRoundUser(int projectRoundUnitID, int proejctRoundUserID)
		{
			throw new NotImplementedException();
		}
		
		public ProjectRound ReadRound(int projectRoundID)
		{
			return NHibernateHelper.OpenSession().Load<ProjectRound>(projectRoundID);
		}
		
		public ProjectRoundUnit ReadRoundUnit(int projectRoundUnitID)
		{
			throw new NotImplementedException();
		}
		
		public IList<ProjectRoundUnit> FindRoundUnitsBySortString(string sortString)
		{
			throw new NotImplementedException();
		}
		
		public int CountForSortString(string sortString)
		{
			throw new NotImplementedException();
		}
		
		public IList<ProjectRoundUnit> FindAllProjectRoundUnits()
		{
			return FindAll<ProjectRoundUnit>();
		}
	}
}
