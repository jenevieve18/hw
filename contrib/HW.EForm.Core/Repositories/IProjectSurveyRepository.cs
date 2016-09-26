using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HW.EForm.Core.Repositories
{
	public interface IProjectSurveyRepository : IBaseRepository<ProjectSurvey>
	{
		IList<ProjectSurvey> FindByProject(int projectID);
	}
}
