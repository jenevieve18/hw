using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HW.Core.FromHW;

namespace HW
{
    public partial class linechart : System.Web.UI.Page
    {
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
            string rp = "0";
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            for (int i = 0; i < 4; i++)
            {
                if (HttpContext.Current.Request.QueryString["RP" + i] != null)
                {
                    int ii = Convert.ToInt32(HttpContext.Current.Request.QueryString["RP" + i]);
                    if (ii != 0)
                    {
                        ht.Add(ii, i);
                        rp += "," + ii;
                    }
                }
            }
            //foreach (string s in ("0," + HttpContext.Current.Request.QueryString["RPID"].ToString()).Split(','))
            //{
            //    try
            //    {
            //        rp += (rp != "" ? "," : "") + Convert.ToInt32(s).ToString();
            //    }
            //    catch (Exception) { }
            //}
            //string[] so = ("0," + HttpContext.Current.Request.QueryString["RPO"].ToString()).Split(',');

            int SID = (HttpContext.Current.Request.QueryString["SID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["SID"]) : 1);
            int LID = (HttpContext.Current.Request.QueryString["LID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["LID"]) : 1);
            int UID = (HttpContext.Current.Request.QueryString["UID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["UID"]) : 0);

            bool compare = (HttpContext.Current.Request.QueryString["C"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["C"]) == 2);
            bool sponsored = (SID > 1);
            DateTime TDT = DateTime.ParseExact(HttpContext.Current.Request.QueryString["TDT"].ToString(), "yyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            DateTime FDT = DateTime.ParseExact(HttpContext.Current.Request.QueryString["FDT"].ToString(), "yyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            //string answer = "";
            //if (HttpContext.Current.Request.QueryString["E"] != null)
            //{
            //    foreach (string s in ("0," + HttpContext.Current.Request.QueryString["E"].ToString()).Split(','))
            //    {
            //        try
            //        {
            //            answer += (answer != "" ? "," : "") + Convert.ToInt32(s).ToString();
            //        }
            //        catch (Exception) { }
            //    }
            //    answer = "NOT IN (" + answer + ")";
            //}
            //else
            //{
            //    foreach (string s in ("0," + HttpContext.Current.Request.QueryString["I"].ToString()).Split(','))
            //    {
            //        try
            //        {
            //            answer += (answer != "" ? "," : "") + Convert.ToInt32(s).ToString();
            //        }
            //        catch (Exception) { }
            //    }
            //    answer = "IN (" + answer + ")";
            //}
            #endregion
            #region Count answers, get min/max date and calculate steps
            //int cx = 0, userID = 0;
            SqlDataReader rs;
            //rs = Db.rs("SELECT " +
            //    "COUNT(*), " +
            //    "MIN(DT), " +
            //    "MAX(DT) " +
            //"uprua.ProjectRoundUserID " +
            //"FROM UserProjectRoundUser upru " +
            //"INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
            //"WHERE upru.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " " +
            //"AND upru.ProjectRoundUnitID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["S"]) + " " +
            //"AND uprua.DT < '" + TDT.AddDays(1).ToString("yyyy-MM-dd") + "' " +
            //"AND uprua.DT >= '" + FDT.ToString("yyyy-MM-dd") + "' " +
            //"AND uprua.AnswerID " + answer + " " +
            //"GROUP BY uprua.ProjectRoundUserID" +
            //    "");
            //if (rs.Read())
            //{
            //cx = rs.GetInt32(0);
            //FDT = rs.GetDateTime(1).Date;
            //TDT = rs.GetDateTime(2).Date;
            //userID = rs.GetInt32(3);
            //}
            //rs.Close();

            //bool oneDay = false;
            int steps = ((TimeSpan)(TDT - FDT)).Days + 1;
            //if (steps == 1 && cx > 1)
            //{
            //    oneDay = true;
            //    steps = 24 * 6;
            //}
            #endregion

            #region removed
            //if (cx <= 1)
            //{
            //    #region Bars
            //    Graph g = new Graph(520, (rp.Split(',').Length < 5 ? 350 : 440), "#FFFFFF");
            //    g.setMinMax(0f, 100f);
            //    if (compare && sponsored && 1 == 0)
            //    {
            //        g.computeSteping(6);
            //        g.drawOutlines(6);
            //    }
            //    else if (compare)
            //    {
            //        g.computeSteping(5);
            //        g.drawOutlines(5);
            //    }
            //    else
            //    {
            //        g.computeSteping(rp.Split(',').Length + 1);
            //        g.drawOutlines(rp.Split(',').Length + 1);
            //    }
            //    g.drawAxis();

            //    int pos = 1;

            //    bool hasReference = false;
            //    bool hasColors = false;

            //    rs = Db.rs("SELECT " +
            //        "rpc.WeightedQuestionOptionID, " +	// 0
            //        "wqol.WeightedQuestionOption, " +
            //        "wqo.TargetVal, " +
            //        "wqo.YellowLow, " +
            //        "wqo.GreenLow, " +
            //        "wqo.GreenHigh, " +					// 5
            //        "wqo.YellowHigh, " +
            //        "wqo.QuestionID, " +
            //        "wqo.OptionID " +
            //        "FROM Report r " +
            //        "INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID " +
            //        "INNER JOIN ReportPartComponent rpc ON rp.ReportPartID = rpc.ReportPartID " +
            //        "INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
            //        "INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
            //        "WHERE rpc.ReportPartID IN (" + rp + ") " +
            //        "ORDER BY rp.SortOrder, rpc.SortOrder", "eFormSqlConnection");
            //    while (rs.Read())
            //    {
            //        SqlDataReader rs2 = Db.rs("SELECT " +
            //            "av.ValueInt, " +
            //            "a.EndDT, " +
            //            "a.AnswerID " +
            //            "FROM Answer a " +

            //            "INNER JOIN healthWatch..UserProjectRoundUserAnswer ha ON a.AnswerID = ha.AnswerID AND ha.ProjectRoundUserID = a.ProjectRoundUserID " +

            //            "LEFT OUTER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
            //            "AND av.DeletedSessionID IS NULL " +
            //            "AND av.QuestionID = " + rs.GetInt32(7) + " " +
            //            "AND av.OptionID = " + rs.GetInt32(8) + " " +
            //            "WHERE a.EndDT IS NOT NULL " +
            //            "AND a.EndDT < '" + TDT.AddDays(1).ToString("yyyy-MM-dd") + "' " +
            //            "AND a.EndDT >= '" + FDT.ToString("yyyy-MM-dd") + "' " +
            //            "AND a.AnswerID " + answer + " " +
            //            "AND a.ProjectRoundUserID = " + userID, "eFormSqlConnection");
            //        if (rs2.Read())
            //        {
            //            int color = 19;
            //            if (!compare && !rs2.IsDBNull(0))
            //            {
            //                color = 30;

            //                if (!rs.IsDBNull(3))
            //                {
            //                    hasColors = true;

            //                    if (rs.GetInt32(3) >= 0 && rs.GetInt32(3) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(3))
            //                    {
            //                        color = 1;
            //                    }
            //                }
            //                if (!rs.IsDBNull(4))
            //                {
            //                    hasColors = true;

            //                    if (rs.GetInt32(4) >= 0 && rs.GetInt32(4) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(4))
            //                    {
            //                        color = 0;
            //                    }
            //                }
            //                if (!rs.IsDBNull(5))
            //                {
            //                    hasColors = true;

            //                    if (rs.GetInt32(5) >= 0 && rs.GetInt32(5) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(5))
            //                    {
            //                        color = 1;
            //                    }
            //                }
            //                if (!rs.IsDBNull(6))
            //                {
            //                    hasColors = true;

            //                    if (rs.GetInt32(6) >= 0 && rs.GetInt32(6) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(6))
            //                    {
            //                        color = 2;
            //                    }
            //                }
            //                if (hasColors && color == 30)
            //                {
            //                    color = 2;
            //                }
            //            }

            //            g.drawBar(color, pos, (rs2.IsDBNull(0) ? -1 : rs2.GetInt32(0)), true, false);
            //            g.drawAxisExpl((compare ? rs.GetString(1) + ", " : "") + rs2.GetDateTime(1).ToString("yyyy-MM-dd HH:mm"), 0, false, false);

            //            if (compare)
            //            {
            //                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            //                {
            //                    case 1:
            //                        g.drawBottomString("Mitt värde", pos, false);
            //                        g.drawBar(18, ++pos, getProfileVal(rs.GetInt32(7), rs.GetInt32(8), rs2.GetInt32(2), userID, rs2.GetDateTime(1)), true, false);
            //                        g.drawBottomString("Samma profil", pos, false);
            //                        g.drawBar(28, ++pos, getDatabaseVal(rs.GetInt32(7), rs.GetInt32(8), rs2.GetDateTime(1)), true, false);
            //                        g.drawBottomString("Hela databasen", pos, false);

            //                        break;
            //                    case 2:
            //                        g.drawBottomString("My value", pos, false);
            //                        g.drawBar(18, ++pos, getProfileVal(rs.GetInt32(7), rs.GetInt32(8), rs2.GetInt32(2), userID, rs2.GetDateTime(1)), true, false);
            //                        g.drawBottomString("Same profile", pos, false);
            //                        g.drawBar(28, ++pos, getDatabaseVal(rs.GetInt32(7), rs.GetInt32(8), rs2.GetDateTime(1)), true, false);
            //                        g.drawBottomString("Complete database", pos, false);
            //                        break;
            //                }
            //            }
            //        }
            //        rs2.Close();

            //        if (!rs.IsDBNull(2))
            //        {
            //            hasReference = true;
            //            g.drawReference(pos, rs.GetInt32(2));
            //        }
            //        if (!compare)
            //        {
            //            g.drawBottomString(rs.GetString(1), pos, !(rp.Split(',').Length < 5));
            //        }
            //        pos++;
            //    }
            //    rs.Close();

            //    if (hasReference)
            //    {
            //        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            //        {
            //            case 1:
            //                g.drawReference(433, 10, " = riktvärde");
            //                break;
            //            case 2:
            //                g.drawReference(433, 10, " = target value");
            //                break;
            //        }
            //    }

            //    if (hasColors)
            //    {
            //        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            //        {
            //            case 1:
            //                g.drawColorExplBox("Hälsosam nivå", 0, 150, 30);
            //                g.drawColorExplBox("Förbättringsbehov", 1, 270, 30);
            //                g.drawColorExplBox("Ohälsosam nivå", 2, 400, 30);
            //                break;
            //            case 2:
            //                g.drawColorExplBox("Healthy level", 0, 150, 30);
            //                g.drawColorExplBox("Improvement needed", 1, 270, 30);
            //                g.drawColorExplBox("Unhealthy level", 2, 400, 30);
            //                break;
            //        }
            //    }

            //    #endregion

            //    g.render();
            //}
            //else if (cx > 1)
            //{
            #endregion
            #region Line
            Chart g = new Chart(932, 330, "#f7f7f7");
            g.setMinMax(0f, 100f);

            //if (oneDay)
            //{
            //    g.computeSteping(steps);
            //    g.drawOutlines(steps, false);

            //    g.drawBottomString("00:00", 0);
            //    g.drawBottomString("06:00", 6 * 6);
            //    g.drawBottomString("12:00", 12 * 6);
            //    g.drawBottomString("18:00", 18 * 6);
            //    g.drawBottomString("23:59", 24 * 6);
            //}
            //else
            //{
            g.computeSteping(steps + 2);
            g.drawOutlines(steps + 2, false);
            g.drawOutlinesRight();

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
            //}
            g.drawAxis();

            int bx = 0, profileComparisonID = 0;//, answerID = 0;

            //string axisDesc = "Resultat";
            rs = Db.rs("SELECT " +
                "rpc.WeightedQuestionOptionID, " +	// 0
                "wqol.WeightedQuestionOption, " +
                "wqo.QuestionID, " +
                "wqo.OptionID, " +
                "rpc.ReportPartID " +
                "FROM Report r " +
                "INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID " +
                "INNER JOIN ReportPartComponent rpc ON rp.ReportPartID = rpc.ReportPartID " +
                "INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
                "INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + LID + " " +
                "WHERE rpc.ReportPartID IN (" + rp + ") " +
                "ORDER BY rp.SortOrder, rpc.SortOrder", "eFormSqlConnection");
            while (rs.Read())
            {
                float lastVal = -1f;
                int lastPos = 0;
                SqlDataReader rs2;

                rs2 = Db.rs("SELECT " +
                    "a.EndDT, " +
                    "av.ValueInt, " +
                    "a.AnswerID, " +
                    "u.ProfileComparisonID " +
                    "FROM Answer a " +
                    "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +

                    "INNER JOIN healthWatch..UserProjectRoundUserAnswer ha ON a.AnswerID = ha.AnswerID AND ha.ProjectRoundUserID = a.ProjectRoundUserID " +
                    "INNER JOIN healthWatch..UserProfile u ON ha.UserProfileID = u.UserProfileID " +

                    "WHERE av.DeletedSessionID IS NULL " +
                    "AND av.ValueInt IS NOT NULL " +
                    "AND a.EndDT IS NOT NULL " +
                    "AND a.EndDT < '" + TDT.AddDays(1).ToString("yyyy-MM-dd") + "' " +
                    "AND a.EndDT >= '" + FDT.ToString("yyyy-MM-dd") + "' " +
                    //"AND a.AnswerID " + answer + " " +
                    //"AND a.ProjectRoundUserID = " + userID + " " +
                    "AND u.UserID = " + UID + " " +
                    "AND av.QuestionID = " + rs.GetInt32(2) + " " +
                    "AND av.OptionID = " + rs.GetInt32(3) + " " +
                    "ORDER BY a.EndDT", "eFormSqlConnection");
                while (rs2.Read())
                {
                    //answerID = rs2.GetInt32(2);
                    profileComparisonID = rs2.GetInt32(3);

                    //if (lastPos == 0 && oneDay)
                    //{
                    //    g.drawStringInGraph(rs2.GetDateTime(0).ToString("yyyy-MM-dd"), 195, 280);
                    //}
                    //int pos = (oneDay ? rs2.GetDateTime(0).Hour * 6 + Convert.ToInt32(rs2.GetDateTime(0).Minute / 10) : ((TimeSpan)(rs2.GetDateTime(0).Date - FDT)).Days + 1);
                    int pos = ((TimeSpan)(rs2.GetDateTime(0).Date - FDT)).Days + 1;
                    float newVal = (float)Convert.ToDouble(rs2.GetInt32(1));
                    int color = Convert.ToInt32(ht[rs.GetInt32(4)]) + 4;
                    if (lastVal != -1f)
                    {
                        g.drawStepLine(color, lastPos, lastVal, pos, newVal);
                    }
                    g.drawDot(color, pos, newVal, lastPos, lastVal);
                    lastPos = pos;
                    lastVal = newVal;
                }
                rs2.Close();

                if (compare)
                {
                    float lastCompareValue = -1f;
                    int lastComparePosition = 0;
                    lastVal = -1f;
                    lastPos = 0;
                    System.Collections.ArrayList a = new System.Collections.ArrayList();

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
                        "AND a.EndDT < '" + TDT.AddMonths(2).ToString("yyyy-MM") + "-01' " +
                        "AND a.EndDT >= '" + FDT.AddMonths(-1).ToString("yyyy-MM") + "-01' " +
                        "AND av.QuestionID = " + rs.GetInt32(2) + " " +
                        "AND av.OptionID = " + rs.GetInt32(3) + " " +
                        "GROUP BY a.ProjectRoundUserID, YEAR(a.EndDT), MONTH(a.EndDT) " +
                        ") tmp GROUP BY tmp.dtyr, tmp.dtmt ORDER BY tmp.dtyr, tmp.dtmt", "eFormSqlConnection");
                    while (rs2.Read())
                    {
                        //if (oneDay)
                        //{
                        //    compareValue = (float)Convert.ToDouble(rs2.GetValue(2));
                        //    g.drawStepLine(28, 0, compareValue, steps + 1, compareValue);
                        //}
                        //else
                        //{
                        DateTime dt = new DateTime(rs2.GetInt32(0), rs2.GetInt32(1), 15);
                        if (dt < FDT) { dt = FDT; }
                        if (dt > TDT) { dt = TDT; }
                        int pos = ((TimeSpan)(dt - FDT)).Days + 1;

                        if (!a.Contains(pos))
                        {
                            float compareValue = (float)Convert.ToDouble(rs2.GetValue(2));
                            int color = Convert.ToInt32(ht[rs.GetInt32(4)]) + 4;
                            if (lastCompareValue != -1f)
                            {
                                g.drawStepLineBroken(color, lastComparePosition, lastCompareValue, pos, compareValue);
                            }
                            g.drawSquare(color, pos, compareValue, lastPos, lastVal);
                            lastComparePosition = pos;
                            lastCompareValue = compareValue;
                            a.Add(pos);
                        }
                        //}
                    }
                    rs2.Close();

                    //    lastCompareValue = -1f; compareValue = -1f;
                    //    lastComparePosition = 0;

                    //    rs2 = Db.rs("SELECT " +
                    //        "tmp.dtyr, " +
                    //        "tmp.dtmt, " +
                    //        "AVG(tmp.val) " +
                    //        "FROM (" +
                    //        "SELECT " +
                    //        "YEAR(a.EndDT) AS dtyr, " +
                    //        "MONTH(a.EndDT) AS dtmt, " +
                    //        "AVG(av.ValueInt) as val, " +
                    //        "a.ProjectRoundUserID " +
                    //        "FROM Answer a " +

                    //        "INNER JOIN healthWatch..UserProjectRoundUserAnswer ha ON a.AnswerID = ha.AnswerID AND ha.ProjectRoundUserID = a.ProjectRoundUserID " +
                    //        "INNER JOIN healthWatch..UserProfile hu ON ha.UserProfileID = hu.UserProfileID " +
                    //        //"INNER JOIN healthWatch..UserProfile xhu ON hu.ProfileComparisonID = xhu.ProfileComparisonID " +
                    //        //"INNER JOIN healthWatch..UserProjectRoundUserAnswer xha ON xhu.UserProfileID = xha.UserProfileID " +

                    //        "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
                    //        "WHERE av.DeletedSessionID IS NULL " +
                    //        "AND av.ValueInt IS NOT NULL " +
                    //        "AND a.EndDT IS NOT NULL " +
                    //        "AND a.EndDT < '" + TDT.AddDays(1).ToString("yyyy-MM") + "-01' " +
                    //        "AND a.EndDT >= '" + FDT.AddMonths(1).ToString("yyyy-MM") + "-01' " +

                    //        //"AND xha.AnswerID = " + answerID + " " +
                    //        //"AND xha.ProjectRoundUserID = " + userID + " " +
                    //        "AND hu.ProfileComparisonID = " + profileComparisonID + " " +

                    //        "AND av.QuestionID = " + rs.GetInt32(2) + " " +
                    //        "AND av.OptionID = " + rs.GetInt32(3) + " " +
                    //        "GROUP BY a.ProjectRoundUserID, YEAR(a.EndDT), MONTH(a.EndDT) " +
                    //        ") tmp GROUP BY tmp.dtyr, tmp.dtmt ORDER BY tmp.dtyr, tmp.dtmt", "eFormSqlConnection");
                    //    while (rs2.Read())
                    //    {
                    //        //if (oneDay)
                    //        //{
                    //        //    compareValue = (float)Convert.ToDouble(rs2.GetValue(2));
                    //        //    g.drawStepLine(18, 0, compareValue, steps + 1, compareValue);
                    //        //}
                    //        //else
                    //        //{
                    //            int pos = ((TimeSpan)((new DateTime(rs2.GetInt32(0), rs2.GetInt32(1), 15)).Date - FDT)).Days + 1;
                    //            if (pos < 0) { pos = 0; }
                    //            if (pos > steps + 1) { pos = steps + 1; }
                    //            compareValue = (float)Convert.ToDouble(rs2.GetValue(2));
                    //            if (lastCompareValue != -1f)
                    //            {
                    //                g.drawStepLine(18, lastComparePosition, lastCompareValue, pos, compareValue);
                    //            }
                    //            lastComparePosition = pos;
                    //            lastCompareValue = compareValue;
                    //        //}
                    //    }
                    //    rs2.Close();
                    //}

                    //if (lastVal != -1f && !compare)
                    //{
                    //    g.drawColorExplBox(rs.GetString(1), Convert.ToInt32(so[bx + 1]) + 3, 100 + (140 * (bx % 3)), (so.Length > 4 ? (so.Length > 7 ? 5 : 10) : 15) + (13 * Convert.ToInt32(Math.Floor((float)bx / 3))));
                    //}
                    //else if (compare)
                    //{
                    //    axisDesc = rs.GetString(1);
                    //    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    //    {
                    //        case 1:
                    //            g.drawColorExplBox("Mina värden", 19, 140, 15);
                    //            g.drawColorExplBox("Samma profil", 18, 260, 15);
                    //            g.drawColorExplBox("Hela databasen", 28, 380, 15);
                    //            break;
                    //        case 2:
                    //            g.drawColorExplBox("My values", 19, 140, 15);
                    //            g.drawColorExplBox("Same profile", 18, 260, 15);
                    //            g.drawColorExplBox("Complete database", 28, 380, 15);
                    //            break;
                    //    }
                }

                bx++;
            }
            rs.Close();
            #endregion

            //g.drawAxisExpl(axisDesc, 0, false, false);
            g.render();
            //}
        }
    }
}