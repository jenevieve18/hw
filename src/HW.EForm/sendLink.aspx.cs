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
	/// Summary description for sendLink.
	/// </summary>
	public class sendLink : System.Web.UI.Page
	{
		int projectID = 0;
		string surveyName = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(HttpContext.Current.Request.QueryString["UK"] != null)
			{
				try
				{
					string userKey = HttpContext.Current.Request.QueryString["UK"].ToString();
					int projectRoundUserID = Convert.ToInt32(userKey.Substring(8));

					OdbcDataReader rs = Db.recordSet("SELECT p.ProjectID, prl.SurveyName, usr.Email, pr.EmailFromAddress, p.AppURL " +
						"FROM ProjectRoundUser usr " +
						"INNER JOIN ProjectRoundUnit pru ON usr.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
						"INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
						"INNER JOIN Project p ON pr.ProjectID = p.ProjectID " +
						"INNER JOIN ProjectRoundLang prl ON pr.ProjectRoundID = prl.ProjectRoundID AND prl.LangID = dbo.cf_unitLangID(pru.ProjectRoundUnitID) " +
						"WHERE LEFT(CONVERT(VARCHAR(255),usr.UserKey),8) = '" + userKey.Replace("'","").Substring(0,8) + "'");
					if(rs.Read())
					{
						projectID = rs.GetInt32(0);
						surveyName = rs.GetString(1);

						System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
						msg.To = rs.GetString(2);
						msg.From = rs.GetString(3);
						msg.Subject = rs.GetString(1);
						msg.Body = "Här kommer länken som du kan använda för att fortsätta fylla i enkäten.\r\n\r\n" + (rs.IsDBNull(4) ? projectSetup.appURL : rs.GetString(4)) + "/submit.aspx?K=" + userKey;
						msg.BodyFormat = System.Web.Mail.MailFormat.Text;
						msg.BodyEncoding = System.Text.Encoding.GetEncoding(1252);
						System.Web.Mail.SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
						System.Web.Mail.SmtpMail.Send(msg);
					}
					rs.Close();
				}
				catch(Exception) {}
			}

			HttpContext.Current.Response.Redirect("sent.aspx?P=" + projectID + "&S=" + surveyName,true);
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
