using System;
using System.Collections;
using System.ComponentModel;
//using System.Data.Odbc;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace eform
{
	/// <summary>
	/// Summary description for projectSetup.
	/// </summary>
	public class projectSetup : System.Web.UI.Page
	{
		protected TextBox Internal;
		protected TextBox Name;
		protected HtmlInputFile Logo;
		protected Label LogoImg;
		protected Button Save;
		protected Button AddRound;
		protected Button AddPRQO;
		protected Button SaveRound;
		protected Button CancelRound;
		protected PlaceHolder RoundTextElements;
		protected PlaceHolder SendAll;
		protected PlaceHolder Units;
		protected PlaceHolder Rounds;
		protected DropDownList SurveyID;
		protected Label ProjectSurvey;
		protected Label ProjectSurveyText;

		int projectID = 0, projectRoundID = 0, projectRoundUnitID = 0, projectRoundUserID = 0;

		bool exportUnit = false, exportUser = false, showUsers = false, exportSpssDataOneNew = false, exportData = false, exportSpssData = false, exportDataOne = false, exportSpssDataOne = false;

		public const string appURL = "http://eform.se";
		public const string appEmail = "info@eform.se";

		private void Page_Load(object sender, System.EventArgs e)
		{
			Server.ScriptTimeout = 900;

			if(HttpContext.Current.Request.QueryString["DeleteLogo"] != null)
			{
				try
				{
					System.IO.File.Delete(Server.MapPath("..\\img\\project\\logo" + HttpContext.Current.Request.QueryString["DeleteLogo"] + ".gif"));
				}
				catch(Exception){}
			}

			showUsers = (HttpContext.Current.Request.QueryString["ShowUsers"] != null ? (Convert.ToInt32(HttpContext.Current.Request.QueryString["ShowUsers"]) == 1) : Convert.ToBoolean(HttpContext.Current.Session["ShowUsers"]));
			HttpContext.Current.Session["ShowUsers"] = showUsers;

			projectID = (HttpContext.Current.Request.QueryString["ProjectID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["ProjectID"]) : 0);
			projectRoundID = (HttpContext.Current.Request.QueryString["ProjectRoundID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["ProjectRoundID"]) : 0);
			projectRoundUnitID = (HttpContext.Current.Request.QueryString["ProjectRoundUnitID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["ProjectRoundUnitID"]) : 0);
			projectRoundUserID = (HttpContext.Current.Request.QueryString["ProjectRoundUserID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["ProjectRoundUserID"]) : 0);

			Save.Click += new EventHandler(Save_Click);
			AddRound.Click += new EventHandler(AddRound_Click);
			SaveRound.Click += new EventHandler(SaveRound_Click);
			CancelRound.Click += new EventHandler(CancelRound_Click);

			SqlDataReader rs;

			if(HttpContext.Current.Request.QueryString["ExportSurveyID"] != null)
			{
				exportSpssDataOneNew = true;
			}
			if(HttpContext.Current.Request.QueryString["MovePRQO"] != null)
			{
				int SO = 0;
				rs = Db.sqlRecordSet("SELECT SortOrder FROM ProjectRoundQO WHERE ProjectRoundQOID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["MovePRQO"]));
				if(rs.Read())
				{
					SO = rs.GetInt32(0);
				}
				rs.Close();
				int PRQOID2 = 0, SO2 = 0;
				rs = Db.sqlRecordSet("SELECT TOP 1 ProjectRoundQOID, SortOrder FROM ProjectRoundQO WHERE ProjectRoundID = " + projectRoundID + " AND SortOrder < " + SO + " ORDER BY SortOrder DESC");
				if(rs.Read())
				{
					PRQOID2 = rs.GetInt32(0);
					SO2 = rs.GetInt32(1);
				}
				rs.Close();
				if(SO != 0 && PRQOID2 != 0 && SO2 != 0)
				{
					Db.execute("UPDATE ProjectRoundQO SET SortOrder = " + SO2 + " WHERE ProjectRoundQOID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["MovePRQO"]));
					Db.execute("UPDATE ProjectRoundQO SET SortOrder = " + SO + " WHERE ProjectRoundQOID = " + PRQOID2);
				}
				HttpContext.Current.Response.Redirect("projectSetup.aspx?ProjectRoundID=" + projectRoundID + "&ProjectID=" + projectID, true);
			}

			LogoImg.Text = "";
			if(System.IO.File.Exists(Server.MapPath("..\\img\\project\\logo" + projectID + ".gif")))
			{
				LogoImg.Text = "<img src=\"../img/project/logo" + projectID + ".gif?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><br/>[<A href=\"projectSetup.aspx?ProjectID=" + projectID + "&DeleteLogo=" + projectID + "\">delete</A>]";
			}

			RoundTextElements.Controls.Add(new LiteralControl("<BUTTON ONCLICK=\"void(window.open('projectRoundText.aspx?ProjectRoundID=" + projectRoundID + "','projectRoundText" + projectRoundID + "','width=750,height=660,scrollbars=1,resizable=1'));\">Text Elements</BUTTON>"));
			SendAll.Controls.Add(new LiteralControl("<BUTTON ONCLICK=\"location.href='projectSetup.aspx?ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "&SendRound=" + projectRoundID + "';return false;\">Send All</BUTTON>"));
			if(!IsPostBack)
			{
				rs = Db.sqlRecordSet("SELECT SurveyID, Internal FROM Survey WHERE SurveyID NOT IN (SELECT SurveyID FROM ProjectSurvey WHERE ProjectID = " + projectID + ")");
				while(rs.Read())
				{
					SurveyID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				rs.Close();
			}

			if(projectID != 0)
			{
				Rounds.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"4\">&nbsp;</td></tr>"));
				Rounds.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"4\" BGCOLOR=\"#CCCCCC\"><IMG SRC=\"../img/null.gif\" WIDTH=\"1\" HEIGHT=\"1\"></td></tr>"));
				Rounds.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"4\">&nbsp;</td></tr>"));
				Rounds.Controls.Add(new LiteralControl("<tr><td colspan=\"4\"><u>Project survey rounds</u></td></tr>"));
				Rounds.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"4\">&nbsp;</td></tr>"));
				Save.Text = "Update";

				if(!IsPostBack)
				{
					rs = Db.sqlRecordSet("SELECT Internal, Name FROM Project WHERE ProjectID = " + projectID);
					if(rs.Read())
					{
						Internal.Text = rs.GetString(0);
						Name.Text = (rs.IsDBNull(1) ? "" : rs.GetString(1));
					}
					rs.Close();
					rs = Db.sqlRecordSet("SELECT s.SurveyID, s.Internal, LEFT(REPLACE(s.SurveyKey,'{',''),8) AS K FROM Survey s INNER JOIN ProjectSurvey ps ON s.SurveyID = ps.SurveyID WHERE ps.ProjectID = " + projectID);
					if(rs.Read())
					{
						ProjectSurveyText.Text = "Survey(s)&nbsp;";
						do
						{
							ProjectSurvey.Text += "&nbsp;&bull; " + rs.GetString(1) + " (ID: <a href=\"javascript:void(window.open('/submit.aspx?SID=" + rs.GetInt32(0) + "&LID=1','',''));\">" + rs.GetInt32(0) + "</a>)<br/>";
						}
						while(rs.Read());
					}
					rs.Close();
				}

				rs = Db.sqlRecordSet("SELECT ProjectRoundID, Internal FROM ProjectRound WHERE ProjectID = " + projectID);
				if(rs.Read())
				{
					Rounds.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"4\"><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">"));
					do
					{
						Rounds.Controls.Add(new LiteralControl("<TR><TD>&nbsp;&bull;&nbsp;"));
						if(rs.GetInt32(0) == projectRoundID)
						{
							Rounds.Controls.Add(new LiteralControl("<B>"));
						}
						Rounds.Controls.Add(new LiteralControl(rs.GetString(1)));
						if(rs.GetInt32(0) == projectRoundID)
						{
							Rounds.Controls.Add(new LiteralControl("</B>"));
						}
						Rounds.Controls.Add(new LiteralControl("&nbsp;&nbsp;</TD><TD>"));
						if(rs.GetInt32(0) == projectRoundID)
						{
							Rounds.Controls.Add(new LiteralControl("<B>&lt;--</B>"));
						}
						else
						{
							Rounds.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectID=" + projectID + "&ProjectRoundID=" + rs.GetInt32(0) + "';return false;\">Edit</button>"));
						}
						Rounds.Controls.Add(new LiteralControl("</td></tr>"));
					}
					while(rs.Read());
					Rounds.Controls.Add(new LiteralControl("</TABLE></TD></TR><TR><TD COLSPAN=\"4\">&nbsp;</td></tr>"));
				}
				rs.Close();

				if(HttpContext.Current.Request.QueryString["AddRound"] != null || projectRoundID != 0)
				{
					AddRound.Visible = false;

					Rounds.Controls.Add(new LiteralControl("<TR><TD>Internal&nbsp;</TD><TD>"));
					TextBox RoundInternal = new TextBox();
					RoundInternal.ID = "RoundInternal";
					RoundInternal.Width = Unit.Pixel(300);
					Rounds.Controls.Add(RoundInternal);
					Rounds.Controls.Add(new LiteralControl("</TD></TR>"));
					
					Rounds.Controls.Add(new LiteralControl("<TR><TD>Default&nbsp;</TD><TD>"));
					RadioButtonList RoundLangID = new RadioButtonList();
					RoundLangID.ID = "RoundLangID";
					RoundLangID.RepeatDirection = RepeatDirection.Horizontal;
					RoundLangID.RepeatLayout = RepeatLayout.Table;
					RoundLangID.CellSpacing = 0;
					RoundLangID.CellPadding = 0;
					Rounds.Controls.Add(RoundLangID);
					Rounds.Controls.Add(new LiteralControl("</TD></TR>"));
					
					Rounds.Controls.Add(new LiteralControl("<TR><TD>Survey&nbsp;</TD><TD>"));
					DropDownList RoundSurvey = new DropDownList();
					RoundSurvey.ID = "RoundSurvey";
					RoundSurvey.Width = Unit.Pixel(300);
					Rounds.Controls.Add(RoundSurvey);
					Rounds.Controls.Add(new LiteralControl("</TD></TR>"));
					
					Rounds.Controls.Add(new LiteralControl("<TR><TD>Extended survey&nbsp;</TD><TD>"));
					DropDownList RoundExtendedSurvey = new DropDownList();
					RoundExtendedSurvey.ID = "RoundExtendedSurvey";
					RoundExtendedSurvey.Width = Unit.Pixel(300);
					Rounds.Controls.Add(RoundExtendedSurvey);
					Rounds.Controls.Add(new LiteralControl("</TD></TR>"));

					Rounds.Controls.Add(new LiteralControl("<TR><TD>Logo&nbsp;</TD><TD>"));
					System.Web.UI.HtmlControls.HtmlInputFile RoundLogo = new HtmlInputFile();
					RoundLogo.ID = "RoundLogo";
					RoundLogo.Attributes["style"] += "width:300px;";
					Rounds.Controls.Add(RoundLogo);
					Rounds.Controls.Add(new LiteralControl("</TD></TR>"));

					Rounds.Controls.Add(new LiteralControl("<TR><TD>Layout&nbsp;</TD><TD>"));
					RadioButtonList Layout = new RadioButtonList();
					Layout.ID = "Layout";
					Layout.RepeatDirection = RepeatDirection.Horizontal;
					Layout.RepeatLayout = RepeatLayout.Table;
					Layout.CellSpacing = 0;
					Layout.CellPadding = 0;
					Rounds.Controls.Add(Layout);
					Rounds.Controls.Add(new LiteralControl("</TD></TR>"));
					
					Rounds.Controls.Add(new LiteralControl("<TR><TD>Transparency&nbsp;</TD><TD>"));
					RadioButtonList RoundTransparencyLevel = new RadioButtonList();
					RoundTransparencyLevel.ID = "RoundTransparencyLevel";
					RoundTransparencyLevel.RepeatDirection = RepeatDirection.Horizontal;
					RoundTransparencyLevel.RepeatLayout = RepeatLayout.Table;
					RoundTransparencyLevel.CellSpacing = 0;
					RoundTransparencyLevel.CellPadding = 0;
					Rounds.Controls.Add(RoundTransparencyLevel);
					Rounds.Controls.Add(new LiteralControl("</TD></TR>"));

					Rounds.Controls.Add(new LiteralControl("<TR><TD>Measure&nbsp;</TD><TD>"));
					RadioButtonList RepeatedEntry = new RadioButtonList();
					RepeatedEntry.ID = "RepeatedEntry";
					RepeatedEntry.RepeatDirection = RepeatDirection.Horizontal;
					RepeatedEntry.RepeatLayout = RepeatLayout.Table;
					RepeatedEntry.CellSpacing = 0;
					RepeatedEntry.CellPadding = 0;
					Rounds.Controls.Add(RepeatedEntry);
					Rounds.Controls.Add(new LiteralControl("</TD></TR>"));

					Rounds.Controls.Add(new LiteralControl("<TR><TD>Use login&nbsp;</TD><TD>"));
					RadioButtonList Login = new RadioButtonList();
					Login.ID = "Login";
					Login.RepeatDirection = RepeatDirection.Horizontal;
					Login.RepeatLayout = RepeatLayout.Table;
					Login.CellSpacing = 0;
					Login.CellPadding = 0;
					Rounds.Controls.Add(Login);
					Rounds.Controls.Add(new LiteralControl("</TD></TR>"));

					Rounds.Controls.Add(new LiteralControl("<TR><TD>Start round at&nbsp;</TD><TD>"));
					TextBox StartDate = new TextBox();
					StartDate.ID = "StartDate";
					StartDate.Width = Unit.Pixel(110);
					Rounds.Controls.Add(StartDate);
					Rounds.Controls.Add(new LiteralControl("&nbsp;&nbsp;Close round at&nbsp;"));
					TextBox EndDate = new TextBox();
					EndDate.ID = "EndDate";
					EndDate.Width = Unit.Pixel(110);
					Rounds.Controls.Add(EndDate);
					Rounds.Controls.Add(new LiteralControl("</TD></TR>"));

					Rounds.Controls.Add(new LiteralControl("<TR><TD>From email&nbsp;</TD><TD>"));
					TextBox EmailFromAddress = new TextBox();
					EmailFromAddress.ID = "EmailFromAddress";
					EmailFromAddress.Width = Unit.Pixel(120);
					Rounds.Controls.Add(EmailFromAddress);
					Rounds.Controls.Add(new LiteralControl("&nbsp;&nbsp;Minimum reminder interval&nbsp;"));
					TextBox MinRemInt = new TextBox();
					MinRemInt.ID = "MinRemInt";
					MinRemInt.Width = Unit.Pixel(20);
					Rounds.Controls.Add(MinRemInt);
					Rounds.Controls.Add(new LiteralControl("days</TD></TR>"));

					if(projectRoundID != 0)
					{
						AddPRQO.Visible = true;
						AddPRQO.Text = "Add auto-question";
						AddPRQO.Click += new EventHandler(AddPRQO_Click);
						rs = Db.sqlRecordSet("SELECT ProjectRoundQOID, QuestionID, OptionID FROM ProjectRoundQO WHERE ProjectRoundID = " + projectRoundID + " ORDER BY SortOrder");
						if(rs.Read())
						{
							int prqoCX = 0;
							Rounds.Controls.Add(new LiteralControl("<TR><TD valign=\"top\">Auto-questions&nbsp;</TD><TD><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">"));
							do
							{
								string temp = "";
								Rounds.Controls.Add(new LiteralControl("<TR><TD>"));
								DropDownList PRQO = new DropDownList();
								PRQO.ID = "PRQO" + rs.GetInt32(0);
								PRQO.Width = Unit.Pixel(300);
								PRQO.Items.Add(new ListItem("< select >","NULL:NULL"));
								SqlDataReader rs2 = Db.sqlRecordSet("SELECT " +
									"DISTINCT " +
									"q.QuestionID, " +
									"o.OptionID, " +
									"q.Internal, " +
									"o.Internal " +
									"FROM ProjectRound pr " +
									"INNER JOIN ProjectSurvey ps ON pr.ProjectID = ps.ProjectID " +
									"INNER JOIN SurveyQuestion sq ON ps.SurveyID = sq.SurveyID " +
									"INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " +
									"INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " +
									"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
									"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
									"WHERE pr.ProjectRoundID = " + projectRoundID);
								while(rs2.Read())
								{
									PRQO.Items.Add(new ListItem(rs2.GetString(2) + ", " + rs2.GetString(3),rs2.GetInt32(0) + ":" + rs2.GetInt32(1)));
								}
								rs2.Close();
								if(!rs.IsDBNull(1) && !rs.IsDBNull(2))
								{
									if(!IsPostBack)
									{
										PRQO.SelectedValue = rs.GetInt32(1).ToString() + ":" + rs.GetInt32(2).ToString();
									}
									rs2 = Db.sqlRecordSet("SELECT ocs.ExportValue, oc.Internal, oc.OptionComponentID FROM OptionComponents ocs INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID WHERE ocs.OptionID = " + rs.GetInt32(2) + " ORDER BY ocs.SortOrder");
									while(rs2.Read())
									{
										temp += (temp != "" ? "\n" : "") + rs2.GetInt32(0) + " (" + rs2.GetInt32(2) + ") " + rs2.GetString(1);
									}
									rs2.Close();
								}
								Rounds.Controls.Add(PRQO);
								Rounds.Controls.Add(new LiteralControl("</TD><TD>" + (prqoCX > 0 ? "<A HREF=\"projectSetup.aspx?MovePRQO=" + rs.GetInt32(0) + "&ProjectRoundID=" + projectRoundID + "&ProjectID=" + projectID + "\"><img src=\"../img/UpToolSmall.gif\" border=\"0\"></A>" : "") + "</TD><TD>&nbsp;<SPAN TITLE=\"" + temp + "\">?</SPAN></TD></TR>"));
								prqoCX++;
							}
							while(rs.Read());
							Rounds.Controls.Add(new LiteralControl("</TABLE></TD></TR>"));
						}
						rs.Close();
					}

					if(!IsPostBack)
					{
						StartDate.Text = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") + " 06:00";
						EndDate.Text = DateTime.Now.AddDays(21).ToString("yyyy-MM-dd") + " 18:00";
						MinRemInt.Text = "7";

						RoundTransparencyLevel.Items.Add(new ListItem("Open","0"));
						RoundTransparencyLevel.Items.Add(new ListItem("Confidential","2"));
						RoundTransparencyLevel.Items.Add(new ListItem("Anonymous","4"));

						Layout.Items.Add(new ListItem("Nr 1","0"));
						Layout.SelectedValue = "0";

						RepeatedEntry.Items.Add(new ListItem("Once","0"));
						RepeatedEntry.Items.Add(new ListItem("Repeated","1"));

						Login.Items.Add(new ListItem("No","0"));
						Login.Items.Add(new ListItem("Yes","1"));

						RoundExtendedSurvey.Items.Add(new ListItem("< none >","NULL"));

						rs = Db.sqlRecordSet("SELECT s.SurveyID, s.Internal FROM ProjectSurvey ps INNER JOIN Survey s ON ps.SurveyID = s.SurveyID WHERE ProjectID = " + projectID);
						while(rs.Read())
						{
							RoundSurvey.Items.Add(new ListItem(rs.GetString(1) + " (ID: " + rs.GetInt32(0) + ")",rs.GetInt32(0).ToString()));
							RoundExtendedSurvey.Items.Add(new ListItem(rs.GetString(1) + " (ID: " + rs.GetInt32(0) + ")",rs.GetInt32(0).ToString()));
						}
						rs.Close();

						rs = Db.sqlRecordSet("SELECT LangID FROM Lang");
						while(rs.Read())
						{
							RoundLangID.Items.Add(new ListItem("<img SRC=\"../img/lang/" + rs.GetInt32(0) + ".gif\">",rs.GetInt32(0).ToString()));
						}
						rs.Close();

						EmailFromAddress.Text = appEmail;
					}
//					Page.RegisterStartupScript("disableRepeatedEntry","<SCRIPT LANGUAGE=\"JavaScript\">document.getElementById('RepeatedEntry_1').disabled=true;</SCRIPT>");

					if(projectRoundID != 0)
					{
						if(!IsPostBack)
						{
							rs = Db.sqlRecordSet("SELECT " +
								"Internal, " +				// 0
								"TransparencyLevel, " +
								"SurveyID, " +
								"LangID, " +
								"RepeatedEntry, " +
								"EmailFromAddress, " +		// 5
								"Started, " +
								"Closed, " +
								"ReminderInterval, " +
								"Layout, " +
								"UseCode, " +				// 10
								"ExtendedSurveyID " +		
								"FROM ProjectRound " +
								"WHERE ProjectRoundID = " + projectRoundID);
							if(rs.Read())
							{
								RoundInternal.Text = rs.GetString(0);
								RoundTransparencyLevel.SelectedValue = rs.GetInt32(1).ToString();
								RoundSurvey.SelectedValue = rs.GetInt32(2).ToString();
								RoundExtendedSurvey.SelectedValue = (rs.IsDBNull(11) ? "NULL" : rs.GetInt32(11).ToString());
								RoundLangID.SelectedValue = rs.GetInt32(3).ToString();
								RepeatedEntry.SelectedValue = rs.GetInt32(4).ToString();
								if(!rs.IsDBNull(5))
								{
									EmailFromAddress.Text = rs.GetString(5);
								}
								StartDate.Text = "";
								if(!rs.IsDBNull(6))
								{
									StartDate.Text = rs.GetDateTime(6).ToString("yyyy-MM-dd HH:mm");
								}
								EndDate.Text = "";
								if(!rs.IsDBNull(7))
								{
									EndDate.Text = rs.GetDateTime(7).ToString("yyyy-MM-dd HH:mm");
								}
								MinRemInt.Text = "7";
								if(!rs.IsDBNull(8))
								{
									MinRemInt.Text = rs.GetInt32(8).ToString();
								}
								if(!rs.IsDBNull(9))
								{
									Layout.SelectedValue = rs.GetInt32(9).ToString();
								}
								Login.SelectedValue = (rs.IsDBNull(10) ? "0" : rs.GetInt32(10).ToString());
							}
							rs.Close();
						}
						SaveRound.Text = "Update";

						SendAll.Visible = false;
						if(StartDate.Text != "")
						{
							try
							{
								if(Convert.ToDateTime(StartDate.Text) < DateTime.Now && (EndDate.Text == "" || Convert.ToDateTime(EndDate.Text) > DateTime.Now))
								{
									SendAll.Visible = true;
								}
							}
							catch(Exception){}
						}
				
						Units.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"4\">&nbsp;</td></tr>"));
						Units.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"4\" BGCOLOR=\"#CCCCCC\"><IMG SRC=\"../img/null.gif\" WIDTH=\"1\" HEIGHT=\"1\"></td></tr>"));
						Units.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"4\">&nbsp;</td></tr>"));
						
						rs = Db.sqlRecordSet("SELECT " +
							"u.ProjectRoundUnitID, " +								// 0
							"u.Unit, " +											// 1
							"dbo.cf_unitDepth(u.ProjectRoundUnitID), " +			// 2
							"dbo.cf_unitSurvey(u.ProjectRoundUnitID), " +			// 3
							"dbo.cf_unitSurveyID(u.ProjectRoundUnitID), " +			// 4
							"dbo.cf_unitSurveyKey(u.ProjectRoundUnitID), " +		// 5
							"dbo.cf_unitLangID(u.ProjectRoundUnitID), " +			// 6
							"dbo.cf_unitLang(u.ProjectRoundUnitID), " +				// 7
							"u.ID, " +												// 8
							"u.UserCount, " +										// 9
							"(SELECT COUNT(*) FROM ProjectRoundUser q WHERE q.ProjectRoundUnitID = u.ProjectRoundUnitID), " +	// 10
							"(SELECT COUNT(*) FROM ProjectRoundUser q WHERE q.LastSent IS NOT NULL AND q.ProjectRoundUnitID = u.ProjectRoundUnitID), " +	// 11
							"u.UnitKey " +											// 12
							"FROM ProjectRoundUnit u " +
							"WHERE u.ProjectRoundID = " + projectRoundID + " " +
							"ORDER BY u.SortString");

						if(rs.Read())
						{
							Units.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"4\"><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">"));
							Units.Controls.Add(new LiteralControl("<TR>"));
							Units.Controls.Add(new LiteralControl("<td><u>Unit</u>&nbsp;</td>"));
							Units.Controls.Add(new LiteralControl("<td><u>ID</u>&nbsp;</td>"));
							Units.Controls.Add(new LiteralControl("<td><u>Users</u>&nbsp;</td>"));
							Units.Controls.Add(new LiteralControl("<td><u>Sent</u>&nbsp;</td>"));
							Units.Controls.Add(new LiteralControl("<td><u>Survey</u>&nbsp;</td>"));
							Units.Controls.Add(new LiteralControl("<td><u>Lang</u>&nbsp;</td>"));
							Units.Controls.Add(new LiteralControl("<td><u>Action</u></td>"));
							Units.Controls.Add(new LiteralControl("<td><u>Add...</u></td>"));
							Units.Controls.Add(new LiteralControl("</tr>"));
							do
							{
								Units.Controls.Add(new LiteralControl("<TR"));
								if(rs.GetInt32(0) == projectRoundUnitID)
								{
									Units.Controls.Add(new LiteralControl(" BGCOLOR=\"#EECCCC\""));
								}
								else
								{
									switch(rs.GetInt32(2))
									{
										case 2:	Units.Controls.Add(new LiteralControl(" BGCOLOR=\"#EEEEEE\"")); break;
										case 3:	Units.Controls.Add(new LiteralControl(" BGCOLOR=\"#DDDDDD\"")); break;
										case 4:	Units.Controls.Add(new LiteralControl(" BGCOLOR=\"#CCCCCC\"")); break;
										case 5:	Units.Controls.Add(new LiteralControl(" BGCOLOR=\"#BBBBBB\"")); break;
									}
								}
								Units.Controls.Add(new LiteralControl("><TD>"));
								for(int i = 1; i < rs.GetInt32(2); i++)
								{
									Units.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;"));
								}
								Units.Controls.Add(new LiteralControl("<A HREF=\"JavaScript:void(window.open('../controlPanel.aspx?PRUID=" + rs.GetInt32(0) + "','_blank','scrollbars=1,resizable=1,width=820,height=500'));\">»&nbsp;"));
								if(rs.GetInt32(0) == projectRoundUnitID)
								{
									Units.Controls.Add(new LiteralControl("<B>"));
								}
								Units.Controls.Add(new LiteralControl(rs.GetString(1)));
								if(rs.GetInt32(0) == projectRoundUnitID)
								{
									Units.Controls.Add(new LiteralControl("</B>"));
								}
								Units.Controls.Add(new LiteralControl("</A>&nbsp;</TD><TD STYLE=\"color:#999999;\">"));
								Units.Controls.Add(new LiteralControl(rs.GetString(8)));
								Units.Controls.Add(new LiteralControl("&nbsp;</TD><TD>"));
								int maxCount = Math.Max(rs.GetInt32(10),rs.GetInt32(9));
								Units.Controls.Add(new LiteralControl(rs.GetInt32(10).ToString() + (maxCount == 0 ? "" : (maxCount == rs.GetInt32(10) ? "" : "/" + (maxCount.ToString())))));
								Units.Controls.Add(new LiteralControl("&nbsp;</TD><TD>"));
								Units.Controls.Add(new LiteralControl((rs.GetInt32(10) == 0 ? "" : ((double)rs.GetInt32(11)/(double)rs.GetInt32(10)*100d).ToString() + "%&nbsp;")));
								Units.Controls.Add(new LiteralControl("&nbsp;</TD><TD>"));
								Units.Controls.Add(new LiteralControl("<A HREF=\"JavaScript:void(window.open('../submit.aspx?K=U" + rs.GetGuid(12).ToString().Substring(0,8) + rs.GetInt32(0).ToString() + "','',''));\">" + rs.GetString(3) + "</A>"));
								Units.Controls.Add(new LiteralControl("&nbsp;</TD><TD>"));
								Units.Controls.Add(new LiteralControl(rs.GetString(7)));
								Units.Controls.Add(new LiteralControl("&nbsp;</TD><TD>"));
								Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "&ProjectRoundUnitID=" + rs.GetInt32(0) + "';return false;\">Edit</button>"));
								if(StartDate.Text != "")
								{
									try
									{
										if(Convert.ToDateTime(StartDate.Text) < DateTime.Now && (EndDate.Text == "" || Convert.ToDateTime(EndDate.Text) > DateTime.Now))
										{
											Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "&SendUnit=" + rs.GetInt32(0) + "';return false;\">Send</button>"));
										}
									}
									catch(Exception){}
								}
								Units.Controls.Add(new LiteralControl("&nbsp;</TD><TD>"));
								Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "&AddUnit=" + rs.GetInt32(0) + "';return false;\">Subunit</button>"));
								Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "&AddUser=" + rs.GetInt32(0) + "';return false;\">User</button>"));
								Units.Controls.Add(new LiteralControl("</TD></TR>"));
								if(showUsers || rs.GetInt32(0) == projectRoundUnitID)
								{
									SqlDataReader rs2 = Db.sqlRecordSet("SELECT ProjectRoundUserID, Email FROM ProjectRoundUser WHERE ProjectRoundUnitID = " + rs.GetInt32(0));
									if(rs2.Read())
									{
										Units.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"6\" BGCOLOR=\"#DDDDDD\"><IMG SRC=\"../img/null.gif\" width=\"1\" height=\"1\"></TD></TR>"));
										do
										{
											Units.Controls.Add(new LiteralControl("<TR>"));
											Units.Controls.Add(new LiteralControl("<td>&nbsp;</td>"));
											Units.Controls.Add(new LiteralControl("<td colspan=\"5\">" + rs2.GetString(1) + "&nbsp;</td>"));
											Units.Controls.Add(new LiteralControl("<td><button onclick=\"location.href='projectSetup.aspx?ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "&ProjectRoundUserID=" + rs2.GetInt32(0) + "';return false;\">Edit</button>"));
											if(StartDate.Text != "")
											{
												try
												{
													if(Convert.ToDateTime(StartDate.Text) < DateTime.Now && (EndDate.Text == "" || Convert.ToDateTime(EndDate.Text) > DateTime.Now))
													{
														Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "&SendUser=" + rs2.GetInt32(0) + "';return false;\">Send</button>"));
													}
												}
												catch(Exception){}
											}
											Units.Controls.Add(new LiteralControl("</td>"));
											Units.Controls.Add(new LiteralControl("</tr>"));
										}
										while(rs2.Read());
										Units.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"6\" BGCOLOR=\"#999999\"><IMG SRC=\"../img/null.gif\" width=\"1\" height=\"1\"></TD></TR>"));
									}
									rs2.Close();
								}
							}
							while(rs.Read());

							if(showUsers)
							{
								Units.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"6\"><IMG SRC=\"../img/null.gif\" width=\"1\" height=\"10\"></TD></TR>"));
								Units.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"6\"><u>Unknown</u></TD></TR>"));

								SqlDataReader rs2 = Db.sqlRecordSet("SELECT ProjectRoundUserID, Email FROM ProjectRoundUser WHERE ProjectRoundID = " + projectRoundID + " AND ProjectRoundUnitID IS NULL");
								if(rs2.Read())
								{
									do
									{
										Units.Controls.Add(new LiteralControl("<TR>"));
										Units.Controls.Add(new LiteralControl("<td>&nbsp;</td>"));
										Units.Controls.Add(new LiteralControl("<td colspan=\"5\">" + rs2.GetString(1) + "&nbsp;</td>"));
										Units.Controls.Add(new LiteralControl("<td><button onclick=\"location.href='projectSetup.aspx?ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "&ProjectRoundUserID=" + rs2.GetInt32(0) + "';return false;\">Edit</button></td>"));
										Units.Controls.Add(new LiteralControl("</tr>"));
									}
									while(rs2.Read());
								}
								rs2.Close();
							}
							
							Units.Controls.Add(new LiteralControl("</TABLE><br/>* inherited setting</TD></TR><TR><TD COLSPAN=\"4\">&nbsp;</td></tr>"));
						}
						rs.Close();

						if(HttpContext.Current.Request.QueryString["AddUser"] != null || projectRoundUserID != 0)
						{
							Page.RegisterStartupScript("goto","<script language=\"JavaScript\">window.location.href=\"#user_block\";</script>");

							#region Add/Change User Dialog
							Units.Controls.Add(new LiteralControl("<tr>"));
							Units.Controls.Add(new LiteralControl("<td>Unit&nbsp;<A NAME=\"user_block\" HREF=\"#\"></A></td>"));
							Units.Controls.Add(new LiteralControl("<td colspan=\"3\">"));
							DropDownList UserUnit = new DropDownList();
							UserUnit.ID = "ProjectRoundUnitID";
							UserUnit.Width = Unit.Pixel(300);
							UserUnit.Items.Add(new ListItem("< none >","0"));
							rs = Db.sqlRecordSet("SELECT ProjectRoundUnitID, dbo.cf_ProjectUnitTree(ProjectRoundUnitID,' » ') AS Unit FROM ProjectRoundUnit WHERE ProjectRoundID = " + projectRoundID + " ORDER BY SortString");
							while(rs.Read())
							{
								UserUnit.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
							}
							rs.Close();
							Units.Controls.Add(UserUnit);
							Units.Controls.Add(new LiteralControl("</td>"));
							Units.Controls.Add(new LiteralControl("</tr>"));

							Units.Controls.Add(new LiteralControl("<TR>"));
							Units.Controls.Add(new LiteralControl("<TD>Email&nbsp;</TD>"));
							Units.Controls.Add(new LiteralControl("<TD colspan=\"3\">"));
							TextBox Email = new TextBox();
							Email.ID = "Email";
							Email.Width = Unit.Pixel(300);
							Units.Controls.Add(Email);
							Units.Controls.Add(new LiteralControl("</td>"));
							Units.Controls.Add(new LiteralControl("</tr>"));

							Units.Controls.Add(new LiteralControl("<tr>"));
							Units.Controls.Add(new LiteralControl("<td colspan=\"4\">"));
							
							Button SaveUser = new Button();
							SaveUser.ID = "SaveUser";
							SaveUser.Text = (projectRoundUserID != 0 ? "Update" : "Save");
							SaveUser.Click += new EventHandler(SaveUser_Click);
							
							Units.Controls.Add(SaveUser);
							Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "';return false;\">Cancel</button>"));
							Units.Controls.Add(new LiteralControl("</td>"));
							Units.Controls.Add(new LiteralControl("</tr>"));

							if(!IsPostBack)
							{
								if(HttpContext.Current.Request.QueryString["AddUser"] != null)
								{
									UserUnit.SelectedValue = HttpContext.Current.Request.QueryString["AddUser"].ToString();
								}
								else
								{
									rs = Db.sqlRecordSet("SELECT ProjectRoundUnitID, Email FROM ProjectRoundUser WHERE ProjectRoundUserID = " + projectRoundUserID);
									if(rs.Read())
									{
										UserUnit.SelectedValue = (rs.IsDBNull(0) ? "0" : rs.GetInt32(0).ToString());
										Email.Text = rs.GetString(1);
									}
									rs.Close();
								}
							}
							#endregion
						}
						else if(HttpContext.Current.Request.QueryString["AddUnit"] != null || projectRoundUnitID != 0)
						{
							Page.RegisterStartupScript("goto","<script language=\"JavaScript\">window.location.href=\"#units_block\";</script>");

							#region Add/Change Unit Dialog
							Units.Controls.Add(new LiteralControl("<tr>"));
							Units.Controls.Add(new LiteralControl("<td>Parent unit&nbsp;<A NAME=\"units_block\" HREF=\"#\"></A></td>"));
							Units.Controls.Add(new LiteralControl("<td colspan=\"3\">"));
							DropDownList ParentUnit = new DropDownList();
							ParentUnit.ID = "ParentProjectRoundUnitID";
							ParentUnit.Width = Unit.Pixel(300);
							ParentUnit.Items.Add(new ListItem("< top level >","0"));
							rs = Db.sqlRecordSet("SELECT ProjectRoundUnitID, dbo.cf_ProjectUnitTree(ProjectRoundUnitID,' » ') AS Unit FROM ProjectRoundUnit WHERE ProjectRoundID = " + projectRoundID + " AND ProjectRoundUnitID <> " + projectRoundUnitID + " ORDER BY SortString");
							while(rs.Read())
							{
								ParentUnit.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
							}
							rs.Close();
							Units.Controls.Add(ParentUnit);
							Units.Controls.Add(new LiteralControl("</td>"));
							Units.Controls.Add(new LiteralControl("</tr>"));

							Units.Controls.Add(new LiteralControl("<TR>"));
							Units.Controls.Add(new LiteralControl("<TD>Unit name&nbsp;</TD>"));
							Units.Controls.Add(new LiteralControl("<TD colspan=\"3\">"));
							TextBox UnitName = new TextBox();
							UnitName.ID = "UnitName";
							UnitName.Width = Unit.Pixel(300);
							Units.Controls.Add(UnitName);
							Units.Controls.Add(new LiteralControl("</td>"));
							Units.Controls.Add(new LiteralControl("</tr>"));

							Units.Controls.Add(new LiteralControl("<TR>"));
							Units.Controls.Add(new LiteralControl("<TD>Unit external ID&nbsp;</TD>"));
							Units.Controls.Add(new LiteralControl("<TD colspan=\"3\">"));
							TextBox UnitExternalID = new TextBox();
							UnitExternalID.ID = "UnitExternalID";
							UnitExternalID.Width = Unit.Pixel(150);
							Units.Controls.Add(UnitExternalID);
							Units.Controls.Add(new LiteralControl("&nbsp;Estimated user count&nbsp;"));
							TextBox UserCount = new TextBox();
							UserCount.ID = "UserCount";
							UserCount.Width = Unit.Pixel(42);
							Units.Controls.Add(UserCount);
							Units.Controls.Add(new LiteralControl("</td>"));
							Units.Controls.Add(new LiteralControl("</tr>"));

							Units.Controls.Add(new LiteralControl("<tr>"));
							Units.Controls.Add(new LiteralControl("<td>Survey&nbsp;</td>"));
							Units.Controls.Add(new LiteralControl("<td colspan=\"3\">"));
							DropDownList UnitSurvey = new DropDownList();
							UnitSurvey.ID = "UnitSurvey";
							UnitSurvey.Width = Unit.Pixel(300);
							UnitSurvey.Items.Add(new ListItem("< parent/default >","0"));
							rs = Db.sqlRecordSet("SELECT ps.SurveyID, s.Internal FROM ProjectSurvey ps INNER JOIN Survey s ON ps.SurveyID = s.SurveyID WHERE ps.ProjectID = " + projectID);
							while(rs.Read())
							{
								UnitSurvey.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
							}
							rs.Close();
							Units.Controls.Add(UnitSurvey);
							Units.Controls.Add(new LiteralControl("</td>"));
							Units.Controls.Add(new LiteralControl("</tr>"));

							Units.Controls.Add(new LiteralControl("<TR><TD>Language&nbsp;</TD><TD>"));
							RadioButtonList UnitLangID = new RadioButtonList();
							UnitLangID.ID = "UnitLangID";
							UnitLangID.RepeatDirection = RepeatDirection.Horizontal;
							UnitLangID.RepeatLayout = RepeatLayout.Table;
							UnitLangID.CellSpacing = 0;
							UnitLangID.CellPadding = 0;
							Units.Controls.Add(UnitLangID);
							Units.Controls.Add(new LiteralControl("</TD></TR>"));
							UnitLangID.Items.Add(new ListItem("parent/default","0"));
							rs = Db.sqlRecordSet("SELECT LangID FROM Lang");
							while(rs.Read())
							{
								UnitLangID.Items.Add(new ListItem("<img SRC=\"../img/lang/" + rs.GetInt32(0) + ".gif\">",rs.GetInt32(0).ToString()));
							}
							rs.Close();

							if(!IsPostBack)
							{
								if(HttpContext.Current.Request.QueryString["AddUnit"] != null)
								{
									ParentUnit.SelectedValue = HttpContext.Current.Request.QueryString["AddUnit"].ToString();
									UnitLangID.SelectedValue = "0";
									UserCount.Text = "0";
								}
								else
								{
									rs = Db.sqlRecordSet("SELECT Unit, ID, ParentProjectRoundUnitID, SurveyID, LangID, UserCount FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + projectRoundUnitID);
									if(rs.Read())
									{
										UnitName.Text = rs.GetString(0);
										UnitExternalID.Text = rs.GetString(1);
										ParentUnit.SelectedValue = (rs.IsDBNull(2) ? "0" : rs.GetInt32(2).ToString());
										UnitSurvey.SelectedValue = rs.GetInt32(3).ToString();
										UnitLangID.SelectedValue = rs.GetInt32(4).ToString();
										UserCount.Text = rs.GetInt32(5).ToString();
									}
									rs.Close();
								}
							}

							Units.Controls.Add(new LiteralControl("<tr>"));
							Units.Controls.Add(new LiteralControl("<td colspan=\"4\">"));
							Button SaveUnit = new Button();
							SaveUnit.ID = "SaveUnit";
							SaveUnit.EnableViewState = false;
							SaveUnit.Text = (projectRoundUnitID != 0 ? "Update" : "Save");
							Units.Controls.Add(SaveUnit);
							SaveUnit.Click += new EventHandler(SaveUnit_Click);
							Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "';return false;\">Cancel</button>"));
							Units.Controls.Add(new LiteralControl("</td>"));
							Units.Controls.Add(new LiteralControl("</tr>"));
							#endregion
						}
						else if(HttpContext.Current.Request.QueryString["ImportUnit"] != null)
						{
							Page.RegisterStartupScript("goto","<script language=\"JavaScript\">window.location.href=\"#importunit_block\";</script>");

							#region Import Unit Block
							Units.Controls.Add(new LiteralControl("<tr>"));
							Units.Controls.Add(new LiteralControl("<td>Parent unit&nbsp;<A NAME=\"importunit_block\" HREF=\"#\"></A></td>"));
							Units.Controls.Add(new LiteralControl("<td colspan=\"3\">"));
							DropDownList ParentUnit = new DropDownList();
							ParentUnit.ID = "ParentProjectRoundUnitID";
							ParentUnit.Width = Unit.Pixel(300);
							ParentUnit.Items.Add(new ListItem("< top level >","0"));
							rs = Db.sqlRecordSet("SELECT ProjectRoundUnitID, dbo.cf_ProjectUnitTree(ProjectRoundUnitID,' » ') AS Unit FROM ProjectRoundUnit WHERE ProjectRoundID = " + projectRoundID + " ORDER BY SortString");
							while(rs.Read())
							{
								ParentUnit.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
							}
							rs.Close();
							Units.Controls.Add(ParentUnit);
							Units.Controls.Add(new LiteralControl("</td>"));
							Units.Controls.Add(new LiteralControl("</tr>"));

							Units.Controls.Add(new LiteralControl("<tr>"));
							Units.Controls.Add(new LiteralControl("<td>File&nbsp;</td>"));
							Units.Controls.Add(new LiteralControl("<td colspan=\"3\">"));
							HtmlInputFile import = new HtmlInputFile();
							import.ID = "import";
							import.Style["width"] = "250px;";
							Units.Controls.Add(import);
							Button upload = new Button();
							upload.ID = "upload";
							upload.Text = "Upload";
							upload.Width = Unit.Pixel(50);
							upload.Click += new EventHandler(upload_Click);
							Units.Controls.Add(upload);
							Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "';return false;\">Cancel</button>"));
							Units.Controls.Add(new LiteralControl("</TD></TR>"));
							#endregion
						}
						else if(HttpContext.Current.Request.QueryString["ImportUser"] != null)
						{
							Page.RegisterStartupScript("goto","<script language=\"JavaScript\">window.location.href=\"#importuser_block\";</script>");

							#region Import User Dialog
							Units.Controls.Add(new LiteralControl("<tr>"));
							Units.Controls.Add(new LiteralControl("<td>Default unit&nbsp;<A NAME=\"importuser_block\" HREF=\"#\"></A></td>"));
							Units.Controls.Add(new LiteralControl("<td colspan=\"3\">"));
							DropDownList DefaultUnit = new DropDownList();
							DefaultUnit.ID = "ProjectRoundUnitID";
							DefaultUnit.Width = Unit.Pixel(300);
							DefaultUnit.Items.Add(new ListItem("< none >","0"));
							rs = Db.sqlRecordSet("SELECT ProjectRoundUnitID, dbo.cf_ProjectUnitTree(ProjectRoundUnitID,' » ') AS Unit FROM ProjectRoundUnit WHERE ProjectRoundID = " + projectRoundID + " ORDER BY SortString");
							while(rs.Read())
							{
								DefaultUnit.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
							}
							rs.Close();
							Units.Controls.Add(DefaultUnit);
							Units.Controls.Add(new LiteralControl("</td>"));
							Units.Controls.Add(new LiteralControl("</tr>"));

							Units.Controls.Add(new LiteralControl("<tr>"));
							Units.Controls.Add(new LiteralControl("<td>File&nbsp;</td>"));
							Units.Controls.Add(new LiteralControl("<td colspan=\"3\">"));
							HtmlInputFile import = new HtmlInputFile();
							import.ID = "import";
							import.Style["width"] = "250px;";
							Units.Controls.Add(import);
							Button uploadUser = new Button();
							uploadUser.ID = "uploadUser";
							uploadUser.Text = "Upload";
							uploadUser.Width = Unit.Pixel(50);
							uploadUser.Click += new EventHandler(uploadUser_Click);
							Units.Controls.Add(uploadUser);
							Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "';return false;\">Cancel</button>"));
							Units.Controls.Add(new LiteralControl("</TD></TR>"));
							#endregion
						}
						else if(HttpContext.Current.Request.QueryString["SendUnit"] != null || HttpContext.Current.Request.QueryString["SendRound"] != null || HttpContext.Current.Request.QueryString["SendUser"] != null)
						{
							Page.RegisterStartupScript("goto","<script language=\"JavaScript\">window.location.href=\"#send_block\";</script>");
							
							#region Send Dialog

							string langIDs = "0";

							Units.Controls.Add(new LiteralControl("<tr><td>Send to&nbsp;<A NAME=\"send_block\" HREF=\"#\"></A></td><td colspan=\"3\">"));
							
							if(HttpContext.Current.Request.QueryString["SendUnit"] != null)
							{
								string sortString = "";
								rs = Db.sqlRecordSet("SELECT dbo.cf_ProjectUnitTree(ProjectRoundUnitID,' &gt; ') AS Unit, SortString FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + HttpContext.Current.Request.QueryString["SendUnit"]);
								if(rs.Read())
								{
									Units.Controls.Add(new LiteralControl("<B>" + rs.GetString(0) + " &gt; *</B>"));
									sortString = rs.GetString(1);
								}
								rs.Close();

								rs = Db.sqlRecordSet("SELECT DISTINCT dbo.cf_unitLangID(ProjectRoundUnitID) AS LangID FROM ProjectRoundUnit WHERE LEFT(SortString," + sortString.Length + ") = '" + sortString + "'");
								while(rs.Read())
								{
									langIDs += "," + rs.GetInt32(0).ToString();
								}
								rs.Close();
							}
							else if(HttpContext.Current.Request.QueryString["SendRound"] != null)
							{
								Units.Controls.Add(new LiteralControl("<B>All</B>"));

								rs = Db.sqlRecordSet("SELECT DISTINCT dbo.cf_unitLangID(ProjectRoundUnitID) AS LangID FROM ProjectRoundUnit WHERE ProjectRoundID = " + projectRoundID);
								while(rs.Read())
								{
									langIDs += "," + rs.GetInt32(0).ToString();
								}
								rs.Close();
							}
							else if(HttpContext.Current.Request.QueryString["SendUser"] != null)
							{
								rs = Db.sqlRecordSet("SELECT dbo.cf_ProjectUnitTree(u.ProjectRoundUnitID,' &gt; ') AS Unit, dbo.cf_unitLangID(u.ProjectRoundUnitID) AS LangID, u.Email FROM ProjectRoundUser u INNER JOIN ProjectRoundUnit r ON u.ProjectRoundID = r.ProjectRoundID WHERE u.ProjectRoundUserID = " + HttpContext.Current.Request.QueryString["SendUser"]);
								if(rs.Read())
								{
									Units.Controls.Add(new LiteralControl("<B>" + rs.GetString(0) + " &gt; " + rs.GetString(2) + "</B>"));
									langIDs += "," + rs.GetInt32(1).ToString();
								}
								rs.Close();
							}

							Units.Controls.Add(new LiteralControl("</td></tr>"));

							Units.Controls.Add(new LiteralControl("<tr><td>Send type</td><td colspan=\"3\">"));
							DropDownList STID = new DropDownList();
							STID.ID = "STID";
							//SendType.RepeatDirection = RepeatDirection.Horizontal;
							//SendType.RepeatLayout = RepeatLayout.Table;
							//SendType.CellSpacing = 0;
							//SendType.CellPadding = 0;
							STID.AutoPostBack = true;
							STID.Items.Add(new ListItem("Invitation","0"));
							STID.Items.Add(new ListItem("Reminder","1"));
							if(!IsPostBack)
							{
								STID.SelectedValue = "0";
							}
							Units.Controls.Add(STID);
							Units.Controls.Add(new LiteralControl("</td></tr>"));
							Units.Controls.Add(new LiteralControl("<tr><td>Sender email&nbsp;</td><td colspan=\"3\">"));
							TextBox SEFA = new TextBox();
							SEFA.ID = "SEFA";
							SEFA.Width = Unit.Pixel(315);
							if(!IsPostBack)
							{
								SEFA.Text = EmailFromAddress.Text;
							}
							Units.Controls.Add(SEFA);
							Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "';return false;\">Cancel</button>"));
							Button SendToMailQueue = new Button();
							SendToMailQueue.ID = "SendToMailQueueNow";
							SendToMailQueue.Text = "Send";
							SendToMailQueue.Attributes["onclick"] += "if(confirm('Are you sure?')){return true;}else{return false;}";
							Units.Controls.Add(SendToMailQueue);
							Units.Controls.Add(new LiteralControl("</td></tr>"));

							STID.SelectedIndexChanged += new EventHandler(SendTypeID_SelectedIndexChanged);
							SendToMailQueue.Click += new EventHandler(Send_Click);

							rs = Db.sqlRecordSet("SELECT " +
								"l.LangID, " +
								"ISNULL(prl.InvitationSubjectJapaneseUnicode,prl.InvitationSubject), " +
								"ISNULL(prl.InvitationBodyJapaneseUnicode,prl.InvitationBody), " +
								"ISNULL(prl.ReminderSubjectJapaneseUnicode,prl.ReminderSubject), " +
								"ISNULL(prl.ReminderBodyJapaneseUnicode,prl.ReminderBody), " +
								"ISNULL(prl.ExtraInvitationSubjectJapaneseUnicode,prl.ExtraInvitationSubject), " +
								"ISNULL(prl.ExtraInvitationBodyJapaneseUnicode,prl.ExtraInvitationBody), " +
								"ISNULL(prl.ExtraReminderSubjectJapaneseUnicode,prl.ExtraReminderSubject), " +
								"ISNULL(prl.ExtraReminderBodyJapaneseUnicode,prl.ExtraReminderBody) " +
								"FROM Lang l " +
								"LEFT OUTER JOIN ProjectRoundLang prl ON l.LangID = prl.LangID AND prl.ProjectRoundID = " + projectRoundID + " " +
								"WHERE l.LangID IN (" + langIDs + ")");
							while(rs.Read())
							{
								TextBox Subject = new TextBox();
								Subject.ID = "Subject" + rs.GetInt32(0);
								Subject.Width = Unit.Pixel(400);

								TextBox Body = new TextBox();
								Body.TextMode = TextBoxMode.MultiLine;
								Body.Rows = 10;
								Body.ID = "Body" + rs.GetInt32(0);
								Body.Width = Unit.Pixel(400);

								TextBox ExtraSubject = new TextBox();
								ExtraSubject.ID = "ExtraSubject" + rs.GetInt32(0);
								ExtraSubject.Width = Unit.Pixel(400);

								TextBox ExtraBody = new TextBox();
								ExtraBody.TextMode = TextBoxMode.MultiLine;
								ExtraBody.Rows = 10;
								ExtraBody.ID = "ExtraBody" + rs.GetInt32(0);
								ExtraBody.Width = Unit.Pixel(400);

								Units.Controls.Add(new LiteralControl("<TR><TD valign=\"top\"><img align=\"right\" SRC=\"../img/lang/" + rs.GetInt32(0) + ".gif\">Subject</td><td>"));
								Units.Controls.Add(Subject);
								Units.Controls.Add(new LiteralControl("</TD></TR><TR><TD valign=\"top\"><img align=\"right\" SRC=\"../img/lang/" + rs.GetInt32(0) + ".gif\">Body</td><td>"));
								Units.Controls.Add(Body);
								Units.Controls.Add(new LiteralControl("</TD></TR><TR><TD valign=\"top\"><img align=\"right\" SRC=\"../img/lang/" + rs.GetInt32(0) + ".gif\">Subject</td><td>"));
								Units.Controls.Add(ExtraSubject);
								Units.Controls.Add(new LiteralControl("</TD></TR><TR><TD valign=\"top\"><img align=\"right\" SRC=\"../img/lang/" + rs.GetInt32(0) + ".gif\">Body</td><td>"));
								Units.Controls.Add(ExtraBody);
								Units.Controls.Add(new LiteralControl("</TD></TR>"));

								HtmlInputHidden sendlang = new HtmlInputHidden();
								sendlang.ID = "SendLang";
								Units.Controls.Add(sendlang);

								if(!IsPostBack)
								{
									updateSendTexts(
										rs.GetInt32(0),
										(rs.IsDBNull(1) ? "" : rs.GetString(1)),(rs.IsDBNull(2) ? "" : rs.GetString(2)),
										(rs.IsDBNull(3) ? "" : rs.GetString(3)),(rs.IsDBNull(4) ? "" : rs.GetString(4)),
										(rs.IsDBNull(5) ? "" : rs.GetString(5)),(rs.IsDBNull(6) ? "" : rs.GetString(6)),
										(rs.IsDBNull(7) ? "" : rs.GetString(7)),(rs.IsDBNull(8) ? "" : rs.GetString(8))
										);
								}
							}
							rs.Close();

							#endregion
						}
						else if(HttpContext.Current.Request.QueryString["ImportData"] != null)
						{
							Page.RegisterStartupScript("goto","<script language=\"JavaScript\">window.location.href=\"#importdata_block\";</script>");
							
							#region Import Data Dialog
							Units.Controls.Add(new LiteralControl("<tr>"));
							Units.Controls.Add(new LiteralControl("<td>Default unit&nbsp;<A NAME=\"importdata_block\" HREF=\"#\"></A></td>"));
							Units.Controls.Add(new LiteralControl("<td colspan=\"3\">"));
							DropDownList DefaultUnit = new DropDownList();
							DefaultUnit.ID = "ProjectRoundUnitID";
							DefaultUnit.Width = Unit.Pixel(300);
							DefaultUnit.Items.Add(new ListItem("< none >","0"));
							rs = Db.sqlRecordSet("SELECT ProjectRoundUnitID, dbo.cf_ProjectUnitTree(ProjectRoundUnitID,' » ') AS Unit FROM ProjectRoundUnit WHERE ProjectRoundID = " + projectRoundID + " ORDER BY SortString");
							while(rs.Read())
							{
								DefaultUnit.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
							}
							rs.Close();
							Units.Controls.Add(DefaultUnit);
							Units.Controls.Add(new LiteralControl("</td>"));
							Units.Controls.Add(new LiteralControl("</tr>"));

							Units.Controls.Add(new LiteralControl("<tr>"));
							Units.Controls.Add(new LiteralControl("<td>File&nbsp;</td>"));
							Units.Controls.Add(new LiteralControl("<td colspan=\"3\">"));
							HtmlInputFile import = new HtmlInputFile();
							import.ID = "import";
							import.Style["width"] = "250px;";
							Units.Controls.Add(import);
							Button UploadData = new Button();
							UploadData.ID = "UploadData";
							UploadData.Text = "Upload";
							UploadData.Width = Unit.Pixel(50);
							UploadData.Click += new EventHandler(UploadData_Click);
							Units.Controls.Add(UploadData);
							Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "';return false;\">Cancel</button>"));
							Units.Controls.Add(new LiteralControl("</TD></TR>"));

							HtmlInputHidden hiddenImport = new HtmlInputHidden();
							hiddenImport.ID = "hiddenImport";
							Units.Controls.Add(hiddenImport);

							PlaceHolder Mapping = new PlaceHolder();
							Mapping.ID = "Mapping";
							Mapping.Visible = false;
							Units.Controls.Add(Mapping);

							Mapping.Controls.Add(new LiteralControl("<TR><TD>UNIT&nbsp;</TD><TD>"));
							DropDownList ddl = new DropDownList();
							ddl.ID = "colUnit";
							ddl.Width = Unit.Pixel(150);
							Mapping.Controls.Add(ddl);
							Mapping.Controls.Add(new LiteralControl("</TD></TR>"));
							if(!IsPostBack)
							{
								ddl.Items.Add(new ListItem("< ignore, use default >","-1"));
							}

							Mapping.Controls.Add(new LiteralControl("<TR><TD>EXTID&nbsp;</TD><TD>"));
							ddl = new DropDownList();
							ddl.ID = "colExtID";
							ddl.Width = Unit.Pixel(150);
							Mapping.Controls.Add(ddl);
							Mapping.Controls.Add(new LiteralControl("</TD></TR>"));
							if(!IsPostBack)
							{
								ddl.Items.Add(new ListItem("< ignore, anon import >","-1"));
							}

							Mapping.Controls.Add(new LiteralControl("<TR><TD>NAME&nbsp;</TD><TD>"));
							ddl = new DropDownList();
							ddl.ID = "colName";
							ddl.Width = Unit.Pixel(150);
							Mapping.Controls.Add(ddl);
							Mapping.Controls.Add(new LiteralControl("</TD></TR>"));
							if(!IsPostBack)
							{
								ddl.Items.Add(new ListItem("< ignore, anon import >","-1"));
							}

							rs = Db.sqlRecordSet("SELECT DISTINCT " +
								"sq.QuestionID, " +		// 0
								"qo.OptionID, " +		// 1
								"o.OptionType, " +		// 2
								"sq.Variablename, " +	// 3
								"sqo.Variablename " +	// 4
								"FROM SurveyQuestion sq " +
								"INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " + 
								"INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " + 
								"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " + 
								"INNER JOIN ProjectRound pr ON pr.SurveyID = sq.SurveyID " +
								"WHERE pr.ProjectRoundID = " + HttpContext.Current.Request.QueryString["ProjectRoundID"] + " " +
								"ORDER BY sq.QuestionID, qo.OptionID");
							while(rs.Read())
							{
								Mapping.Controls.Add(new LiteralControl("<TR><TD><SPAN TITLE=\""));
								SqlDataReader rs2 = Db.sqlRecordSet("SELECT ql.Question, l.Lang FROM QuestionLang ql INNER JOIN Lang l ON ql.LangID = l.LangID WHERE ql.QuestionID = " + rs.GetInt32(0));
								if(rs2.Read())
								{
									Mapping.Controls.Add(new LiteralControl("Translation, Original"));
									do
									{
										if(!rs2.IsDBNull(0) && rs2.GetString(0) != "")
										{
											Mapping.Controls.Add(new LiteralControl("\r\n" + rs2.GetString(1) + ": " + rs2.GetString(0)));
										}
									}
									while(rs2.Read());
								}
								rs2.Close();
								rs2 = Db.sqlRecordSet("SELECT sql.Question, l.Lang FROM SurveyQuestion sq INNER JOIN SurveyQuestionLang sql ON sq.SurveyQuestionID = sql.SurveyQuestionID INNER JOIN Lang l ON sql.LangID = l.LangID INNER JOIN ProjectSurvey ps ON ps.SurveyID = sq.SurveyID WHERE ps.ProjectID = " + HttpContext.Current.Request.QueryString["ProjectID"] + " AND sq.QuestionID = " + rs.GetInt32(0));
								if(rs2.Read())
								{
									Mapping.Controls.Add(new LiteralControl("\r\nTranslation, Variant(s)"));
									do
									{
										if(!rs2.IsDBNull(0) && rs2.GetString(0) != "")
										{
											Mapping.Controls.Add(new LiteralControl("\r\n" + rs2.GetString(1) + ": " + rs2.GetString(0)));
										}
									}
									while(rs2.Read());
								}
								rs2.Close();
								Mapping.Controls.Add(new LiteralControl("\">" + (rs.IsDBNull(3) || rs.GetString(3) == "" ? "Q" + rs.GetInt32(0) + (rs.IsDBNull(4) || rs.GetString(4) == "" ? "O" + rs.GetInt32(1) : "") : rs.GetString(3) + (rs.IsDBNull(4) || rs.GetString(4) == "" ? "" : rs.GetString(4))) + "</SPAN>&nbsp;</TD><TD>"));
								ddl = new DropDownList();
								ddl.ID = "colQ" + rs.GetInt32(0) + "O" + rs.GetInt32(1);
								ddl.Width = Unit.Pixel(150);
								Mapping.Controls.Add(ddl);
								if(rs.GetInt32(2) == 1)
								{
									int cx = 0;
									rs2 = Db.sqlRecordSet("SELECT ocs.OptionComponentID, ocs.ExportValue FROM OptionComponents ocs WHERE ocs.OptionID = " + rs.GetInt32(1) + " ORDER BY ocs.SortOrder");
									while(rs2.Read())
									{
										Mapping.Controls.Add(new LiteralControl("&nbsp;&nbsp;<SPAN TITLE=\"Translation"));
										SqlDataReader rs3 = Db.sqlRecordSet("SELECT ocl.Text, l.Lang FROM OptionComponentLang ocl INNER JOIN Lang l ON ocl.LangID = l.LangID WHERE ocl.OptionComponentID = " + rs2.GetInt32(0));
										while(rs3.Read())
										{
											if(!rs3.IsDBNull(0) && rs3.GetString(0) != "")
											{
												Mapping.Controls.Add(new LiteralControl("\r\n" + rs3.GetString(1) + ": " + rs3.GetString(0)));
											}
										}
										rs3.Close();
										Mapping.Controls.Add(new LiteralControl("\">OC" + (++cx)));

										TextBox ocEV = new TextBox();
										ocEV.ID = "Q" + rs.GetInt32(0) + "O" + rs.GetInt32(1) + "C" + rs2.GetInt32(0);
										ocEV.Width = Unit.Pixel(20);
										Mapping.Controls.Add(ocEV);

										if(!IsPostBack)
										{
											ocEV.Text = (rs2.IsDBNull(1) ? rs2.GetInt32(0) : rs2.GetInt32(1)).ToString();
										}
									}
									rs2.Close();
								}
								Mapping.Controls.Add(new LiteralControl("</TD></TR>"));
								if(!IsPostBack)
								{
									ddl.Items.Add(new ListItem("< ignore >","-1"));
								}
							}
							rs.Close();

							Mapping.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"2\">"));
							Button ExecImport = new Button();
							ExecImport.ID = "ExecImprt";
							ExecImport.Text = "Execute";
							ExecImport.Click += new EventHandler(ExecImport_Click);
							Mapping.Controls.Add(ExecImport);
							Mapping.Controls.Add(new LiteralControl("</TD></TR>"));
							#endregion
						}
						else
						{
							Units.Controls.Add(new LiteralControl("<TR>"));
							Units.Controls.Add(new LiteralControl("<TD COLSPAN=\"4\">"));
							Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectRoundID=" + projectRoundID + "&ProjectID=" + projectID + "&AddUnit=0';return false;\">Add unit</button>"));
							Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectRoundID=" + projectRoundID + "&ProjectID=" + projectID + "&ImportUnit=0';return false;\">Import unit(s)</button>"));
							Button ExportUnit = new Button();
							ExportUnit.ID = "ExportUnit";
							ExportUnit.Text = "Export units";
							ExportUnit.Click += new EventHandler(ExportUnit_Click);
							Units.Controls.Add(ExportUnit);
							Units.Controls.Add(new LiteralControl("<BR/>"));
							Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectRoundID=" + projectRoundID + "&ProjectID=" + projectID + "&ShowUsers=" + (showUsers ? 0 : 1) + "';return false;\">" + (showUsers ? "Hide all users" : "Show all users") + "</button>"));
							Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectRoundID=" + projectRoundID + "&ProjectID=" + projectID + "&AddUser=0';return false;\">Add user</button>"));
							Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectRoundID=" + projectRoundID + "&ProjectID=" + projectID + "&ImportUser=0';return false;\">Import user(s)</button>"));
							Button ExportUser = new Button();
							ExportUser.ID = "ExportUser";
							ExportUser.Text = "Export users";
							ExportUser.Click += new EventHandler(ExportUser_Click);
							Units.Controls.Add(ExportUser);
							Units.Controls.Add(new LiteralControl("<BR/>"));
							Units.Controls.Add(new LiteralControl("<button onclick=\"location.href='projectSetup.aspx?ProjectRoundID=" + projectRoundID + "&ProjectID=" + projectID + "&ImportData=0';return false;\">Import ASCII data</button>"));
							Button ExportData = new Button();
							ExportData.ID = "ExportData";
							ExportData.Text = "Export ASCII data (all)";
							ExportData.Click += new EventHandler(ExportData_Click);
							Units.Controls.Add(ExportData);
							Button ExportDataOne = new Button();
							ExportDataOne.ID = "ExportDataOne";
							ExportDataOne.Text = "(def)";
							ExportDataOne.Click += new EventHandler(ExportDataOne_Click);
							Units.Controls.Add(ExportDataOne);
							Button ExportSpssData = new Button();
							ExportSpssData.ID = "ExportSpssData";
							ExportSpssData.Text = "Export SPSS data (all)";
							ExportSpssData.Click += new EventHandler(ExportSpssData_Click);
							Units.Controls.Add(ExportSpssData);
							Button ExportSpssDataOne = new Button();
							ExportSpssDataOne.ID = "ExportSpssDataOne";
							ExportSpssDataOne.Text = "(def)";
							ExportSpssDataOne.Click += new EventHandler(ExportSpssDataOne_Click);
							Units.Controls.Add(ExportSpssDataOne);

							//Button ExportSpssDataOneNew = new Button();
							//ExportSpssDataOneNew.ID = "ExportSpssDataOneNew";
							//ExportSpssDataOneNew.Text = "(def/new)";
							//ExportSpssDataOneNew.Click += new EventHandler(ExportSpssDataOneNew_Click);
							//Units.Controls.Add(ExportSpssDataOneNew);

							Units.Controls.Add(new LiteralControl("</td>"));
							Units.Controls.Add(new LiteralControl("</tr>"));
							Units.Controls.Add(new LiteralControl("<tr><td colspan=\"4\"><a href=\"projectSetup.aspx?All=1&ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "&ExportSurveyID=" + RoundSurvey.SelectedValue + "&ExportLangID=" + RoundLangID.SelectedValue + "\">Spss export std (user base)</A>" + (RoundExtendedSurvey.SelectedValue != "NULL" ? "&nbsp;&nbsp;<a href=\"projectSetup.aspx?All=1&ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "&ExportSurveyID=" + RoundExtendedSurvey.SelectedValue + "&Extended=1&ExportLangID=" + RoundLangID.SelectedValue + "\">Spss export ext (user base)</A>" : "") + "</td></tr>"));
							Units.Controls.Add(new LiteralControl("<tr><td colspan=\"4\"><a href=\"projectSetup.aspx?All=1&Base=1&ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "&ExportSurveyID=" + RoundSurvey.SelectedValue + "&ExportLangID=" + RoundLangID.SelectedValue + "\">Spss export std (answer base)</A>" + (RoundExtendedSurvey.SelectedValue != "NULL" ? "&nbsp;&nbsp;<a href=\"projectSetup.aspx?All=1&Base=1&ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "&ExportSurveyID=" + RoundExtendedSurvey.SelectedValue + "&Extended=1&ExportLangID=" + RoundLangID.SelectedValue + "\">Spss export ext (answer base)</A>" : "") + "</td></tr>"));
							rs = Db.sqlRecordSet("SELECT DISTINCT s.SurveyID, s.Internal FROM ProjectRoundUnit r INNER JOIN Survey s ON r.SurveyID = s.SurveyID WHERE r.ProjectRoundID = " + projectRoundID);
							while(rs.Read())
							{
								Units.Controls.Add(new LiteralControl("<tr><td colspan=\"4\"><a href=\"projectSetup.aspx?All=1&Base=2&ProjectID=" + projectID + "&ProjectRoundID=" + projectRoundID + "&ExportSurveyID=" + rs.GetInt32(0) + "&ExportLangID=" + RoundLangID.SelectedValue + "\">Spss export \"" + rs.GetString(1) + "\" (users with answers on units with survey set explicitly)</A></td></tr>"));
							}
							rs.Close();
							Units.Controls.Add(new LiteralControl("<tr><td colspan=\"4\"><a href=\"exporttexts.aspx?ProjectRoundID=" + projectRoundID + "\">Export all texts</A></td></tr>"));
						}
					}
					else
					{
						RoundLangID.SelectedIndex = 0;
						RoundTransparencyLevel.SelectedIndex = 0;
						RepeatedEntry.SelectedIndex = 0;
						Login.SelectedIndex = 0;

						RoundTextElements.Visible = false;
					}
				}
				else
				{
					SaveRound.Visible = false;
					RoundTextElements.Visible = false;
					CancelRound.Visible = false;
				}
			}
			else
			{
				AddRound.Visible = false;
				SaveRound.Visible = false;
				RoundTextElements.Visible = false;
				CancelRound.Visible = false;
			}

			Units.Controls.Add(new LiteralControl("<tr><td colspan=\"4\">&nbsp;</td></tr>"));
			Units.Controls.Add(new LiteralControl("<tr><td colspan=\"4\">"));
			Label error = new Label();
			error.EnableViewState = false;
			error.ID = "error";
			error.Style["color"] = "#CC0000";
			Units.Controls.Add(error);
			Units.Controls.Add(new LiteralControl("</td></tr>"));
			Units.Controls.Add(new LiteralControl("<tr><td colspan=\"4\">&nbsp;</td></tr>"));

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

		private void Save_Click(object sender, EventArgs e)
		{
			if(projectID != 0)
			{
				Db.execute("UPDATE Project SET Internal = '" + Internal.Text.Replace("'","") + "', Name = '" + Name.Text.Replace("'","") + "' WHERE ProjectID = " + projectID);
			}
			else
			{
				projectID = Db.getInt32("SET NOCOUNT ON;SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;BEGIN TRAN;INSERT INTO [Project] (Internal,Name) VALUES ('" + Internal.Text.Replace("'","") + "','" + Name.Text.Replace("'","") + "');SELECT ProjectID FROM [Project] WHERE Internal = '" + Internal.Text.Replace("'","") + "' ORDER BY ProjectID DESC;COMMIT;");
			}
			if(SurveyID.SelectedValue != "0")
			{
				Db.execute("INSERT INTO ProjectSurvey (ProjectID,SurveyID) VALUES (" + projectID + "," + SurveyID.SelectedValue + ")");
			}

			if(Logo.PostedFile != null && Logo.PostedFile.ContentLength != 0)
			{
				if(System.IO.File.Exists(Server.MapPath("..\\img\\project\\logo" + projectID + ".gif")))
				{
					System.IO.File.Delete(Server.MapPath("..\\img\\project\\logo" + projectID + ".gif"));
				}
				Logo.PostedFile.SaveAs(Server.MapPath("..\\img\\project\\logo" + projectID + ".gif"));
			}
			
			HttpContext.Current.Response.Redirect("projectSetup.aspx?ProjectID=" + projectID, true);
		}

		private void AddRound_Click(object sender, EventArgs e)
		{
			HttpContext.Current.Response.Redirect("projectSetup.aspx?AddRound=1&ProjectID=" + projectID, true);
		}

		private void SaveRound_Click(object sender, EventArgs e)
		{
			string startDT = "NULL";
			if(((TextBox)Rounds.FindControl("StartDate")).Text != "")
			{
				try
				{
					startDT = "'" + Convert.ToDateTime(((TextBox)Rounds.FindControl("StartDate")).Text).ToString("yyyy-MM-dd HH:mm") + "'";
				}
				catch(Exception){}
			}
			string endDT = "NULL";
			if(((TextBox)Rounds.FindControl("EndDate")).Text != "")
			{
				try
				{
					endDT = "'" + Convert.ToDateTime(((TextBox)Rounds.FindControl("EndDate")).Text).ToString("yyyy-MM-dd HH:mm") + "'";
				}
				catch(Exception){}
			}
			if(projectRoundID != 0)
			{
				SqlDataReader rs = Db.sqlRecordSet("SELECT ProjectRoundQOID FROM ProjectRoundQO WHERE ProjectRoundID = " + projectRoundID);
				while(rs.Read())
				{
					Db.execute("UPDATE ProjectRoundQO SET QuestionID = " + ((DropDownList)Rounds.FindControl("PRQO" + rs.GetInt32(0))).SelectedValue.Split(':')[0] + ", OptionID = " + ((DropDownList)Rounds.FindControl("PRQO" + rs.GetInt32(0))).SelectedValue.Split(':')[1] + " WHERE ProjectRoundQOID = " + rs.GetInt32(0));
				}
				rs.Close();
				Db.execute("UPDATE ProjectRound " +
					"SET UseCode = " + ((RadioButtonList)Rounds.FindControl("Login")).SelectedValue + ", " +
					"Layout = " + ((RadioButtonList)Rounds.FindControl("Layout")).SelectedValue + ", " +
					"ReminderInterval = " + Convert.ToInt32("0" + ((TextBox)Rounds.FindControl("MinRemInt")).Text) + ", " +
					"Started = " + startDT + ", " +
					"Closed = " + endDT + ", " +
					"EmailFromAddress = '" + ((TextBox)Rounds.FindControl("EmailFromAddress")).Text.Replace("'","") + "', " +
					"RepeatedEntry = " + ((RadioButtonList)Rounds.FindControl("RepeatedEntry")).SelectedValue + ", " +
					"LangID = " + ((RadioButtonList)Rounds.FindControl("RoundLangID")).SelectedValue + ", " +
					"SurveyID = " + ((DropDownList)Rounds.FindControl("RoundSurvey")).SelectedValue + ", " +
					"ExtendedSurveyID = " + ((DropDownList)Rounds.FindControl("RoundExtendedSurvey")).SelectedValue + ", " +
					"TransparencyLevel = " + ((RadioButtonList)Rounds.FindControl("RoundTransparencyLevel")).SelectedValue + ", " +
					"ProjectID=" + projectID + ", " +
					"Internal = '" + ((TextBox)Rounds.FindControl("RoundInternal")).Text.Replace("'","") + "' " +
					"WHERE ProjectRoundID = " + projectRoundID);
			}
			else
			{
				projectRoundID = Db.getInt32("SET NOCOUNT ON;" +
					"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;" +
					"BEGIN TRAN;" +
					"INSERT INTO [ProjectRound] (" +
					"Started," +
					"Closed," +
					"EmailFromAddress," +
					"RepeatedEntry," +
					"LangID," +
					"SurveyID," +
					"ExtendedSurveyID," +
					"Internal," +
					"ProjectID," +
					"TransparencyLevel," +
					"Layout," +
					"UseCode" +
					") VALUES (" +
					"" + startDT + "," +
					"" + endDT + "," +
					"'" + ((TextBox)Rounds.FindControl("EmailFromAddress")).Text.Replace("'","") + "'," +
					"" + ((RadioButtonList)Rounds.FindControl("RepeatedEntry")).SelectedValue + "," +
					"" + ((RadioButtonList)Rounds.FindControl("RoundLangID")).SelectedValue + "," +
					"" + ((DropDownList)Rounds.FindControl("RoundSurvey")).SelectedValue + "," +
					"" + ((DropDownList)Rounds.FindControl("RoundExtendedSurvey")).SelectedValue + "," +
					"'" + ((TextBox)Rounds.FindControl("RoundInternal")).Text.Replace("'","") + "'," +
					"" + projectID + "," +
					"" + ((RadioButtonList)Rounds.FindControl("RoundTransparencyLevel")).SelectedValue + "," +
					"" + ((RadioButtonList)Rounds.FindControl("Layout")).SelectedValue + "," +
					"" + ((RadioButtonList)Rounds.FindControl("Login")).SelectedValue + "" +
					");" +
					"SELECT ProjectRoundID " +
					"FROM [ProjectRound] " +
					"WHERE ProjectID=" + projectID + " " +
					"AND Internal = '" + ((TextBox)Rounds.FindControl("RoundInternal")).Text.Replace("'","") + "' " +
					"ORDER BY ProjectRoundID DESC;" +
					"COMMIT;");
			}
			System.Web.UI.HtmlControls.HtmlInputFile RoundLogo = ((System.Web.UI.HtmlControls.HtmlInputFile)Rounds.FindControl("RoundLogo"));
			if(RoundLogo.PostedFile != null && RoundLogo.PostedFile.ContentLength != 0)
			{
				Db.execute("UPDATE ProjectRound SET Logo = 1 WHERE ProjectRoundID = " + projectRoundID);

				if(System.IO.File.Exists(Server.MapPath("..\\img\\project\\logo" + projectID + "_" + projectRoundID + ".gif")))
				{
					System.IO.File.Delete(Server.MapPath("..\\img\\project\\logo" + projectID + "_" + projectRoundID + ".gif"));
				}
				RoundLogo.PostedFile.SaveAs(Server.MapPath("..\\img\\project\\logo" + projectID + "_" + projectRoundID + ".gif"));
			}
			HttpContext.Current.Response.Redirect("projectSetup.aspx?ProjectRoundID=" + projectRoundID + "&ProjectID=" + projectID, true);
		}

		private void SaveUnit_Click(object sender, EventArgs e)
		{
			string parentProjectRoundUnitID = ((DropDownList)Units.FindControl("ParentProjectRoundUnitID")).SelectedValue;
			if(parentProjectRoundUnitID == "0")
			{
				parentProjectRoundUnitID = "NULL";
			}

			string extID = ((TextBox)Units.FindControl("UnitExternalID")).Text.Replace("'","");
			if(projectRoundUnitID == 0)
			{
				projectRoundUnitID = Db.getInt32("SET NOCOUNT ON;" +
					"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;" +
					"BEGIN TRAN;" +
					"INSERT INTO ProjectRoundUnit (" +
					"UserCount," +
					"LangID," +
					"SurveyID," +
					"ProjectRoundID," +
					"Unit," +
					"ParentProjectRoundUnitID" +
					") VALUES (" +
					"" + Convert.ToInt32("0" + ((TextBox)Units.FindControl("UserCount")).Text) + "," +
					"" + ((RadioButtonList)Units.FindControl("UnitLangID")).SelectedValue + "," +
					"" + ((DropDownList)Units.FindControl("UnitSurvey")).SelectedValue + "," +
					"" + projectRoundID + "," +
					"'" + ((TextBox)Units.FindControl("UnitName")).Text.Replace("'","") + "'," +
					"" + parentProjectRoundUnitID + "" +
					");" +
					"SELECT ProjectRoundUnitID FROM [ProjectRoundUnit] " +
					"WHERE ProjectRoundID=" + projectRoundID + " AND Unit = '" + ((TextBox)Units.FindControl("UnitName")).Text.Replace("'","") + "' " +
					"ORDER BY ProjectRoundUnitID DESC;" +
					"COMMIT;");
				Db.execute("UPDATE ProjectRoundUnit " +
					"SET ID = dbo.cf_unitExtID(" + projectRoundUnitID + ",dbo.cf_unitDepth(" + projectRoundUnitID + "),'" + extID + "'), " +
					"SortOrder = " + projectRoundUnitID + " " +
					"WHERE ProjectRoundUnitID = " + projectRoundUnitID);
			}
			else
			{
				Db.execute("UPDATE ProjectRoundUnit " +
					"SET ID = dbo.cf_unitExtID(" + projectRoundUnitID + "," +
					"dbo.cf_unitDepth(" + projectRoundUnitID + "),'" + extID + "'), " +
					"UserCount = " + Convert.ToInt32("0" + ((TextBox)Units.FindControl("UserCount")).Text) + ", " +
					"LangID = " + ((RadioButtonList)Units.FindControl("UnitLangID")).SelectedValue + ", " +
					"SurveyID = " + ((DropDownList)Units.FindControl("UnitSurvey")).SelectedValue + ", " +
					"Unit = '" + ((TextBox)Units.FindControl("UnitName")).Text.Replace("'","") + "', " +
					"ParentProjectRoundUnitID = " + parentProjectRoundUnitID + " " +
					"WHERE ProjectRoundUnitID = " + projectRoundUnitID);
			}

			Db.execute("UPDATE ProjectRoundUnit SET SortString = dbo.cf_unitSortString(ProjectRoundUnitID) WHERE ProjectRoundID = " + projectRoundID);

			HttpContext.Current.Response.Redirect("projectSetup.aspx?ProjectRoundID=" + projectRoundID + "&ProjectID=" + projectID, true);
		}

		private void upload_Click(object sender, EventArgs e)
		{
			HtmlInputFile import = ((HtmlInputFile)Units.FindControl("import"));
			if(import.PostedFile != null && import.PostedFile.ContentLength != 0)
			{
				System.IO.StreamReader f = new System.IO.StreamReader(import.PostedFile.InputStream, System.Text.Encoding.Default);
				string s = f.ReadToEnd();
				f.Close();
				s = s.Replace("\r","\n");
				s = s.Replace("\n\n","\n");
				string[] sa = s.Split('\n');

				string units = "''", parentUnits = "''";
				bool valid = true;
				Label error = ((Label)Units.FindControl("error"));
				error.Text = "";

				foreach(string a in sa)
				{
					string id = a.Split('\t')[0].Replace("'","");
					if(id != "ID" && id != "")
					{
						string parentID = a.Split('\t')[1].Replace("'","");
						if(parentID.Length > 15)
						{
							parentID = parentID.Substring(0,15);
						}
						units += ",'" + id + "'";
						if(parentID != "")
						{
							parentUnits += ",'" + parentID + "'";
						}
					}
				}

				SqlDataReader rs = Db.sqlRecordSet("SELECT dbo.cf_ProjectUnitTree(ProjectRoundUnitID,' » '), ID FROM ProjectRoundUnit WHERE ProjectRoundID = " + projectRoundID + " AND ID IN (" + units + ")");
				while(rs.Read())
				{
					valid = false;
					error.Text += "Error: Unit with external ID \"" + rs.GetString(1) + "\" already exist (" + rs.GetString(0) + ")<BR/>";
				}
				rs.Close();
				rs = Db.sqlRecordSet("SELECT ID FROM ProjectRoundUnit WHERE ID IS NOT NULL AND ProjectRoundID = " + projectRoundID);
				while(rs.Read())
				{
					units += ",'" + rs.GetString(0).Replace("'","") + "'";
				}
				rs.Close();

				foreach(string p in parentUnits.Split(','))
				{
					if(units.IndexOf(p) < 0)
					{
						valid = false;
						error.Text += "Error: Unit with external ID \"" + p + "\" specified as parent unit does not exist<BR/>";
					}
				}

				if(valid)
				{
					foreach(string a in sa)
					{
						string[] u = a.Split('\t');
						string id = u[0].Replace("'","");
						if(id.Length > 15)
						{
							id = id.Substring(0,15);
						}

						if(id != "ID" && id != "")
						{
							string unit = u[2].Replace("'","");
							string userCount = "0", surveyID = "0", langID = "0";
							if(u.Length > 3 && u[3] != "")
								userCount = u[3];
							if(u.Length > 4 && u[4] != "")
								surveyID = u[4];
							if(u.Length > 5 && u[5] != "")
								langID = u[5];
						
							int projectRoundUnitID = Db.getInt32("SET NOCOUNT ON;" +
								"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;" +
								"BEGIN TRAN;" +
								"INSERT INTO ProjectRoundUnit (" +
								"ProjectRoundID," +
								"Unit," +
								"ID," +
								"UserCount," +
								"SurveyID," +
								"LangID" +
								") VALUES (" +
								"" + projectRoundID + "," +
								"'" + unit + "'," +
								"'" + id + "'," +
								"" + userCount + "," +
								"" + surveyID + "," +
								"" + langID + "" +
								");" +
								"SELECT ProjectRoundUnitID FROM [ProjectRoundUnit] WHERE ProjectRoundID=" + projectRoundID + " AND Unit = '" + unit + "' ORDER BY ProjectRoundUnitID DESC;COMMIT;");
							Db.execute("UPDATE ProjectRoundUnit SET SortOrder = " + projectRoundUnitID + " WHERE ProjectRoundUnitID = " + projectRoundUnitID);
						}
					}
					foreach(string a in sa)
					{
						string[] u = a.Split('\t');
						string id = u[0].Replace("'","");
						if(id.Length > 15)
						{
							id = id.Substring(0,15);
						}

						if(id != "ID" && id != "")
						{
							rs = Db.sqlRecordSet("SELECT ProjectRoundUnitID FROM ProjectRoundUnit WHERE ID = '" + id + "' AND ProjectRoundID = " + projectRoundID);
							if(rs.Read())
							{
								string parentProjectRoundUnitID = ((DropDownList)Units.FindControl("ParentProjectRoundUnitID")).SelectedValue;
								if(parentProjectRoundUnitID == "0")
									parentProjectRoundUnitID = "NULL";

								SqlDataReader rs2 = Db.sqlRecordSet("SELECT ProjectRoundUnitID FROM ProjectRoundUnit WHERE ID = '" + u[1].Replace("'","") + "' AND ProjectRoundID = " + projectRoundID);
								if(rs2.Read())
								{
									parentProjectRoundUnitID = rs2.GetInt32(0).ToString();
								}
								rs2.Close();

								Db.execute("UPDATE ProjectRoundUnit SET ParentProjectRoundUnitID = " + parentProjectRoundUnitID + " WHERE ProjectRoundUnitID = " + rs.GetInt32(0));
							}
							rs.Close();
						}
					}

					Db.execute("UPDATE ProjectRoundUnit SET SortString = dbo.cf_unitSortString(ProjectRoundUnitID) WHERE ProjectRoundID = " + projectRoundID);

					HttpContext.Current.Response.Redirect("projectSetup.aspx?ProjectRoundID=" + projectRoundID + "&ProjectID=" + projectID, true);
				}
			}
		}

		private void ExportUnit_Click(object sender, EventArgs e)
		{
			exportUnit = true;
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);

			if(exportUnit)
			{
				string output = "" +
					"ID" +
					"\t" +
					"parentID" +
					"\t" +
					"unit" +
					"\t" +
					"userCount" +
					"\t" +
					"surveyID" +
					"\t" +
					"langID" +
					"\r\n";

				SqlDataReader rs = Db.sqlRecordSet("SELECT u.ID, p.ID, u.Unit, u.UserCount, u.SurveyID, u.LangID FROM ProjectRoundUnit u LEFT OUTER JOIN ProjectRoundUnit p ON u.ParentProjectRoundUnitID = p.ProjectRoundUnitID WHERE u.ProjectRoundID = " + projectRoundID + " ORDER BY u.SortString");
				while(rs.Read())
				{
					output += "" +
						rs.GetString(0) +
						"\t" +
						(rs.IsDBNull(1) ? "" : rs.GetString(1)) + 
						"\t" +
						rs.GetString(2) +
						"\t" +
						rs.GetInt32(3).ToString() +
						"\t" +
						rs.GetInt32(4).ToString() +
						"\t" +
						rs.GetInt32(5).ToString() +
						"\r\n";
				}
				rs.Close();

				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ClearHeaders();
				HttpContext.Current.Response.Charset = "UTF-8";
				HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
				HttpContext.Current.Response.ContentType = "text/plain";
				//HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
				HttpContext.Current.Response.AddHeader("content-disposition","attachment; filename=" + DateTime.Now.Ticks + ".txt");
				HttpContext.Current.Response.Write(output);
				HttpContext.Current.Response.Flush();
				HttpContext.Current.Response.End();
			}
			else if(exportUser)
			{
				StringBuilder output = new StringBuilder();
				output.Append("" +
					"Email" +
					"\t" +
					"ID" +
					"\t" +
					"Name" +
					"\t" +
					"Extended" +
					"\t" +
					"Extra" +
					"\t" +
					"ExternalID" +
					"\t" +
					"Login" +
					"\t" +
					"Link" +
					"\r\n");

				SqlDataReader rs = Db.sqlRecordSet("SELECT " +
					"u.Email, " +
					"uu.ID, " +
					"u.Name, " +
					"u.Extended, " +
					"u.Extra, " +
					"u.ExternalID, " +
					"u.ProjectRoundUserID, " +
					"LEFT(CONVERT(VARCHAR(255),u.UserKey),8), " +
					"RIGHT(CONVERT(VARCHAR(255),u.UserKey),4), " +
					"p.AppURL " +
					"FROM ProjectRoundUser u " +
					"LEFT OUTER JOIN ProjectRoundUnit uu ON u.ProjectRoundUnitID = uu.ProjectRoundUnitID " +
					"INNER JOIN ProjectRound pr ON u.ProjectRoundID = pr.ProjectRoundID " +
					"INNER JOIN Project p ON pr.ProjectID = p.ProjectID " +
					"WHERE u.ProjectRoundID = " + projectRoundID + " ORDER BY uu.SortString, u.ProjectRoundUserID");
				while(rs.Read())
				{
					output.Append("" +
						rs.GetString(0) +
						"\t" +
						(rs.IsDBNull(1) ? "" : rs.GetString(1)) + 
						"\t" +
						(rs.IsDBNull(2) ? "" : rs.GetString(2)) + 
						"\t" +
						(rs.IsDBNull(3) ? "" : rs.GetInt32(3).ToString()) + 
						"\t" +
						(rs.IsDBNull(4) ? "" : rs.GetString(4)) + 
						"\t" +
						(rs.IsDBNull(5) ? "" : rs.GetInt64(5).ToString()) + 
						"\t" +
						rs.GetString(8) + rs.GetInt32(6).ToString() +
						"\t" +
						(!rs.IsDBNull(9) ? rs.GetString(9) : "http://eform.se") + "/submit.aspx?K=" + rs.GetString(7) + rs.GetInt32(6).ToString() +
						"\r\n");
				}
				rs.Close();

				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ClearHeaders();
				HttpContext.Current.Response.Charset = "UTF-8";
				HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
				HttpContext.Current.Response.ContentType = "text/plain";
				//HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
				HttpContext.Current.Response.AddHeader("content-disposition","attachment; filename=" + DateTime.Now.Ticks + ".txt");
				HttpContext.Current.Response.Write(output.ToString());
				HttpContext.Current.Response.Flush();
				HttpContext.Current.Response.End();
			}
			else if(exportData || exportSpssData || exportDataOne || exportSpssDataOne)
			{
				#region EXPORT

				StringBuilder output = new StringBuilder();
				string header = "", def = "", syn = "";
				bool first = true;
				int answerID = 0, userID = 0;

				string caseDelim = "\t";
				string rowDelim = "\r\n";

				if(exportSpssData || exportSpssDataOne)
				{
					caseDelim = "\r\n";
				}

				int caseCounter = 0;
				int userCaseCounter = 0;

				#region old
				/*OdbcDataReader rs = Db.recordSet("SELECT DISTINCT " +
					"sq.QuestionID, " +			// 0
					"qo.OptionID, " +			// 1
					"o.OptionType, " +			// 2
					"dbo.cf_isBlank(sq.QuestionID,q.Variablename,sq.Variablename), " +		// 3
					"dbo.cf_isBlank(qo.OptionID,o.Variablename,sqo.Variablename), " +		// 4
					"pru.ID, " +				// 5
					"a.AnswerID, " +			// 6
					"ISNULL(a.ProjectRoundUserID,-a.AnswerID), " +	// 7
					//"CAST(ISNULL(sql.Question,ql.Question) AS VARCHAR(255)), " +	// 8
					"CAST(ql.Question AS VARCHAR(255)), " +	// 8
					"(SELECT COUNT(*) FROM [SurveyQuestionOption] x WHERE x.SurveyQuestionID = sq.SurveyQuestionID), " + // 9
					"pr.LangID, " +				// 10
					"usr.Extended, " +			// 11
					"a.StartDT, " +				// 12
					"a.EndDT, " +				// 13
					"usr.Extra, " +				// 14
					"(SELECT COUNT(*) FROM [OptionComponents] x WHERE x.OptionID = o.OptionID), " + // 15
					"a.ProjectRoundUserID, " +	// 16
					"usr.NoSend, " +			// 17
					"usr.Terminated, " +		// 18
					"usr.SendCount, " +			// 19
					"usr.ReminderCount, " +		// 20
					"pru.ProjectRoundUnitID " +	// 21
					"FROM SurveyQuestion sq " +
					"INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " + 
					"INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " + 
					"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " + 
					"INNER JOIN ProjectSurvey ps ON ps.SurveyID = sq.SurveyID " +
					"INNER JOIN ProjectRound pr ON ps.ProjectID = pr.ProjectID " +
					"INNER JOIN Answer a ON a.ProjectRoundID = pr.ProjectRoundID " +
					"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
					"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
					"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = pr.LangID " +
					"LEFT OUTER JOIN ProjectRoundUser usr ON a.ProjectRoundUserID = usr.ProjectRoundUserID " +
					"LEFT OUTER JOIN SurveyQuestionLang sql ON sq.SurveyQuestionID = sql.SurveyQuestionID AND sql.LangID = pr.LangID " + 
					"WHERE pru.Terminated IS NULL " + 
					"AND (usr.ProjectRoundUserID IS NULL OR (usr.Terminated IS NULL AND usr.NoSend IS NULL)) " + 
					"AND a.EndDT IS NOT NULL " + 
					"AND a.ProjectRoundID = " + HttpContext.Current.Request.QueryString["ProjectRoundID"] + " " +
					"AND ps.ProjectID = " + HttpContext.Current.Request.QueryString["ProjectID"] + " " +
					"ORDER BY a.ProjectRoundUserID, a.AnswerID, dbo.cf_isBlank(sq.QuestionID,q.Variablename,sq.Variablename), sq.QuestionID, dbo.cf_isBlank(qo.OptionID,o.Variablename,sqo.Variablename), qo.OptionID");*/
					/* Answer is base
					string SQL = "SELECT DISTINCT " +
					"sq.QuestionID, " +			// 0
					"qo.OptionID, " +			// 1
					"o.OptionType, " +			// 2
					"dbo.cf_isBlank(q.QuestionID,q.Variablename,xq.Variablename) AS s3, " +		// 3
					"dbo.cf_isBlank(qo.OptionID,o.Variablename,xo.Variablename) AS s4, " +		// 4
					"pru.ID, " +				// 5
					"a.AnswerID, " +			// 6
					"ISNULL(a.ProjectRoundUserID,-a.AnswerID), " +	// 7
					"CAST(ql.Question AS VARCHAR(255)), " +	// 8
					"(SELECT COUNT(*) FROM [SurveyQuestionOption] x WHERE x.SurveyQuestionID = sq.SurveyQuestionID), " +					 // 9
					"pr.LangID, " +				// 10
					"usr.Extended, " +			// 11
					"a.StartDT, " +				// 12
					"a.EndDT, " +				// 13
					"usr.Extra, " +				// 14
					"(SELECT COUNT(*) FROM [OptionComponents] x WHERE x.OptionID = o.OptionID), " + // 15
					"a.ProjectRoundUserID, " +	// 16
					"usr.NoSend, " +			// 17
					"usr.Terminated, " +		// 18
					"usr.SendCount, " +			// 19
					"usr.ReminderCount, " +		// 20
					"pru.ProjectRoundUnitID, " +// 21
					"pru.Terminated, " +		// 22
					"ISNULL(xq.SortOrder,999999) AS s1, " +
					"ISNULL(xo.SortOrder,999999) AS s2 " +
					"FROM SurveyQuestion sq " +
					"INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " + 
					"INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " + 
					"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " + 
					"INNER JOIN ProjectSurvey ps ON ps.SurveyID = sq.SurveyID " +
					"INNER JOIN ProjectRound pr ON ps.ProjectID = pr.ProjectID " +//AND pr.SurveyID = ps.SurveyID " +
					"INNER JOIN Answer a ON a.ProjectRoundID = pr.ProjectRoundID " +
					"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
					"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
					"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = pr.LangID " +
					"LEFT OUTER JOIN ProjectRoundUser usr ON a.ProjectRoundUserID = usr.ProjectRoundUserID " +
					"LEFT OUTER JOIN SurveyQuestion xq ON q.QuestionID = xq.QuestionID AND xq.SurveyID = pr.SurveyID " +
					"LEFT OUTER JOIN SurveyQuestionOption xo ON xq.SurveyQuestionID = xo.SurveyQuestionID AND xo.QuestionOptionID = qo.QuestionOptionID " +
					//"LEFT OUTER JOIN SurveyQuestionLang sql ON sq.SurveyQuestionID = sql.SurveyQuestionID AND sql.LangID = pr.LangID " + 
					"WHERE a.ProjectRoundID = " + HttpContext.Current.Request.QueryString["ProjectRoundID"] + " " +
					//"AND pru.Terminated IS NULL " + 
					//"AND (usr.ProjectRoundUserID IS NULL OR (usr.Terminated IS NULL AND usr.NoSend IS NULL)) " + 
					//"AND a.EndDT IS NOT NULL " + 
					"AND ps.ProjectID = " + HttpContext.Current.Request.QueryString["ProjectID"] + " " +
					"ORDER BY " +
					"a.ProjectRoundUserID, " +
					"a.AnswerID, " +
					"s1, " +
					"s2, " +
					"s3, " +
					"s4" +
					"";*/
				#endregion
				int tmpcx = 0;
				string SQL = "SELECT DISTINCT " +
					"sq.QuestionID, " +			// 0
					"qo.OptionID, " +			// 1
					"o.OptionType, " +			// 2
					"dbo.cf_isBlank(q.QuestionID,q.Variablename,xq.Variablename) AS s3, " +		// 3
					"dbo.cf_isBlank(qo.OptionID,o.Variablename,xo.Variablename) AS s4, " +		// 4
					"pru.ID, " +				// 5
					"a.AnswerID, " +			// 6
					"ISNULL(a.AnswerID,-usr.ProjectRoundUserID) AS ansID, " +	// 7
					"LTRIM(RTRIM(CAST(ql.Question AS VARCHAR(8000)))), " +	// 8
					"(SELECT COUNT(*) FROM [SurveyQuestionOption] x WHERE x.SurveyQuestionID = sq.SurveyQuestionID), " +					 // 9
					"pr.LangID, " +				// 10
					"usr.Extended, " +			// 11
					"a.StartDT, " +				// 12
					"a.EndDT, " +				// 13
					"usr.Extra, " +				// 14
					"(SELECT COUNT(*) FROM [OptionComponents] x WHERE x.OptionID = o.OptionID), " + // 15
					"usr.ProjectRoundUserID, " +	// 16
					"usr.NoSend, " +			// 17
					"usr.Terminated, " +		// 18
					"usr.SendCount, " +			// 19
					"usr.ReminderCount, " +		// 20
					"pru.ProjectRoundUnitID, " +// 21
					"pru.Terminated, " +		// 22
					"ISNULL(xq.SortOrder,999999) AS s1, " +	// 23
					"ISNULL(xo.SortOrder,999999) AS s2, " +	// 24
					"pru.SortString, " +		// 25
					"usr.ExternalID, " +		// 26
					"usr.GroupID, " +			// 27
					"usr.ExtendedTag " +		// 28
					"FROM SurveyQuestion sq " +
					"INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " + 
					"INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " + 
					"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " + 
					"INNER JOIN ProjectSurvey ps ON ps.SurveyID = sq.SurveyID " +
					"INNER JOIN ProjectRound pr ON ps.ProjectID = pr.ProjectID " + (exportSpssDataOne || exportDataOne ? " AND pr.SurveyID = ps.SurveyID " : "") +
					"INNER JOIN ProjectRoundUnit pru ON pr.ProjectRoundID = pru.ProjectRoundID " +
					"INNER JOIN ProjectRoundUser usr ON pru.ProjectRoundUnitID = usr.ProjectRoundUnitID " +
					"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
					"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = pr.LangID " +
					"LEFT OUTER JOIN SurveyQuestion xq ON q.QuestionID = xq.QuestionID AND xq.SurveyID = pr.SurveyID " +
					"LEFT OUTER JOIN SurveyQuestionOption xo ON xq.SurveyQuestionID = xo.SurveyQuestionID AND xo.QuestionOptionID = qo.QuestionOptionID " +
					"LEFT OUTER JOIN Answer a ON usr.ProjectRoundUserID = a.ProjectRoundUserID " +
					"WHERE pr.ProjectRoundID = " + HttpContext.Current.Request.QueryString["ProjectRoundID"] + " " +
					"AND ps.ProjectID = " + HttpContext.Current.Request.QueryString["ProjectID"] + " " +

					"UNION ALL " +

					"sq.QuestionID, " +			// 0
					"qo.OptionID, " +			// 1
					"o.OptionType, " +			// 2
					"dbo.cf_isBlank(q.QuestionID,q.Variablename,xq.Variablename) AS s3, " +		// 3
					"dbo.cf_isBlank(qo.OptionID,o.Variablename,xo.Variablename) AS s4, " +		// 4
					"pru.ID, " +				// 5
					"a.AnswerID, " +			// 6
					"a.AnswerID AS ansID, " +	// 7
					"LTRIM(RTRIM(CAST(ql.Question AS VARCHAR(8000)))), " +	// 8
					"(SELECT COUNT(*) FROM [SurveyQuestionOption] x WHERE x.SurveyQuestionID = sq.SurveyQuestionID), " +					 // 9
					"pr.LangID, " +				// 10
					"usr.Extended, " +			// 11
					"a.StartDT, " +				// 12
					"a.EndDT, " +				// 13
					"usr.Extra, " +				// 14
					"(SELECT COUNT(*) FROM [OptionComponents] x WHERE x.OptionID = o.OptionID), " + // 15
					"-pru.ProjectRoundUnitID, " +	// 16
					"usr.NoSend, " +			// 17
					"usr.Terminated, " +		// 18
					"usr.SendCount, " +			// 19
					"usr.ReminderCount, " +		// 20
					"pru.ProjectRoundUnitID, " +// 21
					"pru.Terminated, " +		// 22
					"ISNULL(xq.SortOrder,999999) AS s1, " +	// 23
					"ISNULL(xo.SortOrder,999999) AS s2, " +	// 24
					"pru.SortString, " +		// 25
					"usr.ExternalID, " +		// 26
					"usr.GroupID, " +			// 27
					"usr.ExtendedTag " +		// 28
					"FROM SurveyQuestion sq " +
					"INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " + 
					"INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " + 
					"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " + 
					"INNER JOIN ProjectSurvey ps ON ps.SurveyID = sq.SurveyID " +
					"INNER JOIN ProjectRound pr ON ps.ProjectID = pr.ProjectID " + (exportSpssDataOne || exportDataOne ? " AND pr.SurveyID = ps.SurveyID " : "") +
					"INNER JOIN ProjectRoundUnit pru ON pr.ProjectRoundID = pru.ProjectRoundID " +
					"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
					"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = pr.LangID " +
					"INNER JOIN Answer a ON a.ProjectRoundUserID IS NULL AND pru.ProjectRoundUnitID = a.ProjectRoundUnitID " +
					"LEFT OUTER JOIN SurveyQuestion xq ON q.QuestionID = xq.QuestionID AND xq.SurveyID = pr.SurveyID " +
					"LEFT OUTER JOIN SurveyQuestionOption xo ON xq.SurveyQuestionID = xo.SurveyQuestionID AND xo.QuestionOptionID = qo.QuestionOptionID " +
					"WHERE pr.ProjectRoundID = " + HttpContext.Current.Request.QueryString["ProjectRoundID"] + " " +
					"AND ps.ProjectID = " + HttpContext.Current.Request.QueryString["ProjectID"] + " " +

					"ORDER BY " +
					"pru.SortString, " +
					"usr.Extra, " +
					"usr.ProjectRoundUserID, " +
					"a.AnswerID, " +
					"s1, " +
					"s2, " +
					"s3, " +
					"s4" +
					"";
				if(exportSpssDataOne)
				{
					exportSpssData = true;
				}
				if(exportDataOne)
				{
					exportData = true;
				}
//				HttpContext.Current.Response.Write(SQL);
//				HttpContext.Current.Response.End();
				#region Answer base
				/*
				SqlDataReader rs = Db.sqlRecordSet(SQL);
				while(rs.Read())
				{
					if(userID == 0 || userID != rs.GetInt32(7))
					{
						userCaseCounter = 0;
						userID = rs.GetInt32(7);
					}
					if(answerID == 0 || answerID != rs.GetInt32(6))
					{
						userCaseCounter++;
						if(answerID == 0)
						{
							answerID = rs.GetInt32(6);
							header += "USER" + caseDelim + 
								"UNITCODE" + caseDelim + 
								"UNIT" + caseDelim + 
								"CASE" + caseDelim + 
								"START_DT" + caseDelim + 
								"END_DT" + caseDelim + 
								"EXT" + caseDelim + 
								"EXTRA" + caseDelim + 
								"SENDCOUNT" + caseDelim + 
								"REMINDERCOUNT" + caseDelim + 
								"NOSEND" + caseDelim + 
								"TERMINATED";
							
							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F6.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUser)." + rowDelim;
							syn += "VARIABLE LABELS sysUser 'User identification'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(A255)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUcde)." + rowDelim;
							syn += "VARIABLE LABELS sysUcde 'Unit code'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F6.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUnit)." + rowDelim;
							syn += "VARIABLE LABELS sysUnit 'Unit'." + rowDelim;
							syn += "VALUE LABELS sysUnit";
							SqlDataReader rs3 = Db.sqlRecordSet("SELECT ProjectRoundUnitID, Unit FROM ProjectRoundUnit WHERE ProjectRoundID = " + HttpContext.Current.Request.QueryString["ProjectRoundID"]);
							while(rs3.Read())
							{
								syn += " " + rs3.GetInt32(0) + " '" + rs3.GetString(1) + "'";
							}
							rs3.Close();
							syn += "." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysCase)." + rowDelim;
							syn += "VARIABLE LABELS sysCase 'User case counter'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(A255)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysStart)." + rowDelim;
							syn += "VARIABLE LABELS sysStart 'Start Date Time'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(A255)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysEnd)." + rowDelim;
							syn += "VARIABLE LABELS sysEnd 'End Date Time'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysExt)." + rowDelim;
							syn += "VARIABLE LABELS sysExt 'Extended Survey'." + rowDelim;
							syn += "VALUE LABELS sysExt 0 'No' 1 'Yes'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(A255)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysExtra)." + rowDelim;
							syn += "VARIABLE LABELS sysExtra 'Extra info'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysSndCt)." + rowDelim;
							syn += "VARIABLE LABELS sysSndCt 'Invitation send count'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysRemCt)." + rowDelim;
							syn += "VARIABLE LABELS sysRemCt 'Reminder send count'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysNoSnd)." + rowDelim;
							syn += "VARIABLE LABELS sysNoSnd 'Unsubscribed for further reminders'." + rowDelim;
							syn += "VALUE LABELS sysNoSnd 0 'No' 1 'Yes'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserTerm)." + rowDelim;
							syn += "VARIABLE LABELS sysUserTerm 'User terminated/withdrawn'." + rowDelim;
							syn += "VALUE LABELS sysUserTerm 0 'No' 1 'Yes'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUnitTerm)." + rowDelim;
							syn += "VARIABLE LABELS sysUnitTerm 'Unit terminated/withdrawn'." + rowDelim;
							syn += "VALUE LABELS sysUnitTerm 0 'No' 1 'Yes'." + rowDelim;
						}
						else if(answerID != rs.GetInt32(6))
						{
							first = false;
							output.Append(rowDelim);
						}
						answerID = rs.GetInt32(6);
						output.Append(userID + caseDelim + 
							rs.GetString(5) + caseDelim + 
							rs.GetInt32(21) + caseDelim + 
							userCaseCounter + caseDelim + 
							rs.GetDateTime(12).ToString("yyyy-MM-dd HH:mm") + caseDelim +
							(rs.IsDBNull(13) ? "" : rs.GetDateTime(13).ToString("yyyy-MM-dd HH:mm")) + caseDelim +
							(rs.IsDBNull(11) ? 0 : rs.GetInt32(11)) + caseDelim +
							(rs.IsDBNull(14) ? "" : rs.GetString(14)) + caseDelim +
							(rs.IsDBNull(19) ? 0 : rs.GetInt32(19)) + caseDelim +
							(rs.IsDBNull(20) ? 0 : rs.GetInt32(20)) + caseDelim +
							(rs.IsDBNull(17) ? 0 : rs.GetInt32(17)) + caseDelim +
							(rs.IsDBNull(18) ? 0 : rs.GetInt32(18)) + caseDelim +
							(rs.IsDBNull(22) ? 0 : 1));
					}
					if(first)
					{
						string var = rs.GetString(3);
						if(rs.GetInt32(9) > 1)
						{
							var += rs.GetString(4);
						}

						header += caseDelim + var;

						syn += "RENAME VARIABLES (V" + (++caseCounter) + "=" + var + ")." + rowDelim;
						syn += "VARIABLE LABELS " + var + " '" + rs.GetString(8) + "'." + rowDelim;

						switch(rs.GetInt32(2))
						{
							case 0:
								syn += "VALUE LABELS " + var + (rs.GetInt32(2) == 9 && rs.GetInt32(15) > 2 ? "Zone" : "");
								int cx = 0;
								SqlDataReader rs3 = Db.sqlRecordSet("SELECT " +
									"ocl.Text, " +
									"oc.ExportValue " +
									"FROM OptionComponents oc " +
									"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + rs.GetInt32(10) + " " +
									"WHERE oc.OptionID = " + rs.GetInt32(1) + " " +
									"ORDER BY oc.SortOrder");
								while(rs3.Read())
								{
									syn += " " + (rs.GetInt32(2) == 9 && rs.GetInt32(15) > 2 ? (cx++) : rs3.GetInt32(1)) + " '" + RemoveHTMLTags(rs3.GetString(0)) + "'";
								}
								rs3.Close();
								syn += "." + rowDelim;
								break;
							case 1:	
								def += "/" + (caseCounter) + " V" + (caseCounter) + "(F6.0)" + rowDelim;
								goto case 0;
							case 2: 
								def += "/" + (caseCounter) + " V" + (caseCounter) + 
									"(A" + 
									(Math.Min(255,Db.sqlGetInt32("SELECT MAX(LEN(CAST(ValueText AS VARCHAR(8000)))) FROM AnswerValue WHERE QuestionID = " + rs.GetInt32(0) + " AND OptionID = " + rs.GetInt32(1))+16)) + 
									")" + rowDelim; 
								break;
							case 3: 
								goto case 1;
							case 4: // should be decimal
								def += "/" + (caseCounter) + " V" + (caseCounter) + "(F6.0)" + rowDelim; 
								break; 
							case 9:
								if(rs.GetInt32(15) > 2)
								{
									def += "/" + (caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;

									syn += "RENAME VARIABLES (V" + (++caseCounter) + "=" + var + "Zone)." + rowDelim;
									syn += "VARIABLE LABELS " + var + "Zone '" + rs.GetString(8) + "'." + rowDelim;
								}
								def += "/" + (caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim; 
								goto case 0;
						}
					}
					output.Append(caseDelim);

					SqlDataReader rs2 = Db.sqlRecordSet("SELECT " +
						"SUBSTRING(av.ValueText,1,250), " +		// 0
						"op.ExportValue, " +	// 1
						"av.ValueDecimal, " +	// 2
						"av.ValueInt " +		// 3
						"FROM AnswerValue av " +
						"LEFT OUTER JOIN OptionComponents op ON av.ValueInt = op.OptionComponentID AND op.OptionID = av.OptionID " +
						"WHERE av.AnswerID = " + rs.GetInt32(6) + " " +
						"AND av.QuestionID = " + rs.GetInt32(0) + " " +
						"AND av.OptionID = " + rs.GetInt32(1) + " " +
						"AND av.DeletedSessionID IS NULL");
					if(rs2.Read())
					{
						try
						{
							switch(rs.GetInt32(2))
							{
								case 1:	
									output.Append((rs2.IsDBNull(1) ? "" : rs2.GetInt32(1).ToString())); 
									break;
								case 2: 
									output.Append((rs2.IsDBNull(0) ? "" : rs2.GetString(0).Replace("\r\n"," ").Replace("\r"," ").Replace("\n"," "))); 
									break;
								case 3: 
									goto case 1;
								case 4: 
									output.Append((rs2.IsDBNull(2) ? "" : ((float)rs2.GetDecimal(2)).ToString().Replace(".",","))); 
									break;
								case 9:	
									output.Append((rs2.IsDBNull(3) ? "" : rs2.GetInt32(3).ToString())); 
									if(rs.GetInt32(15) > 2)
									{
										output.Append(caseDelim);

										if(!rs2.IsDBNull(3))
										{
											int cx = 0;
											for(int i=0; i<rs.GetInt32(15); i++)
											{
												if(rs2.GetInt32(3) >= 100/rs.GetInt32(15)*i)
												{
													cx = i;
												}
											}
											output.Append(cx);
										}
									}
									break;
							}
						}
						catch(Exception) {}
					}
					else if(rs.GetInt32(2) == 9 && rs.GetInt32(15) > 2)
					{
						output.Append(caseDelim);
					}
					rs2.Close();
				}
				rs.Close();
				*/
				#endregion

				#region User base
				SqlDataReader rs = Db.sqlRecordSet(SQL);
				while(rs.Read())
				{
					if(userID == 0 || userID != rs.GetInt32(16))
					{
						tmpcx++;
						userCaseCounter = 0;
						userID = rs.GetInt32(16);
					}
					if(answerID == 0 || answerID != rs.GetInt32(7))
					{
						userCaseCounter++;
						if(answerID == 0)
						{
							answerID = rs.GetInt32(7);
							#region  ASCII Header
							header += "USER" + caseDelim + 
								"UNITCODE" + caseDelim + 
								"UNIT" + caseDelim + 
								"CASE" + caseDelim + 
								"START_DT" + caseDelim + 
								"END_DT" + caseDelim + 
								"EXT" + caseDelim + 
								"EXTTAG" + caseDelim + 
								"GROUP" + caseDelim + 
								"EXTRA" + caseDelim + 
								"EXTID" + caseDelim + 
								"SENDCOUNT" + caseDelim + 
								"REMINDERCOUNT" + caseDelim + 
								"NOSEND" + caseDelim + 
								"USER_TERM" + caseDelim + 
								"UNIT_TERM";
							#endregion

							#region SPSS header
							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F6.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUser)." + rowDelim;
							syn += "VARIABLE LABELS sysUser 'User identification'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(A255)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUcde)." + rowDelim;
							syn += "VARIABLE LABELS sysUcde 'Unit code'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F6.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUnit)." + rowDelim;
							syn += "VARIABLE LABELS sysUnit 'Unit'." + rowDelim;
							syn += "VALUE LABELS sysUnit";
							SqlDataReader rs3 = Db.sqlRecordSet("SELECT ProjectRoundUnitID, Unit FROM ProjectRoundUnit WHERE ProjectRoundID = " + HttpContext.Current.Request.QueryString["ProjectRoundID"]);
							while(rs3.Read())
							{
								syn += " " + rs3.GetInt32(0) + " '" + rs3.GetString(1) + "'";
							}
							rs3.Close();
							syn += "." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysCase)." + rowDelim;
							syn += "VARIABLE LABELS sysCase 'User case counter'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(A255)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysStart)." + rowDelim;
							syn += "VARIABLE LABELS sysStart 'Start Date Time'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(A255)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysEnd)." + rowDelim;
							syn += "VARIABLE LABELS sysEnd 'End Date Time'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysExt)." + rowDelim;
							syn += "VARIABLE LABELS sysExt 'Extended Survey'." + rowDelim;
							syn += "VALUE LABELS sysExt 0 'No' 1 'Yes'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysExtTag)." + rowDelim;
							syn += "VARIABLE LABELS sysExtTag 'Extended'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysGroup)." + rowDelim;
							syn += "VARIABLE LABELS sysGroup 'Group'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(A255)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysExtra)." + rowDelim;
							syn += "VARIABLE LABELS sysExtra 'Extra info'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F8.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysExtID)." + rowDelim;
							syn += "VARIABLE LABELS sysExtra 'External ID'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysSndCt)." + rowDelim;
							syn += "VARIABLE LABELS sysSndCt 'Invitation send count'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysRemCt)." + rowDelim;
							syn += "VARIABLE LABELS sysRemCt 'Reminder send count'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysNoSnd)." + rowDelim;
							syn += "VARIABLE LABELS sysNoSnd 'Unsubscribed for further reminders'." + rowDelim;
							syn += "VALUE LABELS sysNoSnd 0 'No' 1 'Yes'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserTerm)." + rowDelim;
							syn += "VARIABLE LABELS sysUserTerm 'User terminated/withdrawn'." + rowDelim;
							syn += "VALUE LABELS sysUserTerm 0 'No' 1 'Yes'." + rowDelim;

							def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
							syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUnitTerm)." + rowDelim;
							syn += "VARIABLE LABELS sysUnitTerm 'Unit terminated/withdrawn'." + rowDelim;
							syn += "VALUE LABELS sysUnitTerm 0 'No' 1 'Yes'." + rowDelim;
							#endregion
						}
						else if(answerID != rs.GetInt32(7))
						{
							first = false;
							output.Append(rowDelim);
						}
						answerID = rs.GetInt32(7);
						output.Append(userID + caseDelim + 
							rs.GetString(5) + caseDelim + 
							rs.GetInt32(21) + caseDelim + 
							userCaseCounter + caseDelim + 
							(rs.IsDBNull(12) ? "" : rs.GetDateTime(12).ToString("yyyy-MM-dd HH:mm")) + caseDelim +
							(rs.IsDBNull(13) ? "" : rs.GetDateTime(13).ToString("yyyy-MM-dd HH:mm")) + caseDelim +
							(rs.IsDBNull(11) ? 0 : rs.GetInt32(11)) + caseDelim +
							(rs.IsDBNull(27) ? 0 : rs.GetInt32(27)) + caseDelim +
							(rs.IsDBNull(28) ? 0 : rs.GetInt32(28)) + caseDelim +
							(rs.IsDBNull(14) ? "" : rs.GetString(14)) + caseDelim +
							(rs.IsDBNull(26) ? "" : rs.GetInt64(26).ToString()) + caseDelim +
							(rs.IsDBNull(19) ? 0 : rs.GetInt32(19)) + caseDelim +
							(rs.IsDBNull(20) ? 0 : rs.GetInt32(20)) + caseDelim +
							(rs.IsDBNull(17) ? 0 : rs.GetInt32(17)) + caseDelim +
							(rs.IsDBNull(18) ? 0 : rs.GetInt32(18)) + caseDelim +
							(rs.IsDBNull(22) ? 0 : 1));
					}
					if(first)
					{
						#region Header
						string var = rs.GetString(3);
						if(rs.GetInt32(9) > 1)
						{
							var += rs.GetString(4);
						}

						header += caseDelim + var;

						syn += "RENAME VARIABLES (V" + (++caseCounter) + "=" + var + ")." + rowDelim;
						syn += "VARIABLE LABELS " + var + " '" + Db.trunc(Db.RemoveHTMLTags(rs.GetString(8)),230) + "'." + rowDelim;

						switch(rs.GetInt32(2))
						{
							case 0:
								syn += "VALUE LABELS " + var + (rs.GetInt32(2) == 9 && rs.GetInt32(15) > 2 ? "Zone" : "");
								int cx = 0;
								SqlDataReader rs3 = Db.sqlRecordSet("SELECT " +
									"ocl.Text, " +
									"oc.ExportValue " +
									"FROM OptionComponents oc " +
									"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + rs.GetInt32(10) + " " +
									"WHERE oc.OptionID = " + rs.GetInt32(1) + " " +
									"ORDER BY oc.SortOrder");
								while(rs3.Read())
								{
									syn += " " + (rs.GetInt32(2) == 9 && rs.GetInt32(15) > 2 ? (cx++) : rs3.GetInt32(1)) + " '" + Db.trunc(Db.RemoveHTMLTags(rs3.GetString(0)),230) + "'";
								}
								rs3.Close();
								syn += "." + rowDelim;
								break;
							case 1:	
								def += "/" + (caseCounter) + " V" + (caseCounter) + "(F6.0)" + rowDelim;
								goto case 0;
							case 2: 
								def += "/" + (caseCounter) + " V" + (caseCounter) + 
									"(A" + 
									(Math.Min(255,Db.sqlGetInt32("SELECT MAX(LEN(LTRIM(RTRIM(CAST(ValueText AS VARCHAR(8000)))))) FROM AnswerValue WHERE QuestionID = " + rs.GetInt32(0) + " AND OptionID = " + rs.GetInt32(1))+16)) + 
									")" + rowDelim; 
								break;
							case 3: 
								goto case 1;
							case 4: // should be decimal
								def += "/" + (caseCounter) + " V" + (caseCounter) + "(F6.0)" + rowDelim; 
								break; 
							case 9:
								if(rs.GetInt32(15) > 2)
								{
									def += "/" + (caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;

									syn += "RENAME VARIABLES (V" + (++caseCounter) + "=" + var + "Zone)." + rowDelim;
									syn += "VARIABLE LABELS " + var + "Zone '" + rs.GetString(8) + "'." + rowDelim;
								}
								def += "/" + (caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim; 
								goto case 0;
						}
						#endregion
					}
					output.Append(caseDelim);

					string SQL2 = "SELECT " +
						"SUBSTRING(av.ValueText,1,250), " +		// 0
						"op.ExportValue, " +	// 1
						"av.ValueDecimal, " +	// 2
						"av.ValueInt " +		// 3
						"FROM AnswerValue av " +
						"LEFT OUTER JOIN OptionComponents op ON av.ValueInt = op.OptionComponentID AND op.OptionID = av.OptionID " +
						"WHERE av.AnswerID = " + rs.GetInt32(7) + " " +
						"AND av.QuestionID = " + rs.GetInt32(0) + " " +
						"AND av.OptionID = " + rs.GetInt32(1) + " " +
						"AND av.DeletedSessionID IS NULL " +
						"UNION ALL " +
						"SELECT " +
						"SUBSTRING(pruqo.Answer,1,250), " +
						"op.ExportValue, " +	// 1
						"CAST(pruqo.Answer AS DECIMAL), " +
						"CAST(pruqo.Answer AS INT) " +
						"FROM ProjectRoundUserQO pruqo " +
						"LEFT OUTER JOIN OptionComponents op ON CAST(pruqo.Answer AS INT) = op.OptionComponentID AND pruqo.OptionID = op.OptionID " +
						"WHERE -pruqo.ProjectRoundUserID = " + rs.GetInt32(7) + " " +
						"AND pruqo.QuestionID = " + rs.GetInt32(0) + " " +
						"AND pruqo.OptionID = " + rs.GetInt32(1);
					SqlDataReader rs2 = Db.sqlRecordSet(SQL2);
					if(rs2.Read())
					{
						try
						{
							switch(rs.GetInt32(2))
							{
								case 1:	
									output.Append((rs2.IsDBNull(1) ? "" : rs2.GetInt32(1).ToString())); 
									break;
								case 2: 
									output.Append((rs2.IsDBNull(0) ? "" : rs2.GetString(0).Replace("\r\n"," ").Replace("\r"," ").Replace("\n"," "))); 
									break;
								case 3: 
									goto case 1;
								case 4: 
									output.Append((rs2.IsDBNull(2) ? "" : ((float)rs2.GetDecimal(2)).ToString().Replace(".",","))); 
									break;
								case 9:	
									output.Append((rs2.IsDBNull(3) ? "" : rs2.GetInt32(3).ToString())); 
									if(rs.GetInt32(15) > 2)
									{
										output.Append(caseDelim);

										if(!rs2.IsDBNull(3))
										{
											int cx = 0;
											for(int i=0; i<rs.GetInt32(15); i++)
											{
												if(rs2.GetInt32(3) >= 100/rs.GetInt32(15)*i)
												{
													cx = i;
												}
											}
											output.Append(cx);
										}
									}
									break;
							}
						}
						catch(Exception) {}
					}
					else if(rs.GetInt32(2) == 9 && rs.GetInt32(15) > 2)
					{
						output.Append(caseDelim);
					}
					rs2.Close();
				}
				rs.Close();
				#endregion
			
				string ret = "";
				if(exportData)
				{
					ret = header + rowDelim + output.ToString() + rowDelim;
				}
				else
				{
					ret = "DATA LIST FIXED RECORDS=" + caseCounter + rowDelim + def + "." + rowDelim + "BEGIN DATA" + rowDelim + output.ToString() + rowDelim + "END DATA." + rowDelim + syn + rowDelim;
				}
				try
				{
					System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("tmp/" + DateTime.Now.Ticks + ".sps"), System.IO.FileMode.Create);
					System.IO.StreamWriter f = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);
					f.Write(ret);
					f.Close();
					fs.Close();
				}
				catch(Exception){}
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ClearHeaders();
				
				HttpContext.Current.Response.Charset = "UTF-8";
				HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
				//HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
				if(exportData)
				{
					HttpContext.Current.Response.ContentType = "text/plain";
					HttpContext.Current.Response.AddHeader("content-disposition","attachment; filename=" + DateTime.Now.Ticks + ".txt");
				}
				else
				{
					HttpContext.Current.Response.ContentType = "text/x-spss-syntax";
					HttpContext.Current.Response.AddHeader("content-disposition","attachment; filename=" + DateTime.Now.Ticks + ".sps");
				}
				HttpContext.Current.Response.Write(ret);
				HttpContext.Current.Response.Flush();
				HttpContext.Current.Response.End();

				#endregion
			}
			else if(exportSpssDataOneNew)
			{
				Db.execExport(
					Convert.ToInt32(HttpContext.Current.Request.QueryString["ProjectRoundID"].ToString()),
					(HttpContext.Current.Request.QueryString["CompareProjectRoundID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["CompareProjectRoundID"].ToString()) : 0),
					Convert.ToInt32(HttpContext.Current.Request.QueryString["ExportSurveyID"].ToString()),
					Convert.ToInt32(HttpContext.Current.Request.QueryString["ExportLangID"].ToString()),
					(HttpContext.Current.Request.QueryString["Manager"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["Manager"].ToString()) : 0),
					(HttpContext.Current.Request.QueryString["All"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["All"].ToString()) : 0),
					(HttpContext.Current.Request.QueryString["Base"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["Base"].ToString()) : 0),
					(HttpContext.Current.Request.QueryString["Extended"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["Extended"].ToString()) : 0),
					(HttpContext.Current.Request.QueryString["ToScreen"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["ToScreen"].ToString()) : 0)
					);
			}
		}

		public static int createUser(int PRID,string PRUID,string email)
		{
			return Db.getInt32("SET NOCOUNT ON;SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;BEGIN TRAN;INSERT INTO ProjectRoundUser (ProjectRoundID,ProjectRoundUnitID,Email) VALUES (" + PRID + "," + PRUID + ",'" + email.Replace("'","") + "');SELECT ProjectRoundUserID FROM [ProjectRoundUser] WHERE ProjectRoundID=" + PRID + " AND Email = '" + email.Replace("'","") + "' ORDER BY ProjectRoundUserID DESC;COMMIT;");
		}

		private void SaveUser_Click(object sender, EventArgs e)
		{
			string email = ((TextBox)Units.FindControl("Email")).Text.Replace("'","");
			if(isEmail(email))
			{
				string unitID = ((DropDownList)Units.FindControl("ProjectRoundUnitID")).SelectedValue;
				if(unitID == "0")
				{
					unitID = "NULL";
				}

				if(projectRoundUserID == 0)
				{
					projectRoundUserID = createUser(projectRoundID,unitID,email);
				}
				else
				{
					Db.execute("UPDATE ProjectRoundUser SET ProjectRoundUnitID = " + unitID + ", Email = '" + email + "' WHERE ProjectRoundUserID = " + projectRoundUserID);
				}

				HttpContext.Current.Response.Redirect("projectSetup.aspx?ProjectRoundID=" + projectRoundID + "&ProjectID=" + projectID, true);
			}
			else
			{
				((Label)Units.FindControl("error")).Text = "Invalid email-address!";
			}
		}

		public static bool isEmail(string inputEmail)
		{
			string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
				@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + 
				@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
			Regex re = new Regex(strRegex);
			if (re.IsMatch(inputEmail))
				return true;
			else
				return false;
		}

		private void uploadUser_Click(object sender, EventArgs e)
		{
			HtmlInputFile import = ((HtmlInputFile)Units.FindControl("import"));
			if(import.PostedFile != null && import.PostedFile.ContentLength != 0)
			{
				System.IO.StreamReader f = new System.IO.StreamReader(import.PostedFile.InputStream, System.Text.Encoding.Default);
				string s = f.ReadToEnd();
				f.Close();
				s = s.Replace("\r","\n");
				s = s.Replace("\n\n","\n");
				string[] sa = s.Split('\n');

				string units = "";
				bool valid = true;
				Label error = ((Label)Units.FindControl("error"));
				error.Text = "";

				foreach(string a in sa)
				{
					string email = a.Split('\t')[0].Replace("'","").Trim();
					if(email != "Email" && email != "")
					{
						if(!isEmail(email))
						{
							valid = false;
							error.Text += "Error: Invalid email-address \"" + email + "\"<BR/>";
						}
						if(a.Split('\t').Length > 1)
						{
							string unitID = a.Split('\t')[1].Replace("'","").ToLower().Trim();
							if(unitID.Length > 15)
							{
								unitID = unitID.Substring(0,15);
							}
//							if(unitID != "" && units.IndexOf(unitID) >= 0)
//							{
								units += (units != "" ? "," : "") + unitID + "";
//							}
						}
					}
				}

				System.Collections.Hashtable existingUnits = new System.Collections.Hashtable();
				SqlDataReader rs = Db.sqlRecordSet("SELECT ID, ProjectRoundUnitID FROM ProjectRoundUnit WHERE ID IS NOT NULL AND ProjectRoundID = " + projectRoundID);
				while(rs.Read())
				{
					existingUnits.Add(rs.GetString(0).ToLower().Trim(),rs.GetInt32(1));
				}
				rs.Close();

				foreach(string u in units.Split(','))
				{
					if(!existingUnits.Contains(u) && u != "")
					{
						valid = false;
						error.Text += "Error: Unit with external ID \"" + u + "\" does not exist<BR/>";
					}
				}

				System.Collections.ArrayList extra = new System.Collections.ArrayList();
				rs = Db.sqlRecordSet("SELECT QuestionID, OptionID FROM ProjectRoundQO WHERE ProjectRoundID = " + projectRoundID + " ORDER BY SortOrder");
				while(rs.Read())
				{
					extra.Add("" + rs.GetInt32(0) + "," + rs.GetInt32(1));
				}
				rs.Close();

				if(valid)
				{
					foreach(string a in sa)
					{
						string[] u = a.Split('\t');
						string email = u[0].Replace("'","").Trim();

						if(email != "Email" && email != "")
						{
							string unit = ((DropDownList)Units.FindControl("ProjectRoundUnitID")).SelectedValue;
							if(unit == "0")
								unit = "NULL";

							if(u.Length > 1)
							{
								string unitID = u[1].Replace("'","").ToLower().Trim();
								if(unitID.Length > 15)
								{
									unitID = unitID.Substring(0,15);
								}
								if(unitID != "")
								{
									unit = existingUnits[unitID].ToString();
								}
							}
							
							int uid = Db.getInt32("SET NOCOUNT ON;SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;BEGIN TRAN;INSERT INTO ProjectRoundUser (" +
								"ProjectRoundID," +
								"ProjectRoundUnitID," +
								"Email," +
								"Name," +
								"Extended," +
								"Extra," +
								"ExternalID" +
								") VALUES (" +
								"" + projectRoundID + "," +
								"" + unit + "," +
								"'" + email + "'," +
								(u.Length > 2 && u[2] != "" ? "'" + u[2].Replace("'","") + "'" : "NULL") + "," +
								(u.Length > 3 && u[3] != "" ? "" + u[3].Replace("'","") + "" : "NULL") + "," +
								(u.Length > 4 && u[4] != "" ? "'" + u[4].Replace("'","") + "'" : "NULL") + "," +
								(u.Length > 5 && u[5] != "" ? "" + u[5].Replace("'","") + "" : "NULL") + "" +
								");SELECT ProjectRoundUserID FROM [ProjectRoundUser] WHERE ProjectRoundID=" + projectRoundID + " AND Email = '" + email + "' ORDER BY ProjectRoundUserID DESC;COMMIT;");
                            
							if(extra.Count > 0)
							{
								if(u.Length > 6 && u[6] != "" && extra[0].ToString() != "")
								{
									Db.execute("INSERT INTO ProjectRoundUserQO (ProjectRoundUserID,QuestionID,OptionID,Answer) VALUES (" + uid + "," + extra[0] + ",'" + u[6] + "')");
								}
								if(u.Length > 7 && u[7] != "" && extra[1].ToString() != "")
								{
									Db.execute("INSERT INTO ProjectRoundUserQO (ProjectRoundUserID,QuestionID,OptionID,Answer) VALUES (" + uid + "," + extra[1] + ",'" + u[7] + "')");
								}
								if(u.Length > 8 && u[8] != "" && extra[2].ToString() != "")
								{
									Db.execute("INSERT INTO ProjectRoundUserQO (ProjectRoundUserID,QuestionID,OptionID,Answer) VALUES (" + uid + "," + extra[2] + ",'" + u[8] + "')");
								}
							}
						}
					}

					HttpContext.Current.Response.Redirect("projectSetup.aspx?ProjectRoundID=" + projectRoundID + "&ProjectID=" + projectID, true);
				}
			}
		}

		private void ExportUser_Click(object sender, EventArgs e)
		{
			exportUser = true;
		}

		private void CancelRound_Click(object sender, EventArgs e)
		{
			HttpContext.Current.Response.Redirect("projectSetup.aspx?ProjectID=" + projectID, true);
		}

		private void updateSendTexts(int langID, string invSub, string invBody, string remSub, string remBody, string EinvSub, string EinvBody, string EremSub, string EremBody)
		{
			try
			{
				((HtmlInputHidden)Units.FindControl("SendLang")).Value = langID.ToString();
			}
			catch(Exception) {}

			DropDownList ddl = null;
			try
			{
				ddl = ((DropDownList)Units.FindControl("STID"));
			}
			catch(Exception) {}

			try
			{
				((TextBox)Units.FindControl("Subject" + langID)).Text = (ddl.SelectedValue == "0" ? invSub : remSub);
			}
			catch(Exception) {}
			try
			{
				((TextBox)Units.FindControl("Body" + langID)).Text = (ddl.SelectedValue == "0" ? invBody : remBody);
			}
			catch(Exception) {}
			try
			{
				((TextBox)Units.FindControl("ExtraSubject" + langID)).Text = (ddl.SelectedValue == "0" ? EinvSub : EremSub);
			}
			catch(Exception) {}
			try
			{
				((TextBox)Units.FindControl("ExtraBody" + langID)).Text = (ddl.SelectedValue == "0" ? EinvBody : EremBody);
			}
			catch(Exception) {}
		}

		private void SendTypeID_SelectedIndexChanged(object sender, EventArgs e)
		{
			SqlDataReader rs = Db.sqlRecordSet("SELECT " +
				"l.LangID, " +
				"ISNULL(prl.InvitationSubjectJapaneseUnicode,prl.InvitationSubject), " +
				"ISNULL(prl.InvitationBodyJapaneseUnicode,prl.InvitationBody), " +
				"ISNULL(prl.ReminderSubjectJapaneseUnicode,prl.ReminderSubject), " +
				"ISNULL(prl.ReminderBodyJapaneseUnicode,prl.ReminderBody), " +
				"ISNULL(prl.ExtraInvitationSubjectJapaneseUnicode,prl.ExtraInvitationSubject), " +
				"ISNULL(prl.ExtraInvitationBodyJapaneseUnicode,prl.ExtraInvitationBody), " +
				"ISNULL(prl.ExtraReminderSubjectJapaneseUnicode,prl.ExtraReminderSubject), " +
				"ISNULL(prl.ExtraReminderBodyJapaneseUnicode,prl.ExtraReminderBody) " +
				"FROM Lang l " +
				"LEFT OUTER JOIN ProjectRoundLang prl ON l.LangID = prl.LangID AND prl.ProjectRoundID = " + projectRoundID);
			while(rs.Read())
			{
				updateSendTexts(
					rs.GetInt32(0),
					(rs.IsDBNull(1) ? "" : rs.GetString(1)),(rs.IsDBNull(2) ? "" : rs.GetString(2)),
					(rs.IsDBNull(3) ? "" : rs.GetString(3)),(rs.IsDBNull(4) ? "" : rs.GetString(4)),
					(rs.IsDBNull(5) ? "" : rs.GetString(5)),(rs.IsDBNull(6) ? "" : rs.GetString(6)),
					(rs.IsDBNull(7) ? "" : rs.GetString(7)),(rs.IsDBNull(8) ? "" : rs.GetString(8))
					);;
			}
			rs.Close();
		}

		private void Send_Click(object sender, EventArgs e)
		{
			string fromEmail = ((TextBox)Units.FindControl("SEFA")).Text;
			if(isEmail(fromEmail))
			{
				string sql = "";
				string sendURL = appURL;
				SqlDataReader rs = Db.sqlRecordSet("SELECT AppURL FROM Project WHERE ProjectID = " + projectID);
				if(rs.Read() && !rs.IsDBNull(0))
				{
					sendURL = rs.GetString(0);
				}
				rs.Close();

				if(HttpContext.Current.Request.QueryString["SendUnit"] != null)
				{
					rs = Db.sqlRecordSet("SELECT SortString FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + HttpContext.Current.Request.QueryString["SendUnit"]);
					if(rs.Read())
					{
						sql += "LEFT(r.SortString," + rs.GetString(0).Length + ") = '" + rs.GetString(0) + "'";
					}
					rs.Close();
				}
				else if(HttpContext.Current.Request.QueryString["SendRound"] != null)
				{
					sql += "r.ProjectRoundID = " + projectRoundID;
				}
				else if(HttpContext.Current.Request.QueryString["SendUser"] != null)
				{
					sql += "u.ProjectRoundUserID = " + HttpContext.Current.Request.QueryString["SendUser"];
				}

				if(((RadioButtonList)Rounds.FindControl("RepeatedEntry")).SelectedValue == "0")
				{
					sql += " AND u.ProjectRoundUserID NOT IN (SELECT a.ProjectRoundUserID FROM Answer a WHERE a.ProjectRoundUserID = u.ProjectRoundUserID AND a.EndDT IS NOT NULL)";
				}

				if(HttpContext.Current.Request.QueryString["SendUser"] == null)
				{
					if(((DropDownList)Units.FindControl("STID")).SelectedValue == "0")
					{
						sql += " AND u.LastSent IS NULL";
					}
					else
					{
						sql += " AND u.LastSent IS NOT NULL AND DATEADD(dd," + ((TextBox)Rounds.FindControl("MinRemInt")).Text + ",u.LastSent) < GETDATE()";
					}
				}
				sql += " AND u.NoSend IS NULL AND u.Terminated IS NULL";

				rs = Db.sqlRecordSet("SELECT " +
					"u.ProjectRoundUserID, " +
					"u.UserKey, " +
					"u.Email, " +
					"dbo.cf_unitLangID(r.ProjectRoundUnitID) AS LangID, " +
					"u.Extended " +
					"FROM ProjectRoundUnit r " +
					"INNER JOIN ProjectRoundUser u ON r.ProjectRoundUnitID = u.ProjectRoundUnitID " +
					"WHERE " + sql);
				while(rs.Read())
				{
					string body = (rs.IsDBNull(4) ? ((TextBox)Units.FindControl("Body" + rs.GetInt32(3))).Text.Replace("\r\n","\n").Replace("\n\r","\n").Replace("\n","\r\n") : ((TextBox)Units.FindControl("ExtraBody" + rs.GetInt32(3))).Text.Replace("\r\n","\n").Replace("\n\r","\n").Replace("\n","\r\n"));
					string link = sendURL + "/submit.aspx?K=" + rs.GetGuid(1).ToString().Substring(0,8) + rs.GetInt32(0).ToString();
					if(body.IndexOf("<LINK>") >= 0)
					{
						body = body.Replace("<LINK>",link);
					}
					else
					{
						body = body + "\r\n\r\n" + link;
					}
					string subject = (rs.IsDBNull(4) ? ((TextBox)Units.FindControl("Subject" + rs.GetInt32(3))).Text : ((TextBox)Units.FindControl("ExtraSubject" + rs.GetInt32(3))).Text);
					
					int langID = Convert.ToInt32(((HtmlInputHidden)Units.FindControl("SendLang")).Value);
					DateTime sent = DateTime.MinValue;
					string ex = "";
					int sendType = Convert.ToInt32(((DropDownList)Units.FindControl("STID")).SelectedValue);

					if(langID == 3)
					{
						sent = DateTime.Now;

						ex = sendMail(rs.GetString(2).Replace("'",""),fromEmail.Replace("'",""),subject,body,System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"]);

						switch(sendType)
						{
							case 0:
								Db.sqlExecute("UPDATE ProjectRoundUser SET LastSent = GETDATE(), SendCount = SendCount + 1 WHERE ProjectRoundUserID = " + rs.GetInt32(0));
								break;
							case 1:
								Db.sqlExecute("UPDATE ProjectRoundUser SET LastSent = GETDATE(), ReminderCount = ReminderCount + 1 WHERE ProjectRoundUserID = " + rs.GetInt32(0));
								break;
						}
					}

					Db.execute("INSERT INTO MailQueue (" +
						"ProjectRoundUserID, " +
						"AdrTo, " +
						"AdrFrom, " +
						"Subject, " +
						"Body, " +
						"SendType, " +
						"SubjectJapaneseUnicode, " +
						"BodyJapaneseUnicode," +
						"LangID," +
						"Sent," +
						"ErrorDescription" +
						") VALUES (" +
						"" + rs.GetInt32(0) + "," +
						"'" + rs.GetString(2).Replace("'","") + "'," +
						"'" + fromEmail.Replace("'","") + "'," +
						"'" + subject.Replace("'","''") + "'," +
						"'" + body.Replace("'","''") + "'," +
						"" + sendType + "," +
						"N'" + subject.Replace("'","''") + "'," +
						"N'" + body.Replace("'","''") + "'," +
						"" + langID + "," +
						(sent > DateTime.MinValue ? "GETDATE()" : "NULL") + "," +
						(ex != "" ? "'" + ex.Replace("'","''") + "'" : "NULL") +
						")");
				}
				rs.Close();

				HttpContext.Current.Response.Redirect("mailQueue.aspx", true);
			}
			else
			{
				Units.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"2\"><TABLE BORDER=\"0\"><TR><TD>Failure: Bad from-address&nbsp;</TD></TR></TABLE></TD></TR>"));
			}
		}

		public static string sendMail(string to, string from, string subject, string body, string smtpServer)
		{
			string ex = "";

			try
			{
				System.Text.Encoding enc = System.Text.Encoding.UTF8;

				System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
				msg.To = to.Replace(" ","").Replace(",",";");
				msg.From = from.Replace(" ","").Replace(",",";");
			
				string ss = "";
				while (subject.Length > 0)
				{
					string x = subject.Substring(0, Math.Min(subject.Length, 16));
					ss += (ss != "" ? " " : "") + "=?" + enc.HeaderName + "?B?" + Convert.ToBase64String(enc.GetBytes(x)) + "?=";
					subject = subject.Substring(Math.Min(subject.Length, 16));
				}
				msg.Subject = ss;

				msg.BodyFormat = System.Web.Mail.MailFormat.Text;
				msg.BodyEncoding = enc;
				msg.Body = body;
			
				System.Web.Mail.SmtpMail.SmtpServer = smtpServer;
				System.Web.Mail.SmtpMail.Send(msg);
			}
			catch(Exception err){ex = err.Message;}

			return ex;
		}

		private void ExportData_Click(object sender, EventArgs e)
		{
			exportData = true;
		}

		private void ExportSpssData_Click(object sender, EventArgs e)
		{
			exportSpssData = true;
		}

		private void UploadData_Click(object sender, EventArgs e)
		{
			HtmlInputFile import = ((HtmlInputFile)Units.FindControl("import"));
			if(import.PostedFile != null && import.PostedFile.ContentLength != 0)
			{
				string filename = DateTime.Now.Ticks + ".txt";
				import.PostedFile.SaveAs(HttpContext.Current.Server.MapPath("tmp\\" + filename));
				((HtmlInputHidden)Units.FindControl("hiddenImport")).Value = filename;

				readImportHeader();
			}
			else
			{
				((PlaceHolder)Units.FindControl("Units")).Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"2\" STYLE=\"color:#CC0000;\">Uploaded import file reference is missing or file is empty!</TD></TR>"));
			}
		}

		private void readImportHeader()
		{
			PlaceHolder Mapping = ((PlaceHolder)Units.FindControl("Mapping"));

			System.IO.StreamReader f = System.IO.File.OpenText(HttpContext.Current.Server.MapPath("tmp\\" + ((HtmlInputHidden)Units.FindControl("hiddenImport")).Value));
			string firstRow = f.ReadLine();
			f.Close();
			string[] firstRowElements = firstRow.Split('\t');
			
			DropDownList var = ((DropDownList)Mapping.FindControl("colUnit"));
			var.Items.Clear();
			var.Items.Add(new ListItem("< ignore, use default >","-1"));
			int cx = 0;
			foreach(string s in firstRowElements)
			{
				var.Items.Add(new ListItem(s,cx.ToString()));
				if(s.ToUpper() == "UNIT")
				{
					var.SelectedValue = cx.ToString();
				}
				cx++;
			}

			var = ((DropDownList)Mapping.FindControl("colExtID"));
			var.Items.Clear();
			var.Items.Add(new ListItem("< ignore, anon import >","-1"));
			cx = 0;
			foreach(string s in firstRowElements)
			{
				var.Items.Add(new ListItem(s,cx.ToString()));
				if(s.ToUpper() == "EXTID")
				{
					var.SelectedValue = cx.ToString();
				}
				cx++;
			}

			var = ((DropDownList)Mapping.FindControl("colName"));
			var.Items.Clear();
			var.Items.Add(new ListItem("< ignore, anon import >","-1"));
			cx = 0;
			foreach(string s in firstRowElements)
			{
				var.Items.Add(new ListItem(s,cx.ToString()));
				if(s.ToUpper() == "NAME")
				{
					var.SelectedValue = cx.ToString();
				}
				cx++;
			}

			string sql = "SELECT DISTINCT " +
								"sq.QuestionID, " +		// 0
								"qo.OptionID, " +		// 1
								"o.OptionType, " +		// 2
								"sq.Variablename, " +	// 3
								"sqo.Variablename " +	// 4
								"FROM SurveyQuestion sq " +
								"INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " + 
								"INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " + 
								"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " + 
								"INNER JOIN ProjectRound pr ON pr.SurveyID = sq.SurveyID " +
								"WHERE pr.ProjectRoundID = " + HttpContext.Current.Request.QueryString["ProjectRoundID"] + " " +
								"ORDER BY sq.QuestionID, qo.OptionID";
			//HttpContext.Current.Response.Write(sql);
			SqlDataReader rs = Db.sqlRecordSet(sql);
			while(rs.Read())
			{
				string varname = (rs.IsDBNull(3) || rs.GetString(3) == "" ? "Q" + rs.GetInt32(0) + (rs.IsDBNull(4) || rs.GetString(4) == "" ? "O" + rs.GetInt32(1) : "") : rs.GetString(3) + (rs.IsDBNull(4) || rs.GetString(4) == "" ? "" : rs.GetString(4)));

				var = ((DropDownList)Mapping.FindControl("colQ" + rs.GetInt32(0) + "O" + rs.GetInt32(1)));
				var.Items.Clear();
				var.Items.Add(new ListItem("< ignore >","-1"));
				cx = 0;
				foreach(string s in firstRowElements)
				{
					var.Items.Add(new ListItem(s,cx.ToString()));
					if(s.ToUpper() == varname.ToUpper())
					{
						var.SelectedValue = cx.ToString();
					}
					cx++;
				}
			}
			rs.Close();

			Mapping.Visible = true;
		}

		private void ExecImport_Click(object sender, EventArgs e)
		{
			PlaceHolder Mapping = ((PlaceHolder)Units.FindControl("Mapping"));

			int ProjectRoundUnitID = Convert.ToInt32(((DropDownList)Units.FindControl("ProjectRoundUnitID")).SelectedValue);

			System.Collections.Hashtable mapVar = new System.Collections.Hashtable();
			System.Collections.Hashtable mapOpt = new System.Collections.Hashtable();
			System.Collections.Hashtable mapUnit = new System.Collections.Hashtable();

			SqlDataReader rs = Db.sqlRecordSet("SELECT DISTINCT " +
				"sq.QuestionID, " +		// 0
				"qo.OptionID, " +		// 1
				"o.OptionType, " +		// 2
				"sq.Variablename, " +	// 3
				"sqo.Variablename " +	// 4
				"FROM SurveyQuestion sq " +
				"INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " + 
				"INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " + 
				"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " + 
				"INNER JOIN ProjectRound pr ON pr.SurveyID = sq.SurveyID " +
				"WHERE pr.ProjectRoundID = " + HttpContext.Current.Request.QueryString["ProjectRoundID"] + " " +
				"ORDER BY sq.QuestionID, qo.OptionID");
			while(rs.Read())
			{
				string qo = ((DropDownList)Mapping.FindControl("colQ" + rs.GetInt32(0) + "O" + rs.GetInt32(1))).SelectedValue;

				if(qo != "-1")
				{
					mapVar.Add("Q" + rs.GetInt32(0) + "O" + rs.GetInt32(1),qo);
					if(rs.GetInt32(2) == 1)
					{
						SqlDataReader rs2 = Db.sqlRecordSet("SELECT ocs.OptionComponentID FROM OptionComponents ocs WHERE ocs.OptionID = " + rs.GetInt32(1) + " ORDER BY ocs.SortOrder");
						while(rs2.Read())
						{
							string o = ((TextBox)Mapping.FindControl("Q" + rs.GetInt32(0) + "O" + rs.GetInt32(1) + "C" + rs2.GetInt32(0))).Text;
							if(o != "")
							{
								mapOpt.Add("Q" + rs.GetInt32(0) + "O" + rs.GetInt32(1) + "C" + rs2.GetInt32(0),o);
							}
						}
						rs2.Close();
					}
				}
			}
			rs.Close();

			rs = Db.sqlRecordSet("SELECT ProjectRoundUnitID, ID FROM ProjectRoundUnit WHERE ProjectRoundID = " + HttpContext.Current.Request.QueryString["ProjectRoundID"]);
			while(rs.Read())
			{
				mapUnit.Add(rs.GetString(1),rs.GetInt32(0));
			}
			rs.Close();
			
			System.IO.StreamReader f = System.IO.File.OpenText(HttpContext.Current.Server.MapPath("tmp\\" + ((HtmlInputHidden)Units.FindControl("hiddenImport")).Value));
			string content = f.ReadLine();
			content = f.ReadToEnd();
			f.Close();

			content = content.Replace("\r","\n").Replace("\n\n","\n");
			foreach(string row in content.Split('\n'))
			{
				int atPos;

				string[] col = row.Split('\t');
				int projectRoundUnitID = ProjectRoundUnitID;

				string unit = ((DropDownList)Mapping.FindControl("colUnit")).SelectedValue;
				if(unit != "-1")
				{
					atPos = Convert.ToInt32(unit);
					if(col.Length > atPos)
					{
						if(mapUnit.Contains(col[atPos]))
						{
							projectRoundUnitID = Convert.ToInt32(mapUnit[col[atPos]]);
						}
					}
				}
				
				if(projectRoundUnitID != 0)
				{
					string extID = "NULL", name = "NULL";

					string tmp = ((DropDownList)Mapping.FindControl("colExtID")).SelectedValue;
					if(tmp != "-1")
					{
						atPos = Convert.ToInt32(tmp);
						if(col.Length > atPos)
						{
							if(col[atPos].Trim() != "")
							{
								extID = col[atPos].Trim();
							}
						}
					}
					tmp = ((DropDownList)Mapping.FindControl("colName")).SelectedValue;
					if(tmp != "-1")
					{
						atPos = Convert.ToInt32(tmp);
						if(col.Length > atPos)
						{
							if(col[atPos].Trim() != "")
							{
								name = "'" + col[atPos].Trim().Replace("'","") + "'";
							}
						}
					}

					if(extID != "NULL" || name != "NULL")
					{
						int uid = Db.getInt32("SET NOCOUNT ON;SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;BEGIN TRAN;INSERT INTO ProjectRoundUser (" +
							"ProjectRoundID," +
							"ProjectRoundUnitID," +
							"Name," +
							"ExternalID" +
							") VALUES (" +
							"" + HttpContext.Current.Request.QueryString["ProjectRoundID"] + "," + 
							"" + projectRoundUnitID + "," +
							"" + name + "," +
							"" + extID + "" +
							");SELECT ProjectRoundUserID FROM [ProjectRoundUser] WHERE ProjectRoundUnitID=" + projectRoundUnitID + " ORDER BY ProjectRoundUserID DESC;COMMIT;");
                        Db.execute("UPDATE ProjectRoundUser SET Email = 'anon" + uid + "@eform.se' WHERE ProjectRoundUserID = " + uid);
						Db.execute("INSERT INTO Answer (ProjectRoundUserID,ProjectRoundID,ProjectRoundUnitID,StartDT,EndDT) VALUES (" + uid + "," + HttpContext.Current.Request.QueryString["ProjectRoundID"] + "," + projectRoundUnitID + ",GETDATE(),GETDATE())");
					}
					else
					{
						Db.execute("INSERT INTO Answer (ProjectRoundID,ProjectRoundUnitID,StartDT,EndDT) VALUES (" + HttpContext.Current.Request.QueryString["ProjectRoundID"] + "," + projectRoundUnitID + ",GETDATE(),GETDATE())");
					}
					rs = Db.sqlRecordSet("SELECT TOP 1 AnswerID FROM Answer ORDER BY AnswerID DESC");
					if(rs.Read())
					{
						foreach(DictionaryEntry i in mapVar) 
						{
							int oStart = i.Key.ToString().IndexOf("O");
							bool found = false;
							if(col.Length > Convert.ToInt32(i.Value))
							{
								string val = col[Convert.ToInt32(i.Value)];
								foreach(DictionaryEntry v in mapOpt)
								{
									int cStart = v.Key.ToString().IndexOf("C");
									if(v.Key.ToString().IndexOf(i.Key + "C") >= 0 && v.Value.ToString() == val)
									{
										found = true;
										Db.execute("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueInt,CreatedDateTime,CreatedSessionID) VALUES (" + rs.GetInt32(0) + "," + i.Key.ToString().Substring(1,oStart-1) + "," + i.Key.ToString().Substring(oStart+1) + "," + v.Key.ToString().Substring(cStart+1) + ",GETDATE(),0)");
										break;
									}
								}
							}
							if(!found)
							{
								if(col.Length > Convert.ToInt32(i.Value))
								{
									string val = col[Convert.ToInt32(i.Value)];
									if(val != "")
									{
										Db.execute("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueText,CreatedDateTime,CreatedSessionID) VALUES (" + rs.GetInt32(0) + "," + i.Key.ToString().Substring(1,oStart-1) + "," + i.Key.ToString().Substring(oStart+1) + ",'" + val + "',GETDATE(),0)");
									}
								}
							}
						}
					}
					rs.Close();
				}
			}
		}

		public static int createUnit(int userCount, int langID, int surveyID, int projectRoundID, string unitName, string parentProjectRoundUnitID, string extID)
		{
			int projectRoundUnitID = Db.getInt32("SET NOCOUNT ON;" +
				"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;" +
				"BEGIN TRAN;" +
				"INSERT INTO ProjectRoundUnit (" +
				"UserCount," +
				"LangID," +
				"SurveyID," +
				"ProjectRoundID," +
				"Unit," +
				"ParentProjectRoundUnitID" +
				") VALUES (" +
				"" + userCount + "," +
				"" + langID + "," +
				"" + surveyID + "," +
				"" + projectRoundID + "," +
				"'" + unitName.Replace("'","") + "'," +
				"" + parentProjectRoundUnitID + "" +
				");" +
				"SELECT ProjectRoundUnitID " +
				"FROM [ProjectRoundUnit] " +
				"WHERE ProjectRoundID=" + projectRoundID + " " +
				"AND Unit = '" + unitName.Replace("'","") + "' " +
				"ORDER BY ProjectRoundUnitID DESC;" +
				"COMMIT;");
			Db.execute("UPDATE ProjectRoundUnit SET " +
				"ID = dbo.cf_unitExtID(" + projectRoundUnitID + ",dbo.cf_unitDepth(" + projectRoundUnitID + "),'" + extID + "'), " +
				"SortOrder = " + projectRoundUnitID + " " +
				"WHERE ProjectRoundUnitID = " + projectRoundUnitID);

			updateUnits(projectRoundID);

			return projectRoundUnitID;
		}

		public static void updateUnit(int projectRoundUnitID, int projectRoundID, string parentProjectRoundUnitID, string extID, int userCount, int langID, int surveyID, string unit)
		{
			bool parentValid = true;
			if(parentProjectRoundUnitID != "NULL")
			{
				SqlDataReader rs = Db.sqlRecordSet("SELECT " +
					"pru.SortString, " +
					"prup.SortString " +
					"FROM ProjectRoundUnit pru " +
					"LEFT OUTER JOIN ProjectRoundUnit prup ON " + parentProjectRoundUnitID + " = prup.ProjectRoundUnitID " +
					"WHERE pru.ProjectRoundUnitID = " + projectRoundUnitID);
				if(rs.Read() && !rs.IsDBNull(1))
				{
					if(rs.GetString(0).Length <= rs.GetString(1).Length && rs.GetString(1).Substring(0,rs.GetString(0).Length) == rs.GetString(0))
					{
						parentValid = false;
					}
				}
				rs.Close();
			}
			Db.execute("UPDATE ProjectRoundUnit SET " +
				(extID != null ? "ID = dbo.cf_unitExtID(" + projectRoundUnitID + ",dbo.cf_unitDepth(" + projectRoundUnitID + "),'" + extID + "'), " : "") +
				(userCount != -1 ? "UserCount = " + userCount + ", " : "") +
				(langID != -1 ? "LangID = " + langID + ", " : "") +
				(surveyID != -1 ? "SurveyID = " + surveyID + ", " : "") +
				(unit != null ? "Unit = '" + unit.Replace("'","") + "', " : "") +
				(parentValid ? "ParentProjectRoundUnitID = " + parentProjectRoundUnitID + ", " : "") +
				"SortOrder=SortOrder " +
				"WHERE ProjectRoundUnitID = " + projectRoundUnitID);

			updateUnits(projectRoundID);
		}

		public static void updateUnits(int projectRoundID)
		{
			Db.execute("UPDATE ProjectRoundUnit SET SortString = dbo.cf_unitSortString(ProjectRoundUnitID) WHERE ProjectRoundID = " + projectRoundID);
		}

		private void ExportSpssDataOne_Click(object sender, EventArgs e)
		{
			exportSpssDataOne = true;
		}

		private void ExportDataOne_Click(object sender, EventArgs e)
		{
			exportDataOne = true;
		}

		private void AddPRQO_Click(object sender, EventArgs e)
		{
			Db.execute("INSERT INTO ProjectRoundQO (ProjectRoundID) VALUES (" + projectRoundID + ")");
			SqlDataReader rs = Db.sqlRecordSet("SELECT TOP 1 ProjectRoundQOID FROM ProjectRoundQO WHERE ProjectRoundID = " + projectRoundID + " ORDER BY ProjectRoundQOID DESC");
			if(rs.Read())
			{
				Db.execute("UPDATE ProjectRoundQO SET SortOrder = " + rs.GetInt32(0) + " WHERE ProjectRoundQOID = " + rs.GetInt32(0));
			}
			rs.Close();
			HttpContext.Current.Response.Redirect("projectSetup.aspx?ProjectRoundID=" + projectRoundID + "&ProjectID=" + projectID, true);
		}

		//private void ExportSpssDataOneNew_Click(object sender, EventArgs e)
		//{
		//	exportSpssDataOneNew = true;
		//}
	}
}
