//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Repositories
{
	[TestFixture]
	public class LanguageRepositoryTests
	{
		SqlLanguageRepository r;
		
		[SetUp]
		public void Setup()
		{
			r = new SqlLanguageRepository();
		}
		
		[Test]
		public void TestFindBySponsor()
		{
			r.FindBySponsor(1);
		}
	}
}
