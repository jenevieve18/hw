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
	/// Summary description for sendMailStatic.
	/// </summary>
	public class sendMailStatic : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			OdbcDataReader rs = Db.recordSet("SELECT TOP 500 Email, Boss, PNR, KST FROM anstkst WHERE premailsent IS NULL AND send = 1","Driver={SQL Server};Server=.\\SQLEXPRESS;Database=webbqps;uid=eForm;pwd=eForm;option=3;");
			while(rs.Read())
			{
				try
				{
					if(projectSetup.isEmail(rs.GetString(0)))
					{
						if(rs.IsDBNull(1))
						{
							// medarbetare
							System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
							msg.To = rs.GetString(0);
							msg.From = "Therese Wahlström <Therese.A.Wahlstrom@ki.se>";
							//msg.Headers.Add("BCC","dan.hasson@ki.se");
							msg.Subject = "Nu startar forskningsprojektet om ledarskap, arbetstillfredsställelse och hälsa!";
							msg.Body = "INFORMATION TILL MEDARBETARE VARS CHEFER DELTAR" +
								"\r\n\r\n" +
								"Bästa medarbetare!" +
								"\r\n\r\n" +
								"Under någon av de närmaste dagarna kommer Du att få ett mail med en länk till en " +
								"enkät som Du kan besvara direkt på din dator. Enkäten ingår som en del i ett " +
								"forskningsprojekt, den så kallade Chefsstudien, som avser att belysa " +
								"sambandet mellan medarbetarnas hälsa och ledarskap. Syftet med " +
								"forskningsprojektet är också att samla kunskaper om effekten av att chefer " +
								"deltar i samtalsgrupper." +
								"\r\n\r\n" +
								"Din medverkan är frivillig och sker helt anonymt och data analyseras och " +
								"återkopplas enbart på gruppnivå. Du själv får personlig återkoppling på dina " +
								"egna svar och om det skulle finnas tecken på hälsorelaterade problem kommer " +
								"Du att få erbjudande om stöd som är anpassat just till Dig. Ingen annan får " +
								"tillgång till dina resultat. Det är viktigt att så många som möjligt svarar " +
								"eftersom sammanställningen av svaren på gruppnivå blir mer tillförlitlig och " +
								"intressant. " +
								"\r\n\r\n" +
								"Bästa hälsningar " +
								"\r\n\r\n" +
								"Agneta Jöhnk" +
								"\r\n" +
								"HR-direktör Karolinska universitetssjukhuset" + 
								"\r\n\r\n" +
								"genom forskningsassistent Therese Wahlström";
							msg.BodyFormat = System.Web.Mail.MailFormat.Text;
							msg.BodyEncoding = System.Text.Encoding.GetEncoding(1252);
							System.Web.Mail.SmtpMail.SmtpServer = "mail.interactivehealthgroup.com";
							System.Web.Mail.SmtpMail.Send(msg);
						}
						else
						{
							// chefer
							System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
							msg.To = rs.GetString(0);
							msg.From = "Therese Wahlström <Therese.A.Wahlstrom@ki.se>";
							//msg.Headers.Add("BCC","dan.hasson@ki.se");
							msg.Subject = "Nu drar chefsstudien igång!";
							msg.Body = "INFORMATION TILL DELTAGANDE CHEFER" +
								"\r\n\r\n" +
								"Bästa deltagare i Chefsstudien!" +
								"\r\n\r\n" +
								"Under någon av de närmaste dagarna går ett mail ut med en länk till den " +
								"webbaserade enkäten om hälsa och arbetsförhållanden till Dig och Dina " +
								"medarbetare. Jag vore tacksam om Du kan meddela detta till alla dem som Du " +
								"har personalansvar för samt be dem att fylla i enkäten när de får den. Det " +
								"är viktigt att så många som möjligt svarar eftersom sammanställningen av " +
								"svaren på gruppnivå blir mer tillförlitlig och intressant. Kom ihåg att " +
								"själv besvara enkäten när du får den. Samtidigt som du får detta mail så får " +
								"dina medarbetare ett mail med information om att studien drar igång." +
								"\r\n\r\n" +
								"Bästa hälsningar " +
								"\r\n\r\n" +
								"Agneta Jöhnk" +
								"\r\n" +
								"HR-direktör Karolinska universitetssjukhuset" + 
								"\r\n\r\n" +
								"genom forskningsassistent Therese Wahlström";
							msg.BodyFormat = System.Web.Mail.MailFormat.Text;
							msg.BodyEncoding = System.Text.Encoding.GetEncoding(1252);
							System.Web.Mail.SmtpMail.SmtpServer = "mail.interactivehealthgroup.com";
							System.Web.Mail.SmtpMail.Send(msg);
						}

						Db.execute("UPDATE anstkst SET premailsent = 1 WHERE PNR = " + rs.GetInt64(2) + " AND KST = " + rs.GetInt32(3),"Driver={SQL Server};Server=.\\SQLEXPRESS;Database=webbqps;uid=eForm;pwd=eForm;option=3;");
					}
				}
				catch(Exception ex)
				{
					HttpContext.Current.Response.Write(rs.GetString(0) + " (ERROR:" + ex.Message + ")\r\n");
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
	}
}
