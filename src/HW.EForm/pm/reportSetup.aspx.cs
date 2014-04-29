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

namespace eform.pm
{
	/// <summary>
	/// Summary description for reportSetup.
	/// </summary>
	public class reportSetup : System.Web.UI.Page
	{
		public int reportID = 0;
		int questionPartID = 0;
		int idxPartID = 0;
		int wqoPartID = 0;

		protected Label Part;
		protected Label Idxes;
		protected Label Wqos;
		protected Button Save;
		protected Button AddQuestionPart;
		protected Button SaveQuestionPart;
		protected Button AddWqoPart;
		protected Button SaveWqoPart;
		protected Button AddIdxPart;
		protected Button SaveIdxPart;
		protected TextBox Internal;
		protected TextBox QuestionInternal;
		protected TextBox IdxInternal;
		protected TextBox WqoInternal;
		protected TextBox QuestionRequiredAnswerCount;
		protected TextBox WqoRequiredAnswerCount;
		protected PlaceHolder EditPart;
		protected PlaceHolder EditWqoPart;
		protected PlaceHolder EditQuestionPart;
		protected PlaceHolder EditIdxPart;
		protected DropDownList QuestionIDOptionID;
		protected CheckBoxList WeightedQuestionOptionID;
		protected CheckBoxList IdxID;
		protected PlaceHolder QuestionText;
		protected PlaceHolder IdxText;
		protected PlaceHolder WqoText;
		protected CheckBox WqoOvertime;

		private void Page_Load(object sender, System.EventArgs e)
		{
			questionPartID = (HttpContext.Current.Request.QueryString["QuestionPartID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["QuestionPartID"]) : 0);
			idxPartID = (HttpContext.Current.Request.QueryString["IdxPartID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["IdxPartID"]) : 0);
			wqoPartID = (HttpContext.Current.Request.QueryString["WqoPartID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["WqoPartID"]) : 0);
			reportID = (HttpContext.Current.Request.QueryString["ReportID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["ReportID"]) : 0);

			OdbcDataReader rs;
			
			if(HttpContext.Current.Request.QueryString["Delete"] != null)
			{
				Db.execute("DELETE FROM ReportPart WHERE ReportPartID = " + HttpContext.Current.Request.QueryString["Delete"] + " AND ReportID = " + reportID);
				Db.execute("DELETE FROM ReportPartComponent WHERE ReportPartID = " + HttpContext.Current.Request.QueryString["Delete"]);
				Db.execute("DELETE FROM ReportPartLang WHERE ReportPartID = " + HttpContext.Current.Request.QueryString["Delete"]);
			}
			
			if(HttpContext.Current.Request.QueryString["MoveUp"] != null)
			{
				int sortOrder = Db.getInt32("SELECT SortOrder FROM ReportPart WHERE ReportPartID = " + HttpContext.Current.Request.QueryString["MoveUp"]);
				rs = Db.recordSet("SELECT ReportPartID, SortOrder FROM ReportPart WHERE ReportID = " + reportID + " AND SortOrder < " + sortOrder + " ORDER BY SortOrder DESC");
				if(rs.Read())
				{
					Db.execute("UPDATE ReportPart SET SortOrder = " + sortOrder + " WHERE ReportPartID = " + rs.GetInt32(0));
					Db.execute("UPDATE ReportPart SET SortOrder = " + rs.GetInt32(1) + " WHERE ReportPartID = " + HttpContext.Current.Request.QueryString["MoveUp"]);
				}
				rs.Close();

				HttpContext.Current.Response.Redirect("reportSetup.aspx?ReportID=" + reportID, true);
			}

			if(HttpContext.Current.Request.QueryString["IdxMoveUp"] != null)
			{
				int sortOrder = Db.getInt32("SELECT SortOrder FROM ReportPartComponent WHERE ReportPartComponentID = " + HttpContext.Current.Request.QueryString["IdxMoveUp"]);
				rs = Db.recordSet("SELECT ReportPartComponentID, SortOrder FROM ReportPartComponent WHERE ReportPartID = " + idxPartID + " AND SortOrder < " + sortOrder + " ORDER BY SortOrder DESC");
				if(rs.Read())
				{
					Db.execute("UPDATE ReportPartComponent SET SortOrder = " + sortOrder + " WHERE ReportPartComponentID = " + rs.GetInt32(0));
					Db.execute("UPDATE ReportPartComponent SET SortOrder = " + rs.GetInt32(1) + " WHERE ReportPartComponentID = " + HttpContext.Current.Request.QueryString["IdxMoveUp"]);
				}
				rs.Close();

				HttpContext.Current.Response.Redirect("reportSetup.aspx?IdxPartID=" + idxPartID + "&ReportID=" + reportID, true);
			}

			if(HttpContext.Current.Request.QueryString["WqoMoveUp"] != null)
			{
				int sortOrder = Db.getInt32("SELECT SortOrder FROM ReportPartComponent WHERE ReportPartComponentID = " + HttpContext.Current.Request.QueryString["WqoMoveUp"]);
				rs = Db.recordSet("SELECT ReportPartComponentID, SortOrder FROM ReportPartComponent WHERE ReportPartID = " + wqoPartID + " AND SortOrder < " + sortOrder + " ORDER BY SortOrder DESC");
				if(rs.Read())
				{
					Db.execute("UPDATE ReportPartComponent SET SortOrder = " + sortOrder + " WHERE ReportPartComponentID = " + rs.GetInt32(0));
					Db.execute("UPDATE ReportPartComponent SET SortOrder = " + rs.GetInt32(1) + " WHERE ReportPartComponentID = " + HttpContext.Current.Request.QueryString["WqoMoveUp"]);
				}
				rs.Close();

				HttpContext.Current.Response.Redirect("reportSetup.aspx?WqoPartID=" + wqoPartID + "&ReportID=" + reportID, true);
			}

			if(reportID != 0)
			{
				EditPart.Visible = true;

				int cx = 0;
				Part.Text = "<tr><td colspan=\"4\">&nbsp;</td></tr>";
				rs = Db.recordSet("SELECT ReportPartID, Internal, Type FROM ReportPart WHERE ReportID = " + reportID + " ORDER BY SortOrder");
				while(rs.Read())
				{
					Part.Text += "<TR><TD ALIGN=\"RIGHT\">" + (cx++ > 0 ? "<A HREF=\"reportSetup.aspx?ReportID=" + reportID + "&MoveUp=" + rs.GetInt32(0) + "\"><IMG SRC=\"../img/UpToolSmall.gif\" border=\"0\"></A>" : "") + "</TD><TD COLSPAN=\"3\"><A HREF=\"reportSetup.aspx?ReportID=" + reportID + "&Delete=" + rs.GetInt32(0) + "\"><IMG SRC=\"../img/DelToolSmall.gif\" border=\"0\" align=\"right\"></A>&nbsp;<A HREF=\"reportSetup.aspx?ReportID=" + reportID + "&";
					switch(rs.GetInt32(2))
					{
						case 1:		Part.Text += "QuestionPartID="; break;
						case 8:		goto case 9;
						case 9:		Part.Text += "WqoPartID="; break;
						default:	Part.Text += "IdxPartID="; break;
					}
					Part.Text += rs.GetInt32(0) + "\">" + rs.GetString(1) + "</A>&nbsp;&nbsp;</TD></TR>";
				}
				rs.Close();

				cx = 0;
				rs = Db.recordSet("SELECT LangID FROM Lang ORDER BY LangID ASC");
				while(rs.Read())
				{
					QuestionText.Controls.Add(new LiteralControl("<tr><td valign=\"top\" align=\"right\">"));
					if(cx == 0)
					{
						QuestionText.Controls.Add(new LiteralControl("Page Header&nbsp;"));
					}
					QuestionText.Controls.Add(new LiteralControl("<img SRC=\"../img/lang/_" + rs.GetInt32(0) + ".gif\"></td><td colspan=\"3\">&nbsp;"));
					TextBox text = new TextBox();
					text.Width = Unit.Pixel(300);
					text.ID = "QuestionSubject" + rs.GetInt32(0);
					QuestionText.Controls.Add(text);
					QuestionText.Controls.Add(new LiteralControl("</td></tr>"));

					WqoText.Controls.Add(new LiteralControl("<tr><td valign=\"top\" align=\"right\">"));
					if(cx == 0)
					{
						WqoText.Controls.Add(new LiteralControl("Page Header&nbsp;"));
					}
					WqoText.Controls.Add(new LiteralControl("<img SRC=\"../img/lang/_" + rs.GetInt32(0) + ".gif\"></td><td colspan=\"3\">&nbsp;"));
					text = new TextBox();
					text.Width = Unit.Pixel(500);
					text.ID = "WqoSubject" + rs.GetInt32(0);
					WqoText.Controls.Add(text);
					WqoText.Controls.Add(new LiteralControl("</td></tr>"));

					IdxText.Controls.Add(new LiteralControl("<tr><td valign=\"top\" align=\"right\">"));
					if(cx++ == 0)
					{
						IdxText.Controls.Add(new LiteralControl("Page Header&nbsp;"));
					}
					IdxText.Controls.Add(new LiteralControl("<img SRC=\"../img/lang/_" + rs.GetInt32(0) + ".gif\"></td><td colspan=\"3\">&nbsp;"));
					text = new TextBox();
					text.Width = Unit.Pixel(300);
					text.ID = "IdxSubject" + rs.GetInt32(0);
					IdxText.Controls.Add(text);
					IdxText.Controls.Add(new LiteralControl("</td></tr>"));
				}
				rs.Close();
				cx = 0;
				rs = Db.recordSet("SELECT LangID FROM Lang ORDER BY LangID ASC");
				while(rs.Read())
				{
					QuestionText.Controls.Add(new LiteralControl("<tr><td valign=\"top\" align=\"right\">"));
					if(cx == 0)
					{
						QuestionText.Controls.Add(new LiteralControl("Top text&nbsp;"));
					}
					QuestionText.Controls.Add(new LiteralControl("<img SRC=\"../img/lang/_" + rs.GetInt32(0) + ".gif\"></td><td colspan=\"3\">&nbsp;"));
					TextBox text = new TextBox();
					text.Width = Unit.Pixel(300);
					text.Rows = 3;
					text.TextMode = TextBoxMode.MultiLine;
					text.ID = "QuestionTop" + rs.GetInt32(0);
					QuestionText.Controls.Add(text);
					QuestionText.Controls.Add(new LiteralControl("</td></tr>"));

					WqoText.Controls.Add(new LiteralControl("<tr><td valign=\"top\" align=\"right\">"));
					if(cx == 0)
					{
						WqoText.Controls.Add(new LiteralControl("Top text&nbsp;"));
					}
					WqoText.Controls.Add(new LiteralControl("<img SRC=\"../img/lang/_" + rs.GetInt32(0) + ".gif\"></td><td colspan=\"3\">&nbsp;"));
					text = new TextBox();
					text.Width = Unit.Pixel(500);
					text.Rows = 10;
					text.TextMode = TextBoxMode.MultiLine;
					text.ID = "WqoTop" + rs.GetInt32(0);
					WqoText.Controls.Add(text);
					WqoText.Controls.Add(new LiteralControl("</td></tr>"));

					IdxText.Controls.Add(new LiteralControl("<tr><td valign=\"top\" align=\"right\">"));
					if(cx++ == 0)
					{
						IdxText.Controls.Add(new LiteralControl("Top text&nbsp;"));
					}
					IdxText.Controls.Add(new LiteralControl("<img SRC=\"../img/lang/_" + rs.GetInt32(0) + ".gif\"></td><td colspan=\"3\">&nbsp;"));
					text = new TextBox();
					text.Width = Unit.Pixel(300);
					text.Rows = 3;
					text.TextMode = TextBoxMode.MultiLine;
					text.ID = "IdxTop" + rs.GetInt32(0);
					IdxText.Controls.Add(text);
					IdxText.Controls.Add(new LiteralControl("</td></tr>"));
				}
				rs.Close();
				cx = 0;
				rs = Db.recordSet("SELECT LangID FROM Lang ORDER BY LangID ASC");
				while(rs.Read())
				{
					QuestionText.Controls.Add(new LiteralControl("<tr><td valign=\"top\" align=\"right\">"));
					if(cx == 0)
					{
						QuestionText.Controls.Add(new LiteralControl("Bottom text&nbsp;"));
					}
					QuestionText.Controls.Add(new LiteralControl("<img SRC=\"../img/lang/_" + rs.GetInt32(0) + ".gif\"></td><td colspan=\"3\">&nbsp;"));
					TextBox text = new TextBox();
					text.Width = Unit.Pixel(300);
					text.Rows = 3;
					text.TextMode = TextBoxMode.MultiLine;
					text.ID = "QuestionBottom" + rs.GetInt32(0);
					QuestionText.Controls.Add(text);
					QuestionText.Controls.Add(new LiteralControl("</td></tr>"));

					WqoText.Controls.Add(new LiteralControl("<tr><td valign=\"top\" align=\"right\">"));
					if(cx == 0)
					{
						WqoText.Controls.Add(new LiteralControl("Bottom text&nbsp;"));
					}
					WqoText.Controls.Add(new LiteralControl("<img SRC=\"../img/lang/_" + rs.GetInt32(0) + ".gif\"></td><td colspan=\"3\">&nbsp;"));
					text = new TextBox();
					text.Width = Unit.Pixel(500);
					text.Rows = 10;
					text.TextMode = TextBoxMode.MultiLine;
					text.ID = "WqoBottom" + rs.GetInt32(0);
					WqoText.Controls.Add(text);
					WqoText.Controls.Add(new LiteralControl("</td></tr>"));

					IdxText.Controls.Add(new LiteralControl("<tr><td valign=\"top\" align=\"right\">"));
					if(cx++ == 0)
					{
						IdxText.Controls.Add(new LiteralControl("Bottom text&nbsp;"));
					}
					IdxText.Controls.Add(new LiteralControl("<img SRC=\"../img/lang/_" + rs.GetInt32(0) + ".gif\"></td><td colspan=\"3\">&nbsp;"));
					text = new TextBox();
					text.Width = Unit.Pixel(300);
					text.Rows = 3;
					text.TextMode = TextBoxMode.MultiLine;
					text.ID = "IdxBottom" + rs.GetInt32(0);
					IdxText.Controls.Add(text);
					IdxText.Controls.Add(new LiteralControl("</td></tr>"));
				}
				rs.Close();

				cx = 0;
				rs = Db.recordSet("SELECT LangID FROM Lang ORDER BY LangID ASC");
				while(rs.Read())
				{
					WqoText.Controls.Add(new LiteralControl("<tr><td valign=\"top\" align=\"right\">"));
					if(cx == 0)
					{
						WqoText.Controls.Add(new LiteralControl("Alternative text&nbsp;"));
					}
					WqoText.Controls.Add(new LiteralControl("<img SRC=\"../img/lang/_" + rs.GetInt32(0) + ".gif\"></td><td colspan=\"3\">&nbsp;"));
					TextBox text = new TextBox();
					text = new TextBox();
					text.Width = Unit.Pixel(500);
					text.Rows = 10;
					text.TextMode = TextBoxMode.MultiLine;
					text.ID = "WqoAlt" + rs.GetInt32(0);
					WqoText.Controls.Add(text);
					WqoText.Controls.Add(new LiteralControl("</td></tr>"));
				}
				rs.Close();
				
				if(!IsPostBack)
				{
					rs = Db.recordSet("SELECT Internal FROM Report WHERE ReportID = " + reportID);
					if(rs.Read())
					{
						Internal.Text = rs.GetString(0);
					}
					rs.Close();
					rs = Db.recordSet("SELECT " +
						"q.Internal, " +
						"o.Internal, " +
						"q.QuestionID, " +
						"o.OptionID " +
						"FROM Question q " +
						"INNER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID " +
						"INNER JOIN [Option] o ON qo.OptionID = o.OptionID");
					while(rs.Read())
					{
						QuestionIDOptionID.Items.Add(new ListItem(rs.GetString(0) + " / " + rs.GetString(1),rs.GetInt32(2) + "," + rs.GetInt32(3)));
					}
					rs.Close();
					rs = Db.recordSet("SELECT IdxID, Internal FROM Idx ORDER BY SortOrder");
					while(rs.Read())
					{
						IdxID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
					}
					rs.Close();

					rs = Db.recordSet("SELECT WeightedQuestionOptionID, Internal FROM WeightedQuestionOption ORDER BY SortOrder");
					while(rs.Read())
					{
						WeightedQuestionOptionID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
					}
					rs.Close();
				}
			}

			Save.Click += new EventHandler(Save_Click);
			AddQuestionPart.Click += new EventHandler(AddQuestionPart_Click);
			SaveQuestionPart.Click += new EventHandler(SaveQuestionPart_Click);
			AddWqoPart.Click += new EventHandler(AddWqoPart_Click);
			SaveWqoPart.Click += new EventHandler(SaveWqoPart_Click);
			AddIdxPart.Click += new EventHandler(AddIdxPart_Click);
			SaveIdxPart.Click += new EventHandler(SaveIdxPart_Click);
			QuestionIDOptionID.SelectedIndexChanged += new EventHandler(QuestionIDOptionID_SelectedIndexChanged);

			if(!IsPostBack)
			{
				if(questionPartID != 0)
				{
					showQuestionPart();
					rs = Db.recordSet("SELECT Subject, Header, Footer, LangID FROM ReportPartLang WHERE ReportPartID = " + questionPartID);
					while(rs.Read())
					{
						((TextBox)QuestionText.FindControl("QuestionSubject" + rs.GetInt32(3))).Text = rs.GetString(0);
						((TextBox)QuestionText.FindControl("QuestionTop" + rs.GetInt32(3))).Text = rs.GetString(1);
						((TextBox)QuestionText.FindControl("QuestionBottom" + rs.GetInt32(3))).Text = rs.GetString(2);
					}
					rs.Close();
					rs = Db.recordSet("SELECT QuestionID, OptionID, RequiredAnswerCount, Internal FROM ReportPart WHERE ReportPartID = " + questionPartID);
					if(rs.Read())
					{
						QuestionIDOptionID.SelectedValue = rs.GetInt32(0).ToString() + "," + rs.GetInt32(1).ToString();
						QuestionRequiredAnswerCount.Text = rs.GetInt32(2).ToString();
						QuestionInternal.Text = rs.GetString(3);
					}
					rs.Close();
				}
				if(idxPartID != 0)
				{
					showIdxPart();
					rs = Db.recordSet("SELECT Subject, Header, Footer, LangID FROM ReportPartLang WHERE ReportPartID = " + idxPartID);
					while(rs.Read())
					{
						((TextBox)IdxText.FindControl("IdxSubject" + rs.GetInt32(3))).Text = rs.GetString(0);
						((TextBox)IdxText.FindControl("IdxTop" + rs.GetInt32(3))).Text = rs.GetString(1);
						((TextBox)IdxText.FindControl("IdxBottom" + rs.GetInt32(3))).Text = rs.GetString(2);
					}
					rs.Close();
					rs = Db.recordSet("SELECT Internal FROM ReportPart WHERE ReportPartID = " + idxPartID);
					if(rs.Read())
					{
						IdxInternal.Text = rs.GetString(0);
					}
					rs.Close();

					int cx = 0;
					Idxes.Text = "<tr><td colspan=\"4\">&nbsp;</td></tr>";
					rs = Db.recordSet("SELECT IdxID,ReportPartComponentID FROM ReportPartComponent WHERE ReportPartID = " + idxPartID + " ORDER BY SortOrder");
					while(rs.Read())
					{
						Idxes.Text += "<TR><TD ALIGN=\"RIGHT\">" + (cx++ > 0 ? "<A HREF=\"reportSetup.aspx?ReportID=" + reportID + "&IdxPartID=" + idxPartID + "&IdxMoveUp=" + rs.GetInt32(1) + "\"><IMG SRC=\"../img/UpToolSmall.gif\" border=\"0\"></A>" : "") + "</TD><TD COLSPAN=\"3\">&nbsp;" + IdxID.Items.FindByValue(rs.GetInt32(0).ToString()).Text + "</TD></TR>";
						IdxID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected = true;
					}
					rs.Close();
					Idxes.Text += "<tr><td colspan=\"4\">&nbsp;</td></tr>";
				}
				if(wqoPartID != 0)
				{
					showWqoPart();
					rs = Db.recordSet("SELECT Subject, Header, Footer, LangID, AltText FROM ReportPartLang WHERE ReportPartID = " + wqoPartID);
					while(rs.Read())
					{
						((TextBox)IdxText.FindControl("WqoSubject" + rs.GetInt32(3))).Text = rs.GetString(0);
						((TextBox)IdxText.FindControl("WqoTop" + rs.GetInt32(3))).Text = rs.GetString(1);
						((TextBox)IdxText.FindControl("WqoBottom" + rs.GetInt32(3))).Text = rs.GetString(2);
						((TextBox)IdxText.FindControl("WqoAlt" + rs.GetInt32(3))).Text = (rs.IsDBNull(4) ? "" : rs.GetString(4));
					}
					rs.Close();
					rs = Db.recordSet("SELECT Internal, RequiredAnswerCount, Type FROM ReportPart WHERE ReportPartID = " + wqoPartID);
					if(rs.Read())
					{
						WqoInternal.Text = rs.GetString(0);
						if(!rs.IsDBNull(1))
						{
							WqoRequiredAnswerCount.Text = rs.GetInt32(1).ToString();
						}
						WqoOvertime.Checked = (rs.GetInt32(2) == 8);
					}
					rs.Close();

					int cx = 0;
					Wqos.Text = "<tr><td colspan=\"4\">&nbsp;</td></tr>";
					rs = Db.recordSet("SELECT WeightedQuestionOptionID, ReportPartComponentID FROM ReportPartComponent WHERE ReportPartID = " + wqoPartID + " ORDER BY SortOrder");
					while(rs.Read())
					{
						Wqos.Text += "<TR><TD ALIGN=\"RIGHT\">" + (cx++ > 0 ? "<A HREF=\"reportSetup.aspx?ReportID=" + reportID + "&WqoPartID=" + wqoPartID + "&WqoMoveUp=" + rs.GetInt32(1) + "\"><IMG SRC=\"../img/UpToolSmall.gif\" border=\"0\"></A>" : "") + "</TD><TD COLSPAN=\"3\">&nbsp;" + WeightedQuestionOptionID.Items.FindByValue(rs.GetInt32(0).ToString()).Text + "</TD></TR>";
						WeightedQuestionOptionID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected = true;
					}
					rs.Close();
					Wqos.Text += "<tr><td colspan=\"4\">&nbsp;</td></tr>";
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

		private void Save_Click(object sender, EventArgs e)
		{
			if(reportID != 0)
			{
				Db.execute("UPDATE Report SET Internal = '" + Internal.Text.Replace("'","''") + "' WHERE ReportID = " + reportID);
			}
			else
			{
				Db.execute("INSERT INTO Report (Internal) VALUES ('" + Internal.Text.Replace("'","''") + "')");
				OdbcDataReader rs = Db.recordSet("SELECT ReportID FROM Report ORDER BY ReportID DESC");
				if(rs.Read())
				{
					reportID = rs.GetInt32(0);
				}
				rs.Close();
			}

			HttpContext.Current.Response.Redirect("reportSetup.aspx?ReportID=" + reportID,true);
		}
		private void showIdxPart()
		{
			EditPart.Visible = false;
			EditIdxPart.Visible = true;
			EditWqoPart.Visible = false;
			Save.Visible = false;
			Internal.ReadOnly = true;
		}
		private void showQuestionPart()
		{
			EditPart.Visible = false;
			EditQuestionPart.Visible = true;
			EditWqoPart.Visible = false;
			Save.Visible = false;
			Internal.ReadOnly = true;
		}
		private void showWqoPart()
		{
			EditPart.Visible = false;
			EditQuestionPart.Visible = false;
			EditWqoPart.Visible = true;
			Save.Visible = false;
			Internal.ReadOnly = true;
		}
		private void AddQuestionPart_Click(object sender, EventArgs e)
		{
			showQuestionPart();
			OdbcDataReader rs = Db.recordSet("SELECT Question, LangID FROM QuestionLang WHERE QuestionID = " + QuestionIDOptionID.SelectedValue.Split(',')[0]);
			while(rs.Read())
			{
				((TextBox)QuestionText.FindControl("QuestionSubject" + rs.GetInt32(1))).Text = rs.GetString(0);
			}
			rs.Close();
			rs = Db.recordSet("SELECT q.Internal, o.Internal FROM Question q INNER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID INNER JOIN [Option] o ON qo.OptionID = o.OptionID WHERE q.QuestionID = " + QuestionIDOptionID.SelectedValue.Split(',')[0] + " AND o.OptionID = " + QuestionIDOptionID.SelectedValue.Split(',')[1]);
			while(rs.Read())
			{
				QuestionInternal.Text = rs.GetString(0) + " / " + rs.GetString(1);
			}
			rs.Close();
		}

		private void SaveQuestionPart_Click(object sender, EventArgs e)
		{
			OdbcDataReader rs;
			if(questionPartID != 0)
			{
				Db.execute("UPDATE ReportPart SET Internal = '" + QuestionInternal.Text.Replace("'","''") + "', QuestionID = " + QuestionIDOptionID.SelectedValue.Split(',')[0] + ", OptionID = " + QuestionIDOptionID.SelectedValue.Split(',')[1] + ", RequiredAnswerCount = " + Convert.ToInt32("0" + QuestionRequiredAnswerCount.Text) + " WHERE ReportPartID = " + questionPartID);
				Db.execute("DELETE FROM ReportPartLang WHERE ReportPartID = " + questionPartID);
			}
			else
			{
				Db.execute("INSERT INTO ReportPart (ReportID,Internal,Type,QuestionID,OptionID,RequiredAnswerCount) VALUES (" + reportID + ",'" + QuestionInternal.Text.Replace("'","''") + "',1," + QuestionIDOptionID.SelectedValue.Split(',')[0] + "," + QuestionIDOptionID.SelectedValue.Split(',')[1] + "," + Convert.ToInt32("0" + QuestionRequiredAnswerCount.Text) + ")");
				rs = Db.recordSet("SELECT TOP 1 ReportPartID FROM ReportPart WHERE ReportID = " + reportID + " ORDER BY ReportPartID DESC");
				if(rs.Read())
				{
					questionPartID = rs.GetInt32(0);
				}
				rs.Close();
				Db.execute("UPDATE ReportPart SET SortOrder = " + questionPartID + " WHERE ReportPartID = " + questionPartID);
			}
			rs = Db.recordSet("SELECT LangID FROM Lang ORDER BY LangID ASC");
			while(rs.Read())
			{
				Db.execute("INSERT INTO ReportPartLang (ReportPartID,LangID,Subject,Header,Footer) VALUES (" + questionPartID + "," + rs.GetInt32(0) + ",'" + ((TextBox)QuestionText.FindControl("QuestionSubject" + rs.GetInt32(0))).Text.Replace("'","''") + "','" + ((TextBox)QuestionText.FindControl("QuestionTop" + rs.GetInt32(0))).Text.Replace("'","''") + "','" + ((TextBox)QuestionText.FindControl("QuestionBottom" + rs.GetInt32(0))).Text.Replace("'","''") + "')");
			}
			rs.Close();
			HttpContext.Current.Response.Redirect("reportSetup.aspx?ReportID=" + reportID,true);
		}

		private void QuestionIDOptionID_SelectedIndexChanged(object sender, EventArgs e)
		{
			OdbcDataReader rs = Db.recordSet("SELECT Question, LangID FROM QuestionLang WHERE QuestionID = " + QuestionIDOptionID.SelectedValue.Split(',')[0]);
			while(rs.Read())
			{
				((TextBox)QuestionText.FindControl("QuestionSubject" + rs.GetInt32(1))).Text = rs.GetString(0);
			}
			rs.Close();
			rs = Db.recordSet("SELECT q.Internal, o.Internal FROM Question q INNER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID INNER JOIN [Option] o ON qo.OptionID = o.OptionID WHERE q.QuestionID = " + QuestionIDOptionID.SelectedValue.Split(',')[0] + " AND o.OptionID = " + QuestionIDOptionID.SelectedValue.Split(',')[1]);
			while(rs.Read())
			{
				QuestionInternal.Text = rs.GetString(0) + " / " + rs.GetString(1);
			}
			rs.Close();
		}

		private void AddIdxPart_Click(object sender, EventArgs e)
		{
			showIdxPart();
		}

		private void SaveIdxPart_Click(object sender, EventArgs e)
		{
			OdbcDataReader rs;
			if(idxPartID != 0)
			{
				Db.execute("UPDATE ReportPart SET Internal = '" + IdxInternal.Text.Replace("'","''") + "' WHERE ReportPartID = " + idxPartID);
				Db.execute("DELETE FROM ReportPartLang WHERE ReportPartID = " + idxPartID);
			}
			else
			{
				Db.execute("INSERT INTO ReportPart (ReportID,Internal,Type) VALUES (" + reportID + ",'" + IdxInternal.Text.Replace("'","''") + "',2)");
				rs = Db.recordSet("SELECT TOP 1 ReportPartID FROM ReportPart WHERE ReportID = " + reportID + " ORDER BY ReportPartID DESC");
				if(rs.Read())
				{
					idxPartID = rs.GetInt32(0);
				}
				rs.Close();
				Db.execute("UPDATE ReportPart SET SortOrder = " + idxPartID + " WHERE ReportPartID = " + idxPartID);
			}
			rs = Db.recordSet("SELECT IdxID FROM Idx ORDER BY SortOrder");
			while(rs.Read())
			{
				if(IdxID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected)
				{
					bool exist = false;
					OdbcDataReader rs2 = Db.recordSet("SELECT IdxID FROM ReportPartComponent WHERE ReportPartID = " + idxPartID + " AND IdxID = " + rs.GetInt32(0));
					if(rs2.Read())
					{
						exist = true;
					}
					rs2.Close();
					if(!exist)
					{
						Db.execute("INSERT INTO ReportPartComponent (ReportPartID,IdxID) VALUES (" + idxPartID + "," + rs.GetInt32(0) + ")");
						rs2 = Db.recordSet("SELECT ReportPartComponentID FROM ReportPartComponent ORDER BY ReportPartComponentID DESC");
						if(rs2.Read())
						{
							Db.execute("UPDATE ReportPartComponent SET SortOrder = " + rs2.GetInt32(0) + " WHERE ReportPartComponentID = " + rs2.GetInt32(0));
						}
						rs2.Close();
					}
				}
				else
				{
					Db.execute("DELETE FROM ReportPartComponent WHERE IdxID = " + rs.GetInt32(0) + " AND ReportPartID = " + idxPartID);
				}
			}
			rs.Close();

			rs = Db.recordSet("SELECT LangID FROM Lang ORDER BY LangID ASC");
			while(rs.Read())
			{
				Db.execute("INSERT INTO ReportPartLang (ReportPartID,LangID,Subject,Header,Footer) VALUES (" + idxPartID + "," + rs.GetInt32(0) + ",'" + ((TextBox)IdxText.FindControl("IdxSubject" + rs.GetInt32(0))).Text.Replace("'","''") + "','" + ((TextBox)IdxText.FindControl("IdxTop" + rs.GetInt32(0))).Text.Replace("'","''") + "','" + ((TextBox)IdxText.FindControl("IdxBottom" + rs.GetInt32(0))).Text.Replace("'","''") + "')");
			}
			rs.Close();
			HttpContext.Current.Response.Redirect("reportSetup.aspx?ReportID=" + reportID,true);
		}

		private void AddWqoPart_Click(object sender, EventArgs e)
		{
			showWqoPart();
		}

		private void SaveWqoPart_Click(object sender, EventArgs e)
		{
			OdbcDataReader rs;
			if(wqoPartID != 0)
			{
				Db.execute("UPDATE ReportPart SET Type = " + (WqoOvertime.Checked ? "8" : "9") + ", Internal = '" + WqoInternal.Text.Replace("'","''") + "', RequiredAnswerCount = " + Convert.ToInt32("0" + WqoRequiredAnswerCount.Text) + " WHERE ReportPartID = " + wqoPartID);
				Db.execute("DELETE FROM ReportPartLang WHERE ReportPartID = " + wqoPartID);
			}
			else
			{
				Db.execute("INSERT INTO ReportPart (RequiredAnswerCount,ReportID,Internal,Type) VALUES (" + Convert.ToInt32("0" + WqoRequiredAnswerCount.Text) + "," + reportID + ",'" + WqoInternal.Text.Replace("'","''") + "'," + (WqoOvertime.Checked ? "8" : "9") + ")");
				rs = Db.recordSet("SELECT TOP 1 ReportPartID FROM ReportPart WHERE ReportID = " + reportID + " ORDER BY ReportPartID DESC");
				if(rs.Read())
				{
					wqoPartID = rs.GetInt32(0);
				}
				rs.Close();
				Db.execute("UPDATE ReportPart SET SortOrder = " + wqoPartID + " WHERE ReportPartID = " + wqoPartID);
			}
			rs = Db.recordSet("SELECT WeightedQuestionOptionID FROM WeightedQuestionOption ORDER BY SortOrder");
			while(rs.Read())
			{
				if(WeightedQuestionOptionID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected)
				{
					bool exist = false;
					OdbcDataReader rs2 = Db.recordSet("SELECT WeightedQuestionOptionID FROM ReportPartComponent WHERE ReportPartID = " + wqoPartID + " AND WeightedQuestionOptionID = " + rs.GetInt32(0));
					if(rs2.Read())
					{
						exist = true;
					}
					rs2.Close();
					if(!exist)
					{
						Db.execute("INSERT INTO ReportPartComponent (ReportPartID,WeightedQuestionOptionID) VALUES (" + wqoPartID + "," + rs.GetInt32(0) + ")");
						rs2 = Db.recordSet("SELECT ReportPartComponentID FROM ReportPartComponent ORDER BY ReportPartComponentID DESC");
						if(rs2.Read())
						{
							Db.execute("UPDATE ReportPartComponent SET SortOrder = " + rs2.GetInt32(0) + " WHERE ReportPartComponentID = " + rs2.GetInt32(0));
						}
						rs2.Close();
					}
				}
				else
				{
					Db.execute("DELETE FROM ReportPartComponent WHERE WeightedQuestionOptionID = " + rs.GetInt32(0) + " AND ReportPartID = " + wqoPartID);
				}
			}
			rs.Close();

			rs = Db.recordSet("SELECT LangID FROM Lang ORDER BY LangID ASC");
			while(rs.Read())
			{
				Db.execute("INSERT INTO ReportPartLang (ReportPartID,LangID,Subject,Header,Footer,AltText) VALUES (" + wqoPartID + "," + rs.GetInt32(0) + ",'" + ((TextBox)IdxText.FindControl("WqoSubject" + rs.GetInt32(0))).Text.Replace("'","''") + "','" + ((TextBox)IdxText.FindControl("WqoTop" + rs.GetInt32(0))).Text.Replace("'","''") + "','" + ((TextBox)IdxText.FindControl("WqoBottom" + rs.GetInt32(0))).Text.Replace("'","''") + "','" + ((TextBox)IdxText.FindControl("WqoAlt" + rs.GetInt32(0))).Text.Replace("'","''") + "')");
			}
			rs.Close();
			HttpContext.Current.Response.Redirect("reportSetup.aspx?ReportID=" + reportID,true);
		}
	}
}
