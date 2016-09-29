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
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Grp.Tests.Views
{
	[TestFixture]
	public class MyExerciseTests
	{
		SqlExerciseRepository er = new SqlExerciseRepository();
		MyExercise v;
		int lid = 1;
		int sponsorAdminID = 514;
		
		[SetUpAttribute]
		public void Setup()
		{
			v = new MyExercise(lid);
		}
		
		[Test]
		public void TestShow()
		{
			var exercises = er.FindBySponsorAdminExerciseHistory(lid - 1, sponsorAdminID);
			v.Show(exercises);
		}
		
		[Test]
		public void TestSetLanguage()
		{
			v.SetLanguage(new UserSession {});
		}
	}
}
