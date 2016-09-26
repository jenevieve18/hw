// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

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
