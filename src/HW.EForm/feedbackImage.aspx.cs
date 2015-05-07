using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
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
	/// Summary description for feedbackImage.
	/// </summary>
	public class feedbackImage : System.Web.UI.Page
	{
		private static void t3(int groupID, ref int dx, ref int ex, ref float score, ref float scoreD, ArrayList al, string query)
		{
			System.Data.SqlClient.SqlDataReader rs = Db.sqlRecordSet("SELECT " +
				"av1.ValueInt, " +
				"av2.ValueInt, " +
				"av3.ValueInt, " +
				"av4.ValueInt, " +
				"av5.ValueInt " +
				"FROM ProjectRoundUser u " +
				"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
				"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
				"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
				"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 459 AND av1.OptionID = 114 AND av1.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 460 AND av2.OptionID = 114 AND av2.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 461 AND av3.OptionID = 114 AND av3.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 462 AND av4.OptionID = 114 AND av4.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 463 AND av5.OptionID = 114 AND av5.DeletedSessionID IS NULL " +
				"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL " + 
				(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
				query);
			while(rs.Read())
			{
				dx ++;
	
				float scoreN = 0;
				for(int i=0;i<5;i++)
				{
					if(i == 1 || i == 2 || i == 3)
					{
						switch(rs.GetInt32(i))
						{
							case 361: scoreN += 1; break;
							case 362: scoreN += 2; break;
							case 363: scoreN += 3; break;
							case 360: scoreN += 4; break;
						}
					}
					else
					{
						switch(rs.GetInt32(i))
						{
							case 361: scoreN += 4; break;
							case 362: scoreN += 3; break;
							case 363: scoreN += 2; break;
							case 360: scoreN += 1; break;
						}
					}
				}
	
				scoreN = scoreN/5;
				scoreD += scoreN;
				al.Add(scoreN);

				if(scoreN > 2.6)
				{
					ex++;
				}
				score += 4-scoreN;
			}
			rs.Close();
		}
		private static void t6(int groupID, ref int dx, ref int ex, ref int qx, ref int wx, ref float score, string query)
		{
			System.Data.SqlClient.SqlDataReader rs = Db.sqlRecordSet("SELECT " +
				"av1.ValueInt, " +
				"av2.ValueInt, " +
				"av3.ValueInt, " +
				"av4.ValueInt, " +
				"av5.ValueInt, " +
				"av13.ValueInt, " +
				"av14.ValueInt, " +
				"av15.ValueInt, " +
				"av16.ValueInt " +
				"FROM ProjectRoundUser u " +
				"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
				"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
				"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
				"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 380 AND av1.OptionID = 114 AND av1.DeletedSessionID IS NULL AND av1.ValueInt IS NOT NULL " +
				"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 381 AND av2.OptionID = 114 AND av2.DeletedSessionID IS NULL AND av2.ValueInt IS NOT NULL " +
				"INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 382 AND av3.OptionID = 114 AND av3.DeletedSessionID IS NULL AND av3.ValueInt IS NOT NULL " +
				"INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 383 AND av4.OptionID = 114 AND av4.DeletedSessionID IS NULL AND av4.ValueInt IS NOT NULL " +
				"INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 384 AND av5.OptionID = 114 AND av5.DeletedSessionID IS NULL AND av5.ValueInt IS NOT NULL " +
				"INNER JOIN AnswerValue av13 ON a.AnswerID = av13.AnswerID AND av13.QuestionID = 401 AND av13.OptionID = 116 AND av13.DeletedSessionID IS NULL AND av13.ValueInt IS NOT NULL " +
				"INNER JOIN AnswerValue av14 ON a.AnswerID = av14.AnswerID AND av14.QuestionID = 402 AND av14.OptionID = 116 AND av14.DeletedSessionID IS NULL AND av14.ValueInt IS NOT NULL " +
				"INNER JOIN AnswerValue av15 ON a.AnswerID = av15.AnswerID AND av15.QuestionID = 403 AND av15.OptionID = 116 AND av15.DeletedSessionID IS NULL AND av15.ValueInt IS NOT NULL " +
				"INNER JOIN AnswerValue av16 ON a.AnswerID = av16.AnswerID AND av16.QuestionID = 404 AND av16.OptionID = 116 AND av16.DeletedSessionID IS NULL AND av16.ValueInt IS NOT NULL " +
				"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL " + 
				(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
				query);
			while(rs.Read())
			{
				dx ++;

				bool pbs = false;
				float scoreN = 0;
				for(int i=5;i<9;i++)
				{
					switch(Convert.ToInt32(rs.GetValue(i)))
					{
						case 367: scoreN += 5; break;
						case 363: scoreN += 4; break;
						case 368: scoreN += 3; break;
						case 369: scoreN += 2; break;
						case 361: scoreN += 1; break;
					}
				}
				if(scoreN/4 > 3.25)
				{
					pbs = true;
					qx++;
				}
				score += scoreN;

				scoreN = 0;
				for(int i=0;i<5;i++)
				{
					if(i == 0 || i == 1 || i == 4)
					{
						switch(Convert.ToInt32(rs.GetValue(i)))
						{
							case 361: scoreN += 1; break;
							case 362: scoreN += 2; break;
							case 363: scoreN += 3; break;
							case 360: scoreN += 4; break;
						}
					}
					else
					{
						switch(Convert.ToInt32(rs.GetValue(i)))
						{
							case 361: scoreN += 4; break;
							case 362: scoreN += 3; break;
							case 363: scoreN += 2; break;
							case 360: scoreN += 1; break;
						}
					}
				}

				scoreN = scoreN/5;
				if(scoreN > 2.6)
				{
					wx++;
					if(pbs)
					{
						ex++;
					}
				}
			}
			rs.Close();
		}
		private static void t5(int groupID, ref int dx, ref int ex, ref double[] n, string query)
		{
			System.Data.SqlClient.SqlDataReader rs = Db.sqlRecordSet("SELECT " +
				"av1.ValueInt, " +
				"av2.ValueInt, " +
				"av3.ValueInt, " +
				"av4.ValueInt, " +
				"av5.ValueInt, " +
				"av6.ValueInt, " +
				"av7.ValueInt, " +

				"av31.ValueInt, " +			// 7
				"av32.ValueInt, " +
				"av33.ValueInt, " +
				"av34.ValueInt, " +	
				"av35.ValueInt, " +
				"av11.ValueInt, " +			// 12
				"av12.ValueInt, " +
				"av13.ValueInt, " +
				"av14.ValueInt, " +
				"av15.ValueInt, " +
				"av21.ValueInt, " +			// 17
				"av22.ValueInt, " +
				"av23.ValueInt, " +
				"av24.ValueInt " +
				"FROM ProjectRoundUser u " +
				"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
				"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
				"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
				"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 387 AND av1.OptionID = 115 AND av1.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 388 AND av2.OptionID = 115 AND av2.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 389 AND av3.OptionID = 115 AND av3.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 390 AND av4.OptionID = 115 AND av4.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 391 AND av5.OptionID = 115 AND av5.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av6 ON a.AnswerID = av6.AnswerID AND av6.QuestionID = 392 AND av6.OptionID = 115 AND av6.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av7 ON a.AnswerID = av7.AnswerID AND av7.QuestionID = 393 AND av7.OptionID = 122 AND av7.DeletedSessionID IS NULL " +

				// OLBIE
				"INNER JOIN AnswerValue av31 ON a.AnswerID = av31.AnswerID AND av31.QuestionID = 380 AND av31.OptionID = 114 AND av31.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av32 ON a.AnswerID = av32.AnswerID AND av32.QuestionID = 381 AND av32.OptionID = 114 AND av32.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av33 ON a.AnswerID = av33.AnswerID AND av33.QuestionID = 382 AND av33.OptionID = 114 AND av33.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av34 ON a.AnswerID = av34.AnswerID AND av34.QuestionID = 383 AND av34.OptionID = 114 AND av34.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av35 ON a.AnswerID = av35.AnswerID AND av35.QuestionID = 384 AND av35.OptionID = 114 AND av35.DeletedSessionID IS NULL " +
				// OLBID
				"INNER JOIN AnswerValue av11 ON a.AnswerID = av11.AnswerID AND av11.QuestionID = 459 AND av11.OptionID = 114 AND av11.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av12 ON a.AnswerID = av12.AnswerID AND av12.QuestionID = 460 AND av12.OptionID = 114 AND av12.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av13 ON a.AnswerID = av13.AnswerID AND av13.QuestionID = 461 AND av13.OptionID = 114 AND av13.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av14 ON a.AnswerID = av14.AnswerID AND av14.QuestionID = 462 AND av14.OptionID = 114 AND av14.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av15 ON a.AnswerID = av15.AnswerID AND av15.QuestionID = 463 AND av15.OptionID = 114 AND av15.DeletedSessionID IS NULL " +
				// PBS
				"INNER JOIN AnswerValue av21 ON a.AnswerID = av21.AnswerID AND av21.QuestionID = 401 AND av21.OptionID = 116 AND av21.DeletedSessionID IS NULL AND av21.ValueInt IS NOT NULL " +
				"INNER JOIN AnswerValue av22 ON a.AnswerID = av22.AnswerID AND av22.QuestionID = 402 AND av22.OptionID = 116 AND av22.DeletedSessionID IS NULL AND av22.ValueInt IS NOT NULL " +
				"INNER JOIN AnswerValue av23 ON a.AnswerID = av23.AnswerID AND av23.QuestionID = 403 AND av23.OptionID = 116 AND av23.DeletedSessionID IS NULL AND av23.ValueInt IS NOT NULL " +
				"INNER JOIN AnswerValue av24 ON a.AnswerID = av24.AnswerID AND av24.QuestionID = 404 AND av24.OptionID = 116 AND av24.DeletedSessionID IS NULL AND av24.ValueInt IS NOT NULL " +
				"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL " + 
				(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
				query);
			while(rs.Read())
			{
				dx ++;

				float scoreN = 0;
				#region OLBIE - exhaustion/utmattning - lågt värde bra (mellan 2.6 och 4 dåligt)
				for(int i=7;i<12;i++)
				{
					if(i == 7 || i == 8 || i == 11)
					{
						// high - bad
						switch(rs.GetInt32(i))
						{
							case 361: scoreN += 1; break;	// stämmer inte alls
							case 362: scoreN += 2; break;
							case 363: scoreN += 3; break;
							case 360: scoreN += 4; break;	// stämmer precis
						}
					}
					else
					{
						// high - good
						switch(rs.GetInt32(i))
						{
							case 361: scoreN += 4; break;	// stämmer inte alls
							case 362: scoreN += 3; break;
							case 363: scoreN += 2; break;
							case 360: scoreN += 1; break;	// stämmer precis
						}
					}
				}
				#endregion
				scoreN = scoreN/5;

				float scoreD = 0;
				#region OLBID - disengement/oengagemang - lågt värde bra (mellan 2.6 och 4 dåligt, e.g. engagemang under 1.4)
				for(int i=12;i<17;i++)
				{
					if(i == 13 || i == 14 || i == 15)
					{
						// high - bad
						switch(rs.GetInt32(i))
						{
							case 361: scoreD += 1; break;	// stämmer inte alls
							case 362: scoreD += 2; break;
							case 363: scoreD += 3; break;
							case 360: scoreD += 4; break;	// stämmer precis
						}
					}
					else
					{
						// high - good
						switch(rs.GetInt32(i))
						{
							case 361: scoreD += 4; break;	// stämmer inte alls
							case 362: scoreD += 3; break;
							case 363: scoreD += 2; break;
							case 360: scoreD += 1; break;	// stämmer precis
						}
					}
				}
				#endregion
				scoreD = scoreD/5;

				float pbs = 0;
				#region PBS
				for(int i=17;i<21;i++)
				{
					switch(Convert.ToInt32(rs.GetValue(i)))
					{
						case 367: pbs += 5; break;
						case 363: pbs += 4; break;
						case 368: pbs += 3; break;
						case 369: pbs += 2; break;
						case 361: pbs += 1; break;
					}
				}
				#endregion
				pbs = pbs/4;

				bool depr = false, semidepr = false;
				for(int i=0;i<6;i++)
				{
					switch(rs.GetInt32(i))
					{
						case 364: if(semidepr) { depr = true; } semidepr = true; break;
						case 365: if(semidepr) { depr = true; } semidepr = true; break;
					}
				}
				if(rs.GetInt32(6) == 294 && depr && !(scoreN > 2.6 && (pbs > 3.25 || scoreD > 2.6)))
				{
					// Depressed but not burn-out
					ex++;
					n[1] += 1;
				}
				else
				{
					n[0] += 1;
				}
			}
			rs.Close();
		}
		private static void t1(int groupID, ref int dx, ref int ex, ref int fx, ref int qx, ref float score, ref float scoreE, ArrayList al, string query)
		{
			System.Data.SqlClient.SqlDataReader rs = Db.sqlRecordSet("SELECT " +
				"av1.ValueInt, " +
				"av2.ValueInt, " +
				"av3.ValueInt, " +
				"av4.ValueInt, " +
				"av5.ValueInt, " +
				"av11.ValueInt, " +
				"av12.ValueInt, " +
				"av13.ValueInt, " +
				"av14.ValueInt, " +
				"av15.ValueInt, " +
				"av21.ValueInt, " +			// 10
				"av22.ValueInt, " +
				"av23.ValueInt, " +
				"av24.ValueInt " +
				"FROM ProjectRoundUser u " +
				"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
				"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
				"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
				"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 380 AND av1.OptionID = 114 AND av1.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 381 AND av2.OptionID = 114 AND av2.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 382 AND av3.OptionID = 114 AND av3.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 383 AND av4.OptionID = 114 AND av4.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 384 AND av5.OptionID = 114 AND av5.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av11 ON a.AnswerID = av11.AnswerID AND av11.QuestionID = 459 AND av11.OptionID = 114 AND av11.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av12 ON a.AnswerID = av12.AnswerID AND av12.QuestionID = 460 AND av12.OptionID = 114 AND av12.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av13 ON a.AnswerID = av13.AnswerID AND av13.QuestionID = 461 AND av13.OptionID = 114 AND av13.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av14 ON a.AnswerID = av14.AnswerID AND av14.QuestionID = 462 AND av14.OptionID = 114 AND av14.DeletedSessionID IS NULL " +
				"INNER JOIN AnswerValue av15 ON a.AnswerID = av15.AnswerID AND av15.QuestionID = 463 AND av15.OptionID = 114 AND av15.DeletedSessionID IS NULL " +					
				// PBS
				"INNER JOIN AnswerValue av21 ON a.AnswerID = av21.AnswerID AND av21.QuestionID = 401 AND av21.OptionID = 116 AND av21.DeletedSessionID IS NULL AND av21.ValueInt IS NOT NULL " +
				"INNER JOIN AnswerValue av22 ON a.AnswerID = av22.AnswerID AND av22.QuestionID = 402 AND av22.OptionID = 116 AND av22.DeletedSessionID IS NULL AND av22.ValueInt IS NOT NULL " +
				"INNER JOIN AnswerValue av23 ON a.AnswerID = av23.AnswerID AND av23.QuestionID = 403 AND av23.OptionID = 116 AND av23.DeletedSessionID IS NULL AND av23.ValueInt IS NOT NULL " +
				"INNER JOIN AnswerValue av24 ON a.AnswerID = av24.AnswerID AND av24.QuestionID = 404 AND av24.OptionID = 116 AND av24.DeletedSessionID IS NULL AND av24.ValueInt IS NOT NULL " +
				"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL " +
				(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
				query +
				"");
			while(rs.Read())
			{
				dx ++;
	
				float scoreN = 0;
				for(int i=0;i<5;i++)
				{
					if(i == 0 || i == 1 || i == 4)
					{
						switch(rs.GetInt32(i))
						{
							case 361: scoreN += 1; break;
							case 362: scoreN += 2; break;
							case 363: scoreN += 3; break;
							case 360: scoreN += 4; break;
						}
					}
					else
					{
						switch(rs.GetInt32(i))
						{
							case 361: scoreN += 4; break;
							case 362: scoreN += 3; break;
							case 363: scoreN += 2; break;
							case 360: scoreN += 1; break;
						}
					}
				}
				scoreN = scoreN/5;
				scoreE += scoreN;
				al.Add(scoreN);

				float scoreD = 0;
				for(int i=5;i<10;i++)
				{
					if(i == 6 || i == 7 || i == 8)
					{
						switch(rs.GetInt32(i))
						{
							case 361: scoreD += 1; break;
							case 362: scoreD += 2; break;
							case 363: scoreD += 3; break;
							case 360: scoreD += 4; break;
						}
					}
					else
					{
						switch(rs.GetInt32(i))
						{
							case 361: scoreD += 4; break;
							case 362: scoreD += 3; break;
							case 363: scoreD += 2; break;
							case 360: scoreD += 1; break;
						}
					}
				}
				scoreD = scoreD/5;
	
				if(scoreD > 2.6)
				{
					qx++;
				}

				if(scoreN > 2.6 && scoreD > 2.6)
				{
					float pbs = 0;
					for(int i=10;i<14;i++)
					{
						switch(Convert.ToInt32(rs.GetValue(i)))
						{
							case 367: pbs += 5; break;
							case 363: pbs += 4; break;
							case 368: pbs += 3; break;
							case 369: pbs += 2; break;
							case 361: pbs += 1; break;
						}
					}
					if(pbs/4 <= 3.25)
					{
						// Utbränd utan PBS
						fx++;
					}
				}

				scoreN -= 1;
				if(scoreN > 1.6)
				{
					ex++;
				}
				score += scoreN;
			}
			rs.Close();
		}
		public static string cut(string t)
		{
			string tt = Db.RemoveHTMLTags(HttpContext.Current.Server.HtmlDecode(t)).Replace("\r\n"," ");
			if(tt.Length > 20)
			{
				string a = tt.Substring(0,20);
				string b = tt.Substring(20);
				if(b.IndexOf(" ") >= 0)
				{
					a += b.Substring(0,b.IndexOf(" ")) + "\n";
					b = b.Substring(b.IndexOf(" "));
				}
				tt = a + b;
			}
			return tt;
		}
		public static void getIdxVal(int groupID, int idx, string sql, ref int dx, ref float val, ref string desc)
		{
			System.Data.SqlClient.SqlDataReader rs = Db.sqlRecordSet("SELECT " +
				"AVG(tmp.AX), " +
				"tmp.Idx, " +
				"tmp.IdxID, " +
				"COUNT(*) AS DX " +
				"FROM " +
				"(" +
				"SELECT " +
				"100*CAST(SUM(ipc.Val*ip.Multiple) AS REAL)/i.MaxVal AS AX, " +
				"i.IdxID, " +
				"il.Idx, " +
				"i.CX, " +
				"i.AllPartsRequired, " +
				"COUNT(*) AS BX " +
				"FROM Idx i " +
				"INNER JOIN IdxLang il ON i.IdxID = il.IdxID AND il.LangID = 1 " +
				"INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID " +
				"INNER JOIN IdxPartComponent ipc ON ip.IdxPartID = ipc.IdxPartID " +
				"INNER JOIN AnswerValue av ON ip.QuestionID = av.QuestionID AND ip.OptionID = av.OptionID AND av.ValueInt = ipc.OptionComponentID AND av.DeletedSessionID IS NULL " +
				"INNER JOIN Answer a ON av.AnswerID = a.AnswerID " +
				"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
				(groupID != 0 ? "INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " : "") +
				"WHERE a.EndDT IS NOT NULL AND i.IdxID = " + idx + " " +
				(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
				sql + 
				"GROUP BY i.IdxID, a.AnswerID, i.MaxVal, il.Idx, i.CX, i.AllPartsRequired" +
				") tmp " +
				"WHERE tmp.AllPartsRequired = 0 OR tmp.CX = tmp.BX " +
				"GROUP BY tmp.IdxID, tmp.Idx");
			while(rs.Read())
			{
				dx = rs.GetInt32(3);
				val = (float)Convert.ToDouble(rs.GetValue(0));
				desc = rs.GetString(1);
			}
			rs.Close();
		}
		private static bool significant(decimal tot, decimal tot2, int Q, double[] v1, double[] n1, double[] v2, double[] n2, ref double tt, ref int df)
		{
			double s1 = 0, m1 = 0;
			for(int i=0; i<Q; i++) { m1 += (double)v1[i]*(double)n1[i]; }
			m1 /= (double)tot;
			for(int i=0; i<Q; i++) { s1 += ((double)v1[i]-m1) * ((double)v1[i]-m1) * (double)n1[i]; }
			s1 /= (double)tot-1;

			double s2 = 0, m2 = 0;
			for(int i=0; i<Q; i++) { m2 += (double)v2[i]*(double)n2[i]; }
			m2 /= (double)tot2;
			for(int i=0; i<Q; i++) { s2 += ((double)v2[i]-m2) * ((double)v2[i]-m2) * (double)n2[i]; }
			s2 /= (double)tot2-1;

			tt = Math.Abs(m1 - m2) / Math.Sqrt(s1/(double)tot + s2/(double)tot2);
			df = (int)tot+(int)tot2-1;
			double T = 0;
			switch(df)
			{
				case	1	: T=	12.71	; break;
				case	2	: T=	4.3		; break;
				case	3	: T=	3.18	; break;
				case	4	: T=	2.78	; break;
				case	5	: T=	2.57	; break;
				case	6	: T=	2.45	; break;
				case	7	: T=	2.36	; break;
				case	8	: T=	2.31	; break;
				case	9	: T=	2.26	; break;
				case	10	: T=	2.23	; break;
				case	11	: T=	2.2		; break;
				case	12	: T=	2.18	; break;
				case	13	: T=	2.16	; break;
				case	14	: T=	2.14	; break;
				case	15	: T=	2.13	; break;
				case	16	: T=	2.12	; break;
				case	17	: T=	2.11	; break;
				case	18	: T=	2.1		; break;
				case	19	: T=	2.09	; break;
				case	20	: T=	2.09	; break;
			}
			if(df>20){T=	2.06	;}
			if(df>25){T=	2.04	;}
			if(df>30){T=	2.02	;}
			if(df>40){T=	2		;}
			if(df>60){T=	1.96	;}

			return tt>T;
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			Server.ScriptTimeout = 900;

			int LID = (HttpContext.Current.Request.QueryString["LID"] != null ? Convert.ToInt32("0" + HttpContext.Current.Request.QueryString["LID"].ToString()) : 1);
			bool showN = (HttpContext.Current.Request.QueryString["ShowN"] != null);
			string fn = (HttpContext.Current.Request.QueryString["fn"] != null ? HttpContext.Current.Request.QueryString["fn"].ToString() : "");
			string projectRoundDesc = (HttpContext.Current.Request.QueryString["PRDESC"] != null ? HttpContext.Current.Server.HtmlDecode(HttpContext.Current.Request.QueryString["PRDESC"].ToString().Replace("_0_","&").Replace("_1_","#").Replace("_2_","\"")) : "Hela sjukhuset[x]");
			string unitDesc = (HttpContext.Current.Request.QueryString["UNITDESC"] != null ? HttpContext.Current.Server.HtmlDecode(HttpContext.Current.Request.QueryString["UNITDESC"].ToString().Replace("_0_","&").Replace("_1_","#").Replace("_2_","\"")) : "Min(a) grupp(er)[x]");
			

			int AID1 = (HttpContext.Current.Request.QueryString["AID1"] != null ? Convert.ToInt32("0" + HttpContext.Current.Request.QueryString["AID1"].ToString()) : 0);
			string AID1txt = (HttpContext.Current.Request.QueryString["AID1txt"] != null ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Server.HtmlDecode(HttpContext.Current.Request.QueryString["AID1txt"].ToString().Replace("_0_","&").Replace("_1_","#").Replace("_2_","\""))) : "");
			int AID2 = (HttpContext.Current.Request.QueryString["AID2"] != null ? Convert.ToInt32("0" + HttpContext.Current.Request.QueryString["AID2"].ToString()) : 0);
			string AID2txt = (HttpContext.Current.Request.QueryString["AID2txt"] != null ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Server.HtmlDecode(HttpContext.Current.Request.QueryString["AID2txt"].ToString().Replace("_0_","&").Replace("_1_","#").Replace("_2_","\""))) : "");

			int t = Convert.ToInt32("0" + HttpContext.Current.Request.QueryString["T"]);
			int r = Convert.ToInt32((HttpContext.Current.Request.QueryString["R"].ToString().StartsWith("-") ? "" : "0") + HttpContext.Current.Request.QueryString["R"]);
			int rr = (HttpContext.Current.Request.QueryString["RR"] != null ? Convert.ToInt32("0" + HttpContext.Current.Request.QueryString["RR"].ToString()) : 0);
			
			string r1 = (HttpContext.Current.Request.QueryString["R1"] != null ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["R1"]) : "");
			string r2 = (HttpContext.Current.Request.QueryString["R2"] != null ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["R2"]) : "");

			string rnds1 = (HttpContext.Current.Request.QueryString["RNDS1"] != null ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["RNDS1"]) : "");
			string rnds2 = (HttpContext.Current.Request.QueryString["RNDS2"] != null ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["RNDS2"]) : "");
			string depts1 = (HttpContext.Current.Request.QueryString["DEPTS1"] != null ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["DEPTS1"]) : "");
			string depts2 = (HttpContext.Current.Request.QueryString["DEPTS2"] != null ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["DEPTS2"]) : "");
			string rnds = (HttpContext.Current.Request.QueryString["RNDS"] != null ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["RNDS"]) : "0");

			string aids = (HttpContext.Current.Request.QueryString["AIDS"] != null ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["AIDS"]) : "");	
			string units = (HttpContext.Current.Request.QueryString["U"] != null ? HttpContext.Current.Request.QueryString["U"].ToString().Replace(" ","").Replace("'","") : "");			
			string units2 = (HttpContext.Current.Request.QueryString["RRU"] != null ? HttpContext.Current.Request.QueryString["RRU"].ToString().Replace(" ","").Replace("'","") : "");
			int rac = (HttpContext.Current.Request.QueryString["RAC"] != null ? Convert.ToInt32("0" + HttpContext.Current.Request.QueryString["RAC"].ToString()) : 10);

			bool showTotal = (HttpContext.Current.Request.QueryString["ST"] != null ? HttpContext.Current.Request.QueryString["ST"] != "0" : true);
			bool noSD = (HttpContext.Current.Request.QueryString["NOSD"] != null);
			bool exportValues = (HttpContext.Current.Request.QueryString["EV"] != null);
			bool extremeValuesOnly = (HttpContext.Current.Request.QueryString["EVO"] != null);

			string color = (HttpContext.Current.Request.QueryString["BGCOLOR"] != null ? "#" + HttpContext.Current.Request.QueryString["BGCOLOR"] : "#EFEFEF");

			int q = Convert.ToInt32("0" + (HttpContext.Current.Request.QueryString["Q"] != null ? HttpContext.Current.Request.QueryString["Q"].ToString() : ""));
			int o = Convert.ToInt32("0" + (HttpContext.Current.Request.QueryString["O"] != null ? HttpContext.Current.Request.QueryString["O"].ToString() : ""));

			bool grey = HttpContext.Current.Request.QueryString["G"] != null;

			string q1 = (HttpContext.Current.Request.QueryString["Q1"] != null ? HttpContext.Current.Request.QueryString["Q1"].ToString() : "");
			string q2 = (HttpContext.Current.Request.QueryString["Q2"] != null ? HttpContext.Current.Request.QueryString["Q2"].ToString() : "");

			bool values = (HttpContext.Current.Request.QueryString["Values"] != null);

			string u2 = (HttpContext.Current.Request.QueryString["U2"] != null ? HttpContext.Current.Request.QueryString["U2"].ToString() : "");
			int u2c = Convert.ToInt32(HttpContext.Current.Request.QueryString["U2C"] != null ? HttpContext.Current.Request.QueryString["U2C"].ToString() : "0");

			bool percent = (HttpContext.Current.Request.QueryString["Percent"] == null || Convert.ToInt32(HttpContext.Current.Request.QueryString["Percent"]) == 1);
			int likert = (HttpContext.Current.Request.QueryString["VTL"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["VTL"]) : 0);

			int groupID = (HttpContext.Current.Request.QueryString["GroupID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["GroupID"]) : 0);

			exec(groupID, HttpContext.Current.Response.OutputStream, LID, showN, fn, projectRoundDesc, unitDesc, AID1, AID1txt, AID2, AID2txt, t, r, rr, r1, r2, rnds1, rnds2, depts1, depts2, rnds, aids, units, units2, rac, showTotal, noSD, exportValues, extremeValuesOnly, color, q, grey, q1, q2, o, values, u2c, u2, percent, likert);
		}

		public static void exec(int groupID, System.IO.Stream outputStream, int LID, bool showN, string fn, string projectRoundDesc, string unitDesc, int AID1, string AID1txt, int AID2, string AID2txt, int t, int r, int rr, string r1, string r2, string rnds1, string rnds2, string depts1, string depts2, string rnds, string aids, string units, string units2, int rac, bool showTotal, bool noSD, bool exportValues, bool extremeValuesOnly, string color, int q, bool grey, string q1, string q2, int o, bool values, int u2c, string u2, bool percent, int likert)
		{
			if(color != "" && !color.StartsWith("#"))
			{
				color = "#" + color;
			}
			else if(color == "")
			{
				color = "#EFEFEF";
			}
			#region Init
			if(LID == 2)
			{
				if(unitDesc == "Min(a) grupp(er)[x]")
				{
					unitDesc = "My group(s)[x]";
				}
				if(projectRoundDesc == "Hela sjukhuset[x]")
				{
					projectRoundDesc = "All hospital[x]";
				}
			}
			if(aids != "")
			{
				string[] unitList = aids.Split(',');
				foreach(string s in unitList)
				{
					Convert.ToInt32(s);
				}
			}
			if(units != "")
			{
				string[] unitList = units.Split(',');
				foreach(string s in unitList)
				{
					Convert.ToInt32(s);
				}
			}
			if(units2 != "")
			{
				string[] unitList = units2.Split(',');
				foreach(string s in unitList)
				{
					Convert.ToInt32(s);
				}
			}
			if(rnds != "")
			{
				string[] rndList = rnds.Split(',');
				foreach(string s in rndList)
				{
					Convert.ToInt32(s);
				}
			}

			Graph g = new Graph(550,440,color);
			int cx = 0;

			//OdbcDataReader rs;
			System.Data.SqlClient.SqlDataReader rs;
			System.Data.SqlClient.SqlDataReader sdr;
			#endregion
			
			if(rnds1 != "" || rnds2 != "")
			{
				// No support for GroupID yet (didn't have energy to even open and look if it's complicated to do

				rnds1 = (rnds1 == "" ? "" : (rnds1 == "0" ? (rnds != "" ? " AND u.ProjectRoundID NOT IN (" + rnds.Replace("'","''") + ")" : "") : " AND u.ProjectRoundID IN (" + rnds1.Replace("'","''") + ")"));
				rnds2 = (rnds2 == "" ? "" : (rnds2 == "0" ? (rnds != "" ? " AND u.ProjectRoundID NOT IN (" + rnds.Replace("'","''") + ")" : "") : " AND u.ProjectRoundID IN (" + rnds2.Replace("'","''") + ")"));

				if(depts1 != "" || depts2 != "")
				{
					rnds1 += (depts1 == "" ? "" : " AND u.ProjectRoundUnitID IN (" + depts1.Replace("'","''") + ")");
					rnds2 += (depts2 == "" ? "" : " AND u.ProjectRoundUnitID IN (" + depts2.Replace("'","''") + ")");
				}

				#region Rounds vs rounds
				if(t == 1 || t == 2)		// w=320
				{
					#region E
					#region Init
					g = new Graph(550,320,color);
					if(t == 1)
					{
						g.setMinMax(0f,3f);
						g.computeSteping(3);
						g.drawOutlines(7,true,false);
					}
					else
					{
						g.setMinMax(0f,100f);
						g.computeSteping(3);
						g.drawOutlines(11,true,false);
					}
					g.drawAxis(false);
					//g.drawRightAxis();

					int dx = 0, ex = 0, fx = 0, qx = 0, div = 1;
					float score = 0, scoreE = 0;

					ArrayList al = new ArrayList();

					#endregion

					if(rnds2 != "")
					{
						div++;

						#region Comp round
							rs = Db.sqlRecordSet("SELECT " +
								"av1.ValueInt, " +			// 0
								"av2.ValueInt, " +
								"av3.ValueInt, " +
								"av4.ValueInt, " +
								"av5.ValueInt, " +
								"av11.ValueInt, " +			// 5
								"av12.ValueInt, " +
								"av13.ValueInt, " +
								"av14.ValueInt, " +
								"av15.ValueInt, " +
								"av21.ValueInt, " +			// 10
								"av22.ValueInt, " +
								"av23.ValueInt, " +
								"av24.ValueInt " +
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 380 AND av1.OptionID = 114 AND av1.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 381 AND av2.OptionID = 114 AND av2.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 382 AND av3.OptionID = 114 AND av3.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 383 AND av4.OptionID = 114 AND av4.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 384 AND av5.OptionID = 114 AND av5.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av11 ON a.AnswerID = av11.AnswerID AND av11.QuestionID = 459 AND av11.OptionID = 114 AND av11.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av12 ON a.AnswerID = av12.AnswerID AND av12.QuestionID = 460 AND av12.OptionID = 114 AND av12.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av13 ON a.AnswerID = av13.AnswerID AND av13.QuestionID = 461 AND av13.OptionID = 114 AND av13.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av14 ON a.AnswerID = av14.AnswerID AND av14.QuestionID = 462 AND av14.OptionID = 114 AND av14.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av15 ON a.AnswerID = av15.AnswerID AND av15.QuestionID = 463 AND av15.OptionID = 114 AND av15.DeletedSessionID IS NULL " +
                                // PBS
								"INNER JOIN AnswerValue av21 ON a.AnswerID = av21.AnswerID AND av21.QuestionID = 401 AND av21.OptionID = 116 AND av21.DeletedSessionID IS NULL AND av21.ValueInt IS NOT NULL " +
								"INNER JOIN AnswerValue av22 ON a.AnswerID = av22.AnswerID AND av22.QuestionID = 402 AND av22.OptionID = 116 AND av22.DeletedSessionID IS NULL AND av22.ValueInt IS NOT NULL " +
								"INNER JOIN AnswerValue av23 ON a.AnswerID = av23.AnswerID AND av23.QuestionID = 403 AND av23.OptionID = 116 AND av23.DeletedSessionID IS NULL AND av23.ValueInt IS NOT NULL " +
								"INNER JOIN AnswerValue av24 ON a.AnswerID = av24.AnswerID AND av24.QuestionID = 404 AND av24.OptionID = 116 AND av24.DeletedSessionID IS NULL AND av24.ValueInt IS NOT NULL " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + rnds2);
							while(rs.Read())
							{
								// respondenter
								dx ++;
	
								float scoreN = 0;
								#region OLBIE - exhaustion/utmattning - lågt värde bra (mellan 2.6 och 4 dåligt)
								for(int i=0;i<5;i++)
								{
									if(i == 0 || i == 1 || i == 4)
									{
										// high - bad
										switch(rs.GetInt32(i))
										{
											case 361: scoreN += 1; break;	// stämmer inte alls
											case 362: scoreN += 2; break;
											case 363: scoreN += 3; break;
											case 360: scoreN += 4; break;	// stämmer precis
										}
									}
									else
									{
										// high - good
										switch(rs.GetInt32(i))
										{
											case 361: scoreN += 4; break;	// stämmer inte alls
											case 362: scoreN += 3; break;
											case 363: scoreN += 2; break;
											case 360: scoreN += 1; break;	// stämmer precis
										}
									}
								}
								#endregion
								scoreN = scoreN/5;
								scoreE += scoreN;
								al.Add(scoreN);

								float scoreD = 0;
								#region OLBID - disengement/oengagemang - lågt värde bra (mellan 2.6 och 4 dåligt, e.g. engagemang under 1.4)
								for(int i=5;i<10;i++)
								{
									if(i == 6 || i == 7 || i == 8)
									{
										// high - bad
										switch(rs.GetInt32(i))
										{
											case 361: scoreD += 1; break;	// stämmer inte alls
											case 362: scoreD += 2; break;
											case 363: scoreD += 3; break;
											case 360: scoreD += 4; break;	// stämmer precis
										}
									}
									else
									{
										// high - good
										switch(rs.GetInt32(i))
										{
											case 361: scoreD += 4; break;	// stämmer inte alls
											case 362: scoreD += 3; break;
											case 363: scoreD += 2; break;
											case 360: scoreD += 1; break;	// stämmer precis
										}
									}
								}
								#endregion
								scoreD = scoreD/5;

								if(scoreD > 2.6)
								{
									qx++;
								}
	
								if(scoreN > 2.6 && scoreD > 2.6) // read more than 1.6 and less than 1.4
								{
									float pbs = 0;
									for(int i=10;i<14;i++)
									{
										switch(Convert.ToInt32(rs.GetValue(i)))
										{
											case 367: pbs += 5; break;
											case 363: pbs += 4; break;
											case 368: pbs += 3; break;
											case 369: pbs += 2; break;
											case 361: pbs += 1; break;
										}
									}
									if(pbs/4 <= 3.25)
									{
										// Utbränd utan PBS
										fx++;
									}
								}

								scoreN -= 1;
								if(scoreN > 1.6)
								{
									// Utmattad
									ex++;
								}
								score += scoreN;
							}
							rs.Close();

							g.drawColorExplBox(r2 + (showN ? ", n=" + dx : ""), 8, 320, 40);
							if(t == 1)
							{
								g.drawMultiBar(8,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,1,100,false,false);
								//g.drawStringInGraph(score.ToString() + ":" + dx,100,100);

								double scoreT = 0;
								IEnumerator alE = al.GetEnumerator();
								while(alE.MoveNext())
									scoreT += Math.Pow(Convert.ToDouble(alE.Current)-((double)scoreE/(double)dx),2);
								scoreT = Math.Sqrt(scoreT/dx);
								
								if(!noSD)
								{
									g.drawMultiStd(20,1,(float)Math.Round(score/(float)dx,2),(float)scoreT,g.steping,g.barW,div,1,100,true,false);
								}
							}
							else
							{
								g.drawAxisExpl("%", 0,false,false);
								g.drawMultiBar(8,1,(float)Math.Round(((float)fx/(float)dx)*100f,2),g.steping,g.barW,div,1,100,true,true);
							}
							#endregion
						
						dx = 0; ex = 0; fx = 0; qx = 0;
						score = 0; scoreE = 0;
						al = new ArrayList();
					}
					#region Current round
					rs = Db.sqlRecordSet("SELECT " +
						"av1.ValueInt, " +
						"av2.ValueInt, " +
						"av3.ValueInt, " +
						"av4.ValueInt, " +
						"av5.ValueInt, " +
						"av11.ValueInt, " +
						"av12.ValueInt, " +
						"av13.ValueInt, " +
						"av14.ValueInt, " +
						"av15.ValueInt, " +
						"av21.ValueInt, " +			// 10
						"av22.ValueInt, " +
						"av23.ValueInt, " +
						"av24.ValueInt " +
						"FROM ProjectRoundUser u " +
						"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
						"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
						"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
						"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 380 AND av1.OptionID = 114 AND av1.DeletedSessionID IS NULL " +
						"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 381 AND av2.OptionID = 114 AND av2.DeletedSessionID IS NULL " +
						"INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 382 AND av3.OptionID = 114 AND av3.DeletedSessionID IS NULL " +
						"INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 383 AND av4.OptionID = 114 AND av4.DeletedSessionID IS NULL " +
						"INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 384 AND av5.OptionID = 114 AND av5.DeletedSessionID IS NULL " +
						"INNER JOIN AnswerValue av11 ON a.AnswerID = av11.AnswerID AND av11.QuestionID = 459 AND av11.OptionID = 114 AND av11.DeletedSessionID IS NULL " +
						"INNER JOIN AnswerValue av12 ON a.AnswerID = av12.AnswerID AND av12.QuestionID = 460 AND av12.OptionID = 114 AND av12.DeletedSessionID IS NULL " +
						"INNER JOIN AnswerValue av13 ON a.AnswerID = av13.AnswerID AND av13.QuestionID = 461 AND av13.OptionID = 114 AND av13.DeletedSessionID IS NULL " +
						"INNER JOIN AnswerValue av14 ON a.AnswerID = av14.AnswerID AND av14.QuestionID = 462 AND av14.OptionID = 114 AND av14.DeletedSessionID IS NULL " +
						"INNER JOIN AnswerValue av15 ON a.AnswerID = av15.AnswerID AND av15.QuestionID = 463 AND av15.OptionID = 114 AND av15.DeletedSessionID IS NULL " +						
						// PBS
						"INNER JOIN AnswerValue av21 ON a.AnswerID = av21.AnswerID AND av21.QuestionID = 401 AND av21.OptionID = 116 AND av21.DeletedSessionID IS NULL AND av21.ValueInt IS NOT NULL " +
						"INNER JOIN AnswerValue av22 ON a.AnswerID = av22.AnswerID AND av22.QuestionID = 402 AND av22.OptionID = 116 AND av22.DeletedSessionID IS NULL AND av22.ValueInt IS NOT NULL " +
						"INNER JOIN AnswerValue av23 ON a.AnswerID = av23.AnswerID AND av23.QuestionID = 403 AND av23.OptionID = 116 AND av23.DeletedSessionID IS NULL AND av23.ValueInt IS NOT NULL " +
						"INNER JOIN AnswerValue av24 ON a.AnswerID = av24.AnswerID AND av24.QuestionID = 404 AND av24.OptionID = 116 AND av24.DeletedSessionID IS NULL AND av24.ValueInt IS NOT NULL " +
						"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + rnds1);
					while(rs.Read())
					{
						dx ++;

						float scoreN = 0;
						for(int i=0;i<5;i++)
						{
							if(i == 0 || i == 1 || i == 4)
							{
								switch(rs.GetInt32(i))
								{
									case 361: scoreN += 1; break;
									case 362: scoreN += 2; break;
									case 363: scoreN += 3; break;
									case 360: scoreN += 4; break;
								}
							}
							else
							{
								switch(rs.GetInt32(i))
								{
									case 361: scoreN += 4; break;
									case 362: scoreN += 3; break;
									case 363: scoreN += 2; break;
									case 360: scoreN += 1; break;
								}
							}
						}
						scoreN = scoreN/5;
						scoreE += scoreN;
						al.Add(scoreN);

						float scoreD = 0;
						for(int i=5;i<10;i++)
						{
							if(i == 6 || i == 7 || i == 8)
							{
								switch(rs.GetInt32(i))
								{
									case 361: scoreD += 1; break;
									case 362: scoreD += 2; break;
									case 363: scoreD += 3; break;
									case 360: scoreD += 4; break;
								}
							}
							else
							{
								switch(rs.GetInt32(i))
								{
									case 361: scoreD += 4; break;
									case 362: scoreD += 3; break;
									case 363: scoreD += 2; break;
									case 360: scoreD += 1; break;
								}
							}
						}
						scoreD = scoreD/5;

						if(scoreD > 2.6)
						{
							qx++;
						}

						if(scoreN > 2.6 && scoreD > 2.6)
						{
							float pbs = 0;
							for(int i=10;i<14;i++)
							{
								switch(Convert.ToInt32(rs.GetValue(i)))
								{
									case 367: pbs += 5; break;
									case 363: pbs += 4; break;
									case 368: pbs += 3; break;
									case 369: pbs += 2; break;
									case 361: pbs += 1; break;
								}
							}
							if(pbs/4 <= 3.25)
							{
								// Utbränd utan PBS
								fx++;
							}
						}

						scoreN -= 1;
						if(scoreN > 1.6)
						{
							ex++;
						}
						score += scoreN;
					}
					rs.Close();

					g.drawColorExplBox(r1 + (showN ? ", n=" + dx : ""), 6, 320, 20);
					if(t == 1)
					{
						g.drawMultiBar(6,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,0,100,false,false);
						//g.drawStringInGraph(score.ToString() + ":" + dx,100,100);

						double scoreT = 0;
						IEnumerator alE = al.GetEnumerator();
						while(alE.MoveNext())
							scoreT += Math.Pow(Convert.ToDouble(alE.Current)-((double)scoreE/(double)dx),2);
						scoreT = Math.Sqrt(scoreT/dx);

						if(!noSD)
						{
							g.drawMultiStd(20,1,(float)Math.Round(score/(float)dx,2),(float)scoreT,g.steping,g.barW,div,0,100,true,false);
						}
					}
					else
					{
						g.drawAxisExpl("%", 0,false,false);
						g.drawMultiBar(6,1,(float)Math.Round(((float)fx/(float)dx)*100f,2),g.steping,g.barW,div,0,100,true,true);
					}
					#endregion
					#endregion
				}
				else if(t == 3 || t == 4)	// w=320
				{
					#region D
					#region Init
					g = new Graph(550,320,color);
					if(t == 3)
					{
						g.setMinMax(0f,3f);
						g.computeSteping(3);
						g.drawOutlines(7,true,false);
					}
					else
					{
						g.setMinMax(0f,100f);
						g.computeSteping(3);
						g.drawOutlines(11,true,false);
					}
					g.drawAxis(false);
					//g.drawRightAxis();

					int dx = 0, ex = 0, div = 1;
					float score = 0, scoreD = 0;

					ArrayList al = new ArrayList();
					#endregion

					if(rnds2 != "")
					{
						div ++;

						#region Comparative round
						rs = Db.sqlRecordSet("SELECT " +
							"av1.ValueInt, " +
							"av2.ValueInt, " +
							"av3.ValueInt, " +
							"av4.ValueInt, " +
							"av5.ValueInt " +
							"FROM ProjectRoundUser u " +
							"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
							"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 459 AND av1.OptionID = 114 AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 460 AND av2.OptionID = 114 AND av2.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 461 AND av3.OptionID = 114 AND av3.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 462 AND av4.OptionID = 114 AND av4.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 463 AND av5.OptionID = 114 AND av5.DeletedSessionID IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + rnds2);
						while(rs.Read())
						{
							dx ++;

							float scoreN = 0;
							for(int i=0;i<5;i++)
							{
								if(i == 1 || i == 2 || i == 3)
								{
									switch(rs.GetInt32(i))
									{
										case 361: scoreN += 1; break;
										case 362: scoreN += 2; break;
										case 363: scoreN += 3; break;
										case 360: scoreN += 4; break;
									}
								}
								else
								{
									switch(rs.GetInt32(i))
									{
										case 361: scoreN += 4; break;
										case 362: scoreN += 3; break;
										case 363: scoreN += 2; break;
										case 360: scoreN += 1; break;
									}
								}
							}

							scoreN = scoreN/5;
							scoreD += scoreN;
							al.Add(scoreN);

							if(scoreN > 2.6)
							{
								ex++;
							}
							score += 4-scoreN;
						}
						rs.Close();

						g.drawColorExplBox(r2 + (showN ? ", n=" + dx : ""), 8, 320, 40);
						if(t == 3)
						{
							g.drawMultiBar(8,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,1,100,false,false);

							double scoreT = 0;
							IEnumerator alE = al.GetEnumerator();
							while(alE.MoveNext())
								scoreT += Math.Pow(Convert.ToDouble(alE.Current)-((double)scoreD/(double)dx),2);
							scoreT = Math.Sqrt(scoreT/dx);

							if(!noSD)
							{
								g.drawMultiStd(20,1,(float)Math.Round(score/(float)dx,2),(float)scoreT,g.steping,g.barW,div,1,100,true,false);
							}
						}
						else
						{
							g.drawAxisExpl("%", 0,false,false);
							g.drawMultiBar(8,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,1,100,true,true);
						}
						#endregion

						dx = 0; ex = 0;
						score = 0; scoreD = 0;
						al = new ArrayList();
					}

					#region Current round
					rs = Db.sqlRecordSet("SELECT " +
						"av1.ValueInt, " +
						"av2.ValueInt, " +
						"av3.ValueInt, " +
						"av4.ValueInt, " +
						"av5.ValueInt " +
						"FROM ProjectRoundUser u " +
						"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
						"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
						"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
						"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 459 AND av1.OptionID = 114 AND av1.DeletedSessionID IS NULL " +
						"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 460 AND av2.OptionID = 114 AND av2.DeletedSessionID IS NULL " +
						"INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 461 AND av3.OptionID = 114 AND av3.DeletedSessionID IS NULL " +
						"INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 462 AND av4.OptionID = 114 AND av4.DeletedSessionID IS NULL " +
						"INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 463 AND av5.OptionID = 114 AND av5.DeletedSessionID IS NULL " +
						"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + rnds1);
					while(rs.Read())
					{
						dx ++;

						float scoreN = 0;
						for(int i=0;i<5;i++)
						{
							if(i == 1 || i == 2 || i == 3)
							{
								switch(rs.GetInt32(i))
								{
									case 361: scoreN += 1; break;
									case 362: scoreN += 2; break;
									case 363: scoreN += 3; break;
									case 360: scoreN += 4; break;
								}
							}
							else
							{
								switch(rs.GetInt32(i))
								{
									case 361: scoreN += 4; break;
									case 362: scoreN += 3; break;
									case 363: scoreN += 2; break;
									case 360: scoreN += 1; break;
								}
							}
						}

						scoreN = scoreN/5;
						scoreD += scoreN;
						al.Add(scoreN);

						if(scoreN > 2.6)
						{
							ex++;
						}
						score += 4-scoreN;
					}
					rs.Close();

					g.drawColorExplBox(r1 + (showN ? ", n=" + dx : ""), 6, 320, 20);
					if(t == 3)
					{
						g.drawMultiBar(6,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,0,100,false,false);
						//g.drawStringInGraph(score.ToString() + ":" + dx,100,100);

						double scoreT = 0;
						IEnumerator alE = al.GetEnumerator();
						while(alE.MoveNext())
							scoreT += Math.Pow(Convert.ToDouble(alE.Current)-((double)scoreD/(double)dx),2);
						scoreT = Math.Sqrt(scoreT/dx);

						if(!noSD)
						{
							g.drawMultiStd(20,1,(float)Math.Round(score/(float)dx,2),(float)scoreT,g.steping,g.barW,div,0,100,true,false);
						}
					}
					else
					{
						g.drawAxisExpl("%", 0,false,false);
						g.drawMultiBar(6,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,0,100,true,true);
					}
					#endregion
					#endregion
				}
				else if(t == 5)				// w=320
				{
					#region Depression
					g = new Graph(550,320,color);
					g.setMinMax(0f,100f);
					g.computeSteping(3);
					g.drawOutlines(11,true,false);
					g.drawAxis(false);
					//g.drawRightAxis();
					g.drawAxisExpl("%",0,false,false);

					rac = 20;

					int dx = 0, ex = 0, div = 1;

					int Q = 2;
					double[] v1 = new double[Q]; v1[0] = 0; v1[1] = 1;
					double[] n1 = new double[Q]; n1[0] = 0; n1[1] = 0;
					double[] v2 = new double[Q]; v2[0] = 0; v2[1] = 1;
					double[] n2 = new double[Q]; n2[0] = 0; n2[1] = 0;

					n1[0] = 0; n1[1] = 0;
					n2[0] = 0; n2[1] = 0;

					if(rnds2 != "")
					{
						div++;

						#region Comparative round
						t5(groupID, ref dx, ref ex, ref n2, " AND r.Terminated IS NULL" + rnds2);

						g.drawColorExplBox(r2 + (showN ? ", n=" + dx : ""), 8, 320, 40);
						g.drawMultiBar(8,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,1,100,true,true);

						#endregion
					
						dx = 0; ex = 0;
					}

					#region Current round
					t5(groupID, ref dx, ref ex, ref n1, " AND r.Terminated IS NULL" + rnds1);

					g.drawColorExplBox(r1 + (showN ? ", n=" + dx : ""), 6, 320, 20);
					g.drawMultiBar(6,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,0,100,true,true);

					#endregion
					#endregion
				}
				else if(t == 6)				// w=320
				{
					#region PBS+E
					g = new Graph(550,320,color);
					if(t == 1)
					{
						g.setMinMax(1f,4f);
						g.computeSteping(3);
						g.drawOutlines(7,true,false);
					}
					else
					{
						g.setMinMax(0f,100f);
						g.computeSteping(3);
						g.drawOutlines(11,true,false);
					}
					g.drawAxis(false);
					//g.drawRightAxis();

					int dx = 0, ex = 0, qx = 0, wx = 0, div = 1;
					float score = 0;

					if(rnds2 != "")
					{
						div++;

						#region Current round
						t6(groupID, ref dx, ref ex, ref qx, ref wx, ref score, " AND r.Terminated IS NULL" + rnds2);

						g.drawColorExplBox(r2 + (showN ? ", n=" + dx : ""), 8, 320, 40);
						if(t == 1)
						{
							g.drawMultiBar(8,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,1,100,true,false);
						}
						else
						{
							g.drawAxisExpl("%", 0,false,false);
							g.drawMultiBar(8,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,1,100,true,true);
						}
						#endregion

						dx = 0; ex = 0; qx = 0; wx = 0;
						score = 0;
					}

					#region Current round
					t6(groupID, ref dx, ref ex, ref qx, ref wx, ref score, " AND r.Terminated IS NULL" + rnds1);

					g.drawColorExplBox(r1 + (showN ? ", n=" + dx : ""), 6, 320, 20);
					if(t == 1)
					{
						g.drawMultiBar(6,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,0,100,true,false);
					}
					else
					{
						g.drawAxisExpl("%", 0,false,false);
						g.drawMultiBar(6,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,0,100,true,true);
					}
					#endregion
					#endregion
				}
				else if(t == 7 || t == 17)				// w=480
				{
					string sql = "";

					#region square
					string s1 = "", s2 = "", s3 = "", s4 = "", s5 = "", s6 = "";
					switch(LID)
					{
						case 1:
							s1 = "Nöjd";
							s2 = "Varken eller";
							s3 = "Missnöjd";
							s4 = "Ofta";
							s5 = "Varken eller";
							s6 = "Sällan";
							break;
						case 2:
							s1 = "Satisfied";
							s2 = "Neither";
							s3 = "Dissatisfied";
							s4 = "Often";
							s5 = "Neither";
							s6 = "Rarely";
							break;
					}
					
					#region quadruple
					g = new Graph(550,480,color);

					int tl = 0, tc = 0, tr = 0, cl = 0, cc = 0, cr = 0, ll = 0, lc = 0, lr = 0, dx = 0;
					int ctl = 0, ctc = 0, ctr = 0, ccl = 0, ccc = 0, ccr = 0, cll = 0, clc = 0, clr = 0, cdx = 0;
				
					float CTL = 0;
					if(rnds2 != "")
					{
						#region Current round
						sql = "SELECT " +
							"COUNT(*), " +
							"av1.ValueInt, " +			// values are 370,372,373,374,371
							"av2.ValueInt " +			// range is 375-379
							"FROM ProjectRoundUser u " +
							"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
							"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 117 AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 118 AND av2.DeletedSessionID IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL" + rnds2 + " " +
							"GROUP BY av1.ValueInt, av2.ValueInt";
						if(t == 17)
						{
							sql = "" +
								"SELECT " +
								"COUNT(*), " +
								"372, " +
								"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL" + rnds2 + " " +
								"AND av1.ValueInt <= 40 " +
								"GROUP BY av2.ValueInt-2473 " +
								"UNION ALL " +
								"SELECT " +
								"COUNT(*), " +
								"373, " +
								"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL" + rnds2 + " " +
								"AND av1.ValueInt > 40 " +
								"AND av1.ValueInt < 60 " +
								"GROUP BY av2.ValueInt-2473 " +
								"UNION ALL " +
								"SELECT " +
								"COUNT(*), " +
								"374, " +
								"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL" + rnds2 + " " +
								"AND av1.ValueInt >= 60 " +
								"GROUP BY av2.ValueInt-2473 ";
						}
						rs = Db.sqlRecordSet(sql);
						while(rs.Read())
						{
							cdx += rs.GetInt32(0);
	
							//bool pos = false, mid = false;

							switch(rs.GetInt32(1) + ":" + rs.GetInt32(2))
							{
								case "370:375" : clr += rs.GetInt32(0); break;
								case "370:376" : clr += rs.GetInt32(0); break;
								case "370:377" : clc += rs.GetInt32(0); break;
								case "370:378" : cll += rs.GetInt32(0); break;
								case "370:379" : cll += rs.GetInt32(0); break;
								case "372:375" : clr += rs.GetInt32(0); break;
								case "372:376" : clr += rs.GetInt32(0); break;
								case "372:377" : clc += rs.GetInt32(0); break;
								case "372:378" : cll += rs.GetInt32(0); break;
								case "372:379" : cll += rs.GetInt32(0); break;

								case "373:375" : ccr += rs.GetInt32(0); break;
								case "373:376" : ccr += rs.GetInt32(0); break;
								case "373:377" : ccc += rs.GetInt32(0); break;
								case "373:378" : ccl += rs.GetInt32(0); break;
								case "373:379" : ccl += rs.GetInt32(0); break;

								case "374:375" : ctr += rs.GetInt32(0); break;
								case "374:376" : ctr += rs.GetInt32(0); break;
								case "374:377" : ctc += rs.GetInt32(0); break;
								case "374:378" : ctl += rs.GetInt32(0); break;
								case "374:379" : ctl += rs.GetInt32(0); break;
								case "371:375" : ctr += rs.GetInt32(0); break;
								case "371:376" : ctr += rs.GetInt32(0); break;
								case "371:377" : ctc += rs.GetInt32(0); break;
								case "371:378" : ctl += rs.GetInt32(0); break;
								case "371:379" : ctl += rs.GetInt32(0); break;
							}
						}
						rs.Close();

						CTL = (float)(100-
							Math.Round((float)clr/(float)cdx*100f,0)-
							Math.Round((float)clc/(float)cdx*100f,0)-
							Math.Round((float)cll/(float)cdx*100f,0)-
							Math.Round((float)ccr/(float)cdx*100f,0)-
							Math.Round((float)ccc/(float)cdx*100f,0)-
							Math.Round((float)ccl/(float)cdx*100f,0)-
							Math.Round((float)ctr/(float)cdx*100f,0)-
							Math.Round((float)ctc/(float)cdx*100f,0));
						#endregion
					}
				
					#region Current round
					sql = "SELECT " +
						"av1.ValueInt, " +
						"av2.ValueInt " +
						"FROM ProjectRoundUser u " +
						"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
						"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
						"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
						"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 117 AND av1.DeletedSessionID IS NULL " +
						"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 118 AND av2.DeletedSessionID IS NULL " +
						"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL" + rnds1;
					if(t == 17)
					{
						sql = "" +
							"SELECT " +
							//"COUNT(*), " +
							"372, " +
							"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
							"FROM ProjectRoundUser u " +
							"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
							"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL" + rnds1 + " " +
							"AND av1.ValueInt <= 40 " +
							//"GROUP BY av2.ValueInt-2473 " +
							"UNION ALL " +
							"SELECT " +
							//"COUNT(*), " +
							"373, " +
							"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
							"FROM ProjectRoundUser u " +
							"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
							"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL" + rnds1 + " " +
							"AND av1.ValueInt > 40 " +
							"AND av1.ValueInt < 60 " +
							//"GROUP BY av2.ValueInt-2473 " +
							"UNION ALL " +
							"SELECT " +
							//"COUNT(*), " +
							"374, " +
							"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
							"FROM ProjectRoundUser u " +
							"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
							"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL" + rnds1 + " " +
							"AND av1.ValueInt >= 60 " +
							//"GROUP BY av2.ValueInt-2473 " +
							"";
					}
					rs = Db.sqlRecordSet(sql);
					while(rs.Read())
					{
						dx ++;

						bool pos = false, mid = false;

						switch(rs.GetInt32(0))
						{
							case 373: mid = true; break;
							case 374: pos = true; break;
							case 371: goto case 374;	// was 373
						}
						switch(rs.GetInt32(1))
						{
							case 377: if(pos) { tc++; } 
										else if(mid) { cc++;}
										else { lc++; }
								break;
							case 378: if(pos) { tl++; }
										else if(mid) { cl++; }
										else { ll++; }
								break;
							case 379: goto case 378;
							default:  if(pos) { tr++; } 
										else if(mid) { cr++; }
										else { lr++; }
								break;
						}
					}
					rs.Close();
					#endregion

					float TL = (float)(100-
					Math.Round((float)lr/(float)dx*100f,0)-
					Math.Round((float)lc/(float)dx*100f,0)-
					Math.Round((float)ll/(float)dx*100f,0)-
					Math.Round((float)cr/(float)dx*100f,0)-
					Math.Round((float)cc/(float)dx*100f,0)-
					Math.Round((float)cl/(float)dx*100f,0)-
					Math.Round((float)tr/(float)dx*100f,0)-
					Math.Round((float)tc/(float)dx*100f,0));
					
					
					if(rnds2 != "")
					{
						g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
							Math.Max(0,TL) + "%","(" + Math.Max(0,CTL) + "%)",
							Math.Round((float)tc/(float)dx*100f,0) + "%","(" + Math.Round((float)ctc/(float)cdx*100f,0) + "%)",
							Math.Round((float)tr/(float)dx*100f,0) + "%","(" + Math.Round((float)ctr/(float)cdx*100f,0) + "%)",
							Math.Round((float)cl/(float)dx*100f,0) + "%","(" + Math.Round((float)ccl/(float)cdx*100f,0) + "%)",
							Math.Round((float)cc/(float)dx*100f,0) + "%","(" + Math.Round((float)ccc/(float)cdx*100f,0) + "%)",
							Math.Round((float)cr/(float)dx*100f,0) + "%","(" + Math.Round((float)ccr/(float)cdx*100f,0) + "%)",
							Math.Round((float)ll/(float)dx*100f,0) + "%","(" + Math.Round((float)cll/(float)cdx*100f,0) + "%)",
							Math.Round((float)lc/(float)dx*100f,0) + "%","(" + Math.Round((float)clc/(float)cdx*100f,0) + "%)",
							Math.Round((float)lr/(float)dx*100f,0) + "%","(" + Math.Round((float)clr/(float)cdx*100f,0) + "%)",
							HttpContext.Current.Server.UrlDecode(q1),
							HttpContext.Current.Server.UrlDecode(q2),grey);
					}
					else
					{
						g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
							Math.Max(0,TL) + "%","",
							Math.Round((float)tc/(float)dx*100f,0) + "%","",
							Math.Round((float)tr/(float)dx*100f,0) + "%","",
							Math.Round((float)cl/(float)dx*100f,0) + "%","",
							Math.Round((float)cc/(float)dx*100f,0) + "%","",
							Math.Round((float)cr/(float)dx*100f,0) + "%","",
							Math.Round((float)ll/(float)dx*100f,0) + "%","",
							Math.Round((float)lc/(float)dx*100f,0) + "%","",
							Math.Round((float)lr/(float)dx*100f,0) + "%","",
							HttpContext.Current.Server.UrlDecode(q1),
							HttpContext.Current.Server.UrlDecode(q2),grey);
					}
					#endregion
					#endregion
				}
				else if(t == 12)			// w=320
				{
					#region Freetext meanvalue
					g = new Graph(550,320,color);
					//g.leftSpacing = 150;

					sdr = Db.sqlRecordSet("SELECT " +
						"MAX(ISNULL(CAST(av1.ValueDecimal AS INT),CAST(av1.ValueText AS INT))) " +
						"FROM ProjectRoundUser u " +
						"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
						"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
						"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
						"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
						"WHERE (av1.ValueDecimal IS NOT NULL OR ISNUMERIC(av1.ValueText) = 1) AND a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + rnds1);
					if(sdr.Read() && !sdr.IsDBNull(0))
					{
						g.setMinMax(0f,(float)Convert.ToDouble(sdr.GetValue(0)));
					}
					sdr.Close();

					g.computeSteping(3);
					int steps = 5;
					g.drawOutlines(steps,false,false);
					g.drawAxis(false);
					//g.drawRightAxis();

					int dx = 0;
					float score = 0; float std = 0;

					//int tmp = 0;
					g.drawAxisVal("0",steps,0);
					g.drawAxisVal(g.maxVal.ToString(),steps,steps-1);
					rs = Db.sqlRecordSet("SELECT oc.OptionComponentID, ocl.Text, ocs.ExportValue, oc.ExportValue FROM OptionComponents ocs " +
						"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
						"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + LID + " " +
						"WHERE ocs.OptionID = " + o + " ORDER BY ocs.SortOrder DESC");
					if(rs.Read())
					{
						string tt = cut(rs.GetString(1));
						g.drawAxisExpl(tt,0,false,false);
					}
					rs.Close();

					int div = 1;

					//dx = 1;

					if(rnds2 != "")
					{
						div++;

						#region Comparative round
						sdr = Db.sqlRecordSet("SELECT " +
							"AVG(ISNULL(CAST(av1.ValueDecimal AS INT),CAST(av1.ValueText AS INT))), COUNT(ISNULL(CAST(av1.ValueDecimal AS INT),CAST(av1.ValueText AS INT))), STDEV(ISNULL(CAST(av1.ValueDecimal AS INT),CAST(av1.ValueText AS INT))) " +
							"FROM ProjectRoundUser u " +
							"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
							"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"WHERE (av1.ValueDecimal IS NOT NULL OR ISNUMERIC(av1.ValueText) = 1) AND a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + rnds2);
						while(sdr.Read() && !sdr.IsDBNull(0))
						{
							dx = Convert.ToInt32(sdr.GetValue(1));
							score = (float)Convert.ToDecimal(sdr.GetValue(0));
							std = (float)Convert.ToDecimal(sdr.GetValue(2));
						}
						sdr.Close();

						if(dx < rac)
						{
							g.drawColorExplBox(r2 + ", Ingen återkoppling", 8, 320, 40);
						}
						else
						{
							g.drawColorExplBox(r2 + (showN ? ", n=" + dx : ""), 8, 320, 40);
							g.drawMultiBar(8,1,(float)Math.Round(score,2),g.steping,g.barW,div,1,100,false,false);
							if(!noSD)
							{
								g.drawMultiStd(20,1,(float)Math.Round(score,2),std,g.steping,g.barW,div,1,100,false,false);
							}
						}
						#endregion

						dx = 0;
						score = 0; std = 0;
					}

					#region Current round
					sdr = Db.sqlRecordSet("SELECT " +
						"AVG(ISNULL(CAST(av1.ValueDecimal AS INT),CAST(av1.ValueText AS INT))), " +
						"COUNT(ISNULL(CAST(av1.ValueDecimal AS INT),CAST(av1.ValueText AS INT))), " +
						"STDEV(ISNULL(CAST(av1.ValueDecimal AS INT),CAST(av1.ValueText AS INT))) " +
						"FROM ProjectRoundUser u " +
						"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
						"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
						"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
						"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
						"WHERE (av1.ValueDecimal IS NOT NULL OR ISNUMERIC(av1.ValueText) = 1) AND a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + rnds1);
					while(sdr.Read() && !sdr.IsDBNull(0))
					{
						dx = Convert.ToInt32(sdr.GetValue(1));
						score = (float)Convert.ToDecimal(sdr.GetValue(0));
						std = (float)(sdr.IsDBNull(2) ? 0 : Convert.ToDecimal(sdr.GetValue(2)));
					}
					sdr.Close();

					if(dx < rac)
					{
						g.drawColorExplBox(r1 + ", Ingen återkoppling", 6, 320, 20);
					}
					else
					{
						g.drawColorExplBox(r1 + (showN ? ", n=" + dx : ""), 6, 320, 20);
						g.drawMultiBar(6,1,(float)Math.Round(score,2),g.steping,g.barW,div,0,100,false,false);
						if(!noSD)
						{
							g.drawMultiStd(20,1,(float)Math.Round(score,2),std,g.steping,g.barW,div,0,100,false,false);
						}
					}
					#endregion
					#endregion
				}
				else if(t == 8)				// w=320
				{
					#region VAS
					g = new Graph(550,320,color);
					g.leftSpacing = 150;
					g.setMinMax(0f,100f);
					g.computeSteping(3);
					int steps = 5;
					rs = Db.sqlRecordSet("SELECT COUNT(*) FROM OptionComponents WHERE OptionID = " + o);
					if(rs.Read())
					{
						steps = rs.GetInt32(0);
					}
					rs.Close();
					g.drawOutlines(steps,false,false);
					g.drawAxis(false);
					//g.drawRightAxis();

					int dx = 0;
					float score = 0; float std = 0;

					int tmp = 0;
					rs = Db.sqlRecordSet("SELECT oc.OptionComponentID, ocl.Text, ocs.ExportValue, oc.ExportValue FROM OptionComponents ocs " +
						"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
						"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + LID + " " +
						"WHERE ocs.OptionID = " + o + " ORDER BY ocs.SortOrder");
					while(rs.Read())
					{
						if(!extremeValuesOnly || tmp == 0 || tmp + 1 == steps)
						{
							string tt = cut(rs.GetString(1));
							g.drawAxisVal(tt + (exportValues && (!rs.IsDBNull(2) || !rs.IsDBNull(3)) ? " - " + (rs.IsDBNull(2) ? rs.GetInt32(3) : rs.GetInt32(2)) : ""),steps,dx);
						}
						dx ++;
						tmp ++;
					}
					rs.Close();

					int div = 1;

					dx = 1;

					if(rnds2 != "")
					{
						div++;

						#region Comparative round
						rs = Db.sqlRecordSet("SELECT " +
							"AVG(av1.ValueInt), COUNT(av1.ValueInt), STDEV(av1.ValueInt) " +
							"FROM ProjectRoundUser u " +
							"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
							"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + rnds2);
						if(rs.Read())
						{
							dx = 0; score = 0; std = 0;
							if(!rs.IsDBNull(1))
								dx = Convert.ToInt32(rs.GetValue(1));
							if(!rs.IsDBNull(0))
								score = (float)Convert.ToDecimal(rs.GetValue(0));
							if(!rs.IsDBNull(2))
								std = (float)Convert.ToDecimal(rs.GetValue(2));
						}
						rs.Close();

						if(dx < rac)
						{
							g.drawColorExplBox(r2 + ", Ingen återkoppling", 8, 320, 40);
						}
						else
						{
							g.drawColorExplBox(r2 + (showN ? ", n=" + dx : ""), 8, 320, 40);
							g.drawMultiBar(8,1,(float)Math.Round(score,2),g.steping,g.barW,div,1,100,values,false);
							if(!noSD)
							{
								g.drawMultiStd(20,1,(float)Math.Round(score,2),std,g.steping,g.barW,div,1,100,false,false);
							}
						}
						#endregion

						dx = 0;
						score = 0; std = 0;
					}

					#region Current round
					rs = Db.sqlRecordSet("SELECT " +
						"AVG(av1.ValueInt), COUNT(av1.ValueInt), STDEV(av1.ValueInt) " +
						"FROM ProjectRoundUser u " +
						"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
						"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
						"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
						"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
						"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + rnds1);
					if(rs.Read())
					{
						dx = 0; score = 0; std = 0;
						if(!rs.IsDBNull(1))
							dx = Convert.ToInt32(rs.GetValue(1));
						if(!rs.IsDBNull(0))
							score = (float)Convert.ToDecimal(rs.GetValue(0));
						if(!rs.IsDBNull(2))
							std = (float)Convert.ToDecimal(rs.GetValue(2));
					}
					rs.Close();

					if(dx < rac)
					{
						g.drawColorExplBox(r1 + ", Ingen återkoppling", 6, 320, 20);
					}
					else
					{
						g.drawColorExplBox(r1 + (showN ? ", n=" + dx : ""), 6, 320, 20);
						g.drawMultiBar(6,1,(float)Math.Round(score,2),g.steping,g.barW,div,0,100,values,false);
						if(!noSD)
						{
							g.drawMultiStd(20,1,(float)Math.Round(score,2),std,g.steping,g.barW,div,0,100,false,false);
						}
					}
					#endregion
					#endregion
				}
				else if(t == 0 || t == 13)	// w=440
				{
					#region Likert, checkbox

					bool printedDesc = false;

					int optioncomponentcount = 0, totPercent = 0;
					rs = Db.sqlRecordSet("SELECT COUNT(*) FROM OptionComponents ocs WHERE ocs.OptionID = " + o);
					if(rs.Read())
					{
						optioncomponentcount = Convert.ToInt32(rs.GetValue(0));
						cx = optioncomponentcount+2;
					}
					rs.Close();

					decimal tot = 0, tot2 = 0;
					if(rnds2 != "")
					{
						rs = Db.sqlRecordSet("SELECT COUNT(*) FROM Answer a " +
							"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
							"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							(t != 13 ? "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " : "") +
							"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + rnds2);
						if(rs.Read())
						{
							tot2 = Convert.ToDecimal(rs.GetInt32(0));
						}
						rs.Close();
					}
					rs = Db.sqlRecordSet("SELECT COUNT(*) FROM Answer a " +
						"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
						"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
						(t != 13 ? "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " : "") +
						"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + rnds1);
					if(rs.Read())
					{
						tot = Convert.ToDecimal(rs.GetInt32(0));
					}
					rs.Close();
					float max = (percent ? 100f : (float)Convert.ToDouble(Math.Max(tot2,tot)));
					max = (float)Math.Round(max/10,0)*10;
					g.setMinMax(0f,max);

					if(cx > 0)
					{
						int Q = cx;
						double[] v1 = new double[Q];
						double[] n1 = new double[Q];
						double[] v2 = new double[Q];
						double[] n2 = new double[Q];

						g.computeSteping(cx);
						g.drawOutlines(11,true,false);
						g.drawAxis(false);
						//g.drawRightAxis();
						g.drawAxisExpl((percent ? "%" : ""), 0,false,false);

						cx = 0;

						int div = 1;

						if(rnds2 != "")
						{
							div++;

							#region Other comparative round, color 8, start at 1

							if(tot2 < rac)
							{
								g.drawColorExplBox(r2 + ", Ingen återkoppling", 8, 320, 40);
							}
							else
							{
								g.drawColorExplBox(r2 + (showN ? ", n=" + tot2 : ""), 8, 320, 40);

								totPercent = 0;
								rs = Db.sqlRecordSet("SELECT " +
									"oc.OptionComponentID, " +
									"ocl.Text, " +
									"(" +
									"SELECT COUNT(*) FROM Answer a " + 
									"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
									"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
									"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt = oc.OptionComponentID AND av.DeletedSessionID IS NULL " +
									"AND av.OptionID = ocs.OptionID " +
									"AND av.QuestionID = " + q + " " +
									"AND r.Terminated IS NULL" + rnds2 +
									"), " +
									"oc.ExportValue " +
									"FROM OptionComponents ocs " +
									"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
									"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + LID + " " +
									"WHERE ocs.OptionID = " + o + " ORDER BY ocs.SortOrder");
								while(rs.Read())
								{
									if(!rs.IsDBNull(3))
									{
										n2[cx] = Convert.ToDouble(rs.GetInt32(2));
										v2[cx] = Convert.ToDouble(rs.GetInt32(3));
									}

									cx++;

									if(tot2 > 0)
									{
										int v = rs.GetInt32(2);
										if(percent)
										{
											v = Convert.ToInt32(Math.Round(Convert.ToDecimal(v)/tot2*100M,0));
											while(t == 0 && (totPercent+v) > 100)
											{
												v--;
											}
											totPercent += v;
										}
										g.drawMultiBar(8,cx,v,g.steping,g.barW,div,1,100,true,percent);
									}
									if(!printedDesc)
									{
										string tt = cut(rs.GetString(1));
										g.drawBottomString(tt,cx,optioncomponentcount != 1);
									}
								}
								rs.Close();

								printedDesc = true;
							}

							cx = 0;
							#endregion
						}

						#region Current round, color 6, start at 0

						if(tot < rac)
						{
							g.drawColorExplBox(r1 + ", Ingen återkoppling", 6, 320, 20);
						}
						else
						{
							g.drawColorExplBox(r1 + (showN ? ", n=" + tot : ""), 6, 320, 20);

							totPercent = 0;
							rs = Db.sqlRecordSet("SELECT " +
								"oc.OptionComponentID, " +
								"ocl.Text, " +
								"(" +
								"SELECT COUNT(*) FROM Answer a " + 
								"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
								"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt = oc.OptionComponentID AND av.DeletedSessionID IS NULL " +
								"AND av.OptionID = ocs.OptionID " +
								"AND av.QuestionID = " + q + " " +
								"AND r.Terminated IS NULL" + rnds1 +
								"), " +
								"oc.ExportValue " +
								"FROM OptionComponents ocs " +
								"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
								"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + LID + " " +
								"WHERE ocs.OptionID = " + o + " ORDER BY ocs.SortOrder");
							while(rs.Read())
							{
								if(!rs.IsDBNull(3))
								{
									n1[cx] = Convert.ToDouble(rs.GetInt32(2));
									v1[cx] = Convert.ToDouble(rs.GetInt32(3));
								}

								cx++;

								if(tot > 0)
								{
									int v = rs.GetInt32(2);
									if(percent)
									{
										v = Convert.ToInt32(Math.Round(Convert.ToDecimal(v)/tot*100M,0));
										while(t == 0 && (totPercent+v) > 100)
										{
											v--;
										}
										totPercent += v;
									}
									g.drawMultiBar(6,cx,v,g.steping,g.barW,div,0,100,true,percent);
								}
								if(!printedDesc)
								{
									string tt = cut(rs.GetString(1));
									g.drawBottomString(tt,cx,optioncomponentcount != 1);
								}
							}
							rs.Close();

							printedDesc = true;
						}
						#endregion
					}
					#endregion
				}
				#endregion
			}
			else
			{
				#region Round vs round
				if(t == 20)					// w=320
				{
					#region Index
					g = new Graph(550,320,color);
					g.leftSpacing = 150;
					g.setMinMax(0f,100f);
					g.computeSteping(3);
					int steps = 6;
					g.drawOutlines(steps,false,false);
					g.drawAxis(false);
					//g.drawRightAxis();

					bool hasAID = false;

					int dx = 0; string tmp = "";
					float score = 0; 
					int bar1 = 0, bar2 = 0, bar3 = 0, bar4 = 0, bar5 = 0;

					g.drawAxisVal("0",steps,0);
					g.drawAxisVal("20",steps,1);
					g.drawAxisVal("40",steps,2);
					g.drawAxisVal("60",steps,3);
					g.drawAxisVal("80",steps,4);
					g.drawAxisVal("100",steps,5);

					float a1 = -99, a2 = -99;
					int div = 0;
					
					if(showTotal)
					{
						div++;
						if(rr != 0)
						{
							div++;
							bar5 = 1;
						}
					}

					if(AID1 != 0)
					{
						hasAID = true;

						getIdxVal(groupID,q,"AND a.AnswerID = " + AID1 + " ",ref bar2,ref a1,ref tmp);
						div += bar2;
					}
					if(AID2 != 0)
					{
						getIdxVal(groupID,q,"AND a.AnswerID = " + AID2 + " ",ref bar1,ref a2,ref tmp);
						div += bar2;
					}

					dx = 0;
					if(units != "" || aids != "")
					{
						div++;
						bar4 = 1;

						if(rr != 0 && units2 != "")
						{
							div++;
							bar3 = 1;

							#region Comparative round
							dx = 0; score = 0; 
							getIdxVal(groupID,q,"AND a.ProjectRoundUnitID IN (" + units2 + ") ",ref dx,ref score,ref tmp);

							if(dx < rac)
							{
								switch(LID)
								{
									case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120+(hasAID?75:0), 40);break;
									case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120+(hasAID?75:0), 40);break;
								}
							
							}
							else
							{
								g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + dx : ""), 7, 120+(hasAID?75:0), 40);
								g.drawMultiBar(7,1,(float)Math.Round(score,2),g.steping,g.barW,div,bar1+bar2,100,true,false);
							}
							#endregion
						}

						#region Current round
						dx = 0; score = 0; 
						getIdxVal(groupID,q,(units != "" ? "AND a.ProjectRoundUnitID IN (" + units + ") " : "") + (aids != "" ? "AND a.AnswerID IN (" + aids + ") " : ""),ref dx,ref score,ref tmp);

						if(dx < rac)
						{
							switch(LID)
							{
								case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
								case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
							}
						
						}
						else
						{
							g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + dx : ""), 5, 120+(hasAID?75:0), 20);
							g.drawMultiBar(5,1,(float)Math.Round(score,2),g.steping,g.barW,div,bar1+bar2+bar3,100,true,false);
						}
						#endregion

						dx = 0;
						score = 0; 
					}

					if(a1 != -99)
					{
						g.drawColorExplBox(AID1txt, 20, 70, 20);
						g.drawMultiBar(20,1,(float)a1,g.steping,g.barW,div,bar1,100,true,false);
					}
					if(a2 != -99)
					{
						g.drawColorExplBox(AID2txt, 19, 70, 40);
						g.drawMultiBar(19,1,(float)a2,g.steping,g.barW,div,0,100,true,false);
					}

					if(showTotal)
					{
						if(rr != 0)
						{
							#region Comparative round
							dx = 0; score = 0; 
							getIdxVal(groupID,q,"AND a.ProjectRoundID = " + rr + " ",ref dx,ref score,ref tmp);

							if(dx < rac)
							{
								switch(LID)
								{
									case 1: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + ", n < " + rac, 8, 320, 40); break;
									case 2: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + ", n < " + rac, 8, 320, 40); break;
								}
						
							}
							else
							{
								g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + dx : ""), 8, 320, 40);
								g.drawMultiBar(8,1,(float)Math.Round(score,2),g.steping,g.barW,div,bar1+bar2+bar3+bar4,100,true,false);
							}
							#endregion
						}

						#region Current round
						dx = 0; score = 0; 
						getIdxVal(groupID,q,(r != 0 ? " AND a.ProjectRoundID = " + r + " ": ""),ref dx,ref score,ref tmp);

						if(dx < rac)
						{
							switch(LID)
							{
								case 1: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + ", n < " + rac, 6, 320, 20); break;
								case 2: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + ", n < " + rac, 6, 320, 20); break;
							}
						
						}
						else
						{
							g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + dx : ""), 6, 320, 20);
							g.drawMultiBar(6,1,(float)Math.Round(score,2),g.steping,g.barW,div,bar1+bar2+bar3+bar4+bar5,100,true,false);
						}
						#endregion
					}
					#endregion
				}
				else if(t == 1 || t == 2)	// w=320
				{
					#region E
					#region Init
					g = new Graph(550,320,color);
					if(t == 1)
					{
						g.setMinMax(0f,3f);
						g.computeSteping(3);
						g.drawOutlines(7,true,false);
					}
					else
					{
						g.setMinMax(0f,100f);
						g.computeSteping(3);
						g.drawOutlines(11,true,false);
					}
					g.drawAxis(false);
					//g.drawRightAxis();

					int dx = 0, ex = 0, fx = 0, qx = 0, div = (showTotal ? (rr != 0 ? 2 : 1) : 0);
					float score = 0, scoreE = 0;
					int bar1 = 0, bar2 = 0, bar3 = 0, bar4 = 0, bar5 = 0;
					bool hasAID = false;

					ArrayList al = new ArrayList();

					float a1 = -99, a2 = -99;
					#endregion

					if(showTotal)
					{
						div++;
						if(rr != 0)
						{
							div++;
							bar5 = 1;
						}
					}

					if(AID1 != 0)
					{
						hasAID = true;
						bar2 = 1;
						#region AID1
						div++;
					
						rs = Db.sqlRecordSet("SELECT " +
							"av1.ValueInt, " +
							"av2.ValueInt, " +
							"av3.ValueInt, " +
							"av4.ValueInt, " +
							"av5.ValueInt " +
							"FROM Answer a " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 380 AND av1.OptionID = 114 AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 381 AND av2.OptionID = 114 AND av2.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 382 AND av3.OptionID = 114 AND av3.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 383 AND av4.OptionID = 114 AND av4.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 384 AND av5.OptionID = 114 AND av5.DeletedSessionID IS NULL " +
							"WHERE a.AnswerID = " + AID1);
						if(rs.Read())
						{
							float scoreN = 0;
							for(int i=0;i<5;i++)
							{
								if(i == 0 || i == 1 || i == 4)
								{
									switch(rs.GetInt32(i))
									{
										case 361: scoreN += 1; break;
										case 362: scoreN += 2; break;
										case 363: scoreN += 3; break;
										case 360: scoreN += 4; break;
									}
								}
								else
								{
									switch(rs.GetInt32(i))
									{
										case 361: scoreN += 4; break;
										case 362: scoreN += 3; break;
										case 363: scoreN += 2; break;
										case 360: scoreN += 1; break;
									}
								}
							}
							a1 = scoreN/5 - 1;
						}
						rs.Close();
						#endregion
					}
					if(AID2 != 0)
					{
						bar1 = 1;
						#region AID2
						div++;
					
						rs = Db.sqlRecordSet("SELECT " +
							"av1.ValueInt, " +
							"av2.ValueInt, " +
							"av3.ValueInt, " +
							"av4.ValueInt, " +
							"av5.ValueInt " +
							"FROM Answer a " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 380 AND av1.OptionID = 114 AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 381 AND av2.OptionID = 114 AND av2.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 382 AND av3.OptionID = 114 AND av3.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 383 AND av4.OptionID = 114 AND av4.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 384 AND av5.OptionID = 114 AND av5.DeletedSessionID IS NULL " +
							"WHERE a.AnswerID = " + AID2);
						if(rs.Read())
						{
							float scoreN = 0;
							for(int i=0;i<5;i++)
							{
								if(i == 0 || i == 1 || i == 4)
								{
									switch(rs.GetInt32(i))
									{
										case 361: scoreN += 1; break;
										case 362: scoreN += 2; break;
										case 363: scoreN += 3; break;
										case 360: scoreN += 4; break;
									}
								}
								else
								{
									switch(rs.GetInt32(i))
									{
										case 361: scoreN += 4; break;
										case 362: scoreN += 3; break;
										case 363: scoreN += 2; break;
										case 360: scoreN += 1; break;
									}
								}
							}
							a2 = scoreN/5 - 1;
						}
						rs.Close();
						#endregion
					}

					if(units != "" || aids != "")
					{
						div++;
						bar4 = 1;
						if(rr != 0 && units2 != "")
						{
							div++;
							bar3 = 1;
							#region Comparative round
							t1(groupID, ref dx, ref ex, ref fx, ref qx, ref score, ref scoreE, al, " AND u.ProjectRoundUnitID IN (" + units2 + ")");

							if(dx < rac)
							{
								switch(LID)
								{
									case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120+(hasAID?75:0), 40);break;
									case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120+(hasAID?75:0), 40);break;
								}
							
							}
							else
							{
								g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + dx : ""), 7, 120+(hasAID?75:0), 40);
								if(t == 1)
								{
									g.drawMultiBar(7,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,bar1+bar2,100,!(AID1 == 0 && AID2 == 0),false);
									//g.drawStringInGraph(score.ToString() + ":" + dx,100,100);

									if(AID1 == 0 && AID2 == 0)
									{
										double scoreT = 0;
										IEnumerator alE = al.GetEnumerator();
										while(alE.MoveNext())
											scoreT += Math.Pow(Convert.ToDouble(alE.Current)-((double)scoreE/(double)dx),2);
										scoreT = Math.Sqrt(scoreT/dx);

										if(!noSD)
										{
											g.drawMultiStd(20,1,(float)Math.Round(score/(float)dx,2),(float)scoreT,g.steping,g.barW,div,bar1+bar2,100,true,false);
										}
									}
								}
								else
								{
									g.drawMultiBar(7,1,(float)Math.Round(((float)fx/(float)dx)*100f,2),g.steping,g.barW,div,bar1+bar2,100,true,true);
								}
							}
							#endregion

							dx = 0; ex = 0; fx = 0; qx = 0;
							score = 0; scoreE = 0;
							al = new ArrayList();
						}
						#region Current round
						t1(groupID, ref dx, ref ex, ref fx, ref qx, ref score, ref scoreE, al, (units != "" ? " AND u.ProjectRoundUnitID IN (" + units + ") " : "") + (aids != "" ? " AND a.AnswerID IN (" + aids + ") " : ""));

						if(dx < rac)
						{
							switch(LID)
							{
								case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
								case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
							}
						
						}
						else
						{
							g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + dx : ""), 5, 120+(hasAID?75:0), 20);
							if(t == 1)
							{
						
								g.drawMultiBar(5,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,bar1+bar2+bar3,100,!(AID1 == 0 && AID2 == 0),false);
								//g.drawStringInGraph(score.ToString() + ":" + dx,100,100);

								if(AID1 == 0 && AID2 == 0)
								{
									double scoreT = 0;
									IEnumerator alE = al.GetEnumerator();
									while(alE.MoveNext())
										scoreT += Math.Pow(Convert.ToDouble(alE.Current)-((double)scoreE/(double)dx),2);
									scoreT = Math.Sqrt(scoreT/dx);

									if(!noSD)
									{
										g.drawMultiStd(20,1,(float)Math.Round(score/(float)dx,2),(float)scoreT,g.steping,g.barW,div,bar1+bar2+bar3,100,true,false);
									}
								}
							}
							else
							{
								g.drawMultiBar(5,1,(float)Math.Round(((float)fx/(float)dx)*100f,2),g.steping,g.barW,div,bar1+bar2+bar3,100,true,true);
							}
						}
						#endregion

						dx = 0; ex = 0; fx = 0; qx = 0;
						score = 0; scoreE = 0;
						al = new ArrayList();
					}

					if(showTotal)
					{
						if(rr != 0)
						{
							#region Current round
							t1(groupID, ref dx, ref ex, ref fx, ref qx, ref score, ref scoreE, al, " AND r.Terminated IS NULL AND u.ProjectRoundID = " + rr);
							
							g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + dx : ""), 8, 320, 40);
							if(t == 1)
							{
								g.drawMultiBar(8,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,bar1+bar2+bar3+bar4,100,!(AID1 == 0 && AID2 == 0),false);
								//g.drawStringInGraph(score.ToString() + ":" + dx,100,100);

								if(AID1 == 0 && AID2 == 0)
								{
									double scoreT = 0;
									IEnumerator alE = al.GetEnumerator();
									while(alE.MoveNext())
										scoreT += Math.Pow(Convert.ToDouble(alE.Current)-((double)scoreE/(double)dx),2);
									scoreT = Math.Sqrt(scoreT/dx);

									if(!noSD)
									{
										g.drawMultiStd(20,1,(float)Math.Round(score/(float)dx,2),(float)scoreT,g.steping,g.barW,div,bar1+bar2+bar3+bar4,100,true,false);
									}
								}
							}
							else
							{
								g.drawAxisExpl("%", 0,false,false);
								g.drawMultiBar(8,1,(float)Math.Round(((float)fx/(float)dx)*100f,2),g.steping,g.barW,div,bar1+bar2+bar3+bar4,100,true,true);
							}
							#endregion
						
							dx = 0; ex = 0; fx = 0; qx = 0;
							score = 0; scoreE = 0;
							al = new ArrayList();
						}
						#region Current round
						t1(groupID, ref dx, ref ex, ref fx, ref qx, ref score, ref scoreE, al, " AND r.Terminated IS NULL" + (r != 0 ? " AND u.ProjectRoundID = " + r : ""));

						g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + dx : ""), 6, 320, 20);
						if(t == 1)
						{
							g.drawMultiBar(6,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,bar1+bar2+bar3+bar4+bar5,100,!(AID1 == 0 && AID2 == 0),false);
							//g.drawStringInGraph(score.ToString() + ":" + dx,100,100);

							if(AID1 == 0 && AID2 == 0)
							{
								double scoreT = 0;
								IEnumerator alE = al.GetEnumerator();
								while(alE.MoveNext())
									scoreT += Math.Pow(Convert.ToDouble(alE.Current)-((double)scoreE/(double)dx),2);
								scoreT = Math.Sqrt(scoreT/dx);

								if(!noSD)
								{
									g.drawMultiStd(20,1,(float)Math.Round(score/(float)dx,2),(float)scoreT,g.steping,g.barW,div,bar1+bar2+bar3+bar4+bar5,100,true,false);
								}
							}
							else
							{
								if(a1 != -99)
								{
									g.drawColorExplBox(AID1txt, 20, 70, 20);
									g.drawMultiBar(20,1,(float)a1,g.steping,g.barW,div,bar1,100,true,false);
								}
								if(a2 != -99)
								{
									g.drawColorExplBox(AID2txt, 19, 70, 40);
									g.drawMultiBar(19,1,(float)a2,g.steping,g.barW,div,0,100,true,false);
								}
							}
						}
						else
						{
							g.drawAxisExpl("%", 0,false,false);
							g.drawMultiBar(6,1,(float)Math.Round(((float)fx/(float)dx)*100f,2),g.steping,g.barW,div,bar1+bar2+bar3+bar4+bar5,100,true,true);
						}
						#endregion
					}
					#endregion
				}
				else if(t == 3 || t == 4)	// w=320
				{
					#region D
					#region Init
					g = new Graph(550,320,color);
					if(t == 3)
					{
						g.setMinMax(0f,3f);
						g.computeSteping(3);
						g.drawOutlines(7,true,false);
					}
					else
					{
						g.setMinMax(0f,100f);
						g.computeSteping(3);
						g.drawOutlines(11,true,false);
					}
					g.drawAxis(false);
					//g.drawRightAxis();

					int dx = 0, ex = 0, div = (showTotal ? (rr != 0 ? 2 : 1) : 0);
					float score = 0, scoreD = 0;
					int bar1 = 0, bar2 = 0, bar3 = 0, bar4 = 0, bar5 = 0;
					bool hasAID = false;

					ArrayList al = new ArrayList();
					float a1 = -99, a2 = -99;
					#endregion

					if(showTotal)
					{
						div++;
						if(rr != 0)
						{
							div++;
							bar5 = 1;
						}
					}

					#region Individual
					if(AID1 != 0)
					{
						bar2 = 1;
						hasAID = true;
						div++;
					
						rs = Db.sqlRecordSet("SELECT " +
							"av1.ValueInt, " +
							"av2.ValueInt, " +
							"av3.ValueInt, " +
							"av4.ValueInt, " +
							"av5.ValueInt " +
							"FROM Answer a " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 459 AND av1.OptionID = 114 AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 460 AND av2.OptionID = 114 AND av2.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 461 AND av3.OptionID = 114 AND av3.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 462 AND av4.OptionID = 114 AND av4.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 463 AND av5.OptionID = 114 AND av5.DeletedSessionID IS NULL " +
							"WHERE a.AnswerID = " + AID1);
						if(rs.Read())
						{
							float scoreN = 0;
							for(int i=0;i<5;i++)
							{
								if(i == 1 || i == 2 || i == 3)
								{
									switch(rs.GetInt32(i))
									{
										case 361: scoreN += 1; break;
										case 362: scoreN += 2; break;
										case 363: scoreN += 3; break;
										case 360: scoreN += 4; break;
									}
								}
								else
								{
									switch(rs.GetInt32(i))
									{
										case 361: scoreN += 4; break;
										case 362: scoreN += 3; break;
										case 363: scoreN += 2; break;
										case 360: scoreN += 1; break;
									}
								}
							}
	
							a1 = 4-scoreN/5;
						}
						rs.Close();
					}
					if(AID2 != 0)
					{
						bar1 = 1;
						div++;
					
						rs = Db.sqlRecordSet("SELECT " +
							"av1.ValueInt, " +
							"av2.ValueInt, " +
							"av3.ValueInt, " +
							"av4.ValueInt, " +
							"av5.ValueInt " +
							"FROM Answer a " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 459 AND av1.OptionID = 114 AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 460 AND av2.OptionID = 114 AND av2.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 461 AND av3.OptionID = 114 AND av3.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 462 AND av4.OptionID = 114 AND av4.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 463 AND av5.OptionID = 114 AND av5.DeletedSessionID IS NULL " +
							"WHERE a.AnswerID = " + AID2);
						if(rs.Read())
						{
							float scoreN = 0;
							for(int i=0;i<5;i++)
							{
								if(i == 1 || i == 2 || i == 3)
								{
									switch(rs.GetInt32(i))
									{
										case 361: scoreN += 1; break;
										case 362: scoreN += 2; break;
										case 363: scoreN += 3; break;
										case 360: scoreN += 4; break;
									}
								}
								else
								{
									switch(rs.GetInt32(i))
									{
										case 361: scoreN += 4; break;
										case 362: scoreN += 3; break;
										case 363: scoreN += 2; break;
										case 360: scoreN += 1; break;
									}
								}
							}
	
							a2 = 4-scoreN/5;
						}
						rs.Close();
					}
					#endregion

					if(units != "" || aids != "")
					{
						bar4=1;
						div++;
					
						if(rr != 0 && units2 != "")
						{
							bar3 = 1;
							#region Comparative round
							div++;

							t3(groupID, ref dx, ref ex, ref score, ref scoreD, al, "AND u.ProjectRoundUnitID IN (" + units2 + ")");

							if(dx < rac)
							{
								switch(LID)
								{
									case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120+(hasAID?75:0), 40);break;
									case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120+(hasAID?75:0), 40);break;
								}
							
							}
							else
							{
								g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + dx : ""), 7, 120+(hasAID?75:0), 40);

								if(t == 3)
								{
									g.drawMultiBar(7,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,bar1+bar2,100,!(AID1 == 0 && AID2 == 0),false);

									if(AID1 == 0 && AID2 == 0)
									{
										double scoreT = 0;
										IEnumerator alE = al.GetEnumerator();
										while(alE.MoveNext())
											scoreT += Math.Pow(Convert.ToDouble(alE.Current)-((double)scoreD/(double)dx),2);
										scoreT = Math.Sqrt(scoreT/dx);

										if(!noSD)
										{
											g.drawMultiStd(20,1,(float)Math.Round(score/(float)dx,2),(float)scoreT,g.steping,g.barW,div,bar1+bar2,100,true,false);
										}
									}
								}
								else
								{
									g.drawMultiBar(7,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,bar1+bar2,100,true,true);
								}
							}
							#endregion

							dx = 0; ex = 0;
							score = 0; scoreD = 0;
							al = new ArrayList();
						}

						#region Current round
						t3(groupID, ref dx, ref ex, ref score, ref scoreD, al, (units != "" ? " AND u.ProjectRoundUnitID IN (" + units + ") " : "") + (aids != "" ? " AND a.AnswerID IN (" + aids + ") " : ""));

						if(dx < rac)
						{
							switch(LID)
							{
								case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
								case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
							}
						
						}
						else
						{
							g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + dx : ""), 5, 120+(hasAID?75:0), 20);

							if(t == 3)
							{
								g.drawMultiBar(5,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,bar1+bar2+bar3,100,!(AID1 == 0 && AID2 == 0),false);

								if(AID1 == 0 && AID2 == 0)
								{
									double scoreT = 0;
									IEnumerator alE = al.GetEnumerator();
									while(alE.MoveNext())
										scoreT += Math.Pow(Convert.ToDouble(alE.Current)-((double)scoreD/(double)dx),2);
									scoreT = Math.Sqrt(scoreT/dx);

									if(!noSD)
									{
										g.drawMultiStd(20,1,(float)Math.Round(score/(float)dx,2),(float)scoreT,g.steping,g.barW,div,bar1+bar2+bar3,100,true,false);
									}
								}
							}
							else
							{
								g.drawMultiBar(5,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,bar1+bar2+bar3,100,true,true);
							}
						}
						#endregion

						dx = 0; ex = 0;
						score = 0; scoreD = 0;
						al = new ArrayList();
					}

					if(showTotal)
					{
						if(rr != 0)
						{
							#region Comparative round
							t3(groupID, ref dx, ref ex, ref score, ref scoreD, al, "AND r.Terminated IS NULL AND u.ProjectRoundID = " + rr);

							g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + dx : ""), 8, 320, 40);
							if(t == 3)
							{
								g.drawMultiBar(8,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,bar1+bar2+bar3+bar4,100,!(AID1 == 0 && AID2 == 0),false);

								if(AID1 == 0 && AID2 == 0)
								{
									double scoreT = 0;
									IEnumerator alE = al.GetEnumerator();
									while(alE.MoveNext())
										scoreT += Math.Pow(Convert.ToDouble(alE.Current)-((double)scoreD/(double)dx),2);
									scoreT = Math.Sqrt(scoreT/dx);

									if(!noSD)
									{
										g.drawMultiStd(20,1,(float)Math.Round(score/(float)dx,2),(float)scoreT,g.steping,g.barW,div,bar1+bar2+bar3+bar4,100,true,false);
									}
								}
							}
							else
							{
								g.drawAxisExpl("%", 0,false,false);
								g.drawMultiBar(8,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,bar1+bar2+bar3+bar4,100,true,true);
							}
							#endregion

							dx = 0; ex = 0;
							score = 0; scoreD = 0;
							al = new ArrayList();
						}

						#region Current round
						t3(groupID, ref dx, ref ex, ref score, ref scoreD, al, "AND r.Terminated IS NULL" + (r != 0 ? " AND u.ProjectRoundID = " + r : ""));

						g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + dx : ""), 6, 320, 20);
						if(t == 3)
						{
							g.drawMultiBar(6,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,bar1+bar2+bar3+bar4+bar5,100,!(AID1 == 0 && AID2 == 0),false);

							if(AID1 == 0 && AID2 == 0)
							{
								double scoreT = 0;
								IEnumerator alE = al.GetEnumerator();
								while(alE.MoveNext())
									scoreT += Math.Pow(Convert.ToDouble(alE.Current)-((double)scoreD/(double)dx),2);
								scoreT = Math.Sqrt(scoreT/dx);

								if(!noSD)
								{
									g.drawMultiStd(20,1,(float)Math.Round(score/(float)dx,2),(float)scoreT,g.steping,g.barW,div,bar1+bar2+bar3+bar4+bar5,100,true,false);
								}
							}
							else
							{
								if(a1 != -99)
								{
									g.drawColorExplBox(AID1txt, 20, 70, 20);
									g.drawMultiBar(20,1,(float)a1,g.steping,g.barW,div,bar1,100,true,false);
								}
								if(a2 != -99)
								{
									g.drawColorExplBox(AID2txt, 19, 70, 40);
									g.drawMultiBar(19,1,(float)a2,g.steping,g.barW,div,0,100,true,false);
								}
							}
						}
						else
						{
							g.drawAxisExpl("%", 0,false,false);
							g.drawMultiBar(6,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,bar1+bar2+bar3+bar4+bar5,100,true,true);
						}
						#endregion
					}
					#endregion
				}
				else if(t == 5)				// w=320
				{
					#region Depression
					g = new Graph(550,320,color);
					g.setMinMax(0f,100f);
					g.computeSteping(3);
					g.drawOutlines(11,true,false);
					g.drawAxis(false);
					//g.drawRightAxis();
					g.drawAxisExpl("%",0,false,false);

					rac = 20;

					int dx = 0, ex = 0, div = (showTotal ? (rr != 0 ? 2 : 1) : 0);

					int Q = 2;
					double[] v1 = new double[Q]; v1[0] = 0; v1[1] = 1;
					double[] n1 = new double[Q]; n1[0] = 0; n1[1] = 0;
					double[] v2 = new double[Q]; v2[0] = 0; v2[1] = 1;
					double[] n2 = new double[Q]; n2[0] = 0; n2[1] = 0;

					if(units != "" || aids != "")
					{
						div++;
					
						if(rr != 0 && units2 != "")
						{
							div++;

							#region Comparative round
							t5(groupID,ref dx, ref ex, ref n2, " AND u.ProjectRoundUnitID IN (" + units2 + ")");

							if(dx < rac)
							{
								switch(LID)
								{
									case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120, 40);break;
									case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120, 40);break;
								}
							
							}
							else
							{
								g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + dx : ""), 7, 120, 40);
								g.drawMultiBar(7,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,0,100,true,true);
							}
							#endregion

							dx = 0; ex = 0;
						}

						#region Current round
						t5(groupID,ref dx, ref ex, ref n1, (units != "" ? " AND u.ProjectRoundUnitID IN (" + units + ") " : "") + (aids != "" ? " AND a.AnswerID IN (" + aids + ") " : ""));

						if(dx < rac)
						{
							switch(LID)
							{
								case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120, 20);break;
								case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120, 20);break;
							}
						
						}
						else
						{
							g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + dx : ""), 5, 120, 20);
							g.drawMultiBar(5,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,(rr != 0 && units2 != "" ? 1 : 0),100,true,true);
						}
						#endregion

						if(rr != 0 && units2 != "" && (n1[0]+n1[1]) >= rac && (n2[0]+n2[1]) >= rac)
						{
							double tt = 0; int df = 0;
							bool sign = significant(Convert.ToDecimal(n1[0]+n1[1]),Convert.ToDecimal(n2[0]+n2[1]),Q,v1,n1,v2,n2,ref tt,ref df);

							g.drawStringInGraph("Change " + (sign ? "" : "not ") + "significant (t=" + Math.Round(tt,2) + ", p" + (sign ? "<" : ">") + "0.05, df " + df + ")",10,10);
						}

						dx = 0; ex = 0;
					}

					if(showTotal)
					{
						n1[0] = 0; n1[1] = 0;
						n2[0] = 0; n2[1] = 0;

						if(rr != 0)
						{
							#region Comparative round
							t5(groupID,ref dx, ref ex, ref n2, " AND r.Terminated IS NULL AND u.ProjectRoundID = " + rr);

							g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + dx : ""), 8, 320, 40);
							g.drawMultiBar(8,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,1*(div-2),100,true,true);

							#endregion
						
							dx = 0; ex = 0;
						}

						#region Current round
						t5(groupID,ref dx, ref ex, ref n1, " AND r.Terminated IS NULL" + (r != 0 ? " AND u.ProjectRoundID = " + r : ""));

						g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + dx : ""), 6, 320, 20);
						g.drawMultiBar(6,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,1*(div-1),100,true,true);

						#endregion

						if(rr != 0 && (n1[0]+n1[1]) >= rac && (n2[0]+n2[1]) >= rac)
						{
							double tt = 0; int df = 0;
							bool sign = significant(Convert.ToDecimal(n1[0]+n1[1]),Convert.ToDecimal(n2[0]+n2[1]),Q,v1,n1,v2,n2,ref tt,ref df);

							g.drawStringInGraph("Change " + (sign ? "" : "not ") + "significant (t=" + Math.Round(tt,2) + ", p" + (sign ? "<" : ">") + "0.05, df " + df + ")",260,10);
						}
					}
					#endregion
				}
				else if(t == 6)				// w=320
				{
					#region PBS+E
					g = new Graph(550,320,color);
					if(t == 1)
					{
						g.setMinMax(1f,4f);
						g.computeSteping(3);
						g.drawOutlines(7,true,false);
					}
					else
					{
						g.setMinMax(0f,100f);
						g.computeSteping(3);
						g.drawOutlines(11,true,false);
					}
					g.drawAxis(false);
					//g.drawRightAxis();

					int dx = 0, ex = 0, qx = 0, wx = 0, div = (showTotal ? (rr != 0 ? 2 : 1) : 0);
					float score = 0;

					if(units != "" || aids != "")
					{
						div++;

						if(rr != 0 && units2 != "")
						{
							div++;

							#region Comparative round
							t6(groupID, ref dx, ref ex, ref qx, ref wx, ref score, " AND u.ProjectRoundUnitID IN (" + units2 + ")");

							if(dx < rac)
							{
								switch(LID)
								{
									case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120, 40);break;
									case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120, 40);break;
								}
							
							}
							else
							{
								g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + dx : ""), 7, 120, 40);
								if(t == 1)
								{
									g.drawMultiBar(7,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,0,100,true,false);
								}
								else
								{
									g.drawMultiBar(7,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,0,100,true,true);
								}
							}
							#endregion

							dx = 0; ex = 0; qx = 0; wx = 0;
							score = 0;
						}

						#region Current round
						t6(groupID, ref dx, ref ex, ref qx, ref wx, ref score, (units != "" ? " AND u.ProjectRoundUnitID IN (" + units + ") " : "") + (aids != "" ? " AND a.AnswerID IN (" + aids + ") " : ""));
						
						if(dx < rac)
						{
							switch(LID)
							{
								case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120, 20);break;
								case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120, 20);break;
							}
						
						}
						else
						{
							g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + dx : ""), 5, 120, 20);
							if(t == 1)
							{
								g.drawMultiBar(5,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,(rr != 0 && units2 != "" ? 1 : 0),100,true,false);
							}
							else
							{
								g.drawMultiBar(5,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,(rr != 0 && units2 != "" ? 1 : 0),100,true,true);
							}
						}
						#endregion

						dx = 0; ex = 0; qx = 0; wx = 0;
						score = 0;
					}

					if(showTotal)
					{
						if(rr != 0)
						{
							#region Current round
							t6(groupID, ref dx, ref ex, ref qx, ref wx, ref score, " AND r.Terminated IS NULL AND u.ProjectRoundID = " + rr);
							
							g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + dx : ""), 8, 320, 40);
							if(t == 1)
							{
								g.drawMultiBar(8,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,1*(div-2),100,true,false);
							}
							else
							{
								g.drawAxisExpl("%", 0,false,false);
								g.drawMultiBar(8,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,1*(div-2),100,true,true);
							}
							#endregion

							dx = 0; ex = 0; qx = 0; wx = 0;
							score = 0;
						}

						#region Current round
						t6(groupID, ref dx, ref ex, ref qx, ref wx, ref score, " AND r.Terminated IS NULL" + (r != 0 ? " AND u.ProjectRoundID = " + r : ""));
						
						g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + dx : ""), 6, 320, 20);
						if(t == 1)
						{
							g.drawMultiBar(6,1,(float)Math.Round(score/(float)dx,2),g.steping,g.barW,div,1*(div-1),100,true,false);
						}
						else
						{
							g.drawAxisExpl("%", 0,false,false);
							g.drawMultiBar(6,1,(float)Math.Round(((float)ex/(float)dx)*100f,2),g.steping,g.barW,div,1*(div-1),100,true,true);
						}
						#endregion
					}
					#endregion
				}
				else if(t == 7 || t == 17)				// w=480
				{
					string sql = "";

					#region Init
					string s1 = "", s2 = "", s3 = "", s4 = "", s5 = "", s6 = "";
					switch(LID)
					{
						case 1:
							s1 = "Nöjd";
							s2 = "Varken eller";
							s3 = "Missnöjd";
							s4 = "Ofta";
							s5 = "Varken eller";
							s6 = "Sällan";
							break;
						case 2:
							s1 = "Satisfied";
							s2 = "Neither";
							s3 = "Dissatisfied";
							s4 = "Often";
							s5 = "Neither";
							s6 = "Rarely";
							break;
					}
					#endregion

					#region quadruple
					g = new Graph(550,480,color);

					int tl = 0, tc = 0, tr = 0, cl = 0, cc = 0, cr = 0, ll = 0, lc = 0, lr = 0, dx = 0;
					int ctl = 0, ctc = 0, ctr = 0, ccl = 0, ccc = 0, ccr = 0, cll = 0, clc = 0, clr = 0, cdx = 0;
				
					int tl2 = 0, tc2 = 0, tr2 = 0, cl2 = 0, cc2 = 0, cr2 = 0, ll2 = 0, lc2 = 0, lr2 = 0, dx2 = 0;
					int ctl2 = 0, ctc2 = 0, ctr2 = 0, ccl2 = 0, ccc2 = 0, ccr2 = 0, cll2 = 0, clc2 = 0, clr2 = 0, cdx2 = 0;

					float CTL = 0, CTL2 = 0;
					if(showTotal)
					{
						if(rr != 0)
						{
							// cdx2 contains number of answers
							#region Comparative round
							sql = "SELECT " +
								"COUNT(*), " +
								"av1.ValueInt, " +
								"av2.ValueInt " +
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 117 AND av1.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 118 AND av2.DeletedSessionID IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL AND p.ProjectRoundID = " + rr + " " +
								(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
								"GROUP BY av1.ValueInt, av2.ValueInt";
							if(t == 17)
							{
								sql = "" +
									"SELECT " +
									"COUNT(*), " +
									"372, " +
									"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
									"FROM ProjectRoundUser u " +
									"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
									"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
									"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
									"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
									"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL AND p.ProjectRoundID = " + rr + " " +
									"AND av1.ValueInt <= 40 " +
									(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
									"GROUP BY av2.ValueInt-2473 " +
									"UNION ALL " +
									"SELECT " +
									"COUNT(*), " +
									"373, " +
									"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
									"FROM ProjectRoundUser u " +
									"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
									"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
									"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
									"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
									"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL AND p.ProjectRoundID = " + rr + " " +
									"AND av1.ValueInt > 40 " +
									"AND av1.ValueInt < 60 " +
									(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
									"GROUP BY av2.ValueInt-2473 " +
									"UNION ALL " +
									"SELECT " +
									"COUNT(*), " +
									"374, " +
									"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
									"FROM ProjectRoundUser u " +
									"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
									"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
									"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
									"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
									"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL AND p.ProjectRoundID = " + rr + " " +
									"AND av1.ValueInt >= 60 " +
									(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
									"GROUP BY av2.ValueInt-2473 ";
							}
							rs = Db.sqlRecordSet(sql);
							while(rs.Read())
							{
								cdx2 += rs.GetInt32(0);
	
								//bool pos = false, mid = false;

								switch(rs.GetInt32(1) + ":" + rs.GetInt32(2))
								{
									case "370:375" : clr2 += rs.GetInt32(0); break;
									case "370:376" : clr2 += rs.GetInt32(0); break;
									case "370:377" : clc2 += rs.GetInt32(0); break;
									case "370:378" : cll2 += rs.GetInt32(0); break;
									case "370:379" : cll2 += rs.GetInt32(0); break;
									case "372:375" : clr2 += rs.GetInt32(0); break;
									case "372:376" : clr2 += rs.GetInt32(0); break;
									case "372:377" : clc2 += rs.GetInt32(0); break;
									case "372:378" : cll2 += rs.GetInt32(0); break;
									case "372:379" : cll2 += rs.GetInt32(0); break;

									case "373:375" : ccr2 += rs.GetInt32(0); break;
									case "373:376" : ccr2 += rs.GetInt32(0); break;
									case "373:377" : ccc2 += rs.GetInt32(0); break;
									case "373:378" : ccl2 += rs.GetInt32(0); break;
									case "373:379" : ccl2 += rs.GetInt32(0); break;

									case "374:375" : ctr2 += rs.GetInt32(0); break;
									case "374:376" : ctr2 += rs.GetInt32(0); break;
									case "374:377" : ctc2 += rs.GetInt32(0); break;
									case "374:378" : ctl2 += rs.GetInt32(0); break;
									case "374:379" : ctl2 += rs.GetInt32(0); break;
									case "371:375" : ctr2 += rs.GetInt32(0); break;
									case "371:376" : ctr2 += rs.GetInt32(0); break;
									case "371:377" : ctc2 += rs.GetInt32(0); break;
									case "371:378" : ctl2 += rs.GetInt32(0); break;
									case "371:379" : ctl2 += rs.GetInt32(0); break;
								}
							}
							rs.Close();

							if(cdx2 > 0)
							{
								CTL2 = (float)(100-
									Math.Round((float)clr2/(float)cdx2*100f,0)-
									Math.Round((float)clc2/(float)cdx2*100f,0)-
									Math.Round((float)cll2/(float)cdx2*100f,0)-
									Math.Round((float)ccr2/(float)cdx2*100f,0)-
									Math.Round((float)ccc2/(float)cdx2*100f,0)-
									Math.Round((float)ccl2/(float)cdx2*100f,0)-
									Math.Round((float)ctr2/(float)cdx2*100f,0)-
									Math.Round((float)ctc2/(float)cdx2*100f,0));
							}
							#endregion
						}

						// cdx contains number of answers
						#region Current round
						sql = "SELECT " +
							"COUNT(*), " +
							"av1.ValueInt, " +
							"av2.ValueInt " +
							"FROM ProjectRoundUser u " +
							"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
							"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 117 AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 118 AND av2.DeletedSessionID IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + (r != 0 ? " AND u.ProjectRoundID = " + r : "") + " " +
							(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
							"GROUP BY av1.ValueInt, av2.ValueInt";
						if(t == 17)
						{
							sql = "" +
								"SELECT " +
								"COUNT(*), " +
								"372, " +
								"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + (r != 0 ? " AND u.ProjectRoundID = " + r : "") + " " +
								"AND av1.ValueInt <= 40 " +
								(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
								"GROUP BY av2.ValueInt-2473 " +
								"UNION ALL " +
								"SELECT " +
								"COUNT(*), " +
								"373, " +
								"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + (r != 0 ? " AND u.ProjectRoundID = " + r : "") + " " +
								"AND av1.ValueInt > 40 " +
								"AND av1.ValueInt < 60 " +
								(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
								"GROUP BY av2.ValueInt-2473 " +
								"UNION ALL " +
								"SELECT " +
								"COUNT(*), " +
								"374, " +
								"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + (r != 0 ? " AND u.ProjectRoundID = " + r : "") + " " +
								"AND av1.ValueInt >= 60 " +
								(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
								"GROUP BY av2.ValueInt-2473 ";
						}
						rs = Db.sqlRecordSet(sql);
						while(rs.Read())
						{
							cdx += rs.GetInt32(0);
	
							//bool pos = false, mid = false;

							switch(rs.GetInt32(1) + ":" + rs.GetInt32(2))
							{
								case "370:375" : clr += rs.GetInt32(0); break;
								case "370:376" : clr += rs.GetInt32(0); break;
								case "370:377" : clc += rs.GetInt32(0); break;
								case "370:378" : cll += rs.GetInt32(0); break;
								case "370:379" : cll += rs.GetInt32(0); break;
								case "372:375" : clr += rs.GetInt32(0); break;
								case "372:376" : clr += rs.GetInt32(0); break;
								case "372:377" : clc += rs.GetInt32(0); break;
								case "372:378" : cll += rs.GetInt32(0); break;
								case "372:379" : cll += rs.GetInt32(0); break;

								case "373:375" : ccr += rs.GetInt32(0); break;
								case "373:376" : ccr += rs.GetInt32(0); break;
								case "373:377" : ccc += rs.GetInt32(0); break;
								case "373:378" : ccl += rs.GetInt32(0); break;
								case "373:379" : ccl += rs.GetInt32(0); break;

								case "374:375" : ctr += rs.GetInt32(0); break;
								case "374:376" : ctr += rs.GetInt32(0); break;
								case "374:377" : ctc += rs.GetInt32(0); break;
								case "374:378" : ctl += rs.GetInt32(0); break;
								case "374:379" : ctl += rs.GetInt32(0); break;
								case "371:375" : ctr += rs.GetInt32(0); break;
								case "371:376" : ctr += rs.GetInt32(0); break;
								case "371:377" : ctc += rs.GetInt32(0); break;
								case "371:378" : ctl += rs.GetInt32(0); break;
								case "371:379" : ctl += rs.GetInt32(0); break;
							}
						}
						rs.Close();

						if(cdx > 0)
						{
							CTL = (float)(100-
								Math.Round((float)clr/(float)cdx*100f,0)-
								Math.Round((float)clc/(float)cdx*100f,0)-
								Math.Round((float)cll/(float)cdx*100f,0)-
								Math.Round((float)ccr/(float)cdx*100f,0)-
								Math.Round((float)ccc/(float)cdx*100f,0)-
								Math.Round((float)ccl/(float)cdx*100f,0)-
								Math.Round((float)ctr/(float)cdx*100f,0)-
								Math.Round((float)ctc/(float)cdx*100f,0));
						}
						#endregion
					}

					if(units != "" || aids != "")
					{
						#region has units
						if(rr != 0 && units2 != "")
						{
							// dx2 contains number of answers
							#region Comparative round
							sql = "SELECT " +
								"av1.ValueInt, " +
								"av2.ValueInt " +
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 117 AND av1.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 118 AND av2.DeletedSessionID IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL " +
								(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
								"AND u.ProjectRoundUnitID IN (" + units2 + ")";
							if(t == 17)
							{
								sql = "" +
									"SELECT " +
									//"COUNT(*), " +
									"372, " +
									"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
									"FROM ProjectRoundUser u " +
									"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
									"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
									"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
									"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
									"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND u.ProjectRoundUnitID IN (" + units2 + ") " +
									"AND av1.ValueInt <= 40 " +
									(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
									//"GROUP BY av2.ValueInt-2473 " +	// why this never there?
									"UNION ALL " +
									"SELECT " +
									//"COUNT(*), " +
									"373, " +
									"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
									"FROM ProjectRoundUser u " +
									"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
									"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
									"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
									"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
									"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND u.ProjectRoundUnitID IN (" + units2 + ") " +
									"AND av1.ValueInt > 40 " +
									"AND av1.ValueInt < 60 " +
									(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
									//"GROUP BY av2.ValueInt-2473 " +
									"UNION ALL " +
									"SELECT " +
									//"COUNT(*), " +
									"374, " +
									"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
									"FROM ProjectRoundUser u " +
									"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
									"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
									"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
									"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
									"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND u.ProjectRoundUnitID IN (" + units2 + ") " +
									"AND av1.ValueInt >= 60 " +
									(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
									//"GROUP BY av2.ValueInt-2473 " +
									"";
							}
							rs = Db.sqlRecordSet(sql);
							while(rs.Read())
							{
								dx2 ++;
	
								bool pos2 = false, mid2 = false;

								switch(rs.GetInt32(0))
								{
									case 373: mid2 = true; break;
									case 374: pos2 = true; break;
									case 371: goto case 374;	// was 373
								}
								switch(rs.GetInt32(1))
								{
									case 377: if(pos2) { tc2++; } 
											  else if(mid2) { cc2++;}
											  else { lc2++; }
										break;
									case 378: if(pos2) { tl2++; }
											  else if(mid2) { cl2++; }
											  else { ll2++; }
										break;
									case 379: goto case 378;
									default:  if(pos2) { tr2++; } 
											  else if(mid2) { cr2++; }
											  else { lr2++; }
										break;
								}
							}
							rs.Close();
							#endregion
						}
						// dx contains number of answers
						#region Current round
						sql = "SELECT " +
							"av1.ValueInt, " +
							"av2.ValueInt " +
							"FROM ProjectRoundUser u " +
							"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
							"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 117 AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 118 AND av2.DeletedSessionID IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL " +
							(units != "" ? " AND u.ProjectRoundUnitID IN (" + units + ") " : "") +
							(aids != "" ? " AND a.AnswerID IN (" + aids + ") " : "") +
							(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
							"";
						if(t == 17)
						{
							sql = "" +
								"SELECT " +
								//"COUNT(*), " +
								"372, " +
								"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL " +
								(units != "" ? " AND u.ProjectRoundUnitID IN (" + units + ") " : "") +
								(aids != "" ? " AND a.AnswerID IN (" + aids + ") " : "") +
								"" +
								"AND av1.ValueInt <= 40 " +
								(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
								//"GROUP BY av2.ValueInt-2473 " +
								"UNION ALL " +
								"SELECT " +
								//"COUNT(*), " +
								"373, " +
								"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL " +
								(units != "" ? " AND u.ProjectRoundUnitID IN (" + units + ") " : "") +
								(aids != "" ? " AND a.AnswerID IN (" + aids + ") " : "") +
								"" +
								"AND av1.ValueInt > 40 " +
								"AND av1.ValueInt < 60 " +
								(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
								//"GROUP BY av2.ValueInt-2473 " +
								"UNION ALL " +
								"SELECT " +
								//"COUNT(*), " +
								"374, " +
								"av2.ValueInt-2473 " +	// range is 2848-2852, diff -2473
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 1080 AND av1.DeletedSessionID IS NULL " +
								"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 1079 AND av2.DeletedSessionID IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL " +
								(units != "" ? " AND u.ProjectRoundUnitID IN (" + units + ") " : "") +
								(aids != "" ? " AND a.AnswerID IN (" + aids + ") " : "") +
								"" +
								"AND av1.ValueInt >= 60 " +
								(groupID != 0 ? "AND u.GroupID = " + groupID + " " : "") +
								//"GROUP BY av2.ValueInt-2473 " +
								"";
						}
						rs = Db.sqlRecordSet(sql);
						while(rs.Read())
						{
							dx ++;
	
							bool pos = false, mid = false;

							switch(rs.GetInt32(0))
							{
								case 373: mid = true; break;
								case 374: pos = true; break;
								case 371: goto case 374;	// was 373
							}
							switch(rs.GetInt32(1))
							{
								case 377: if(pos) { tc++; } 
										  else if(mid) { cc++;}
										  else { lc++; }
									break;
								case 378: if(pos) { tl++; }
										  else if(mid) { cl++; }
										  else { ll++; }
									break;
								case 379: goto case 378;
								default:  if(pos) { tr++; } 
										  else if(mid) { cr++; }
										  else { lr++; }
									break;
							}
						}
						rs.Close();
						#endregion

						if(dx == 0 && dx2 == 0 && cdx == 0 && cdx2 == 0)
						{
							switch(LID)
							{
								case 1: g.drawStringInGraph("INGEN ÅTERKOPPLING", 200, 200);break;
								case 2: g.drawStringInGraph("NO FEEDBACK", 200, 200);break;
							}						
						}
						else
						{
							float TL = 0;
							if(dx > 0)
							{
								TL = (float)(100-
									Math.Round((float)lr/(float)dx*100f,0)-
									Math.Round((float)lc/(float)dx*100f,0)-
									Math.Round((float)ll/(float)dx*100f,0)-
									Math.Round((float)cr/(float)dx*100f,0)-
									Math.Round((float)cc/(float)dx*100f,0)-
									Math.Round((float)cl/(float)dx*100f,0)-
									Math.Round((float)tr/(float)dx*100f,0)-
									Math.Round((float)tc/(float)dx*100f,0));
							}
							float TL2 = 0;
							if(rr != 0 && units2 != "" && dx2 > 0)
							{
								TL2 = (float)(100-
									Math.Round((float)lr2/(float)dx2*100f,0)-
									Math.Round((float)lc2/(float)dx2*100f,0)-
									Math.Round((float)ll2/(float)dx2*100f,0)-
									Math.Round((float)cr2/(float)dx2*100f,0)-
									Math.Round((float)cc2/(float)dx2*100f,0)-
									Math.Round((float)cl2/(float)dx2*100f,0)-
									Math.Round((float)tr2/(float)dx2*100f,0)-
									Math.Round((float)tc2/(float)dx2*100f,0));
							}

							if(showTotal)
							{
								if(rr != 0 && units2 != "" && dx2 >= rac && dx2 > 0 && cdx2 >= rac && cdx2 > 0)
								{
									#region top comp vs curr, unit comp vs curr
									if(dx < rac || dx == 0)
									{
										if(cdx < rac || cdx == 0)
										{
											g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
												Math.Max(0,TL2) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Max(0,CTL2) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												Math.Round((float)tc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)ctc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												Math.Round((float)tr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)ctr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												Math.Round((float)cl2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)ccl2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												Math.Round((float)cc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)ccc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												Math.Round((float)cr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)ccr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												Math.Round((float)ll2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)cll2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												Math.Round((float)lc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)clc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												Math.Round((float)lr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)clr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												HttpContext.Current.Server.UrlDecode(q1),
												HttpContext.Current.Server.UrlDecode(q2),grey);
										}
										else
										{
											g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
												Math.Max(0,TL2) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Max(0,CTL2) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Max(0,CTL) + "%)",
												Math.Round((float)tc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)ctc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ctc/(float)cdx*100f,0) + "%)",
												Math.Round((float)tr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)ctr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ctr/(float)cdx*100f,0) + "%)",
												Math.Round((float)cl2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)ccl2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ccl/(float)cdx*100f,0) + "%)",
												Math.Round((float)cc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)ccc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ccc/(float)cdx*100f,0) + "%)",
												Math.Round((float)cr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)ccr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ccr/(float)cdx*100f,0) + "%)",
												Math.Round((float)ll2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)cll2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)cll/(float)cdx*100f,0) + "%)",
												Math.Round((float)lc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)clc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)clc/(float)cdx*100f,0) + "%)",
												Math.Round((float)lr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)clr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)clr/(float)cdx*100f,0) + "%)",
												HttpContext.Current.Server.UrlDecode(q1),
												HttpContext.Current.Server.UrlDecode(q2),grey);
										}
									}
									else
									{
										g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
											Math.Max(0,TL2) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Max(0,TL) + "%","(" + Math.Max(0,CTL2) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Max(0,CTL) + "%)",
											Math.Round((float)tc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)tc/(float)dx*100f,0) + "%","(" + Math.Round((float)ctc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ctc/(float)cdx*100f,0) + "%)",
											Math.Round((float)tr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)tr/(float)dx*100f,0) + "%","(" + Math.Round((float)ctr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ctr/(float)cdx*100f,0) + "%)",
											Math.Round((float)cl2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)cl/(float)dx*100f,0) + "%","(" + Math.Round((float)ccl2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ccl/(float)cdx*100f,0) + "%)",
											Math.Round((float)cc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)cc/(float)dx*100f,0) + "%","(" + Math.Round((float)ccc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ccc/(float)cdx*100f,0) + "%)",
											Math.Round((float)cr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)cr/(float)dx*100f,0) + "%","(" + Math.Round((float)ccr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ccr/(float)cdx*100f,0) + "%)",
											Math.Round((float)ll2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ll/(float)dx*100f,0) + "%","(" + Math.Round((float)cll2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)cll/(float)cdx*100f,0) + "%)",
											Math.Round((float)lc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)lc/(float)dx*100f,0) + "%","(" + Math.Round((float)clc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)clc/(float)cdx*100f,0) + "%)",
											Math.Round((float)lr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)lr/(float)dx*100f,0) + "%","(" + Math.Round((float)clr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)clr/(float)cdx*100f,0) + "%)",
											HttpContext.Current.Server.UrlDecode(q1),
											HttpContext.Current.Server.UrlDecode(q2),grey);
									}
									#endregion
								}
								else if(rr != 0 && cdx2 >= rac && cdx2 > 0)
								{
									#region top comp vs curr, unit curr
									if(cdx < rac || cdx == 0)
									{
										if(dx < rac || dx == 0)
										{
											g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
												"?","(" + Math.Max(0,CTL2) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												"?","(" + Math.Round((float)ctc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												"?","(" + Math.Round((float)ctr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												"?","(" + Math.Round((float)ccl2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												"?","(" + Math.Round((float)ccc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												"?","(" + Math.Round((float)ccr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												"?","(" + Math.Round((float)cll2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												"?","(" + Math.Round((float)clc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												"?","(" + Math.Round((float)clr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												HttpContext.Current.Server.UrlDecode(q1),
												HttpContext.Current.Server.UrlDecode(q2),grey);
										}
										else
										{
											g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
												Math.Max(0,TL) + "%","(" + Math.Max(0,CTL2) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												Math.Round((float)tc/(float)dx*100f,0) + "%","(" + Math.Round((float)ctc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												Math.Round((float)tr/(float)dx*100f,0) + "%","(" + Math.Round((float)ctr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												Math.Round((float)cl/(float)dx*100f,0) + "%","(" + Math.Round((float)ccl2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												Math.Round((float)cc/(float)dx*100f,0) + "%","(" + Math.Round((float)ccc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												Math.Round((float)cr/(float)dx*100f,0) + "%","(" + Math.Round((float)ccr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												Math.Round((float)ll/(float)dx*100f,0) + "%","(" + Math.Round((float)cll2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												Math.Round((float)lc/(float)dx*100f,0) + "%","(" + Math.Round((float)clc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												Math.Round((float)lr/(float)dx*100f,0) + "%","(" + Math.Round((float)clr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?)",
												HttpContext.Current.Server.UrlDecode(q1),
												HttpContext.Current.Server.UrlDecode(q2),grey);
										}
									}
									else
									{
										g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
											Math.Max(0,TL) + "%","(" + Math.Max(0,CTL2) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Max(0,CTL) + "%)",
											Math.Round((float)tc/(float)dx*100f,0) + "%","(" + Math.Round((float)ctc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ctc/(float)cdx*100f,0) + "%)",
											Math.Round((float)tr/(float)dx*100f,0) + "%","(" + Math.Round((float)ctr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ctr/(float)cdx*100f,0) + "%)",
											Math.Round((float)cl/(float)dx*100f,0) + "%","(" + Math.Round((float)ccl2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ccl/(float)cdx*100f,0) + "%)",
											Math.Round((float)cc/(float)dx*100f,0) + "%","(" + Math.Round((float)ccc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ccc/(float)cdx*100f,0) + "%)",
											Math.Round((float)cr/(float)dx*100f,0) + "%","(" + Math.Round((float)ccr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ccr/(float)cdx*100f,0) + "%)",
											Math.Round((float)ll/(float)dx*100f,0) + "%","(" + Math.Round((float)cll2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)cll/(float)cdx*100f,0) + "%)",
											Math.Round((float)lc/(float)dx*100f,0) + "%","(" + Math.Round((float)clc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)clc/(float)cdx*100f,0) + "%)",
											Math.Round((float)lr/(float)dx*100f,0) + "%","(" + Math.Round((float)clr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)clr/(float)cdx*100f,0) + "%)",
											HttpContext.Current.Server.UrlDecode(q1),
											HttpContext.Current.Server.UrlDecode(q2),grey);
									}
									#endregion
								}
								else if (units2 != "" && dx2 >= rac && dx2 > 0 && cdx >= rac && cdx > 0)
								{
									#region top curr, unit comp vs curr
									if(dx < rac || dx == 0)
									{
										g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
											Math.Max(0,TL2) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Max(0,CTL) + "%)",
											Math.Round((float)tc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)ctc/(float)cdx*100f,0) + "%)",
											Math.Round((float)tr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)ctr/(float)cdx*100f,0) + "%)",
											Math.Round((float)cl2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)ccl/(float)cdx*100f,0) + "%)",
											Math.Round((float)cc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)ccc/(float)cdx*100f,0) + "%)",
											Math.Round((float)cr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)ccr/(float)cdx*100f,0) + "%)",
											Math.Round((float)ll2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)cll/(float)cdx*100f,0) + "%)",
											Math.Round((float)lc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)clc/(float)cdx*100f,0) + "%)",
											Math.Round((float)lr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","(" + Math.Round((float)clr/(float)cdx*100f,0) + "%)",
											HttpContext.Current.Server.UrlDecode(q1),
											HttpContext.Current.Server.UrlDecode(q2),grey);
									}
									else
									{
										g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
											Math.Max(0,TL2) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Max(0,TL) + "%","(" + Math.Max(0,CTL) + "%)",
											Math.Round((float)tc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)tc/(float)dx*100f,0) + "%","(" + Math.Round((float)ctc/(float)cdx*100f,0) + "%)",
											Math.Round((float)tr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)tr/(float)dx*100f,0) + "%","(" + Math.Round((float)ctr/(float)cdx*100f,0) + "%)",
											Math.Round((float)cl2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)cl/(float)dx*100f,0) + "%","(" + Math.Round((float)ccl/(float)cdx*100f,0) + "%)",
											Math.Round((float)cc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)cc/(float)dx*100f,0) + "%","(" + Math.Round((float)ccc/(float)cdx*100f,0) + "%)",
											Math.Round((float)cr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)cr/(float)dx*100f,0) + "%","(" + Math.Round((float)ccr/(float)cdx*100f,0) + "%)",
											Math.Round((float)ll2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ll/(float)dx*100f,0) + "%","(" + Math.Round((float)cll/(float)cdx*100f,0) + "%)",
											Math.Round((float)lc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)lc/(float)dx*100f,0) + "%","(" + Math.Round((float)clc/(float)cdx*100f,0) + "%)",
											Math.Round((float)lr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)lr/(float)dx*100f,0) + "%","(" + Math.Round((float)clr/(float)cdx*100f,0) + "%)",
											HttpContext.Current.Server.UrlDecode(q1),
											HttpContext.Current.Server.UrlDecode(q2),grey);
									}
									#endregion
								}
								else
								{
									#region top curr, unit curr
									if(cdx < rac || cdx == 0)
									{
										if(dx < rac || dx == 0)
										{
											switch(LID)
											{
												case 1: g.drawStringInGraph("INGEN ÅTERKOPPLING", 200, 200);break;
												case 2: g.drawStringInGraph("NO FEEDBACK", 200, 200);break;
											}						
										}
										else
										{
											g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
												Math.Max(0,TL) + "%","",
												Math.Round((float)tc/(float)dx*100f,0) + "%","",
												Math.Round((float)tr/(float)dx*100f,0) + "%","",
												Math.Round((float)cl/(float)dx*100f,0) + "%","",
												Math.Round((float)cc/(float)dx*100f,0) + "%","",
												Math.Round((float)cr/(float)dx*100f,0) + "%","",
												Math.Round((float)ll/(float)dx*100f,0) + "%","",
												Math.Round((float)lc/(float)dx*100f,0) + "%","",
												Math.Round((float)lr/(float)dx*100f,0) + "%","",
												HttpContext.Current.Server.UrlDecode(q1),
												HttpContext.Current.Server.UrlDecode(q2),grey);
										}					
									}
									else
									{
										g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
											Math.Max(0,TL) + "%","(" + Math.Max(0,CTL) + "%)",
											Math.Round((float)tc/(float)dx*100f,0) + "%","(" + Math.Round((float)ctc/(float)cdx*100f,0) + "%)",
											Math.Round((float)tr/(float)dx*100f,0) + "%","(" + Math.Round((float)ctr/(float)cdx*100f,0) + "%)",
											Math.Round((float)cl/(float)dx*100f,0) + "%","(" + Math.Round((float)ccl/(float)cdx*100f,0) + "%)",
											Math.Round((float)cc/(float)dx*100f,0) + "%","(" + Math.Round((float)ccc/(float)cdx*100f,0) + "%)",
											Math.Round((float)cr/(float)dx*100f,0) + "%","(" + Math.Round((float)ccr/(float)cdx*100f,0) + "%)",
											Math.Round((float)ll/(float)dx*100f,0) + "%","(" + Math.Round((float)cll/(float)cdx*100f,0) + "%)",
											Math.Round((float)lc/(float)dx*100f,0) + "%","(" + Math.Round((float)clc/(float)cdx*100f,0) + "%)",
											Math.Round((float)lr/(float)dx*100f,0) + "%","(" + Math.Round((float)clr/(float)cdx*100f,0) + "%)",
											HttpContext.Current.Server.UrlDecode(q1),
											HttpContext.Current.Server.UrlDecode(q2),grey);
									}
									#endregion
								}
								//g.drawQuadrant(tl.ToString(),tr.ToString(),ll.ToString(),lr.ToString());
							}
							else
							{
								if(rr != 0 && units2 != "" && dx2 >= rac && dx2 > 0)
								{
									#region unit comp vs curr
									if(dx < rac || dx == 0)
									{
										g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
											Math.Max(0,TL2) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
											Math.Round((float)tc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
											Math.Round((float)tr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
											Math.Round((float)cl2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
											Math.Round((float)cc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
											Math.Round((float)cr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
											Math.Round((float)ll2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
											Math.Round((float)lc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
											Math.Round((float)lr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
											HttpContext.Current.Server.UrlDecode(q1),
											HttpContext.Current.Server.UrlDecode(q2),grey);
									}
									else
									{
										g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
											Math.Max(0,TL2) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Max(0,TL) + "%","",
											Math.Round((float)tc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)tc/(float)dx*100f,0) + "%","",
											Math.Round((float)tr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)tr/(float)dx*100f,0) + "%","",
											Math.Round((float)cl2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)cl/(float)dx*100f,0) + "%","",
											Math.Round((float)cc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)cc/(float)dx*100f,0) + "%","",
											Math.Round((float)cr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)cr/(float)dx*100f,0) + "%","",
											Math.Round((float)ll2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ll/(float)dx*100f,0) + "%","",
											Math.Round((float)lc2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)lc/(float)dx*100f,0) + "%","",
											Math.Round((float)lr2/(float)dx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)lr/(float)dx*100f,0) + "%","",
											HttpContext.Current.Server.UrlDecode(q1),
											HttpContext.Current.Server.UrlDecode(q2),grey);
									}
									#endregion
								}
								else
								{
									#region unit curr
									if(dx < rac || dx == 0)
									{
										switch(LID)
										{
											case 1: g.drawStringInGraph("INGEN ÅTERKOPPLING", 200, 200);break;
											case 2: g.drawStringInGraph("NO FEEDBACK", 200, 200);break;
										}						
									}
									else
									{
										g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
											Math.Max(0,TL) + "%","",
											Math.Round((float)tc/(float)dx*100f,0) + "%","",
											Math.Round((float)tr/(float)dx*100f,0) + "%","",
											Math.Round((float)cl/(float)dx*100f,0) + "%","",
											Math.Round((float)cc/(float)dx*100f,0) + "%","",
											Math.Round((float)cr/(float)dx*100f,0) + "%","",
											Math.Round((float)ll/(float)dx*100f,0) + "%","",
											Math.Round((float)lc/(float)dx*100f,0) + "%","",
											Math.Round((float)lr/(float)dx*100f,0) + "%","",
											HttpContext.Current.Server.UrlDecode(q1),
											HttpContext.Current.Server.UrlDecode(q2),grey);
									}
									#endregion
								}
							}
						}
						#endregion
					}
					else
					{
						#region only top-level
						if(rr != 0 && cdx2 >= rac && cdx2 > 0)
						{
							// uses c**2
							#region something
							if(cdx < rac || cdx == 0)
							{
								g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
									Math.Max(0,CTL2) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
									Math.Round((float)ctc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
									Math.Round((float)ctr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
									Math.Round((float)ccl2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
									Math.Round((float)ccc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
									Math.Round((float)ccr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
									Math.Round((float)cll2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
									Math.Round((float)clc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
									Math.Round((float)clr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " ?","",
									HttpContext.Current.Server.UrlDecode(q1),
									HttpContext.Current.Server.UrlDecode(q2),grey);
							}
							else
							{
								g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
									Math.Max(0,CTL2) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Max(0,CTL) + "%","",
									Math.Round((float)ctc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ctc/(float)cdx*100f,0) + "%","",
									Math.Round((float)ctr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ctr/(float)cdx*100f,0) + "%","",
									Math.Round((float)ccl2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ccl/(float)cdx*100f,0) + "%","",
									Math.Round((float)ccc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ccc/(float)cdx*100f,0) + "%","",
									Math.Round((float)ccr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)ccr/(float)cdx*100f,0) + "%","",
									Math.Round((float)cll2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)cll/(float)cdx*100f,0) + "%","",
									Math.Round((float)clc2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)clc/(float)cdx*100f,0) + "%","",
									Math.Round((float)clr2/(float)cdx2*100f,0) + "% " + HttpContext.Current.Server.HtmlDecode("&#8594;") + " " + Math.Round((float)clr/(float)cdx*100f,0) + "%","",
									HttpContext.Current.Server.UrlDecode(q1),
									HttpContext.Current.Server.UrlDecode(q2),grey);
							}
							#endregion
						}
						else
						{
							// uses c**
							#region something
							if(cdx < rac || cdx == 0)
							{
								switch(LID)
								{
									case 1: g.drawStringInGraph("INGEN ÅTERKOPPLING", 200, 200);break;
									case 2: g.drawStringInGraph("NO FEEDBACK", 200, 200);break;
								}						
							}
							else
							{
								g.drawNiner(fn,s1,s2,s3,s4,s5,s6,
									Math.Max(0,CTL) + "%","",
									Math.Round((float)ctc/(float)cdx*100f,0) + "%","",
									Math.Round((float)ctr/(float)cdx*100f,0) + "%","",
									Math.Round((float)ccl/(float)cdx*100f,0) + "%","",
									Math.Round((float)ccc/(float)cdx*100f,0) + "%","",
									Math.Round((float)ccr/(float)cdx*100f,0) + "%","",
									Math.Round((float)cll/(float)cdx*100f,0) + "%","",
									Math.Round((float)clc/(float)cdx*100f,0) + "%","",
									Math.Round((float)clr/(float)cdx*100f,0) + "%","",
									HttpContext.Current.Server.UrlDecode(q1),
									HttpContext.Current.Server.UrlDecode(q2),grey);
							}
							#endregion
						}
						#endregion
					}

					#region Individual
					string a1 = "", a2 = "";
					if(AID1 != 0)
					{
						rs = Db.sqlRecordSet("SELECT " +
							"av1.ValueInt, " +
							"av2.ValueInt " +
							"FROM Answer a " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 117 AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 118 AND av2.DeletedSessionID IS NULL " +
							"WHERE a.AnswerID = " + AID1);
						if(rs.Read() && !rs.IsDBNull(0) && !rs.IsDBNull(1))
						{
							a1 = rs.GetInt32(0) + ":" + rs.GetInt32(1);

							switch(a1)
							{
								case "370:375" : a1 = "33"; break;
								case "370:376" : a1 = "33"; break;
								case "370:377" : a1 = "23"; break;
								case "370:378" : a1 = "13"; break;
								case "370:379" : a1 = "13"; break;
								case "372:375" : a1 = "33"; break;
								case "372:376" : a1 = "33"; break;
								case "372:377" : a1 = "23"; break;
								case "372:378" : a1 = "13"; break;
								case "372:379" : a1 = "13"; break;

								case "373:375" : a1 = "32"; break;
								case "373:376" : a1 = "32"; break;
								case "373:377" : a1 = "22"; break;
								case "373:378" : a1 = "12"; break;
								case "373:379" : a1 = "12"; break;

								case "374:375" : a1 = "31"; break;
								case "374:376" : a1 = "31"; break;
								case "374:377" : a1 = "21"; break;
								case "374:378" : a1 = "11"; break;
								case "374:379" : a1 = "11"; break;
								case "371:375" : a1 = "31"; break;
								case "371:376" : a1 = "31"; break;
								case "371:377" : a1 = "21"; break;
								case "371:378" : a1 = "11"; break;
								case "371:379" : a1 = "11"; break;
							}
						}
						rs.Close();
					}
					if(AID2 != 0)
					{
						rs = Db.sqlRecordSet("SELECT " +
							"av1.ValueInt, " +
							"av2.ValueInt " +
							"FROM Answer a " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = 117 AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = " + q + " AND av2.OptionID = 118 AND av2.DeletedSessionID IS NULL " +
							"WHERE a.AnswerID = " + AID2);
						if(rs.Read() && !rs.IsDBNull(0) && !rs.IsDBNull(1))
						{
							a2 = rs.GetInt32(0) + ":" + rs.GetInt32(1);

							switch(a2)
							{
								case "370:375" : a2 = "33"; break;
								case "370:376" : a2 = "33"; break;
								case "370:377" : a2 = "23"; break;
								case "370:378" : a2 = "13"; break;
								case "370:379" : a2 = "13"; break;
								case "372:375" : a2 = "33"; break;
								case "372:376" : a2 = "33"; break;
								case "372:377" : a2 = "23"; break;
								case "372:378" : a2 = "13"; break;
								case "372:379" : a2 = "13"; break;

								case "373:375" : a2 = "32"; break;
								case "373:376" : a2 = "32"; break;
								case "373:377" : a2 = "22"; break;
								case "373:378" : a2 = "12"; break;
								case "373:379" : a2 = "12"; break;

								case "374:375" : a2 = "31"; break;
								case "374:376" : a2 = "31"; break;
								case "374:377" : a2 = "21"; break;
								case "374:378" : a2 = "11"; break;
								case "374:379" : a2 = "11"; break;
								case "371:375" : a2 = "31"; break;
								case "371:376" : a2 = "31"; break;
								case "371:377" : a2 = "21"; break;
								case "371:378" : a2 = "11"; break;
								case "371:379" : a2 = "11"; break;
							}
						}
						rs.Close();
					}

					if(a1 != "")
					{
						switch(a1)
						{
							case "33" : g.drawDotNiner(false,(a1 == a2 ? -10 : 0),18,3,3); break;
							case "23" : g.drawDotNiner(false,(a1 == a2 ? -10 : 0),18,2,3); break;
							case "13" : g.drawDotNiner(false,(a1 == a2 ? -10 : 0),18,1,3); break;

							case "32" : g.drawDotNiner(false,(a1 == a2 ? -10 : 0),18,3,2); break;
							case "22" : g.drawDotNiner(false,(a1 == a2 ? -10 : 0),18,2,2); break;
							case "12" : g.drawDotNiner(false,(a1 == a2 ? -10 : 0),18,1,2); break;

							case "31" : g.drawDotNiner(false,(a1 == a2 ? -10 : 0),18,3,1); break;
							case "21" : g.drawDotNiner(false,(a1 == a2 ? -10 : 0),18,2,1); break;
							case "11" : g.drawDotNiner(false,(a1 == a2 ? -10 : 0),18,1,1); break;
						}

						g.drawColorExplBoxRight(AID1txt,20,20,g.w-20);
					}
					if(a2 != "")
					{
						switch(a2)
						{
							case "33" : g.drawDotNiner(true,(a1 == a2 ? 10 : 0),18,3,3); break;
							case "23" : g.drawDotNiner(true,(a1 == a2 ? 10 : 0),18,2,3); break;
							case "13" : g.drawDotNiner(true,(a1 == a2 ? 10 : 0),18,1,3); break;

							case "32" : g.drawDotNiner(true,(a1 == a2 ? 10 : 0),18,3,2); break;
							case "22" : g.drawDotNiner(true,(a1 == a2 ? 10 : 0),18,2,2); break;
							case "12" : g.drawDotNiner(true,(a1 == a2 ? 10 : 0),18,1,2); break;

							case "31" : g.drawDotNiner(true,(a1 == a2 ? 10 : 0),18,3,1); break;
							case "21" : g.drawDotNiner(true,(a1 == a2 ? 10 : 0),18,2,1); break;
							case "11" : g.drawDotNiner(true,(a1 == a2 ? 10 : 0),18,1,1); break;
						}

						g.drawColorExplBoxRight(AID2txt,19,150,g.w-20);
					}
					#endregion
					#endregion
				}
				else if(t == 8)				// w=320
				{
					#region VAS
					g = new Graph(550,320,color);
					g.leftSpacing = 150;
					g.setMinMax(0f,100f);
					g.computeSteping(3);
					int steps = 5;
					rs = Db.sqlRecordSet("SELECT COUNT(*) FROM OptionComponents WHERE OptionID = " + o);
					if(rs.Read())
					{
						steps = rs.GetInt32(0);
					}
					rs.Close();
					g.drawOutlines(steps,false,false);
					g.drawAxis(false);
					//g.drawRightAxis();

					bool hasAID = false;

					int dx = 0;
					float score = 0; float std = 0;
					int bar1 = 0, bar2 = 0, bar3 = 0, bar4 = 0, bar5 = 0;

					int tmp = 0;
					rs = Db.sqlRecordSet("SELECT oc.OptionComponentID, ocl.Text, ocs.ExportValue, oc.ExportValue FROM OptionComponents ocs " +
						"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
						"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + LID + " " +
						"WHERE ocs.OptionID = " + o + " ORDER BY ocs.SortOrder");
					while(rs.Read())
					{
						if(!extremeValuesOnly || tmp == 0 || tmp + 1 == steps)
						{
							string tt = cut(rs.GetString(1));
							g.drawAxisVal(tt + (exportValues && (!rs.IsDBNull(2) || !rs.IsDBNull(3)) ? " - " + (rs.IsDBNull(2) ? rs.GetInt32(3) : rs.GetInt32(2)) : ""),steps,dx);
						}
						dx ++;
						tmp ++;
					}
					rs.Close();

					int a1 = -99;
					int a2 = -99;
					int div = 0;
					
					if(showTotal)
					{
						div++;
						if(rr != 0)
						{
							div++;
							bar5 = 1;
						}
					}

					if(AID1 != 0)
					{
						hasAID = true;

						rs = Db.sqlRecordSet("SELECT av.ValueInt FROM AnswerValue av WHERE av.QuestionID = " + q + " AND av.OptionID = " + o + " AND av.AnswerID = " + AID1 + " AND av.DeletedSessionID IS NULL");
						if(rs.Read())
						{
							a1 = rs.GetInt32(0);
							div++;
							bar2 = 1;
						}
						rs.Close();
					}
					if(AID2 != 0)
					{
						rs = Db.sqlRecordSet("SELECT av.ValueInt FROM AnswerValue av WHERE av.QuestionID = " + q + " AND av.OptionID = " + o + " AND av.AnswerID = " + AID2 + " AND av.DeletedSessionID IS NULL");
						if(rs.Read())
						{
							a2 = rs.GetInt32(0);
							div++;
							bar1 = 1;
						}
						rs.Close();
					}

					dx = 0;
					if(units != "" || aids != "")
					{
						string sql = "";

						div++;
						bar4 = 1;

						if(rr != 0 && units2 != "")
						{
							div++;
							bar3 = 1;

							#region Comparative round
							sql = "SELECT " +
								"AVG(av1.ValueInt), COUNT(av1.ValueInt), STDEV(av1.ValueInt) " +
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND u.ProjectRoundUnitID IN (" + units2 + ")" +
								(groupID != 0 ? " AND u.GroupID = " + groupID : "");
							rs = Db.sqlRecordSet(sql);
							if(rs.Read())
							{
								dx = 0; score = 0; std = 0;
								if(!rs.IsDBNull(1))
									dx = Convert.ToInt32(rs.GetValue(1));
								if(!rs.IsDBNull(0))
									score = (float)Convert.ToDecimal(rs.GetValue(0));
								if(!rs.IsDBNull(2))
									std = (float)Convert.ToDecimal(rs.GetValue(2));
							}
							rs.Close();

							if(dx < rac)
							{
								switch(LID)
								{
									case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120+(hasAID?75:0), 40);break;
									case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120+(hasAID?75:0), 40);break;
								}
							
							}
							else
							{
								g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + dx : ""), 7, 120+(hasAID?75:0), 40);
								g.drawMultiBar(7,1,(float)Math.Round(score,2),g.steping,g.barW,div,bar1+bar2,100,false,false);
								if(AID1 == 0 && AID2 == 0)
								{
									if(!noSD)
									{
										g.drawMultiStd(20,1,(float)Math.Round(score,2),std,g.steping,g.barW,div,bar1+bar2,100,false,false);
									}
								}
							}
							#endregion

							dx = 0;
							score = 0; std = 0;
						}

						#region Current round
						sql = "SELECT " +
							"AVG(av1.ValueInt), COUNT(av1.ValueInt), STDEV(av1.ValueInt) " +
							"FROM ProjectRoundUser u " +
							"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
							"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL " +
							(units != "" ? " AND u.ProjectRoundUnitID IN (" + units + ") " : "") +
							(aids != "" ? " AND a.AnswerID IN (" + aids + ") " : "") +
							(groupID != 0 ? " AND u.GroupID = " + groupID : "") +
							"";
						rs = Db.sqlRecordSet(sql);
						if(rs.Read())
						{
							dx = 0; score = 0; std = 0;
							if(!rs.IsDBNull(1))
								dx = Convert.ToInt32(rs.GetValue(1));
							if(!rs.IsDBNull(0))
								score = (float)Convert.ToDecimal(rs.GetValue(0));
							if(!rs.IsDBNull(2))
								std = (float)Convert.ToDecimal(rs.GetValue(2));
						}
						rs.Close();

						if(dx < rac)
						{
							switch(LID)
							{
								case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
								case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
							}
						
						}
						else
						{
							g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + dx : ""), 5, 120+(hasAID?75:0), 20);
							g.drawMultiBar(5,1,(float)Math.Round(score,2),g.steping,g.barW,div,bar1+bar2+bar3,100,false,false);
							if(AID1 == 0 && AID2 == 0)
							{
								if(!noSD)
								{
									g.drawMultiStd(20,1,(float)Math.Round(score,2),std,g.steping,g.barW,div,bar1+bar2+bar3,100,false,false);
								}
							}
						}
						#endregion

						dx = 0;
						score = 0; std = 0;
					}

					if(a1 != -99)
					{
						g.drawColorExplBox(AID1txt, 20, 70, 20);
						g.drawMultiBar(20,1,(float)a1,g.steping,g.barW,div,bar1,100,false,false);
					}
					if(a2 != -99)
					{
						g.drawColorExplBox(AID2txt, 19, 70, 40);
						g.drawMultiBar(19,1,(float)a2,g.steping,g.barW,div,0,100,false,false);
					}

					if(showTotal)
					{
						if(rr != 0)
						{
							#region Comparative round
							rs = Db.sqlRecordSet("SELECT " +
								"AVG(av1.ValueInt), COUNT(av1.ValueInt), STDEV(av1.ValueInt) " +
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL AND u.ProjectRoundID = " + rr +
								(groupID != 0 ? " AND u.GroupID = " + groupID : ""));
							if(rs.Read())
							{
								dx = 0; score = 0; std = 0;
								if(!rs.IsDBNull(1))
									dx = Convert.ToInt32(rs.GetValue(1));
								if(!rs.IsDBNull(0))
									score = (float)Convert.ToDecimal(rs.GetValue(0));
								if(!rs.IsDBNull(2))
									std = (float)Convert.ToDecimal(rs.GetValue(2));
							}
							rs.Close();

							if(dx < rac)
							{
								switch(LID)
								{
									case 1: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + ", n < " + rac, 8, 320, 40); break;
									case 2: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + ", n < " + rac, 8, 320, 40); break;
								}
						
							}
							else
							{
								g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + dx : ""), 8, 320, 40);
								g.drawMultiBar(8,1,(float)Math.Round(score,2),g.steping,g.barW,div,bar1+bar2+bar3+bar4,100,false,false);
								if(AID1 == 0 && AID2 == 0)
								{
									if(!noSD)
									{
										g.drawMultiStd(20,1,(float)Math.Round(score,2),std,g.steping,g.barW,div,bar1+bar2+bar3+bar4,100,false,false);
									}
								}
							}
							#endregion
						}

						#region Current round
						rs = Db.sqlRecordSet("SELECT " +
							"AVG(av1.ValueInt), COUNT(av1.ValueInt), STDEV(av1.ValueInt) " +
							"FROM ProjectRoundUser u " +
							"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
							"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + (r != 0 ? " AND u.ProjectRoundID = " + r : "") +
							(groupID != 0 ? " AND u.GroupID = " + groupID : ""));
						if(rs.Read())
						{
							dx = 0; score = 0; std = 0;
							if(!rs.IsDBNull(1))
								dx = Convert.ToInt32(rs.GetValue(1));
							if(!rs.IsDBNull(0))
								score = (float)Convert.ToDecimal(rs.GetValue(0));
							if(!rs.IsDBNull(2))
								std = (float)Convert.ToDecimal(rs.GetValue(2));
						}
						rs.Close();

						if(dx < rac)
						{
							switch(LID)
							{
								case 1: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + ", n < " + rac, 6, 320, 20); break;
								case 2: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + ", n < " + rac, 6, 320, 20); break;
							}
						
						}
						else
						{
							g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + dx : ""), 6, 320, 20);
							g.drawMultiBar(6,1,(float)Math.Round(score,2),g.steping,g.barW,div,bar1+bar2+bar3+bar4+bar5,100,false,false);
							if(AID1 == 0 && AID2 == 0)
							{
								if(!noSD)
								{
									g.drawMultiStd(20,1,(float)Math.Round(score,2),std,g.steping,g.barW,div,bar1+bar2+bar3+bar4+bar5,100,false,false);
								}
							}
						}
						#endregion
					}
					#endregion
				}
				else if(t == 0 || t == 13)	// w=440
				{
					#region Likert

					int a1 = 0;
					int a2 = 0;
					bool printedDesc = false;
					bool hasAID = false;
					int totPercent = 0;

					if(AID1 != 0)
					{
						hasAID = true;

						rs = Db.sqlRecordSet("SELECT av.ValueInt FROM AnswerValue av WHERE av.QuestionID = " + q + " AND av.OptionID = " + o + " AND av.AnswerID = " + AID1 + " AND av.DeletedSessionID IS NULL");
						if(rs.Read())
						{
							a1 = rs.GetInt32(0);
						}
						rs.Close();
					}
					if(AID2 != 0)
					{
						rs = Db.sqlRecordSet("SELECT av.ValueInt FROM AnswerValue av WHERE av.QuestionID = " + q + " AND av.OptionID = " + o + " AND av.AnswerID = " + AID2 + " AND av.DeletedSessionID IS NULL");
						if(rs.Read())
						{
							a2 = rs.GetInt32(0);
						}
						rs.Close();
					}

					rs = Db.sqlRecordSet("SELECT COUNT(*) FROM OptionComponents ocs WHERE ocs.OptionID = " + o);
					if(rs.Read())
					{
						cx = Convert.ToInt32(rs.GetValue(0))+2;
					}
					rs.Close();

					decimal tot = 0, tot2 = 0, totalTot = 0, totalTot2 = 0;
					#region counts
					if(units != "" || aids != "")
					{
						rs = Db.sqlRecordSet("SELECT COUNT(DISTINCT a.AnswerID) FROM Answer a " +
							"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
							//"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.DeletedSessionID IS NULL " +
							//(t != 13 ? "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " : "") +
							"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL " +
							(units != "" ? " AND u.ProjectRoundUnitID IN (" + units + ") " : "") +
							(aids != "" ? " AND a.AnswerID IN (" + aids + ") " : "") +
							(groupID != 0 ? " AND u.GroupID = " + groupID : "") +
							"");
						if(rs.Read())
						{
							tot = Convert.ToDecimal(rs.GetInt32(0));
						}
						rs.Close();
						if(rr != 0 && units2 != "")
						{
							rs = Db.sqlRecordSet("SELECT COUNT(DISTINCT a.AnswerID) FROM Answer a " +
								"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								//"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.DeletedSessionID IS NULL " +
								//(t != 13 ? "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " : "") +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND u.ProjectRoundUnitID IN (" + units2 + ")" +
								(groupID != 0 ? " AND u.GroupID = " + groupID : ""));
							if(rs.Read())
							{
								tot2 = Convert.ToDecimal(rs.GetInt32(0));
							}
							rs.Close();
						}
					}
					if(showTotal)
					{
						if(u2c != 0)
						{
							if(rr != 0)
							{
								rs = Db.sqlRecordSet("SELECT COUNT(DISTINCT a.AnswerID) FROM Answer a " +
									"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
									"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									//"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.DeletedSessionID IS NULL " +
									//(t != 13 ? "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " : "") +
									"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL AND u.ProjectRoundID = " + rr + " AND r.UnitCategoryID = " + u2c +
									(groupID != 0 ? " AND u.GroupID = " + groupID : ""));
								if(rs.Read())
								{
									totalTot2 = Convert.ToDecimal(rs.GetInt32(0));
								}
								rs.Close();
							}
							rs = Db.sqlRecordSet("SELECT COUNT(DISTINCT a.AnswerID) FROM Answer a " +
								"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								//"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.DeletedSessionID IS NULL " +
								//(t != 13 ? "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " : "") +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL AND u.ProjectRoundID = " + r + " AND r.UnitCategoryID = " + u2c +
								(groupID != 0 ? " AND u.GroupID = " + groupID : ""));
							if(rs.Read())
							{
								totalTot = Convert.ToDecimal(rs.GetInt32(0));
							}
							rs.Close();
						}
						else
						{
							if(rr != 0)
							{
								rs = Db.sqlRecordSet("SELECT COUNT(DISTINCT a.AnswerID) FROM Answer a " +
									"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
									"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									//"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.DeletedSessionID IS NULL " +
									//(t != 13 ? "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " : "") +
									"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL AND u.ProjectRoundID = " + rr +
									(groupID != 0 ? " AND u.GroupID = " + groupID : ""));
								if(rs.Read())
								{
									totalTot2 = Convert.ToDecimal(rs.GetInt32(0));
								}
								rs.Close();
							}
							rs = Db.sqlRecordSet("SELECT COUNT(DISTINCT a.AnswerID) FROM Answer a " +
								"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								//"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.DeletedSessionID IS NULL " +
								//(t != 13 ? "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " : "") +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + (r != 0 ? " AND u.ProjectRoundID = " + r : "") +
								(groupID != 0 ? " AND u.GroupID = " + groupID : ""));
							if(rs.Read())
							{
								totalTot = Convert.ToDecimal(rs.GetInt32(0));
							}
							rs.Close();
						}
					}
					#endregion
					float max = (percent ? 100f : (float)Convert.ToDouble(Math.Max(Math.Max(Math.Max(tot,tot2),totalTot),totalTot2)));
					max = (float)Math.Round(max/10,0)*10;
					g.setMinMax(0f,max);

					if(cx > 0)
					{
						int Q = cx;
						double[] v1 = new double[Q];
						double[] n1 = new double[Q];
						double[] v2 = new double[Q];
						double[] n2 = new double[Q];

						g.computeSteping(cx);
						g.drawOutlines(11,true,false);
						g.drawAxis(false);
						//g.drawRightAxis();
						g.drawAxisExpl((percent ? "%" : ""), 0,false,false);

						cx = 0;

						int div = (showTotal ? (rr != 0 ? 2 : 1) : 0);	// if comparative round, add 2 instead of 1

						if(units != "" || aids != "")
						{
							#region Current round, increment div and calculate tot
							div++;							
							#endregion

							if(rr != 0 && units2 != "")
							{
								#region Other comparative round, increment div and calculate tot
								div++;
								#endregion
							}

							if(rac > Convert.ToInt32(tot) && (rr == 0 && units2 == "" || rac > Convert.ToInt32(tot2)))
							{
								switch(LID)
								{
									case 1: g.drawColorExplBox(unitDesc.Replace("[x]","") + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
									case 2: g.drawColorExplBox(unitDesc.Replace("[x]","") + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
								}
							
							}
							else
							{
								if(rr != 0 && units2 != "")
								{
									#region Other comparative round, color 7, start at 0
									if(rac > Convert.ToInt32(tot2))
									{
										switch(LID)
										{
											case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120+(hasAID?75:0), 40);break;
											case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120+(hasAID?75:0), 40);break;
										}
									
									}
									else
									{
										g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + tot2 : ""), 7, 120+(hasAID?75:0), 40);

										totPercent = 0;
										rs = Db.sqlRecordSet("SELECT " +
											"oc.OptionComponentID, " +
											"ocl.Text, " +
											"(" +
												"SELECT COUNT(*) FROM Answer a " + 
												"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
												"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
												"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt = oc.OptionComponentID AND av.DeletedSessionID IS NULL " +
												"AND av.OptionID = ocs.OptionID " +
												"AND av.QuestionID = " + q + " " +
												"AND u.ProjectRoundUnitID IN (" + units2 + ")" +
												(groupID != 0 ? " AND u.GroupID = " + groupID : "") +
											"), " +
											"oc.ExportValue " +
											"FROM OptionComponents ocs " +
											"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
											"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + LID + " " +
											"WHERE ocs.OptionID = " + o + " ORDER BY ocs.SortOrder");
										while(rs.Read())
										{
											if(!rs.IsDBNull(3))
											{
												n2[cx] = Convert.ToDouble(rs.GetInt32(2));
												v2[cx] = Convert.ToDouble(rs.GetInt32(3));
											}
											cx++;

											int v = rs.GetInt32(2);
											if(percent)
											{
												v = Convert.ToInt32(Math.Round(Convert.ToDecimal(v)/tot2*100M,0));
												while(t == 0 && (totPercent+v) > 100)
												{
													v--;
												}
												totPercent += v;
											}
											g.drawMultiBar(7,cx,v,g.steping,g.barW,div,0,100,true,percent);
											if(!printedDesc)
											{
												string tt = cut(rs.GetString(1));
												g.drawBottomString(tt,cx,true);
											}
										}
										rs.Close();

										printedDesc = true;

										cx = 0;
									}
									#endregion
								}

								#region Current round, color 5, start at 0 or 1
								if(rac > Convert.ToInt32(tot))
								{
									switch(LID)
									{
										case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
										case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
									}
								
								}
								else
								{
									g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + tot : ""), 5, 120+(hasAID?75:0), 20);

									totPercent = 0;
									rs = Db.sqlRecordSet("SELECT " +
										"oc.OptionComponentID, " +
										"ocl.Text, " +
										"(" +
											"SELECT COUNT(*) FROM Answer a " + 
											"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
											"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
											"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt = oc.OptionComponentID AND av.DeletedSessionID IS NULL " +
											"AND av.OptionID = ocs.OptionID " +
											"AND av.QuestionID = " + q + " " +
											(units != "" ? " AND u.ProjectRoundUnitID IN (" + units + ") " : "") +
											(aids != "" ? " AND a.AnswerID IN (" + aids + ") " : "") +
											(groupID != 0 ? " AND u.GroupID = " + groupID : "") +
											"" +
										"), " +
										"oc.ExportValue " +
										"FROM OptionComponents ocs " +
										"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
										"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + LID + " " +
										"WHERE ocs.OptionID = " + o + " ORDER BY ocs.SortOrder");
									while(rs.Read())
									{
										if(!rs.IsDBNull(3))
										{
											n1[cx] = Convert.ToDouble(rs.GetInt32(2));
											v1[cx] = Convert.ToDouble(rs.GetInt32(3));
										}
										cx++;

										/*OdbcDataReader rs2 = Db.sqlRecordSet("SELECT COUNT(*) FROM Answer a " + 
											"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
											"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
											"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt = " + rs.GetInt32(0) + " AND av.DeletedSessionID IS NULL " +
											"AND av.OptionID = " + o + " " +
											"AND av.QuestionID = " + q + " " +
											"AND u.ProjectRoundUnitID IN (" + HttpContext.Current.Request.QueryString["U"] + ")");
										if(rs2.Read())
										{
											g.drawMultiBar(5,cx,Convert.ToInt32(Math.Round(Convert.ToDecimal(rs2.GetInt32(0))/tot*100M,0)),g.steping,g.barW,div,0,100,true,true);
										}
										rs2.Close();*/

										int v = rs.GetInt32(2);
										if(percent)
										{
											v = Convert.ToInt32(Math.Round(Convert.ToDecimal(v)/tot*100M,0));
											while(t == 0 && (totPercent+v) > 100)
											{
												v--;
											}
											totPercent += v;
										}
										g.drawMultiBar(5,cx,v,g.steping,g.barW,div,(rr != 0 && units2 != "" ? 1 : 0),100,true,percent);
										if(!printedDesc)
										{
											string tt = cut(rs.GetString(1));
											g.drawBottomString(tt,cx,true);
										}
									}
									rs.Close();

									printedDesc = true;
								}
								#endregion

								if(rr != 0 && units2 != "" && rac <= Convert.ToInt32(tot) && rac <= Convert.ToInt32(tot2))
								{
									double tt = 0; int df = 0;
									bool sign = significant(tot,tot2,Q,v1,n1,v2,n2,ref tt,ref df);

									g.drawStringInGraph("Change " + (sign ? "" : "not ") + "significant (t=" + Math.Round(tt,2) + ", p" + (sign ? "<" : ">") + "0.05, df " + df + ")",10,10);
									//g.drawStringInGraph("Change " + (sign ? "" : "not ") + "significant (p" + (sign ? "<" : ">") + "0.05)",80,10);
								}
							}

							cx = 0;
						}

						if(showTotal)
						{
							if(u2c != 0)
							{
								if(rr != 0)
								{
									#region Other comparative round, color 8, start at div-2
									if(rac > Convert.ToInt32(totalTot2))
									{
										switch(LID)
										{
											case 1: g.drawColorExplBox(u2.Replace("[x]"," " + r2) + ", n < " + rac, 8, 320, 40); break;
											case 2: g.drawColorExplBox(u2.Replace("[x]"," " + r2) + ", n < " + rac, 8, 320, 40); break;
										}
								
									}
									else
									{
										g.drawColorExplBox(u2.Replace("[x]"," " + r2) + (showN ? ", n=" + totalTot2 : ""), 8, 320, 40);
									}

									totPercent = 0;
									rs = Db.sqlRecordSet("SELECT " +
										"oc.OptionComponentID, " +
										"ocl.Text, " +
										"(" +
											"SELECT COUNT(*) FROM Answer a " + 
											"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
											"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
											"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
											"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt = oc.OptionComponentID AND av.DeletedSessionID IS NULL " +
											"AND av.OptionID = ocs.OptionID " +
											"AND av.QuestionID = " + q + " " +
											"AND r.Terminated IS NULL AND u.ProjectRoundID = " + rr + " AND r.UnitCategoryID = " + u2c +
											(groupID != 0 ? " AND u.GroupID = " + groupID : "") +
										"), " +
										"oc.ExportValue " +
										"FROM OptionComponents ocs " +
										"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
										"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + LID + " " +
										"WHERE ocs.OptionID = " + o + " ORDER BY ocs.SortOrder");
									while(rs.Read())
									{
										if(!rs.IsDBNull(3))
										{
											n2[cx] = Convert.ToDouble(rs.GetInt32(2));
											v2[cx] = Convert.ToDouble(rs.GetInt32(3));
										}

										cx++;

										if(rac > Convert.ToInt32(totalTot2))
										{
											//
										}
										else
										{
											int v = rs.GetInt32(2);
											if(percent)
											{
												v = Convert.ToInt32(Math.Round(Convert.ToDecimal(v)/totalTot2*100M,0));
												while(t == 0 && (totPercent+v) > 100)
												{
													v--;
												}
												totPercent += v;
											}
											g.drawMultiBar(8,cx,v,g.steping,g.barW,div,1*(div-2),100,true,percent);
										}
										if(!printedDesc)
										{
											string tt = cut(rs.GetString(1));
											g.drawBottomString(tt,cx,true);
										}
									}
									rs.Close();

									printedDesc = true;

									cx = 0;
									#endregion
								}

								#region Current round, color 6, start at div-1
								if(rac > Convert.ToInt32(totalTot))
								{
									switch(LID)
									{
										case 1: g.drawColorExplBox(u2.Replace("[x]"," " + r1) + ", n < " + rac, 6, 320, 20); break;
										case 2: g.drawColorExplBox(u2.Replace("[x]"," " + r1) + ", n < " + rac, 6, 320, 20); break;
									}
								
								}
								else
								{
									g.drawColorExplBox(u2.Replace("[x]"," " + r1) + (showN ? ", n=" + totalTot : ""), 6, 320, 20);
								}

								totPercent = 0;
								rs = Db.sqlRecordSet("SELECT " +
									"oc.OptionComponentID, " +
									"ocl.Text, " +
									"(" +
										"SELECT COUNT(*) FROM Answer a " + 
										"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
										"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
										"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
										"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt = oc.OptionComponentID AND av.DeletedSessionID IS NULL " +
										"AND av.OptionID = ocs.OptionID " +
										"AND av.QuestionID = " + q + " " +
										"AND r.Terminated IS NULL AND u.ProjectRoundID = " + r + " AND r.UnitCategoryID = " + u2c +
										(groupID != 0 ? " AND u.GroupID = " + groupID : "") +
									"), " +
									"oc.ExportValue " +
									"FROM OptionComponents ocs " +
									"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
									"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + LID + " " +
									"WHERE ocs.OptionID = " + o + " ORDER BY ocs.SortOrder");
								while(rs.Read())
								{
									if(!rs.IsDBNull(3))
									{
										n1[cx] = Convert.ToDouble(rs.GetInt32(2));
										v1[cx] = Convert.ToDouble(rs.GetInt32(3));
									}

									cx++;

									/*OdbcDataReader rs2 = Db.sqlRecordSet("SELECT COUNT(*) FROM Answer a " + 
										"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
										"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
										"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
										"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt = " + rs.GetInt32(0) + " AND av.DeletedSessionID IS NULL " +
										"AND av.OptionID = " + o + " " +
										"AND av.QuestionID = " + q + " " +
										"AND r.Terminated IS NULL AND u.ProjectRoundID = " + r + " AND r.UnitCategoryID = " + HttpContext.Current.Request.QueryString["U2C"]);
									if(rs2.Read())
									{
										g.drawMultiBar(6,cx,Convert.ToInt32(Math.Round(Convert.ToDecimal(rs2.GetInt32(0))/totalTot*100M,0)),g.steping,g.barW,div,1*(div-1),100,true,true);
									}
									rs2.Close();*/
									if(rac > Convert.ToInt32(totalTot))
									{
										//
									}
									else
									{
										int v = rs.GetInt32(2);
										if(percent)
										{
											v = Convert.ToInt32(Math.Round(Convert.ToDecimal(v)/totalTot*100M,0));
											while(t == 0 && (totPercent+v) > 100)
											{
												v--;
											}
											totPercent += v;
										}
										g.drawMultiBar(6,cx,v,g.steping,g.barW,div,1*(div-1),100,true,percent);
									}
									if(!printedDesc)
									{
										string tt = cut(rs.GetString(1));
										g.drawBottomString(tt,cx,true);
									}

									if(a1 == rs.GetInt32(0))
									{
										g.drawColorExplBox(AID1txt, 20, 70, 20);
										g.drawDotUnder(cx,false,(a1 == a2 ? -10 : 0),18);
									}
									if(a2 == rs.GetInt32(0))
									{
										g.drawColorExplBox(AID2txt, 19, 70, 40);
										g.drawDotUnder(cx,true,(a1 == a2 ? 10 : 0),18);
									}
								}
								rs.Close();

								printedDesc = true;
								#endregion
							}
							else
							{
								if(rr != 0)
								{
									#region Other comparative round, color 8, start at div-2
									if(rac > Convert.ToInt32(totalTot2))
									{
										switch(LID)
										{
											case 1: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + ", n < " + rac, 8, 320, 40); break;
											case 2: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + ", n < " + rac, 8, 320, 40); break;
										}
								
									}
									else
									{
										g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + totalTot2 : ""), 8, 320, 40);
									}

									totPercent = 0;
									rs = Db.sqlRecordSet("SELECT " +
										"oc.OptionComponentID, " +
										"ocl.Text, " +
										"(" +
											"SELECT COUNT(*) FROM Answer a " + 
											"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
											"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
											"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
											"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt = oc.OptionComponentID AND av.DeletedSessionID IS NULL " +
											"AND av.OptionID = ocs.OptionID " +
											"AND av.QuestionID = " + q + " " +
											"AND r.Terminated IS NULL AND u.ProjectRoundID = " + rr +
											(groupID != 0 ? " AND u.GroupID = " + groupID : "") +
										"), " +
										"oc.ExportValue " +
										"FROM OptionComponents ocs " +
										"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
										"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + LID + " " +
										"WHERE ocs.OptionID = " + o + " ORDER BY ocs.SortOrder");
									while(rs.Read())
									{
										if(!rs.IsDBNull(3))
										{
											n2[cx] = Convert.ToDouble(rs.GetInt32(2));
											v2[cx] = Convert.ToDouble(rs.GetInt32(3));
										}

										cx++;

										if(rac > Convert.ToInt32(totalTot2))
										{
											//
										}
										else
										{
											int v = rs.GetInt32(2);
											if(percent)
											{
												v = Convert.ToInt32(Math.Round(Convert.ToDecimal(v)/totalTot2*100M,0));
												while(t == 0 && (totPercent+v) > 100)
												{
													v--;
												}
												totPercent += v;
											}
											g.drawMultiBar(8,cx,v,g.steping,g.barW,div,1*(div-2),100,true,percent);
										}
										if(!printedDesc)
										{
											string tt = cut(rs.GetString(1));
											g.drawBottomString(tt,cx,true);
										}
									}
									rs.Close();

									printedDesc = true;

									cx = 0;
									#endregion
								}

								#region Current round, color 6, start at div-1
								if(rac > Convert.ToInt32(totalTot))
								{
									switch(LID)
									{
										case 1: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + ", n < " + rac, 6, 320, 20); break;
										case 2: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + ", n < " + rac, 6, 320, 20); break;
									}
								
								}
								else
								{
									g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + totalTot : ""), 6, 320, 20);
								}

								totPercent = 0;
								rs = Db.sqlRecordSet("SELECT " +
									"oc.OptionComponentID, " +
									"ocl.Text, " +
									"(" +
										"SELECT COUNT(*) FROM Answer a " + 
										"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
										"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
										"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
										"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt = oc.OptionComponentID AND av.DeletedSessionID IS NULL " +
										"AND av.OptionID = ocs.OptionID " +
										"AND av.QuestionID = " + q + " " +
										"AND r.Terminated IS NULL" + (r != 0 ? " AND u.ProjectRoundID = " + r : "") +
										(groupID != 0 ? " AND u.GroupID = " + groupID : "") +
									"), " +
									"oc.ExportValue " +
									"FROM OptionComponents ocs " +
									"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
									"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + LID + " " +
									"WHERE ocs.OptionID = " + o + " ORDER BY ocs.SortOrder");
								while(rs.Read())
								{
									if(!rs.IsDBNull(3))
									{
										n1[cx] = Convert.ToDouble(rs.GetInt32(2));
										v1[cx] = Convert.ToDouble(rs.GetInt32(3));
									}

									cx++;

									/*OdbcDataReader rs2 = Db.sqlRecordSet("SELECT COUNT(*) FROM Answer a " + 
										"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
										"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
										"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
										"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt = " + rs.GetInt32(0) + " AND av.DeletedSessionID IS NULL " +
										"AND av.OptionID = " + o + " " +
										"AND av.QuestionID = " + q + " " +
										"AND r.Terminated IS NULL AND u.ProjectRoundID = " + r);
									if(rs2.Read())
									{
										g.drawMultiBar(6,cx,Convert.ToInt32(Math.Round(Convert.ToDecimal(rs2.GetInt32(0))/totalTot*100M,0)),g.steping,g.barW,div,1*(div-1),100,true,true);
									}
									rs2.Close();*/
									if(rac > Convert.ToInt32(totalTot))
									{
										//
									}
									else
									{
										int v = rs.GetInt32(2);
										if(percent)
										{
											v = Convert.ToInt32(Math.Round(Convert.ToDecimal(v)/totalTot*100M,0));
											while(t == 0 && (totPercent+v) > 100)
											{
												v--;
											}
											totPercent += v;
										}
										g.drawMultiBar(6,cx,v,g.steping,g.barW,div,1*(div-1),100,true,percent);
									}
									if(!printedDesc)
									{
										string tt = cut(rs.GetString(1));
										g.drawBottomString(tt,cx,true);
									}

									if(a1 == rs.GetInt32(0))
									{
										g.drawColorExplBox(HttpContext.Current.Server.HtmlDecode(AID1txt), 20, 70, 20);
										g.drawDotUnder(cx,false,(a1 == a2 ? -10 : 0),18);
									}
									if(a2 == rs.GetInt32(0))
									{
										g.drawColorExplBox(HttpContext.Current.Server.HtmlDecode(AID2txt), 19, 70, 40);
										g.drawDotUnder(cx,true,(a1 == a2 ? 10 : 0),18);
									}
								}
								rs.Close();

								printedDesc = true;
								#endregion
							}
							if(rr != 0 && rac <= Convert.ToInt32(totalTot) && rac <= Convert.ToInt32(totalTot2))
							{
								double tt = 0; int df = 0;
								bool sign = significant(totalTot,totalTot2,Q,v1,n1,v2,n2,ref tt,ref df);

								g.drawStringInGraph("Change " + (sign ? "" : "not ") + "significant (t=" + Math.Round(tt,2) + ", p" + (sign ? "<" : ">") + "0.05, df " + df + ")",260,10);
								//g.drawStringInGraph("Change " + (sign ? "" : "not ") + "significant (p" + (sign ? "<" : ">") + "0.05)",280,10);
							}
						}
					}
					#endregion
				}
				else if(t == 100)			// w=320
				{
					#region Likert scale -> VAS
					g = new Graph(550,320,color);
					g.leftSpacing = 150;
					g.setMinMax(0f,100f);
					g.computeSteping(3);
					int steps = 5;
					rs = Db.sqlRecordSet("SELECT COUNT(*) FROM OptionComponents WHERE OptionID = " + o + " AND NoOrderValue IS NULL");
					if(rs.Read())
					{
						steps = rs.GetInt32(0);
					}
					rs.Close();
					g.drawOutlines(steps,false,false);
					g.drawAxis(false);
					//g.drawRightAxis();

					bool hasAID = false;

					int dx = 0;
					float score = 0; float std = 0;
					int bar1 = 0, bar2 = 0, bar3 = 0, bar4 = 0, bar5 = 0;

					int tmp = 0;
					rs = Db.sqlRecordSet("SELECT oc.OptionComponentID, ocl.Text, ocs.ExportValue, oc.ExportValue FROM OptionComponents ocs " +
						"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
						"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + LID + " " +
						"WHERE ocs.OptionID = " + o + " AND ocs.NoOrderValue IS NULL ORDER BY ocs.SortOrder");
					while(rs.Read())
					{
						if(!extremeValuesOnly || tmp == 0 || tmp + 1 == steps)
						{
							string tt = cut(rs.GetString(1));
							g.drawAxisVal(tt + (exportValues && (!rs.IsDBNull(2) || !rs.IsDBNull(3)) ? " - " + (rs.IsDBNull(2) ? rs.GetInt32(3) : rs.GetInt32(2)) : ""),steps,dx);
						}
						dx ++;
						tmp ++;
					}
					rs.Close();

					int a1 = -99;
					int a2 = -99;
					int div = 0;
					
					if(showTotal)
					{
						div++;
						if(rr != 0)
						{
							div++;
							bar5 = 1;
						}
					}

					if(AID1 != 0)
					{
						hasAID = true;

						rs = Db.sqlRecordSet("SELECT ocs.OrderValue FROM AnswerValue av INNER JOIN OptionComponents ocs ON av.ValueInt = ocs.OptionComponentID AND ocs.NoOrderValue IS NULL AND av.OptionID = ocs.OptionID WHERE av.QuestionID = " + q + " AND av.OptionID = " + o + " AND av.AnswerID = " + AID1 + " AND av.DeletedSessionID IS NULL");
						if(rs.Read())
						{
							a1 = rs.GetInt32(0);
							div++;
							bar2 = 1;
						}
						rs.Close();
					}
					if(AID2 != 0)
					{
						rs = Db.sqlRecordSet("SELECT ocs.OrderValue FROM AnswerValue av INNER JOIN OptionComponents ocs ON av.ValueInt = ocs.OptionComponentID AND ocs.NoOrderValue IS NULL AND av.OptionID = ocs.OptionID WHERE av.QuestionID = " + q + " AND av.OptionID = " + o + " AND av.AnswerID = " + AID2 + " AND av.DeletedSessionID IS NULL");
						if(rs.Read())
						{
							a2 = rs.GetInt32(0);
							div++;
							bar1 = 1;
						}
						rs.Close();
					}

					dx = 0;
					if(units != "" || aids != "")
					{
						string sql = "";

						div++;
						bar4 = 1;

						if(rr != 0 && units2 != "")
						{
							div++;
							bar3 = 1;

							#region Comparative round
							sql = "SELECT " +
								"AVG(ocs.OrderValue), COUNT(ocs.OrderValue), STDEV(ocs.OrderValue) " +
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
								"INNER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.NoOrderValue IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND u.ProjectRoundUnitID IN (" + units2 + ")" +
								(groupID != 0 ? " AND u.GroupID = " + groupID : "");
							rs = Db.sqlRecordSet(sql);
							if(rs.Read())
							{
								dx = 0; score = 0; std = 0;
								if(!rs.IsDBNull(1))
									dx = Convert.ToInt32(rs.GetValue(1));
								if(!rs.IsDBNull(0))
									score = (float)Convert.ToDecimal(rs.GetValue(0));
								if(!rs.IsDBNull(2))
									std = (float)Convert.ToDecimal(rs.GetValue(2));
							}
							rs.Close();

							if(dx < rac)
							{
								switch(LID)
								{
									case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120+(hasAID?75:0), 40);break;
									case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120+(hasAID?75:0), 40);break;
								}
							
							}
							else
							{
								g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + dx : ""), 7, 120+(hasAID?75:0), 40);
								g.drawMultiBar(7,1,(float)Math.Round(score,2),g.steping,g.barW,div,bar1+bar2,100,false,false);
								if(AID1 == 0 && AID2 == 0)
								{
									if(!noSD)
									{
										g.drawMultiStd(20,1,(float)Math.Round(score,2),std,g.steping,g.barW,div,bar1+bar2,100,false,false);
									}
								}
							}
							#endregion

							dx = 0;
							score = 0; std = 0;
						}

						#region Current round
						sql = "SELECT " +
							"AVG(ocs.OrderValue), COUNT(ocs.OrderValue), STDEV(ocs.OrderValue) " +
							"FROM ProjectRoundUser u " +
							"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
							"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.NoOrderValue IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL " +
							(units != "" ? " AND u.ProjectRoundUnitID IN (" + units + ") " : "") +
							(aids != "" ? " AND a.AnswerID IN (" + aids + ") " : "") +
							(groupID != 0 ? " AND u.GroupID = " + groupID : "") +
							"";
						rs = Db.sqlRecordSet(sql);
						if(rs.Read())
						{
							dx = 0; score = 0; std = 0;
							if(!rs.IsDBNull(1))
								dx = Convert.ToInt32(rs.GetValue(1));
							if(!rs.IsDBNull(0))
								score = (float)Convert.ToDecimal(rs.GetValue(0));
							if(!rs.IsDBNull(2))
								std = (float)Convert.ToDecimal(rs.GetValue(2));
						}
						rs.Close();

						if(dx < rac)
						{
							switch(LID)
							{
								case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
								case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
							}
						
						}
						else
						{
							g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + dx : ""), 5, 120+(hasAID?75:0), 20);
							g.drawMultiBar(5,1,(float)Math.Round(score,2),g.steping,g.barW,div,bar1+bar2+bar3,100,false,false);
							if(AID1 == 0 && AID2 == 0)
							{
								if(!noSD)
								{
									g.drawMultiStd(20,1,(float)Math.Round(score,2),std,g.steping,g.barW,div,bar1+bar2+bar3,100,false,false);
								}
							}
						}
						#endregion

						dx = 0;
						score = 0; std = 0;
					}

					if(a1 != -99)
					{
						g.drawColorExplBox(AID1txt, 20, 70, 20);
						g.drawMultiBar(20,1,(float)a1,g.steping,g.barW,div,bar1,100,false,false);
					}
					if(a2 != -99)
					{
						g.drawColorExplBox(AID2txt, 19, 70, 40);
						g.drawMultiBar(19,1,(float)a2,g.steping,g.barW,div,0,100,false,false);
					}

					if(showTotal)
					{
						if(rr != 0)
						{
							#region Comparative round
							rs = Db.sqlRecordSet("SELECT " +
								"AVG(ocs.OrderValue), COUNT(ocs.OrderValue), STDEV(ocs.OrderValue) " +
								"FROM ProjectRoundUser u " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
								"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
								"INNER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.NoOrderValue IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL AND u.ProjectRoundID = " + rr +
								(groupID != 0 ? " AND u.GroupID = " + groupID : ""));
							if(rs.Read())
							{
								dx = 0; score = 0; std = 0;
								if(!rs.IsDBNull(1))
									dx = Convert.ToInt32(rs.GetValue(1));
								if(!rs.IsDBNull(0))
									score = (float)Convert.ToDecimal(rs.GetValue(0));
								if(!rs.IsDBNull(2))
									std = (float)Convert.ToDecimal(rs.GetValue(2));
							}
							rs.Close();

							if(dx < rac)
							{
								switch(LID)
								{
									case 1: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + ", n < " + rac, 8, 320, 40); break;
									case 2: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + ", n < " + rac, 8, 320, 40); break;
								}
						
							}
							else
							{
								g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + dx : ""), 8, 320, 40);
								g.drawMultiBar(8,1,(float)Math.Round(score,2),g.steping,g.barW,div,bar1+bar2+bar3+bar4,100,false,false);
								if(AID1 == 0 && AID2 == 0)
								{
									if(!noSD)
									{
										g.drawMultiStd(20,1,(float)Math.Round(score,2),std,g.steping,g.barW,div,bar1+bar2+bar3+bar4,100,false,false);
									}
								}
							}
							#endregion
						}

						#region Current round
						rs = Db.sqlRecordSet("SELECT " +
							"AVG(ocs.OrderValue), COUNT(ocs.OrderValue), STDEV(ocs.OrderValue) " +
							"FROM ProjectRoundUser u " +
							"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
							"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.NoOrderValue IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + (r != 0 ? " AND u.ProjectRoundID = " + r : "") +
							(groupID != 0 ? " AND u.GroupID = " + groupID : ""));
						if(rs.Read())
						{
							dx = 0; score = 0; std = 0;
							if(!rs.IsDBNull(1))
								dx = Convert.ToInt32(rs.GetValue(1));
							if(!rs.IsDBNull(0))
								score = (float)Convert.ToDecimal(rs.GetValue(0));
							if(!rs.IsDBNull(2))
								std = (float)Convert.ToDecimal(rs.GetValue(2));
						}
						rs.Close();

						if(dx < rac)
						{
							switch(LID)
							{
								case 1: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + ", n < " + rac, 6, 320, 20); break;
								case 2: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + ", n < " + rac, 6, 320, 20); break;
							}						
						}
						else
						{
							g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + dx : ""), 6, 320, 20);
							g.drawMultiBar(6,1,(float)Math.Round(score,2),g.steping,g.barW,div,bar1+bar2+bar3+bar4+bar5,100,false,false);
							if(AID1 == 0 && AID2 == 0)
							{
								if(!noSD)
								{
									g.drawMultiStd(20,1,(float)Math.Round(score,2),std,g.steping,g.barW,div,bar1+bar2+bar3+bar4+bar5,100,false,false);
								}
							}
						}
						#endregion
					}
					#endregion
				}
				else if(t == 200)			// w=320, JPE 20141215 stopped implenting GroupID before this one...
				{
					string avgSql = "", sql = "";

					#region Likert scale -> VAS, comp with children
					SortedList sl = new SortedList();
					System.Data.SqlClient.SqlDataReader rs2 = Db.sqlRecordSet("SELECT pru.Unit, pru.SortString FROM ProjectRoundUnit pru WHERE pru.ProjectRoundID = " + r + " AND (" + 
						(units != "" ? "pru.ProjectRoundUnitID IN (" + units + ") OR pru.ParentProjectRoundUnitID IN (" + units + ")" : "pru.ParentProjectRoundUnitID IS NULL") +
						") ORDER BY pru.Unit");
					while(rs2.Read())
					{
						string tempSql = " LEFT(r.SortString," + rs2.GetString(1).Length + ") = '" + rs2.GetString(1) + "'";
						avgSql += " OR" + tempSql;
						sql = "SELECT " +
							"AVG(ocs.OrderValue) " +
							"FROM ProjectRoundUnit r " +
							"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.NoOrderValue IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND" + tempSql;
//						HttpContext.Current.Response.Write(sql);
						rs = Db.sqlRecordSet(sql);
						if(rs.Read())
						{
							float d = (rs.IsDBNull(0) ? -1f : (float)Convert.ToDouble(rs.GetValue(0)));
							while(sl.Contains(d)) d += 0.01f;
							sl.Add(d,rs2.GetString(0));
						}
						rs.Close();
					}
					rs2.Close();
//					HttpContext.Current.Response.End();

					g = new Graph(550,520,color);
					g.leftSpacing = 150;
					g.setMinMax(0f,100f);
					g.computeSteping(sl.Count+2);
					
					int steps = 5;
					rs = Db.sqlRecordSet("SELECT COUNT(*) FROM OptionComponents WHERE OptionID = " + o + " AND NoOrderValue IS NULL");
					if(rs.Read())
					{
						steps = rs.GetInt32(0);
					}
					rs.Close();
					g.drawOutlines(steps,false,false);
					g.drawAxis(false);

					int dx = 0, tmp = 0;
					rs = Db.sqlRecordSet("SELECT oc.OptionComponentID, ocl.Text, ocs.ExportValue, oc.ExportValue FROM OptionComponents ocs " +
						"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
						"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + LID + " " +
						"WHERE ocs.OptionID = " + o + " AND ocs.NoOrderValue IS NULL ORDER BY ocs.OrderValue");
					while(rs.Read())
					{
						if(!extremeValuesOnly || tmp == 0 || tmp + 1 == steps)
						{
							string tt = cut(rs.GetString(1));
							g.drawAxisVal(tt + (exportValues && (!rs.IsDBNull(2) || !rs.IsDBNull(3)) ? " - " + (rs.IsDBNull(2) ? rs.GetInt32(3) : rs.GetInt32(2)) : ""),steps,dx);
						}
						dx ++;
						tmp ++;
					}
					rs.Close();

					for(int i=0; i<sl.Count; i++)
					{
						int pos = sl.Count - i;

						float v = (float)sl.GetKey(i);
						if(v >= 0)
						{
							g.drawBar(1,pos,v);
						}
						g.drawBottomString((string)sl.GetByIndex(i),pos,true,false,false);
					}

					sql = "SELECT " +
						"AVG(ocs.OrderValue) " +
						"FROM ProjectRoundUnit r " +
						"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
						"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
						"INNER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.NoOrderValue IS NULL " +
						"WHERE a.EndDT IS NOT NULL AND (1=0" + avgSql + ")";
					rs = Db.sqlRecordSet(sql);
					if(rs.Read())
					{
						float d = (rs.IsDBNull(0) ? -1f : (float)Convert.ToDouble(rs.GetValue(0)));
						if(d != -1f)
							g.drawReferenceLine(Convert.ToInt32(d));
					}
					rs.Close();

					#endregion
				}
				else if(t == 400)			// w=320
				{
					string sql = "";

					#region Likert scale -> VAS, comp with children
					SortedList sl = new SortedList();

					#region Comps
					System.Data.SqlClient.SqlDataReader rs2;
					if(r == -1)
					{
						rs2 = Db.sqlRecordSet("SELECT Internal, ProjectRoundID FROM ProjectRound WHERE ProjectRoundID IN (" + rnds + ") ORDER BY Internal");
					}
					else
					{
						rs2 = Db.sqlRecordSet("SELECT " +
							"pru.Unit, " +
							"pru.SortString " +
							"FROM ProjectRoundUnit pru " +
							"WHERE pru.ProjectRoundID = " + r + " " +
							"AND pru.ProjectRoundUnitID IN (0" + units2 + ")");
					}
					while(rs2.Read())
					{
						string tempSql = "";
						if(r == -1)
						{
							if(HttpContext.Current.Request.QueryString["SS" + rs2.GetInt32(1)] != null)
							{
								tempSql = " LEFT(r.SortString," + HttpContext.Current.Request.QueryString["SS" + rs2.GetInt32(1)].ToString().Replace("'","").Length + ") = '" + HttpContext.Current.Request.QueryString["SS" + rs2.GetInt32(1)].ToString().Replace("'","") + "'";
							}
							else
							{
								tempSql = " r.ProjectRoundID = " + rs2.GetInt32(1);
							}
						}
						else
						{
							tempSql = " LEFT(r.SortString," + rs2.GetString(1).Length + ") = '" + rs2.GetString(1) + "'";
						}
						sql = "SELECT " +
							"AVG(ocs.OrderValue), " +
							"COUNT(ocs.OrderValue) " +
							"FROM ProjectRoundUnit r " +
							"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.NoOrderValue IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND" + tempSql;
						rs = Db.sqlRecordSet(sql);
						if(rs.Read())
						{
							if(rs.GetInt32(1) >= rac)
							{
								string s = (r == -1 && HttpContext.Current.Request.QueryString["U" + rs2.GetInt32(1)] != null ? HttpContext.Current.Request.QueryString["U" + rs2.GetInt32(1)] : rs2.GetString(0));
								while(sl.Contains(s)) s += " ";
								sl.Add(s,(rs.IsDBNull(0) ? -1f : (float)Convert.ToDouble(rs.GetValue(0))));
							}
						}
						rs.Close();
					}
					rs2.Close();
					#endregion

					bool noTotal = (HttpContext.Current.Request.QueryString["NoTotal"] != null);
					g = new Graph(550,520,color);
					g.leftSpacing = 150;
					g.setMinMax(0f,100f);
					g.computeSteping(sl.Count+2+(noTotal?0:1));
					
					int steps = 5;
					rs = Db.sqlRecordSet("SELECT COUNT(*) FROM OptionComponents WHERE OptionID = " + o + " AND NoOrderValue IS NULL");
					if(rs.Read())
					{
						steps = rs.GetInt32(0);
					}
					rs.Close();
					g.drawOutlines(steps,false,false);
					g.drawAxis(false);

					int dx = 0, tmp = 0;
					rs = Db.sqlRecordSet("SELECT oc.OptionComponentID, ocl.Text, ocs.ExportValue, oc.ExportValue FROM OptionComponents ocs " +
						"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
						"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + LID + " " +
						"WHERE ocs.OptionID = " + o + " AND ocs.NoOrderValue IS NULL ORDER BY ocs.OrderValue");
					while(rs.Read())
					{
						if(!extremeValuesOnly || tmp == 0 || tmp + 1 == steps)
						{
							string tt = cut(rs.GetString(1));
							g.drawAxisVal(tt + (exportValues && (!rs.IsDBNull(2) || !rs.IsDBNull(3)) ? " - " + (rs.IsDBNull(2) ? rs.GetInt32(3) : rs.GetInt32(2)) : ""),steps,dx);
						}
						dx ++;
						tmp ++;
					}
					rs.Close();

					int sx = 0;
					if(!noTotal)
					{
						#region Total and groups
						if(r == -1)
						{
							rs2 = Db.sqlRecordSet("SELECT '" + (unitDesc != "" ? unitDesc : projectRoundDesc).Replace("'","") + "'");
						}
						else
						{
							rs2 = Db.sqlRecordSet("SELECT " +
								"pru.Unit, " +
								"pru.SortString " +
								"FROM ProjectRoundUnit pru " +
								"WHERE pru.ProjectRoundID = " + r + " " +
								"AND pru.ProjectRoundUnitID IN (" + units + ")");
						}
						while(rs2.Read())
						{
							sx++;

							string tempSql = "";
							int x = 0;

							if(r == -1)
							{
								tempSql = " r.ProjectRoundID IN (" + rnds + ")";
								x = Db.getInt32("SELECT COUNT(DISTINCT pru.GroupID) FROM ProjectRoundUser pru INNER JOIN ProjectRoundUnit r ON pru.ProjectRoundUnitID = r.ProjectRoundUnitID WHERE" + tempSql);
							}
							else
							{
								tempSql = " LEFT(r.SortString," + rs2.GetString(1).Length + ") = '" + rs2.GetString(1) + "'";
								x = Db.getInt32("SELECT COUNT(DISTINCT pru.GroupID) FROM ProjectRoundUser pru INNER JOIN ProjectRoundUnit r ON pru.ProjectRoundUnitID = r.ProjectRoundUnitID WHERE" + tempSql);
							}

							sql = "SELECT " +
								"AVG(ocs.OrderValue), " +
								"COUNT(ocs.OrderValue) " +
								"FROM ProjectRoundUnit r " +
								"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
								"INNER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.NoOrderValue IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND" + tempSql;
							rs = Db.sqlRecordSet(sql);
							if(rs.Read())
							{
								if(rs.GetInt32(1) >= rac)
								{
									if(x > 0)
									{
										g.drawMultiBar(1,sx,(rs.IsDBNull(0) ? -1f : (float)Convert.ToDouble(rs.GetValue(0))),x+1,0,"");
									}
									else
									{
										g.drawBar(1,sx,(rs.IsDBNull(0) ? -1f : (float)Convert.ToDouble(rs.GetValue(0))));
									}
								}
							}
							rs.Close();
							if(x > 0)
							{
								g.drawColorExplBox("Total",1,175,20);

								int sxx = 0;
								sql = "SELECT " +
									"AVG(ocs.OrderValue), " +
									"g.GroupDesc, " +
									"COUNT(ocs.OrderValue) " +
									"FROM ProjectRoundUnit r " +
									"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									"INNER JOIN ProjectRoundUser pru ON a.ProjectRoundUserID = pru.ProjectRoundUserID " +
									"INNER JOIN [Group] g ON pru.GroupID = g.GroupID " +
									"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
									"INNER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.NoOrderValue IS NULL " +
									"WHERE a.EndDT IS NOT NULL AND" + tempSql + " GROUP BY g.GroupDesc ORDER BY COUNT(*) DESC";
								rs = Db.sqlRecordSet(sql);
								while(rs.Read())
								{
									if(rs.GetInt32(2) >= rac)
									{
										sxx++;
										g.drawMultiBar(3+sxx,sx,(rs.IsDBNull(0) ? -1f : (float)Convert.ToDouble(rs.GetValue(0))),x+1,sxx,"");
										if(sx == 1)
										{
											g.drawColorExplBox(rs.GetString(1),3+sxx,175+sxx*80,20);
										}
									}
								}
								rs.Close();
							}
							g.drawBottomString(rs2.GetString(0),sx,true,false,false);
						}
						rs2.Close();
						#endregion
					}

					for(int i=0; i<sl.Count; i++)
					{
						int pos = i + 1 + sx;

						float v = (float)sl.GetByIndex(i);
						if(v >= 0)
						{
							g.drawBar(1,pos,v);
						}
						g.drawBottomString((string)sl.GetKey(i),pos,true,false,false);
					}

					#region Database
					if(r == -1)
					{
						sql = "SELECT " +
							"AVG(ocs.OrderValue), " +
							"COUNT(ocs.OrderValue) " +
							"FROM ProjectRoundUnit r " +
							"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.NoOrderValue IS NULL ";
					}
					else
					{
						sql = "SELECT " +
							"AVG(ocs.OrderValue), " +
							"COUNT(ocs.OrderValue) " +
							"FROM ProjectRoundUnit r " +
							"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"INNER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.NoOrderValue IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND a.ProjectRoundID IN (" + rnds + ")";
					}
					rs = Db.sqlRecordSet(sql);
					if(rs.Read())
					{
						if(rs.GetInt32(1) >= rac)
						{
							float d = (rs.IsDBNull(0) ? -1f : (float)Convert.ToDouble(rs.GetValue(0)));
							if(d != -1f)
							{
								g.drawReferenceLine(Convert.ToInt32(d), (r == -1 ? "Database" : r1));
							}
						}
					}
					rs.Close();
					#endregion

					#endregion
				}
				else if(t == 300)			// w=320
				{
					string avgSql = "", sql = "";

					#region Likert top option, comp with children
					SortedList sl = new SortedList();
					System.Data.SqlClient.SqlDataReader rs2 = Db.sqlRecordSet("SELECT pru.Unit, pru.SortString FROM ProjectRoundUnit pru WHERE pru.ProjectRoundID = " + r + " AND (" + 
						(units != "" ? "pru.ProjectRoundUnitID IN (" + units + ") OR pru.ParentProjectRoundUnitID IN (" + units + ")" : "pru.ParentProjectRoundUnitID IS NULL") +
						") ORDER BY pru.Unit");
					while(rs2.Read())
					{
						string tempSql = " LEFT(r.SortString," + rs2.GetString(1).Length + ") = '" + rs2.GetString(1) + "'";
						avgSql += " OR" + tempSql;
						sql = "SELECT " +
							"COUNT(av1.AnswerID), " +
							"COUNT(ocs.OrderValue) " +
							"FROM ProjectRoundUnit r " +
							"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"LEFT OUTER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.OrderValue = 100 " +
							"WHERE a.EndDT IS NOT NULL AND" + tempSql;
						//						HttpContext.Current.Response.Write(sql);
						rs = Db.sqlRecordSet(sql);
						if(rs.Read())
						{
							float d = (rs.IsDBNull(0) || rs.IsDBNull(1) || (float)Convert.ToDouble(rs.GetValue(0)) == 0f ? -1f : (float)Convert.ToDouble(rs.GetValue(1))/(float)Convert.ToDouble(rs.GetValue(0))*100f);
							while(sl.Contains(d)) d += 0.01f;
							sl.Add(d,rs2.GetString(0));
						}
						rs.Close();
					}
					rs2.Close();
					//					HttpContext.Current.Response.End();

					g = new Graph(550,520,color);
					//g.leftSpacing = 150;
					g.setMinMax(0f,100f);
					g.computeSteping(sl.Count+2);
					
					g.drawOutlines(11,true,false);
					g.drawAxis(false);
					rs = Db.sqlRecordSet("SELECT ocl.Text FROM OptionComponentLang ocl INNER JOIN OptionComponents ocs ON ocl.OptionComponentID = ocs.OptionComponentID WHERE ocs.OptionID = " + o + " AND ocl.LangID = " + LID + " AND ocs.OrderValue = 100");
					if(rs.Read())
					{
						g.drawAxisExpl(rs.GetString(0) + (percent ? ", %" : ""), 0,false,false);
					}
					rs.Close();

					for(int i=0; i<sl.Count; i++)
					{
						int pos = sl.Count - i;

						float v = (float)sl.GetKey(i);
						if(v >= 0)
						{
							g.drawBar(1,pos,v);
						}
						g.drawBottomString((string)sl.GetByIndex(i),pos,true,false,false);
					}

					sql = "SELECT " +
						"COUNT(av1.AnswerID), " +
						"COUNT(ocs.OrderValue) " +
						"FROM ProjectRoundUnit r " +
						"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
						"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
						"LEFT OUTER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.OrderValue = 100 " +
						"WHERE a.EndDT IS NOT NULL AND (1=0" + avgSql + ")";
					rs = Db.sqlRecordSet(sql);
					if(rs.Read())
					{
						float d = (rs.IsDBNull(0) || rs.IsDBNull(1) || (float)Convert.ToDouble(rs.GetValue(0)) == 0f ? -1f : (float)Convert.ToDouble(rs.GetValue(1))/(float)Convert.ToDouble(rs.GetValue(0))*100f);
						if(d != -1f)
							g.drawReferenceLine(Convert.ToInt32(d));
					}
					rs.Close();

					#endregion
				}
				else if(t == 500)			// w=320
				{
					string sql = "";

					#region Likert top option, comp with children
					SortedList sl = new SortedList();
					
					// Comps
					System.Data.SqlClient.SqlDataReader rs2;
					if(r == -1)
					{
						rs2 = Db.sqlRecordSet("SELECT Internal, ProjectRoundID FROM ProjectRound WHERE ProjectRoundID IN (" + rnds + ") ORDER BY Internal");
					}
					else
					{
						rs2 = Db.sqlRecordSet("SELECT " +
							"pru.Unit, " +
							"pru.SortString " +
							"FROM ProjectRoundUnit pru " +
							"WHERE pru.ProjectRoundID = " + r + " " +
							"AND pru.ProjectRoundUnitID IN (0" + units2 + ")");
					}
					while(rs2.Read())
					{
						string tempSql = "";
						if(r == -1)
						{
							if(HttpContext.Current.Request.QueryString["SS" + rs2.GetInt32(1)] != null)
							{
								tempSql = " LEFT(r.SortString," + HttpContext.Current.Request.QueryString["SS" + rs2.GetInt32(1)].ToString().Replace("'","").Length + ") = '" + HttpContext.Current.Request.QueryString["SS" + rs2.GetInt32(1)].ToString().Replace("'","") + "'";
							}
							else
							{
								tempSql = " r.ProjectRoundID = " + rs2.GetInt32(1);
							}
						}
						else
						{
							tempSql = " LEFT(r.SortString," + rs2.GetString(1).Length + ") = '" + rs2.GetString(1) + "'";
						}
						sql = "SELECT " +
							"COUNT(av1.AnswerID), " +
							"COUNT(ocs.OrderValue) " +
							"FROM ProjectRoundUnit r " +
							"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"LEFT OUTER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.OrderValue = 100 " +
							"WHERE a.EndDT IS NOT NULL AND" + tempSql;
						rs = Db.sqlRecordSet(sql);
						if(rs.Read())
						{
							if(rs.GetInt32(0) >= rac)
							{
								string s = (r == -1 && HttpContext.Current.Request.QueryString["U" + rs2.GetInt32(1)] != null ? HttpContext.Current.Request.QueryString["U" + rs2.GetInt32(1)] : rs2.GetString(0));
								while(sl.Contains(s)) s += " ";
								sl.Add(s,(rs.IsDBNull(0) || rs.IsDBNull(1) || (float)Convert.ToDouble(rs.GetValue(0)) == 0f ? -1f : (float)Convert.ToDouble(rs.GetValue(1))/(float)Convert.ToDouble(rs.GetValue(0))*100f));
							}
						}
						rs.Close();
					}
					rs2.Close();

					bool noTotal = (HttpContext.Current.Request.QueryString["NoTotal"] != null);
					g = new Graph(550,520,color);
					//g.leftSpacing = 150;
					g.setMinMax(0f,100f);
					g.computeSteping(sl.Count+2+(noTotal?0:1));
					
					g.drawOutlines(11,true,false);
					g.drawAxis(false);
					rs = Db.sqlRecordSet("SELECT ocl.Text FROM OptionComponentLang ocl INNER JOIN OptionComponents ocs ON ocl.OptionComponentID = ocs.OptionComponentID WHERE ocs.OptionID = " + o + " AND ocl.LangID = " + LID + " AND ocs.OrderValue = 100");
					if(rs.Read())
					{
						g.drawAxisExpl(rs.GetString(0) + (percent ? ", %" : ""), 0,false,false);
					}
					rs.Close();

					int sx = 0;
					if(!noTotal)
					{
						if(r == -1)
						{
							rs2 = Db.sqlRecordSet("SELECT '" + (unitDesc != "" ? unitDesc : projectRoundDesc).Replace("'","") + "'");
						}
						else
						{
							rs2 = Db.sqlRecordSet("SELECT " +
								"pru.Unit, " +
								"pru.SortString " +
								"FROM ProjectRoundUnit pru " +
								"WHERE pru.ProjectRoundID = " + r + " " +
								"AND pru.ProjectRoundUnitID IN (" + units + ")");
						}
						while(rs2.Read())
						{
							sx++;

							string tempSql = "";
							int x = 0;

							if(r == -1)
							{
								tempSql = " r.ProjectRoundID IN (" + rnds + ")";
								x = Db.getInt32("SELECT COUNT(DISTINCT pru.GroupID) FROM ProjectRoundUser pru INNER JOIN ProjectRoundUnit r ON pru.ProjectRoundUnitID = r.ProjectRoundUnitID WHERE" + tempSql);
							}
							else
							{
								tempSql = " LEFT(r.SortString," + rs2.GetString(1).Length + ") = '" + rs2.GetString(1) + "'";
								x = Db.getInt32("SELECT COUNT(DISTINCT pru.GroupID) FROM ProjectRoundUser pru INNER JOIN ProjectRoundUnit r ON pru.ProjectRoundUnitID = r.ProjectRoundUnitID WHERE" + tempSql);
							}

							sql = "SELECT " +
								"COUNT(av1.AnswerID), " +
								"COUNT(ocs.OrderValue) " +
								"FROM ProjectRoundUnit r " +
								"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
								"LEFT OUTER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.OrderValue = 100 " +
								"WHERE a.EndDT IS NOT NULL AND" + tempSql;
							rs = Db.sqlRecordSet(sql);
							if(rs.Read())
							{
								if(rs.GetInt32(0) >= rac)
								{
									if(x > 0)
									{
										g.drawMultiBar(1,sx,(rs.IsDBNull(0) || rs.IsDBNull(1) || (float)Convert.ToDouble(rs.GetValue(0)) == 0f ? -1f : (float)Convert.ToDouble(rs.GetValue(1))/(float)Convert.ToDouble(rs.GetValue(0))*100f),x+1,0,"");
									}
									else
									{
										g.drawBar(1,sx,(rs.IsDBNull(0) || rs.IsDBNull(1) || (float)Convert.ToDouble(rs.GetValue(0)) == 0f ? -1f : (float)Convert.ToDouble(rs.GetValue(1))/(float)Convert.ToDouble(rs.GetValue(0))*100f));
									}
								}
							}
							rs.Close();
							if(x > 0)
							{
								g.drawColorExplBox("Total",1,175,20);

								int sxx = 0;
								sql = "SELECT " +
									"COUNT(av1.AnswerID), " +
									"COUNT(ocs.OrderValue), " +
									"g.GroupDesc " +
									"FROM ProjectRoundUnit r " +
									"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									"INNER JOIN ProjectRoundUser pru ON a.ProjectRoundUserID = pru.ProjectRoundUserID " +
									"INNER JOIN [Group] g ON pru.GroupID = g.GroupID " +
									"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
									"LEFT OUTER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.OrderValue = 100 " +
									"WHERE a.EndDT IS NOT NULL AND" + tempSql + " GROUP BY g.GroupDesc ORDER BY COUNT(*) DESC";
								rs = Db.sqlRecordSet(sql);
								while(rs.Read())
								{
									if(rs.GetInt32(0) >= rac)
									{
										sxx++;
										g.drawMultiBar(3+sxx,sx,(rs.IsDBNull(0) || rs.IsDBNull(1) || (float)Convert.ToDouble(rs.GetValue(0)) == 0f ? -1f : (float)Convert.ToDouble(rs.GetValue(1))/(float)Convert.ToDouble(rs.GetValue(0))*100f),x+1,sxx,"");
										if(sx == 1)
										{
											g.drawColorExplBox(rs.GetString(2),3+sxx,175+sxx*80,20);
										}
									}
								}
								rs.Close();
							}
							g.drawBottomString(rs2.GetString(0),sx,true,false,false);
						}
						rs2.Close();
					}

					for(int i=0; i<sl.Count; i++)
					{
						int pos = i + 1 + sx;

						float v = (float)sl.GetByIndex(i);
						if(v >= 0)
						{
							g.drawBar(1,pos,v);
						}
						g.drawBottomString((string)sl.GetKey(i),pos,true,false,false);
					}

					if(r == -1)
					{
						sql = "SELECT " +
							"COUNT(av1.AnswerID), " +
							"COUNT(ocs.OrderValue) " +
							"FROM ProjectRoundUnit r " +
							"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"LEFT OUTER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.OrderValue = 100 ";
					}
					else
					{
						sql = "SELECT " +
							"COUNT(av1.AnswerID), " +
							"COUNT(ocs.OrderValue) " +
							"FROM ProjectRoundUnit r " +
							"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"LEFT OUTER JOIN OptionComponents ocs ON av1.ValueInt = ocs.OptionComponentID AND av1.OptionID = ocs.OptionID AND ocs.OrderValue = 100 " +
							"WHERE a.EndDT IS NOT NULL AND a.ProjectRoundID IN (" + rnds + ")";
					}
					rs = Db.sqlRecordSet(sql);
					if(rs.Read())
					{
						if(rs.GetInt32(0) >= rac)
						{
							float d = (rs.IsDBNull(0) || rs.IsDBNull(1) || (float)Convert.ToDouble(rs.GetValue(0)) == 0f ? -1f : (float)Convert.ToDouble(rs.GetValue(1))/(float)Convert.ToDouble(rs.GetValue(0))*100f);
							if(d != -1f)
								g.drawReferenceLine(Convert.ToInt32(d), (r == -1 ? "Database" : r1));
						}
					}
					rs.Close();

					#endregion
				}
				else if(t == 401 || t == 402 || t == 405 || t == 406)			// w=320
				{
					string sql = "";

					#region Likert top option, comp with children
					int dx = 0, ex = 0, fx = 0, qx = 0, wx = 0; float score = 0, scoreE = 0; ArrayList al = new ArrayList();
					SortedList sl = new SortedList();
					
					// Comps
					System.Data.SqlClient.SqlDataReader rs2;
					if(r == -1)
					{
						rs2 = Db.sqlRecordSet("SELECT Internal, ProjectRoundID FROM ProjectRound WHERE ProjectRoundID IN (" + rnds + ") ORDER BY Internal");
					}
					else
					{
						rs2 = Db.sqlRecordSet("SELECT " +
							"pru.Unit, " +
							"pru.SortString " +
							"FROM ProjectRoundUnit pru " +
							"WHERE pru.ProjectRoundID = " + r + " " +
							"AND pru.ProjectRoundUnitID IN (0" + units2 + ")");
					}
					while(rs2.Read())
					{
						string tempSql = "";
						if(r == -1)
						{
							if(HttpContext.Current.Request.QueryString["SS" + rs2.GetInt32(1)] != null)
							{
								tempSql = " LEFT(r.SortString," + HttpContext.Current.Request.QueryString["SS" + rs2.GetInt32(1)].ToString().Replace("'","").Length + ") = '" + HttpContext.Current.Request.QueryString["SS" + rs2.GetInt32(1)].ToString().Replace("'","") + "'";
							}
							else
							{
								tempSql = " r.ProjectRoundID = " + rs2.GetInt32(1);
							}
						}
						else
						{
							tempSql = " LEFT(r.SortString," + rs2.GetString(1).Length + ") = '" + rs2.GetString(1) + "'";
						}

						dx = 0; ex = 0; fx = 0; qx = 0; wx = 0; score = 0; scoreE = 0; al = new ArrayList();
						if(t == 401 || t == 402)
						{
							t1(groupID, ref dx, ref ex, ref fx, ref qx, ref score, ref scoreE, al, "AND" + tempSql);
						}
						else if(t == 405)
						{
							double[] n = new double[2];n[0]=0;n[1]=0;
							t5(groupID, ref dx, ref ex, ref n, "AND" + tempSql);
						}
						else if(t == 406)
						{
							t6(groupID, ref dx, ref ex, ref qx, ref wx, ref score, "AND" + tempSql);
						}
						if(dx >= rac)
						{
							string s = (r == -1 && HttpContext.Current.Request.QueryString["U" + rs2.GetInt32(1)] != null ? HttpContext.Current.Request.QueryString["U" + rs2.GetInt32(1)] : rs2.GetString(0));
							while(sl.Contains(s)) s += " ";
							if(t == 401)
							{
								sl.Add(s,(float)Math.Round(score/(float)dx,2));
							}
							else if(t == 402)
							{
								sl.Add(s,(float)Math.Round(((float)fx/(float)dx)*100f,2));
							}
							else if(t == 405 || t == 406)
							{
								sl.Add(s,(float)Math.Round(((float)ex/(float)dx)*100f,2));
							}
						}
					}
					rs2.Close();
					//					HttpContext.Current.Response.End();

					bool noTotal = (HttpContext.Current.Request.QueryString["NoTotal"] != null);
					g = new Graph(550,520,color);
					//g.leftSpacing = 150;
					if(t == 401)
					{
						g.setMinMax(0f,3f);
						g.computeSteping(sl.Count+2+(noTotal?0:1));
						g.drawOutlines(7,true,false);
					}
					else
					{
						g.setMinMax(0f,100f);
						g.computeSteping(sl.Count+2+(noTotal?0:1));
						g.drawOutlines(11,true,false);
					}
					g.drawAxis(false);
					if(t != 401)
					{
						g.drawAxisExpl("%", 0,false,false);
					}
					if(t == 405)
					{
						rac = 20;
					}

					int sx = 0;
					if(!noTotal)
					{
						if(r == -1)
						{
							rs2 = Db.sqlRecordSet("SELECT '" + (unitDesc != "" ? unitDesc : projectRoundDesc).Replace("'","") + "'");
						}
						else
						{
							rs2 = Db.sqlRecordSet("SELECT " +
								"pru.Unit, " +
								"pru.SortString " +
								"FROM ProjectRoundUnit pru " +
								"WHERE pru.ProjectRoundID = " + r + " " +
								"AND pru.ProjectRoundUnitID IN (" + units + ")");
						}
						while(rs2.Read())
						{
							sx++;

							string tempSql = "";
							int x = 0;

							if(r == -1)
							{
								tempSql = " r.ProjectRoundID IN (" + rnds + ")";
								x = Db.getInt32("SELECT COUNT(DISTINCT pru.GroupID) FROM ProjectRoundUser pru INNER JOIN ProjectRoundUnit r ON pru.ProjectRoundUnitID = r.ProjectRoundUnitID WHERE" + tempSql);
							}
							else
							{
								tempSql = " LEFT(r.SortString," + rs2.GetString(1).Length + ") = '" + rs2.GetString(1) + "'";
								x = Db.getInt32("SELECT COUNT(DISTINCT pru.GroupID) FROM ProjectRoundUser pru INNER JOIN ProjectRoundUnit r ON pru.ProjectRoundUnitID = r.ProjectRoundUnitID WHERE" + tempSql);
							}

							dx = 0; ex = 0; fx = 0; qx = 0; wx = 0; score = 0; scoreE = 0; al = new ArrayList();
							if(t == 401 || t == 402)
							{
								t1(groupID, ref dx, ref ex, ref fx, ref qx, ref score, ref scoreE, al, "AND" + tempSql);
							}
							else if(t == 405)
							{
								double[] n = new double[2];n[0]=0;n[1]=0;
								t5(groupID, ref dx, ref ex, ref n, "AND" + tempSql);
							}
							else if(t == 406)
							{
								t6(groupID, ref dx, ref ex, ref qx, ref wx, ref score, "AND" + tempSql);
							}

							if(dx >= rac)
							{
								if(x > 0)
								{
									if(t == 401)
									{
										g.drawMultiBar(1,sx,(float)Math.Round(score/(float)dx,2),x+1,0,"");
									}
									else if(t == 402)
									{
										g.drawMultiBar(1,sx,(float)Math.Round(((float)fx/(float)dx)*100f,2),x+1,0,"");
									}
									else if(t == 405 || t == 406)
									{
										g.drawMultiBar(1,sx,(float)Math.Round(((float)ex/(float)dx)*100f,2),x+1,0,"");
									}
								}
								else
								{
									if(t == 401)
									{
										g.drawBar(1,sx,(float)Math.Round(score/(float)dx,2));
									}
									else if(t == 402)
									{
										g.drawBar(1,sx,(float)Math.Round(((float)fx/(float)dx)*100f,2));
									}
									else if(t == 405 || t == 406)
									{
										g.drawBar(1,sx,(float)Math.Round(((float)ex/(float)dx)*100f,2));
									}
								}
							}

							if(x > 0)
							{
								g.drawColorExplBox("Total",1,175,20);

								int sxx = 0;
								sql = "SELECT " +
									"g.GroupID, " +
									"g.GroupDesc " +
									"FROM ProjectRoundUnit r " +
									"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									"INNER JOIN ProjectRoundUser pru ON a.ProjectRoundUserID = pru.ProjectRoundUserID " +
									"INNER JOIN [Group] g ON pru.GroupID = g.GroupID " +
									"WHERE a.EndDT IS NOT NULL AND" + tempSql + " GROUP BY g.GroupID, g.GroupDesc ORDER BY COUNT(*) DESC";
								rs = Db.sqlRecordSet(sql);
								while(rs.Read())
								{
									dx = 0; ex = 0; fx = 0; qx = 0; wx = 0; score = 0; scoreE = 0; al = new ArrayList();
									if(t == 401 || t == 402)
									{
										t1(groupID, ref dx, ref ex, ref fx, ref qx, ref score, ref scoreE, al, "AND" + tempSql + " AND u.GroupID = " + rs.GetInt32(0));
									}
									else if(t == 405)
									{
										double[] n = new double[2];n[0]=0;n[1]=0;
										t5(groupID, ref dx, ref ex, ref n, "AND" + tempSql + " AND u.GroupID = " + rs.GetInt32(0));
									}
									else if(t == 406)
									{
										t6(groupID, ref dx, ref ex, ref qx, ref wx, ref score, "AND" + tempSql + " AND u.GroupID = " + rs.GetInt32(0));
									}

									if(dx >= rac)
									{
										sxx++;
										if(t == 401)
										{
											g.drawMultiBar(3+sxx,sx,(float)Math.Round(score/(float)dx,2),x+1,sxx,"");
										}
										else if(t == 402)
										{
											g.drawMultiBar(3+sxx,sx,(float)Math.Round(((float)fx/(float)dx)*100f,2),x+1,sxx,"");
										}
										else if(t == 405 || t == 406)
										{
											g.drawMultiBar(3+sxx,sx,(float)Math.Round(((float)ex/(float)dx)*100f,2),x+1,sxx,"");
										}
										if(sx == 1)
										{
											g.drawColorExplBox(rs.GetString(1),3+sxx,175+sxx*80,20);
										}
									}
								}
								rs.Close();
							}
							g.drawBottomString(rs2.GetString(0),sx,true,false,false);
						}
						rs2.Close();
					}

					for(int i=0; i<sl.Count; i++)
					{
						int pos = i + 1 + sx;

						float v = (float)sl.GetByIndex(i);
						if(v >= 0)
						{
							g.drawBar(1,pos,v);
						}
						g.drawBottomString((string)sl.GetKey(i),pos,true,false,false);
					}

					if(r == -1)
					{
						sql = "";
					}
					else
					{
						sql = "AND a.ProjectRoundID IN (" + rnds + ")";
					}
					dx = 0; ex = 0; fx = 0; qx = 0; wx = 0; score = 0; scoreE = 0; al = new ArrayList();
					if(t == 401 || t == 402)
					{
						t1(groupID, ref dx, ref ex, ref fx, ref qx, ref score, ref scoreE, al, sql);
					}
					else if(t == 405)
					{
						double[] n = new double[2];n[0]=0;n[1]=0;
						t5(groupID, ref dx, ref ex, ref n, sql);
					}
					else if(t == 406)
					{
						t6(groupID, ref dx, ref ex, ref qx, ref wx, ref score, sql);
					}
					if(dx >= rac)
					{
						float d = 0;
						if(t == 401)
						{
							d = (float)Math.Round(score/(float)dx,2);
						}
						else if(t == 402)
						{
							d = (float)Math.Round(((float)fx/(float)dx)*100f,2);
						}
						else if(t == 405 || t == 406)
						{
							d = (float)Math.Round(((float)ex/(float)dx)*100f,2);
						}
						if(d != -1f)
							g.drawReferenceLine(Convert.ToInt32(d), (r == -1 ? "Database" : r1));
					}
					#endregion
				}
				else if(t == 208)			// w=320
				{
					string avgSql = "", sql = "";

					#region VAS, comp with children
					SortedList sl = new SortedList();
					System.Data.SqlClient.SqlDataReader rs2 = Db.sqlRecordSet("SELECT pru.Unit, pru.SortString FROM ProjectRoundUnit pru WHERE pru.ProjectRoundID = " + r + " AND (" + 
						(units != "" ? "pru.ProjectRoundUnitID IN (" + units + ") OR pru.ParentProjectRoundUnitID IN (" + units + ")" : "pru.ParentProjectRoundUnitID IS NULL") +
						") ORDER BY pru.Unit");
					while(rs2.Read())
					{
						string tempSql = " LEFT(r.SortString," + rs2.GetString(1).Length + ") = '" + rs2.GetString(1) + "'";
						avgSql += " OR" + tempSql;
						sql = "SELECT " +
							"AVG(av1.ValueInt) " +
							"FROM ProjectRoundUnit r " +
							"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND" + tempSql;
						//						HttpContext.Current.Response.Write(sql);
						rs = Db.sqlRecordSet(sql);
						if(rs.Read())
						{
							float d = (rs.IsDBNull(0) ? -1f : (float)Convert.ToDouble(rs.GetValue(0)));
							while(sl.Contains(d)) d += 0.01f;
							sl.Add(d,rs2.GetString(0));
						}
						rs.Close();
					}
					rs2.Close();
					//					HttpContext.Current.Response.End();

					g = new Graph(550,520,color);
					g.leftSpacing = 150;
					g.setMinMax(0f,100f);
					g.computeSteping(sl.Count+2);
					
					int steps = 5;
					rs = Db.sqlRecordSet("SELECT COUNT(*) FROM OptionComponents WHERE OptionID = " + o);
					if(rs.Read())
					{
						steps = rs.GetInt32(0);
					}
					rs.Close();
					g.drawOutlines(steps,false,false);
					g.drawAxis(false);

					int dx = 0, tmp = 0;
					rs = Db.sqlRecordSet("SELECT oc.OptionComponentID, ocl.Text, ocs.ExportValue, oc.ExportValue FROM OptionComponents ocs " +
						"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
						"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + LID + " " +
						"WHERE ocs.OptionID = " + o + " ORDER BY ocs.OrderValue");
					while(rs.Read())
					{
						if(!extremeValuesOnly || tmp == 0 || tmp + 1 == steps)
						{
							string tt = cut(rs.GetString(1));
							g.drawAxisVal(tt + (exportValues && (!rs.IsDBNull(2) || !rs.IsDBNull(3)) ? " - " + (rs.IsDBNull(2) ? rs.GetInt32(3) : rs.GetInt32(2)) : ""),steps,dx);
						}
						dx ++;
						tmp ++;
					}
					rs.Close();

					for(int i=0; i<sl.Count; i++)
					{
						int pos = sl.Count - i;

						float v = (float)sl.GetKey(i);
						if(v >= 0)
						{
							g.drawBar(1,pos,v);
						}
						g.drawBottomString((string)sl.GetByIndex(i),pos,true,false,false);
					}

					sql = "SELECT " +
						"AVG(av1.ValueInt) " +
						"FROM ProjectRoundUnit r " +
						"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
						"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
						"WHERE a.EndDT IS NOT NULL AND (1=0" + avgSql + ")";
					rs = Db.sqlRecordSet(sql);
					if(rs.Read())
					{
						float d = (rs.IsDBNull(0) ? -1f : (float)Convert.ToDouble(rs.GetValue(0)));
						if(d != -1f)
							g.drawReferenceLine(Convert.ToInt32(d));
					}
					rs.Close();

					#endregion
				}
				if(t == 408)			// w=320
				{
					string sql = "";

					#region VAS, comp with children
					SortedList sl = new SortedList();
					
					// Comps
					System.Data.SqlClient.SqlDataReader rs2;
					if(r == -1)
					{
						rs2 = Db.sqlRecordSet("SELECT Internal, ProjectRoundID FROM ProjectRound WHERE ProjectRoundID IN (" + rnds + ") ORDER BY Internal");
					}
					else
					{
						rs2 = Db.sqlRecordSet("SELECT " +
							"pru.Unit, " +
							"pru.SortString " +
							"FROM ProjectRoundUnit pru " +
							"WHERE pru.ProjectRoundID = " + r + " " +
							"AND pru.ProjectRoundUnitID IN (0" + units2 + ")");
					}
					while(rs2.Read())
					{
						string tempSql = "";
						if(r == -1)
						{
							if(HttpContext.Current.Request.QueryString["SS" + rs2.GetInt32(1)] != null)
							{
								tempSql = " LEFT(r.SortString," + HttpContext.Current.Request.QueryString["SS" + rs2.GetInt32(1)].ToString().Replace("'","").Length + ") = '" + HttpContext.Current.Request.QueryString["SS" + rs2.GetInt32(1)].ToString().Replace("'","") + "'";
							}
							else
							{
								tempSql = " r.ProjectRoundID = " + rs2.GetInt32(1);
							}
						}
						else
						{
							tempSql = " LEFT(r.SortString," + rs2.GetString(1).Length + ") = '" + rs2.GetString(1) + "'";
						}
						sql = "SELECT " +
							"AVG(av1.ValueInt), " +
							"COUNT(av1.ValueInt) " +
							"FROM ProjectRoundUnit r " +
							"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND" + tempSql;
						rs = Db.sqlRecordSet(sql);
						if(rs.Read())
						{
							if(rs.GetInt32(1) >= rac)
							{
								string s = (r == -1 && HttpContext.Current.Request.QueryString["U" + rs2.GetInt32(1)] != null ? HttpContext.Current.Request.QueryString["U" + rs2.GetInt32(1)] : rs2.GetString(0));
								while(sl.Contains(s)) s += " ";
								sl.Add(s,(rs.IsDBNull(0) ? -1f : (float)Convert.ToDouble(rs.GetValue(0))));
							}
						}
						rs.Close();
					}
					rs2.Close();
					//					HttpContext.Current.Response.End();

					bool noTotal = (HttpContext.Current.Request.QueryString["NoTotal"] != null);
					g = new Graph(550,520,color);
					g.leftSpacing = 150;
					g.setMinMax(0f,100f);
					g.computeSteping(sl.Count+2+(noTotal?0:1));
					
					int steps = 5;
					rs = Db.sqlRecordSet("SELECT COUNT(*) FROM OptionComponents WHERE OptionID = " + o);
					if(rs.Read())
					{
						steps = rs.GetInt32(0);
					}
					rs.Close();
					g.drawOutlines(steps,false,false);
					g.drawAxis(false);

					int dx = 0, tmp = 0;
					rs = Db.sqlRecordSet("SELECT oc.OptionComponentID, ocl.Text, ocs.ExportValue, oc.ExportValue FROM OptionComponents ocs " +
						"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
						"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + LID + " " +
						"WHERE ocs.OptionID = " + o + " ORDER BY ocs.OrderValue");
					while(rs.Read())
					{
						if(!extremeValuesOnly || tmp == 0 || tmp + 1 == steps)
						{
							string tt = cut(rs.GetString(1));
							g.drawAxisVal(tt + (exportValues && (!rs.IsDBNull(2) || !rs.IsDBNull(3)) ? " - " + (rs.IsDBNull(2) ? rs.GetInt32(3) : rs.GetInt32(2)) : ""),steps,dx);
						}
						dx ++;
						tmp ++;
					}
					rs.Close();

					int sx = 0;
					if(!noTotal)
					{
						if(r == -1)
						{
							rs2 = Db.sqlRecordSet("SELECT '" + (unitDesc != "" ? unitDesc : projectRoundDesc).Replace("'","") + "'");
						}
						else
						{
							rs2 = Db.sqlRecordSet("SELECT " +
								"pru.Unit, " +
								"pru.SortString " +
								"FROM ProjectRoundUnit pru " +
								"WHERE pru.ProjectRoundID = " + r + " " +
								"AND pru.ProjectRoundUnitID IN (" + units + ")");
						}
						while(rs2.Read())
						{
							sx++;

							string tempSql = "";
							int x = 0;

							if(r == -1)
							{
								tempSql = " r.ProjectRoundID IN (" + rnds + ")";
								x = Db.getInt32("SELECT COUNT(DISTINCT pru.GroupID) FROM ProjectRoundUser pru INNER JOIN ProjectRoundUnit r ON pru.ProjectRoundUnitID = r.ProjectRoundUnitID WHERE" + tempSql);
							}
							else
							{
								tempSql = " LEFT(r.SortString," + rs2.GetString(1).Length + ") = '" + rs2.GetString(1) + "'";
								x = Db.getInt32("SELECT COUNT(DISTINCT pru.GroupID) FROM ProjectRoundUser pru INNER JOIN ProjectRoundUnit r ON pru.ProjectRoundUnitID = r.ProjectRoundUnitID WHERE" + tempSql);
							}

							sql = "SELECT " +
								"AVG(av1.ValueInt), " +
								"COUNT(av1.ValueInt) " +
								"FROM ProjectRoundUnit r " +
								"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
								"WHERE a.EndDT IS NOT NULL AND" + tempSql;
							//						HttpContext.Current.Response.Write(sql);
							rs = Db.sqlRecordSet(sql);
							if(rs.Read())
							{
								if(rs.GetInt32(1) >= rac)
								{
									if(x > 0)
									{
										// g.drawMultiBar(6,Convert.ToInt32(rs.GetValue(1)),v,g.steping,g.barW,div,1*(div-1),100,true,percent);
										g.drawMultiBar(1,sx,(rs.IsDBNull(0) ? -1f : (float)Convert.ToDouble(rs.GetValue(0))),x+1,0,"");
									}
									else
									{
										g.drawBar(1,sx,(rs.IsDBNull(0) ? -1f : (float)Convert.ToDouble(rs.GetValue(0))));
									}
								}
							}
							rs.Close();
							if(x > 0)
							{
								g.drawColorExplBox("Total",1,175,20);

								int sxx = 0;
								sql = "SELECT " +
									"AVG(av1.ValueInt), " +
									"g.GroupDesc, " +
									"COUNT(av1.ValueInt) " +
									"FROM ProjectRoundUnit r " +
									"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									"INNER JOIN ProjectRoundUser pru ON a.ProjectRoundUserID = pru.ProjectRoundUserID " +
									"INNER JOIN [Group] g ON pru.GroupID = g.GroupID " +
									"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
									"WHERE a.EndDT IS NOT NULL AND" + tempSql + " GROUP BY g.GroupDesc ORDER BY COUNT(*) DESC";
								//						HttpContext.Current.Response.Write(sql);
								rs = Db.sqlRecordSet(sql);
								while(rs.Read())
								{
									if(rs.GetInt32(2) >= rac)
									{
										sxx++;
										// g.drawMultiBar(6,Convert.ToInt32(rs.GetValue(1)),v,g.steping,g.barW,div,1*(div-1),100,true,percent);
										g.drawMultiBar(3+sxx,sx,(rs.IsDBNull(0) ? -1f : (float)Convert.ToDouble(rs.GetValue(0))),x+1,sxx,"");
										if(sx == 1)
										{
											g.drawColorExplBox(rs.GetString(1),3+sxx,175+sxx*80,20);
										}
									}
								}
								rs.Close();
							}
							g.drawBottomString(rs2.GetString(0),sx,true,false,false);
						}
						rs2.Close();
					}

					for(int i=0; i<sl.Count; i++)
					{
						int pos = i + 1 + sx;

						float v = (float)sl.GetByIndex(i);
						if(v >= 0)
						{
							g.drawBar(1,pos,v);
						}
						g.drawBottomString((string)sl.GetKey(i),pos,true,false,false);
					}

					if(r == -1)
					{
						sql = "SELECT " +
							"AVG(av1.ValueInt), " +
							"COUNT(av1.ValueInt) " +
							"FROM ProjectRoundUnit r " +
							"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL ";
					}
					else
					{
						sql = "SELECT " +
							"AVG(av1.ValueInt), " +
							"COUNT(av1.ValueInt) " +
							"FROM ProjectRoundUnit r " +
							"INNER JOIN Answer a ON a.ProjectRoundUnitID = r.ProjectRoundUnitID " +
							"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = " + q + " AND av1.OptionID = " + o + " AND av1.DeletedSessionID IS NULL " +
							"WHERE a.EndDT IS NOT NULL AND a.ProjectRoundID IN (" + rnds + ")";
					}
					rs = Db.sqlRecordSet(sql);
					if(rs.Read())
					{
						if(rs.GetInt32(1) >= rac)
						{
							float d = (rs.IsDBNull(0) ? -1f : (float)Convert.ToDouble(rs.GetValue(0)));
							if(d != -1f)
							{
								g.drawReferenceLine(Convert.ToInt32(d), (r == -1 ? "Database" : r1));
							}
						}
					}
					rs.Close();

					#endregion
				}
				else if(t == 18)	// w=440
				{
					#region VAS -> Likert

					int a1 = 0;
					int a2 = 0;
					bool printedDesc = false;
					bool hasAID = false;

					if(AID1 != 0)
					{
						hasAID = true;

						rs = Db.sqlRecordSet("SELECT av.ValueInt FROM AnswerValue av WHERE av.QuestionID = " + q + " AND av.OptionID = " + o + " AND av.AnswerID = " + AID1 + " AND av.DeletedSessionID IS NULL");
						if(rs.Read())
						{
							a1 = vasToLikert(rs.GetInt32(0));
						}
						rs.Close();
					}
					if(AID2 != 0)
					{
						rs = Db.sqlRecordSet("SELECT av.ValueInt FROM AnswerValue av WHERE av.QuestionID = " + q + " AND av.OptionID = " + o + " AND av.AnswerID = " + AID2 + " AND av.DeletedSessionID IS NULL");
						if(rs.Read())
						{
							a2 = vasToLikert(rs.GetInt32(0));
						}
						rs.Close();
					}

					cx = likert + 2;

					decimal tot = 0, tot2 = 0, totalTot = 0, totalTot2 = 0;
					#region counts
					if(units != "" || aids != "")
					{
						rs = Db.sqlRecordSet("SELECT COUNT(DISTINCT a.AnswerID) FROM Answer a " +
							"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
							//"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.DeletedSessionID IS NULL " +
							//(t != 13 ? "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " : "") +
							"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL " +
							(units != "" ? " AND u.ProjectRoundUnitID IN (" + units + ") " : "") +
							(aids != "" ? " AND a.AnswerID IN (" + aids + ") " : "") +
							"");
						if(rs.Read())
						{
							tot = Convert.ToDecimal(rs.GetInt32(0));
						}
						rs.Close();
						if(rr != 0 && units2 != "")
						{
							rs = Db.sqlRecordSet("SELECT COUNT(DISTINCT a.AnswerID) FROM Answer a " +
								"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								//"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.DeletedSessionID IS NULL " +
								//(t != 13 ? "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " : "") +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND u.ProjectRoundUnitID IN (" + units2 + ")");
							if(rs.Read())
							{
								tot2 = Convert.ToDecimal(rs.GetInt32(0));
							}
							rs.Close();
						}
					}
					if(showTotal)
					{
						if(u2c != 0)
						{
							if(rr != 0)
							{
								rs = Db.sqlRecordSet("SELECT COUNT(DISTINCT a.AnswerID) FROM Answer a " +
									"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
									"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									//"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.DeletedSessionID IS NULL " +
									//(t != 13 ? "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " : "") +
									"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL AND u.ProjectRoundID = " + rr + " AND r.UnitCategoryID = " + u2c);
								if(rs.Read())
								{
									totalTot2 = Convert.ToDecimal(rs.GetInt32(0));
								}
								rs.Close();
							}
							rs = Db.sqlRecordSet("SELECT COUNT(DISTINCT a.AnswerID) FROM Answer a " +
								"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								//"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.DeletedSessionID IS NULL " +
								//(t != 13 ? "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " : "") +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL AND u.ProjectRoundID = " + r + " AND r.UnitCategoryID = " + u2c);
							if(rs.Read())
							{
								totalTot = Convert.ToDecimal(rs.GetInt32(0));
							}
							rs.Close();
						}
						else
						{
							if(rr != 0)
							{
								rs = Db.sqlRecordSet("SELECT COUNT(DISTINCT a.AnswerID) FROM Answer a " +
									"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
									"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									//"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.DeletedSessionID IS NULL " +
									//(t != 13 ? "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " : "") +
									"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL AND u.ProjectRoundID = " + rr);
								if(rs.Read())
								{
									totalTot2 = Convert.ToDecimal(rs.GetInt32(0));
								}
								rs.Close();
							}
							rs = Db.sqlRecordSet("SELECT COUNT(DISTINCT a.AnswerID) FROM Answer a " +
								"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
								"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
								//"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.DeletedSessionID IS NULL " +
								//(t != 13 ? "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.OptionID = " + o + " AND av.QuestionID = " + q + " AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " : "") +
								"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND r.Terminated IS NULL" + (r != 0 ? " AND u.ProjectRoundID = " + r : ""));
							if(rs.Read())
							{
								totalTot = Convert.ToDecimal(rs.GetInt32(0));
							}
							rs.Close();
						}
					}
					#endregion
					float max = (percent ? 100f : (float)Convert.ToDouble(Math.Max(Math.Max(Math.Max(tot,tot2),totalTot),totalTot2)));
					max = (float)Math.Round(max/10,0)*10;
					g.setMinMax(0f,max);

					if(cx > 0)
					{
						int Q = cx;
						double[] v1 = new double[Q];
						double[] n1 = new double[Q];
						double[] v2 = new double[Q];
						double[] n2 = new double[Q];

						g.computeSteping(cx);
						g.drawOutlines(11,true,false);
						g.drawAxis(false);
						//g.drawRightAxis();
						g.drawAxisExpl((percent ? "%" : ""), 0,false,false);

						cx = 0;

						int div = (showTotal ? (rr != 0 ? 2 : 1) : 0);	// if comparative round, add 2 instead of 1

						if(units != "" || aids != "")
						{
							#region Current round, increment div and calculate tot
							div++;							
							#endregion

							if(rr != 0 && units2 != "")
							{
								#region Other comparative round, increment div and calculate tot
								div++;
								#endregion
							}

							if(rac > Convert.ToInt32(tot) && (rr == 0 && units2 == "" || rac > Convert.ToInt32(tot2)))
							{
								switch(LID)
								{
									case 1: g.drawColorExplBox(unitDesc.Replace("[x]","") + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
									case 2: g.drawColorExplBox(unitDesc.Replace("[x]","") + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
								}
							
							}
							else
							{
								if(rr != 0 && units2 != "")
								{
									#region Other comparative round, color 7, start at 0
									if(rac > Convert.ToInt32(tot2))
									{
										switch(LID)
										{
											case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120+(hasAID?75:0), 40);break;
											case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + ", n < " + rac, 7, 120+(hasAID?75:0), 40);break;
										}
									
									}
									else
									{
										g.drawColorExplBox(unitDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + tot2 : ""), 7, 120+(hasAID?75:0), 40);

										cx = 0;
										rs = Db.sqlRecordSet("SELECT " +
											"NULL, " +
											"CEILING(CAST(av.ValueInt + 1 AS REAL) / 101 * " + likert + ") AS AX, " +
											"COUNT(*) " +
											"FROM Answer a " + 
											"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
											"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
											"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " +
											"AND av.OptionID = " + o + " " +
											"AND av.QuestionID = " + q + " " +
											"AND u.ProjectRoundUnitID IN (" + units2 + ") " +
											"GROUP BY CEILING(CAST(av.ValueInt + 1 AS REAL) / 101 * " + likert + ") " +
											"ORDER BY AX");
										while(rs.Read())
										{
											if(!rs.IsDBNull(1))
											{
												n2[Convert.ToInt32(rs.GetValue(1))-1] = Convert.ToDouble(rs.GetValue(2));
												v2[Convert.ToInt32(rs.GetValue(1))-1] = Convert.ToDouble(rs.GetValue(1));
											}

											int v = Convert.ToInt32(rs.GetValue(2));
											if(percent)
												v = Convert.ToInt32(Math.Round(Convert.ToDecimal(v)/tot2*100M,0));
											g.drawMultiBar(7,Convert.ToInt32(rs.GetValue(1)),v,g.steping,g.barW,div,0,100,true,percent);
											if(!printedDesc)
											{
												while(cx < Convert.ToInt32(rs.GetValue(1)))
												{
													cx++;
													g.drawBottomString(cx.ToString(),cx,true);
												}
											}
										}
										rs.Close();

										if(!printedDesc && cx < likert)
										{
											while(cx < likert)
											{
												cx++;
												g.drawBottomString(cx.ToString(),cx,true);
											}
										}
										printedDesc = true;
									}
									#endregion
								}

								#region Current round, color 5, start at 0 or 1
								if(rac > Convert.ToInt32(tot))
								{
									switch(LID)
									{
										case 1: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
										case 2: g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + ", n < " + rac, 5, 120+(hasAID?75:0), 20);break;
									}
								
								}
								else
								{
									g.drawColorExplBox(unitDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + tot : ""), 5, 120+(hasAID?75:0), 20);

									rs = Db.sqlRecordSet("SELECT " +
										"NULL, " +
										"CEILING(CAST(av.ValueInt + 1 AS REAL) / 101 * " + likert + ") AS AX, " +
										"COUNT(*) " +
										"FROM Answer a " +  
										"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
										"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
										"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " +
										"AND av.OptionID = " + o + " " +
										"AND av.QuestionID = " + q + " " +
										(units != "" ? " AND u.ProjectRoundUnitID IN (" + units + ") " : "") +
										(aids != "" ? " AND a.AnswerID IN (" + aids + ") " : "") +
										"" +
										"GROUP BY CEILING(CAST(av.ValueInt + 1 AS REAL) / 101 * " + likert + ") " +
										"ORDER BY AX");
									while(rs.Read())
									{
										if(!rs.IsDBNull(1))
										{
											n1[Convert.ToInt32(rs.GetValue(1))-1] = Convert.ToDouble(rs.GetValue(2));
											v1[Convert.ToInt32(rs.GetValue(1))-1] = Convert.ToDouble(rs.GetValue(1));
										}

										int v = Convert.ToInt32(rs.GetValue(2));
										if(percent)
											v = Convert.ToInt32(Math.Round(Convert.ToDecimal(v)/tot*100M,0));
										g.drawMultiBar(5,Convert.ToInt32(rs.GetValue(1)),v,g.steping,g.barW,div,(rr != 0 && units2 != "" ? 1 : 0),100,true,percent);
										if(!printedDesc)
										{
											while(cx < Convert.ToInt32(rs.GetValue(1)))
											{
												cx++;
												g.drawBottomString(cx.ToString(),cx,true);
											}
										}
									}
									rs.Close();

									if(!printedDesc && cx < likert)
									{
										while(cx < likert)
										{
											cx++;
											g.drawBottomString(cx.ToString(),cx,true);
										}
									}
									printedDesc = true;
								}
								#endregion

								if(rr != 0 && units2 != "" && rac <= Convert.ToInt32(tot) && rac <= Convert.ToInt32(tot2))
								{
									double tt = 0; int df = 0;
									bool sign = significant(tot,tot2,Q,v1,n1,v2,n2,ref tt,ref df);

									g.drawStringInGraph("Change " + (sign ? "" : "not ") + "significant (t=" + Math.Round(tt,2) + ", p" + (sign ? "<" : ">") + "0.05, df " + df + ")",10,10);
									//g.drawStringInGraph("Change " + (sign ? "" : "not ") + "significant (p" + (sign ? "<" : ">") + "0.05)",80,10);
								}
							}
						}

						if(showTotal)
						{
							if(u2c != 0)
							{
								if(rr != 0)
								{
									#region Other comparative round, color 8, start at div-2
									if(rac > Convert.ToInt32(totalTot2))
									{
										switch(LID)
										{
											case 1: g.drawColorExplBox(u2.Replace("[x]"," " + r2) + ", n < " + rac, 8, 320, 40); break;
											case 2: g.drawColorExplBox(u2.Replace("[x]"," " + r2) + ", n < " + rac, 8, 320, 40); break;
										}
								
									}
									else
									{
										g.drawColorExplBox(u2.Replace("[x]"," " + r2) + (showN ? ", n=" + totalTot2 : ""), 8, 320, 40);
									}

									cx = 0;
									rs = Db.sqlRecordSet("SELECT " +
										"NULL, " +
										"CEILING(CAST(av.ValueInt + 1 AS REAL) / 101 * " + likert + ") AS AX, " +
										"COUNT(*) " +
										"FROM Answer a " + 
										"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
										"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
										"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
										"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " +
										"AND av.OptionID = " + o + " " +
										"AND av.QuestionID = " + q + " " +
										"AND r.Terminated IS NULL AND u.ProjectRoundID = " + rr + " AND r.UnitCategoryID = " + u2c +
										"GROUP BY CEILING(CAST(av.ValueInt + 1 AS REAL) / 101 * " + likert + ") " +
										"ORDER BY AX");
									while(rs.Read())
									{
										if(!rs.IsDBNull(1))
										{
											n2[Convert.ToInt32(rs.GetValue(1))-1] = Convert.ToDouble(rs.GetValue(2));
											v2[Convert.ToInt32(rs.GetValue(1))-1] = Convert.ToDouble(rs.GetValue(1));
										}

										if(rac > Convert.ToInt32(totalTot2))
										{
											//
										}
										else
										{
											int v = Convert.ToInt32(rs.GetValue(2));
											if(percent)
												v = Convert.ToInt32(Math.Round(Convert.ToDecimal(v)/totalTot2*100M,0));
											g.drawMultiBar(8,Convert.ToInt32(rs.GetValue(1)),v,g.steping,g.barW,div,1*(div-2),100,true,percent);
										}
										if(!printedDesc)
										{
											while(cx < Convert.ToInt32(rs.GetValue(1)))
											{
												cx++;
												g.drawBottomString(cx.ToString(),cx,true);
											}
										}
									}
									rs.Close();

									if(!printedDesc && cx < likert)
									{
										while(cx < likert)
										{
											cx++;
											g.drawBottomString(cx.ToString(),cx,true);
										}
									}
									printedDesc = true;
									#endregion
								}

								#region Current round, color 6, start at div-1
								if(rac > Convert.ToInt32(totalTot))
								{
									switch(LID)
									{
										case 1: g.drawColorExplBox(u2.Replace("[x]"," " + r1) + ", n < " + rac, 6, 320, 20); break;
										case 2: g.drawColorExplBox(u2.Replace("[x]"," " + r1) + ", n < " + rac, 6, 320, 20); break;
									}
								
								}
								else
								{
									g.drawColorExplBox(u2.Replace("[x]"," " + r1) + (showN ? ", n=" + totalTot : ""), 6, 320, 20);
								}

								cx = 0;
								rs = Db.sqlRecordSet("SELECT " +
									"NULL, " +
									"CEILING(CAST(av.ValueInt + 1 AS REAL) / 101 * " + likert + ") AS AX, " +
									"COUNT(*) " +
									"FROM Answer a " + 
									"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
									"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
									"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " +
									"AND av.OptionID = " + o + " " +
									"AND av.QuestionID = " + q + " " +
									"AND r.Terminated IS NULL AND u.ProjectRoundID = " + r + " AND r.UnitCategoryID = " + u2c +
									"GROUP BY CEILING(CAST(av.ValueInt + 1 AS REAL) / 101 * " + likert + ") " +
									"ORDER BY AX");
								while(rs.Read())
								{
									if(!rs.IsDBNull(1))
									{
										n1[Convert.ToInt32(rs.GetValue(1))-1] = Convert.ToDouble(rs.GetValue(2));
										v1[Convert.ToInt32(rs.GetValue(1))-1] = Convert.ToDouble(rs.GetValue(1));
									}
									if(rac > Convert.ToInt32(totalTot))
									{
										//
									}
									else
									{
										int v = Convert.ToInt32(rs.GetValue(2));
										if(percent)
											v = Convert.ToInt32(Math.Round(Convert.ToDecimal(v)/totalTot*100M,0));
										g.drawMultiBar(6,Convert.ToInt32(rs.GetValue(1)),v,g.steping,g.barW,div,1*(div-1),100,true,percent);
									}
									if(!printedDesc)
									{
										while(cx < Convert.ToInt32(rs.GetValue(1)))
										{
											cx++;
											g.drawBottomString(cx.ToString(),cx,true);
										}
									}

									if(a1 == Convert.ToInt32(rs.GetValue(1)))
									{
										g.drawColorExplBox(AID1txt, 20, 70, 20);
										g.drawDotUnder(Convert.ToInt32(rs.GetValue(1)),false,(a1 == a2 ? -10 : 0),18);
									}
									if(a2 == Convert.ToInt32(rs.GetValue(1)))
									{
										g.drawColorExplBox(AID2txt, 19, 70, 40);
										g.drawDotUnder(Convert.ToInt32(rs.GetValue(1)),true,(a1 == a2 ? 10 : 0),18);
									}
								}
								rs.Close();

								if(!printedDesc && cx < likert)
								{
									while(cx < likert)
									{
										cx++;
										g.drawBottomString(cx.ToString(),cx,true);
									}
								}
								printedDesc = true;
								#endregion
							}
							else
							{
								if(rr != 0)
								{
									#region Other comparative round, color 8, start at div-2
									if(rac > Convert.ToInt32(totalTot2))
									{
										switch(LID)
										{
											case 1: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + ", n < " + rac, 8, 320, 40); break;
											case 2: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + ", n < " + rac, 8, 320, 40); break;
										}
								
									}
									else
									{
										g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r2) + (showN ? ", n=" + totalTot2 : ""), 8, 320, 40);
									}

									cx = 0;
									rs = Db.sqlRecordSet("SELECT " +
										"NULL, " +
										"CEILING(CAST(av.ValueInt + 1 AS REAL) / 101 * " + likert + ") AS AX, " +
										"COUNT(*) " +
										"FROM Answer a " + 
										"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
										"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
										"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
										"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " +
										"AND av.OptionID = " + o + " " +
										"AND av.QuestionID = " + q + " " +
										"AND r.Terminated IS NULL AND u.ProjectRoundID = " + rr +
										"GROUP BY CEILING(CAST(av.ValueInt + 1 AS REAL) / 101 * " + likert + ") " +
										"ORDER BY AX");
									while(rs.Read())
									{
										if(!rs.IsDBNull(1))
										{
											n2[Convert.ToInt32(rs.GetValue(1))-1] = Convert.ToDouble(rs.GetValue(2));
											v2[Convert.ToInt32(rs.GetValue(1))-1] = Convert.ToDouble(rs.GetValue(1));
										}

										if(rac > Convert.ToInt32(totalTot2))
										{
											//
										}
										else
										{
											int v = Convert.ToInt32(rs.GetValue(2));
											if(percent)
												v = Convert.ToInt32(Math.Round(Convert.ToDecimal(v)/totalTot2*100M,0));
											g.drawMultiBar(8,Convert.ToInt32(rs.GetValue(1)),v,g.steping,g.barW,div,1*(div-2),100,true,percent);
										}
										if(!printedDesc)
										{
											while(cx < Convert.ToInt32(rs.GetValue(1)))
											{
												cx++;
												g.drawBottomString(cx.ToString(),cx,true);
											}
										}
									}
									rs.Close();

									if(!printedDesc && cx < likert)
									{
										while(cx < likert)
										{
											cx++;
											g.drawBottomString(cx.ToString(),cx,true);
										}
									}
									printedDesc = true;
									#endregion
								}

								#region Current round, color 6, start at div-1
								if(rac > Convert.ToInt32(totalTot))
								{
									switch(LID)
									{
										case 1: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + ", n < " + rac, 6, 320, 20); break;
										case 2: g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + ", n < " + rac, 6, 320, 20); break;
									}
								
								}
								else
								{
									g.drawColorExplBox(projectRoundDesc.Replace("[x]"," " + r1) + (showN ? ", n=" + totalTot : ""), 6, 320, 20);
								}

								string sql = "SELECT " +
									"NULL, " +
									"CEILING(CAST(av.ValueInt + 1 AS REAL) / 101 * " + likert + ") AS AX, " +
									"COUNT(*) " +
									"FROM Answer a " + 
									"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
									"INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
									"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
									"WHERE a.EndDT IS NOT NULL AND u.Terminated IS NULL AND u.NoSend IS NULL AND av.ValueInt IS NOT NULL AND av.DeletedSessionID IS NULL " +
									"AND av.OptionID = " + o + " " +
									"AND av.QuestionID = " + q + " " +
									"AND r.Terminated IS NULL" + (r != 0 ? " AND u.ProjectRoundID = " + r : "") +
									"GROUP BY CEILING(CAST(av.ValueInt + 1 AS REAL) / 101 * " + likert + ") " +
									"ORDER BY AX";
//								HttpContext.Current.Response.Clear();
//								HttpContext.Current.Response.ClearHeaders();
//								HttpContext.Current.Response.Write(sql);
//								HttpContext.Current.Response.End();
								rs = Db.sqlRecordSet(sql);
								while(rs.Read())
								{
									if(!rs.IsDBNull(1))
									{
										n1[Convert.ToInt32(rs.GetValue(1))-1] = Convert.ToDouble(rs.GetValue(2));
										v1[Convert.ToInt32(rs.GetValue(1))-1] = Convert.ToDouble(rs.GetValue(1));
									}
									if(rac > Convert.ToInt32(totalTot))
									{
										//
									}
									else
									{
										int v = Convert.ToInt32(rs.GetValue(2));
										if(percent)
											v = Convert.ToInt32(Math.Round(Convert.ToDecimal(v)/totalTot*100M,0));
										g.drawMultiBar(6,Convert.ToInt32(rs.GetValue(1)),v,g.steping,g.barW,div,1*(div-1),100,true,percent);
									}
									if(!printedDesc)
									{
										while(cx < Convert.ToInt32(rs.GetValue(1)))
										{
											cx++;
											g.drawBottomString(cx.ToString(),cx,true);
										}
									}

									if(a1 == Convert.ToInt32(rs.GetValue(1)))
									{
										g.drawColorExplBox(HttpContext.Current.Server.HtmlDecode(AID1txt), 20, 70, 20);
										g.drawDotUnder(Convert.ToInt32(rs.GetValue(1)),false,(a1 == a2 ? -10 : 0),18);
									}
									if(a2 == Convert.ToInt32(rs.GetValue(1)))
									{
										g.drawColorExplBox(HttpContext.Current.Server.HtmlDecode(AID2txt), 19, 70, 40);
										g.drawDotUnder(Convert.ToInt32(rs.GetValue(1)),true,(a1 == a2 ? 10 : 0),18);
									}
								}
								rs.Close();

								if(!printedDesc && cx < likert)
								{
									while(cx < likert)
									{
										cx++;
										g.drawBottomString(cx.ToString(),cx,true);
									}
								}
								printedDesc = true;
								#endregion
							}
							if(rr != 0 && rac <= Convert.ToInt32(totalTot) && rac <= Convert.ToInt32(totalTot2))
							{
								double tt = 0; int df = 0;
								bool sign = significant(totalTot,totalTot2,Q,v1,n1,v2,n2,ref tt,ref df);

								g.drawStringInGraph("Change " + (sign ? "" : "not ") + "significant (t=" + Math.Round(tt,2) + ", p" + (sign ? "<" : ">") + "0.05, df " + df + ")",260,10);
								//g.drawStringInGraph("Change " + (sign ? "" : "not ") + "significant (p" + (sign ? "<" : ">") + "0.05)",280,10);
							}
						}
					}
					#endregion
				}
				#endregion
			}
			g.render(outputStream);			
		}
		private static int vasToLikert(int v)
		{
			return Convert.ToInt32(Math.Ceiling(((double)v+1)/(double)101 * (double)v));
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
