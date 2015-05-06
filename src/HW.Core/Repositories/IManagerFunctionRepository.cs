using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IManagerFunctionRepository : IBaseRepository<ManagerFunction>
	{
		IList<ManagerFunctionLang> FindBySponsorAdmin(int sponsorAdminID, int langID);
		
		IList<ManagerFunction> FindBySponsorAdminX(int sponsorAdminID, int langID);
	}
	
	public class ManagerFunctionRepositoryStub : BaseRepositoryStub<ManagerFunction>, IManagerFunctionRepository
	{
		public IList<ManagerFunctionLang> FindBySponsorAdmin(int sponsorAdminID, int langID)
		{
            return new[] {
				new ManagerFunctionLang {},
				new ManagerFunctionLang {},
				new ManagerFunctionLang {},
			};
		}
		
		public IList<ManagerFunction> FindBySponsorAdminX(int sponsorAdminID, int langID)
		{
            return new[] {
				new ManagerFunction(new [] { new ManagerFunctionLang { Function = "org.aspx" }}),
				new ManagerFunction(new [] { new ManagerFunctionLang { Function = "stats.aspx" }}),
				new ManagerFunction(new [] { new ManagerFunctionLang { Function = "managers.aspx" }}),
			};
		}
	}
}
