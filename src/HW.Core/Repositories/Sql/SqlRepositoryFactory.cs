using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlRepositoryFactory : IRepositoryFactory
	{
		public ISponsorRepository CreateSponsorRepository()
		{
			return new SqlSponsorRepository();
		}
		
		public ISponsorAdminRepository CreateSponsorAdminRepository()
		{
			return new SqlSponsorAdminRepository();
		}
		
		public IManagerFunctionRepository CreateManagerFunctionRepository()
		{
			return new SqlManagerFunctionRepository();
		}
	}
}
