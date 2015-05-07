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
	/// Summary description for dashboard.
	/// </summary>
	public class dashboard : System.Web.UI.Page
	{
		protected PlaceHolder LoginBox;
		protected TextBox Username;
		protected TextBox Password;
		protected Button Login;
		protected LinkButton DeleteUser;

		protected Button ExportButton;
		protected Button SPSS;
		protected RadioButtonList ProjectRoundID;
		protected PlaceHolder ExportSurvey;
		protected Button ExportSPSS;
		protected CheckBox IDS;
		protected CheckBox Unfinished;

		protected PlaceHolder SendVisitReminder;
		protected Label SendVisitReminderDT;
		protected Label SendVisitReminderTo;
		protected Label SendVisitReminderFrom;
		protected TextBox SendVisitReminderSubject;
		protected TextBox SendVisitReminderBody;
		protected Button SendVisitReminderButton;
		protected Label FirstLastName;

		protected Button AddVisit;
		protected Label Visits;
		protected PlaceHolder VisitPH;
		protected TextBox VisitDT;
		protected TextBox VisitNote;
		protected TextBox VisitReminder;
		protected DropDownList VisitSponsorReminderID;
		protected DropDownList VisitUPRUID;
		protected TextBox VisitEmail;

		protected Button Logout;

		protected PlaceHolder LoggedIn;
		protected Button AddUser;
		protected Label SearchResults;
		protected TextBox SearchText;
		protected Button Search;

		protected PlaceHolder EditUser;
		protected Label Edit;

		protected PlaceHolder UserNrPH;
		protected Label UserNrText;
		protected Label UserNr;

		protected PlaceHolder UserIdent1PH;
		protected PlaceHolder UserIdent2PH;
		protected PlaceHolder UserIdent3PH;
		protected PlaceHolder UserIdent4PH;
		protected PlaceHolder UserIdent5PH;
		protected PlaceHolder UserIdent6PH;
		protected PlaceHolder UserIdent7PH;
		protected PlaceHolder UserIdent8PH;
		protected PlaceHolder UserIdent9PH;
		protected PlaceHolder UserIdent10PH;
		protected Label UserIdent1Text;
		protected Label UserIdent2Text;
		protected Label UserIdent3Text;
		protected Label UserIdent4Text;
		protected Label UserIdent5Text;
		protected Label UserIdent6Text;
		protected Label UserIdent7Text;
		protected Label UserIdent8Text;
		protected Label UserIdent9Text;
		protected Label UserIdent10Text;
		protected TextBox UserIdent1;
		protected TextBox UserIdent2;
		protected TextBox UserIdent3;
		protected TextBox UserIdent4;
		protected TextBox UserIdent5;
		protected TextBox UserIdent6;
		protected TextBox UserIdent7;
		protected TextBox UserIdent8;
		protected TextBox UserIdent9;
		protected TextBox UserIdent10;
		protected Button Save;
		protected Button Cancel;
		protected Button AddNote;
		protected Button AddSurvey;
		protected Label Notes;
		protected Label Surveys;
		protected TextBox Note;
		protected PlaceHolder NotePH;
		protected Label NoteDetails;
		protected DropDownList ProjectRoundUnitID;
		protected PlaceHolder SurveyPH;
		protected TextBox SurveyNote;
		protected TextBox Email;

		protected HtmlInputHidden sendlinkUPRUID;
		protected TextBox sendlinksubject;
		protected TextBox sendlinkbody;
		protected Label sendlinkto;
		protected Label sendlinkfrom;
		protected Label sendlinksurvey;
		protected Button sendlinkbutton;

		protected Label DashboardHeader;
		protected Label LeftLogo;

		protected PlaceHolder UserCheck1PH;
		protected Label UserCheck1Text;
		protected RadioButtonList UserCheck1;
		protected PlaceHolder UserCheck2PH;
		protected Label UserCheck2Text;
		protected RadioButtonList UserCheck2;
		protected PlaceHolder UserCheck3PH;
		protected Label UserCheck3Text;
		protected RadioButtonList UserCheck3;

		string appURL = "https://secure.eform.se";

		private void Page_Load(object sender, System.EventArgs e)
		{
			HttpContext.Current.Response.Charset = "UTF-8";
			HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
			//			HttpContext.Current.Response.Charset = "ISO-8859-1";
			//			HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
			HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

			if(!HttpContext.Current.Request.IsSecureConnection)
			{
				HttpContext.Current.Response.Redirect(appURL + "/dashboard.aspx", true);
			}
			if(HttpContext.Current.Session["SponsorAdminID"] == null)
			{
				LoginBox.Visible = true;
				LoggedIn.Visible = false;

				DashboardHeader.Text = "eForm Dashboard";
				Login.Click += new EventHandler(Login_Click);
			}
			else
			{
				SPSS.Visible = !(HttpContext.Current.Session["Restricted"] != null && Convert.ToBoolean(HttpContext.Current.Session["Restricted"]));
				SPSS.Click += new EventHandler(SPSS_Click);
				ExportSPSS.Click += new EventHandler(ExportSPSS_Click);
				
				if(!IsPostBack)
				{
					OdbcDataReader rs = Db.recordSet("SELECT pr.ProjectRoundID, pr.Internal FROM Sponsor s INNER JOIN ProjectRound pr ON s.ProjectID = pr.ProjectID WHERE s.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
					while(rs.Read())
					{
						ProjectRoundID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
						if(ProjectRoundID.SelectedIndex == -1)
						{
							ProjectRoundID.SelectedValue = rs.GetInt32(0).ToString();
						}
					}
					rs.Close();

					DashboardHeader.Text = HttpContext.Current.Session["Sponsor"].ToString();
					LeftLogo.Text = "<img src=\"img/sponsor/logo" + HttpContext.Current.Session["SponsorID"] + ".gif\"/>";

					LoginBox.Visible = false;
					LoggedIn.Visible = true;

					if(HttpContext.Current.Session["UserIdent1"] != null)
					{
						UserIdent1PH.Visible = true;
						UserIdent1Text.Text = HttpContext.Current.Session["UserIdent1"].ToString();
					}
					if(HttpContext.Current.Session["UserIdent2"] != null)
					{
						UserIdent2PH.Visible = true;
						UserIdent2Text.Text = HttpContext.Current.Session["UserIdent2"].ToString();
					}
					if(HttpContext.Current.Session["UserIdent3"] != null)
					{
						UserIdent3PH.Visible = true;
						UserIdent3Text.Text = HttpContext.Current.Session["UserIdent3"].ToString();
					}
					if(HttpContext.Current.Session["UserIdent4"] != null)
					{
						UserIdent4PH.Visible = true;
						UserIdent4Text.Text = HttpContext.Current.Session["UserIdent4"].ToString();
					}
					if(HttpContext.Current.Session["UserIdent5"] != null)
					{
						UserIdent5PH.Visible = true;
						UserIdent5Text.Text = HttpContext.Current.Session["UserIdent5"].ToString();
					}
					if(HttpContext.Current.Session["UserIdent6"] != null)
					{
						UserIdent6PH.Visible = true;
						UserIdent6Text.Text = HttpContext.Current.Session["UserIdent6"].ToString();
					}
					if(HttpContext.Current.Session["UserIdent7"] != null)
					{
						UserIdent7PH.Visible = true;
						UserIdent7Text.Text = HttpContext.Current.Session["UserIdent7"].ToString();
					}
					if(HttpContext.Current.Session["UserIdent8"] != null)
					{
						UserIdent8PH.Visible = true;
						UserIdent8Text.Text = HttpContext.Current.Session["UserIdent8"].ToString();
					}
					if(HttpContext.Current.Session["UserIdent9"] != null)
					{
						UserIdent9PH.Visible = true;
						UserIdent9Text.Text = HttpContext.Current.Session["UserIdent9"].ToString();
					}
					if(HttpContext.Current.Session["UserIdent10"] != null)
					{
						UserIdent10PH.Visible = true;
						UserIdent10Text.Text = HttpContext.Current.Session["UserIdent10"].ToString();
					}
					if(HttpContext.Current.Session["UserNr"] != null)
					{
						UserNrPH.Visible = true;
						UserNrText.Text = HttpContext.Current.Session["UserNr"].ToString();
					}
					if(HttpContext.Current.Session["UserCheck1"] != null)
					{
						UserCheck1PH.Visible = true;
						UserCheck1Text.Text = HttpContext.Current.Session["UserCheck1"].ToString();
						rs = Db.recordSet("SELECT SponsorUserCheckID, Txt FROM SponsorUserCheck WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " AND UserCheckNr = 1");
						while(rs.Read())
						{
							UserCheck1.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
						}
						rs.Close();
					}
					if(HttpContext.Current.Session["UserCheck2"] != null)
					{
						UserCheck2PH.Visible = true;
						UserCheck2Text.Text = HttpContext.Current.Session["UserCheck1"].ToString();
						rs = Db.recordSet("SELECT SponsorUserCheckID, Txt FROM SponsorUserCheck WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " AND UserCheckNr = 2");
						while(rs.Read())
						{
							UserCheck2.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
						}
						rs.Close();
					}
					if(HttpContext.Current.Session["UserCheck3"] != null)
					{
						UserCheck3PH.Visible = true;
						UserCheck3Text.Text = HttpContext.Current.Session["UserCheck1"].ToString();
						rs = Db.recordSet("SELECT SponsorUserCheckID, Txt FROM SponsorUserCheck WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " AND UserCheckNr = 3");
						while(rs.Read())
						{
							UserCheck3.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
						}
						rs.Close();
					}

					if(HttpContext.Current.Request.QueryString["UID"] != null)
					{
						Cancel.Text = "Stäng";
						ProjectRoundUnitID.Items.Add(new ListItem("< välj >","0"));
						rs = Db.recordSet("SELECT pru.ProjectRoundUnitID, pr.Internal, pru.ProjectRoundID FROM ProjectRound pr INNER JOIN ProjectRoundUnit pru ON pr.ProjectRoundID = pru.ProjectRoundID WHERE pr.ProjectID = " + Convert.ToInt32(HttpContext.Current.Session["ProjectID"]));
						while(rs.Read())
						{
							ProjectRoundUnitID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString() + ":" + rs.GetInt32(2).ToString()));
						}
						rs.Close();

						VisitSponsorReminderID.Items.Add(new ListItem("< välj >","0"));
						rs = Db.recordSet("SELECT SponsorReminderID, Reminder FROM SponsorReminder WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
						while(rs.Read())
						{
							VisitSponsorReminderID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
						}
						rs.Close();

						VisitUPRUID.Items.Add(new ListItem("< välj >","0"));
						rs = Db.recordSet("SELECT " +
							"u.UserProjectRoundUserID, " +
							"pr.Internal, " +
							"u.Note " +
							"FROM UserProjectRoundUser u " +
							"INNER JOIN ProjectRoundUser pru ON u.ProjectRoundUserID = pru.ProjectRoundUserID " +
							"INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
							"WHERE u.UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]));
						while(rs.Read())
						{
							VisitUPRUID.Items.Add(new ListItem(rs.GetString(1) + (!rs.IsDBNull(2) && rs.GetString(2) != "" ? " / " + rs.GetString(2) : ""),rs.GetInt32(0).ToString()));
						}
						rs.Close();
					}
				}

				if(HttpContext.Current.Request.QueryString["UID"] != null)
				{
					SendVisitReminderButton.Click += new EventHandler(SendVisitReminderButton_Click);
					DeleteUser.Click += new EventHandler(DeleteUser_Click);
					if(!IsPostBack)
					{
						DeleteUser.Visible = !(HttpContext.Current.Session["Restricted"] != null && Convert.ToBoolean(HttpContext.Current.Session["Restricted"]));
						DeleteUser.ToolTip = "Ta bort";
						DeleteUser.Attributes["onclick"] += "return(confirm('Är du säker på att du vill ta bort denna person?'));";
						AddSurvey.Visible = true;
						AddNote.Visible = true;
						AddVisit.Visible = true;
						//VisitDT.Text = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") + " 09:00";

						if(HttpContext.Current.Request.QueryString["DNID"] != null)
						{
							Db.execute("UPDATE UserNote SET UserID = -" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]) + " WHERE UserNoteID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["DNID"]) + " AND UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]));
							HttpContext.Current.Response.Redirect("dashboard.aspx?UID=" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
						}

						if(HttpContext.Current.Request.QueryString["DUSID"] != null)
						{
							Db.execute("UPDATE UserSchedule SET UserID = -" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]) + " WHERE UserScheduleID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["DUSID"]) + " AND UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]));
							HttpContext.Current.Response.Redirect("dashboard.aspx?UID=" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
						}

						if(HttpContext.Current.Request.QueryString["DUPRUID"] != null)
						{
							if(HttpContext.Current.Request.QueryString["AID"] != null && HttpContext.Current.Request.QueryString["PRUID"] != null)
							{
								Db.execute("UPDATE Answer SET ProjectRoundUserID = -" + Convert.ToInt32(HttpContext.Current.Request.QueryString["PRUID"]) + " WHERE AnswerID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["AID"]) + " AND ProjectRoundUserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["PRUID"]));
							}
							else
							{
								Db.execute("UPDATE UserProjectRoundUser SET UserID = -" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]) + " WHERE UserProjectRoundUserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["DUPRUID"]) + " AND UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]));
							}
							HttpContext.Current.Response.Redirect("dashboard.aspx?UID=" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
						}

						if(HttpContext.Current.Request.QueryString["SUPRUID"] != null)
						{
							string UK = "";
							OdbcDataReader rs2 = Db.recordSet("SELECT u.ProjectRoundUserID, u.UserKey FROM UserProjectRoundUser up INNER JOIN [User] upu ON up.UserID = upu.UserID INNER JOIN ProjectRoundUser u ON up.ProjectRoundUserID = u.ProjectRoundUserID INNER JOIN SponsorAdmin sa ON upu.SponsorID = sa.SponsorID WHERE up.UserProjectRoundUserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SUPRUID"]) + " AND sa.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]));
							if(rs2.Read())
							{
								UK = appURL + "/submit.aspx?K=" + rs2.GetGuid(1).ToString().Substring(0,8) + rs2.GetInt32(0).ToString();
							}
							rs2.Close();

							HttpContext.Current.Session.Abandon();
							HttpContext.Current.Response.Redirect(UK,true);
						}
						if(HttpContext.Current.Request.QueryString["UUPRUID"] != null)
						{
							OdbcDataReader rs2 = Db.recordSet("SELECT u.ProjectRoundUserID FROM UserProjectRoundUser up INNER JOIN [User] upu ON up.UserID = upu.UserID INNER JOIN ProjectRoundUser u ON up.ProjectRoundUserID = u.ProjectRoundUserID INNER JOIN SponsorAdmin sa ON upu.SponsorID = sa.SponsorID WHERE up.UserProjectRoundUserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UUPRUID"]) + " AND sa.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]));
							if(rs2.Read())
							{
								Db.execute("UPDATE Answer SET EndDT = NULL WHERE ProjectRoundUserID = " + rs2.GetInt32(0));
							}
							rs2.Close();

							HttpContext.Current.Response.Redirect("dashboard.aspx?UID=" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
						}

						EditUser.Visible = true;
						Edit.Text = "Visa/ändra person";
						AddUser.Visible = false;
						FirstLastName.Text = "";

						string ident = "";

						int uid = 0;
						OdbcDataReader rs = Db.recordSet("SELECT " +
							"u.UserID " +
							",UserIdent1 " +
							",UserIdent2 " +
							",UserIdent3 " +
							",UserIdent4 " +
							",UserIdent5 " +
							",UserIdent6 " +
							",UserIdent7 " +
							",UserIdent8 " +
							",UserIdent9 " +
							",UserIdent10 " +
							",UserNr " +
							",UserCheck1 " +
							",UserCheck2 " +
							",UserCheck3 " +
							"FROM [User] u WHERE u.UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]) + " AND u.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
						if(rs.Read())
						{
							if(HttpContext.Current.Session["UserNr"] != null && (bool)HttpContext.Current.Session["ShowUserNr"])
							{
								ident += (ident != "" ? "$" : "") + UserNrText.Text + "_" + (!rs.IsDBNull(11) ? rs.GetInt32(11) : 0).ToString();
							}
							if(HttpContext.Current.Session["UserIdent1"] != null)
							{
								if((bool)HttpContext.Current.Session["ShowUserIdent1"])
								{
									ident += (ident != "" ? "$" : "") + UserIdent1Text.Text + "_" + (!rs.IsDBNull(1) ? rs.GetString(1) : "");
								}

								UserIdent1.Text = (!rs.IsDBNull(1) ? rs.GetString(1) : "");
								if(Convert.ToInt32(HttpContext.Current.Session["EmailIdent"]) == 1)
								{
									Email.Text = (!rs.IsDBNull(1) ? rs.GetString(1) : "");
									VisitEmail.Text = (!rs.IsDBNull(1) ? rs.GetString(1) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["FirstnameIdent"]) == 1)
								{
									FirstLastName.Text = (!rs.IsDBNull(1) ? rs.GetString(1) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["LastnameIdent"]) == 1)
								{
									FirstLastName.Text += " " + (!rs.IsDBNull(1) ? rs.GetString(1) : "");
								}
							}
							if(HttpContext.Current.Session["UserIdent2"] != null)
							{
								if((bool)HttpContext.Current.Session["ShowUserIdent2"])
								{
									ident += (ident != "" ? "$" : "") + UserIdent2Text.Text + "_" + (!rs.IsDBNull(2) ? rs.GetString(2) : "");
								}

								UserIdent2.Text = (!rs.IsDBNull(2) ? rs.GetString(2) : "");
								if(Convert.ToInt32(HttpContext.Current.Session["EmailIdent"]) == 2)
								{
									Email.Text = (!rs.IsDBNull(2) ? rs.GetString(2) : "");
									VisitEmail.Text = (!rs.IsDBNull(2) ? rs.GetString(2) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["FirstnameIdent"]) == 2)
								{
									FirstLastName.Text = (!rs.IsDBNull(2) ? rs.GetString(2) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["LastnameIdent"]) == 2)
								{
									FirstLastName.Text += " " + (!rs.IsDBNull(2) ? rs.GetString(2) : "");
								}
							}
							if(HttpContext.Current.Session["UserIdent3"] != null)
							{
								if((bool)HttpContext.Current.Session["ShowUserIdent3"])
								{
									ident += (ident != "" ? "$" : "") + UserIdent3Text.Text + "_" + (!rs.IsDBNull(3) ? rs.GetString(3) : "");
								}

								UserIdent3.Text = (!rs.IsDBNull(3) ? rs.GetString(3) : "");
								if(Convert.ToInt32(HttpContext.Current.Session["EmailIdent"]) == 3)
								{
									Email.Text = (!rs.IsDBNull(3) ? rs.GetString(3) : "");
									VisitEmail.Text = (!rs.IsDBNull(3) ? rs.GetString(3) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["FirstnameIdent"]) == 3)
								{
									FirstLastName.Text = (!rs.IsDBNull(3) ? rs.GetString(3) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["LastnameIdent"]) == 3)
								{
									FirstLastName.Text += " " + (!rs.IsDBNull(3) ? rs.GetString(3) : "");
								}
							}
							if(HttpContext.Current.Session["UserIdent4"] != null)
							{
								if((bool)HttpContext.Current.Session["ShowUserIdent4"])
								{
									ident += (ident != "" ? "$" : "") + UserIdent4Text.Text + "_" + (!rs.IsDBNull(4) ? rs.GetString(4) : "");
								}

								UserIdent4.Text = (!rs.IsDBNull(4) ? rs.GetString(4) : "");
								if(Convert.ToInt32(HttpContext.Current.Session["EmailIdent"]) == 4)
								{
									Email.Text = (!rs.IsDBNull(4) ? rs.GetString(4) : "");
									VisitEmail.Text = (!rs.IsDBNull(4) ? rs.GetString(4) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["FirstnameIdent"]) == 4)
								{
									FirstLastName.Text = (!rs.IsDBNull(4) ? rs.GetString(4) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["LastnameIdent"]) == 4)
								{
									FirstLastName.Text += " " + (!rs.IsDBNull(4) ? rs.GetString(4) : "");
								}
							}
							if(HttpContext.Current.Session["UserIdent5"] != null)
							{
								if((bool)HttpContext.Current.Session["ShowUserIdent5"])
								{
									ident += (ident != "" ? "$" : "") + UserIdent5Text.Text + "_" + (!rs.IsDBNull(5) ? rs.GetString(5) : "");
								}

								UserIdent5.Text = (!rs.IsDBNull(5) ? rs.GetString(5) : "");
								if(Convert.ToInt32(HttpContext.Current.Session["EmailIdent"]) == 5)
								{
									Email.Text = (!rs.IsDBNull(5) ? rs.GetString(5) : "");
									VisitEmail.Text = (!rs.IsDBNull(5) ? rs.GetString(5) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["FirstnameIdent"]) == 5)
								{
									FirstLastName.Text = (!rs.IsDBNull(5) ? rs.GetString(5) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["LastnameIdent"]) == 5)
								{
									FirstLastName.Text += " " + (!rs.IsDBNull(5) ? rs.GetString(5) : "");
								}
							}
							if(HttpContext.Current.Session["UserIdent6"] != null)
							{
								UserIdent6.Text = (!rs.IsDBNull(6) ? rs.GetString(6) : "");
								if(Convert.ToInt32(HttpContext.Current.Session["EmailIdent"]) == 6)
								{
									Email.Text = (!rs.IsDBNull(6) ? rs.GetString(6) : "");
									VisitEmail.Text = (!rs.IsDBNull(6) ? rs.GetString(6) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["FirstnameIdent"]) == 6)
								{
									FirstLastName.Text = (!rs.IsDBNull(6) ? rs.GetString(6) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["LastnameIdent"]) == 6)
								{
									FirstLastName.Text += " " + (!rs.IsDBNull(6) ? rs.GetString(6) : "");
								}
							}
							if(HttpContext.Current.Session["UserIdent7"] != null)
							{
								UserIdent7.Text = (!rs.IsDBNull(7) ? rs.GetString(7) : "");
								if(Convert.ToInt32(HttpContext.Current.Session["EmailIdent"]) == 7)
								{
									Email.Text = (!rs.IsDBNull(7) ? rs.GetString(7) : "");
									VisitEmail.Text = (!rs.IsDBNull(7) ? rs.GetString(7) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["FirstnameIdent"]) == 7)
								{
									FirstLastName.Text = (!rs.IsDBNull(7) ? rs.GetString(7) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["LastnameIdent"]) == 7)
								{
									FirstLastName.Text += " " + (!rs.IsDBNull(7) ? rs.GetString(7) : "");
								}
							}
							if(HttpContext.Current.Session["UserIdent8"] != null)
							{
								UserIdent8.Text = (!rs.IsDBNull(8) ? rs.GetString(8) : "");
								if(Convert.ToInt32(HttpContext.Current.Session["EmailIdent"]) == 8)
								{
									Email.Text = (!rs.IsDBNull(8) ? rs.GetString(8) : "");
									VisitEmail.Text = (!rs.IsDBNull(8) ? rs.GetString(8) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["FirstnameIdent"]) == 8)
								{
									FirstLastName.Text = (!rs.IsDBNull(8) ? rs.GetString(8) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["LastnameIdent"]) == 8)
								{
									FirstLastName.Text += " " + (!rs.IsDBNull(8) ? rs.GetString(8) : "");
								}
							}
							if(HttpContext.Current.Session["UserIdent9"] != null)
							{
								UserIdent9.Text = (!rs.IsDBNull(9) ? rs.GetString(9) : "");
								if(Convert.ToInt32(HttpContext.Current.Session["EmailIdent"]) == 9)
								{
									Email.Text = (!rs.IsDBNull(9) ? rs.GetString(9) : "");
									VisitEmail.Text = (!rs.IsDBNull(9) ? rs.GetString(9) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["FirstnameIdent"]) == 9)
								{
									FirstLastName.Text = (!rs.IsDBNull(9) ? rs.GetString(9) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["LastnameIdent"]) == 9)
								{
									FirstLastName.Text += " " + (!rs.IsDBNull(9) ? rs.GetString(9) : "");
								}
							}
							if(HttpContext.Current.Session["UserIdent10"] != null)
							{
								UserIdent10.Text = (!rs.IsDBNull(10) ? rs.GetString(10) : "");
								if(Convert.ToInt32(HttpContext.Current.Session["EmailIdent"]) == 10)
								{
									Email.Text = (!rs.IsDBNull(10) ? rs.GetString(10) : "");
									VisitEmail.Text = (!rs.IsDBNull(10) ? rs.GetString(10) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["FirstnameIdent"]) == 10)
								{
									FirstLastName.Text = (!rs.IsDBNull(10) ? rs.GetString(10) : "");
								}
								if(Convert.ToInt32(HttpContext.Current.Session["LastnameIdent"]) == 10)
								{
									FirstLastName.Text += " " + (!rs.IsDBNull(10) ? rs.GetString(10) : "");
								}
							}
							if(HttpContext.Current.Session["UserNr"] != null)
							{
								UserNr.Text = (!rs.IsDBNull(11) ? rs.GetInt32(11) : 0).ToString();
							}
							if(HttpContext.Current.Session["UserCheck1"] != null && !rs.IsDBNull(12))
							{
								UserCheck1.SelectedValue = rs.GetInt32(12).ToString();
							}
							if(HttpContext.Current.Session["UserCheck2"] != null && !rs.IsDBNull(13))
							{
								UserCheck2.SelectedValue = rs.GetInt32(13).ToString();
							}
							if(HttpContext.Current.Session["UserCheck3"] != null && !rs.IsDBNull(14))
							{
								UserCheck3.SelectedValue = rs.GetInt32(14).ToString();
							}
							uid = rs.GetInt32(0);
						}
						rs.Close();
						if(uid == 0)
						{
							HttpContext.Current.Response.Redirect("dashboard.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
						}
						else
						{
							rs = Db.recordSet("SELECT un.UserNoteID, un.DT, ISNULL(sa.Name,sa.Username), ISNULL(sae.Name,sae.Username), un.EditDT, un.Note FROM UserNote un INNER JOIN [User] u ON un.UserID = u.UserID INNER JOIN SponsorAdmin sa ON un.SponsorAdminID = sa.SponsorAdminID LEFT OUTER JOIN SponsorAdmin sae ON sae.SponsorAdminID = un.EditSponsorAdminID WHERE u.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " AND un.UserID = " + uid + " ORDER BY un.DT DESC");
							if(rs.Read())
							{
								Notes.Text = "<hr/><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><TR><TD><B>Notering/Datum</B>&nbsp;&nbsp;</TD><TD></TD><TD><B>Skapad av</B>&nbsp;&nbsp;</TD><TD><B>Senast ändrad</B>&nbsp;&nbsp;</TD></TR>";

								do
								{
									if(HttpContext.Current.Request.QueryString["NID"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["NID"]) == rs.GetInt32(0))
									{
										NotePH.Visible = true;
										AddNote.Visible = false;
										Note.Text = rs.GetString(5);
										NoteDetails.Text = "Skapad " + rs.GetDateTime(1).ToString("yyyy-MM-dd HH:mm") + " " + rs.GetString(2);
										if(!rs.IsDBNull(4))
										{
											NoteDetails.Text += ", Senast ändrad " + rs.GetDateTime(4).ToString("yyyy-MM-dd HH:mm") + " " + rs.GetString(3);
										}
									}
									Notes.Text += "<TR><TD><A HREF=\"dashboard.aspx?UID=" + uid + "&NID=" + rs.GetInt32(0) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">" + rs.GetDateTime(1).ToString("yyyy-MM-dd HH:mm") + "</A>&nbsp;&nbsp;</TD><TD>[<A HREF=\"javascript:if(confirm('Är du säker?')){location.href='dashboard.aspx?DNID=" + rs.GetInt32(0) + "&UID=" + uid + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "';}\">ta bort</A>]&nbsp;&nbsp;</TD><TD>" + rs.GetString(2) + "&nbsp;&nbsp;</TD><TD>" + (!rs.IsDBNull(4) ? rs.GetDateTime(4).ToString("yyyy-MM-dd HH:mm") + " " + rs.GetString(3) : "") + "&nbsp;&nbsp;</TD></TR>";
								}
								while(rs.Read());

								Notes.Text += "</table>";
							}
							rs.Close();

							rs = Db.recordSet("SELECT " +
								"a.UserScheduleID, " +
								"a.UserProjectRoundUserID, " +
								"a.DT, " +
								"a.SponsorReminderID, " +
								"a.Reminder, " +
								"a.Note, " +
								"a.Email " +
								"FROM UserSchedule a " +
								"LEFT OUTER JOIN SponsorReminder s ON a.SponsorReminderID = s.SponsorReminderID " +
								"WHERE a.UserID = " + uid);
							if(rs.Read())
							{
								Visits.Text = "<hr/><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><TR><TD><B>Besöksdatum</B>&nbsp;&nbsp;</TD><TD></TD><TD></TD><TD><B>Kommentar</B>&nbsp;&nbsp;</TD></TR>";
								do
								{
									if(HttpContext.Current.Request.QueryString["SUSID"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["SUSID"]) == rs.GetInt32(0))
									{
										SendVisitReminder.Visible = true;
										SendVisitReminderDT.Text = rs.GetDateTime(2).ToString("yyyy-MM-dd HH:mm");
										if(!rs.IsDBNull(3))
										{
											string UK = "";
											OdbcDataReader rs2 = Db.recordSet("SELECT " +
												"pru.ProjectRoundUserID, " +
												"pru.UserKey " +
												"FROM UserProjectRoundUser upru " +
												"INNER JOIN ProjectRoundUser pru ON upru.ProjectRoundUserID = pru.ProjectRoundUserID " +
												"WHERE upru.UserProjectRoundUserID = " + rs.GetInt32(1));
											if(rs2.Read())
											{
												UK = appURL + "/submit.aspx?K=" + rs2.GetGuid(1).ToString().Substring(0,8) + rs2.GetInt32(0).ToString();
											}
											rs2.Close();

											rs2 = Db.recordSet("SELECT Subject, Body FROM SponsorReminder WHERE SponsorReminderID = " + rs.GetInt32(3));
											if(rs2.Read())
											{
												SendVisitReminderSubject.Text = rs2.GetString(0);
												SendVisitReminderBody.Text = rs2.GetString(1).Replace("<LINK/>",UK).Replace("<NAME/>",FirstLastName.Text).Replace("<DATE/>",rs.GetDateTime(2).ToString("dd/MM")).Replace("<TIME/>",rs.GetDateTime(2).ToString("HH:mm"));
											}
											rs2.Close();

											SendVisitReminderTo.Text = (!rs.IsDBNull(6) && rs.GetString(6) != "" ? rs.GetString(6) : Email.Text);

											rs2 = Db.recordSet("SELECT Email FROM SponsorAdmin WHERE SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]));
											if(rs2.Read())
											{
												SendVisitReminderFrom.Text = rs2.GetString(0);
											}
											rs2.Close();
										}
									}
									if(HttpContext.Current.Request.QueryString["USID"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["USID"]) == rs.GetInt32(0))
									{
										VisitPH.Visible = true;
										AddVisit.Visible = false;

										if(!rs.IsDBNull(1) && rs.GetInt32(1) != 0)
										{
											VisitUPRUID.SelectedValue = rs.GetInt32(1).ToString();
										}
										VisitDT.Text = rs.GetDateTime(2).ToString("yyyy-MM-dd HH:mm");
										if(!rs.IsDBNull(3) && rs.GetInt32(3) != 0)
										{
											VisitSponsorReminderID.SelectedValue = rs.GetInt32(3).ToString();
										}
										if(!rs.IsDBNull(4) && rs.GetInt32(4) != 0)
										{
											VisitReminder.Text = rs.GetInt32(4).ToString();
										}
										if(!rs.IsDBNull(5))
										{
											VisitNote.Text = rs.GetString(5);
										}
										if(!rs.IsDBNull(6))
										{
											VisitEmail.Text = rs.GetString(6);
										}
									}
									Visits.Text += "<TR><TD><A HREF=\"dashboard.aspx?UID=" + uid + "&USID=" + rs.GetInt32(0) + "\">" + rs.GetDateTime(2).ToString("yyyy-MM-dd HH:mm") + "</A>&nbsp;&nbsp;</TD><TD>[<A HREF=\"javascript:if(confirm('Är du säker?')){location.href='dashboard.aspx?UID=" + uid + "&DUSID=" + rs.GetInt32(0) + "';}\">ta bort</A>]&nbsp;&nbsp;</TD><TD>[<A HREF=\"dashboard.aspx?UID=" + uid + "&SUSID=" + rs.GetInt32(0) + "\">skicka påminnelse</A>]&nbsp;&nbsp;</TD><TD>" + (rs.IsDBNull(5) ? "" : rs.GetString(5)) + "&nbsp;&nbsp;</TD></TR>";
								}
								while(rs.Read());
								Visits.Text += "</table>";
							}
							rs.Close();
							string sqlQuery = "SELECT " +
								"upru.UserProjectRoundUserID, " +						// 0
								"upru.Note, " +
								"pru.ProjectRoundID, " +
								"pru.ProjectRoundUnitID, " +
								"pru.Email, " +
								"pr.Internal, " +										// 5
								"a.StartDT, " +
								"a.EndDT, " +
								"a.CurrentPage, " +
								"REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-',''), " +
								"pru.UserKey, " +										// 10
								"a.AnswerID, " +
								"pru.ProjectRoundUserID, " +
								"prl.InvitationSubject, " +
								"prl.InvitationBody, " +
								"sa.Email, " +											// 15
								"REPLACE(CONVERT(VARCHAR(255),r.ReportKey),'-',''), " +
								"u.UserID, " +
								"s.NoEmail, " +
								"s.NoLogout, " +
								"spruid.NoLogout, " +									// 20
								"spruid.NoSend " +
								"FROM UserProjectRoundUser upru " +
								"INNER JOIN [User] u ON upru.UserID = u.UserID " +
								"INNER JOIN ProjectRoundUser pru ON upru.ProjectRoundUserID = pru.ProjectRoundUserID " +
								"INNER JOIN ProjectRoundUnit p ON pru.ProjectRoundUnitID = p.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound pr ON p.ProjectRoundID = pr.ProjectRoundID " +
								"INNER JOIN SponsorAdmin sa ON u.SponsorID = sa.SponsorID AND sa.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " " +
								"INNER JOIN Sponsor s ON sa.SponsorID = s.SponsorID " +
								"LEFT OUTER JOIN SponsorPRU spruid ON p.ProjectRoundUnitID = spruid.PRUID AND spruid.SponsorID = sa.SponsorID " +
								"LEFT OUTER JOIN Report r ON pr.ConfidentialIndividualReportID = r.ReportID " +
								"LEFT OUTER JOIN Answer a ON pru.ProjectRoundUserID = a.ProjectRoundUserID " +
								"LEFT OUTER JOIN ProjectRoundLang prl ON pr.ProjectRoundID = prl.ProjectRoundID AND prl.LangID = 1 " +
								"WHERE upru.UserID = " + uid + " ORDER BY pr.ProjectRoundID, a.AnswerID";
							//HttpContext.Current.Response.Write(sqlQuery);
							//HttpContext.Current.Response.End();
							rs = Db.recordSet(sqlQuery);
							if(rs.Read())
							{
								Surveys.Text = "<hr/><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
									"<TR>" +
									"<TD><B>Enkät</B>&nbsp;&nbsp;</TD>" +
									"<TD><B></B>&nbsp;&nbsp;</TD>" +
									"</TR>";

								sendlinkfrom.Text = rs.GetString(15);

								int PRID = 0, UPRUID = 0;

								do
								{
									if(HttpContext.Current.Request.QueryString["UPRUID"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["UPRUID"]) == rs.GetInt32(0))
									{
										SurveyPH.Visible = true;
										AddSurvey.Visible = false;
										SurveyNote.Text = rs.GetString(1);
										Email.Text = rs.GetString(4);
										ProjectRoundUnitID.SelectedValue = rs.GetInt32(3).ToString() + ":" + rs.GetInt32(2).ToString();
									}
									string status = "Ej påbörjad";
									if(!rs.IsDBNull(6))
									{
										status = (HttpContext.Current.Session["Restricted"] != null && Convert.ToBoolean(HttpContext.Current.Session["Restricted"]) ? "" : "<A TITLE=\"Ta bort svar\" HREF=\"javascript:if(confirm('Är du säker på att du vill ta bort enkätsvaret?')){location.href='dashboard.aspx?DUPRUID=" + rs.GetInt32(0) + "&PRUID=" + rs.GetInt32(12) + "&AID=" + rs.GetInt32(11) + "&UID=" + uid + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "';}\"><img src=\"img/extraYellow.gif\" border=\"0\"/></A>") +
											"&nbsp;&nbsp;<span title=\"Startad " + rs.GetDateTime(6).ToString("yyMMdd HH:mm") + "\">";
										if(!rs.IsDBNull(7))
										{
											if(rs.IsDBNull(21))
											{
												status += "Inskickad " + rs.GetDateTime(7).ToString("yyMMdd HH:mm");
											}
											else
											{
												status += "Sparad " + rs.GetDateTime(7).ToString("yyMMdd HH:mm");
											}
										}
										else
										{
											status += "Sidan " + (rs.IsDBNull(8) ? 1 : rs.GetInt32(8));
										}
										status += "</span>";
									}
									string sendlink = "";
									if(rs.IsDBNull(7) && rs.IsDBNull(18) && rs.IsDBNull(21))
									{
										sendlink = "[<A HREF=\"JavaScript:void(document.forms[0].sendlinkUPRUID.value=" + rs.GetInt32(0) + ");void(document.getElementById('sendlink').style.display='');void(document.getElementById('sendlinksurvey').innerHTML='" + (rs.IsDBNull(5) ? "" : rs.GetString(5)) + "');void(document.getElementById('sendlinkto').innerHTML='" + (rs.IsDBNull(4) ? "" : rs.GetString(4)) + "');void(document.forms[0].sendlinksubject.value='" + (rs.IsDBNull(13) ? "" : rs.GetString(13)) + "');void(document.forms[0].sendlinkbody.value='" + (rs.IsDBNull(14) ? "" : rs.GetString(14).Replace("\r\n","\\n")) + "');\">skicka e-post</A>]";
									}

									string AK = (!rs.IsDBNull(9) ? rs.GetString(9) : "");
									string UK = appURL + "/submit.aspx?K=" + rs.GetGuid(10).ToString().Substring(0,8) + rs.GetInt32(12).ToString();

									string start = "", show = "";
									if(rs.IsDBNull(7))
									{
										if(rs.IsDBNull(19) && rs.IsDBNull(20))
										{
											start = "[<A HREF=\"dashboard.aspx?UID=" + uid + "&SUPRUID=" + rs.GetInt32(0) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">logga ut &amp; öppna</A>]";
										}
										else
										{
											start = "[<A HREF=\"" + appURL + "/submit.aspx?K=" + rs.GetGuid(10).ToString().Substring(0,8) + rs.GetInt32(12).ToString() + "\" target=\"_blank\">öppna</A>]";
										}
									}
									else
									{
										if(rs.IsDBNull(21))
										{
											start = "[<A HREF=\"dashboard.aspx?UID=" + uid + "&UUPRUID=" + rs.GetInt32(0) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">ångra&nbsp;inskickning</A>]";
										}
										else
										{
											start = "[<A HREF=\"" + appURL + "/submit.aspx?Edit=1&K=" + rs.GetGuid(10).ToString().Substring(0,8) + rs.GetInt32(12).ToString() + "\" target=\"_blank\">redigera</A>]";
										}
										show += "<A HREF=\"JavaScript:void(window.open('" + UK + "&AIOP=1&SM=" + AK.Substring(0,8) + "','',''));\"><img src=\"img/view.gif\" title=\"visa/skriv ut\" border=\"0\"/></A>";
										show += "&nbsp;<A HREF=\"" + UK + "&AIOP=1&SM=" + AK.Substring(0,8) + "&MakePDF=" + ident.Split('$')[0].Split('_')[1] + "&Ident=" + ident + "$Inskickad_" + rs.GetDateTime(7).ToString("yyyy-MM-dd") + "\"><img src=\"img/pdfSmall.gif\" border=\"0\"/></A>";
										if(!rs.IsDBNull(16))
										{
											show += "&nbsp;<A HREF=\"javascript:void(window.open('report.aspx?RK=" + rs.GetString(16) + "&UID=" + uid + "','','width=1000,scrollbars=1'));\"><img src=\"img/unit_on.gif\" border=\"0\"/></A>";
											show += "&nbsp;<A HREF=\"report.aspx?RK=" + rs.GetString(16) + "&UID=" + uid + "&MakePDF=" + ident.Split('$')[0].Split('_')[1] + "&Ident=" + ident + "$Genererad_" + DateTime.Now.ToString("yyyy-MM-dd") + "\"><img src=\"img/pdfSmall.gif\" border=\"0\"/></A>";
										}									
									}

									Surveys.Text += "<TR>" +
										"<TD>" + (UPRUID != rs.GetInt32(0) ? "<A TITLE=\"" + rs.GetString(1) + "\" HREF=\"dashboard.aspx?UID=" + uid + "&UPRUID=" + rs.GetInt32(0) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">" + rs.GetString(5) + "</A>" : "") + "&nbsp;&nbsp;</TD>" +
										"<TD>" + (UPRUID != rs.GetInt32(0) ? (HttpContext.Current.Session["Restricted"] != null && Convert.ToBoolean(HttpContext.Current.Session["Restricted"]) ? "" : "<A TITLE=\"Ta bort enkät\" HREF=\"javascript:if(confirm('Är du säker på att du vill ta bort enkäten?')){location.href='dashboard.aspx?DUPRUID=" + rs.GetInt32(0) + "&UID=" + uid + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "';}\"><img src=\"img/extraRed.gif\" border=\"0\"/></A>") : "") + "</TD>" +
										"<TD>&nbsp;&nbsp;" + status + "&nbsp;&nbsp;</TD>" +
										"<TD>" + sendlink + show + "&nbsp;&nbsp;</TD>" +
										"<TD>" + start + "&nbsp;&nbsp;</TD>" +
										"</TR>";

									PRID = rs.GetInt32(2);
									UPRUID = rs.GetInt32(0);
								}
								while(rs.Read());

								Surveys.Text += "</table>";
							}
							rs.Close();
						}
					}

					AddNote.Click += new EventHandler(AddNote_Click);
					AddSurvey.Click += new EventHandler(AddSurvey_Click);
					AddVisit.Click += new EventHandler(AddVisit_Click);
					sendlinkbutton.Click += new EventHandler(sendlinkbutton_Click);
				}

				AddUser.Click += new EventHandler(AddUser_Click);
				Save.Click += new EventHandler(Save_Click);
				Search.Click += new EventHandler(Search_Click);
				ExportButton.Click += new EventHandler(ExportButton_Click);
				if(!IsPostBack && HttpContext.Current.Request.QueryString["Search"] != null)
				{
					search();
				}
			}
			Logout.Click += new EventHandler(Logout_Click);
			Cancel.Click += new EventHandler(Cancel_Click);

			#region Create admin
			if(HttpContext.Current.Request.QueryString["UN"] != null && HttpContext.Current.Request.QueryString["PW"] != null && HttpContext.Current.Request.QueryString["SID"] != null)
			{
				Db.execute("INSERT INTO SponsorAdmin (Username,Password,SponsorID) VALUES ('" + HttpContext.Current.Request.QueryString["UN"].Replace("'","") + "','" + Db.HexHashMD5(HttpContext.Current.Request.QueryString["PW"]) + "'," + Convert.ToInt32(HttpContext.Current.Request.QueryString["SID"]) + ")");
			}
			#endregion
		}

		private void Login_Click(object sender, EventArgs e)
		{
			OdbcDataReader rs = Db.recordSet("SELECT " +
				"s.SponsorID, " +			// 0
				"s.ProjectID, " +
				"u.SponsorAdminID, " +
				"s.UserIdent1, " +
				"s.UserIdent2, " +
				"s.UserIdent3, " +			// 5
				"s.UserIdent4, " +
				"s.UserIdent5, " +
				"s.UserIdent6, " +
				"s.UserIdent7, " +
				"s.UserIdent8, " +			// 10
				"s.UserIdent9, " +
				"s.UserIdent10, " +
				"s.ShowUserIdent1, " +
				"s.ShowUserIdent2, " +
				"s.ShowUserIdent3, " +		// 15
				"s.ShowUserIdent4, " +
				"s.ShowUserIdent5, " +
				"s.ShowUserIdent6, " +
				"s.ShowUserIdent7, " +
				"s.ShowUserIdent8, " +		// 20
				"s.ShowUserIdent9, " +
				"s.ShowUserIdent10, " +
				"s.Sponsor, " +
				"s.UserNr, " +
				"s.ShowUserNr, " +			// 25
				"s.EmailIdent, " +
				"s.FirstnameIdent, " +
				"s.LastnameIdent, " +
				"s.UserCheck1, " +
				"s.UserCheck2, " +			// 30
				"s.UserCheck3, " +
				"s.ShowUserCheck1, " +
				"s.ShowUserCheck2, " +
				"s.ShowUserCheck3, " +
				"u.Restricted, " +			// 35
				"s.CustomReportTemplateID, " +
				"s.FeedbackEmailFrom, " +
				"u.Email " +
				"FROM SponsorAdmin u " +
				"INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
				"WHERE u.Username = '" + Username.Text.Trim().Replace("'","") + "' AND u.Password = '" + Db.HexHashMD5(Password.Text) + "'");
			if(rs.Read())
			{
				// SponsorID of user logged in
				HttpContext.Current.Session["SponsorID"] = rs.GetInt32(0);
				// ProjectID of user logged in
				HttpContext.Current.Session["ProjectID"] = rs.GetInt32(1);
				// SponsorAdminID of user logged in
				HttpContext.Current.Session["SponsorAdminID"] = rs.GetInt32(2);
				if(!rs.IsDBNull(3))
				{
					HttpContext.Current.Session["UserIdent1"] = rs.GetString(3);
					HttpContext.Current.Session["ShowUserIdent1"] = !rs.IsDBNull(13);
				}
				if(!rs.IsDBNull(4))
				{
					HttpContext.Current.Session["UserIdent2"] = rs.GetString(4);
					HttpContext.Current.Session["ShowUserIdent2"] = !rs.IsDBNull(14);
				}
				if(!rs.IsDBNull(5))
				{
					HttpContext.Current.Session["UserIdent3"] = rs.GetString(5);
					HttpContext.Current.Session["ShowUserIdent3"] = !rs.IsDBNull(15);
				}
				if(!rs.IsDBNull(6))
				{
					HttpContext.Current.Session["UserIdent4"] = rs.GetString(6);
					HttpContext.Current.Session["ShowUserIdent4"] = !rs.IsDBNull(16);
				}
				if(!rs.IsDBNull(7))
				{
					HttpContext.Current.Session["UserIdent5"] = rs.GetString(7);
					HttpContext.Current.Session["ShowUserIdent5"] = !rs.IsDBNull(17);
				}
				if(!rs.IsDBNull(8))
				{
					HttpContext.Current.Session["UserIdent6"] = rs.GetString(8);
					HttpContext.Current.Session["ShowUserIdent6"] = !rs.IsDBNull(18);
				}
				if(!rs.IsDBNull(9))
				{
					HttpContext.Current.Session["UserIdent7"] = rs.GetString(9);
					HttpContext.Current.Session["ShowUserIdent7"] = !rs.IsDBNull(19);
				}
				if(!rs.IsDBNull(10))
				{
					HttpContext.Current.Session["UserIdent8"] = rs.GetString(10);
					HttpContext.Current.Session["ShowUserIdent8"] = !rs.IsDBNull(20);
				}
				if(!rs.IsDBNull(11))
				{
					HttpContext.Current.Session["UserIdent9"] = rs.GetString(11);
					HttpContext.Current.Session["ShowUserIdent9"] = !rs.IsDBNull(21);
				}
				if(!rs.IsDBNull(12))
				{
					HttpContext.Current.Session["UserIdent10"] = rs.GetString(12);
					HttpContext.Current.Session["ShowUserIdent10"] = !rs.IsDBNull(22);
				}
				if(!rs.IsDBNull(23))
				{
					HttpContext.Current.Session["Sponsor"] = rs.GetString(23);
				}

				if(!rs.IsDBNull(24))
				{
					HttpContext.Current.Session["UserNr"] = rs.GetString(24);
					HttpContext.Current.Session["ShowUserNr"] = !rs.IsDBNull(25);
				}
				HttpContext.Current.Session["EmailIdent"] = (rs.IsDBNull(26) ? 0 : rs.GetInt32(26));
				HttpContext.Current.Session["FirstnameIdent"] = (rs.IsDBNull(27) ? 0 : rs.GetInt32(27));
				HttpContext.Current.Session["LastnameIdent"] = (rs.IsDBNull(28) ? 0 : rs.GetInt32(28));
				if(!rs.IsDBNull(29))
				{
					HttpContext.Current.Session["UserCheck1"] = rs.GetString(29);
					HttpContext.Current.Session["ShowUserCheck1"] = !rs.IsDBNull(32);
				}
				if(!rs.IsDBNull(30))
				{
					HttpContext.Current.Session["UserCheck2"] = rs.GetString(30);
					HttpContext.Current.Session["ShowUserCheck2"] = !rs.IsDBNull(33);
				}
				if(!rs.IsDBNull(31))
				{
					HttpContext.Current.Session["UserCheck3"] = rs.GetString(31);
					HttpContext.Current.Session["ShowUserCheck3"] = !rs.IsDBNull(34);
				}
				if(!rs.IsDBNull(35))
				{
					HttpContext.Current.Session["Restricted"] = true;
				}
				if(!rs.IsDBNull(36))
				{
					HttpContext.Current.Session["CustomReportTemplateID"] = 1;
				}
				if(!rs.IsDBNull(37))
				{
					HttpContext.Current.Session["FeedbackEmail"] = 1;
				}
				// Email of user logged in
				HttpContext.Current.Session["SponsorAdminEmail"] = (rs.IsDBNull(38) ? "" : rs.GetString(38));
			}
			rs.Close();
			HttpContext.Current.Response.Redirect("dashboard.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
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

		private void Logout_Click(object sender, EventArgs e)
		{
			HttpContext.Current.Session.Abandon();
			HttpContext.Current.Response.Redirect("dashboard.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
		}

		private void AddUser_Click(object sender, EventArgs e)
		{
			EditUser.Visible = true;
			Edit.Text = "Lägg till person";
			AddUser.Visible = false;

			if(HttpContext.Current.Session["UserNr"] != null)
			{
				UserNr.Text = "";
			}
			if(HttpContext.Current.Session["UserIdent1"] != null)
			{
				UserIdent1.Text = "";
			}
			if(HttpContext.Current.Session["UserIdent2"] != null)
			{
				UserIdent2.Text = "";
			}
			if(HttpContext.Current.Session["UserIdent3"] != null)
			{
				UserIdent3.Text = "";
			}
			if(HttpContext.Current.Session["UserIdent4"] != null)
			{
				UserIdent4.Text = "";
			}
			if(HttpContext.Current.Session["UserIdent5"] != null)
			{
				UserIdent5.Text = "";
			}
			if(HttpContext.Current.Session["UserIdent6"] != null)
			{
				UserIdent6.Text = "";
			}
			if(HttpContext.Current.Session["UserIdent7"] != null)
			{
				UserIdent7.Text = "";
			}
			if(HttpContext.Current.Session["UserIdent8"] != null)
			{
				UserIdent8.Text = "";
			}
			if(HttpContext.Current.Session["UserIdent9"] != null)
			{
				UserIdent9.Text = "";
			}
			if(HttpContext.Current.Session["UserIdent10"] != null)
			{
				UserIdent10.Text = "";
			}
			if(HttpContext.Current.Session["UserCheck1"] != null)
			{
				UserCheck1.SelectedIndex = -1;
			}
			if(HttpContext.Current.Session["UserCheck2"] != null)
			{
				UserCheck2.SelectedIndex = -1;
			}
			if(HttpContext.Current.Session["UserCheck3"] != null)
			{
				UserCheck3.SelectedIndex = -1;
			}
		}

		private void Cancel_Click(object sender, EventArgs e)
		{
			HttpContext.Current.Response.Redirect("dashboard.aspx",true);
		}

		private void Save_Click(object sender, EventArgs e)
		{
			int UID = 0;
			if(HttpContext.Current.Request.QueryString["UID"] != null)
			{
				UID = Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]);
				Db.execute("UPDATE [User] SET " +
					(HttpContext.Current.Session["UserIdent1"] != null ? "UserIdent1='" + UserIdent1.Text.Replace("'","''") + "' " : "") +
					(HttpContext.Current.Session["UserIdent2"] != null ? ",UserIdent2='" + UserIdent2.Text.Replace("'","''") + "' " : "") +
					(HttpContext.Current.Session["UserIdent3"] != null ? ",UserIdent3='" + UserIdent3.Text.Replace("'","''") + "' " : "") +
					(HttpContext.Current.Session["UserIdent4"] != null ? ",UserIdent4='" + UserIdent4.Text.Replace("'","''") + "' " : "") +
					(HttpContext.Current.Session["UserIdent5"] != null ? ",UserIdent5='" + UserIdent5.Text.Replace("'","''") + "' " : "") +
					(HttpContext.Current.Session["UserIdent6"] != null ? ",UserIdent6='" + UserIdent6.Text.Replace("'","''") + "' " : "") +
					(HttpContext.Current.Session["UserIdent7"] != null ? ",UserIdent7='" + UserIdent7.Text.Replace("'","''") + "' " : "") +
					(HttpContext.Current.Session["UserIdent8"] != null ? ",UserIdent8='" + UserIdent8.Text.Replace("'","''") + "' " : "") +
					(HttpContext.Current.Session["UserIdent9"] != null ? ",UserIdent9='" + UserIdent9.Text.Replace("'","''") + "' " : "") +
					(HttpContext.Current.Session["UserIdent10"] != null ? ",UserIdent10='" + UserIdent10.Text.Replace("'","''") + "' " : "") +
					(HttpContext.Current.Session["UserCheck1"] != null && UserCheck1.SelectedIndex != -1 ? ",UserCheck1=" + UserCheck1.SelectedValue + " " : "") +
					(HttpContext.Current.Session["UserCheck2"] != null && UserCheck2.SelectedIndex != -1 ? ",UserCheck2=" + UserCheck2.SelectedValue + " " : "") +
					(HttpContext.Current.Session["UserCheck3"] != null && UserCheck3.SelectedIndex != -1 ? ",UserCheck3=" + UserCheck3.SelectedValue + " " : "") +
					"WHERE UserID = " + UID + " AND SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));

				if(Note.Text != "" || HttpContext.Current.Request.QueryString["NID"] != null)
				{
					if(HttpContext.Current.Request.QueryString["NID"] != null)
					{
						OdbcDataReader rs = Db.recordSet("SELECT un.Note, un.UserNoteID FROM UserNote un INNER JOIN [User] u ON un.UserID = u.UserID WHERE u.UserID = " + UID + " AND u.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " AND un.UserNoteID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["NID"]));
						if(rs.Read())
						{
							if(rs.GetString(0) != Note.Text)
							{
								Db.execute("UPDATE UserNote SET Note = '" + Note.Text.Replace("'","''") + "', EditSponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + ", EditDT = GETDATE() WHERE UserNoteID = " + rs.GetInt32(1));
							}
						}
						rs.Close();
					}
					else
					{
						Db.execute("INSERT INTO UserNote (UserID,DT,SponsorAdminID,Note) VALUES (" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]) + ",GETDATE()," + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + ",'" + Note.Text.Replace("'","''") + "')");
					}
				}
				if(VisitDT.Text != "")
				{
					bool cont = true;
					try
					{
						Convert.ToDateTime(VisitDT.Text);
					}
					catch(Exception){cont=false;}
					if(cont)
					{
						if(HttpContext.Current.Request.QueryString["USID"] != null)
						{
							Db.execute("UPDATE UserSchedule SET " +
								"UserProjectRoundUserID = " + (VisitUPRUID.SelectedValue != "NULL" && VisitUPRUID.SelectedValue != "0" ? Convert.ToInt32(VisitUPRUID.SelectedValue).ToString() : "NULL") + "," +
								"DT = '" + Convert.ToDateTime(VisitDT.Text).ToString("yyyy-MM-dd HH:mm") + "', " +
								"SponsorReminderID = " + (VisitSponsorReminderID.SelectedValue != "NULL" && VisitSponsorReminderID.SelectedValue != "0" ? Convert.ToInt32(VisitSponsorReminderID.SelectedValue).ToString() : "NULL") + "," +
								"Reminder = " + (VisitReminder.Text != "" && VisitReminder.Text != "0" ? Convert.ToInt32(VisitReminder.Text).ToString() : "NULL") + "," +
								"Note = '" + VisitNote.Text.Replace("'","''") + "', " +
								"Email = '" + VisitEmail.Text.Replace("'","''") + "' " +
								"WHERE UserID = " + UID + " AND UserScheduleID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["USID"]));
						}
						else
						{
							Db.execute("INSERT INTO UserSchedule " +
								"(UserID,UserProjectRoundUserID,DT,SponsorReminderID,Reminder,Note,Email) " +
								"VALUES " +
								"(" + UID + "," + (VisitUPRUID.SelectedValue != "NULL" && VisitUPRUID.SelectedValue != "0" ? Convert.ToInt32(VisitUPRUID.SelectedValue).ToString() : "NULL") + ",'" + Convert.ToDateTime(VisitDT.Text).ToString("yyyy-MM-dd HH:mm") + "'," + (VisitSponsorReminderID.SelectedValue != "NULL" && VisitSponsorReminderID.SelectedValue != "0" ? Convert.ToInt32(VisitSponsorReminderID.SelectedValue).ToString() : "NULL") + "," + (VisitReminder.Text != "" && VisitReminder.Text != "0" ? Convert.ToInt32(VisitReminder.Text).ToString() : "NULL") + ",'" + VisitNote.Text.Replace("'","''") + "','" + VisitEmail.Text.Replace("'","''") + "')");
						}
					}
				}
				if(ProjectRoundUnitID.SelectedValue != "0")
				{
					if(HttpContext.Current.Request.QueryString["UPRUID"] != null)
					{
						Db.execute("UPDATE UserProjectRoundUser SET Note = '" + SurveyNote.Text.Replace("'","''") + "' WHERE UserProjectRoundUserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UPRUID"]) + " AND UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]));
						OdbcDataReader rs = Db.recordSet("SELECT ProjectRoundUserID " +
							"FROM UserProjectRoundUser " +
							"WHERE UserProjectRoundUserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UPRUID"]) + " " +
							"AND UserID = " + UID);
						if(rs.Read())
						{
							Db.execute("UPDATE ProjectRoundUser SET " +
								"Email = '" + Email.Text.Replace("'","") + "', " +
								"ProjectRoundUnitID = " + Convert.ToInt32(ProjectRoundUnitID.SelectedValue.Split(':')[0]) + ", " +
								"ProjectRoundID = " + Convert.ToInt32(ProjectRoundUnitID.SelectedValue.Split(':')[1]) + " " +
								"WHERE ProjectRoundUserID = " + rs.GetInt32(0));
						}
						rs.Close();
					}
					else
					{
						int pruid = projectSetup.createUser(
							Convert.ToInt32(ProjectRoundUnitID.SelectedValue.Split(':')[1]),
							Convert.ToInt32(ProjectRoundUnitID.SelectedValue.Split(':')[0]).ToString(),
							Email.Text.Replace("'",""));
						Db.execute("INSERT INTO UserProjectRoundUser (" +
							"UserID," +
							"ProjectRoundUserID," +
							"Note" +
							") VALUES (" +
							"" + UID + "," +
							"" + pruid + "," +
							"'" + SurveyNote.Text.Replace("'","''") + "'" +
							")");
					}
				}
			}
			else
			{
				Db.execute("INSERT INTO [User] (" +
					"SponsorID" +
					(HttpContext.Current.Session["UserIdent1"] != null ? ",UserIdent1" : "") +
					(HttpContext.Current.Session["UserIdent2"] != null ? ",UserIdent2" : "") +
					(HttpContext.Current.Session["UserIdent3"] != null ? ",UserIdent3" : "") +
					(HttpContext.Current.Session["UserIdent4"] != null ? ",UserIdent4" : "") +
					(HttpContext.Current.Session["UserIdent5"] != null ? ",UserIdent5" : "") +
					(HttpContext.Current.Session["UserIdent6"] != null ? ",UserIdent6" : "") +
					(HttpContext.Current.Session["UserIdent7"] != null ? ",UserIdent7" : "") +
					(HttpContext.Current.Session["UserIdent8"] != null ? ",UserIdent8" : "") +
					(HttpContext.Current.Session["UserIdent9"] != null ? ",UserIdent9" : "") +
					(HttpContext.Current.Session["UserIdent10"] != null ? ",UserIdent10" : "") +
					(HttpContext.Current.Session["UserCheck1"] != null && UserCheck1.SelectedIndex != -1 ? ",UserCheck1" : "") +
					(HttpContext.Current.Session["UserCheck2"] != null && UserCheck2.SelectedIndex != -1 ? ",UserCheck2" : "") +
					(HttpContext.Current.Session["UserCheck3"] != null && UserCheck3.SelectedIndex != -1 ? ",UserCheck3" : "") +
					") VALUES (" +
					Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) +
					(HttpContext.Current.Session["UserIdent1"] != null ? ",'" + UserIdent1.Text.Replace("'","''") + "'" : "") +
					(HttpContext.Current.Session["UserIdent2"] != null ? ",'" + UserIdent2.Text.Replace("'","''") + "'" : "") +
					(HttpContext.Current.Session["UserIdent3"] != null ? ",'" + UserIdent3.Text.Replace("'","''") + "'" : "") +
					(HttpContext.Current.Session["UserIdent4"] != null ? ",'" + UserIdent4.Text.Replace("'","''") + "'" : "") +
					(HttpContext.Current.Session["UserIdent5"] != null ? ",'" + UserIdent5.Text.Replace("'","''") + "'" : "") +
					(HttpContext.Current.Session["UserIdent6"] != null ? ",'" + UserIdent6.Text.Replace("'","''") + "'" : "") +
					(HttpContext.Current.Session["UserIdent7"] != null ? ",'" + UserIdent7.Text.Replace("'","''") + "'" : "") +
					(HttpContext.Current.Session["UserIdent8"] != null ? ",'" + UserIdent8.Text.Replace("'","''") + "'" : "") +
					(HttpContext.Current.Session["UserIdent9"] != null ? ",'" + UserIdent9.Text.Replace("'","''") + "'" : "") +
					(HttpContext.Current.Session["UserIdent10"] != null ? ",'" + UserIdent10.Text.Replace("'","''") + "'" : "") +
					(HttpContext.Current.Session["UserCheck1"] != null && UserCheck1.SelectedIndex != -1 ? "," + UserCheck1.SelectedValue + "" : "") +
					(HttpContext.Current.Session["UserCheck2"] != null && UserCheck2.SelectedIndex != -1 ? "," + UserCheck2.SelectedValue + "" : "") +
					(HttpContext.Current.Session["UserCheck3"] != null && UserCheck3.SelectedIndex != -1 ? "," + UserCheck3.SelectedValue + "" : "") +
					")");
				string email = "";
				OdbcDataReader rs = Db.recordSet("SELECT TOP 1 UserID " +
					(HttpContext.Current.Session["EmailIdent"].ToString() != "0" ? ", UserIdent" + HttpContext.Current.Session["EmailIdent"] + " " : "") +
					"FROM [User] WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " ORDER BY UserID DESC");
				if(rs.Read())
				{
					UID = rs.GetInt32(0);
					if(HttpContext.Current.Session["EmailIdent"].ToString() != "0")
					{
						email = rs.GetString(1).Replace("'","''");
					}
				}
				rs.Close();
				rs = Db.recordSet("SELECT TOP 1 UserNr FROM [User] WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " ORDER BY UserNr DESC");
				if(rs.Read())
				{
					Db.execute("UPDATE [User] SET UserNr = " + ((rs.IsDBNull(0) ? 0 : rs.GetInt32(0))+1) + " WHERE UserNr IS NULL AND UserID = " + UID + " AND SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
				}
				rs.Close();
				rs = Db.recordSet("SELECT a.PRUID, u.ProjectRoundID, a.Note FROM SponsorAutoPRU a INNER JOIN ProjectRoundUnit u ON a.PRUID = u.ProjectRoundUnitID WHERE a.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
				while(rs.Read())
				{
					int pruid = projectSetup.createUser(
						rs.GetInt32(1),
						rs.GetInt32(0).ToString(),
						email);
					Db.execute("INSERT INTO UserProjectRoundUser (" +
						"UserID," +
						"ProjectRoundUserID," +
						"Note" +
						") VALUES (" +
						"" + UID + "," +
						"" + pruid + "," +
						"'" + rs.GetString(2).Replace("'","''") + "'" +
						")");
				}
				rs.Close();
			}
			HttpContext.Current.Response.Redirect("dashboard.aspx?UID=" + UID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
		}

		private void Search_Click(object sender, EventArgs e)
		{
			search();
		}
		private void search()
		{
			ExportButton.Visible = true;
			SPSS.Visible = false;
			ExportSurvey.Visible = false;

			SearchResults.Text = "<TABLE BORDER=\"0\" CELLPADDING=\"0\" CELLSPACING=\"0\">";

			int uid = 0;
			OdbcDataReader rs = Db.recordSet("SELECT " +
				"u.UserID, " +			// 0
				"u.UserIdent1, " +
				"u.UserIdent2, " +
				"u.UserIdent3, " +
				"u.UserIdent4, " +
				"u.UserIdent5, " +		// 5
				"u.UserIdent6, " +
				"u.UserIdent7, " +
				"u.UserIdent8, " +
				"u.UserIdent9, " +
				"u.UserIdent10, " +		// 10
				"u.UserNr, " +
				"(SELECT TOP 1 x.DT FROM UserSchedule x WHERE x.DT >= GETDATE() AND x.UserID = u.UserID ORDER BY x.DT ASC), " +
				"sc1.Txt, " +
				"sc2.Txt, " +
				"sc3.Txt, " +			// 15
				"u.FeedbackSent " +
				"FROM [User] u " +
				"LEFT OUTER JOIN SponsorUserCheck sc1 ON u.UserCheck1 = sc1.SponsorUserCheckID AND sc1.SponsorID = u.SponsorID AND sc1.UserCheckNr = 1 " +
				"LEFT OUTER JOIN SponsorUserCheck sc2 ON u.UserCheck2 = sc2.SponsorUserCheckID AND sc2.SponsorID = u.SponsorID AND sc2.UserCheckNr = 2 " +
				"LEFT OUTER JOIN SponsorUserCheck sc3 ON u.UserCheck3 = sc3.SponsorUserCheckID AND sc3.SponsorID = u.SponsorID AND sc3.UserCheckNr = 3 " +
				"WHERE u.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " " +
				"AND (" +
					(HttpContext.Current.Session["UserIdent1"] != null ? "UserIdent1 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
					(HttpContext.Current.Session["UserIdent2"] != null ? "OR UserIdent2 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
					(HttpContext.Current.Session["UserIdent3"] != null ? "OR UserIdent3 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
					(HttpContext.Current.Session["UserIdent4"] != null ? "OR UserIdent4 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
					(HttpContext.Current.Session["UserIdent5"] != null ? "OR UserIdent5 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
					(HttpContext.Current.Session["UserIdent6"] != null ? "OR UserIdent6 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
					(HttpContext.Current.Session["UserIdent7"] != null ? "OR UserIdent7 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
					(HttpContext.Current.Session["UserIdent8"] != null ? "OR UserIdent8 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
					(HttpContext.Current.Session["UserIdent9"] != null ? "OR UserIdent9 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
					(HttpContext.Current.Session["UserIdent10"] != null ? "OR UserIdent10 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
					(HttpContext.Current.Session["UserNr"] != null ? "OR CAST(UserNr AS VARCHAR(8)) LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
				")");
			while(rs.Read())
			{
				if(uid == 0)
				{
					SearchResults.Text += "<TR>";
					if(HttpContext.Current.Session["UserNr"] != null && HttpContext.Current.Session["ShowUserNr"] != null && (bool)HttpContext.Current.Session["ShowUserNr"])
					{
						SearchResults.Text += "<TD><B>" + HttpContext.Current.Session["UserNr"] + "</B>&nbsp;&nbsp;</TD>";
					}
					if(HttpContext.Current.Session["UserIdent1"] != null && HttpContext.Current.Session["ShowUserIdent1"] != null && (bool)HttpContext.Current.Session["ShowUserIdent1"])
					{
						SearchResults.Text += "<TD><B>" + HttpContext.Current.Session["UserIdent1"] + "</B>&nbsp;&nbsp;</TD>";
					}
					if(HttpContext.Current.Session["UserIdent2"] != null && HttpContext.Current.Session["ShowUserIdent2"] != null && (bool)HttpContext.Current.Session["ShowUserIdent2"])
					{
						SearchResults.Text += "<TD><B>" + HttpContext.Current.Session["UserIdent2"] + "</B>&nbsp;&nbsp;</TD>";
					}
					if(HttpContext.Current.Session["UserIdent3"] != null && HttpContext.Current.Session["ShowUserIdent3"] != null && (bool)HttpContext.Current.Session["ShowUserIdent3"])
					{
						SearchResults.Text += "<TD><B>" + HttpContext.Current.Session["UserIdent3"] + "</B>&nbsp;&nbsp;</TD>";
					}
					if(HttpContext.Current.Session["UserIdent4"] != null && HttpContext.Current.Session["ShowUserIdent4"] != null && (bool)HttpContext.Current.Session["ShowUserIdent4"])
					{
						SearchResults.Text += "<TD><B>" + HttpContext.Current.Session["UserIdent4"] + "</B>&nbsp;&nbsp;</TD>";
					}
					if(HttpContext.Current.Session["UserIdent5"] != null && HttpContext.Current.Session["ShowUserIdent5"] != null && (bool)HttpContext.Current.Session["ShowUserIdent5"])
					{
						SearchResults.Text += "<TD><B>" + HttpContext.Current.Session["UserIdent5"] + "</B>&nbsp;&nbsp;</TD>";
					}
					if(HttpContext.Current.Session["UserIdent6"] != null && HttpContext.Current.Session["ShowUserIdent6"] != null && (bool)HttpContext.Current.Session["ShowUserIdent6"])
					{
						SearchResults.Text += "<TD><B>" + HttpContext.Current.Session["UserIdent6"] + "</B>&nbsp;&nbsp;</TD>";
					}
					if(HttpContext.Current.Session["UserIdent7"] != null && HttpContext.Current.Session["ShowUserIdent7"] != null && (bool)HttpContext.Current.Session["ShowUserIdent7"])
					{
						SearchResults.Text += "<TD><B>" + HttpContext.Current.Session["UserIdent7"] + "</B>&nbsp;&nbsp;</TD>";
					}
					if(HttpContext.Current.Session["UserIdent8"] != null && HttpContext.Current.Session["ShowUserIdent8"] != null && (bool)HttpContext.Current.Session["ShowUserIdent8"])
					{
						SearchResults.Text += "<TD><B>" + HttpContext.Current.Session["UserIdent8"] + "</B>&nbsp;&nbsp;</TD>";
					}
					if(HttpContext.Current.Session["UserIdent9"] != null && HttpContext.Current.Session["ShowUserIdent9"] != null && (bool)HttpContext.Current.Session["ShowUserIdent9"])
					{
						SearchResults.Text += "<TD><B>" + HttpContext.Current.Session["UserIdent9"] + "</B>&nbsp;&nbsp;</TD>";
					}
					if(HttpContext.Current.Session["UserIdent10"] != null && HttpContext.Current.Session["ShowUserIdent10"] != null && (bool)HttpContext.Current.Session["ShowUserIdent10"])
					{
						SearchResults.Text += "<TD><B>" + HttpContext.Current.Session["UserIdent10"] + "</B>&nbsp;&nbsp;</TD>";
					}
					if(HttpContext.Current.Session["UserCheck1"] != null && HttpContext.Current.Session["ShowUserCheck1"] != null && (bool)HttpContext.Current.Session["ShowUserCheck1"])
					{
						SearchResults.Text += "<TD><B>" + HttpContext.Current.Session["UserCheck1"] + "</B>&nbsp;&nbsp;</TD>";
					}
					if(HttpContext.Current.Session["UserCheck2"] != null && HttpContext.Current.Session["ShowUserCheck2"] != null && (bool)HttpContext.Current.Session["ShowUserCheck2"])
					{
						SearchResults.Text += "<TD><B>" + HttpContext.Current.Session["UserCheck2"] + "</B>&nbsp;&nbsp;</TD>";
					}
					if(HttpContext.Current.Session["UserCheck3"] != null && HttpContext.Current.Session["ShowUserCheck3"] != null && (bool)HttpContext.Current.Session["ShowUserCheck3"])
					{
						SearchResults.Text += "<TD><B>" + HttpContext.Current.Session["UserCheck3"] + "</B>&nbsp;&nbsp;</TD>";
					}
					SearchResults.Text += "<TD><B>Nästa besök</B>&nbsp;&nbsp;</TD>";
					if(HttpContext.Current.Session["CustomReportTemplateID"] != null)
					{
						SearchResults.Text += "<TD><B>Återkoppling</B>&nbsp;&nbsp;</TD>";
					}
					SearchResults.Text += "</TR>";
				}
				uid = rs.GetInt32(0);
				SearchResults.Text += "<TR>";
				if(HttpContext.Current.Session["UserNr"] != null && HttpContext.Current.Session["ShowUserNr"] != null && (bool)HttpContext.Current.Session["ShowUserNr"])
				{
					SearchResults.Text += "<TD><A NAME=\"UserID" + rs.GetInt32(0) + "\" HREF=\"dashboard.aspx?UID=" + rs.GetInt32(0) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">" + (!rs.IsDBNull(11) ? rs.GetInt32(11) : 0) + "</A>&nbsp;&nbsp;</TD>";
				}
				if(HttpContext.Current.Session["UserIdent1"] != null && HttpContext.Current.Session["ShowUserIdent1"] != null && (bool)HttpContext.Current.Session["ShowUserIdent1"])
				{
					SearchResults.Text += "<TD><A HREF=\"dashboard.aspx?UID=" + rs.GetInt32(0) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">" + (!rs.IsDBNull(1) && rs.GetString(1).Trim() != "" ? rs.GetString(1) : "&lt; blank &gt;") + "</A>&nbsp;&nbsp;</TD>";
				}
				if(HttpContext.Current.Session["UserIdent2"] != null && HttpContext.Current.Session["ShowUserIdent2"] != null && (bool)HttpContext.Current.Session["ShowUserIdent2"])
				{
					SearchResults.Text += "<TD>" + (!rs.IsDBNull(2) ? rs.GetString(2) : "") + "&nbsp;&nbsp;</TD>";
				}
				if(HttpContext.Current.Session["UserIdent3"] != null && HttpContext.Current.Session["ShowUserIdent3"] != null && (bool)HttpContext.Current.Session["ShowUserIdent3"])
				{
					SearchResults.Text += "<TD>" + (!rs.IsDBNull(3) ? rs.GetString(3) : "") + "&nbsp;&nbsp;</TD>";
				}
				if(HttpContext.Current.Session["UserIdent4"] != null && HttpContext.Current.Session["ShowUserIdent4"] != null && (bool)HttpContext.Current.Session["ShowUserIdent4"])
				{
					SearchResults.Text += "<TD>" + (!rs.IsDBNull(4) ? rs.GetString(4) : "") + "&nbsp;&nbsp;</TD>";
				}
				if(HttpContext.Current.Session["UserIdent5"] != null && HttpContext.Current.Session["ShowUserIdent5"] != null && (bool)HttpContext.Current.Session["ShowUserIdent5"])
				{
					SearchResults.Text += "<TD>" + (!rs.IsDBNull(5) ? rs.GetString(5) : "") + "&nbsp;&nbsp;</TD>";
				}
				if(HttpContext.Current.Session["UserIdent6"] != null && HttpContext.Current.Session["ShowUserIdent6"] != null && (bool)HttpContext.Current.Session["ShowUserIdent6"])
				{
					SearchResults.Text += "<TD>" + (!rs.IsDBNull(6) ? rs.GetString(6) : "") + "&nbsp;&nbsp;</TD>";
				}
				if(HttpContext.Current.Session["UserIdent7"] != null && HttpContext.Current.Session["ShowUserIdent7"] != null && (bool)HttpContext.Current.Session["ShowUserIdent7"])
				{
					SearchResults.Text += "<TD>" + (!rs.IsDBNull(7) ? rs.GetString(7) : "") + "&nbsp;&nbsp;</TD>";
				}
				if(HttpContext.Current.Session["UserIdent8"] != null && HttpContext.Current.Session["ShowUserIdent8"] != null && (bool)HttpContext.Current.Session["ShowUserIdent8"])
				{
					SearchResults.Text += "<TD>" + (!rs.IsDBNull(8) ? rs.GetString(8) : "") + "&nbsp;&nbsp;</TD>";
				}
				if(HttpContext.Current.Session["UserIdent9"] != null && HttpContext.Current.Session["ShowUserIdent9"] != null && (bool)HttpContext.Current.Session["ShowUserIdent9"])
				{
					SearchResults.Text += "<TD>" + (!rs.IsDBNull(9) ? rs.GetString(9) : "") + "&nbsp;&nbsp;</TD>";
				}
				if(HttpContext.Current.Session["UserIdent10"] != null && HttpContext.Current.Session["ShowUserIdent10"] != null && (bool)HttpContext.Current.Session["ShowUserIdent10"])
				{
					SearchResults.Text += "<TD>" + (!rs.IsDBNull(10) ? rs.GetString(10) : "") + "&nbsp;&nbsp;</TD>";
				}
				if(HttpContext.Current.Session["UserCheck1"] != null && HttpContext.Current.Session["ShowUserCheck1"] != null && (bool)HttpContext.Current.Session["ShowUserCheck1"])
				{
					SearchResults.Text += "<TD>" + (!rs.IsDBNull(13) ? rs.GetString(13) : "") + "&nbsp;&nbsp;</TD>";
				}
				if(HttpContext.Current.Session["UserCheck2"] != null && HttpContext.Current.Session["ShowUserCheck2"] != null && (bool)HttpContext.Current.Session["ShowUserCheck2"])
				{
					SearchResults.Text += "<TD>" + (!rs.IsDBNull(14) ? rs.GetString(14) : "") + "&nbsp;&nbsp;</TD>";
				}
				if(HttpContext.Current.Session["UserCheck3"] != null && HttpContext.Current.Session["ShowUserCheck3"] != null && (bool)HttpContext.Current.Session["ShowUserCheck3"])
				{
					SearchResults.Text += "<TD>" + (!rs.IsDBNull(15) ? rs.GetString(15) : "") + "&nbsp;&nbsp;</TD>";
				}
				SearchResults.Text += "<TD>" + (!rs.IsDBNull(12) ? rs.GetDateTime(12).ToString("yyyy-MM-dd HH:mm") : "") + "&nbsp;&nbsp;</TD>";
				if(HttpContext.Current.Session["CustomReportTemplateID"] != null)
				{
					SearchResults.Text += "<TD>" +
						"<A TITLE=\"Redigera\" HREF=\"customFeedback.aspx?UserID=" + rs.GetInt32(0) + "&CustomReportTemplateID=" + HttpContext.Current.Session["CustomReportTemplateID"] + "&Edit=1\"><img src=\"img/unit_on.gif\" border=\"0\"/></A>&nbsp;" +
						"<A TARGET=\"_blank\" TITLE=\"Ladda ner PDF\" HREF=\"customFeedback.aspx?UserID=" + rs.GetInt32(0) + "&CustomReportTemplateID=" + HttpContext.Current.Session["CustomReportTemplateID"] + "&PDF=1\"><img src=\"img/pdfsmall.gif\" border=\"0\"/></A>&nbsp;" +
						"<A TITLE=\"Återställ till orginal\" HREF=\"customFeedback.aspx?UserID=" + rs.GetInt32(0) + "&CustomReportTemplateID=" + HttpContext.Current.Session["CustomReportTemplateID"] + "&Regen=1\"><img src=\"img/refresh.gif\" border=\"0\"/></A>&nbsp;" +
						(HttpContext.Current.Session["FeedbackEmail"] != null ? "" +
						"<A TITLE=\"Testa utskick\" HREF=\"customFeedback.aspx?UserID=" + rs.GetInt32(0) + "&CustomReportTemplateID=" + HttpContext.Current.Session["CustomReportTemplateID"] + "&SendTest=1\"><img src=\"img/emailTest.gif\" border=\"0\"/></A>&nbsp;" +
						"<A TITLE=\"Skicka med e-post\" HREF=\"javascript:;\" ONCLICK=\"if(confirm('Är du säker?')){location.href='customFeedback.aspx?UserID=" + rs.GetInt32(0) + "&CustomReportTemplateID=" + HttpContext.Current.Session["CustomReportTemplateID"] + "&Send=1';}\"><img src=\"img/email.gif\" border=\"0\"/></A>&nbsp;" +
						(rs.IsDBNull(16) ? "" : "Skickad " + rs.GetDateTime(16).ToString("yyyy-MM-dd HH:mm")) +
						"" : "") +
						"&nbsp;</TD>";
				}
				SearchResults.Text += "</TR>";
			}
			rs.Close();

			SearchResults.Text += "</TABLE>";
		}

		private void AddNote_Click(object sender, EventArgs e)
		{
			NotePH.Visible = true;
			AddNote.Visible = false;
		}

		private void AddSurvey_Click(object sender, EventArgs e)
		{
			SurveyPH.Visible = true;
			AddSurvey.Visible = false;
		}

		private void sendlinkbutton_Click(object sender, EventArgs e)
		{
			OdbcDataReader rs = Db.recordSet("SELECT u.ProjectRoundUserID, u.UserKey, sa.Email, u.Email FROM UserProjectRoundUser up INNER JOIN ProjectRoundUser u ON up.ProjectRoundUserID = u.ProjectRoundUserID INNER JOIN [User] upu ON up.UserID = upu.UserID INNER JOIN SponsorAdmin sa ON upu.SponsorID = sa.SponsorID WHERE up.UserProjectRoundUserID = " + Convert.ToInt32(sendlinkUPRUID.Value) + " AND sa.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]));
			if(rs.Read())
			{
				string UK = appURL + "/submit.aspx?K=" + rs.GetGuid(1).ToString().Substring(0,8) + rs.GetInt32(0).ToString();
				string body = sendlinkbody.Text;
				try
				{
					if(body.IndexOf("<LINK>") >= 0)
					{
						body = body.Replace("<LINK>",UK);
					}
					else if(body.IndexOf("<LINK/>") >= 0)
					{
						body = body.Replace("<LINK/>",UK);
					}
					else
					{
						body += "\r\n\r\n" + UK;
					}

					System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
					msg.To = rs.GetString(3);
					msg.From = rs.GetString(2);
					msg.Bcc = rs.GetString(2);
					msg.Subject = sendlinksubject.Text;
					msg.Body = body;
					msg.BodyFormat = System.Web.Mail.MailFormat.Text;
					msg.BodyEncoding = System.Text.Encoding.GetEncoding(1252);
					System.Web.Mail.SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
					System.Web.Mail.SmtpMail.Send(msg);
				}
				catch(Exception){}
			}
			rs.Close();

			HttpContext.Current.Response.Redirect("dashboard.aspx?UID=" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
		}

		private void AddVisit_Click(object sender, EventArgs e)
		{
			VisitPH.Visible = true;
			AddVisit.Visible = false;
		}

		private void SendVisitReminderButton_Click(object sender, EventArgs e)
		{
			try
			{
				System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
				msg.To = SendVisitReminderTo.Text;
				msg.Bcc = SendVisitReminderFrom.Text;
				msg.From = SendVisitReminderFrom.Text;
				msg.Subject = SendVisitReminderSubject.Text;
				msg.Body = SendVisitReminderBody.Text;
				msg.BodyFormat = System.Web.Mail.MailFormat.Text;
				msg.BodyEncoding = System.Text.Encoding.GetEncoding(1252);
				System.Web.Mail.SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
				System.Web.Mail.SmtpMail.Send(msg);
			}
			catch(Exception){}

			HttpContext.Current.Response.Redirect("dashboard.aspx?UID=" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
		}

		private void ExportButton_Click(object sender, EventArgs e)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			int uid = 0;
			OdbcDataReader rs = Db.recordSet("SELECT " +
				"u.UserID, " +
				"u.UserIdent1, " +
				"u.UserIdent2, " +
				"u.UserIdent3, " +
				"u.UserIdent4, " +
				"u.UserIdent5, " +
				"u.UserIdent6, " +
				"u.UserIdent7, " +
				"u.UserIdent8, " +
				"u.UserIdent9, " +
				"u.UserIdent10, " +
				"u.UserNr, " +
				"sc1.Txt, " +
				"sc2.Txt, " +
				"sc3.Txt " +
				"FROM [User] u " +
				"LEFT OUTER JOIN SponsorUserCheck sc1 ON u.UserCheck1 = sc1.SponsorUserCheckID AND sc1.SponsorID = u.SponsorID AND sc1.UserCheckNr = 1 " +
				"LEFT OUTER JOIN SponsorUserCheck sc2 ON u.UserCheck2 = sc2.SponsorUserCheckID AND sc2.SponsorID = u.SponsorID AND sc2.UserCheckNr = 2 " +
				"LEFT OUTER JOIN SponsorUserCheck sc3 ON u.UserCheck3 = sc3.SponsorUserCheckID AND sc3.SponsorID = u.SponsorID AND sc3.UserCheckNr = 3 " +
				"WHERE u.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " " +
				"AND (" +
				(HttpContext.Current.Session["UserIdent1"] != null ? "UserIdent1 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
				(HttpContext.Current.Session["UserIdent2"] != null ? "OR UserIdent2 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
				(HttpContext.Current.Session["UserIdent3"] != null ? "OR UserIdent3 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
				(HttpContext.Current.Session["UserIdent4"] != null ? "OR UserIdent4 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
				(HttpContext.Current.Session["UserIdent5"] != null ? "OR UserIdent5 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
				(HttpContext.Current.Session["UserIdent6"] != null ? "OR UserIdent6 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
				(HttpContext.Current.Session["UserIdent7"] != null ? "OR UserIdent7 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
				(HttpContext.Current.Session["UserIdent8"] != null ? "OR UserIdent8 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
				(HttpContext.Current.Session["UserIdent9"] != null ? "OR UserIdent9 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
				(HttpContext.Current.Session["UserIdent10"] != null ? "OR UserIdent10 LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
				(HttpContext.Current.Session["UserNr"] != null ? "OR CAST(UserNr AS VARCHAR(8)) LIKE '%" + SearchText.Text.Replace("'","''") + "%'" : "") +
				")");
			while(rs.Read())
			{
				bool tab = false;
				if(uid == 0)
				{
					if(HttpContext.Current.Session["UserNr"] != null)
					{
						sb.Append((tab ? "\t" : "") + HttpContext.Current.Session["UserNr"]); tab = true;
					}
					if(HttpContext.Current.Session["UserIdent1"] != null)
					{
						sb.Append((tab ? "\t" : "") + HttpContext.Current.Session["UserIdent1"]); tab = true;
					}
					if(HttpContext.Current.Session["UserIdent2"] != null)
					{
						sb.Append((tab ? "\t" : "") + HttpContext.Current.Session["UserIdent2"]); tab = true;
					}
					if(HttpContext.Current.Session["UserIdent3"] != null)
					{
						sb.Append((tab ? "\t" : "") + HttpContext.Current.Session["UserIdent3"]); tab = true;
					}
					if(HttpContext.Current.Session["UserIdent4"] != null)
					{
						sb.Append((tab ? "\t" : "") + HttpContext.Current.Session["UserIdent4"]); tab = true;
					}
					if(HttpContext.Current.Session["UserIdent5"] != null)
					{
						sb.Append((tab ? "\t" : "") + HttpContext.Current.Session["UserIdent5"]); tab = true;
					}
					if(HttpContext.Current.Session["UserIdent6"] != null)
					{
						sb.Append((tab ? "\t" : "") + HttpContext.Current.Session["UserIdent6"]); tab = true;
					}
					if(HttpContext.Current.Session["UserIdent7"] != null)
					{
						sb.Append((tab ? "\t" : "") + HttpContext.Current.Session["UserIdent7"]); tab = true;
					}
					if(HttpContext.Current.Session["UserIdent8"] != null)
					{
						sb.Append((tab ? "\t" : "") + HttpContext.Current.Session["UserIdent8"]); tab = true;
					}
					if(HttpContext.Current.Session["UserIdent9"] != null)
					{
						sb.Append((tab ? "\t" : "") + HttpContext.Current.Session["UserIdent9"]); tab = true;
					}
					if(HttpContext.Current.Session["UserIdent10"] != null)
					{
						sb.Append((tab ? "\t" : "") + HttpContext.Current.Session["UserIdent10"]); tab = true;
					}
					if(HttpContext.Current.Session["UserCheck1"] != null)
					{
						sb.Append((tab ? "\t" : "") + HttpContext.Current.Session["UserCheck1"]); tab = true;
					}
					if(HttpContext.Current.Session["UserCheck2"] != null)
					{
						sb.Append((tab ? "\t" : "") + HttpContext.Current.Session["UserCheck2"]); tab = true;
					}
					if(HttpContext.Current.Session["UserCheck3"] != null)
					{
						sb.Append((tab ? "\t" : "") + HttpContext.Current.Session["UserCheck3"]); tab = true;
					}
					tab = false;
				}
				uid = rs.GetInt32(0);

				sb.Append("\r\n");
				if(HttpContext.Current.Session["UserNr"] != null)
				{
					sb.Append((tab ? "\t" : "") + (!rs.IsDBNull(11) ? rs.GetInt32(11) : 0)); tab = true;
				}
				if(HttpContext.Current.Session["UserIdent1"] != null)
				{
					sb.Append((tab ? "\t" : "") + (!rs.IsDBNull(1) ? rs.GetString(1) : "")); tab = true;
				}
				if(HttpContext.Current.Session["UserIdent2"] != null)
				{
					sb.Append((tab ? "\t" : "") + (!rs.IsDBNull(2) ? rs.GetString(2) : "")); tab = true;
				}
				if(HttpContext.Current.Session["UserIdent3"] != null)
				{
					sb.Append((tab ? "\t" : "") + (!rs.IsDBNull(3) ? rs.GetString(3) : "")); tab = true;
				}
				if(HttpContext.Current.Session["UserIdent4"] != null)
				{
					sb.Append((tab ? "\t" : "") + (!rs.IsDBNull(4) ? rs.GetString(4) : "")); tab = true;
				}
				if(HttpContext.Current.Session["UserIdent5"] != null)
				{
					sb.Append((tab ? "\t" : "") + (!rs.IsDBNull(5) ? rs.GetString(5) : "")); tab = true;
				}
				if(HttpContext.Current.Session["UserIdent6"] != null)
				{
					sb.Append((tab ? "\t" : "") + (!rs.IsDBNull(6) ? rs.GetString(6) : "")); tab = true;
				}
				if(HttpContext.Current.Session["UserIdent7"] != null)
				{
					sb.Append((tab ? "\t" : "") + (!rs.IsDBNull(7) ? rs.GetString(7) : "")); tab = true;
				}
				if(HttpContext.Current.Session["UserIdent8"] != null)
				{
					sb.Append((tab ? "\t" : "") + (!rs.IsDBNull(8) ? rs.GetString(8) : "")); tab = true;
				}
				if(HttpContext.Current.Session["UserIdent9"] != null)
				{
					sb.Append((tab ? "\t" : "") + (!rs.IsDBNull(9) ? rs.GetString(9) : "")); tab = true;
				}
				if(HttpContext.Current.Session["UserIdent10"] != null)
				{
					sb.Append((tab ? "\t" : "") + (!rs.IsDBNull(10) ? rs.GetString(10) : "")); tab = true;
				}
				if(HttpContext.Current.Session["UserCheck1"] != null)
				{
					sb.Append((tab ? "\t" : "") + (!rs.IsDBNull(12) ? rs.GetString(12) : "")); tab = true;
				}
				if(HttpContext.Current.Session["UserCheck2"] != null)
				{
					sb.Append((tab ? "\t" : "") + (!rs.IsDBNull(13) ? rs.GetString(13) : "")); tab = true;
				}
				if(HttpContext.Current.Session["UserCheck3"] != null)
				{
					sb.Append((tab ? "\t" : "") + (!rs.IsDBNull(14) ? rs.GetString(14) : "")); tab = true;
				}
			}
			rs.Close();

			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.ClearHeaders();
			HttpContext.Current.Response.Charset = "UTF-8";
			HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
			HttpContext.Current.Response.ContentType = "text/plain";
			HttpContext.Current.Response.AddHeader("content-length", sb.Length.ToString());
			HttpContext.Current.Response.AddHeader("content-disposition","attachment; filename=EFORM_DASHBOARD_" + DateTime.Now.Ticks + ".txt");
			HttpContext.Current.Response.Write(sb.ToString());
			HttpContext.Current.Response.Flush();
			HttpContext.Current.Response.End();
		}

		private void SPSS_Click(object sender, EventArgs e)
		{
			ExportSurvey.Visible = true;
		}

		private void ExportSPSS_Click(object sender, EventArgs e)
		{
			int PRID = 0, SID = 0, LID = 0;
			OdbcDataReader rs = Db.recordSet("SELECT pr.ProjectRoundID, pr.SurveyID, pr.LangID FROM Sponsor s INNER JOIN ProjectRound pr ON s.ProjectID = pr.ProjectID WHERE s.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
			while(rs.Read())
			{
				if(ProjectRoundID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected)
				{
					PRID = rs.GetInt32(0); SID = rs.GetInt32(1); LID = rs.GetInt32(2);
				}
			}
			rs.Close();

			if(PRID != 0)
			{
				Db.execExport(PRID,0,SID,LID,0,(Unfinished.Checked ? 1 : 0),0,0,0,Convert.ToInt32(HttpContext.Current.Session["SponsorID"]),IDS.Checked);
			}
		}

		private void DeleteUser_Click(object sender, EventArgs e)
		{
			Db.execute("UPDATE [User] SET SponsorID = -" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " AND UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]));
			HttpContext.Current.Response.Redirect("dashboard.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);

		}
	}
}
