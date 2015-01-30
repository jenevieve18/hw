using System;
using HW.Core;
using HW.Core.Models;
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
		
		[Test]
		public void a()
		{
			var e = new Exercise {
				Area = new ExerciseArea { Id = 7 },
				Category = new ExerciseCategory { Id = 2 },
				RequiredUserLevel = 10,
				Minutes = 10
			};
			e.AddLanguage("Celebrating success", "5-10 min", "All you need to know to acknowledge your employees in a way that increases motivation", 1);
			r.SaveExercise(e);
		}
	}
}
