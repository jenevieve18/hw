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
using HW.Core.Services;
using NUnit.Framework;

namespace HW.Grp.Tests
{
	[TestFixture]
	public class MyExerciseShowTests
	{
		MyExerciseShow v;
		ExerciseService s;
		
		[SetUp]
		public void Setup()
		{
			s = new ExerciseService();
			v = new MyExerciseShow();
		}
		
		[Test]
		public void TestShow()
		{
			v.Show(s.ReadSponsorAdminExercise(1));
		}
		
		[Test]
		public void TestIndex()
		{
			v.Index(1, 1, null, 1, 1, 1, 1, 1, 1);
		}
		
		[Test]
		public void TestSetSponsor()
		{
			v.SetSponsor(
				new Sponsor {
					SuperSponsor = new SuperSponsor {
						Languages = new [] {
							new SuperSponsorLanguage { Header = "Header" }
						}
					}
				}
			);
		}
		
		[Test]
		public void TestSaveStatistics()
		{
			v.SaveStatistics(1, 1, 1);
		}
		
		[Test]
		public void TestCloseWindow()
		{
			v.CloseWindow();
		}
	}
}
