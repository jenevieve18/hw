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
        int sponsorExtendedSurveyID = 0;
		bool incorrectPassword = false;
		bool sent = false;
		Sponsor sponsor;
		
		SqlUserRepository userRepository = new SqlUserRepository();
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		SqlProjectRepository projectRepository = new SqlProjectRepository();

		protected void Page_Load(object sender, EventArgs e)
		{
			sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);

			sponsorRepository.SaveSponsorAdminSessionFunction(Convert.ToInt32(Session["SponsorAdminSessionID"]), ManagerFunction.Messages, DateTime.Now);
			Save.Click += new EventHandler(Save_Click);
			Send.Click += new EventHandler(Send_Click);

			if (sponsorID != 0) {
				sent = (HttpContext.Current.Request.QueryString["Sent"] != null);
				
				int sponsorAdminID;

				if (!IsPostBack) {
					sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
					var u = userRepository.a(sponsorID, sponsorAdminID);
					AllMessageLastSent.Text = "Recipients: " + u + ", ";

					sponsor = sponsorRepository.ReadSponsor(sponsorID);
					if (sponsor != null) {
						InviteTxt.Text = sponsor.InviteText;
						InviteReminderTxt.Text = sponsor.InviteReminderText;
						LoginTxt.Text = sponsor.LoginText;

						InviteSubject.Text = sponsor.InviteSubject;
						InviteReminderSubject.Text = sponsor.InviteReminderSubject;
						LoginSubject.Text = sponsor.LoginSubject;

						InviteLastSent.Text = (sponsor.InviteLastSent ==  null ? "Never" : sponsor.InviteLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
						InviteReminderLastSent.Text = (sponsor.InviteReminderLastSent == null ? "Never" : sponsor.InviteReminderLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
						LoginLastSent.Text = (sponsor.LoginLastSent == null ? "Never" : sponsor.LoginLastSent.Value.ToString("yyyy-MM-dd HH:mm"));

						LoginDays.SelectedValue = (sponsor.LoginDays <= 0 ? "14" : sponsor.LoginDays.ToString());
						LoginWeekday.SelectedValue = (sponsor.LoginWeekday <= -1 ? "NULL" : sponsor.LoginWeekday.ToString());

						AllMessageSubject.Text = sponsor.AllMessageSubject;
						AllMessageBody.Text = sponsor.AllMessageBody;
						AllMessageLastSent.Text += "Last sent: " + (sponsor.AllMessageLastSent == null ? "Never" : sponsor.AllMessageLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
					}
				}
				#region SponsorExtendedSurvey
				int projectRoundId = 0;
				string extendedSurvey = "";
				bool found = false;
				ArrayList seen = new ArrayList();
				sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
				foreach (var s in sponsorRepository.FindExtendedSurveysBySponsorAdmin(sponsorID, sponsorAdminID)) {
					if (!seen.Contains(s.Id)) {
						if (s.ProjectRound != null) {
							if (!found) {
								projectRoundId = s.ProjectRound.Id;
								if (!IsPostBack) {
									extendedSurvey = s.Internal + s.RoundText;
									ExtendedSurvey.Text = "Reminder for <B>" + extendedSurvey + "</B> (<span style='font-size:9px;'>[x]Last sent: " + (s.EmailLastSent == null ? "Never" : s.EmailLastSent.Value.ToString("yyyy-MM-dd")) + "</span>)";
									ExtendedSurveyTxt.Text = s.EmailBody;
									ExtendedSurveySubject.Text = s.EmailSubject;

									ExtendedSurveyFinished.Text = "Thank you mail for <B>" + extendedSurvey + "</B> (<span style='font-size:9px;'>[x]Last sent: " + (s.EmailLastSent == null ? "Never" : s.EmailLastSent.Value.ToString("yyyy-MM-dd")) + "</span>)";
									ExtendedSurveyFinishedTxt.Text = s.FinishedEmailBody;
									ExtendedSurveyFinishedSubject.Text = s.FinishedEmailSubject;
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
								SendType.Items.Add(new ListItem("Reminder: " + extendedSurvey, "4"));
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
				HttpContext.Current.Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			}
		}

		void save()
		{
			sponsorRepository.UpdateSponsor(
				new Sponsor {
					InviteText = InviteTxt.Text,
					InviteReminderText = InviteReminderTxt.Text,
					AllMessageSubject = AllMessageSubject.Text,
					LoginText = LoginTxt.Text,
					InviteSubject = InviteSubject.Text,
					InviteReminderSubject = InviteReminderSubject.Text,
					AllMessageBody = AllMessageBody.Text,
					LoginSubject = LoginSubject.Text,
					LoginDays = Convert.ToInt32(LoginDays.SelectedValue),
					LoginWeekday = ConvertHelper.ToInt32(LoginWeekday.SelectedValue, -1),
					Id = sponsorID
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

			HttpContext.Current.Response.Redirect("messages.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
		}

		void Send_Click(object sender, EventArgs e)
		{
			save();

			if (SendType.SelectedIndex != -1) {
				bool valid = (HttpContext.Current.Session["SponsorAdminID"].ToString() == "-1");
				if (!valid) {
					int sponsorAdminId = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
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

					switch (Convert.ToInt32(SendType.SelectedValue)) {
						case 1:
							#region Invite
							sponsorRepository.UpdateSponsorLastInviteSent(sponsorID);

							int sponsorAdminId = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
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

							sponsorAdminId = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
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

							sponsorAdminId = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
							int selectedValue = Convert.ToInt32(LoginDays.SelectedValue);
                            LoggingService.Info("test");
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

//										success = Db.sendMail(u.Email, LoginSubject.Text, body);
                                        LoggingService.Info(sponsor.EmailFrom);
                                        LoggingService.Info(u.Email);
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

							sponsorAdminId = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
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

							sponsorAdminId = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
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

							sponsorAdminId = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
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
					HttpContext.Current.Response.Redirect("messages.aspx?Sent=" + cx + "&Fail=" + bx + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
				}
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			if (incorrectPassword) {
				Page.RegisterStartupScript("ERROR", "<script language='JavaScript'>alert('Incorrect password!');</script>");
			}
			if (sent) {
				Page.RegisterStartupScript("SENT", "<script language='JavaScript'>alert('" + HttpContext.Current.Request.QueryString["Sent"].ToString() + " messages successfully sent.\\r\\n" + HttpContext.Current.Request.QueryString["Fail"].ToString() + " incorrect email address(es) found.');</script>");
			}
		}
	}
}