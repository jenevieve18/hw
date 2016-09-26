using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HW.EForm.Core.Repositories
{
	public interface IProjectRoundUnitRepository : IBaseRepository<ProjectRoundUnit>
	{
		IList<ProjectRoundUnit> FindProjectRoundUnits(int[] projectRoundUnitIDs);
		IList<ProjectRoundUnit> FindByProjectRound(int projectRoundID);
		IList<ProjectRoundUnit> FindByProjectRound(int rojectRoundID, int managerID);
	}
}
