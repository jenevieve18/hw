// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Core.Tests.Repositories
{
	[TestFixture]
	public class SponsorAdminRepositoryTests
	{
		SqlSponsorAdminRepository r;
		
		[SetUp]
		public void Setup()
		{
			r = new SqlSponsorAdminRepository();
		}
		
		[Test]
		public void TestSaveAdminExerciseDataInputs()
		{
//			r.SaveAdminExerciseDataInputs(new[] { "life", "box", "chocolate" }, 514, 135);
		}
		
		[Test]
		public void TestFindSponsorAdminExerciseDataInputs()
		{
//			foreach (var i in r.FindSponsorAdminExerciseDataInputs(514, 135)) {
//				Console.WriteLine(i.Content);
//			}
		}
	}
}
