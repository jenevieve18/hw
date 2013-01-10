//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core;
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
			r.UpdateSponsorLastLoginSent(1);
		}
		
		[Test]
		public void TestUpdateLastAllMessageSent()
		{
			r.UpdateLastAllMessageSent(1);
		}
		
		[Test]
		public void TestUpdateSponsorLastInviteReminderSent()
		{
			r.UpdateSponsorLastInviteReminderSent(1);
		}
		
		[Test]
		public void TesetUpdateSponsorLastInviteSent()
		{
			r.UpdateSponsorLastInviteSent(1);
		}
		
		[Test]
		public void TestUpdateSponsorInviteSent()
		{
			r.UpdateSponsorInviteSent(1, 1);
		}
		
		[Test]
		public void TestUpdateNullUserForUserInvite()
		{
			r.UpdateNullUserForUserInvite(1);
		}
		
		[Test]
		public void TestUpdateExtendedSurveyLastEmailSent()
		{
			r.UpdateExtendedSurveyLastEmailSent(1);
		}
		
		[Test]
		public void TestUpdateExtendedSurveyLastFinishedSent()
		{
			r.UpdateExtendedSurveyLastFinishedSent(1);
		}
		
		[Test]
		public void TestUpdateSponsor()
		{
			r.UpdateSponsor(1);
			r.UpdateSponsor(new Sponsor { });
		}
		
		[Test]
		public void TestUpdateSponsorAdmin()
		{
			r.UpdateSponsorAdmin(new SponsorAdmin());
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
		public void TestY()
		{
			r.Y(1);
		}
		
		[Test]
		public void TestFindAndCountDetailsBySuperAdmin()
		{
			var ss = r.FindAndCountDetailsBySuperAdmin(1);
//			int cx = 0;
//			string h = "localhost";
//			string q = "/superadmin.aspx";
//			int i = 0;
//			foreach (var s in ss) {
//				string str = "<TR" + (cx % 2 == 0 ? " BGCOLOR=\"#F2F2F2\"" : "") + ">" +
//					"<TD><A" + (!s.Closed ? " style=\"text-decoration:line-through;color:#cc0000;\"" : "") + " HREF=\"http://" + h + q.Substring(0, q.LastIndexOf("/")) + "/default.aspx?SA=0&SKEY=" + s.SponsorKey + s.Id.ToString() + "\" TARGET=\"_blank\">" + s.Name + "</A></TD>" +
//					"<TD>" + s.ExtendedSurveys.Count + "</TD>" +
//					"<TD>" + s.Invites.Count + "</TD>" +
//					"<TD>" + s.SentInvites.Count + "</TD>" +
//					"<TD>" + s.ActiveInvites.Count + "</TD>" +
//					"<TD>" + (s.MinimumInviteDate == null ? "N/A" : s.MinimumInviteDate.Value.ToString("yyyy-MM-dd")) + "</TD>" +
//					"<TD>" + (!s.SuperAdminSponsors[0].SeeUsers ? "No" : "Yes") + "</TD>" +
//					"<TD>" + (s.MinimumInviteDate != null ? "<A HREF=\"superadmin.aspx?ATSID=" + s.Id + "\"><img src=\"img/auditTrail.gif\" border=\"0\"/></A>" : "") + (!s.Closed ? "" : " <span style=\"color:#cc0000;\">Closed " + s.ClosedAt.Value.ToString("yyyy-MM-dd") + "</span>") + "</TD>" +
//					"</TR>";
//				Console.WriteLine(str);
//				Console.WriteLine(i++);
//			}
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
			r.FindAdminBySponsor(1, 1);
		}
		
		[Test]
		public void TestFindBySponsor()
		{
			r.FindBySponsor(1);
		}
		
		[Test]
		public void TestReadSponsorInviteBySponsor()
		{
			r.ReadSponsorInviteBySponsor(1, 1);
		}
		
		[Test]
		public void TestReadSponsorAdmin()
		{
			r.ReadSponsorAdmin(1, 1, "");
			r.ReadSponsorAdmin(1, "");
			r.ReadSponsorAdmin(null, null, null, "", "Usr5", "Pas5");
			r.ReadSponsorAdmin(1);
			r.ReadSponsorAdmin(1, 1, 1);
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
		public void TestX()
		{
			var x = r.X(5);
//			var s = r.X(1);
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
			r.DeleteSponsorAdmin(1);
		}
		
		[Test]
		public void TestUpdateDeletedAdmin()
		{
			r.UpdateDeletedAdmin(1, 1);
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
		
		[Test]
		public void TestReadSponsorProjectRoundUnit()
		{
			r.ReadSponsorProjectRoundUnit(1);
		}
		
		[Test]
		public void TestFindInviteBackgroundQuestionsByUser()
		{
			r.FindInviteBackgroundQuestionsByUser(1);
		}
	}
}
