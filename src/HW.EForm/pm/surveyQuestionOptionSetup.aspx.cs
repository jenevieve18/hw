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
	/// Summary description for surveyQuestionOptionSetup.
	/// </summary>
	public class surveyQuestionOptionSetup : System.Web.UI.Page
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
			OdbcDataReader rs2 = Db.recordSet("SELECT " +
				"oc.OptionComponentID, " +
				"oc.Internal, " +
				"(SELECT TOP 1 ocl.Text FROM OptionComponentLang ocl WHERE ocl.OptionComponentID = oc.OptionComponentID AND ocl.Text IS NOT NULL) AS t, " +
				"sqoc.Hide " +
				"FROM OptionComponent oc " +
				"INNER JOIN OptionComponents ocs ON oc.OptionComponentID = ocs.OptionComponentID " +
				"INNER JOIN [Option] o ON ocs.OptionID = o.OptionID " +
				"INNER JOIN QuestionOption qo ON o.OptionID = qo.OptionID " +
				"INNER JOIN SurveyQuestionOption sqo ON qo.QuestionOptionID = sqo.QuestionOptionID " +
				"LEFT OUTER JOIN SurveyQuestionOptionComponent sqoc ON sqo.SurveyQuestionOptionID = sqoc.SurveyQuestionOptionID AND sqoc.OptionComponentID = oc.OptionComponentID " +
				"WHERE sqo.SurveyQuestionOptionID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SQOID"]));
			while(rs2.Read())
			{
				Lang.Controls.Add(new LiteralControl("<tr><td colspan=\"3\">" + (rs2.IsDBNull(1) || rs2.GetString(1) == "" ? (rs2.IsDBNull(2) ? rs2.GetInt32(0).ToString() : rs2.GetString(2)) : rs2.GetString(1)) + "&nbsp;"));
				CheckBox cb = new CheckBox();
				cb.ID = "Hide" + rs2.GetInt32(0);
				cb.Text = "Hide";
				if(!IsPostBack && !rs2.IsDBNull(3))
				{
					cb.Checked = true;
				}
				Lang.Controls.Add(cb);
				Lang.Controls.Add(new LiteralControl("</td></tr>"));

				OdbcDataReader rs = Db.recordSet("SELECT l.LangID, ocl.Text, sqocl.Text FROM Lang l LEFT OUTER JOIN OptionComponentLang ocl ON l.LangID = ocl.LangID AND ocl.OptionComponentID = " + rs2.GetInt32(0) + " LEFT OUTER JOIN SurveyQuestionOptionComponentLang sqocl ON ocl.LangID = sqocl.LangID AND ocl.OptionComponentID = sqocl.OptionComponentID AND sqocl.SurveyQuestionOptionID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SQOID"]) + " ORDER BY l.LangID ASC");
				while(rs.Read())
				{
					Lang.Controls.Add(new LiteralControl("<tr><td valign=\"top\"><img SRC=\"../img/lang/" + rs.GetInt32(0) + ".gif\"></td><td colspan=\"3\"></td><td>"));
					TextBox text = new TextBox();
					text.Width = Unit.Pixel(370);
					text.Rows = 2;
					text.TextMode = TextBoxMode.MultiLine;
					text.ID = "Text" + rs2.GetInt32(0) + "_" + rs.GetInt32(0);
					if(!IsPostBack)
					{
						if(!rs.IsDBNull(2))
						{
							text.Text = rs.GetString(2);
						}
						else if(!rs.IsDBNull(1))
						{
							text.Text = rs.GetString(1);
						}
					}
					Lang.Controls.Add(text);
					Lang.Controls.Add(new LiteralControl("</td><td valign=\"top\">"));
					if(!rs.IsDBNull(2))
					{
						Lang.Controls.Add(new LiteralControl("&nbsp;Customized ["));
						LinkButton tempButt = new LinkButton();
						tempButt.ID = "tempButt" + rs2.GetInt32(0) + "_" + rs.GetInt32(0);
						tempButt.Attributes["onclick"] += "document.forms[0].Text" + rs2.GetInt32(0) + "_" + rs.GetInt32(0) + ".value='';";
						tempButt.Text = "revert to original";
						Lang.Controls.Add(tempButt);
						Lang.Controls.Add(new LiteralControl("]"));
						tempButt.Click += new EventHandler(Save_Click);
					}
					Lang.Controls.Add(new LiteralControl("</td></tr>"));
				}
				rs.Close();
			}
			rs2.Close();

			if(!IsPostBack)
			{
				Close.Attributes["onclick"] += "window.close();";
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
			OdbcDataReader rs2 = Db.recordSet("SELECT " +
				"oc.OptionComponentID " +
				"FROM OptionComponent oc " +
				"INNER JOIN OptionComponents ocs ON oc.OptionComponentID = ocs.OptionComponentID " +
				"INNER JOIN [Option] o ON ocs.OptionID = o.OptionID " +
				"INNER JOIN QuestionOption qo ON o.OptionID = qo.OptionID " +
				"INNER JOIN SurveyQuestionOption sqo ON qo.QuestionOptionID = sqo.QuestionOptionID " +
				"WHERE sqo.SurveyQuestionOptionID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SQOID"]));
			while(rs2.Read())
			{
				Db.execute("DELETE FROM SurveyQuestionOptionComponent " +
					"WHERE OptionComponentID = " + rs2.GetInt32(0) + " " +
					"AND SurveyQuestionOptionID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SQOID"]));
				if(((CheckBox)Lang.FindControl("Hide" + rs2.GetInt32(0))).Checked)
				{
					Db.execute("INSERT INTO SurveyQuestionOptionComponent (SurveyQuestionOptionID,OptionComponentID,Hide) VALUES (" + Convert.ToInt32(HttpContext.Current.Request.QueryString["SQOID"]) + "," + rs2.GetInt32(0) + ",1)");
				}

				OdbcDataReader rs = Db.recordSet("SELECT l.LangID, ocl.Text FROM Lang l LEFT OUTER JOIN OptionComponentLang ocl ON ocl.LangID = l.LangID AND ocl.OptionComponentID = " + rs2.GetInt32(0));
				while(rs.Read())
				{
					Db.execute("DELETE FROM SurveyQuestionOptionComponentLang " +
						"WHERE SurveyQuestionOptionID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SQOID"]) + " " +
						"AND OptionComponentID = " + rs2.GetInt32(0) + " " +
						"AND LangID = " + rs.GetInt32(0));
					string t = ((TextBox)Lang.FindControl("Text" + rs2.GetInt32(0) + "_" + rs.GetInt32(0))).Text;
					if(t != "" && t != (rs.IsDBNull(1) ? "" : rs.GetString(1)))
					{
						Db.execute("INSERT INTO SurveyQuestionOptionComponentLang (SurveyQuestionOptionID,OptionComponentID,LangID,Text) VALUES (" + Convert.ToInt32(HttpContext.Current.Request.QueryString["SQOID"]) + "," + rs2.GetInt32(0) + "," + rs.GetInt32(0) + ",'" + t.Replace("'","''") + "')");
					}
				}
				rs.Close();
			}
			rs2.Close();

			HttpContext.Current.Response.Redirect("surveyQuestionOptionSetup.aspx?ReloadParent=1&SQOID=" + Convert.ToInt32(HttpContext.Current.Request.QueryString["SQOID"]),true);
		}
	}
}
