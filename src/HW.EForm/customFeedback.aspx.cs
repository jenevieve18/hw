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
using WebSupergoo.ABCpdf6;

namespace eform
{
	/// <summary>
	/// Summary description for customFeedback.
	/// </summary>
	public class customFeedback : System.Web.UI.Page
	{
		protected PlaceHolder feedback;
		protected Button Save;

		private void createPDF(string content, int usernr, int userID, bool redirect)
		{
			Doc theDoc = new Doc();
			theDoc.HtmlOptions.PageCacheEnabled = false;
			theDoc.HtmlOptions.Timeout = 600000;
			theDoc.MediaBox.String = "A4";
			theDoc.Rect.String = "A4";

			double w = theDoc.MediaBox.Width;
			double h = theDoc.MediaBox.Height;
			double l = theDoc.MediaBox.Left;
			double b = theDoc.MediaBox.Bottom; 
			//					theDoc.Transform.Rotate(90, l, b);
			//					theDoc.Transform.Translate(w, 0); 
			//					theDoc.Rect.Width = h;
			//					theDoc.Rect.Height = w;

			theDoc.Rect.Inset(0,50);

			theDoc.Page = theDoc.AddPage();
			int obj = theDoc.AddImageHtml(content,true,850,true);
			while(theDoc.Chainable(obj)) 
			{
				theDoc.Page = theDoc.AddPage();
				obj = theDoc.AddImageToChain(obj);
			}

			//theDoc.HPos = 0.5;
			//theDoc.VPos = 0.5;
			//theDoc.Font = theDoc.AddFont("Helvetica");
			//theDoc.FontSize = 9;

			theDoc.Rect.String = "0 10 595 50";
			for (int i = 1; i <= theDoc.PageCount; i++)
			{
				theDoc.PageNumber = i;
				theDoc.AddImageHtml("<html><body style=\"width:100%;text-align:center;font-size:14px;font-family:Tahoma, Arial, Helvetica, sans-serif;\">Sidan " + i + " av " + theDoc.PageCount + " [ID#" + usernr + "]</body></html>");
			}

			for (int i = 1; i <= theDoc.PageCount; i++) 
			{
				theDoc.PageNumber = i;
				theDoc.Flatten();
			}

			//					int theID = theDoc.GetInfoInt(theDoc.Root, "Pages");
			//					theDoc.SetInfo(theID, "/Rotate", "90");

			//					byte[] output = theDoc.GetData();

			theDoc.Save(Server.MapPath("report") + "\\" + userID + ".pdf");
			theDoc.Clear();
			theDoc.Dispose();
			theDoc = null;

			//					HttpContext.Current.Response.Clear();
			//					HttpContext.Current.Response.ClearHeaders();
			//					HttpContext.Current.Response.ContentType = "application/pdf";
			//					HttpContext.Current.Response.AddHeader("content-length", output.Length.ToString());
			//					HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=feedback.PDF");
			//					HttpContext.Current.Response.BinaryWrite(output);
			//					HttpContext.Current.Response.End();

			if(redirect)
			{
				HttpContext.Current.Response.Redirect("report/" + userID + ".pdf?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "",true);
			}
		}
		private void generateReport(int userID, int usernr)
		{
			#region Generate report
			int crid = 0;
			Db.execute("UPDATE CustomReport SET UserID = -" + userID + " WHERE UserID = " + userID);
			Db.execute("INSERT INTO CustomReport (UserID) VALUES (" + userID + ")");
			OdbcDataReader rs = Db.recordSet("SELECT CustomReportID FROM CustomReport WHERE UserID = " + userID);
			if(rs.Read())
			{
				crid = rs.GetInt32(0);
			}
			rs.Close();

			string pruid = "1183,1614,1694,1182,1531,1573,1693";
					
			#region Fetch values
			System.Collections.Hashtable oldValues = new System.Collections.Hashtable();
			OdbcDataReader rs2 = Db.recordSet("SELECT " +
				"a.ValueInt, " +
				"a.ValueText, " +
				"a.ValueDecimal, " +
				"a.QuestionID, " +
				"a.OptionID, " +
				"o.OptionType " +
				"FROM AnswerValue a " +
				"INNER JOIN [Option] o ON a.OptionID = o.OptionID " +
				"INNER JOIN Answer ans ON a.AnswerID = ans.AnswerID " +
				"INNER JOIN UserProjectRoundUser upru ON upru.ProjectRoundUserID = ans.ProjectRoundUserID " +
				"WHERE ans.ProjectRoundUnitID IN (" + pruid + ") " +
				"AND upru.UserID = " + userID + " " +
				"AND a.DeletedSessionID IS NULL " +
				"ORDER BY a.AnswerID DESC");
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

				if(!oldValues.Contains("Q" + rs2.GetInt32(3) + "O" + rs2.GetInt32(4)) && val != "")
				{
					oldValues.Add("Q" + rs2.GetInt32(3) + "O" + rs2.GetInt32(4), HttpUtility.HtmlEncode(val));
				}
			}
			rs2.Close();
			#endregion

			string tmp = "";

			#region webbqps
			crr("<B STYLE=\"font-size:20px;\">","Återkoppling","</B>",crid,100,10);

			crr("<BR><BR><B STYLE=\"font-size:16px;\">","Sammanvägd bedömning","</B><BR>",crid,100,10);
			crr("","Sammanfattningsvis tyder dina enkätsvar och våra mätningar på att du har en god hälsa. Det kan vara så att du rapporterat någon form av hälsoproblem (så som beskrivs nedan), eller uppvisat någon avvikelse i de biologiska mätnigarna (d.v.s. EKG, hormonmätningar, hörseltester och Body Mass Index). Om du önskar få en personlig genomgång av dina resultat är du välkommen att höra av dig till Martin Benka Wallén på telefon 08-553 789 24 eller e-post Martin.Benka-Wallen@ki.se. Du kan givetvis också ta kontakt med din husläkare eller företagsläkare."," ",crid,100,100);

			tmp = "";
			#region Rygg/nacke (disabled)
			//					bool Qback = 
			//						oldValues.Contains("Q337O90") && Convert.ToInt32("0" + (string)oldValues["Q337O90"]) == 294 
			//						&& 
			//						(
			//						oldValues.Contains("Q341O90") && Convert.ToInt32("0" + (string)oldValues["Q341O90"]) == 294 
			//						|| 
			//						oldValues.Contains("Q342O90") && Convert.ToInt32("0" + (string)oldValues["Q342O90"]) == 294 
			//						|| 
			//						oldValues.Contains("Q343O90") && Convert.ToInt32("0" + (string)oldValues["Q343O90"]) == 294
			//						);
			//					bool Qneck = 
			//						oldValues.Contains("Q339O90") && Convert.ToInt32("0" + (string)oldValues["Q339O90"]) == 294 
			//						&& 
			//						(
			//						oldValues.Contains("Q346O90") && Convert.ToInt32("0" + (string)oldValues["Q346O90"]) == 294 
			//						|| 
			//						oldValues.Contains("Q347O90") && Convert.ToInt32("0" + (string)oldValues["Q347O90"]) == 294 
			//						|| 
			//						oldValues.Contains("Q348O90") && Convert.ToInt32("0" + (string)oldValues["Q348O90"]) == 294
			//						);
			//					bool QbnSleep = 
			//						oldValues.Contains("Q337O90") && Convert.ToInt32("0" + (string)oldValues["Q337O90"]) == 294 
			//						&& 
			//						oldValues.Contains("Q341O90") && Convert.ToInt32("0" + (string)oldValues["Q341O90"]) == 294 
			//						|| 
			//						oldValues.Contains("Q339O90") && Convert.ToInt32("0" + (string)oldValues["Q339O90"]) == 294 
			//						&& 
			//						oldValues.Contains("Q346O90") && Convert.ToInt32("0" + (string)oldValues["Q346O90"]) == 294;
			//					bool QbnWork = 
			//						oldValues.Contains("Q337O90") && Convert.ToInt32("0" + (string)oldValues["Q337O90"]) == 294 
			//						&& 
			//						oldValues.Contains("Q342O90") && Convert.ToInt32("0" + (string)oldValues["Q342O90"]) == 294 
			//						|| 
			//						oldValues.Contains("Q339O90") && Convert.ToInt32("0" + (string)oldValues["Q339O90"]) == 294 
			//						&& 
			//						oldValues.Contains("Q347O90") && Convert.ToInt32("0" + (string)oldValues["Q347O90"]) == 294;
			//					bool QbnWorse = 
			//						oldValues.Contains("Q337O90") && Convert.ToInt32("0" + (string)oldValues["Q337O90"]) == 294 
			//						&& 
			//						oldValues.Contains("Q343O90") && Convert.ToInt32("0" + (string)oldValues["Q343O90"]) == 294 
			//						|| 
			//						oldValues.Contains("Q339O90") && Convert.ToInt32("0" + (string)oldValues["Q339O90"]) == 294 
			//						&& 
			//						oldValues.Contains("Q348O90") && Convert.ToInt32("0" + (string)oldValues["Q348O90"]) == 294;
			//					if(Qback || Qneck)
			//					{
			//						if(Qback)
			//						{
			//							tmp += "Rygg";
			//							if(Qneck)
			//							{
			//								tmp += " och ";
			//							}
			//						}
			//						if(Qneck)
			//						{
			//							tmp += "Nacke";
			//						}
			//						crr("<BR><BR><B STYLE=\"font-size:16px;\">",tmp,"</B><BR>",crid);
			//					
			//						tmp = "Du har angivit att du har besvär från ";
			//						if(Qback)
			//						{
			//							tmp += "ryggen";
			//							if(Qneck)
			//							{
			//								tmp += " och ";
			//							}
			//						}
			//						if(Qneck)
			//						{
			//							tmp += "nacken";
			//						}
			//						tmp += " samt att dessa besvär ";
			//						if(QbnWorse)
			//						{
			//							tmp += "förvärras av arbetet";
			//							if(QbnSleep || QbnWork)
			//							{
			//								tmp += " och att besvären ";
			//							}
			//						}
			//						if(QbnSleep || QbnWork)
			//						{
			//							tmp += "varit så pass svåra att du haft svårt ";
			//							if(QbnSleep)
			//							{
			//								tmp += "att sova";
			//								if(QbnWork)
			//								{
			//									tmp += " och ";
			//								}
			//							}
			//							if(QbnWork)
			//							{
			//								tmp += "utföra ditt arbete";
			//							}
			//						}
			//						tmp += ". Du rekommenderas att kontakta sjukgymnast.";
			//
			//						crr("",tmp,"",crid);
			//					}
			#endregion

			tmp = "";
			#region Smoke/Hosta/Pip
			bool Qhosta = oldValues.Contains("Q349O90") && Convert.ToInt32("0" + (string)oldValues["Q349O90"]) == 294;
			bool Qpip = oldValues.Contains("Q350O90") && Convert.ToInt32("0" + (string)oldValues["Q350O90"]) == 294;
			bool Qsmoke = oldValues.Contains("Q370O90") && Convert.ToInt32("0" + (string)oldValues["Q370O90"]) == 294;

			if(Qsmoke)
			{
				if(Qhosta || Qpip)
				{
					if(Qhosta)
					{
						tmp += "Långvarig hosta";
						if(Qpip)
						{
							tmp += " och ";
						}
					}
					if(Qpip)
					{
						tmp += "Pip i bröstet";
					}
					crr("<BR><BR><B STYLE=\"font-size:16px;\">",tmp,"</B><BR>",crid,100,10);
						
					crr("","Dina svar visar att du har symtom som skulle kunna bero på din rökning. Om du inte redan har sökt läkarhjälp, rekommenderar vi att du gör det.","",crid);
				}
				else
				{
					crr("<BR><BR><B STYLE=\"font-size:16px;\">","Rökning","</B><BR>",crid,100,10);
					crr("","Som du säkert vet, är rökning riskabelt för hälsan. Om du vill hjälp med att sluta röka rekommenderas du att kontakta din husläkare.","",crid);
				}
			}
			else if(Qpip || Qhosta)
			{
				if(Qhosta)
				{
					tmp += "Långvarig hosta";
					if(Qpip)
					{
						tmp += " och ";
					}
				}
				if(Qpip)
				{
					tmp += "Pip i bröstet";
				}
				crr("<BR><BR><B STYLE=\"font-size:16px;\">",tmp,"</B><BR>",crid,100,10);
				crr("","Symtomen långvarig hosta och pip i bröstet kan vara symtom på astma. Om du inte du redan har behandling, rekommenderar vi att du söker din husläkare för en bedömning.","",crid);
			}
			#endregion

			tmp = "";
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
				crr("<BR><BR><B STYLE=\"font-size:16px;\">","Alkohol","</B>",crid,100,10);
				crr("","","" +
					"<BR>" +
					"I vår kultur ingår ofta alkohol i den sociala samvaron och kan ha flera positiva effekter. Dina svar talar för att alkoholen medför problem i ditt liv. Om du gör något åt situationen nu kan du få god hjälp. Om du väntar för länge med att ta i tu med detta kan problemen bli långvariga och förvärras." +
					"<BR><BR>" +
					"Du kan läsa mer om alkoholvanor på följande länkar: <A TARGET=\"_blank\" HREF=\"http://www.alna.se\">www.alna.se</A>, <A TARGET=\"_blank\" HREF=\"http://www.stressochalkohol.se\">www.stressochalkohol.se</A>, <A TARGET=\"_blank\" HREF=\"http://www.escreen.se\">www.escreen.se</A>." +
					"<BR><BR>" +
					"Nedan finns information om vart du kan vända dig för råd och stöd." +
					"<BR><BR>",crid);
				crr("","","" +
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
					"</DIV>",crid);
			}
			#endregion

			tmp = "";
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
			if(score/qos.Length > 3.25)
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
					crr("<BR><BR><B STYLE=\"font-size:16px;\">","Utmattningsdepression","</B><BR>",crid,100,10);
					crr("","Dina svar talar för att du kan ha symtom på depression. Nuförtiden kan depressioner behandlas med mycket gott resultat. Om du inte redan har en läkarkontakt, föreslår vi att du tar kontakt med din husläkare för en undersökning.","",crid);
				}
				else if(Qpbs)
				{
					crr("<BR><BR><B STYLE=\"font-size:16px;\">","Utbrändhet","</B><BR>",crid,100,10);
					crr("","" +
						"Dina svar visar att du har symtom som tyder på en hög stressnivå. Långvarig stress utan tillräcklig återhämtning kan leda till utbrändhet eller utmattningssyndrom. " +
						"Risken för långvariga besvär ökar påtagligt om man har så höga krav på sig själv att man inte tar sig tid för att vila och återhämta sig. " +
						"Det finns bra belägg för att en utbränningsprocess kan stoppas upp. Om du inte redan har en läkarkontakt, föreslår vi att du tar kontakt med din husläkare för en undersökning.","",crid);
				}
				else
				{
					crr("<BR><BR><B STYLE=\"font-size:16px;\">","Utbrändhet","</B><BR>",crid,100,10);
					crr("","" +
						"Dina svar visar att du har symtom som tyder på en hög stressnivå. Långvarig stress utan tillräcklig återhämtning kan leda till utbrändhet eller utmattningssyndrom. " +
						"Det finns bra belägg för att en utbränningsprocess kan stoppas upp. Om du inte redan har en läkarkontakt, föreslår vi att du tar kontakt med din husläkare för en undersökning.","",crid);
				}
			}
			else if(Qdepr)
			{
				crr("<BR><BR><B STYLE=\"font-size:16px;\">","Depression","</B><BR>",crid,100,10);
				crr("",""+
					"Dina svar talar för att du kan ha symtom på depression. Nuförtiden kan depressioner behandlas med mycket gott resultat. Om du inte redan har en läkarkontakt, föreslår vi att du tar kontakt med din husläkare för en undersökning.","",crid);
			}
			#endregion

			tmp = "";
			#region Sleep
			if(oldValues.Contains("Q374O86"))
			{
				crr("<BR><BR><B STYLE=\"font-size:16px;\">","Sömn","</B><BR>",crid,100,10);
				crr("",""+
					"Sömnen är vår viktigaste resurs för återhämtning och är väsentlig bland annat för att bevara hälsa, välbefinnande och prestationsförmåga. Under sömnen insöndras flera viktiga hormoner som reparerar, läker och bygger upp kroppen. Om sömnen under en längre period är störd löper man stor risk för att på sikt drabbas av olika stressrelaterade sjukdomar.","",crid);

				switch(Convert.ToInt32("0" + (string)oldValues["Q374O86"]))
				{
					case 310: crr("<BR><BR>","Du har angivit att din sömn är Bra, vilket tyder på att din sömn och återhämtning är god; att du generellt sett somnar lätt, sover gott och känner dig utvilad när du vaknar.","",crid); break;
					case 311: crr("<BR><BR>","Du har angivit att din sömn är Ganska bra, vilket tyder på att din sömn och återhämtning är god; att du generellt sett somnar lätt, sover gott och känner dig utvilad när du vaknar.","",crid); break;
					case 312: crr("<BR><BR>","Du har angivit att din sömn är varken är bra eller dålig. Om du upplever att du återkommande har svårt att somna, sova gott eller inte är utvilad när du vaknar finns det anledning att förbättra din sömn för att på sikt bevara hälsa och välbefinnande. Det är i sådana fall viktigt att tänka på att varva ned minst två timmar före sänggåendet. Man kan dessutom förbättra sömnkvaliteten genom att regelbundet genomföra andnings- och avslappningsövningar före sänggåendet.","",crid); break;
					case 313: crr("<BR><BR>","Du har angivit att din sömn är Ganska dålig, vilket generellt sett speglar något/några av följande symtom: svårigheter att somna, dålig sömnkvalitet, trötthet vid uppvaknande och att man inte känner sig utvilad. Dessa symtom signalerar ett tydligt behov av att se över sömnen. Det är helt normalt att vid enstaka tillfällen eller under kortare perioder sova dåligt av olika skäl, men om du haft sömnbesvär en längre tid behöver dessa signaler tas på allvar. Du kan börja med att reflektera över om det finns uppenbara anledningar till att du sover dåligt. Om du är osäker på hur du ska hantera dina sömnproblem rekommenderar vi dig att kontakta din husläkare.","",crid); break;
					case 314: crr("<BR><BR>","Du har angivit att din sömn är Dålig, vilket generellt sett speglar något/några av följande symtom: svårigheter att somna, dålig sömnkvalitet, trötthet vid uppvaknande och att man inte känner sig utvilad. Dessa symtom signalerar ett tydligt behov av att se över sömnen. Det är helt normalt att vid enstaka tillfällen eller under kortare perioder sova dåligt av olika skäl, men om du haft sömnbesvär en längre tid behöver dessa signaler tas på allvar. Du kan börja med att reflektera över om det finns uppenbara anledningar till att du sover dåligt. Om du är osäker på hur du ska hantera dina sömnproblem rekommenderar vi dig att kontakta din husläkare.","",crid); break;
				}
			}
			#endregion

			tmp = "";
			#region Health
			if(oldValues.Contains("Q331O98"))
			{
				crr("<BR><BR><B STYLE=\"font-size:16px;\">","Hälsa","</B><BR>",crid,100,10);
				crr("",""+
					"Det är numer väletablerat att självskattad hälsa är en viktig indikator för hur man kommer att må i framtiden. Om man bedömer sin hälsa som ganska bra eller bra har man betydligt större chans att bevara/förbättra sin hälsa i framtiden jämfört med dem som skattat den egna hälsan som dålig.","",crid);

				switch(Convert.ToInt32("0" + (string)oldValues["Q331O98"]))
				{
					case 310: crr("<BR><BR>","Du har angivit att din hälsa är Bra och verkar således ha en hälsosam livsstil och/eller må ganska bra. Personer med din skattning har störst sannolikhet att bevara eller öka sin hälsa i framtiden.","",crid); break;
					case 311: crr("<BR><BR>","Du har angivit att din hälsa är Ganska bra och verkar således ha en hälsosam livsstil och/eller må ganska bra. Personer med din skattning har störst sannolikhet att bevara eller öka sin hälsa i framtiden.","",crid); break;
					case 315: crr("<BR><BR>","Du har angivit att din hälsa varken är bra eller dålig, vilket betyder att det kan finnas en risk för att din hälsa försämras ytterligare i framtiden. Eftersom självskattad hälsa är ett sammanfattande mått på både fysiskt och mentalt välbefinnande rekommenderar vi att du ser över din livsstil, till exempel vad gäller motion, sömn, och matvanor.","",crid); break;
					case 316: crr("<BR><BR>","Du har angivit att din hälsa är Ganska dålig. Om det inte är fråga om en tillfällig nedgång i hur du mår, och om du inte redan har läkarkontakt, rekommenderar vi att du kontaktar din husläkare för en diskussion av ditt hälsotillstånd och vad som eventuellt kan göras för att förbättra det.","",crid); break;
					case 317: crr("<BR><BR>","Du har angivit att din hälsa är Dålig. Om det inte är fråga om en tillfällig nedgång i hur du mår, och om du inte redan har läkarkontakt, rekommenderar vi att du kontaktar din husläkare för en diskussion av ditt hälsotillstånd och vad som eventuellt kan göras för att förbättra det.","",crid); break;
				}
			}
			#endregion

			tmp = "";
			#region BMI
			double Qbmi = 0;
			if(
				oldValues.Contains("Q1980O555") && (string)oldValues["Q1980O555"] != ""	// weight
				&&
				oldValues.Contains("Q1981O554") && (string)oldValues["Q1981O554"] != ""	// height
				)
			{
				Qbmi = Convert.ToDouble(strFloatToStr((string)oldValues["Q1980O555"])) / (Convert.ToDouble(strFloatToStr((string)oldValues["Q1981O554"]))/100 * Convert.ToDouble(strFloatToStr((string)oldValues["Q1981O554"]))/100);

				crr("<BR><BR><B STYLE=\"font-size:16px;\">","BMI","</B><BR>",crid,100,10);
				crr("","" +
					"Body-mass index (BMI) eller kroppsmasseindex är det mått man oftast använder för att definiera övervikt."," ",crid);
				crr("<B>","Ditt BMI är " + Math.Round(Qbmi,1) + "","</B>.<BR><BR><TABLE CELLSPACING=0 BORDER=0 CELLPADDING=3 style=\"border:1px solid #000000; page-break-inside:avoid;\"><TR><TD style=\"border:1px solid #000000;\"><B>Klassifikation</B></td><TD style=\"border:1px solid #000000;\"><B>(alternativ benämning)</B></td><TD style=\"border:1px solid #000000;\"><B>BMI (kg/m2)</B></td><TD style=\"border:1px solid #000000;\"><B>Hälsorisk</B></TD></TR><TR><TD style=\"border:1px solid #000000;\">Undervikt</td><TD style=\"border:1px solid #000000;\">&nbsp;</td><TD style=\"border:1px solid #000000;\">&lt; 18,5</td><TD style=\"border:1px solid #000000;\">Riskerna varierar beroende på orsak till undervikten</TD></TR><TR><TD style=\"border:1px solid #000000;\">Normalvikt</td><TD style=\"border:1px solid #000000;\">&nbsp;</td><TD style=\"border:1px solid #000000;\">18,5 - 24,9</td><TD style=\"border:1px solid #000000;\">Normalrisk</TD></TR>" +
					"<TR><TD style=\"border:1px solid #000000;\">Övervikt</td><TD style=\"border:1px solid #000000;\">&nbsp;</td><TD style=\"border:1px solid #000000;\">25 - 29,9</td><TD style=\"border:1px solid #000000;\">Lätt ökad</TD></TR><TR><TD style=\"border:1px solid #000000;\"><I>Fetma</I></td><TD style=\"border:1px solid #000000;\"><I>Obesitas/ kraftig övervikt</I></td><TD style=\"border:1px solid #000000;\"><I>&ge; 30</I></td><TD style=\"border:1px solid #000000;\"><I>Måttligt till mycket ökad</I></TD></TR><TR><TD style=\"border:1px solid #000000;\">Fetma klass I</td><TD style=\"border:1px solid #000000;\">Fetma</td><TD style=\"border:1px solid #000000;\">30,0 - 34,9</td><TD style=\"border:1px solid #000000;\">Måttligt ökad, ökad risk</TD></TR><TR><TD style=\"border:1px solid #000000;\">Fetma klass II</td><TD style=\"border:1px solid #000000;\">Kraftig/svar fetma</td><TD style=\"border:1px solid #000000;\">35,0 - 39,9</td><TD style=\"border:1px solid #000000;\">Hög, kraftigt ökad risk</TD></TR><TR><TD style=\"border:1px solid #000000;\">Fetma klass III</td><TD style=\"border:1px solid #000000;\">Extrem fetma</td><TD style=\"border:1px solid #000000;\">&ge; 40</td><TD style=\"border:1px solid #000000;\">Mycket hög, extrem riskökning</TD></TR></TABLE>",crid);
			}
			#endregion

			int Qscore = 0;
			tmp = "";
			#region Findrisk
			if(
				(
				oldValues.Contains("Q354O104") && Convert.ToInt32("0" + (string)oldValues["Q354O104"]) == 295 
				||
				oldValues.Contains("Q354O104") && Convert.ToInt32("0" + (string)oldValues["Q354O104"]) == 334 
				)
				&&
				Qbmi != 0
				&&
				oldValues.Contains("Q1982O554") && (string)oldValues["Q1982O554"] != ""	// waist
				&&
				oldValues.Contains("Q1652O422") && (string)oldValues["Q1652O422"] != ""	// gender
				&&
				oldValues.Contains("Q2272O625") && (string)oldValues["Q2272O625"] != ""		// age
				&&
				oldValues.Contains("Q368O109") && (string)oldValues["Q368O109"] != ""	// act1, A=342, B=343
				&&
				oldValues.Contains("Q369O110") && (string)oldValues["Q369O110"] != ""	// act2, A=322,346, B=322
				)
			{
				int Qage = Convert.ToInt32(Convert.ToDouble(strFloatToStr((string)oldValues["Q2272O625"])));
				bool Qfemale = ((string)oldValues["Q1652O422"] == "1265");
				double Qwaist = Convert.ToDouble(strFloatToStr((string)oldValues["Q1982O554"]));
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
					oldValues.Contains("Q352O104") && Convert.ToInt32("0" + (string)oldValues["Q352O104"]) == 294 //Högt blodtryck (hypertoni)
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

				crr("<BR><BR><B STYLE=\"font-size:16px;\">","Diabetes","</B><BR>",crid,100,10);
				if(Qscore > 20)
				{
					crr("","Dina sammanvägda poäng av livsstil och ärftliga faktorer antyder att du löper mycket stor risk att drabbas av typ-2 diabetes. Uppskattningsvis 50% insjuknar. Regelbunden och måttlig motion samt god kosthållning reducerar risken att insjukna i diabetes. Vi föreslår därför att du kontaktar din husläkare för en noggrannare bedömning av blodsocker (både fasteglukos och glukosbelastning alternativt blodsockret efter en måltid). Det visar om du eventuellt har symtomfri diabetes.","",crid);
				}
				else if(Qscore >= 15)
				{
					crr("","Dina sammanvägda poäng av livsstil och ärftliga faktorer antyder att du löper stor risk att drabbas av typ-2 diabetes. Uppskattningsvis 33% insjuknar. Regelbunden och måttlig motion samt god kosthållning reducerar risken att insjukna i diabetes. Vi föreslår därför att du kontaktar din husläkare för en noggrannare bedömning av risken.","",crid);
				}
				else if(Qscore >= 12)
				{
					crr("","Dina sammanvägda poäng av livsstil och ärftliga faktorer antyder att du har en måttlig risk att drabbas av typ-2 diabetes. Uppskattningsvis 17% insjuknar. Regelbunden och måttlig motion samt god kosthållning reducerar risken att insjukna i diabetes.","",crid);
				}
				else if(Qscore >= 7)
				{
					crr("","Dina sammanvägda poäng av livsstil och ärftliga faktorer antyder att du har en något förhöjd risk att drabbas av typ-2 diabetes. Uppskattningsvis 4% insjuknar. Regelbunden och måttlig motion samt god kosthållning reducerar risken att insjukna i diabetes.","",crid);
				}
				else
				{
					crr("","Dina sammanvägda poäng av livsstil och ärftliga faktorer antyder att du inte löper särskilt stor risk att drabbas av typ-2 diabetes. Uppskattningsvis 1% insjuknar. Regelbunden och måttlig motion samt god kosthållning reducerar risken att insjukna i diabetes.","",crid);
				}
				crr("<BR><BR>","" +
					"Typ 2-diabetes (vuxendiabetes, åldersdiabetes) är en allvarlig och ärftlig sjukdom. Men man kan i hög grad påverka diabetesrisken genom livsstilen. Övervikt, särskilt bukfetma, fysisk inaktivitet, dåliga matvanor och rökning ökar risken att få typ 2-diabetes.","",crid);
				crr("" +
					"<BR><BR>","" +
					"Både högre ålder och ärftliga faktorer kan öka risken för typ 2-diabetes och dessa faktorer kan man inte påverka. Däremot kan man påverka de övriga riskfaktorerna såsom till exempel övervikt, bukfetma, fysisk inaktivitet, matvanor och rökning. Genom livsstilen kan man helt och hållet förhindra eller åtminstone skjuta upp typ 2-diabetes så långt fram i tiden som möjligt. Om man har diabetes i släkten ska man vara extra noga med att hålla vikten med åren. Fett runt midjan, så kallad bukfetma, utgör en extra stor risk. Det kan ta tid innan man märker av symtom från typ 2-diabetes.","",crid,100,50);
			}
			#endregion

			#endregion

			if(oldValues.Contains("Q2274O626") || oldValues.Contains("Q1955O551"))
			{
				crr("<BR><BR><B STYLE=\"font-size:16px;\">","EKG","</B><BR>",crid,100,10);
				crr("","EKG-registreringen av din hjärtaktivitet visade på helt normala förhållanden.","<BR>",crid);
			}

			if(oldValues.Contains("Q2262O622") || 
				oldValues.Contains("Q2262O624") || 
				oldValues.Contains("Q2258O622") || 
				oldValues.Contains("Q2258O624") || 
				oldValues.Contains("Q2260O622") ||
				oldValues.Contains("Q2260O624") || 
				oldValues.Contains("Q2254O622") || 
				oldValues.Contains("Q2254O624"))
			{
				crr("<B STYLE=\"font-size:16px;page-break-before:always;\">","Hormoner","</B><BR><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">",crid,100,10);
				crr("</TR><TR><TD COLSPAN=\"2\">","Hormonmätningarna tyder på att du inte har en onormal hormonbalans. Du som var med om ett stresstest i laboratoriet: Dina hormoner ser inte ut att reagera nämnvärt på den kortvariga stressituationen, vilket innebär att eventuella förändringar är så små att de ryms inom ramen för en slumpmässig variation.","<BR><BR>&nbsp;</TD></TR>" +
					"<TR>" +
					(oldValues.Contains("Q2262O622") || 
					oldValues.Contains("Q2262O624") ?
					"<TD><b>Kortisol</b><br><IMG SRC=\"" + System.Configuration.ConfigurationSettings.AppSettings["InstanceURL"] + "/graphTwoBarWithReference.aspx?X=1" +
					(oldValues.Contains("Q2262O622") ? "&V1=" + oldValues["Q2262O622"] : "") +
					(oldValues.Contains("Q2262O624") ? "&V2=" + oldValues["Q2262O624"] : "") +
					(oldValues.Contains("Q2262O621") && (oldValues["Q2262O621"].ToString().IndexOf("<") >= 0 || oldValues["Q2262O621"].ToString().IndexOf("-") > 0) ? "&R1=" + (oldValues["Q2262O621"].ToString().IndexOf("<") >= 0 ? oldValues["Q2262O621"].ToString().Replace("<","") : oldValues["Q2262O621"].ToString().Split('-')[0]).Replace(" ","") : "") +
					(oldValues.Contains("Q2262O621") && (oldValues["Q2262O621"].ToString().IndexOf(">") >= 0 || oldValues["Q2262O621"].ToString().IndexOf("-") > 0) ? "&R2=" + (oldValues["Q2262O621"].ToString().IndexOf(">") >= 0 ? oldValues["Q2262O621"].ToString().Replace(">","") : oldValues["Q2262O621"].ToString().Split('-')[1]).Replace(" ","") : "") +
					"&A=ng/mL&D1=Prov 1&D2=Prov 2\"></TD>" : "") +
					(oldValues.Contains("Q2258O622") || 
					oldValues.Contains("Q2258O624") ?
					"<TD><b>DHEA</b><br><IMG SRC=\"" + System.Configuration.ConfigurationSettings.AppSettings["InstanceURL"] + "/graphTwoBarWithReference.aspx?X=1" +
					(oldValues.Contains("Q2258O622") ? "&V1=" + oldValues["Q2258O622"] : "") +
					(oldValues.Contains("Q2258O624") ? "&V2=" + oldValues["Q2258O624"] : "") +
					(oldValues.Contains("Q2258O621") && (oldValues["Q2258O621"].ToString().IndexOf("<") >= 0 || oldValues["Q2258O621"].ToString().IndexOf("-") > 0) ? "&R1=" + (oldValues["Q2258O621"].ToString().IndexOf("<") >= 0 ? oldValues["Q2258O621"].ToString().Replace("<","") : oldValues["Q2258O621"].ToString().Split('-')[0]).Replace(" ","") : "") +
					(oldValues.Contains("Q2258O621") && (oldValues["Q2258O621"].ToString().IndexOf(">") >= 0 || oldValues["Q2258O621"].ToString().IndexOf("-") > 0) ? "&R2=" + (oldValues["Q2258O621"].ToString().IndexOf(">") >= 0 ? oldValues["Q2258O621"].ToString().Replace(">","") : oldValues["Q2258O621"].ToString().Split('-')[1]).Replace(" ","") : "") +
					"&A=ng/mL&D1=Prov 1&D2=Prov 2\"></TD>" : "") +
					"</TR><TR>",crid,100,100);
				if(oldValues.Contains("Q2262O622") || 
					oldValues.Contains("Q2262O624"))
				{
					crr("<TD VALIGN=\"TOP\" ALIGN=\"CENTER\"><DIV style=\"width:280px;text-align:left;\">","Kortisol är ett hormon som reagerar på stress. Förändringar inom referensområdena över tid är av större intresse än enstaka värden. Kortisol är ett klassiskt stresshormon som under senare års forskning visat sig vara mer relaterat till tillstånd av hopplöshet nedstämdhet samt stressfyllda situationer där man upplever en hjälplöshet och hopplöshet. Det har även en rad effekter på återhämtning och ämnesomsättning.","</DIV></TD>",crid,50);
				}
				if(oldValues.Contains("Q2258O622") || 
					oldValues.Contains("Q2258O624"))
				{
					crr("<TD VALIGN=\"TOP\" ALIGN=\"CENTER\"><DIV style=\"width:280px;text-align:left;\">","DHEA, dehydroepiandrosteron, är ett så kallad anabolt hormon, eller könshormon. Det bygger upp kroppens reserver av energi och stimulerar reparationsprocessen. DHEA minskar vid långvarig stress.","</DIV></TD>",crid,50);
				}

				crr("</TR><TR><TD COLSPAN=\"2\"><br><br>","","</TD></TR>" +
					"<TR>" +
					(oldValues.Contains("Q2260O622") ||
					oldValues.Contains("Q2260O624") ?
					"<TD><b>Testosteron</b><br><IMG SRC=\"" + System.Configuration.ConfigurationSettings.AppSettings["InstanceURL"] + "/graphTwoBarWithReference.aspx?X=1" +
					(oldValues.Contains("Q2260O622") ? "&V1=" + oldValues["Q2260O622"] : "") +
					(oldValues.Contains("Q2260O624") ? "&V2=" + oldValues["Q2260O624"] : "") +
					(oldValues.Contains("Q2260O621") && (oldValues["Q2260O621"].ToString().IndexOf("<") >= 0 || oldValues["Q2260O621"].ToString().IndexOf("-") > 0) ? "&R1=" + (oldValues["Q2260O621"].ToString().IndexOf("<") >= 0 ? oldValues["Q2260O621"].ToString().Replace("<","") : oldValues["Q2260O621"].ToString().Split('-')[0]).Replace(" ","") : "") +
					(oldValues.Contains("Q2260O621") && (oldValues["Q2260O621"].ToString().IndexOf(">") >= 0 || oldValues["Q2260O621"].ToString().IndexOf("-") > 0) ? "&R2=" + (oldValues["Q2260O621"].ToString().IndexOf(">") >= 0 ? oldValues["Q2260O621"].ToString().Replace(">","") : oldValues["Q2260O621"].ToString().Split('-')[1]).Replace(" ","") : "") +
					"&A=ng/mL&D1=Prov 1&D2=Prov 2\"></TD>" : "") +
					(oldValues.Contains("Q2254O622") || 
					oldValues.Contains("Q2254O624") ? 
					"<TD><b>Östradiol</b><br><IMG SRC=\"" + System.Configuration.ConfigurationSettings.AppSettings["InstanceURL"] + "/graphTwoBarWithReference.aspx?X=1" +
					(oldValues.Contains("Q2254O622") ? "&V1=" + oldValues["Q2254O622"] : "") +
					(oldValues.Contains("Q2254O624") ? "&V2=" + oldValues["Q2254O624"] : "") +
					(oldValues.Contains("Q2254O621") && (oldValues["Q2254O621"].ToString().IndexOf("<") >= 0 || oldValues["Q2254O621"].ToString().IndexOf("-") > 0) ? "&R1=" + (oldValues["Q2254O621"].ToString().IndexOf("<") >= 0 ? oldValues["Q2254O621"].ToString().Replace("<","") : oldValues["Q2254O621"].ToString().Split('-')[0]).Replace(" ","") : "") +
					(oldValues.Contains("Q2254O621") && (oldValues["Q2254O621"].ToString().IndexOf(">") >= 0 || oldValues["Q2254O621"].ToString().IndexOf("-") > 0) ? "&R2=" + (oldValues["Q2254O621"].ToString().IndexOf(">") >= 0 ? oldValues["Q2254O621"].ToString().Replace(">","") : oldValues["Q2254O621"].ToString().Split('-')[1]).Replace(" ","") : "") +
					"&A=ng/mL&D1=Prov 1&D2=Prov 2\"></TD>" : "") +
					"</TR><TR>",crid,100,10);
				if(oldValues.Contains("Q2260O622") ||
					oldValues.Contains("Q2260O624"))
				{
					crr("<TD VALIGN=\"TOP\" ALIGN=\"CENTER\"><DIV style=\"width:280px;text-align:left;\">","Testosteron är ett så kallat manligt könshormon. I själva verket finns hormonet hos både kvinnor och män, om än i olika halter. Testosteron är ett anti-stresshormon, som bygger upp kroppens energiresurser och muskulatur. Vid långvarig stress kan testosteronet sjunka.","</DIV></TD>",crid,50);
				}
				if(oldValues.Contains("Q2254O622") || 
					oldValues.Contains("Q2254O624"))
				{
					crr("<TD VALIGN=\"TOP\" ALIGN=\"CENTER\"><DIV style=\"width:280px;text-align:left;\">","Östradiol är ett så kallat kvinnligt könshormon. I själva verket finns hormonet hos både kvinnor och män, om än i olika halter. Östrogen är ett anti-stresshormon, som bygger upp kroppens energiresurser och muskulatur. Vid långvarig stress kan östradiolet sjunka.","</DIV></TD>",crid,50);
				}

				crr("</TR><TR><TD COLSPAN=\"2\">","","</TD></TR></TABLE>",crid,100,10);
			}
			if(oldValues.Contains("Q1702O447") || oldValues.Contains("Q1703O447") || oldValues.Contains("Q1705O447") || oldValues.Contains("Q1706O447") || oldValues.Contains("Q1707O447") || oldValues.Contains("Q1708O447") ||
				oldValues.Contains("Q1702O445") || oldValues.Contains("Q1703O445") || oldValues.Contains("Q1705O445") || oldValues.Contains("Q1706O445") || oldValues.Contains("Q1707O445") || oldValues.Contains("Q1708O445") ||
				oldValues.Contains("Q1711O448") || oldValues.Contains("Q1710O448"))
			{
				crr("<B STYLE=\"font-size:16px;page-break-before:always;\">","Hörsel","</B><BR><br>",crid,100,10);
				crr("<b>","Audiometritestet","</b><br/>",crid,100,10);
				crr("","Hörbarhetsnivån uttrycks i decibel (dB) och baserar sig på genomsnittet av toner med frekvenser mellan 500 Hz(låga frekvenser; bastoner) till 8000 Hz (höga frekvenser; ljusa ljud). Man har normal hörsel om man kan höra toner på 25 dB eller lägre.","<BR><br>",crid);
				crr("<b>","Hörselnedsättning","</b><br/>",crid,100,10);
				crr("","Det finns många olika former och grader av hörselnedsättning. " +
					"I vissa former, förlorar man bara förmågan att höra höga eller låga toner. " +
					"Oförmåga att höra rena toner svagare än 25 dB tyder på en lätt hörselnedsättning. " +
					"Lätt hörselnedsättning (26-40 dB HL), " +
					"måttlig hörselnedsättning (41-55 dB HL), " +
					"måttligt svår (56-70 dB HL), " +
					"svår (71-90 dB HL) och " +
					"mycket svår hörselnedsättning (91dB HL eller större)." +
					"","<br/><br/>",crid);
				crr("","Följande tillstånd kan påverka testresultat:","<br/><UL style=\"margin-top:0px;margin-bottom:0px;\">",crid,100,10);
				crr("<LI>","Akustiskt neurom","</LI>",crid,100,10);
				crr("<LI>","Bullertrauma","</LI>",crid,100,10);
				crr("<LI>","Åldersrelaterad hörselnedsättning","</LI>",crid,100,10);
				crr("<LI>","Menières sjukdom","</LI>",crid,100,10);
				crr("<LI>","Arbetsrelaterad hörselnedsättning","</LI>",crid,100,10);
				crr("<LI>","Otoskleros","</LI>",crid,100,10);
				crr("<LI>","Problem med trumhinnan","</LI>",crid,100,10);
				crr("<LI>","Vax i hörselgången","</LI>",crid,100,10);
				crr("<LI>","Infektion i mellanörat","</LI>",crid,100,10);
				crr("</UL>","","<br>",crid,100,10);
				if(oldValues.Contains("Q1702O447") || oldValues.Contains("Q1703O447") || oldValues.Contains("Q1705O447") || oldValues.Contains("Q1706O447") || oldValues.Contains("Q1707O447") || oldValues.Contains("Q1708O447"))
				{
					crr(""+
						"<b>Audiometri, Vänster öra</b><br><IMG SRC=\"" + System.Configuration.ConfigurationSettings.AppSettings["InstanceURL"] + "/graphSixGreenRed.aspx?X=Frekvens" +
						"&L1=" + Server.UrlEncode("Normal hörsel") + 
						"&L2=" + Server.UrlEncode("Lätt hörselnedsättning") +
						"&L3=" + Server.UrlEncode("Måttlig hörselnedsättning") +
						"&L4=" + Server.UrlEncode("Måttligt svår hörselnedsättning") +
						"&L5=" + Server.UrlEncode("Svår hörselnedsättning") +
						"&L6=" + Server.UrlEncode("Mycket svår hörselnedsättning") +
						(oldValues.Contains("Q1702O447") ? "&V1=" + oldValues["Q1702O447"] : "") +
						(oldValues.Contains("Q1703O447") ? "&V2=" + oldValues["Q1703O447"] : "") +
						(oldValues.Contains("Q1705O447") ? "&V3=" + oldValues["Q1705O447"] : "") +
						(oldValues.Contains("Q1706O447") ? "&V4=" + oldValues["Q1706O447"] : "") +
						(oldValues.Contains("Q1707O447") ? "&V5=" + oldValues["Q1707O447"] : "") +
						(oldValues.Contains("Q1708O447") ? "&V6=" + oldValues["Q1708O447"] : "") +
						"&A=" + Server.UrlEncode("Ljudnivå (dB)") + "&D1=500 Hz&D2=1000 Hz&D3=2000 Hz&D4=4000 Hz&D5=6000 Hz&D6=8000 Hz\"><BR>" +
						"","","<BR><BR>",crid,100,10);

					string db0 = "", db1 = "", db2 = "", db3 = "", db4 = "", db5 = "";

					#region 500Hz
					if(oldValues.Contains("Q1702O447"))
					{
						float v = (float)Convert.ToDouble(strFloatToStr(oldValues["Q1702O447"].ToString()));
						if(v > 90) { db5 += (db5 != "" ? "," : "") + "500Hz"; }
						else if(v > 70) { db4 += (db4 != "" ? "," : "") + "500Hz"; }
						else if(v > 55) { db3 += (db3 != "" ? "," : "") + "500Hz"; }
						else if(v > 40) { db2 += (db2 != "" ? "," : "") + "500Hz"; }
						else if(v > 25) { db1 += (db1 != "" ? "," : "") + "500Hz"; }
						else { db0 += (db0 != "" ? "," : "") + "500Hz"; }
					}
					#endregion
					#region 1000Hz
					if(oldValues.Contains("Q1703O447"))
					{
						float v = (float)Convert.ToDouble(strFloatToStr(oldValues["Q1703O447"].ToString()));
						if(v > 90) { db5 += (db5 != "" ? "," : "") + "1000Hz"; }
						else if(v > 70) { db4 += (db4 != "" ? "," : "") + "1000Hz"; }
						else if(v > 55) { db3 += (db3 != "" ? "," : "") + "1000Hz"; }
						else if(v > 40) { db2 += (db2 != "" ? "," : "") + "1000Hz"; }
						else if(v > 25) { db1 += (db1 != "" ? "," : "") + "1000Hz"; }
						else { db0 += (db0 != "" ? "," : "") + "1000Hz"; }
					}
					#endregion
					#region 2000Hz
					if(oldValues.Contains("Q1705O447"))
					{
						float v = (float)Convert.ToDouble(strFloatToStr(oldValues["Q1705O447"].ToString()));
						if(v > 90) { db5 += (db5 != "" ? "," : "") + "2000Hz"; }
						else if(v > 70) { db4 += (db4 != "" ? "," : "") + "2000Hz"; }
						else if(v > 55) { db3 += (db3 != "" ? "," : "") + "2000Hz"; }
						else if(v > 40) { db2 += (db2 != "" ? "," : "") + "2000Hz"; }
						else if(v > 25) { db1 += (db1 != "" ? "," : "") + "2000Hz"; }
						else { db0 += (db0 != "" ? "," : "") + "2000Hz"; }
					}
					#endregion
					#region 4000Hz
					if(oldValues.Contains("Q1706O447"))
					{
						float v = (float)Convert.ToDouble(strFloatToStr(oldValues["Q1706O447"].ToString()));
						if(v > 90) { db5 += (db5 != "" ? "," : "") + "4000Hz"; }
						else if(v > 70) { db4 += (db4 != "" ? "," : "") + "4000Hz"; }
						else if(v > 55) { db3 += (db3 != "" ? "," : "") + "4000Hz"; }
						else if(v > 40) { db2 += (db2 != "" ? "," : "") + "4000Hz"; }
						else if(v > 25) { db1 += (db1 != "" ? "," : "") + "4000Hz"; }
						else { db0 += (db0 != "" ? "," : "") + "4000Hz"; }
					}
					#endregion
					#region 6000Hz
					if(oldValues.Contains("Q1707O447"))
					{
						float v = (float)Convert.ToDouble(strFloatToStr(oldValues["Q1707O447"].ToString()));
						if(v > 90) { db5 += (db5 != "" ? "," : "") + "6000Hz"; }
						else if(v > 70) { db4 += (db4 != "" ? "," : "") + "6000Hz"; }
						else if(v > 55) { db3 += (db3 != "" ? "," : "") + "6000Hz"; }
						else if(v > 40) { db2 += (db2 != "" ? "," : "") + "6000Hz"; }
						else if(v > 25) { db1 += (db1 != "" ? "," : "") + "6000Hz"; }
						else { db0 += (db0 != "" ? "," : "") + "6000Hz"; }
					}
					#endregion
					#region 8000Hz
					if(oldValues.Contains("Q1708O447"))
					{
						float v = (float)Convert.ToDouble(strFloatToStr(oldValues["Q1708O447"].ToString()));
						if(v > 90) { db5 += (db5 != "" ? "," : "") + "8000Hz"; }
						else if(v > 70) { db4 += (db4 != "" ? "," : "") + "8000Hz"; }
						else if(v > 55) { db3 += (db3 != "" ? "," : "") + "8000Hz"; }
						else if(v > 40) { db2 += (db2 != "" ? "," : "") + "8000Hz"; }
						else if(v > 25) { db1 += (db1 != "" ? "," : "") + "8000Hz"; }
						else { db0 += (db0 != "" ? "," : "") + "8000Hz"; }
					}
					#endregion

					string feedback = "";
					if(db5 != "")
					{
						feedback = "Hörseltestet visar att du har en mycket svår hörselnedsättning på följande frekvenser i ditt vänstra öra: " + db5 + ". ";
						string rest = db4;
						if(db3 != "") { rest += (rest != "" ? "," : "") + db3; }
						if(db2 != "") { rest += (rest != "" ? "," : "") + db2; }
						if(db1 != "") { rest += (rest != "" ? "," : "") + db1; }
						if(rest != "") { feedback += "Du har även hörselnedsättning på följande frekvenser: " + rest + ". "; }
						else if(db0 != "") { feedback += "Hörseln är normal på övriga frekvenser. "; }
						feedback += "Du har förmodligen svårt att uppfatta ljud i din omgivning med ditt vänstra öra. Ett mer djupgående hörseltest rekommenderas.";
					}
					else if(db4 != "")
					{
						feedback = "Hörseltestet visar att du har en svår hörselnedsättning på följande frekvenser i ditt vänstra öra: " + db4 + ". ";
						string rest = db3;
						if(db2 != "") { rest += (rest != "" ? "," : "") + db2; }
						if(db1 != "") { rest += (rest != "" ? "," : "") + db1; }
						if(rest != "") { feedback += "Du har även hörselnedsättning på följande frekvenser: " + rest + ". "; }
						else if(db0 != "") { feedback += "Hörseln är normal på övriga frekvenser. "; }
						feedback += "Du har förmodligen svårt att uppfatta många ljud i din omgivning med ditt vänstra öra. Ett mer djupgående hörseltest rekommenderas.";
					}
					else if(db3 != "")
					{
						feedback = "Hörseltestet visar att du har en måttligt svår hörselnedsättning på följande frekvenser i ditt vänstra öra: " + db3 + ". ";
						string rest = db2;
						if(db1 != "") { rest += (rest != "" ? "," : "") + db1; }
						if(rest != "") { feedback += "Du har även hörselnedsättning på följande frekvenser: " + rest + ". "; }
						else if(db0 != "") { feedback += "Hörseln är normal på övriga frekvenser. "; }
						feedback += "Du har förmodligen svårt att uppfatta vissa ljud i din omgivning med ditt vänstra öra. Ett mer djupgående hörseltest rekommenderas.";
					}
					else if(db2 != "")
					{
						feedback = "Hörseltestet visar att du har en måttlig hörselnedsättning på följande frekvenser i ditt vänstra öra: " + db2 + ". Det betyder att du har svårt att uppfatta ljud inom " + db2 + ". ";
						string rest = db1;
						if(rest != "") { feedback += "Du har även hörselnedsättning på följande frekvenser: " + rest + ". "; }
						else if(db0 != "") { feedback += "Hörseln är normal på övriga frekvenser. "; }
						feedback += "Du har förmodligen svårt att uppfatta vissa ljud i din omgivning med ditt vänstra öra. Ett mer djupgående hörseltest rekommenderas.";
					}
					else if(db1 != "")
					{
						feedback = "Hörseltestet visar att du har en lätt hörselnedsättning på följande frekvenser i ditt vänstra öra: " + db1 + ". Det betyder att du har viss svårighet att uppfatta ljud inom " + db1 + ". ";
						if(db0 != "") { feedback += "Hörseln är normal på övriga frekvenser. "; }
						feedback += "Du har förmodligen svårt att uppfatta vissa ljud i din omgivning med ditt vänstra öra. Ett mer djupgående hörseltest rekommenderas.";
					}
					else
					{
						feedback = "Hörseltestet visar att du har normal hörsel på vänster öra. Det innebär att du kan uppfatta ljud inom ett brett frekvensområde som omfattar både låg- och högfrekventa ljud). Du kan dessutom höra dessa frekvenser på en svag ljudnivå vilket tyder på bra hörsel.";
					}
					crr("",feedback,"<BR>",crid);
				}

				if(oldValues.Contains("Q1702O445") || oldValues.Contains("Q1703O445") || oldValues.Contains("Q1705O445") || oldValues.Contains("Q1706O445") || oldValues.Contains("Q1707O445") || oldValues.Contains("Q1708O445"))
				{
					crr(""+
						"<b style=\"page-break-before:always;\">Audiometri, Höger öra</b><br><IMG SRC=\"" + System.Configuration.ConfigurationSettings.AppSettings["InstanceURL"] + "/graphSixGreenRed.aspx?X=Frekvens" +
						"&L1=" + Server.UrlEncode("Normal hörsel") + 
						"&L2=" + Server.UrlEncode("Lätt hörselnedsättning") +
						"&L3=" + Server.UrlEncode("Måttlig hörselnedsättning") +
						"&L4=" + Server.UrlEncode("Måttligt svår hörselnedsättning") +
						"&L5=" + Server.UrlEncode("Svår hörselnedsättning") +
						"&L6=" + Server.UrlEncode("Mycket svår hörselnedsättning") +
						(oldValues.Contains("Q1702O445") ? "&V1=" + oldValues["Q1702O445"] : "") +
						(oldValues.Contains("Q1703O445") ? "&V2=" + oldValues["Q1703O445"] : "") +
						(oldValues.Contains("Q1705O445") ? "&V3=" + oldValues["Q1705O445"] : "") +
						(oldValues.Contains("Q1706O445") ? "&V4=" + oldValues["Q1706O445"] : "") +
						(oldValues.Contains("Q1707O445") ? "&V5=" + oldValues["Q1707O445"] : "") +
						(oldValues.Contains("Q1708O445") ? "&V6=" + oldValues["Q1708O445"] : "") +
						"&A=" + Server.UrlEncode("Ljudnivå (dB)") + "&D1=500 Hz&D2=1000 Hz&D3=2000 Hz&D4=4000 Hz&D5=6000 Hz&D6=8000 Hz\"><BR>" +
						"","","<BR><BR>",crid,100,10);

					string db0 = "", db1 = "", db2 = "", db3 = "", db4 = "", db5 = "";

					#region 500Hz
					if(oldValues.Contains("Q1702O445"))
					{
						float v = (float)Convert.ToDouble(strFloatToStr(oldValues["Q1702O445"].ToString()));
						if(v > 90) { db5 += (db5 != "" ? "," : "") + "500Hz"; }
						else if(v > 70) { db4 += (db4 != "" ? "," : "") + "500Hz"; }
						else if(v > 55) { db3 += (db3 != "" ? "," : "") + "500Hz"; }
						else if(v > 40) { db2 += (db2 != "" ? "," : "") + "500Hz"; }
						else if(v > 25) { db1 += (db1 != "" ? "," : "") + "500Hz"; }
						else { db0 += (db0 != "" ? "," : "") + "500Hz"; }
					}
					#endregion
					#region 1000Hz
					if(oldValues.Contains("Q1703O445"))
					{
						float v = (float)Convert.ToDouble(strFloatToStr(oldValues["Q1703O445"].ToString()));
						if(v > 90) { db5 += (db5 != "" ? "," : "") + "1000Hz"; }
						else if(v > 70) { db4 += (db4 != "" ? "," : "") + "1000Hz"; }
						else if(v > 55) { db3 += (db3 != "" ? "," : "") + "1000Hz"; }
						else if(v > 40) { db2 += (db2 != "" ? "," : "") + "1000Hz"; }
						else if(v > 25) { db1 += (db1 != "" ? "," : "") + "1000Hz"; }
						else { db0 += (db0 != "" ? "," : "") + "1000Hz"; }
					}
					#endregion
					#region 2000Hz
					if(oldValues.Contains("Q1705O445"))
					{
						float v = (float)Convert.ToDouble(strFloatToStr(oldValues["Q1705O445"].ToString()));
						if(v > 90) { db5 += (db5 != "" ? "," : "") + "2000Hz"; }
						else if(v > 70) { db4 += (db4 != "" ? "," : "") + "2000Hz"; }
						else if(v > 55) { db3 += (db3 != "" ? "," : "") + "2000Hz"; }
						else if(v > 40) { db2 += (db2 != "" ? "," : "") + "2000Hz"; }
						else if(v > 25) { db1 += (db1 != "" ? "," : "") + "2000Hz"; }
						else { db0 += (db0 != "" ? "," : "") + "2000Hz"; }
					}
					#endregion
					#region 4000Hz
					if(oldValues.Contains("Q1706O445"))
					{
						float v = (float)Convert.ToDouble(strFloatToStr(oldValues["Q1706O445"].ToString()));
						if(v > 90) { db5 += (db5 != "" ? "," : "") + "4000Hz"; }
						else if(v > 70) { db4 += (db4 != "" ? "," : "") + "4000Hz"; }
						else if(v > 55) { db3 += (db3 != "" ? "," : "") + "4000Hz"; }
						else if(v > 40) { db2 += (db2 != "" ? "," : "") + "4000Hz"; }
						else if(v > 25) { db1 += (db1 != "" ? "," : "") + "4000Hz"; }
						else { db0 += (db0 != "" ? "," : "") + "4000Hz"; }
					}
					#endregion
					#region 6000Hz
					if(oldValues.Contains("Q1707O445"))
					{
						float v = (float)Convert.ToDouble(strFloatToStr(oldValues["Q1707O445"].ToString()));
						if(v > 90) { db5 += (db5 != "" ? "," : "") + "6000Hz"; }
						else if(v > 70) { db4 += (db4 != "" ? "," : "") + "6000Hz"; }
						else if(v > 55) { db3 += (db3 != "" ? "," : "") + "6000Hz"; }
						else if(v > 40) { db2 += (db2 != "" ? "," : "") + "6000Hz"; }
						else if(v > 25) { db1 += (db1 != "" ? "," : "") + "6000Hz"; }
						else { db0 += (db0 != "" ? "," : "") + "6000Hz"; }
					}
					#endregion
					#region 8000Hz
					if(oldValues.Contains("Q1708O445"))
					{
						float v = (float)Convert.ToDouble(strFloatToStr(oldValues["Q1708O445"].ToString()));
						if(v > 90) { db5 += (db5 != "" ? "," : "") + "8000Hz"; }
						else if(v > 70) { db4 += (db4 != "" ? "," : "") + "8000Hz"; }
						else if(v > 55) { db3 += (db3 != "" ? "," : "") + "8000Hz"; }
						else if(v > 40) { db2 += (db2 != "" ? "," : "") + "8000Hz"; }
						else if(v > 25) { db1 += (db1 != "" ? "," : "") + "8000Hz"; }
						else { db0 += (db0 != "" ? "," : "") + "8000Hz"; }
					}
					#endregion

					string feedback = "";
					if(db5 != "")
					{
						feedback = "Hörseltestet visar att du har en mycket svår hörselnedsättning på följande frekvenser i ditt högra öra: " + db5 + ". ";
						string rest = db4;
						if(db3 != "") { rest += (rest != "" ? "," : "") + db3; }
						if(db2 != "") { rest += (rest != "" ? "," : "") + db2; }
						if(db1 != "") { rest += (rest != "" ? "," : "") + db1; }
						if(rest != "") { feedback += "Du har även hörselnedsättning på följande frekvenser: " + rest + ". "; }
						else if(db0 != "") { feedback += "Hörseln är normal på övriga frekvenser. "; }
						feedback += "Du har förmodligen svårt att uppfatta ljud i din omgivning med ditt högra öra. Ett mer djupgående hörseltest rekommenderas.";
					}
					else if(db4 != "")
					{
						feedback = "Hörseltestet visar att du har en svår hörselnedsättning på följande frekvenser i ditt högra öra: " + db4 + ". ";
						string rest = db3;
						if(db2 != "") { rest += (rest != "" ? "," : "") + db2; }
						if(db1 != "") { rest += (rest != "" ? "," : "") + db1; }
						if(rest != "") { feedback += "Du har även hörselnedsättning på följande frekvenser: " + rest + ". "; }
						else if(db0 != "") { feedback += "Hörseln är normal på övriga frekvenser. "; }
						feedback += "Du har förmodligen svårt att uppfatta många ljud i din omgivning med ditt högra öra. Ett mer djupgående hörseltest rekommenderas.";
					}
					else if(db3 != "")
					{
						feedback = "Hörseltestet visar att du har en måttligt svår hörselnedsättning på följande frekvenser i ditt högra öra: " + db3 + ". ";
						string rest = db2;
						if(db1 != "") { rest += (rest != "" ? "," : "") + db1; }
						if(rest != "") { feedback += "Du har även hörselnedsättning på följande frekvenser: " + rest + ". "; }
						else if(db0 != "") { feedback += "Hörseln är normal på övriga frekvenser. "; }
						feedback += "Du har förmodligen svårt att uppfatta vissa ljud i din omgivning med ditt högra öra. Ett mer djupgående hörseltest rekommenderas.";
					}
					else if(db2 != "")
					{
						feedback = "Hörseltestet visar att du har en måttlig hörselnedsättning på följande frekvenser i ditt högra öra: " + db2 + ". Det betyder att du har svårt att uppfatta ljud inom " + db2 + ". ";
						string rest = db1;
						if(rest != "") { feedback += "Du har även hörselnedsättning på följande frekvenser: " + rest + ". "; }
						else if(db0 != "") { feedback += "Hörseln är normal på övriga frekvenser. "; }
						feedback += "Du har förmodligen svårt att uppfatta vissa ljud i din omgivning med ditt högra öra. Ett mer djupgående hörseltest rekommenderas.";
					}
					else if(db1 != "")
					{
						feedback = "Hörseltestet visar att du har en lätt hörselnedsättning på följande frekvenser i ditt högra öra: " + db1 + ". Det betyder att du har viss svårighet att uppfatta ljud inom " + db1 + ". ";
						if(db0 != "") { feedback += "Hörseln är normal på övriga frekvenser. "; }
						feedback += "Du har förmodligen svårt att uppfatta vissa ljud i din omgivning med ditt högra öra. Ett mer djupgående hörseltest rekommenderas.";
					}
					else
					{
						feedback = "Hörseltestet visar att du har normal hörsel på höger öra. Det innebär att du kan uppfatta ljud inom ett brett frekvensområde som omfattar både låg- och högfrekventa ljud). Du kan dessutom höra dessa frekvenser på en svag ljudnivå vilket tyder på bra hörsel.";
					}
					crr("",feedback,"<BR><BR>",crid);
				}

				if(oldValues.Contains("Q1711O448") || oldValues.Contains("Q1710O448"))
				{
					crr("<b>","Tal-i-brus-testet","</b><br/><br/>",crid,100,10);
			
					float v1 = (oldValues.Contains("Q1711O448") ? (float)Convert.ToDouble(strFloatToStr(oldValues["Q1711O448"].ToString())) : float.MinValue);
					float v2 = v1;//(oldValues.Contains("Q1724O448") ? (float)Convert.ToDouble(strFloatToStr(oldValues["Q1724O448"].ToString())) : float.MinValue);
					float h1 = (oldValues.Contains("Q1710O448") ? (float)Convert.ToDouble(strFloatToStr(oldValues["Q1710O448"].ToString())) : float.MinValue);
					float h2 = h1;//(oldValues.Contains("Q1723O448") ? (float)Convert.ToDouble(strFloatToStr(oldValues["Q1723O448"].ToString())) : float.MinValue);

					crr("","Tal-i-brustestet återspeglar din förmåga att uppfatta tal i en stimmig miljö. Ditt resultat finner du i tabellen nedan.","<br/><br/>",crid);
					crr("" +
						"<table border=\"0\" style=\"border:1px solid #000000;cellpadding=\"5\" cellspacing=\"0\">" +
						//"<tr><td><B>Öra</B></td><td><b>Resultat 1</b></td><td><b>Resultat 2</b></td></tr>" +
						"<tr><td><B>Öra</B></td><td><b>Resultat</b></td></tr>" +
						"<tr>" +
						"<td>Vänster</td>" +
						"<td>" + (v1 > float.MinValue ? v1.ToString() : "") + "</td>" +
						//"<td>" + (v2 > float.MinValue ? v2.ToString() : "")  + "</td>" +
						"</tr>" +
						"<tr>" +
						"<td>Höger</td>" +
						"<td>" + (h1 > float.MinValue ? h1.ToString() : "")  + "</td>" +
						//"<td>" + (h2 > float.MinValue ? h2.ToString() : "")  + "</td>" +
						"</tr>" +
						"</table><br/>","" +
						(v1 < 7 && v1 > float.MinValue || v2 < 7 && v2 > float.MinValue || h1 < 7 && h1 > float.MinValue || h2 < 7 && h2 > float.MinValue ? "Resultat mellan -24 och + 6 anses vara ett bra så kallat tal-i-brusvärde. Det betyder att du kan uppfatta tal när bakgrundsljudet (dvs bruset) är minst 7 dB starkare än talnivån.<br>" : "") +
						(v1 >= 7 && v1 <= 12 || v2 >= 7 && v2 <= 12 || h1 >= 7 && h1 <= 12 || h2 >= 7 && h2 <= 12 ? "Resultat mellan +7 och +12 innebär att du förmodligen har vissa svårigheter att uppfatta tal i stimmiga miljöer. Ett mer djupgående hörseltest hos audionom, företagssjuksköterska eller öronspecialist rekommenderas för att du ska kunna få en mer fullständig bedömning av din hörsel.<br>" : "") +
						(v1 > 12 || v2 > 12 || h1 > 12 || h2 > 12 ? "Ett resultat som är större än +12 antyder ett dåligt tal-i-brus värde. Du har förmodligen svårt att uppfatta tal även då bakgrundsljudet är lågt. Ett mer djupgående hörseltest hos audionom, företagssjuksköterska eller öronspecialist rekommenderas för att du ska kunna få en mer fullständig bedömning av din hörsel.<br>" : "") +
						"","",crid);
				}
			}
			#endregion
		}
		public static string strFloatToStr(string s)
		{
			s = s.Trim();
			s = s.Replace(",",System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
			s = s.Replace(".",System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
			return s;
		}

		public static string strFloatToSqlStr(string s)
		{
			s = s.Trim();
			s = s.Replace("'","");
			s = s.Replace(",",".");
			return (s == "" ? "NULL" : s);
		}
		private void crr(string before, string editable, string after, int crid)
		{
			crr(before,editable,after,crid,100,30);
		}
		private void crr(string before, string editable, string after, int crid, int width)
		{
			crr(before,editable,after,crid,width,30);
		}
		private void crr(string before, string editable, string after, int crid, int width, int height)
		{
			Db.execute("INSERT INTO CustomReportRow (CustomReportID,Before,Editable,After,Width,Height) VALUES (" + crid + ",'" + before.Replace("'","''") + "','" + editable.Replace("'","''") + "','" + after.Replace("'","''") + "'," + width + "," + height + ")");
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(HttpContext.Current.Request.QueryString["DeleteCRRID"] != null)
			{
				Db.execute("UPDATE CustomReportRow SET CustomReportID = -ABS(CustomReportID) WHERE CustomReportRowID = " + HttpContext.Current.Request.QueryString["DeleteCRRID"]);

				System.Text.StringBuilder content = new System.Text.StringBuilder();
				content.Append("<HTML><HEAD><style type=\"text/css\">body, div, td {font-family: Arial, Helvetica, sans-serif; font-size: 12px;}body {margin: 0;padding: 0;text-align: center;}</style></HEAD><BODY><div style=\"text-align:left;margin: 0 auto;width:620px;\">");
				OdbcDataReader rs = Db.recordSet("SELECT " +
					"crr.Before, " +
					"crr.Editable, " +
					"crr.After, " +
					"crr.Width, " +
					"crr.CustomReportRowID " +
					"FROM CustomReport cr " +
					"INNER JOIN CustomReportRow crr ON cr.CustomReportID = crr.CustomReportID " +
					"WHERE cr.UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]) + " ORDER BY crr.CustomReportRowID");
				while(rs.Read())
				{
					content.Append(rs.GetString(0)+rs.GetString(1)+rs.GetString(2));
				}
				rs.Close();
				content.Append("</div></BODY></HTML>");

				rs = Db.recordSet("SELECT u.UserNr, u.UserID FROM [User] u WHERE u.UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]));
				while(rs.Read())
				{
					createPDF(content.ToString(),(rs.IsDBNull(0) ? rs.GetInt32(1) : rs.GetInt32(0)),Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]),false);
				}
				rs.Close();
			}
			if(HttpContext.Current.Request.QueryString["RegenAll"] != null)
			{
				OdbcDataReader rs = Db.recordSet("SELECT u.UserNr, u.UserID FROM [User] u WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
				while(rs.Read())
				{
					generateReport(rs.GetInt32(1),(rs.IsDBNull(0) ? rs.GetInt32(1) : rs.GetInt32(0)));
				}
				rs.Close();
			}
			else if(HttpContext.Current.Request.QueryString["PdfAll"] != null)
			{
				WebSupergoo.ABCpdf6.Doc theDoc = new WebSupergoo.ABCpdf6.Doc();

				OdbcDataReader rs = Db.recordSet("SELECT u.UserNr, u.UserID FROM [User] u WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
				while(rs.Read())
				{
					System.Text.StringBuilder content = new System.Text.StringBuilder();
					content.Append("<HTML><HEAD><style type=\"text/css\">body, div, td {font-family: Arial, Helvetica, sans-serif; font-size: 12px;}body {margin: 0;padding: 0;text-align: center;}</style></HEAD><BODY><div style=\"text-align:left;margin: 0 auto;width:620px;\">");
					OdbcDataReader rs2 = Db.recordSet("SELECT " +
						"crr.Before, " +
						"crr.Editable, " +
						"crr.After, " +
						"crr.Width, " +
						"crr.CustomReportRowID " +
						"FROM CustomReport cr " +
						"INNER JOIN CustomReportRow crr ON cr.CustomReportID = crr.CustomReportID " +
						"WHERE cr.UserID = " + rs.GetInt32(1) + " ORDER BY crr.CustomReportRowID");
					while(rs2.Read())
					{
						content.Append(rs2.GetString(0)+rs2.GetString(1)+rs2.GetString(2));
					}
					rs2.Close();
					content.Append("</div></BODY></HTML>");

					createPDF(content.ToString(),(rs.IsDBNull(0) ? rs.GetInt32(1) : rs.GetInt32(0)),rs.GetInt32(1),false);

					WebSupergoo.ABCpdf6.Doc theSrc = new WebSupergoo.ABCpdf6.Doc();
					try
					{
						theSrc.Read(HttpContext.Current.Server.MapPath("report/" + rs.GetInt32(1) + ".pdf"));
						theDoc.Append(theSrc);
					}
					catch(Exception){}
					theSrc.Clear();
					theSrc.Dispose();
				}
				rs.Close();

				byte[] output = theDoc.GetData();

				theDoc.Clear();
				theDoc.Dispose();
				theDoc = null;

				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ClearHeaders();
				HttpContext.Current.Response.Charset = "UTF-8";
				HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
				HttpContext.Current.Response.ContentType = "application/pdf";
				HttpContext.Current.Response.AddHeader("content-length", output.LongLength.ToString());
				HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=feedback.PDF");
				HttpContext.Current.Response.BinaryWrite(output);
				HttpContext.Current.Response.End();
			}
			else if(HttpContext.Current.Request.QueryString["UserID"] != null)
			{
				//bool pdf = false; 
				int crid = 0, usernr = 0;//(HttpContext.Current.Request.QueryString["CustomReportID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["CustomReportID"]) : 0);
				OdbcDataReader rs = Db.recordSet("SELECT c.CustomReportID, u.UserNr, u.UserID FROM [User] u LEFT OUTER JOIN CustomReport c ON c.UserID = u.UserID WHERE u.UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]));
				if(rs.Read())
				{
					if(!rs.IsDBNull(0))
					{
						crid = rs.GetInt32(0);
					}
					if(!rs.IsDBNull(1))
					{
						usernr = rs.GetInt32(1);
					}
					else
					{
						usernr = rs.GetInt32(2);
					}
				}
				rs.Close();

				if(crid == 0 || HttpContext.Current.Request.QueryString["Regen"] != null)
				{
					generateReport(Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]), usernr);

					//pdf = true;
				}

				System.Text.StringBuilder content = new System.Text.StringBuilder();
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				sb.Append("<HTML><HEAD><style type=\"text/css\">body, div, td {font-family: Arial, Helvetica, sans-serif; font-size: 12px;}body {margin: 0;padding: 0;text-align: center;}</style></HEAD><BODY><div style=\"text-align:left;margin: 0 auto;width:620px;\">");
				if(HttpContext.Current.Request.QueryString["Edit"] != null)
				{
					Save.Visible=true;
				}
				rs = Db.recordSet("SELECT crr.Before, crr.Editable, crr.After, crr.Width, crr.CustomReportRowID, crr.Height FROM CustomReport cr INNER JOIN CustomReportRow crr ON cr.CustomReportID = crr.CustomReportID WHERE cr.UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]) + " ORDER BY crr.CustomReportRowID");
				while(rs.Read())
				{
					if(HttpContext.Current.Request.QueryString["Edit"] != null)
					{
						content.Append("" + rs.GetString(0)+"<TEXTAREA style=\"" + (rs.IsDBNull(5) || rs.GetInt32(5) != 100 ? "" : "background-color:#FFF7D7;") + "width:" + Convert.ToInt32(((rs.IsDBNull(3) ? 100d : (double)rs.GetInt32(3))/100d*500d)) + "px;height:" + Convert.ToInt32(((rs.IsDBNull(5) ? 40d : (double)rs.GetInt32(5))/100d*500d)) + "px;\" ID=\"CRRID" + rs.GetInt32(4) + "\" NAME=\"CRRID" + rs.GetInt32(4) + "\">" + rs.GetString(1) + "</TEXTAREA><br/><span style=\"font-size:10px;\">[<A HREF=\"customFeedback.aspx?UserID=" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]) + "&Edit=1&DeleteCRRID=" + rs.GetInt32(4) + "\">ta bort avsnitt</A>]</span><br/>" + rs.GetString(2));
					}
					else
					{
						content.Append(rs.GetString(0)+rs.GetString(1)+rs.GetString(2));
					}
				}
				rs.Close();
				sb.Append(content.ToString() + "</div></BODY></HTML>");

				if(crid == 0)
				{
					createPDF(sb.ToString(),usernr,Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]),false);
				}
				if(HttpContext.Current.Request.QueryString["PDF"] != null)
				{
					HttpContext.Current.Response.Redirect("report/" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]) + ".pdf?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "",true);
				}
				if(HttpContext.Current.Request.QueryString["Regen"] != null)
				{
					createPDF(sb.ToString(),usernr,Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]),false);
					HttpContext.Current.Response.Redirect("dashboard.aspx?Search=1#UserID" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]),true);
				}
				if(HttpContext.Current.Request.QueryString["SendTest"] != null)
				{
					if(projectSetup.isEmail((HttpContext.Current.Request.QueryString["SendTo"] != null ? HttpContext.Current.Request.QueryString["SendTo"].ToString() : HttpContext.Current.Session["SponsorAdminEmail"].ToString())))
					{
						rs = Db.recordSet("SELECT FeedbackEmailFrom, FeedbackEmailSubject, FeedbackEmailBody FROM Sponsor WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
						if(rs.Read())
						{
							if(projectSetup.isEmail(rs.GetString(0)))
							{
								try
								{
									System.Web.Mail.MailAttachment attachment = new	System.Web.Mail.MailAttachment(Page.MapPath("report/" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]) + ".pdf"));

									System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
									msg.To = (HttpContext.Current.Request.QueryString["SendTo"] != null ? HttpContext.Current.Request.QueryString["SendTo"].ToString() : HttpContext.Current.Session["SponsorAdminEmail"].ToString());
									msg.From = rs.GetString(0);
									msg.Subject = rs.GetString(1);
									msg.Body = rs.GetString(2);
									msg.Attachments.Add(attachment);
									msg.BodyFormat = System.Web.Mail.MailFormat.Text;
									msg.BodyEncoding = System.Text.Encoding.GetEncoding(1252);
									System.Web.Mail.SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
									System.Web.Mail.SmtpMail.Send(msg);
								}
								catch(Exception){}
							}
						}
						rs.Close();
					}
					HttpContext.Current.Response.Redirect("dashboard.aspx?Search=1#UserID" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]),true);
				}
				if(HttpContext.Current.Request.QueryString["Send"] != null)
				{
					rs = Db.recordSet("SELECT s.FeedbackEmailFrom, s.FeedbackEmailSubject, s.FeedbackEmailBody, u.UserIdent" + HttpContext.Current.Session["EmailIdent"] + " FROM Sponsor s INNER JOIN [User] u ON s.SponsorID = u.SponsorID WHERE u.UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]) + " AND s.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
					if(rs.Read())
					{
						if(projectSetup.isEmail(rs.GetString(0)) && projectSetup.isEmail(rs.GetString(3)))
						{
							try
							{
								System.Web.Mail.MailAttachment attachment = new	System.Web.Mail.MailAttachment(Page.MapPath("report/" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]) + ".pdf"));

								System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
								msg.To = rs.GetString(3);
								msg.From = rs.GetString(0);
								msg.Subject = rs.GetString(1);
								msg.Body = rs.GetString(2);
								msg.Attachments.Add(attachment);
								msg.BodyFormat = System.Web.Mail.MailFormat.Text;
								msg.BodyEncoding = System.Text.Encoding.GetEncoding(1252);
								System.Web.Mail.SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
								System.Web.Mail.SmtpMail.Send(msg);

								Db.execute("UPDATE [User] SET FeedbackSent = GETDATE() WHERE UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]));
							}
							catch(Exception){}
						}
					}
					rs.Close();

					HttpContext.Current.Response.Redirect("dashboard.aspx?Search=1#UserID" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]),true);
				}

				feedback.Controls.Add(new LiteralControl(content.ToString()));

				Save.Click += new EventHandler(Save_Click);
			}
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

		private void Save_Click(object sender, EventArgs e)
		{
			System.Text.StringBuilder content = new System.Text.StringBuilder();
			content.Append("<HTML><HEAD><style type=\"text/css\">body, div, td {font-family: Arial, Helvetica, sans-serif; font-size: 12px;}body {margin: 0;padding: 0;text-align: center;}</style></HEAD><BODY><div style=\"text-align:left;margin: 0 auto;width:620px;\">");
			OdbcDataReader rs = Db.recordSet("SELECT " +
				"crr.Before, " +
				"crr.Editable, " +
				"crr.After, " +
				"crr.Width, " +
				"crr.CustomReportRowID " +
				"FROM CustomReport cr " +
				"INNER JOIN CustomReportRow crr ON cr.CustomReportID = crr.CustomReportID " +
				"WHERE cr.UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]) + " ORDER BY crr.CustomReportRowID");
			while(rs.Read())
			{
				if(HttpContext.Current.Request.Form["CRRID" + rs.GetInt32(4) + ""] != null)
				{
					content.Append(rs.GetString(0)+HttpContext.Current.Request.Form["CRRID" + rs.GetInt32(4) + ""].ToString()+rs.GetString(2));
					Db.execute("UPDATE CustomReportRow SET Editable = '" + HttpContext.Current.Request.Form["CRRID" + rs.GetInt32(4) + ""].ToString().Replace("'","''") + "' WHERE CustomReportRowID = " + rs.GetInt32(4));
				}
				else
				{
					content.Append(rs.GetString(0)+rs.GetString(1)+rs.GetString(2));
				}
			}
			rs.Close();
			content.Append("</div></BODY></HTML>");

			rs = Db.recordSet("SELECT u.UserNr, u.UserID FROM [User] u WHERE u.UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]));
			while(rs.Read())
			{
				createPDF(content.ToString(),(rs.IsDBNull(0) ? rs.GetInt32(1) : rs.GetInt32(0)),Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]),false);
			}
			rs.Close();

			HttpContext.Current.Response.Redirect("dashboard.aspx?Search=1#UserID" + Convert.ToInt32(HttpContext.Current.Request.QueryString["UserID"]),true);
		}
	}
}
