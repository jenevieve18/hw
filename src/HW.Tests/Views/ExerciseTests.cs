using System;
using HW.Core.Repositories;
using NUnit.Framework;

namespace HW.Tests.Views
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
		}
		
		[Test]
		public void TestCategories()
		{
			v.Categories = r.FindCategories(1, 1, 1);
			Assert.AreEqual(3, v.Categories.Count);
		}
	}
}
