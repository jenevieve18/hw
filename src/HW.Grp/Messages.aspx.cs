using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;
using HW.Core.Services;

namespace HW.Grp
{
	public partial class Messages : System.Web.UI.Page
	{
		int sponsorID = 0;
		int sponsorAdminID = 0;
		int sponsorExtendedSurveyID = 0;
		int sponsorAdminExtendedSurveyID;
		bool incorrectPassword = false;
		bool sent = false;
		bool loginWithSkey = false;
		IAdmin sponsor;
		SqlUserRepository userRepository = new SqlUserRepository();
//		protected int lid = Language.ENGLISH;
		protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);
		
//		protected int lid;
		
		MessageService service;
		
		int SponsorAndSponsorAdminID {
			get {
				int x = ConvertHelper.ToInt32(Session["SponsorAdminID"], -1);
				if (x != -1) {
					return x;
				} else {
					return ConvertHelper.ToInt32(Session["SponsorID"]);
				}
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["SponsorID"] == null, "default.aspx", true);
			
			sponsorID = ConvertHelper.ToInt32(Session["SponsorID"]);
			sponsorAdminID = ConvertHelper.ToInt32(Session["SponsorAdminID"], -1);
			
			HtmlHelper.RedirectIf(!new SqlSponsorAdminRepository().SponsorAdminHasAccess(sponsorAdminID, ManagerFunction.Messages), "default.aspx", true);
			
//			lid = ConvertHelper.ToInt32(Session["lid"], 2);
			var userSession = userRepository.ReadUserSession(Request.UserHostAddress, Request.UserAgent);
			if (userSession != null) {
				lid = userSession.Lang;
			}
			loginWithSkey = Session["SponsorKey"] != null;
			
			buttonSave.Click += new EventHandler(SaveClick);
			buttonSend.Click += new EventHandler(SendClick);
			buttonRevert.Click += new EventHandler(RevertClick);

			IExtendedSurveyRepository repository = ExtendedSurveyRepositoryFactory.CreateRepository(sponsorAdminID);
			service = new MessageService(repository, new SqlSponsorRepository(), new SqlUserRepository(), new SqlProjectRepository());
			
			service.SaveSponsorAdminSessionFunction(ConvertHelper.ToInt32(Session["SponsorAdminSessionID"]), ManagerFunction.Messages, DateTime.Now);

			sent = (Request.QueryString["Sent"] != null);
			
			if (!IsPostBack) {
				PopulateDropDownLists();
			}
			LoadMessages(service.ReadSponsor(SponsorAndSponsorAdminID), service.FindExtendedSurveysBySponsorAdmin(sponsorID, sponsorAdminID), IsPostBack, false);
		}
		
		void LoadMessages(IAdmin sponsor, IList<IExtendedSurvey> surveys, bool postBack, bool revert)
		{
			SetSponsor(sponsor, postBack, revert);
			SetExtendedSurveys(surveys, postBack, revert);
		}
		
		public void SetSponsor(IAdmin sponsor, bool postBack, bool revert)
		{
			this.sponsor = sponsor;
			if (!postBack) {
				var u = service.CountAllActivatedUsersRecipients(sponsorID, sponsorAdminID);
				labelAllMessageLastSent.Text = R.Str(lid, "recipients", "Recipients") + ": " + u + ", ";
				
				if (sponsor != null) {
					textBoxInviteTxt.Text = sponsor.InviteText;
					textBoxInviteSubject.Text = sponsor.InviteSubject;
					
					textBoxInviteReminderTxt.Text = sponsor.InviteReminderText;
					textBoxInviteReminderSubject.Text = sponsor.InviteReminderSubject;
					
					textBoxAllMessageSubject.Text = sponsor.AllMessageSubject;
					textBoxAllMessageBody.Text = sponsor.AllMessageBody;

					textBoxLoginTxt.Text = sponsor.LoginText;
					textBoxLoginSubject.Text = sponsor.LoginSubject;

					dropDownLoginDays.SelectedValue = (sponsor.LoginDays <= 0 ? "14" : sponsor.LoginDays.ToString());
					dropDownLoginWeekday.SelectedValue = (sponsor.LoginWeekday <= -1 ? "NULL" : sponsor.LoginWeekday.ToString());

					if (!revert) {
						labelInviteLastSent.Text = (sponsor.InviteLastSent ==  null ? R.Str(lid, "never", "Never") : sponsor.InviteLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
						labelInviteReminderLastSent.Text = (sponsor.InviteReminderLastSent == null ? R.Str(lid, "never", "Never") : sponsor.InviteReminderLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
						labelAllMessageLastSent.Text += R.Str(lid, "sent.last", "Last sent") + ": " + (sponsor.AllMessageLastSent == null ? R.Str(lid, "never", "Never") : sponsor.AllMessageLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
						labelLoginLastSent.Text = (sponsor.LoginLastSent == null ? R.Str(lid, "never", "Never") : sponsor.LoginLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
					}
				}
			}
		}
		
		public void SetExtendedSurveys(IList<IExtendedSurvey> surveys, bool postBack, bool revert)
		{
			int projectRoundID = 0;
			string extendedSurvey = "";
			bool found = false;
			IList<int> seen = new List<int>();
			foreach (var s in surveys) {
				if (!seen.Contains(s.ExtraExtendedSurveyId != 0 ? s.ExtraExtendedSurveyId : s.Id)) {
					if (s.ProjectRound != null) {
						if (!found) {
							projectRoundID = s.ProjectRound.Id;
							if (!postBack) {
								extendedSurvey = s.Internal + s.RoundText;
								
								textBoxExtendedSurveySubject.Text = s.EmailSubject;
								textBoxExtendedSurveyTxt.Text = s.EmailBody;

								textBoxExtendedSurveyFinishedSubject.Text = s.FinishedEmailSubject;
								textBoxExtendedSurveyFinishedTxt.Text = s.FinishedEmailBody;

								if (!revert) {
									labelExtendedSurvey.Text = R.Str(lid, "reminder.for", "Reminder for") + " <b>" + extendedSurvey + "</b> (<span style='font-size:9px;'>[x]" + R.Str(lid, "sent.last", "Last sent") + ": " + (s.EmailLastSent == null ? R.Str(lid, "never", "Never") : s.EmailLastSent.Value.ToString("yyyy-MM-dd")) + "</span>)";
									labelExtendedSurveyFinished.Text = R.Str(lid, "thanks.mail", "Thank you mail for") + " <b>" + extendedSurvey + "</b> (<span style='font-size:9px;'>[x]" + R.Str(lid, "sent.last", "Last sent") + ": " + (s.FinishedLastSent == null ? R.Str(lid, "never", "Never") : s.FinishedLastSent.Value.ToString("yyyy-MM-dd")) + "</span>)";
								}
							}
							sponsorExtendedSurveyID = s.Id;
							sponsorAdminExtendedSurveyID = s.ExtraExtendedSurveyId;
							
							found = true;

							if (!postBack && !revert) {
								var count = service.CountBySponsorExtendedSurveyReminderRecipients(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
								labelExtendedSurvey.Text = labelExtendedSurvey.Text.Replace("[x]", "[x]" + R.Str(lid, "recipients", "Recipients") + ": " + count + ", ");

								count = service.CountThankYouMessageRecipients(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
								labelExtendedSurveyFinished.Text = labelExtendedSurveyFinished.Text.Replace("[x]", "[x]" + R.Str(lid, "recipients", "Recipients") + ": " + count + ", ");
							}
						} else {
							HtmlHelper.SetTextIfEmpty(textBoxExtendedSurveyTxt, s.EmailBody);
							HtmlHelper.SetTextIfEmpty(textBoxExtendedSurveySubject, s.EmailSubject);
							HtmlHelper.SetTextIfEmpty(textBoxExtendedSurveyFinishedTxt, s.FinishedEmailBody);
							HtmlHelper.SetTextIfEmpty(textBoxExtendedSurveyFinishedSubject, s.FinishedEmailSubject);
						}
					}
					seen.Add(s.ExtraExtendedSurveyId != 0 ? s.ExtraExtendedSurveyId : s.Id);
				}
			}
			if (projectRoundID != 0) {
				ProjectRound r = service.ReadRound(projectRoundID);
				if (r != null) {
					if (!postBack && !revert) {
						if (!revert) {
							labelExtendedSurvey.Text = labelExtendedSurvey.Text.Replace("[x]", R.Str(lid, "period", "Period") + ": " + r.ToPeriodString() + ", ");
							labelExtendedSurveyFinished.Text = labelExtendedSurveyFinished.Text.Replace("[x]", R.Str(lid, "period", "Period") + ": " + r.ToPeriodString() + ", ");
						}
						if (r.IsOpen) {
							textBoxExtendedSurveySubject.Visible = labelExtendedSurvey.Visible = textBoxExtendedSurveyTxt.Visible = true;
							dropDownSendType.Items.Add(new ListItem(R.Str(lid, "reminder", "Reminder: ") + extendedSurvey, "4"));
						}
					}
				}

				if (!labelExtendedSurvey.Visible && labelExtendedSurveyFinished.Text != "" && !postBack && !revert) {
					textBoxExtendedSurveyFinishedSubject.Visible = labelExtendedSurveyFinished.Visible = textBoxExtendedSurveyFinishedTxt.Visible = true;
					dropDownSendType.Items.Add(new ListItem(R.Str(lid, "thanks", "Thank you: ") + extendedSurvey, "5"));
				}
			}
		}
		
		void PopulateDropDownLists()
		{
			dropDownSendType.Items.Clear();
			dropDownSendType.Items.Add(new ListItem(R.Str(lid, "select.send.type", "< select send type >"), "0"));
			dropDownSendType.Items.Add(new ListItem(R.Str(lid, "registration", "Registration"), "1"));
			dropDownSendType.Items.Add(new ListItem(R.Str(lid, "registration.reminder", "Registration reminder"), "2"));
			dropDownSendType.Items.Add(new ListItem(R.Str(lid, "login.reminder", "Login reminder"), "3"));
			dropDownSendType.Items.Add(new ListItem(R.Str(lid, "users.activated.all", "All activated users"), "9"));
			
			dropDownLoginDays.Items.Clear();
			dropDownLoginDays.Items.Add(new ListItem(R.Str(lid, "day.everyday", "every day"), "1"));
			dropDownLoginDays.Items.Add(new ListItem(R.Str(lid, "week", "week"), "7"));
			dropDownLoginDays.Items.Add(new ListItem(R.Str(lid, "week.two", "2 weeks"), "14"));
			dropDownLoginDays.Items.Add(new ListItem(R.Str(lid, "month", "month"), "30"));
			dropDownLoginDays.Items.Add(new ListItem(R.Str(lid, "month.three", "3 months"), "90"));
			dropDownLoginDays.Items.Add(new ListItem(R.Str(lid, "month.six", "6 months"), "100"));
			
			dropDownLoginWeekday.Items.Clear();
			dropDownLoginWeekday.Items.Add(new ListItem(R.Str(lid, "week.disabled", "< disabled >"), "NULL"));
			dropDownLoginWeekday.Items.Add(new ListItem(R.Str(lid, "week.everyday", "< every day >"), "0"));
			dropDownLoginWeekday.Items.Add(new ListItem(R.Str(lid, "week.monday", "Monday"), "1"));
			dropDownLoginWeekday.Items.Add(new ListItem(R.Str(lid, "week.tuesday", "Tuesday"), "2"));
			dropDownLoginWeekday.Items.Add(new ListItem(R.Str(lid, "week.wednesday", "Wednesday"), "3"));
			dropDownLoginWeekday.Items.Add(new ListItem(R.Str(lid, "week.thursday", "Thursday"), "4"));
			dropDownLoginWeekday.Items.Add(new ListItem(R.Str(lid, "week.friday", "Friday"), "5"));
			
			textBoxLoginSubject.Enabled = textBoxLoginTxt.Enabled = dropDownLoginDays.Enabled = dropDownLoginWeekday.Enabled = loginWithSkey;
		}

		int Save()
		{
			service.UpdateInviteTexts(SponsorAndSponsorAdminID, textBoxInviteSubject.Text, textBoxInviteTxt.Text, textBoxInviteReminderSubject.Text, textBoxInviteReminderTxt.Text, textBoxAllMessageSubject.Text, textBoxAllMessageBody.Text);
			if (textBoxLoginTxt.Enabled) {
				service.Update(textBoxLoginSubject.Text, textBoxLoginTxt.Text, Convert.ToInt32(dropDownLoginDays.SelectedValue), ConvertHelper.ToInt32(dropDownLoginWeekday.SelectedValue, -1), sponsorID);
			}

			if ((textBoxExtendedSurveyFinishedSubject.Visible || textBoxExtendedSurveySubject.Visible) && sponsorExtendedSurveyID != 0) {
				sponsorAdminExtendedSurveyID = service.UpdateEmailTexts(sponsorExtendedSurveyID, sponsorAdminID, sponsorAdminExtendedSurveyID, textBoxExtendedSurveySubject.Text, textBoxExtendedSurveyTxt.Text, textBoxExtendedSurveyFinishedSubject.Text, textBoxExtendedSurveyFinishedTxt.Text);
			}
			return sponsorAdminExtendedSurveyID;
		}

		void RevertClick(object sender, EventArgs e)
		{
			var r = new SqlSponsorRepository();
			LoadMessages(r.ReadSponsor(sponsorID), r.FindExtendedSurveysBySponsorAdmin(sponsorID, sponsorAdminID), false, true);
		}

		void SaveClick(object sender, EventArgs e)
		{
			Save();

			Response.Redirect(
				string.Format("messages.aspx?Rnd={0}", (new Random(unchecked((int)DateTime.Now.Ticks))).Next()),
				true
			);
		}

		void SendClick(object sender, EventArgs e)
		{
			sponsorAdminExtendedSurveyID = Save();

			if (dropDownSendType.SelectedIndex != -1) {
				bool valid = (Session["SponsorAdminID"].ToString() == "-1");
				if (!valid) {
					var a = service.ReadSponsorAdmin(sponsorID, sponsorAdminID, textBoxPassword.Text);
					if (a != null && a.Id == sponsorAdminID) {
						valid = true;
					} else {
						incorrectPassword = true;
					}
				}

				if (valid) {
					int sendType = ConvertHelper.ToInt32(dropDownSendType.SelectedValue);
					MessageSendType t = MessageSendType.CreateSendType(service, sendType);
					if (t != null) {
						t.Message = CreateMessage(sendType);
						t.Send(sponsorID, sponsorAdminID);
						
						Response.Redirect("messages.aspx?Sent=" + t.Message.Sent + "&Fail=" + t.Message.Failed + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
					}
				}
			}
		}
		
		Message CreateMessage(int sendType)
		{
			switch (sendType) {
				case MessageSendType.RegistrationSendType:
					return new Message { Subject = textBoxInviteSubject.Text, Body = textBoxInviteTxt.Text };
				case MessageSendType.RegistrationReminderSendType:
					return new Message { Subject = textBoxInviteReminderSubject.Text, Body = textBoxInviteReminderTxt.Text };
				case MessageSendType.LoginReminderSendType:
					return new LoginReminderMessage { From = sponsor.EmailFrom, Subject = textBoxLoginSubject.Text, Body = textBoxLoginTxt.Text, LoginDays = ConvertHelper.ToInt32(dropDownLoginDays.SelectedValue) };
				case MessageSendType.ExtendedSurveySendType:
					return new ExtendedSurveyMessage { From = sponsor.EmailFrom, Subject = textBoxExtendedSurveySubject.Text, Body = textBoxExtendedSurveyTxt.Text, SponsorExtendedSurveyID = sponsorExtendedSurveyID, SponsorAdminExtendedSurveyID = sponsorAdminExtendedSurveyID };
				case MessageSendType.ThankYouSendType:
					return new ExtendedSurveyMessage { From = sponsor.EmailFrom, Subject = textBoxExtendedSurveyFinishedSubject.Text, Body = textBoxExtendedSurveyFinishedTxt.Text, SponsorExtendedSurveyID = sponsorExtendedSurveyID, SponsorAdminExtendedSurveyID = sponsorAdminExtendedSurveyID };
				case MessageSendType.AllActivatedUsersSendType:
					return new Message { Subject = textBoxAllMessageSubject.Text, Body = textBoxAllMessageBody.Text };
				default:
					return null;
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			
			buttonSave.Text = R.Str(lid, "save", "Save");
			buttonSend.Text = R.Str(lid, "send", "Send");
			buttonRevert.Text = R.Str(lid, "revert.default", "Revert to default");

			if (incorrectPassword) {
				string script = string.Format(
					"<script language='JavaScript'>alert('{0}');</script>",
					R.Str(lid, "password.incorrect", "Incorrect password!")
				);
				ClientScript.RegisterStartupScript(this.GetType(), "ERROR", script);
			}
			if (sent && !IsPostBack) {
				string script = string.Format(
					"<script language='JavaScript'>alert('{0} {3}\\r\\n{1} {2}');</script>",
					Request.QueryString["Sent"].ToString(),
					Request.QueryString["Fail"].ToString(),
					R.Str(lid, "email.incorrect", "incorrect email address(es) found."),
					R.Str(lid, "message.sent", "messages successfully sent.")
				);
				ClientScript.RegisterStartupScript(this.GetType(), "SENT", script);
			}
		}
	}
}