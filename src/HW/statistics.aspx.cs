using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.FromHW;

namespace HW
{
    public partial class statistics : System.Web.UI.Page
    {
        private string measureTimeFrame = "";
        private string surveyName = "";
        private string[] rpt = { "", "", "", "" };
        private int measurementType = 0;
        private int compare = 0;
        int[] rp = { -1, -1, 0, 0 };
        DateTime highDT = DateTime.Now, lowDT = DateTime.Now;

        public string measure()
        {
            return measureTimeFrame;
        }
        private string exercises(string str)
        {
            string s = str;
            if (s.IndexOf("<EXID") >= 0)
            {
                while (s.IndexOf("<EXID") >= 0)
                {
                    int i = Convert.ToInt32(s.Substring(s.IndexOf("<EXID") + 5, 3));
                    SqlDataReader rs = Db.rs("SELECT e.ExerciseAreaID, el.Exercise FROM [Exercise] e " +
                                                "INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID AND el.Lang = " + (Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1) + " " +
                                                "WHERE e.ExerciseID = " + i);
                    if (rs.Read())
                    {
                        s = s.Replace("<EXID" + i.ToString().PadLeft(3, '0') + ">", "<a href=\"exercise.aspx?EAID=" + rs.GetInt32(0) + "\" class=\"desk\">" + rs.GetString(1) + "</a>");
                    }
                    else
                    {
                        s = s.Replace("<EXID" + i.ToString().PadLeft(3, '0') + ">", "");
                    }
                    rs.Close();
                }
            }
            return s;
        }
        public string colors()
        {
            if (measurementType == 0)
            {
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    default: return "<div class=\"largelegend\">" +
                                    "<div class=\"healthy\">H&auml;lsosam niv&aring;</div>" +
                                    "<div class=\"med\">F&ouml;rb&auml;ttringsbehov</div>" +
                                    "<div class=\"unhealthy\">Oh&auml;lsosam niv&aring;</div>" +
                                    "</div>";
                    case 2: return "<div class=\"largelegend\">" +
                                    "<div class=\"healthy\">Healthy level</div>" +
                                    "<div class=\"med\">Improvement needed</div>" +
                                    "<div class=\"unhealthy\">Unhealthy level</div>" +
                                    "</div>";
                }
            }
            else
            {
                string q = "";
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: q = " - jämförelse"; break;
                    case 2: q = " - comparison"; break;
                }
                string s = "<div class=\"largelegend\">";
                if (rp[0] > 0)
                {
                    s += "<div class=\"var1\">" + rpt[0] + "</div>";
                    if (compare > 1)
                    {
                        if (rp[0] > 0)
                        {
                            s += "<div class=\"var1 alt\">" + rpt[0] + q + "</div>";
                        }
                    }
                }
                if (rp[1] > 0)
                {
                    s += "<div class=\"var2\">" + rpt[1] + "</div>";
                    if (compare > 1)
                    {
                        if (rp[1] > 0)
                        {
                            s += "<div class=\"var2 alt\">" + rpt[1] + q + "</div>";
                        }
                    }
                }
                if (rp[2] > 0)
                {
                    s += "<div class=\"var3\">" + rpt[2] + "</div>";
                    if (compare > 1)
                    {
                        if (rp[2] > 0)
                        {
                            s += "<div class=\"var3 alt\">" + rpt[2] + q + "</div>";
                        }
                    }
                }
                if (rp[3] > 0)
                {
                    s += "<div class=\"var4\">" + rpt[3] + "</div>";
                    if (compare > 1)
                    {
                        if (rp[3] > 0)
                        {
                            s += "<div class=\"var4 alt\">" + rpt[3] + q + "</div>";
                        }
                    }
                }
                s += "</div>";
                return s;
            }
        }
        public string limits()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                default: return "Gränsvärden";
                case 2: return "Limits";
            }
        }
        public string analysis()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                default: return "Tolkning";
                case 2: return "Interpretation";
            }
        }
        public string actionPlan()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                default: return "Handlingsplan";
                case 2: return "Action plan";
            }
        }
        public int feedbackIdx(int level)
        {
            return 10 + level;
        }
        public int actionIdx(int level)
        {
            return 15 + level;
        }
        public string levels(string level)
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2:
                    switch (level)
                    {
                        default: return "Healthy level";
                        case "medium": return "Improvement needed";
                        case "unhealthy": return "Unhealthy level";
                    }
                default:
                    switch (level)
                    {
                        default: return "H&auml;lsosam niv&aring;";
                        case "medium": return "F&ouml;rb&auml;ttringsbehov";
                        case "unhealthy": return "Oh&auml;lsosam niv&aring;";
                    }
            }

        }
        private void loadSurveyKeys(string SK)
        {
            string s = "";
            SqlDataReader rs = Db.rs("SELECT " +
                        "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-',''), " +
                        "ISNULL(sprul.Nav,spru.Nav) AS Nav, " +
                        "spru.ProjectRoundUnitID " +
                        "FROM [Sponsor] s " +
                        "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
                        "LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID AND sprul.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                        "WHERE s.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " " +
                        "AND eform.dbo.cf_unitIndividualReportID(spru.ProjectRoundUnitID) IS NOT NULL");
            while (rs.Read())
            {
                SurveyKey.Items.Add(new ListItem(rs.GetString(1), rs.GetString(0) + ":" + rs.GetInt32(2).ToString()));

                if (SK == "")
                {
                    SK = rs.GetString(0);
                }
                if (s != "")
                {
                    surveyKeys.Controls.Add(new LiteralControl("<li" + s));
                }
                if (SK == rs.GetString(0))
                {
                    surveyName = rs.GetString(1);
                    SurveyKey.SelectedValue = rs.GetString(0) + ":" + rs.GetInt32(2).ToString();
                }
                s = " id=\"" + rs.GetString(0) + "\"><a href=\"statistics.aspx?SK=" + rs.GetString(0) + "\">" + rs.GetString(1) + "</a></li>";
            }
            rs.Close();
            if (s != "")
            {
                surveyKeys.Controls.Add(new LiteralControl("<li class=\"last\"" + s));
            }
        }
        private void updateIndexes()
        {
            IndexOne.Items.Clear();
            IndexMulti.Items.Clear();

            int cx = 0;
            SqlDataReader rs = Db.rs("SELECT " +
                            "rp.ReportPartID, " +
                            "rpl.Subject " +
                            "FROM ProjectRoundUnit pru " +
                            "INNER JOIN Report r ON dbo.cf_unitIndividualReportID(pru.ProjectRoundUnitID) = r.ReportID " +
                            "INNER JOIN ReportPart rp ON rp.ReportID = r.ReportID " +
                            "INNER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID AND rpl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                            "WHERE rp.Type = 8 AND pru.ProjectRoundUnitID = " + Convert.ToInt32(SurveyKey.SelectedValue.Split(':')[1]) + " " +
                            "ORDER BY rp.SortOrder", "eFormSqlConnection");
            while (rs.Read())
            {
                IndexOne.Items.Add(new ListItem(rs.GetString(1) + "&nbsp;", rs.GetInt32(0).ToString()));
                IndexMulti.Items.Add(new ListItem(rs.GetString(1) + "&nbsp;", rs.GetInt32(0).ToString()));

                IndexMulti.Items.FindByValue(rs.GetInt32(0).ToString()).Selected = true;
                if (cx++ == 0)
                {
                    IndexOne.Items.FindByValue(rs.GetInt32(0).ToString()).Selected = true;
                }
            }
            rs.Close();
        }
        private string getIndexes()
        {
            return getIndexes(false);
        }
        private string getIndexes(bool forSql)
        {
            string ret1 = "", ret2 = "";
            int cx = 0;

            if (Compare.SelectedValue == "1")
            {
                foreach (ListItem i in IndexMulti.Items)
                {
                    cx++;
                    if (i.Selected)
                    {
                        ret1 += (ret1 != "" ? "," : "") + Convert.ToInt32(i.Value);
                        ret2 += (ret2 != "" ? "," : "") + cx;
                    }
                }
            }
            else
            {
                foreach (ListItem i in IndexOne.Items)
                {
                    cx++;
                    if (i.Selected)
                    {
                        ret1 += (ret1 != "" ? "," : "") + Convert.ToInt32(i.Value);
                        ret2 += (ret2 != "" ? "," : "") + cx;
                    }
                }
            }
            return (forSql ? ret1 : "&RPID=" + ret1 + "&RPO=" + ret2);
        }
        //private void updateDates(DateTime highDT, DateTime lowDT)
        //{
        //    YearHigh.SelectedValue = highDT.Year.ToString();
        //    YearLow.SelectedValue = lowDT.Year.ToString();
        //    MonthHigh.SelectedValue = highDT.Month.ToString();
        //    MonthLow.SelectedValue = lowDT.Month.ToString();
        //    DayHigh.SelectedValue = highDT.Day.ToString();
        //    DayLow.SelectedValue = lowDT.Day.ToString();
        //}
        private string getAnswers()
        {
            string excl = "", incl = "";
            foreach (ListItem i in Submission.Items)
            {
                if (i.Selected)
                {
                    incl += (incl != "" ? "," : "") + i.Value;
                }
                else
                {
                    excl += (excl != "" ? "," : "") + i.Value;
                }
            }

            return "&" + (incl.Length > excl.Length ? "E=" + excl : "I=" + incl);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["UserID"] == null)
            {
                HttpContext.Current.Response.Redirect("inactivity.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }

            //SurveyKey.SelectedIndexChanged += new EventHandler(SurveyKey_SelectedIndexChanged);
            //Submit.Click += new EventHandler(Submit_Click);
            //UpdateIndexes.Click += new EventHandler(UpdateIndexes_Click);
            //IndexOne.SelectedIndexChanged += new EventHandler(IndexOne_SelectedIndexChanged);

            SqlDataReader rs;

            int RPID = (HttpContext.Current.Request.QueryString["RPID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["RPID"]) : 0);
            string SK = (HttpContext.Current.Request.QueryString["SK"] != null ? HttpContext.Current.Request.QueryString["SK"].ToString().Replace("'", "") : "");
            string AK = (HttpContext.Current.Request.QueryString["AK"] != null ? HttpContext.Current.Request.QueryString["AK"].ToString().Replace("'", "") : "");
            int AID = 0;
            //int UID = 0;

            if (HttpContext.Current.Request.QueryString["RP0"] != null)
            {
                rp[0] = Convert.ToInt32(HttpContext.Current.Request.QueryString["RP0"]);
            }
            if (HttpContext.Current.Request.QueryString["RP1"] != null)
            {
                rp[1] = Convert.ToInt32(HttpContext.Current.Request.QueryString["RP1"]);
            }
            if (HttpContext.Current.Request.QueryString["RP2"] != null)
            {
                rp[2] = Convert.ToInt32(HttpContext.Current.Request.QueryString["RP2"]);
            }
            if (HttpContext.Current.Request.QueryString["RP3"] != null)
            {
                rp[3] = Convert.ToInt32(HttpContext.Current.Request.QueryString["RP3"]);
            }
            string rpQ = "&RP0=" + rp[0] + "&RP1=" + rp[1] + "&RP2=" + rp[2] + "&RP3=" + rp[3];

            compare = (HttpContext.Current.Request.QueryString["C"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["C"]) : 1);
            measurementType = (HttpContext.Current.Request.QueryString["MT"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["MT"]) : 0);
            if (compare > 1 && measurementType == 0) { measurementType = 2; }

            if (!IsPostBack)
            {
                //switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                //{
                //    case 2: 
                //        Compare.Items.FindByValue("1").Text = "different indexes";
                //        Compare.Items.FindByValue("2").Text = "with others";
                //        Submit.Text = "Update";
                //        UpdateIndexes.Text = "<-- Update";
                //        break;
                //}

                //Compare.Attributes["onclick"] += "toggleIndex();";

                //#region Populate date range
                //for (int i = 2003; i <= DateTime.Now.Year; i++)
                //{
                //    YearHigh.Items.Add(new ListItem(i.ToString(), i.ToString()));
                //    YearLow.Items.Add(new ListItem(i.ToString(), i.ToString()));
                //}
                //for (int i = 1; i <= 12; i++)
                //{
                //    MonthHigh.Items.Add(new ListItem(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[i - 1], i.ToString()));
                //    MonthLow.Items.Add(new ListItem(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[i - 1], i.ToString()));
                //}
                //for (int i = 1; i <= 31; i++)
                //{
                //    DayHigh.Items.Add(new ListItem(i.ToString(), i.ToString()));
                //    DayLow.Items.Add(new ListItem(i.ToString(), i.ToString()));
                //}

                //#endregion

                if (AK != "")
                {
                    #region Find survey of requested answerkey
                    rs = Db.rs("SELECT " +
                        "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-',''), " +
                        "spru.ProjectRoundUnitID, " +
                        "uprua.DT, " +
                        "uprua.AnswerID " +
                        "FROM [User] u " +
                        //"INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
                        //"INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
                        //"INNER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND u.UserID = upru.UserID " +
                        "INNER JOIN UserProjectRoundUser upru ON u.UserID = upru.UserID " +
                        "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
                        "INNER JOIN eform..Answer a ON uprua.AnswerID = a.AnswerID " +
                        "INNER JOIN eform..ProjectRound pr ON a.ProjectRoundID = pr.ProjectRoundID " +
                        "INNER JOIN eform..ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
                        "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
                        "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID AND spru.SurveyID = ISNULL(NULLIF(pru.SurveyID,0),pr.SurveyID) " +
                        "WHERE uprua.AnswerKey = '" + AK.Replace("'", "") + "' " +
                        "AND u.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]));
                    if (rs.Read())
                    {
                        SK = rs.GetString(0);
                        //SurveyKey.SelectedValue = rs.GetString(0) + ":" + rs.GetInt32(1).ToString();
                        highDT = rs.GetDateTime(2).Date;
                        AID = rs.GetInt32(3);
                    }
                    else
                    {
                        AK = "";
                    }
                    rs.Close();
                    #endregion
                }
                if (SK == "")
                {
                    #region Find last filled out survey
                    rs = Db.rs("SELECT " +
                        "TOP 1 " +
                        "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-',''), " +
                        "spru.ProjectRoundUnitID, " +
                        "uprua.DT, " +
                        "uprua.AnswerID, " +
                        "REPLACE(CONVERT(VARCHAR(255),uprua.AnswerKey),'-','') " +
                        "FROM [User] u " +
                        "INNER JOIN UserProjectRoundUser upru ON u.UserID = upru.UserID " +
                        "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
                        "INNER JOIN eform..Answer a ON uprua.AnswerID = a.AnswerID " +
                        "INNER JOIN eform..ProjectRound pr ON a.ProjectRoundID = pr.ProjectRoundID " +
                        "INNER JOIN eform..ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
                        "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
                        "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID AND spru.SurveyID = ISNULL(NULLIF(pru.SurveyID,0),pr.SurveyID) " +
                        "WHERE u.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " " +
                        "ORDER BY uprua.DT DESC, uprua.AnswerID DESC");
                    if (rs.Read())
                    {
                        SK = rs.GetString(0);
                        //SurveyKey.SelectedValue = rs.GetString(0) + ":" + rs.GetInt32(1).ToString();
                        highDT = rs.GetDateTime(2).Date;
                        AID = rs.GetInt32(3);
                        AK = rs.GetString(4);
                    }
                    rs.Close();
                    #endregion
                }
                else
                {
                    rs = Db.rs("SELECT " +
                        "TOP 1 " +
                        "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-',''), " +
                        "spru.ProjectRoundUnitID, " +
                        "uprua.DT, " +
                        "uprua.AnswerID, " +
                        "REPLACE(CONVERT(VARCHAR(255),uprua.AnswerKey),'-','') " +
                        "FROM [User] u " +
                        "INNER JOIN UserProjectRoundUser upru ON u.UserID = upru.UserID " +
                        "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
                        "INNER JOIN eform..Answer a ON uprua.AnswerID = a.AnswerID " +
                        "INNER JOIN eform..ProjectRound pr ON a.ProjectRoundID = pr.ProjectRoundID " +
                        "INNER JOIN eform..ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
                        "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
                        "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID AND spru.SurveyID = ISNULL(NULLIF(pru.SurveyID,0),pr.SurveyID) " +
                        "WHERE u.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " " +
                        "AND REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') = '" + SK + "' " +
                        "ORDER BY uprua.DT DESC, uprua.AnswerID DESC");
                    if (rs.Read())
                    {
                        highDT = rs.GetDateTime(2).Date;
                        AID = rs.GetInt32(3);
                        AK = rs.GetString(4);
                    }
                    rs.Close();
                }

                loadSurveyKeys(SK);

                if (measurementType == 4)
                {
                    #region Find first DT
                    rs = Db.rs("SELECT " +
                            "TOP 1 " +
                            "uprua.DT " +
                            "FROM [User] u " +
                            "INNER JOIN UserProjectRoundUser upru ON u.UserID = upru.UserID " +
                            "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
                        // "INNER JOIN SponsorProjectRoundUnit spru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') = '" + SurveyKey.SelectedValue.Split(':')[0].Replace("'", "") + "' " +
                            "WHERE u.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " " +
                            "ORDER BY uprua.DT ASC");
                    if (rs.Read())
                    {
                        lowDT = rs.GetDateTime(0).Date;
                    }
                    rs.Close();
                    #endregion
                }

                selectedSurveyKey.Controls.Add(new LiteralControl(surveyName));
                selectedSurvey2.Controls.Add(new LiteralControl(surveyName));

                //updateDates(highDT, lowDT);
                //updateIndexes();
                //updateAnswers(AID);
                loadStats(AK);

                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1:
                        availableCompare.Controls.Add(new LiteralControl("<li id=\"compare0\"><a href=\"statistics.aspx?MT=" + measurementType + "&C=1" + rpQ + "\">Ingen</a></li>"));
                        availableCompare.Controls.Add(new LiteralControl("<li id=\"compare1\"><a href=\"statistics.aspx?MT=" + measurementType + "&C=2" + rpQ + "\">Databasen</a></li>"));
                        availableMeasurements.Controls.Add(new LiteralControl("<li id=\"timeframe0\"><a href=\"statistics.aspx?C=" + compare + "&MT=0\">Specifik mätning</a></li>"));
                        availableMeasurements.Controls.Add(new LiteralControl("<li id=\"timeframe1\"><a href=\"statistics.aspx?C=" + compare + "&MT=1" + rpQ + "\">Senaste veckan</a></li>"));
                        availableMeasurements.Controls.Add(new LiteralControl("<li id=\"timeframe2\"><a href=\"statistics.aspx?C=" + compare + "&MT=2" + rpQ + "\">Senaste månaden</a></li>"));
                        availableMeasurements.Controls.Add(new LiteralControl("<li id=\"timeframe3\"><a href=\"statistics.aspx?C=" + compare + "&MT=3" + rpQ + "\">Senaste året</a></li>"));
                        availableMeasurements.Controls.Add(new LiteralControl("<li id=\"timeframe4\" class=\"last\"><a href=\"statistics.aspx?C=" + compare + "&MT=4" + rpQ + "\">Sen första mätning</a></li>"));
                        if (compare == 2)
                        {
                            selectedCompare.Controls.Add(new LiteralControl("Databasen"));
                            selectedCompare2.Controls.Add(new LiteralControl("Databasen"));
                        }
                        else
                        {
                            selectedCompare.Controls.Add(new LiteralControl("Ingen"));
                            selectedCompare2.Controls.Add(new LiteralControl("Ingen"));
                        }
                        if (measurementType == 0)
                        {
                            selectedMeasurement.Controls.Add(new LiteralControl("Specifik mätning"));
                            selectedMeasurement2.Controls.Add(new LiteralControl("Specifik mätning [" + measure() + "]"));
                        }
                        if (measurementType == 1)
                        {
                            selectedMeasurement.Controls.Add(new LiteralControl("Senaste veckan"));
                            selectedMeasurement2.Controls.Add(new LiteralControl("Senaste veckan"));
                        }
                        if (measurementType == 2)
                        {
                            selectedMeasurement.Controls.Add(new LiteralControl("Senaste månaden"));
                            selectedMeasurement2.Controls.Add(new LiteralControl("Senaste månaden"));
                        }
                        if (measurementType == 3)
                        {
                            selectedMeasurement.Controls.Add(new LiteralControl("Senaste året"));
                            selectedMeasurement2.Controls.Add(new LiteralControl("Senaste året"));
                        }
                        if (measurementType == 4)
                        {
                            selectedMeasurement.Controls.Add(new LiteralControl("Sen första mätning"));
                            selectedMeasurement2.Controls.Add(new LiteralControl("Sen första mätning"));
                        }
                        break;
                    case 2:
                        availableCompare.Controls.Add(new LiteralControl("<li id=\"compare0\"><a href=\"statistics.aspx?MT=" + measurementType + "&C=1" + rpQ + "\">None</a></li>"));
                        availableCompare.Controls.Add(new LiteralControl("<li id=\"compare1\" class=\"last\"><a href=\"statistics.aspx?MT=" + measurementType + "&C=2" + rpQ + "\">Database</a></li>"));
                        availableMeasurements.Controls.Add(new LiteralControl("<li id=\"timeframe0\"><a href=\"statistics.aspx?C=" + compare + "&MT=0\">Specific measurement</a></li>"));
                        availableMeasurements.Controls.Add(new LiteralControl("<li id=\"timeframe1\"><a href=\"statistics.aspx?C=" + compare + "&MT=1" + rpQ + "\">Last week</a></li>"));
                        availableMeasurements.Controls.Add(new LiteralControl("<li id=\"timeframe2\"><a href=\"statistics.aspx?C=" + compare + "&MT=2" + rpQ + "\">Last month</a></li>"));
                        availableMeasurements.Controls.Add(new LiteralControl("<li id=\"timeframe3\"><a href=\"statistics.aspx?C=" + compare + "&MT=3" + rpQ + "\">Last year</a></li>"));
                        availableMeasurements.Controls.Add(new LiteralControl("<li id=\"timeframe4\" class=\"last\"><a href=\"statistics.aspx?C=" + compare + "&MT=4" + rpQ + "\">Since first measurement</a></li>"));
                        if (compare == 2)
                        {
                            selectedCompare.Controls.Add(new LiteralControl("Database"));
                            selectedCompare2.Controls.Add(new LiteralControl("Database"));
                        }
                        else
                        {
                            selectedCompare.Controls.Add(new LiteralControl("None"));
                            selectedCompare2.Controls.Add(new LiteralControl("None"));
                        }
                        if (measurementType == 0)
                        {
                            selectedMeasurement.Controls.Add(new LiteralControl("Specific measurement"));
                            selectedMeasurement2.Controls.Add(new LiteralControl("Specific measurement [" + measure() + "]"));
                        }
                        if (measurementType == 1)
                        {
                            selectedMeasurement.Controls.Add(new LiteralControl("Last week"));
                            selectedMeasurement2.Controls.Add(new LiteralControl("Last week"));
                        }
                        if (measurementType == 2)
                        {
                            selectedMeasurement.Controls.Add(new LiteralControl("Last month"));
                            selectedMeasurement2.Controls.Add(new LiteralControl("Last month"));
                        }
                        if (measurementType == 3)
                        {
                            selectedMeasurement.Controls.Add(new LiteralControl("Last year"));
                            selectedMeasurement2.Controls.Add(new LiteralControl("Last year"));
                        }
                        if (measurementType == 4)
                        {
                            selectedMeasurement.Controls.Add(new LiteralControl("Since first measurement"));
                            selectedMeasurement2.Controls.Add(new LiteralControl("Since first measurement"));
                        }
                        break;
                }
            }
        }

        //void IndexOne_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    loadStats();
        //}

        //void UpdateIndexes_Click(object sender, EventArgs e)
        //{
        //    loadStats();
        //}

        //void Submit_Click(object sender, EventArgs e)
        //{
        //    updateAnswers(0);
        //    loadStats();
        //}

        //void SurveyKey_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    updateIndexes();
        //    updateAnswers(0);
        //    loadStats();
        //}

        //protected override void OnPreRender(EventArgs e)
        //{
        //    base.OnPreRender(e);
        //    if (HttpContext.Current.Request.Form["Refresh"] != null && HttpContext.Current.Request.Form["Refresh"].ToString() == "1")
        //    {
        //        loadStats();
        //    }
        //}

        private DateTime parseDT(bool high)
        {
            DateTime ret = DateTime.Now.Date;

            int toYear = DateTime.Now.Year, toMonth = DateTime.Now.Month, toDay = DateTime.Now.Day;
            try
            {
                toYear = Math.Min(DateTime.Now.Year, Math.Max(2003, Convert.ToInt32(YearHigh.SelectedValue)));
                toMonth = Math.Min(12, Math.Max(1, Convert.ToInt32(MonthHigh.SelectedValue)));
                toDay = Math.Min(31, Math.Max(1, Convert.ToInt32(DayHigh.SelectedValue)));
            }
            catch (Exception) { }

            bool fail = true;
            while (fail)
            {
                try
                {
                    ret = new DateTime(toYear, toMonth, toDay);
                    fail = false;
                }
                catch (Exception) { fail = true; toDay--; }
            }
            if (high)
            {
                return ret;
            }
            else
            {
                DateTime tmp = ret;

                int fromYear = DateTime.Now.Year, fromMonth = DateTime.Now.Month, fromDay = DateTime.Now.Day;
                try
                {
                    fromYear = Math.Min(DateTime.Now.Year, Math.Max(2003, Convert.ToInt32(YearLow.SelectedValue)));
                    fromMonth = Math.Min(12, Math.Max(1, Convert.ToInt32(MonthLow.SelectedValue)));
                    fromDay = Math.Min(31, Math.Max(1, Convert.ToInt32(DayLow.SelectedValue)));
                }
                catch (Exception) { }
                fail = true;
                while (fail)
                {
                    try
                    {
                        ret = new DateTime(fromYear, fromMonth, fromDay);
                        fail = false;
                    }
                    catch (Exception) { fail = true; fromDay--; }
                }
                if (ret > tmp)
                {
                    return ret.AddMonths(-1);
                }
                else
                {
                    return ret;
                }
            }
        }

        private void updateAnswers(int AID)
        {
            ArrayList s = new ArrayList();
            for (int i = 0; i < Submission.Items.Count; i++)
            {
                if (Submission.Items[i].Selected)
                {
                    s.Add(Convert.ToInt32(Submission.Items[i].Value));
                }
            }

            Submission.Items.Clear();

            SqlDataReader rs = Db.rs("SELECT " +
                "uprua.AnswerID, " +
                "uprua.DT " +
                "FROM [User] u " +
                "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
                "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
                "INNER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND u.UserID = upru.UserID " +
                "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
                "WHERE u.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " " +
                "AND spru.ProjectRoundUnitID = " + Convert.ToInt32(SurveyKey.SelectedValue.Split(':')[1]) + " " +
                "AND uprua.DT < '" + parseDT(true).AddDays(1).ToString("yyyy-MM-dd") + "' " +
                "AND uprua.DT >= '" + parseDT(false).ToString("yyyy-MM-dd") + "' " +
                "ORDER BY uprua.DT DESC, uprua.AnswerID DESC");
            while (rs.Read())
            {
                Submission.Items.Add(new ListItem(rs.GetDateTime(1).ToString("yyMMdd HH:mm") + "&nbsp;&nbsp;&nbsp;", rs.GetInt32(0).ToString()));
                if (AID == rs.GetInt32(0))
                {
                    Submission.Items.FindByValue(rs.GetInt32(0).ToString()).Selected = true;
                }
                else if (AID == 0 && s.Contains(rs.GetInt32(0)))
                {
                    Submission.Items.FindByValue(rs.GetInt32(0).ToString()).Selected = true;
                }
            }
            rs.Close();
        }

        private void loadStats(string AK)
        {
            //string answers = getAnswers();
            //bool bars = 
            //    Compare.SelectedValue == "1" 
            //    && 
            //    answers.Substring(1, 1) == "I" 
            //    && 
            //    answers.Substring(3).Split(',').Length == 1;

            //ExplWins.Text = "";
            //Stats.Text = "<img " + (bars ? "border=\"0\" [zzz] " : "") + "src=\"statisticsImage.aspx" +
            //    "?TDT=" + parseDT(true).ToString("yyMMdd") +
            //    "&FDT=" + parseDT(false).ToString("yyMMdd") +
            //    "&LID=" + Convert.ToInt32(HttpContext.Current.Session["LID"]) + 
            //    "&C=" + Convert.ToInt32(Compare.SelectedValue) +
            //    "&S=" + Convert.ToInt32(SurveyKey.SelectedValue.Split(':')[1]) +
            //    getIndexes() +
            //    answers +
            //    "\"/>";

            ///**/
            //int cx = 0;
            //string expl = "";

            if (measurementType == 0)
            {
                #region Specific measurement
                string sql = "SELECT " +
                    "rl.Feedback, " +
                    "rpl.Subject, " +
                    "rpl.AltText, " +
                    "rp.ReportPartID " +
                    "FROM ProjectRoundUnit pru " +
                    "INNER JOIN Report r ON dbo.cf_unitIndividualReportID(pru.ProjectRoundUnitID) = r.ReportID " +
                    "LEFT OUTER JOIN ReportLang rl ON r.ReportID = rl.ReportID AND rl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                    "LEFT OUTER JOIN ReportPart rp ON rp.ReportID = r.ReportID AND rp.Type = 8 " +
                    "LEFT OUTER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID AND rpl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                    "WHERE " +
                    //(getIndexes(true) != "" ? "rp.ReportPartID IN (" + getIndexes(true) + ") AND " : "") + 
                    "pru.ProjectRoundUnitID = " + Convert.ToInt32(SurveyKey.SelectedValue.Split(':')[1]) + " " +
                    "ORDER BY rp.SortOrder";
                //HttpContext.Current.Response.Write(sql);
                //HttpContext.Current.Response.End();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                SqlDataReader rs = Db.rs(sql, "eFormSqlConnection");
                if (rs.Read())
                {
                    feedback.Controls.Add(new LiteralControl(rs.GetString(0).Replace("\r\n", "<br/>")));
                    do
                    {
                        SqlDataReader rs2 = Db.rs("SELECT " +
                            "rpc.WeightedQuestionOptionID, " +	// 0
                            "wqol.WeightedQuestionOption, " +
                            "wqo.TargetVal, " +
                            "wqo.YellowLow, " +
                            "wqo.GreenLow, " +
                            "wqo.GreenHigh, " +					// 5
                            "wqo.YellowHigh, " +
                            "wqo.QuestionID, " +
                            "wqo.OptionID, " +
                            "wqol.FeedbackHeader, " +
                            "wqol.Feedback," +                  // 10
                            "wqol.FeedbackRedLow," +
                            "wqol.FeedbackYellowLow," +
                            "wqol.FeedbackGreen," +
                            "wqol.FeedbackYellowHigh," +
                            "wqol.FeedbackRedHigh," +           // 15
                            "wqol.ActionRedLow," +
                            "wqol.ActionYellowLow," +
                            "wqol.ActionGreen," +
                            "wqol.ActionYellowHigh," +
                            "wqol.ActionRedHigh " +             // 20
                            "FROM Report r " +
                            "INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID " +
                            "INNER JOIN ReportPartComponent rpc ON rp.ReportPartID = rpc.ReportPartID " +
                            "INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
                            "INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                            "WHERE rpc.ReportPartID = " + rs.GetInt32(3) + " " +
                            "ORDER BY rp.SortOrder, rpc.SortOrder", "eFormSqlConnection");
                        while (rs2.Read())
                        {

                            SqlDataReader rs3 = Db.rs("SELECT TOP 1 " +
                                "av.ValueInt, " +
                                "a.EndDT, " +
                                "a.AnswerID " +
                                "FROM Answer a " +
                                "INNER JOIN healthWatch..UserProjectRoundUserAnswer ha ON a.AnswerID = ha.AnswerID AND ha.ProjectRoundUserID = a.ProjectRoundUserID " +
                                (AK == "" ? "INNER JOIN healthWatch..UserProjectRoundUser h ON ha.ProjectRoundUserID = h.ProjectRoundUserID " : "") +
                                "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
                                "AND av.DeletedSessionID IS NULL " +
                                "AND av.QuestionID = " + rs2.GetInt32(7) + " " +
                                "AND av.OptionID = " + rs2.GetInt32(8) + " " +
                                "WHERE a.EndDT IS NOT NULL " +
                                (AK != "" ? "AND ha.AnswerKey = '" + AK + "' " : "AND h.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " ") +
                                //"AND a.EndDT < '" + TDT.AddDays(1).ToString("yyyy-MM-dd") + "' " +
                                //"AND a.EndDT >= '" + FDT.ToString("yyyy-MM-dd") + "' " +
                                //"AND a.AnswerID " + answer + " " +
                                //"AND a.ProjectRoundUserID = " + userID +
                                "ORDER BY a.EndDT DESC", "eFormSqlConnection");
                            if (rs3.Read())
                            {
                                measureTimeFrame = rs3.GetDateTime(1).ToString("yyyy-MM-dd HH:mm");

                                if (!rs3.IsDBNull(0))
                                {
                                    bool hasColor = false;
                                    string level = "", legend = "";
                                    int lastBreak = 0, totW = 0, levelID = 0;

                                    #region Levels
                                    if (!rs2.IsDBNull(3))
                                    {
                                        hasColor = true;
                                        if (rs2.GetInt32(3) > 0)
                                        {
                                            if (legend != "")
                                            {
                                                legend += "<td class=\"nomargin\"><img src=\"img/null.gif\" width=\"2\" height=\"6\"></td>";
                                                totW += 2;
                                            }
                                            int w = Convert.ToInt32(((decimal)(Math.Min(rs2.GetInt32(3), 100) - lastBreak)) / 100 * 684);
                                            totW += 4;
                                            while (totW + w > 700)
                                            {
                                                w--;
                                            }
                                            legend += "<td class=\"nomargin\"><img src=\"includes/resources/bar_legend_2_right.gif\" width=\"2\" height=\"6\"></td>";
                                            legend += "<td class=\"nomargin\"><img src=\"includes/resources/bar_legend_2.gif\" width=\"" + w + "\" height=\"6\"></td>";
                                            legend += "<td class=\"nomargin\"><img src=\"includes/resources/bar_legend_2_left.gif\" width=\"2\" height=\"6\"></td>";
                                            lastBreak = rs2.GetInt32(3);
                                            totW += w;
                                        }
                                        if (rs2.GetInt32(3) >= 0 && rs2.GetInt32(3) <= 100)
                                        {

                                            if (rs3.GetInt32(0) >= rs2.GetInt32(3))
                                            {
                                                levelID = 2;
                                                level = "medium";
                                            }
                                        }
                                    }
                                    if (!rs2.IsDBNull(4))
                                    {
                                        hasColor = true;
                                        if (rs2.GetInt32(4) > 0)
                                        {
                                            if (legend != "")
                                            {
                                                legend += "<td class=\"nomargin\"><img src=\"img/null.gif\" width=\"2\" height=\"6\"></td>";
                                                totW += 2;
                                            }
                                            int w = Convert.ToInt32(((decimal)(Math.Min(rs2.GetInt32(4), 100) - lastBreak)) / 100 * 684);
                                            totW += 4;
                                            while (totW + w > 700)
                                            {
                                                w--;
                                            }
                                            legend += "<td class=\"nomargin\"><img src=\"includes/resources/bar_legend_1_right.gif\" width=\"2\" height=\"6\"></td>";
                                            legend += "<td class=\"nomargin\"><img src=\"includes/resources/bar_legend_1.gif\" width=\"" + w + "\" height=\"6\"></td>";
                                            legend += "<td class=\"nomargin\"><img src=\"includes/resources/bar_legend_1_left.gif\" width=\"2\" height=\"6\"></td>";
                                            totW += w;
                                            lastBreak = rs2.GetInt32(4);
                                        }
                                        if (rs2.GetInt32(4) >= 0 && rs2.GetInt32(4) <= 100)
                                        {

                                            if (rs3.GetInt32(0) >= rs2.GetInt32(4))
                                            {
                                                levelID = 3;
                                                level = "healthy";
                                            }
                                        }
                                    }
                                    if (!rs2.IsDBNull(5))
                                    {
                                        hasColor = true;
                                        if (legend != "")
                                        {
                                            legend += "<td class=\"nomargin\"><img src=\"img/null.gif\" width=\"2\" height=\"6\"></td>";
                                            totW += 2;
                                        }
                                        int w = Convert.ToInt32(((decimal)(Math.Min(rs2.GetInt32(5), 100) - lastBreak)) / 100 * 684);
                                        totW += 4;
                                        while (totW + w > 700)
                                        {
                                            w--;
                                        }
                                        legend += "<td class=\"nomargin\"><img src=\"includes/resources/bar_legend_0_right.gif\" width=\"2\" height=\"6\"></td>";
                                        legend += "<td class=\"nomargin\"><img src=\"includes/resources/bar_legend_0.gif\" width=\"" + w + "\" height=\"6\"></td>";
                                        legend += "<td class=\"nomargin\"><img src=\"includes/resources/bar_legend_0_left.gif\" width=\"2\" height=\"6\"></td>";
                                        totW += w;
                                        lastBreak = rs2.GetInt32(5);
                                        if (rs2.GetInt32(5) >= 0 && rs2.GetInt32(5) <= 100)
                                        {

                                            if (rs3.GetInt32(0) >= rs2.GetInt32(5))
                                            {
                                                levelID = 4;
                                                level = "medium";
                                            }
                                        }
                                    }
                                    if (!rs2.IsDBNull(6))
                                    {
                                        hasColor = true;
                                        if (lastBreak < 100)
                                        {
                                            if (legend != "")
                                            {
                                                legend += "<td class=\"nomargin\"><img src=\"img/null.gif\" width=\"2\" height=\"6\"></td>";
                                                totW += 2;
                                            }
                                            int w = Convert.ToInt32(((decimal)(Math.Min(rs2.GetInt32(6), 100) - lastBreak)) / 100 * 684);
                                            totW += 4;
                                            while (totW + w > 700)
                                            {
                                                w--;
                                            }
                                            legend += "<td class=\"nomargin\"><img src=\"includes/resources/bar_legend_1_right.gif\" width=\"2\" height=\"6\"></td>";
                                            legend += "<td class=\"nomargin\"><img src=\"includes/resources/bar_legend_1.gif\" width=\"" + w + "\" height=\"6\"></td>";
                                            legend += "<td class=\"nomargin\"><img src=\"includes/resources/bar_legend_1_left.gif\" width=\"2\" height=\"6\"></td>";
                                            totW += w;
                                            lastBreak = rs2.GetInt32(6);
                                        }
                                        if (lastBreak < 100)
                                        {
                                            if (legend != "")
                                            {
                                                legend += "<td class=\"nomargin\"><img src=\"img/null.gif\" width=\"2\" height=\"6\"></td>";
                                                totW += 2;
                                            }
                                            int w = Convert.ToInt32(((decimal)(100 - lastBreak)) / 100 * 684);
                                            totW += 4;
                                            while (totW + w > 700)
                                            {
                                                w--;
                                            }
                                            legend += "<td class=\"nomargin\"><img src=\"includes/resources/bar_legend_2_right.gif\" width=\"2\" height=\"6\"></td>";
                                            legend += "<td class=\"nomargin\"><img src=\"includes/resources/bar_legend_2.gif\" width=\"" + w + "\" height=\"6\"></td>";
                                            legend += "<td class=\"nomargin\"><img src=\"includes/resources/bar_legend_2_left.gif\" width=\"2\" height=\"6\"></td>";
                                            totW += w;
                                            lastBreak = rs2.GetInt32(6);
                                        }
                                        if (rs2.GetInt32(6) >= 0 && rs2.GetInt32(6) <= 100)
                                        {

                                            if (rs3.GetInt32(0) >= rs2.GetInt32(6))
                                            {
                                                levelID = 5;
                                                level = "unhealthy";
                                            }
                                        }
                                    }
                                    if (level == "")
                                    {
                                        if (hasColor)
                                        {
                                            levelID = 1;
                                            level = "unhealthy";
                                        }
                                        else
                                        {
                                            level = "unknown";
                                        }
                                    }
                                    #endregion
                                    sb.Append("<div class=\"bar " + level + "\">" +
                                          "<div class=\"overview\">" +
                                          "<span class=\"legend\">&nbsp;</span>" +
                                          "<span class=\"title\">" + rs2.GetString(9) + "</span>" +
                                          "<span class=\"graph\">" +
                                          "<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"width:700px;\">" +
                                          "<tr>" +
                                          "<td><img src=\"includes/resources/bar_" + level + "_right.gif\" width=\"3\" height=\"16\"></td>" +
                                          "<td><img src=\"includes/resources/bar_" + level + ".gif\" width=\"" + Convert.ToInt32((decimal)rs3.GetInt32(0) / 100 * 691) + "\" height=\"16\"></td>" +
                                          "<td><img src=\"includes/resources/bar_" + level + "_left.gif\" width=\"3\" height=\"16\"></td>" +
                                          "<td><img src=\"includes/resources/bar.gif\" width=\"" + Convert.ToInt32((decimal)(100 - rs3.GetInt32(0)) / 100 * 691) + "\" height=\"16\"></td>" +
                                          "<td><img src=\"includes/resources/bar_left.gif\" width=\"3\" height=\"16\"></td>" +
                                          "</tr>" +
                                          "</table>" +
                                          "</span>" +
                                          (levelID != 0 ? "<span class=\"detailtoggle\">&nbsp;</span>" : "") +
                                          "</div>" +
                                          (levelID != 0 ? "" +
                                          "<div class=\"detail\">" +
                                          "<table>" +
                                          (hasColor && legend != "" ? "" +
                                          "<tr class=\"limits\"><td>" + limits() + "</td>" +
                                          "<td>" +
                                          "<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"width:700px;\">" +
                                          "<tr>" +
                                          legend +
                                          "</tr>" +
                                          "</table>" +
                                          "</td>" +
                                          "</tr>" +
                                          "" : "") +
                                          (!rs2.IsDBNull(10) && rs2.GetString(10) != "" ? "" +
                                          "<tr><td>" + analysis() + "</td>" +
                                          "<td>" +
                                          rs2.GetString(10) +
                                          "</td>" +
                                          "</tr>" +
                                          "" : "") +
                                          (!rs2.IsDBNull(feedbackIdx(levelID)) && rs2.GetString(feedbackIdx(levelID)) != "" ? "" +
                                          "<tr><td>" + levels(level) + "</td>" +
                                          "<td>" +
                                          rs2.GetString(feedbackIdx(levelID)) +
                                          "</td>" +
                                          "</tr>" +
                                          "" : "") +
                                          (!rs2.IsDBNull(actionIdx(levelID)) && rs2.GetString(actionIdx(levelID)) != "" ? "" +
                                          "<tr><td>" + actionPlan() + "</td>" +
                                          "<td>" +
                                          exercises(rs2.GetString(actionIdx(levelID))) +
                                          "</td>" +
                                          "</tr>" +
                                          "" : "") +
                                          "</table>" +
                                          "<div class=\"bottom\">&nbsp;</div>" +
                                          "</div><!-- end .detail --> " +
                                          "" : "") +
                                          "</div><!-- end .bar -->");
                                }
                            }
                            rs3.Close();
                        }
                        rs2.Close();
                    }
                    while (rs.Read());
                }
                rs.Close();
                #endregion

                bars.Controls.Add(new LiteralControl("<div class=\"barchart\">" + sb.ToString() + "</div><!-- end .barchart -->"));
            }
            else
            {
                System.Collections.Hashtable ht = new Hashtable(), ht2 = new Hashtable();
                //string idx = "";
                //string icx = "";
                string sql = "SELECT " +
                    "rl.Feedback, " +
                    "rpl.Subject, " +
                    "rpl.AltText, " +
                    "rp.ReportPartID " +
                    "FROM ProjectRoundUnit pru " +
                    "INNER JOIN Report r ON dbo.cf_unitIndividualReportID(pru.ProjectRoundUnitID) = r.ReportID " +
                    "LEFT OUTER JOIN ReportLang rl ON r.ReportID = rl.ReportID AND rl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                    "LEFT OUTER JOIN ReportPart rp ON rp.ReportID = r.ReportID AND rp.Type = 8 " +
                    "LEFT OUTER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID AND rpl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                    "WHERE " +
                    //(getIndexes(true) != "" ? "rp.ReportPartID IN (" + getIndexes(true) + ") AND " : "") + 
                    "pru.ProjectRoundUnitID = " + Convert.ToInt32(SurveyKey.SelectedValue.Split(':')[1]) + " " +
                    "ORDER BY rp.SortOrder";
                //HttpContext.Current.Response.Write(sql);
                //HttpContext.Current.Response.End();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                SqlDataReader rs = Db.rs(sql, "eFormSqlConnection");
                if (rs.Read())
                {
                    feedback.Controls.Add(new LiteralControl(rs.GetString(0).Replace("\r\n", "<br/>")));
                    string s = "";
                    int cx = 0;
                    do
                    {
                        ht.Add(cx, rs.GetInt32(3));
                        if (s != "")
                        {
                            sb.Append("<li" + s);
                        }
                        s = "><a href=\"statistics.aspx?C=" + compare + "&MT=" + measurementType + "&RPx=" + cx + "x\">" + rs.GetString(1) + "</a></li>";
                        ht2.Add(rs.GetInt32(3), s);
                        if (rp[0] == -1 || rp[0] == rs.GetInt32(3))
                        {
                            rp[0] = rs.GetInt32(3); rpt[0] = rs.GetString(1);
                        }
                        else if (rp[1] == -1 || rp[1] == rs.GetInt32(3))
                        {
                            rp[1] = rs.GetInt32(3); rpt[1] = rs.GetString(1);
                        }
                        else if (rp[2] == rs.GetInt32(3))
                        {
                            rp[2] = rs.GetInt32(3); rpt[2] = rs.GetString(1);
                        }
                        else if (rp[3] == rs.GetInt32(3))
                        {
                            rp[3] = rs.GetInt32(3); rpt[3] = rs.GetString(1);
                        }
                        cx++;
                        //if (rp[0] == rs.GetInt32(3) || rp[1] == rs.GetInt32(3) || rp[2] == rs.GetInt32(3) || rp[3] == rs.GetInt32(3))
                        //{
                        //    idx += (idx != "" ? "," : "") + rs.GetInt32(3).ToString();
                        //    icx += (icx != "" ? "," + (icx.Split(',').Length+1) : "1");
                        //}
                    }
                    while (rs.Read());
                    sb.Append("<li class=\"last\"" + s);
                }
                rs.Close();
                vars.Controls.Add(new LiteralControl("<div class=\"varchoser choser\"><div class=\"title\"><strong>" + surveyName + ":</strong> "));
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: vars.Controls.Add(new LiteralControl("Välj upp till fyra mått att visa")); break;
                    case 2: vars.Controls.Add(new LiteralControl("Select up to four indicators")); break;
                }
                vars.Controls.Add(new LiteralControl("</div>"));
                for (int i = 0; i < 4; i++)
                {
                    string s = sb.ToString(), q = "";
                    string ss = "";
                    for (int ii = 0; ii < 4; ii++)
                    {
                        if (ii != i)
                        {
                            if (rp[ii] > 0)
                            {
                                s = s.Replace("<li" + ht2[rp[ii]], "");
                                s = s.Replace("<li class=\"last\"" + ht2[rp[ii]], "");
                            }
                            ss += "&RP" + ii + "=" + rp[ii];
                        }
                    }

                    vars.Controls.Add(new LiteralControl("" +
                        "<div class=\"var var" + (i + 1) + "\">" +
                        "<dl class=\"dropdown\">"));
                    if (rp[i] == 0)
                    {
                        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                        {
                            case 1: vars.Controls.Add(new LiteralControl("<dt><a><span>Ingen</span></a></dt>")); break;
                            case 2: vars.Controls.Add(new LiteralControl("<dt><a><span>None</span></a></dt>")); break;
                        }
                    }
                    else
                    {
                        vars.Controls.Add(new LiteralControl("" +
                            "<dt><a><span>" + rpt[i] + "</span></a></dt>"));
                    }
                    vars.Controls.Add(new LiteralControl("" +
                        "<dd>" +
                        "<ul>"));
                    if (i > 0)
                    {
                        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                        {
                            case 1: q = "<li><a href=\"statistics.aspx?C=" + compare + "&MT=" + measurementType + ss + "&RP" + i + "=0\">Ingen</a></li>"; break;
                            case 2: q = "<li><a href=\"statistics.aspx?C=" + compare + "&MT=" + measurementType + ss + "&RP" + i + "=0\">None</a></li>"; break;
                        }
                    }

                    for (int ii = 0; ii < ht.Count; ii++)
                    {
                        s = s.Replace("RPx=" + ii + "x", "RP" + i + "=" + ht[ii] + ss);
                    }

                    if (i == 0)
                    {
                        vars.Controls.Add(new LiteralControl("" + s + ""));
                    }
                    else if (rp[i - 1] != 0)
                    {
                        vars.Controls.Add(new LiteralControl("" + q + s + ""));
                    }
                    else
                    {
                        vars.Controls.Add(new LiteralControl("" + q.Replace("<li>", "<li class=\"last\">") + ""));
                    }
                    vars.Controls.Add(new LiteralControl("" +
                        "</ul>" +
                        "</dd>" +
                        "</dl>" +
                        "</div>"));
                }
                vars.Controls.Add(new LiteralControl("</div>"));

                switch (measurementType)
                {
                    case 1: highDT = DateTime.Now; lowDT = highDT.AddDays(-7); break;
                    case 2: highDT = DateTime.Now; lowDT = highDT.AddMonths(-1); break;
                    case 3: highDT = DateTime.Now; lowDT = highDT.AddYears(-1); break;
                }
                bars.Controls.Add(new LiteralControl("<div class=\"linechart\">" +
                        "<img src=\"lineChart.aspx" +
                        "?TDT=" + highDT.ToString("yyMMdd") +
                        "&FDT=" + lowDT.ToString("yyMMdd") +
                        "&LID=" + Convert.ToInt32(HttpContext.Current.Session["LID"]) +
                        "&C=" + compare +
                        "&S=" + Convert.ToInt32(SurveyKey.SelectedValue.Split(':')[1]) +
                        "&RP0=" + rp[0] +
                        "&RP1=" + rp[1] +
                        "&RP2=" + rp[2] +
                        "&RP3=" + rp[3] +
                    //"&RPID=" + idx + 
                    //"&RPO=" + icx +
                    //getIndexes() +
                    //answers +
                        "\"/>" +
                        "</div><!-- end .linechart -->"));
            }

            //Stats.Text += "<br/><br/>";
            //switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            //{
            //    case 1:
            //        Stats.Text += "<B>OBS!</B> Kom ihåg att det är betydelsefullt att regelbundet följa sig själv över tid då förändringar och trender är viktigare än resultat från enskilda mätningar.";
            //        break;
            //    case 2:
            //        Stats.Text += "<B>Please note!</B> Remember that it is important to regularly monitor yourself over time when the changes and trends are more important than results from individual measurements.";
            //        break;
            //}

            //if (bars)
            //{
            //    double stepping = (440 / (cx + 1));
            //    for (int i = 0; i < cx; i++)
            //    {
            //        int left = Convert.ToInt32(40+stepping*(i+0.5));
            //        expl = expl.Replace("[x" + i + "x]", left + ",60," + (left + stepping) + ",400");
            //    }
            //    Stats.Text += "<MAP NAME=\"expl\">" + expl + "</MAP>";			
            //}
        }
    }
}