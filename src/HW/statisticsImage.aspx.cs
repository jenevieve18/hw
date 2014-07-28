using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.FromHW;

namespace HW
{
    public partial class statisticsImage : System.Web.UI.Page
    {
        #region removed
        /*
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
						getIdxVal(rs.GetInt32(0), sortString);
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
		*/
        #endregion

        private int getDatabaseVal(int QID, int OID, DateTime dt)
        {
            int ret = -1;
            SqlDataReader rs2 = Db.rs("SELECT " +
                "AVG(tmp.AX) " +
                "FROM " +
                "(" +
                "SELECT " +
                "AVG(av.ValueInt) AS AX " +
                "FROM Answer a " +
                "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
                "WHERE av.DeletedSessionID IS NULL " +
                "AND av.ValueInt IS NOT NULL " +
                "AND a.EndDT IS NOT NULL " +
                "AND YEAR(a.EndDT) = " + dt.AddMonths(-1).Year + " " +
                "AND MONTH(a.EndDT) = " + dt.AddMonths(-1).Month + " " +
                "AND av.QuestionID = " + QID + " " +
                "AND av.OptionID = " + OID + " " +
                "GROUP BY a.ProjectRoundUserID " +
                ") tmp", "eFormSqlConnection");
            if (rs2.Read() && !rs2.IsDBNull(0))
            {
                ret = Convert.ToInt32(rs2.GetValue(0));
            }
            rs2.Close();

            return ret;
        }
        private int getProfileVal(int QID, int OID, int AID, int UID, DateTime dt)
        {
            int ret = -1;

            SqlDataReader rs2 = Db.rs("SELECT " +
                "AVG(tmp.AX) " +
                "FROM " +
                "(" +
                "SELECT " +
                "AVG(av.ValueInt) AS AX " +
                "FROM Answer a " +
                "INNER JOIN healthWatch..UserProjectRoundUserAnswer ha ON a.AnswerID = ha.AnswerID " +
                "INNER JOIN healthWatch..UserProfile hu ON ha.UserProfileID = hu.UserProfileID " +
                "INNER JOIN healthWatch..UserProfile xhu ON hu.ProfileComparisonID = xhu.ProfileComparisonID " +
                "INNER JOIN healthWatch..UserProjectRoundUserAnswer xha ON xhu.UserProfileID = xha.UserProfileID " +
                "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
                "WHERE av.DeletedSessionID IS NULL " +
                "AND av.ValueInt IS NOT NULL " +
                "AND a.EndDT IS NOT NULL " +
                "AND YEAR(a.EndDT) = " + dt.AddMonths(-1).Year + " " +
                "AND MONTH(a.EndDT) = " + dt.AddMonths(-1).Month + " " +
                "AND xha.AnswerID = " + AID + " " +
                "AND xha.ProjectRoundUserID = " + UID + " " +
                "AND av.QuestionID = " + QID + " " +
                "AND av.OptionID = " + OID + " " +
                "GROUP BY a.ProjectRoundUserID " +
                ") tmp", "eFormSqlConnection");
            if (rs2.Read() && !rs2.IsDBNull(0))
            {
                ret = Convert.ToInt32(rs2.GetValue(0));
            }
            rs2.Close();

            return ret;
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            #region Init
            string rp = "";
            foreach (string s in ("0," + HttpContext.Current.Request.QueryString["RPID"].ToString()).Split(','))
            {
                try
                {
                    rp += (rp != "" ? "," : "") + Convert.ToInt32(s).ToString();
                }
                catch (Exception) { }
            }
            string[] so = ("0," + HttpContext.Current.Request.QueryString["RPO"].ToString()).Split(',');

            bool compare = (HttpContext.Current.Request.QueryString["C"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["C"]) == 2);
            bool sponsored = (Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) > 1);
            DateTime TDT = DateTime.ParseExact(HttpContext.Current.Request.QueryString["TDT"].ToString(), "yyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            DateTime FDT = DateTime.ParseExact(HttpContext.Current.Request.QueryString["FDT"].ToString(), "yyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            string answer = "";
            if (HttpContext.Current.Request.QueryString["E"] != null)
            {
                foreach (string s in ("0," + HttpContext.Current.Request.QueryString["E"].ToString()).Split(','))
                {
                    try
                    {
                        answer += (answer != "" ? "," : "") + Convert.ToInt32(s).ToString();
                    }
                    catch (Exception) { }
                }
                answer = "NOT IN (" + answer + ")";
            }
            else
            {
                foreach (string s in ("0," + HttpContext.Current.Request.QueryString["I"].ToString()).Split(','))
                {
                    try
                    {
                        answer += (answer != "" ? "," : "") + Convert.ToInt32(s).ToString();
                    }
                    catch (Exception) { }
                }
                answer = "IN (" + answer + ")";
            }
            #endregion
            #region Count answers, get min/max date and calculate steps
            int cx = 0, userID = 0;
            SqlDataReader rs = Db.rs("SELECT " +
                "COUNT(*), " +
                "MIN(DT), " +
                "MAX(DT), " +
                "uprua.ProjectRoundUserID " +
                "FROM UserProjectRoundUser upru " +
                "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
                "WHERE upru.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " " +
                "AND upru.ProjectRoundUnitID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["S"]) + " " +
                "AND uprua.DT < '" + TDT.AddDays(1).ToString("yyyy-MM-dd") + "' " +
                "AND uprua.DT >= '" + FDT.ToString("yyyy-MM-dd") + "' " +
                "AND uprua.AnswerID " + answer + " " +
                "GROUP BY uprua.ProjectRoundUserID");
            if (rs.Read())
            {
                cx = rs.GetInt32(0);
                FDT = rs.GetDateTime(1).Date;
                TDT = rs.GetDateTime(2).Date;
                userID = rs.GetInt32(3);
            }
            rs.Close();

            bool oneDay = false;
            int steps = ((TimeSpan)(TDT - FDT)).Days + 1;
            if (steps == 1 && cx > 1)
            {
                oneDay = true;
                steps = 24 * 6;
            }
            #endregion

            if (cx <= 1)
            {
                #region Bars
                Graph g = new Graph(520, (rp.Split(',').Length < 5 ? 350 : 440), "#FFFFFF");
                g.setMinMax(0f, 100f);
                if (compare && sponsored && 1 == 0)
                {
                    g.computeSteping(6);
                    g.drawOutlines(6);
                }
                else if (compare)
                {
                    g.computeSteping(5);
                    g.drawOutlines(5);
                }
                else
                {
                    g.computeSteping(rp.Split(',').Length + 1);
                    g.drawOutlines(rp.Split(',').Length + 1);
                }
                g.drawAxis();

                int pos = 1;

                bool hasReference = false;
                bool hasColors = false;

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
                    "FROM Report r " +
                    "INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID " +
                    "INNER JOIN ReportPartComponent rpc ON rp.ReportPartID = rpc.ReportPartID " +
                    "INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
                    "INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                    "WHERE rpc.ReportPartID IN (" + rp + ") " +
                    "ORDER BY rp.SortOrder, rpc.SortOrder", "eFormSqlConnection");
                while (rs.Read())
                {
                    SqlDataReader rs2 = Db.rs("SELECT " +
                        "av.ValueInt, " +
                        "a.EndDT, " +
                        "a.AnswerID " +
                        "FROM Answer a " +

                        "INNER JOIN healthWatch..UserProjectRoundUserAnswer ha ON a.AnswerID = ha.AnswerID AND ha.ProjectRoundUserID = a.ProjectRoundUserID " +

                        "LEFT OUTER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
                        "AND av.DeletedSessionID IS NULL " +
                        "AND av.QuestionID = " + rs.GetInt32(7) + " " +
                        "AND av.OptionID = " + rs.GetInt32(8) + " " +
                        "WHERE a.EndDT IS NOT NULL " +
                        "AND a.EndDT < '" + TDT.AddDays(1).ToString("yyyy-MM-dd") + "' " +
                        "AND a.EndDT >= '" + FDT.ToString("yyyy-MM-dd") + "' " +
                        "AND a.AnswerID " + answer + " " +
                        "AND a.ProjectRoundUserID = " + userID, "eFormSqlConnection");
                    if (rs2.Read())
                    {
                        int color = 19;
                        if (!compare && !rs2.IsDBNull(0))
                        {
                            color = 30;

                            if (!rs.IsDBNull(3))
                            {
                                hasColors = true;

                                if (rs.GetInt32(3) >= 0 && rs.GetInt32(3) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(3))
                                {
                                    color = 1;
                                }
                            }
                            if (!rs.IsDBNull(4))
                            {
                                hasColors = true;

                                if (rs.GetInt32(4) >= 0 && rs.GetInt32(4) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(4))
                                {
                                    color = 0;
                                }
                            }
                            if (!rs.IsDBNull(5))
                            {
                                hasColors = true;

                                if (rs.GetInt32(5) >= 0 && rs.GetInt32(5) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(5))
                                {
                                    color = 1;
                                }
                            }
                            if (!rs.IsDBNull(6))
                            {
                                hasColors = true;

                                if (rs.GetInt32(6) >= 0 && rs.GetInt32(6) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(6))
                                {
                                    color = 2;
                                }
                            }
                            if (hasColors && color == 30)
                            {
                                color = 2;
                            }
                        }

                        g.drawBar(color, pos, (rs2.IsDBNull(0) ? -1 : rs2.GetInt32(0)), true, false);
                        g.drawAxisExpl((compare ? rs.GetString(1) + ", " : "") + rs2.GetDateTime(1).ToString("yyyy-MM-dd HH:mm"), 0, false, false);

                        if (compare)
                        {
                            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                            {
                                case 1:
                                    g.drawBottomString("Mitt värde", pos, false);
                                    g.drawBar(18, ++pos, getProfileVal(rs.GetInt32(7), rs.GetInt32(8), rs2.GetInt32(2), userID, rs2.GetDateTime(1)), true, false);
                                    g.drawBottomString("Samma profil", pos, false);
                                    g.drawBar(28, ++pos, getDatabaseVal(rs.GetInt32(7), rs.GetInt32(8), rs2.GetDateTime(1)), true, false);
                                    g.drawBottomString("Hela databasen", pos, false);

                                    break;
                                case 2:
                                    g.drawBottomString("My value", pos, false);
                                    g.drawBar(18, ++pos, getProfileVal(rs.GetInt32(7), rs.GetInt32(8), rs2.GetInt32(2), userID, rs2.GetDateTime(1)), true, false);
                                    g.drawBottomString("Same profile", pos, false);
                                    g.drawBar(28, ++pos, getDatabaseVal(rs.GetInt32(7), rs.GetInt32(8), rs2.GetDateTime(1)), true, false);
                                    g.drawBottomString("Complete database", pos, false);
                                    break;
                            }
                        }
                    }
                    rs2.Close();

                    if (!rs.IsDBNull(2))
                    {
                        hasReference = true;
                        g.drawReference(pos, rs.GetInt32(2));
                    }
                    if (!compare)
                    {
                        g.drawBottomString(rs.GetString(1), pos, !(rp.Split(',').Length < 5));
                    }
                    pos++;
                }
                rs.Close();

                if (hasReference)
                {
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1:
                            g.drawReference(433, 10, " = riktvärde");
                            break;
                        case 2:
                            g.drawReference(433, 10, " = target value");
                            break;
                    }
                }

                if (hasColors)
                {
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1:
                            g.drawColorExplBox("Hälsosam nivå", 0, 150, 30);
                            g.drawColorExplBox("Förbättringsbehov", 1, 270, 30);
                            g.drawColorExplBox("Ohälsosam nivå", 2, 400, 30);
                            break;
                        case 2:
                            g.drawColorExplBox("Healthy level", 0, 150, 30);
                            g.drawColorExplBox("Improvement needed", 1, 270, 30);
                            g.drawColorExplBox("Unhealthy level", 2, 400, 30);
                            break;
                    }
                }

                #endregion

                g.render();
            }
            else if (cx > 1)
            {
                #region Line
                Graph g = new Graph(520, 350, "#FFFFFF");
                g.setMinMax(0f, 100f);

                if (oneDay)
                {
                    g.computeSteping(steps);
                    g.drawOutlines(steps, false);

                    g.drawBottomString("00:00", 0);
                    g.drawBottomString("06:00", 6 * 6);
                    g.drawBottomString("12:00", 12 * 6);
                    g.drawBottomString("18:00", 18 * 6);
                    g.drawBottomString("23:59", 24 * 6);
                }
                else
                {
                    g.computeSteping(steps + 2);
                    g.drawOutlines(steps + 2, false);

                    g.drawBottomString(FDT.ToString("yyyy-MM-dd"), 1);
                    g.drawBottomString(TDT.ToString("yyyy-MM-dd"), steps);
                    if (steps > 2)
                    {
                        if ((steps - 5) % 4 == 0)
                        {
                            g.drawBottomString(FDT.AddDays((steps - 1) / 4 * 1).ToString("yyyy-MM-dd"), (steps - 1) / 4 * 1 + 1);
                            g.drawBottomString(FDT.AddDays((steps - 1) / 4 * 2).ToString("yyyy-MM-dd"), (steps - 1) / 4 * 2 + 1);
                            g.drawBottomString(FDT.AddDays((steps - 1) / 4 * 3).ToString("yyyy-MM-dd"), (steps - 1) / 4 * 3 + 1);
                        }
                        else if ((steps - 4) % 3 == 0)
                        {
                            g.drawBottomString(FDT.AddDays((steps - 1) / 3 * 1).ToString("yyyy-MM-dd"), (steps - 1) / 3 * 1 + 1);
                            g.drawBottomString(FDT.AddDays((steps - 1) / 3 * 2).ToString("yyyy-MM-dd"), (steps - 1) / 3 * 2 + 1);
                        }
                        else if ((steps - 3) % 2 == 0)
                        {
                            g.drawBottomString(FDT.AddDays((steps - 1) / 2).ToString("yyyy-MM-dd"), ((steps - 1) / 2) + 1);
                        }
                    }
                }
                g.drawAxis();

                int bx = 0, answerID = 0;

                string axisDesc = "Resultat";
                rs = Db.rs("SELECT " +
                    "rpc.WeightedQuestionOptionID, " +	// 0
                    "wqol.WeightedQuestionOption, " +
                    "wqo.QuestionID, " +
                    "wqo.OptionID " +
                    "FROM Report r " +
                    "INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID " +
                    "INNER JOIN ReportPartComponent rpc ON rp.ReportPartID = rpc.ReportPartID " +
                    "INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
                    "INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                    "WHERE rpc.ReportPartID IN (" + rp + ") " +
                    "ORDER BY rp.SortOrder, rpc.SortOrder", "eFormSqlConnection");
                while (rs.Read())
                {
                    float lastVal = -1f;//, compareVal = -1f;
                    int lastPos = 0;
                    SqlDataReader rs2 = Db.rs("SELECT " +
                        "a.EndDT, " +
                        "av.ValueInt, " +
                        "a.AnswerID " +
                        "FROM Answer a " +
                        "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +

                        "INNER JOIN healthWatch..UserProjectRoundUserAnswer ha ON a.AnswerID = ha.AnswerID AND ha.ProjectRoundUserID = a.ProjectRoundUserID " +

                        "WHERE av.DeletedSessionID IS NULL " +
                        "AND av.ValueInt IS NOT NULL " +
                        "AND a.EndDT IS NOT NULL " +
                        "AND a.EndDT < '" + TDT.AddDays(1).ToString("yyyy-MM-dd") + "' " +
                        "AND a.EndDT >= '" + FDT.ToString("yyyy-MM-dd") + "' " +
                        "AND a.AnswerID " + answer + " " +
                        "AND a.ProjectRoundUserID = " + userID + " " +
                        "AND av.QuestionID = " + rs.GetInt32(2) + " " +
                        "AND av.OptionID = " + rs.GetInt32(3) + " " +
                        "ORDER BY a.EndDT", "eFormSqlConnection");
                    while (rs2.Read())
                    {
                        answerID = rs2.GetInt32(2);
                        //if (compare && compareVal == -1f)
                        //{
                        //    compareVal = (float)getProfileVal(rs.GetInt32(2), rs.GetInt32(3), rs2.GetInt32(2), userID);
                        //    g.drawStepLine(18, 0, compareVal, steps + 1, compareVal);

                        //    compareVal = (float)getDatabaseVal(rs.GetInt32(2), rs.GetInt32(3));
                        //    g.drawStepLine(28, 0, compareVal, steps + 1, compareVal);
                        //}

                        if (lastPos == 0 && oneDay)
                        {
                            g.drawStringInGraph(rs2.GetDateTime(0).ToString("yyyy-MM-dd"), 195, 280);
                        }
                        int pos = (oneDay ? rs2.GetDateTime(0).Hour * 6 + Convert.ToInt32(rs2.GetDateTime(0).Minute / 10) : ((TimeSpan)(rs2.GetDateTime(0).Date - FDT)).Days + 1);
                        float newVal = (float)Convert.ToDouble(rs2.GetInt32(1));
                        int color = (compare ? 19 : Convert.ToInt32(so[bx + 1]) + 3);
                        if (lastVal != -1f)
                        {
                            g.drawStepLine(color, lastPos, lastVal, pos, newVal);
                        }
                        g.drawDot(color, pos, newVal, lastPos, lastVal, 3);
                        lastPos = pos;
                        lastVal = newVal;
                    }
                    rs2.Close();

                    if (compare)
                    {
                        float lastCompareValue = -1f, compareValue = -1f;
                        int lastComparePosition = 0;

                        rs2 = Db.rs("SELECT " +
                            "tmp.dtyr, " +
                            "tmp.dtmt, " +
                            "AVG(tmp.val) " +
                            "FROM (" +
                            "SELECT " +
                            "YEAR(a.EndDT) AS dtyr, " +
                            "MONTH(a.EndDT) AS dtmt, " +
                            "AVG(av.ValueInt) as val, " +
                            "a.ProjectRoundUserID " +
                            "FROM Answer a " +
                            "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
                            "WHERE av.DeletedSessionID IS NULL " +
                            "AND av.ValueInt IS NOT NULL " +
                            "AND a.EndDT IS NOT NULL " +
                            "AND a.EndDT < '" + TDT.AddDays(1).ToString("yyyy-MM") + "-01' " +
                            "AND a.EndDT >= '" + FDT.AddMonths(1).ToString("yyyy-MM") + "-01' " +
                            "AND av.QuestionID = " + rs.GetInt32(2) + " " +
                            "AND av.OptionID = " + rs.GetInt32(3) + " " +
                            "GROUP BY a.ProjectRoundUserID, YEAR(a.EndDT), MONTH(a.EndDT) " +
                            ") tmp GROUP BY tmp.dtyr, tmp.dtmt ORDER BY tmp.dtyr, tmp.dtmt", "eFormSqlConnection");
                        while (rs2.Read())
                        {
                            if (oneDay)
                            {
                                compareValue = (float)Convert.ToDouble(rs2.GetValue(2));
                                g.drawStepLine(28, 0, compareValue, steps + 1, compareValue);
                            }
                            else
                            {
                                int pos = ((TimeSpan)((new DateTime(rs2.GetInt32(0), rs2.GetInt32(1), 15)).Date - FDT)).Days + 1;
                                if (pos < 0) { pos = 0; }
                                if (pos > steps + 1) { pos = steps + 1; }
                                compareValue = (float)Convert.ToDouble(rs2.GetValue(2));
                                if (lastCompareValue != -1f)
                                {
                                    g.drawStepLine(28, lastComparePosition, lastCompareValue, pos, compareValue);
                                }
                                lastComparePosition = pos;
                                lastCompareValue = compareValue;
                            }
                        }
                        rs2.Close();

                        lastCompareValue = -1f; compareValue = -1f;
                        lastComparePosition = 0;

                        rs2 = Db.rs("SELECT " +
                            "tmp.dtyr, " +
                            "tmp.dtmt, " +
                            "AVG(tmp.val) " +
                            "FROM (" +
                            "SELECT " +
                            "YEAR(a.EndDT) AS dtyr, " +
                            "MONTH(a.EndDT) AS dtmt, " +
                            "AVG(av.ValueInt) as val, " +
                            "a.ProjectRoundUserID " +
                            "FROM Answer a " +

                            "INNER JOIN healthWatch..UserProjectRoundUserAnswer ha ON a.AnswerID = ha.AnswerID AND ha.ProjectRoundUserID = a.ProjectRoundUserID " +
                            "INNER JOIN healthWatch..UserProfile hu ON ha.UserProfileID = hu.UserProfileID " +
                            "INNER JOIN healthWatch..UserProfile xhu ON hu.ProfileComparisonID = xhu.ProfileComparisonID " +
                            "INNER JOIN healthWatch..UserProjectRoundUserAnswer xha ON xhu.UserProfileID = xha.UserProfileID " +

                            "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
                            "WHERE av.DeletedSessionID IS NULL " +
                            "AND av.ValueInt IS NOT NULL " +
                            "AND a.EndDT IS NOT NULL " +
                            "AND a.EndDT < '" + TDT.AddDays(1).ToString("yyyy-MM") + "-01' " +
                            "AND a.EndDT >= '" + FDT.AddMonths(1).ToString("yyyy-MM") + "-01' " +

                            "AND xha.AnswerID = " + answerID + " " +
                            "AND xha.ProjectRoundUserID = " + userID + " " +

                            "AND av.QuestionID = " + rs.GetInt32(2) + " " +
                            "AND av.OptionID = " + rs.GetInt32(3) + " " +
                            "GROUP BY a.ProjectRoundUserID, YEAR(a.EndDT), MONTH(a.EndDT) " +
                            ") tmp GROUP BY tmp.dtyr, tmp.dtmt ORDER BY tmp.dtyr, tmp.dtmt", "eFormSqlConnection");
                        while (rs2.Read())
                        {
                            if (oneDay)
                            {
                                compareValue = (float)Convert.ToDouble(rs2.GetValue(2));
                                g.drawStepLine(18, 0, compareValue, steps + 1, compareValue);
                            }
                            else
                            {
                                int pos = ((TimeSpan)((new DateTime(rs2.GetInt32(0), rs2.GetInt32(1), 15)).Date - FDT)).Days + 1;
                                if (pos < 0) { pos = 0; }
                                if (pos > steps + 1) { pos = steps + 1; }
                                compareValue = (float)Convert.ToDouble(rs2.GetValue(2));
                                if (lastCompareValue != -1f)
                                {
                                    g.drawStepLine(18, lastComparePosition, lastCompareValue, pos, compareValue);
                                }
                                lastComparePosition = pos;
                                lastCompareValue = compareValue;
                            }
                        }
                        rs2.Close();
                    }

                    if (lastVal != -1f && !compare)
                    {
                        g.drawColorExplBox(rs.GetString(1), Convert.ToInt32(so[bx + 1]) + 3, 100 + (140 * (bx % 3)), (so.Length > 4 ? (so.Length > 7 ? 5 : 10) : 15) + (13 * Convert.ToInt32(Math.Floor((float)bx / 3))));
                    }
                    else if (compare)
                    {
                        axisDesc = rs.GetString(1);
                        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                        {
                            case 1:
                                g.drawColorExplBox("Mina värden", 19, 140, 15);
                                g.drawColorExplBox("Samma profil", 18, 260, 15);
                                g.drawColorExplBox("Hela databasen", 28, 380, 15);
                                break;
                            case 2:
                                g.drawColorExplBox("My values", 19, 140, 15);
                                g.drawColorExplBox("Same profile", 18, 260, 15);
                                g.drawColorExplBox("Complete database", 28, 380, 15);
                                break;
                        }
                    }

                    bx++;
                }
                rs.Close();
                #endregion

                g.drawAxisExpl(axisDesc, 0, false, false);
                g.render();
            }

            #region group stats
            /*
				string sortString = "";
				int langID = 0;
				rs = Db.recordSet("SELECT SortString, dbo.cf_unitLangID(ProjectRoundUnitID) FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + HttpContext.Current.Request.QueryString["PRUID"]);
				if (rs.Read())
				{
					sortString = rs.GetString(0);
					langID = rs.GetInt32(1);
				}
				rs.Close();

				if (type == 1)
				{
					g = new Graph(895, 550, "#FFFFFF");
					g.setMinMax(0f, 100f);

					rs = Db.recordSet("SELECT COUNT(*) FROM OptionComponents WHERE OptionID = " + o);
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

					rs = Db.recordSet("SELECT COUNT(*) FROM ProjectRoundUnit pru " +
						"WHERE LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'");
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

					rs = Db.recordSet("SELECT " +
						"" + groupBy + "(MAX(a.EndDT)) - " + groupBy + "(MIN(a.EndDT))" +
						"FROM Answer a " +
						"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
						"INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
						"WHERE a.EndDT IS NOT NULL " +
						"AND a.EndDT >= pr.Started " +
						"AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'");
					if (rs.Read())
					{
						cx = Convert.ToInt32(rs.GetValue(0)) + 3;
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
					while (rs.Read())
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
						if (rs2.Read())
						{
							g.setMinMax((float)Convert.ToDouble(rs2.GetValue(1)), (float)Convert.ToDouble(rs2.GetValue(0)));
							//g.roundMinMax();
							g.computeMinMax(0.1f, 0.1f);
						}
						else
						{
							g.setMinMax(0f, 100f);
						}
						rs2.Close();
					}
					rs.Close();

					if (g.minVal != 0f)
					{
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
				g.computeSteping(steps);
				g.drawOutlines(11);
				g.drawAxis();

				cx = 0;

				if (type == 1)
				{
					#region Question

					decimal tot = 0;
					decimal sum = 0;

					rs = Db.recordSet("SELECT COUNT(*) FROM Answer a " +
						"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
						"WHERE a.EndDT IS NOT NULL AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'");
					if (rs.Read())
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

						rs = Db.recordSet("SELECT oc.OptionComponentID, ocl.Text FROM OptionComponents ocs " +
							"INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
							"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = 1 " +
							"WHERE ocs.OptionID = " + o + " ORDER BY ocs.SortOrder");
						while (rs.Read())
						{
							cx++;

							OdbcDataReader rs2 = Db.recordSet("SELECT COUNT(*) FROM Answer a " +
								"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
								"INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
								"WHERE a.EndDT IS NOT NULL AND av.ValueInt = " + rs.GetInt32(0) + " " +
								"AND av.OptionID = " + o + " " +
								"AND av.QuestionID = " + q + " " +
								"AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'");
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
					while (rs.Read())
					{
						System.Collections.SortedList all = new System.Collections.SortedList();

						OdbcDataReader rs2 = Db.recordSet("SELECT dbo.cf_projectUnitTree(pru.ProjectRoundUnitID,' » '), SortString FROM ProjectRoundUnit pru " +
							"WHERE LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'");
						while (rs2.Read())
						{
							res = new System.Collections.Hashtable();

							if (rs.GetInt32(1) == 0)
							{
								getIdxVal(rs.GetInt32(0), rs2.GetString(1));
							}
							else
							{
								getOtherIdxVal(rs.GetInt32(0), rs2.GetString(1));
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
					while (rs.Read())
					{
						if (rs.GetInt32(1) == 0)
						{
							getIdxVal(rs.GetInt32(0), sortString);
						}
						else
						{
							getOtherIdxVal(rs.GetInt32(0), sortString);
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
					if (HttpContext.Current.Request.QueryString["TRID"] != null)
					{
						int COUNT = 0;
						OdbcDataReader rs3 = Db.recordSet("SELECT COUNT(*) FROM TempReportComponent WHERE TempReportID = " + HttpContext.Current.Request.QueryString["TRID"]);
						if (rs3.Read())
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
						if (rs.Read())
						{
							int bx = 0;

							rs3 = Db.recordSet("SELECT TempReportComponentID, TempReportComponent FROM TempReportComponent WHERE TempReportID = " + HttpContext.Current.Request.QueryString["TRID"]);
							while (rs3.Read())
							{
								if (bx == 0)
								{
									g.drawAxis(false);
									g.drawAxisExpl((langID == 1 ? "Medelvärde" : "Mean value") + " " + HttpUtility.HtmlDecode("&plusmn;") + "SD", 0, false, false);
								}
								g.drawColorExplBox(rs3.GetString(1), bx + 4, 130 + (int)((bx % 6) * 120), 15 + (int)Math.Ceiling(bx / 6) * 15);
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
								while (rs2.Read())
								{
									if (lastDT == 0)
										lastDT = rs2.GetInt32(0);

									while (lastDT + 1 < rs2.GetInt32(0))
									{
										lastDT++;
										cx++;
									}

									if (rs2.GetInt32(2) >= rac)
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
													string v = "v" + w + "-" + (w + 1) + ", " + ((d + 1) - (d % 52)) / 52 + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
													g.drawBottomString(v, cx, true);
													break;
												}
										}

										float newVal = (rs2.IsDBNull(1) ? -1f : (float)Convert.ToDouble(rs2.GetValue(1)));
										float newStd = (rs2.IsDBNull(3) ? -1f : (float)Convert.ToDouble(rs2.GetValue(3)));

										g.drawLine(bx + 4, cx * g.steping - 10, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping + 10, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
										g.drawLine(20, cx * g.steping, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
										g.drawLine(bx + 4, cx * g.steping - 10, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping + 10, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);

										if (lastVal != -1f && newVal != -1f)
										{
											g.drawStepLine(bx + 4, lastCX, lastVal, cx, newVal, 1);
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
						while (rs.Read() && bx <= 1)
						{
							if (bx == 0)
							{
								g.drawAxisExpl(rs.GetString(1) + ", " + (langID == 1 ? "medelvärde" : "mean value") + " " + HttpUtility.HtmlDecode("&plusmn;") + "SD", bx + 4, false, true);
								g.drawAxis(false);
							}
							else
							{
								g.drawAxisExpl(rs.GetString(1) + ", " + (langID == 1 ? "medelvärde" : "mean value") + " " + HttpUtility.HtmlDecode("&plusmn;") + "SD", bx + 4, true, true);
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
							while (rs2.Read())
							{
								if (lastDT == 0)
									lastDT = rs2.GetInt32(0);

								while (lastDT + 1 < rs2.GetInt32(0))
								{
									lastDT++;
									cx++;
								}

								if (rs2.GetInt32(2) >= rac)
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
												string v = "v" + w + "-" + (w + 1) + ", " + ((d + 1) - (d % 52)) / 52 + ", n = " + rs2.GetInt32(2);
												g.drawBottomString(v, cx, true);
												break;
											}
									}

									float newVal = (rs2.IsDBNull(1) ? -1f : (float)Convert.ToDouble(rs2.GetValue(1)));
									float newStd = (rs2.IsDBNull(3) ? -1f : (float)Convert.ToDouble(rs2.GetValue(3)));

									g.drawLine(20, cx * g.steping - 10, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping + 10, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
									g.drawLine(20, cx * g.steping, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
									g.drawLine(20, cx * g.steping - 10, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping + 10, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);

									if (lastVal != -1f && newVal != -1f)
									{
										g.drawStepLine(bx + 4, lastCX, lastVal, cx, newVal, 3);
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
				*/
            #endregion
        }
    }
}