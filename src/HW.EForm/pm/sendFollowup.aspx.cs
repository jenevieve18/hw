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
				msg.Subject = "Anm�l dig till h�stens h�lsofr�mjande aktiviteter!";
				msg.Body = "" +
					"Du �r en av de personer som i v�ras fick den h�lsoenk�t som �r en del av projektet Chefsutveckling KI-LIME. Enk�ten skickades till 4600 personer p� Karolinska Universitetssjukhuset. Syftet med enk�ten �r att kartl�gga f�r�ndringar i arbetsmilj�, stressfaktorer och h�lsa f�re, under och efter den tid d� cheferna deltar i samtalsgrupper." +
					"\r\n\r\n" +
					"Jag vet att alla har fullt upp och att det �r sv�rt att boka upp sig p� aktiviteter men vill p�minna om de olika aktiviteter som startar till h�sten. F�r att se din �terkoppling och de f�rslag p� h�lsofr�mjande aktiviteter som erbjuds dig beroende p� ditt resultat i enk�ten g�r du s� h�r:" +
					"\r\n\r\n" +
					"1. G� in p� adressen http://www.webbqps.se" +
					"\r\n" +
					"2. Klicka p� \"F�r projektdeltagare\"." +
					"\r\n" +
					"3. Skriv din e-postadress i rutan och klicka d�refter p� \"Skicka\"." +
					"\r\n" +
					"4. L�nken till din �terkoppling skickas direkt till din e-postadress." +
					"\r\n\r\n" +
					"N�gra exempel p� aktiviteter som erbjuds �r:" +
					"\r\n\r\n" +
					"* Kollegiala samtalsgrupper � som ger dig m�jlighet att arbeta med ditt f�rh�llningss�tt till stress och att identifiera arbetss�tt och rutiner som minska stress." +
					"\r\n" +
					"* Kost- och r�relsekurs, nio tr�ffar" +
					"\r\n" +
					"* Sluta r�ka-kurs via Internet och mail" +
					"\r\n" +
					"* F�rel�sningar om s�mn" +
					"\r\n" +
					"* Behandling av personalsjukgymnaster" +
					"\r\n" +
					"* Enskilda samtal med sjukhuskyrkan" +
					"\r\n" +
					"* Kontakt med f�retagsh�lsov�rden" +
					"\r\n" +
					"* Mindfulness-grupper (meditationsform f�r stresshantering)" +
					"\r\n\r\n" +
					"F�r eventuella fr�gor kontakta Mariette Weideskog, h�lsopedagog p� HR-avdelningen: mariette.veideskog@karolinska.se eller 070-484 58 05." +
					"\r\n\r\n" +
					"Mer information om projektet finns p� Inuti:" +
					"\r\n" +
					"Medarbetare > Chefsutveckling KI-LIME" +
					"\r\n\r\n" +
					"Med v�nliga h�lsningar" +
					"\r\n" +
					"Agneta J�hnk, HR-direkt�r";
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
