//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IManagerFunctionRepository : IBaseRepository<ManagerFunction>
	{
		ManagerFunction ReadFirstFunctionBySponsorAdmin(int sponsorAdminID);
		
		IList<ManagerFunction> FindBySponsorAdmin(int sponsorAdminID);
	}
	
	public class ManagerFunctionRepositoryStub : BaseRepositoryStub<ManagerFunction>, IManagerFunctionRepository
	{
		public ManagerFunctionRepositoryStub()
		{
//			data.Add(new ManagerFunction { URL = "org.aspx", Function = "Organization", Expl = "Organization" });
			data.Add(new ManagerFunction { URL = "stats.aspx", Function = "Statistics", Expl = "Statistics" });
			data.Add(new ManagerFunction { URL = "messages.aspx", Function = "Messages", Expl = "Messages" });
			data.Add(new ManagerFunction { URL = "managers.aspx", Function = "Managers", Expl = "Managers" });
			data.Add(new ManagerFunction { URL = "debug.aspx", Function = "TEST", Expl = "TEST" });
			data.Add(new ManagerFunction { URL = "exercise.aspx", Function = "Exercises", Expl = "Exercises" });
		}
		
		public ManagerFunction ReadFirstFunctionBySponsorAdmin(int sponsorAdminID)
		{
			return data[0];
		}
		
		public IList<ManagerFunction> FindBySponsorAdmin(int sponsorAdminID)
		{
			return data;
		}
	}
}
