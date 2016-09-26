using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HW.EForm.Core.Repositories
{
	public interface IProjectRoundRepository : IBaseRepository<ProjectRound>
	{
		IList<ProjectRound> FindByProject(int projectID);
		IList<ProjectRound> FindByProject(int projectID, int managerID);
		ProjectRound Read(int projectRoundID, int managerID);
	}
}
