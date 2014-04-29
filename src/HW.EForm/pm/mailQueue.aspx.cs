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
	/// Summary description for mailQueue.
	/// </summary>
	public class mailQueue : System.Web.UI.Page
	{
		protected Label List;

		private void Page_Load(object sender, System.EventArgs e)
		{
			List.Text = "";

			OdbcDataReader rs = Db.recordSet("SELECT TOP 100 " +
				"m.MailQueueID, " +			// 0
				"m.AdrTo, " +
				"dbo.cf_ProjectUnitTree(u.ProjectRoundUnitID,' » ') AS Unit, " +
				"r.Internal, " +
				"p.Internal, " +
				"m.Subject, " +				// 5
				"m.Body, " +
				"m.Sent, " +
				"m.SendType, " +
				"m.ErrorDescription, " +
				"p.ProjectID, " +			// 10
				"r.ProjectRoundID, " +
				"u.ProjectRoundUnitID " +
				"FROM MailQueue m " +
				"INNER JOIN ProjectRoundUser u ON m.ProjectRoundUserID = u.ProjectRoundUserID " +
				"INNER JOIN ProjectRound r ON u.ProjectRoundID = r.ProjectRoundID " +
				"INNER JOIN Project p ON r.ProjectID = p.ProjectID " +
				"ORDER BY ISNULL(m.Sent,'2020-02-02') DESC, m.MailQueueID DESC");
			while(rs.Read())
			{
				List.Text += "<TR>";
				List.Text += "<TD><A HREF=\"projectSetup.aspx?ProjectID=" + rs.GetInt32(10) + "&ProjectRoundID=" + rs.GetInt32(11) + (rs.IsDBNull(12) ? "" : "&ProjectRoundUnitID=" + rs.GetInt32(12)) + "\">" + rs.GetString(4) + "&nbsp;»&nbsp;" + rs.GetString(3) + "</A>&nbsp;</TD>";
				List.Text += "<TD>" + (rs.IsDBNull(2) ? "&gt; unknown &lt;" : rs.GetString(2)) + "&nbsp;</TD>";
				List.Text += "<TD><A TITLE=\"" + (rs.GetInt32(8) == 0 ? "Invitation" : "Reminder") + "\" HREF=\"mailto:" + rs.GetString(1) + "?subject=" + HttpUtility.UrlEncode(rs.GetString(5)).Replace("+"," ") + "&body=" + HttpUtility.UrlEncode(rs.GetString(6)).Replace("+"," ") + "\">" + rs.GetString(1) + "</A>&nbsp;</TD>";
				List.Text += "<TD>" + (!rs.IsDBNull(9) ? "<SPAN TITLE=\"" + rs.GetString(9) + "\">Failure</SPAN>" : (rs.IsDBNull(7) ? "In queue" : "Sent " + rs.GetDateTime(7).ToString("yyyy-MM-dd HH:mm"))) + "&nbsp;</TD>";
				List.Text += "</TR>";
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
