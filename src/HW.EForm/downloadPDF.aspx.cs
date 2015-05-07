using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
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
	/// Summary description for downloadPDF.
	/// </summary>
	public class downloadPDF : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(HttpContext.Current.Request.QueryString["AK"] != null)
			{
				string REGEN = "";
				string AK = HttpContext.Current.Request.QueryString["AK"].Replace(".","").Replace("/","").Replace("\\","").Replace("'","");
				if(System.IO.File.Exists(Page.MapPath("archive/" + AK + ".pdf")))
				{
					HttpContext.Current.Response.Clear();
					HttpContext.Current.Response.ClearHeaders();
					HttpContext.Current.Response.Charset = "UTF-8";
					HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
					HttpContext.Current.Response.ContentType = "application/pdf";
					HttpContext.Current.Response.AppendHeader("content-disposition","attachment; filename=feedback.pdf");
					HttpContext.Current.Response.WriteFile(Page.MapPath("archive/" + AK + ".pdf"));
				}
				else
				{
					OdbcDataReader rs = Db.recordSet("SELECT " +
						"TOP 1 " +
						"u.ProjectRoundUserID, " +						// 0
						"LEFT(CONVERT(VARCHAR(255),u.UserKey),8), " +	// 1
						"dbo.cf_unitLangID(u.ProjectRoundUnitID), " +	// 2
						"pr.LangID " +									// 3
						"FROM ProjectRoundUser u " +
						"INNER JOIN ProjectRound pr ON u.ProjectRoundID = pr.ProjectRoundID " +
						"INNER JOIN Answer a ON u.ProjectRoundUserID = a.ProjectRoundUserID " +
						"WHERE a.EndDT IS NOT NULL " +
						"AND REPLACE(CONVERT(VARCHAR(255),AnswerKey),'-','') = '" + AK + "' " +
						"ORDER BY a.AnswerID DESC");
					if(rs.Read())
					{
						REGEN = "LID=" + (rs.IsDBNull(2) ? rs.GetInt32(3) : rs.GetInt32(2)) + "&K=" + rs.GetString(1) + rs.GetInt32(0) + "&GenerateDownloadFeedback=feedback";
					}
					rs.Close();

					if(REGEN != "")
					{
						HttpContext.Current.Response.Redirect("submit.aspx?" + REGEN,true);
					}
					else
					{
						HttpContext.Current.Response.Clear();
						HttpContext.Current.Response.ClearHeaders();
						HttpContext.Current.Response.Charset = "UTF-8";
						HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
						HttpContext.Current.Response.ContentType = "text/plain";
						HttpContext.Current.Response.AppendHeader("content-disposition","inline; filename=error.txt");
						HttpContext.Current.Response.Write("An error occured!");
					}
				}
			}
			else if(HttpContext.Current.Request.QueryString["R"] != null)
			{
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ClearHeaders();
				HttpContext.Current.Response.Charset = "UTF-8";
				HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
				HttpContext.Current.Response.ContentType = "text/plain";
				HttpContext.Current.Response.AppendHeader("content-disposition","attachment; filename=" + HttpContext.Current.Request.QueryString["R"] + "");
				HttpContext.Current.Response.WriteFile(Page.MapPath("report/" + HttpContext.Current.Request.QueryString["R"]));
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
