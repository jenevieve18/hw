using System;
using HW.Core;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Repositories
{
	[TestFixture]
	public class ExerciseRepositoryTests
	{
		SqlExerciseRepository r;
		
		[SetUp]
		public void Setup()
		{
			r = new SqlExerciseRepository();
		}
		
		[Test]
		public void TestFindByAreaAndCategory()
		{
			r.FindByAreaAndCategory(1, 1, 1, 1);
		}
		
		[Test]
		public void TestFindCategories()
		{
			r.FindCategories(1, 1, 1);
		}
		
		[Test]
		public void TestFindAreas()
		{
			r.FindAreas(1, 1);
		}
	}
}
