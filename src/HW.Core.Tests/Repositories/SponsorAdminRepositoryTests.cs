// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.Core.Models;
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
		public void TestSaveSponsorAdminExercise()
		{
			var e = new SponsorAdminExercise {
				Date = DateTime.Now,
				SponsorAdmin = new SponsorAdmin { Id = 514 },
				ExerciseVariantLanguage = new ExerciseVariantLanguage { Id = 1 }
			};
			
			var i = new SponsorAdminExerciseDataInput {};
			i.AddComponent(new SponsorAdminExerciseDataInputComponent { ValueText = "hello world" });
			i.AddComponent(new SponsorAdminExerciseDataInputComponent { ValueText = "world hello" });
			e.AddDataInput(i);
			
			i = new SponsorAdminExerciseDataInput {};
			i.AddComponent(new SponsorAdminExerciseDataInputComponent { ValueText = "world hello" });
			i.AddComponent(new SponsorAdminExerciseDataInputComponent { ValueText = "hello world" });
			e.AddDataInput(i);
			
			r.SaveSponsorAdminExercise(e);
		}
		
		[Test]
		public void TestReadSponsorAdminExercise()
		{
			var e = r.ReadSponsorAdminExercise(8);
			Console.WriteLine(e.Date);
			foreach (var i in e.Inputs) {
				Console.WriteLine("\t" + i.Id);
				foreach (var c in i.Components) {
					Console.WriteLine("\t\t" + c.ValueText);
				}
			}
		}
	}
}
