using System;
using System.Collections.Generic;
using HW.Core;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Repositories
{
	[TestFixture]
	public class SponsorRepositoryTests
	{
		SqlSponsorRepository r;
		
		[SetUp]
		public void Setup()
		{
			r = new SqlSponsorRepository();
		}
		
		[Test]
		public void TestUpdateSponsorLastLoginSent()
		{
			r.UpdateSponsorLastLoginSent(-1);
		}
		
		[Test]
		public void TestUpdateLastAllMessageSent()
		{
			r.UpdateLastAllMessageSent(-1);
		}
		
		[Test]
		public void TestUpdateSponsorLastInviteReminderSent()
		{
			r.UpdateSponsorLastInviteReminderSent(-1);
		}
		
		[Test]
		public void TesetUpdateSponsorLastInviteSent()
		{
			r.UpdateSponsorLastInviteSent(1);
		}
		
		[Test]
		public void TestUpdateExtendedSurveyLastEmailSent()
		{
			r.UpdateExtendedSurveyLastEmailSent(-1);
		}
		
		[Test]
		public void TestUpdateExtendedSurveyLastFinishedSent()
		{
			r.UpdateExtendedSurveyLastFinishedSent(-1);
		}
		
		[Test]
		public void TestUpdateSponsor()
		{
			r.UpdateSponsor(-1);
			r.UpdateSponsor(new Sponsor { });
		}
		
		[Test]
		public void TestUpdateSponsorAdmin()
		{
			r.UpdateSponsorAdmin(new SponsorAdmin { Id = -1 });
		}
		
		[Test]
		public void TestReadSponsorInviteByUser()
		{
			r.ReadSponsorInviteByUser(1);
		}
		
		[Test]
		public void TestReadSponsorInviteBackgroundQuestion()
		{
			r.ReadSponsorInviteBackgroundQuestion(1, 1, 1);
		}
		
		[Test]
		public void TestFindExtendedSurveysBySponsorAdmin()
		{
			r.FindExtendedSurveysBySponsorAdmin(1, 1);
		}
		
		[Test]
		public void TestReadSponsor()
		{
			r.ReadSponsor(1);
		}
		
		[Test]
		public void TestFindAdminDepartmentBySponsorAdmin()
		{
			r.FindAdminDepartmentBySponsorAdmin(1);
		}
		
		[Test]
		public void TestFindBySponsorAndLanguage()
		{
			r.FindBySponsorAndLanguage(1, 1);
		}
		
		[Test]
		public void TestFindDistinctRoundUnitsWithReportBySuperAdmin()
		{
			r.FindDistinctRoundUnitsWithReportBySuperAdmin(1);
		}
		
		[Test]
		public void TestFindAdminFunctionBySponsorAdmin()
		{
			r.FindAdminFunctionBySponsorAdmin(1);
		}
		
		[Test]
		public void TestFindAdminBySponsor()
		{
			r.FindAdminBySponsor(2, 188, "");
		}
		
		[Test]
		public void TestFindBySponsor()
		{
			r.FindBySponsor(1);
		}
		
		[Test]
		public void TestReadSponsorAdmin()
		{
			r.ReadSponsorAdmin(1, 1, "");
			r.ReadSponsorAdmin(1, "");
			r.ReadSponsorAdmin(null, null, null, "", "Usr5", "Pas5");
//			r.ReadSponsorAdmin(1);
			r.ReadSponsorAdmin(1, 1, 1);
		}
		
		[Test]
		public void TestReadSponsorAdmin2()
		{
			r.ReadSponsorAdmin("FB8CC11083", null, null, "", null, null);
		}
		
//		[Test]
//		public void TestReadSponsorAdmin2()
//		{
//			r.ReadSponsorAdmin2(1, "");
//		}
//		
		[Test]
		public void TestSponsorAdminExists()
		{
			r.SponsorAdminExists(1, "");
		}
		
		[Test]
		public void TestB()
		{
			r.ReadSponsorAdmin(1, 1, 1);
		}
		
		[Test]
		public void TestReadSponsor2()
		{
			var x = r.ReadSponsor2(5);
			string s = "test";
			string h = "test";
			Console.WriteLine(s.Replace(" ", "").IndexOf(h.Replace(" ", "")));
		}
		
		[Test]
		public void TestCountCreatedInvitesBySponsor()
		{
			r.CountCreatedInvitesBySponsor(1, DateTime.Now);
		}
		
		[Test]
		public void TestFindInvitesBySponsor()
		{
			r.FindInvitesBySponsor(1, 1);
		}
		
		[Test]
		public void TestInsertSponsorAdmin()
		{
			r.SaveSponsorAdmin(new SponsorAdmin { Sponsor = new Sponsor() });
		}
		
		[Test]
		public void TestDeleteSponsorAdmin()
		{
			r.DeleteSponsorAdminFunction(1);
		}
		
		[Test]
		public void TestUpdateDeletedAdmin()
		{
			r.UpdateDeletedAdmin(-1, -1);
		}
		
		[Test]
		public void TestFindExtendedSurveysBySponsor()
		{
			r.FindExtendedSurveysBySponsor(1);
		}
		
		[Test]
		public void TestCountSentInvitesBySponsor()
		{
			r.CountSentInvitesBySponsor(1, DateTime.Now);
		}
		
		[Test]
		public void TestFindSentInvitesBySponsor()
		{
			r.FindSentInvitesBySponsor(1, 1);
		}
		
		[Test]
		public void TestFindExtendedSurveysBySuperAdmin()
		{
			r.FindExtendedSurveysBySuperAdmin(1);
		}
		
		[Test]
		public void TestInsertSponsorAdminFunction()
		{
			r.SaveSponsorAdminFunction(new SponsorAdminFunction { Admin = new SponsorAdmin(), Function = new ManagerFunction() });
		}
		
//		[Test]
//		public void TestReadSponsorProjectRoundUnit()
//		{
//			r.ReadSponsorProjectRoundUnit(1);
//		}
//		
//		[Test]
//		public void TestFindInviteBackgroundQuestionsByUser()
//		{
//			r.FindInviteBackgroundQuestionsByUser(1);
//		}
	}
}
