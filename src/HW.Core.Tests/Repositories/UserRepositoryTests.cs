using System;
using HW.Core;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Core.Tests.Repositories
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
		public void TestA()
		{
			r.CountAllActivatedUsersRecipients(1, 1);
		}
		
		[Test]
		public void TestUpdateEmailFailure()
		{
			r.UpdateEmailFailure(1);
		}
		
		[Test]
		public void TestCountBySponsorWithAdminAndExtendedSurvey2()
		{
			r.CountBySponsorExtendedSurveyReminderRecipients(1, 1, 1);
		}
		
		[Test]
		public void TestCountBySponsorWithAdminAndExtendedSurvey()
		{
			r.CountThankYouMessageRecipients(1, 1, 1);
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
		
//		[Test]
//		public void TestFindUserProjectRoundUser()
//		{
//			r.FindUserProjectRoundUser(1, 1, 1);
//		}
//		
		[Test]
		public void TestReadById()
		{
			r.ReadById(1);
		}
	}
}
