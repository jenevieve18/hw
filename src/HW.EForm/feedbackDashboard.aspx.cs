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

namespace eform
{
	/// <summary>
	/// Summary description for feedbackDashboard.
	/// </summary>
	public class feedbackDashboard : System.Web.UI.Page
	{
		protected PlaceHolder Org;

		private void Page_Load(object sender, System.EventArgs e)
		{
			SqlDataReader rs = Db.sqlRecordSet("SELECT " +
				"pr.ProjectRoundID, " +			// 0
				"pr.Internal, " +
				"p.Name, " +
				"pru.ProjectRoundUnitID, " +
				"pru.Unit, " +
				"dbo.cf_unitAndChildrenUserCount(0,NULL,pru.ProjectRoundUnitID), " +		// 5
				"dbo.cf_unitAndChildrenAnswerCount(NULL,NULL,pru.ProjectRoundUnitID), " +
				"pru.RequiredAnswerCount, " +
				"pr.FeedbackID, " +
				"dbo.cf_unitDepth(pru.ProjectRoundUnitID), " +
				"dbo.cf_unitSurveyID(pru.ProjectRoundUnitID) " +								// 10
				"FROM ProjectRound pr " +
				"INNER JOIN Project p ON pr.ProjectID = p.ProjectID " +
				"INNER JOIN ProjectRoundUnit pru ON pr.ProjectRoundID = pru.ProjectRoundID " +
				//"LEFT OUTER JOIN ProjectRoundUnit q ON p.ID = q.ID AND q.ProjectRoundID = " + (HttpContext.Current.Request.QueryString["RNDS2"] != null ? HttpContext.Current.Request.QueryString["RNDS2"] : "0") + " " +
				"WHERE pr.RoundKey = '" + HttpContext.Current.Request.QueryString["RK"].ToString().Replace("'","") + "' " +
				"ORDER BY pru.SortString");
			while(rs.Read())
			{
				Org.Controls.Add(new LiteralControl("<tr><td>"));
				for(int i=0; i<rs.GetInt32(9); i++)
					Org.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
				int rac = (rs.IsDBNull(7) ? 10 : rs.GetInt32(7));
				bool show = (rs.GetInt32(6) >= rac && rs.GetInt32(6) > (rs.GetInt32(5)/2));
				Org.Controls.Add(new LiteralControl(rs.GetString(4) + "</td><td>" + rs.GetInt32(6) + "/" + rs.GetInt32(5) + "</td><td>" +
					(show ? "<a href=\"feedback.aspx?R=" + rs.GetInt32(0) + "&SID=" + rs.GetInt32(10) + "&ShowN=1&N=" + rs.GetString(1) + "&ST=0&AB=1&U=" + rs.GetInt32(3) + "&UD=" + rs.GetString(4) + "\" target=\"_blank\">Open</a>" : "") +
					"</td><td>" +
					(show ? "<a href=\"feedback.aspx?R=" + rs.GetInt32(0) + "&SID=" + rs.GetInt32(10) + "&ShowN=1&N=" + rs.GetString(1) + "&ST=1&AB=1&U=" + rs.GetInt32(3) + "&UD=" + rs.GetString(4) + "\" target=\"_blank\">Open</a>" : "") +
					"</td></tr>"));
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
	}
}
