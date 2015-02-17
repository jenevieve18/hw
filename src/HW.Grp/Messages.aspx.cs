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
		ISponsor sponsor;
		bool loginWithSkey = false;
		
		SqlUserRepository userRepository = new SqlUserRepository();
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		SqlProjectRepository projectRepository = new SqlProjectRepository();
		protected int lid;
		IExtendedSurveyRepository repository;

		protected void Page_Load(object sender, EventArgs e)
		{
			sponsorID = ConvertHelper.ToInt32(Session["SponsorID"]);
			sponsorAdminID = ConvertHelper.ToInt32(Session["SponsorAdminID"], -1);
			lid = ConvertHelper.ToInt32(Session["lid"], 1);
			loginWithSkey = Session["SponsorKey"] != null;

			sponsorRepository.SaveSponsorAdminSessionFunction(Convert.ToInt32(Session["SponsorAdminSessionID"]), ManagerFunction.Messages, DateTime.Now);
			Save.Click += new EventHandler(Save_Click);
			Send.Click += new EventHandler(Send_Click);
			buttonRevert.Click += new EventHandler(RevertClick);
			
			repository = sponsorAdminID != -1 ? new SqlSponsorAdminRepository() as IExtendedSurveyRepository : new SqlSponsorRepository() as IExtendedSurveyRepository;

			if (sponsorID != 0) {
				sent = (Request.QueryString["Sent"] != null);
				
				sponsor = repository.ReadSponsor(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);
				DisplayMessages(sponsor, repository, IsPostBack);
			} else {
				Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			}
		}
		
		void DisplayMessages(ISponsor sponsor, IExtendedSurveyRepository repository, bool postBack)
		{
			if (!postBack) {
				SendType.Items.Add(new ListItem(R.Str(lid, "select.send.type", "< select send type >"), "0"));
				SendType.Items.Add(new ListItem(R.Str(lid, "registration", "Registration"), "1"));
				SendType.Items.Add(new ListItem(R.Str(lid, "registration.reminder", "Registration reminder"), "2"));
				SendType.Items.Add(new ListItem(R.Str(lid, "login.reminder", "Login reminder"), "3"));
				SendType.Items.Add(new ListItem(R.Str(lid, "users.activated.all", "All activated users"), "9"));
				
				LoginSubject.Enabled = LoginTxt.Enabled = LoginDays.Enabled = LoginWeekday.Enabled = loginWithSkey;
				
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
				
				var u = userRepository.a(sponsorID, sponsorAdminID);
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
			#region SponsorExtendedSurvey
			int projectRoundId = 0;
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
							projectRoundId = s.ProjectRound.Id;
							if (!postBack) {
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

							if (!postBack) {
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
			if (projectRoundId != 0) {
				ProjectRound r = projectRepository.ReadRound(projectRoundId);
				if (r != null) {
					if (!postBack) {
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
						projectRoundId = 0;
					}
				} else {
					projectRoundId = 0;
				}

				if (!ExtendedSurvey.Visible && ExtendedSurveyFinished.Text != "" && !postBack) {
					ExtendedSurveyFinishedSubject.Visible = true;
					ExtendedSurveyFinished.Visible = true;
					ExtendedSurveyFinishedTxt.Visible = true;
					SendType.Items.Add(new ListItem(R.Str(lid, "thanks", "Thank you: ") + extendedSurvey, "5"));
				}
			}
			#endregion
		}

		int save()
		{
//			sponsorRepository.UpdateSponsor(
//				new Sponsor {
//					InviteSubject = sponsorAdmin != null ? sponsor.InviteSubject : InviteSubject.Text,
//					InviteText = sponsorAdmin != null ? sponsor.InviteText : InviteTxt.Text,
//					InviteReminderSubject = sponsorAdmin != null ? sponsor.InviteReminderSubject : InviteReminderSubject.Text,
//					InviteReminderText = sponsorAdmin != null ? sponsor.InviteReminderText : InviteReminderTxt.Text,
//					AllMessageSubject = sponsorAdmin != null ? sponsor.AllMessageSubject : AllMessageSubject.Text,
//					AllMessageBody = sponsorAdmin != null ? sponsor.AllMessageBody : AllMessageBody.Text,
//					LoginSubject = LoginSubject.Text,
//					LoginText = LoginTxt.Text,
//					LoginDays = Convert.ToInt32(LoginDays.SelectedValue),
//					LoginWeekday = ConvertHelper.ToInt32(LoginWeekday.SelectedValue, -1),
//					Id = sponsorID
//				}
//			);
//			if (sponsorAdmin != null) {
//				sponsorRepository.UpdateSponsorAdmin2(
//					new SponsorAdmin {
//						InviteSubject = InviteSubject.Text,
//						InviteText = InviteTxt.Text,
//						InviteReminderSubject = InviteReminderSubject.Text,
//						InviteReminderText = InviteReminderTxt.Text,
//						AllMessageSubject = AllMessageSubject.Text,
//						AllMessageBody = AllMessageBody.Text,
//						Id = sponsorAdminID
//					}
//				);
//			}
			repository.UpdateInviteTexts(sponsorAdminID != -1 ? sponsorAdminID : sponsorID, InviteSubject.Text, InviteTxt.Text, InviteReminderSubject.Text, InviteReminderTxt.Text, AllMessageSubject.Text, AllMessageBody.Text);
			if (LoginTxt.Enabled) {
				sponsorRepository.Update(LoginSubject.Text, LoginTxt.Text, Convert.ToInt32(LoginDays.SelectedValue), ConvertHelper.ToInt32(LoginWeekday.SelectedValue, -1), sponsorID);
			}

			if ((ExtendedSurveyFinishedSubject.Visible || ExtendedSurveySubject.Visible) && sponsorExtendedSurveyID != 0) {
//				sponsorRepository.UpdateSponsorExtendedSurvey(
//					new SponsorExtendedSurvey {
//						EmailSubject = ExtendedSurveySubject.Text,
//						EmailBody = ExtendedSurveyTxt.Text,
//						FinishedEmailSubject = ExtendedSurveyFinishedSubject.Text,
//						FinishedEmailBody = ExtendedSurveyFinishedTxt.Text,
//						Id = sponsorExtendedSurveyID
//					}
//				);
//				if (sponsorAdmin !=  null) {
//					sponsorRepository.InsertSponsorAdminExtendedSurvey(
//						new SponsorAdminExtendedSurvey {
//							SponsorAdmin = new SponsorAdmin { Id = sponsorAdminID },
//							EmailSubject = ExtendedSurveySubject.Text,
//							EmailBody = ExtendedSurveyTxt.Text,
//							FinishedEmailSubject = ExtendedSurveyFinishedSubject.Text,
//							FinishedEmailBody = ExtendedSurveyFinishedTxt.Text,
//						}
//					);
//				}
				sponsorAdminExtendedSurveyID = repository.UpdateEmailTexts(sponsorExtendedSurveyID, sponsorAdminID, sponsorAdminExtendedSurveyID, ExtendedSurveySubject.Text, ExtendedSurveyTxt.Text, ExtendedSurveyFinishedSubject.Text, ExtendedSurveyFinishedTxt.Text);
			}
			return sponsorAdminExtendedSurveyID;
		}

		void RevertClick(object sender, EventArgs e)
		{
			DisplayMessages(sponsorRepository.ReadSponsor(sponsorID), new SqlSponsorRepository(), IsPostBack);
		}

		void Save_Click(object sender, EventArgs e)
		{
			save();

			Response.Redirect(
				string.Format("messages.aspx?Rnd={0}", (new Random(unchecked((int)DateTime.Now.Ticks))).Next()),
				true
			);
		}

		void Send_Click(object sender, EventArgs e)
		{
			sponsorExtendedSurveyID = save();

			if (SendType.SelectedIndex != -1) {
				bool valid = (Session["SponsorAdminID"].ToString() == "-1");
				if (!valid) {
//					int sponsorAdminId = Convert.ToInt32(Session["SponsorAdminID"]);
					var a = sponsorRepository.ReadSponsorAdmin(sponsorID, sponsorAdminID, Password.Text);
					if (a != null && a.Id == sponsorAdminID) {
						valid = true;
					} else {
						incorrectPassword = true;
					}
				}

				if (valid) {
					int cx = 0;
					int bx = 0;

					switch (ConvertHelper.ToInt32(SendType.SelectedValue)) {
						case 1:
							#region Invite
//							sponsorRepository.UpdateSponsorLastInviteSent(sponsorID);
//							repository.UpdateSponsorLastInviteSent(sponsorAdminID != -1 ? sponsorAdminExtendedSurveyID : sponsorID);
							repository.UpdateSponsorLastInviteSent(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);

//							int sponsorAdminId = Convert.ToInt32(Session["SponsorAdminID"]);
							foreach (var i in sponsorRepository.FindInvitesBySponsor(sponsorID, sponsorAdminID)) {
								bool success = Db.sendInvitation(i.Id, i.Email, InviteSubject.Text, InviteTxt.Text, i.InvitationKey);
								if (success) {
									cx++;
								} else {
									bx++;
								}
							}
							#endregion
							break;
						case 2:
							#region Invite reminder
//							sponsorRepository.UpdateSponsorLastInviteReminderSent(sponsorID);
//							repository.UpdateSponsorLastInviteReminderSent(sponsorAdminID != -1 ? sponsorAdminExtendedSurveyID : sponsorID);
							repository.UpdateSponsorLastInviteReminderSent(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);

//							sponsorAdminId = Convert.ToInt32(Session["SponsorAdminID"]);
							foreach (var i in sponsorRepository.FindSentInvitesBySponsor(sponsorID, sponsorAdminID)) {
								bool success = Db.sendInvitation(i.Id, i.Email, InviteReminderSubject.Text, InviteReminderTxt.Text, i.InvitationKey);

								if (success) {
									cx++;
								} else {
									bx++;
								}
							}
							#endregion
							break;
						case 3:
							#region Login reminder
//							sponsorRepository.UpdateSponsorLastLoginSent(sponsorID);
							repository.UpdateSponsorLastLoginSent(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);

//							sponsorAdminId = Convert.ToInt32(Session["SponsorAdminID"]);
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
							#endregion
							break;
						case 4:
							#region Extended survey
//							sponsorRepository.UpdateExtendedSurveyLastEmailSent(sponsorExtendedSurveyID);
							repository.UpdateExtendedSurveyLastEmailSent(sponsorExtendedSurveyID != -1 ? sponsorAdminExtendedSurveyID : sponsorID);

//							sponsorAdminId = Convert.ToInt32(Session["SponsorAdminID"]);
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

//										success = Db.sendMail(u.Email, ExtendedSurveySubject.Text, body);
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
							#endregion
							break;
						case 5:
							#region Thank you: Extended survey
//							sponsorRepository.UpdateExtendedSurveyLastFinishedSent(sponsorExtendedSurveyID);
							repository.UpdateExtendedSurveyLastFinishedSent(sponsorExtendedSurveyID != -1 ? sponsorAdminExtendedSurveyID : sponsorID);

//							sponsorAdminId = Convert.ToInt32(Session["SponsorAdminID"]);
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

//										success = Db.sendMail(u.Email, ExtendedSurveyFinishedSubject.Text, body);
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
							#endregion
							break;
						case 9:
							#region All activated
//							sponsorRepository.UpdateLastAllMessageSent(sponsorID);
//							repository.UpdateLastAllMessageSent(sponsorAdminID != -1 ? sponsorAdminExtendedSurveyID : sponsorID);
							repository.UpdateLastAllMessageSent(sponsorAdminID != -1 ? sponsorAdminID : sponsorID);

//							sponsorAdminId = Convert.ToInt32(Session["SponsorAdminID"]);
							foreach (var u in userRepository.Find2(sponsorID, sponsorAdminID)) {
								bool success = false;
								bool badEmail = false;
								if (Db.isEmail(u.Email)) {
									try {
										success = Db.sendMail(u.Email, AllMessageSubject.Text, AllMessageBody.Text);
										//success = Db.sendMail(sponsor.EmailFrom, u.Email, AllMessageSubject.Text, AllMessageBody.Text);
										//success = Db.sendMail("reminder@healthwatch.se", u.Email, AllMessageSubject.Text, AllMessageBody.Text);
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
							#endregion
							break;
					}
					Response.Redirect("messages.aspx?Sent=" + cx + "&Fail=" + bx + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
				}
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Save.Text = R.Str(lid, "save", "Save");
			Send.Text = R.Str(lid, "send", "Send");
			buttonRevert.Text = R.Str(lid, "revert.default", "Revert to default");

			if (incorrectPassword) {
				string script = string.Format(
					"<script language='JavaScript'>alert('{0}');</script>",
					R.Str(lid, "password.incorrect", "Incorrect password!")
				);
				Page.RegisterStartupScript("ERROR", script);
			}
			if (sent) {
				string script = string.Format(
					"<script language='JavaScript'>alert('{0} {3}\\r\\n{1} {2}');</script>",
					Request.QueryString["Sent"].ToString(),
					Request.QueryString["Fail"].ToString(),
					R.Str(lid, "email.incorrect", "incorrect email address(es) found."),
					R.Str(lid, "message.sent", "messages successfully sent.")
				);
				Page.RegisterStartupScript("SENT", script);
			}
		}
	}
}