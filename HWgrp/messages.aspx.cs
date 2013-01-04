using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;
using HW.Core;

namespace HWgrp
{
	public partial class messages : System.Web.UI.Page
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

//				SqlDataReader rs;

				if (!IsPostBack)
				{
//					rs = Db.rs("SELECT COUNT(*) " +
//					           "FROM [User] u " +
//					           "INNER JOIN SponsorInvite si ON u.UserID = si.UserID " +
//					           (HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
//					            "INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID " +
//					            "WHERE sad.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " " +
//					            "AND " : "WHERE ") + "si.SponsorID = " + sponsorID + " " +
//					           "AND u.Email IS NOT NULL " +
//					           "AND u.Email <> '' " +
//					           "AND si.StoppedReason IS NULL " +
//					           "AND u.Email NOT LIKE '%DELETED'");
					int sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
					var u = userRepository.a(sponsorID, sponsorAdminID);
//					if (rs.Read())
//					{
//						AllMessageLastSent.Text = "Recipients: " + rs.GetInt32(0) + ", ";
					AllMessageLastSent.Text = "Recipients: " + u + ", ";
//					}
//					rs.Close();

//					rs = Db.rs("SELECT " +
//					           "s.InviteTxt, " +
//					           "s.InviteReminderTxt, " +
//					           "s.LoginTxt, " +
//					           "s.InviteSubject, " +
//					           "s.InviteReminderSubject, " +
//					           "s.LoginSubject, " +
//					           "s.InviteLastSent, " +
//					           "s.InviteReminderLastSent, " +
//					           "s.LoginLastSent, " +
//					           "s.LoginDays, " +
//					           "s.LoginWeekday, " +
//					           "s.AllMessageSubject, " +
//					           "s.AllMessageBody, " +
//					           "s.AllMessageLastSent " +
//					           "FROM Sponsor s " +
//					           "WHERE s.SponsorID = " + sponsorID);
					var s = sponsorRepository.ReadSponsor(sponsorID);
//					if (rs.Read())
					if (s != null)
					{
//						InviteTxt.Text = (rs.IsDBNull(0) ? "" : rs.GetString(0));
//						InviteReminderTxt.Text = (rs.IsDBNull(1) ? "" : rs.GetString(1));
//						LoginTxt.Text = (rs.IsDBNull(2) ? "" : rs.GetString(2));
//
//						InviteSubject.Text = (rs.IsDBNull(3) ? "" : rs.GetString(3));
//						InviteReminderSubject.Text = (rs.IsDBNull(4) ? "" : rs.GetString(4));
//						LoginSubject.Text = (rs.IsDBNull(5) ? "" : rs.GetString(5));
//
//						InviteLastSent.Text = (rs.IsDBNull(6) ? "Never" : rs.GetDateTime(6).ToString("yyyy-MM-dd HH:mm"));
//						InviteReminderLastSent.Text = (rs.IsDBNull(7) ? "Never" : rs.GetDateTime(7).ToString("yyyy-MM-dd HH:mm"));
//						LoginLastSent.Text = (rs.IsDBNull(8) ? "Never" : rs.GetDateTime(8).ToString("yyyy-MM-dd HH:mm"));

//						LoginDays.SelectedValue = (rs.IsDBNull(9) ? "14" : rs.GetInt32(9).ToString());
//						LoginWeekday.SelectedValue = (rs.IsDBNull(10) ? "NULL" : rs.GetInt32(10).ToString());
//
//						AllMessageSubject.Text = (rs.IsDBNull(11) ? "" : rs.GetString(11));
//						AllMessageBody.Text = (rs.IsDBNull(12) ? "" : rs.GetString(12));
//						AllMessageLastSent.Text += "Last sent: " + (rs.IsDBNull(13) ? "Never" : rs.GetDateTime(13).ToString("yyyy-MM-dd HH:mm"));

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
//					rs.Close();
				}
				#region SponsorExtendedSurvey
				int projectRoundID = 0; string extendedSurvey = ""; bool found = false;
				ArrayList seen = new ArrayList();
//				rs = Db.rs("SELECT " +
//				           "ses.ProjectRoundID, " +            // 0
//				           "ses.EmailSubject, " +
//				           "ses.EmailBody, " +
//				           "ses.EmailLastSent, " +
//				           "ses.Internal, " +
//				           "ses.SponsorExtendedSurveyID, " +   // 5
//				           "ses.FinishedEmailSubject, " +
//				           "ses.FinishedEmailBody, " +
//				           "ses.RoundText " +
//				           "FROM SponsorExtendedSurvey ses " +
//				           "INNER JOIN Sponsor s ON ses.SponsorID = s.SponsorID " +
//				           "INNER JOIN Department d ON s.SponsorID = d.SponsorID " +
//				           "LEFT OUTER JOIN SponsorExtendedSurveyDepartment dd ON dd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID AND dd.DepartmentID = d.DepartmentID " +
//				           (HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
//				            "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " +
//				            "WHERE sad.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " " +
//				            "AND " : "WHERE ") + "ses.SponsorID = " + sponsorID + " AND dd.Hide IS NULL " +
//				           "ORDER BY ses.SponsorExtendedSurveyID DESC");
//				while (rs.Read())
				int SAID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
				foreach (var s in sponsorRepository.FindExtendedSurveysBySponsorAdmin(sponsorID, SAID))
				{
//					if (!rs.IsDBNull(5) && !seen.Contains(rs.GetInt32(5)))
					if (!seen.Contains(s.Id))
					{
//						if (!rs.IsDBNull(0))
						if (s.ProjectRoundUnit != null)
						{
							if (!found)
							{
//								projectRoundID = rs.GetInt32(0);
								projectRoundID = s.ProjectRoundUnit.Id;
								if (!IsPostBack)
								{
//									extendedSurvey = rs.GetString(4) + (!rs.IsDBNull(8) ? " / " + rs.GetString(8) : "");
									extendedSurvey = s.Internal + s.RoundText;
//									ExtendedSurvey.Text = "Reminder for <B>" + extendedSurvey + "</B> (<span style=\"font-size:9px;\">[x]Last sent: " + (rs.IsDBNull(3) ? "Never" : rs.GetDateTime(3).ToString("yyyy-MM-dd")) + "</span>)";
									ExtendedSurvey.Text = "Reminder for <B>" + extendedSurvey + "</B> (<span style=\"font-size:9px;\">[x]Last sent: " + (s.EmailLastSent == null ? "Never" : s.EmailLastSent.ToString("yyyy-MM-dd")) + "</span>)";
//									ExtendedSurveyTxt.Text = (!rs.IsDBNull(2) ? rs.GetString(2) : "");
									ExtendedSurveyTxt.Text = s.EmailBody;
//									ExtendedSurveySubject.Text = (!rs.IsDBNull(1) ? rs.GetString(1) : "");
									ExtendedSurveySubject.Text = s.EmailSubject;

//									ExtendedSurveyFinished.Text = "Thank you mail for <B>" + extendedSurvey + "</B> (<span style=\"font-size:9px;\">[x]Last sent: " + (rs.IsDBNull(3) ? "Never" : rs.GetDateTime(3).ToString("yyyy-MM-dd")) + "</span>)";
									ExtendedSurveyFinished.Text = "Thank you mail for <B>" + extendedSurvey + "</B> (<span style=\"font-size:9px;\">[x]Last sent: " + (s.EmailLastSent == null ? "Never" : s.EmailLastSent.ToString("yyyy-MM-dd")) + "</span>)";
//									ExtendedSurveyFinishedTxt.Text = (!rs.IsDBNull(7) ? rs.GetString(7) : "");
									ExtendedSurveyFinishedTxt.Text = s.FinishedEmailBody;
//									ExtendedSurveyFinishedSubject.Text = (!rs.IsDBNull(6) ? rs.GetString(6) : "");
									ExtendedSurveyFinishedSubject.Text = s.FinishedEmailSubject;
								}
//								sponsorExtendedSurveyID = rs.GetInt32(5);
								sponsorExtendedSurveyID = s.Id;
								found = true;

								if (!IsPostBack)
								{
//									SqlDataReader rs2 = Db.rs("SELECT COUNT(*) " +
//									                          "FROM [User] u " +
//									                          "INNER JOIN Department d ON u.DepartmentID = d.DepartmentID " +
//									                          "INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = " + sponsorExtendedSurveyID + " " +
//									                          "INNER JOIN SponsorInvite si ON u.UserID = si.UserID AND si.SponsorID = ses.SponsorID " +
//									                          "INNER JOIN eform..ProjectRound pr ON pr.ProjectRoundID = ses.ProjectRoundID " +
//									                          "LEFT OUTER JOIN UserSponsorExtendedSurvey x ON u.UserID = x.UserID AND x.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID AND x.AnswerID IS NOT NULL " +
//									                          "LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON si.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID " +
//									                          (HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
//									                           "INNER JOIN SponsorAdminDepartment sad ON u.DepartmentID = sad.DepartmentID " +
//									                           "WHERE sad.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " " +
//									                           "AND " : "WHERE ") + "u.SponsorID = " + sponsorID + " " +
//									                          "AND (pr.Started <= GETDATE() OR ISNULL(d.PreviewExtendedSurveys,si.PreviewExtendedSurveys) IS NOT NULL) " +
//									                          "AND x.AnswerID IS NULL " +
//									                          "AND u.Email IS NOT NULL " +
//									                          "AND u.Email <> '' " +
//									                          "AND si.StoppedReason IS NULL " +
//									                          "AND sesd.Hide IS NULL " +
//									                          "AND u.Email NOT LIKE '%DELETED'");
									var r = userRepository.CountBySponsorWithAdminAndExtendedSurvey2(sponsorID, SAID, sponsorExtendedSurveyID);
//									if (rs2.Read())
//									{
//										ExtendedSurvey.Text = ExtendedSurvey.Text.Replace("[x]", "[x]Recipients: " + rs2.GetInt32(0) + ", ");
									ExtendedSurvey.Text = ExtendedSurvey.Text.Replace("[x]", "[x]Recipients: " + r + ", ");
//									}
//									rs2.Close();

//									rs2 = Db.rs("SELECT COUNT(*) " +
//									            "FROM [User] u " +
//									            "INNER JOIN Department d ON u.DepartmentID = d.DepartmentID " +
//									            "INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = " + sponsorExtendedSurveyID + " " +
//									            "INNER JOIN SponsorInvite si ON u.UserID = si.UserID AND si.SponsorID = ses.SponsorID " +
//									            "INNER JOIN eform..ProjectRound pr ON pr.ProjectRoundID = ses.ProjectRoundID " +
//									            "INNER JOIN UserSponsorExtendedSurvey x ON u.UserID = x.UserID AND x.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID AND x.AnswerID IS NOT NULL " +
//									            "LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON si.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID " +
//									            (HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
//									             "INNER JOIN SponsorAdminDepartment sad ON u.DepartmentID = sad.DepartmentID " +
//									             "WHERE sad.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " " +
//									             "AND " : "WHERE ") + "u.SponsorID = " + sponsorID + " " +
//									            "AND x.FinishedEmail IS NULL " +
//									            "AND x.AnswerID IS NOT NULL " +
//									            "AND u.Email IS NOT NULL " +
//									            "AND u.Email <> '' " +
//									            "AND si.StoppedReason IS NULL " +
//									            "AND sesd.Hide IS NULL " +
//									            "AND u.Email NOT LIKE '%DELETED'");
									r = userRepository.CountBySponsorWithAdminAndExtendedSurvey(sponsorID, SAID, sponsorExtendedSurveyID);
//									if (rs2.Read())
//									{
//										ExtendedSurveyFinished.Text = ExtendedSurveyFinished.Text.Replace("[x]", "[x]Recipients: " + rs2.GetInt32(0) + ", ");
									ExtendedSurveyFinished.Text = ExtendedSurveyFinished.Text.Replace("[x]", "[x]Recipients: " + r + ", ");
//									}
//									rs2.Close();
								}
							}
							else
							{
								if (ExtendedSurveyTxt.Text == "")
								{
//									ExtendedSurveyTxt.Text = (!rs.IsDBNull(2) ? rs.GetString(2) : "");
									ExtendedSurveyTxt.Text = s.EmailBody;
								}
								if (ExtendedSurveySubject.Text == "")
								{
//									ExtendedSurveySubject.Text = (!rs.IsDBNull(1) ? rs.GetString(1) : "");
									ExtendedSurveySubject.Text = s.EmailSubject;
								}
								if (ExtendedSurveyFinishedTxt.Text == "")
								{
//									ExtendedSurveyFinishedTxt.Text = (!rs.IsDBNull(7) ? rs.GetString(7) : "");
									ExtendedSurveyFinishedTxt.Text = s.FinishedEmailBody;
								}
								if (ExtendedSurveyFinishedSubject.Text == "")
								{
//									ExtendedSurveyFinishedSubject.Text = (!rs.IsDBNull(6) ? rs.GetString(6) : "");
									ExtendedSurveyFinishedSubject.Text = s.FinishedEmailSubject;
								}
							}
						}
//						seen.Add(rs.GetInt32(5));
						seen.Add(s.Id);
					}
				}
//				rs.Close();
				if (projectRoundID != 0)
				{
//					rs = Db.rs("" +
//					           "SELECT " +
//					           "Started, " +
//					           "Closed " +
//					           "FROM ProjectRound " +
//					           "WHERE ProjectRoundID = " + projectRoundID, "eFormSqlConnection");
					var u = projectRepository.ReadRound(projectRoundID);
//					if (rs.Read())
					if (u != null)
					{
						if (!IsPostBack)
						{
//							ExtendedSurvey.Text = ExtendedSurvey.Text.Replace("[x]", "Period: " + (rs.IsDBNull(0) ? "?" : rs.GetDateTime(0).ToString("yyyy-MM-dd")) + "--" + (rs.IsDBNull(1) ? "?" : rs.GetDateTime(1).ToString("yyyy-MM-dd")) + ", ");
							ExtendedSurvey.Text = ExtendedSurvey.Text.Replace("[x]", "Period: " + (u.Started ==  null ? "?" : u.Started.Value.ToString("yyyy-MM-dd")) + "--" + (u.Closed != null ? "?" : u.Closed.Value.ToString("yyyy-MM-dd")) + ", ");
//							ExtendedSurveyFinished.Text = ExtendedSurveyFinished.Text.Replace("[x]", "Period: " + (rs.IsDBNull(0) ? "?" : rs.GetDateTime(0).ToString("yyyy-MM-dd")) + "--" + (rs.IsDBNull(1) ? "?" : rs.GetDateTime(1).ToString("yyyy-MM-dd")) + ", ");
							ExtendedSurveyFinished.Text = ExtendedSurveyFinished.Text.Replace("[x]", "Period: " + (u.Started != null ? "?" : u.Started.Value.ToString("yyyy-MM-dd")) + "--" + (u.Closed != null ? "?" : u.Closed.Value.ToString("yyyy-MM-dd")) + ", ");
						}
						if (
							//!rs.IsDBNull(0) && rs.GetDateTime(0) <= DateTime.Now
							//&&
//							(rs.IsDBNull(1) || rs.GetDateTime(1) >= DateTime.Now)
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
//					rs.Close();

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
//			Db.exec("UPDATE Sponsor SET " +
//			        "InviteTxt = '" + InviteTxt.Text.Replace("'", "''") + "', " +
//			        "InviteReminderTxt = '" + InviteReminderTxt.Text.Replace("'", "''") + "', " +
//			        "AllMessageSubject = '" + AllMessageSubject.Text.Replace("'", "''") + "', " +
//			        "LoginTxt = '" + LoginTxt.Text.Replace("'", "''") + "', " +
//			        "InviteSubject = '" + InviteSubject.Text.Replace("'", "''") + "', " +
//			        "InviteReminderSubject = '" + InviteReminderSubject.Text.Replace("'", "''") + "', " +
//			        "AllMessageBody = '" + AllMessageBody.Text.Replace("'", "''") + "', " +
//			        "LoginSubject = '" + LoginSubject.Text.Replace("'", "''") + "', " +
//			        "LoginDays = " + LoginDays.SelectedValue + ", " +
//			        "LoginWeekday = " + LoginWeekday.SelectedValue + " " +
//			        "WHERE SponsorID = " + sponsorID);
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
					LoginWeekday = Convert.ToInt32(LoginWeekday.SelectedValue),
					Id = sponsorID
				}
			);

			if ((ExtendedSurveyFinishedSubject.Visible || ExtendedSurveySubject.Visible) && sponsorExtendedSurveyID != 0)
			{
//				Db.exec("UPDATE SponsorExtendedSurvey " +
//				        "SET " +
//				        "EmailSubject = '" + ExtendedSurveySubject.Text.Replace("'", "''") + "', " +
//				        "EmailBody = '" + ExtendedSurveyTxt.Text.Replace("'", "''") + "', " +
//				        "FinishedEmailSubject = '" + ExtendedSurveyFinishedSubject.Text.Replace("'", "''") + "', " +
//				        "FinishedEmailBody = '" + ExtendedSurveyFinishedTxt.Text.Replace("'", "''") + "' " +
//				        "WHERE SponsorExtendedSurveyID = " + sponsorExtendedSurveyID);
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
//				SqlDataReader rs;
				bool valid = (HttpContext.Current.Session["SponsorAdminID"].ToString() == "-1");
				if (!valid)
				{
//					rs = Db.rs("SELECT " +
//					           "SponsorAdminID FROM SponsorAdmin " +
//					           "WHERE SponsorID = " + sponsorID + " " +
//					           "AND SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " " +
//					           "AND Pas = '" + Password.Text.Replace("'", "''") + "'");
					int sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
					var a = sponsorRepository.ReadSponsorAdmin(sponsorID, sponsorAdminID, Password.Text);
//					if (rs.Read() && rs.GetInt32(0) == Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]))
					if (a != null && a.Id == sponsorAdminID)
					{
						valid = true;
					}
					else
					{
						incorrectPassword = true;
					}
//					rs.Close();
				}

				if (valid)
				{
					int cx = 0;
					int bx = 0;

					switch (Convert.ToInt32(SendType.SelectedValue))
					{
						case 1:
							#region Invite
//							Db.exec("UPDATE Sponsor SET InviteLastSent = GETDATE() WHERE SponsorID = " + sponsorID); // TODO: move to department???
							sponsorRepository.UpdateSponsorLastInviteSent(sponsorID);

//							rs = Db.rs("SELECT DISTINCT " +
//							           "si.SponsorInviteID, " +
//							           "si.Email, " +
//							           "LEFT(REPLACE(CONVERT(VARCHAR(255),si.InvitationKey),'-',''),8) " +
//							           "FROM SponsorInvite si " +
//							           (HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
//							            "INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID " +
//							            "WHERE sad.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " " +
//							            "AND " : "WHERE ") + "si.SponsorID = " + sponsorID + " " +
//							           "AND si.UserID IS NULL " +
//							           "AND si.StoppedReason IS NULL " +
//							           "AND si.Sent IS NULL");
//							while (rs.Read())
							int sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
							foreach (var i in sponsorRepository.FindInvitesBySponsor(sponsorID, sponsorAdminID))
							{
//								bool success = Db.sendInvitation(rs.GetInt32(0), rs.GetString(1), InviteTxt.Text, InviteSubject.Text, rs.GetString(2));
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
//							rs.Close();
							#endregion
							break;
						case 2:
							#region Invite reminder
//							Db.exec("UPDATE Sponsor SET InviteReminderLastSent = GETDATE() WHERE SponsorID = " + sponsorID); // TODO: move to department???
							sponsorRepository.UpdateSponsorLastInviteReminderSent(sponsorID);

//							rs = Db.rs("SELECT DISTINCT " +
//							           "si.SponsorInviteID, " +
//							           "si.Email, " +
//							           "LEFT(REPLACE(CONVERT(VARCHAR(255),si.InvitationKey),'-',''),8) " +
//							           "FROM SponsorInvite si " +
//							           (HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
//							            "INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID " +
//							            "WHERE sad.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " " +
//							            "AND " : "WHERE ") + "si.UserID IS NULL " +
//							           "AND si.SponsorID = " + sponsorID + " " +
//							           "AND si.Sent IS NOT NULL " +
//							           "AND si.StoppedReason IS NULL " +
//							           "AND DATEADD(hh,1,si.Sent) < GETDATE()");
//							while (rs.Read())
							sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
							foreach (var i in sponsorRepository.FindSentInvitesBySponsor(sponsorID, sponsorAdminID))
							{
//								bool success = Db.sendInvitation(rs.GetInt32(0), rs.GetString(1), InviteReminderTxt.Text, InviteReminderSubject.Text, rs.GetString(2));
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
//							rs.Close();
							#endregion
							break;
						case 3:
							#region Login reminder
//							Db.exec("UPDATE Sponsor SET LoginLastSent = GETDATE() WHERE SponsorID = " + sponsorID); // TODO: move to department???
							sponsorRepository.UpdateSponsorLastLoginSent(sponsorID);

//							rs = Db.rs("SELECT DISTINCT " +
//							           "u.UserID, " +
//							           "u.Email, " +
//							           "u.ReminderLink, " +
//							           "LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12) " +
//							           "FROM [User] u " +
//							           "INNER JOIN SponsorInvite si ON u.UserID = si.UserID " +
//							           (HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
//							            "INNER JOIN SponsorAdminDepartment sad ON u.DepartmentID = sad.DepartmentID " +
//							            "WHERE sad.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " " +
//							            "AND " : "WHERE ") + "u.SponsorID = " + sponsorID + " " +
//							           "AND u.Email IS NOT NULL " +
//							           "AND u.Email <> '' " +
//							           "AND si.StoppedReason IS NULL " +
//							           "AND u.Email NOT LIKE '%DELETED' " +
//							           "AND dbo.cf_daysFromLastLogin(u.UserID) >= " + LoginDays.SelectedValue + " " +
//							           "AND (u.ReminderLastSent IS NULL OR DATEADD(hh,1,u.ReminderLastSent) < GETDATE())");
//							while (rs.Read())
							sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
							int selectedValue = Convert.ToInt32(LoginDays.SelectedValue);
							foreach (var u in userRepository.FindBySponsorWithLoginDays(sponsorID, sponsorAdminID, selectedValue))
							{
								bool success = false;
								bool badEmail = false;
//								if (Db.isEmail(rs.GetString(1)))
								if (Db.isEmail(u.Email))
								{
									try
									{
										string body = LoginTxt.Text;

										string personalLink = "" + System.Configuration.ConfigurationSettings.AppSettings["healthWatchURL"] + "";
//										if (!rs.IsDBNull(2) && rs.GetInt32(2) > 0)
										if (u.ReminderLink > 0)
										{
//											personalLink += "/c/" + rs.GetString(3).ToLower() + rs.GetInt32(0).ToString();
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

//										Db.sendMail(rs.GetString(1), body, LoginSubject.Text);
										Db.sendMail(u.Email, body, LoginSubject.Text);

//										Db.exec("UPDATE [User] SET ReminderLastSent = GETDATE() WHERE UserID = " + rs.GetInt32(0));
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
//									Db.exec("UPDATE [User] SET EmailFailure = GETDATE() WHERE UserID = " + rs.GetInt32(0));
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
//							rs.Close();
							#endregion
							break;
						case 4:
							#region Extended survey
//							Db.exec("UPDATE SponsorExtendedSurvey SET EmailLastSent = GETDATE() WHERE SponsorExtendedSurveyID = " + sponsorExtendedSurveyID);
							sponsorRepository.UpdateExtendedSurveyLastEmailSent(sponsorExtendedSurveyID);

//							string sql = "SELECT DISTINCT " +
//								"u.UserID, " +
//								"u.Email, " +
//								"u.ReminderLink, " +
//								"LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12) " +
//								"FROM [User] u " +
//								"INNER JOIN Department d ON u.DepartmentID = d.DepartmentID " +
//								"INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = " + sponsorExtendedSurveyID + " " +
//								"INNER JOIN SponsorInvite si ON u.UserID = si.UserID AND si.SponsorID = ses.SponsorID " +
//								"INNER JOIN eform..ProjectRound pr ON pr.ProjectRoundID = ses.ProjectRoundID " +
//								"LEFT OUTER JOIN UserSponsorExtendedSurvey x ON u.UserID = x.UserID AND x.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID AND x.AnswerID IS NOT NULL " +
//								"LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON si.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID " +
//								(HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
//								 "INNER JOIN SponsorAdminDepartment sad ON u.DepartmentID = sad.DepartmentID " +
//								 "WHERE sad.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " " +
//								 "AND " : "WHERE ") + "u.SponsorID = " + sponsorID + " " +
//								"AND (pr.Started <= GETDATE() OR ISNULL(d.PreviewExtendedSurveys,si.PreviewExtendedSurveys) IS NOT NULL) " +
//								"AND x.AnswerID IS NULL " +
//								"AND u.Email IS NOT NULL " +
//								"AND u.Email <> '' " +
//								"AND si.StoppedReason IS NULL " +
//								"AND sesd.Hide IS NULL " +
//								"AND u.Email NOT LIKE '%DELETED'";
							//HttpContext.Current.Response.Write(sql);
							//HttpContext.Current.Response.End();
//							rs = Db.rs(sql);
//							while (rs.Read())
							sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
							foreach (var u in userRepository.FindBySponsorWithExtendedSurvey2(sponsorID, sponsorAdminID, sponsorExtendedSurveyID))
							{
								bool success = false;
								bool badEmail = false;
//								if (Db.isEmail(rs.GetString(1)))
								if (Db.isEmail(u.Email))
								{
									try
									{
										string body = ExtendedSurveyTxt.Text;

										string personalLink = "" + System.Configuration.ConfigurationSettings.AppSettings["healthWatchURL"] + "";
//										if (!rs.IsDBNull(2) && rs.GetInt32(2) > 0)
										if (u.ReminderLink > 0)
										{
//											personalLink += "/c/" + rs.GetString(3).ToLower() + rs.GetInt32(0).ToString();
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

//										Db.sendMail(rs.GetString(1), body, ExtendedSurveySubject.Text);
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
//									Db.exec("UPDATE [User] SET EmailFailure = GETDATE() WHERE UserID = " + rs.GetInt32(0));
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
//							rs.Close();
							#endregion
							break;
						case 5:
							#region Thank you: Extended survey
//							Db.exec("UPDATE SponsorExtendedSurvey SET FinishedLastSent = GETDATE() WHERE SponsorExtendedSurveyID = " + sponsorExtendedSurveyID);
							sponsorRepository.UpdateExtendedSurveyLastFinishedSent(sponsorExtendedSurveyID);

//							rs = Db.rs("SELECT DISTINCT " +
//							           "u.UserID, " +
//							           "u.Email, " +
//							           "u.ReminderLink, " +
//							           "LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12) " +
//							           "FROM [User] u " +
//							           "INNER JOIN Department d ON u.DepartmentID = d.DepartmentID " +
//							           "INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = " + sponsorExtendedSurveyID + " " +
//							           "INNER JOIN SponsorInvite si ON u.UserID = si.UserID AND si.SponsorID = ses.SponsorID " +
//							           "INNER JOIN eform..ProjectRound pr ON pr.ProjectRoundID = ses.ProjectRoundID " +
//							           "INNER JOIN UserSponsorExtendedSurvey x ON u.UserID = x.UserID AND x.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID AND x.AnswerID IS NOT NULL " +
//							           "LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON si.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID " +
//							           (HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
//							            "INNER JOIN SponsorAdminDepartment sad ON u.DepartmentID = sad.DepartmentID " +
//							            "WHERE sad.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " " +
//							            "AND " : "WHERE ") + "u.SponsorID = " + sponsorID + " " +
//							           "AND x.FinishedEmail IS NULL " +
//							           "AND x.AnswerID IS NOT NULL " +
//							           "AND u.Email IS NOT NULL " +
//							           "AND u.Email <> '' " +
//							           "AND si.StoppedReason IS NULL " +
//							           "AND sesd.Hide IS NULL " +
//							           "AND u.Email NOT LIKE '%DELETED'");
//							while (rs.Read())
							sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
							foreach (var u in userRepository.FindBySponsorWithExtendedSurvey(sponsorID, sponsorAdminID, sponsorExtendedSurveyID))
							{
								bool success = false;
								bool badEmail = false;
//								if (Db.isEmail(rs.GetString(1)))
								if (Db.isEmail(u.Email))
								{
									try
									{
										string body = ExtendedSurveyFinishedTxt.Text;

										string personalLink = "" + System.Configuration.ConfigurationSettings.AppSettings["healthWatchURL"] + "";
//										if (!rs.IsDBNull(2) && rs.GetInt32(2) > 0)
										if (u.ReminderLink > 0)
										{
//											personalLink += "/c/" + rs.GetString(3).ToLower() + rs.GetInt32(0).ToString();
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

//										Db.sendMail(rs.GetString(1), body, ExtendedSurveyFinishedSubject.Text);
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
//									Db.exec("UPDATE [User] SET EmailFailure = GETDATE() WHERE UserID = " + rs.GetInt32(0));
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
//							rs.Close();
							#endregion
							break;
						case 9:
							#region All activated
//							Db.exec("UPDATE Sponsor SET AllMessageLastSent = GETDATE() WHERE SponsorID = " + sponsorID); // TODO: move to department???
							sponsorRepository.UpdateLastAllMessageSent(sponsorID);

//							rs = Db.rs("SELECT DISTINCT " +
//							           "u.UserID, " +
//							           "u.Email " +
//							           "FROM [User] u " +
//							           "INNER JOIN SponsorInvite si ON u.UserID = si.UserID " +
//							           (HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
//							            "INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID " +
//							            "WHERE sad.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " " +
//							            "AND " : "WHERE ") + "si.SponsorID = " + sponsorID + " " +
//							           "AND u.Email IS NOT NULL " +
//							           "AND u.Email <> '' " +
//							           "AND si.StoppedReason IS NULL " +
//							           "AND u.Email NOT LIKE '%DELETED'");
//							while (rs.Read())
							sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
							foreach (var u in userRepository.Find2(sponsorID, sponsorAdminID))
							{
								bool success = false;
								bool badEmail = false;
//								if (Db.isEmail(rs.GetString(1)))
								if (Db.isEmail(u.Email))
								{
									try
									{
//										Db.sendMail(rs.GetString(1), AllMessageBody.Text, AllMessageSubject.Text);
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
//									Db.exec("UPDATE [User] SET EmailFailure = GETDATE() WHERE UserID = " + rs.GetInt32(0));
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
//							rs.Close();
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