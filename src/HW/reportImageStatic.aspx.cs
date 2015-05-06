using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace healthWatch
{
	/// <summary>
	/// Summary description for reportImageStatic.
	/// </summary>
	public partial class reportImageStatic : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Graph g = null;

			if(HttpContext.Current.Request.QueryString["AK"] != null)
			{
				#region User-level

				int surveyID = 0;
				int answerID = 0;
				int langID = 0;
				int projectRoundUserID = 0;
				SqlDataReader rs = Db.rs("SELECT a.AnswerID, dbo.cf_unitLangID(a.ProjectRoundUnitID), a.ProjectRoundUserID, dbo.cf_unitSurveyID(a.ProjectRoundUnitID) FROM Answer a WHERE REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-','') = '" + HttpContext.Current.Request.QueryString["AK"] + "'","eFormSqlConnection");
				if(rs.Read())
				{
					answerID = rs.GetInt32(0);
					langID = rs.GetInt32(1);
					projectRoundUserID = (rs.IsDBNull(2) ? 0 : rs.GetInt32(2));
					surveyID = rs.GetInt32(3);
				}
				rs.Close();

				if(surveyID == 13)
				{
					#region DUDIT

					//g = new Graph(580,360,"#EFEFEF");
					g = new Graph(580,380,"#FFFFFF");

					int cx = 0;

					rs = Db.rs("SELECT COUNT(*) FROM Answer a WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = " + projectRoundUserID,"eFormSqlConnection");
					if(rs.Read())
					{
						cx = Convert.ToInt32(rs.GetValue(0))+2;
					}
					rs.Close();
					if(1 == 0 && cx > 8)
					{
						cx = 8;
					}

					g.setMinMax(-1f,100f);
					//g.leftSpacing = 100;
					g.computeSteping(cx);

					#region obsolete
					/*
					bool gender = false;
					decimal age = 0;
					float green = 3f;
					string refgr = "";
					rs = Db.rs("SELECT TOP 1 av.ValueDecimal FROM Answer a INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID WHERE a.ProjectRoundUserID = " + projectRoundUserID + " AND av.QuestionID = 291 ORDER BY a.EndDT DESC","eFormSqlConnection");
					if(rs.Read() && !rs.IsDBNull(0))
					{
						age = rs.GetDecimal(0);
					}
					rs.Close();

					rs = Db.rs("SELECT TOP 1 av.ValueInt FROM Answer a INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID WHERE a.ProjectRoundUserID = " + projectRoundUserID + " AND av.QuestionID = 290 ORDER BY a.EndDT DESC","eFormSqlConnection");
					if(rs.Read() && !rs.IsDBNull(0))
					{
						gender = (rs.GetInt32(0) == 139);
					}
					rs.Close();

					if(age >= 16 && age <= 25)
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
					}
					*/
					//g.drawBg(0f,green,16);
					//g.drawBg(green,25f,17);
					//g.drawBg(25f,44f,18);
					#endregion

					g.drawBg(0.00f,0.05f,"33FF33");
					g.drawBg(0.05f,0.10f,"66FF33");
					g.drawBg(0.10f,0.15f,"99FF33");
					g.drawBg(0.15f,0.20f,"CCFF33");
					g.drawBg(0.20f,0.25f,"EEFF33");
					g.drawBg(0.25f,0.30f,"FFFF33");
					g.drawBg(0.30f,0.35f,"FFFF33");
					g.drawBg(0.35f,0.40f,"FFEE33");
					g.drawBg(0.40f,0.45f,"FFCC33");
					g.drawBg(0.45f,0.50f,"FFAA33");
					g.drawBg(0.50f,0.55f,"FF8833");
					g.drawBg(0.55f,0.60f,"FF6633");
					g.drawBg(0.60f,0.65f,"FF4433");
					g.drawBg(0.65f,0.70f,"FF3333");
					g.drawBg(0.70f,0.75f,"FF3333");
					g.drawBg(0.75f,0.80f,"FF3333");
					g.drawBg(0.80f,0.85f,"FF3333");
					g.drawBg(0.85f,0.90f,"FF3333");
					g.drawBg(0.90f,0.95f,"FF3333");
					g.drawBg(0.95f,1.00f,"FF3333");

					//g.drawOutlines(12,false);
					//g.drawOutlineAt("Referensgrupp 1",33);
					//g.drawOutlineAt("Referensgrupp 2",17);
					//g.drawOutlineAt(refgr,green);
					//g.drawAxisExpl("Din poäng",0,false,false);

					g.drawCircleOutlineAt("", "33FF33",0f,0);
					g.drawCircleOutlineAt("", "99FF33",12f,0);
					g.drawCircleOutlineAt("", "FFFF33",30f,0);
					g.drawCircleOutlineAt("", "FFCC33",45f,0);
					g.drawCircleOutlineAt("", "FF3333",65f,0);

					g.drawAxisExpl("Jämförelsevärden\n(för muspekaren över ringarna)",0,false,false);

					g.drawAxis(false);
					g.drawColorExplBox("Låg eller ingen risk",16,200,15);
					g.drawColorExplBox("Risk",17,330,15);
					g.drawColorExplBox("Drogrelaterade problem",18,400,15);

					int bx = 0;
					if(1 == 1 || cx < 8)
					{
						float lastVal = -1f;
						rs = Db.rs("SELECT a.AnswerID, a.EndDT FROM Answer a WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = " + projectRoundUserID + " ORDER BY a.EndDT","eFormSqlConnection");
						while(rs.Read())
						{
							drawDUDITscores(rs.GetInt32(0).ToString(),rs.GetDateTime(1).ToString("yyyy-MM-dd"),ref bx, ref g, (answerID == rs.GetInt32(0) && HttpContext.Current.Request.QueryString["T"] == "1"), ref lastVal);
						}
						rs.Close();
					}
					else
					{
						/*string answerIDs = "0";
						rs = Db.rs("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(yyyy,-1,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID,"eFormSqlConnection");
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawDUDITscores(answerIDs,"Senaste\nåret",ref bx, ref g);

						answerIDs = "0";
						rs = Db.rs("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(mm,-6,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID,"eFormSqlConnection");
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawDUDITscores(answerIDs,"Senaste\nhalvåret",ref bx, ref g);
						
						answerIDs = "0";
						rs = Db.rs("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(mm,-3,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID,"eFormSqlConnection");
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawDUDITscores(answerIDs,"Senaste\nkvartalet",ref bx, ref g);

						answerIDs = "0";
						rs = Db.rs("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(mm,-1,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID,"eFormSqlConnection");
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawDUDITscores(answerIDs,"Senaste\nmånaden",ref bx, ref g);

						answerIDs = "0";
						rs = Db.rs("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(wk,-1,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID,"eFormSqlConnection");
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawDUDITscores(answerIDs,"Senaste\nveckan",ref bx, ref g);

						rs = Db.rs("SELECT a.AnswerID, a.EndDT FROM Answer a WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = " + projectRoundUserID + " ORDER BY a.EndDT DESC","eFormSqlConnection");
						if(rs.Read())
						{
							drawDUDITscores(rs.GetInt32(0).ToString(),"Denna\nmätning",ref bx, ref g);
						}
						rs.Close();*/
					}
					#endregion
				}
				else if(surveyID == 10)
				{
					#region AUDIT

					//g = new Graph(580,360,"#EFEFEF");
					g = new Graph(580,380,"#FFFFFF");
					
					int cx = 0;

					rs = Db.rs("SELECT COUNT(*) FROM Answer a WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = " + projectRoundUserID,"eFormSqlConnection");
					if(rs.Read())
					{
						cx = Convert.ToInt32(rs.GetValue(0))+2;
					}
					rs.Close();
					if(1 == 0 && cx>7)
					{
						cx = 7;
					}

					g.setMinMax(-1f,100f);
					g.leftSpacing = 100;
					g.computeSteping(cx);

					#region obsolete
					/*
					bool gender = false;
					decimal age = 0;
					float green = 3f;
					string refgr = "";
					rs = Db.rs("SELECT TOP 1 av.ValueDecimal FROM Answer a INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID WHERE a.ProjectRoundUserID = " + projectRoundUserID + " AND av.QuestionID = 201 ORDER BY a.EndDT DESC","eFormSqlConnection");
					if(rs.Read() && !rs.IsDBNull(0))
					{
						age = rs.GetDecimal(0);
					}
					rs.Close();

					rs = Db.rs("SELECT TOP 1 av.ValueInt FROM Answer a INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID WHERE a.ProjectRoundUserID = " + projectRoundUserID + " AND av.QuestionID = 214 ORDER BY a.EndDT DESC","eFormSqlConnection");
					if(rs.Read() && !rs.IsDBNull(0))
					{
						gender = (rs.GetInt32(0) == 139);
					}
					rs.Close();

					if(age >= 16 && age <= 25)
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
					}
					*/
					//g.drawBg(0f,green,16);
					//g.drawBg(green,25f,17);
					//g.drawBg(25f,40f,18);
					#endregion

					int age = 18;
					if(HttpContext.Current.Request.QueryString["AGE"] != null)
					{
						age = Convert.ToInt32(HttpContext.Current.Request.QueryString["AGE"]);
					}
					if(age < 18)
					{
						g.drawBg(0.00f,0.05f,"33FF33");
						g.drawBg(0.05f,0.10f,"CCFF33");
						g.drawBg(0.10f,0.15f,"EEFF33");
						g.drawBg(0.15f,0.20f,"FFFF33");
						g.drawBg(0.20f,0.25f,"FFFF33");
						g.drawBg(0.25f,0.30f,"FFFF33");
						g.drawBg(0.30f,0.35f,"FFFF33");
						g.drawBg(0.35f,0.40f,"FFEE33");
						g.drawBg(0.40f,0.45f,"FFCC33");
						g.drawBg(0.45f,0.50f,"FFAA33");
						g.drawBg(0.50f,0.55f,"FF8833");
						g.drawBg(0.55f,0.60f,"FF6633");
						g.drawBg(0.60f,0.65f,"FF4433");
						g.drawBg(0.65f,0.70f,"FF3333");
						g.drawBg(0.70f,0.75f,"FF3333");
						g.drawBg(0.75f,0.80f,"FF3333");
						g.drawBg(0.80f,0.85f,"FF3333");
						g.drawBg(0.85f,0.90f,"FF3333");
						g.drawBg(0.90f,0.95f,"FF3333");
						g.drawBg(0.95f,1.00f,"FF3333");
					}
					else
					{
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
					}
					//g.drawOutlines(12,false);
					//g.drawOutlineAt("Referensgrupp 1",33);
					//g.drawOutlineAt("Referensgrupp 2",17);
					//g.drawOutlineAt(refgr,green);
					g.drawCircleOutlineAt("", "33FF33",0f,0);
					//g.drawColorExplCircle("1", "cirka 12 procent av befolkningen ligger på den allra lägsta risknivån", "33FF33", 20, 350);
					g.drawCircleOutlineAt("", "99FF33",34f,0);
					//g.drawColorExplCircle("2", "här ligger genomsnittspoängen för kvinnor mellan 17-27 år", "99FF33", 20, 370);
					g.drawCircleOutlineAt("", "CCFF33",49.8f,0);
					//g.drawColorExplCircle("3", "här ligger genomsnittspoängen för män mellan 17-27 år", "CCFF33", 20, 390);
					g.drawCircleOutlineAt("", "DDFF33",54f,1);
					//g.drawColorExplCircle("4", "bara cirka 5 procent av kvinnorna ligger över denna nivå", "DDFF33", 20, 410);
					g.drawCircleOutlineAt("", "DDFF33",58f,2);
					//g.drawColorExplCircle("5", "bara cirka 10 procent av männen ligger över denna nivå", "DDFF33", 20, 430);
					g.drawCircleOutlineAt("", "FFFF33",70f,0);
					//g.drawColorExplCircle("6", "bara cirka 5 procent av männen ligger över denna nivå", "FFFF33", 20, 450);
					g.drawCircleOutlineAt("", "FFEE33",81f,0);
					//g.drawColorExplCircle("7", "bland kvinnor är det mindre än 1 procent som dricker mer än så här", "FFEE33", 20, 470);
					g.drawCircleOutlineAt("", "FFCC33",85f,1);
					//g.drawColorExplCircle("8", "bland män är det mindre än 1 procent som dricker mer än så här", "FFCC33", 20, 490);
					g.drawAxisExpl("Jämförelsevärden\n(för muspekaren över ringarna)",0,false,false);
					g.drawAxis(false);
					g.drawColorExplBox("Låg eller ingen risk",16,200,15);
					g.drawColorExplBox("Risk",17,330,15);
					g.drawColorExplBox("Alkoholproblem",18,400,15);

					int bx = 0;

					if(1 == 1 || cx < 7)
					{
						float lastVal = -1f;
						rs = Db.rs("SELECT a.AnswerID, a.EndDT FROM Answer a WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = " + projectRoundUserID + " ORDER BY a.EndDT","eFormSqlConnection");
						while(rs.Read())
						{
							drawAUDITscores(rs.GetInt32(0).ToString(),rs.GetDateTime(1).ToString("yyyy-MM-dd"),ref bx, ref g, (answerID == rs.GetInt32(0) && HttpContext.Current.Request.QueryString["T"] == "1"), ref lastVal);
						}
						rs.Close();
					}
					else
					{
						/*string answerIDs = "0";
						rs = Db.rs("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(yyyy,-1,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID,"eFormSqlConnection");
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawAUDITscores(answerIDs,"Senaste\nåret",ref bx, ref g);

						answerIDs = "0";
						rs = Db.rs("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(mm,-3,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID,"eFormSqlConnection");
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawAUDITscores(answerIDs,"Senaste\nkvartalet",ref bx, ref g);

						answerIDs = "0";
						rs = Db.rs("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(mm,-1,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID,"eFormSqlConnection");
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawAUDITscores(answerIDs,"Senaste\nmånaden",ref bx, ref g);

						answerIDs = "0";
						rs = Db.rs("SELECT a.AnswerID FROM Answer a WHERE a.EndDT >= DATEADD(wk,-1,GETDATE()) AND a.ProjectRoundUserID = " + projectRoundUserID,"eFormSqlConnection");
						while(rs.Read())
						{
							answerIDs += "," + rs.GetInt32(0).ToString();
						}
						rs.Close();
						drawAUDITscores(answerIDs,"Senaste\nveckan",ref bx, ref g);

						rs = Db.rs("SELECT a.AnswerID, a.EndDT FROM Answer a WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = " + projectRoundUserID + " ORDER BY a.EndDT DESC","eFormSqlConnection");
						if(rs.Read())
						{
							drawAUDITscores(rs.GetInt32(0).ToString(),"Denna\nmätning",ref bx, ref g);
						}
						rs.Close();*/

					}
					#endregion
				}
				else if(surveyID == 9)
				{
					#region AUDIT-E

					g = new Graph(580,380,"#FFFFFF");

					int cx = 11;

					g.setMinMax(-1f,100f);
					g.computeSteping(cx);

					g.drawBg(0.00f,0.05f,"33FF33");
					g.drawBg(0.05f,0.10f,"44FF33");
					g.drawBg(0.10f,0.15f,"55FF33");
					g.drawBg(0.15f,0.20f,"66FF33");
					g.drawBg(0.20f,0.25f,"77FF33");
					g.drawBg(0.25f,0.30f,"99FF33");
					g.drawBg(0.30f,0.35f,"BBFF33");
					g.drawBg(0.35f,0.40f,"EEFF33");
					g.drawBg(0.40f,0.45f,"FFFF33");
					g.drawBg(0.45f,0.50f,"FFEE33");
					g.drawBg(0.50f,0.55f,"FFCC33");
					g.drawBg(0.55f,0.60f,"FFAA33");
					g.drawBg(0.60f,0.65f,"FF8833");
					g.drawBg(0.65f,0.70f,"FF6633");
					g.drawBg(0.70f,0.75f,"FF4433");
					g.drawBg(0.75f,0.80f,"FF3333");
					g.drawBg(0.80f,0.85f,"FF3333");
					g.drawBg(0.85f,0.90f,"FF3333");
					g.drawBg(0.90f,0.95f,"FF3333");
					g.drawBg(0.95f,1.00f,"FF3333");

					//g.drawAxisExpl("Jämförelsevärden\n(för muspekaren över ringarna)",0,false,false);
					g.drawAxis(false);

					string s1 = "", s2 = "", s3 = "", s4 = "", s5 = "", s6 = "", s7 = "", s8 = "", s9 = "";
					int i1 = 0, i2 = 0, i3 = 0, i4 = 0, i5 = 0, i6 = 0, i7 = 0, i8 = 0, i9 = 0;
					int c1 = 0, c2 = 0, c3 = 0, c4 = 0, c5 = 0, c6 = 0, c7 = 0, c8 = 0, c9 = 0;
					bool o1 = false, o2 = false, o3 = false, o4 = false, o5 = false, o6 = false, o7 = false, o8 = false, o9 = false;

					SqlDataReader rs2 = Db.rs("SELECT " +
						"av.QuestionID, " +
						"ql.Question, " +
						"oc.ExportValue " +
						"FROM AnswerValue av " +
						"INNER JOIN OptionComponents oc ON av.OptionID = oc.OptionID AND oc.OptionComponentID = av.ValueInt " +
						"INNER JOIN QuestionLang ql ON av.QuestionID = ql.QuestionID AND ql.LangID = " + langID + " " +
						"WHERE av.AnswerID = " + answerID + " " +
						"AND av.OptionID = 34 " +
						"AND av.QuestionID IN (140,141,142,143,144,145,146,147,148)","eFormSqlConnection");
					while(rs2.Read())
					{
						switch(rs2.GetInt32(0))
						{
							case 140: s1 = rs2.GetString(1); i1 = rs2.GetInt32(2); break;
							case 141: s2 = rs2.GetString(1); i2 = rs2.GetInt32(2); break;
							case 142: s3 = rs2.GetString(1); i3 = rs2.GetInt32(2); break;
							case 143: s4 = rs2.GetString(1); i4 = rs2.GetInt32(2); break;
							case 144: s5 = rs2.GetString(1); i5 = rs2.GetInt32(2); break;
							case 145: s6 = rs2.GetString(1); i6 = rs2.GetInt32(2); break;
							case 146: s7 = rs2.GetString(1); i7 = rs2.GetInt32(2); break;
							case 147: s8 = rs2.GetString(1); i8 = rs2.GetInt32(2); break;
							case 148: s9 = rs2.GetString(1); i9 = rs2.GetInt32(2); break;
						}
					}
					rs2.Close();

					rs2 = Db.rs("SELECT " +
						"av.QuestionID " +
						"FROM AnswerValue av " +
						"INNER JOIN OptionComponents oc ON av.OptionID = oc.OptionID AND oc.OptionComponentID = av.ValueInt " +
						"WHERE av.AnswerID = " + answerID + " " +
						"AND av.OptionID = 35 " +
						"AND av.QuestionID IN (140,141,142,143,144,145,146,147,148)","eFormSqlConnection");
					while(rs2.Read())
					{
						switch(rs2.GetInt32(0))
						{
							case 140: o1 = true; break;
							case 141: o2 = true; break;
							case 142: o3 = true; break;
							case 143: o4 = true; break;
							case 144: o5 = true; break;
							case 145: o6 = true; break;
							case 146: o7 = true; break;
							case 147: o8 = true; break;
							case 148: o9 = true; break;
						}
					}
					rs2.Close();

					rs2 = Db.rs("SELECT TOP 9 " +
						"av.QuestionID, " +
						"oc.ExportValue, " +
						"a.EndDT " +
						"FROM AnswerValue av " +
						"INNER JOIN Answer a ON av.AnswerID = a.AnswerID " +
						"INNER JOIN OptionComponents oc ON av.OptionID = oc.OptionID AND oc.OptionComponentID = av.ValueInt " +
						"WHERE av.AnswerID < " + answerID + " " +
						"AND a.ProjectRoundUserID = " + projectRoundUserID + " " +
						"AND av.OptionID = 34 " +
						"AND av.QuestionID IN (140,141,142,143,144,145,146,147,148) " +
						"ORDER BY av.AnswerID DESC","eFormSqlConnection");
					while(rs2.Read())
					{
						switch(rs2.GetInt32(0))
						{
							case 140: if(rs2.GetInt32(1) > i1){ c1 = -1; }
									  else if(rs2.GetInt32(1) < i1){c1 = 1;} break;
							case 141: if(rs2.GetInt32(1) > i2){ c2 = -1; }
									  else if(rs2.GetInt32(1) < i2){c2 = 1;} break;
							case 142: if(rs2.GetInt32(1) > i3){ c3 = -1; }
									  else if(rs2.GetInt32(1) < i3){c3 = 1;} break;
							case 143: if(rs2.GetInt32(1) > i4){ c4 = -1; }
									  else if(rs2.GetInt32(1) < i4){c4 = 1;} break;
							case 144: if(rs2.GetInt32(1) > i5){ c5 = -1; }
									  else if(rs2.GetInt32(1) < i5){c5 = 1;} break;
							case 145: if(rs2.GetInt32(1) > i6){ c6 = -1; }
									  else if(rs2.GetInt32(1) < i6){c6 = 1;} break;
							case 146: if(rs2.GetInt32(1) > i7){ c7 = -1; }
									  else if(rs2.GetInt32(1) < i7){c7 = 1;} break;
							case 147: if(rs2.GetInt32(1) > i8){ c8 = -1; }
									  else if(rs2.GetInt32(1) < i8){c8 = 1;} break;
							case 148: if(rs2.GetInt32(1) > i9){ c9 = -1; }
									  else if(rs2.GetInt32(1) < i9){c9 = 1;} break;
						}
					}
					rs2.Close();

					float prev = -1f;
					for(int i=1; i <= 9; i++)
					{
						int score = 0;
						string s = "";
						int c = 0;
						bool o = false;

						switch(i)
						{
							case 1: score = i1; s = s1; c = c1; o = o1; break;
							case 2: score = i2; s = s2; c = c2; o = o2; break;
							case 3: score = i3; s = s3; c = c3; o = o3; break;
							case 4: score = i4; s = s4; c = c4; o = o4; break;
							case 5: score = i5; s = s5; c = c5; o = o5; break;
							case 6: score = i6; s = s6; c = c6; o = o6; break;
							case 7: score = i7; s = s7; c = c7; o = o7; break;
							case 8: score = i8; s = s8; c = c8; o = o8; break;
							case 9: score = i9; s = s9; c = c9; o = o9; break;
						}

						g.drawBottomString((s.IndexOf(" ") >= 0 ? s.Substring(0,s.IndexOf(" ")) : s),i,true);

						switch(score)
						{
							case 1: score = 0; break;
							case 2: score = 30; break;
							case 3: score = 50; break;
							case 4: score = 60; break;
							case 5: score = 75; break;
							case 6: score = 90; break;
						}

						g.drawCircle((!o ? "FFFFFF" : "6744E9"),i,(float)score, prev, c);
						prev = (float)score;
					}

					if(o1 || o2 || o3 || o4 || o5 || o6 || o7 || o8 || o9)
					{
						g.drawColorExplBox("Enbart vid återfall",12,200,15);
					}

					#endregion
				}
				else if(surveyID == 15)
				{
					#region DUDIT-E

					g = new Graph(580,380,"#FFFFFF");

					int cx = 12;

					g.setMinMax(-1f,100f);
					g.computeSteping(cx);

					g.drawBg(0.00f,0.05f,"33FF33");
					g.drawBg(0.05f,0.10f,"66FF33");
					g.drawBg(0.10f,0.15f,"99FF33");
					g.drawBg(0.15f,0.20f,"CCFF33");
					g.drawBg(0.20f,0.25f,"EEFF33");
					g.drawBg(0.25f,0.30f,"FFFF33");
					g.drawBg(0.30f,0.35f,"FFFF33");
					g.drawBg(0.35f,0.40f,"FFEE33");
					g.drawBg(0.40f,0.45f,"FFCC33");
					g.drawBg(0.45f,0.50f,"FFAA33");
					g.drawBg(0.50f,0.55f,"FF8833");
					g.drawBg(0.55f,0.60f,"FF6633");
					g.drawBg(0.60f,0.65f,"FF4433");
					g.drawBg(0.65f,0.70f,"FF3333");
					g.drawBg(0.70f,0.75f,"FF3333");
					g.drawBg(0.75f,0.80f,"FF3333");
					g.drawBg(0.80f,0.85f,"FF3333");
					g.drawBg(0.85f,0.90f,"FF3333");
					g.drawBg(0.90f,0.95f,"FF3333");
					g.drawBg(0.95f,1.00f,"FF3333");

					//g.drawAxisExpl("Jämförelsevärden\n(för muspekaren över ringarna)",0,false,false);
					g.drawAxis(false);

					string s1 = "", s2 = "", s3 = "", s4 = "", s5 = "", s6 = "", s7 = "", s8 = "", s9 = "", s10 = "";
					int i1 = 0, i2 = 0, i3 = 0, i4 = 0, i5 = 0, i6 = 0, i7 = 0, i8 = 0, i9 = 0, i10 = 0;
					int c1 = 0, c2 = 0, c3 = 0, c4 = 0, c5 = 0, c6 = 0, c7 = 0, c8 = 0, c9 = 0, c10 = 0;
					bool o1 = false, o2 = false, o3 = false, o4 = false, o5 = false, o6 = false, o7 = false, o8 = false, o9 = false, o10 = false;

					SqlDataReader rs2 = Db.rs("SELECT " +
						"av.QuestionID, " +
						"ql.Question, " +
						"oc.ExportValue " +
						"FROM AnswerValue av " +
						"INNER JOIN OptionComponents oc ON av.OptionID = oc.OptionID AND oc.OptionComponentID = av.ValueInt " +
						"INNER JOIN QuestionLang ql ON av.QuestionID = ql.QuestionID AND ql.LangID = " + langID + " " +
						"WHERE av.AnswerID = " + answerID + " " +
						"AND av.OptionID = 128 " +
						"AND av.QuestionID IN (474,475,476,477,478,479,480,481,482,483)","eFormSqlConnection");
					while(rs2.Read())
					{
						switch(rs2.GetInt32(0))
						{
							case 474: s1 = rs2.GetString(1); i1 = rs2.GetInt32(2); break;
							case 475: s2 = rs2.GetString(1); i2 = rs2.GetInt32(2); break;
							case 476: s3 = rs2.GetString(1); i3 = rs2.GetInt32(2); break;
							case 477: s4 = rs2.GetString(1); i4 = rs2.GetInt32(2); break;
							case 478: s5 = rs2.GetString(1); i5 = rs2.GetInt32(2); break;
							case 479: s6 = rs2.GetString(1); i6 = rs2.GetInt32(2); break;
							case 480: s7 = rs2.GetString(1); i7 = rs2.GetInt32(2); break;
							case 481: s8 = "Sömn/lugnande-tabl"; i8 = rs2.GetInt32(2); break;
							case 482: s9 = "Smärtstillande-tabl"; i9 = rs2.GetInt32(2); break;
							case 483: s10 = rs2.GetString(1); i10 = rs2.GetInt32(2); break;
						}
					}
					rs2.Close();

					rs2 = Db.rs("SELECT " +
						"av.QuestionID " +
						"FROM AnswerValue av " +
						"INNER JOIN OptionComponents oc ON av.OptionID = oc.OptionID AND oc.OptionComponentID = av.ValueInt " +
						"WHERE av.AnswerID = " + answerID + " " +
						"AND av.OptionID = 129 " +
						"AND av.QuestionID IN (474,475,476,477,478,479,480,481,482,483)","eFormSqlConnection");
					while(rs2.Read())
					{
						switch(rs2.GetInt32(0))
						{
							case 474: o1 = true; break;
							case 475: o2 = true; break;
							case 476: o3 = true; break;
							case 477: o4 = true; break;
							case 478: o5 = true; break;
							case 479: o6 = true; break;
							case 480: o7 = true; break;
							case 481: o8 = true; break;
							case 482: o9 = true; break;
							case 483: o10 = true; break;
						}
					}
					rs2.Close();

					rs2 = Db.rs("SELECT TOP 10 " +
						"av.QuestionID, " +
						"oc.ExportValue " +
						"FROM AnswerValue av " +
						"INNER JOIN Answer a ON av.AnswerID = a.AnswerID " +
						"INNER JOIN OptionComponents oc ON av.OptionID = oc.OptionID AND oc.OptionComponentID = av.ValueInt " +
						"WHERE av.AnswerID < " + answerID + " " +
						"AND a.ProjectRoundUserID = " + projectRoundUserID + " " +
						"AND av.OptionID = 128 " +
						"AND av.QuestionID IN (474,475,476,477,478,479,480,481,482,483) " +
						"ORDER BY av.AnswerID DESC","eFormSqlConnection");
					while(rs2.Read())
					{
						switch(rs2.GetInt32(0))
						{
							case 474: if(rs2.GetInt32(1) > i1){ c1 = -1; }
									  else if(rs2.GetInt32(1) < i1){c1 = 1;} break;
							case 475: if(rs2.GetInt32(1) > i2){ c2 = -1; }
									  else if(rs2.GetInt32(1) < i2){c2 = 1;} break;
							case 476: if(rs2.GetInt32(1) > i3){ c3 = -1; }
									  else if(rs2.GetInt32(1) < i3){c3 = 1;} break;
							case 477: if(rs2.GetInt32(1) > i4){ c4 = -1; }
									  else if(rs2.GetInt32(1) < i4){c4 = 1;} break;
							case 478: if(rs2.GetInt32(1) > i5){ c5 = -1; }
									  else if(rs2.GetInt32(1) < i5){c5 = 1;} break;
							case 479: if(rs2.GetInt32(1) > i6){ c6 = -1; }
									  else if(rs2.GetInt32(1) < i6){c6 = 1;} break;
							case 480: if(rs2.GetInt32(1) > i7){ c7 = -1; }
									  else if(rs2.GetInt32(1) < i7){c7 = 1;} break;
							case 481: if(rs2.GetInt32(1) > i8){ c8 = -1; }
									  else if(rs2.GetInt32(1) < i8){c8 = 1;} break;
							case 482: if(rs2.GetInt32(1) > i9){ c9 = -1; }
									  else if(rs2.GetInt32(1) < i9){c9 = 1;} break;
							case 483: if(rs2.GetInt32(1) > i10){ c10 = -1; }
									  else if(rs2.GetInt32(1) < i10){c10 = 1;} break;
						}
					}
					rs2.Close();

					float prev = -1f;
					for(int i=1; i <= 10; i++)
					{
						int score = 0;
						string s = "";
						int c = 0;
						bool o = false;

						switch(i)
						{
							case 1: score = i1; s = s1; c = c1; o = o1; break;
							case 2: score = i2; s = s2; c = c2; o = o2; break;
							case 3: score = i3; s = s3; c = c3; o = o3; break;
							case 4: score = i4; s = s4; c = c4; o = o4; break;
							case 5: score = i5; s = s5; c = c5; o = o5; break;
							case 6: score = i6; s = s6; c = c6; o = o6; break;
							case 7: score = i7; s = s7; c = c7; o = o7; break;
							case 8: score = i8; s = s8; c = c8; o = o8; break;
							case 9: score = i9; s = s9; c = c9; o = o9; break;
							case 10: score = i10; s = s10; c = c10; o = o10; break;
						}

						s = (s.IndexOf(" ") >= 0 ? s.Substring(0,s.IndexOf(" ")) : s);
						g.drawBottomString(s,i,true);

						switch(score)
						{
							case 1: score = 0; break;
							case 2: score = 30; break;
							case 3: score = 50; break;
							case 4: score = 60; break;
							case 5: score = 75; break;
							case 6: score = 90; break;
						}

						g.drawCircle((!o ? "FFFFFF" : "6744E9"),i,(float)score, prev, c);
						prev = (float)score;
					}

					if(o1 || o2 || o3 || o4 || o5 || o6 || o7 || o8 || o9 || o10)
					{
						g.drawColorExplBox("Enbart vid återfall",12,200,15);
					}

					#endregion
				}
				#endregion

				g.render();
			}
		}

		private void drawDUDITscores(string answerIDs, string expl, ref int bx, ref Graph g, bool current, ref float prev)
		{
			int score = getDUDITscore(answerIDs);

			if(score > 1)
				score *= 2;
			if(score > 0)
				score += 12;

			bx++;
			//g.drawBar(5,bx,(float)score,false,false);
			g.drawCircle((current ? "FFFFFF" : "AAAAAA"),bx,(float)score, prev);
			g.drawBottomString(expl,bx,true);
			prev = (float)score;
		}

		private void drawAUDITscores(string answerIDs, string expl, ref int bx, ref Graph g, bool current, ref float prev)
		{
			int score = getAUDITscore(answerIDs);

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

			bx++;
			//g.drawBar(5,bx,(float)score,false,false);
			g.drawCircle((current ? "FFFFFF" : "AAAAAA"),bx,(float)score,prev);
			g.drawBottomString(expl,bx,true);
			prev = (float)score;
		}

		public static int getDUDITscore(string answerIDs)
		{
			return getDUDITscore(answerIDs,false);
		}
		public static int getDUDITscore(string answerIDs, bool byKey)
		{
			double temp = 0;
			SqlDataReader rs2 = Db.rs("SELECT " +
				"AVG(oc.ExportValue) " +
				"FROM AnswerValue av " +
				"INNER JOIN OptionComponents oc ON av.OptionID = oc.OptionID " +
				"AND oc.OptionComponentID = av.ValueInt " +
				(byKey ? "INNER JOIN Answer a ON av.AnswerID = a.AnswerID " : "") +
				"WHERE " +
				(byKey ? "REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-','') = '" + answerIDs + "' " : "av.AnswerID IN (" + answerIDs + ") ") +
				"AND av.QuestionID IN (292,293,294,295,296,297,298,299,300,301,302) " +
				"GROUP BY av.QuestionID","eFormSqlConnection");
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

			return score;
		}

		public static int getDUDITscoreFirst(string answerIDs, bool byKey)
		{
			double temp = 0;
			SqlDataReader rs2 = Db.rs("SELECT " +
				"AVG(oc.ExportValue) " +
				"FROM AnswerValue av " +
				"INNER JOIN OptionComponents oc ON av.OptionID = oc.OptionID " +
				"AND oc.OptionComponentID = av.ValueInt " +
				(byKey ? "INNER JOIN Answer a ON av.AnswerID = a.AnswerID " : "") +
				"WHERE " +
				(byKey ? "REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-','') = '" + answerIDs + "' " : "av.AnswerID IN (" + answerIDs + ") ") +
				"AND av.QuestionID IN (292,293,294,295) " +
				"GROUP BY av.QuestionID","eFormSqlConnection");
			while(rs2.Read())
			{
				temp += (rs2.IsDBNull(0) ? 0d : Convert.ToDouble(rs2.GetValue(0)));
			}
			rs2.Close();

			int score = Convert.ToInt32(temp);

			return score;
		}

		public static int getAUDITscore(string answerIDs)
		{
			return getAUDITscore(answerIDs,false);
		}
		public static int getAUDITscore(string answerIDs, bool byKey)
		{
			double temp = 0d;
			SqlDataReader rs2 = Db.rs("SELECT " +
				"AVG(oc.ExportValue) " +
				"FROM AnswerValue av " +
				"INNER JOIN OptionComponents oc ON av.OptionID = oc.OptionID AND oc.OptionComponentID = av.ValueInt " +
				(byKey ? "INNER JOIN Answer a ON av.AnswerID = a.AnswerID " : "") +
				"WHERE " +
				(byKey ? "REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-','') = '" + answerIDs + "' " : "av.AnswerID IN (" + answerIDs + ") ") +
				"AND av.QuestionID IN (204,205,206,207,208,209,210,211,212,213) " +
				"GROUP BY av.QuestionID","eFormSqlConnection");
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

			return score;
		}
		public static int getAUDITscoreFirst(string answerIDs, bool byKey)
		{
			double temp = 0d;
			SqlDataReader rs2 = Db.rs("SELECT " +
				"AVG(oc.ExportValue) " +
				"FROM AnswerValue av " +
				"INNER JOIN OptionComponents oc ON av.OptionID = oc.OptionID AND oc.OptionComponentID = av.ValueInt " +
				(byKey ? "INNER JOIN Answer a ON av.AnswerID = a.AnswerID " : "") +
				"WHERE " +
				(byKey ? "REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-','') = '" + answerIDs + "' " : "av.AnswerID IN (" + answerIDs + ") ") +
				"AND av.QuestionID IN (204,205,206) " +
				"GROUP BY av.QuestionID","eFormSqlConnection");
			while(rs2.Read())
			{
				temp += (rs2.IsDBNull(0) ? 0 : rs2.GetInt32(0));
			}
			rs2.Close();
            
			int score = Convert.ToInt32(temp);

			return score;
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
		}
		#endregion
	}
}
