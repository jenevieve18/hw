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
	/// Summary description for surveySetup.
	/// </summary>
	public class surveySetup : System.Web.UI.Page
	{
		protected CheckBox ClearQuestions;
		protected CheckBox FlipFlopBg;
		protected CheckBox NoTime;
		protected TextBox Copyright;
		protected TextBox Internal;
		protected PlaceHolder Questions;
		protected Button Save, Export, Move;
		protected TextBox MoveStep;
		protected DropDownList QuestionContainerID;
		protected CheckBoxList LangID;

		int surveyID = 0;

		private bool moveUp(int SQID)
		{
			bool success = false;
			int sortOrder = Db.getInt32("SELECT SortOrder FROM SurveyQuestion WHERE SurveyQuestionID = " + SQID);
			OdbcDataReader rs = Db.recordSet("SELECT SurveyQuestionID, SortOrder FROM SurveyQuestion WHERE SurveyID = " + surveyID + " AND SortOrder < " + sortOrder + " ORDER BY SortOrder DESC");
			if(rs.Read())
			{
				success = true;
				Db.execute("UPDATE SurveyQuestion SET SortOrder = " + sortOrder + " WHERE SurveyQuestionID = " + rs.GetInt32(0));
				Db.execute("UPDATE SurveyQuestion SET SortOrder = " + rs.GetInt32(1) + " WHERE SurveyQuestionID = " + SQID);
			}
			rs.Close();
			return success;
		}
		private bool moveUpOption(int SQID, int SQOID)
		{
			bool success = false;
			int sortOrder = Db.getInt32("SELECT " +
				"SortOrder " +
				"FROM SurveyQuestionOption " +
				"WHERE SurveyQuestionOptionID = " + SQOID + " " +
				"AND SurveyQuestionID = " + SQID);
			OdbcDataReader rs = Db.recordSet("SELECT " +
				"SurveyQuestionOptionID, " +
				"SortOrder " +
				"FROM SurveyQuestionOption " +
				"WHERE SurveyQuestionID = " + SQID + " " +
				"AND SortOrder < " + sortOrder + " " +
				"ORDER BY SortOrder DESC");
			if(rs.Read())
			{
				success = true;
				Db.execute("UPDATE SurveyQuestionOption SET SortOrder = " + sortOrder + " WHERE SurveyQuestionOptionID = " + rs.GetInt32(0));
				Db.execute("UPDATE SurveyQuestionOption SET SortOrder = " + rs.GetInt32(1) + " WHERE SurveyQuestionOptionID = " + SQOID);
			}
			rs.Close();
			return success;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			OdbcDataReader rs;

			Move.Click += new EventHandler(Move_Click);
			Export.Click += new EventHandler(Export_Click);

			#region copysurvey
			if(HttpContext.Current.Request.QueryString["CopySurveyID"] != null)
			{
				int oldSurveyID = Convert.ToInt32(HttpContext.Current.Request.QueryString["CopySurveyID"]);

				rs = Db.recordSet("SELECT Internal, Copyright, FlipFlopBg, NoTime, ClearQuestions FROM Survey WHERE SurveyID = " + oldSurveyID);
				if(rs.Read())
				{
					Db.execute("INSERT INTO Survey (Internal, Copyright, FlipFlopBg, NoTime, ClearQuestions) VALUES ('" + rs.GetString(0).Replace("'","''") + " (copy)'," + (rs.IsDBNull(1) ? "NULL" : "'" + rs.GetString(1).Replace("'","''") + "'") + "," + (rs.IsDBNull(2) ? "NULL" : rs.GetInt32(2).ToString()) + "," + (rs.IsDBNull(3) ? "NULL" : rs.GetInt32(3).ToString()) + "," + (rs.IsDBNull(4) ? "NULL" : rs.GetInt32(4).ToString()) + ")");
				}
				rs.Close();

				int newSurveyID = 0;

				rs = Db.recordSet("SELECT SurveyID FROM Survey ORDER BY SurveyID DESC");
				if(rs.Read())
				{
					newSurveyID = rs.GetInt32(0);
				}
				rs.Close();

				rs = Db.recordSet("SELECT LangID FROM SurveyLang WHERE SurveyID = " + oldSurveyID);
				while(rs.Read())
				{
					Db.execute("INSERT INTO SurveyLang (SurveyID,LangID) VALUES (" + newSurveyID + "," + rs.GetInt32(0) + ")");
				}
				rs.Close();

				rs = Db.recordSet("SELECT " +
					"QuestionID, " +			// 0
					"OptionsPlacement, " +
					"Variablename, " +
					"SurveyQuestionID, " +
					"NoCount, " +
					"RestartCount, " +			// 5
					"ExtendedFirst, " +
					"NoBreak, " +
					"BreakAfterQuestion, " +
					"PageBreakBeforeQuestion " +
					"FROM SurveyQuestion " +
					"WHERE SurveyID = " + oldSurveyID + " " +
					"ORDER BY SortOrder");
				while(rs.Read())
				{
					Db.execute("INSERT INTO SurveyQuestion (" +
						"SurveyID, " +
						"QuestionID, " +
						"OptionsPlacement, " +
						"Variablename, " +
						"NoCount, " +
						"RestartCount, " +
						"ExtendedFirst, " +
						"NoBreak, " +
						"BreakAfterQuestion," +
						"PageBreakBeforeQuestion" +
						") VALUES (" +
						"" + newSurveyID + "," +
						"" + rs.GetInt32(0) + "," +
						"" + rs.GetInt32(1) + "," +
						"'" + rs.GetString(2) + "'," +
						"" + (rs.IsDBNull(4) ? "NULL" : rs.GetInt32(4).ToString()) + "," +
						"" + (rs.IsDBNull(5) ? "NULL" : rs.GetInt32(5).ToString()) + "," +
						"" + (rs.IsDBNull(6) ? "NULL" : rs.GetInt32(6).ToString()) + "," +
						"" + (rs.IsDBNull(7) ? "NULL" : rs.GetInt32(7).ToString()) + "," +
						"" + (rs.IsDBNull(8) ? "NULL" : rs.GetInt32(8).ToString()) + "," +
						"" + (rs.IsDBNull(9) ? "NULL" : rs.GetInt32(9).ToString()) + "" +
						")");

					int newSQID = 0;
					OdbcDataReader rs2 = Db.recordSet("SELECT TOP 1 SurveyQuestionID FROM SurveyQuestion WHERE SurveyID = " + newSurveyID + " ORDER BY SurveyQuestionID DESC");
					if(rs2.Read())
					{
						newSQID = rs2.GetInt32(0);
					}
					rs2.Close();

					Db.execute("UPDATE SurveyQuestion SET SortOrder = " + newSQID + " WHERE SurveyQuestionID = " + newSQID);
					
					rs2 = Db.recordSet("SELECT LangID, Question FROM SurveyQuestionLang WHERE SurveyQuestionID = " + rs.GetInt32(3));
					while(rs2.Read())
					{
						Db.execute("INSERT INTO SurveyQuestionLang (SurveyQuestionID, LangID, Question) VALUES (" + newSQID + "," + rs2.GetInt32(0) + ",'" + rs2.GetString(1).Replace("'","''") + "')");
					}
					rs2.Close();

					rs2 = Db.recordSet("SELECT QuestionOptionID, OptionPlacement, Variablename, Forced, Warn FROM SurveyQuestionOption WHERE SurveyQuestionID = " + rs.GetInt32(3) + " ORDER BY SortOrder");
					while(rs2.Read())
					{
						Db.execute("INSERT INTO SurveyQuestionOption (SurveyQuestionID,QuestionOptionID, OptionPlacement, Variablename, Forced, Warn) VALUES (" + newSQID + "," + rs2.GetInt32(0) + "," + rs2.GetInt32(1) + ",'" + rs2.GetString(2).Replace("'","''") + "'," + rs2.GetInt32(3) + "," + (rs2.IsDBNull(4) ? "NULL" : rs2.GetInt32(4).ToString()) + ")");

						OdbcDataReader rs3 = Db.recordSet("SELECT TOP 1 SurveyQuestionOptionID FROM SurveyQuestionOption WHERE SurveyQuestionID = " + newSQID + " ORDER BY SurveyQuestionOptionID DESC");
						if(rs3.Read())
						{
							Db.execute("UPDATE SurveyQuestionOption SET SortOrder = " + rs3.GetInt32(0) + " WHERE SurveyQuestionOptionID = " + rs3.GetInt32(0));
						}
						rs3.Close();
					}
					rs2.Close();

					rs2 = Db.recordSet("SELECT QuestionID, OptionID, OptionComponentID FROM SurveyQuestionIf WHERE SurveyQuestionID = " + rs.GetInt32(3));
					while(rs2.Read())
					{
						Db.execute("INSERT INTO SurveyQuestionIf (SurveyID, SurveyQuestionID, QuestionID, OptionID, OptionComponentID) VALUES (" + newSurveyID + "," + newSQID + "," + (rs2.IsDBNull(0) ? "NULL" : rs2.GetInt32(0).ToString()) + "," + (rs2.IsDBNull(1) ? "NULL" : rs2.GetInt32(1).ToString()) + "," + (rs2.IsDBNull(2) ? "NULL" : rs2.GetInt32(2).ToString()) + ")");
					}
					rs2.Close();
				}
				rs.Close();

				HttpContext.Current.Response.Redirect("surveySetup.aspx?SurveyID=" + newSurveyID, true);
			}
			#endregion

			QuestionContainerID.SelectedIndexChanged += new EventHandler(QuestionContainerID_SelectedIndexChanged);
			if(!IsPostBack)
			{
				rs = Db.recordSet("SELECT LangID FROM Lang");
				while(rs.Read())
				{
					LangID.Items.Add(new ListItem("<img src=\"../img/lang/_" + rs.GetInt32(0) + ".gif\" border=\"0\"/>",rs.GetInt32(0).ToString()));
				}
				rs.Close();

				QuestionContainerID.Items.Add(new ListItem("Uncategorized","0"));
				rs = Db.recordSet("SELECT QuestionContainerID, QuestionContainer FROM QuestionContainer");
				while(rs.Read())
				{
					QuestionContainerID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				rs.Close();
				QuestionContainerID.SelectedValue = Convert.ToInt32("0" + HttpContext.Current.Session["QuestionContainerID"]).ToString();
			}
			
			surveyID = (HttpContext.Current.Request.QueryString["SurveyID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["SurveyID"]) : 0);

			Save.Click += new EventHandler(Save_Click);

			if(HttpContext.Current.Request.QueryString["MoveUp"] != null)
			{
				if(HttpContext.Current.Request.QueryString["SurveyQuestionID"] != null)
				{
					moveUpOption(Convert.ToInt32(HttpContext.Current.Request.QueryString["SurveyQuestionID"]),Convert.ToInt32(HttpContext.Current.Request.QueryString["MoveUp"]));
				}
				else
				{
					moveUp(Convert.ToInt32(HttpContext.Current.Request.QueryString["MoveUp"]));
				}

				HttpContext.Current.Response.Redirect("surveySetup.aspx?SurveyID=" + surveyID, true);
			}
			if(HttpContext.Current.Request.QueryString["MoveUpTop"] != null)
			{
				for(int i = 0; i < 25 && moveUp(Convert.ToInt32(HttpContext.Current.Request.QueryString["MoveUpTop"])); i++){}

				HttpContext.Current.Response.Redirect("surveySetup.aspx?SurveyID=" + surveyID, true);
			}

			if(!IsPostBack && surveyID != 0)
			{
				rs = Db.recordSet("SELECT Internal, Copyright, FlipFlopBg, NoTime, ClearQuestions FROM [Survey] WHERE SurveyID = " + surveyID);
				if(rs.Read())
				{
					Internal.Text = rs.GetString(0);
					Copyright.Text = (rs.IsDBNull(1) ? "" : rs.GetString(1));
					FlipFlopBg.Checked = (!rs.IsDBNull(2) && rs.GetInt32(2) == 1);
					NoTime.Checked = (!rs.IsDBNull(3) && rs.GetInt32(3) == 1);
					ClearQuestions.Checked = (!rs.IsDBNull(4) && rs.GetInt32(4) == 1);
				}
				rs.Close();

				rs = Db.recordSet("SELECT LangID FROM SurveyLang WHERE SurveyID = " + surveyID);
				while(rs.Read())
				{
					LangID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected = true;
				}
				rs.Close();
			}

			Questions.Controls.Add(new LiteralControl("<TR><TD>&nbsp;</TD><TD COLSPAN=\"3\" VALIGN=\"TOP\"><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">"));
			int cx = 0, questionNumber = 0;
			bool first = true;

			int QCID = Convert.ToInt32("0" + HttpContext.Current.Session["QuestionContainerID"]);

			rs = Db.recordSet("SELECT " +
				"q.QuestionID, " +			// 0
				"q.Internal, " +			// 1
				"sq.SurveyQuestionID, " +	// 2
				"sq.Variablename, " +		// 3
				"sq.OptionsPlacement, " +	// 4
				"(SELECT COUNT(*) FROM SurveyQuestionLang sql WHERE sql.SurveyQuestionID = sq.SurveyQuestionID) AS CX, " +	// 5
				"sq.NoCount, " +			// 6
				"sq.ExtendedFirst, " +		// 7
				"sq.RestartCount, " +		// 8
				"sq.NoBreak, " +			// 9
				"sq.BreakAfterQuestion, " +	// 10
				"sq.PageBreakBeforeQuestion, " +		// 11
				"q.Box, " +					// 12
				"(SELECT COUNT(*) FROM QuestionOption qo WHERE qo.QuestionID = q.QuestionID), " +	// 13
				"(SELECT COUNT(*) FROM SurveyQuestionIf sqi WHERE sqi.SurveyQuestionID = sq.SurveyQuestionID), " +	// 14
				"(SELECT COUNT(*) FROM SurveyQuestionIf sqi WHERE (sqi.QuestionID IS NULL OR sqi.OptionID IS NULL OR sqi.OptionComponentID IS NULL) AND sqi.SurveyQuestionID = sq.SurveyQuestionID) " +	// 15
				"FROM [Question] q " +
				"LEFT OUTER JOIN SurveyQuestion sq ON q.QuestionID = sq.QuestionID AND sq.SurveyID = " + surveyID + " " +
				"WHERE sq.SurveyID IS NOT NULL OR (q.QuestionContainerID" + (QCID != 0 ? " = " + QCID + " " : " IS NULL ") + ") " +
				"ORDER BY sq.SurveyID DESC, sq.SortOrder ASC, q.Internal");
			if(rs.Read())
			{
				if(!rs.IsDBNull(2))
				{
					Questions.Controls.Add(new LiteralControl("<TR>"));
					Questions.Controls.Add(new LiteralControl("<TD>&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD>&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD colspan=\"2\"><u>Selected questions</u>&nbsp;&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD><u>Variable</u>&nbsp;&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD><u>NoCount</u>&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD><u>Restart</u>&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD><u>NoBreak</u>&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD><u>BreakAQ</u>&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD><u>ExtFirst</u>&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD><u>PageBreak</u>&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD><u>Options</u>&nbsp;&nbsp;<br/><u>placement</u>&nbsp;&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD>&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD>&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD><u>Option(s)</u>&nbsp;&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD><u>Sub-</u>&nbsp;&nbsp;<br/><u>variable</u>&nbsp;&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD><u>Option</u>&nbsp;&nbsp;<br/><u>layout</u>&nbsp;&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD><u>Forced</u>&nbsp;&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD><u>Warn</u>&nbsp;&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("<TD><u>QID/SQID</u>&nbsp;&nbsp;</TD>"));
					Questions.Controls.Add(new LiteralControl("</TR>"));
				}

				do
				{
					if(!rs.IsDBNull(2))
					{
						Questions.Controls.Add(new LiteralControl("<TR>"));
						Questions.Controls.Add(new LiteralControl("<TD>"));
						if(cx != 0)
						{
							CheckBox cbMove = new CheckBox();
							cbMove.ID = "MoveSurveyQuestionID" + rs.GetInt32(2);
							Questions.Controls.Add(cbMove);

							Questions.Controls.Add(new LiteralControl("<A HREF=\"surveySetup.aspx?SurveyID=" + surveyID + "&MoveUp=" + rs.GetInt32(2) + "\"><IMG SRC=\"../img/UpToolSmall.gif\" border=\"0\"></A>"));
							Questions.Controls.Add(new LiteralControl("<A HREF=\"surveySetup.aspx?SurveyID=" + surveyID + "&MoveUpTop=" + rs.GetInt32(2) + "\"><IMG SRC=\"../img/UpToolSmallTop.gif\" border=\"0\"></A>"));
						}
						Questions.Controls.Add(new LiteralControl("</TD>"));
						Questions.Controls.Add(new LiteralControl("<TD>"));
						CheckBox cb = new CheckBox();
						cb.ID = "SurveyQuestionID" + rs.GetInt32(2);
						if(!IsPostBack)
						{
							cb.Checked = true;
						}
						Questions.Controls.Add(cb);
						Questions.Controls.Add(new LiteralControl("</TD>"));
						Questions.Controls.Add(new LiteralControl("<TD>"));
						if(rs.GetInt32(13) > 0 && rs.IsDBNull(6))
						{
							questionNumber++;
							Questions.Controls.Add(new LiteralControl("<span style=\"font-family:Courier New;font-size:9px;color:#" + (rs.GetInt32(14) != 0 ? "770000" : "007700") + ";" + (rs.GetInt32(15) != 0 ? "font-weight:bold;" : "") + "\">" + questionNumber.ToString().PadLeft(3,'0') + ".</span>&nbsp;"));
						}
						else
						{
							Questions.Controls.Add(new LiteralControl("<span style=\"font-family:Courier New;font-size:9px;\">&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;"));
						}
						Questions.Controls.Add(new LiteralControl("<A HREF=\"JavaScript:void(window.open('surveyQuestionSetup.aspx?SurveyQuestionID=" + rs.GetInt32(2) + "','surveyQuestionSetup" + rs.GetInt32(2) + "','width=750,height=660,scrollbars=1,resizable=1'));\">" + rs.GetString(1) + "</A>&nbsp;" + (rs.GetInt32(5) > 0 ? "*" : "") + "&nbsp;</TD>"));
						Questions.Controls.Add(new LiteralControl("<TD><A HREF=\"JavaScript:void(window.open('surveyQuestionIf.aspx?SurveyQuestionID=" + rs.GetInt32(2) + "','surveyQuestionIf" + rs.GetInt32(2) + "','width=400,height=600,scrollbars=1,resizable=1'));\"" + (rs.GetInt32(14) != 0 ? " style=\"font-weight:bold;font-size:16px;color:#770000;\"" : "") + ">?</A>&nbsp;</TD>"));
						#region Checks
						Questions.Controls.Add(new LiteralControl("<TD>"));
						TextBox vn = new TextBox();
						vn.ID = "Variablename" + rs.GetInt32(2);
						vn.Width = Unit.Pixel(40);
						if(!IsPostBack)
						{
							vn.Text = rs.GetString(3);
						}
						Questions.Controls.Add(vn);
						Questions.Controls.Add(new LiteralControl("&nbsp;&nbsp;</TD><TD>"));
						CheckBox noc = new CheckBox();
						noc.ID = "NoCountSurveyQuestionID" + rs.GetInt32(2);
						if(!IsPostBack)
						{
							noc.Checked = (!rs.IsDBNull(6) && rs.GetInt32(6) == 1);
						}
						Questions.Controls.Add(noc);
						Questions.Controls.Add(new LiteralControl("&nbsp;&nbsp;</TD><TD>"));
						CheckBox rc = new CheckBox();
						rc.ID = "RestartCountSurveyQuestionID" + rs.GetInt32(2);
						if(!IsPostBack)
						{
							rc.Checked = (!rs.IsDBNull(8) && rs.GetInt32(8) == 1);
						}
						Questions.Controls.Add(rc);
						Questions.Controls.Add(new LiteralControl("&nbsp;&nbsp;</TD><TD>"));
						CheckBox nb = new CheckBox();
						nb.ID = "NoBreakSurveyQuestionID" + rs.GetInt32(2);
						if(!IsPostBack)
						{
							nb.Checked = (!rs.IsDBNull(9) && rs.GetInt32(9) == 1);
						}
						Questions.Controls.Add(nb);
						Questions.Controls.Add(new LiteralControl("&nbsp;&nbsp;</TD><TD>"));
						CheckBox baq = new CheckBox();
						baq.ID = "BreakAfterQuestionSurveyQuestionID" + rs.GetInt32(2);
						if(!IsPostBack)
						{
							baq.Checked = (!rs.IsDBNull(10) && rs.GetInt32(10) == 1);
						}
						Questions.Controls.Add(baq);
						Questions.Controls.Add(new LiteralControl("&nbsp;&nbsp;</TD><TD>"));
						CheckBox ef = new CheckBox();
						ef.ID = "ExtendedFirstSurveyQuestionID" + rs.GetInt32(2);
						if(!IsPostBack)
						{
							ef.Checked = (!rs.IsDBNull(7) && rs.GetInt32(7) == 1);
						}
						Questions.Controls.Add(ef);
						Questions.Controls.Add(new LiteralControl("&nbsp;&nbsp;</TD><TD>"));
						CheckBox pb = new CheckBox();
						if(!rs.IsDBNull(12) && rs.GetInt32(12) == 1)
						{
							pb.Text = "box";
						}
						pb.ID = "PageBreakSurveyQuestionID" + rs.GetInt32(2);
						if(!IsPostBack)
						{
							pb.Checked = (!rs.IsDBNull(11) && rs.GetInt32(11) == 1);
						}
						Questions.Controls.Add(pb);
						Questions.Controls.Add(new LiteralControl("&nbsp;&nbsp;</TD>"));
						#endregion
						Questions.Controls.Add(new LiteralControl("<TD>"));
						DropDownList osp = new DropDownList();
						osp.ID = "OptionsPlacement" + rs.GetInt32(2);
						osp.Width = Unit.Pixel(70);
						osp.Items.Add(new ListItem("Right","1"));
						osp.Items.Add(new ListItem("Below","2"));
						if(!IsPostBack)
						{
							osp.SelectedValue = rs.GetInt32(4).ToString();
						}
						Questions.Controls.Add(osp);
						Questions.Controls.Add(new LiteralControl("&nbsp;&nbsp;</TD>"));
						
						int bx = 0, dx = 0; string optionIDs = "";
						OdbcDataReader rs2 = Db.recordSet("SELECT " +
							"qo.QuestionOptionID, " +			// 0
							"o.Internal, " +					// 1
							"sqo.SurveyQuestionOptionID, " +	// 2
							"sqo.OptionPlacement, " +			// 3
							"sqo.Variablename, " +				// 4
							"sqo.Forced, " +					// 5
							"sqo.Warn, " +						// 6
							"(SELECT COUNT(*) FROM SurveyQuestionOptionComponent x WHERE x.SurveyQuestionOptionID = sqo.SurveyQuestionOptionID) AS CX, " +
							"(SELECT COUNT(*) FROM SurveyQuestionOptionComponentLang y WHERE y.SurveyQuestionOptionID = sqo.SurveyQuestionOptionID) AS DX, " +
							"o.OptionID " +						// 9
							"FROM QuestionOption qo " +
							"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
							"LEFT OUTER JOIN SurveyQuestionOption sqo ON qo.QuestionOptionID = sqo.QuestionOptionID AND sqo.SurveyQuestionID = " + rs.GetInt32(2) + " " +
							"WHERE qo.QuestionID = " + rs.GetInt32(0) + " " +
							"ORDER BY sqo.SurveyQuestionID DESC, sqo.SortOrder ASC, qo.SortOrder ASC");
						while(rs2.Read())
						{
							optionIDs += "/" + rs2.GetInt32(9);
							#region Question options
							if(bx++ > 0)
							{
								Questions.Controls.Add(new LiteralControl("</TR><TR><TD COLSPAN=\"12\">&nbsp;</TD>"));
							}

							Questions.Controls.Add(new LiteralControl("<TD>"));

							if(dx != 0 && !rs2.IsDBNull(2))
							{
								Questions.Controls.Add(new LiteralControl("<A HREF=\"surveySetup.aspx?SurveyID=" + surveyID + "&SurveyQuestionID=" + rs.GetInt32(2) + "&MoveUp=" + rs2.GetInt32(2) + "\"><IMG SRC=\"../img/UpToolSmall.gif\" border=\"0\"></A>"));
							}

							Questions.Controls.Add(new LiteralControl("</TD><TD>"));
							
							cb = new CheckBox();
							if(!rs2.IsDBNull(2))
							{
								cb.ID = "SurveyQuestionOptionID" + rs2.GetInt32(2);
								if(!IsPostBack)
								{
									cb.Checked = true;
								}
								dx++;
							}
							else
							{
								cb.ID = "QuestionOptionID" + rs2.GetInt32(0);
							}

							Questions.Controls.Add(cb);
							Questions.Controls.Add(new LiteralControl("</TD><TD>"));
							if(!rs2.IsDBNull(2))
							{
								Questions.Controls.Add(new LiteralControl("<A HREF=\"javascript:void(window.open('surveyQuestionOptionSetup.aspx?SQOID=" + rs2.GetInt32(2) + "','SQOID" + rs2.GetInt32(2) + "','width=600,height=600,scrollbars=1,resizeable=1'));\">" + rs2.GetString(1) + "</A>" + ((rs2.GetInt32(7) + rs2.GetInt32(8)) > 0 ? "*" : "") + "&nbsp;&nbsp;"));
							}
							Questions.Controls.Add(new LiteralControl("</TD><TD>"));
							
							if(!rs2.IsDBNull(2))
							{
								vn = new TextBox();
								vn.ID = "SubVariablename" + rs2.GetInt32(2);
								vn.Width = Unit.Pixel(40);
								if(!IsPostBack)
								{
									vn.Text = rs2.GetString(4);
								}
								Questions.Controls.Add(vn);
							}

							Questions.Controls.Add(new LiteralControl("&nbsp;&nbsp;</TD><TD>"));

							if(!rs2.IsDBNull(2))
							{
								DropDownList op = new DropDownList();
								op.ID = "OptionPlacement" + rs2.GetInt32(2);
								op.Items.Add(new ListItem("Horizontal, top","1"));
								//op.Items.Add(new ListItem("Horizontal, left","2"));
								op.Items.Add(new ListItem("Horizontal, right","3"));
								//op.Items.Add(new ListItem("Horizontal, bottom","4"));
								op.Items.Add(new ListItem("Horizontal, -","5"));
								//op.Items.Add(new ListItem("Vertical, top","6"));
								//op.Items.Add(new ListItem("Vertical, left","7"));
								op.Items.Add(new ListItem("Vertical, right","8"));
								//op.Items.Add(new ListItem("Vertical, bottom","9"));
								//op.Items.Add(new ListItem("Vertical, -","10"));
								if(!IsPostBack)
								{
									op.SelectedValue = rs2.GetInt32(3).ToString();
								}
								Questions.Controls.Add(op);
							}

							Questions.Controls.Add(new LiteralControl("</TD><TD>"));
							
							if(!rs2.IsDBNull(2))
							{
								CheckBox f = new CheckBox();
								f.ID = "Forced" + rs2.GetInt32(2);
								if(!IsPostBack)
								{
									f.Checked = (rs2.GetInt32(5) == 1);
								}
								Questions.Controls.Add(f);
							}

							Questions.Controls.Add(new LiteralControl("</TD><TD>"));
							
							if(!rs2.IsDBNull(2))
							{
								CheckBox w = new CheckBox();
								w.ID = "Warn" + rs2.GetInt32(2);
								if(!IsPostBack)
								{
									w.Checked = (!rs2.IsDBNull(6) && rs2.GetInt32(6) == 1);
								}
								Questions.Controls.Add(w);
							}
							Questions.Controls.Add(new LiteralControl("</TD>"));
							#endregion
						}
						rs2.Close();

						Questions.Controls.Add(new LiteralControl("<TD>" + rs.GetInt32(0) + "/" + rs.GetInt32(2) + optionIDs + "</TD>"));
						Questions.Controls.Add(new LiteralControl("</TR>"));
						
						cx++;
					}
					else
					{
						if(first)
						{
							Questions.Controls.Add(new LiteralControl("</TABLE></TD></TR><TR><TD>&nbsp;</TD><TD COLSPAN=\"3\" VALIGN=\"TOP\"><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\"><TR><TD>&nbsp;</TD><TD><u>Available questions</u>&nbsp;&nbsp;</TD><TD><u>Option(s)</u>&nbsp;&nbsp;</TD></TR>"));
							first = false;
						}
						Questions.Controls.Add(new LiteralControl("<TR><TD>"));
						CheckBox cb = new CheckBox();
						cb.ID = "QuestionID" + rs.GetInt32(0);
						Questions.Controls.Add(cb);
						Questions.Controls.Add(new LiteralControl("</TD><TD>" + rs.GetString(1) + "&nbsp;&nbsp;</TD><TD>"));

						int ax = 0;
						OdbcDataReader rs2 = Db.recordSet("SELECT o.Internal FROM QuestionOption qo INNER JOIN [Option] o ON qo.OptionID = o.OptionID WHERE qo.QuestionID = " + rs.GetInt32(0) + " ORDER BY qo.SortOrder");
						while(rs2.Read())
						{
							if(ax != 0)
							{
								Questions.Controls.Add(new LiteralControl(", "));
							}
							ax++;
							Questions.Controls.Add(new LiteralControl(rs2.GetString(0)));
						}
						rs2.Close();
						Questions.Controls.Add(new LiteralControl("</TD></TR>"));
					}
				}
				while(rs.Read());
			}
			rs.Close();

			Questions.Controls.Add(new LiteralControl("</TABLE></TD></TR>"));
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
			if(surveyID != 0)
			{
				Db.execute("UPDATE Survey SET " +
					"Internal = '" + Internal.Text.Replace("'","''") + "', " +
					"Copyright = " + (Copyright.Text == "" ? "NULL" : "'" + Copyright.Text.Replace("'","''") + "'") + ", " +
					"FlipFlopBg = " + (FlipFlopBg.Checked ? "1" : "NULL") + ", " +
					"NoTime = " + (NoTime.Checked ? "1" : "NULL") + ", " +
					"ClearQuestions = " + (ClearQuestions.Checked ? "1" : "NULL") + " " +
					"WHERE SurveyID = " + surveyID);
			}
			else
			{
				surveyID = Db.getInt32("SET NOCOUNT ON;SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;BEGIN TRAN;INSERT INTO [Survey] (" +
					"Internal," +
					"Copyright," +
					"FlipFlopBg," +
					"NoTime," +
					"ClearQuestions" +
					") VALUES (" +
					"'" + Internal.Text.Replace("'","") + "'," +
					"" + (Copyright.Text == "" ? "NULL" : "'" + Copyright.Text.Replace("'","''") + "'") + "," +
					"" + (FlipFlopBg.Checked ? "1" : "NULL") + "," +
					"" + (NoTime.Checked ? "1" : "NULL") + "," +
					"" + (ClearQuestions.Checked ? "1" : "NULL") + "" +
					");SELECT SurveyID FROM [Survey] WHERE Internal = '" + Internal.Text.Replace("'","") + "' ORDER BY SurveyID DESC;COMMIT;");
			}

			Db.execute("DELETE FROM SurveyLang WHERE SurveyID = " + surveyID);
			OdbcDataReader rs = Db.recordSet("SELECT LangID FROM Lang");
			while(rs.Read())
			{
				if(LangID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected)
				{
					Db.execute("INSERT INTO SurveyLang (SurveyID,LangID) VALUES (" + surveyID + "," + rs.GetInt32(0) + ")");
				}
			}
			rs.Close();

			rs = Db.recordSet("SELECT SurveyQuestionID, QuestionID FROM SurveyQuestion WHERE SurveyID = " + surveyID);
			while(rs.Read())
			{
				if(Questions.FindControl("SurveyQuestionID" + rs.GetInt32(0)) != null)
				{
					if(((CheckBox)Questions.FindControl("SurveyQuestionID" + rs.GetInt32(0))).Checked)
					{
						Db.execute("UPDATE SurveyQuestion SET PageBreakBeforeQuestion = " + (((CheckBox)Questions.FindControl("PageBreakSurveyQuestionID" + rs.GetInt32(0))).Checked ? "1" : "NULL") + ", BreakAfterQuestion = " + (((CheckBox)Questions.FindControl("BreakAfterQuestionSurveyQuestionID" + rs.GetInt32(0))).Checked ? "1" : "NULL") + ", NoBreak = " + (((CheckBox)Questions.FindControl("NoBreakSurveyQuestionID" + rs.GetInt32(0))).Checked ? "1" : "NULL") + ", ExtendedFirst = " + (((CheckBox)Questions.FindControl("ExtendedFirstSurveyQuestionID" + rs.GetInt32(0))).Checked ? "1" : "NULL") + ", RestartCount = " + (((CheckBox)Questions.FindControl("RestartCountSurveyQuestionID" + rs.GetInt32(0))).Checked ? "1" : "NULL") + ", NoCount = " + (((CheckBox)Questions.FindControl("NoCountSurveyQuestionID" + rs.GetInt32(0))).Checked ? "1" : "NULL") + ", OptionsPlacement = " + ((DropDownList)Questions.FindControl("OptionsPlacement" + rs.GetInt32(0))).SelectedValue + ", Variablename = '" + ((TextBox)Questions.FindControl("Variablename" + rs.GetInt32(0))).Text.Replace("'","") + "' WHERE SurveyQuestionID = " + rs.GetInt32(0));
						OdbcDataReader rs2 = Db.recordSet("SELECT " +
							"sqo.SurveyQuestionOptionID " +
							"FROM SurveyQuestionOption sqo " +
							"WHERE sqo.SurveyQuestionID = " + rs.GetInt32(0));
						while(rs2.Read())
						{
							if(((CheckBox)Questions.FindControl("SurveyQuestionOptionID" + rs2.GetInt32(0))) != null && ((CheckBox)Questions.FindControl("SurveyQuestionOptionID" + rs2.GetInt32(0))).Checked)
							{
								Db.execute("UPDATE SurveyQuestionOption SET OptionPlacement = " + ((DropDownList)Questions.FindControl("OptionPlacement" + rs2.GetInt32(0))).SelectedValue + ", Variablename = '" + ((TextBox)Questions.FindControl("SubVariablename" + rs2.GetInt32(0))).Text.Replace("'","''") + "', Forced = " + (((CheckBox)Questions.FindControl("Forced" + rs2.GetInt32(0))).Checked ? "1" : "0") + ", Warn = " + (((CheckBox)Questions.FindControl("Warn" + rs2.GetInt32(0))).Checked ? "1" : "NULL") + " WHERE SurveyQuestionOptionID = " + rs2.GetInt32(0));
							}
							else
							{
								Db.execute("UPDATE SurveyQuestionOption SET SurveyQuestionID = -" + rs.GetInt32(0) + " WHERE SurveyQuestionOptionID = " + rs2.GetInt32(0));
							}
						}
						rs2.Close();

						rs2 = Db.recordSet("SELECT qo.QuestionOptionID, qo.Variablename, qo.OptionPlacement, qo.Forced FROM QuestionOption qo WHERE qo.QuestionID = " + rs.GetInt32(1) + " ORDER BY qo.SortOrder");
						while(rs2.Read())
						{
							if(Questions.FindControl("QuestionOptionID" + rs2.GetInt32(0)) != null)
							{
								if(((CheckBox)Questions.FindControl("QuestionOptionID" + rs2.GetInt32(0))).Checked)
								{
									int surveyQuestionOptionID = Db.getInt32("SET NOCOUNT ON;SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;BEGIN TRAN;INSERT INTO SurveyQuestionOption (SurveyQuestionID,QuestionOptionID,Variablename,OptionPlacement,Forced) VALUES (" + rs.GetInt32(0) + "," + rs2.GetInt32(0) + ",'" + rs2.GetString(1).Replace("'","") + "'," + rs2.GetInt32(2) + "," + rs2.GetInt32(3) + ");SELECT SurveyQuestionOptionID FROM [SurveyQuestionOption] WHERE QuestionOptionID = " + rs2.GetInt32(0) + " ORDER BY SurveyQuestionOptionID DESC;COMMIT;");
									Db.execute("UPDATE SurveyQuestionOption SET SortOrder = " + surveyQuestionOptionID + " WHERE SurveyQuestionOptionID = " + surveyQuestionOptionID);
								}
							}
						}
						rs2.Close();
					}
					else
					{
						Db.execute("UPDATE SurveyQuestion SET SurveyID = -" + surveyID + " WHERE SurveyQuestionID = " + rs.GetInt32(0));
					}
				}
			}
			rs.Close();

			rs = Db.recordSet("SELECT QuestionID, Variablename, OptionsPlacement FROM Question ORDER BY Internal");
			while(rs.Read())
			{
				if(Questions.FindControl("QuestionID" + rs.GetInt32(0)) != null)
				{
					if(((CheckBox)Questions.FindControl("QuestionID" + rs.GetInt32(0))).Checked)
					{
						int surveyQuestionID = Db.getInt32("SET NOCOUNT ON;SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;BEGIN TRAN;INSERT INTO SurveyQuestion (SurveyID,QuestionID,Variablename,OptionsPlacement) VALUES (" + surveyID + "," + rs.GetInt32(0) + ",'" + rs.GetString(1).Replace("'","") + "'," + rs.GetInt32(2) + ");SELECT SurveyQuestionID FROM [SurveyQuestion] WHERE QuestionID = " + rs.GetInt32(0) + " ORDER BY SurveyQuestionID DESC;COMMIT;");
						Db.execute("UPDATE SurveyQuestion SET SortOrder = " + surveyQuestionID + " WHERE SurveyQuestionID = " + surveyQuestionID);

						OdbcDataReader rs2 = Db.recordSet("SELECT qo.QuestionOptionID, qo.Variablename, qo.OptionPlacement, qo.Forced FROM QuestionOption qo WHERE qo.QuestionID = " + rs.GetInt32(0) + " ORDER BY qo.SortOrder");
						while(rs2.Read())
						{
							int surveyQuestionOptionID = Db.getInt32("SET NOCOUNT ON;SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;BEGIN TRAN;INSERT INTO SurveyQuestionOption (SurveyQuestionID,QuestionOptionID,Variablename,OptionPlacement,Forced) VALUES (" + surveyQuestionID + "," + rs2.GetInt32(0) + ",'" + rs2.GetString(1).Replace("'","") + "'," + rs2.GetInt32(2) + "," + rs2.GetInt32(3) + ");SELECT SurveyQuestionOptionID FROM [SurveyQuestionOption] WHERE QuestionOptionID = " + rs2.GetInt32(0) + " ORDER BY SurveyQuestionOptionID DESC;COMMIT;");
							Db.execute("UPDATE SurveyQuestionOption SET SortOrder = " + surveyQuestionOptionID + " WHERE SurveyQuestionOptionID = " + surveyQuestionOptionID);
						}
						rs2.Close();
					}
				}
			}
			rs.Close();

			HttpContext.Current.Response.Redirect("surveySetup.aspx?SurveyID=" + surveyID,true);
		}

		private void QuestionContainerID_SelectedIndexChanged(object sender, EventArgs e)
		{
			HttpContext.Current.Session["QuestionContainerID"] = QuestionContainerID.SelectedValue;
			HttpContext.Current.Response.Redirect("surveySetup.aspx?SurveyID=" + surveyID,true);
		}

		private void Move_Click(object sender, EventArgs e)
		{
			OdbcDataReader rs = Db.recordSet("SELECT " +
				"SurveyQuestionID, " +
				"QuestionID " +
				"FROM SurveyQuestion " +
				"WHERE SurveyID = " + surveyID + " ORDER BY SortOrder");
			while(rs.Read())
			{
				if(Questions.FindControl("MoveSurveyQuestionID" + rs.GetInt32(0)) != null)
				{
					if(((CheckBox)Questions.FindControl("MoveSurveyQuestionID" + rs.GetInt32(0))).Checked)
					{
						for(int i=0; i<Convert.ToInt32(MoveStep.Text) && moveUp(rs.GetInt32(0)); i++){}
					}
				}
			}
			rs.Close();
			HttpContext.Current.Response.Redirect("surveySetup.aspx?SurveyID=" + surveyID, true);
		}

		private void Export_Click(object sender, EventArgs e)
		{
			System.Text.StringBuilder res = new System.Text.StringBuilder();

			res.Append("NO\tDESCRIPTION\tVISIBLE IF"); int cx = 0;
			OdbcDataReader rs = Db.recordSet("SELECT " +
				"q.QuestionID, " +			// 0
				"q.Internal, " +			// 1
				"sq.SurveyQuestionID, " +	// 2
				"sq.NoCount, " +			// 3
				"sq.RestartCount, " +		// 4
				"(SELECT COUNT(*) FROM QuestionOption qo WHERE qo.QuestionID = q.QuestionID), " +	// 5
				"(SELECT COUNT(*) FROM SurveyQuestionIf sqi WHERE sqi.SurveyQuestionID = sq.SurveyQuestionID) " +	// 6
				"FROM [Question] q " +
				"INNER JOIN SurveyQuestion sq ON q.QuestionID = sq.QuestionID " +
				"WHERE sq.SurveyID = " + surveyID + " " +
				"ORDER BY sq.SurveyID DESC, sq.SortOrder ASC, q.Internal");
			while(rs.Read())
			{
				if(!rs.IsDBNull(4)){cx = 0;}
				if(rs.IsDBNull(3) && rs.GetInt32(5) > 0){cx++;}
				res.Append("\r\n" + 
					(!rs.IsDBNull(3) || rs.GetInt32(5) == 0 ? "" : cx.ToString()) + "\t" +
					rs.GetString(1));
				if(rs.GetInt32(6) > 0)
				{
					bool first = true;
					OdbcDataReader rs2 = Db.recordSet("SELECT sqi.ConditionAnd, q.Internal, o.Internal, oc.ExportValue, (SELECT COUNT(*) FROM SurveyQuestion sq INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID WHERE sq.QuestionID = q.QuestionID AND sq.SurveyID = " + surveyID + ") AS CX FROM SurveyQuestionIf sqi INNER JOIN Question q ON q.QuestionID = sqi.QuestionID INNER JOIN [Option] o ON sqi.OptionID = o.OptionID INNER JOIN OptionComponent oc ON sqi.OptionComponentID = oc.OptionComponentID WHERE sqi.SurveyQuestionID = " + rs.GetInt32(2) + " ORDER BY sqi.SurveyQuestionIfID");
					while(rs2.Read())
					{
						res.Append((first ? "\t" : (rs2.IsDBNull(0) ? " OR " : " AND ")) + rs2.GetString(1) + (rs2.GetInt32(4) > 1 ? "/" + rs2.GetString(2) : "") + "/" + rs2.GetInt32(3));
						first = false;
					}
					rs2.Close();
				}
			}
			rs.Close();

			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.ClearHeaders();
			//HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
			HttpContext.Current.Response.Charset = "UTF-8";
			HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
			HttpContext.Current.Response.ContentType = "text/plain";
			HttpContext.Current.Response.AddHeader("content-disposition","attachment; filename=" + DateTime.Now.Ticks + ".txt");
			HttpContext.Current.Response.Write(res.ToString());
			HttpContext.Current.Response.Flush();
			HttpContext.Current.Response.End();
		}
	}
}
