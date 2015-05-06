using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IRepositoryFactory
	{
		ISponsorRepository CreateSponsorRepository();
		
		ISponsorAdminRepository CreateSponsorAdminRepository();
		
		IManagerFunctionRepository CreateManagerFunctionRepository();
	}
	
	public class RepositoryFactoryStub : IRepositoryFactory
	{
		public ISponsorRepository CreateSponsorRepository()
		{
			return new SponsorRepositoryStub();
		}
		
		public ISponsorAdminRepository CreateSponsorAdminRepository()
		{
			return new SponsorAdminRepositoryStub();
		}
		
		public IManagerFunctionRepository CreateManagerFunctionRepository()
		{
			return new ManagerFunctionRepositoryStub();
		}
	}
}
