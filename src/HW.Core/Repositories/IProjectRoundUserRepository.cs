using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IProjectRoundUserRepository : IBaseRepository<ProjectRoundUser>
	{
		IList<ProjectRoundUser> FindByProjectRoundUnit(int projectRoundUnitID);
	}
}
