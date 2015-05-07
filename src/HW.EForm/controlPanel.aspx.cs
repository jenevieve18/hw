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
	/// Summary description for controlPanel.
	/// </summary>
	public class controlPanel : System.Web.UI.Page
	{
		protected PlaceHolder Send;
		protected TextBox Message;
		protected TextBox FromEmail;
		protected TextBox Subject;
		protected Label SendTo;

		protected TextBox Timeframe;
		protected TextBox SurveyIntro;
		protected TextBox Yellow;
		protected TextBox Green;
		protected PlaceHolder Settings;
		protected PlaceHolder ChangeUser;
		protected PlaceHolder ChangeUnit;
		protected HtmlInputHidden ChangeMode;
		protected HtmlInputHidden SaveSettings;
		protected Label Change;
		protected DropDownList YearLow;
		protected DropDownList MonthLow;
		protected DropDownList DayLow;
		protected DropDownList YearHigh;
		protected DropDownList MonthHigh;
		protected DropDownList DayHigh;
		protected Label Org;
		protected Label Intro;
		protected Label List;
		protected Label OrgStatus;
		protected Label OrgAnswer;
		protected HtmlInputHidden ProjectRoundUnitID;
		protected HtmlInputHidden UserOrgProjectRoundUnitID;
		protected HtmlInputHidden MoveTarget;
		protected HtmlInputHidden MoveObject;
		protected HtmlInputHidden MoveData;
		protected HtmlInputHidden Action;
		protected HtmlInputHidden SaveAction;
		protected HtmlInputHidden ProjectRoundUserID;
		protected TextBox UserEmail;
		protected TextBox UserName;
		protected DropDownList UserProjectRoundUnitID;
		protected DropDownList UserCategoryID;
		protected Label ErrorEmail;
		protected HtmlInputHidden UnitProjectRoundUnitID;
		protected TextBox UnitName;
		protected DropDownList UnitParentProjectRoundUnitID;
		protected Label logo;
		protected Label Powered;

		int days = 30;
		int LID = 0;
		int PID = 0;
		int PRID = 0;
		int managerID = 0;
		int masterPRUID = 0;
		string allPRUID = "0";
		string myPRUID = "0";
		bool showCompleteOrg = false;
		int transparencyLevel = 0;

		private void Page_Load(object sender, System.EventArgs e)
		{
			OdbcDataReader rs;

			loadAccess();

			if(!IsPostBack)
			{
				//SurveyIntro.BorderStyle = BorderStyle.None;
				//SurveyIntro.BorderWidth = Unit.Pixel(0);
				SurveyIntro.Width = Unit.Pixel(400);
				SurveyIntro.TextMode = TextBoxMode.MultiLine;
				SurveyIntro.Rows = 5;
				SurveyIntro.CssClass = "cp_11px";
				SurveyIntro.Attributes["style"] += "overflow:hidden;background-color:#f2f2f2;";

				Message.Width = Unit.Pixel(320);
				Message.TextMode = TextBoxMode.MultiLine;
				Message.Rows = 8;
				Message.CssClass = "cp_11px";
				Message.Attributes["style"] += "overflow:hidden;background-color:#f2f2f2;";

				Subject.Width = Unit.Pixel(390);
				Subject.CssClass = "cp_11px";
				Subject.Attributes["style"] += "background-color:#f2f2f2;";

				FromEmail.Width = Unit.Pixel(300);
				FromEmail.CssClass = "cp_11px";
				FromEmail.Attributes["style"] += "background-color:#f2f2f2;";

				//UserEmail.BorderStyle = BorderStyle.None;
				//UserEmail.BorderWidth = Unit.Pixel(0);
				UserEmail.Width = Unit.Pixel(250);
				UserEmail.CssClass = "cp_11px";
				UserEmail.Attributes["style"] += "background-color:#f2f2f2;";

				UserName.Width = Unit.Pixel(250);
				UserName.CssClass = "cp_11px";
				UserName.Attributes["style"] += "background-color:#f2f2f2;";

				UserProjectRoundUnitID.Width = Unit.Pixel(420);
				UserProjectRoundUnitID.CssClass = "cp_11px";
				UserProjectRoundUnitID.Attributes["style"] += "background-color:#f2f2f2;";

				UserCategoryID.Width = Unit.Pixel(360);
				UserCategoryID.CssClass = "cp_11px";
				UserCategoryID.Attributes["style"] += "background-color:#f2f2f2;";
				UserCategoryID.Items.Add(new ListItem("< välj >","NULL"));
				rs = Db.recordSet("SELECT uc.UserCategoryID, ucl.Category FROM ProjectUserCategory puc INNER JOIN UserCategory uc ON puc.UserCategoryID = uc.UserCategoryID INNER JOIN UserCategoryLang ucl ON uc.UserCategoryID = ucl.UserCategoryID AND ucl.LangID = " + LID + " WHERE puc.ProjectID = " + PID + " ORDER BY ucl.Category");
				while(rs.Read())
				{
					UserCategoryID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				rs.Close();

				UnitName.Width = Unit.Pixel(250);
				UnitName.CssClass = "cp_11px";
				UnitName.Attributes["style"] += "background-color:#f2f2f2;";

				UnitParentProjectRoundUnitID.Width = Unit.Pixel(380);
				UnitParentProjectRoundUnitID.CssClass = "cp_11px";
				UnitParentProjectRoundUnitID.Attributes["style"] += "background-color:#f2f2f2;";

				Timeframe.BorderStyle = BorderStyle.None;
				Timeframe.BorderWidth = Unit.Pixel(0);
				Timeframe.Width = Unit.Pixel(20);
				Timeframe.CssClass = "cp_11px";
				Timeframe.Attributes["style"] += "background-color:#f2f2f2;";

				Yellow.BorderStyle = BorderStyle.None;
				Yellow.BorderWidth = Unit.Pixel(0);
				Yellow.Width = Unit.Pixel(20);
				Yellow.CssClass = "cp_11px";
				Yellow.Attributes["style"] += "background-color:#f2f2f2;";

				Green.BorderStyle = BorderStyle.None;
				Green.BorderWidth = Unit.Pixel(0);
				Green.Width = Unit.Pixel(20);
				Green.CssClass = "cp_11px";
				Green.Attributes["style"] += "background-color:#f2f2f2;";

				ChangeMode.Value = "0";

				for(int i=2006; i<=DateTime.Now.Year; i++)
				{
					YearLow.Items.Add(new ListItem(i.ToString(),i.ToString()));
					YearHigh.Items.Add(new ListItem(i.ToString(),i.ToString()));
				}
				for(int i=1; i<=12; i++)
				{
					MonthLow.Items.Add(new ListItem(i.ToString().PadLeft(2,'0'),i.ToString()));
					MonthHigh.Items.Add(new ListItem(i.ToString().PadLeft(2,'0'),i.ToString()));
				}
				for(int i=1; i<=31; i++)
				{
					DayLow.Items.Add(new ListItem(i.ToString().PadLeft(2,'0'),i.ToString()));
					DayHigh.Items.Add(new ListItem(i.ToString().PadLeft(2,'0'),i.ToString()));
				}
				YearLow.SelectedValue = DateTime.Now.AddDays(-days).Year.ToString();
				MonthLow.SelectedValue = DateTime.Now.AddDays(-days).Month.ToString();
				DayLow.SelectedValue = DateTime.Now.AddDays(-days).Day.ToString();
				YearHigh.SelectedValue = DateTime.Now.Year.ToString();
				MonthHigh.SelectedValue = DateTime.Now.Month.ToString();
				DayHigh.SelectedValue = DateTime.Now.Day.ToString();

				ProjectRoundUserID.Value = "0";
				UnitProjectRoundUnitID.Value = "0";
				MoveData.Value = "0";
				UserOrgProjectRoundUnitID.Value = "0";
				SaveSettings.Value = "0";

				loadUnits();
			}

			if(MoveTarget.Value != "" && MoveObject.Value != "")
			{
				if(MoveObject.Value.StartsWith("A"))
				{
					Db.execute("UPDATE Answer SET ProjectRoundUnitID = " + MoveTarget.Value + " WHERE AnswerID = " + MoveObject.Value.Substring(1));
				}
				else if(MoveObject.Value.StartsWith("U") || MoveObject.Value.StartsWith("X"))
				{
					string userID = MoveObject.Value.Substring(1);

					string oldUnitID = "NULL";
					rs = Db.recordSet("SELECT ProjectRoundUnitID FROM ProjectRoundUser WHERE ProjectRoundUserID = " + userID);
					if(rs.Read() && !rs.IsDBNull(0))
					{
						oldUnitID = rs.GetInt32(0).ToString();
					}
					rs.Close();
					if(oldUnitID != MoveTarget.Value)
					{
						if(MoveData.Value == "1")
						{
							Db.execute("UPDATE ProjectRoundUser SET ProjectRoundUnitID = " + MoveTarget.Value + " WHERE ProjectRoundUserID = " + userID);
							Db.execute("UPDATE Answer SET ProjectRoundUnitID = " + MoveTarget.Value + " WHERE ProjectRoundUserID = " + userID);
						}
						else
						{
							Db.execute("UPDATE ProjectRoundUser SET Created = GETDATE(), LastSent = NULL, SendCount = 0, ReminderCount = 0, ProjectRoundUnitID = " + MoveTarget.Value + " WHERE ProjectRoundUserID = " + userID);
							Db.execute("UPDATE Answer SET ProjectRoundUserID = NULL WHERE ProjectRoundUserID = " + userID);
						}
					}
					MoveData.Value = "0";
				}
				else
				{
					projectSetup.updateUnit(Convert.ToInt32(MoveObject.Value),PRID,MoveTarget.Value,null,-1,-1,-1,null);
				}

				loadUnits();

				MoveTarget.Value = "";
				MoveObject.Value = "";
			}
		}
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);

			OdbcDataReader rs;

			ErrorEmail.Text = "";

			if(SaveAction.Value == "1")
			{
				if(Action.Value == "6")
				{
					if(managerID != 0)
					{
						Db.execute("UPDATE ManagerProjectRound SET EmailSubject = '" + Subject.Text.Replace("'","''") + "', EmailBody = '" + Message.Text.Replace("'","''") + "' WHERE ManagerID = " + managerID + " AND ProjectRoundID = " + PRID);
					}
					if(UnitProjectRoundUnitID.Value != "0")
					{
						string ss = "";
						rs = Db.recordSet("SELECT SortString FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + UnitProjectRoundUnitID.Value);
						if(rs.Read())
						{
							ss = rs.GetString(0);
						}
						rs.Close();
						rs = Db.recordSet("SELECT " +
							"u.UserKey, " +
							"u.Email, " +
							"u.LastSent, " +
							"u.ProjectRoundUserID " +
							"FROM ProjectRoundUser u " +
							"INNER JOIN ProjectRoundUnit t ON u.ProjectRoundUnitID = t.ProjectRoundUnitID " +
							"WHERE u.Terminated IS NULL AND u.NoSend IS NULL AND LEFT(t.SortString," + ss.Length + ") = '" + ss + "' AND 0 = (SELECT COUNT(*) FROM Answer a WHERE a.EndDT IS NOT NULL AND a.EndDT >= DATEADD(d,-dbo.cf_unitTimeframe(t.ProjectRoundUnitID),GETDATE()) AND a.ProjectRoundUserID = u.ProjectRoundUserID)");
					}
					else
					{
						rs = Db.recordSet("SELECT " +
							"u.UserKey, " +
							"u.Email, " +
							"u.LastSent, " +
							"u.ProjectRoundUserID " +
							"FROM ProjectRoundUser u " +
							"WHERE u.Terminated IS NULL AND u.NoSend IS NULL AND u.ProjectRoundUserID = " + ProjectRoundUserID.Value);
					}
					while(rs.Read())
					{
						string body = Message.Text.Replace("\r\n","\n").Replace("\n\r","\n").Replace("\n","\r\n");
						string link = projectSetup.appURL + "/submit.aspx?K=" + rs.GetGuid(0).ToString().Substring(0,8) + rs.GetInt32(3);
						if(body.IndexOf("<LINK>") >= 0)
						{
							body = body.Replace("<LINK>",link);
						}
						else
						{
							body = body + "\r\n\r\n" + link;
						}

						Db.execute("INSERT INTO MailQueue (ProjectRoundUserID, AdrTo, AdrFrom, Subject, Body, SendType) VALUES (" + rs.GetInt32(3) + ",'" + rs.GetString(1).Replace("'","") + "','" + FromEmail.Text.Replace("'","") + "','" + Subject.Text.Replace("'","''") + "','" + body.Replace("'","''") + "'," + (rs.IsDBNull(2) ? "0" : "1") + ")");
					}
					rs.Close();

					Action.Value = "0";
				}
				else if(Action.Value == "1")
				{
					if(UnitProjectRoundUnitID.Value == "0")
					{
						int temp = projectSetup.createUnit(0,0,0,PRID,UnitName.Text,UnitParentProjectRoundUnitID.SelectedValue,"");
						if(managerID != 0)
						{
							Db.execute("INSERT INTO ManagerProjectRoundUnit (ManagerID,ProjectRoundID,ProjectRoundUnitID) VALUES (" + managerID + "," + PRID + "," + temp + ")");
						}
						loadAccess();
					}
					else
					{
						projectSetup.updateUnit(Convert.ToInt32(UnitProjectRoundUnitID.Value),PRID,UnitParentProjectRoundUnitID.SelectedValue,null,-1,-1,-1,UnitName.Text);
					}
					loadUnits();
					Action.Value = "0";
					UnitProjectRoundUnitID.Value = "0";
					UnitName.Text = "";
				}
				else if(Action.Value == "2")
				{
					string email = UserEmail.Text.ToLower().Trim().Replace("'","");
					if(projectSetup.isEmail(email))
					{
						string unitID = UserProjectRoundUnitID.SelectedValue;
						if(unitID == "0")
						{
							unitID = "NULL";
						}

						if(ProjectRoundUserID.Value == "0")
						{
							Db.execute("INSERT INTO ProjectRoundUser (ProjectRoundID,ProjectRoundUnitID,Email,Name,UserCategoryID) VALUES (" + PRID + "," + unitID + ",'" + email + "','" + UserName.Text.Replace("'","''") + "'," + UserCategoryID.SelectedValue + ")");
						}
						else
						{
							string oldUnitID = "NULL", oldEmail = "";
							rs = Db.recordSet("SELECT ProjectRoundUnitID, Email FROM ProjectRoundUser WHERE ProjectRoundUserID = " + ProjectRoundUserID.Value);
							if(rs.Read() && !rs.IsDBNull(0))
							{
								oldUnitID = rs.GetInt32(0).ToString();
								oldEmail = (!rs.IsDBNull(1) ? rs.GetString(1) : "").ToLower().Trim().Replace("'","");
							}
							rs.Close();
							Db.execute("UPDATE ProjectRoundUser SET " +
								"ProjectRoundUnitID = " + unitID + ", " +
								"Email = '" + email + "', " +
								"Name = '" + UserName.Text.Replace("'","''") + "', " +
								"UserCategoryID = " + UserCategoryID.SelectedValue + " " +
								"WHERE ProjectRoundUserID = " + ProjectRoundUserID.Value);
							if(email != oldEmail)
							{
								Db.execute("UPDATE ProjectRoundUser SET LastSent = NULL, SendCount = 0, ReminderCount = 0 WHERE ProjectRoundUserID = " + ProjectRoundUserID.Value);
							}
							if(oldUnitID != unitID)
							{
								if(MoveData.Value == "1")
								{
									Db.execute("UPDATE Answer SET ProjectRoundUnitID = " + unitID + " WHERE ProjectRoundUserID = " + ProjectRoundUserID.Value);
								}
								else
								{
									Db.execute("UPDATE ProjectRoundUser SET Created = GETDATE(), LastSent = NULL, SendCount = 0, ReminderCount = 0 WHERE ProjectRoundUserID = " + ProjectRoundUserID.Value);
									Db.execute("UPDATE Answer SET ProjectRoundUserID = NULL WHERE ProjectRoundUserID = " + ProjectRoundUserID.Value);
								}
							}
							else
							{
								Db.execute("UPDATE ProjectRoundUser SET ProjectRoundUnitID = " + unitID + ", Email = '" + email + "',Name = '" + UserName.Text.Replace("'","''") + "',UserCategoryID = " + UserCategoryID.SelectedValue + " WHERE ProjectRoundUserID = " + ProjectRoundUserID.Value);
							}
						}
						Action.Value = "0";
						ProjectRoundUserID.Value = "0";
						UserEmail.Text = "";
						UserName.Text = "";
					}
					else
					{
						ErrorEmail.Text = "<img alt=\"Felaktig e-postadress!\" src=\"img/warning.gif\">";
					}
				}
				SaveAction.Value = "0";
				MoveData.Value = "0";
			}

			if(Action.Value == "4")
			{
				if(ProjectRoundUserID.Value != "0")
				{
					Db.execute("DELETE FROM ProjectRoundUser WHERE ProjectRoundUserID = " + ProjectRoundUserID.Value);
					Db.execute("UPDATE Answer SET ProjectRoundUserID = NULL WHERE ProjectRoundUserID = " + ProjectRoundUserID.Value);
				}
				Action.Value = "0";
			}

			if(Action.Value == "5")
			{
				if(UnitProjectRoundUnitID.Value != "0")
				{
					string parent = "NULL";
					rs = Db.recordSet("SELECT ParentProjectRoundUnitID FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + UnitProjectRoundUnitID.Value);
					if(rs.Read() && !rs.IsDBNull(0))
					{
						parent = rs.GetInt32(0).ToString();
					}
					rs.Close();
					Db.execute("UPDATE ProjectRoundUnit SET ParentProjectRoundUnitID = " + parent + " WHERE ParentProjectRoundUnitID = " + UnitProjectRoundUnitID.Value);
					Db.execute("UPDATE ProjectRoundUser SET ProjectRoundUnitID = " + parent + " WHERE ProjectRoundUnitID = " + UnitProjectRoundUnitID.Value);
					Db.execute("UPDATE Answer SET ProjectRoundUnitID = " + parent + " WHERE ProjectRoundUnitID = " + UnitProjectRoundUnitID.Value);
					Db.execute("DELETE FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + UnitProjectRoundUnitID.Value);
				}
				Action.Value = "0";
			}

			Send.Visible = false;
			if(Action.Value == "6")
			{
				if(ChangeMode.Value == "0")
				{
					Send.Visible = true;
				}
				else
				{
					Action.Value = "0";
				}
			}
			if(Send.Visible)
			{
				if(UnitProjectRoundUnitID.Value != "0")
				{
					rs = Db.recordSet("SELECT dbo.cf_ProjectUnitTree(ProjectRoundUnitID,' » ') AS Unit FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + UnitProjectRoundUnitID.Value);
					if(rs.Read())
					{
						SendTo.Text = rs.GetString(0) + " » Alla utestående";
					}
					rs.Close();
				}
				else
				{
					rs = Db.recordSet("SELECT dbo.cf_ProjectUnitTree(ProjectRoundUnitID,' » ') AS Unit, Email, Name FROM ProjectRoundUser WHERE ProjectRoundUserID = " + ProjectRoundUserID.Value);
					if(rs.Read())
					{
						SendTo.Text = rs.GetString(0) + " » " + (rs.IsDBNull(2) || rs.GetString(2) == "" ? rs.GetString(1) : rs.GetString(2));
					}
					rs.Close();
				}
			}

			ChangeUnit.Visible = (Action.Value == "1");
			if(ChangeUnit.Visible && UnitProjectRoundUnitID.Value != "0")
			{
				rs = Db.recordSet("SELECT Unit, ParentProjectRoundUnitID FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + UnitProjectRoundUnitID.Value);
				if(rs.Read())
				{
					UnitName.Text = (!rs.IsDBNull(0) ? rs.GetString(0) : "");
					UnitParentProjectRoundUnitID.SelectedValue = (!rs.IsDBNull(1) ? rs.GetInt32(1).ToString() : "NULL");
				}
				rs.Close();
			}

			ChangeUser.Visible = (Action.Value == "2");
			if(ChangeUser.Visible && ProjectRoundUserID.Value != "0")
			{
				rs = Db.recordSet("SELECT Email, Name, UserCategoryID, ProjectRoundUnitID FROM ProjectRoundUser WHERE ProjectRoundUserID = " + ProjectRoundUserID.Value);
				if(rs.Read())
				{
					UserEmail.Text = (!rs.IsDBNull(0) ? rs.GetString(0) : "");
					UserName.Text = (!rs.IsDBNull(1) ? rs.GetString(1) : "");
					UserCategoryID.SelectedValue = (!rs.IsDBNull(2) ? rs.GetInt32(2).ToString() : "NULL");
					if(!rs.IsDBNull(3))
					{
						UserProjectRoundUnitID.SelectedValue = rs.GetInt32(3).ToString();
					}
				}
				rs.Close();
			}

			if(SaveSettings.Value != "0")
			{
				if(SaveSettings.Value == "2")
				{
					Db.execute("UPDATE ProjectRoundUnit SET SurveyIntro = NULL, Yellow = NULL, Green = NULL, Timeframe = NULL WHERE ProjectRoundUnitID IN (" + myPRUID + ")");
				}
				rs = Db.recordSet("SELECT ProjectRoundUnitID FROM ProjectRoundUnit WHERE ProjectRoundUnitID IN (" + myPRUID + ") AND dbo.cf_unitDepth(ProjectRoundUnitID) = dbo.cf_unitDepth(" + masterPRUID + ")");
				while(rs.Read())
				{
					Db.execute("UPDATE ProjectRoundUnit SET SurveyIntro = '" + SurveyIntro.Text.Replace("'","''") + "', Yellow = " + Convert.ToInt32("0" + Yellow.Text.Replace("'","")) + ", Green = " + Convert.ToInt32("0" + Green.Text.Replace("'","")) + ", Timeframe = " + Convert.ToInt32(Timeframe.Text.Replace("'","")) + " WHERE ProjectRoundUnitID = " + rs.GetInt32(0));
				}
				rs.Close();
				SaveSettings.Value = "0";
			}

			if(masterPRUID != 0)
			{
				string sortString = "";
				int depth = 0;

				rs = Db.recordSet("SELECT " +
					"p.Name, " +
					"p.Internal, " +
					"prl.SurveyIntro, " +
					"pru.SortString, " +
					"dbo.cf_unitDepth(pru.ProjectRoundUnitID), " +
					"NULL, " +	// 5
					"NULL, " +
					"NULL, " +
					"NULL, " +
					"pr.Timeframe, " +
					"prl.Surveyname, " +														// 10
					"NULL, " +
					"NULL, " +
					"dbo.cf_unitTimeframe(pru.ProjectRoundUnitID), " +
					"NULL, " +
					"dbo.cf_unitSurveyIntro(pru.ProjectRoundUnitID), " +						// 15
					"NULL, " +
					"dbo.cf_unitYellow(pru.ProjectRoundUnitID), " +
					"dbo.cf_unitGreen(pru.ProjectRoundUnitID) " +
					"FROM ProjectRoundUnit pru " +
					"INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
					"INNER JOIN Project p ON pr.ProjectID = p.ProjectID " +
					"LEFT OUTER JOIN ProjectRoundLang prl ON pr.ProjectRoundID = prl.ProjectRoundID AND prl.LangID = dbo.cf_unitLangID(pru.ProjectRoundUnitID) " +
					"WHERE pru.ProjectRoundUnitID = " + masterPRUID);
				if(rs.Read())
				{
					Org.Text = (!rs.IsDBNull(0) ? rs.GetString(0) : rs.GetString(1));
					Intro.Text = (rs.IsDBNull(10) ? (!rs.IsDBNull(2) ? rs.GetString(2) : "Normal") : rs.GetString(10));
					Yellow.Text = (!rs.IsDBNull(17) ? rs.GetInt32(17).ToString() : "50");
					Green.Text = (!rs.IsDBNull(18) ? rs.GetInt32(18).ToString() : "70");
					Timeframe.Text = (!rs.IsDBNull(13) ? rs.GetInt32(13).ToString() : "30");
					SurveyIntro.Text = (!rs.IsDBNull(15) ? rs.GetString(15) : "");
					sortString = (!rs.IsDBNull(3) ? rs.GetString(3) : "");
					depth = (!rs.IsDBNull(4) ? rs.GetInt32(4) : 0);
				}
				rs.Close();

				days = Convert.ToInt32(Timeframe.Text);
				DateTime toDT = Convert.ToDateTime(YearHigh.SelectedValue + "-" + MonthHigh.SelectedValue.PadLeft(2,'0') + "-" + DayHigh.SelectedValue.PadLeft(2,'0'));
				DateTime fromDT = toDT.AddDays(-days);
				YearLow.SelectedValue = fromDT.Year.ToString();
				MonthLow.SelectedValue = fromDT.Month.ToString();
				DayLow.SelectedValue = fromDT.Day.ToString();

				rs = Db.recordSet("SELECT " +
					"dbo.cf_projectRoundFinishedAnswerCount('" + toDT.ToString("yyyy-MM-dd") + "',pr.ProjectRoundID), " +
					"dbo.cf_projectRoundUserCount('" + toDT.ToString("yyyy-MM-dd") + "',pr.ProjectRoundID), " +
					"ISNULL(pr.Yellow,40), " +
					"ISNULL(pr.Green,70) " +
					"FROM ProjectRoundUnit pru " +
					"INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
					"WHERE pru.ProjectRoundUnitID = " + masterPRUID);
				if(rs.Read())
				{
					int orgMax = Math.Max(rs.GetInt32(0), rs.GetInt32(1));
					int orgPerc = (orgMax == 0 ? 100 : (100*rs.GetInt32(0)/orgMax));
					OrgStatus.Text = (orgMax > 0 ? "<b>" + orgPerc + "%</b> (" + rs.GetInt32(0) + " av " + orgMax + ")" : "-");
					OrgAnswer.Text = "<img src=\"img/status01_0" + (orgPerc < rs.GetInt32(2) ? "1" : (orgPerc < rs.GetInt32(3) ? "2" : "3")) + ".gif\" width=\"113\" height=\"22\">";
				}
				rs.Close();

				if(sortString != "")
				{
					// SELECT SUM(CX) AS Expr1 FROM (SELECT CASE WHEN tmp.col1 > tmp.col2 THEN tmp.col1 ELSE tmp.col2 END AS CX FROM (SELECT COUNT(a.AnswerID) AS col1, (SELECT COUNT(*) FROM ProjectRoundUser pr WHERE pr.ProjectRoundUnitID = p.ProjectRoundUnitID) AS col2 FROM ProjectRoundUnit p LEFT OUTER JOIN Answer a ON a.ProjectRoundUnitID = p.ProjectRoundUnitID WHERE (LEFT(p.SortString, 8) = '00000063') GROUP BY p.ProjectRoundUnitID) tmp) tmp2 
					string RK = "";
					rs = Db.recordSet("SELECT TOP 1 REPLACE(CONVERT(VARCHAR(255),r.ReportKey),'-','') FROM Report r");
					if(rs.Read())
					{
						RK = rs.GetString(0);
					}
					rs.Close();

					rs = Db.recordSet("SELECT " +
						"pru.ProjectRoundUnitID, " +																// 0
						"pru.Unit, " +
						"dbo.cf_unitDepth(pru.ProjectRoundUnitID), " +
						"dbo.cf_unitAndChildrenNonfinishedAnswerCount(dbo.cf_unitTimeframe(pru.ProjectRoundUnitID),'" + toDT.ToString("yyyy-MM-dd") + "',pru.ProjectRoundUnitID), " +
						"dbo.cf_unitAndChildrenFinishedAnswerCount(dbo.cf_unitTimeframe(pru.ProjectRoundUnitID),'" + toDT.ToString("yyyy-MM-dd") + "',pru.ProjectRoundUnitID), " +
						"dbo.cf_unitAndChildrenUserCount(dbo.cf_unitTimeframe(pru.ProjectRoundUnitID),'" + toDT.ToString("yyyy-MM-dd") + "',pru.ProjectRoundUnitID), " +								// 5
						"dbo.cf_unitAndChildrenUserSendCount(dbo.cf_unitTimeframe(pru.ProjectRoundUnitID),'" + toDT.ToString("yyyy-MM-dd") + "',pru.ProjectRoundUnitID), " +
						"dbo.cf_unitUserCount(dbo.cf_unitTimeframe(pru.ProjectRoundUnitID),'" + toDT.ToString("yyyy-MM-dd") + "',pru.ProjectRoundUnitID), " +
						"(SELECT COUNT(*) FROM Answer WHERE EndDT IS NOT NULL AND EndDT >= DATEADD(d,-dbo.cf_unitTimeframe(pru.ProjectRoundUnitID),'" + toDT.ToString("yyyy-MM-dd") + "') AND EndDT <= DATEADD(d,1,'" + toDT.ToString("yyyy-MM-dd") + "') AND ProjectRoundUnitID = pru.ProjectRoundUnitID), " +
						"dbo.cf_unitYellow(pru.ProjectRoundUnitID), " +
						"dbo.cf_unitGreen(pru.ProjectRoundUnitID) " +												// 10
						"FROM ProjectRoundUnit pru " +
						"WHERE pru.ProjectRoundUnitID IN (" + allPRUID + ") " +
						"ORDER BY pru.SortString");
					while(rs.Read())
					{
						bool myUnit = (myPRUID + ",0").IndexOf("," + rs.GetInt32(0).ToString() + ",") > 0;

						string indent = "";
						List.Controls.Add(new LiteralControl("<tr>"));
						List.Controls.Add(new LiteralControl("<td id=\"content1\">" + (myUnit && ChangeMode.Value == "1" ? "<a href=\"JavaScript:if(confirm('Är du säker på att du vill ta bort denna enhet?\\n\\nEv. underliggande enheter samt ev. användare på denna enhet kommer att flyttas ett steg upp i organisationen.')){document.forms[0].Action.value=5;document.forms[0].UnitProjectRoundUnitID.value=" + rs.GetInt32(0) + ";document.forms[0].submit();}\" title=\"Ta bort enhet\"><img src=\"img/delete.gif\" border=\"0\" align=\"right\"></A><a href=\"JavaScript:document.forms[0].Action.value=1;document.forms[0].UnitProjectRoundUnitID.value=" + rs.GetInt32(0) + ";document.forms[0].submit();\" title=\"Ändra enhet\"><img src=\"img/edit.gif\" border=\"0\" align=\"right\"></A>" : "")));
						if(myUnit && (rs.GetInt32(7) > 0 || rs.GetInt32(8) > 0))
						{
							List.Controls.Add(new LiteralControl("<A HREF=\"JavaScript:document.forms[0].ProjectRoundUnitID.value=" + (ProjectRoundUnitID.Value != rs.GetInt32(0).ToString() ? rs.GetInt32(0) : 0) + ";document.forms[0].submit();\" title=\"Visa användare/enkäter\"><img src=\"img/users_" + (ProjectRoundUnitID.Value != rs.GetInt32(0).ToString() ? "off" : "on") + ".gif\" align=\"right\" border=\"0\"></A>"));
						}
						if(myUnit && (rs.GetInt32(7) > 0 && ChangeMode.Value == "0"))
						{
							List.Controls.Add(new LiteralControl("<A HREF=\"JavaScript:document.forms[0].Action.value=6;document.forms[0].UnitProjectRoundUnitID.value=" + rs.GetInt32(0) + ";document.forms[0].submit();\" title=\"Skicka meddelande\"><img src=\"img/remind.gif\" border=\"0\" align=\"right\"></A>"));
						}
						//if(rs.GetInt32(2) - depth > 0)
						if(rs.GetInt32(2) - 1 > 0)
						{
							//for(int i = 1; i < rs.GetInt32(2) - depth; i++)
							for(int i = 1; i < rs.GetInt32(2) - 1; i++)
							{
								indent += "&nbsp;&nbsp;&nbsp;&nbsp;";
							}
							List.Controls.Add(new LiteralControl(indent + "<img src=\"img/bullet1.gif\" width=\"11\" height=\"9\" border=\"0\">"));
						}
						if(myUnit && ChangeMode.Value == "1")
						{
							List.Controls.Add(new LiteralControl("<a ondragstart=\"setSrc(" + rs.GetInt32(0) + ")\" ondragenter=\"cncSrc()\" ondrop=\"getSrc()\" ondragover=\"cncSrc()\" href=\"#\" style=\"text-decoration:none;\" id=\"PRUID" + rs.GetInt32(0) + "\">" + rs.GetString(1) + "</a>"));
						}
						else
						{
							List.Controls.Add(new LiteralControl((!myUnit ? "<span style=\"color:#777777;\">" : "") + rs.GetString(1) + (!myUnit ? "</span>" : "")));
						}

						List.Controls.Add(new LiteralControl("</td>"));
						int realMax = rs.GetInt32(5);//Math.Max(rs.GetInt32(5),rs.GetInt32(4));
						int max = realMax;
						int perc = (max > 0 ? 100*rs.GetInt32(4)/max : 0);
						List.Controls.Add(new LiteralControl("<td id=\"content2\">" + (realMax > 0 ? "<B>" + realMax + "</B>" + (realMax != rs.GetInt32(7) ? " (" + rs.GetInt32(7) + ")" : "") : "-") + "</td>"));
						List.Controls.Add(new LiteralControl("<td id=\"content2\">" + (max > 0 ? "<b>" +(max > 0 ? 100*rs.GetInt32(6)/max : 0) + "%</b> (" + rs.GetInt32(6) + ")" : "-") + "</td>"));
						List.Controls.Add(new LiteralControl("<td id=\"content2\">" + (max > 0 ? "<img src=\"img/status02_0" + (perc < rs.GetInt32(9) ? "1" : (perc < rs.GetInt32(10) ? "2" : "3")) + ".gif\">" : "-") + "</td>"));
						List.Controls.Add(new LiteralControl("<td id=\"content2\">" + (max > 0 ? "<b>" +(max > 0 ? 100*rs.GetInt32(3)/max : 0) + "%</b> (" + rs.GetInt32(3) + ")"  : "-") + "</td>"));
						List.Controls.Add(new LiteralControl("<td id=\"content2\">" + (max > 0 ? "<b>" + perc + "%</b> (" + rs.GetInt32(4) + ")" : "-") + "</td>"));
						List.Controls.Add(new LiteralControl("<td id=\"content2\">" + (myUnit && 1 == 0 ? "<A HREF=\"JavaScript:void(window.open('report.aspx?RK=" + RK + "&PRUID=" + rs.GetInt32(0) + "','_blank','scrollbars=1,resizable=1,width=940,height=600'));\"><IMG SRC=\"img/html.gif\" BORDER=\"0\"></A>" : "-") + "</td>"));
						//List.Controls.Add(new LiteralControl("<td id=\"content2\">" + (max > 0 ? "-" : "-") + "</td>"));
						List.Controls.Add(new LiteralControl("</tr>"));
						if(myUnit && ProjectRoundUnitID.Value == rs.GetInt32(0).ToString())
						{
							int UID = 0;
							string buffer = "";
							OdbcDataReader rs2 = Db.recordSet("" +
								"SELECT " +
								"REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-',''), " +	// 0
								"a.StartDT, " +											// 1
								"a.EndDT, " +											// 2
								"u.Email AS Email, " +									// 3
								"u.LastSent, " +										// 4
								"u.Name, " +											// 5
								"u.Created, " +											// 6
								"u.ProjectRoundUserID, " +								// 7
								"a.AnswerID, " +										// 8
								"u.UserKey, " +											// 9
								"u.ExternalID " +										// 10
								"FROM ProjectRoundUser u " +
								"LEFT OUTER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID AND a.StartDT >= DATEADD(d,-dbo.cf_unitTimeframe(u.ProjectRoundUnitID),'" + toDT.ToString("yyyy-MM-dd") + "') AND a.StartDT <= DATEADD(d,1,'" + toDT.ToString("yyyy-MM-dd") + "') " +
								"WHERE u.Terminated IS NULL AND u.ProjectRoundUnitID = " + ProjectRoundUnitID.Value + " " +
								"UNION ALL " +
								"SELECT " +
								"REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-',''), " +
								"a.StartDT, " +
								"a.EndDT, " +
								"NULL AS Email, " +
								"NULL, " +
								"NULL, " +
								"NULL, " +
								"NULL, " +
								"a.AnswerID, " +
								"NULL, " +
								"NULL " +
								"FROM Answer a " +
								"WHERE a.ProjectRoundUnitID = " + ProjectRoundUnitID.Value + " AND a.ProjectRoundUserID IS NULL " +
								"AND a.StartDT >= DATEADD(d,-dbo.cf_unitTimeframe(a.ProjectRoundUnitID),'" + toDT.ToString("yyyy-MM-dd") + "') AND a.StartDT <= DATEADD(d,1,'" + toDT.ToString("yyyy-MM-dd") + "')" +
								"ORDER BY Email ASC, a.AnswerID DESC " +
								"");
							while(rs2.Read())
							{
								if(rs2.IsDBNull(7) || rs2.GetInt32(7) != UID)
								{
									buffer += "<tr>";
									buffer += "<td class=\"content3\">" + (ChangeMode.Value == "0" && !rs2.IsDBNull(7) ? "<A HREF=\"JavaScript:document.forms[0].Action.value=6;document.forms[0].UnitProjectRoundUnitID.value=0;document.forms[0].ProjectRoundUserID.value=" + rs2.GetInt32(7) + ";document.forms[0].submit();\" title=\"Skicka meddelande\"><img src=\"img/remind.gif\" border=\"0\" align=\"right\"></A>" : "") + (ChangeMode.Value == "1" && !rs2.IsDBNull(7) ? "<a href=\"JavaScript:if(confirm('Är du säker på att du vill ta bort denna användare?')){document.forms[0].Action.value=4;document.forms[0].ProjectRoundUserID.value=" + rs2.GetInt32(7) + ";document.forms[0].submit();}\" title=\"Ta bort användare\"><img src=\"img/delete.gif\" border=\"0\" align=\"right\"></A><a href=\"JavaScript:document.forms[0].UserOrgProjectRoundUnitID.value=" + ProjectRoundUnitID.Value + ";document.forms[0].Action.value=2;document.forms[0].MoveData.value=0;document.forms[0].ProjectRoundUserID.value=" + rs2.GetInt32(7) + ";document.forms[0].submit();\" title=\"Ändra användare\"><img src=\"img/edit.gif\" border=\"0\" align=\"right\"></A>" : "");
									if(rs2.IsDBNull(0) || transparencyLevel >= 4)
									{
										buffer += indent + "&nbsp;&nbsp;&nbsp;&nbsp;" + (ChangeMode.Value == "1" ? "<A HREF=\"#\" STYLE=\"text-decoration:none\" ondragstart=\"setSrc('" + (rs2.IsDBNull(7) ? "A" + rs2.GetInt32(8) : "X" + rs2.GetInt32(7)) + "')\">" : "") + (rs2.IsDBNull(5) || rs2.GetString(5) == "" ? (rs2.IsDBNull(3) ? "&lt; anonym respondent &gt;" : rs2.GetString(3)) : rs2.GetString(5)) + (ChangeMode.Value == "1" ? "</A>" : "");
									}
									else
									{
										//List.Controls.Add(new LiteralControl(indent + "&nbsp;&nbsp;&nbsp;&nbsp;" + "<A" + (ChangeMode.Value == "1" ? " HREF=\"#\" ondragstart=\"setSrc('" + (rs2.IsDBNull(7) ? "A" + rs2.GetInt32(8) : "X" + rs2.GetInt32(7)) + "')\"" : " HREF=\"JavaScript:void(window.open('submit.aspx?AK=" + rs2.GetString(0) + "','','width=820,height=600,scrollbars=1'));\"") + ">" + (rs2.IsDBNull(5) || rs2.GetString(5) == "" ? (rs2.IsDBNull(3) ? "&lt; anonym användare &gt;" : rs2.GetString(3)) : rs2.GetString(5)) + "</A>"));
										buffer += indent + "&nbsp;&nbsp;&nbsp;&nbsp;" + "<A" + (ChangeMode.Value == "1" ? " HREF=\"#\" ondragstart=\"setSrc('" + (rs2.IsDBNull(7) ? "A" + rs2.GetInt32(8) : "X" + rs2.GetInt32(7)) + "')\"" : " HREF=\"" + (rs2.IsDBNull(7) || rs2.IsDBNull(2) ? "#" : "/submit.aspx?SM=" + rs2.GetString(0).Substring(0,8) + "&K=" + rs2.GetGuid(9).ToString().Substring(0,8) + rs2.GetInt32(7).ToString()) + "\"") + ">" + (rs2.IsDBNull(5) || rs2.GetString(5) == "" ? (rs2.IsDBNull(3) ? "&lt; anonym användare &gt;" : rs2.GetString(3)) : rs2.GetString(5)) + "</A>";
									}
									buffer += "</td>";
									buffer += "<td class=\"content4\">" + (rs2.IsDBNull(6) ? "&nbsp;" : rs2.GetDateTime(6).ToString("yyMMdd")) + "</TD>";
									buffer += "<td class=\"content4\">" + (rs2.IsDBNull(4) ? "&nbsp;" : rs2.GetDateTime(4).ToString("yyMMdd")) + "</TD>";
									if(transparencyLevel < 4)
									{
										buffer += "<td class=\"content4\"><img src=\"img/status02_0" + (rs2.IsDBNull(1) ? "1" : (rs2.IsDBNull(2) ? "2" : "3")) + ".gif\"></td>";
										buffer += "<td class=\"content4\">" + (rs2.IsDBNull(1) ? "&nbsp;" : rs2.GetDateTime(1).ToString("yyMMdd")) + "</TD>";
										buffer += "<td class=\"content4\">" + (rs2.IsDBNull(2) ? "&nbsp;" : rs2.GetDateTime(2).ToString("yyMMdd")) + "</TD>";
										buffer += "<td class=\"content4\">" + (ChangeMode.Value == "1" || rs2.IsDBNull(7) || rs2.IsDBNull(2) ? "" : "<A HREF=\"/submit.aspx?GenerateDownloadFeedback=" + (rs2.IsDBNull(10) ? "" : rs2.GetValue(10).ToString() + " - ") + (rs2.IsDBNull(5) || rs2.GetString(5) == "" ? (rs2.IsDBNull(3) ? "anonym användare" : rs2.GetString(3)) : rs2.GetString(5)) + "&K=" + rs2.GetGuid(9).ToString().Substring(0,8) + rs2.GetInt32(7).ToString() + "\"><IMG SRC=\"img/pdfSmall.gif\" BORDER=\"0\"></A>") + "&nbsp;</TD>";
									}
									else
									{
										buffer += "<td class=\"content4\" colspan=\"4\" align=\"center\">&nbsp;<i>konfidentiellt</i></TD>";
									}
									buffer += "</tr>";
								}
								UID = (rs2.IsDBNull(7) ? 0 : rs2.GetInt32(7));
							}
							rs2.Close();

							List.Controls.Add(new LiteralControl(buffer));
						}
					}
					rs.Close();

					if(ChangeMode.Value == "0")
					{
						Settings.Visible = false;
						Change.Text =	"<tr><td><a title=\"Uppdatera\" href=\"JavaScript:document.forms[0].submit();\"><img src=\"img/refresh.gif\" vspace=\"3\" hspace=\"3\" border=0/></a></td><td class=\"cp_11px\">Uppdatera</td></tr>" +
										"<tr><td><A title=\"Ändra inställningar, organisation och användare\" HREF=\"JavaScript:document.forms[0].ChangeMode.value='1';document.forms[0].submit();\"><img vspace=\"3\" hspace=\"3\" border=\"0\" src=\"img/gear.gif\"></A></td><td class=\"cp_11px\">Ändra</td></tr>";
					}
					else
					{
						Settings.Visible = true;
						Change.Text =	"<tr><td><a title=\"Återgå till visaläge\" href=\"JavaScript:document.forms[0].ChangeMode.value='0';document.forms[0].submit();\"><img src=\"img/CancelBox.gif\" vspace=\"3\" hspace=\"3\" border=0/></a></td><td class=\"cp_11px\">Återgå</td></tr>" +
										"<tr><td><A title=\"Spara inställningar och återgå till visaläge\" HREF=\"JavaScript:document.forms[0].ChangeMode.value='0';if(confirm('Vill du skriva över ev. inställningar gjorda av andra\\nadministratörer på nivåer under din toppnivå?')){document.forms[0].SaveSettings.value='2';}else{document.forms[0].SaveSettings.value='1';}document.forms[0].submit();\"><img vspace=\"3\" hspace=\"3\" border=\"0\" src=\"img/saveTool.gif\"></A></td><td class=\"cp_11px\">Spara</tr>";
					}
				}
			}
		}
		private void loadUnits()
		{
			UnitParentProjectRoundUnitID.Items.Clear();
			UserProjectRoundUnitID.Items.Clear();

			OdbcDataReader rs = Db.recordSet("SELECT ProjectRoundUnitID FROM ProjectRoundUnit WHERE ProjectRoundUnitID IN (" + myPRUID + ") AND ParentProjectRoundUnitID IS NULL");
			if(rs.Read())
			{
				UnitParentProjectRoundUnitID.Items.Add(new ListItem("< toppnivå >","NULL"));
			}
			rs.Close();
			rs = Db.recordSet("SELECT ProjectRoundUnitID, dbo.cf_ProjectUnitTree(ProjectRoundUnitID,' » ') AS Unit, ParentProjectRoundUnitID, dbo.cf_ProjectUnitTree(ParentProjectRoundUnitID,' » ') AS PUnit FROM ProjectRoundUnit WHERE ProjectRoundUnitID IN (" + myPRUID + ") ORDER BY SortString");
			while(rs.Read())
			{
				if(!rs.IsDBNull(2) && UnitParentProjectRoundUnitID.Items.FindByValue(rs.GetInt32(2).ToString()) == null)
				{
					UnitParentProjectRoundUnitID.Items.Add(new ListItem(rs.GetString(3),rs.GetInt32(2).ToString()));
				}
				if(!rs.IsDBNull(0) && UnitParentProjectRoundUnitID.Items.FindByValue(rs.GetInt32(0).ToString()) == null)
				{
					UnitParentProjectRoundUnitID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				UserProjectRoundUnitID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
			}
			rs.Close();

		}

		private void loadAccess()
		{
			if(!IsPostBack)
			{
				FromEmail.Text = projectSetup.appEmail;
			}

			if(HttpContext.Current.Request.Form["email"] != null && HttpContext.Current.Request.Form["password"] != null)
			{
				string MPRK = "";
				OdbcDataReader rs = Db.recordSet("SELECT TOP 1 REPLACE(CONVERT(VARCHAR(255),mpr.MPRK),'-','') FROM ManagerProjectRound mpr INNER JOIN Manager m ON m.ManagerID = mpr.ManagerID WHERE m.Email = '" + HttpContext.Current.Request.Form["email"].ToString().Replace("'","") + "' AND m.Password = '" + HttpContext.Current.Request.Form["password"].ToString().Replace("'","") + "'");
				if(rs.Read())
				{
					MPRK = rs.GetString(0);
				}
				rs.Close();

				if(MPRK != "")
				{
					HttpContext.Current.Response.Redirect("controlPanel.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&MPRK=" + MPRK,true);
				}
				else
				{
					HttpContext.Current.Response.Redirect("default.asp",true);
				}
			}

			masterPRUID = (HttpContext.Current.Request.QueryString["PRUID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["PRUID"]) : 0);
			if(masterPRUID != 0)
			{
				string sortString = "";
				OdbcDataReader rs = Db.recordSet("SELECT " +
					"pru.SortString, " +
					"pru.ProjectRoundID, " +
					"pr.ProjectID, " +
					"dbo.cf_unitLangID(pru.ProjectRoundUnitID), " +
					"prl.InvitationSubject, " +
					"prl.InvitationBody, " +
					"pr.EmailFromAddress, " +
					"pr.TransparencyLevel " +
					"FROM ProjectRoundUnit pru " +
					"INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
					"LEFT OUTER JOIN ProjectRoundLang prl ON pr.ProjectRoundID = prl.ProjectRoundID AND prl.LangID = dbo.cf_unitLangID(pru.ProjectRoundUnitID) " +
					"WHERE pru.ProjectRoundUnitID = " + masterPRUID);
				if(rs.Read())
				{
					transparencyLevel = rs.GetInt32(7);
					sortString = rs.GetString(0);
					PRID = rs.GetInt32(1);
					PID = rs.GetInt32(2);
					LID = rs.GetInt32(3);
					if(!IsPostBack)
					{
						if(!rs.IsDBNull(4))
						{
							Subject.Text = rs.GetString(4);
						}
						if(!rs.IsDBNull(5))
						{
							Message.Text = rs.GetString(5);
						}
						if(!rs.IsDBNull(6))
						{
							FromEmail.Text = rs.GetString(6);
						}
					}
				}
				rs.Close();
				rs = Db.recordSet("SELECT ProjectRoundUnitID, ParentProjectRoundUnitID FROM ProjectRoundUnit WHERE LEFT(SortString," + sortString.Length + ") = '" + sortString + "'");
				while(rs.Read())
				{
					myPRUID += "," + rs.GetInt32(0);
					if(!showCompleteOrg)
					{
						allPRUID += "," + rs.GetInt32(0);
						int parentPRUID = (rs.IsDBNull(1) ? 0 : rs.GetInt32(1));
						while(parentPRUID != 0 && (allPRUID + ",0").IndexOf("," + parentPRUID + ",") < 0)
						{
							OdbcDataReader rs2 = Db.recordSet("SELECT ProjectRoundUnitID, ParentProjectRoundUnitID FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + parentPRUID);
							if(rs2.Read())
							{
								parentPRUID = (rs2.IsDBNull(1) ? 0 : rs2.GetInt32(1));
								allPRUID += "," + rs2.GetInt32(0);
							}
							else
							{
								parentPRUID = 0;
							}
							rs2.Close();
						}
					}
				}
				rs.Close();
			}
			else if(HttpContext.Current.Request.QueryString["MPRK"] != null)
			{
				OdbcDataReader rs = Db.recordSet("SELECT " +
					"m.ManagerID, " +
					"mpr.ProjectRoundID, " +
					"pr.ProjectID, " +
					"dbo.cf_unitLangID(pru.ProjectRoundUnitID), " +
					"ISNULL(mpr.EmailSubject,prl.InvitationSubject), " +
					"ISNULL(mpr.EmailBody,prl.InvitationBody), " +
					"m.Email, " +
					"pru.ProjectRoundUnitID, " +
					"mpr.ShowAllUnits, " +
					"pru.ParentProjectRoundUnitID, " +
					"pr.TransparencyLevel " +
					"FROM ManagerProjectRound mpr " +
					"INNER JOIN Manager m ON mpr.ManagerID = m.ManagerID " +
					"INNER JOIN ProjectRound pr ON mpr.ProjectRoundID = pr.ProjectRoundID " +
					"INNER JOIN ManagerProjectRoundUnit mpru ON m.ManagerID = mpru.ManagerID AND mpr.ProjectRoundID = mpru.ProjectRoundID " +
					"INNER JOIN ProjectRoundUnit pru ON mpru.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
					"LEFT OUTER JOIN ProjectRoundLang prl ON pr.ProjectRoundID = prl.ProjectRoundID AND prl.LangID = dbo.cf_unitLangID(pru.ProjectRoundUnitID) " +
					"WHERE REPLACE(CONVERT(VARCHAR(255),mpr.MPRK),'-','') = '" + HttpContext.Current.Request.QueryString["MPRK"].ToString().Replace("'","") + "' " +
					"ORDER BY pru.SortString");
				if(rs.Read())
				{
					transparencyLevel = rs.GetInt32(10);
					masterPRUID = (rs.IsDBNull(7) ? 0 : rs.GetInt32(7));
					managerID = rs.GetInt32(0);
					PRID = rs.GetInt32(1);
					PID = rs.GetInt32(2);
					LID = rs.GetInt32(3);
					showCompleteOrg = (!rs.IsDBNull(8) && rs.GetInt32(8) == 1);
					if(!IsPostBack)
					{
						if(!rs.IsDBNull(4))
						{
							Subject.Text = rs.GetString(4);
						}
						if(!rs.IsDBNull(5))
						{
							Message.Text = rs.GetString(5);
						}
						if(!rs.IsDBNull(6))
						{
							FromEmail.Text = rs.GetString(6);
						}
					}
					
					do
					{
						if(!rs.IsDBNull(7))
						{
							myPRUID += "," + rs.GetInt32(7);

							if(!showCompleteOrg)
							{
								allPRUID += "," + rs.GetInt32(7);
								int parentPRUID = (rs.IsDBNull(9) ? 0 : rs.GetInt32(9));
								while(parentPRUID != 0 && (allPRUID + ",0").IndexOf("," + parentPRUID + ",") < 0)
								{
									OdbcDataReader rs2 = Db.recordSet("SELECT ProjectRoundUnitID, ParentProjectRoundUnitID FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + parentPRUID);
									if(rs2.Read())
									{
										parentPRUID = (rs2.IsDBNull(1) ? 0 : rs2.GetInt32(1));
										allPRUID += "," + rs2.GetInt32(0);
									}
									else
									{
										parentPRUID = 0;
									}
									rs2.Close();
								}
							}
						}
					}
					while(rs.Read());
				}
				rs.Close();
			}
			if(showCompleteOrg)
			{
				OdbcDataReader rs = Db.recordSet("SELECT ProjectRoundUnitID FROM ProjectRoundUnit WHERE ProjectRoundID = " + PRID);
				while(rs.Read())
				{
					allPRUID += "," + rs.GetInt32(0);
				}
				rs.Close();
			}
			if(masterPRUID == 0)
			{
				HttpContext.Current.Response.Redirect("default.asp",true);
			}
			if(!IsPostBack)
			{
				if(PID != 0 && System.IO.File.Exists(Server.MapPath("img\\project\\logo" + PID + ".gif")))
				{
					logo.Text = "<img src=\"img/project/logo" + PID + ".gif?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">";
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
	}
}
