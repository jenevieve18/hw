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
	/// Summary description for feedbackOrg.
	/// </summary>
	public class feedbackOrg : System.Web.UI.Page
	{
		protected Label Org;

		private void Page_Load(object sender, System.EventArgs e)
		{
			int FID = (HttpContext.Current.Request.QueryString["FID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["FID"]) : 0);
			int PRID = (HttpContext.Current.Request.QueryString["PRID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["PRID"]) : 0);

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			SqlDataReader rs = Db.sqlRecordSet("SELECT QuestionID FROM FeedbackQuestion WHERE FeedbackID = " + FID);
			while(rs.Read())
			{
				if(sb.Length != 0) sb.Append(",");
				sb.Append(rs.GetInt32(0));
			}
			rs.Close();

			System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
			string org = "";
			rs = Db.sqlRecordSet("SELECT " +
				"p.ProjectRoundID, " +
				"p.ParentProjectRoundUnitID, " +
				"p.ProjectRoundUnitID, " +
				"p.Unit, " +
				"p.SortString, " +
				//"COUNT(u.ProjectRoundUserID), " +
				"dbo.cf_unitAndChildrenUserCount(0,NULL,p.ProjectRoundUnitID), " +
				//"COUNT(a.AnswerID), " +
				"dbo.cf_unitAndChildrenAnswerCount(NULL,NULL,p.ProjectRoundUnitID), " +
				"p.RequiredAnswerCount, " +
				"q.ProjectRoundUnitID, " +
				"p.SortString, " +
				"q.SortString " +
				"FROM ProjectRoundUnit p " +
//				"LEFT OUTER JOIN ProjectRoundUser u ON p.ProjectRoundUnitID = u.ProjectRoundUnitID " +
//				"LEFT OUTER JOIN Answer a ON u.ProjectRoundUserID = a.ProjectRoundUserID AND a.EndDT IS NOT NULL " +
				"LEFT OUTER JOIN ProjectRoundUnit q ON p.ID = q.ID AND q.ProjectRoundID = " + (HttpContext.Current.Request.QueryString["RNDS2"] != null ? HttpContext.Current.Request.QueryString["RNDS2"] : "0") + " " +
				"WHERE p.ProjectRoundID = " + PRID + " " +
//				"GROUP BY " +
//				"p.ProjectRoundID, " +
//				"p.ParentProjectRoundUnitID, " +
//				"p.ProjectRoundUnitID, " +
//				"p.Unit, " +
//				"p.SortString, " +
//				"p.RequiredAnswerCount, " +
//				"q.ProjectRoundUnitID " +
				"ORDER BY p.SortString");
			while(rs.Read())
			{
				if(rs.IsDBNull(1))
				{
					org = rs.GetString(3);

					sb2.Append(rs.GetString(3) + " ");

					sb2.Append("(" + rs.GetInt32(6) + "/" + rs.GetInt32(5) + ") ");

					sb2.Append("[<a href=\"feedbackQuestion.aspx?" + 
						(rs.IsDBNull(7) ? "" : "RAC=" + rs.GetInt32(7) + "&") + "" +
						"EV=1&" +
						"EVO=1&" +
						"NOSD=1&" +
						"Q=" + sb.ToString() + "&" +
						"RNDS1=" + rs.GetInt32(0) + "&" +
						"R1=" + rs.GetString(3) + "&" +
						"\" TARGET=\"_blank\">self</a>] ");

					if(!rs.IsDBNull(8))
					{
						sb2.Append("[<a href=\"feedbackQuestion.aspx?" + 
							(rs.IsDBNull(7) ? "" : "RAC=" + rs.GetInt32(7) + "&") + "" +
							"EV=1&" +
							"EVO=1&" +
							"NOSD=1&" +
							"Q=" + sb.ToString() + "&" +
							"RNDS1=" + rs.GetInt32(0) + "&" +
							"R1=" + rs.GetString(3) + "&" +
							"RNDS2=" + HttpContext.Current.Request.QueryString["RNDS2"] + "&" +
							"R2=" + rs.GetString(3) + " " + HttpContext.Current.Request.QueryString["R2"] + "&" +
							"\" TARGET=\"_blank\">vs " + HttpContext.Current.Request.QueryString["R2"] + "</a>] ");
					}
				}
				else
				{
					string depts1 = rs.GetInt32(2).ToString();
					SqlDataReader rs2 = Db.sqlRecordSet("SELECT ProjectRoundUnitID FROM ProjectRoundUnit WHERE LEN(SortString) > " + rs.GetString(9).Length + " AND LEFT(SortString," + rs.GetString(9).Length + ") = '" + rs.GetString(9) + "'");
					while(rs2.Read())
					{
						depts1 += "," + rs2.GetInt32(0);
					}
					rs2.Close();

					if(rs.GetInt32(6) < rs.GetInt32(5)/2 || rs.GetInt32(6) < (rs.IsDBNull(7) ? 10 : rs.GetInt32(7)))
					{
						sb2.Append("<br/><span style=\"color:#cc0000;\">" + rs.GetString(3) + "</span>");
					}
					else
					{
						sb2.Append("<br/>" + rs.GetString(3) + " ");

						sb2.Append("(" + rs.GetInt32(6) + "/" + rs.GetInt32(5) + ") ");

						sb2.Append("[<a href=\"feedbackQuestion.aspx?" + 
							(rs.IsDBNull(7) ? "" : "RAC=" + rs.GetInt32(7) + "&") + "" +
							"EV=1&" +
							"EVO=1&" +
							"NOSD=1&" +
							"Q=" + sb.ToString() + "&" +
							"RNDS1=" + rs.GetInt32(0) + "&" +
							"R1=" + rs.GetString(3) + "&" +
							"DEPTS1=" + depts1 + "" +
							"\" TARGET=\"_blank\">self</a>] ");

						sb2.Append("[<a href=\"feedbackQuestion.aspx?" + 
							(rs.IsDBNull(7) ? "" : "RAC=" + rs.GetInt32(7) + "&") + "" +
							"EV=1&" +
							"EVO=1&" +
							"NOSD=1&" +
							"Q=" + sb.ToString() + "&" +
							"RNDS1=" + rs.GetInt32(0) + "&" +
							"R1=" + org + "&" +
							"RNDS2=" + rs.GetInt32(0) + "&" +
							"R2=" + rs.GetString(3) + "&" +
							"DEPTS2=" + depts1 + "" +
							"\" TARGET=\"_blank\">vs parent</a>] ");

						if(!rs.IsDBNull(8))
						{
							string depts2 = rs.GetInt32(8).ToString();
							rs2 = Db.sqlRecordSet("SELECT ProjectRoundUnitID FROM ProjectRoundUnit WHERE LEN(SortString) > " + rs.GetString(10).Length + " AND LEFT(SortString," + rs.GetString(10).Length + ") = '" + rs.GetString(10) + "'");
							while(rs2.Read())
							{
								depts2 += "," + rs2.GetInt32(0);
							}
							rs2.Close();

							sb2.Append("[<a href=\"feedbackQuestion.aspx?" + 
								(rs.IsDBNull(7) ? "" : "RAC=" + rs.GetInt32(7) + "&") + "" +
								"EV=1&" +
								"EVO=1&" +
								"NOSD=1&" +
								"Q=" + sb.ToString() + "&" +
								"RNDS1=" + rs.GetInt32(0) + "&" +
								"R1=" + rs.GetString(3) + "&" +
								"DEPTS1=" + depts1 + "&" +
								"RNDS2=" + HttpContext.Current.Request.QueryString["RNDS2"] + "&" +
								"R2=" + rs.GetString(3) + " " + HttpContext.Current.Request.QueryString["R2"] + "&" +
								"DEPTS2=" + depts2 + "" +
								"\" TARGET=\"_blank\">vs " + HttpContext.Current.Request.QueryString["R2"] + "</a>] ");
						}
					}
				}
			}
			rs.Close();

			Org.Text = sb2.ToString();
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
