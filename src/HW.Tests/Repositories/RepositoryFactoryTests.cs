using System;
using HW.Core;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Repositories
{
	[TestFixture]
	public class RepositoryFactoryTests
	{
		SqlRepositoryFactory f;
		
		[SetUp]
		public void Setup()
		{
			f = new SqlRepositoryFactory();
		}
	}
}
