//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Repositories
{
	[TestFixture]
	public class UserRepositoryTests
	{
		SqlUserRepository r;
		
		[SetUp]
		public void Setup()
		{
			r = new SqlUserRepository();
		}
		
		[Test]
		public void TestUpdateUser()
		{
			r.UpdateUser(1, 1, 1);
		}
		
		[Test]
		public void TestA()
		{
			r.a(1, 1);
		}
		
		[Test]
		public void TestUpdateLastReminderSent()
		{
			r.UpdateLastReminderSent(1);
		}
		
		[Test]
		public void TestUpdateEmailFailure()
		{
			r.UpdateEmailFailure(1);
		}
		
		[Test]
		public void TestCountBySponsorWithAdminAndExtendedSurvey2()
		{
			r.CountBySponsorWithAdminAndExtendedSurvey2(1, 1, 1);
		}
		
		[Test]
		public void TestCountBySponsorWithAdminAndExtendedSurvey()
		{
			r.CountBySponsorWithAdminAndExtendedSurvey(1, 1, 1);
		}
		
		[Test]
		public void TestFindBySponsorWithLoginDays()
		{
			r.FindBySponsorWithLoginDays(1, 1, 1);
		}
		
		[Test]
		public void TestFindBySponsorWithExtendedSurvey()
		{
			r.FindBySponsorWithExtendedSurvey(1, 1, 1);
		}
		
		[Test]
		public void TestFindBySponsorWithExtendedSurvey2()
		{
			r.FindBySponsorWithExtendedSurvey2(1, 1, 1);
		}
		
		[Test]
		public void TestFind2()
		{
			r.Find2(1, 1);
		}
		
		[Test]
		public void TestFindUserProjectRoundUser()
		{
			r.FindUserProjectRoundUser(1, 1, 1);
		}
		
		[Test]
		public void TestReadById()
		{
			r.ReadById(1);
		}
		
		[Test]
		public void TestReadByIdAndSponsorExtendedSurvey()
		{
			r.ReadByIdAndSponsorExtendedSurvey(1, 1);
		}
		
		[Test]
		public void TestUpdateUserProfile()
		{
			r.UpdateUserProfile(1, 1, 1);
		}
		
		[Test]
		public void TestUpdateUserProjectRoundUser()
		{
			r.UpdateUserProjectRoundUser(1, 1);
		}
	}
}
