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
	/// Summary description for surveys.
	/// </summary>
	public class surveys : System.Web.UI.Page
	{
		protected Label List;

		private void Page_Load(object sender, System.EventArgs e)
		{
			List.Text = "";

			OdbcDataReader rs = Db.recordSet("SELECT SurveyID, Internal FROM Survey ORDER BY Internal");
			while(rs.Read())
			{
				List.Text += "<TR>" +
					"<TD><span style=\"color:#cccccc;\">" + rs.GetInt32(0).ToString().PadLeft(3,'0') + "</span> " + rs.GetString(1) + "&nbsp;&nbsp;</TD>" +
					"<TD>" +
					"<button onclick=\"location.href='surveySetup.aspx?SurveyID=" + rs.GetInt32(0) + "';return false;\">Edit</button>" +
					"<button onclick=\"void(window.open('../submit.aspx?SID=" + rs.GetInt32(0) + "&LID=1','',''));return false;\">View</button>" +
					"<button onclick=\"if(confirm('Are you sure you want to make a copy of survey \\'" + rs.GetString(1) + "\\'?')){location.href='surveySetup.aspx?CopySurveyID=" + rs.GetInt32(0) + "'};return false;\">Copy</button>" +
					"<button onclick=\"location.href='surveys.aspx?ExportSurveyID=" + rs.GetInt32(0) + "';return false;\">Export</button>&nbsp;" +
					"</td>" +
					"<td>";
				OdbcDataReader rs2 = Db.recordSet("SELECT p.Internal, p.ProjectID FROM Project p INNER JOIN ProjectSurvey ps ON p.ProjectID = ps.ProjectID WHERE ps.SurveyID = " + rs.GetInt32(0) + " ORDER BY p.ProjectID");
				while(rs2.Read())
				{
					List.Text += "<SPAN TITLE=\"" + rs2.GetString(0) + "\">" + rs2.GetInt32(1) + "</SPAN> ";
				}
				rs2.Close();
				List.Text += "" +
					"</td>" +
					"</tr>";
			}
			rs.Close();
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);

			if(HttpContext.Current.Request.QueryString["ExportSurveyID"] != null)
			{
				System.Text.StringBuilder sb = new System.Text.StringBuilder();

				int lid = 0, bx = 1, cx = 0, sqid = 0;

				OdbcDataReader rs = Db.recordSet("SELECT " +
					"sq.QuestionID, " +			// 0
					"qo.OptionID, " +			// 1
					"o.OptionType, " +			// 2
					"dbo.cf_isBlank(q.QuestionID,q.Variablename,sq.Variablename) AS s3, " +		// 3
					"dbo.cf_isBlank(qo.OptionID,o.Variablename,qo.Variablename) AS s4, " +		// 4
					"LTRIM(RTRIM(CAST(ISNULL(sql.Question,ql.Question) AS VARCHAR(8000)))), " +				// 5
					"(SELECT COUNT(*) FROM [SurveyQuestionOption] x WHERE x.SurveyQuestionID = sq.SurveyQuestionID), " +	// 6
					"(SELECT COUNT(*) FROM [OptionComponents] x WHERE x.OptionID = o.OptionID), " +							// 7
					"ql.LangID " +				// 8
					"FROM SurveyQuestion sq " +
					"LEFT OUTER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " + 
					"LEFT OUTER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " + 
					"LEFT OUTER JOIN [Option] o ON qo.OptionID = o.OptionID " + 
					"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
					"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID " +
					"LEFT OUTER JOIN SurveyQuestionLang sql ON sq.SurveyQuestionID = sql.SurveyQuestionID AND ql.LangID = sql.LangID " +
					"WHERE sq.SurveyID = " + HttpContext.Current.Request.QueryString["ExportSurveyID"] + " " +
					"ORDER BY " +
					"ql.LangID, " +
					"sq.SortOrder, " +
					"sqo.SortOrder" +
					"");
				while(rs.Read())
				{
					if(lid != rs.GetInt32(8))
					{
						lid++;
						cx = 0;
						bx = 1;
						sqid = 0;
					}
					if(sqid != rs.GetInt32(0) && !rs.IsDBNull(1))
					{
						cx++;
						bx = 1;
						sqid = rs.GetInt32(0);
					}
					else
					{
						bx++;
					}
					sb.Append(lid.ToString());
					sb.Append("\t");
					if(!rs.IsDBNull(1))
					{
						sb.Append(cx.ToString() + ":" + bx.ToString());
					}
					sb.Append("\t");
					if(!rs.IsDBNull(1))
					{
						sb.Append("Q" + rs.GetInt32(0).ToString() + "O" + rs.GetInt32(1).ToString());
					}
					sb.Append("\t");
					if(!rs.IsDBNull(1))
					{
						sb.Append(rs.GetString(3) + (rs.GetInt32(6) > 1 ? "_" + rs.GetString(4) : ""));
					}
					sb.Append("\t");
					if(!rs.IsDBNull(1))
					{
						switch(rs.GetInt32(2))
						{
							case 1:
								sb.Append("Select one - radio button");
								break;
							case 2:
								sb.Append("Free text");
								break;
							case 3:
								sb.Append("Select none/many - checkbox");
								break;
							case 4:
								sb.Append("Numeric");
								break;
							case 9:
								sb.Append("Visual analogue scale");
								break;
						}
					}
					sb.Append("\t");
					sb.Append(Db.RemoveHTMLTags(HttpUtility.HtmlDecode(rs.GetString(5))));
					if(!rs.IsDBNull(1))
					{
						OdbcDataReader rs2 = Db.recordSet("SELECT " +
							"ocl.Text, " +
							"oc.ExportValue " +
							"FROM OptionComponents oc " +
							"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + rs.GetInt32(8) + " " +
							"WHERE oc.OptionID = " + rs.GetInt32(1) + " " +
							"ORDER BY oc.SortOrder");
						while(rs2.Read())
						{
							sb.Append("\t");
							if(!rs2.IsDBNull(1) && (rs.GetInt32(2) == 1 || rs.GetInt32(2) == 3 || rs.GetInt32(2) == 9))
							{
								sb.Append(rs2.GetInt32(1).ToString() + " = ");
							}
							sb.Append(Db.RemoveHTMLTags(HttpUtility.HtmlDecode(rs2.GetString(0))));
						}
						rs2.Close();
					}
					sb.Append("\r\n");
				}
				rs.Close();

				HttpContext.Current.Response.Clear();				
				HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
				HttpContext.Current.Response.ContentType = "text/plain";
				HttpContext.Current.Response.AddHeader("content-disposition","attachment; filename=" + DateTime.Now.Ticks + ".txt");
				HttpContext.Current.Response.Write(sb.ToString());
				HttpContext.Current.Response.Flush();
				HttpContext.Current.Response.End();
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
