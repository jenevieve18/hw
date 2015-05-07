using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Odbc;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace eform
{
	/// <summary>
	/// Summary description for usermgr.
	/// </summary>
	public class usermgr : System.Web.UI.Page
	{
		protected PlaceHolder LoginBox;
		protected TextBox Username;
		protected TextBox Password;
		protected Button Login;

		protected PlaceHolder LoggedIn;

		protected PlaceHolder EditUser;
		protected TextBox Email;
		protected DropDownList ProjectRoundUnitID;
		protected CheckBox Terminated;
		protected CheckBox Extended;
		protected Label Started;
		protected Label Ended;
		protected Button Cancel;
		protected Button Save;

		protected TextBox SearchEmail;
		protected Label Stats;
		protected Button Search;
		protected Button AddUser;
		protected Label SearchResults;

		protected Label EmailLabel;
		protected Label SearchEmailLabel;
		protected PlaceHolder ExtendedPH;
		protected PlaceHolder EmailPH;
		protected PlaceHolder UnitPH;
		protected PlaceHolder TerminatedPH;
		protected Label CodeTxt;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(HttpContext.Current.Session["UserMgrLoggedIn"] == null)
			{
				LoginBox.Visible = true;
				LoggedIn.Visible = false;

				Login.Click += new EventHandler(Login_Click);
			}
			else
			{
				LoginBox.Visible = false;
				LoggedIn.Visible = true;

				AddUser.Visible = Convert.ToBoolean(HttpContext.Current.Session["UserMgrAddUser"].ToString());
				EmailPH.Visible = !Convert.ToBoolean(HttpContext.Current.Session["UserMgrUseExternalID"].ToString());
				UnitPH.Visible = Convert.ToBoolean(HttpContext.Current.Session["UserMgrSeeUnit"].ToString());
				EmailLabel.Text = "E-post";
				SearchEmailLabel.Text = (Convert.ToBoolean(HttpContext.Current.Session["UserMgrUseExternalID"].ToString()) ? "Sök kod" : "Sök e-post");
				ExtendedPH.Visible = (Convert.ToBoolean(HttpContext.Current.Session["UserMgrHasExtended"]));
				TerminatedPH.Visible = (Convert.ToBoolean(HttpContext.Current.Session["UserMgrSeeTerminated"]));
				Save.Visible = 
					Convert.ToBoolean(HttpContext.Current.Session["UserMgrSeeUnit"].ToString())
					||
					Convert.ToBoolean(HttpContext.Current.Session["UserMgrAddUser"].ToString())
					||
					(Convert.ToBoolean(HttpContext.Current.Session["UserMgrHasExtended"]))
					||
					(Convert.ToBoolean(HttpContext.Current.Session["UserMgrSeeTerminated"]))
					||
					!Convert.ToBoolean(HttpContext.Current.Session["UserMgrUseExternalID"].ToString());

				if(!IsPostBack)
				{
					if(Convert.ToBoolean(HttpContext.Current.Session["UserMgrExpandAll"]))
					{
						search("");
					}
				}

				if(HttpContext.Current.Request.QueryString["PRUID"] != null)
				{
					EditUser.Visible = true;

					if(!IsPostBack)
					{
						Email.Text = "";
						Terminated.Checked = false;
						Extended.Checked = false;
						Started.Text = "-";
						Ended.Text = "-";

						ProjectRoundUnitID.Attributes["style"] += "font-size:8px;";
						OdbcDataReader rs = Db.recordSet("SELECT ProjectRoundUnitID, dbo.cf_ProjectUnitTree(ProjectRoundUnitID,' » ') AS Unit, ID FROM ProjectRoundUnit WHERE ProjectRoundID = " + Convert.ToInt32(HttpContext.Current.Session["UserMgrPRID"]) + " ORDER BY SortString");
						while(rs.Read())
						{
							ProjectRoundUnitID.Items.Add(new ListItem(rs.GetString(2) + " " + rs.GetString(1).ToLower().Replace("avdelningen","").Replace("sektionen","").Replace("avdelning","").Replace("sektion","").Replace("karolinska","KUS"),rs.GetInt32(0).ToString()));
						}
						rs.Close();

						if(HttpContext.Current.Request.QueryString["PRUID"] != "0")
						{
							string AK = "";
							string UK = "";
							int AID = 0;
							string invitationSubject = "", invitationBody = "", mailfrom = "", reminderSubject = "", reminderBody = "";

							rs = Db.recordSet("SELECT " +
								"u.ProjectRoundUserID, " +									// 0
								"u.Email, " +
								"u.ProjectRoundUnitID, " +
								"u.Terminated, " +
								"u.Extended, " +
								"a.StartDT, " +												// 5
								"a.EndDT, " +
								"a.CurrentPage, " +
								"REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-',''), " +
								"u.UserKey, " +
								"a.AnswerID, " +											// 10
								"rl.InvitationSubject, " +
								"rl.InvitationBody, " +
								"rl.ExtraInvitationSubject, " +
								"rl.ExtraInvitationBody, " +
								"r.EmailFromAddress, " +									// 15
								"rl.ReminderSubject, " +
								"rl.ReminderBody, " +
								"rl.ExtraReminderSubject, " +
								"rl.ExtraReminderBody, " +
								"p.AppURL, " +												// 20
								"RIGHT(CONVERT(VARCHAR(255),u.UserKey),4), " +
								"u.ExternalID " +
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRound r ON u.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Project p ON r.ProjectID = p.ProjectID " +
								"INNER JOIN ProjectRoundLang rl ON r.ProjectRoundID = rl.ProjectRoundID AND rl.LangID = dbo.cf_unitLangID(u.ProjectRoundUnitID) " +
								"LEFT OUTER JOIN Answer a ON u.ProjectRoundUserID = a.ProjectRoundUserID " +
								"WHERE u.ProjectRoundUserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["PRUID"]));
							if(rs.Read())
							{
								CodeTxt.Text = "";
								if(Convert.ToBoolean(Convert.ToBoolean(HttpContext.Current.Session["UserMgrUseExternalID"])))
								{
									CodeTxt.Text = "ID " + (rs.IsDBNull(22) ? "&lt; missing &gt;" : rs.GetInt64(22).ToString()) + "<br/>";
								}
								if(Convert.ToBoolean(HttpContext.Current.Session["UserMgrUseCode"]))
								{
									CodeTxt.Text += "Kod " + rs.GetString(21) + rs.GetInt32(0).ToString() + "<br/>";
								}
								Email.Text = rs.GetString(1);
								ProjectRoundUnitID.SelectedValue = rs.GetInt32(2).ToString();
								Terminated.Checked = (!rs.IsDBNull(3));
								Extended.Checked = (!rs.IsDBNull(4));
								Started.Text = 
									(!rs.IsDBNull(5) ? rs.GetDateTime(5).ToString("yyyy-MM-dd HH:mm") : "<B>Nej</B>") + 
									// Skicka länken, skicka påminnelse
									(!Convert.ToBoolean(HttpContext.Current.Session["UserMgrUseCode"]) && rs.IsDBNull(6) ? " [<A HREF=\"usermgr.aspx?PRUID=" + HttpContext.Current.Request.QueryString["PRUID"] + "&SendLink=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">skicka länken</A>] [<A HREF=\"usermgr.aspx?PRUID=" + HttpContext.Current.Request.QueryString["PRUID"] + "&SendLink=2&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">skicka påminnelse</A>]" : "");
								AK = (!rs.IsDBNull(8) ? rs.GetString(8) : "");
								UK = (!rs.IsDBNull(20) ? rs.GetString(20) : "http://eform.se") + "/submit.aspx?K=" + rs.GetGuid(9).ToString().Substring(0,8) + rs.GetInt32(0).ToString();

								Ended.Text = 
									(
									!rs.IsDBNull(6) 
									? 
									rs.GetDateTime(6).ToString("yyyy-MM-dd HH:mm") + 
									" [<A HREF=\"usermgr.aspx?PRUID=" + HttpContext.Current.Request.QueryString["PRUID"] + "&ResetAnswer=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">avmarkera som inskickad</A>]" + 
									(Convert.ToBoolean(HttpContext.Current.Session["UserMgrHasFeedback"]) && Convert.ToBoolean(HttpContext.Current.Session["UserMgrUseCode"]) ? " [<A HREF=\"usermgr.aspx?PRUID=" + HttpContext.Current.Request.QueryString["PRUID"] + "&SendFeedback=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">skicka återkoppling</A>]" : "") +
									(Convert.ToBoolean(HttpContext.Current.Session["UserMgrSeeAnswer"]) ? " [<A HREF=\"JavaScript:void(window.open('" + UK + "&SM=" + AK.Substring(0,8) + "','',''));\">visa enkät</A>]" : "") + 
									(Convert.ToBoolean(HttpContext.Current.Session["UserMgrSeeFeedback"]) ? " [<A HREF=\"JavaScript:void(window.open('http://webbqps.se/archive/" + AK + ".pdf','',''));\">visa återkoppling</A>]" : "")
									: 
									(!rs.IsDBNull(5) && !rs.IsDBNull(7) ? "Sida " + rs.GetInt32(7) : "-") +
									(Convert.ToBoolean(HttpContext.Current.Session["UserMgrSeeSurvey"]) ? " [<A HREF=\"JavaScript:void(window.open('" + UK + "','',''));\">starta/öppna enkät för ifyllnad/redigering</A>]" : "")
									);
								
								AID = (!rs.IsDBNull(10) ? rs.GetInt32(10) : 0);
								invitationSubject = (rs.IsDBNull(4) ? rs.GetString(11) : rs.GetString(13));
								invitationBody = (rs.IsDBNull(4) ? rs.GetString(12) : rs.GetString(14));
								reminderSubject = (rs.IsDBNull(4) ? rs.GetString(16) : rs.GetString(18));
								reminderBody = (rs.IsDBNull(4) ? rs.GetString(17) : rs.GetString(19));
								mailfrom = rs.GetString(15);
							}
							rs.Close();

							if(HttpContext.Current.Request.QueryString["SendLink"] != null && UK != "")
							{
								bool reminder = (Convert.ToInt32(HttpContext.Current.Request.QueryString["SendLink"]) == 2);
								string body = (reminder ? reminderBody : invitationBody);
								string subject = (reminder ? reminderSubject : invitationSubject);
								try
								{
									if(body.IndexOf("<LINK>") >= 0)
									{
										body = body.Replace("<LINK>",UK);
									}
									else
									{
										body += "\r\n\r\n" + UK;
									}

									System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
									msg.To = Email.Text;
									msg.From = mailfrom;
									msg.Subject = subject;
									msg.Body = body;
									msg.BodyFormat = System.Web.Mail.MailFormat.Text;
									msg.BodyEncoding = System.Text.Encoding.GetEncoding(1252);
									System.Web.Mail.SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
									System.Web.Mail.SmtpMail.Send(msg);
								}
								catch(Exception){}

								HttpContext.Current.Response.Redirect("usermgr.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&PRUID=" + HttpContext.Current.Request.QueryString["PRUID"],true);
							}
							if(HttpContext.Current.Request.QueryString["SendFeedback"] != null && AK != "")
							{
								try
								{
									System.Web.Mail.MailAttachment attachment = new	System.Web.Mail.MailAttachment("c:\\inetpub\\wwwroot\\eform.se\\archive\\" + AK + ".pdf");
									System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
									msg.To = Email.Text;
									msg.From = mailfrom;
									msg.Subject = "Återkoppling på enkäten";
									msg.Body = "Se bifogad fil.";
									msg.Attachments.Add(attachment);
									msg.BodyFormat = System.Web.Mail.MailFormat.Text;
									msg.BodyEncoding = System.Text.Encoding.GetEncoding(1252);
									System.Web.Mail.SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
									System.Web.Mail.SmtpMail.Send(msg);
								}
								catch(Exception){}

								HttpContext.Current.Response.Redirect("usermgr.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&PRUID=" + HttpContext.Current.Request.QueryString["PRUID"],true);
							}
							if(HttpContext.Current.Request.QueryString["ResetAnswer"] != null && AID != 0)
							{
								Db.execute("UPDATE Answer SET EndDT = NULL WHERE AnswerID = " + AID);
								HttpContext.Current.Response.Redirect("usermgr.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&PRUID=" + HttpContext.Current.Request.QueryString["PRUID"],true);
							}
						}
					}

					Save.Click += new EventHandler(Save_Click);
					Cancel.Click += new EventHandler(Cancel_Click);
				}

				Search.Click += new EventHandler(Search_Click);
				AddUser.Click += new EventHandler(AddUser_Click);

				if(!IsPostBack)
				{
					bool noUsers = false;

					OdbcDataReader rs = Db.recordSet("SELECT COUNT(DISTINCT p.ProjectRoundUserID), COUNT(DISTINCT a.StartDT), COUNT(DISTINCT a.EndDT) FROM ProjectRoundUser p LEFT OUTER JOIN Answer a ON p.ProjectRoundUserID = a.ProjectRoundUserID WHERE p.ProjectRoundID = " + Convert.ToInt32(HttpContext.Current.Session["UserMgrPRID"]) + " AND p.Terminated IS NULL");
					if(rs.Read())
					{
						if(rs.IsDBNull(0) || rs.GetInt32(0) == 0)
						{
							noUsers = true;
						}
						else
						{
							Stats.Text = "&nbsp;&nbsp;TOTALT: " + rs.GetInt32(0) + ", SVARAT: " + rs.GetInt32(2) + " (" + Convert.ToInt32((float)rs.GetInt32(2)/(float)rs.GetInt32(0)*100) + "%)";
						}
					}
					rs.Close();

					if(noUsers)
					{
						int langID = 0; string surveyID = "0";

						Stats.Text = "<TABLE><TR><TD><B>Unit</B>&nbsp;</TD><TD><B>Started</B>&nbsp;</TD><TD><B>Submitted</B>&nbsp;</TD></TR>";
						rs = Db.recordSet("SELECT " +
							"u.Unit, " +
							"COUNT(a.StartDT), " +
							"COUNT(a.EndDT), " +
							"dbo.cf_unitSurveyID(u.ProjectRoundUnitID), " +
							"r.LangID " +
							"FROM ProjectRoundUnit u " +
							"INNER JOIN ProjectRound r ON u.ProjectRoundID = r.ProjectRoundID " +
							"LEFT OUTER JOIN Answer a ON u.ProjectRoundUnitID = a.ProjectRoundUnitID " +
							"WHERE u.ProjectRoundID = " + Convert.ToInt32(HttpContext.Current.Session["UserMgrPRID"]) + " " +
							"GROUP BY u.Unit, u.ProjectRoundUnitID, r.LangID");
						while(rs.Read())
						{
							surveyID += "," + rs.GetInt32(3);
							langID = rs.GetInt32(4);
							Stats.Text += "<TR><TD>" + rs.GetString(0) + "&nbsp;</TD><TD>" + rs.GetInt32(1) + "&nbsp;</TD><TD>" + rs.GetInt32(2) + "</TD></TR>";
						}
						rs.Close();
						Stats.Text += "</TABLE>";
						Stats.Text += "<BR/><B>SPSS export</B>";
						rs = Db.recordSet("SELECT SurveyID, Internal FROM Survey WHERE SurveyID IN (" + surveyID + ")");
						while(rs.Read())
						{
							Stats.Text += "<BR/>[<A HREF=\"userMgr.aspx?Export=0&SurveyID=" + rs.GetInt32(0) + "\">all started</A>] or [<A HREF=\"userMgr.aspx?Export=1&SurveyID=" + rs.GetInt32(0) + "\">only submitted</A>] variable template " + rs.GetString(1);
						}
						rs.Close();

						SearchEmailLabel.Visible = false;
						SearchEmail.Visible = false;
						Search.Visible = false;

						if(HttpContext.Current.Request.QueryString["Export"] != null)
						{
							if(Convert.ToInt32(HttpContext.Current.Request.QueryString["Export"]) == 0)
							{
								Db.execExport(Convert.ToInt32(HttpContext.Current.Session["UserMgrPRID"]),0,Convert.ToInt32(HttpContext.Current.Request.QueryString["SurveyID"]),langID,0,1,1,0,0);
							}
							else
							{
								Db.execExport(Convert.ToInt32(HttpContext.Current.Session["UserMgrPRID"]),0,Convert.ToInt32(HttpContext.Current.Request.QueryString["SurveyID"]),langID,0,0,1,0,0);
							}
						}

										}

					if(HttpContext.Current.Request.QueryString["Missing"] != null)
					{
						rs = Db.recordSet("SELECT " +
							"u.Email, " +
							"a.EndDT, " +
							"REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-','') " +
							"FROM ProjectRoundUser u " +
							"INNER JOIN ProjectRound r ON u.ProjectRoundID = r.ProjectRoundID " +
							"INNER JOIN Answer a ON u.ProjectRoundUserID = a.ProjectRoundUserID " +
							"WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = " + Convert.ToInt32(HttpContext.Current.Session["UserMgrPRID"]) + " ORDER BY a.EndDT");
						while(rs.Read())
						{
							if(!System.IO.File.Exists("c:\\inetpub\\wwwroot\\eform.se\\archive\\" + rs.GetString(2) + ".pdf"))
							{
								SearchResults.Text += rs.GetDateTime(1).ToString("yyyy-MM-dd HH:mm:ss") + " " + rs.GetString(0) + "<BR>";
							}
						}
						rs.Close();
					}
				}
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		private void Login_Click(object sender, EventArgs e)
		{
			OdbcDataReader rs = Db.recordSet("SELECT " +
				"m.ManagerID, " +			// 0
				"mp.ProjectRoundID, " +
				"pr.UseCode, " +
				"m.AddUser, " +
				"m.SeeAnswer, " +
				"m.ExpandAll, " +			// 5
				"m.UseExternalID, " +
				"pr.ExtendedSurveyID, " +
				"m.SeeFeedback, " +
				"m.HasFeedback, " +
				"m.SeeUnit, " +				// 10
				"m.SeeTerminated, " +
				"m.SeeSurvey " +
				"FROM Manager m " +
				"INNER JOIN ManagerProjectRound mp ON m.ManagerID = mp.ManagerID " +
				"INNER JOIN ProjectRound pr ON mp.ProjectRoundID = pr.ProjectRoundID " +
				"WHERE m.Name = '" + Username.Text.Trim().Replace("'","") + "' AND m.Password = '" + Password.Text.Trim().Replace("'","''") + "'");
			if(rs.Read())
			{
				// ManagerID of manager logged in
				HttpContext.Current.Session["UserMgrLoggedIn"] = rs.GetInt32(0);
				// ProjectRoundID of manager logged in
				HttpContext.Current.Session["UserMgrPRID"] = rs.GetInt32(1);
				// Shows login code for users
				HttpContext.Current.Session["UserMgrUseCode"] = (!rs.IsDBNull(2) && rs.GetInt32(2) == 1);
				// Manager can add users
				HttpContext.Current.Session["UserMgrAddUser"] = (!rs.IsDBNull(3) && rs.GetInt32(3) == 1);
				// Manager can see answers
				HttpContext.Current.Session["UserMgrSeeAnswer"] = (!rs.IsDBNull(4) && rs.GetInt32(4) == 1);
				// Perform match all search when login
				HttpContext.Current.Session["UserMgrExpandAll"] = (!rs.IsDBNull(5) && rs.GetInt32(5) == 1);
				// Replace email with external ID
				HttpContext.Current.Session["UserMgrUseExternalID"] = (!rs.IsDBNull(6) && rs.GetInt32(6) == 1);
				// Has extended survey
				HttpContext.Current.Session["UserMgrHasExtended"] = (!rs.IsDBNull(7));
				// Manager can see feedback
				HttpContext.Current.Session["UserMgrSeeFeedback"] = (!rs.IsDBNull(8) && rs.GetInt32(8) == 1);
				// Users have feedback
				HttpContext.Current.Session["UserMgrHasFeedback"] = (!rs.IsDBNull(9) && rs.GetInt32(9) == 1);
				// Manager can see unit
				HttpContext.Current.Session["UserMgrSeeUnit"] = (!rs.IsDBNull(10) && rs.GetInt32(10) == 1);
				// Manager can see terminated
				HttpContext.Current.Session["UserMgrSeeTerminated"] = (!rs.IsDBNull(11) && rs.GetInt32(11) == 1);
				// Manager can see survey
				HttpContext.Current.Session["UserMgrSeeSurvey"] = (!rs.IsDBNull(12) && rs.GetInt32(12) == 1);
			}
			rs.Close();
			HttpContext.Current.Response.Redirect("usermgr.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
		}

		private void search(string str)
		{
			SearchResults.Text = "<TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"5\"><TR><TD>" + (Convert.ToBoolean(HttpContext.Current.Session["UserMgrUseExternalID"]) ? "ID" : "Email") + "</TD>" + (Convert.ToBoolean(HttpContext.Current.Session["UserMgrUseCode"]) ? "<TD>Kod</TD>" : "") + (Convert.ToBoolean(HttpContext.Current.Session["UserMgrSeeUnit"].ToString()) ? "<TD>Grupp</TD>" : "") + (Convert.ToBoolean(HttpContext.Current.Session["UserMgrSeeTerminated"].ToString()) ? "<TD>Avreg</TD>" : "") + (Convert.ToBoolean(HttpContext.Current.Session["UserMgrHasExtended"]) ? "<TD>Utökad</TD>" : "") + "<TD>Startat</TD><TD>Inskickad</TD></TR>";

			OdbcDataReader rs = Db.recordSet("SELECT " +
				"u.ProjectRoundUserID, " +		// 0
				"u.Email, " +
				"r.ID, " +
				"u.Terminated, " +
				"u.Extended, " +
				"a.StartDT, " +					// 5
				"a.EndDT, " +
				"a.CurrentPage, " +
				"u.ExternalID, " +
				"RIGHT(CONVERT(VARCHAR(255),u.UserKey),4) " +
				"FROM ProjectRoundUser u " +
				"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
				"LEFT OUTER JOIN Answer a ON u.ProjectRoundUserID = a.ProjectRoundUserID " +
				"WHERE u.ProjectRoundID = " + Convert.ToInt32(HttpContext.Current.Session["UserMgrPRID"]) + " " + str +
				"ORDER BY " + (Convert.ToBoolean(HttpContext.Current.Session["UserMgrUseExternalID"]) ? "u.ExternalID" : "u.Email"));
			while(rs.Read())
			{
				SearchResults.Text += "<TR>" +
					// Clickable text, if UserMgrUseExternalID show ExternalID else Email
					"<TD><A HREF=\"usermgr.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&PRUID=" + rs.GetInt32(0) + "\">" + (Convert.ToBoolean(HttpContext.Current.Session["UserMgrUseExternalID"]) ? (!rs.IsDBNull(8) ? rs.GetInt64(8).ToString() : "&lt; missing &gt;") : rs.GetString(1)) + "</A></TD>" +
					// If UserMgrUseCode, show code
					"" + (Convert.ToBoolean(HttpContext.Current.Session["UserMgrUseCode"]) ? "<TD>" + rs.GetString(9) + rs.GetInt32(0).ToString() + "</TD>" : "") + "" +
					// Unit
					"" + (Convert.ToBoolean(HttpContext.Current.Session["UserMgrSeeUnit"].ToString()) ? "<TD ALIGN=\"CENTER\">" + rs.GetString(2) + "</TD>" : "") + "" +
					// Terminated
					"" + (Convert.ToBoolean(HttpContext.Current.Session["UserMgrSeeTerminated"].ToString()) ? "<TD ALIGN=\"CENTER\">" + (rs.IsDBNull(3) ? "-" : "<B>X</B>") + "</TD>" : "") +
					// If has extended survey, show extended
					"" + (Convert.ToBoolean(HttpContext.Current.Session["UserMgrHasExtended"]) ? "<TD ALIGN=\"CENTER\">" + (rs.IsDBNull(4) ? "-" : "<B>X</B>") + "</TD>" : "") + "" +
					// Start date/time
					"<TD ALIGN=\"CENTER\">" + (rs.IsDBNull(5) ? "-" : rs.GetDateTime(5).ToString("dd/MM HH:mm")) + "</TD>" +
					// End date/time or progress
					"<TD ALIGN=\"CENTER\">" + (rs.IsDBNull(6) ? (rs.IsDBNull(5) || rs.IsDBNull(7) ? "-" : "Sida " + rs.GetInt32(7)) : rs.GetDateTime(6).ToString("dd/MM HH:mm")) + "</TD>" +
					"</TR>";
			}
			rs.Close();

			SearchResults.Text += "</TABLE>";
		}
		private void Search_Click(object sender, EventArgs e)
		{
			if(SearchEmail.Text == "")
			{
				search("");
			}
			else
			{
				try
				{
					search(" AND " + (Convert.ToBoolean(HttpContext.Current.Session["UserMgrUseExternalID"]) ? "u.ExternalID = " + Convert.ToInt64(SearchEmail.Text) + " " : "u.Email LIKE '%" + SearchEmail.Text.Replace("'","") + "%' "));
				}
				catch(Exception){}
			}
		}

		private void AddUser_Click(object sender, EventArgs e)
		{
			HttpContext.Current.Response.Redirect("usermgr.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&PRUID=0",true);
		}

		private void Save_Click(object sender, EventArgs e)
		{
			int PRUID = Convert.ToInt32(HttpContext.Current.Request.QueryString["PRUID"]);
			if(PRUID == 0)
			{
				Db.execute("INSERT INTO ProjectRoundUser (ProjectRoundID, ProjectRoundUnitID, Email, Name, Terminated, Extended) VALUES (" +
					"" + Convert.ToInt32(HttpContext.Current.Session["UserMgrPRID"]) + "," +
					Convert.ToInt32(ProjectRoundUnitID.SelectedValue) + "," +
					"'" + Email.Text.Replace("'","") + "'," +
					"'" + Email.Text.Replace("'","").Substring(0, + Email.Text.Replace("'","").IndexOf("@")).Replace("."," ") + "'," +
					(Terminated.Checked ? "1" : "NULL") + "," + 
					(Extended.Checked ? "1" : "NULL") + " " +
					")");
				OdbcDataReader rs = Db.recordSet("SELECT TOP 1 ProjectRoundUserID FROM ProjectRoundUser WHERE ProjectRoundID = " + Convert.ToInt32(HttpContext.Current.Session["UserMgrPRID"]) + " ORDER BY ProjectRoundUserID DESC");
				if(rs.Read())
				{
					PRUID = rs.GetInt32(0);
				}
				rs.Close();
			}
			else
			{
				Db.execute("UPDATE ProjectRoundUser SET ProjectRoundUnitID = " + Convert.ToInt32(ProjectRoundUnitID.SelectedValue) + ", Email = '" + Email.Text.Replace("'","") + "',Terminated=" + (Terminated.Checked ? "1" : "NULL") + ",Extended = " + (Extended.Checked ? "1" : "NULL") + " WHERE ProjectRoundUserID = " + PRUID);
			}
			HttpContext.Current.Response.Redirect("usermgr.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&PRUID=" + PRUID,true);
		}

		private void Cancel_Click(object sender, EventArgs e)
		{
			HttpContext.Current.Response.Redirect("usermgr.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(),true);
		}
	}
}
