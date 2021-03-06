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
				FB.Append("<B STYLE=\"font-size:20px;\">�terkoppling</B>");

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
					FB.Append("</B><BR>Du har angivit att du har besv�r fr�n ");
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
					FB.Append(" samt att dessa besv�r ");
					if(QbnWorse)
					{
						FB.Append("f�rv�rras av arbetet");
						if(QbnSleep || QbnWork)
						{
							FB.Append(" och att besv�ren ");
						}
					}
					if(QbnSleep || QbnWork)
					{
						FB.Append("varit s� pass sv�ra att du haft sv�rt ");
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
							FB.Append("utf�ra ditt arbete");
						}
					}
					FB.Append(". Du rekommenderas att kontakta personalsjukgymnast.");
		
					FB.Append("" +
						"<BR><BR>" +
						"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:14px;\">Sjukgymnastik</B>" +
						"<BR><BR>" +
						"Som en del av det f�rebyggande h�lso- och arbetsmilj�arbetet finns det sjukgymnaster som arbetar med belastnings- och stressrelaterade besv�r hos de anst�llda, b�de i Huddinge och i Solna. Syftet �r att kunna g�ra en tidig insats vid arbetsrelaterade besv�r." +
						"<BR><BR>" +
						"Du har m�jlighet att f� hj�lp av en sjukgymnast om du har drabbats av sm�rta eller led-, rygg- eller muskelbesv�r. Vi erbjuder en sjukgymnastisk bed�mning, behandling och uppf�ljning. Du kan erbjudas olika former av friskv�rdstr�ning alternativt remitteras till en annan v�rdgivare." +
						"<BR>" +
						"Sjukgymnasterna arbetar p� uppdrag av HR-avdelningen och samarbetar med Sektionen f�r arbetsmilj� och h�lsa, samt med f�retagsh�lsov�rden." +
						"<BR><BR>" +
						"<B>Huddinge</B>" +
						"<BR>" +
						"Susanne Sandstr�m och Eva Ajax, Leg sjukgymnast, 585 818 26." +
						"<BR>" +
						"Sjukgymnastikkliniken, R 41" +
						"<BR>" +
						"Telefontid: m�ndag kl. 12:30-13:30, torsdag kl. 10:30-11:30" +
						"<BR><BR>" +
						"<B>Solna</B>" +
						"<BR>" +
						"Ulla Oxelbeck, Leg sjukgymnast 517 727 55." +
						"<BR>" +
						"Enheten f�r sjukgymnastik, en trappa ner (U1), i huvudentr�n (r�da hissarna)." +
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
							FB.Append("L�ngvarig hosta");
							if(Qpip)
							{
								FB.Append(" och ");
							}
						}
						if(Qpip)
						{
							FB.Append("Pip i br�stet");
						}
						FB.Append("</B><BR>Dina svar visar att du har symtom som skulle kunna bero p� din r�kning. Om du inte redan har s�kt l�karhj�lp, rekommenderar vi att du g�r det. Om du vill hj�lp med att sluta r�ka, kan du f� det genom arbetsplatsen.");
					}
					else
					{
						FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">R�kning</B><BR>Som du s�kert vet, �r r�kning riskabelt f�r h�lsan. Om du vill hj�lp med att sluta r�ka, kan du f� det genom arbetsplatsen.");
					}
					FB.Append("" +
						"<BR><BR>" +
						"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:14px;\">Har du best�mt dig f�r att sluta r�ka eller snusa?<BR/>Internetbaserad kurs</B>" +
						"<BR><BR>" +
						"Ni kommer att p� egen hand arbeta med materialet &quot;Sluta nu&quot;, vilket Cancerfonden, Folkh�lsoinstitutet, Hj�rt- och Lungfonden samt L�kare mot tobak st�r bakom." +
						"<BR><BR>" +
						"Varje vecka f�r du ett veckobrev av oss och mellan dem arbetar du med en internetbaserad sluta-r�ka/snusa skola sj�lv. Du kan n�r som helst f� st�d av oss via e-post. Du kommer under den f�rsta h�lften av kursen f�rbereda dig mentalt p� att sluta r�ka eller snusa, samt f� information om nikotinhj�lpmedel. Fimpardatum sker efter n�gra veckor. Efter &quot;fimpardatumet&quot; kommer du att l�ra dig vad som h�nder vid nikotinstoppet och hur du kan ta hand om dig sj�lv." +
						"<BR><BR>" +
						"<B>Start</B>: 1/10" +
						"<BR>" +
						"<B>Anm�lan</B>: senast 24/9" +
						"<BR><BR>" +
						"<B>S� h�r anm�ler du dig till kursen</B>" +
						"<BR>" +
						"<A HREF=\"mailto:mariette.veideskog@karolinska.se?Subject=Anm�lan till kursen sluta r�ka med hj�lp av internet&Body=Namn:%0D%0AKontaktinformation:\">Klicka h�r</A> f�r att skicka anm�lan via e-post till <A HREF=\"mailto:mariette.veideskog@karolinska.se?Subject=Anm�lan till kursen sluta r�ka med hj�lp av internet&Body=Namn:%0D%0AKontaktinformation:\">mariette.veideskog@karolinska.se</A>." +
						"<BR><BR>" +
						"Har ni fr�gor om kursverksamheten kan ni ringa till n�gon av h�lsopedagogerna Charlotte Ovefelt 585 894 82 eller Mariette Veideskog, 585 868 43." +
						"</DIV>" +
						"<BR>" +
						"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:14px;\">Den sv�ra konsten att sluta r�ka<BR/>F�rel�sning</B>" +
						"<BR><BR>" +
						"Har du n�gon g�ng funderat p� att sluta r�ka? Det har de flesta som r�ker. Oavsett om du har funderat p� att sluta eller ej �r du v�lkommen p� denna f�rel�sning." +
						"<BR>" +
						"�ven du som inte r�ker, men vill st�tta folk i din n�rhet som vill sluta �r v�lkommen." +
						"<BR><BR>" +
						"Vi kommer att diskutera de sv�righeter vi f�r n�r vi f�rs�ker sluta r�ka, men ocks� om olika m�jligheter som kan underl�tta beslutet och stoppet. Du f�r verktyg du kan anv�nda dig av om och n�r du best�mt dig f�r att sluta." +
						"<BR><BR>" +
						"<B>Huddinge</B>:" +
						"<BR>" +
						"M�ndagen den 1 oktober, kl 14:30-16:00" +
						"<BR>" +
						"Konferensrum 2, HR-avdelningen, CI 77" +
						"<BR><BR>" +
						"<B>Solna</B>:" +
						"<BR>" +
						"Onsdagen den 3 oktober, kl 14:30-16:00" +
						"<BR>" +
						"AGA-salen, Eugeniahemmet" +
						"<BR><BR>" +
						"<B>F�rel�sare</B>:" +
						"<BR>" +
						"Mariette Weideskog, H�lsopedagog, Sektionen f�r arbetsmilj� och h�lsa, HR-avdelningen, Karolinska" +
						"</DIV>");
				}
				else if(Qpip || Qhosta)
				{
					FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">");
					if(Qhosta)
					{
						FB.Append("L�ngvarig hosta");
						if(Qpip)
						{
							FB.Append(" och ");
						}
					}
					if(Qpip)
					{
						FB.Append("Pip i br�stet");
					}
					FB.Append("</B><BR>Symtomen l�ngvarig hosta och pip i br�stet kan vara symtom p� astma. Om du inte du redan har behandling, rekommenderar vi att du s�ker din husl�kare f�r en bed�mning.<BR>");
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
						"I v�r kultur ing�r ofta alkohol i den sociala samvaron och kan ha flera positiva effekter. Dina svar talar f�r att alkoholen medf�r problem i ditt liv. Om du g�r n�got �t situationen nu kan du f� god hj�lp. Om du v�ntar f�r l�nge med att ta i tu med detta kan problemen bli l�ngvariga och f�rv�rras." +
						"<BR><BR>" +
						"Du kan l�sa mer om alkoholvanor p� f�ljande l�nkar: <A TARGET=\"_blank\" HREF=\"http://www.alna.se\">www.alna.se</A>, <A TARGET=\"_blank\" HREF=\"http://www.stressochalkohol.se\">www.stressochalkohol.se</A>, <A TARGET=\"_blank\" HREF=\"http://www.escreen.se\">www.escreen.se</A>." +
						"<BR><BR>" +
						"Nedan finns information om vart du kan v�nda dig f�r r�d och st�d." +
						"<BR><BR>" +
						"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:16px;\">St�d fr�n Alkohollinjen</B>" +
						"<BR><BR>" +
						"Alkohollinjen erbjuder st�d per telefon." +
						"<BR><BR>" +
						"Funderar du �ver dina alkoholvanor? Ring oss s� f�r du prata med v�ra behandlare. Om du beh�ver hj�lp f�r att f�r�ndra dina vanor kan vi ge dig st�d. Det �r kostnadsfritt." +
						"<BR><BR>" +
						"De som v�nder sig till Alkohollinjen kan v�lja mellan tv� alternativ:" +
						"<BR>" +
						"- Att antingen ringa f�r r�dgivning n�r du sj�lv tycker att du beh�ver det, eller" +
						"<BR>" +
						"- Att vid ett antal tillf�llen bli uppringd f�r r�dgivning och uppf�ljning." +
						"<BR><BR>" +
						"Telefon 020-84 44 48" +
						"<BR>" +
						"Mejladress: <A HREF=\"mailto:info@alkohollinjen.se\">info@alkohollinjen.se</A>" +
						"<BR><BR>" +
						"Vilka svarar p� Alkohollinjen?" +
						"<BR>" +
						"Bland r�dgivarna finns allm�nl�kare, sjuksk�terskor, mentalsk�tare, psykologer, h�lsopedagoger och folkh�lsovetare." +
						"<BR><BR>" +
						"Alkohollinjens �ppettider kommer inledningsvis att vara m�ndag-torsdag kl 16:00-19:00." +
						"</DIV>" +
						"<BR>" +
						"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:16px;\">St�d fr�n Previa</B>" +
						"<BR><BR>" +
						"Kontakta f�retagssk�terskan f�r r�dgivning i alkohol- och drogfr�gor." +
						"<BR><BR>" +
						"F�r dig som arbetar p� Karolinska, Solna:" +
						"<BR>" +
						"<B>Previa Haga</B>" +
						"<BR>" +
						"Tfn tid: M�n-fre 09.00-12.00 samt 15.00-16.00 f�r r�dgivning och tidbokning 08-728 51 55" +
						"<BR>" +
						"�ppet:	M�n-fre 08.00-16.45" +
						"<BR>" +
						"Adress: G�vlegatan 22, plan 6, 113 30 Stockholm" +
						"<BR>" +
						"Tfn vxl: 08-728 51 40" +
						"<BR><BR>" +
						"F�r dig som arbetar p� Karolinska, Huddinge:" +
						"<BR>" +
						"<B>Previa Huddinge</B>" +
						"<BR>" +
						"Tfn tid: M�n-fre 09.00-12.00 f�r r�dgivning och tidbokning 08-608 63 90" +
						"<BR>" +
						"�ppet: M�n-fre 08.00-16.45" +
						"<BR>" +
						"Adress: Patron Pehrs v�g 4, 141 35 Huddinge" +
						"<BR>" +
						"Tfn vxl: 08-608 63 80" +
						"<BR><BR>" +
						"F�r dig som arbetar p� S�dersjukhuset och n�rbel�gen v�rdcentral:" +
						"<BR>" +
						"<B>Previa Gullmarsplan</B>" +
						"<BR>" +
						"Tfn tid: M�n-fre 09.00-11.00 samt 14.00-16.00 f�r r�dgivning och tidbokning 08-722 24 01" +
						"<BR>" +
						"�ppet: M�n-fre 08.00-16.45" +
						"<BR>" +
						"Adress: Gullmarsplan 13, 121 40 Johanneshov" +
						"<BR>" +
						"Tfn vxl: 08 - 722 24 00" +
						"</DIV>");
				}
				#endregion

				#region Utbr�ndhet
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
							"Dina svar talar f�r att du kan ha symtom p� depression. Nuf�rtiden kan depressioner behandlas med mycket gott resultat. Om du inte redan har en l�karkontakt, f�resl�r vi att du tar kontakt med din husl�kare. �r dina problem arbetsrelaterade kan du �ven kontakta Previa, f�retagsh�lsov�rden." +
							"<BR><BR>");
					}
					else if(Qpbs)
					{
						FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">Utbr�ndhet</B>" +
							"<BR>" +
							"Dina svar visar att du har symtom som tyder p� en h�g stressniv�. L�ngvarig stress utan tillr�cklig �terh�mtning kan leda till utbr�ndhet eller utmattningssyndrom. " +
							"Risken f�r l�ngvariga besv�r �kar p�tagligt om man har s� h�ga krav p� sig sj�lv att man inte tar sig tid f�r att vila och �terh�mta sig. " +
							"Det finns bra bel�gg f�r att en utbr�nningsprocess kan stoppas upp. Man kan ha hj�lp av olika metoder, t ex kollegiala samtalsgrupper och mindfulnesstr�ning. " +
							"B�da dessa alternativ kommer att finnas tillg�ngliga p� din arbetsplats. �r dina problem arbetsrelaterade kan du �ven kontakta Previa, f�retagsh�lsov�rden." +
							"<BR><BR>");
					}
					else
					{
						FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">Utbr�ndhet</B>" +
							"<BR>" +
							"Dina svar visar att du har symtom som tyder p� en h�g stressniv�. L�ngvarig stress utan tillr�cklig �terh�mtning kan leda till utbr�ndhet eller utmattningssyndrom. " +
							"Det finns bra bel�gg f�r att en utbr�nningsprocess kan stoppas upp. Man kan ha hj�lp av olika metoder, t ex kollegiala samtalsgrupper och mindfulnesstr�ning. " +
							"B�da dessa alternativ kommer att finnas tillg�ngliga p� din arbetsplats. �r dina problem arbetsrelaterade kan du �ven kontakta Previa, f�retagsh�lsov�rden." +
							"<BR><BR>");
					}

					#region Mindfulness
					FB.Append("<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:16px;\">Mindfulnessmeditation</B>" +
						"<BR><BR>" +
						"Mindfulness kan beskrivas som ett f�rh�llningss�tt. Ett s�tt att vara, lyssna och relatera. Mindfulness �r ett observerande, icke-v�rderande och accepterande av det som h�nder i nuet. Ist�llet f�r att ryckas med av t.ex. sm�rtf�rnimmelser, oro och katastroftankar l�r man sig att f�rh�lla sig som en v�lvillig men opartisk observat�r av sin egen mentala aktivitet. Detta f�rh�llningss�tt underl�ttas av meditation d�r man �var sin f�rm�ga att rikta och beh�lla uppm�rksamheten. Mindfulnessmeditationen utf�rs i olika kroppspositioner; sittande, st�ende, liggande och n�r kroppen �r i r�relse s�v�l som i stillhet." +
						"<BR><BR>" +
						"Mindfulnessmeditationen har visat sig ha en stressreducerade effekt, vilket antyder att den skulle kunna vara anv�ndbar ocks� f�r att f�rebygga stress och stressrelaterade sjukdomstillst�nd." +
						"<BR><BR>" +
						"Kursprogrammet �r upplagt i enlighet med MBSR-programmet (Mindfulness Based Stress Reduction) som det utarbetats av pionj�ren Jon Kabat-Zinn i USA. Inledningsvis h�lls ett orienteringsseminarium om kursens inneh�ll och bakgrund. Tyngdpunkten i kursen ligger p� praktiska meditations�vningar under �tta grupptillf�llen. Du f�rv�ntas att delta praktiskt (efter egen f�rm�ga) i alla formella mindfulness�vningar och reflektera i grupp kring dina erfarenheter av �vningarna. Var och en f�r dessutom en reflektionsdagbok." +
						"<BR><BR>" +
						"MBSR-programmet inkluderar en tyst dag (7-8 timmar) som �r f�rlagd till l�rdagen eller s�ndagen (dag ej best�mt) efter seminarietillf�lle sex. Detta tillf�lle f�r du g� p� din fritid. Under denna dag h�lls l�rarledda meditations�vningar." +
						"<BR><BR>" +
						"Du f�rv�ntas att utf�ra dagliga mindfulness�vningar i hemmet under ca 30 min 6-7 dagar/vecka. Vi vill passa p� att g�ra dig uppm�rksam p� att kursen kr�ver motivation och beslutsamhet. Det kr�vs att du g�r meditations�vningarna oavsett om det tar emot. Det kan paradoxalt nog upplevas stressande att ta del av en stressreducerande kurs. Om du redan p� f�rhand vet att du inte kommer att kunna avs�tta tid f�r de dagliga praktiska hem�vningarna, s� �r kanske denna kurs inte r�tt f�r dig just nu." +
						"<BR><BR>" +
						"<B>Tider f�r mindfulnessgrupper</B>" +
						"<BR><BR>" +
						"Solna: Torsdagar, kl 13:00-15:00, Gympasalen, Norrbacka. Start vecka 40." +
						"<BR>" +
						"Huddinge: Fredagar, kl 13:00-15:00. Gr�nasalen, personalgymmet, Novumhuset." +
						"<BR><BR>" +
						"Helgen, vecka 47 � kommer vi att tr�ffas en utav dagarna (l�rdag eller s�ndag �r inte best�mt �nnu) och ha en tyst dag f�r b�da grupperna gemensamt (7-8 tim), prelimin�rt i Solna." +
						"<BR><BR>" +
						"<B>Fr�gor och anm�lan</B>" +
						"<BR>" +
						"�r du intresserad av att deltaga i en mindfulnessgrupp, kan du v�lja mellan att g� den i Huddinge eller i Solna. Efter f�rankring hos arbetsledare, sker en av timmarna p� betald arbetstid, den andra timmen f�r du ta av din fritid. �ven helgdagen vecka 47 f�r du ta av din egen tid. Grupperna startar vecka 40. Kostnad f�r material (2 CD och 1 DVD) tillkommer med 290 kr. Deltagandet i gruppen kommer sedan att utv�rderas avseende effekten p� upplevd arbetsrelaterad stress och h�lsa. " +
						"<BR><BR>" +
						"Eventuella fr�gor besvaras av kursledaren Camilla Sk�ld, Med dr, leg sjukgymnast via e-post till <A HREF=\"mailto:camilla.skold@telia.com\">camilla.skold@telia.com</A>." +
						"<BR><BR>" +
						"<A HREF=\"mailto:mariette.veideskog@karolinska.se?Subject=Anm�lan om intresse f�r Mindfulnessmeditation&Body=Namn:%0D%0ATelefonnummer hem:%0D%0APlats: (ange Huddinge eller Solna)\">Klicka h�r</A> f�r att skicka en anm�lan via e-post till <A HREF=\"mailto:mariette.veideskog@karolinska.se?Subject=Anm�lan om intresse f�r Mindfulnessmeditation&Body=Namn:%0D%0ATelefonnummer hem:%0D%0APlats: (ange Huddinge eller Solna)\">mariette.veideskog@karolinska.se</A>. I din anm�lan vill vi att du uppger om du vill g� i Huddinge eller i Solna, samt telefonnummer hem. Sista anm�lningsdag �r den 24/9." +
						"</DIV>" +
						"<BR>");
					#endregion

					FB.Append("<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:16px;\">Erbjudande om att delta i Kollegiala Samtalsgrupper</B>" +
						"<BR><BR>" +
						"Ditt resultat av enk�ten pekar p� att du ligger p� en h�g stressniv� just nu. Upplevd stress hanteras p� m�nga olika s�tt av olika individer och det �r inte alls s�kert att det beh�ver inneb�ra n�got negativt, men det kan ocks� tolkas som signaler p� att n�got b�r g�ras �t det." +
						"<BR>" +
						"Det finns inte <B><U>en</U></B> enkel l�sning p� problemet med stress och f�rebyggande �tg�rder m�ste d�rf�r ocks� ske inom m�nga olika omr�den. Det kr�vs insatser s�v�l inom organisationen, p� arbetsplatsen, i samh�llet i stort, som f�r den enskilde individen. Du som individ ges nu m�jligheten att delta i;" +
						"<BR><BR>" +
						"<B>Reflekterande, kollegiala samtalsgrupper</B>" +
						"<BR>" +
						"Reflekterande kollegiala samtalsgrupper sker utifr�n en pedagogisk metod  och �r ett s�tt att f�rebygga utveckling av arbetsrelaterad stress och utmattning. I tidigare studier med liknande grupper har man konstaterat att kollegialt st�d, tid f�r reflektion och erfarenhetsutbyte mellan kollegor visat sig vara mycket v�rdefullt f�r deltagarna." +
						"<BR>" +
						"Kollegor fr�n olika arbetsplatser har ofta gemensamma yrkesrelaterade problem och erfarenheter, men samtidigt olika infallsvinklar och l�sningar p� dessa." +
						"<BR><BR>" +
						"<B>Genomf�rande</B>" +
						"<BR>" +
						"Vi kommer att skapa grupper med 8 personer ur samma eller liknande yrkesgrupp och leds av en handledare. Man tr�ffas under 11 veckor, 2 timmar vid varje tillf�lle. Efter f�rankring hos arbetsledare, sker en av timmarna p� betald arbetstid, den andra timmen f�r du ta av din fritid." +
						"<BR><BR>" +
						"<B>Intresseanm�lan och f�rfr�gningar</B>" +
						"<BR>" +
						"<A HREF=\"mailto:mariette.veideskog@karolinska.se?Subject=Anm�lan om intresse f�r Reflekterande, kollegiala samtalsgrupper&Body=Namn:%0D%0AKontaktinformation:%0D%0AYrkeskategori:%0D%0APlats: (ange Huddinge eller Solna)\">Klicka h�r</A> f�r att skicka en intresseanm�lan (och eventuella fr�gor) via e-post till <A HREF=\"mailto:mariette.veideskog@karolinska.se?Subject=Anm�lan om intresse f�r Reflekterande, kollegiala samtalsgrupper&Body=Namn:%0D%0AKontaktinformation:%0D%0AYrkeskategori:%0D%0APlats: (ange Huddinge eller Solna)\">mariette.veideskog@karolinska.se</A>. I din anm�lan vill vi att du uppger om du vill g� i Solna eller i Huddine, samt yrkeskategori." +
						"</DIV>" +
						"<BR>" +

						"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:16px;\">Enskilda samtal</B>" +
						"<BR><BR>" +
						"Vi som arbetar i Sjukhuskyrkan har l�ng erfarenhet av att m�ta m�nniskor i sorg och kris. Att lyssna och tala om det riktigt sv�ra �r en av v�ra allra viktigaste uppgifter. Syftet med samtalen �r att st�dja och hj�lpa, ge hopp och mod. Samtalen kan ske vid ett eller flera tillf�llen. " +
						"<BR><BR>" +
						"Sjukhuskyrkans enskilda samtal erbjuds alla, oavsett tro och livs�sk�dning. M�nga av dem vi tr�ffar har i vanliga fall ingen anknytning till kyrkan. " +
						"<BR><BR>" +
						"<B>Huddinge</B>:" +
						"<BR>" +
						"Kontakta Leena Wid�n, ank 802 92 f�r bokning av samtal." +
						"<BR><BR>" +
						"<B>Solna</B>:" +
						"<BR>" +
						"Kontakta Hans-Peter Rasmussen, ank 740 73 f�r bokning av samtal." +
						"</DIV>" +
						"<BR>" +

						"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:16px;\">St�dsamtal hos Previa</B>" +
						"<BR><BR>" +
						"�r dina problem arbetsrelaterade kan du kontakta f�retagssk�terskan p� Previa f�r r�dgivning." +
						"<BR><BR>" +
						"F�r dig som arbetar p� Karolinska, Solna:" +
						"<BR>" +
						"<B>Previa Haga</B>" +
						"<BR>" +
						"Tfn tid: M�n-fre 09.00-12.00 samt 15.00-16.00 f�r r�dgivning och tidbokning 08-728 51 55" +
						"<BR>" +
						"�ppet:	M�n-fre 08.00-16.45" +
						"<BR>" +
						"Adress: G�vlegatan 22, plan 6, 113 30 Stockholm" +
						"<BR>" +
						"Tfn vxl: 08-728 51 40" +
						"<BR><BR>" +
						"F�r dig som arbetar p� Karolinska, Huddinge:" +
						"<BR>" +
						"<B>Previa Huddinge</B>" +
						"<BR>" +
						"Tfn tid: M�n-fre 09.00-12.00 f�r r�dgivning och tidbokning 08-608 63 90" +
						"<BR>" +
						"�ppet: M�n-fre 08.00-16.45" +
						"<BR>" +
						"Adress: Patron Pehrs v�g 4, 141 35 Huddinge" +
						"<BR>" +
						"Tfn vxl: 08-608 63 80" +
						"<BR><BR>" +
						"F�r dig som arbetar p� S�dersjukhuset och n�rbel�gen v�rdcentral:" +
						"<BR>" +
						"<B>Previa Gullmarsplan</B>" +
						"<BR>" +
						"Tfn tid: M�n-fre 09.00-11.00 samt 14.00-16.00 f�r r�dgivning och tidbokning 08-722 24 01" +
						"<BR>" +
						"�ppet: M�n-fre 08.00-16.45" +
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
						"Dina svar talar f�r att du kan ha symtom p� depression. Nuf�rtiden kan depressioner behandlas med mycket gott resultat. Om du inte redan har en l�karkontakt, f�resl�r vi att du tar kontakt med din husl�kare. �r dina problem arbetsrelaterade kan du �ven kontakta Previa, f�retagsh�lsov�rden." +
						"<BR><BR>" +
						"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
						"<B STYLE=\"font-size:16px;\">Previa, f�retagsh�lsov�rden</B>" +
						"<BR><BR>" +
						"F�r dig som arbetar p� Karolinska, Solna:" +
						"<BR>" +
						"<B>Previa Haga</B>" +
						"<BR>" +
						"Tfn tid: M�n-fre 09.00-12.00 samt 15.00-16.00 f�r r�dgivning och tidbokning 08-728 51 55" +
						"<BR>" +
						"�ppet:	M�n-fre 08.00-16.45" +
						"<BR>" +
						"Adress: G�vlegatan 22, plan 6, 113 30 Stockholm" +
						"<BR>" +
						"Tfn vxl: 08-728 51 40" +
						"<BR><BR>" +
						"F�r dig som arbetar p� Karolinska, Huddinge:" +
						"<BR>" +
						"<B>Previa Huddinge</B>" +
						"<BR>" +
						"Tfn tid: M�n-fre 09.00-12.00 f�r r�dgivning och tidbokning 08-608 63 90" +
						"<BR>" +
						"�ppet: M�n-fre 08.00-16.45" +
						"<BR>" +
						"Adress: Patron Pehrs v�g 4, 141 35 Huddinge" +
						"<BR>" +
						"Tfn vxl: 08-608 63 80" +
						"<BR><BR>" +
						"F�r dig som arbetar p� S�dersjukhuset och n�rbel�gen v�rdcentral:" +
						"<BR>" +
						"<B>Previa Gullmarsplan</B>" +
						"<BR>" +
						"Tfn tid: M�n-fre 09.00-11.00 samt 14.00-16.00 f�r r�dgivning och tidbokning 08-722 24 01" +
						"<BR>" +
						"�ppet: M�n-fre 08.00-16.45" +
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

					FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">S�mn</B>" +
						"<BR>" +
						"S�mnen �r v�r viktigaste resurs f�r �terh�mtning och �r v�sentlig bland annat f�r att bevara h�lsa, v�lbefinnande och prestationsf�rm�ga. Under s�mnen ins�ndras flera viktiga hormoner som reparerar, l�ker och bygger upp kroppen. Om s�mnen under en l�ngre period �r st�rd l�per man stor risk f�r att p� sikt drabbas av olika stressrelaterade sjukdomar." +
						"<BR><BR>");

					switch(Convert.ToInt32("0" + (string)oldValues["Q374O86"]))
					{
						case 310: FB.Append("Du har angivit att din s�mn �r Bra, vilket tyder p� att din s�mn och �terh�mtning �r god; att du generellt sett somnar l�tt, sover gott och k�nner dig utvilad n�r du vaknar.<BR>"); break;
						case 311: FB.Append("Du har angivit att din s�mn �r Ganska bra, vilket tyder p� att din s�mn och �terh�mtning �r god; att du generellt sett somnar l�tt, sover gott och k�nner dig utvilad n�r du vaknar.<BR>"); break;
						case 312: QsleepI = true; FB.Append("Du har angivit att din s�mn �r varken �r bra eller d�lig. Om du upplever att du �terkommande har sv�rt att somna, sova gott eller inte �r utvilad n�r du vaknar finns det anledning att f�rb�ttra din s�mn f�r att p� sikt bevara h�lsa och v�lbefinnande. Det �r i s�dana fall viktigt att t�nka p� att varva ned minst tv� timmar f�re s�ngg�endet. Man kan dessutom f�rb�ttra s�mnkvaliteten genom att regelbundet genomf�ra andnings- och avslappnings�vningar f�re s�ngg�endet."); break;
						case 313: QsleepI = true; FB.Append("Du har angivit att din s�mn �r Ganska d�lig, vilket generellt sett speglar n�got/n�gra av f�ljande symtom: sv�righeter att somna, d�lig s�mnkvalitet, tr�tthet vid uppvaknande och att man inte k�nner sig utvilad. Dessa symtom signalerar ett tydligt behov av att se �ver s�mnen. Det �r helt normalt att vid enstaka tillf�llen eller under kortare perioder sova d�ligt av olika sk�l, men om du haft s�mnbesv�r en l�ngre tid beh�ver dessa signaler tas p� allvar. Du kan b�rja med att reflektera �ver om det finns uppenbara anledningar till att du sover d�ligt. Om du �r os�ker p� hur du ska hantera dina s�mnproblem rekommenderar vi dig att kontakta din husl�kare. �r dina problem arbetsrelaterade kan du �ven kontakta Previa, f�retagsh�lsov�rden."); break;
						case 314: QsleepI = true; FB.Append("Du har angivit att din s�mn �r D�lig, vilket generellt sett speglar n�got/n�gra av f�ljande symtom: sv�righeter att somna, d�lig s�mnkvalitet, tr�tthet vid uppvaknande och att man inte k�nner sig utvilad. Dessa symtom signalerar ett tydligt behov av att se �ver s�mnen. Det �r helt normalt att vid enstaka tillf�llen eller under kortare perioder sova d�ligt av olika sk�l, men om du haft s�mnbesv�r en l�ngre tid beh�ver dessa signaler tas p� allvar. Du kan b�rja med att reflektera �ver om det finns uppenbara anledningar till att du sover d�ligt. Om du �r os�ker p� hur du ska hantera dina s�mnproblem rekommenderar vi dig att kontakta din husl�kare. �r dina problem arbetsrelaterade kan du �ven kontakta Previa, f�retagsh�lsov�rden."); break;
					}
					if(QsleepI)
					{
						FB.Append("<BR><BR>" +
							"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
							"<B STYLE=\"font-size:16px;\">&quot;Hur sover du egentligen?&quot;</B>" +
							"<BR><BR>" +
							"Tillh�r du den kategorin som r�knar f�r p� natten s� ska du komma p� denna f�rel�sning. Vad �r s�mn egentligen? N�r ska man ta tupplurar?  �r det bra att r�kna f�r? Du kommer f� kunskap om hur v�r s�mncykel fungerar och f� tips och id�er p� vad du sj�lv kan g�ra f�r att f�rb�ttra din s�mn." +
							"<BR><BR>" +
							"<B>Huddinge</B>:" +
							"<BR>" +
							"Torsdagen den 11/10, kl 14:30-16:00, R 64." +
							"<BR><BR>" +
							"<B>Solna</B>:" +
							"<BR>" +
							"M�ndagen den 8/10, kl 14:30-16:00, Skandiasalen, Astrid Lindgren." +
							"<BR><BR>" +
							"<B>F�rel�sare</B>: Mariette Weideskog, H�lsopedagog, Sektionen f�r arbetsmilj� och h�lsa, HR-avdelningen " +
							"<BR><BR>" +
							"Du beh�ver inte anm�la dig till f�rel�sningen, det �r bara att komma. Du f�r g� p� arbetstid och f�ruts�tter medgivande fr�n din n�rmsta chef." +
							"</DIV>");
					}
				}
				#endregion

				#region Health
				if(oldValues.Contains("Q331O98"))
				{
					FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">H�lsa</B>" +
						"<BR>" +
						"Det �r numer v�letablerat att sj�lvskattad h�lsa �r en viktig indikator f�r hur man kommer att m� i framtiden. Om man bed�mer sin h�lsa som ganska bra eller bra har man betydligt st�rre chans att bevara/f�rb�ttra sin h�lsa i framtiden j�mf�rt med dem som skattat den egna h�lsan som d�lig." +
						"<BR><BR>");

					switch(Convert.ToInt32("0" + (string)oldValues["Q331O98"]))
					{
						case 310: FB.Append("Du har angivit att din h�lsa �r Bra och verkar s�ledes ha en h�lsosam livsstil och/eller m� ganska bra. Personer med din skattning har st�rst sannolikhet att bevara eller �ka sin h�lsa i framtiden."); break;
						case 311: FB.Append("Du har angivit att din h�lsa �r Ganska bra och verkar s�ledes ha en h�lsosam livsstil och/eller m� ganska bra. Personer med din skattning har st�rst sannolikhet att bevara eller �ka sin h�lsa i framtiden."); break;
						case 315: FB.Append("Du har angivit att din h�lsa varken �r bra eller d�lig, vilket betyder att det kan finnas en risk f�r att din h�lsa f�rs�mras ytterligare i framtiden. Eftersom sj�lvskattad h�lsa �r ett sammanfattande m�tt p� b�de fysiskt och mentalt v�lbefinnande rekommenderar vi att du ser �ver din livsstil, till exempel vad g�ller motion, s�mn, och matvanor."); break;
						case 316: FB.Append("Du har angivit att din h�lsa �r Ganska d�lig. Om det inte �r fr�ga om en tillf�llig nedg�ng i hur du m�r, och om du inte redan har l�karkontakt, rekommenderar vi att du kontaktar din husl�kare f�r en diskussion av ditt h�lsotillst�nd och vad som eventuellt kan g�ras f�r att f�rb�ttra det. �r dina problem arbetsrelaterade kan du �ven kontakta f�retagsh�lsov�rden, Previa."); break;
						case 317: FB.Append("Du har angivit att din h�lsa �r D�lig. Om det inte �r fr�ga om en tillf�llig nedg�ng i hur du m�r, och om du inte redan har l�karkontakt, rekommenderar vi att du kontaktar din husl�kare f�r en diskussion av ditt h�lsotillst�nd och vad som eventuellt kan g�ras f�r att f�rb�ttra det. �r dina problem arbetsrelaterade kan du �ven kontakta f�retagsh�lsov�rden, Previa."); break;
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
						FB.Append("Dina sammanv�gda po�ng av livsstil och �rftliga faktorer antyder att du l�per mycket stor risk att drabbas av typ-2 diabetes. Uppskattningsvis 50% insjuknar. Regelbunden och m�ttlig motion samt god kosth�llning reducerar risken att insjukna i diabetes. Vi f�resl�r d�rf�r att du deltar i n�got av eller b�da alternativen nedan samt kontaktar din husl�kare f�r en noggrannare bed�mning av blodsocker (b�de fasteglukos och glukosbelastning alternativt blodsockret efter en m�ltid). Det visar om du eventuellt har symtomfri diabetes.");
					}
					else if(Qscore >= 15)
					{
						FB.Append("Dina sammanv�gda po�ng av livsstil och �rftliga faktorer antyder att du l�per stor risk att drabbas av typ-2 diabetes. Uppskattningsvis 33% insjuknar. Regelbunden och m�ttlig motion samt god kosth�llning reducerar risken att insjukna i diabetes. Vi f�resl�r d�rf�r att du deltar i n�got av eller b�da alternativen nedan samt kontaktar din husl�kare f�r en noggrannare bed�mning av risken.");
					}
					else if(Qscore >= 12)
					{
						FB.Append("Dina sammanv�gda po�ng av livsstil och �rftliga faktorer antyder att du har en m�ttlig risk att drabbas av typ-2 diabetes. Uppskattningsvis 17% insjuknar. Regelbunden och m�ttlig motion samt god kosth�llning reducerar risken att insjukna i diabetes.");
					}
					else if(Qscore >= 7)
					{
						FB.Append("Dina sammanv�gda po�ng av livsstil och �rftliga faktorer antyder att du har en n�got f�rh�jd risk att drabbas av typ-2 diabetes. Uppskattningsvis 4% insjuknar. Regelbunden och m�ttlig motion samt god kosth�llning reducerar risken att insjukna i diabetes.");
					}
					else
					{
						FB.Append("Dina sammanv�gda po�ng av livsstil och �rftliga faktorer antyder att du inte l�per s�rskilt stor risk att drabbas av typ-2 diabetes. Uppskattningsvis 1% insjuknar. Regelbunden och m�ttlig motion samt god kosth�llning reducerar risken att insjukna i diabetes.");
					}
					FB.Append("<BR><BR>" +
						"Typ 2-diabetes (vuxendiabetes, �ldersdiabetes) �r en allvarlig och �rftlig sjukdom. Men man kan i h�g grad p�verka diabetesrisken genom livsstilen. �vervikt, s�rskilt bukfetma, fysisk inaktivitet, d�liga matvanor och r�kning �kar risken att f� typ 2-diabetes." +
						"<BR><BR>" +
						"B�de h�gre �lder och �rftliga faktorer kan �ka risken f�r typ 2-diabetes och dessa faktorer kan man inte p�verka. D�remot kan man p�verka de �vriga riskfaktorerna s�som till exempel �vervikt, bukfetma, fysisk inaktivitet, matvanor och r�kning. Genom livsstilen kan man helt och h�llet f�rhindra eller �tminstone skjuta upp typ 2-diabetes s� l�ngt fram i tiden som m�jligt. Om man har diabetes i sl�kten ska man vara extra noga med att h�lla vikten med �ren. Fett runt midjan, s� kallad bukfetma, utg�r en extra stor risk. Det kan ta tid innan man m�rker av symtom fr�n typ 2-diabetes.");

					if(Qscore >= 12)
					{
						FB.Append("<BR><BR>" +
							"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
							"<B STYLE=\"font-size:16px;\">Individuell tr�ningsr�dgivning</B>" +
							"<BR><BR>" +
							"Som anst�lld p� Karolinska erbjuds du hj�lp med individuell tr�ningsr�dgivning.  Det kan vara att f� en styrketr�nings introduktion i v�rt personalgym, hj�lp med r�rlighets- eller konditionstr�ning, stretch�vningar m.m." +
							"<BR><BR>" +
							"F�r bokning av tr�ningsr�dgivning i <B>Solna</B>" +
							"<BR>" +
							"Tomas Hernvall, Friskv�rdskonsulent/Gymf�rest�ndare, Sektionen f�r arbetsmilj� och h�lsa, HR-avdelningen,  ank 757 10.  " +
							"<BR><BR>" +
							"F�r bokning av tr�ningsr�dgivning i <B>Huddinge</B>" +
							"<BR>" +
							"Marie Hammarstr�m, friskv�rdskonsulent/Gymf�rest�ndare, Sektionen f�r arbetsmilj� och h�lsa, HR-avdelningen, ank 868 42." +
							"<BR><BR>" +
							"Du f�r g� p� tr�ningsr�dgivningen p� arbetstid och f�ruts�tter medgivande av din n�rmsta chef." +
							"</DIV>" +
							"<BR>" +
							"<DIV STYLE=\"background-color:#FFFFFF;padding:15px;border:1px solid #888888;\">" +
							"<B STYLE=\"font-size:16px;\">&quot;Kost, R�relse, H�lsa&quot;</B>" +
							"<BR><BR>" +
							"Vill du l�ra dig mer om kost och motion? Hur p�verkar kosten och r�relsen ditt v�lbefinnande och din h�lsa? B�rjar favoritbyxorna sitta lite tight i midjan?  Nu har du chansen att p� ett bra s�tt hitta en h�lsosam vikt och f� bra vanor genom att l�ra dig mer om kostens och r�relsens betydelse." +
							"<BR><BR>" +
							"Kursen best�r av 9 grupptr�ffar a� 1 timme. P� tr�ffarna kommer vi bland annat att diskutera v�ra mat- och motionsvanor och beteenden. Inom kosten kommer vi bland annat att prata om v�ra energik�llor, m�ltidssammans�ttning, fettk�llor samt kosttrender. Du kommer inte att f�lja n�gon diet, utan tanken �r att du ska l�ra dig s� mycket om kost s� att du kan s�tta samman en bra m�ltid sj�lv. Fysisk aktivitet och r�relse �r en viktig grundpelare i en h�lsosam livsstil och en hj�lp till att f� energibalans och d�rmed en h�lsosam vikt. Vi kommer bland annat att prata om positiva effekter av fysisk aktivitet och praktisera detta p� ett enklare s�tt. Hur lite kan du &quot;komma undan med&quot;?" +
							"<BR><BR>" +
							"Vid varje kurstillf�lle har samtliga m�jlighet att v�ga sig. Kursinneh�ll delas ut vid f�rsta tillf�llet." +
							"<BR><BR>" +
							"<B>Datum, Tid & Lokal</B>" +
							"<BR><BR>" +
							"<B>Solna</B>" +
							"<BR>" +
							"Tisdagar kl 11:00-12:00, med start vecka 40. Kursen kommer att h�llas i Eugeniahemmet, f�rsta kurstillf�llet i Aga-salen." +
							"<BR><BR>" +
							"<B>Huddinge</B>" +
							"<BR>" +
							"Tisdagar, kl 15:00-16:00, med start vecka 40. Kursen kommer att h�llas i konferensrummet i personalgymmet, Novumhuset." +
							"<BR><BR>" +
							"<B>Kursledare</B>" +
							"<BR>" +
							"Charlotte Ovefelt, H�lsopedagog" +
							"<BR><BR>" +
							"<B>Kostnad</B>" +
							"<BR>" +
							"Kursen �r avgiftsfri och sker p� arbetstid och f�ruts�tter medgivande fr�n din n�rmsta chef." +
							"<BR><BR>" +
							"<B>Anm�lan</B>" +
							"<BR>" +
							"Senast fredag 24/9. <A HREF=\"mailto:mariette.veideskog@karolinska.se?Subject=Anm�lan till Kost, R�relse, H�lsa&Body=Namn:%0D%0AKontaktinformation:%0A%0APlats: (ange Huddinge eller Solna)\">Klicka h�r</A> f�r att skicka anm�lan via e-post till <A HREF=\"mailto:mariette.veideskog@karolinska.se?Subject=Anm�lan till Kost, R�relse, H�lsa&Body=Namn:%0D%0AKontaktinformation:%0A%0APlats: (ange Huddinge eller Solna)\">mariette.veideskog@karolinska.se</A>. N�r du skickar din anm�lan via e-post vill vi att du skriver kursens namn som �rende samt om du vill g� i Solna eller Huddinge." +
							"<BR><BR>" +
							"Har ni fr�gor om kursverksamheten kan ni ringa till n�gon av h�lsopedagogerna Charlotte Ovefelt 585 894 82 eller Mariette Veideskog, 585 868 43." +
							"</DIV>");
					}
				}
				#endregion

				if(!Qalco && !Qdepr && !Qburnout && Qscore < 12)
				{
					FB.Append("<BR><BR><B STYLE=\"font-size:16px;\">Friskv�rd</B><BR>Du �r v�lkommen att delta i friskv�rdens vanliga utbud. Klicka p� l�nken f�r mer information:<BR><A HREF=\"http://inuti.karolinskauniversitetssjukhuset.se/templates/Page____8345.aspx\" TARGET=\"_blank\">http://inuti.karolinskauniversitetssjukhuset.se/templates/Page____8345.aspx</A>");
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
