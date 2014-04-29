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
	/// Summary description for surveyQuestionSetup.
	/// </summary>
	public class surveyQuestionSetup : System.Web.UI.Page
	{
		protected Button Save;
		protected Button Close;
		protected PlaceHolder Lang;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(HttpContext.Current.Request.QueryString["ReloadParent"] != null)
			{
				Page.RegisterStartupScript("RELOAD_PARENT","<script language=\"JavaScript\">opener.location.reload();</script>");
			}

			Save.Click += new EventHandler(Save_Click);

			int cx = 0;
			OdbcDataReader rs = Db.recordSet("SELECT LangID FROM Lang ORDER BY LangID ASC");
			while(rs.Read())
			{
				Lang.Controls.Add(new LiteralControl("<tr><td valign=\"top\" align=\"right\">"));
				if(cx++ == 0)
				{
					Lang.Controls.Add(new LiteralControl("Text&nbsp;"));
				}
				Lang.Controls.Add(new LiteralControl("<img SRC=\"../img/lang/" + rs.GetInt32(0) + ".gif\"></td><td colspan=\"3\">&nbsp;"));
				TextBox text = new TextBox();
				text.Width = Unit.Pixel(470);
				text.Rows = 8;
				text.TextMode = TextBoxMode.MultiLine;
				text.ID = "Text" + rs.GetInt32(0);
				Lang.Controls.Add(text);
				Lang.Controls.Add(new LiteralControl("</td><td valign=\"top\">"));
				PlaceHolder Customized = new PlaceHolder();
				Customized.ID = "Customized" + rs.GetInt32(0);
				Lang.Controls.Add(Customized);
				Lang.Controls.Add(new LiteralControl("</td></tr>"));
			}
			rs.Close();

			if(!IsPostBack)
			{
				Close.Attributes["onclick"] += "window.close();";
			}

			rs = Db.recordSet("SELECT " +
				"sq.SurveyQuestionID, " +
				"ql.LangID, " +
				"ISNULL(sql.Question,ql.Question) AS Question, " +
				"sql.LangID " +
				"FROM SurveyQuestion sq " +
				"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
				"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID " +
				"LEFT OUTER JOIN SurveyQuestionLang sql ON sq.SurveyQuestionID = sql.SurveyQuestionID AND ql.LangID = sql.LangID " +
				"WHERE sq.SurveyQuestionID = " + HttpContext.Current.Request.QueryString["SurveyQuestionID"]);
			while(rs.Read())
			{
				if(!IsPostBack)
				{
					((TextBox)Lang.FindControl("Text" + rs.GetInt32(1))).Text = rs.GetString(2);
				}
				if(!rs.IsDBNull(3))
				{
					PlaceHolder temp = ((PlaceHolder)Lang.FindControl("Customized" + rs.GetInt32(1)));
					temp.Controls.Add(new LiteralControl("&nbsp;Customized ["));
					LinkButton tempButt = new LinkButton();
					tempButt.ID = "tempButt" + rs.GetInt32(1);
					tempButt.Attributes["onclick"] += "document.forms[0].Text" + rs.GetInt32(1) + ".value='';";
					tempButt.Text = "revert to original";
					temp.Controls.Add(tempButt);
					temp.Controls.Add(new LiteralControl("]"));
					tempButt.Click += new EventHandler(Save_Click);
				}
			}
			rs.Close();
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
			OdbcDataReader rs = Db.recordSet("SELECT " +
				"ql.LangID, " +
				"ql.Question " +
				"FROM SurveyQuestion sq " +
				"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
				"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID " +
				"WHERE sq.SurveyQuestionID = " + HttpContext.Current.Request.QueryString["SurveyQuestionID"]);
			while(rs.Read())
			{
				Db.execute("DELETE FROM SurveyQuestionLang " +
					"WHERE SurveyQuestionID = " + HttpContext.Current.Request.QueryString["SurveyQuestionID"] + " " +
					"AND LangID = " + rs.GetInt32(0));

				string newText = ((TextBox)Lang.FindControl("Text" + rs.GetInt32(0))).Text;
				if(newText != "" && newText != (rs.IsDBNull(1) ? "" : rs.GetString(1)))
				{
					Db.execute("INSERT INTO SurveyQuestionLang (" +
						"SurveyQuestionID," +
						"LangID," +
						"Question" +
						") VALUES (" +
						"" + HttpContext.Current.Request.QueryString["SurveyQuestionID"] + "," +
						"" + rs.GetInt32(0) + "," +
						"'" + ((TextBox)Lang.FindControl("Text" + rs.GetInt32(0))).Text.Replace("'","''") + "'" +
						")");
				}
			}
			rs.Close();

			HttpContext.Current.Response.Redirect("surveyQuestionSetup.aspx?ReloadParent=1&SurveyQuestionID=" + HttpContext.Current.Request.QueryString["SurveyQuestionID"],true);
		}
	}
}
