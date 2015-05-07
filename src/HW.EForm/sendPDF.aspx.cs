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
	/// Summary description for sendPDF.
	/// </summary>
	public class sendPDF : System.Web.UI.Page
	{
		int projectID = 0;
		string surveyName = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(HttpContext.Current.Request.QueryString["AK"] != null)
			{
				OdbcDataReader rs = Db.recordSet("SELECT p.ProjectID, prl.SurveyName, usr.Email, pr.EmailFromAddress " +
					"FROM Answer a " +
					"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
					"INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
					"INNER JOIN Project p ON pr.ProjectID = p.ProjectID " +
					"INNER JOIN ProjectRoundLang prl ON pr.ProjectRoundID = prl.ProjectRoundID AND prl.LangID = dbo.cf_unitLangID(pru.ProjectRoundUnitID) " +
					"LEFT OUTER JOIN ProjectRoundUser usr ON a.ProjectRoundUserID = usr.ProjectRoundUserID " +
					"WHERE REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-','') = '" + HttpContext.Current.Request.QueryString["AK"].Replace("'","") + "'");
				if(rs.Read())
				{
					projectID = rs.GetInt32(0);
					surveyName = rs.GetString(1);

					string email = "";
					if(!rs.IsDBNull(2))
					{
						email = rs.GetString(2);
					}
					if((email == "" || !projectSetup.isEmail(email)) && HttpContext.Current.Request.QueryString["Email"] != null)
					{
						email = HttpContext.Current.Request.QueryString["Email"].ToString();
					}
					if(projectSetup.isEmail(email))
					{
						System.Web.Mail.MailAttachment attachment = new	System.Web.Mail.MailAttachment(Page.MapPath("archive/" + HttpContext.Current.Request.QueryString["AK"] + ".pdf"));

						System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
						msg.To = email;
						msg.From = rs.GetString(3);
						msg.Subject = rs.GetString(1);
						msg.Body = "Bifogat finner du din återkoppling.";
						msg.Attachments.Add(attachment);
						msg.BodyFormat = System.Web.Mail.MailFormat.Text;
						msg.BodyEncoding = System.Text.Encoding.GetEncoding(1252);
						System.Web.Mail.SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
						System.Web.Mail.SmtpMail.Send(msg);
					}
				}
				rs.Close();
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
