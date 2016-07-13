using System;
using HW.Core;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Core.Tests.Repositories
{
	[TestFixture]
	public class ProjectRepositoryTests
	{
		SqlProjectRepository r;
		
		[SetUp]
		public void Setup()
		{
			r = new SqlProjectRepository();
		}
		
		[Test]
		public void TestUpdateProjectRoundUser()
		{
			r.UpdateProjectRoundUser(1, 1);
		}
		
		[Test]
		public void TestCountForSortString()
		{
			r.CountForSortString("");
		}
		
		[Test]
		public void TestReadRoundUnit()
		{
			r.ReadRoundUnit(100);
		}
		
		[Test]
		public void TestReadRound()
		{
			r.ReadRound(10);
		}
		
		[Test]
		public void TestFindRoundUnitsBySortString()
		{
			r.FindRoundUnitsBySortString("0000010900");
		}
	}
}
