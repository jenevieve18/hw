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
	/// Summary description for reportImage.
	/// </summary>
	public class reportImage : System.Web.UI.Page
	{
		int lastCount = 0;
		float lastVal = 0;
		string lastDesc = "";
		System.Collections.Hashtable res = new System.Collections.Hashtable();
		System.Collections.Hashtable cnt = new System.Collections.Hashtable();

		private void getIdxVal(int idx, string sortString)
		{
			OdbcDataReader rs = Db.recordSet("SELECT " +
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
				"INNER JOIN AnswerValue av ON ip.QuestionID = av.QuestionID AND ip.OptionID = av.OptionID AND av.ValueInt = ipc.OptionComponentID " +
				"INNER JOIN Answer a ON av.AnswerID = a.AnswerID " +
				"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
				"WHERE a.EndDT IS NOT NULL AND i.IdxID = " + idx + " AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "' " +
				"GROUP BY i.IdxID, a.AnswerID, i.MaxVal, il.Idx, i.CX, i.AllPartsRequired" +
				") tmp " +
				"WHERE tmp.AllPartsRequired = 0 OR tmp.CX = tmp.BX " +
				"GROUP BY tmp.IdxID, tmp.Idx");
			while(rs.Read())
			{
				lastCount = rs.GetInt32(3);
				lastVal = (float)Convert.ToDouble(rs.GetValue(0));
				lastDesc = rs.GetString(1);
				
				if(!res.Contains(rs.GetInt32(2)))
					res.Add(rs.GetInt32(2),lastVal);
				
				if(!cnt.Contains(rs.GetInt32(2)))
					cnt.Add(rs.GetInt32(2),lastCount);
			}
			rs.Close();
		}

		private void getOtherIdxVal(int idx, string sortString)
		{
			float tot = 0;
			//int div = 0;
			int max = 0;
			int minCnt = Int32.MaxValue;
			OdbcDataReader rs = Db.recordSet("SELECT " +
				"ip.OtherIdxID, " +
				"il.Idx, " +
				"i.MaxVal, " +
				"ip.Multiple " +
				"FROM Idx i " +
				"INNER JOIN IdxLang il ON i.IdxID = il.IdxID AND il.LangID = 1 " +
				"INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID " +
				"WHERE i.IdxID = " + idx);
			if(rs.Read())
			{
				lastDesc = rs.GetString(1);
				do
				{
					max += 100*rs.GetInt32(3);
					if(res.Contains(rs.GetInt32(0)))
					{
						tot += (float)res[rs.GetInt32(0)]*rs.GetInt32(3);
						minCnt = Math.Min((int)cnt[rs.GetInt32(0)], minCnt);
					}
					else
					{
						getIdxVal(rs.GetInt32(0),sortString);
						tot += lastVal*rs.GetInt32(3);
						minCnt = Math.Min(lastCount, minCnt);
					}
					//div = rs.GetInt32(2);
				}
				while(rs.Read());
			}
			rs.Close();
			lastVal = 100*tot/max;
			lastCount = minCnt;
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			int cx = 0, type = 0, q = 0, o = 0, rac = 0, pl = 0, langID = 1;
			Graph g;

			int steps = 0, GB = (HttpContext.Current.Request.QueryString["GB"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["GB"].ToString()) : 0);
			string groupBy = "";

			OdbcDataReader rs = Db.recordSet("SELECT " +
				"rp.Type, " +
				"(" +
				"SELECT COUNT(*) " +
				"FROM ReportPartComponent rpc " +
				"WHERE rpc.ReportPartID = rp.ReportPartID" +
				"), " +
				"rp.QuestionID, " +
				"rp.OptionID, " +
				"rp.RequiredAnswerCount, " +
				"rp.PartLevel " +
				"FROM ReportPart rp " +
				"WHERE rp.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"]);
			if(rs.Read())
			{
				type = rs.GetInt32(0);
				cx = rs.GetInt32(1);
				q = (rs.IsDBNull(2) ? 0 : rs.GetInt32(2));
				o = (rs.IsDBNull(3) ? 0 : rs.GetInt32(3));
				rac = (rs.IsDBNull(4) ? 0 : rs.GetInt32(4));
				pl = (rs.IsDBNull(5) ? 0 : rs.GetInt32(5));
			}
			rs.Close();
			
			if(HttpContext.Current.Request.QueryString["UID"] != null)
			{
				#region User-level
				if(type == 2)
				{
					g = new Graph(895,550,"#FFFFFF");
					g.setMinMax(0f,100f);

					cx += 2;

					steps = cx;
					g.computeSteping(steps);
					g.drawOutlines(11);
					g.drawAxis();

					cx = 0;

					#region Index
					rs = Db.recordSet("SELECT " +
						"rpc.IdxID, " +
						"(SELECT COUNT(*) FROM IdxPart ip WHERE ip.IdxID = rpc.IdxID AND ip.OtherIdxID IS NOT NULL), " +
						"i.TargetVal, " +
						"i.YellowLow, " +
						"i.GreenLow, " +
						"i.GreenHigh, " +
						"i.YellowHigh " +
						"FROM ReportPartComponent rpc " +
						"INNER JOIN Idx i ON rpc.IdxID = i.IdxID " +
						"WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
						"ORDER BY rpc.SortOrder");
					while(rs.Read())
					{
						cx++;

						if(rs.GetInt32(1) == 0)
						{
							string vals = "";
							string dts = "";
							string desc = "";

							string query = "SELECT " +
								"100*CAST(SUM(ipc.Val*ip.Multiple) AS REAL)/i.MaxVal AS AX, " +
								"il.Idx, " +
								"a.EndDT, " +
								"pr.Internal " +
								"FROM Idx i " +
								"INNER JOIN IdxLang il ON i.IdxID = il.IdxID AND il.LangID = " + langID + " " +
								"INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID " +
								"INNER JOIN IdxPartComponent ipc ON ip.IdxPartID = ipc.IdxPartID " +
								"INNER JOIN AnswerValue av ON ip.QuestionID = av.QuestionID AND ip.OptionID = av.OptionID AND av.ValueInt = ipc.OptionComponentID AND av.DeletedSessionID IS NULL " +
								"INNER JOIN Answer a ON av.AnswerID = a.AnswerID " +
								"INNER JOIN [User] u ON u.UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]) + " " +
								"INNER JOIN UserProjectRoundUser uu ON u.UserID = uu.UserID AND a.ProjectRoundUserID = uu.ProjectRoundUserID " +
								"INNER JOIN ProjectRound pr ON a.ProjectRoundID = pr.ProjectRoundID " +
								"WHERE a.EndDT IS NOT NULL AND i.IdxID = " + rs.GetInt32(0) + " " +
								"GROUP BY i.IdxID, i.MaxVal, il.Idx, a.AnswerID, a.EndDT, pr.Internal";
							OdbcDataReader rs2 = Db.recordSet(query);
							while(rs2.Read())
							{
								if(!rs2.IsDBNull(0))
								{
									vals += (vals != "" ? ";" : "") + ((float)Convert.ToDouble(rs2.GetValue(0))).ToString();
									desc = rs2.GetString(1);
									dts += (dts != "" ? ";" : "") + rs2.GetString(3) + "\n" + rs2.GetDateTime(2).ToString("yyyy-MM-dd");
								}
							}
							rs2.Close();

							if(vals != "")
							{
								string[] val = vals.Split(';');
								if(val.Length > 0)
								{
									int color = 2;
									for(int i=0;i<val.Length;i++)
									{
										float f = ((float)Convert.ToDouble(val[i]));
										if(!rs.IsDBNull(3) && rs.GetInt32(3) >= 0 && rs.GetInt32(3) <= 100 && f >= rs.GetInt32(3))
											color = 1;
										if(!rs.IsDBNull(4) && rs.GetInt32(4) >= 0 && rs.GetInt32(4) <= 100 && f >= rs.GetInt32(4))
											color = 0;
										if(!rs.IsDBNull(5) && rs.GetInt32(5) >= 0 && rs.GetInt32(5) <= 100 && f >= rs.GetInt32(5))
											color = 1;
										if(!rs.IsDBNull(6) && rs.GetInt32(6) >= 0 && rs.GetInt32(6) <= 100 && f >= rs.GetInt32(6))
											color = 2;
										g.drawMultiBar(color,cx,f,val.Length,i,dts.Split(';')[i]);
									}
								}
								g.drawBottomString(desc,cx,false);
							}
						}
						else
						{
							// Index of indexes, not implemented
						}
				
						if(!rs.IsDBNull(3) && rs.GetInt32(3) > 0 && rs.GetInt32(3) < 100)
							g.drawDiamond(cx,rs.GetInt32(3),1,2);
						if(!rs.IsDBNull(4) && rs.GetInt32(4) > 0 && rs.GetInt32(4) < 100)
							g.drawDiamond(cx,rs.GetInt32(4),0,1);
						if(!rs.IsDBNull(5) && rs.GetInt32(5) > 0 && rs.GetInt32(5) < 100)
							g.drawDiamond(cx,rs.GetInt32(5),1,0);
						if(!rs.IsDBNull(6) && rs.GetInt32(6) > 0 && rs.GetInt32(6) < 100)
							g.drawDiamond(cx,rs.GetInt32(6),2,1);
						//if(!rs.IsDBNull(2) && rs.GetInt32(2) >= 0 && rs.GetInt32(2) <= 100)
						//	g.drawReference(cx,rs.GetInt32(2));
					}
					rs.Close();

					//g.drawAxisExpl("Poäng", 0, false, false);
					//g.drawReference(780,25," = riktvärde");
					#endregion

				}
				else if(type == 9)
				{
					g = new Graph(895,550,"#FFFFFF");
					g.setMinMax(0f,100f);

					g.computeSteping(cx+2);
					g.drawOutlines(11);
					g.drawAxis();

					cx = 0;

					#region Weighted question / Bars
					bool hasReference = false;

					rs = Db.recordSet("SELECT " +
						"rpc.WeightedQuestionOptionID, " +	// 0
						"wqol.WeightedQuestionOption, " +
						"wqo.TargetVal, " +
						"wqo.YellowLow, " +
						"wqo.GreenLow, " +
						"wqo.GreenHigh, " +					// 5
						"wqo.YellowHigh, " +
						"wqo.QuestionID, " +
						"wqo.OptionID " +
						"FROM ReportPartComponent rpc " +
						"INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
						"INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + langID + " " +
						"WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
						"ORDER BY rpc.SortOrder");
					while(rs.Read())
					{
						cx++;

						string vals = "";
						string dts = "";

						OdbcDataReader rs2 = Db.recordSet("SELECT " +
							"av.ValueInt, " +
							"NULL, " +
							"a.EndDT, " +
							"pr.Internal " +
							"FROM AnswerValue av " +
							"INNER JOIN Answer a ON av.AnswerID = a.AnswerID " +
							"INNER JOIN [User] u ON u.UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]) + " " +
							"INNER JOIN UserProjectRoundUser uu ON u.UserID = uu.UserID AND a.ProjectRoundUserID = uu.ProjectRoundUserID " +
							"INNER JOIN ProjectRound pr ON a.ProjectRoundID = pr.ProjectRoundID " +
							"WHERE a.EndDT IS NOT NULL AND av.DeletedSessionID IS NULL " +
							"AND av.QuestionID = " + rs.GetInt32(7) + " " +
							"AND av.OptionID = " + rs.GetInt32(8));
						while(rs2.Read())
						{
							if(!rs2.IsDBNull(0))
							{
								vals += (vals != "" ? ";" : "") + ((float)Convert.ToDouble(rs2.GetValue(0))).ToString();
								dts += (dts != "" ? ";" : "") + rs2.GetString(3) + "\n" + rs2.GetDateTime(2).ToString("yyyy-MM-dd");
							}
						}
						rs2.Close();

						if(vals != "")
						{
							string[] val = vals.Split(';');
							if(val.Length > 0)
							{
								int color = 2;
								for(int i=0;i<val.Length;i++)
								{
									float f = ((float)Convert.ToDouble(val[i]));
									if(!rs.IsDBNull(3) && rs.GetInt32(3) >= 0 && rs.GetInt32(3) <= 100 && f >= rs.GetInt32(3))
										color = 1;
									if(!rs.IsDBNull(4) && rs.GetInt32(4) >= 0 && rs.GetInt32(4) <= 100 && f >= rs.GetInt32(4))
										color = 0;
									if(!rs.IsDBNull(5) && rs.GetInt32(5) >= 0 && rs.GetInt32(5) <= 100 && f >= rs.GetInt32(5))
										color = 1;
									if(!rs.IsDBNull(6) && rs.GetInt32(6) >= 0 && rs.GetInt32(6) <= 100 && f >= rs.GetInt32(6))
										color = 2;
									g.drawMultiBar(color,cx,f,val.Length,i,dts.Split(';')[i]);
								}
							}
							g.drawBottomString(rs.GetString(1),cx,false);
						}
					}
					rs.Close();

					if(hasReference)
					{
						g.drawReference(450,25," = riktvärde");
					}
					
					#endregion
				}
				else
				{
					g = new Graph(895,550,"#FFFFFF");
				}
				#endregion
			}
			else if(HttpContext.Current.Request.QueryString["AK"] != null)
			{
				#region Answer-level

				int answerID = 0;
				int projectRoundUserID = 0;
				rs = Db.recordSet("SELECT " +
					"a.AnswerID, " +
					"dbo.cf_unitLangID(a.ProjectRoundUnitID), " +
					"a.ProjectRoundUserID " +
					"FROM Answer a " +
					"WHERE REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-','') = '" + HttpContext.Current.Request.QueryString["AK"] + "'");
				if(rs.Read())
				{
					answerID = rs.GetInt32(0);
					langID = rs.GetInt32(1);
					projectRoundUserID = (rs.IsDBNull(2) ? 0 : rs.GetInt32(2));
				}
				rs.Close();

				if(HttpContext.Current.Request.QueryString["W"] != null && HttpContext.Current.Request.QueryString["H"] != null && HttpContext.Current.Request.QueryString["BG"] != null)
				{
					g = new Graph(Convert.ToInt32(HttpContext.Current.Request.QueryString["W"]),Convert.ToInt32(HttpContext.Current.Request.QueryString["H"]),"#" + HttpContext.Current.Request.QueryString["BG"]);
				}
				else
				{
					g = new Graph(550,440,"#EFEFEF");
				}
				g.setMinMax(0f,100f);
				
				if(type == 8)
				{
					int dx = 0;
					rs = Db.recordSet("SELECT COUNT(DISTINCT dbo.cf_yearMonthDay(a.EndDT)) FROM Answer a WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = " + projectRoundUserID);
					if(rs.Read())
					{
						dx = Convert.ToInt32(rs.GetValue(0));
						if(dx == 1)
						{
							type = 9;
						}
						else
						{
							cx = dx;
						}
					}
					rs.Close();
				}
				if(type == 8)
				{
					g.computeSteping(cx);
					g.drawOutlines(11);

					int bx = 0;
					rs = Db.recordSet("SELECT " +
						"rpc.WeightedQuestionOptionID, " +	// 0
						"wqol.WeightedQuestionOption, " +
						"wqo.QuestionID, " +
						"wqo.OptionID " +
						"FROM ReportPartComponent rpc " +
						"INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
						"INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + langID + " " +
						"WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
						"ORDER BY rpc.SortOrder");
					while(rs.Read() && bx <= 1)
					{
						if(bx == 0)
						{
							g.drawAxisExpl(rs.GetString(1),bx+4,false,true);
							g.drawAxis(false);
						}
						else
						{
							g.drawAxisExpl(rs.GetString(1),bx+4,true,true);
							g.drawAxis(true);
						}
						float lastVal = -1f;
						int lastCX = 0;
						cx = 0;
						OdbcDataReader rs2 = Db.recordSet("SELECT " +
							"dbo.cf_yearMonthDay(a.EndDT), " +
							"AVG(av.ValueInt) " +
							"FROM Answer a " +
							"LEFT OUTER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = " + rs.GetInt32(2) + " AND av.OptionID = " + rs.GetInt32(3) + " " +
							"WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = " + projectRoundUserID + " " +
							"GROUP BY dbo.cf_yearMonthDay(a.EndDT) " + 
							"ORDER BY dbo.cf_yearMonthDay(a.EndDT)");
						while(rs2.Read())
						{
							if(bx==0)
							{
								g.drawBottomString(rs2.GetString(0),cx);
							}
							float newVal = (rs2.IsDBNull(1) ? -1f : (float)Convert.ToDouble(rs2.GetValue(1)));
							if(lastVal != -1f && newVal != -1f)
							{
								g.drawStepLine(bx+4,lastCX,lastVal,cx,newVal);
								lastCX = cx;
							}
							cx++;
							lastVal = newVal;
						}
						rs2.Close();

						bx++;
					}
					rs.Close();
				}
				else if(type == 9)
				{
					#region Bars
					g.computeSteping(cx+2);
					g.drawOutlines(11);
					g.drawAxis();

					cx = 0;

					bool hasReference = false;

					rs = Db.recordSet("SELECT " +
						"rpc.WeightedQuestionOptionID, " +	// 0
						"wqol.WeightedQuestionOption, " +
						"wqo.TargetVal, " +
						"wqo.YellowLow, " +
						"wqo.GreenLow, " +
						"wqo.GreenHigh, " +					// 5
						"wqo.YellowHigh, " +
						"wqo.QuestionID, " +
						"wqo.OptionID " +
						"FROM ReportPartComponent rpc " +
						"INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
						"INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + langID + " " +
						"WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
						"ORDER BY rpc.SortOrder");
					while(rs.Read())
					{
						OdbcDataReader rs2 = Db.recordSet("SELECT " +
							"av.ValueInt " +
							"FROM AnswerValue av " +
							"WHERE av.DeletedSessionID IS NULL " +
							"AND av.AnswerID = " + answerID + " " +
							"AND av.QuestionID = " + rs.GetInt32(7) + " " +
							"AND av.OptionID = " + rs.GetInt32(8));
						if(rs2.Read())
						{
							int color = 2;
							if(!rs.IsDBNull(3) && rs.GetInt32(3) >= 0 && rs.GetInt32(3) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(3))
								color = 1;
							if(!rs.IsDBNull(4) && rs.GetInt32(4) >= 0 && rs.GetInt32(4) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(4))
								color = 0;
							if(!rs.IsDBNull(5) && rs.GetInt32(5) >= 0 && rs.GetInt32(5) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(5))
								color = 1;
							if(!rs.IsDBNull(6) && rs.GetInt32(6) >= 0 && rs.GetInt32(6) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(6))
								color = 2;

							g.drawBar(color,++cx,rs2.GetInt32(0));
							if(!rs.IsDBNull(2))
							{
								hasReference = true;
								g.drawReference(cx,rs.GetInt32(2));
							}
							g.drawBottomString(rs.GetString(1),cx,true);
						}
						rs2.Close();
					}
					rs.Close();

					//g.drawAxisExpl("poäng", 0, false, false);

					if(hasReference)
					{
						g.drawReference(450,25," = riktvärde");
					}

					g.drawColorExplBox("Hälsosam nivå", 0, 100, 30);
					g.drawColorExplBox("Förbättringsbehov", 1, 250, 30);
					g.drawColorExplBox("Ohälsosam nivå", 2, 400, 30);
					
					#endregion
				}
				#endregion
			}
			else
			{
				#region group stats

				string sortString = "";
				rs = Db.recordSet("SELECT SortString, dbo.cf_unitLangID(ProjectRoundUnitID) FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + HttpContext.Current.Request.QueryString["PRUID"]);
				if(rs.Read())
				{
					sortString = rs.GetString(0);
					langID = rs.GetInt32(1);
				}
				rs.Close();

				if(type == 1)
				{
					g = new Graph(895,550,"#FFFFFF");
					g.setMinMax(0f,100f);

					rs = Db.recordSet("SELECT COUNT(*) FROM OptionComponents WHERE OptionID = " + o);
					if(rs.Read())
					{
						cx = rs.GetInt32(0)+1+2;
					}
					rs.Close();
				}
				else if(type == 3)
				{
					g = new Graph(895,550,"#FFFFFF");
					g.setMinMax(0f,100f);

					rs = Db.recordSet("SELECT COUNT(*) FROM ProjectRoundUnit pru " +
						"WHERE LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'");
					if(rs.Read())
					{
						cx = rs.GetInt32(0)+2;
					}
					rs.Close();
				}
				else if(type == 8)
				{
					if(GB == 0)
					{
						GB = 2;
					}
					switch(GB)
					{
						case 1:	groupBy = "dbo.cf_yearWeek"; break;
						case 2: groupBy = "dbo.cf_year2Week"; break;
						case 3: groupBy = "dbo.cf_yearMonth"; break;
						case 4: groupBy = "dbo.cf_year3Month"; break;
						case 5: groupBy = "dbo.cf_year6Month"; break;
						case 6: groupBy = "YEAR"; break;
						case 7: groupBy = "dbo.cf_year2WeekEven"; break;
					}
					g = new Graph(895,440,"#FFFFFF");

					rs = Db.recordSet("SELECT " +
						"" + groupBy + "(MAX(a.EndDT)) - " + groupBy + "(MIN(a.EndDT))" +
						"FROM Answer a " +
						"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
						"INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
						"WHERE a.EndDT IS NOT NULL " +
						"AND a.EndDT >= pr.Started " +
						"AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'");
					if(rs.Read())
					{
						cx = Convert.ToInt32(rs.GetValue(0))+3;
					}
					rs.Close();

					rs = Db.recordSet("SELECT " +
						"rpc.WeightedQuestionOptionID, " +	// 0
						"wqo.QuestionID, " +
						"wqo.OptionID " +
						"FROM ReportPartComponent rpc " +
						"INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
						"WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
						"ORDER BY rpc.SortOrder");
					while(rs.Read())
					{
						OdbcDataReader rs2 = Db.recordSet("SELECT " +
							"MAX(tmp2.VA + tmp2.SD), " +
							"MIN(tmp2.VA - tmp2.SD) " +
							"FROM (" +
							"SELECT " +
							"AVG(tmp.V) AS VA, " +
							"STDEV(tmp.V) AS SD " +
							"FROM (" +
							"SELECT " +
							"" + groupBy + "(a.EndDT) AS DT, " +
							"AVG(av.ValueInt) AS V " +
							"FROM Answer a " +
							"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = " + rs.GetInt32(1) + " AND av.OptionID = " + rs.GetInt32(2) + " " +
							"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
							"INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
							"WHERE a.EndDT IS NOT NULL " +
							"AND a.EndDT >= pr.Started " +
							"AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "' " +
							"GROUP BY a.ProjectRoundUserID, " + groupBy + "(a.EndDT) " + 
							") tmp " +
							"GROUP BY tmp.DT " + 
							") tmp2");
						if(rs2.Read())
						{
							g.setMinMax((float)Convert.ToDouble(rs2.GetValue(1)),(float)Convert.ToDouble(rs2.GetValue(0)));
							//g.roundMinMax();
							g.computeMinMax(0.1f,0.1f);
						}
						else
						{
							g.setMinMax(0f,100f);
						}
						rs2.Close();
					}
					rs.Close();

					if(g.minVal != 0f)
					{
						g.drawLine(20, 0, (int)g.maxH+20, 0, (int)g.maxH+23,1);
						g.drawLine(20, 0, (int)g.maxH+23, -5, (int)g.maxH+26,1);
						g.drawLine(20, -5, (int)g.maxH+26, 5, (int)g.maxH+32,1);
						g.drawLine(20, 5, (int)g.maxH+32, 0, (int)g.maxH+35,1);
						g.drawLine(20, 0, (int)g.maxH+35, 0, (int)g.maxH+38,1);
					}
				}
				else
				{
					g = new Graph(895,550,"#FFFFFF");
					g.setMinMax(0f,100f);

					cx += 2;
				}

				steps = cx;
				g.computeSteping(steps);
				g.drawOutlines(11);
				g.drawAxis();

				cx = 0;
			
				if(type == 1)
				{
					#region Likert

					decimal tot = 0;
					decimal sum = 0;

					rs = Db.recordSet("SELECT COUNT(*) FROM Answer a " +
						"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
						"WHERE a.EndDT IS NOT NULL AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'");
					if(rs.Read())
					{
						tot = Convert.ToDecimal(rs.GetInt32(0));
					}
					rs.Close();

					if(rac > Convert.ToInt32(tot))
					{
						g = new Graph(895,50,"#FFFFFF");
						g.drawStringInGraph("Ingen återkoppling pga otillräckligt underlag",300,-30);
					}
					else
					{
						g.drawAxisExpl("% (n = " + tot + ")", 5, false, false);

						rs = Db.recordSet("SELECT oc.OptionComponentID, ocl.Text FROM OptionComponents ocs " +
							"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
							"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = 1 " +
							"WHERE ocs.OptionID = " + o + " ORDER BY ocs.SortOrder");
						while(rs.Read())
						{
							cx++;

							OdbcDataReader rs2 = Db.recordSet("SELECT COUNT(*) FROM Answer a " + 
								"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
								"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
								"WHERE a.EndDT IS NOT NULL AND av.ValueInt = " + rs.GetInt32(0) + " " +
								"AND av.OptionID = " + o + " " +
								"AND av.QuestionID = " + q + " " +
								"AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'");
							if(rs2.Read())
							{
								sum += Convert.ToDecimal(rs2.GetInt32(0));
								g.drawBar(5,cx,Convert.ToInt32(Math.Round(Convert.ToDecimal(rs2.GetInt32(0))/tot*100M,0)));
							}
							rs2.Close();
							g.drawBottomString(rs.GetString(1),cx,true);
						}
						rs.Close();

						g.drawBar(4,++cx,Convert.ToInt32(Math.Round((tot-sum)/tot*100M,0)));
						g.drawBottomString("Inget svar",cx,true);
					}
					#endregion
				}
				else if(type == 3)
				{
					#region Benchmark
					rs = Db.recordSet("SELECT " +
						"rpc.IdxID, " +
						"(SELECT COUNT(*) FROM IdxPart ip WHERE ip.IdxID = rpc.IdxID AND ip.OtherIdxID IS NOT NULL), " +
						"i.TargetVal, " +
						"i.YellowLow, " +
						"i.GreenLow, " +
						"i.GreenHigh, " +
						"i.YellowHigh " +
						"FROM ReportPartComponent rpc " +
						"INNER JOIN Idx i ON rpc.IdxID = i.IdxID " +
						"WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
						"ORDER BY rpc.SortOrder");
					while(rs.Read())
					{
						System.Collections.SortedList all = new System.Collections.SortedList();

						OdbcDataReader rs2 = Db.recordSet("SELECT dbo.cf_projectUnitTree(pru.ProjectRoundUnitID,' » '), SortString FROM ProjectRoundUnit pru " +
							"WHERE LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'");
						while(rs2.Read())
						{
							res = new System.Collections.Hashtable();

							if(rs.GetInt32(1) == 0)
							{
								getIdxVal(rs.GetInt32(0),rs2.GetString(1));
							}
							else
							{
								getOtherIdxVal(rs.GetInt32(0),rs2.GetString(1));
							}

							if(all.Contains(lastVal))
							{
								all[lastVal] += "," + rs2.GetString(0);
							}
							else
							{
								all.Add(lastVal,rs2.GetString(0));
							}
						}
						rs2.Close();

						for (int i=all.Count-1; i>=0; i--)
						{
							int color = 2;
							if(!rs.IsDBNull(3) && rs.GetInt32(3) >= 0 && rs.GetInt32(3) <= 100 && Convert.ToInt32(all.GetKey(i)) >= rs.GetInt32(3))
								color = 1;
							if(!rs.IsDBNull(4) && rs.GetInt32(4) >= 0 && rs.GetInt32(4) <= 100 && Convert.ToInt32(all.GetKey(i)) >= rs.GetInt32(4))
								color = 0;
							if(!rs.IsDBNull(5) && rs.GetInt32(5) >= 0 && rs.GetInt32(5) <= 100 && Convert.ToInt32(all.GetKey(i)) >= rs.GetInt32(5))
								color = 1;
							if(!rs.IsDBNull(6) && rs.GetInt32(6) >= 0 && rs.GetInt32(6) <= 100 && Convert.ToInt32(all.GetKey(i)) >= rs.GetInt32(6))
								color = 2;

							string[] u = all.GetByIndex(i).ToString().Split(',');

							foreach(string s in u)
							{
								g.drawBar(color,++cx,Convert.ToInt32(all.GetKey(i)));
								//g.drawReference(cx,rs.GetInt32(2));
								g.drawBottomString(s,cx,true);
							}
						}

						g.drawReferenceLine(rs.GetInt32(2)," = riktvärde");
					}
					rs.Close();

					g.drawAxisExpl("poäng", 0, false, false);
				
					//g.drawReferenceLineExpl(770,25," = riktvärde");
					#endregion
				}
				else if(type == 2)
				{
					#region Index
					rs = Db.recordSet("SELECT " +
						"rpc.IdxID, " +
						"(SELECT COUNT(*) FROM IdxPart ip WHERE ip.IdxID = rpc.IdxID AND ip.OtherIdxID IS NOT NULL), " +
						"i.TargetVal, " +
						"i.YellowLow, " +
						"i.GreenLow, " +
						"i.GreenHigh, " +
						"i.YellowHigh " +
						"FROM ReportPartComponent rpc " +
						"INNER JOIN Idx i ON rpc.IdxID = i.IdxID " +
						"WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
						"ORDER BY rpc.SortOrder");
					while(rs.Read())
					{
						if(rs.GetInt32(1) == 0)
						{
							getIdxVal(rs.GetInt32(0),sortString);
						}
						else
						{
							getOtherIdxVal(rs.GetInt32(0),sortString);
						}
						int color = 2;
						if(!rs.IsDBNull(3) && rs.GetInt32(3) >= 0 && rs.GetInt32(3) <= 100 && lastVal >= rs.GetInt32(3))
							color = 1;
						if(!rs.IsDBNull(4) && rs.GetInt32(4) >= 0 && rs.GetInt32(4) <= 100 && lastVal >= rs.GetInt32(4))
							color = 0;
						if(!rs.IsDBNull(5) && rs.GetInt32(5) >= 0 && rs.GetInt32(5) <= 100 && lastVal >= rs.GetInt32(5))
							color = 1;
						if(!rs.IsDBNull(6) && rs.GetInt32(6) >= 0 && rs.GetInt32(6) <= 100 && lastVal >= rs.GetInt32(6))
							color = 2;
						g.drawBar(color,++cx,lastVal);
				
						if(!rs.IsDBNull(2) && rs.GetInt32(2) >= 0 && rs.GetInt32(2) <= 100)
							g.drawReference(cx,rs.GetInt32(2));
				
						g.drawBottomString(lastDesc,cx,true);
					}
					rs.Close();

					g.drawAxisExpl("poäng", 0, false, false);

					g.drawReference(780,25," = riktvärde");
					#endregion
				}
				else if(type == 8)
				{
					#region Weighted question over time
					if(HttpContext.Current.Request.QueryString["TRID"] != null)
					{
						int COUNT = 0;
						OdbcDataReader rs3 = Db.recordSet("SELECT COUNT(*) FROM TempReportComponent WHERE TempReportID = " + HttpContext.Current.Request.QueryString["TRID"]);
						if(rs3.Read())
						{
							COUNT = rs3.GetInt32(0);
						}
						rs3.Close();
						rs = Db.recordSet("SELECT " +
							"rpc.WeightedQuestionOptionID, " +	// 0
							"wqol.WeightedQuestionOption, " +
							"wqo.QuestionID, " +
							"wqo.OptionID " +
							"FROM ReportPartComponent rpc " +
							"INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
							"INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + langID + " " +
							"WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
							"ORDER BY rpc.SortOrder");
						if(rs.Read())
						{
							int bx = 0;

							rs3 = Db.recordSet("SELECT TempReportComponentID, TempReportComponent FROM TempReportComponent WHERE TempReportID = " + HttpContext.Current.Request.QueryString["TRID"]);
							while(rs3.Read())
							{
								if(bx == 0)
								{
									g.drawAxis(false);
									g.drawAxisExpl((langID == 1 ? "Medelvärde" : "Mean value") + " " + HttpUtility.HtmlDecode("&plusmn;") + "SD",0,false,false);
								}
								g.drawColorExplBox(rs3.GetString(1),bx+4,130+(int)((bx%6)*120),15+(int)Math.Ceiling(bx/6.0)*15);
								float lastVal = -1f;
								float lastStd = -1f;
								int lastCX = 1;
								cx = 1;
								int lastDT = 0;
								#region Data loop
								OdbcDataReader rs2 = Db.recordSet("SELECT " +
									"tmp.DT, " +
									"AVG(tmp.V), " +
									"COUNT(tmp.V), " +
									"STDEV(tmp.V) " +
									"FROM (" +
									"SELECT " +
									"" + groupBy + "(a.EndDT) AS DT, " +
									"AVG(av.ValueInt) AS V " +
									"FROM Answer a " +
									"INNER JOIN TempReportComponentAnswer trca ON a.AnswerID = trca.AnswerID AND trca.TempReportComponentID = " + rs3.GetInt32(0) + " " +
									"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = " + rs.GetInt32(2) + " AND av.OptionID = " + rs.GetInt32(3) + " " +
									"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
									"INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
									"WHERE a.EndDT IS NOT NULL " +
									"AND a.EndDT >= pr.Started " +
									"AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "' " +
									"GROUP BY a.ProjectRoundUserID, " + groupBy + "(a.EndDT) " + 
									") tmp " +
									"GROUP BY tmp.DT " + 
									"ORDER BY tmp.DT");
								while(rs2.Read())
								{
									if(lastDT == 0)
										lastDT = rs2.GetInt32(0);

									while(lastDT+1 < rs2.GetInt32(0))
									{
										lastDT++;
										cx++;
									}

									if(rs2.GetInt32(2) >= rac)
									{
										switch(GB)
										{
											case 1:
											{
												int d = rs2.GetInt32(0);
												int w = d%52;
												if(w == 0)
												{
													w = 52;
												}
												string v = "v" + w + ", " + (d/52) + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
												g.drawBottomString(v,cx,true);
												break;
											}
											case 2:
											{
												int d = rs2.GetInt32(0)*2;
												int w = d%52;
												if(w == 0)
												{
													w = 52;
												}
												//string v = "v" + (w-1) + "-" + w + "\n" + (d-(d-1)%52)/52;
												string v = "v" + (w-1) + "-" + w + ", " + (d-((d-1)%52))/52 + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
												g.drawBottomString(v,cx,true);
												break;
											}
											case 3:
											{
												int d = rs2.GetInt32(0);
												int w = d%12;
												if(w == 0)
												{
													w = 12;
												}
												string v = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[w-1] + ", " + ((d-w)/12) + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
												g.drawBottomString(v,cx,true);
												break;
											}
											case 4:
											{
												int d = rs2.GetInt32(0)*3;
												int w = d%12;
												if(w == 0)
												{
													w = 12;
												}
												string v = "Q" + (w/3) + ", " + ((d-w)/12) + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
												g.drawBottomString(v,cx,true);
												break;
											}
											case 5:
											{
												int d = rs2.GetInt32(0)*6;
												int w = d%12;
												if(w == 0)
												{
													w = 12;
												}
												string v = ((d-w)/12) + "/" + (w/6) + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
												g.drawBottomString(v,cx,true);
												break;
											}
											case 6:
											{
												g.drawBottomString(rs2.GetInt32(0).ToString() + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : ""),cx,true);
												break;
											}
											case 7:
											{
												int d = rs2.GetInt32(0)*2;
												int w = d%52;
												if(w == 0)
												{
													w = 52;
												}
												//string v = "v" + (w-1) + "-" + w + "\n" + (d-(d-1)%52)/52;
												string v = "v" + w + "-" + (w+1) + ", " + ((d+1)-(d%52))/52 + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
												g.drawBottomString(v,cx,true);
												break;
											}
										}

										float newVal = (rs2.IsDBNull(1) ? -1f : (float)Convert.ToDouble(rs2.GetValue(1)));
										float newStd = (rs2.IsDBNull(3) ? -1f : (float)Convert.ToDouble(rs2.GetValue(3)));

										g.drawLine(bx+4,cx*g.steping-10,Convert.ToInt32(g.maxH-((newVal-newStd)-g.minVal)/(g.maxVal-g.minVal)*g.maxH),cx*g.steping+10,Convert.ToInt32(g.maxH-((newVal-newStd)-g.minVal)/(g.maxVal-g.minVal)*g.maxH),1);
										g.drawLine(20,cx*g.steping,Convert.ToInt32(g.maxH-((newVal-newStd)-g.minVal)/(g.maxVal-g.minVal)*g.maxH),cx*g.steping,Convert.ToInt32(g.maxH-((newVal+newStd)-g.minVal)/(g.maxVal-g.minVal)*g.maxH),1);
										g.drawLine(bx+4,cx*g.steping-10,Convert.ToInt32(g.maxH-((newVal+newStd)-g.minVal)/(g.maxVal-g.minVal)*g.maxH),cx*g.steping+10,Convert.ToInt32(g.maxH-((newVal+newStd)-g.minVal)/(g.maxVal-g.minVal)*g.maxH),1);

										if(lastVal != -1f && newVal != -1f)
										{
											g.drawStepLine(bx+4,lastCX,lastVal,cx,newVal,1);
											lastCX = cx;
										}
										lastVal = newVal;
										lastStd = newStd;

										g.drawCircle(cx,newVal,bx+4);
									}
									lastDT = rs2.GetInt32(0);
									cx++;
								}
								rs2.Close();
								#endregion

								bx++;
							}
							rs3.Close();
						}
						rs.Close();
					}
					else
					{
						int bx = 0;
						rs = Db.recordSet("SELECT " +
							"rpc.WeightedQuestionOptionID, " +	// 0
							"wqol.WeightedQuestionOption, " +
							"wqo.QuestionID, " +
							"wqo.OptionID " +
							"FROM ReportPartComponent rpc " +
							"INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
							"INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + langID + " " +
							"WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
							"ORDER BY rpc.SortOrder");
						while(rs.Read() && bx <= 1)
						{
							if(bx == 0)
							{
								g.drawAxisExpl(rs.GetString(1) + ", " + (langID == 1 ? "medelvärde" : "mean value") + " " + HttpUtility.HtmlDecode("&plusmn;") + "SD",bx+4,false,true);
								g.drawAxis(false);
							}
							else
							{
								g.drawAxisExpl(rs.GetString(1) + ", " + (langID == 1 ? "medelvärde" : "mean value") + " " + HttpUtility.HtmlDecode("&plusmn;") + "SD",bx+4,true,true);
								g.drawAxis(true);
							}
							float lastVal = -1f;
							float lastStd = -1f;
							int lastCX = 1;
							cx = 1;
							int lastDT = 0;
							#region Data loop
							OdbcDataReader rs2 = Db.recordSet("SELECT " +
								"tmp.DT, " +
								"AVG(tmp.V), " +
								"COUNT(tmp.V), " +
								"STDEV(tmp.V) " +
								"FROM (" +
								"SELECT " +
								"" + groupBy + "(a.EndDT) AS DT, " +
								"AVG(av.ValueInt) AS V " +
								"FROM Answer a " +
								"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = " + rs.GetInt32(2) + " AND av.OptionID = " + rs.GetInt32(3) + " " +
								"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
								"WHERE a.EndDT IS NOT NULL " +
								"AND a.EndDT >= pr.Started " +
								"AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "' " +
								"GROUP BY a.ProjectRoundUserID, " + groupBy + "(a.EndDT) " + 
								") tmp " +
								"GROUP BY tmp.DT " + 
								"ORDER BY tmp.DT");
							while(rs2.Read())
							{
								if(lastDT == 0)
									lastDT = rs2.GetInt32(0);

								while(lastDT+1 < rs2.GetInt32(0))
								{
									lastDT++;
									cx++;
								}

								if(rs2.GetInt32(2) >= rac)
								{
									switch(GB)
									{
										case 1:
										{
											int d = rs2.GetInt32(0);
											int w = d%52;
											if(w == 0)
											{
												w = 52;
											}
											string v = "v" + w + ", " + (d/52) + ", n = " + rs2.GetInt32(2);
											g.drawBottomString(v,cx,true);
											break;
										}
										case 2:
										{
											int d = rs2.GetInt32(0)*2;
											int w = d%52;
											if(w == 0)
											{
												w = 52;
											}
											//string v = "v" + (w-1) + "-" + w + "\n" + (d-(d-1)%52)/52 + "\n\nn = " + rs2.GetInt32(2);
											string v = "v" + (w-1) + "-" + w + ", " + (d-((d-1)%52))/52 + ", n = " + rs2.GetInt32(2);
											g.drawBottomString(v,cx,true);
											break;
										}
										case 3:
										{
											int d = rs2.GetInt32(0);
											int w = d%12;
											if(w == 0)
											{
												w = 12;
											}
											string v = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[w-1] + ", " + ((d-w)/12) + ", n = " + rs2.GetInt32(2);
											g.drawBottomString(v,cx,true);
											break;
										}
										case 4:
										{
											int d = rs2.GetInt32(0)*3;
											int w = d%12;
											if(w == 0)
											{
												w = 12;
											}
											string v = "Q" + (w/3) + ", " + ((d-w)/12) + ", n = " + rs2.GetInt32(2);
											g.drawBottomString(v,cx,true);
											break;
										}
										case 5:
										{
											int d = rs2.GetInt32(0)*6;
											int w = d%12;
											if(w == 0)
											{
												w = 12;
											}
											string v = ((d-w)/12) + "/" + (w/6) + ", n = " + rs2.GetInt32(2);
											g.drawBottomString(v,cx,true);
											break;
										}
										case 6:
										{
											g.drawBottomString(rs2.GetInt32(0).ToString() + ", n = " + rs2.GetInt32(2),cx,true);
											break;
										}
										case 7:
										{
											int d = rs2.GetInt32(0)*2;
											int w = d%52;
											if(w == 0)
											{
												w = 52;
											}
											//string v = "v" + (w-1) + "-" + w + "\n" + (d-(d-1)%52)/52 + "\n\nn = " + rs2.GetInt32(2);
											string v = "v" + w + "-" + (w+1) + ", " + ((d+1)-(d%52))/52 + ", n = " + rs2.GetInt32(2);
											g.drawBottomString(v,cx,true);
											break;
										}
									}

									float newVal = (rs2.IsDBNull(1) ? -1f : (float)Convert.ToDouble(rs2.GetValue(1)));
									float newStd = (rs2.IsDBNull(3) ? -1f : (float)Convert.ToDouble(rs2.GetValue(3)));

									g.drawLine(20,cx*g.steping-10,Convert.ToInt32(g.maxH-((newVal-newStd)-g.minVal)/(g.maxVal-g.minVal)*g.maxH),cx*g.steping+10,Convert.ToInt32(g.maxH-((newVal-newStd)-g.minVal)/(g.maxVal-g.minVal)*g.maxH),1);
									g.drawLine(20,cx*g.steping,Convert.ToInt32(g.maxH-((newVal-newStd)-g.minVal)/(g.maxVal-g.minVal)*g.maxH),cx*g.steping,Convert.ToInt32(g.maxH-((newVal+newStd)-g.minVal)/(g.maxVal-g.minVal)*g.maxH),1);
									g.drawLine(20,cx*g.steping-10,Convert.ToInt32(g.maxH-((newVal+newStd)-g.minVal)/(g.maxVal-g.minVal)*g.maxH),cx*g.steping+10,Convert.ToInt32(g.maxH-((newVal+newStd)-g.minVal)/(g.maxVal-g.minVal)*g.maxH),1);

									if(lastVal != -1f && newVal != -1f)
									{
										g.drawStepLine(bx+4,lastCX,lastVal,cx,newVal,3);
										lastCX = cx;
									}
									lastVal = newVal;
									lastStd = newStd;

									g.drawCircle(cx,newVal);
								}
								lastDT = rs2.GetInt32(0);
								cx++;
							}
							rs2.Close();
							#endregion

							bx++;
						}
						rs.Close();
					}
					#endregion
				}

				#endregion
			}

			// g.printCopyRight();
			g.render();
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
