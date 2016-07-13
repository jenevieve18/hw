using System;
using HW.Core;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Core.Tests.Repositories
{
	[TestFixture]
	public class IndexRepositoryTests
	{
		SqlIndexRepository r;
		
		[SetUp]
		public void Setup()
		{
			r = new SqlIndexRepository();
		}
		
		[Test]
		public void TestFindByLanguage()
		{
			r.FindByLanguage(1, 1, 2011, 2012, "", 3, 3);
		}
		
		[Test]
		public void TestReadByIdAndLanguage()
		{
			r.ReadByIdAndLanguage(1, 1);
		}
	}
}
