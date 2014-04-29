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
	/// Summary description for sendFollowup.
	/// </summary>
	public class sendFollowup : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			int cx = 0;
			OdbcDataReader rs = Db.recordSet("" +
				"SELECT " +
				"u.ProjectRoundUserID, " +
				"u.Email " +
				"FROM ProjectRoundUser u " +
				"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
				"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
				"WHERE u.FollowupSendCount IS NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.ProjectRoundID = 20 AND a.EndDT IS NOT NULL");
			while(rs.Read())
			{
				HttpContext.Current.Response.Write(cx + " : " + rs.GetInt32(0) + " : " + rs.GetString(1) + "<BR>\r\n");

				System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
				msg.To = rs.GetString(1);
				msg.From = "info@webbqps.se";
				msg.Subject = "Anmäl dig till höstens hälsofrämjande aktiviteter!";
				msg.Body = "" +
					"Du är en av de personer som i våras fick den hälsoenkät som är en del av projektet Chefsutveckling KI-LIME. Enkäten skickades till 4600 personer på Karolinska Universitetssjukhuset. Syftet med enkäten är att kartlägga förändringar i arbetsmiljö, stressfaktorer och hälsa före, under och efter den tid då cheferna deltar i samtalsgrupper." +
					"\r\n\r\n" +
					"Jag vet att alla har fullt upp och att det är svårt att boka upp sig på aktiviteter men vill påminna om de olika aktiviteter som startar till hösten. För att se din återkoppling och de förslag på hälsofrämjande aktiviteter som erbjuds dig beroende på ditt resultat i enkäten gör du så här:" +
					"\r\n\r\n" +
					"1. Gå in på adressen http://www.webbqps.se" +
					"\r\n" +
					"2. Klicka på \"För projektdeltagare\"." +
					"\r\n" +
					"3. Skriv din e-postadress i rutan och klicka därefter på \"Skicka\"." +
					"\r\n" +
					"4. Länken till din återkoppling skickas direkt till din e-postadress." +
					"\r\n\r\n" +
					"Några exempel på aktiviteter som erbjuds är:" +
					"\r\n\r\n" +
					"* Kollegiala samtalsgrupper – som ger dig möjlighet att arbeta med ditt förhållningssätt till stress och att identifiera arbetssätt och rutiner som minska stress." +
					"\r\n" +
					"* Kost- och rörelsekurs, nio träffar" +
					"\r\n" +
					"* Sluta röka-kurs via Internet och mail" +
					"\r\n" +
					"* Föreläsningar om sömn" +
					"\r\n" +
					"* Behandling av personalsjukgymnaster" +
					"\r\n" +
					"* Enskilda samtal med sjukhuskyrkan" +
					"\r\n" +
					"* Kontakt med företagshälsovården" +
					"\r\n" +
					"* Mindfulness-grupper (meditationsform för stresshantering)" +
					"\r\n\r\n" +
					"För eventuella frågor kontakta Mariette Weideskog, hälsopedagog på HR-avdelningen: mariette.veideskog@karolinska.se eller 070-484 58 05." +
					"\r\n\r\n" +
					"Mer information om projektet finns på Inuti:" +
					"\r\n" +
					"Medarbetare > Chefsutveckling KI-LIME" +
					"\r\n\r\n" +
					"Med vänliga hälsningar" +
					"\r\n" +
					"Agneta Jöhnk, HR-direktör";
				msg.BodyFormat = System.Web.Mail.MailFormat.Text;
				msg.BodyEncoding = System.Text.Encoding.GetEncoding(1252);
				System.Web.Mail.SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
				System.Web.Mail.SmtpMail.Send(msg);

				Db.execute("UPDATE ProjectRoundUser SET FollowupSendCount = 1 WHERE ProjectRoundUserID = " + rs.GetInt32(0));

				cx++;
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
