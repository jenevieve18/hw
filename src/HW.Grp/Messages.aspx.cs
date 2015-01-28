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
		bool incorrectPassword = false;
		bool sent = false;
		Sponsor sponsor;
		SponsorAdmin sponsorAdmin;
		bool loginWithSkey = false;
		
		SqlUserRepository userRepository = new SqlUserRepository();
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		SqlProjectRepository projectRepository = new SqlProjectRepository();
		protected int lid;

		protected void Page_Load(object sender, EventArgs e)
		{
			sponsorID = ConvertHelper.ToInt32(Session["SponsorID"]);
			sponsorAdminID = ConvertHelper.ToInt32(Session["SponsorAdminID"]);
			lid = ConvertHelper.ToInt32(Session["lid"], 1);
			loginWithSkey = Session["SponsorKey"] != null;

			sponsorRepository.SaveSponsorAdminSessionFunction(Convert.ToInt32(Session["SponsorAdminSessionID"]), ManagerFunction.Messages, DateTime.Now);
			Save.Click += new EventHandler(Save_Click);
			Send.Click += new EventHandler(Send_Click);

			if (sponsorID != 0) {

				SendType.Items.Add(new ListItem(R.Str(lid, "select.send.type", "< select send type >"), "0"));
				SendType.Items.Add(new ListItem(R.Str(lid, "registration", "Registration"), "1"));
				SendType.Items.Add(new ListItem(R.Str(lid, "registration.reminder", "Registration reminder"), "2"));
				SendType.Items.Add(new ListItem(R.Str(lid, "login.reminder", "Login reminder"), "3"));
				SendType.Items.Add(new ListItem(R.Str(lid, "users.activated.all", "All activated users"), "9"));

				sent = (Request.QueryString["Sent"] != null);
				
//				int sponsorAdminID;

				sponsor = sponsorRepository.ReadSponsor(sponsorID);
				sponsorAdmin = sponsorRepository.ReadSponsorAdmin(sponsorAdminID);
				if (!IsPostBack) {

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
					
//					sponsorAdminID = ConvertHelper.ToInt32(Session["SponsorAdminID"]);
					var u = userRepository.a(sponsorID, sponsorAdminID);
					AllMessageLastSent.Text = R.Str(lid, "recipients", "Recipients") + ": " + u + ", ";

					if (sponsor != null) {
//						InviteTxt.Text = sponsor.InviteText;
//						InviteSubject.Text = sponsor.InviteSubject;
//
//						InviteReminderTxt.Text = sponsor.InviteReminderText;
//						InviteReminderSubject.Text = sponsor.InviteReminderSubject;
//
//						AllMessageSubject.Text = sponsor.AllMessageSubject;
//						AllMessageBody.Text = sponsor.AllMessageBody;
						
						InviteTxt.Text = sponsorAdmin != null ? sponsorAdmin.InviteText : sponsor.InviteText;
						InviteSubject.Text = sponsorAdmin != null ? sponsorAdmin.InviteSubject : sponsor.InviteSubject;
						
						InviteReminderTxt.Text = sponsorAdmin != null ? sponsorAdmin.InviteReminderText : sponsor.InviteReminderText;
						InviteReminderSubject.Text = sponsorAdmin != null ? sponsorAdmin.InviteReminderSubject : sponsor.InviteReminderSubject;
						
						AllMessageSubject.Text = sponsorAdmin != null ? sponsorAdmin.AllMessageSubject : sponsor.AllMessageSubject;
						AllMessageBody.Text = sponsorAdmin != null ? sponsorAdmin.AllMessageBody : sponsor.AllMessageBody;

						LoginTxt.Text = sponsor.LoginText;
						LoginSubject.Text = sponsor.LoginSubject;

						LoginDays.SelectedValue = (sponsor.LoginDays <= 0 ? "14" : sponsor.LoginDays.ToString());
						LoginWeekday.SelectedValue = (sponsor.LoginWeekday <= -1 ? "NULL" : sponsor.LoginWeekday.ToString());

						InviteLastSent.Text = (sponsor.InviteLastSent ==  null ? "Never" : sponsor.InviteLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
						InviteReminderLastSent.Text = (sponsor.InviteReminderLastSent == null ? "Never" : sponsor.InviteReminderLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
						AllMessageLastSent.Text += "Last sent: " + (sponsor.AllMessageLastSent == null ? "Never" : sponsor.AllMessageLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
						LoginLastSent.Text = (sponsor.LoginLastSent == null ? "Never" : sponsor.LoginLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
					}
				}
				#region SponsorExtendedSurvey
				int projectRoundId = 0;
				string extendedSurvey = "";
				bool found = false;
				ArrayList seen = new ArrayList();
//				sponsorAdminID = ConvertHelper.ToInt32(Session["SponsorAdminID"]);
				foreach (var s in sponsorRepository.FindExtendedSurveysBySponsorAdmin(sponsorID, sponsorAdminID)) {
					if (!seen.Contains(s.Id)) {
						if (s.ProjectRound != null) {
							if (!found) {
								projectRoundId = s.ProjectRound.Id;
								if (!IsPostBack) {
									extendedSurvey = s.Internal + s.RoundText;
									ExtendedSurvey.Text = R.Str(lid, "reminder.for", "Reminder for") + " <B>" + extendedSurvey + "</B> (<span style='font-size:9px;'>[x]Last sent: " + (s.EmailLastSent == null ? "Never" : s.EmailLastSent.Value.ToString("yyyy-MM-dd")) + "</span>)";
									ExtendedSurveyFinished.Text = "Thank you mail for <B>" + extendedSurvey + "</B> (<span style='font-size:9px;'>[x]Last sent: " + (s.EmailLastSent == null ? "Never" : s.EmailLastSent.Value.ToString("yyyy-MM-dd")) + "</span>)";
									
									ExtendedSurveySubject.Text = s.EmailSubject;
									ExtendedSurveyTxt.Text = s.EmailBody;
									ExtendedSurveyFinishedSubject.Text = s.FinishedEmailSubject;
									ExtendedSurveyFinishedTxt.Text = s.FinishedEmailBody;
								}
								sponsorExtendedSurveyID = s.Id;
								found = true;

								if (!IsPostBack) {
									var r = userRepository.CountBySponsorWithAdminAndExtendedSurvey2(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
									ExtendedSurvey.Text = ExtendedSurvey.Text.Replace("[x]", "[x]Recipients: " + r + ", ");

									r = userRepository.CountBySponsorWithAdminAndExtendedSurvey(sponsorID, sponsorAdminID, sponsorExtendedSurveyID);
									ExtendedSurveyFinished.Text = ExtendedSurveyFinished.Text.Replace("[x]", "[x]Recipients: " + r + ", ");
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
					var u = projectRepository.ReadRound(projectRoundId);
					if (u != null) {
						if (!IsPostBack) {
							ExtendedSurvey.Text = ExtendedSurvey.Text.Replace("[x]", "Period: " + (u.Started ==  null ? "?" : u.Started.Value.ToString("yyyy-MM-dd")) + "--" + (u.Closed == null ? "?" : u.Closed.Value.ToString("yyyy-MM-dd")) + ", ");
							ExtendedSurveyFinished.Text = ExtendedSurveyFinished.Text.Replace("[x]", "Period: " + (u.Started == null ? "?" : u.Started.Value.ToString("yyyy-MM-dd")) + "--" + (u.Closed == null ? "?" : u.Closed.Value.ToString("yyyy-MM-dd")) + ", ");
						}
						if ((u.Closed == null || u.Closed >= DateTime.Now)) {
							if (!IsPostBack) {
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

					if (!ExtendedSurvey.Visible && ExtendedSurveyFinished.Text != "") {
						ExtendedSurveyFinishedSubject.Visible = true;
						ExtendedSurveyFinished.Visible = true;
						ExtendedSurveyFinishedTxt.Visible = true;
						SendType.Items.Add(new ListItem("Thank you: " + extendedSurvey, "5"));
					}
				}
				#endregion
			} else {
				Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			}
		}

		void save()
		{
			sponsorRepository.UpdateSponsor(
				new Sponsor {
//					InviteSubject = InviteSubject.Text,
//					InviteText = InviteTxt.Text,
//					InviteReminderSubject = InviteReminderSubject.Text,
//					InviteReminderText = InviteReminderTxt.Text,
//					AllMessageSubject = AllMessageSubject.Text,
//					AllMessageBody = AllMessageBody.Text,
					LoginSubject = LoginSubject.Text,
					LoginText = LoginTxt.Text,
					LoginDays = Convert.ToInt32(LoginDays.SelectedValue),
					LoginWeekday = ConvertHelper.ToInt32(LoginWeekday.SelectedValue, -1),
					Id = sponsorID
				}
			);
			sponsorRepository.UpdateSponsorAdmin2(
				new SponsorAdmin {
					InviteSubject = InviteSubject.Text,
					InviteText = InviteTxt.Text,
					InviteReminderSubject = InviteReminderSubject.Text,
					InviteReminderText = InviteReminderTxt.Text,
					AllMessageSubject = AllMessageSubject.Text,
					AllMessageBody = AllMessageBody.Text,
//					LoginText = LoginTxt.Text,
//					LoginSubject = LoginSubject.Text,
//					LoginDays = Convert.ToInt32(LoginDays.SelectedValue),
//					LoginWeekday = ConvertHelper.ToInt32(LoginWeekday.SelectedValue, -1),
					Id = sponsorAdminID
				}
			);

			if ((ExtendedSurveyFinishedSubject.Visible || ExtendedSurveySubject.Visible) && sponsorExtendedSurveyID != 0) {
				sponsorRepository.UpdateSponsorExtendedSurvey(
					new SponsorExtendedSurvey {
						EmailSubject = ExtendedSurveySubject.Text,
						EmailBody = ExtendedSurveyTxt.Text,
						FinishedEmailSubject = ExtendedSurveyFinishedSubject.Text,
						FinishedEmailBody = ExtendedSurveyFinishedTxt.Text,
						Id = sponsorExtendedSurveyID
					}
				);
			}
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
			save();

			if (SendType.SelectedIndex != -1) {
				bool valid = (Session["SponsorAdminID"].ToString() == "-1");
				if (!valid) {
					int sponsorAdminId = Convert.ToInt32(Session["SponsorAdminID"]);
					var a = sponsorRepository.ReadSponsorAdmin(sponsorID, sponsorAdminId, Password.Text);
					if (a != null && a.Id == sponsorAdminId) {
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
							sponsorRepository.UpdateSponsorLastInviteSent(sponsorID);

							int sponsorAdminId = Convert.ToInt32(Session["SponsorAdminID"]);
							foreach (var i in sponsorRepository.FindInvitesBySponsor(sponsorID, sponsorAdminId)) {
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
							sponsorRepository.UpdateSponsorLastInviteReminderSent(sponsorID);

							sponsorAdminId = Convert.ToInt32(Session["SponsorAdminID"]);
							foreach (var i in sponsorRepository.FindSentInvitesBySponsor(sponsorID, sponsorAdminId)) {
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
							sponsorRepository.UpdateSponsorLastLoginSent(sponsorID);

							sponsorAdminId = Convert.ToInt32(Session["SponsorAdminID"]);
							int selectedValue = Convert.ToInt32(LoginDays.SelectedValue);
							foreach (var u in userRepository.FindBySponsorWithLoginDays(sponsorID, sponsorAdminId, selectedValue)) {
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
							sponsorRepository.UpdateExtendedSurveyLastEmailSent(sponsorExtendedSurveyID);

							sponsorAdminId = Convert.ToInt32(Session["SponsorAdminID"]);
							foreach (var u in userRepository.FindBySponsorWithExtendedSurvey2(sponsorID, sponsorAdminId, sponsorExtendedSurveyID)) {
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
							sponsorRepository.UpdateExtendedSurveyLastFinishedSent(sponsorExtendedSurveyID);

							sponsorAdminId = Convert.ToInt32(Session["SponsorAdminID"]);
							foreach (var u in userRepository.FindBySponsorWithExtendedSurvey(sponsorID, sponsorAdminId, sponsorExtendedSurveyID)) {
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
							sponsorRepository.UpdateLastAllMessageSent(sponsorID);

							sponsorAdminId = Convert.ToInt32(Session["SponsorAdminID"]);
							foreach (var u in userRepository.Find2(sponsorID, sponsorAdminId)) {
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

			if (incorrectPassword) {
				Page.RegisterStartupScript("ERROR", "<script language='JavaScript'>alert('Incorrect password!');</script>");
			}
			if (sent) {
				Page.RegisterStartupScript("SENT", "<script language='JavaScript'>alert('" + Request.QueryString["Sent"].ToString() + " messages successfully sent.\\r\\n" + Request.QueryString["Fail"].ToString() + " incorrect email address(es) found.');</script>");
			}
		}
	}
}