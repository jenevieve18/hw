//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories.NHibernate
{
	public class NHibernateManagerFunctionRepository : BaseNHibernateRepository<ManagerFunction>, IManagerFunctionRepository
	{
		public NHibernateManagerFunctionRepository()
		{
		}
		
		public override IList<ManagerFunction> FindAll()
		{
			return base.FindAll("healthWatch");
		}
		
		public ManagerFunction ReadFirstFunctionBySponsorAdmin(int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<ManagerFunction> FindBySponsorAdmin(int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
	}
}
