using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;

namespace eform
{
	/// <summary>
	/// Summary description for feedbackQuestion.
	/// </summary>
	public class feedbackQuestion : System.Web.UI.Page
	{
		protected Label FeedbackText;
		protected int projectID = 0;
		protected string surveyName = "";
		protected Label Lang;

		protected string printSurveyName()
		{
			return surveyName;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			int cx = 0;
			int RAC = (HttpContext.Current.Request.QueryString["RAC"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["RAC"]) : 10);
			int LID = (HttpContext.Current.Request.QueryString["LID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["LID"]) : 1);
			string rnds1 = (HttpContext.Current.Request.QueryString["RNDS1"] != null ? HttpContext.Current.Request.QueryString["RNDS1"] : "");
			string rnds2 = (HttpContext.Current.Request.QueryString["RNDS2"] != null ? HttpContext.Current.Request.QueryString["RNDS2"] : "");
			string r1 = (HttpContext.Current.Request.QueryString["R1"] != null ? HttpContext.Current.Request.QueryString["R1"] : "");
			string r2 = (HttpContext.Current.Request.QueryString["R2"] != null ? HttpContext.Current.Request.QueryString["R2"] : "");
			int surveyID = (HttpContext.Current.Request.QueryString["SID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["SID"]) : 0);
			surveyName = (HttpContext.Current.Request.QueryString["SN"] != null ? HttpContext.Current.Request.QueryString["SN"] : "");
			string depts1 = (HttpContext.Current.Request.QueryString["DEPTS1"] != null ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["DEPTS1"]) : "");
			string depts2 = (HttpContext.Current.Request.QueryString["DEPTS2"] != null ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["DEPTS2"]) : "");
			bool noStandardDeviation = (HttpContext.Current.Request.QueryString["NOSD"] != null);
			bool exportValues = (HttpContext.Current.Request.QueryString["EV"] != null);
			bool extremeValuesOnly = (HttpContext.Current.Request.QueryString["EVO"] != null);
			
			StringBuilder sb = new StringBuilder();

			foreach(string s in HttpContext.Current.Request.QueryString["Q"].ToString().Split(','))
			{
				SqlDataReader rs = Db.sqlRecordSet("SELECT " +
					"q.QuestionID, " +						// 0
					"qo.OptionID, " +						// 1
					"ql.Question, " +						// 2
					"ISNULL(ql.QuestionArea,''), " +		// 3
					"o.OptionType " +						// 4
					"FROM Question q " +
					"INNER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID " +
					"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
					"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + LID + " " +
					"WHERE q.QuestionID = " + Convert.ToInt32(s));
				while(rs.Read())
				{
					sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
					sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>" + rs.GetString(3) + "</p></div>");
					sb.Append("<div class=\"eform_ques\">");
					sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
					if(rs.GetString(3).IndexOf(rs.GetString(2)) < 0)
					{
						sb.Append("<TR><TD><B>" + rs.GetString(2) + "</B></TD></TR>");
					}
					sb.Append("<TR><TD>");
					sb.Append("<img src=\"" + System.Configuration.ConfigurationSettings.AppSettings["InstanceURL"] + "/feedbackImage.aspx?Values=1&RAC=" + RAC + "&LID=" + LID);
					if(noStandardDeviation)
					{
						sb.Append("&NOSD=1");
					}
					if(exportValues)
					{
						sb.Append("&EV=1");
					}
					if(extremeValuesOnly)
					{
						sb.Append("&EVO=1");
					}
					sb.Append("" + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + "");
					sb.Append("" + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + "");
					sb.Append("&R1=" + r1 + "");
					sb.Append("&R2=" + r2 + "");
					sb.Append("&DEPTS1=" + depts1 + "");
					sb.Append("&DEPTS2=" + depts2 + "");
					sb.Append("&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "");
					switch(rs.GetInt32(4))
					{
						case 1:	
							sb.Append("&T=0"); break;
						case 2:	
						case 4:
							sb.Append("&T=12"); break;
						case 9:	
							sb.Append("&T=8"); break;
						case 3:
							sb.Append("&T=13"); break;
					}	
					sb.Append("&Q=" + rs.GetInt32(0) + "");
					sb.Append("&O=" + rs.GetInt32(1) + "");
					sb.Append("\"></TD></TR>");
					sb.Append("</TABLE>");
					sb.Append("</div>");
					cx++;
				}
				rs.Close();

				FeedbackText.Text = sb.ToString();
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
