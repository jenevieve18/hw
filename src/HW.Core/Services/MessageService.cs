using System;
using System.Collections.Generic;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Core.Services
{
	public class MessageService
	{
		IExtendedSurveyRepository extendedSurveyRepository;
		SqlUserRepository userRepository;
		SqlSponsorRepository sponsorRepository;
		SqlProjectRepository projectRepository;
		
		public MessageService(IExtendedSurveyRepository r, SqlSponsorRepository s, SqlUserRepository u, SqlProjectRepository p)
		{
			this.extendedSurveyRepository = r;
			this.sponsorRepository = s;
			this.userRepository = u;
			this.projectRepository = p;
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorID, int sponsorAdminID, string password)
		{
			return sponsorRepository.ReadSponsorAdmin(sponsorID, sponsorAdminID, password);
		}
		
		public void Update(string loginSubject, string loginTxt, int loginDays, int loginWeekday, int sponsorID)
		{
			sponsorRepository.Update(loginSubject, loginTxt, loginDays, loginWeekday, sponsorID);
		}
		
		public ProjectRound ReadRound(int projectRoundID)
		{
			return projectRepository.ReadRound(projectRoundID);
		}
		
		public int CountThankYouMessageRecipients(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			return userRepository.CountThankYouMessageRecipients(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
		}
		
		public int CountBySponsorExtendedSurveyReminderRecipients(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			return userRepository.CountBySponsorExtendedSurveyReminderRecipients(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
		}
		
		public int CountAllActivatedUsersRecipients(int sponsorID, int sponsorAdminID)
		{
			return userRepository.CountAllActivatedUsersRecipients(sponsorID, sponsorAdminID);
		}
		
		public int UpdateEmailTexts(int sponsorExtendedSurveyID, int sponsorAdminID, int sponsorAdminExtendedSurveyID, string extendedSurveySubject, string extendedSurveyTxt, string extendedSurveyFinishedSubject, string extendedSurveyFinishedTxt)
		{
			return extendedSurveyRepository.UpdateEmailTexts(sponsorExtendedSurveyID, sponsorAdminID, sponsorAdminExtendedSurveyID, extendedSurveySubject, extendedSurveyTxt, extendedSurveyFinishedSubject, extendedSurveyFinishedTxt);
		}
		
		public void UpdateInviteTexts(int sponsorID, string inviteSubject, string inviteTxt, string inviteReminderSubject, string inviteReminderTxt, string allMessageSubject, string allMessageBody)
		{
			extendedSurveyRepository.UpdateInviteTexts(sponsorID, inviteSubject, inviteTxt, inviteReminderSubject, inviteReminderTxt, allMessageSubject, allMessageBody);
		}
		
		public void SaveSponsorAdminSessionFunction(int sponsorAdminSessionID, int functionID, DateTime date)
		{
			sponsorRepository.SaveSponsorAdminSessionFunction(sponsorAdminSessionID, functionID, date);
		}
		
		public ISponsor ReadSponsor(int ID)
		{
			return extendedSurveyRepository.ReadSponsor(ID);
		}
		
		public IList<IExtendedSurvey> FindExtendedSurveysBySponsorAdmin(int sponsorID, int sponsorAdminID)
		{
//			IList<IExtendedSurvey> surveys = extendedSurveyRepository.FindExtendedSurveysBySponsorAdmin(sponsorID, sponsorAdminID);
//			if ((surveys != null && surveys.Count <= 0) || sponsorAdminID == -1) {
//				surveys = sponsorRepository.FindExtendedSurveysBySponsorAdmin(sponsorID, sponsorAdminID);
//			}
			
			var surveys = sponsorRepository.FindExtendedSurveysBySponsorAdmin(sponsorID, sponsorAdminID);
			
			var x = new SqlSponsorAdminRepository().ReadExtendedSurveysBySponsorAdmin(sponsorID, sponsorAdminID);
			if (x != null) {
			bool found = false;
			IList<int> seen = new List<int>();
			foreach (var s in surveys) {
				if (!seen.Contains(s.ExtraExtendedSurveyId != 0 ? s.ExtraExtendedSurveyId : s.Id)) {
					if (s.ProjectRound != null) {
						if (!found) {
//							projectRoundID = s.ProjectRound.Id;
//							if (!postBack) {
//								extendedSurvey = s.Internal + s.RoundText;
//								
//								textBoxExtendedSurveySubject.Text = s.EmailSubject;
//								textBoxExtendedSurveyTxt.Text = s.EmailBody;
							
							s.EmailSubject = x.EmailSubject;
							s.EmailBody = x.EmailBody;
							
							s.FinishedEmailSubject = x.FinishedEmailSubject;
							s.FinishedEmailBody = x.FinishedEmailBody;
//
//								textBoxExtendedSurveyFinishedSubject.Text = s.FinishedEmailSubject;
//								textBoxExtendedSurveyFinishedTxt.Text = s.FinishedEmailBody;
							
//								if (!revert) {
//									labelExtendedSurvey.Text = R.Str(lid, "reminder.for", "Reminder for") + " <b>" + extendedSurvey + "</b> (<span style='font-size:9px;'>[x]" + R.Str(lid, "sent.last", "Last sent") + ": " + (s.EmailLastSent == null ? R.Str(lid, "never", "Never") : s.EmailLastSent.Value.ToString("yyyy-MM-dd")) + "</span>)";
//									labelExtendedSurveyFinished.Text = R.Str(lid, "thanks.mail", "Thank you mail for") + " <b>" + extendedSurvey + "</b> (<span style='font-size:9px;'>[x]" + R.Str(lid, "sent.last", "Last sent") + ": " + (s.FinishedLastSent == null ? R.Str(lid, "never", "Never") : s.FinishedLastSent.Value.ToString("yyyy-MM-dd")) + "</span>)";
//								}
//							}
//							sponsorExtendedSurveyID = s.Id;
//							sponsorAdminExtendedSurveyID = s.ExtraExtendedSurveyId;
//							
							found = true;
//
//							if (!postBack && !revert) {
//								var count = service.CountBySponsorExtendedSurveyReminderRecipients(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
//								labelExtendedSurvey.Text = labelExtendedSurvey.Text.Replace("[x]", "[x]" + R.Str(lid, "recipients", "Recipients") + ": " + count + ", ");
//
//								count = service.CountThankYouMessageRecipients(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
//								labelExtendedSurveyFinished.Text = labelExtendedSurveyFinished.Text.Replace("[x]", "[x]" + R.Str(lid, "recipients", "Recipients") + ": " + count + ", ");
//							}
//						} else {
//							HtmlHelper.SetTextIfEmpty(textBoxExtendedSurveyTxt, s.EmailBody);
//							HtmlHelper.SetTextIfEmpty(textBoxExtendedSurveySubject, s.EmailSubject);
//							HtmlHelper.SetTextIfEmpty(textBoxExtendedSurveyFinishedTxt, s.FinishedEmailBody);
//							HtmlHelper.SetTextIfEmpty(textBoxExtendedSurveyFinishedSubject, s.FinishedEmailSubject);
						}
					}
					seen.Add(s.ExtraExtendedSurveyId != 0 ? s.ExtraExtendedSurveyId : s.Id);
				}
			}
			}
			return surveys;
		}
		
		public IList<User> FindBySponsorWithExtendedSurvey2(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			return userRepository.FindBySponsorWithExtendedSurvey2(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
		}
		
		public IList<User> Find2(int sponsorID, int sponsorAdminID)
		{
			return userRepository.Find2(sponsorID, sponsorAdminID);
		}
		
		public IList<User> FindBySponsorWithExtendedSurvey(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			return userRepository.FindBySponsorWithExtendedSurvey(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
		}
		
		public void UpdateEmailFailure(int userID)
		{
			userRepository.UpdateEmailFailure(userID);
		}
		
		public void UpdateLastReminderSent(int userID)
		{
			userRepository.UpdateLastReminderSent(userID);
		}
		
		public IList<SponsorInvite> FindSentInvitesBySponsor(int sponsorID, int sponsorAdminID)
		{
			return sponsorRepository.FindSentInvitesBySponsor(sponsorID, sponsorAdminID);
		}
		
		public IList<SponsorInvite> FindInvitesBySponsor(int sponsorID, int sponsorAdminID)
		{
			return sponsorRepository.FindInvitesBySponsor(sponsorID, sponsorAdminID);
		}
		
		public IList<User> FindBySponsorWithLoginDays(int sponsorID, int sponsorAdminID, int selectedValue)
		{
			return userRepository.FindBySponsorWithLoginDays(sponsorID, sponsorAdminID, selectedValue);
		}
		
		public void UpdateExtendedSurveyLastFinishedSent(int sponsorExtendedSurveyID, int sponsorAdminExtendedSurveyID)
		{
			extendedSurveyRepository.UpdateExtendedSurveyLastFinishedSent(sponsorAdminExtendedSurveyID != 0 ? sponsorAdminExtendedSurveyID : sponsorExtendedSurveyID);
		}
		
		public void UpdateLastAllMessageSent(int sponsorID, int sponsorAdminID)
		{
			extendedSurveyRepository.UpdateLastAllMessageSent(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);
		}
		
		public void UpdateSponsorLastInviteSent(int sponsorID, int sponsorAdminID)
		{
			extendedSurveyRepository.UpdateSponsorLastInviteSent(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);
		}
		
		public void UpdateSponsorLastInviteReminderSent(int sponsorID, int sponsorAdminID) {
			extendedSurveyRepository.UpdateSponsorLastInviteReminderSent(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);
		}
		
		public void UpdateSponsorLastLoginSent(int sponsorID, int sponsorAdminID)
		{
			extendedSurveyRepository.UpdateSponsorLastLoginSent(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);
		}
		
		public void UpdateExtendedSurveyLastEmailSent(int sponsorExtendedSurveyID, int sponsorAdminExtendedSurveyID)
		{
			extendedSurveyRepository.UpdateExtendedSurveyLastEmailSent(sponsorAdminExtendedSurveyID != 0 ? sponsorAdminExtendedSurveyID : sponsorExtendedSurveyID);
		}
	}
}
