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
	/// Summary description for reGenStats2.
	/// </summary>
	public class reGenStats2 : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			OdbcDataReader rs = Db.recordSet("" +
				"SELECT " +
				"REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-',''), " +
				"a.AnswerID " +
				"FROM ProjectRoundUser u " +
				"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
				"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
				"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
				"WHERE p.ProjectRoundID = 20 AND a.EndDT IS NOT NULL");
			while(rs.Read())
			{
				#region regen
				System.Collections.Hashtable oldValues = new System.Collections.Hashtable();
				System.Text.StringBuilder FB = new System.Text.StringBuilder();

				OdbcDataReader rs2 = Db.recordSet("SELECT " +
					"a.ValueInt, " +
					"a.ValueText, " +
					"a.ValueDecimal, " +
					"a.QuestionID, " +
					"a.OptionID, " +
					"o.OptionType " +
					"FROM AnswerValue a " +
					"INNER JOIN [Option] o ON a.OptionID = o.OptionID " +
					"WHERE a.AnswerID = " + rs.GetInt32(1) + " AND a.DeletedSessionID IS NULL");
				while(rs2.Read())
				{
					string val = "";

					#region Fetch previously stored value
					switch(rs2.GetInt32(5))
					{
						case 1:
						{
							if(!rs2.IsDBNull(0))
							{
								val = rs2.GetInt32(0).ToString();
							}
							break;
						}
						case 2:
						{
							if(!rs2.IsDBNull(1))
							{
								val = rs2.GetString(1);
							}
							break;
						}
						case 3:
						{
							goto case 1;
						}
						case 4:
						{
							if(!rs2.IsDBNull(2))
							{
								val = ((float)rs2.GetDecimal(2)).ToString();
							}
							break;
						}
						case 9:
						{
							goto case 1;
						}
					}
					#endregion

					oldValues.Add("Q" + rs2.GetInt32(3) + "O" + rs2.GetInt32(4), HttpUtility.HtmlEncode(val));
				}
				rs2.Close();

				#region webbqps
				FB.Append("<B STYLE=\"font-size:20px;\">Återkoppling</B>");

				#region Rygg/nacke
				bool Qback = 
					oldValues.Contains("Q337O90") && Convert.ToInt32("0" + (string)oldValues["Q337O90"]) == 294 
					&& 
					(
					oldValues.Contains("Q341O90") && Convert.ToInt32("0" + (string)oldValues["Q341O90"]) == 294 
					|| 
					oldValues.Contains("Q342O90") && Convert.ToInt32("0" + (string)oldValues["Q342O90"]) == 294 
					|| 
					oldValues.Contains("Q343O90") && Convert.ToInt32("0" + (string)oldValues["Q343O90"]) == 294
					);
				bool Qneck = 
					oldValues.Contains("Q339O90") && Convert.ToInt32("0" + (string)oldValues["Q339O90"]) == 294 
					&& 
					(
					oldValues.Contains("Q346O90") && Convert.ToInt32("0" + (string)oldValues["Q346O90"]) == 294 
					|| 
					oldValues.Contains("Q347O90") && Convert.ToInt32("0" + (string)oldValues["Q347O90"]) == 294 
					|| 
					oldValues.Contains("Q348O90") && Convert.ToInt32("0" + (string)oldValues["Q348O90"]) == 294
					);
				bool QbnSleep = 
					oldValues.Contains("Q337O90") && Convert.ToInt32("0" + (string)oldValues["Q337O90"]) == 294 
					&& 
					oldValues.Contains("Q341O90") && Convert.ToInt32("0" + (string)oldValues["Q341O90"]) == 294 
					|| 
					oldValues.Contains("Q339O90") && Convert.ToInt32("0" + (string)oldValues["Q339O90"]) == 294 
					&& 
					oldValues.Contains("Q346O90") && Convert.ToInt32("0" + (string)oldValues["Q346O90"]) == 294;
				bool QbnWork = 
					oldValues.Contains("Q337O90") && Convert.ToInt32("0" + (string)oldValues["Q337O90"]) == 294 
					&& 
					oldValues.Contains("Q342O90") && Convert.ToInt32("0" + (string)oldValues["Q342O90"]) == 294 
					|| 
					oldValues.Contains("Q339O90") && Convert.ToInt32("0" + (string)oldValues["Q339O90"]) == 294 
					&& 
					oldValues.Contains("Q347O90") && Convert.ToInt32("0" + (string)oldValues["Q347O90"]) == 294;
				bool QbnWorse = 
					oldValues.Contains("Q337O90") && Convert.ToInt32("0" + (string)oldValues["Q337O90"]) == 294 
					&& 
					oldValues.Contains("Q343O90") && Convert.ToInt32("0" + (string)oldValues["Q343O90"]) == 294 
					|| 
					oldValues.Contains("Q339O90") && Convert.ToInt32("0" + (string)oldValues["Q339O90"]) == 294 
					&& 
					oldValues.Contains("Q348O90") && Convert.ToInt32("0" + (string)oldValues["Q348O90"]) == 294;
				if(Qback || Qneck)
				{
					FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">");
					if(Qback)
					{
						FB.Append("Rygg");
						if(Qneck)
						{
							FB.Append(" och ");
						}
					}
					if(Qneck)
					{
						FB.Append("Nacke");
					}
					FB.Append("</B><BR>Du har angivit att du har besvär från ");
					if(Qback)
					{
						FB.Append("ryggen");
						if(Qneck)
						{
							FB.Append(" och ");
						}
					}
					if(Qneck)
					{
						FB.Append("nacken");
					}
					FB.Append(" samt att dessa besvär ");
					if(QbnWorse)
					{
						FB.Append("förvärras av arbetet");
						if(QbnSleep || QbnWork)
						{
							FB.Append(" och att besvären ");
						}
					}
					if(QbnSleep || QbnWork)
					{
						FB.Append("varit så pass svåra att du haft svårt ");
						if(QbnSleep)
						{
							FB.Append("att sova");
							if(QbnWork)
							{
								FB.Append(" och ");
							}
						}
						if(QbnWork)
						{
							FB.Append("utföra ditt arbete");
						}
					}
					FB.Append(". Du rekommenderas att kontakta personalsjukgymnast.");
		
					FB.Append("" +
						"<BR><BR>" +
						"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:14px;\">Sjukgymnastik</B>" +
						"<BR><BR>" +
						"Som en del av det förebyggande hälso- och arbetsmiljöarbetet finns det sjukgymnaster som arbetar med belastnings- och stressrelaterade besvär hos de anställda, både i Huddinge och i Solna. Syftet är att kunna göra en tidig insats vid arbetsrelaterade besvär." +
						"<BR><BR>" +
						"Du har möjlighet att få hjälp av en sjukgymnast om du har drabbats av smärta eller led-, rygg- eller muskelbesvär. Vi erbjuder en sjukgymnastisk bedömning, behandling och uppföljning. Du kan erbjudas olika former av friskvårdsträning alternativt remitteras till en annan vårdgivare." +
						"<BR>" +
						"Sjukgymnasterna arbetar på uppdrag av HR-avdelningen och samarbetar med Sektionen för arbetsmiljö och hälsa, samt med företagshälsovården." +
						"<BR><BR>" +
						"<B>Huddinge</B>" +
						"<BR>" +
						"Susanne Sandström och Eva Ajax, Leg sjukgymnast, 585 818 26." +
						"<BR>" +
						"Sjukgymnastikkliniken, R 41" +
						"<BR>" +
						"Telefontid: måndag kl. 12:30-13:30, torsdag kl. 10:30-11:30" +
						"<BR><BR>" +
						"<B>Solna</B>" +
						"<BR>" +
						"Ulla Oxelbeck, Leg sjukgymnast 517 727 55." +
						"<BR>" +
						"Enheten för sjukgymnastik, en trappa ner (U1), i huvudentrén (röda hissarna)." +
						"</DIV>");
				}
				#endregion

				#region Smoke/Hosta/Pip
				bool Qhosta = oldValues.Contains("Q349O90") && Convert.ToInt32("0" + (string)oldValues["Q349O90"]) == 294;
				bool Qpip = oldValues.Contains("Q350O90") && Convert.ToInt32("0" + (string)oldValues["Q350O90"]) == 294;
				bool Qsmoke = oldValues.Contains("Q370O90") && Convert.ToInt32("0" + (string)oldValues["Q370O90"]) == 294;

				if(Qsmoke)
				{
					if(Qhosta || Qpip)
					{
						FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">");
						if(Qhosta)
						{
							FB.Append("Långvarig hosta");
							if(Qpip)
							{
								FB.Append(" och ");
							}
						}
						if(Qpip)
						{
							FB.Append("Pip i bröstet");
						}
						FB.Append("</B><BR>Dina svar visar att du har symtom som skulle kunna bero på din rökning. Om du inte redan har sökt läkarhjälp, rekommenderar vi att du gör det. Om du vill hjälp med att sluta röka, kan du få det genom arbetsplatsen.");
					}
					else
					{
						FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">Rökning</B><BR>Som du säkert vet, är rökning riskabelt för hälsan. Om du vill hjälp med att sluta röka, kan du få det genom arbetsplatsen.");
					}
					FB.Append("" +
						"<BR><BR>" +
						"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:14px;\">Har du bestämt dig för att sluta röka eller snusa?<BR/>Internetbaserad kurs</B>" +
						"<BR><BR>" +
						"Ni kommer att på egen hand arbeta med materialet &quot;Sluta nu&quot;, vilket Cancerfonden, Folkhälsoinstitutet, Hjärt- och Lungfonden samt Läkare mot tobak står bakom." +
						"<BR><BR>" +
						"Varje vecka får du ett veckobrev av oss och mellan dem arbetar du med en internetbaserad sluta-röka/snusa skola själv. Du kan när som helst få stöd av oss via e-post. Du kommer under den första hälften av kursen förbereda dig mentalt på att sluta röka eller snusa, samt få information om nikotinhjälpmedel. Fimpardatum sker efter några veckor. Efter &quot;fimpardatumet&quot; kommer du att lära dig vad som händer vid nikotinstoppet och hur du kan ta hand om dig själv." +
						"<BR><BR>" +
						"<B>Start</B>: 1/10" +
						"<BR>" +
						"<B>Anmälan</B>: senast 24/9" +
						"<BR><BR>" +
						"<B>Så här anmäler du dig till kursen</B>" +
						"<BR>" +
						"<A HREF=\"mailto:mariette.veideskog@karolinska.se?Subject=Anmälan till kursen sluta röka med hjälp av internet&Body=Namn:%0D%0AKontaktinformation:\">Klicka här</A> för att skicka anmälan via e-post till <A HREF=\"mailto:mariette.veideskog@karolinska.se?Subject=Anmälan till kursen sluta röka med hjälp av internet&Body=Namn:%0D%0AKontaktinformation:\">mariette.veideskog@karolinska.se</A>." +
						"<BR><BR>" +
						"Har ni frågor om kursverksamheten kan ni ringa till någon av hälsopedagogerna Charlotte Ovefelt 585 894 82 eller Mariette Veideskog, 585 868 43." +
						"</DIV>" +
						"<BR>" +
						"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:14px;\">Den svåra konsten att sluta röka<BR/>Föreläsning</B>" +
						"<BR><BR>" +
						"Har du någon gång funderat på att sluta röka? Det har de flesta som röker. Oavsett om du har funderat på att sluta eller ej är du välkommen på denna föreläsning." +
						"<BR>" +
						"Även du som inte röker, men vill stötta folk i din närhet som vill sluta är välkommen." +
						"<BR><BR>" +
						"Vi kommer att diskutera de svårigheter vi får när vi försöker sluta röka, men också om olika möjligheter som kan underlätta beslutet och stoppet. Du får verktyg du kan använda dig av om och när du bestämt dig för att sluta." +
						"<BR><BR>" +
						"<B>Huddinge</B>:" +
						"<BR>" +
						"Måndagen den 1 oktober, kl 14:30-16:00" +
						"<BR>" +
						"Konferensrum 2, HR-avdelningen, CI 77" +
						"<BR><BR>" +
						"<B>Solna</B>:" +
						"<BR>" +
						"Onsdagen den 3 oktober, kl 14:30-16:00" +
						"<BR>" +
						"AGA-salen, Eugeniahemmet" +
						"<BR><BR>" +
						"<B>Föreläsare</B>:" +
						"<BR>" +
						"Mariette Weideskog, Hälsopedagog, Sektionen för arbetsmiljö och hälsa, HR-avdelningen, Karolinska" +
						"</DIV>");
				}
				else if(Qpip || Qhosta)
				{
					FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">");
					if(Qhosta)
					{
						FB.Append("Långvarig hosta");
						if(Qpip)
						{
							FB.Append(" och ");
						}
					}
					if(Qpip)
					{
						FB.Append("Pip i bröstet");
					}
					FB.Append("</B><BR>Symtomen långvarig hosta och pip i bröstet kan vara symtom på astma. Om du inte du redan har behandling, rekommenderar vi att du söker din husläkare för en bedömning.<BR>");
				}
				#endregion
	
				#region Alkohol
				bool Qalco = 
					(
					oldValues.Contains("Q364O108") && Convert.ToInt32("0" + (string)oldValues["Q364O108"]) == 340 
					||
					oldValues.Contains("Q364O108") && Convert.ToInt32("0" + (string)oldValues["Q364O108"]) == 341
					) 
					&& 
					(
					oldValues.Contains("Q208O41") && Convert.ToInt32("0" + (string)oldValues["Q208O41"]) == 133
					||
					oldValues.Contains("Q208O41") && Convert.ToInt32("0" + (string)oldValues["Q208O41"]) == 134
					||
					oldValues.Contains("Q208O41") && Convert.ToInt32("0" + (string)oldValues["Q208O41"]) == 135
					||
					oldValues.Contains("Q210O41") && Convert.ToInt32("0" + (string)oldValues["Q210O41"]) == 133
					||
					oldValues.Contains("Q210O41") && Convert.ToInt32("0" + (string)oldValues["Q210O41"]) == 134
					||
					oldValues.Contains("Q210O41") && Convert.ToInt32("0" + (string)oldValues["Q210O41"]) == 135
					||
					oldValues.Contains("Q213O42") && Convert.ToInt32("0" + (string)oldValues["Q213O42"]) == 138
					);
				if(Qalco)
				{
					FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">Alkohol</B>" +
						"<BR>" +
						"I vår kultur ingår ofta alkohol i den sociala samvaron och kan ha flera positiva effekter. Dina svar talar för att alkoholen medför problem i ditt liv. Om du gör något åt situationen nu kan du få god hjälp. Om du väntar för länge med att ta i tu med detta kan problemen bli långvariga och förvärras." +
						"<BR><BR>" +
						"Du kan läsa mer om alkoholvanor på följande länkar: <A TARGET=\"_blank\" HREF=\"http://www.alna.se\">www.alna.se</A>, <A TARGET=\"_blank\" HREF=\"http://www.stressochalkohol.se\">www.stressochalkohol.se</A>, <A TARGET=\"_blank\" HREF=\"http://www.escreen.se\">www.escreen.se</A>." +
						"<BR><BR>" +
						"Nedan finns information om vart du kan vända dig för råd och stöd." +
						"<BR><BR>" +
						"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:16px;\">Stöd från Alkohollinjen</B>" +
						"<BR><BR>" +
						"Alkohollinjen erbjuder stöd per telefon." +
						"<BR><BR>" +
						"Funderar du över dina alkoholvanor? Ring oss så får du prata med våra behandlare. Om du behöver hjälp för att förändra dina vanor kan vi ge dig stöd. Det är kostnadsfritt." +
						"<BR><BR>" +
						"De som vänder sig till Alkohollinjen kan välja mellan två alternativ:" +
						"<BR>" +
						"- Att antingen ringa för rådgivning när du själv tycker att du behöver det, eller" +
						"<BR>" +
						"- Att vid ett antal tillfällen bli uppringd för rådgivning och uppföljning." +
						"<BR><BR>" +
						"Telefon 020-84 44 48" +
						"<BR>" +
						"Mejladress: <A HREF=\"mailto:info@alkohollinjen.se\">info@alkohollinjen.se</A>" +
						"<BR><BR>" +
						"Vilka svarar på Alkohollinjen?" +
						"<BR>" +
						"Bland rådgivarna finns allmänläkare, sjuksköterskor, mentalskötare, psykologer, hälsopedagoger och folkhälsovetare." +
						"<BR><BR>" +
						"Alkohollinjens öppettider kommer inledningsvis att vara måndag-torsdag kl 16:00-19:00." +
						"</DIV>" +
						"<BR>" +
						"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:16px;\">Stöd från Previa</B>" +
						"<BR><BR>" +
						"Kontakta företagssköterskan för rådgivning i alkohol- och drogfrågor." +
						"<BR><BR>" +
						"För dig som arbetar på Karolinska, Solna:" +
						"<BR>" +
						"<B>Previa Haga</B>" +
						"<BR>" +
						"Tfn tid: Mån-fre 09.00-12.00 samt 15.00-16.00 för rådgivning och tidbokning 08-728 51 55" +
						"<BR>" +
						"Öppet:	Mån-fre 08.00-16.45" +
						"<BR>" +
						"Adress: Gävlegatan 22, plan 6, 113 30 Stockholm" +
						"<BR>" +
						"Tfn vxl: 08-728 51 40" +
						"<BR><BR>" +
						"För dig som arbetar på Karolinska, Huddinge:" +
						"<BR>" +
						"<B>Previa Huddinge</B>" +
						"<BR>" +
						"Tfn tid: Mån-fre 09.00-12.00 för rådgivning och tidbokning 08-608 63 90" +
						"<BR>" +
						"Öppet: Mån-fre 08.00-16.45" +
						"<BR>" +
						"Adress: Patron Pehrs väg 4, 141 35 Huddinge" +
						"<BR>" +
						"Tfn vxl: 08-608 63 80" +
						"<BR><BR>" +
						"För dig som arbetar på Södersjukhuset och närbelägen vårdcentral:" +
						"<BR>" +
						"<B>Previa Gullmarsplan</B>" +
						"<BR>" +
						"Tfn tid: Mån-fre 09.00-11.00 samt 14.00-16.00 för rådgivning och tidbokning 08-722 24 01" +
						"<BR>" +
						"Öppet: Mån-fre 08.00-16.45" +
						"<BR>" +
						"Adress: Gullmarsplan 13, 121 40 Johanneshov" +
						"<BR>" +
						"Tfn vxl: 08 - 722 24 00" +
						"</DIV>");
				}
				#endregion

				#region Utbrändhet
				bool Qburnout = false;
				float score = 0f;
				string[] qos = ("Q380O114,Q381O114,Q384O114").Split(',');
				foreach(string s in qos)
				{
					if(oldValues.Contains(s))
					{
						switch(Convert.ToInt32("0" + (string)oldValues[s]))
						{
							case 361: score += 1; break;
							case 362: score += 2; break;
							case 363: score += 3; break;
							case 360: score += 4; break;
						}
					}
				}
				qos = ("Q382O114,Q383O114").Split(',');
				foreach(string s in qos)
				{
					if(oldValues.Contains(s))
					{
						switch(Convert.ToInt32("0" + (string)oldValues[s]))
						{
							case 361: score += 4; break;
							case 362: score += 3; break;
							case 363: score += 2; break;
							case 360: score += 1; break;
						}
					}
				}
				if(score/5 > 2.6)
				{
					Qburnout = true;
				}

				bool Qpbs = false;
				score = 0f;
				qos = ("Q401O116,Q402O116,Q403O116,Q404O116").Split(',');
				foreach(string s in qos)
				{
					if(oldValues.Contains(s))
					{
						switch(Convert.ToInt32("0" + (string)oldValues[s]))
						{
							case 367: score += 5; break;
							case 363: score += 4; break;
							case 368: score += 3; break;
							case 369: score += 2; break;
							case 361: score += 1; break;
						}
					}
				}
				if(score/qos.Length > 2.75)
				{
					Qpbs = true;
				}

				bool Qdepr = 
					oldValues.Contains("Q393O122") && Convert.ToInt32("0" + (string)oldValues["Q393O122"]) == 294 
					&& 
					(
					oldValues.Contains("Q387O115") && Convert.ToInt32("0" + (string)oldValues["Q387O115"]) == 364
					||
					oldValues.Contains("Q387O115") && Convert.ToInt32("0" + (string)oldValues["Q387O115"]) == 365
					||
					oldValues.Contains("Q388O115") && Convert.ToInt32("0" + (string)oldValues["Q388O115"]) == 364
					||
					oldValues.Contains("Q388O115") && Convert.ToInt32("0" + (string)oldValues["Q388O115"]) == 365
					||
					oldValues.Contains("Q389O115") && Convert.ToInt32("0" + (string)oldValues["Q389O115"]) == 364
					||
					oldValues.Contains("Q389O115") && Convert.ToInt32("0" + (string)oldValues["Q389O115"]) == 365
					||
					oldValues.Contains("Q390O115") && Convert.ToInt32("0" + (string)oldValues["Q390O115"]) == 364
					||
					oldValues.Contains("Q390O115") && Convert.ToInt32("0" + (string)oldValues["Q390O115"]) == 365
					||
					oldValues.Contains("Q391O115") && Convert.ToInt32("0" + (string)oldValues["Q391O115"]) == 364
					||
					oldValues.Contains("Q391O115") && Convert.ToInt32("0" + (string)oldValues["Q391O115"]) == 365
					||
					oldValues.Contains("Q392O115") && Convert.ToInt32("0" + (string)oldValues["Q392O115"]) == 364
					||
					oldValues.Contains("Q392O115") && Convert.ToInt32("0" + (string)oldValues["Q392O115"]) == 365
					);

				if(Qburnout)
				{
					if(Qdepr)
					{
						FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">Utmattningsdepression</B>" +
							"<BR>" +
							"Dina svar talar för att du kan ha symtom på depression. Nuförtiden kan depressioner behandlas med mycket gott resultat. Om du inte redan har en läkarkontakt, föreslår vi att du tar kontakt med din husläkare. Är dina problem arbetsrelaterade kan du även kontakta Previa, företagshälsovården." +
							"<BR><BR>");
					}
					else if(Qpbs)
					{
						FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">Utbrändhet</B>" +
							"<BR>" +
							"Dina svar visar att du har symtom som tyder på en hög stressnivå. Långvarig stress utan tillräcklig återhämtning kan leda till utbrändhet eller utmattningssyndrom. " +
							"Risken för långvariga besvär ökar påtagligt om man har så höga krav på sig själv att man inte tar sig tid för att vila och återhämta sig. " +
							"Det finns bra belägg för att en utbränningsprocess kan stoppas upp. Man kan ha hjälp av olika metoder, t ex kollegiala samtalsgrupper och mindfulnessträning. " +
							"Båda dessa alternativ kommer att finnas tillgängliga på din arbetsplats. Är dina problem arbetsrelaterade kan du även kontakta Previa, företagshälsovården." +
							"<BR><BR>");
					}
					else
					{
						FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">Utbrändhet</B>" +
							"<BR>" +
							"Dina svar visar att du har symtom som tyder på en hög stressnivå. Långvarig stress utan tillräcklig återhämtning kan leda till utbrändhet eller utmattningssyndrom. " +
							"Det finns bra belägg för att en utbränningsprocess kan stoppas upp. Man kan ha hjälp av olika metoder, t ex kollegiala samtalsgrupper och mindfulnessträning. " +
							"Båda dessa alternativ kommer att finnas tillgängliga på din arbetsplats. Är dina problem arbetsrelaterade kan du även kontakta Previa, företagshälsovården." +
							"<BR><BR>");
					}

					#region Mindfulness
					FB.Append("<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:16px;\">Mindfulnessmeditation</B>" +
						"<BR><BR>" +
						"Mindfulness kan beskrivas som ett förhållningssätt. Ett sätt att vara, lyssna och relatera. Mindfulness är ett observerande, icke-värderande och accepterande av det som händer i nuet. Istället för att ryckas med av t.ex. smärtförnimmelser, oro och katastroftankar lär man sig att förhålla sig som en välvillig men opartisk observatör av sin egen mentala aktivitet. Detta förhållningssätt underlättas av meditation där man övar sin förmåga att rikta och behålla uppmärksamheten. Mindfulnessmeditationen utförs i olika kroppspositioner; sittande, stående, liggande och när kroppen är i rörelse såväl som i stillhet." +
						"<BR><BR>" +
						"Mindfulnessmeditationen har visat sig ha en stressreducerade effekt, vilket antyder att den skulle kunna vara användbar också för att förebygga stress och stressrelaterade sjukdomstillstånd." +
						"<BR><BR>" +
						"Kursprogrammet är upplagt i enlighet med MBSR-programmet (Mindfulness Based Stress Reduction) som det utarbetats av pionjären Jon Kabat-Zinn i USA. Inledningsvis hålls ett orienteringsseminarium om kursens innehåll och bakgrund. Tyngdpunkten i kursen ligger på praktiska meditationsövningar under åtta grupptillfällen. Du förväntas att delta praktiskt (efter egen förmåga) i alla formella mindfulnessövningar och reflektera i grupp kring dina erfarenheter av övningarna. Var och en för dessutom en reflektionsdagbok." +
						"<BR><BR>" +
						"MBSR-programmet inkluderar en tyst dag (7-8 timmar) som är förlagd till lördagen eller söndagen (dag ej bestämt) efter seminarietillfälle sex. Detta tillfälle får du gå på din fritid. Under denna dag hålls lärarledda meditationsövningar." +
						"<BR><BR>" +
						"Du förväntas att utföra dagliga mindfulnessövningar i hemmet under ca 30 min 6-7 dagar/vecka. Vi vill passa på att göra dig uppmärksam på att kursen kräver motivation och beslutsamhet. Det krävs att du gör meditationsövningarna oavsett om det tar emot. Det kan paradoxalt nog upplevas stressande att ta del av en stressreducerande kurs. Om du redan på förhand vet att du inte kommer att kunna avsätta tid för de dagliga praktiska hemövningarna, så är kanske denna kurs inte rätt för dig just nu." +
						"<BR><BR>" +
						"<B>Tider för mindfulnessgrupper</B>" +
						"<BR><BR>" +
						"Solna: Torsdagar, kl 13:00-15:00, Gympasalen, Norrbacka. Start vecka 40." +
						"<BR>" +
						"Huddinge: Fredagar, kl 13:00-15:00. Grönasalen, personalgymmet, Novumhuset." +
						"<BR><BR>" +
						"Helgen, vecka 47 – kommer vi att träffas en utav dagarna (lördag eller söndag är inte bestämt ännu) och ha en tyst dag för båda grupperna gemensamt (7-8 tim), preliminärt i Solna." +
						"<BR><BR>" +
						"<B>Frågor och anmälan</B>" +
						"<BR>" +
						"Är du intresserad av att deltaga i en mindfulnessgrupp, kan du välja mellan att gå den i Huddinge eller i Solna. Efter förankring hos arbetsledare, sker en av timmarna på betald arbetstid, den andra timmen får du ta av din fritid. Även helgdagen vecka 47 får du ta av din egen tid. Grupperna startar vecka 40. Kostnad för material (2 CD och 1 DVD) tillkommer med 290 kr. Deltagandet i gruppen kommer sedan att utvärderas avseende effekten på upplevd arbetsrelaterad stress och hälsa. " +
						"<BR><BR>" +
						"Eventuella frågor besvaras av kursledaren Camilla Sköld, Med dr, leg sjukgymnast via e-post till <A HREF=\"mailto:camilla.skold@telia.com\">camilla.skold@telia.com</A>." +
						"<BR><BR>" +
						"<A HREF=\"mailto:mariette.veideskog@karolinska.se?Subject=Anmälan om intresse för Mindfulnessmeditation&Body=Namn:%0D%0ATelefonnummer hem:%0D%0APlats: (ange Huddinge eller Solna)\">Klicka här</A> för att skicka en anmälan via e-post till <A HREF=\"mailto:mariette.veideskog@karolinska.se?Subject=Anmälan om intresse för Mindfulnessmeditation&Body=Namn:%0D%0ATelefonnummer hem:%0D%0APlats: (ange Huddinge eller Solna)\">mariette.veideskog@karolinska.se</A>. I din anmälan vill vi att du uppger om du vill gå i Huddinge eller i Solna, samt telefonnummer hem. Sista anmälningsdag är den 24/9." +
						"</DIV>" +
						"<BR>");
					#endregion

					FB.Append("<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:16px;\">Erbjudande om att delta i Kollegiala Samtalsgrupper</B>" +
						"<BR><BR>" +
						"Ditt resultat av enkäten pekar på att du ligger på en hög stressnivå just nu. Upplevd stress hanteras på många olika sätt av olika individer och det är inte alls säkert att det behöver innebära något negativt, men det kan också tolkas som signaler på att något bör göras åt det." +
						"<BR>" +
						"Det finns inte <B><U>en</U></B> enkel lösning på problemet med stress och förebyggande åtgärder måste därför också ske inom många olika områden. Det krävs insatser såväl inom organisationen, på arbetsplatsen, i samhället i stort, som för den enskilde individen. Du som individ ges nu möjligheten att delta i;" +
						"<BR><BR>" +
						"<B>Reflekterande, kollegiala samtalsgrupper</B>" +
						"<BR>" +
						"Reflekterande kollegiala samtalsgrupper sker utifrån en pedagogisk metod  och är ett sätt att förebygga utveckling av arbetsrelaterad stress och utmattning. I tidigare studier med liknande grupper har man konstaterat att kollegialt stöd, tid för reflektion och erfarenhetsutbyte mellan kollegor visat sig vara mycket värdefullt för deltagarna." +
						"<BR>" +
						"Kollegor från olika arbetsplatser har ofta gemensamma yrkesrelaterade problem och erfarenheter, men samtidigt olika infallsvinklar och lösningar på dessa." +
						"<BR><BR>" +
						"<B>Genomförande</B>" +
						"<BR>" +
						"Vi kommer att skapa grupper med 8 personer ur samma eller liknande yrkesgrupp och leds av en handledare. Man träffas under 11 veckor, 2 timmar vid varje tillfälle. Efter förankring hos arbetsledare, sker en av timmarna på betald arbetstid, den andra timmen får du ta av din fritid." +
						"<BR><BR>" +
						"<B>Intresseanmälan och förfrågningar</B>" +
						"<BR>" +
						"<A HREF=\"mailto:mariette.veideskog@karolinska.se?Subject=Anmälan om intresse för Reflekterande, kollegiala samtalsgrupper&Body=Namn:%0D%0AKontaktinformation:%0D%0AYrkeskategori:%0D%0APlats: (ange Huddinge eller Solna)\">Klicka här</A> för att skicka en intresseanmälan (och eventuella frågor) via e-post till <A HREF=\"mailto:mariette.veideskog@karolinska.se?Subject=Anmälan om intresse för Reflekterande, kollegiala samtalsgrupper&Body=Namn:%0D%0AKontaktinformation:%0D%0AYrkeskategori:%0D%0APlats: (ange Huddinge eller Solna)\">mariette.veideskog@karolinska.se</A>. I din anmälan vill vi att du uppger om du vill gå i Solna eller i Huddine, samt yrkeskategori." +
						"</DIV>" +
						"<BR>" +

						"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:16px;\">Enskilda samtal</B>" +
						"<BR><BR>" +
						"Vi som arbetar i Sjukhuskyrkan har lång erfarenhet av att möta människor i sorg och kris. Att lyssna och tala om det riktigt svåra är en av våra allra viktigaste uppgifter. Syftet med samtalen är att stödja och hjälpa, ge hopp och mod. Samtalen kan ske vid ett eller flera tillfällen. " +
						"<BR><BR>" +
						"Sjukhuskyrkans enskilda samtal erbjuds alla, oavsett tro och livsåskådning. Många av dem vi träffar har i vanliga fall ingen anknytning till kyrkan. " +
						"<BR><BR>" +
						"<B>Huddinge</B>:" +
						"<BR>" +
						"Kontakta Leena Widén, ank 802 92 för bokning av samtal." +
						"<BR><BR>" +
						"<B>Solna</B>:" +
						"<BR>" +
						"Kontakta Hans-Peter Rasmussen, ank 740 73 för bokning av samtal." +
						"</DIV>" +
						"<BR>" +

						"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:16px;\">Stödsamtal hos Previa</B>" +
						"<BR><BR>" +
						"Är dina problem arbetsrelaterade kan du kontakta företagssköterskan på Previa för rådgivning." +
						"<BR><BR>" +
						"För dig som arbetar på Karolinska, Solna:" +
						"<BR>" +
						"<B>Previa Haga</B>" +
						"<BR>" +
						"Tfn tid: Mån-fre 09.00-12.00 samt 15.00-16.00 för rådgivning och tidbokning 08-728 51 55" +
						"<BR>" +
						"Öppet:	Mån-fre 08.00-16.45" +
						"<BR>" +
						"Adress: Gävlegatan 22, plan 6, 113 30 Stockholm" +
						"<BR>" +
						"Tfn vxl: 08-728 51 40" +
						"<BR><BR>" +
						"För dig som arbetar på Karolinska, Huddinge:" +
						"<BR>" +
						"<B>Previa Huddinge</B>" +
						"<BR>" +
						"Tfn tid: Mån-fre 09.00-12.00 för rådgivning och tidbokning 08-608 63 90" +
						"<BR>" +
						"Öppet: Mån-fre 08.00-16.45" +
						"<BR>" +
						"Adress: Patron Pehrs väg 4, 141 35 Huddinge" +
						"<BR>" +
						"Tfn vxl: 08-608 63 80" +
						"<BR><BR>" +
						"För dig som arbetar på Södersjukhuset och närbelägen vårdcentral:" +
						"<BR>" +
						"<B>Previa Gullmarsplan</B>" +
						"<BR>" +
						"Tfn tid: Mån-fre 09.00-11.00 samt 14.00-16.00 för rådgivning och tidbokning 08-722 24 01" +
						"<BR>" +
						"Öppet: Mån-fre 08.00-16.45" +
						"<BR>" +
						"Adress: Gullmarsplan 13, 121 40 Johanneshov" +
						"<BR>" +
						"Tfn vxl: 08 - 722 24 00" +
						"</DIV>");
				}
				else if(Qdepr)
				{
					FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">Depression</B>" +
						"<BR>" +
						"Dina svar talar för att du kan ha symtom på depression. Nuförtiden kan depressioner behandlas med mycket gott resultat. Om du inte redan har en läkarkontakt, föreslår vi att du tar kontakt med din husläkare. Är dina problem arbetsrelaterade kan du även kontakta Previa, företagshälsovården." +
						"<BR><BR>" +
						"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:16px;\">Previa, företagshälsovården</B>" +
						"<BR><BR>" +
						"För dig som arbetar på Karolinska, Solna:" +
						"<BR>" +
						"<B>Previa Haga</B>" +
						"<BR>" +
						"Tfn tid: Mån-fre 09.00-12.00 samt 15.00-16.00 för rådgivning och tidbokning 08-728 51 55" +
						"<BR>" +
						"Öppet:	Mån-fre 08.00-16.45" +
						"<BR>" +
						"Adress: Gävlegatan 22, plan 6, 113 30 Stockholm" +
						"<BR>" +
						"Tfn vxl: 08-728 51 40" +
						"<BR><BR>" +
						"För dig som arbetar på Karolinska, Huddinge:" +
						"<BR>" +
						"<B>Previa Huddinge</B>" +
						"<BR>" +
						"Tfn tid: Mån-fre 09.00-12.00 för rådgivning och tidbokning 08-608 63 90" +
						"<BR>" +
						"Öppet: Mån-fre 08.00-16.45" +
						"<BR>" +
						"Adress: Patron Pehrs väg 4, 141 35 Huddinge" +
						"<BR>" +
						"Tfn vxl: 08-608 63 80" +
						"<BR><BR>" +
						"För dig som arbetar på Södersjukhuset och närbelägen vårdcentral:" +
						"<BR>" +
						"<B>Previa Gullmarsplan</B>" +
						"<BR>" +
						"Tfn tid: Mån-fre 09.00-11.00 samt 14.00-16.00 för rådgivning och tidbokning 08-722 24 01" +
						"<BR>" +
						"Öppet: Mån-fre 08.00-16.45" +
						"<BR>" +
						"Adress: Gullmarsplan 13, 121 40 Johanneshov" +
						"<BR>" +
						"Tfn vxl: 08 - 722 24 00" +
						"</DIV>");
				}
				#endregion

				#region Sleep
				if(oldValues.Contains("Q374O86"))
				{
					bool QsleepI = false;

					FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">Sömn</B>" +
						"<BR>" +
						"Sömnen är vår viktigaste resurs för återhämtning och är väsentlig bland annat för att bevara hälsa, välbefinnande och prestationsförmåga. Under sömnen insöndras flera viktiga hormoner som reparerar, läker och bygger upp kroppen. Om sömnen under en längre period är störd löper man stor risk för att på sikt drabbas av olika stressrelaterade sjukdomar." +
						"<BR><BR>");

					switch(Convert.ToInt32("0" + (string)oldValues["Q374O86"]))
					{
						case 310: FB.Append("Du har angivit att din sömn är Bra, vilket tyder på att din sömn och återhämtning är god; att du generellt sett somnar lätt, sover gott och känner dig utvilad när du vaknar.<BR>"); break;
						case 311: FB.Append("Du har angivit att din sömn är Ganska bra, vilket tyder på att din sömn och återhämtning är god; att du generellt sett somnar lätt, sover gott och känner dig utvilad när du vaknar.<BR>"); break;
						case 312: QsleepI = true; FB.Append("Du har angivit att din sömn är varken är bra eller dålig. Om du upplever att du återkommande har svårt att somna, sova gott eller inte är utvilad när du vaknar finns det anledning att förbättra din sömn för att på sikt bevara hälsa och välbefinnande. Det är i sådana fall viktigt att tänka på att varva ned minst två timmar före sänggåendet. Man kan dessutom förbättra sömnkvaliteten genom att regelbundet genomföra andnings- och avslappningsövningar före sänggåendet."); break;
						case 313: QsleepI = true; FB.Append("Du har angivit att din sömn är Ganska dålig, vilket generellt sett speglar något/några av följande symtom: svårigheter att somna, dålig sömnkvalitet, trötthet vid uppvaknande och att man inte känner sig utvilad. Dessa symtom signalerar ett tydligt behov av att se över sömnen. Det är helt normalt att vid enstaka tillfällen eller under kortare perioder sova dåligt av olika skäl, men om du haft sömnbesvär en längre tid behöver dessa signaler tas på allvar. Du kan börja med att reflektera över om det finns uppenbara anledningar till att du sover dåligt. Om du är osäker på hur du ska hantera dina sömnproblem rekommenderar vi dig att kontakta din husläkare. Är dina problem arbetsrelaterade kan du även kontakta Previa, företagshälsovården."); break;
						case 314: QsleepI = true; FB.Append("Du har angivit att din sömn är Dålig, vilket generellt sett speglar något/några av följande symtom: svårigheter att somna, dålig sömnkvalitet, trötthet vid uppvaknande och att man inte känner sig utvilad. Dessa symtom signalerar ett tydligt behov av att se över sömnen. Det är helt normalt att vid enstaka tillfällen eller under kortare perioder sova dåligt av olika skäl, men om du haft sömnbesvär en längre tid behöver dessa signaler tas på allvar. Du kan börja med att reflektera över om det finns uppenbara anledningar till att du sover dåligt. Om du är osäker på hur du ska hantera dina sömnproblem rekommenderar vi dig att kontakta din husläkare. Är dina problem arbetsrelaterade kan du även kontakta Previa, företagshälsovården."); break;
					}
					if(QsleepI)
					{
						FB.Append("<BR><BR>" +
							"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
							"<B STYLE=\"font-size:16px;\">&quot;Hur sover du egentligen?&quot;</B>" +
							"<BR><BR>" +
							"Tillhör du den kategorin som räknar får på natten så ska du komma på denna föreläsning. Vad är sömn egentligen? När ska man ta tupplurar?  Är det bra att räkna får? Du kommer få kunskap om hur vår sömncykel fungerar och få tips och idéer på vad du själv kan göra för att förbättra din sömn." +
							"<BR><BR>" +
							"<B>Huddinge</B>:" +
							"<BR>" +
							"Torsdagen den 11/10, kl 14:30-16:00, R 64." +
							"<BR><BR>" +
							"<B>Solna</B>:" +
							"<BR>" +
							"Måndagen den 8/10, kl 14:30-16:00, Skandiasalen, Astrid Lindgren." +
							"<BR><BR>" +
							"<B>Föreläsare</B>: Mariette Weideskog, Hälsopedagog, Sektionen för arbetsmiljö och hälsa, HR-avdelningen " +
							"<BR><BR>" +
							"Du behöver inte anmäla dig till föreläsningen, det är bara att komma. Du får gå på arbetstid och förutsätter medgivande från din närmsta chef." +
							"</DIV>");
					}
				}
				#endregion

				#region Health
				if(oldValues.Contains("Q331O98"))
				{
					FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">Hälsa</B>" +
						"<BR>" +
						"Det är numer väletablerat att självskattad hälsa är en viktig indikator för hur man kommer att må i framtiden. Om man bedömer sin hälsa som ganska bra eller bra har man betydligt större chans att bevara/förbättra sin hälsa i framtiden jämfört med dem som skattat den egna hälsan som dålig." +
						"<BR><BR>");

					switch(Convert.ToInt32("0" + (string)oldValues["Q331O98"]))
					{
						case 310: FB.Append("Du har angivit att din hälsa är Bra och verkar således ha en hälsosam livsstil och/eller må ganska bra. Personer med din skattning har störst sannolikhet att bevara eller öka sin hälsa i framtiden."); break;
						case 311: FB.Append("Du har angivit att din hälsa är Ganska bra och verkar således ha en hälsosam livsstil och/eller må ganska bra. Personer med din skattning har störst sannolikhet att bevara eller öka sin hälsa i framtiden."); break;
						case 315: FB.Append("Du har angivit att din hälsa varken är bra eller dålig, vilket betyder att det kan finnas en risk för att din hälsa försämras ytterligare i framtiden. Eftersom självskattad hälsa är ett sammanfattande mått på både fysiskt och mentalt välbefinnande rekommenderar vi att du ser över din livsstil, till exempel vad gäller motion, sömn, och matvanor."); break;
						case 316: FB.Append("Du har angivit att din hälsa är Ganska dålig. Om det inte är fråga om en tillfällig nedgång i hur du mår, och om du inte redan har läkarkontakt, rekommenderar vi att du kontaktar din husläkare för en diskussion av ditt hälsotillstånd och vad som eventuellt kan göras för att förbättra det. Är dina problem arbetsrelaterade kan du även kontakta företagshälsovården, Previa."); break;
						case 317: FB.Append("Du har angivit att din hälsa är Dålig. Om det inte är fråga om en tillfällig nedgång i hur du mår, och om du inte redan har läkarkontakt, rekommenderar vi att du kontaktar din husläkare för en diskussion av ditt hälsotillstånd och vad som eventuellt kan göras för att förbättra det. Är dina problem arbetsrelaterade kan du även kontakta företagshälsovården, Previa."); break;
					}
				}
				#endregion

				int Qscore = 0;

				#region Findrisk
				if(
					(
					oldValues.Contains("Q354O104") && Convert.ToInt32("0" + (string)oldValues["Q354O104"]) == 295 
					||
					oldValues.Contains("Q354O104") && Convert.ToInt32("0" + (string)oldValues["Q354O104"]) == 334 
					)
					&&
					oldValues.Contains("Q314O83") && (string)oldValues["Q314O83"] != ""	// weight
					&&
					oldValues.Contains("Q313O82") && (string)oldValues["Q313O82"] != ""	// height
					&&
					oldValues.Contains("Q538O82") && (string)oldValues["Q538O82"] != ""	// waist
					&&
					oldValues.Contains("Q310O79") && (string)oldValues["Q310O79"] != ""	// gender
					&&
					oldValues.Contains("Q311O81") && (string)oldValues["Q311O81"] != ""	// age
					&&
					oldValues.Contains("Q368O109") && (string)oldValues["Q368O109"] != ""	// act1, A=342, B=343
					&&
					oldValues.Contains("Q369O110") && (string)oldValues["Q369O110"] != ""	// act2, A=322,346, B=322
					)
				{
					int Qage = Convert.ToInt32(submit.strFloatToStr((string)oldValues["Q311O81"]));
					bool Qfemale = ((string)oldValues["Q310O79"] == "255");
		
					double Qbmi = Convert.ToDouble(submit.strFloatToStr((string)oldValues["Q314O83"])) / (Convert.ToDouble(submit.strFloatToStr((string)oldValues["Q313O82"]))/100 * Convert.ToDouble(submit.strFloatToStr((string)oldValues["Q313O82"]))/100);
					double Qwaist = Convert.ToDouble(submit.strFloatToStr((string)oldValues["Q538O82"]));
					bool Qnomotion = false;
					if(((string)oldValues["Q368O109"] == "342") && (((string)oldValues["Q369O110"] == "322") || ((string)oldValues["Q369O110"] == "346")))
					{
						Qnomotion = true;
					}
					if(((string)oldValues["Q368O109"] == "343") && ((string)oldValues["Q369O110"] == "322"))
					{
						Qnomotion = true;
					}
					bool Qnoveggies = oldValues.Contains("Q539O134") && Convert.ToInt32("0" + (string)oldValues["Q539O134"]) == 417;
					bool Qbp = 
						oldValues.Contains("Q352O104") && Convert.ToInt32("0" + (string)oldValues["Q352O104"]) == 294 
						&& 
						oldValues.Contains("Q356O90") && Convert.ToInt32("0" + (string)oldValues["Q356O90"]) == 294;
					bool Qbs = oldValues.Contains("Q638O90") && Convert.ToInt32("0" + (string)oldValues["Q638O90"]) == 294;

					if(oldValues.Contains("Q639O137") && Convert.ToInt32("0" + (string)oldValues["Q639O137"]) == 428)
						Qscore += 3;
					if(oldValues.Contains("Q639O137") && Convert.ToInt32("0" + (string)oldValues["Q639O137"]) == 429)
						Qscore += 5;

					if(Qage >= 65)
					{
						Qscore += 4;
					}
					else if(Qage >= 55)
					{
						Qscore += 3;
					}
					else if(Qage >= 45)
					{
						Qscore += 2;
					}

					if(Qbmi > 30)
					{
						Qscore += 3;
					}
					else if(Qbmi >= 25)
					{
						Qscore += 1;
					}
		
					if(Qfemale && Qwaist > 88 || !Qfemale && Qwaist > 102)
					{
						Qscore += 4;
					}
					else if(Qfemale && Qwaist >= 80 || !Qfemale && Qwaist >= 94)
					{
						Qscore += 3;
					}
					if(Qnomotion)
					{
						Qscore += 2;
					}
					if(Qnoveggies)
					{
						Qscore += 1;
					}
					if(Qbp)
					{
						Qscore += 2;
					}
					if(Qbs)
					{
						Qscore += 5;
					}

					FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">Diabetes</B>" +
						"<BR>");
					if(Qscore > 20)
					{
						FB.Append("Dina sammanvägda poäng av livsstil och ärftliga faktorer antyder att du löper mycket stor risk att drabbas av typ-2 diabetes. Uppskattningsvis 50% insjuknar. Regelbunden och måttlig motion samt god kosthållning reducerar risken att insjukna i diabetes. Vi föreslår därför att du deltar i något av eller båda alternativen nedan samt kontaktar din husläkare för en noggrannare bedömning av blodsocker (både fasteglukos och glukosbelastning alternativt blodsockret efter en måltid). Det visar om du eventuellt har symtomfri diabetes.");
					}
					else if(Qscore >= 15)
					{
						FB.Append("Dina sammanvägda poäng av livsstil och ärftliga faktorer antyder att du löper stor risk att drabbas av typ-2 diabetes. Uppskattningsvis 33% insjuknar. Regelbunden och måttlig motion samt god kosthållning reducerar risken att insjukna i diabetes. Vi föreslår därför att du deltar i något av eller båda alternativen nedan samt kontaktar din husläkare för en noggrannare bedömning av risken.");
					}
					else if(Qscore >= 12)
					{
						FB.Append("Dina sammanvägda poäng av livsstil och ärftliga faktorer antyder att du har en måttlig risk att drabbas av typ-2 diabetes. Uppskattningsvis 17% insjuknar. Regelbunden och måttlig motion samt god kosthållning reducerar risken att insjukna i diabetes.");
					}
					else if(Qscore >= 7)
					{
						FB.Append("Dina sammanvägda poäng av livsstil och ärftliga faktorer antyder att du har en något förhöjd risk att drabbas av typ-2 diabetes. Uppskattningsvis 4% insjuknar. Regelbunden och måttlig motion samt god kosthållning reducerar risken att insjukna i diabetes.");
					}
					else
					{
						FB.Append("Dina sammanvägda poäng av livsstil och ärftliga faktorer antyder att du inte löper särskilt stor risk att drabbas av typ-2 diabetes. Uppskattningsvis 1% insjuknar. Regelbunden och måttlig motion samt god kosthållning reducerar risken att insjukna i diabetes.");
					}
					FB.Append("<BR><BR>" +
						"Typ 2-diabetes (vuxendiabetes, åldersdiabetes) är en allvarlig och ärftlig sjukdom. Men man kan i hög grad påverka diabetesrisken genom livsstilen. Övervikt, särskilt bukfetma, fysisk inaktivitet, dåliga matvanor och rökning ökar risken att få typ 2-diabetes." +
						"<BR><BR>" +
						"Både högre ålder och ärftliga faktorer kan öka risken för typ 2-diabetes och dessa faktorer kan man inte påverka. Däremot kan man påverka de övriga riskfaktorerna såsom till exempel övervikt, bukfetma, fysisk inaktivitet, matvanor och rökning. Genom livsstilen kan man helt och hållet förhindra eller åtminstone skjuta upp typ 2-diabetes så långt fram i tiden som möjligt. Om man har diabetes i släkten ska man vara extra noga med att hålla vikten med åren. Fett runt midjan, så kallad bukfetma, utgör en extra stor risk. Det kan ta tid innan man märker av symtom från typ 2-diabetes.");

					if(Qscore >= 12)
					{
						FB.Append("<BR><BR>" +
							"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
							"<B STYLE=\"font-size:16px;\">Individuell träningsrådgivning</B>" +
							"<BR><BR>" +
							"Som anställd på Karolinska erbjuds du hjälp med individuell träningsrådgivning.  Det kan vara att få en styrketränings introduktion i vårt personalgym, hjälp med rörlighets- eller konditionsträning, stretchövningar m.m." +
							"<BR><BR>" +
							"För bokning av träningsrådgivning i <B>Solna</B>" +
							"<BR>" +
							"Tomas Hernvall, Friskvårdskonsulent/Gymföreståndare, Sektionen för arbetsmiljö och hälsa, HR-avdelningen,  ank 757 10.  " +
							"<BR><BR>" +
							"För bokning av träningsrådgivning i <B>Huddinge</B>" +
							"<BR>" +
							"Marie Hammarström, friskvårdskonsulent/Gymföreståndare, Sektionen för arbetsmiljö och hälsa, HR-avdelningen, ank 868 42." +
							"<BR><BR>" +
							"Du får gå på träningsrådgivningen på arbetstid och förutsätter medgivande av din närmsta chef." +
							"</DIV>" +
							"<BR>" +
							"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
							"<B STYLE=\"font-size:16px;\">&quot;Kost, Rörelse, Hälsa&quot;</B>" +
							"<BR><BR>" +
							"Vill du lära dig mer om kost och motion? Hur påverkar kosten och rörelsen ditt välbefinnande och din hälsa? Börjar favoritbyxorna sitta lite tight i midjan?  Nu har du chansen att på ett bra sätt hitta en hälsosam vikt och få bra vanor genom att lära dig mer om kostens och rörelsens betydelse." +
							"<BR><BR>" +
							"Kursen består av 9 gruppträffar a´ 1 timme. På träffarna kommer vi bland annat att diskutera våra mat- och motionsvanor och beteenden. Inom kosten kommer vi bland annat att prata om våra energikällor, måltidssammansättning, fettkällor samt kosttrender. Du kommer inte att följa någon diet, utan tanken är att du ska lära dig så mycket om kost så att du kan sätta samman en bra måltid själv. Fysisk aktivitet och rörelse är en viktig grundpelare i en hälsosam livsstil och en hjälp till att få energibalans och därmed en hälsosam vikt. Vi kommer bland annat att prata om positiva effekter av fysisk aktivitet och praktisera detta på ett enklare sätt. Hur lite kan du &quot;komma undan med&quot;?" +
							"<BR><BR>" +
							"Vid varje kurstillfälle har samtliga möjlighet att väga sig. Kursinnehåll delas ut vid första tillfället." +
							"<BR><BR>" +
							"<B>Datum, Tid & Lokal</B>" +
							"<BR><BR>" +
							"<B>Solna</B>" +
							"<BR>" +
							"Tisdagar kl 11:00-12:00, med start vecka 40. Kursen kommer att hållas i Eugeniahemmet, första kurstillfället i Aga-salen." +
							"<BR><BR>" +
							"<B>Huddinge</B>" +
							"<BR>" +
							"Tisdagar, kl 15:00-16:00, med start vecka 40. Kursen kommer att hållas i konferensrummet i personalgymmet, Novumhuset." +
							"<BR><BR>" +
							"<B>Kursledare</B>" +
							"<BR>" +
							"Charlotte Ovefelt, Hälsopedagog" +
							"<BR><BR>" +
							"<B>Kostnad</B>" +
							"<BR>" +
							"Kursen är avgiftsfri och sker på arbetstid och förutsätter medgivande från din närmsta chef." +
							"<BR><BR>" +
							"<B>Anmälan</B>" +
							"<BR>" +
							"Senast fredag 24/9. <A HREF=\"mailto:mariette.veideskog@karolinska.se?Subject=Anmälan till Kost, Rörelse, Hälsa&Body=Namn:%0D%0AKontaktinformation:%0A%0APlats: (ange Huddinge eller Solna)\">Klicka här</A> för att skicka anmälan via e-post till <A HREF=\"mailto:mariette.veideskog@karolinska.se?Subject=Anmälan till Kost, Rörelse, Hälsa&Body=Namn:%0D%0AKontaktinformation:%0A%0APlats: (ange Huddinge eller Solna)\">mariette.veideskog@karolinska.se</A>. När du skickar din anmälan via e-post vill vi att du skriver kursens namn som ärende samt om du vill gå i Solna eller Huddinge." +
							"<BR><BR>" +
							"Har ni frågor om kursverksamheten kan ni ringa till någon av hälsopedagogerna Charlotte Ovefelt 585 894 82 eller Mariette Veideskog, 585 868 43." +
							"</DIV>");
					}
				}
				#endregion

				if(!Qalco && !Qdepr && !Qburnout && Qscore < 12)
				{
					FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">Friskvård</B><BR>Du är välkommen att delta i friskvårdens vanliga utbud. Klicka på länken för mer information:<BR><A HREF=\"http://inuti.karolinskauniversitetssjukhuset.se/templates/Page____8345.aspx\" TARGET=\"_blank\">http://inuti.karolinskauniversitetssjukhuset.se/templates/Page____8345.aspx</A>");
				}

				#endregion

				submit.exportPDF(FB.ToString(),rs.GetString(0));
				#endregion

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
