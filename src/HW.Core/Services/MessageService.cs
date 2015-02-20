using System;
using System.Collections.Generic;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Core.Services
{
	public class MessageService
	{
		IExtendedSurveyRepository r;
		SqlUserRepository u;
		SqlSponsorRepository s;
		SqlProjectRepository p;
		
		public MessageService(IExtendedSurveyRepository r, SqlSponsorRepository s, SqlUserRepository u, SqlProjectRepository p)
		{
			this.r = r;
			this.s = s;
			this.u = u;
			this.p = p;
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorID, int sponsorAdminID, string password)
		{
			return s.ReadSponsorAdmin(sponsorID, sponsorAdminID, password);
		}
		
		public void Update(string loginSubject, string loginTxt, int loginDays, int loginWeekday, int sponsorID)
		{
			s.Update(loginSubject, loginTxt, loginDays, loginWeekday, sponsorID);
		}
		
		public ProjectRound ReadRound(int projectRoundID)
		{
			return p.ReadRound(projectRoundID);
		}
		
		public int CountBySponsorWithAdminAndExtendedSurvey(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			return u.CountBySponsorWithAdminAndExtendedSurvey(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
		}
		
		public int CountBySponsorWithAdminAndExtendedSurvey2(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			return u.CountBySponsorWithAdminAndExtendedSurvey2(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
		}
		
		public int a(int sponsorID, int sponsorAdminID)
		{
			return u.a(sponsorID, sponsorAdminID);
		}
		
		public int UpdateEmailTexts(int sponsorExtendedSurveyID, int sponsorAdminID, int sponsorAdminExtendedSurveyID, string extendedSurveySubject, string extendedSurveyTxt, string extendedSurveyFinishedSubject, string extendedSurveyFinishedTxt)
		{
			return r.UpdateEmailTexts(sponsorExtendedSurveyID, sponsorAdminID, sponsorAdminExtendedSurveyID, extendedSurveySubject, extendedSurveyTxt, extendedSurveyFinishedSubject, extendedSurveyFinishedTxt);
		}
		
		public void UpdateInviteTexts(int sponsorID, string inviteSubject, string inviteTxt, string inviteReminderSubject, string inviteReminderTxt, string allMessageSubject, string allMessageBody)
		{
			r.UpdateInviteTexts(sponsorID, inviteSubject, inviteTxt, inviteReminderSubject, inviteReminderTxt, allMessageSubject, allMessageBody);
		}
		
		public void SaveSponsorAdminSessionFunction(int sponsorAdminSessionID, int functionID, DateTime date)
		{
			s.SaveSponsorAdminSessionFunction(sponsorAdminSessionID, functionID, date);
		}
		
		public ISponsor ReadSponsor(int ID)
		{
			return r.ReadSponsor(ID);
		}
		
		public IList<IExtendedSurvey> FindExtendedSurveysBySponsorAdmin(int sponsorID, int sponsorAdminID)
		{
			IList<IExtendedSurvey> surveys = r.FindExtendedSurveysBySponsorAdmin(sponsorID, sponsorAdminID);
			if ((surveys != null && surveys.Count <= 0) || sponsorAdminID == -1) {
				surveys = s.FindExtendedSurveysBySponsorAdmin(sponsorID, sponsorAdminID);
			}
			return surveys;
		}
		
		public IList<User> FindBySponsorWithExtendedSurvey2(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			return u.FindBySponsorWithExtendedSurvey2(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
		}
		
		public IList<User> Find2(int sponsorID, int sponsorAdminID)
		{
			return u.Find2(sponsorID, sponsorAdminID);
		}
		
		public IList<User> FindBySponsorWithExtendedSurvey(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			return u.FindBySponsorWithExtendedSurvey(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
		}
		
		public void UpdateEmailFailure(int userID)
		{
			u.UpdateEmailFailure(userID);
		}
		
		public void UpdateLastReminderSent(int userID)
		{
			u.UpdateLastReminderSent(userID);
		}
		
		public IList<SponsorInvite> FindSentInvitesBySponsor(int sponsorID, int sponsorAdminID)
		{
			return s.FindSentInvitesBySponsor(sponsorID, sponsorAdminID);
		}
		
		public IList<SponsorInvite> FindInvitesBySponsor(int sponsorID, int sponsorAdminID)
		{
			return s.FindInvitesBySponsor(sponsorID, sponsorAdminID);
		}
		
		public IList<User> FindBySponsorWithLoginDays(int sponsorID, int sponsorAdminID, int selectedValue)
		{
			return u.FindBySponsorWithLoginDays(sponsorID, sponsorAdminID, selectedValue);
		}
		
		public void UpdateExtendedSurveyLastFinishedSent(int sponsorID, int sponsorExtendedSurveyID, int sponsorAdminExtendedSurveyID)
		{
			r.UpdateExtendedSurveyLastFinishedSent(sponsorExtendedSurveyID != -1 ? sponsorAdminExtendedSurveyID : sponsorID);
		}
		
		public void UpdateLastAllMessageSent(int sponsorID, int sponsorAdminID)
		{
			r.UpdateLastAllMessageSent(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);
		}
		
		public void UpdateSponsorLastInviteSent(int sponsorID, int sponsorAdminID)
		{
			r.UpdateSponsorLastInviteSent(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);
		}
		
		public void UpdateSponsorLastInviteReminderSent(int sponsorID, int sponsorAdminID) {
			r.UpdateSponsorLastInviteReminderSent(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);
		}
		
		public void UpdateSponsorLastLoginSent(int sponsorID, int sponsorAdminID)
		{
			r.UpdateSponsorLastLoginSent(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);
		}
		
		public void UpdateExtendedSurveyLastEmailSent(int sponsorID, int sponsorExtendedSurveyID, int sponsorAdminExtendedSurveyID)
		{
			r.UpdateExtendedSurveyLastEmailSent(sponsorExtendedSurveyID != -1 ? sponsorAdminExtendedSurveyID : sponsorID);
		}
	}
}
