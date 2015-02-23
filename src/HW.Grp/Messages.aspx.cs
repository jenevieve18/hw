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
//		ISponsor sponsor;
		bool loginWithSkey = false;
		
		protected int lid;
		
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
			
			lid = ConvertHelper.ToInt32(Session["lid"], 1);
			loginWithSkey = Session["SponsorKey"] != null;
			
			buttonSave.Click += new EventHandler(SaveClick);
			buttonSend.Click += new EventHandler(SendClick);
			buttonRevert.Click += new EventHandler(RevertClick);

			IExtendedSurveyRepository repository = ExtendedSurveyRepositoryFactory.CreateRepository(sponsorAdminID);
			service = new MessageService(repository, new SqlSponsorRepository(), new SqlUserRepository(), new SqlProjectRepository());
			
			service.SaveSponsorAdminSessionFunction(ConvertHelper.ToInt32(Session["SponsorAdminSessionID"]), ManagerFunction.Messages, DateTime.Now);

			if (sponsorID != 0) {
				sent = (Request.QueryString["Sent"] != null);
				
				if (!IsPostBack) {
					PopulateDropDownLists();
					Sponsor = service.ReadSponsor(SponsorAndSponsorAdminID);
					ExtendedSurveys = service.FindExtendedSurveysBySponsorAdmin(sponsorID, sponsorAdminID);
				}
			} else {
				Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			}
		}
		
		public ISponsor Sponsor {
			set {
				var sponsor = value;
				
				var u = service.a(sponsorID, sponsorAdminID);
				AllMessageLastSent.Text = R.Str(lid, "recipients", "Recipients") + ": " + u + ", ";
				
				if (sponsor != null) {
					InviteTxt.Text = sponsor.InviteText;
					InviteSubject.Text = sponsor.InviteSubject;
					
					InviteReminderTxt.Text = sponsor.InviteReminderText;
					InviteReminderSubject.Text = sponsor.InviteReminderSubject;
					
					AllMessageSubject.Text = sponsor.AllMessageSubject;
					AllMessageBody.Text = sponsor.AllMessageBody;

					LoginTxt.Text = sponsor.LoginText;
					LoginSubject.Text = sponsor.LoginSubject;

					LoginDays.SelectedValue = (sponsor.LoginDays <= 0 ? "14" : sponsor.LoginDays.ToString());
					LoginWeekday.SelectedValue = (sponsor.LoginWeekday <= -1 ? "NULL" : sponsor.LoginWeekday.ToString());

					InviteLastSent.Text = (sponsor.InviteLastSent ==  null ? R.Str(lid, "never", "Never") : sponsor.InviteLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
					InviteReminderLastSent.Text = (sponsor.InviteReminderLastSent == null ? R.Str(lid, "never", "Never") : sponsor.InviteReminderLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
					AllMessageLastSent.Text += R.Str(lid, "sent.last", "Last sent") + ": " + (sponsor.AllMessageLastSent == null ? R.Str(lid, "never", "Never") : sponsor.AllMessageLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
					LoginLastSent.Text = (sponsor.LoginLastSent == null ? R.Str(lid, "never", "Never") : sponsor.LoginLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
				}
			}
		}
		
		void PopulateDropDownLists()
		{
			SendType.Items.Add(new ListItem(R.Str(lid, "select.send.type", "< select send type >"), "0"));
			SendType.Items.Add(new ListItem(R.Str(lid, "registration", "Registration"), "1"));
			SendType.Items.Add(new ListItem(R.Str(lid, "registration.reminder", "Registration reminder"), "2"));
			SendType.Items.Add(new ListItem(R.Str(lid, "login.reminder", "Login reminder"), "3"));
			SendType.Items.Add(new ListItem(R.Str(lid, "users.activated.all", "All activated users"), "9"));
			
			LoginDays.Items.Add(new ListItem(R.Str(lid, "day.everyday", "every day"), "1"));
			LoginDays.Items.Add(new ListItem(R.Str(lid, "week", "week"), "7"));
			LoginDays.Items.Add(new ListItem(R.Str(lid, "week.two", "2 weeks"), "14"));
			LoginDays.Items.Add(new ListItem(R.Str(lid, "month", "month"), "30"));
			LoginDays.Items.Add(new ListItem(R.Str(lid, "month.three", "3 months"), "90"));
			LoginDays.Items.Add(new ListItem(R.Str(lid, "month.six", "6 months"), "100"));
			
			LoginWeekday.Items.Add(new ListItem(R.Str(lid, "week.disabled", "< disabled >"), "NULL"));
			LoginWeekday.Items.Add(new ListItem(R.Str(lid, "week.everyday", "< every day >"), "0"));
			LoginWeekday.Items.Add(new ListItem(R.Str(lid, "week.monday", "Monday"), "1"));
			LoginWeekday.Items.Add(new ListItem(R.Str(lid, "week.tuesday", "Tuesday"), "2"));
			LoginWeekday.Items.Add(new ListItem(R.Str(lid, "week.wednesday", "Wednesday"), "3"));
			LoginWeekday.Items.Add(new ListItem(R.Str(lid, "week.thursday", "Thursday"), "4"));
			LoginWeekday.Items.Add(new ListItem(R.Str(lid, "week.friday", "Friday"), "5"));
			
			LoginSubject.Enabled = LoginTxt.Enabled = LoginDays.Enabled = LoginWeekday.Enabled = loginWithSkey;
		}
		
		public IList<IExtendedSurvey> ExtendedSurveys
		{
			set {
				int projectRoundID = 0;
				string extendedSurvey = "";
				bool found = false;
				IList<int> seen = new List<int>();
				foreach (var s in value) {
					if (!seen.Contains(s.Id)) {
						if (s.ProjectRound != null) {
							if (!found) {
								projectRoundID = s.ProjectRound.Id;
								extendedSurvey = s.Internal + s.RoundText;
								
								ExtendedSurvey.Text = R.Str(lid, "reminder.for", "Reminder for") + " <b>" + extendedSurvey + "</b> (<span style='font-size:9px;'>[x]" + R.Str(lid, "sent.last", "Last sent") + ": " + (s.EmailLastSent == null ? R.Str(lid, "never", "Never") : s.EmailLastSent.Value.ToString("yyyy-MM-dd")) + "</span>)";
								ExtendedSurveySubject.Text = s.EmailSubject;
								ExtendedSurveyTxt.Text = s.EmailBody;

								ExtendedSurveyFinished.Text = R.Str(lid, "thanks.mail", "Thank you mail for") + " <b>" + extendedSurvey + "</b> (<span style='font-size:9px;'>[x]" + R.Str(lid, "sent.last", "Last sent") + ": " + (s.EmailLastSent == null ? R.Str(lid, "never", "Never") : s.EmailLastSent.Value.ToString("yyyy-MM-dd")) + "</span>)";
								ExtendedSurveyFinishedSubject.Text = s.FinishedEmailSubject;
								ExtendedSurveyFinishedTxt.Text = s.FinishedEmailBody;
								
								sponsorExtendedSurveyID = s.Id;
								sponsorAdminExtendedSurveyID = s.ExtraExtendedSurveyId;
								
								found = true;

								var count = service.CountBySponsorWithAdminAndExtendedSurvey2(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
								ExtendedSurvey.Text = ExtendedSurvey.Text.Replace("[x]", "[x]" + R.Str(lid, "recipients", "Recipients") + ": " + count + ", ");

								count = service.CountBySponsorWithAdminAndExtendedSurvey(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
								ExtendedSurveyFinished.Text = ExtendedSurveyFinished.Text.Replace("[x]", "[x]" + R.Str(lid, "recipients", "Recipients") + ": " + count + ", ");
							} else {
								HtmlHelper.SetTextIfEmpty(ExtendedSurveyTxt, s.EmailBody);
								HtmlHelper.SetTextIfEmpty(ExtendedSurveySubject, s.EmailSubject);
								HtmlHelper.SetTextIfEmpty(ExtendedSurveyFinishedTxt, s.FinishedEmailBody);
								HtmlHelper.SetTextIfEmpty(ExtendedSurveyFinishedSubject, s.FinishedEmailSubject);
							}
						}
						seen.Add(s.Id);
					}
				}
				if (projectRoundID != 0) {
					ProjectRound r = service.ReadRound(projectRoundID);
					if (r != null) {
						ExtendedSurvey.Text = ExtendedSurvey.Text.Replace("[x]", R.Str(lid, "period", "Period") + ": " + r.ToPeriodString() + ", ");
						ExtendedSurveyFinished.Text = ExtendedSurveyFinished.Text.Replace("[x]", R.Str(lid, "period", "Period") + ": " + r.ToPeriodString() + ", ");
						if (r.IsOpen) {
							ExtendedSurveySubject.Visible = ExtendedSurvey.Visible = ExtendedSurveyTxt.Visible = true;
							SendType.Items.Add(new ListItem(R.Str(lid, "reminder", "Reminder: ") + extendedSurvey, "4"));
						}
					}

					if (!ExtendedSurvey.Visible && ExtendedSurveyFinished.Text != "") {
						ExtendedSurveyFinishedSubject.Visible = ExtendedSurveyFinished.Visible = ExtendedSurveyFinishedTxt.Visible = true;
						SendType.Items.Add(new ListItem(R.Str(lid, "thanks", "Thank you: ") + extendedSurvey, "5"));
					}
				}
			}
		}

		int Save()
		{
			service.UpdateInviteTexts(SponsorAndSponsorAdminID, InviteSubject.Text, InviteTxt.Text, InviteReminderSubject.Text, InviteReminderTxt.Text, AllMessageSubject.Text, AllMessageBody.Text);
			if (LoginTxt.Enabled) {
				service.Update(LoginSubject.Text, LoginTxt.Text, Convert.ToInt32(LoginDays.SelectedValue), ConvertHelper.ToInt32(LoginWeekday.SelectedValue, -1), sponsorID);
			}

			if ((ExtendedSurveyFinishedSubject.Visible || ExtendedSurveySubject.Visible) && sponsorExtendedSurveyID != 0) {
				sponsorAdminExtendedSurveyID = service.UpdateEmailTexts(sponsorExtendedSurveyID, sponsorAdminID, sponsorAdminExtendedSurveyID, ExtendedSurveySubject.Text, ExtendedSurveyTxt.Text, ExtendedSurveyFinishedSubject.Text, ExtendedSurveyFinishedTxt.Text);
			}
			return sponsorAdminExtendedSurveyID;
		}

		void RevertClick(object sender, EventArgs e)
		{
			var r = new SqlSponsorRepository();
			Sponsor = r.ReadSponsor(sponsorID);
			ExtendedSurveys = r.FindExtendedSurveysBySponsorAdmin(sponsorID, sponsorAdminID);
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
			sponsorExtendedSurveyID = Save();

			if (SendType.SelectedIndex != -1) {
				bool valid = (Session["SponsorAdminID"].ToString() == "-1");
				if (!valid) {
					var a = service.ReadSponsorAdmin(sponsorID, sponsorAdminID, Password.Text);
					if (a != null && a.Id == sponsorAdminID) {
						valid = true;
					} else {
						incorrectPassword = true;
					}
				}

				if (valid) {
					int sendType = ConvertHelper.ToInt32(SendType.SelectedValue);
					MessageSendType t = MessageSendType.CreateSendType(service, sendType);
					t.Message = CreateMessage(sendType);
					t.Send(sponsorID, sponsorAdminID);
					
					Response.Redirect("messages.aspx?Sent=" + t.Message.Sent + "&Fail=" + t.Message.Failed + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
				}
			}
		}
		
		Message CreateMessage(int sendType)
		{
			switch (sendType) {
				case MessageSendType.RegistrationSendType:
					return new Message { Subject = InviteSubject.Text, Body = InviteTxt.Text };
				case MessageSendType.RegistrationReminderSendType:
					return new Message { Subject = InviteReminderSubject.Text, Body = InviteReminderTxt.Text };
				case MessageSendType.LoginReminderSendType:
					return new LoginReminderMessage { Subject = LoginSubject.Text, Body = LoginTxt.Text, LoginDays = ConvertHelper.ToInt32(LoginDays.SelectedValue) };
				case MessageSendType.ExtendedSurveySendType:
					return new ExtendedSurveyMessage { Subject = ExtendedSurveyTxt.Text, Body = ExtendedSurveySubject.Text, SponsorExtendedSurveyID = sponsorExtendedSurveyID };
				case MessageSendType.ThankYouSendType:
					return new ExtendedSurveyMessage{ Subject = ExtendedSurveyFinishedSubject.Text, Body = ExtendedSurveyFinishedTxt.Text, SponsorExtendedSurveyID = sponsorExtendedSurveyID };
				case MessageSendType.AllActivatedUsersSendType:
					return new Message { Subject = AllMessageSubject.Text, Body = AllMessageBody.Text };
				default:
					throw new NotImplementedException();
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
		
		/*void DisplayMessages(ISponsor sponsor, IExtendedSurveyRepository repository, bool postBack, bool postBack2)
		{
			if (!postBack) {
				PopulateDropDownLists();
			}
			if (!postBack2) {
//				var u = userRepository.a(sponsorID, sponsorAdminID);
				var u = service.a(sponsorID, sponsorAdminID);
				AllMessageLastSent.Text = R.Str(lid, "recipients", "Recipients") + ": " + u + ", ";

				if (sponsor != null) {
					InviteTxt.Text = sponsor.InviteText;
					InviteSubject.Text = sponsor.InviteSubject;
					
					InviteReminderTxt.Text = sponsor.InviteReminderText;
					InviteReminderSubject.Text = sponsor.InviteReminderSubject;
					
					AllMessageSubject.Text = sponsor.AllMessageSubject;
					AllMessageBody.Text = sponsor.AllMessageBody;

					LoginTxt.Text = sponsor.LoginText;
					LoginSubject.Text = sponsor.LoginSubject;

					LoginDays.SelectedValue = (sponsor.LoginDays <= 0 ? "14" : sponsor.LoginDays.ToString());
					LoginWeekday.SelectedValue = (sponsor.LoginWeekday <= -1 ? "NULL" : sponsor.LoginWeekday.ToString());

					InviteLastSent.Text = (sponsor.InviteLastSent ==  null ? R.Str(lid, "never", "Never") : sponsor.InviteLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
					InviteReminderLastSent.Text = (sponsor.InviteReminderLastSent == null ? R.Str(lid, "never", "Never") : sponsor.InviteReminderLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
					AllMessageLastSent.Text += R.Str(lid, "sent.last", "Last sent") + ": " + (sponsor.AllMessageLastSent == null ? R.Str(lid, "never", "Never") : sponsor.AllMessageLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
					LoginLastSent.Text = (sponsor.LoginLastSent == null ? R.Str(lid, "never", "Never") : sponsor.LoginLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
				}
			}
			SetExtendedSurveys(repository, postBack, postBack2);
		}*/
		
		/*void SetExtendedSurveys(IExtendedSurveyRepository repository, bool postBack, bool postBack2)
		{
			int projectRoundID = 0;
			string extendedSurvey = "";
			bool found = false;
			ArrayList seen = new ArrayList();
			IList<IExtendedSurvey> surveys = repository.FindExtendedSurveysBySponsorAdmin(sponsorID, sponsorAdminID);
			if ((surveys != null && surveys.Count <= 0) || sponsorAdminID == -1) {
				surveys = sponsorRepository.FindExtendedSurveysBySponsorAdmin(sponsorID, sponsorAdminID);
			}
			foreach (var s in surveys) {
				if (!seen.Contains(s.Id)) {
					if (s.ProjectRound != null) {
						if (!found) {
							projectRoundID = s.ProjectRound.Id;
							if (!postBack2) {
								extendedSurvey = s.Internal + s.RoundText;
								ExtendedSurvey.Text = R.Str(lid, "reminder.for", "Reminder for") + " <b>" + extendedSurvey + "</b> (<span style='font-size:9px;'>[x]" + R.Str(lid, "sent.last", "Last sent") + ": " + (s.EmailLastSent == null ? R.Str(lid, "never", "Never") : s.EmailLastSent.Value.ToString("yyyy-MM-dd")) + "</span>)";
								ExtendedSurveyFinished.Text = "Thank you mail for <b>" + extendedSurvey + "</b> (<span style='font-size:9px;'>[x]" + R.Str(lid, "sent.last", "Last sent") + ": " + (s.EmailLastSent == null ? R.Str(lid, "never", "Never") : s.EmailLastSent.Value.ToString("yyyy-MM-dd")) + "</span>)";
								
								ExtendedSurveySubject.Text = s.EmailSubject;
								ExtendedSurveyTxt.Text = s.EmailBody;
								ExtendedSurveyFinishedSubject.Text = s.FinishedEmailSubject;
								ExtendedSurveyFinishedTxt.Text = s.FinishedEmailBody;
							}
							sponsorExtendedSurveyID = s.Id;
							sponsorAdminExtendedSurveyID = s.ExtraExtendedSurveyId;
							found = true;

							if (!postBack2) {
								var r = userRepository.CountBySponsorWithAdminAndExtendedSurvey2(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
								ExtendedSurvey.Text = ExtendedSurvey.Text.Replace("[x]", "[x]" + R.Str(lid, "recipients", "Recipients") + ": " + r + ", ");

								r = userRepository.CountBySponsorWithAdminAndExtendedSurvey(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
								ExtendedSurveyFinished.Text = ExtendedSurveyFinished.Text.Replace("[x]", "[x]" + R.Str(lid, "recipients", "Recipients") + ": " + r + ", ");
							}
						} else {
							if (ExtendedSurveyTxt.Text == "") {
								ExtendedSurveyTxt.Text = s.EmailBody;
							}
							if (ExtendedSurveySubject.Text == "") {
								ExtendedSurveySubject.Text = s.EmailSubject;
							}
							if (ExtendedSurveyFinishedTxt.Text == "") {
								ExtendedSurveyFinishedTxt.Text = s.FinishedEmailBody;
							}
							if (ExtendedSurveyFinishedSubject.Text == "") {
								ExtendedSurveyFinishedSubject.Text = s.FinishedEmailSubject;
							}
						}
					}
					seen.Add(s.Id);
				}
			}
			if (projectRoundID != 0) {
				ProjectRound r = projectRepository.ReadRound(projectRoundID);
				if (r != null) {
					if (!postBack2) {
						ExtendedSurvey.Text = ExtendedSurvey.Text.Replace("[x]", R.Str(lid, "period", "Period") + ": " + r.ToPeriodString() + ", ");
						ExtendedSurveyFinished.Text = ExtendedSurveyFinished.Text.Replace("[x]", R.Str(lid, "period", "Period") + ": " + r.ToPeriodString() + ", ");
					}
					if (r.IsOpen) {
						if (!postBack) {
							ExtendedSurveySubject.Visible = true;
							ExtendedSurvey.Visible = true;
							ExtendedSurveyTxt.Visible = true;
							SendType.Items.Add(new ListItem(R.Str(lid, "reminder", "Reminder: ") + extendedSurvey, "4"));
						}
					} else {
						projectRoundID = 0;
					}
				} else {
					projectRoundID = 0;
				}

				if (!ExtendedSurvey.Visible && ExtendedSurveyFinished.Text != "" && !postBack) {
					ExtendedSurveyFinishedSubject.Visible = true;
					ExtendedSurveyFinished.Visible = true;
					ExtendedSurveyFinishedTxt.Visible = true;
					SendType.Items.Add(new ListItem(R.Str(lid, "thanks", "Thank you: ") + extendedSurvey, "5"));
				}
			}
		}*/
		
		/*void lalala()
		{
			int cx = 0;
			int bx = 0;

			switch (ConvertHelper.ToInt32(SendType.SelectedValue)) {
				case 1:
					// Invite
					repository.UpdateSponsorLastInviteSent(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);

					foreach (var i in sponsorRepository.FindInvitesBySponsor(sponsorID, sponsorAdminID)) {
						bool success = Db.sendInvitation(i.Id, i.Email, InviteSubject.Text, InviteTxt.Text, i.InvitationKey);
						if (success) {
							cx++;
						} else {
							bx++;
						}
					}
					break;
				case 2:
					// Invite reminder
					repository.UpdateSponsorLastInviteReminderSent(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);

					foreach (var i in sponsorRepository.FindSentInvitesBySponsor(sponsorID, sponsorAdminID)) {
						bool success = Db.sendInvitation(i.Id, i.Email, InviteReminderSubject.Text, InviteReminderTxt.Text, i.InvitationKey);

						if (success) {
							cx++;
						} else {
							bx++;
						}
					}
					break;
				case 3:
					// Login reminder
					repository.UpdateSponsorLastLoginSent(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);

					int selectedValue = Convert.ToInt32(LoginDays.SelectedValue);
					foreach (var u in userRepository.FindBySponsorWithLoginDays(sponsorID, sponsorAdminID, selectedValue)) {
						bool success = false;
						bool badEmail = false;
						if (Db.isEmail(u.Email)) {
							try {
								string body = LoginTxt.Text;

								string path = ConfigurationManager.AppSettings["healthWatchURL"];
								string personalLink = "" + path + "";
								if (u.ReminderLink > 0) {
									personalLink += "/c/" + u.UserKey.ToLower() + u.Id.ToString();
								}
								if (body.IndexOf("<LINK/>") >= 0) {
									body = body.Replace("<LINK/>", personalLink);
								} else {
									body += "\r\n\r\n" + personalLink;
								}

								success = Db.sendMail(sponsor.EmailFrom, u.Email, LoginSubject.Text, body);

								if (success) {
									userRepository.UpdateLastReminderSent(u.Id);
								}
							} catch (Exception) {
								badEmail = true;
							}
						} else {
							badEmail = true;
						}
						if (badEmail) {
							userRepository.UpdateEmailFailure(u.Id);
						}

						if (success) {
							cx++;
						} else {
							bx++;
						}
					}
					break;
				case 4:
					// Extended survey
					repository.UpdateExtendedSurveyLastEmailSent(sponsorExtendedSurveyID != -1 ? sponsorAdminExtendedSurveyID : sponsorID);

					foreach (var u in userRepository.FindBySponsorWithExtendedSurvey2(sponsorID, sponsorAdminID, sponsorExtendedSurveyID)) {
						bool success = false;
						bool badEmail = false;
						if (Db.isEmail(u.Email)) {
							try {
								string body = ExtendedSurveyTxt.Text;

								string path = ConfigurationManager.AppSettings["healthWatchURL"];
								string personalLink = "" + path + "";
								if (u.ReminderLink > 0) {
									personalLink += "c/" + u.UserKey.ToLower() + u.Id.ToString();
								}
								if (body.IndexOf("<LINK/>") >= 0) {
									body = body.Replace("<LINK/>", personalLink);
								} else {
									body += "\r\n\r\n" + personalLink;
								}

								success = Db.sendMail(sponsor.EmailFrom, u.Email, ExtendedSurveySubject.Text, body);
							} catch (Exception) {
								badEmail = true;
							}
						} else {
							badEmail = true;
						}
						if (badEmail) {
							userRepository.UpdateEmailFailure(u.Id);
						}

						if (success) {
							cx++;
						} else {
							bx++;
						}
					}
					break;
				case 5:
					// Thank you: Extended survey
					repository.UpdateExtendedSurveyLastFinishedSent(sponsorExtendedSurveyID != -1 ? sponsorAdminExtendedSurveyID : sponsorID);

					foreach (var u in userRepository.FindBySponsorWithExtendedSurvey(sponsorID, sponsorAdminID, sponsorExtendedSurveyID)) {
						bool success = false;
						bool badEmail = false;
						if (Db.isEmail(u.Email)) {
							try {
								string body = ExtendedSurveyFinishedTxt.Text;

								string path = ConfigurationManager.AppSettings["healthWatchURL"];
								string personalLink = "" + path + "";
								if (u.ReminderLink > 0) {
									personalLink += "c/" + u.UserKey.ToLower() + u.Id.ToString();
								}
								if (body.IndexOf("<LINK/>") >= 0) {
									body = body.Replace("<LINK/>", personalLink);
								} else {
									body += "\r\n\r\n" + personalLink;
								}

								success = Db.sendMail(sponsor.EmailFrom, u.Email, ExtendedSurveyFinishedSubject.Text, body);
							} catch (Exception) {
								badEmail = true;
							}
						} else {
							badEmail = true;
						}
						if (badEmail) {
							userRepository.UpdateEmailFailure(u.Id);
						}

						if (success) {
							cx++;
						} else {
							bx++;
						}
					}
					break;
				case 9:
					// All activated
					repository.UpdateLastAllMessageSent(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);

					foreach (var u in userRepository.Find2(sponsorID, sponsorAdminID)) {
						bool success = false;
						bool badEmail = false;
						if (Db.isEmail(u.Email)) {
							try {
								success = Db.sendMail(u.Email, AllMessageSubject.Text, AllMessageBody.Text);
							} catch (Exception) {
								badEmail = true;
							}
						} else {
							badEmail = true;
						}
						if (badEmail) {
							userRepository.UpdateEmailFailure(u.Id);
						}

						if (success) {
							cx++;
						} else {
							bx++;
						}
					}
					break;
			}
			Response.Redirect("messages.aspx?Sent=" + cx + "&Fail=" + bx + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
		}*/
	}
}