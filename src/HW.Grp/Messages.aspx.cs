using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;

namespace HW.Grp
{
	public partial class Messages : System.Web.UI.Page
	{
		int sponsorID = 0, sponsorExtendedSurveyID = 0;
		bool incorrectPassword = false;
		bool sent = false;
		
		IUserRepository userRepository = AppContext.GetRepositoryFactory().CreateUserRepository();
		ISponsorRepository sponsorRepository = AppContext.GetRepositoryFactory().CreateSponsorRepository();
		IProjectRepository projectRepository = AppContext.GetRepositoryFactory().CreateProjectRepository();

		protected void Page_Load(object sender, EventArgs e)
		{
			sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);

			Save.Click += new EventHandler(Save_Click);
			Send.Click += new EventHandler(Send_Click);

			if (sponsorID != 0)
			{
				sent = (HttpContext.Current.Request.QueryString["Sent"] != null);

				if (!IsPostBack)
				{
					int sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
					var u = userRepository.a(sponsorID, sponsorAdminID);
					AllMessageLastSent.Text = "Recipients: " + u + ", ";

					var s = sponsorRepository.ReadSponsor(sponsorID);
					if (s != null)
					{
						InviteTxt.Text = s.InviteText;
						InviteReminderTxt.Text = s.InviteReminderText;
						LoginTxt.Text = s.LoginText;

						InviteSubject.Text = s.InviteSubject;
						InviteReminderSubject.Text = s.InviteReminderSubject;
						LoginSubject.Text = s.LoginSubject;

						InviteLastSent.Text = (s.InviteLastSent ==  null ? "Never" : s.InviteLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
						InviteReminderLastSent.Text = (s.InviteReminderLastSent == null ? "Never" : s.InviteReminderLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
						LoginLastSent.Text = (s.LoginLastSent == null ? "Never" : s.LoginLastSent.Value.ToString("yyyy-MM-dd HH:mm"));

						LoginDays.SelectedValue = (s.LoginDays <= 0 ? "14" : s.LoginDays.ToString());
						LoginWeekday.SelectedValue = (s.LoginWeekday <= 0 ? "NULL" : s.LoginWeekday.ToString());

						AllMessageSubject.Text = s.AllMessageSubject;
						AllMessageBody.Text = s.AllMessageBody;
						AllMessageLastSent.Text += "Last sent: " + (s.AllMessageLastSent == null ? "Never" : s.AllMessageLastSent.Value.ToString("yyyy-MM-dd HH:mm"));
					}
				}
				#region SponsorExtendedSurvey
				int projectRoundID = 0; string extendedSurvey = ""; bool found = false;
				ArrayList seen = new ArrayList();
				int SAID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
				foreach (var s in sponsorRepository.FindExtendedSurveysBySponsorAdmin(sponsorID, SAID))
				{
					if (!seen.Contains(s.Id))
					{
						if (s.ProjectRoundUnit != null)
						{
							if (!found)
							{
								projectRoundID = s.ProjectRoundUnit.Id;
								if (!IsPostBack)
								{
									extendedSurvey = s.Internal + s.RoundText;
									ExtendedSurvey.Text = "Reminder for <B>" + extendedSurvey + "</B> (<span style=\"font-size:9px;\">[x]Last sent: " + (s.EmailLastSent == null ? "Never" : s.EmailLastSent.ToString("yyyy-MM-dd")) + "</span>)";
									ExtendedSurveyTxt.Text = s.EmailBody;
									ExtendedSurveySubject.Text = s.EmailSubject;

									ExtendedSurveyFinished.Text = "Thank you mail for <B>" + extendedSurvey + "</B> (<span style=\"font-size:9px;\">[x]Last sent: " + (s.EmailLastSent == null ? "Never" : s.EmailLastSent.ToString("yyyy-MM-dd")) + "</span>)";
									ExtendedSurveyFinishedTxt.Text = s.FinishedEmailBody;
									ExtendedSurveyFinishedSubject.Text = s.FinishedEmailSubject;
								}
								sponsorExtendedSurveyID = s.Id;
								found = true;

								if (!IsPostBack)
								{
									var r = userRepository.CountBySponsorWithAdminAndExtendedSurvey2(sponsorID, SAID, sponsorExtendedSurveyID);
									ExtendedSurvey.Text = ExtendedSurvey.Text.Replace("[x]", "[x]Recipients: " + r + ", ");

									r = userRepository.CountBySponsorWithAdminAndExtendedSurvey(sponsorID, SAID, sponsorExtendedSurveyID);
									ExtendedSurveyFinished.Text = ExtendedSurveyFinished.Text.Replace("[x]", "[x]Recipients: " + r + ", ");
								}
							}
							else
							{
								if (ExtendedSurveyTxt.Text == "")
								{
									ExtendedSurveyTxt.Text = s.EmailBody;
								}
								if (ExtendedSurveySubject.Text == "")
								{
									ExtendedSurveySubject.Text = s.EmailSubject;
								}
								if (ExtendedSurveyFinishedTxt.Text == "")
								{
									ExtendedSurveyFinishedTxt.Text = s.FinishedEmailBody;
								}
								if (ExtendedSurveyFinishedSubject.Text == "")
								{
									ExtendedSurveyFinishedSubject.Text = s.FinishedEmailSubject;
								}
							}
						}
						seen.Add(s.Id);
					}
				}
				if (projectRoundID != 0)
				{
					var u = projectRepository.ReadRound(projectRoundID);
					if (u != null)
					{
						if (!IsPostBack)
						{
							ExtendedSurvey.Text = ExtendedSurvey.Text.Replace("[x]", "Period: " + (u.Started ==  null ? "?" : u.Started.Value.ToString("yyyy-MM-dd")) + "--" + (u.Closed == null ? "?" : u.Closed.Value.ToString("yyyy-MM-dd")) + ", ");
							ExtendedSurveyFinished.Text = ExtendedSurveyFinished.Text.Replace("[x]", "Period: " + (u.Started == null ? "?" : u.Started.Value.ToString("yyyy-MM-dd")) + "--" + (u.Closed == null ? "?" : u.Closed.Value.ToString("yyyy-MM-dd")) + ", ");
						}
						if (
							//!rs.IsDBNull(0) && rs.GetDateTime(0) <= DateTime.Now
							//&&
							(u.Closed == null || u.Closed >= DateTime.Now)
						)
						{
							if (!IsPostBack)
							{
								ExtendedSurveySubject.Visible = true;
								ExtendedSurvey.Visible = true;
								ExtendedSurveyTxt.Visible = true;
								SendType.Items.Add(new ListItem("Reminder: " + extendedSurvey, "4"));
							}
						}
						else
						{
							projectRoundID = 0;
						}
					}
					else
					{
						projectRoundID = 0;
					}

					if (!ExtendedSurvey.Visible && ExtendedSurveyFinished.Text != "")
					{
						ExtendedSurveyFinishedSubject.Visible = true;
						ExtendedSurveyFinished.Visible = true;
						ExtendedSurveyFinishedTxt.Visible = true;
						SendType.Items.Add(new ListItem("Thank you: " + extendedSurvey, "5"));
					}
				}
				#endregion
			}
			else
			{
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
					LoginWeekday = ConvertHelper.ToInt32(LoginWeekday.SelectedValue),
					Id = sponsorID
				}
			);

			if ((ExtendedSurveyFinishedSubject.Visible || ExtendedSurveySubject.Visible) && sponsorExtendedSurveyID != 0)
			{
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

			if (SendType.SelectedIndex != -1)
			{
				bool valid = (HttpContext.Current.Session["SponsorAdminID"].ToString() == "-1");
				if (!valid)
				{
					int sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
					var a = sponsorRepository.ReadSponsorAdmin(sponsorID, sponsorAdminID, Password.Text);
					if (a != null && a.Id == sponsorAdminID)
					{
						valid = true;
					}
					else
					{
						incorrectPassword = true;
					}
				}

				if (valid)
				{
					int cx = 0;
					int bx = 0;

					switch (Convert.ToInt32(SendType.SelectedValue))
					{
						case 1:
							#region Invite
							sponsorRepository.UpdateSponsorLastInviteSent(sponsorID);

							int sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
							foreach (var i in sponsorRepository.FindInvitesBySponsor(sponsorID, sponsorAdminID))
							{
								bool success = Db.sendInvitation(i.Id, i.Email, InviteTxt.Text, InviteSubject.Text, i.InvitationKey);

								if (success)
								{
									cx++;
								}
								else
								{
									bx++;
								}
							}
							#endregion
							break;
						case 2:
							#region Invite reminder
							sponsorRepository.UpdateSponsorLastInviteReminderSent(sponsorID);

							sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
							foreach (var i in sponsorRepository.FindSentInvitesBySponsor(sponsorID, sponsorAdminID))
							{
								bool success = Db.sendInvitation(i.Id, i.Email, InviteReminderTxt.Text, InviteReminderSubject.Text, i.InvitationKey);

								if (success)
								{
									cx++;
								}
								else
								{
									bx++;
								}
							}
							#endregion
							break;
						case 3:
							#region Login reminder
							sponsorRepository.UpdateSponsorLastLoginSent(sponsorID);

							sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
							int selectedValue = Convert.ToInt32(LoginDays.SelectedValue);
							foreach (var u in userRepository.FindBySponsorWithLoginDays(sponsorID, sponsorAdminID, selectedValue))
							{
								bool success = false;
								bool badEmail = false;
								if (Db.isEmail(u.Email))
								{
									try
									{
										string body = LoginTxt.Text;

//										string personalLink = "" + System.Configuration.ConfigurationSettings.AppSettings["healthWatchURL"] + "";
										string path = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
										string personalLink = "" + path + "";
										if (u.ReminderLink > 0)
										{
											personalLink += "/c/" + u.UserKey.ToLower() + u.Id.ToString();
										}
										if (body.IndexOf("<LINK/>") >= 0)
										{
											body = body.Replace("<LINK/>", personalLink);
										}
										else
										{
											body += "\r\n\r\n" + personalLink;
										}

										Db.sendMail(u.Email, body, LoginSubject.Text);

										userRepository.UpdateLastReminderSent(u.Id);

										success = true;
									}
									catch (Exception)
									{
										badEmail = true;
									}
								}
								else
								{
									badEmail = true;
								}
								if (badEmail)
								{
									userRepository.UpdateEmailFailure(u.Id);
								}

								if (success)
								{
									cx++;
								}
								else
								{
									bx++;
								}
							}
							#endregion
							break;
						case 4:
							#region Extended survey
							sponsorRepository.UpdateExtendedSurveyLastEmailSent(sponsorExtendedSurveyID);

							//HttpContext.Current.Response.Write(sql);
							//HttpContext.Current.Response.End();
							sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
							foreach (var u in userRepository.FindBySponsorWithExtendedSurvey2(sponsorID, sponsorAdminID, sponsorExtendedSurveyID))
							{
								bool success = false;
								bool badEmail = false;
								if (Db.isEmail(u.Email))
								{
									try
									{
										string body = ExtendedSurveyTxt.Text;

//										string personalLink = "" + System.Configuration.ConfigurationSettings.AppSettings["healthWatchURL"] + "";
										string path = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
										string personalLink = "" + path + "";
										if (u.ReminderLink > 0)
										{
											personalLink += "/c/" + u.UserKey.ToLower() + u.Id.ToString();
										}
										if (body.IndexOf("<LINK/>") >= 0)
										{
											body = body.Replace("<LINK/>", personalLink);
										}
										else
										{
											body += "\r\n\r\n" + personalLink;
										}

										Db.sendMail(u.Email, body, ExtendedSurveySubject.Text);

										success = true;
									}
									catch (Exception)
									{
										badEmail = true;
									}
								}
								else
								{
									badEmail = true;
								}
								if (badEmail)
								{
									userRepository.UpdateEmailFailure(u.Id);
								}

								if (success)
								{
									cx++;
								}
								else
								{
									bx++;
								}
							}
							#endregion
							break;
						case 5:
							#region Thank you: Extended survey
							sponsorRepository.UpdateExtendedSurveyLastFinishedSent(sponsorExtendedSurveyID);

							sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
							foreach (var u in userRepository.FindBySponsorWithExtendedSurvey(sponsorID, sponsorAdminID, sponsorExtendedSurveyID))
							{
								bool success = false;
								bool badEmail = false;
								if (Db.isEmail(u.Email))
								{
									try
									{
										string body = ExtendedSurveyFinishedTxt.Text;

//										string personalLink = "" + System.Configuration.ConfigurationSettings.AppSettings["healthWatchURL"] + "";
										string path = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
										string personalLink = "" + path + "";
										if (u.ReminderLink > 0)
										{
											personalLink += "/c/" + u.UserKey.ToLower() + u.Id.ToString();
										}
										if (body.IndexOf("<LINK/>") >= 0)
										{
											body = body.Replace("<LINK/>", personalLink);
										}
										else
										{
											body += "\r\n\r\n" + personalLink;
										}

										Db.sendMail(u.Email, body, ExtendedSurveyFinishedSubject.Text);

										success = true;
									}
									catch (Exception)
									{
										badEmail = true;
									}
								}
								else
								{
									badEmail = true;
								}
								if (badEmail)
								{
									userRepository.UpdateEmailFailure(u.Id);
								}

								if (success)
								{
									cx++;
								}
								else
								{
									bx++;
								}
							}
							#endregion
							break;
						case 9:
							#region All activated
							sponsorRepository.UpdateLastAllMessageSent(sponsorID);

							sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
							foreach (var u in userRepository.Find2(sponsorID, sponsorAdminID))
							{
								bool success = false;
								bool badEmail = false;
								if (Db.isEmail(u.Email))
								{
									try
									{
										Db.sendMail(u.Email, AllMessageBody.Text, AllMessageSubject.Text);

										success = true;
									}
									catch (Exception)
									{
										badEmail = true;
									}
								}
								else
								{
									badEmail = true;
								}
								if (badEmail)
								{
									userRepository.UpdateEmailFailure(u.Id);
								}

								if (success)
								{
									cx++;
								}
								else
								{
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

			if (incorrectPassword)
			{
				Page.RegisterStartupScript("ERROR", "<script language=\"JavaScript\">alert('Incorrect password!');</SCRIPT>");
			}
			if (sent)
			{
				Page.RegisterStartupScript("SENT", "<script language=\"JavaScript\">alert('" + HttpContext.Current.Request.QueryString["Sent"].ToString() + " messages successfully sent.\\r\\n" + HttpContext.Current.Request.QueryString["Fail"].ToString() + " incorrect email address(es) found.');</SCRIPT>");
			}
		}
	}
}