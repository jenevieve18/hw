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
    public partial class calendarGraphOverTime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int h = 300, w = 378;
            if (HttpContext.Current.Request.QueryString["W"] != null && HttpContext.Current.Request.QueryString["H"] != null)
            {
                w = Convert.ToInt32(HttpContext.Current.Request.QueryString["W"].ToString());
                h = Convert.ToInt32(HttpContext.Current.Request.QueryString["H"].ToString());
            }

            Graph g = new Graph(w, h, "#FFFFFF");
            g.maxH = h - 100;
            g.vertLines = (h > 300 ? 11 : 5);

            string leftHeader = "";
            string leftUnit = "";
            string rightHeader = "";
            string rightUnit = "";
            string measure = "";
            bool left = true;
            int valCX = 0;

            int MUID = Convert.ToInt32(HttpContext.Current.Request.QueryString["MUID"]);

            if (HttpContext.Current.Request.QueryString["T"] != null && HttpContext.Current.Request.QueryString["T"].ToString() == "1")
            {
                DateTime TDT = (HttpContext.Current.Request.QueryString["TDT"] != null ? DateTime.ParseExact(HttpContext.Current.Request.QueryString["TDT"].ToString(), "yyMMdd", System.Globalization.CultureInfo.CurrentCulture) : DateTime.Now.Date);
                DateTime FDT = (HttpContext.Current.Request.QueryString["FDT"] != null ? DateTime.ParseExact(HttpContext.Current.Request.QueryString["FDT"].ToString(), "yyMMdd", System.Globalization.CultureInfo.CurrentCulture) : DateTime.Now.Date);

                int timeCX = 0, steps = 0, typeCX = 0;

                SqlDataReader rs = Db.rs("SELECT " +
                    "MIN(um.DT), " +
                    "MAX(um.DT), " +
                    "NULL, " +
                    "COUNT(DISTINCT um.UserMeasureID), " +
                    "COUNT(DISTINCT z.MeasureComponentID), " +
                    "MIN(m.Measure), " +
                    "MIN(umc.ValInt), " +
                    "MAX(umc.ValInt), " +
                    "MIN(umc.ValDec), " +
                    "MAX(umc.ValDec) " +
                    "FROM UserMeasure x " +
                    "INNER JOIN UserMeasureComponent y ON x.UserMeasureID = y.UserMeasureID " +
                    "INNER JOIN MeasureComponent z ON y.MeasureComponentID = z.MeasureComponentID " +
                    "INNER JOIN Measure m ON z.MeasureID = m.MeasureID " +
                    "INNER JOIN UserMeasureComponent umc ON z.MeasureComponentID = umc.MeasureComponentID " +
                    "INNER JOIN UserMeasure um ON umc.UserMeasureID = um.UserMeasureID " +
                    "WHERE x.UserMeasureID = " + MUID + " " +
                    "AND um.DeletedDT IS NULL " +
                    "AND um.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]));
                if (rs.Read() && !rs.IsDBNull(0) && !rs.IsDBNull(1) && !rs.IsDBNull(8) && !rs.IsDBNull(9))
                {
                    FDT = rs.GetDateTime(0).Date;
                    TDT = rs.GetDateTime(1).Date;
                    steps = ((TimeSpan)(TDT - FDT)).Days + 1;
                    g.setMinMax((float)Convert.ToDouble(rs.GetDecimal(8)), (float)Convert.ToDouble(rs.GetDecimal(9)));

                    valCX = rs.GetInt32(3);
                    typeCX = rs.GetInt32(4);
                    measure = rs.GetString(5);
                }
                rs.Close();

                g.computeMinMax(0.1f, 0.1f);
                g.computeSteping(steps + 2);
                g.drawOutlines(steps + 2, false, (typeCX > 1), 0, 100);
                if (typeCX > 1)
                {
                    g.drawOutlinesRight();
                }

                g.drawBottomString(FDT.ToString("yyyy-MM-dd"), 1);
                if (steps > 1)
                {
                    g.drawBottomString(TDT.ToString("yyyy-MM-dd"), steps);
                }
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

                int MCID = 0;
                float maxRight = 0;
                float maxLeft = 0;
                float lastVal = -1f, compareVal = -1f;
                int lastPos = 0, bx = 0;
                rs = Db.rs("SELECT " +
                    "z.MeasureComponentID, " +
                    "ISNULL(mcl.MeasureComponent,z.MeasureComponent), " +
                    "ISNULL(mcl.Unit,z.Unit), " +
                    "z.Type, " +
                    "um.DT, " +
                    "umc.ValInt, " +
                    "umc.ValDec, " +
                    "um.UserMeasureID, " +
                    "z.MeasureComponent " +
                    "FROM UserMeasure x " +
                    "INNER JOIN UserMeasureComponent y ON x.UserMeasureID = y.UserMeasureID " +
                    "INNER JOIN MeasureComponent z ON y.MeasureComponentID = z.MeasureComponentID " +
                    "LEFT OUTER JOIN MeasureComponentLang mcl ON z.MeasureComponentID = mcl.MeasureComponentID AND mcl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                    "INNER JOIN UserMeasureComponent umc ON z.MeasureComponentID = umc.MeasureComponentID " +
                    "INNER JOIN UserMeasure um ON umc.UserMeasureID = um.UserMeasureID " +
                    "WHERE z.ShowInGraph = 1 " +
                    "AND x.UserMeasureID = " + MUID + " " +
                    "AND um.DeletedDT IS NULL " +
                    "AND um.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " " +
                    "ORDER BY z.SortOrder ASC, um.DT ASC, umc.UserMeasureComponentID");
                while (rs.Read())
                {
                    if (MCID == 0)
                    {
                        leftHeader = rs.GetString(8);
                        leftUnit = rs.GetString(2);
                    }
                    else if (MCID != rs.GetInt32(0))
                    {
                        rightHeader = rs.GetString(8);
                        rightUnit = rs.GetString(2);
                        left = false;
                        lastVal = -1f;
                        lastPos = 0;
                    }

                    int pos = ((TimeSpan)(rs.GetDateTime(4).Date - FDT)).Days + 1;
                    float newVal = (float)Convert.ToDouble(rs.GetDecimal(6));
                    if (lastVal != -1f)
                    {
                        g.drawStepLine((left ? 1 : 2), lastPos, lastVal, pos, newVal);
                    }
                    g.drawDot((left ? 1 : 2), pos, newVal, lastPos, lastVal, 3); //(MUID == rs.GetInt32(7) ? 16 : 30)
                    lastPos = pos;
                    lastVal = newVal;
                    MCID = rs.GetInt32(0);
                }
                rs.Close();
            }
            else
            {
                int timeS = 0, timeE = 0, timeCX = 0, steps = 0, typeCX = 0;

                SqlDataReader rs = Db.rs("SELECT " +
                    "MIN(dbo.cf_hourMinutes(um.DT)), " +
                    "MAX(dbo.cf_hourMinutes(um.DT)), " +
                    "COUNT(DISTINCT dbo.cf_hourMinutes(um.DT)), " +
                    "COUNT(DISTINCT um.UserMeasureID), " +
                    "COUNT(DISTINCT z.MeasureComponentID), " +
                    "MIN(m.Measure) " +
                    "FROM UserMeasure x " +
                    "INNER JOIN UserMeasureComponent y ON x.UserMeasureID = y.UserMeasureID " +
                    "INNER JOIN MeasureComponent z ON y.MeasureComponentID = z.MeasureComponentID " +
                    "INNER JOIN Measure m ON z.MeasureID = m.MeasureID " +
                    "INNER JOIN UserMeasureComponent umc ON z.MeasureComponentID = umc.MeasureComponentID " +
                    "INNER JOIN UserMeasure um ON umc.UserMeasureID = um.UserMeasureID " +
                    "WHERE x.UserMeasureID = " + MUID + " " +
                    "AND dbo.cf_yearMonthDay(um.DT) = dbo.cf_yearMonthDay(x.DT) " +
                    "AND um.DeletedDT IS NULL " +
                    "AND um.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]));
                if (rs.Read() && !rs.IsDBNull(0))
                {
                    timeS = rs.GetInt32(0) - rs.GetInt32(0) % 60;
                    timeE = (rs.GetInt32(1) + 60) - rs.GetInt32(1) % 60;
                    timeCX = rs.GetInt32(2);
                    valCX = rs.GetInt32(3);
                    typeCX = rs.GetInt32(4);
                    measure = rs.GetString(5);
                }
                rs.Close();

                if (timeS != 0)
                {
                    if (valCX == 1)
                    {
                        steps = 3;
                        g.computeSteping(steps);
                    }
                    else
                    {
                        steps = (timeE - timeS) / 5 + 1;
                        g.computeSteping(steps);

                        g.drawBottomString((timeS / 60).ToString().PadLeft(2, '0') + ":" + (timeS % 60).ToString().PadLeft(2, '0'), 0);

                        if (steps <= 2)
                        {
                            g.drawBottomString((timeE / 60).ToString().PadLeft(2, '0') + ":" + (timeE % 60).ToString().PadLeft(2, '0'), steps);
                        }
                        else
                        {
                            int maxDescs = 8;
                            int descMinuteInt = 5;
                            if (steps > maxDescs)
                            {
                                for (int i = 1; Math.Ceiling(Convert.ToDecimal(steps) / i) > maxDescs; i++)
                                {
                                    descMinuteInt = i * 5 + 5;
                                }
                            }

                            for (int i = 1; i <= steps; i++)
                            {
                                int T = (timeS + i * 5);
                                if (T % descMinuteInt == timeS % descMinuteInt)
                                {
                                    g.drawBottomString((T / 60).ToString().PadLeft(2, '0') + ":" + (T % 60).ToString().PadLeft(2, '0'), i);
                                }
                            }
                        }
                    }

                    float[] valsLeft = new float[valCX];
                    int[] timeLeft = new int[valCX];
                    float[] valsRight = new float[valCX];
                    int[] timeRight = new int[valCX];
                    int activeRightCX = 0;
                    float maxRight = 0;
                    int activeLeftCX = 0;
                    float maxLeft = 0;
                    int MCID = 0;
                    int cx = 0;
                    rs = Db.rs("SELECT " +
                        "z.MeasureComponentID, " +
                        "ISNULL(mcl.MeasureComponent,z.MeasureComponent), " +
                        "ISNULL(mcl.Unit,z.Unit), " +
                        "z.Type, " +
                        "dbo.cf_hourMinutes(um.DT), " +
                        "umc.ValInt, " +
                        "umc.ValDec, " +
                        "um.UserMeasureID, " +
                        "z.MeasureComponent " +
                        "FROM UserMeasure x " +
                        "INNER JOIN UserMeasureComponent y ON x.UserMeasureID = y.UserMeasureID " +
                        "INNER JOIN MeasureComponent z ON y.MeasureComponentID = z.MeasureComponentID " +
                        "LEFT OUTER JOIN MeasureComponentLang mcl ON z.MeasureComponentID = mcl.MeasureComponentID AND mcl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                        "INNER JOIN UserMeasureComponent umc ON z.MeasureComponentID = umc.MeasureComponentID " +
                        "INNER JOIN UserMeasure um ON umc.UserMeasureID = um.UserMeasureID " +
                        "WHERE z.ShowInGraph = 1 " +
                        "AND x.UserMeasureID = " + MUID + " " +
                        "AND um.DeletedDT IS NULL " +
                        "AND dbo.cf_yearMonthDay(um.DT) = dbo.cf_yearMonthDay(x.DT) " +
                        "AND um.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " " +
                        "ORDER BY z.SortOrder ASC, um.DT ASC, umc.UserMeasureComponentID");
                    while (rs.Read())
                    {
                        if (MCID == 0)
                        {
                            leftHeader = rs.GetString(8);
                            leftUnit = rs.GetString(2);
                        }
                        else if (MCID != rs.GetInt32(0))
                        {
                            rightHeader = rs.GetString(8);
                            rightUnit = rs.GetString(2);
                            cx = 0;
                            left = false;
                        }
                        if (MUID == rs.GetInt32(7))
                        {
                            if (left)
                            {
                                activeLeftCX = cx;
                            }
                            else
                            {
                                activeRightCX = cx;
                            }
                        }
                        switch (rs.GetInt32(3))
                        {
                            case 4:
                                if (left)
                                {
                                    if (rs.IsDBNull(6))
                                    {
                                        valsLeft[cx] = float.MinValue;
                                    }
                                    else
                                    {
                                        valsLeft[cx] = (float)rs.GetDecimal(6);
                                        timeLeft[cx] = rs.GetInt32(4);
                                        if (valsLeft[cx] > maxLeft)
                                        {
                                            maxLeft = valsLeft[cx];
                                        }
                                    }
                                }
                                else
                                {
                                    if (rs.IsDBNull(6))
                                    {
                                        valsRight[cx] = float.MinValue;
                                    }
                                    else
                                    {
                                        valsRight[cx] = (float)rs.GetDecimal(6);
                                        timeRight[cx] = rs.GetInt32(4);
                                        if (valsRight[cx] > maxRight)
                                        {
                                            maxRight = valsRight[cx];
                                        }
                                    }
                                }
                                break;
                        }
                        cx++;
                        MCID = rs.GetInt32(0);
                    }
                    rs.Close();

                    g.setMinMax(0, (valCX == 1 ? Math.Max(maxLeft, maxRight) : maxLeft));
                    //g.computeMinMax(0.1f, 0.1f);
                    g.roundMinMax();
                    g.drawOutlines(steps, false, !left, 0, 100);

                    if (valCX == 1)
                    {
                        if (!left)
                        {
                            g.drawMultiBar(1, 1, valsLeft[0], (int)g.steping, g.barW, 2, 0, 100, false, false);
                        }
                        else
                        {
                            g.drawBar(1, 1, valsLeft[0]);
                        }
                        g.drawBottomString((timeLeft[0] / 60).ToString().PadLeft(2, '0') + ":" + (timeLeft[0] % 60).ToString().PadLeft(2, '0'), 1);
                    }
                    else
                    {
                        float lastVal = float.MinValue; int lastPos = 0;
                        for (int i = 0; i < valCX; i++)
                        {
                            if (valsLeft[i] != float.MinValue)
                            {
                                if (lastVal != float.MinValue)
                                {
                                    g.drawStepLine(1, lastPos, lastVal, (timeLeft[i] - timeS) / 5, valsLeft[i]);
                                }
                                lastPos = (timeLeft[i] - timeS) / 5;
                                lastVal = valsLeft[i];
                            }
                        }
                        for (int i = 0; i < valCX; i++)
                        {
                            if (valsLeft[i] != float.MinValue)
                            {
                                g.drawCircle((i == activeLeftCX ? "333333" : "FFFFFF"), (timeLeft[i] - timeS) / 5, valsLeft[i]);
                            }
                        }
                    }
                    if (!left)
                    {
                        if (valCX == 1)
                        {
                            g.drawMultiBar(2, 1, valsRight[0], (int)g.steping, g.barW, 2, 1, 100, false, false);
                        }
                        else
                        {
                            g.resetMinMax();
                            g.setMinMax(0, maxRight);
                            //g.computeMinMax(0.1f, 0.1f);
                            g.roundMinMax();
                            g.drawOutlinesRight();
                            float lastVal = float.MinValue; int lastPos = 0;
                            for (int i = 0; i < valCX; i++)
                            {
                                if (valsRight[i] != float.MinValue)
                                {
                                    if (lastVal != float.MinValue)
                                    {
                                        g.drawStepLine(2, lastPos, lastVal, (timeRight[i] - timeS) / 5, valsRight[i]);
                                    }
                                    lastPos = (timeRight[i] - timeS) / 5;
                                    lastVal = valsRight[i];
                                }
                            }
                            for (int i = 0; i < valCX; i++)
                            {
                                if (valsRight[i] != float.MinValue)
                                {
                                    g.drawCircle((i == activeRightCX ? "333333" : "FFFFFF"), (timeRight[i] - timeS) / 5, valsRight[i]);
                                }
                            }
                        }
                    }
                }
            }

            if (!left)
            {
                g.drawAxisExpl(leftHeader + (leftUnit != "" ? " (" + leftUnit + ")" : ""), 1, false, true);
                g.drawAxisExpl(rightHeader + (rightUnit != "" ? " (" + rightUnit + ")" : ""), 2, true, true);
                g.drawCenterHeader(measure);
                if (valCX == 1)
                {
                    g.drawAxis(false);
                }
                else
                {
                    g.drawAxis(true);
                    g.drawRightAxis();
                }
            }
            else
            {
                g.drawAxis(false);
                g.drawAxisExpl(measure + (leftUnit != "" ? " (" + leftUnit + ")" : ""), 1, false, true);
            }

            g.render();
        }
    }
}