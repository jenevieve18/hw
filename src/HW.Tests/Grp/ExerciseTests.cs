using System;
using HW.Core.Repositories;
using NUnit.Framework;

namespace HW.Tests.Grp
{
	[TestFixture]
	public class ExerciseTests
	{
		HW.Grp.Exercise v;
		IExerciseRepository r;
		
		[SetUp]
		public void Setup()
		{
			v = new HW.Grp.Exercise();
			r = new ExerciseRepositoryStub();
		}
		
		[Test]
		public void TestAreas()
		{
			v.Areas = r.FindAreas(1, 1);
			Assert.AreEqual(3, v.Areas.Count);
			
			v.SetSelectedArea(1);
			Assert.IsNotNull(v.SelectedArea);
			Assert.IsTrue(v.HasSelectedArea);
			
			v.SetSelectedArea(-1);
			Assert.IsNull(v.SelectedArea);
			Assert.IsFalse(v.HasSelectedArea);
		}
		
		[Test]
		public void TestCategories()
		{
			v.Categories = r.FindCategories(1, 1, 1);
			Assert.AreEqual(3, v.Categories.Count);
			
			v.SetSelectedCategory(1);
			Assert.IsNotNull(v.SelectedCategory);
			Assert.IsTrue(v.HasSelectedCategory);
			
			v.SetSelectedCategory(-1);
			Assert.IsNull(v.SelectedCategory);
			Assert.IsFalse(v.HasSelectedCategory);
		}
		
		[Test]
		public void TestExercises()
		{
//			v.Exercises = r.FindByAreaAndCategory(1, 1, 1, 1);
//			Assert.AreEqual(3, v.Exercises.Count);
		}
	}
}
