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
	/// Summary description for weightedQuestionOptionSetup.
	/// </summary>
	public class weightedQuestionOptionSetup : System.Web.UI.Page
	{
		protected TextBox Internal;
		protected TextBox TargetVal;
		protected TextBox YellowLow;
		protected TextBox GreenLow;
		protected TextBox GreenHigh;
		protected TextBox YellowHigh;
		protected PlaceHolder Text;
		protected Button Save;
		protected DropDownList QuestionID;
		protected DropDownList OptionID;

		int wqoID = 0;

		private void Page_Load(object sender, System.EventArgs e)
		{
			wqoID = (HttpContext.Current.Request.QueryString["wqoID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["wqoID"]) : 0);

			int cx = 0;
			OdbcDataReader rs = Db.recordSet("SELECT " +
				"l.LangID, " +
				"w.WeightedQuestionOption, " +
				"w.FeedbackHeader, " +
				"w.Feedback, " +
				"w.FeedbackRedLow, " +
				"w.FeedbackYellowLow, " +
				"w.FeedbackGreen, " +
				"w.FeedbackYellowHigh, " +
				"w.FeedbackRedHigh, " +
				"w.ActionRedLow, " +
				"w.ActionYellowLow, " +
				"w.ActionGreen, " +
				"w.ActionYellowHigh, " +
				"w.ActionRedHigh " +
				"FROM Lang l " +
				"LEFT OUTER JOIN WeightedQuestionOptionLang w ON l.LangID = w.LangID " +
				"AND w.WeightedQuestionOptionID = " + wqoID + " " +
				"ORDER BY l.LangID ASC");
			while(rs.Read())
			{
				Text.Controls.Add(new LiteralControl("<tr><td valign=\"top\">Name<img SRC=\"../img/lang/" + rs.GetInt32(0) + ".gif\"></td><td colspan=\"3\">&nbsp;"));
				TextBox text = new TextBox();
				text.Width = Unit.Pixel(500);
				text.ID = "Text" + rs.GetInt32(0);
				Text.Controls.Add(text);
				Text.Controls.Add(new LiteralControl("</td></tr>"));
				if(!IsPostBack && wqoID != 0)
				{
					text.Text = (rs.IsDBNull(1) ? "" : rs.GetString(1));
				}

				Text.Controls.Add(new LiteralControl("<tr><td valign=\"top\">Feedback header</td><td colspan=\"3\">&nbsp;"));
				text = new TextBox();
				text.Width = Unit.Pixel(500);
				text.ID = "FeedbackHeader" + rs.GetInt32(0);
				Text.Controls.Add(text);
				Text.Controls.Add(new LiteralControl("</td></tr>"));
				if(!IsPostBack && wqoID != 0)
				{
					text.Text = (rs.IsDBNull(2) ? "" : rs.GetString(2));
				}

				Text.Controls.Add(new LiteralControl("<tr><td valign=\"top\">General explanation</td><td colspan=\"3\">&nbsp;"));
				text = new TextBox();
				text.Width = Unit.Pixel(500);
				text.ID = "Feedback" + rs.GetInt32(0);
				text.TextMode = TextBoxMode.MultiLine;
				text.Rows = 10;
				Text.Controls.Add(text);
				Text.Controls.Add(new LiteralControl("</td></tr>"));
				if(!IsPostBack && wqoID != 0)
				{
					text.Text = (rs.IsDBNull(3) ? "" : rs.GetString(3));
				}

				Text.Controls.Add(new LiteralControl("<tr><td valign=\"top\">Red low feedback</td><td colspan=\"3\">&nbsp;"));
				text = new TextBox();
				text.Width = Unit.Pixel(500);
				text.ID = "FeedbackRedLow" + rs.GetInt32(0);
				text.TextMode = TextBoxMode.MultiLine;
				text.Rows = 10;
				Text.Controls.Add(text);
				Text.Controls.Add(new LiteralControl("</td></tr>"));
				if(!IsPostBack && wqoID != 0)
				{
					text.Text = (rs.IsDBNull(4) ? "" : rs.GetString(4));
				}

				Text.Controls.Add(new LiteralControl("<tr><td valign=\"top\">Yellow low feedback</td><td colspan=\"3\">&nbsp;"));
				text = new TextBox();
				text.Width = Unit.Pixel(500);
				text.ID = "FeedbackYellowLow" + rs.GetInt32(0);
				text.TextMode = TextBoxMode.MultiLine;
				text.Rows = 10;
				Text.Controls.Add(text);
				Text.Controls.Add(new LiteralControl("</td></tr>"));
				if(!IsPostBack && wqoID != 0)
				{
					text.Text = (rs.IsDBNull(5) ? "" : rs.GetString(5));
				}

				Text.Controls.Add(new LiteralControl("<tr><td valign=\"top\">Green feedback</td><td colspan=\"3\">&nbsp;"));
				text = new TextBox();
				text.Width = Unit.Pixel(500);
				text.ID = "FeedbackGreen" + rs.GetInt32(0);
				text.TextMode = TextBoxMode.MultiLine;
				text.Rows = 10;
				Text.Controls.Add(text);
				Text.Controls.Add(new LiteralControl("</td></tr>"));
				if(!IsPostBack && wqoID != 0)
				{
					text.Text = (rs.IsDBNull(6) ? "" : rs.GetString(6));
				}

				Text.Controls.Add(new LiteralControl("<tr><td valign=\"top\">Yellow high feedback</td><td colspan=\"3\">&nbsp;"));
				text = new TextBox();
				text.Width = Unit.Pixel(500);
				text.ID = "FeedbackYellowHigh" + rs.GetInt32(0);
				text.TextMode = TextBoxMode.MultiLine;
				text.Rows = 10;
				Text.Controls.Add(text);
				Text.Controls.Add(new LiteralControl("</td></tr>"));
				if(!IsPostBack && wqoID != 0)
				{
					text.Text = (rs.IsDBNull(7) ? "" : rs.GetString(7));
				}

				Text.Controls.Add(new LiteralControl("<tr><td valign=\"top\">Red high feedback</td><td colspan=\"3\">&nbsp;"));
				text = new TextBox();
				text.Width = Unit.Pixel(500);
				text.ID = "FeedbackRedHigh" + rs.GetInt32(0);
				text.TextMode = TextBoxMode.MultiLine;
				text.Rows = 10;
				Text.Controls.Add(text);
				Text.Controls.Add(new LiteralControl("</td></tr>"));
				if(!IsPostBack && wqoID != 0)
				{
					text.Text = (rs.IsDBNull(8) ? "" : rs.GetString(8));
				}

				Text.Controls.Add(new LiteralControl("<tr><td valign=\"top\">Red low action</td><td colspan=\"3\">&nbsp;"));
				text = new TextBox();
				text.Width = Unit.Pixel(500);
				text.ID = "ActionRedLow" + rs.GetInt32(0);
				text.TextMode = TextBoxMode.MultiLine;
				text.Rows = 10;
				Text.Controls.Add(text);
				Text.Controls.Add(new LiteralControl("</td></tr>"));
				if(!IsPostBack && wqoID != 0)
				{
					text.Text = (rs.IsDBNull(9) ? "" : rs.GetString(9));
				}

				Text.Controls.Add(new LiteralControl("<tr><td valign=\"top\">Yellow low action</td><td colspan=\"3\">&nbsp;"));
				text = new TextBox();
				text.Width = Unit.Pixel(500);
				text.ID = "ActionYellowLow" + rs.GetInt32(0);
				text.TextMode = TextBoxMode.MultiLine;
				text.Rows = 10;
				Text.Controls.Add(text);
				Text.Controls.Add(new LiteralControl("</td></tr>"));
				if(!IsPostBack && wqoID != 0)
				{
					text.Text = (rs.IsDBNull(10) ? "" : rs.GetString(10));
				}

				Text.Controls.Add(new LiteralControl("<tr><td valign=\"top\">Green action</td><td colspan=\"3\">&nbsp;"));
				text = new TextBox();
				text.Width = Unit.Pixel(500);
				text.ID = "ActionGreen" + rs.GetInt32(0);
				text.TextMode = TextBoxMode.MultiLine;
				text.Rows = 10;
				Text.Controls.Add(text);
				Text.Controls.Add(new LiteralControl("</td></tr>"));
				if(!IsPostBack && wqoID != 0)
				{
					text.Text = (rs.IsDBNull(11) ? "" : rs.GetString(11));
				}

				Text.Controls.Add(new LiteralControl("<tr><td valign=\"top\">Yellow high action</td><td colspan=\"3\">&nbsp;"));
				text = new TextBox();
				text.Width = Unit.Pixel(500);
				text.ID = "ActionYellowHigh" + rs.GetInt32(0);
				text.TextMode = TextBoxMode.MultiLine;
				text.Rows = 10;
				Text.Controls.Add(text);
				Text.Controls.Add(new LiteralControl("</td></tr>"));
				if(!IsPostBack && wqoID != 0)
				{
					text.Text = (rs.IsDBNull(12) ? "" : rs.GetString(12));
				}

				Text.Controls.Add(new LiteralControl("<tr><td valign=\"top\">Red high action</td><td colspan=\"3\">&nbsp;"));
				text = new TextBox();
				text.Width = Unit.Pixel(500);
				text.ID = "ActionRedHigh" + rs.GetInt32(0);
				text.TextMode = TextBoxMode.MultiLine;
				text.Rows = 10;
				Text.Controls.Add(text);
				Text.Controls.Add(new LiteralControl("</td></tr>"));
				if(!IsPostBack && wqoID != 0)
				{
					text.Text = (rs.IsDBNull(13) ? "" : rs.GetString(13));
				}
			}
			rs.Close();

			if(!IsPostBack)
			{
				rs = Db.recordSet("SELECT q.Internal, q.QuestionID, q.Variablename FROM Question q WHERE q.QuestionID IN (SELECT qo.QuestionID FROM QuestionOption qo INNER JOIN [Option] o ON qo.OptionID = o.OptionID WHERE o.OptionType = 9)");
				if(rs.Read())
				{
					do
					{
						QuestionID.Items.Add(new ListItem((!rs.IsDBNull(2) && rs.GetString(2) != "" ? "[" + rs.GetString(2) + "] " : "") + rs.GetString(0),rs.GetInt32(1).ToString()));
					}
					while(rs.Read());
					updateOptionID();
				}
				else
				{
					QuestionID.Items.Add(new ListItem("< N/A >","0"));
					OptionID.Items.Add(new ListItem("< N/A >","0"));
				}
				rs.Close();

				if(wqoID == 0)
				{
					TargetVal.Text = "";
					YellowLow.Text = "";
					GreenLow.Text = "";
					GreenHigh.Text = "";
					YellowHigh.Text = "";
				}
				else
				{
					rs = Db.recordSet("SELECT Internal, TargetVal, YellowLow, GreenLow, GreenHigh, YellowHigh, QuestionID, OptionID FROM WeightedQuestionOption WHERE WeightedQuestionOptionID = " + wqoID);
					if(rs.Read())
					{
						Internal.Text = (rs.IsDBNull(0) ? "" : rs.GetString(0));
						TargetVal.Text = (rs.IsDBNull(1) ? "" : rs.GetInt32(1).ToString());
						YellowLow.Text = (rs.IsDBNull(2) ? "" : rs.GetInt32(2).ToString());
						GreenLow.Text = (rs.IsDBNull(3) ? "" : rs.GetInt32(3).ToString());
						GreenHigh.Text = (rs.IsDBNull(4) ? "" : rs.GetInt32(4).ToString());
						YellowHigh.Text = (rs.IsDBNull(5) ? "" : rs.GetInt32(5).ToString());
						if(!rs.IsDBNull(6) && QuestionID.Items.FindByValue(rs.GetInt32(6).ToString()) != null)
						{
							QuestionID.SelectedValue = rs.GetInt32(6).ToString();
							updateOptionID();
						}
						if(!rs.IsDBNull(7) && OptionID.Items.FindByValue(rs.GetInt32(7).ToString()) != null)
						{
							OptionID.SelectedValue = rs.GetInt32(7).ToString();
						}
					}
					rs.Close();
				}
			}
			QuestionID.SelectedIndexChanged += new EventHandler(QuestionID_SelectedIndexChanged);

			Save.Text = "Save";
			Save.Click += new EventHandler(Save_Click);
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
			OdbcDataReader rs;

			if(wqoID != 0)
			{
				Db.execute("UPDATE WeightedQuestionOption SET " +
					"TargetVal = " + (TargetVal.Text == "" ? "NULL" : TargetVal.Text) + ", " +
					"Internal = '" + Internal.Text.Replace("'","''") + "', " +
					"YellowLow = " + (YellowLow.Text == "" ? "NULL" : YellowLow.Text) + ", " +
					"GreenLow = " + (GreenLow.Text == "" ? "NULL" : GreenLow.Text) + ", " +
					"GreenHigh = " + (GreenHigh.Text == "" ? "NULL" : GreenHigh.Text) + ", " +
					"YellowHigh = " + (YellowHigh.Text == "" ? "NULL" : YellowHigh.Text) + ", " +
					"QuestionID = " + (QuestionID.SelectedValue == "0" || OptionID.SelectedValue == "0" ? "NULL" : QuestionID.SelectedValue) + ", " +
					"OptionID = " + (OptionID.SelectedValue == "0" ? "NULL" : OptionID.SelectedValue) + " " +
					"WHERE WeightedQuestionOptionID = " + wqoID);
			}
			else
			{
				Db.execute("INSERT INTO WeightedQuestionOption (" +
					"TargetVal, " +
					"Internal, " +
					"YellowLow, " +
					"GreenLow, " +
					"GreenHigh, " +
					"YellowHigh," +
					"QuestionID," +
					"OptionID" +
					") VALUES (" +
					"" + (TargetVal.Text == "" ? "NULL" : TargetVal.Text) + "," +
					"'" + Internal.Text.Replace("'","''") + "'," +
					"" + (YellowLow.Text == "" ? "NULL" : YellowLow.Text) + "," +
					"" + (GreenLow.Text == "" ? "NULL" : GreenLow.Text) + "," +
					"" + (GreenHigh.Text == "" ? "NULL" : GreenHigh.Text) + "," +
					"" + (YellowHigh.Text == "" ? "NULL" : YellowHigh.Text) + "," +
					"" + (QuestionID.SelectedValue == "0" || OptionID.SelectedValue == "0" ? "NULL" : QuestionID.SelectedValue) + "," +
					"" + (OptionID.SelectedValue == "0" ? "NULL" : OptionID.SelectedValue) + ")");
				rs = Db.recordSet("SELECT TOP 1 WeightedQuestionOptionID FROM WeightedQuestionOption ORDER BY WeightedQuestionOptionID DESC");
				if(rs.Read())
				{
					wqoID = rs.GetInt32(0);
				}
				rs.Close();
				Db.execute("UPDATE WeightedQuestionOption SET SortOrder = " + wqoID + " WHERE WeightedQuestionOptionID = " + wqoID);
			}
			Db.execute("DELETE FROM WeightedQuestionOptionLang WHERE WeightedQuestionOptionID = " + wqoID);
			rs = Db.recordSet("SELECT LangID FROM Lang ORDER BY LangID ASC");
			while(rs.Read())
			{
				Db.execute("INSERT INTO WeightedQuestionOptionLang (" +
					"WeightedQuestionOptionID," +
					"LangID," +
					"WeightedQuestionOption," +
					"FeedbackHeader," +
					"Feedback," +
					"FeedbackRedLow," +
					"FeedbackYellowLow," +
					"FeedbackGreen," +
					"FeedbackYellowHigh," +
					"FeedbackRedHigh," +
					"ActionRedLow," +
					"ActionYellowLow," +
					"ActionGreen," +
					"ActionYellowHigh," +
					"ActionRedHigh" +
					") VALUES (" +
					"" + wqoID + "," +
					"" + rs.GetInt32(0) + "," +
					"'" + ((TextBox)Text.FindControl("Text" + rs.GetInt32(0))).Text.Replace("'","''") + "'," +
					"'" + ((TextBox)Text.FindControl("FeedbackHeader" + rs.GetInt32(0))).Text.Replace("'","''") + "'," +
					"'" + ((TextBox)Text.FindControl("Feedback" + rs.GetInt32(0))).Text.Replace("'","''") + "'," +
					"'" + ((TextBox)Text.FindControl("FeedbackRedLow" + rs.GetInt32(0))).Text.Replace("'","''") + "'," +
					"'" + ((TextBox)Text.FindControl("FeedbackYellowLow" + rs.GetInt32(0))).Text.Replace("'","''") + "'," +
					"'" + ((TextBox)Text.FindControl("FeedbackGreen" + rs.GetInt32(0))).Text.Replace("'","''") + "'," +
					"'" + ((TextBox)Text.FindControl("FeedbackYellowHigh" + rs.GetInt32(0))).Text.Replace("'","''") + "'," +
					"'" + ((TextBox)Text.FindControl("FeedbackRedHigh" + rs.GetInt32(0))).Text.Replace("'","''") + "'," +
					"'" + ((TextBox)Text.FindControl("ActionRedLow" + rs.GetInt32(0))).Text.Replace("'","''") + "'," +
					"'" + ((TextBox)Text.FindControl("ActionYellowLow" + rs.GetInt32(0))).Text.Replace("'","''") + "'," +
					"'" + ((TextBox)Text.FindControl("ActionGreen" + rs.GetInt32(0))).Text.Replace("'","''") + "'," +
					"'" + ((TextBox)Text.FindControl("ActionYellowHigh" + rs.GetInt32(0))).Text.Replace("'","''") + "'," +
					"'" + ((TextBox)Text.FindControl("ActionRedHigh" + rs.GetInt32(0))).Text.Replace("'","''") + "'" +
					")");
			}
			rs.Close();

			HttpContext.Current.Response.Redirect("weightedQuestionOptionSetup.aspx?wqoID=" + wqoID, true);
		}

		private void QuestionID_SelectedIndexChanged(object sender, EventArgs e)
		{
			updateOptionID();
		}

		private void updateOptionID()
		{
			OptionID.Items.Clear();
			if(QuestionID.SelectedValue == "0")
			{
				OptionID.Items.Add(new ListItem("< N/A >","0"));
			}
			else
			{
				OdbcDataReader rs = Db.recordSet("SELECT o.Internal, o.OptionID FROM QuestionOption qo INNER JOIN [Option] o ON qo.OptionID = o.OptionID WHERE o.OptionType = 9 AND qo.QuestionID = " + QuestionID.SelectedValue);
				while(rs.Read())
				{
					OptionID.Items.Add(new ListItem(rs.GetString(0),rs.GetInt32(1).ToString()));
				}
				rs.Close();
			}
		}

	}
}
