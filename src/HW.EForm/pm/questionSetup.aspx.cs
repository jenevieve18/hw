using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace eform
{
	/// <summary>
	/// Summary description for questionSetup.
	/// </summary>
	public class questionSetup : System.Web.UI.Page
	{
		protected TextBox Internal;
		protected TextBox Variablename;
		protected DropDownList FontFamily;
		protected DropDownList FontSize;
		protected DropDownList FontDecoration;
		protected DropDownList FontColor;
		protected CheckBox Underlined;
		protected DropDownList OptionsPlacement;
		protected PlaceHolder Options;
		protected PlaceHolder QuestionLang;
		protected Button Save;
		protected CheckBox Box;
		protected DropDownList QuestionContainerID;
		protected DropDownList OptionContainerID;

		int questionID = 0;

		private void Page_Load(object sender, System.EventArgs e)
		{
			SqlDataReader rs;

			questionID = (HttpContext.Current.Request.QueryString["QuestionID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["QuestionID"]) : 0);

			Save.Click += new EventHandler(Save_Click);

			OptionContainerID.SelectedIndexChanged += new EventHandler(OptionContainerID_SelectedIndexChanged);
			if(!IsPostBack)
			{
				QuestionContainerID.Items.Add(new ListItem("Uncategorized","0"));
				rs = Db.sqlRecordSet("SELECT QuestionContainerID, QuestionContainer FROM QuestionContainer");
				while(rs.Read())
				{
					QuestionContainerID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				rs.Close();
				QuestionContainerID.SelectedValue = Convert.ToInt32("0" + HttpContext.Current.Session["QuestionContainerID"]).ToString();

				OptionContainerID.Items.Add(new ListItem("Uncategorized","0"));
				rs = Db.sqlRecordSet("SELECT OptionContainerID, OptionContainer FROM OptionContainer");
				while(rs.Read())
				{
					OptionContainerID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				rs.Close();
				OptionContainerID.SelectedValue = Convert.ToInt32("0" + HttpContext.Current.Session["OptionContainerID"]).ToString();
			}

			if(HttpContext.Current.Request.QueryString["MoveUp"] != null)
			{
				int sortOrder = Db.sqlGetInt32("SELECT SortOrder FROM QuestionOption WHERE QuestionOptionID = " + HttpContext.Current.Request.QueryString["MoveUp"]);
				rs = Db.sqlRecordSet("SELECT QuestionOptionID, SortOrder FROM QuestionOption WHERE QuestionID = " + questionID + " AND SortOrder < " + sortOrder + " ORDER BY SortOrder DESC");
				if(rs.Read())
				{
					Db.sqlExecute("UPDATE QuestionOption SET SortOrder = " + sortOrder + " WHERE QuestionOptionID = " + rs.GetInt32(0));
					Db.sqlExecute("UPDATE QuestionOption SET SortOrder = " + rs.GetInt32(1) + " WHERE QuestionOptionID = " + HttpContext.Current.Request.QueryString["MoveUp"]);
				}
				rs.Close();

				HttpContext.Current.Response.Redirect("questionSetup.aspx?QuestionID=" + questionID, true);
			}

			int cx = 0;
			rs = Db.sqlRecordSet("SELECT LangID FROM Lang ORDER BY LangID ASC");
			while(rs.Read())
			{
				QuestionLang.Controls.Add(new LiteralControl("<tr><td valign=\"top\" align=\"right\">Question&nbsp;<img SRC=\"../img/lang/" + rs.GetInt32(0) + ".gif\"></td><td colspan=\"3\">&nbsp;"));
				TextBox text = new TextBox();
				//text.Width = Unit.Pixel(470);
				text.Attributes["style"] = "width:470px;";
				text.Rows = 2;
				text.TextMode = TextBoxMode.MultiLine;
				text.ID = "Text" + rs.GetInt32(0);
				QuestionLang.Controls.Add(text);
				QuestionLang.Controls.Add(new LiteralControl("</td></tr>"));

				QuestionLang.Controls.Add(new LiteralControl("<tr><td valign=\"top\" align=\"right\">Area&nbsp;</td><td colspan=\"3\">&nbsp;"));
				text = new TextBox();
				//text.Width = Unit.Pixel(470);
				text.Attributes["style"] = "width:470px;";
				text.ID = "AreaText" + rs.GetInt32(0);
				QuestionLang.Controls.Add(text);
				QuestionLang.Controls.Add(new LiteralControl("</td></tr>"));

				QuestionLang.Controls.Add(new LiteralControl("<tr><td valign=\"top\" align=\"right\">For report&nbsp;</td><td colspan=\"3\">&nbsp;"));
				text = new TextBox();
				//text.Width = Unit.Pixel(470);
				text.Attributes["style"] = "width:470px;";
				text.ID = "ReportQuestion" + rs.GetInt32(0);
				QuestionLang.Controls.Add(text);
				QuestionLang.Controls.Add(new LiteralControl("</td></tr>"));
			}
			rs.Close();

			if(!IsPostBack && questionID != 0)
			{
				rs = Db.sqlRecordSet("SELECT Internal, Variablename, OptionsPlacement, FontFamily, FontSize, FontDecoration, Underlined, FontColor, Box, QuestionContainerID FROM [Question] WHERE QuestionID = " + questionID);
				if(rs.Read())
				{
					Internal.Text = rs.GetString(0);
					Variablename.Text = rs.GetString(1);
					OptionsPlacement.SelectedValue = rs.GetInt32(2).ToString();
					FontFamily.SelectedValue = rs.GetInt32(3).ToString();
					FontSize.SelectedValue = rs.GetInt32(4).ToString();
					FontDecoration.SelectedValue = rs.GetInt32(5).ToString();
					Underlined.Checked = (rs.GetInt32(6) == 1);
					FontColor.SelectedValue = rs.GetString(7);
					Box.Checked = (rs.GetInt32(8) == 1);
					QuestionContainerID.SelectedValue = (rs.IsDBNull(9) ? "0" : rs.GetInt32(9).ToString());
				}
				rs.Close();

				rs = Db.sqlRecordSet("SELECT LangID, ISNULL(QuestionJapaneseUnicode,Question), ISNULL(QuestionAreaJapaneseUnicode,QuestionArea), ReportQuestion FROM QuestionLang WHERE QuestionID = " + questionID);
				while(rs.Read())
				{
					((TextBox)QuestionLang.FindControl("Text" + rs.GetInt32(0))).Text = rs.GetString(1);
					((TextBox)QuestionLang.FindControl("AreaText" + rs.GetInt32(0))).Text = (rs.IsDBNull(2) ? "" : rs.GetString(2));
					((TextBox)QuestionLang.FindControl("ReportQuestion" + rs.GetInt32(0))).Text = (rs.IsDBNull(3) ? "" : rs.GetString(3));
				}
				rs.Close();
			}

			Options.Controls.Add(new LiteralControl("<TR><TD>&nbsp;</TD><TD COLSPAN=\"3\" VALIGN=\"TOP\"><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">"));
			cx = 0;
			bool first = true;

			int OCID = Convert.ToInt32("0" + HttpContext.Current.Session["OptionContainerID"]);

			rs = Db.sqlRecordSet("SELECT " +
				"o.OptionID, " +
				"o.Internal, " +
				"o.Variablename, " +
				"o.OptionPlacement, " +
				"qo.QuestionOptionID, " +
				"qo.Variablename, " +
				"qo.OptionPlacement, " +
				"qo.Forced, " +
				"qo.Hide " +
				"FROM [Option] o " +
				"LEFT OUTER JOIN QuestionOption qo ON o.OptionID = qo.OptionID AND qo.QuestionID = " + questionID + " " +
				"WHERE qo.QuestionID IS NOT NULL OR (o.OptionContainerID" + (OCID != 0 ? " = " + OCID + " " : " IS NULL ") + ") " +
				"ORDER BY qo.QuestionID DESC, qo.SortOrder ASC, o.Internal");
			if(rs.Read())
			{
				if(!rs.IsDBNull(4))
				{
					Options.Controls.Add(new LiteralControl("<TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD><u>Selected option(s)</u>&nbsp;&nbsp;</TD><TD><u>Variable&nbsp;name</u>&nbsp;&nbsp;</TD><TD><u>Layout</u>&nbsp;&nbsp;</TD><TD><u>Forced</u></TD><TD><u>Hide</u></TD></TR>"));
				}

				do
				{
					if(!rs.IsDBNull(4))
					{
						Options.Controls.Add(new LiteralControl("<TR><TD>"));
						if(cx != 0)
						{
							Options.Controls.Add(new LiteralControl("<A HREF=\"questionSetup.aspx?QuestionID=" + questionID + "&MoveUp=" + rs.GetInt32(4) + "\"><IMG SRC=\"../img/UpToolSmall.gif\" border=\"0\"></A>"));
						}
						Options.Controls.Add(new LiteralControl("</TD><TD>"));
						CheckBox cb = new CheckBox();
						cb.ID = "QuestionOptionID" + rs.GetInt32(4);
						if(!IsPostBack)
						{
							cb.Checked = true;
						}
						Options.Controls.Add(cb);
						Options.Controls.Add(new LiteralControl("</TD><TD>" + rs.GetString(1) + "&nbsp;&nbsp;</TD><TD>"));
						TextBox vn = new TextBox();
						vn.ID = "Variablename" + rs.GetInt32(4);
						vn.Width = Unit.Pixel(70);
						if(!IsPostBack)
						{
							vn.Text = rs.GetString(5);
						}
						Options.Controls.Add(vn);
						Options.Controls.Add(new LiteralControl("&nbsp;&nbsp;</TD><TD>"));
						DropDownList op = new DropDownList();
						op.ID = "OptionPlacement" + rs.GetInt32(4);
						op.Items.Add(new ListItem("Horizontal, labels top","1"));
						//op.Items.Add(new ListItem("Horizontal, labels left","2"));
						op.Items.Add(new ListItem("Horizontal, labels right","3"));
						//op.Items.Add(new ListItem("Horizontal, labels bottom","4"));
						op.Items.Add(new ListItem("Horizontal, no labels","5"));
						//op.Items.Add(new ListItem("Vertical, labels top","6"));
						//op.Items.Add(new ListItem("Vertical, labels left","7"));
						op.Items.Add(new ListItem("Vertical, labels right","8"));
						//op.Items.Add(new ListItem("Vertical, labels bottom","9"));
						//op.Items.Add(new ListItem("Vertical, no labels","10"));
						if(!IsPostBack)
						{
							op.SelectedValue = rs.GetInt32(6).ToString();
						}
						Options.Controls.Add(op);
						Options.Controls.Add(new LiteralControl("</TD><TD>"));
						CheckBox f = new CheckBox();
						f.ID = "Forced" + rs.GetInt32(4);
						if(!IsPostBack)
						{
							f.Checked = (rs.GetInt32(7) == 1);
						}
						Options.Controls.Add(f);
						Options.Controls.Add(new LiteralControl("</TD><TD>"));
						CheckBox h = new CheckBox();
						h.ID = "Hide" + rs.GetInt32(4);
						if(!IsPostBack)
						{
							h.Checked = !rs.IsDBNull(8);
						}
						Options.Controls.Add(h);
						Options.Controls.Add(new LiteralControl("</TD></TR>"));
						cx++;
					}
					else
					{
						if(first)
						{
							Options.Controls.Add(new LiteralControl("</TABLE></TD></TR><TR><TD>&nbsp;</TD><TD COLSPAN=\"3\" VALIGN=\"TOP\"><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\"><TR><TD>&nbsp;</TD><TD><u>Available option(s)</u>&nbsp;&nbsp;</TD><TD><u>Default variable name</u>&nbsp;&nbsp;</TD><TD><u>Default layout</u></TD></TR>"));
							first = false;
						}
						Options.Controls.Add(new LiteralControl("<TR><TD>"));
						CheckBox cb = new CheckBox();
						cb.ID = "OptionID" + rs.GetInt32(0);
						Options.Controls.Add(cb);
						Options.Controls.Add(new LiteralControl("</TD><TD>" + rs.GetString(1) + "&nbsp;&nbsp;</TD><TD>" + rs.GetString(2) + "&nbsp;&nbsp;</TD><TD>"));
						switch(rs.GetInt32(3))
						{
							case 1: Options.Controls.Add(new LiteralControl("Horizontal, labels top")); break;
							//case 2: Options.Controls.Add(new LiteralControl("Horizontal, labels left")); break;
							case 3: Options.Controls.Add(new LiteralControl("Horizontal, labels right")); break;
							//case 4: Options.Controls.Add(new LiteralControl("Horizontal, labels bottom")); break;
							case 5: Options.Controls.Add(new LiteralControl("Horizontal, no labels")); break;
							//case 6: Options.Controls.Add(new LiteralControl("Vertical, labels top")); break;
							//case 7: Options.Controls.Add(new LiteralControl("Vertical, labels left")); break;
							case 8: Options.Controls.Add(new LiteralControl("Vertical, labels right")); break;
							//case 9: Options.Controls.Add(new LiteralControl("Vertical, labels bottom")); break;
							//case 10: Options.Controls.Add(new LiteralControl("Vertical, no labels")); break;
						}
						Options.Controls.Add(new LiteralControl("</TD></TR>"));
					}
				}
				while(rs.Read());
			}
			rs.Close();

			Options.Controls.Add(new LiteralControl("</TABLE></TD></TR>"));
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
			if(questionID != 0)
			{
				Db.sqlExecute("UPDATE [Question] SET QuestionContainerID = " + (QuestionContainerID.SelectedValue == "0" ? "NULL" : QuestionContainerID.SelectedValue) + ", Box = " + (Box.Checked ? "1" : "0") + ", FontColor = '" + FontColor.SelectedValue + "', Internal = '" + Internal.Text.Replace("'","") + "',Variablename = '" + Variablename.Text.Replace("'","") + "',Underlined = " + (Underlined.Checked ? "1" : "0") + ", FontFamily = " + FontFamily.SelectedValue + ", FontSize = " + FontSize.SelectedValue + ", FontDecoration = " + FontDecoration.SelectedValue + ", OptionsPlacement = " + OptionsPlacement.SelectedValue + " WHERE QuestionID = " + questionID);
			}
			else
			{
				questionID = Db.sqlGetInt32("SET NOCOUNT ON;SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;BEGIN TRAN;INSERT INTO [Question] (FontColor,Internal,Variablename,Underlined,FontFamily,FontSize,FontDecoration,OptionsPlacement,Box,QuestionContainerID) VALUES ('" + FontColor.SelectedValue + "','" + Internal.Text.Replace("'","") + "','" + Variablename.Text.Replace("'","") + "'," + (Underlined.Checked ? "1" : "0") + "," + FontFamily.SelectedValue + "," + FontSize.SelectedValue + "," + FontDecoration.SelectedValue + "," + OptionsPlacement.SelectedValue + "," + (Box.Checked ? "1" : "0") + "," + (QuestionContainerID.SelectedValue == "0" ? "NULL" : QuestionContainerID.SelectedValue) + ");SELECT QuestionID FROM [Question] WHERE Internal = '" + Internal.Text.Replace("'","") + "' ORDER BY QuestionID DESC;COMMIT;");
			}
			Db.sqlExecute("DELETE FROM [QuestionLang] WHERE QuestionID = " + questionID);
			SqlDataReader rs = Db.sqlRecordSet("SELECT LangID FROM Lang ORDER BY LangID ASC");
			while(rs.Read())
			{
				Db.sqlExecute("INSERT INTO [QuestionLang] (QuestionID,LangID,Question,QuestionArea,QuestionJapaneseUnicode,QuestionAreaJapaneseUnicode,ReportQuestion) VALUES (" + questionID + "," + rs.GetInt32(0) + ",'" + ((TextBox)QuestionLang.FindControl("Text" + rs.GetInt32(0))).Text.Replace("'","''") + "','" + ((TextBox)QuestionLang.FindControl("AreaText" + rs.GetInt32(0))).Text.Replace("'","''") + "',N'" + ((TextBox)QuestionLang.FindControl("Text" + rs.GetInt32(0))).Text.Replace("'","''") + "',N'" + ((TextBox)QuestionLang.FindControl("AreaText" + rs.GetInt32(0))).Text.Replace("'","''") + "',N'" + ((TextBox)QuestionLang.FindControl("ReportQuestion" + rs.GetInt32(0))).Text.Replace("'","''") + "')");
			}
			rs.Close();

			rs = Db.sqlRecordSet("SELECT QuestionOptionID FROM QuestionOption WHERE QuestionID = " + questionID);
			while(rs.Read())
			{
				if(((CheckBox)Options.FindControl("QuestionOptionID" + rs.GetInt32(0))).Checked)
				{
					Db.sqlExecute("UPDATE QuestionOption SET Hide = " + (((CheckBox)Options.FindControl("Hide" + rs.GetInt32(0))).Checked ? "1" : "NULL") + ", Variablename = '" + ((TextBox)Options.FindControl("Variablename" + rs.GetInt32(0))).Text.Replace("'","") + "',OptionPlacement=" + ((DropDownList)Options.FindControl("OptionPlacement" + rs.GetInt32(0))).SelectedValue + ", Forced = " + (((CheckBox)Options.FindControl("Forced" + rs.GetInt32(0))).Checked ? "1" : "0") + " WHERE QuestionOptionID = " + rs.GetInt32(0));
				}
				else
				{
					Db.sqlExecute("UPDATE QuestionOption SET QuestionID = -" + questionID + " WHERE QuestionOptionID = " + rs.GetInt32(0));
				}
			}
			rs.Close();
			
			rs = Db.sqlRecordSet("SELECT OptionID, Variablename, OptionPlacement FROM [Option]");
			while(rs.Read())
			{
				if(Options.FindControl("OptionID" + rs.GetInt32(0)) != null)
				{
					if(((CheckBox)Options.FindControl("OptionID" + rs.GetInt32(0))).Checked)
					{
						int questionOptionID = Db.sqlGetInt32("SET NOCOUNT ON;SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;BEGIN TRAN;INSERT INTO [QuestionOption] (OptionID,QuestionID,Variablename,OptionPlacement,Forced) VALUES (" + rs.GetInt32(0) + "," + questionID + ",'" + rs.GetString(1).ToString().Replace("'","") + "'," + rs.GetInt32(2) + ",0);SELECT QuestionOptionID FROM [QuestionOption] WHERE OptionID = " + rs.GetInt32(0) + " AND QuestionID = " + questionID + " ORDER BY QuestionOptionID DESC;COMMIT;");
						Db.sqlExecute("UPDATE QuestionOption SET SortOrder = " + questionOptionID + " WHERE QuestionOptionID = " + questionOptionID);
					}
				}
			}
			rs.Close();

			HttpContext.Current.Session["QuestionContainerID"] = QuestionContainerID.SelectedValue;
			HttpContext.Current.Response.Redirect("questionSetup.aspx?QuestionID=" + questionID, true);
		}

		private void OptionContainerID_SelectedIndexChanged(object sender, EventArgs e)
		{
			HttpContext.Current.Session["OptionContainerID"] = OptionContainerID.SelectedValue;
			HttpContext.Current.Response.Redirect("questionSetup.aspx?QuestionID=" + questionID, true);
		}
	}
}
