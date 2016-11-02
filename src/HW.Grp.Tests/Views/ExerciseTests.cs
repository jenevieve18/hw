/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:18 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NUnit.Framework;

namespace HW.Grp.Tests.Views
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
			v.SetExercises(1, 1, 1, 1);
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
			v.SetExercises(1, 1, 1, 1);
			Assert.AreEqual("&EAID=1&ECID=1", v.AdditionalSortQuery);
		}
		
		[Test]
		public void TestAreasAndCategories()
		{
			v.SetExercises(1, 1, 1, 1);
			Assert.IsTrue(v.HasSelectedArea);
			Assert.IsTrue(v.HasSelectedCategory);
			
			v.SetExercises(666, 666, 1, 1);
			Assert.IsFalse(v.HasSelectedArea);
			Assert.IsFalse(v.HasSelectedCategory);
		}
	}
}
