﻿using System;
using HW.Core.Repositories;
using NUnit.Framework;

namespace HW.Tests.Grp
{
	[TestFixture]
	public class ExerciseTests
	{
		HW.Grp.Exercise v;
		
		[SetUp]
		public void Setup()
		{
			v = new HW.Grp.Exercise();
			
//			v = new HW.Grp.Exercise(new SponsorRepositoryStub(), new ExerciseRepositoryStub());
		}
		[Test]
		public void TestExercises()
		{
			v.Index(1, 1, 1, 1);
		}
		
		[Test]
		public void TestSaveAdminSession()
		{
			v.SaveAdminSession(1, 1, DateTime.Now);
		}
		
		[Test]
		public void TestAdditionalSortQuery()
		{
			Assert.AreEqual("", v.AdditionalSortQuery);
			v.Index(1, 1, 1, 1);
			Assert.AreEqual("&EAID=1&ECID=1", v.AdditionalSortQuery);
		}
		
		[Test]
		public void TestAreasAndCategories()
		{
			v.Index(1, 1, 1, 1);
			Assert.IsTrue(v.HasSelectedArea);
			Assert.IsTrue(v.HasSelectedCategory);
			
			v.Index(666, 666, 1, 1);
			Assert.IsFalse(v.HasSelectedArea);
			Assert.IsFalse(v.HasSelectedCategory);
		}
	}
}
