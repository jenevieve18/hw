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
	/// Summary description for reportImageStatic.
	/// </summary>
	public class reportImageStatic : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Graph g = null;

			if(HttpContext.Current.Request.QueryString["AK"] != null)
			{
				#region User-level

				int surveyID = (HttpContext.Current.Request.QueryString["SID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["SID"]) : 0);
				int answerID = 0;
				int langID = 0;
				int projectRoundUserID = 0;
				OdbcDataReader rs = Db.recordSet("SELECT a.AnswerID, dbo.cf_unitLangID(a.ProjectRoundUnitID), a.ProjectRoundUserID FROM Answer a WHERE REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-','') = '" + HttpContext.Current.Request.QueryString["AK"] + "'");
				if(rs.Read())
				{
					answerID = rs.GetInt32(0);
					langID = rs.GetInt32(1);
					projectRoundUserID = (rs.IsDBNull(2) ? 0 : rs.GetInt32(2));
				}
				rs.Close();

				if(surveyID == 13)
				{
					#region DUDIT

					g = new Graph(550,360,"#EFEFEF");

					int cx = 0;

					rs = Db.recordSet("SELECT COUNT(*) FROM Answer a WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = " + projectRoundUserID);
					if(rs.Read())
					{
						cx = Convert.ToInt32(rs.GetValue(0))+2;
					}
					rs.Close();
					if(cx > 8)
					{
						cx = 8;
					}

					g.setMinMax(-1f,100f);
					//g.leftSpacing = 100;
					g.computeSteping(cx);

					bool gender = false;
					decimal age = 0;
					//float green = 3f;
					//string refgr = "";
					rs = Db.recordSet("SELECT TOP 1 av.ValueDecimal FROM Answer a INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID WHERE a.ProjectRoundUserID = " + projectRoundUserID + " AND av.QuestionID = 291 ORDER BY a.EndDT DESC");
					if(rs.Read() && !rs.IsDBNull(0))
					{
						age = rs.GetDecimal(0);
					}
					rs.Close();

					rs = Db.recordSet("SELECT TOP 1 av.ValueInt FROM Answer a INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID WHERE a.ProjectRoundUserID = " + projectRoundUserID + " AND av.QuestionID = 290 ORDER BY a.EndDT DESC");
					if(rs.Read() && !rs.IsDBNull(0))
					{
						gender = (rs.GetInt32(0) == 139);
					}
					rs.Close();

					/*if(age >= 16 && age <= 25)
					{
						refgr = "Risk, kvinnor 16-25 år";
						green = 3f;
						if(gender)
						{
							refgr = "Risk, män 16-25 år";
							green = 7f;
						}
					}
					else
					{
						if(age < 16)
						{
							refgr = "Risk, kvinnor < 16 år";
						}
						else
						{
							refgr = "Risk, kvinnor > 25 år";
						}
						green = 2f;
						if(gender)
						{
							if(age < 16)
							{
								refgr = "Risk, män < 16 år";
							}
							else
							{
								refgr = "Risk, män > 25 år";
							}
							green = 6f;
						}
					}*/

					//g.drawBg(0f,green,16);
					//g.drawBg(green,25f,17);
					//g.drawBg(25f,44f,18);

					g.drawBg(0.00f,0.05f,"33FF33");
					g.drawBg(0.05f,0.10f,"44FF33");
					g.drawBg(0.10f,0.15f,"55FF33");
					g.drawBg(0.15f,0.20f,"66FF33");
					g.drawBg(0.20f,0.25f,"77FF33");
					g.drawBg(0.25f,0.30f,"88FF33");
					g.drawBg(0.30f,0.35f,"99FF33");
					g.drawBg(0.35f,0.40f,"AAFF33");
					g.drawBg(0.40f,0.45f,"BBFF33");
					g.drawBg(0.45f,0.50f,"CCFF33");
					g.drawBg(0.50f,0.55f,"DDFF33");
					g.drawBg(0.55f,0.60f,"EEFF33");
					g.drawBg(0.60f,0.65f,"FFFF33");
					g.drawBg(0.65f,0.70f,"FFEE33");
					g.drawBg(0.70f,0.75f,"FFCC33");
					g.drawBg(0.75f,0.80f,"FFAA33");
					g.drawBg(0.80f,0.85f,"FF8833");
					g.drawBg(0.85f,0.90f,"FF6633");
					g.drawBg(0.90f,0.95f,"FF4433");
					g.drawBg(0.95f,1.00f,"FF3333");

					//g.drawOutlines(12,false);
					//g.drawOutlineAt("Referensgrupp 1",33);
					//g.drawOutlineAt("Referensgrupp 2",17);
					//g.drawOutlineAt(refgr,green);
					g.drawAxisExpl("Din poäng",0,false,false);
					g.drawAxis(false);
					g.drawColorExplBox("Låg eller ingen risk",16,120,15);
					g.drawColorExplBox("Risk",17,270,15);
					g.drawColorExplBox("Drogrelaterade problem",18,350,15);

					int bx = 0;
					if(cx < 8)
					{
						rs = Db.recordSet("SELECT a.AnswerID, a.EndDT FROM Answer a WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = " + projectRoundUserID + " ORDER BY a.EndDT");
						while(rs.Read())
						{
							drawDUDITscores(rs.GetInt32(0).ToString(),rs.GetDateTime(1).ToString("yyyy-MM-dd"),ref bx, ref g);
						}
						rs.Close();
					}
					else
					{
						string answerIDs = "0";
						rs = Db.recordSet("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(yyyy,-1,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID);
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawDUDITscores(answerIDs,"Senaste\nåret",ref bx, ref g);

						answerIDs = "0";
						rs = Db.recordSet("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(mm,-6,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID);
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawDUDITscores(answerIDs,"Senaste\nhalvåret",ref bx, ref g);
						
						answerIDs = "0";
						rs = Db.recordSet("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(mm,-3,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID);
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawDUDITscores(answerIDs,"Senaste\nkvartalet",ref bx, ref g);

						answerIDs = "0";
						rs = Db.recordSet("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(mm,-1,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID);
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawDUDITscores(answerIDs,"Senaste\nmånaden",ref bx, ref g);

						answerIDs = "0";
						rs = Db.recordSet("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(wk,-1,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID);
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawDUDITscores(answerIDs,"Senaste\nveckan",ref bx, ref g);

						rs = Db.recordSet("SELECT a.AnswerID, a.EndDT FROM Answer a WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = " + projectRoundUserID + " ORDER BY a.EndDT DESC");
						if(rs.Read())
						{
							drawDUDITscores(rs.GetInt32(0).ToString(),"Denna\nmätning",ref bx, ref g);
						}
						rs.Close();
					}
					#endregion
				}
				else if(surveyID == 10)
				{
					#region AUDIT

					g = new Graph(550,360,"#EFEFEF");
					
					int cx = 0;

					rs = Db.recordSet("SELECT COUNT(*) FROM Answer a WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = " + projectRoundUserID);
					if(rs.Read())
					{
						cx = Convert.ToInt32(rs.GetValue(0))+2;
					}
					rs.Close();
					if(cx>7)
					{
						cx = 7;
					}

					g.setMinMax(-1f,100f);
					g.leftSpacing = 100;
					g.computeSteping(cx);

					bool gender = false;
					decimal age = 0;
					//float green = 3f;
					//string refgr = "";
					rs = Db.recordSet("SELECT TOP 1 av.ValueDecimal FROM Answer a INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID WHERE a.ProjectRoundUserID = " + projectRoundUserID + " AND av.QuestionID = 201 ORDER BY a.EndDT DESC");
					if(rs.Read() && !rs.IsDBNull(0))
					{
						age = rs.GetDecimal(0);
					}
					rs.Close();

					rs = Db.recordSet("SELECT TOP 1 av.ValueInt FROM Answer a INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID WHERE a.ProjectRoundUserID = " + projectRoundUserID + " AND av.QuestionID = 214 ORDER BY a.EndDT DESC");
					if(rs.Read() && !rs.IsDBNull(0))
					{
						gender = (rs.GetInt32(0) == 139);
					}
					rs.Close();

					/*if(age >= 16 && age <= 25)
					{
						refgr = "Risk, kvinnor 16-25 år";
						green = 3f;
						if(gender)
						{
							refgr = "Risk, män 16-25 år";
							green = 7f;
						}
					}
					else
					{
						if(age < 16)
						{
							refgr = "Risk, kvinnor < 16 år";
						}
						else
						{
							refgr = "Risk, kvinnor > 25 år";
						}
						green = 2f;
						if(gender)
						{
							if(age < 16)
							{
								refgr = "Risk, män < 16 år";
							}
							else
							{
								refgr = "Risk, män > 25 år";
							}
							green = 6f;
						}
					}*/

					//g.drawBg(0f,green,16);
					//g.drawBg(green,25f,17);
					//g.drawBg(25f,40f,18);
					g.drawBg(0.00f,0.05f,"33FF33");
					g.drawBg(0.05f,0.10f,"44FF33");
					g.drawBg(0.10f,0.15f,"55FF33");
					g.drawBg(0.15f,0.20f,"66FF33");
					g.drawBg(0.20f,0.25f,"77FF33");
					g.drawBg(0.25f,0.30f,"88FF33");
					g.drawBg(0.30f,0.35f,"99FF33");
					g.drawBg(0.35f,0.40f,"AAFF33");
					g.drawBg(0.40f,0.45f,"BBFF33");
					g.drawBg(0.45f,0.50f,"CCFF33");
					g.drawBg(0.50f,0.55f,"DDFF33");
					g.drawBg(0.55f,0.60f,"EEFF33");
					g.drawBg(0.60f,0.65f,"FFFF33");
					g.drawBg(0.65f,0.70f,"FFEE33");
					g.drawBg(0.70f,0.75f,"FFCC33");
					g.drawBg(0.75f,0.80f,"FFAA33");
					g.drawBg(0.80f,0.85f,"FF8833");
					g.drawBg(0.85f,0.90f,"FF6633");
					g.drawBg(0.90f,0.95f,"FF4433");
					g.drawBg(0.95f,1.00f,"FF3333");
					//g.drawOutlines(12,false);
					//g.drawOutlineAt("Referensgrupp 1",33);
					//g.drawOutlineAt("Referensgrupp 2",17);
					//g.drawOutlineAt(refgr,green);
					g.drawCircleOutlineAt("1", "33FF33",0f,0);
					//g.drawColorExplCircle("1", "cirka 12 procent av befolkningen ligger på den allra lägsta risknivån", "33FF33", 20, 350);
					g.drawCircleOutlineAt("2", "99FF33",34f,0);
					//g.drawColorExplCircle("2", "här ligger genomsnittspoängen för kvinnor mellan 17-27 år", "99FF33", 20, 370);
					g.drawCircleOutlineAt("3", "CCFF33",49.8f,0);
					//g.drawColorExplCircle("3", "här ligger genomsnittspoängen för män mellan 17-27 år", "CCFF33", 20, 390);
					g.drawCircleOutlineAt("4", "DDFF33",54f,1);
					//g.drawColorExplCircle("4", "bara cirka 5 procent av kvinnorna ligger över denna nivå", "DDFF33", 20, 410);
					g.drawCircleOutlineAt("5", "DDFF33",58f,2);
					//g.drawColorExplCircle("5", "bara cirka 10 procent av männen ligger över denna nivå", "DDFF33", 20, 430);
					g.drawCircleOutlineAt("6", "FFFF33",70f,0);
					//g.drawColorExplCircle("6", "bara cirka 5 procent av männen ligger över denna nivå", "FFFF33", 20, 450);
					g.drawCircleOutlineAt("7", "FFEE33",81f,0);
					//g.drawColorExplCircle("7", "bland kvinnor är det mindre än 1 procent som dricker mer än så här", "FFEE33", 20, 470);
					g.drawCircleOutlineAt("8", "FFCC33",85f,1);
					//g.drawColorExplCircle("8", "bland män är det mindre än 1 procent som dricker mer än så här", "FFCC33", 20, 490);
					g.drawAxisExpl("Jämförelsevärden\n(för muspekaren över ringarna)",0,false,false);
					g.drawAxis(false);
					g.drawColorExplBox("Låg eller ingen risk",16,200,15);
					g.drawColorExplBox("Risk",17,330,15);
					g.drawColorExplBox("Alkoholproblem",18,400,15);

					int bx = 0;

					if(cx < 7)
					{
						rs = Db.recordSet("SELECT a.AnswerID, a.EndDT FROM Answer a WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = " + projectRoundUserID + " ORDER BY a.EndDT");
						while(rs.Read())
						{
							drawAUDITscores(rs.GetInt32(0).ToString(),rs.GetDateTime(1).ToString("yyyy-MM-dd"),ref bx, ref g);
						}
						rs.Close();
					}
					else
					{
						string answerIDs = "0";
						rs = Db.recordSet("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(yyyy,-1,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID);
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawAUDITscores(answerIDs,"Senaste\nåret",ref bx, ref g);

						answerIDs = "0";
						rs = Db.recordSet("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(mm,-3,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID);
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawAUDITscores(answerIDs,"Senaste\nkvartalet",ref bx, ref g);

						answerIDs = "0";
						rs = Db.recordSet("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(mm,-1,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID);
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawAUDITscores(answerIDs,"Senaste\nmånaden",ref bx, ref g);

						answerIDs = "0";
						rs = Db.recordSet("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(wk,-1,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID);
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawAUDITscores(answerIDs,"Senaste\nveckan",ref bx, ref g);

						rs = Db.recordSet("SELECT a.AnswerID, a.EndDT FROM Answer a WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = " + projectRoundUserID + " ORDER BY a.EndDT DESC");
						if(rs.Read())
						{
							drawAUDITscores(rs.GetInt32(0).ToString(),"Denna\nmätning",ref bx, ref g);
						}
						rs.Close();

						
					}
					#endregion
				}
				#endregion

				g.render();
			}
		}

		private void drawDUDITscores(string answerIDs, string expl, ref int bx, ref Graph g)
		{
			double temp = 0;
			OdbcDataReader rs2 = Db.recordSet("SELECT " +
				"AVG(oc.ExportValue) " +
				"FROM AnswerValue av " +
				"INNER JOIN OptionComponents oc ON av.OptionID = oc.OptionID " +
				"AND oc.OptionComponentID = av.ValueInt " +
				"WHERE av.AnswerID IN (" + answerIDs + ") " +
				"AND av.QuestionID IN (292,293,294,295,296,297,298,299,300,301,302) " +
				"GROUP BY av.QuestionID");
			while(rs2.Read())
			{
				temp += (rs2.IsDBNull(0) ? 0d : Convert.ToDouble(rs2.GetValue(0)));
			}
			rs2.Close();

			int score = Convert.ToInt32(temp);
			if(score < 0)
				score = 0;
			else if(score > 44)
				score = 44;

			switch(score)
			{
				case 0: score = 0; break;
				case 1: score = 48; break;

				case 2: score = 50; break;
				case 3: score = 53; break;
				case 4: score = 57; break;
				case 5: score = 62; break;
				case 6: score = 68; break;
				case 7: score = 74; break;

				case 8: score = 75; break;
				case 9: score = 76; break;
				case 10: score = 77; break;
				case 11: score = 78; break;
				case 12: score = 79; break;
				case 13: score = 80; break;
				case 14: score = 81; break;
				case 15: score = 82; break;
				case 16: score = 83; break;
				case 17: score = 84; break;
				case 18: score = 85; break;
				case 19: score = 86; break;
				case 20: score = 87; break;
				case 21: score = 88; break;
				case 22: score = 90; break;
				case 23: score = 91; break;
				case 24: score = 92; break;
				case 25: score = 92; break;
				case 26: score = 93; break;
				case 27: score = 93; break;
				case 28: score = 94; break;
				case 29: score = 94; break;
				case 30: score = 95; break;
				case 31: score = 95; break;
				case 32: score = 96; break;
				case 33: score = 96; break;
				case 34: score = 97; break;
				case 35: score = 97; break;
				case 36: score = 98; break;
				case 37: score = 98; break;
				case 38: score = 98; break;
				case 39: score = 99; break;
				case 40: score = 99; break;
				case 41: score = 99; break;
				case 42: score = 100; break;
				case 43: score = 100; break;
				case 44: score = 100; break;
			}

			g.drawBar(5,++bx,(float)score,false,false);
			g.drawBottomString(expl,bx);
		}

		private void drawAUDITscores(string answerIDs, string expl, ref int bx, ref Graph g)
		{
			double temp = 0d;
			OdbcDataReader rs2 = Db.recordSet("SELECT AVG(oc.ExportValue) FROM AnswerValue av INNER JOIN OptionComponents oc ON av.OptionID = oc.OptionID AND oc.OptionComponentID = av.ValueInt WHERE av.AnswerID IN (" + answerIDs + ") AND av.QuestionID IN (204,205,206,207,208,209,210,211,212,213) GROUP BY av.QuestionID");
			while(rs2.Read())
			{
				temp += (rs2.IsDBNull(0) ? 0 : rs2.GetInt32(0));
			}
			rs2.Close();
            
			int score = Convert.ToInt32(temp);
			if(score < 0)
				score = 0;
			if(score > 40)
				score = 40;

			switch(score)
			{
				case 0: score = 0; break;
				case 1: score = 5; break;
				case 2: score = 10; break;
				case 3: score = 15; break;
				case 4: score = 20; break;
				case 5: score = 30; break;
				case 6: score = 40; break;
				case 7: score = 48; break;

				case 8: score = 50; break;
				case 9: score = 54; break;
				case 10: score = 58; break;
				case 11: score = 62; break;
				case 12: score = 66; break;
				case 13: score = 70; break;
				case 14: score = 74; break;

				case 15: score = 76; break;
				case 16: score = 77; break;
				case 17: score = 78; break;
				case 18: score = 79; break;
				case 19: score = 80; break;
				case 20: score = 81; break;
				case 21: score = 82; break;
				case 22: score = 83; break;
				case 23: score = 84; break;
				case 24: score = 85; break;
				case 25: score = 86; break;
				case 26: score = 87; break;
				case 27: score = 88; break;
				case 28: score = 90; break;
				case 29: score = 91; break;
				case 30: score = 92; break;
				case 31: score = 93; break;
				case 32: score = 94; break;
				case 33: score = 95; break;
				case 34: score = 96; break;
				case 35: score = 97; break;
				case 36: score = 98; break;
				case 37: score = 99; break;
				case 38: score = 99; break;
				case 39: score = 99; break;
				case 40: score = 100; break;
			}

			g.drawBar(5,++bx,(float)score,false,false);
			g.drawBottomString(expl,bx);
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
