//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;

namespace HW.Core
{
	public class AppContext
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
