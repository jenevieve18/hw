//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Web;
using System.Web.SessionState;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Core
{
	public static class AppContext
	{
		static IRepositoryFactory repositoryFactory;
		
		public static void SetRepositoryFactory(string name)
		{
			if (name == "SQL") {
				SetRepositoryFactory(new SqlRepositoryFactory());
			} else if (name == "STUB") {
				SetRepositoryFactory(new RepositoryFactoryStub());
			} else {
				throw new NotSupportedException();
			}
		}
		
		public static void SetRepositoryFactory(IRepositoryFactory repositoryFactory)
		{
			AppContext.repositoryFactory = repositoryFactory;
		}
		
		public static IRepositoryFactory GetRepositoryFactory()
		{
			return repositoryFactory;
		}
	}
}
