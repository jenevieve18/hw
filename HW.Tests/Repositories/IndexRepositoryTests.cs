//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core;
using NUnit.Framework;

namespace HW.Tests.Repositories
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
			r.FindByLanguage(1, 1, 2011, 2012, "");
		}
		
		[Test]
		public void TestReadByIdAndLanguage()
		{
			r.ReadByIdAndLanguage(1, 1);
		}
	}
}
