using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HWgrp___Old
{
	public partial class reportImage : System.Web.UI.Page
	{
		int lastCount = 0;
		float lastVal = 0;
		string lastDesc = "";
		System.Collections.Hashtable res = new System.Collections.Hashtable();
		System.Collections.Hashtable cnt = new System.Collections.Hashtable();

		private void getIdxVal(int idx, string sortString, int langID)
		{
			SqlDataReader rs = Db.rs("SELECT " +
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
				"INNER JOIN IdxLang il ON i.IdxID = il.IdxID AND il.LangID = " + langID + " " +
				"INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID " +
				"INNER JOIN IdxPartComponent ipc ON ip.IdxPartID = ipc.IdxPartID " +
				"INNER JOIN AnswerValue av ON ip.QuestionID = av.QuestionID AND ip.OptionID = av.OptionID AND av.ValueInt = ipc.OptionComponentID " +
				"INNER JOIN Answer a ON av.AnswerID = a.AnswerID " +
				"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
				"WHERE a.EndDT IS NOT NULL AND i.IdxID = " + idx + " AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "' " +
				(HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
				(HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
				"GROUP BY i.IdxID, a.AnswerID, i.MaxVal, il.Idx, i.CX, i.AllPartsRequired" +
				") tmp " +
				"WHERE tmp.AllPartsRequired = 0 OR tmp.CX = tmp.BX " +
				"GROUP BY tmp.IdxID, tmp.Idx", "eFormSqlConnection");
			while (rs.Read())
			{
				lastCount = rs.GetInt32(3);
				lastVal = (float)Convert.ToDouble(rs.GetValue(0));
				lastDesc = rs.GetString(1);

				if (!res.Contains(rs.GetInt32(2)))
					res.Add(rs.GetInt32(2), lastVal);

				if (!cnt.Contains(rs.GetInt32(2)))
					cnt.Add(rs.GetInt32(2), lastCount);
			}
			rs.Close();
		}

		private void getOtherIdxVal(int idx, string sortString, int langID)
		{
			float tot = 0;
			//int div = 0;
			int max = 0;
			int minCnt = Int32.MaxValue;
			SqlDataReader rs = Db.rs("SELECT " +
				"ip.OtherIdxID, " +
				"il.Idx, " +
				"i.MaxVal, " +
				"ip.Multiple " +
				"FROM Idx i " +
				"INNER JOIN IdxLang il ON i.IdxID = il.IdxID AND il.LangID = " + langID + " " +
				"INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID " +
				"WHERE i.IdxID = " + idx, "eFormSqlConnection");
			if (rs.Read())
			{
				lastDesc = rs.GetString(1);
				do
				{
					max += 100 * rs.GetInt32(3);
					if (res.Contains(rs.GetInt32(0)))
					{
						tot += (float)res[rs.GetInt32(0)] * rs.GetInt32(3);
						minCnt = Math.Min((int)cnt[rs.GetInt32(0)], minCnt);
					}
					else
					{
						getIdxVal(rs.GetInt32(0), sortString, langID);
						tot += lastVal * rs.GetInt32(3);
						minCnt = Math.Min(lastCount, minCnt);
					}
					//div = rs.GetInt32(2);
				}
				while (rs.Read());
			}
			rs.Close();
			lastVal = 100 * tot / max;
			lastCount = minCnt;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			int cx = 0, type = 0, q = 0, o = 0, rac = 0, pl = 0;
			Graph g;

			int steps = 0, GB = (HttpContext.Current.Request.QueryString["GB"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["GB"].ToString()) : 0);
			bool stdev = (HttpContext.Current.Request.QueryString["STDEV"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1 : false);
			string groupBy = "";

			SqlDataReader rs = Db.rs("SELECT " +
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
				"WHERE rp.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"], "eFormSqlConnection");
			if (rs.Read())
			{
				type = rs.GetInt32(0);
				cx = rs.GetInt32(1);
				q = (rs.IsDBNull(2) ? 0 : rs.GetInt32(2));
				o = (rs.IsDBNull(3) ? 0 : rs.GetInt32(3));
				rac = (rs.IsDBNull(4) ? 0 : rs.GetInt32(4));
				pl = (rs.IsDBNull(5) ? 0 : rs.GetInt32(5));
			}
			rs.Close();

			if (HttpContext.Current.Request.QueryString["AK"] != null)
			{
				#region User-level

				int answerID = 0;
				int langID = (HttpContext.Current.Request.QueryString["LangID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["LangID"]) : 0);
				int projectRoundUserID = 0;
				rs = Db.rs("SELECT " +
					"a.AnswerID, " +
					"dbo.cf_unitLangID(a.ProjectRoundUnitID), " +
					"a.ProjectRoundUserID " +
					"FROM Answer a " +
					"WHERE REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-','') = '" + HttpContext.Current.Request.QueryString["AK"] + "'", "eFormSqlConnection");
				if (rs.Read())
				{
					answerID = rs.GetInt32(0);
					if (langID == 0)
					{
						langID = rs.GetInt32(1);
					}
					projectRoundUserID = (rs.IsDBNull(2) ? 0 : rs.GetInt32(2));
				}
				rs.Close();

				switch (langID)
				{
					case 1: System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE"); break;
					case 2: System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); break;
				}

				if (HttpContext.Current.Request.QueryString["W"] != null && HttpContext.Current.Request.QueryString["H"] != null && HttpContext.Current.Request.QueryString["BG"] != null)
				{
					g = new Graph(Convert.ToInt32(HttpContext.Current.Request.QueryString["W"]), Convert.ToInt32(HttpContext.Current.Request.QueryString["H"]), "#" + HttpContext.Current.Request.QueryString["BG"]);
				}
				else
				{
					g = new Graph(550, 440, "#EFEFEF");
				}
				g.setMinMax(0f, 100f);

				if (type == 8)
				{
					int dx = 0;
					rs = Db.rs("SELECT COUNT(DISTINCT dbo.cf_yearMonthDay(a.EndDT)) " +
						"FROM Answer a " +
						"WHERE a.EndDT IS NOT NULL " +
						(HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
						(HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
						"AND a.ProjectRoundUserID = " + projectRoundUserID, "eFormSqlConnection");
					if (rs.Read())
					{
						dx = Convert.ToInt32(rs.GetValue(0));
						if (dx == 1)
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
				if (type == 8)
				{
					g.computeSteping(cx);
					g.drawOutlines(11);

					int bx = 0;
					rs = Db.rs("SELECT " +
						"rpc.WeightedQuestionOptionID, " +	// 0
						"wqol.WeightedQuestionOption, " +
						"wqo.QuestionID, " +
						"wqo.OptionID " +
						"FROM ReportPartComponent rpc " +
						"INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
						"INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + langID + " " +
						"WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
						"ORDER BY rpc.SortOrder", "eFormSqlConnection");
					while (rs.Read() && bx <= 1)
					{
						if (bx == 0)
						{
							g.drawAxisExpl(rs.GetString(1), bx + 4, false, true);
							g.drawAxis(false);
						}
						else
						{
							g.drawAxisExpl(rs.GetString(1), bx + 4, true, true);
							g.drawAxis(true);
						}
						float lastVal = -1f;
						int lastCX = 0;
						cx = 0;
						SqlDataReader rs2 = Db.rs("SELECT " +
							"dbo.cf_yearMonthDay(a.EndDT), " +
							"AVG(av.ValueInt) " +
							"FROM Answer a " +
							"LEFT OUTER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = " + rs.GetInt32(2) + " AND av.OptionID = " + rs.GetInt32(3) + " " +
							"WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = " + projectRoundUserID + " " +
							(HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
							(HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
							"GROUP BY dbo.cf_yearMonthDay(a.EndDT) " +
							"ORDER BY dbo.cf_yearMonthDay(a.EndDT)", "eFormSqlConnection");
						while (rs2.Read())
						{
							if (bx == 0)
							{
								g.drawBottomString(rs2.GetString(0), cx);
							}
							float newVal = (rs2.IsDBNull(1) ? -1f : (float)Convert.ToDouble(rs2.GetValue(1)));
							if (lastVal != -1f && newVal != -1f)
							{
								g.drawStepLine(bx + 4, lastCX, lastVal, cx, newVal);
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
				else if (type == 9)
				{
					#region Bars
					g.computeSteping(cx + 2);
					g.drawOutlines(11);
					g.drawAxis();

					cx = 0;

					bool hasReference = false;

					rs = Db.rs("SELECT " +
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
						"ORDER BY rpc.SortOrder", "eFormSqlConnection");
					while (rs.Read())
					{
						SqlDataReader rs2 = Db.rs("SELECT " +
							"av.ValueInt " +
							"FROM AnswerValue av " +
							"WHERE av.DeletedSessionID IS NULL " +
							"AND av.AnswerID = " + answerID + " " +
							"AND av.QuestionID = " + rs.GetInt32(7) + " " +
							"AND av.OptionID = " + rs.GetInt32(8), "eFormSqlConnection");
						if (rs2.Read())
						{
							int color = 2;
							if (!rs.IsDBNull(3) && rs.GetInt32(3) >= 0 && rs.GetInt32(3) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(3))
								color = 1;
							if (!rs.IsDBNull(4) && rs.GetInt32(4) >= 0 && rs.GetInt32(4) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(4))
								color = 0;
							if (!rs.IsDBNull(5) && rs.GetInt32(5) >= 0 && rs.GetInt32(5) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(5))
								color = 1;
							if (!rs.IsDBNull(6) && rs.GetInt32(6) >= 0 && rs.GetInt32(6) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(6))
								color = 2;

							g.drawBar(color, ++cx, rs2.GetInt32(0));
							if (!rs.IsDBNull(2))
							{
								hasReference = true;
								g.drawReference(cx, rs.GetInt32(2));
							}
							g.drawBottomString(rs.GetString(1), cx, true);
						}
						rs2.Close();
					}
					rs.Close();

					//g.drawAxisExpl("poäng", 0, false, false);

					if (hasReference)
					{
						g.drawReference(450, 25, " = riktvärde");
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
				int langID = (HttpContext.Current.Request.QueryString["LangID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["LangID"]) : 0), minDT = 0, maxDT = 0;
				rs = Db.rs("SELECT " +
					"SortString, " +
					"dbo.cf_unitLangID(ProjectRoundUnitID) " +
					"FROM ProjectRoundUnit " +
					"WHERE ProjectRoundUnitID = " + HttpContext.Current.Request.QueryString["PRUID"], "eFormSqlConnection");
				if (rs.Read())
				{
					sortString = rs.GetString(0);
					if (langID == 0)
					{
						langID = rs.GetInt32(1);
					}
				}
				rs.Close();

				switch (langID)
				{
					case 1: System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE"); break;
					case 2: System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); break;
				}

				if (type == 1)
				{
					g = new Graph(895, 550, "#FFFFFF");
					g.setMinMax(0f, 100f);

					rs = Db.rs("SELECT COUNT(*) FROM OptionComponents WHERE OptionID = " + o, "eFormSqlConnection");
					if (rs.Read())
					{
						cx = rs.GetInt32(0) + 1 + 2;
					}
					rs.Close();
				}
				else if (type == 3)
				{
					g = new Graph(895, 550, "#FFFFFF");
					g.setMinMax(0f, 100f);

					rs = Db.rs("SELECT COUNT(*) FROM ProjectRoundUnit pru " +
						"WHERE LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'", "eFormSqlConnection");
					if (rs.Read())
					{
						cx = rs.GetInt32(0) + 2;
					}
					rs.Close();
				}
				else if (type == 8)
				{
					if (GB == 0)
					{
						GB = 2;
					}
					switch (GB)
					{
						case 1: groupBy = "dbo.cf_yearWeek"; break;
						case 2: groupBy = "dbo.cf_year2Week"; break;
						case 3: groupBy = "dbo.cf_yearMonth"; break;
						case 4: groupBy = "dbo.cf_year3Month"; break;
						case 5: groupBy = "dbo.cf_year6Month"; break;
						case 6: groupBy = "YEAR"; break;
						case 7: groupBy = "dbo.cf_year2WeekEven"; break;
					}
					g = new Graph(895, 440, "#FFFFFF");

					rs = Db.rs("SELECT " +
						"" + groupBy + "(MAX(a.EndDT)) - " + groupBy + "(MIN(a.EndDT)), " +
						"" + groupBy + "(MIN(a.EndDT)), " +
						"" + groupBy + "(MAX(a.EndDT)) " +
						"FROM Answer a " +
						"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
						"INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
						"WHERE a.EndDT IS NOT NULL " +
						"AND a.EndDT >= pr.Started " +
						(HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
						(HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
						"AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'", "eFormSqlConnection");
					if (rs.Read() && !rs.IsDBNull(0))
					{
						cx = Convert.ToInt32(rs.GetValue(0)) + 3;
						minDT = rs.GetInt32(1);
						maxDT = rs.GetInt32(2);
					}
					rs.Close();

					rs = Db.rs("SELECT " +
						"rpc.WeightedQuestionOptionID, " +	// 0
						"wqo.QuestionID, " +
						"wqo.OptionID, " +
						"wqo.YellowLow, " +
						"wqo.GreenLow, " +
						"wqo.GreenHigh, " +
						"wqo.YellowHigh " +
						"FROM ReportPartComponent rpc " +
						"INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
						"WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
						"ORDER BY rpc.SortOrder", "eFormSqlConnection");
					while (rs.Read())
					{
						if (HttpContext.Current.Request.QueryString["GRPNG"] == null || HttpContext.Current.Request.QueryString["GRPNG"] == "0")
						{
							SqlDataReader rs2 = Db.rs("SELECT " +
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
								//"AND a.EndDT >= pr.Started " +
										(HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
										(HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
										"AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "' " +
										"GROUP BY a.ProjectRoundUserID, " + groupBy + "(a.EndDT) " +
									") tmp " +
									"GROUP BY tmp.DT " +
								") tmp2", "eFormSqlConnection");
							if (rs2.Read() && !rs2.IsDBNull(0))
							{
								float min = (float)Convert.ToDouble(rs2.GetValue(1));
								if (min < 0) { min = 0; }
								float max = (float)Convert.ToDouble(rs2.GetValue(0));
								if (max > 100) { max = 100; }
								g.setMinMax(min, max);
								g.roundMinMax();
								//g.computeMinMax(0.01f, 0.01f);
							}
							else
							{
								g.setMinMax(0f, 100f);
							}
							rs2.Close();
						}
						else
						{
							g.setMinMax(0f, 100f);
						}

						if (!rs.IsDBNull(3) && !rs.IsDBNull(4) && !rs.IsDBNull(5) && !rs.IsDBNull(6))
						{
							if (rs.GetInt32(3) > 0)
							{
								g.drawBgFromString(g.minVal, Math.Min(g.maxVal, (float)Convert.ToDouble(rs.GetInt32(3))), "FFA8A8");                             // red
							}
							if (rs.GetInt32(3) < 100 && rs.GetInt32(4) > 0)
							{
								g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(rs.GetInt32(3))), Math.Min(g.maxVal, (float)Convert.ToDouble(rs.GetInt32(4))), "FFFEBE");    // yellow
							}
							if (rs.GetInt32(4) < 100 && rs.GetInt32(5) > 0)
							{
								g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(rs.GetInt32(4))), Math.Min(g.maxVal, (float)Convert.ToDouble(rs.GetInt32(5))), "CCFFBB");   // green
							}
							if (rs.GetInt32(5) < 100 && rs.GetInt32(6) > 0)
							{
								g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(rs.GetInt32(5))), Math.Min(g.maxVal, (float)Convert.ToDouble(rs.GetInt32(6))), "FFFEBE"); // yellow
							}
							if (rs.GetInt32(6) < 100)
							{
								g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(rs.GetInt32(6))), g.maxVal, "FFA8A8");                           // red
							}
						}
					}
					rs.Close();

					if (g.minVal != 0f)
					{
						// Crunched graph sign
						g.drawLine(20, 0, (int)g.maxH + 20, 0, (int)g.maxH + 23, 1);
						g.drawLine(20, 0, (int)g.maxH + 23, -5, (int)g.maxH + 26, 1);
						g.drawLine(20, -5, (int)g.maxH + 26, 5, (int)g.maxH + 32, 1);
						g.drawLine(20, 5, (int)g.maxH + 32, 0, (int)g.maxH + 35, 1);
						g.drawLine(20, 0, (int)g.maxH + 35, 0, (int)g.maxH + 38, 1);
					}
				}
				else
				{
					g = new Graph(895, 550, "#FFFFFF");
					g.setMinMax(0f, 100f);

					cx += 2;
				}

				steps = cx;
				g.computeSteping((steps <= 1 ? 2 : steps));
				g.drawOutlines(11);
				g.drawAxis();

				cx = 0;

				if (type == 1)
				{
					#region Question

					decimal tot = 0;
					decimal sum = 0;

					rs = Db.rs("SELECT COUNT(*) FROM Answer a " +
						"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
						"WHERE a.EndDT IS NOT NULL " +
						(HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
						(HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
						"AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'", "eFormSqlConnection");
					if (rs.Read() && !rs.IsDBNull(0))
					{
						tot = Convert.ToDecimal(rs.GetInt32(0));
					}
					rs.Close();

					if (rac > Convert.ToInt32(tot))
					{
						g = new Graph(895, 50, "#FFFFFF");
						g.drawStringInGraph("Ingen återkoppling pga otillräckligt underlag", 300, -30);
					}
					else
					{
						g.drawAxisExpl("% (n = " + tot + ")", 5, false, false);

						rs = Db.rs("SELECT oc.OptionComponentID, ocl.Text FROM OptionComponents ocs " +
							"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
							"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + langID + " " +
							"WHERE ocs.OptionID = " + o + " ORDER BY ocs.SortOrder", "eFormSqlConnection");
						while (rs.Read())
						{
							cx++;

							SqlDataReader rs2 = Db.rs("SELECT COUNT(*) FROM Answer a " +
								"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
								"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
								"WHERE a.EndDT IS NOT NULL AND av.ValueInt = " + rs.GetInt32(0) + " " +
								(HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
								(HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
								"AND av.OptionID = " + o + " " +
								"AND av.QuestionID = " + q + " " +
								"AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'", "eFormSqlConnection");
							if (rs2.Read())
							{
								sum += Convert.ToDecimal(rs2.GetInt32(0));
								g.drawBar(5, cx, Convert.ToInt32(Math.Round(Convert.ToDecimal(rs2.GetInt32(0)) / tot * 100M, 0)));
							}
							rs2.Close();
							g.drawBottomString(rs.GetString(1), cx, true);
						}
						rs.Close();

						g.drawBar(4, ++cx, Convert.ToInt32(Math.Round((tot - sum) / tot * 100M, 0)));
						g.drawBottomString("Inget svar", cx, true);
					}
					#endregion
				}
				else if (type == 3)
				{
					#region Benchmark
					rs = Db.rs("SELECT " +
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
						"ORDER BY rpc.SortOrder", "eFormSqlConnection");
					while (rs.Read())
					{
						System.Collections.SortedList all = new System.Collections.SortedList();

						SqlDataReader rs2 = Db.rs("SELECT dbo.cf_projectUnitTree(pru.ProjectRoundUnitID,' » '), SortString FROM ProjectRoundUnit pru " +
							"WHERE LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'", "eFormSqlConnection");
						while (rs2.Read())
						{
							res = new System.Collections.Hashtable();

							if (rs.GetInt32(1) == 0)
							{
								getIdxVal(rs.GetInt32(0), rs2.GetString(1), langID);
							}
							else
							{
								getOtherIdxVal(rs.GetInt32(0), rs2.GetString(1), langID);
							}

							if (all.Contains(lastVal))
							{
								all[lastVal] += "," + rs2.GetString(0);
							}
							else
							{
								all.Add(lastVal, rs2.GetString(0));
							}
						}
						rs2.Close();

						for (int i = all.Count - 1; i >= 0; i--)
						{
							int color = 2;
							if (!rs.IsDBNull(3) && rs.GetInt32(3) >= 0 && rs.GetInt32(3) <= 100 && Convert.ToInt32(all.GetKey(i)) >= rs.GetInt32(3))
								color = 1;
							if (!rs.IsDBNull(4) && rs.GetInt32(4) >= 0 && rs.GetInt32(4) <= 100 && Convert.ToInt32(all.GetKey(i)) >= rs.GetInt32(4))
								color = 0;
							if (!rs.IsDBNull(5) && rs.GetInt32(5) >= 0 && rs.GetInt32(5) <= 100 && Convert.ToInt32(all.GetKey(i)) >= rs.GetInt32(5))
								color = 1;
							if (!rs.IsDBNull(6) && rs.GetInt32(6) >= 0 && rs.GetInt32(6) <= 100 && Convert.ToInt32(all.GetKey(i)) >= rs.GetInt32(6))
								color = 2;

							string[] u = all.GetByIndex(i).ToString().Split(',');

							foreach (string s in u)
							{
								g.drawBar(color, ++cx, Convert.ToInt32(all.GetKey(i)));
								//g.drawReference(cx,rs.GetInt32(2));
								g.drawBottomString(s, cx, true);
							}
						}

						g.drawReferenceLine(rs.GetInt32(2), " = riktvärde");
					}
					rs.Close();

					g.drawAxisExpl("poäng", 0, false, false);

					//g.drawReferenceLineExpl(770,25," = riktvärde");
					#endregion
				}
				else if (type == 2)
				{
					#region Index
					rs = Db.rs("SELECT " +
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
						"ORDER BY rpc.SortOrder", "eFormSqlConnection");
					while (rs.Read())
					{
						if (rs.GetInt32(1) == 0)
						{
							getIdxVal(rs.GetInt32(0), sortString, langID);
						}
						else
						{
							getOtherIdxVal(rs.GetInt32(0), sortString, langID);
						}
						int color = 2;
						if (!rs.IsDBNull(3) && rs.GetInt32(3) >= 0 && rs.GetInt32(3) <= 100 && lastVal >= rs.GetInt32(3))
							color = 1;
						if (!rs.IsDBNull(4) && rs.GetInt32(4) >= 0 && rs.GetInt32(4) <= 100 && lastVal >= rs.GetInt32(4))
							color = 0;
						if (!rs.IsDBNull(5) && rs.GetInt32(5) >= 0 && rs.GetInt32(5) <= 100 && lastVal >= rs.GetInt32(5))
							color = 1;
						if (!rs.IsDBNull(6) && rs.GetInt32(6) >= 0 && rs.GetInt32(6) <= 100 && lastVal >= rs.GetInt32(6))
							color = 2;
						g.drawBar(color, ++cx, lastVal);

						if (!rs.IsDBNull(2) && rs.GetInt32(2) >= 0 && rs.GetInt32(2) <= 100)
							g.drawReference(cx, rs.GetInt32(2));

						g.drawBottomString(lastDesc, cx, true);
					}
					rs.Close();

					g.drawAxisExpl("poäng", 0, false, false);

					g.drawReference(780, 25, " = riktvärde");
					#endregion
				}
				else if (type == 8)
				{

					#region Bottom string
					int dx = 0;
					for (int i = minDT; i <= maxDT; i++)
					{
						dx++;

						switch (GB)
						{
							case 1:
								{
									int d = i;
									int w = d % 52;
									if (w == 0)
									{
										w = 52;
									}
									string v = "v" + w + ", " + (d / 52);
									g.drawBottomString(v, dx, true);
									break;
								}
							case 2:
								{
									int d = i * 2;
									int w = d % 52;
									if (w == 0)
									{
										w = 52;
									}
									string v = "v" + (w - 1) + "-" + w + ", " + (d - ((d - 1) % 52)) / 52;
									g.drawBottomString(v, dx, true);
									break;
								}
							case 3:
								{
									int d = i;
									int w = d % 12;
									if (w == 0)
									{
										w = 12;
									}
									string v = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[w - 1] + ", " + ((d - w) / 12);
									g.drawBottomString(v, dx, true);
									break;
								}
							case 4:
								{
									int d = i * 3;
									int w = d % 12;
									if (w == 0)
									{
										w = 12;
									}
									string v = "Q" + (w / 3) + ", " + ((d - w) / 12);
									g.drawBottomString(v, dx, true);
									break;
								}
							case 5:
								{
									int d = i * 6;
									int w = d % 12;
									if (w == 0)
									{
										w = 12;
									}
									string v = ((d - w) / 12) + "/" + (w / 6);
									g.drawBottomString(v, dx, true);
									break;
								}
							case 6:
								{
									g.drawBottomString(i.ToString(), dx, true);
									break;
								}
							case 7:
								{
									int d = i * 2;
									int w = d % 52;
									if (w == 0)
									{
										w = 52;
									}
									string v = "v" + w + "-" + ((w == 52 ? 0 : w) + 1) + ", " + ((d + 1) - (d % 52)) / 52;
									g.drawBottomString(v, dx, true);
									break;
								}
						}
					}
					#endregion

					if (HttpContext.Current.Request.QueryString["GRPNG"] != null)
					{
						#region GRPNG
						int COUNT = 0;
						Hashtable desc = new Hashtable();
						Hashtable join = new Hashtable();
						ArrayList item = new ArrayList();
						string extraDesc = "";
						int GRPNG = Convert.ToInt32(HttpContext.Current.Request.QueryString["GRPNG"]);
						int PRUID = Convert.ToInt32(HttpContext.Current.Request.QueryString["PRUID"]);
						int SPONS = Convert.ToInt32((HttpContext.Current.Request.QueryString["SAID"] != null ? HttpContext.Current.Request.QueryString["SAID"] : HttpContext.Current.Session["SponsorAdminID"]));
						int SID = Convert.ToInt32((HttpContext.Current.Request.QueryString["SID"] != null ? HttpContext.Current.Request.QueryString["SID"] : HttpContext.Current.Session["SponsorID"]));
						string GID = (HttpContext.Current.Request.QueryString["GID"] != null ? HttpContext.Current.Request.QueryString["GID"].ToString().Replace(" ", "") : "");

						switch (GRPNG)
						{
							case 0:
								{
									string tmpDesc = ""; int sslen = 0; string tmpSS = "";

									SqlDataReader rs2 = Db.rs("SELECT " +
										(Convert.ToInt32((HttpContext.Current.Request.QueryString["Anonymized"] != null ? HttpContext.Current.Request.QueryString["Anonymized"] : HttpContext.Current.Session["Anonymized"])) == 1 ? "d.DepartmentAnonymized" : "d.Department") + ", " +
										"LEN(d.SortString), " +
										"d.SortString " +
										"FROM Department d " +
										(SPONS != -1 ?
										"INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " +
										"WHERE sad.SponsorAdminID = " + SPONS + " "
										:
										"WHERE d.SponsorID = " + SID + " "
										) +
										"ORDER BY LEN(d.SortString)");
									while (rs2.Read())
									{
										if (sslen == 0)
										{
											sslen = rs2.GetInt32(1);
										}
										if (sslen == rs2.GetInt32(1))
										{
											tmpDesc += (tmpDesc != "" ? ", " : "") + rs2.GetString(0) + "+";
											tmpSS += (tmpSS != "" ? "," : "") + "'" + rs2.GetString(2) + "'";
										}
										else
										{
											break;
										}
									}
									rs2.Close();

									item.Add("1");
									desc.Add("1", tmpDesc);
									join.Add("1", "" +
										"INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID " +
										"INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID " +
										"INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWu.ProjectRoundUnitID = " + PRUID + " " +
										//"INNER JOIN healthWatch..SponsorAdminDepartment HWsad ON HWup.DepartmentID = HWsad.DepartmentID AND HWsad.SponsorAdminID = " + SPONS + " ");
										"INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString," + sslen + ") IN (" + tmpSS + ") ");
									COUNT++;
									break;
								}
							case 1:
								{
									SqlDataReader rs2 = Db.rs("SELECT " +
										(Convert.ToInt32((HttpContext.Current.Request.QueryString["Anonymized"] != null ? HttpContext.Current.Request.QueryString["Anonymized"] : HttpContext.Current.Session["Anonymized"])) == 1 ? "d.DepartmentAnonymized" : "d.Department") + ", " +
										"d.DepartmentID " +
										"FROM Department d " +
										(SPONS != -1 ?
										"INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " +
										"WHERE sad.SponsorAdminID = " + SPONS + " "
										:
										"WHERE d.SponsorID = " + SID + " "
										) +
										"AND d.DepartmentID IN (" + GID + ") " +
										"ORDER BY d.SortString");
									while (rs2.Read())
									{
										item.Add(rs2.GetInt32(1).ToString());
										desc.Add(rs2.GetInt32(1).ToString(), rs2.GetString(0));
										join.Add(rs2.GetInt32(1).ToString(), "" +
											"INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID " +
											"INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = " + PRUID + " " +
											"INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWup.DepartmentID = " + rs2.GetInt32(1) + " " +
											//"INNER JOIN healthWatch..SponsorAdminDepartment HWsad ON HWup.DepartmentID = HWsad.DepartmentID AND HWsad.SponsorAdminID = " + SPONS + " " +
											"");
										COUNT++;
									}
									rs2.Close();
									break;
								}
							case 2:
								{
									SqlDataReader rs2 = Db.rs("SELECT " +
										(Convert.ToInt32((HttpContext.Current.Request.QueryString["Anonymized"] != null ? HttpContext.Current.Request.QueryString["Anonymized"] : HttpContext.Current.Session["Anonymized"])) == 1 ? "d.DepartmentAnonymized" : "d.Department") + ", " +
										"d.DepartmentID, " +
										"d.SortString " +
										"FROM Department d " +
										(SPONS != -1 ?
										"INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " +
										"WHERE sad.SponsorAdminID = " + SPONS + " "
										:
										"WHERE d.SponsorID = " + SID + " "
										) +
										"AND d.DepartmentID IN (" + GID + ") " +
										"ORDER BY d.SortString");
									while (rs2.Read())
									{
										item.Add(rs2.GetInt32(1).ToString());
										desc.Add(rs2.GetInt32(1).ToString(), rs2.GetString(0));
										join.Add(rs2.GetInt32(1).ToString(), "" +
											"INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID " +
											"INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = " + PRUID + " " +
											"INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID " +
											//"INNER JOIN healthWatch..SponsorAdminDepartment HWsad ON HWup.DepartmentID = HWsad.DepartmentID AND HWsad.SponsorAdminID = " + SPONS + " " +
											//"INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString," + rs2.GetString(2).Length + ") = '" + rs2.GetString(2) + "' ");
											"INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString," + rs2.GetString(2).Length + ") = '" + rs2.GetString(2) + "' ");
										COUNT++;
									}
									rs2.Close();
									break;
								}
							case 3:
								{
									string tmpSelect = "", tmpJoin = "", tmpOrder = "";

									string tmpDesc = ""; int sslen = 0; string tmpSS = "";

									SqlDataReader rs2 = Db.rs("SELECT " +
										(Convert.ToInt32((HttpContext.Current.Request.QueryString["Anonymized"] != null ? HttpContext.Current.Request.QueryString["Anonymized"] : HttpContext.Current.Session["Anonymized"])) == 1 ? "d.DepartmentAnonymized" : "d.Department") + ", " +
										"LEN(d.SortString), " +
										"d.SortString " +
										"FROM Department d " +
										(SPONS != -1 ?
										"INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " +
										"WHERE sad.SponsorAdminID = " + SPONS + " "
										:
										"WHERE d.SponsorID = " + SID + " "
										) +
										"ORDER BY LEN(d.SortString)");
									while (rs2.Read())
									{
										if (sslen == 0)
										{
											sslen = rs2.GetInt32(1);
										}
										if (sslen == rs2.GetInt32(1))
										{
											tmpDesc += (tmpDesc != "" ? ", " : "") + rs2.GetString(0) + "+";
											tmpSS += (tmpSS != "" ? "," : "") + "'" + rs2.GetString(2) + "'";
										}
										else
										{
											break;
										}
									}
									rs2.Close();

									rs2 = Db.rs("SELECT BQ.BQID, BQ.Internal FROM BQ WHERE BQ.BQID IN (" + GID.Replace("'", "") + ")");
									GID = "";
									while (rs2.Read())
									{
										GID += (GID != "" ? "," : "") + rs2.GetInt32(0);

										extraDesc += (extraDesc != "" ? " / " : "") + rs2.GetString(1);

										tmpSelect += (tmpSelect != "" ? " ," : "") + "ba" + rs2.GetInt32(0) + ".BAID,ba" + rs2.GetInt32(0) + ".Internal ";
										tmpJoin += (tmpJoin != "" ? "INNER JOIN BA ba" + rs2.GetInt32(0) + " ON ba" + rs2.GetInt32(0) + ".BQID = " + rs2.GetInt32(0) + " " : "FROM BA ba" + rs2.GetInt32(0) + " ");
										tmpOrder += (tmpOrder != "" ? ", ba" + rs2.GetInt32(0) + ".SortOrder" : "WHERE ba" + rs2.GetInt32(0) + ".BQID = " + rs2.GetInt32(0) + " ORDER BY ba" + rs2.GetInt32(0) + ".SortOrder");
									}
									rs2.Close();
									string[] GIDS = GID.Split(',');

									rs2 = Db.rs("SELECT " +
										tmpSelect +
										tmpJoin +
										tmpOrder);
									while (rs2.Read())
									{
										string key = "", txt = "", sql = "" +
											"INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID " +
											"INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = " + PRUID + " " +
											"INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID " +
											//"INNER JOIN healthWatch..SponsorAdminDepartment HWsad ON HWup.DepartmentID = HWsad.DepartmentID AND HWsad.SponsorAdminID = " + SPONS;
											"INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString," + sslen + ") IN (" + tmpSS + ") ";

										for (int i = 0; i < GIDS.Length; i++)
										{
											key += (key != "" ? "X" : "") + rs2.GetInt32(0 + i * 2);
											txt += (txt != "" ? " / " : "") + rs2.GetString(1 + i * 2);
											sql += "INNER JOIN healthWatch..UserProfileBQ HWp" + GIDS[i] + " ON HWup.UserProfileID = HWp" + GIDS[i] + ".UserProfileID AND HWp" + GIDS[i] + ".BQID = " + GIDS[i] + " AND HWp" + GIDS[i] + ".ValueInt = " + rs2.GetInt32(0 + i * 2);
										}
										COUNT++;

										item.Add(key);
										desc.Add(key, txt);
										join.Add(key, sql);
									}
									rs2.Close();

									break;
								}
						}
						int breaker = 6, itemWidth = 120;
						if (COUNT < 6)
						{
							breaker = 4; itemWidth = 180;
						}
						if (COUNT < 4)
						{
							breaker = 3; itemWidth = 240;
						}
						rs = Db.rs("SELECT " +
							"rpc.WeightedQuestionOptionID, " +	// 0
							"wqol.WeightedQuestionOption, " +
							"wqo.QuestionID, " +
							"wqo.OptionID " +
							"FROM ReportPartComponent rpc " +
							"INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
							"INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + langID + " " +
							"WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
							"ORDER BY rpc.SortOrder", "eFormSqlConnection");
						if (rs.Read())
						{
							int bx = 0;

							foreach (string i in item)
							{
								if (bx == 0)
								{
									g.drawAxis(false);
									g.drawAxisExpl((extraDesc != "" ? extraDesc + "\n" : "") + (langID == 1 ? "Medelvärde" : "Mean value") + (HttpContext.Current.Request.QueryString["STDEV"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1 ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""), 0, false, false);
								}
								g.drawColorExplBox((string)desc[i], bx + 4, 130 + (int)((bx % breaker) * itemWidth), 20 + (int)Math.Floor((double)bx / breaker) * 15);
								float lastVal = -1f;
								float lastStd = -1f;
								int lastCX = 1;
								cx = 1;
								int lastDT = minDT - 1;
								#region Data loop
								string SQL = "SELECT " +
									"tmp.DT, " +
									"AVG(tmp.V), " +
									"COUNT(tmp.V), " +
									"STDEV(tmp.V) " +
									"FROM (" +
									"SELECT " +
									"" + groupBy + "(a.EndDT) AS DT, " +
									"AVG(av.ValueInt) AS V " +
									"FROM Answer a " +
									join[i] +
									"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = " + rs.GetInt32(2) + " AND av.OptionID = " + rs.GetInt32(3) + " " +
									"WHERE a.EndDT IS NOT NULL " +
									(HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
									(HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
									"GROUP BY a.ProjectRoundUserID, " + groupBy + "(a.EndDT) " +
									") tmp " +
									"GROUP BY tmp.DT " +
									"ORDER BY tmp.DT";
								//HttpContext.Current.Response.Write("<HTML><BODY><!--" + SQL + "--></BODY></HTML>");
								//HttpContext.Current.Response.End();
								SqlDataReader rs2 = Db.rs(SQL, "eFormSqlConnection");
								while (rs2.Read())
								{
									//if (lastDT == 0) { lastDT = rs2.GetInt32(0); }

									while (lastDT + 1 < rs2.GetInt32(0))
									{
										lastDT++;
										cx++;
									}

									if (rs2.GetInt32(2) >= rac)
									{
										#region Bottom string
										if (COUNT == 1)
										{
											switch (GB)
											{
												case 1:
													{
														int d = rs2.GetInt32(0);
														int w = d % 52;
														if (w == 0)
														{
															w = 52;
														}
														string v = "v" + w + ", " + (d / 52) + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
														g.drawBottomString(v, cx, true);
														break;
													}
												case 2:
													{
														int d = rs2.GetInt32(0) * 2;
														int w = d % 52;
														if (w == 0)
														{
															w = 52;
														}
														//string v = "v" + (w-1) + "-" + w + "\n" + (d-(d-1)%52)/52;
														string v = "v" + (w - 1) + "-" + w + ", " + (d - ((d - 1) % 52)) / 52 + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
														g.drawBottomString(v, cx, true);
														break;
													}
												case 3:
													{
														int d = rs2.GetInt32(0);
														int w = d % 12;
														if (w == 0)
														{
															w = 12;
														}
														string v = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[w - 1] + ", " + ((d - w) / 12) + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
														g.drawBottomString(v, cx, true);
														break;
													}
												case 4:
													{
														int d = rs2.GetInt32(0) * 3;
														int w = d % 12;
														if (w == 0)
														{
															w = 12;
														}
														string v = "Q" + (w / 3) + ", " + ((d - w) / 12) + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
														g.drawBottomString(v, cx, true);
														break;
													}
												case 5:
													{
														int d = rs2.GetInt32(0) * 6;
														int w = d % 12;
														if (w == 0)
														{
															w = 12;
														}
														string v = ((d - w) / 12) + "/" + (w / 6) + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
														g.drawBottomString(v, cx, true);
														break;
													}
												case 6:
													{
														g.drawBottomString(rs2.GetInt32(0).ToString() + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : ""), cx, true);
														break;
													}
												case 7:
													{
														int d = rs2.GetInt32(0) * 2;
														int w = d % 52;
														if (w == 0)
														{
															w = 52;
														}
														//string v = "v" + (w-1) + "-" + w + "\n" + (d-(d-1)%52)/52;
														string v = "v" + w + "-" + ((w == 52 ? 0 : w) + 1) + ", " + ((d + 1) - (d % 52)) / 52 + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
														g.drawBottomString(v, cx, true);
														break;
													}
											}
										}
										#endregion

										float newVal = (rs2.IsDBNull(1) ? -1f : (float)Convert.ToDouble(rs2.GetValue(1)));
										float newStd = (rs2.IsDBNull(3) ? -1f : (float)Convert.ToDouble(rs2.GetValue(3)));

										if (stdev)
										{
											g.drawLine(bx + 4, cx * g.steping - 10, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping + 10, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
											g.drawLine(20, cx * g.steping, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
											g.drawLine(bx + 4, cx * g.steping - 10, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping + 10, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
										}

										if (newVal != -1f)
										{
											if (lastVal != -1f)
											{
												g.drawStepLine(bx + 4, lastCX, lastVal, cx, newVal, 2 + (!stdev ? 1 : 0));
											}
											lastCX = cx;
										}
										lastVal = newVal;
										lastStd = newStd;

										g.drawCircle(cx, newVal, bx + 4);
									}
									lastDT = rs2.GetInt32(0);
									cx++;
								}
								rs2.Close();
								#endregion

								bx++;
							}
						}
						rs.Close();
						#endregion
					}
					else
					{
						int bx = 0;
						rs = Db.rs("SELECT " +
							"rpc.WeightedQuestionOptionID, " +	// 0
							"wqol.WeightedQuestionOption, " +
							"wqo.QuestionID, " +
							"wqo.OptionID " +
							"FROM ReportPartComponent rpc " +
							"INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
							"INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + langID + " " +
							"WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
							"ORDER BY rpc.SortOrder", "eFormSqlConnection");
						while (rs.Read() && bx <= 1)
						{
							if (bx == 0)
							{
								g.drawAxisExpl(rs.GetString(1) + ", " + (langID == 1 ? "medelvärde" : "mean value") + (HttpContext.Current.Request.QueryString["STDEV"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1 ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""), bx + 4, false, true);
								g.drawAxis(false);
							}
							else
							{
								g.drawAxisExpl(rs.GetString(1) + ", " + (langID == 1 ? "medelvärde" : "mean value") + (HttpContext.Current.Request.QueryString["STDEV"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1 ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""), bx + 4, true, true);
								g.drawAxis(true);
							}
							float lastVal = -1f;
							float lastStd = -1f;
							int lastCX = 1;
							cx = 1;
							int lastDT = minDT - 1;
							#region Data loop
							SqlDataReader rs2 = Db.rs("SELECT " +
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
								(HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
								(HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
								"AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "' " +
								"GROUP BY a.ProjectRoundUserID, " + groupBy + "(a.EndDT) " +
								") tmp " +
								"GROUP BY tmp.DT " +
								"ORDER BY tmp.DT", "eFormSqlConnection");
							while (rs2.Read())
							{
								//if (lastDT == 0) { lastDT = rs2.GetInt32(0); }

								while (lastDT + 1 < rs2.GetInt32(0))
								{
									lastDT++;
									cx++;
								}

								if (rs2.GetInt32(2) >= rac)
								{
									#region Bottom string
									switch (GB)
									{
										case 1:
											{
												int d = rs2.GetInt32(0);
												int w = d % 52;
												if (w == 0)
												{
													w = 52;
												}
												string v = "v" + w + ", " + (d / 52) + ", n = " + rs2.GetInt32(2);
												g.drawBottomString(v, cx, true);
												break;
											}
										case 2:
											{
												int d = rs2.GetInt32(0) * 2;
												int w = d % 52;
												if (w == 0)
												{
													w = 52;
												}
												//string v = "v" + (w-1) + "-" + w + "\n" + (d-(d-1)%52)/52 + "\n\nn = " + rs2.GetInt32(2);
												string v = "v" + (w - 1) + "-" + w + ", " + (d - ((d - 1) % 52)) / 52 + ", n = " + rs2.GetInt32(2);
												g.drawBottomString(v, cx, true);
												break;
											}
										case 3:
											{
												int d = rs2.GetInt32(0);
												int w = d % 12;
												if (w == 0)
												{
													w = 12;
												}
												string v = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[w - 1] + ", " + ((d - w) / 12) + ", n = " + rs2.GetInt32(2);
												g.drawBottomString(v, cx, true);
												break;
											}
										case 4:
											{
												int d = rs2.GetInt32(0) * 3;
												int w = d % 12;
												if (w == 0)
												{
													w = 12;
												}
												string v = "Q" + (w / 3) + ", " + ((d - w) / 12) + ", n = " + rs2.GetInt32(2);
												g.drawBottomString(v, cx, true);
												break;
											}
										case 5:
											{
												int d = rs2.GetInt32(0) * 6;
												int w = d % 12;
												if (w == 0)
												{
													w = 12;
												}
												string v = ((d - w) / 12) + "/" + (w / 6) + ", n = " + rs2.GetInt32(2);
												g.drawBottomString(v, cx, true);
												break;
											}
										case 6:
											{
												g.drawBottomString(rs2.GetInt32(0).ToString() + ", n = " + rs2.GetInt32(2), cx, true);
												break;
											}
										case 7:
											{
												int d = rs2.GetInt32(0) * 2;
												int w = d % 52;
												if (w == 0)
												{
													w = 52;
												}
												//string v = "v" + (w-1) + "-" + w + "\n" + (d-(d-1)%52)/52 + "\n\nn = " + rs2.GetInt32(2);
												string v = "v" + w + "-" + ((w == 52 ? 0 : w) + 1) + ", " + ((d + 1) - (d % 52)) / 52 + ", n = " + rs2.GetInt32(2);
												g.drawBottomString(v, cx, true);
												break;
											}
									}
									#endregion

									float newVal = (rs2.IsDBNull(1) ? -1f : (float)Convert.ToDouble(rs2.GetValue(1)));
									float newStd = (rs2.IsDBNull(3) ? -1f : (float)Convert.ToDouble(rs2.GetValue(3)));

									g.drawLine(20, cx * g.steping - 10, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping + 10, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
									g.drawLine(20, cx * g.steping, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
									g.drawLine(20, cx * g.steping - 10, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping + 10, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);

									if (newVal != -1f)
									{
										if (lastVal != -1f)
										{
											g.drawStepLine(bx + 4, lastCX, lastVal, cx, newVal, 3);
										}
										lastCX = cx;
									}
									lastVal = newVal;
									lastStd = newStd;

									g.drawCircle(cx, newVal);
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
				}

				#endregion
			}

			// g.printCopyRight();
			g.render();
		}
	}
}