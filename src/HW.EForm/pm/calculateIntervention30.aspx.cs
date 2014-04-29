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
	/// Summary description for calculateIntervention30.
	/// </summary>
	public class calculateIntervention30 : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			OdbcDataReader rs, rs2;

			int cx = 0;
			int c = 0;
			int c1 = 0, c2 = 0, c3 = 0, c4 = 0, c5 = 0, c6 = 0, c7 = 0, c8 = 0, c9 = 0, c10 = 0;
			
			rs = Db.recordSet("" +
				"SELECT " +
				"REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-',''), " +
				"a.AnswerID, " +
				"u.Email, " +
				"u.ProjectRoundUserID " +
				"FROM ProjectRoundUser u " +
				"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
				"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
				"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
				"WHERE p.ProjectRoundID = 30 AND a.EndDT IS NOT NULL ORDER BY a.EndDT");
			while(rs.Read())
			{
				cx++;

				int i1 = 0, i2 = 0, i3 = 0, i4 = 0, i5 = 0, i6 = 0, i7 = 0, i8 = 0, i9 = 0, i10 = 0;

				#region regen
					System.Collections.Hashtable oldValues = new System.Collections.Hashtable();
					System.Text.StringBuilder FB = new System.Text.StringBuilder();

					rs2 = Db.recordSet("SELECT " +
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
						i1++;
					}
					#endregion

					#region Smoke/Hosta/Pip
					bool Qhosta = oldValues.Contains("Q349O90") && Convert.ToInt32("0" + (string)oldValues["Q349O90"]) == 294;
					bool Qpip = oldValues.Contains("Q350O90") && Convert.ToInt32("0" + (string)oldValues["Q350O90"]) == 294;
					bool Qsmoke = oldValues.Contains("Q370O90") && Convert.ToInt32("0" + (string)oldValues["Q370O90"]) == 294;

					if(Qsmoke)
					{
						i2++;
					}
					else if(Qpip || Qhosta)
					{
						i3++;
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
						i4++;
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
						i5++;
					}
					else if(Qdepr)
					{
						i6++;
					}
					#endregion

					#region Sleep
					if(oldValues.Contains("Q374O86"))
					{
						bool QsleepI = false, QpreviaI = false;

						switch(Convert.ToInt32("0" + (string)oldValues["Q374O86"]))
						{
							case 310: FB.Append("Du har angivit att din sömn är Bra, vilket tyder på att din sömn och återhämtning är god; att du generellt sett somnar lätt, sover gott och känner dig utvilad när du vaknar.<BR>"); break;
							case 311: FB.Append("Du har angivit att din sömn är Ganska bra, vilket tyder på att din sömn och återhämtning är god; att du generellt sett somnar lätt, sover gott och känner dig utvilad när du vaknar.<BR>"); break;
							case 312: QsleepI = true; FB.Append("Du har angivit att din sömn är varken är bra eller dålig. Om du upplever att du återkommande har svårt att somna, sova gott eller inte är utvilad när du vaknar finns det anledning att förbättra din sömn för att på sikt bevara hälsa och välbefinnande. Det är i sådana fall viktigt att tänka på att varva ned minst två timmar före sänggåendet. Man kan dessutom förbättra sömnkvaliteten genom att regelbundet genomföra andnings- och avslappningsövningar före sänggåendet."); break;
							case 313: QsleepI = true; QpreviaI = true; FB.Append("Du har angivit att din sömn är Ganska dålig, vilket generellt sett speglar något/några av följande symtom: svårigheter att somna, dålig sömnkvalitet, trötthet vid uppvaknande och att man inte känner sig utvilad. Dessa symtom signalerar ett tydligt behov av att se över sömnen. Det är helt normalt att vid enstaka tillfällen eller under kortare perioder sova dåligt av olika skäl, men om du haft sömnbesvär en längre tid behöver dessa signaler tas på allvar. Du kan börja med att reflektera över om det finns uppenbara anledningar till att du sover dåligt. Om du är osäker på hur du ska hantera dina sömnproblem rekommenderar vi dig att kontakta din husläkare. Är dina problem arbetsrelaterade kan du även kontakta Previa, företagshälsovården."); break;
							case 314: QsleepI = true; QpreviaI = true; FB.Append("Du har angivit att din sömn är Dålig, vilket generellt sett speglar något/några av följande symtom: svårigheter att somna, dålig sömnkvalitet, trötthet vid uppvaknande och att man inte känner sig utvilad. Dessa symtom signalerar ett tydligt behov av att se över sömnen. Det är helt normalt att vid enstaka tillfällen eller under kortare perioder sova dåligt av olika skäl, men om du haft sömnbesvär en längre tid behöver dessa signaler tas på allvar. Du kan börja med att reflektera över om det finns uppenbara anledningar till att du sover dåligt. Om du är osäker på hur du ska hantera dina sömnproblem rekommenderar vi dig att kontakta din husläkare. Är dina problem arbetsrelaterade kan du även kontakta Previa, företagshälsovården."); break;
						}
						if(QsleepI)
						{
							i7++;
						}
					}
					#endregion

					#region Health
					if(oldValues.Contains("Q331O98"))
					{
						bool QpreviaI = false;

						switch(Convert.ToInt32("0" + (string)oldValues["Q331O98"]))
						{
							case 310: FB.Append("Du har angivit att din hälsa är Bra och verkar således må ganska bra. Personer med din skattning har störst sannolikhet att bevara eller öka sin hälsa i framtiden."); break;
							case 311: FB.Append("Du har angivit att din hälsa är Ganska bra och verkar således må ganska bra. Personer med din skattning har störst sannolikhet att bevara eller öka sin hälsa i framtiden."); break;
							case 315: FB.Append("Du har angivit att din hälsa varken är bra eller dålig, vilket betyder att det kan finnas en risk för att din hälsa försämras ytterligare i framtiden. Eftersom självskattad hälsa är ett sammanfattande mått på både fysiskt och mentalt välbefinnande rekommenderar vi att du ser över din livsstil, till exempel vad gäller motion, sömn, och matvanor."); break;
							case 316: QpreviaI = true; FB.Append("Du har angivit att din hälsa är Ganska dålig. Om det inte är fråga om en tillfällig nedgång i hur du mår, och om du inte redan har läkarkontakt, rekommenderar vi att du kontaktar din husläkare för en diskussion av ditt hälsotillstånd och vad som eventuellt kan göras för att förbättra det. Är dina problem arbetsrelaterade kan du även kontakta företagshälsovården, Previa."); break;
							case 317: QpreviaI = true; FB.Append("Du har angivit att din hälsa är Dålig. Om det inte är fråga om en tillfällig nedgång i hur du mår, och om du inte redan har läkarkontakt, rekommenderar vi att du kontaktar din husläkare för en diskussion av ditt hälsotillstånd och vad som eventuellt kan göras för att förbättra det. Är dina problem arbetsrelaterade kan du även kontakta företagshälsovården, Previa."); break;
						}
						
						if(QpreviaI)
						{
							i8++;
						}
					}
					#endregion


					double Qbmi = 0;
					if(
						oldValues.Contains("Q314O83") && (string)oldValues["Q314O83"] != ""	// weight
						&&
						oldValues.Contains("Q313O82") && (string)oldValues["Q313O82"] != ""	// height
						)
					{
						Qbmi = Convert.ToDouble(submit.strFloatToStr((string)oldValues["Q314O83"])) / (Convert.ToDouble(submit.strFloatToStr((string)oldValues["Q313O82"]))/100 * Convert.ToDouble(submit.strFloatToStr((string)oldValues["Q313O82"]))/100);
					}

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
		
						//double Qbmi = Convert.ToDouble(strFloatToStr((string)oldValues["Q314O83"])) / (Convert.ToDouble(strFloatToStr((string)oldValues["Q313O82"]))/100 * Convert.ToDouble(strFloatToStr((string)oldValues["Q313O82"]))/100);
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

						if(Qscore >= 12)
						{
							i9++;
						}
					}
					#endregion

					if(!Qalco && !Qdepr && !Qburnout && Qscore < 12)
					{
						i10++;
					}
				#endregion

				if(i1 > 0 || i2 > 0 || i3 > 0 || i4 > 0 || i5 > 0 || i6 > 0 || i7 > 0 || i8 > 0 || i9 > 0)
				{
					c++;
				}
				if(i1 > 0) {c1++;}
				if(i2 > 0) {c2++;}
				if(i3 > 0) {c3++;}
				if(i4 > 0) {c4++;}
				if(i5 > 0) {c5++;}
				if(i6 > 0) {c6++;}
				if(i7 > 0) {c7++;}
				if(i8 > 0) {c8++;}
				if(i9 > 0) {c9++;}
				if(i10 > 0) {c10++;}
			}
			rs.Close();

			HttpContext.Current.Response.Write("" +
				"Rygg/nackproblem: " + c1 + "<br/>" +
				"Rökning " + c2 + "<br/>" +
				"Pip/hosta: " + c3 + "<br/>" +
				"Alkohol: " + c4 + "<br/>" +
				"Utbrändhet: " + c5 + "<br/>" +
				"Depression: " + c6 + "<br/>" +
				"Sömnproblem: " + c7 + "<br/>" +
				"Hälsoproblem: " + c8 + "<br/>" +
				"Diabetesrisk: " + c9 + "<br/>" +
				"Friskvård: " + c10 + "<br/>" +
				"Totalt: " + cx + "<br/>" +
				"Total intervention: " + c + "<br/>");
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
