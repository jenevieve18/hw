using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Configuration;

public partial class mailSubmission : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		SqlDataReader rs = Db.rs("SELECT TOP 300 MailSubmissionID, Email, Variable FROM MailSubmission WHERE Processed IS NULL", "eFormSqlConnection");
		while (rs.Read())
		{
			string s = "" +
				"Kära deltagare i PAUS-projektet och delstudien om magnesium," + "\r\n" +
				"\r\n" +
				"Vi vill börja med att tacka för ditt deltagande och engagemang i studien. Vi arbetar för fullt med att ta fram individuell återkoppling, tolkning och jämförelse för dina hörseltest, blodtryck och njurfunktion. Dessa resultat kommer att läggs upp på ditt HealthWatch-konto inom några veckor, och vi meddelar när de är på plats." + "\r\n" +
				"\r\n" +
				"Fick jag magnesium eller placebo?" + "\r\n" +
				"\r\n" +
				"Spänningen har varit olidlig, men vi kan nu äntligen meddela att du fick:" + "\r\n" +
				"\r\n" +
				rs.GetString(2) + "\r\n" +
				"\r\n" +
				"Återigen stort tack för ditt deltagande så här långt. PAUS-projektet fortsätter året ut med den korta 15-sekundersenkäten och en sista längre uppföljande enkät i höst." + "\r\n" +
				"\r\n" +
				"Bästa hälsningar," + "\r\n" +
				"\r\n" +
				"Karin Villaume & Dan Hasson, projektgruppen för PAUS" + "\r\n" +
				"\r\n" +
				"Om du har några frågor eller funderingar är du välkommen att kontakta doktorand Karin Villaume på tel 070-241 56 90 eller e-post: karin.villaume@gmail.com eller paus@healthwatch.se";

			Db.exec("INSERT INTO MailQueue (AdrTo,AdrFrom,Subject,Body) VALUES ('" + rs.GetString(1).Trim() + "','paus@healthwatch.se','Magnesium eller placebo?','" + s + "')", "eFormSqlConnection");
			Db.exec("INSERT INTO MailQueue (AdrTo,AdrFrom,Subject,Body) VALUES ('paus@healthwatch.se','paus@healthwatch.se','Magnesium eller placebo? (" + rs.GetString(1).Trim() + ")','" + s + "')", "eFormSqlConnection");

			Db.exec("UPDATE MailSubmission SET Processed = GETDATE() WHERE MailSubmissionID = " + rs.GetInt32(0), "eFormSqlConnection");

			HttpContext.Current.Response.Write(rs.GetString(1) + ", " + rs.GetString(2) + "<br/>");
		}
		rs.Close();
    }
}