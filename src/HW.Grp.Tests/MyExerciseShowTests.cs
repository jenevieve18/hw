/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 8:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Grp.Tests
{
	[TestFixture]
	public class MyExerciseShowTests
	{
		MyExerciseShow v;
		
		[SetUp]
		public void Setup()
		{
			v = new MyExerciseShow(new ExerciseRepositoryStub());
		}
		
		[Test]
		public void TestShow()
		{
			v.Show(1, 1, 1);
		}
		
		[Test]
		public void TestSetSponsor()
		{
			v.SetSponsor(new Sponsor {});
		}
	}
}
