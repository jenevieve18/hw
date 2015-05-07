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
			crr("<B STYLE=\"font-size:20px;\">","�terkoppling","</B>",crid,100,10);

			crr("<BR><BR><B STYLE=\"font-size:16px;\">","Sammanv�gd bed�mning","</B><BR>",crid,100,10);
			crr("","Sammanfattningsvis tyder dina enk�tsvar och v�ra m�tningar p� att du har en god h�lsa. Det kan vara s� att du rapporterat n�gon form av h�lsoproblem (s� som beskrivs nedan), eller uppvisat n�gon avvikelse i de biologiska m�tnigarna (d.v.s. EKG, hormonm�tningar, h�rseltester och Body Mass Index). Om du �nskar f� en personlig genomg�ng av dina resultat �r du v�lkommen att h�ra av dig till Martin Benka Wall�n p� telefon 08-553 789 24 eller e-post Martin.Benka-Wallen@ki.se. Du kan givetvis ocks� ta kontakt med din husl�kare eller f�retagsl�kare."," ",crid,100,100);

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
			//						tmp = "Du har angivit att du har besv�r fr�n ";
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
			//						tmp += " samt att dessa besv�r ";
			//						if(QbnWorse)
			//						{
			//							tmp += "f�rv�rras av arbetet";
			//							if(QbnSleep || QbnWork)
			//							{
			//								tmp += " och att besv�ren ";
			//							}
			//						}
			//						if(QbnSleep || QbnWork)
			//						{
			//							tmp += "varit s� pass sv�ra att du haft sv�rt ";
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
			//								tmp += "utf�ra ditt arbete";
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
						tmp += "L�ngvarig hosta";
						if(Qpip)
						{
							tmp += " och ";
						}
					}
					if(Qpip)
					{
						tmp += "Pip i br�stet";
					}
					crr("<BR><BR><B STYLE=\"font-size:16px;\">",tmp,"</B><BR>",crid,100,10);
						
					crr("","Dina svar visar att du har symtom som skulle kunna bero p� din r�kning. Om du inte redan har s�kt l�karhj�lp, rekommenderar vi att du g�r det.","",crid);
				}
				else
				{
					crr("<BR><BR><B STYLE=\"font-size:16px;\">","R�kning","</B><BR>",crid,100,10);
					crr("","Som du s�kert vet, �r r�kning riskabelt f�r h�lsan. Om du vill hj�lp med att sluta r�ka rekommenderas du att kontakta din husl�kare.","",crid);
				}
			}
			else if(Qpip || Qhosta)
			{
				if(Qhosta)
				{
					tmp += "L�ngvarig hosta";
					if(Qpip)
					{
						tmp += " och ";
					}
				}
				if(Qpip)
				{
					tmp += "Pip i br�stet";
				}
				crr("<BR><BR><B STYLE=\"font-size:16px;\">",tmp,"</B><BR>",crid,100,10);
				crr("","Symtomen l�ngvarig hosta och pip i br�stet kan vara symtom p� astma. Om du inte du redan har behandling, rekommenderar vi att du s�ker din husl�kare f�r en bed�mning.","",crid);
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
					"I v�r kultur ing�r ofta alkohol i den sociala samvaron och kan ha flera positiva effekter. Dina svar talar f�r att alkoholen medf�r problem i ditt liv. Om du g�r n�got �t situationen nu kan du f� god hj�lp. Om du v�ntar f�r l�nge med att ta i tu med detta kan problemen bli l�ngvariga och f�rv�rras." +
					"<BR><BR>" +
					"Du kan l�sa mer om alkoholvanor p� f�ljande l�nkar: <A TARGET=\"_blank\" HREF=\"http://www.alna.se\">www.alna.se</A>, <A TARGET=\"_blank\" HREF=\"http://www.stressochalkohol.se\">www.stressochalkohol.se</A>, <A TARGET=\"_blank\" HREF=\"http://www.escreen.se\">www.escreen.se</A>." +
					"<BR><BR>" +
					"Nedan finns information om vart du kan v�nda dig f�r r�d och st�d." +
					"<BR><BR>",crid);
				crr("","","" +
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
					"</DIV>",crid);
			}
			#endregion

			tmp = "";
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
					crr("","Dina svar talar f�r att du kan ha symtom p� depression. Nuf�rtiden kan depressioner behandlas med mycket gott resultat. Om du inte redan har en l�karkontakt, f�resl�r vi att du tar kontakt med din husl�kare f�r en unders�kning.","",crid);
				}
				else if(Qpbs)
				{
					crr("<BR><BR><B STYLE=\"font-size:16px;\">","Utbr�ndhet","</B><BR>",crid,100,10);
					crr("","" +
						"Dina svar visar att du har symtom som tyder p� en h�g stressniv�. L�ngvarig stress utan tillr�cklig �terh�mtning kan leda till utbr�ndhet eller utmattningssyndrom. " +
						"Risken f�r l�ngvariga besv�r �kar p�tagligt om man har s� h�ga krav p� sig sj�lv att man inte tar sig tid f�r att vila och �terh�mta sig. " +
						"Det finns bra bel�gg f�r att en utbr�nningsprocess kan stoppas upp. Om du inte redan har en l�karkontakt, f�resl�r vi att du tar kontakt med din husl�kare f�r en unders�kning.","",crid);
				}
				else
				{
					crr("<BR><BR><B STYLE=\"font-size:16px;\">","Utbr�ndhet","</B><BR>",crid,100,10);
					crr("","" +
						"Dina svar visar att du har symtom som tyder p� en h�g stressniv�. L�ngvarig stress utan tillr�cklig �terh�mtning kan leda till utbr�ndhet eller utmattningssyndrom. " +
						"Det finns bra bel�gg f�r att en utbr�nningsprocess kan stoppas upp. Om du inte redan har en l�karkontakt, f�resl�r vi att du tar kontakt med din husl�kare f�r en unders�kning.","",crid);
				}
			}
			else if(Qdepr)
			{
				crr("<BR><BR><B STYLE=\"font-size:16px;\">","Depression","</B><BR>",crid,100,10);
				crr("",""+
					"Dina svar talar f�r att du kan ha symtom p� depression. Nuf�rtiden kan depressioner behandlas med mycket gott resultat. Om du inte redan har en l�karkontakt, f�resl�r vi att du tar kontakt med din husl�kare f�r en unders�kning.","",crid);
			}
			#endregion

			tmp = "";
			#region Sleep
			if(oldValues.Contains("Q374O86"))
			{
				crr("<BR><BR><B STYLE=\"font-size:16px;\">","S�mn","</B><BR>",crid,100,10);
				crr("",""+
					"S�mnen �r v�r viktigaste resurs f�r �terh�mtning och �r v�sentlig bland annat f�r att bevara h�lsa, v�lbefinnande och prestationsf�rm�ga. Under s�mnen ins�ndras flera viktiga hormoner som reparerar, l�ker och bygger upp kroppen. Om s�mnen under en l�ngre period �r st�rd l�per man stor risk f�r att p� sikt drabbas av olika stressrelaterade sjukdomar.","",crid);

				switch(Convert.ToInt32("0" + (string)oldValues["Q374O86"]))
				{
					case 310: crr("<BR><BR>","Du har angivit att din s�mn �r Bra, vilket tyder p� att din s�mn och �terh�mtning �r god; att du generellt sett somnar l�tt, sover gott och k�nner dig utvilad n�r du vaknar.","",crid); break;
					case 311: crr("<BR><BR>","Du har angivit att din s�mn �r Ganska bra, vilket tyder p� att din s�mn och �terh�mtning �r god; att du generellt sett somnar l�tt, sover gott och k�nner dig utvilad n�r du vaknar.","",crid); break;
					case 312: crr("<BR><BR>","Du har angivit att din s�mn �r varken �r bra eller d�lig. Om du upplever att du �terkommande har sv�rt att somna, sova gott eller inte �r utvilad n�r du vaknar finns det anledning att f�rb�ttra din s�mn f�r att p� sikt bevara h�lsa och v�lbefinnande. Det �r i s�dana fall viktigt att t�nka p� att varva ned minst tv� timmar f�re s�ngg�endet. Man kan dessutom f�rb�ttra s�mnkvaliteten genom att regelbundet genomf�ra andnings- och avslappnings�vningar f�re s�ngg�endet.","",crid); break;
					case 313: crr("<BR><BR>","Du har angivit att din s�mn �r Ganska d�lig, vilket generellt sett speglar n�got/n�gra av f�ljande symtom: sv�righeter att somna, d�lig s�mnkvalitet, tr�tthet vid uppvaknande och att man inte k�nner sig utvilad. Dessa symtom signalerar ett tydligt behov av att se �ver s�mnen. Det �r helt normalt att vid enstaka tillf�llen eller under kortare perioder sova d�ligt av olika sk�l, men om du haft s�mnbesv�r en l�ngre tid beh�ver dessa signaler tas p� allvar. Du kan b�rja med att reflektera �ver om det finns uppenbara anledningar till att du sover d�ligt. Om du �r os�ker p� hur du ska hantera dina s�mnproblem rekommenderar vi dig att kontakta din husl�kare.","",crid); break;
					case 314: crr("<BR><BR>","Du har angivit att din s�mn �r D�lig, vilket generellt sett speglar n�got/n�gra av f�ljande symtom: sv�righeter att somna, d�lig s�mnkvalitet, tr�tthet vid uppvaknande och att man inte k�nner sig utvilad. Dessa symtom signalerar ett tydligt behov av att se �ver s�mnen. Det �r helt normalt att vid enstaka tillf�llen eller under kortare perioder sova d�ligt av olika sk�l, men om du haft s�mnbesv�r en l�ngre tid beh�ver dessa signaler tas p� allvar. Du kan b�rja med att reflektera �ver om det finns uppenbara anledningar till att du sover d�ligt. Om du �r os�ker p� hur du ska hantera dina s�mnproblem rekommenderar vi dig att kontakta din husl�kare.","",crid); break;
				}
			}
			#endregion

			tmp = "";
			#region Health
			if(oldValues.Contains("Q331O98"))
			{
				crr("<BR><BR><B STYLE=\"font-size:16px;\">","H�lsa","</B><BR>",crid,100,10);
				crr("",""+
					"Det �r numer v�letablerat att sj�lvskattad h�lsa �r en viktig indikator f�r hur man kommer att m� i framtiden. Om man bed�mer sin h�lsa som ganska bra eller bra har man betydligt st�rre chans att bevara/f�rb�ttra sin h�lsa i framtiden j�mf�rt med dem som skattat den egna h�lsan som d�lig.","",crid);

				switch(Convert.ToInt32("0" + (string)oldValues["Q331O98"]))
				{
					case 310: crr("<BR><BR>","Du har angivit att din h�lsa �r Bra och verkar s�ledes ha en h�lsosam livsstil och/eller m� ganska bra. Personer med din skattning har st�rst sannolikhet att bevara eller �ka sin h�lsa i framtiden.","",crid); break;
					case 311: crr("<BR><BR>","Du har angivit att din h�lsa �r Ganska bra och verkar s�ledes ha en h�lsosam livsstil och/eller m� ganska bra. Personer med din skattning har st�rst sannolikhet att bevara eller �ka sin h�lsa i framtiden.","",crid); break;
					case 315: crr("<BR><BR>","Du har angivit att din h�lsa varken �r bra eller d�lig, vilket betyder att det kan finnas en risk f�r att din h�lsa f�rs�mras ytterligare i framtiden. Eftersom sj�lvskattad h�lsa �r ett sammanfattande m�tt p� b�de fysiskt och mentalt v�lbefinnande rekommenderar vi att du ser �ver din livsstil, till exempel vad g�ller motion, s�mn, och matvanor.","",crid); break;
					case 316: crr("<BR><BR>","Du har angivit att din h�lsa �r Ganska d�lig. Om det inte �r fr�ga om en tillf�llig nedg�ng i hur du m�r, och om du inte redan har l�karkontakt, rekommenderar vi att du kontaktar din husl�kare f�r en diskussion av ditt h�lsotillst�nd och vad som eventuellt kan g�ras f�r att f�rb�ttra det.","",crid); break;
					case 317: crr("<BR><BR>","Du har angivit att din h�lsa �r D�lig. Om det inte �r fr�ga om en tillf�llig nedg�ng i hur du m�r, och om du inte redan har l�karkontakt, rekommenderar vi att du kontaktar din husl�kare f�r en diskussion av ditt h�lsotillst�nd och vad som eventuellt kan g�ras f�r att f�rb�ttra det.","",crid); break;
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
					"Body-mass index (BMI) eller kroppsmasseindex �r det m�tt man oftast anv�nder f�r att definiera �vervikt."," ",crid);
				crr("<B>","Ditt BMI �r " + Math.Round(Qbmi,1) + "","</B>.<BR><BR><TABLE CELLSPACING=0 BORDER=0 CELLPADDING=3 style=\"border:1px solid #000000; page-break-inside:avoid;\"><TR><TD style=\"border:1px solid #000000;\"><B>Klassifikation</B></td><TD style=\"border:1px solid #000000;\"><B>(alternativ ben�mning)</B></td><TD style=\"border:1px solid #000000;\"><B>BMI (kg/m2)</B></td><TD style=\"border:1px solid #000000;\"><B>H�lsorisk</B></TD></TR><TR><TD style=\"border:1px solid #000000;\">Undervikt</td><TD style=\"border:1px solid #000000;\">&nbsp;</td><TD style=\"border:1px solid #000000;\">&lt; 18,5</td><TD style=\"border:1px solid #000000;\">Riskerna varierar beroende p� orsak till undervikten</TD></TR><TR><TD style=\"border:1px solid #000000;\">Normalvikt</td><TD style=\"border:1px solid #000000;\">&nbsp;</td><TD style=\"border:1px solid #000000;\">18,5 - 24,9</td><TD style=\"border:1px solid #000000;\">Normalrisk</TD></TR>" +
					"<TR><TD style=\"border:1px solid #000000;\">�vervikt</td><TD style=\"border:1px solid #000000;\">&nbsp;</td><TD style=\"border:1px solid #000000;\">25 - 29,9</td><TD style=\"border:1px solid #000000;\">L�tt �kad</TD></TR><TR><TD style=\"border:1px solid #000000;\"><I>Fetma</I></td><TD style=\"border:1px solid #000000;\"><I>Obesitas/ kraftig �vervikt</I></td><TD style=\"border:1px solid #000000;\"><I>&ge; 30</I></td><TD style=\"border:1px solid #000000;\"><I>M�ttligt till mycket �kad</I></TD></TR><TR><TD style=\"border:1px solid #000000;\">Fetma klass I</td><TD style=\"border:1px solid #000000;\">Fetma</td><TD style=\"border:1px solid #000000;\">30,0 - 34,9</td><TD style=\"border:1px solid #000000;\">M�ttligt �kad, �kad risk</TD></TR><TR><TD style=\"border:1px solid #000000;\">Fetma klass II</td><TD style=\"border:1px solid #000000;\">Kraftig/svar fetma</td><TD style=\"border:1px solid #000000;\">35,0 - 39,9</td><TD style=\"border:1px solid #000000;\">H�g, kraftigt �kad risk</TD></TR><TR><TD style=\"border:1px solid #000000;\">Fetma klass III</td><TD style=\"border:1px solid #000000;\">Extrem fetma</td><TD style=\"border:1px solid #000000;\">&ge; 40</td><TD style=\"border:1px solid #000000;\">Mycket h�g, extrem risk�kning</TD></TR></TABLE>",crid);
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
					oldValues.Contains("Q352O104") && Convert.ToInt32("0" + (string)oldValues["Q352O104"]) == 294 //H�gt blodtryck (hypertoni)
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
					crr("","Dina sammanv�gda po�ng av livsstil och �rftliga faktorer antyder att du l�per mycket stor risk att drabbas av typ-2 diabetes. Uppskattningsvis 50% insjuknar. Regelbunden och m�ttlig motion samt god kosth�llning reducerar risken att insjukna i diabetes. Vi f�resl�r d�rf�r att du kontaktar din husl�kare f�r en noggrannare bed�mning av blodsocker (b�de fasteglukos och glukosbelastning alternativt blodsockret efter en m�ltid). Det visar om du eventuellt har symtomfri diabetes.","",crid);
				}
				else if(Qscore >= 15)
				{
					crr("","Dina sammanv�gda po�ng av livsstil och �rftliga faktorer antyder att du l�per stor risk att drabbas av typ-2 diabetes. Uppskattningsvis 33% insjuknar. Regelbunden och m�ttlig motion samt god kosth�llning reducerar risken att insjukna i diabetes. Vi f�resl�r d�rf�r att du kontaktar din husl�kare f�r en noggrannare bed�mning av risken.","",crid);
				}
				else if(Qscore >= 12)
				{
					crr("","Dina sammanv�gda po�ng av livsstil och �rftliga faktorer antyder att du har en m�ttlig risk att drabbas av typ-2 diabetes. Uppskattningsvis 17% insjuknar. Regelbunden och m�ttlig motion samt god kosth�llning reducerar risken att insjukna i diabetes.","",crid);
				}
				else if(Qscore >= 7)
				{
					crr("","Dina sammanv�gda po�ng av livsstil och �rftliga faktorer antyder att du har en n�got f�rh�jd risk att drabbas av typ-2 diabetes. Uppskattningsvis 4% insjuknar. Regelbunden och m�ttlig motion samt god kosth�llning reducerar risken att insjukna i diabetes.","",crid);
				}
				else
				{
					crr("","Dina sammanv�gda po�ng av livsstil och �rftliga faktorer antyder att du inte l�per s�rskilt stor risk att drabbas av typ-2 diabetes. Uppskattningsvis 1% insjuknar. Regelbunden och m�ttlig motion samt god kosth�llning reducerar risken att insjukna i diabetes.","",crid);
				}
				crr("<BR><BR>","" +
					"Typ 2-diabetes (vuxendiabetes, �ldersdiabetes) �r en allvarlig och �rftlig sjukdom. Men man kan i h�g grad p�verka diabetesrisken genom livsstilen. �vervikt, s�rskilt bukfetma, fysisk inaktivitet, d�liga matvanor och r�kning �kar risken att f� typ 2-diabetes.","",crid);
				crr("" +
					"<BR><BR>","" +
					"B�de h�gre �lder och �rftliga faktorer kan �ka risken f�r typ 2-diabetes och dessa faktorer kan man inte p�verka. D�remot kan man p�verka de �vriga riskfaktorerna s�som till exempel �vervikt, bukfetma, fysisk inaktivitet, matvanor och r�kning. Genom livsstilen kan man helt och h�llet f�rhindra eller �tminstone skjuta upp typ 2-diabetes s� l�ngt fram i tiden som m�jligt. Om man har diabetes i sl�kten ska man vara extra noga med att h�lla vikten med �ren. Fett runt midjan, s� kallad bukfetma, utg�r en extra stor risk. Det kan ta tid innan man m�rker av symtom fr�n typ 2-diabetes.","",crid,100,50);
			}
			#endregion

			#endregion

			if(oldValues.Contains("Q2274O626") || oldValues.Contains("Q1955O551"))
			{
				crr("<BR><BR><B STYLE=\"font-size:16px;\">","EKG","</B><BR>",crid,100,10);
				crr("","EKG-registreringen av din hj�rtaktivitet visade p� helt normala f�rh�llanden.","<BR>",crid);
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
				crr("</TR><TR><TD COLSPAN=\"2\">","Hormonm�tningarna tyder p� att du inte har en onormal hormonbalans. Du som var med om ett stresstest i laboratoriet: Dina hormoner ser inte ut att reagera n�mnv�rt p� den kortvariga stressituationen, vilket inneb�r att eventuella f�r�ndringar �r s� sm� att de ryms inom ramen f�r en slumpm�ssig variation.","<BR><BR>&nbsp;</TD></TR>" +
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
					crr("<TD VALIGN=\"TOP\" ALIGN=\"CENTER\"><DIV style=\"width:280px;text-align:left;\">","Kortisol �r ett hormon som reagerar p� stress. F�r�ndringar inom referensomr�dena �ver tid �r av st�rre intresse �n enstaka v�rden. Kortisol �r ett klassiskt stresshormon som under senare �rs forskning visat sig vara mer relaterat till tillst�nd av hoppl�shet nedst�mdhet samt stressfyllda situationer d�r man upplever en hj�lpl�shet och hoppl�shet. Det har �ven en rad effekter p� �terh�mtning och �mnesoms�ttning.","</DIV></TD>",crid,50);
				}
				if(oldValues.Contains("Q2258O622") || 
					oldValues.Contains("Q2258O624"))
				{
					crr("<TD VALIGN=\"TOP\" ALIGN=\"CENTER\"><DIV style=\"width:280px;text-align:left;\">","DHEA, dehydroepiandrosteron, �r ett s� kallad anabolt hormon, eller k�nshormon. Det bygger upp kroppens reserver av energi och stimulerar reparationsprocessen. DHEA minskar vid l�ngvarig stress.","</DIV></TD>",crid,50);
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
					"<TD><b>�stradiol</b><br><IMG SRC=\"" + System.Configuration.ConfigurationSettings.AppSettings["InstanceURL"] + "/graphTwoBarWithReference.aspx?X=1" +
					(oldValues.Contains("Q2254O622") ? "&V1=" + oldValues["Q2254O622"] : "") +
					(oldValues.Contains("Q2254O624") ? "&V2=" + oldValues["Q2254O624"] : "") +
					(oldValues.Contains("Q2254O621") && (oldValues["Q2254O621"].ToString().IndexOf("<") >= 0 || oldValues["Q2254O621"].ToString().IndexOf("-") > 0) ? "&R1=" + (oldValues["Q2254O621"].ToString().IndexOf("<") >= 0 ? oldValues["Q2254O621"].ToString().Replace("<","") : oldValues["Q2254O621"].ToString().Split('-')[0]).Replace(" ","") : "") +
					(oldValues.Contains("Q2254O621") && (oldValues["Q2254O621"].ToString().IndexOf(">") >= 0 || oldValues["Q2254O621"].ToString().IndexOf("-") > 0) ? "&R2=" + (oldValues["Q2254O621"].ToString().IndexOf(">") >= 0 ? oldValues["Q2254O621"].ToString().Replace(">","") : oldValues["Q2254O621"].ToString().Split('-')[1]).Replace(" ","") : "") +
					"&A=ng/mL&D1=Prov 1&D2=Prov 2\"></TD>" : "") +
					"</TR><TR>",crid,100,10);
				if(oldValues.Contains("Q2260O622") ||
					oldValues.Contains("Q2260O624"))
				{
					crr("<TD VALIGN=\"TOP\" ALIGN=\"CENTER\"><DIV style=\"width:280px;text-align:left;\">","Testosteron �r ett s� kallat manligt k�nshormon. I sj�lva verket finns hormonet hos b�de kvinnor och m�n, om �n i olika halter. Testosteron �r ett anti-stresshormon, som bygger upp kroppens energiresurser och muskulatur. Vid l�ngvarig stress kan testosteronet sjunka.","</DIV></TD>",crid,50);
				}
				if(oldValues.Contains("Q2254O622") || 
					oldValues.Contains("Q2254O624"))
				{
					crr("<TD VALIGN=\"TOP\" ALIGN=\"CENTER\"><DIV style=\"width:280px;text-align:left;\">","�stradiol �r ett s� kallat kvinnligt k�nshormon. I sj�lva verket finns hormonet hos b�de kvinnor och m�n, om �n i olika halter. �strogen �r ett anti-stresshormon, som bygger upp kroppens energiresurser och muskulatur. Vid l�ngvarig stress kan �stradiolet sjunka.","</DIV></TD>",crid,50);
				}

				crr("</TR><TR><TD COLSPAN=\"2\">","","</TD></TR></TABLE>",crid,100,10);
			}
			if(oldValues.Contains("Q1702O447") || oldValues.Contains("Q1703O447") || oldValues.Contains("Q1705O447") || oldValues.Contains("Q1706O447") || oldValues.Contains("Q1707O447") || oldValues.Contains("Q1708O447") ||
				oldValues.Contains("Q1702O445") || oldValues.Contains("Q1703O445") || oldValues.Contains("Q1705O445") || oldValues.Contains("Q1706O445") || oldValues.Contains("Q1707O445") || oldValues.Contains("Q1708O445") ||
				oldValues.Contains("Q1711O448") || oldValues.Contains("Q1710O448"))
			{
				crr("<B STYLE=\"font-size:16px;page-break-before:always;\">","H�rsel","</B><BR><br>",crid,100,10);
				crr("<b>","Audiometritestet","</b><br/>",crid,100,10);
				crr("","H�rbarhetsniv�n uttrycks i decibel (dB) och baserar sig p� genomsnittet av toner med frekvenser mellan 500 Hz(l�ga frekvenser; bastoner) till 8000 Hz (h�ga frekvenser; ljusa ljud). Man har normal h�rsel om man kan h�ra toner p� 25 dB eller l�gre.","<BR><br>",crid);
				crr("<b>","H�rselneds�ttning","</b><br/>",crid,100,10);
				crr("","Det finns m�nga olika former och grader av h�rselneds�ttning. " +
					"I vissa former, f�rlorar man bara f�rm�gan att h�ra h�ga eller l�ga toner. " +
					"Of�rm�ga att h�ra rena toner svagare �n 25 dB tyder p� en l�tt h�rselneds�ttning. " +
					"L�tt h�rselneds�ttning (26-40 dB HL), " +
					"m�ttlig h�rselneds�ttning (41-55 dB HL), " +
					"m�ttligt sv�r (56-70 dB HL), " +
					"sv�r (71-90 dB HL) och " +
					"mycket sv�r h�rselneds�ttning (91dB HL eller st�rre)." +
					"","<br/><br/>",crid);
				crr("","F�ljande tillst�nd kan p�verka testresultat:","<br/><UL style=\"margin-top:0px;margin-bottom:0px;\">",crid,100,10);
				crr("<LI>","Akustiskt neurom","</LI>",crid,100,10);
				crr("<LI>","Bullertrauma","</LI>",crid,100,10);
				crr("<LI>","�ldersrelaterad h�rselneds�ttning","</LI>",crid,100,10);
				crr("<LI>","Meni�res sjukdom","</LI>",crid,100,10);
				crr("<LI>","Arbetsrelaterad h�rselneds�ttning","</LI>",crid,100,10);
				crr("<LI>","Otoskleros","</LI>",crid,100,10);
				crr("<LI>","Problem med trumhinnan","</LI>",crid,100,10);
				crr("<LI>","Vax i h�rselg�ngen","</LI>",crid,100,10);
				crr("<LI>","Infektion i mellan�rat","</LI>",crid,100,10);
				crr("</UL>","","<br>",crid,100,10);
				if(oldValues.Contains("Q1702O447") || oldValues.Contains("Q1703O447") || oldValues.Contains("Q1705O447") || oldValues.Contains("Q1706O447") || oldValues.Contains("Q1707O447") || oldValues.Contains("Q1708O447"))
				{
					crr(""+
						"<b>Audiometri, V�nster �ra</b><br><IMG SRC=\"" + System.Configuration.ConfigurationSettings.AppSettings["InstanceURL"] + "/graphSixGreenRed.aspx?X=Frekvens" +
						"&L1=" + Server.UrlEncode("Normal h�rsel") + 
						"&L2=" + Server.UrlEncode("L�tt h�rselneds�ttning") +
						"&L3=" + Server.UrlEncode("M�ttlig h�rselneds�ttning") +
						"&L4=" + Server.UrlEncode("M�ttligt sv�r h�rselneds�ttning") +
						"&L5=" + Server.UrlEncode("Sv�r h�rselneds�ttning") +
						"&L6=" + Server.UrlEncode("Mycket sv�r h�rselneds�ttning") +
						(oldValues.Contains("Q1702O447") ? "&V1=" + oldValues["Q1702O447"] : "") +
						(oldValues.Contains("Q1703O447") ? "&V2=" + oldValues["Q1703O447"] : "") +
						(oldValues.Contains("Q1705O447") ? "&V3=" + oldValues["Q1705O447"] : "") +
						(oldValues.Contains("Q1706O447") ? "&V4=" + oldValues["Q1706O447"] : "") +
						(oldValues.Contains("Q1707O447") ? "&V5=" + oldValues["Q1707O447"] : "") +
						(oldValues.Contains("Q1708O447") ? "&V6=" + oldValues["Q1708O447"] : "") +
						"&A=" + Server.UrlEncode("Ljudniv� (dB)") + "&D1=500 Hz&D2=1000 Hz&D3=2000 Hz&D4=4000 Hz&D5=6000 Hz&D6=8000 Hz\"><BR>" +
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
						feedback = "H�rseltestet visar att du har en mycket sv�r h�rselneds�ttning p� f�ljande frekvenser i ditt v�nstra �ra: " + db5 + ". ";
						string rest = db4;
						if(db3 != "") { rest += (rest != "" ? "," : "") + db3; }
						if(db2 != "") { rest += (rest != "" ? "," : "") + db2; }
						if(db1 != "") { rest += (rest != "" ? "," : "") + db1; }
						if(rest != "") { feedback += "Du har �ven h�rselneds�ttning p� f�ljande frekvenser: " + rest + ". "; }
						else if(db0 != "") { feedback += "H�rseln �r normal p� �vriga frekvenser. "; }
						feedback += "Du har f�rmodligen sv�rt att uppfatta ljud i din omgivning med ditt v�nstra �ra. Ett mer djupg�ende h�rseltest rekommenderas.";
					}
					else if(db4 != "")
					{
						feedback = "H�rseltestet visar att du har en sv�r h�rselneds�ttning p� f�ljande frekvenser i ditt v�nstra �ra: " + db4 + ". ";
						string rest = db3;
						if(db2 != "") { rest += (rest != "" ? "," : "") + db2; }
						if(db1 != "") { rest += (rest != "" ? "," : "") + db1; }
						if(rest != "") { feedback += "Du har �ven h�rselneds�ttning p� f�ljande frekvenser: " + rest + ". "; }
						else if(db0 != "") { feedback += "H�rseln �r normal p� �vriga frekvenser. "; }
						feedback += "Du har f�rmodligen sv�rt att uppfatta m�nga ljud i din omgivning med ditt v�nstra �ra. Ett mer djupg�ende h�rseltest rekommenderas.";
					}
					else if(db3 != "")
					{
						feedback = "H�rseltestet visar att du har en m�ttligt sv�r h�rselneds�ttning p� f�ljande frekvenser i ditt v�nstra �ra: " + db3 + ". ";
						string rest = db2;
						if(db1 != "") { rest += (rest != "" ? "," : "") + db1; }
						if(rest != "") { feedback += "Du har �ven h�rselneds�ttning p� f�ljande frekvenser: " + rest + ". "; }
						else if(db0 != "") { feedback += "H�rseln �r normal p� �vriga frekvenser. "; }
						feedback += "Du har f�rmodligen sv�rt att uppfatta vissa ljud i din omgivning med ditt v�nstra �ra. Ett mer djupg�ende h�rseltest rekommenderas.";
					}
					else if(db2 != "")
					{
						feedback = "H�rseltestet visar att du har en m�ttlig h�rselneds�ttning p� f�ljande frekvenser i ditt v�nstra �ra: " + db2 + ". Det betyder att du har sv�rt att uppfatta ljud inom " + db2 + ". ";
						string rest = db1;
						if(rest != "") { feedback += "Du har �ven h�rselneds�ttning p� f�ljande frekvenser: " + rest + ". "; }
						else if(db0 != "") { feedback += "H�rseln �r normal p� �vriga frekvenser. "; }
						feedback += "Du har f�rmodligen sv�rt att uppfatta vissa ljud i din omgivning med ditt v�nstra �ra. Ett mer djupg�ende h�rseltest rekommenderas.";
					}
					else if(db1 != "")
					{
						feedback = "H�rseltestet visar att du har en l�tt h�rselneds�ttning p� f�ljande frekvenser i ditt v�nstra �ra: " + db1 + ". Det betyder att du har viss sv�righet att uppfatta ljud inom " + db1 + ". ";
						if(db0 != "") { feedback += "H�rseln �r normal p� �vriga frekvenser. "; }
						feedback += "Du har f�rmodligen sv�rt att uppfatta vissa ljud i din omgivning med ditt v�nstra �ra. Ett mer djupg�ende h�rseltest rekommenderas.";
					}
					else
					{
						feedback = "H�rseltestet visar att du har normal h�rsel p� v�nster �ra. Det inneb�r att du kan uppfatta ljud inom ett brett frekvensomr�de som omfattar b�de l�g- och h�gfrekventa ljud). Du kan dessutom h�ra dessa frekvenser p� en svag ljudniv� vilket tyder p� bra h�rsel.";
					}
					crr("",feedback,"<BR>",crid);
				}

				if(oldValues.Contains("Q1702O445") || oldValues.Contains("Q1703O445") || oldValues.Contains("Q1705O445") || oldValues.Contains("Q1706O445") || oldValues.Contains("Q1707O445") || oldValues.Contains("Q1708O445"))
				{
					crr(""+
						"<b style=\"page-break-before:always;\">Audiometri, H�ger �ra</b><br><IMG SRC=\"" + System.Configuration.ConfigurationSettings.AppSettings["InstanceURL"] + "/graphSixGreenRed.aspx?X=Frekvens" +
						"&L1=" + Server.UrlEncode("Normal h�rsel") + 
						"&L2=" + Server.UrlEncode("L�tt h�rselneds�ttning") +
						"&L3=" + Server.UrlEncode("M�ttlig h�rselneds�ttning") +
						"&L4=" + Server.UrlEncode("M�ttligt sv�r h�rselneds�ttning") +
						"&L5=" + Server.UrlEncode("Sv�r h�rselneds�ttning") +
						"&L6=" + Server.UrlEncode("Mycket sv�r h�rselneds�ttning") +
						(oldValues.Contains("Q1702O445") ? "&V1=" + oldValues["Q1702O445"] : "") +
						(oldValues.Contains("Q1703O445") ? "&V2=" + oldValues["Q1703O445"] : "") +
						(oldValues.Contains("Q1705O445") ? "&V3=" + oldValues["Q1705O445"] : "") +
						(oldValues.Contains("Q1706O445") ? "&V4=" + oldValues["Q1706O445"] : "") +
						(oldValues.Contains("Q1707O445") ? "&V5=" + oldValues["Q1707O445"] : "") +
						(oldValues.Contains("Q1708O445") ? "&V6=" + oldValues["Q1708O445"] : "") +
						"&A=" + Server.UrlEncode("Ljudniv� (dB)") + "&D1=500 Hz&D2=1000 Hz&D3=2000 Hz&D4=4000 Hz&D5=6000 Hz&D6=8000 Hz\"><BR>" +
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
						feedback = "H�rseltestet visar att du har en mycket sv�r h�rselneds�ttning p� f�ljande frekvenser i ditt h�gra �ra: " + db5 + ". ";
						string rest = db4;
						if(db3 != "") { rest += (rest != "" ? "," : "") + db3; }
						if(db2 != "") { rest += (rest != "" ? "," : "") + db2; }
						if(db1 != "") { rest += (rest != "" ? "," : "") + db1; }
						if(rest != "") { feedback += "Du har �ven h�rselneds�ttning p� f�ljande frekvenser: " + rest + ". "; }
						else if(db0 != "") { feedback += "H�rseln �r normal p� �vriga frekvenser. "; }
						feedback += "Du har f�rmodligen sv�rt att uppfatta ljud i din omgivning med ditt h�gra �ra. Ett mer djupg�ende h�rseltest rekommenderas.";
					}
					else if(db4 != "")
					{
						feedback = "H�rseltestet visar att du har en sv�r h�rselneds�ttning p� f�ljande frekvenser i ditt h�gra �ra: " + db4 + ". ";
						string rest = db3;
						if(db2 != "") { rest += (rest != "" ? "," : "") + db2; }
						if(db1 != "") { rest += (rest != "" ? "," : "") + db1; }
						if(rest != "") { feedback += "Du har �ven h�rselneds�ttning p� f�ljande frekvenser: " + rest + ". "; }
						else if(db0 != "") { feedback += "H�rseln �r normal p� �vriga frekvenser. "; }
						feedback += "Du har f�rmodligen sv�rt att uppfatta m�nga ljud i din omgivning med ditt h�gra �ra. Ett mer djupg�ende h�rseltest rekommenderas.";
					}
					else if(db3 != "")
					{
						feedback = "H�rseltestet visar att du har en m�ttligt sv�r h�rselneds�ttning p� f�ljande frekvenser i ditt h�gra �ra: " + db3 + ". ";
						string rest = db2;
						if(db1 != "") { rest += (rest != "" ? "," : "") + db1; }
						if(rest != "") { feedback += "Du har �ven h�rselneds�ttning p� f�ljande frekvenser: " + rest + ". "; }
						else if(db0 != "") { feedback += "H�rseln �r normal p� �vriga frekvenser. "; }
						feedback += "Du har f�rmodligen sv�rt att uppfatta vissa ljud i din omgivning med ditt h�gra �ra. Ett mer djupg�ende h�rseltest rekommenderas.";
					}
					else if(db2 != "")
					{
						feedback = "H�rseltestet visar att du har en m�ttlig h�rselneds�ttning p� f�ljande frekvenser i ditt h�gra �ra: " + db2 + ". Det betyder att du har sv�rt att uppfatta ljud inom " + db2 + ". ";
						string rest = db1;
						if(rest != "") { feedback += "Du har �ven h�rselneds�ttning p� f�ljande frekvenser: " + rest + ". "; }
						else if(db0 != "") { feedback += "H�rseln �r normal p� �vriga frekvenser. "; }
						feedback += "Du har f�rmodligen sv�rt att uppfatta vissa ljud i din omgivning med ditt h�gra �ra. Ett mer djupg�ende h�rseltest rekommenderas.";
					}
					else if(db1 != "")
					{
						feedback = "H�rseltestet visar att du har en l�tt h�rselneds�ttning p� f�ljande frekvenser i ditt h�gra �ra: " + db1 + ". Det betyder att du har viss sv�righet att uppfatta ljud inom " + db1 + ". ";
						if(db0 != "") { feedback += "H�rseln �r normal p� �vriga frekvenser. "; }
						feedback += "Du har f�rmodligen sv�rt att uppfatta vissa ljud i din omgivning med ditt h�gra �ra. Ett mer djupg�ende h�rseltest rekommenderas.";
					}
					else
					{
						feedback = "H�rseltestet visar att du har normal h�rsel p� h�ger �ra. Det inneb�r att du kan uppfatta ljud inom ett brett frekvensomr�de som omfattar b�de l�g- och h�gfrekventa ljud). Du kan dessutom h�ra dessa frekvenser p� en svag ljudniv� vilket tyder p� bra h�rsel.";
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

					crr("","Tal-i-brustestet �terspeglar din f�rm�ga att uppfatta tal i en stimmig milj�. Ditt resultat finner du i tabellen nedan.","<br/><br/>",crid);
					crr("" +
						"<table border=\"0\" style=\"border:1px solid #000000;cellpadding=\"5\" cellspacing=\"0\">" +
						//"<tr><td><B>�ra</B></td><td><b>Resultat 1</b></td><td><b>Resultat 2</b></td></tr>" +
						"<tr><td><B>�ra</B></td><td><b>Resultat</b></td></tr>" +
						"<tr>" +
						"<td>V�nster</td>" +
						"<td>" + (v1 > float.MinValue ? v1.ToString() : "") + "</td>" +
						//"<td>" + (v2 > float.MinValue ? v2.ToString() : "")  + "</td>" +
						"</tr>" +
						"<tr>" +
						"<td>H�ger</td>" +
						"<td>" + (h1 > float.MinValue ? h1.ToString() : "")  + "</td>" +
						//"<td>" + (h2 > float.MinValue ? h2.ToString() : "")  + "</td>" +
						"</tr>" +
						"</table><br/>","" +
						(v1 < 7 && v1 > float.MinValue || v2 < 7 && v2 > float.MinValue || h1 < 7 && h1 > float.MinValue || h2 < 7 && h2 > float.MinValue ? "Resultat mellan -24 och + 6 anses vara ett bra s� kallat tal-i-brusv�rde. Det betyder att du kan uppfatta tal n�r bakgrundsljudet (dvs bruset) �r minst 7 dB starkare �n talniv�n.<br>" : "") +
						(v1 >= 7 && v1 <= 12 || v2 >= 7 && v2 <= 12 || h1 >= 7 && h1 <= 12 || h2 >= 7 && h2 <= 12 ? "Resultat mellan +7 och +12 inneb�r att du f�rmodligen har vissa sv�righeter att uppfatta tal i stimmiga milj�er. Ett mer djupg�ende h�rseltest hos audionom, f�retagssjuksk�terska eller �ronspecialist rekommenderas f�r att du ska kunna f� en mer fullst�ndig bed�mning av din h�rsel.<br>" : "") +
						(v1 > 12 || v2 > 12 || h1 > 12 || h2 > 12 ? "Ett resultat som �r st�rre �n +12 antyder ett d�ligt tal-i-brus v�rde. Du har f�rmodligen sv�rt att uppfatta tal �ven d� bakgrundsljudet �r l�gt. Ett mer djupg�ende h�rseltest hos audionom, f�retagssjuksk�terska eller �ronspecialist rekommenderas f�r att du ska kunna f� en mer fullst�ndig bed�mning av din h�rsel.<br>" : "") +
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
