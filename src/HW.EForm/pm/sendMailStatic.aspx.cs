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
							msg.From = "Therese Wahlstr�m <Therese.A.Wahlstrom@ki.se>";
							//msg.Headers.Add("BCC","dan.hasson@ki.se");
							msg.Subject = "Nu startar forskningsprojektet om ledarskap, arbetstillfredsst�llelse och h�lsa!";
							msg.Body = "INFORMATION TILL MEDARBETARE VARS CHEFER DELTAR" +
								"\r\n\r\n" +
								"B�sta medarbetare!" +
								"\r\n\r\n" +
								"Under n�gon av de n�rmaste dagarna kommer Du att f� ett mail med en l�nk till en " +
								"enk�t som Du kan besvara direkt p� din dator. Enk�ten ing�r som en del i ett " +
								"forskningsprojekt, den s� kallade Chefsstudien, som avser att belysa " +
								"sambandet mellan medarbetarnas h�lsa och ledarskap. Syftet med " +
								"forskningsprojektet �r ocks� att samla kunskaper om effekten av att chefer " +
								"deltar i samtalsgrupper." +
								"\r\n\r\n" +
								"Din medverkan �r frivillig och sker helt anonymt och data analyseras och " +
								"�terkopplas enbart p� gruppniv�. Du sj�lv f�r personlig �terkoppling p� dina " +
								"egna svar och om det skulle finnas tecken p� h�lsorelaterade problem kommer " +
								"Du att f� erbjudande om st�d som �r anpassat just till Dig. Ingen annan f�r " +
								"tillg�ng till dina resultat. Det �r viktigt att s� m�nga som m�jligt svarar " +
								"eftersom sammanst�llningen av svaren p� gruppniv� blir mer tillf�rlitlig och " +
								"intressant. " +
								"\r\n\r\n" +
								"B�sta h�lsningar " +
								"\r\n\r\n" +
								"Agneta J�hnk" +
								"\r\n" +
								"HR-direkt�r Karolinska universitetssjukhuset" + 
								"\r\n\r\n" +
								"genom forskningsassistent Therese Wahlstr�m";
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
							msg.From = "Therese Wahlstr�m <Therese.A.Wahlstrom@ki.se>";
							//msg.Headers.Add("BCC","dan.hasson@ki.se");
							msg.Subject = "Nu drar chefsstudien ig�ng!";
							msg.Body = "INFORMATION TILL DELTAGANDE CHEFER" +
								"\r\n\r\n" +
								"B�sta deltagare i Chefsstudien!" +
								"\r\n\r\n" +
								"Under n�gon av de n�rmaste dagarna g�r ett mail ut med en l�nk till den " +
								"webbaserade enk�ten om h�lsa och arbetsf�rh�llanden till Dig och Dina " +
								"medarbetare. Jag vore tacksam om Du kan meddela detta till alla dem som Du " +
								"har personalansvar f�r samt be dem att fylla i enk�ten n�r de f�r den. Det " +
								"�r viktigt att s� m�nga som m�jligt svarar eftersom sammanst�llningen av " +
								"svaren p� gruppniv� blir mer tillf�rlitlig och intressant. Kom ih�g att " +
								"sj�lv besvara enk�ten n�r du f�r den. Samtidigt som du f�r detta mail s� f�r " +
								"dina medarbetare ett mail med information om att studien drar ig�ng." +
								"\r\n\r\n" +
								"B�sta h�lsningar " +
								"\r\n\r\n" +
								"Agneta J�hnk" +
								"\r\n" +
								"HR-direkt�r Karolinska universitetssjukhuset" + 
								"\r\n\r\n" +
								"genom forskningsassistent Therese Wahlstr�m";
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
