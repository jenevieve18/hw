using System;
using HW.Core.Repositories;
using NUnit.Framework;

namespace HW.Tests.HWSite
{
	[TestFixture]
	public class ExerciseShowTests
	{
		[Test]
		public void TestShow()
		{
			var v = new healthWatch.exerciseShow();
			
//			v = new healthWatch.exerciseShow(new SponsorRepositoryStub(), new ExerciseRepositoryStub());
//			v.Show(1, 1, 1);
		}
		
		[Test]
		public void TestSetSponsor()
		{
			var v = new healthWatch.exerciseShow();
			
//			v = new healthWatch.exerciseShow(new SponsorRepositoryStub(), new ExerciseRepositoryStub());
//			v.SetSponsor(1);
		}
	}
}
