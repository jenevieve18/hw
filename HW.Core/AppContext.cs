//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Web;
using System.Web.SessionState;

namespace HW.Core
{
	public static class AppContext
	{
		static IRepositoryFactory repositoryFactory;
		
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
