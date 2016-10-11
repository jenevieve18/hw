// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.IO;
using System.Text;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using NUnit.Framework;
using System.Configuration;

namespace HW.Core.Tests.Repositories
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
		public void a()
		{
			var f = new Form1();
			f.ShowDialog();
		}
		
		[Test]
		public void c()
		{
			for (int i = 0; i < 10; i++) {
				Console.WriteLine("{0}. {1}{2}", i, "Writing...".PadRight(60), "[OK]");
			}
		}
		
		[Test]
		public void b()
		{
			string dir = "exercises";
			if (!Directory.Exists(dir)) {
				Directory.CreateDirectory(dir);
			}
			var exercises = r.FindByAreaAndCategory(0, 0, 0, 2);
			Console.WriteLine("Fetched {0} exercises from the database.", exercises.Count);
			int i = 0;
			foreach (var e in exercises) {
				Console.Write("{0}. Writing {1}...", ++i, e.CurrentLanguage.ExerciseName);
				try {
					string file = Path.Combine(dir, e.CurrentVariant.Id + " - " + e.CurrentLanguage.ExerciseName + ".html");
					using (var w = new StreamWriter(FileHelper.SanitizeFileName(file), false, Encoding.UTF8)) {
						w.WriteLine(e.CurrentVariant.Content);
					}
					Console.WriteLine("	[OK]");
				} catch (Exception ex) {
					Console.WriteLine(ex.Message);
					Console.WriteLine("	[FAILED]");
				}
			}
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
		public void TestReadExerciseVariant()
		{
			var e = r.ReadExerciseVariant(81);
		}
	}
}
