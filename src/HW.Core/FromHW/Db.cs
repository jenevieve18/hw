using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.Core.FromHW
{
	public class chartInfoLang
    {
        public int langID = 0;
        public string text = "";

        public chartInfoLang(int langID, string text)
        {
            this.langID = langID;
            this.text = text;
        }
    }
    public class chartInfo
    {
        public int questionID = 0;
        public int optionID = 0;
        public chartInfoLang[] lang;
        public int cutOff = 0;

        public chartInfo(int questionID, int optionID, chartInfoLang[] lang, int cutOff)
        {
            this.questionID = questionID;
            this.optionID = optionID;
            this.lang = lang;
            this.cutOff = cutOff;
        }
    }
    /// <summary>
	/// Summary description for Db.
	/// </summary>
	public class Db
	{
        public static string cutAt(int i, string s)
        {
            return (s.Length <= i ? s : s.Substring(0, i));
        }
        public static int chartCount = 7;
        public static chartInfo[] info = new chartInfo[chartCount];
        public static void initiateChart(bool yesterday)
        {
            chartInfoLang[] tmp; chartInfoLang tmp2; chartInfo tmp3;
            
            // Health
            tmp = new chartInfoLang[2];
            tmp2 = new chartInfoLang(1,"av våra användare mådde ganska eller mycket bra " + (yesterday ? "igår" : "förra veckan") + "."); tmp[0] = tmp2;
            tmp2 = new chartInfoLang(2, "of our users felt quite or very good " + (yesterday ? "yesterday" : "last week") + "."); tmp[1] = tmp2;
            tmp3 = new chartInfo(238, 55, tmp, 62); info[0] = tmp3;

            // Sleep
            tmp = new chartInfoLang[2];
            tmp2 = new chartInfoLang(1, "av våra användare sov ganska eller mycket bra " + (yesterday ? "igår natt" : "förra veckan") + "."); tmp[0] = tmp2;
            tmp2 = new chartInfoLang(2, "of our users slept quite or very good " + (yesterday ? "yesterday night" : "last week") + "."); tmp[1] = tmp2;
            tmp3 = new chartInfo(239, 288, tmp, 62); info[1] = tmp3;

            // WorkAtmosphere
            tmp = new chartInfoLang[2];
            tmp2 = new chartInfoLang(1, "av våra användare hade ganska eller mycket bra stämning på jobbet " + (yesterday ? "igår" : "förra veckan") + "."); tmp[0] = tmp2;
            tmp2 = new chartInfoLang(2, "of our users had a quite or very good work atmosphere " + (yesterday ? "yesterday" : "last week") + "."); tmp[1] = tmp2;
            tmp3 = new chartInfo(248, 54, tmp, 62); info[2] = tmp3;

            // Concentration
            tmp = new chartInfoLang[2];
            tmp2 = new chartInfoLang(1, "av våra användare hade ganska eller mycket bra koncentrationsförmåga " + (yesterday ? "igår" : "förra veckan") + "."); tmp[0] = tmp2;
            tmp2 = new chartInfoLang(2, "of our users had a quite or very good ability to concentrate " + (yesterday ? "yesterday" : "last week") + "."); tmp[1] = tmp2;
            tmp3 = new chartInfo(240, 54, tmp, 62); info[3] = tmp3;

            // Energy
            tmp = new chartInfoLang[2];
            tmp2 = new chartInfoLang(1, "av våra användare hade ganska eller mycket hög energinivå " + (yesterday ? "igår" : "förra veckan") + "."); tmp[0] = tmp2;
            tmp2 = new chartInfoLang(2, "of our users had quite or very high energy level " + (yesterday ? "yesterday" : "last week") + "."); tmp[1] = tmp2;
            tmp3 = new chartInfo(242, 66, tmp, 62); info[4] = tmp3;

            // Workload
            tmp = new chartInfoLang[2];
            tmp2 = new chartInfoLang(1, "av våra användare hade ganska eller mycket hög arbetsbelastning " + (yesterday ? "igår" : "förra veckan") + "."); tmp[0] = tmp2;
            tmp2 = new chartInfoLang(2, "of our users experienced a quite or very high work load " + (yesterday ? "yesterday" : "last week") + "."); tmp[1] = tmp2;
            tmp3 = new chartInfo(247, 71, tmp, 62); info[5] = tmp3;

            // Workjoy
            tmp = new chartInfoLang[2];
            tmp2 = new chartInfoLang(1, "av våra användare kände ganska eller mycket stor arbetsglädje " + (yesterday ? "igår" : "förra veckan") + "."); tmp[0] = tmp2;
            tmp2 = new chartInfoLang(2, "of our users felt a quite or very high job satisfaction " + (yesterday ? "yesterday" : "last week") + "."); tmp[1] = tmp2;
            tmp3 = new chartInfo(246, 73, tmp, 62); info[6] = tmp3;
        }
        public static string getChartText(int chart, int langID)
        {
            string ret = "";
            for (int i = 0; i < info[chart].lang.Length; i++) if (info[chart].lang[i].langID == langID) ret = info[chart].lang[i].text;
            return ret;
        }
        public static int getChartValue(int chart, int sponsorID, bool yesterday)
        {
            string key = DateTime.Today.ToString("yyyy-MM-dd") + "-" + chart + "Today" + (sponsorID != 0 ? sponsorID.ToString() : "");

            int total = (yesterday ? getUsersYesterday(chart, sponsorID) : getUsersLastWeek(chart, sponsorID));
            if(total > 0 && HttpContext.Current.Application[key] == null)
            {
                DateTime dt = DateTime.Now; int daysBack = 1;
                if (!yesterday)
                {
                    daysBack = 7;
                    while (dt.DayOfWeek != DayOfWeek.Monday)
                    {
                        dt = dt.AddDays(-1);
                    }
                }
                SqlDataReader rs = Db.rs("SELECT COUNT(*) " +
                    "FROM Answer a " +
                    "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
                    (sponsorID != 0 ? "INNER JOIN healthwatch..SponsorProjectRoundUnit spru ON a.ProjectRoundUnitID = spru.ProjectRoundUnitID AND spru.SponsorID = " + sponsorID + " " : "") +
                    "WHERE av.QuestionID = " + info[chart].questionID + " AND av.OptionID = " + info[chart].optionID + " AND av.ValueInt > " + info[chart].cutOff + " " +
                    "AND a.EndDT >= '" + dt.AddDays(-daysBack).ToString("yyyy-MM-dd") + "' AND a.EndDT < '" + dt.ToString("yyyy-MM-dd") + "'" + 
                    "", "eFormSqlConnection");
                if (rs.Read())
                {
                    HttpContext.Current.Application[key] = Convert.ToInt32((double)rs.GetInt32(0) / (double)total * 100);
                }
                rs.Close();
            }
            return (HttpContext.Current.Application[key] == null ? 0 : Convert.ToInt32(HttpContext.Current.Application[key]));
        }
        public static string rightNowChart(int sponsorID, int LID, string rating, string color, int chart)
        {
            initiateChart(true);

            int users = getUsersYesterday(chart, sponsorID);
            bool yesterday = (users >= 7);
            if (!yesterday)
            {
                initiateChart(false);
            }

            System.Text.StringBuilder ret = new System.Text.StringBuilder();

            //string rating = "", color = "";
            int val = getChartValue(chart, sponsorID, yesterday);
            //getRating(val, ref rating, ref color);
            //rating = "high";
            //color = "green";
            ret.Append("<div class=\"landing_graph\">");
            ret.Append("<img src=\"/piechart.aspx?C=" + color + "&V=" + val + "&" + DateTime.Today.ToString("yyyyMMdd") + sponsorID.ToString().PadLeft(4,'0') + "\" width=\"80\" height=\"80\" alt=\"" + val + "% " + getChartText(chart, LID) + "\" />");
            ret.Append("<br />");
            ret.Append("<img src=\"/images/" + color + "Shadow.png\" width=\"80\" height=\"22\" />");
            ret.Append("</div>");
            ret.Append("<div class=\"metadata\">");
            ret.Append("<div class=\"number " + rating + "\">" + (val == 0 ? "?" : val.ToString()) + "%</div>");
            ret.Append("<div class=\"label\">" + getChartText(chart, LID) + "</div>");
            //switch (LID)
            //{
            //    case 1: ret.Append("<div class=\"label\">av våra användare <br /> mådde mycket eller ganska <br />bra igår.</div>"); break;
            //    default: ret.Append("<div class=\"label\">of our users <br /> felt very or quite <br />good yesterday.</div>"); break;
            //}
            ret.Append("</div>");

            return ret.ToString();
        }
        public static string rightNow(int sponsorID, int LID)
        {
            System.Text.StringBuilder ret = new System.Text.StringBuilder();
            ret.Append("<div class=\"grid_2 alpha justnow" + LID + "\">&nbsp;</div>");
            ret.Append("<div class=\"grid_14 omega graphs\">");

            System.Collections.ArrayList seen = new System.Collections.ArrayList();
            for (int i = 0; i < info.Length; i++)
            {
                Random rnd = new Random(); int n = 0;
                do
                {
                    n = rnd.Next(0, chartCount);
                }
                while (seen.Contains(n));
                seen.Add(n);
                switch (i)
                {
                    case 0: ret.Append(rightNowChart(sponsorID, LID, "high", "green", n)); break;
                    case 1: ret.Append(rightNowChart(sponsorID, LID, "med", "orange", n)); break;
                    case 2: ret.Append(rightNowChart(sponsorID, LID, "low", "purple", n)); break;
                }
            }
            
            ret.Append("</div>");

            return ret.ToString();
        }
        private static int getUsersYesterday(int chart, int sponsorID)
        {
            string key = DateTime.Today.ToString("yyyy-MM-dd") + "-" + chart + "UsersYesterday" + (sponsorID != 0 ? sponsorID.ToString() : "");

            if (HttpContext.Current.Application[key] == null)
            {
                SqlDataReader rs = Db.rs("SELECT COUNT(*) " +
                    "FROM Answer a " +
                    "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
                    (sponsorID != 0 ? "INNER JOIN healthwatch..SponsorProjectRoundUnit spru ON a.ProjectRoundUnitID = spru.ProjectRoundUnitID AND spru.SponsorID = " + sponsorID + " " : "") +
                    "WHERE av.QuestionID = " + info[chart].questionID + " AND av.OptionID = " + info[chart].optionID + " AND av.ValueInt IS NOT NULL " +
                    "AND a.EndDT >= '" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + "' AND a.EndDT < '" + DateTime.Today.ToString("yyyy-MM-dd") + "'" +
                    "", "eFormSqlConnection");
                if (rs.Read())
                {
                    HttpContext.Current.Application[key] = rs.GetInt32(0);
                }
                rs.Close();
            }
            return Convert.ToInt32((HttpContext.Current.Application[key] == null ? 0 : HttpContext.Current.Application[key]));
        }
        private static int getUsersLastWeek(int chart, int sponsorID)
        {
            string key = DateTime.Today.ToString("yyyy-MM-dd") + "-" + chart + "UsersLastWeek" + (sponsorID != 0 ? sponsorID.ToString() : "");

            if (HttpContext.Current.Application[key] == null)
            {
                DateTime dt = DateTime.Now;
                while (dt.DayOfWeek != DayOfWeek.Monday)
                {
                    dt = dt.AddDays(-1);
                }
                SqlDataReader rs = Db.rs("SELECT COUNT(*) " +
                    "FROM Answer a " +
                    "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
                    (sponsorID != 0 ? "INNER JOIN healthwatch..SponsorProjectRoundUnit spru ON a.ProjectRoundUnitID = spru.ProjectRoundUnitID AND spru.SponsorID = " + sponsorID + " " : "") +
                    "WHERE av.QuestionID = " + info[chart].questionID + " AND av.OptionID = " + info[chart].optionID + " AND av.ValueInt IS NOT NULL " + 
                    "AND a.EndDT >= '" + dt.AddDays(-7).ToString("yyyy-MM-dd") + "' AND a.EndDT < '" + dt.ToString("yyyy-MM-dd") + "'" +
                    "", "eFormSqlConnection");
                if (rs.Read())
                {
                    HttpContext.Current.Application[key] = rs.GetInt32(0);
                }
                rs.Close();
            }
            return Convert.ToInt32((HttpContext.Current.Application[key] == null ? 0 : HttpContext.Current.Application[key]));
        }
        public static void getRating(int val, ref string rating, ref string color)
        {
            if (val > 70)
            {
                rating = "high";
                color = "green";
            }
            else if (val > 40)
            {
                rating = "med";
                color = "orange";
            }
            else
            {
                rating = "low";
                color = "purple";
            }
        }
        public static string cobranded()
        {
            return (Convert.ToInt32(HttpContext.Current.Application["SUPERSPONSOR" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"])]) != 0 ? " cobranded" + (Convert.ToInt32(HttpContext.Current.Application["SUPERSPONSORLOGO" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"])].ToString()) == 1 ? "" : " sl") : "");
        }
        public static string cobranding()
        {
            int sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);
            int superSponsorID = Convert.ToInt32(HttpContext.Current.Application["SUPERSPONSOR" + sponsorID]);
            switch (superSponsorID)
            {
                case 0: return "HealthWatch.se<br />";
                default:
                    string coop = "";
                    if (HttpContext.Current.Application["SUPERSPONSORLOGO" + sponsorID + "LANG" + Convert.ToInt32(HttpContext.Current.Session["LID"])] != null)
                    {
                        coop = HttpContext.Current.Application["SUPERSPONSORLOGO" + sponsorID + "LANG" + Convert.ToInt32(HttpContext.Current.Session["LID"])].ToString();
                    }
                    else
                    {
                        coop = HttpContext.Current.Application["SUPERSPONSORNAME" + sponsorID].ToString();
                        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                        {
                            case 1: { coop = "I samarbete med " + coop; break; }
                            case 2: { coop = "In cooperation with " + coop; break; }
                        }
                    }
                    return "" +
                            "<div class=\"cobrand\"><img src=\"img/partner/" + superSponsorID + ".gif\" alt=\"" + HttpContext.Current.Application["APP" + sponsorID] + "\"></div>" +
                            "<div class=\"brand\">" +
                            (Convert.ToInt32(HttpContext.Current.Application["SUPERSPONSORLOGO" + sponsorID].ToString()) == 1 ?
                            "<div>HealthWatch.se</div>" : "HealthWatch.se<br />") +
                            "<div class=\"regular\">" + coop + "</div>" +
                            "</div>";
            }
        }
        public static string fetchActs(string dt, int LID, int userID, bool edit)
        {
            //string hhmm = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            SqlDataReader rs = Db.rs("SELECT DISTINCT " +
                "um.UserMeasureID, " +
                "dbo.cf_hourMinute(um.DT) AS M, " +
                "ISNULL(ml.Measure,m.Measure), " +
                "m.SortOrder AS S, " +
                "(SELECT COUNT(*) FROM MeasureComponent x WHERE x.MeasureID = m.MeasureID), " +
                "m.MeasureID, " +
                "NULL, " +
                "NULL, " +
                "NULL " +
                "FROM UserMeasure um " +
                "INNER JOIN UserMeasureComponent umc ON um.UserMeasureID = umc.UserMeasureID " +
                "INNER JOIN MeasureComponent mc ON umc.MeasureComponentID = mc.MeasureComponentID " +
                "INNER JOIN Measure m ON mc.MeasureID = m.MeasureID " +
                "LEFT OUTER JOIN MeasureLang ml ON m.MeasureID = ml.MeasureID AND ml.LangID = " + LID + " " +
                "WHERE mc.ShowInList = 1 AND um.DeletedDT IS NULL AND um.UserID = " + userID + " " +
                "AND dbo.cf_yearMonthDay(um.DT) = '" + dt + "' " +

                "UNION ALL " +

                "SELECT DISTINCT " +
                "NULL, " +
                "dbo.cf_hourMinute(es.DateTime) AS M, " +
                "el.Exercise, " +
                "e.ExerciseSortOrder+1000 AS S, " +
                "NULL, " +
                "NULL, " +
                "NULL, " +
                "NULL, " +
                "NULL " +
                "FROM ExerciseStats es " +
                "INNER JOIN ExerciseVariantLang evl ON es.ExerciseVariantLangID = evl.ExerciseVariantLangID " +
                "INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID " +
                "INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID " +
                "LEFT OUTER JOIN ExerciseLang el ON e.ExerciseID = el.ExerciseID AND el.Lang = " + (LID - 1) + " " +
                "WHERE es.UserID = " + userID + " " +
                "AND dbo.cf_yearMonthDay(es.DateTime) = '" + dt + "' " +

                "UNION ALL " +

                "SELECT DISTINCT " +
                "NULL, " +
                "dbo.cf_hourMinute(uprua.DT) AS M, " +
                "ISNULL(mcl.MeasureCategory,mc.MeasureCategory), " +
                "mc.SortOrder+500 AS S, " +
                "NULL, " +
                "NULL, " +
                "uprua.AnswerKey, " +
                "uprua.UserProjectRoundUserAnswerID, " +
                "pru.IndividualReportID " +
                "FROM UserProjectRoundUser upru " +
                "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
                "INNER JOIN [User] u ON upru.UserID = u.UserID " +
                "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
                "INNER JOIN SponsorProjectRoundUnit spru ON upru.ProjectRoundUnitID = spru.ProjectRoundUnitID AND s.SponsorID = spru.SponsorID " +
                "INNER JOIN eform..ProjectRoundUnit pru ON spru.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
                "INNER JOIN MeasureCategory mc ON spru.SponsorProjectRoundUnitID = mc.SPRUID " +
                "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + LID + " " +
                "WHERE upru.UserID = " + userID + " " +
                "AND dbo.cf_yearMonthDay(uprua.DT) = '" + dt + "' " +

                "ORDER BY M, S");
            if (rs.Read())
            {
                do
                {
                    //if (hhmm != rs.GetString(1) && hhmm != "")
                    //{
                    //    actsBox.InnerHtml += "<TR><TD colspan=\"4\" class=\"sep\">&nbsp;</td></tr>";
                    //}
                    //actsBox.InnerHtml += "<TR><TD VALIGN=\"TOP\">";
                    //if (hhmm != rs.GetString(1))
                    //{
                    //    actsBox.InnerHtml += rs.GetString(1);
                    //hhmm = rs.GetString(1);
                    //}
                    //actsBox.InnerHtml += "&nbsp;&nbsp;&nbsp;</TD><TD VALIGN=\"TOP\">" + rs.GetString(2) + "&nbsp;&nbsp;</TD><TD>";
                    sb.Append("<div class=\"activity\"><span><span>" + rs.GetString(1) + " " + rs.GetString(2) + "</span>");

                    if (!rs.IsDBNull(0))
                    {
                        //string s = "";
                        int cx = 0;
                        SqlDataReader rs2 = Db.rs("SELECT " +
                        "umc.ValInt, " +
                        "umc.ValDec, " +
                        "umc.ValTxt, " +
                        "ISNULL(mcl.MeasureComponent,mc.MeasureComponent), " +
                        "ISNULL(mcl.Unit,mc.Unit), " +
                        "mc.Type, " +
                        "mc.Decimals, " +
                        "mc.ShowUnitInList " +
                        "FROM UserMeasure um " +
                        "INNER JOIN UserMeasureComponent umc ON um.UserMeasureID = umc.UserMeasureID " +
                        "INNER JOIN MeasureComponent mc ON umc.MeasureComponentID = mc.MeasureComponentID " +
                        "LEFT OUTER JOIN MeasureComponentLang mcl ON mc.MeasureComponentID = mcl.MeasureComponentID AND mcl.LangID = " + LID + " " +
                        "WHERE mc.ShowInList = 1 AND um.UserMeasureID = " + rs.GetInt32(0) + " " +
                        "ORDER BY mc.SortOrder");
                        while (rs2.Read())
                        {
                            if (cx++ > 0)
                            {
                                sb.Append(" / ");
                            }
                            if (rs.GetInt32(4) > 1 || rs2.GetInt32(7) == 0)
                            {
                                sb.Append("<label style=\"cursor:pointer;cursor:hand;cursor:help;\" title=\"" + (rs.GetInt32(4) > 1 ? rs2.GetString(3) + (rs2.GetInt32(7) == 0 ? ", " : "") : "") + (rs2.GetInt32(7) == 0 ? rs2.GetString(4) : "") + "\">");
                            }
                            switch (rs2.GetInt32(5))
                            {
                                case 4: sb.Append(Math.Round(rs2.GetDecimal(1), rs2.GetInt32(6)).ToString() + (rs2.GetInt32(7) == 1 ? rs2.GetString(4) : "")); break;
                            }
                            if (rs.GetInt32(4) > 1 || rs2.GetInt32(7) == 0)
                            {
                                sb.Append("</label>");
                            }
                        }
                        rs2.Close();
                    }
                    sb.Append("</span>");
                    // <a class="remove"></a>
                    
                    //actsBox.InnerHtml += "</TD><TD>&nbsp;";
                    if (edit)
                    {
                        if (!rs.IsDBNull(0))
                        {
                            //actsBox.InnerHtml += "" +
                            //    "<IMG ONCLICK=\"actG('" + rs.GetInt32(0) + "'," + ld[rs.GetInt32(5)] + ",'0');\" STYLE=\"cursor:pointer;cursor:hand;\" ALT=\"Idag\" SRC=\"img/graphIcon3.gif\" BORDER=\"0\">" +
                            //    "&nbsp;" +
                            //    "<IMG ONCLICK=\"actG('" + rs.GetInt32(0) + "'," + ld[rs.GetInt32(5)] + ",'1');\" STYLE=\"cursor:pointer;cursor:hand;\" ALT=\"Över tid\" SRC=\"img/graphIcon2.gif\" BORDER=\"0\">" +
                            //    "&nbsp;" +
                            sb.Append("<a class=\"remove\" href=\"javascript:if(confirm('");
                            switch (LID)
                            {
                                case 1:
                                    sb.Append("Är du säker på att du vill ta bort detta värde?");
                                    break;
                                case 2:
                                    sb.Append("Are you sure you want to remove this value?");
                                    break;
                            }
                            sb.Append("')){document.forms[0].DeleteUMID.value='" + rs.GetInt32(0) + "';__doPostBack('','');}\"></a>");
                        }
                        else if (!rs.IsDBNull(6))
                        {
                            //actsBox.InnerHtml += "" +
                            //    "<A HREF=\"statistics.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&AK=" + rs.GetString(6) + "\" STYLE=\"cursor:pointer;cursor:hand;\"><IMG BORDER=\"0\" SRC=\"img/graphIcon2.gif\" BORDER=\"0\"></A>" +
                            //    "&nbsp;" +
                            sb.Append("<a class=\"remove\" href=\"javascript:if(confirm('");
                            switch (LID)
                            {
                                case 1:
                                    sb.Append("Är du säker på att du vill ta bort denna mätning?");
                                    break;
                                case 2:
                                    sb.Append("Are you sure you want to remove this measurement?");
                                    break;
                            }
                            sb.Append("')){document.forms[0].DeleteUPRUA.value='" + rs.GetInt32(7) + ":" + rs.GetString(6) + "';__doPostBack('','');}\"></a>");
                        }
                    }
                    //actsBox.InnerHtml += "</TD></TR>";
                    if (!rs.IsDBNull(6) && (!rs.IsDBNull(8) && rs.GetInt32(8) != 0))
                    {
                        sb.Append("<a href=\"statistics.aspx?AK=" + rs.GetString(6) + "\" class=\"statstoggle\"></a>");
                    }
                    sb.Append("</div>");
                }
                while (rs.Read());
            }
            else
            {
                //actsBox.InnerHtml += "<TR><TD style=\"line-height:21px;\">";
                //switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                //{
                //    case 1:
                //        actsBox.InnerHtml += "Inga mätningar/aktiviteter registrerade.";
                //        break;
                //    case 2:
                //        actsBox.InnerHtml += "No measures/activities registered.";
                //        break;
                //}
                //actsBox.InnerHtml += "</td></tr>";
            }
            rs.Close();
            //actsBox.InnerHtml += "</TABLE>";

            return sb.ToString();
        }

        public static bool sendMail(string from, string email, string body, string subject)
        {
            bool success = false;
            try
            {
                System.Web.Mail.SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
                System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
                mail.To = email;
                mail.From = from;
                mail.Subject = subject;
                mail.Body = body;
                System.Web.Mail.SmtpMail.Send(mail);
                success = true;
            }
            catch (Exception) { }
            return success;
        }
        public static bool checkExtendedSurveys(ref int sponsorExtendedSurveyCount, int sponsorID, int userID, string[] units, string email, int userProfileID)
        {
            bool hasExtendedSurveys = false;

            SqlDataReader rs2 = Db.rs("SELECT " +
                        "ses.SponsorExtendedSurveyID, " +   // 0
                        "ses.ProjectRoundID, " +            // 1
                        "u2.ProjectRoundUserID, " +         // 2
                        "u2.AnswerID, " +                   // 3
                        "ISNULL(d.PreviewExtendedSurveys, si.PreviewExtendedSurveys), " +    // 4
                        "sesd.Ext, " +                      // 5
                        "d.DepartmentShort " +              // 6
                        "FROM SponsorExtendedSurvey ses " +
                        "INNER JOIN [User] u ON u.UserID = " + userID + " " +
                        "LEFT OUTER JOIN Department d ON u.DepartmentID = d.DepartmentID " +
                        "LEFT OUTER JOIN SponsorInvite si ON si.UserID = u.UserID AND si.SponsorID = ses.SponsorID " +
                        "LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON si.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID " +
                        "LEFT OUTER JOIN UserSponsorExtendedSurvey u2 ON ses.SponsorExtendedSurveyID = u2.SponsorExtendedSurveyID AND u2.UserID = u.UserID " +
                        "WHERE sesd.Hide IS NULL AND ses.SponsorID = " + sponsorID);
            while (rs2.Read())
            {
                bool active = false;
                if (rs2.IsDBNull(3))
                {
                    SqlDataReader rs3 = Db.rs("SELECT Started, Closed FROM ProjectRound WHERE ProjectRoundID = " + rs2.GetInt32(1), "eFormSqlConnection");
                    if (rs3.Read())
                    {
                        if (!rs2.IsDBNull(4) || !rs3.IsDBNull(0) && rs3.GetDateTime(0) < DateTime.Now)
                        {
                            if (rs3.IsDBNull(1) || rs3.GetDateTime(1) > DateTime.Now)
                            {
                                active = true;
                            }
                        }
                    }
                    rs3.Close();
                    if (active)
                    {
                        sponsorExtendedSurveyCount++;
                        if (rs2.IsDBNull(2))
                        {
                            #region Create user record
                            int parent = 0;
                            for (int i = 0; i < units.Length; i++)
                            {
                                rs3 = Db.rs("SELECT ProjectRoundUnitID FROM ProjectRoundUnit WHERE ParentProjectRoundUnitID " + (parent == 0 ? "IS NULL" : "= " + parent) + " AND ProjectRoundID = " + rs2.GetInt32(1) + " AND Unit = '" + units[i].Replace("'", "''") + "'", "eFormSqlConnection");
                                if (rs3.Read())
                                {
                                    parent = rs3.GetInt32(0);
                                }
                                else
                                {
                                    rs3.Close();
                                    Db.exec("INSERT INTO ProjectRoundUnit (ProjectRoundID,Unit,ParentProjectRoundUnitID,SurveyID,LangID,UserCount) VALUES (" + rs2.GetInt32(1) + ",'" + units[i].Replace("'", "''") + "'," + (parent == 0 ? "NULL" : parent.ToString()) + ",0,0,0)", "eFormSqlConnection");
                                    rs3 = Db.rs("SELECT ProjectRoundUnitID FROM ProjectRoundUnit WHERE ParentProjectRoundUnitID " + (parent == 0 ? "IS NULL" : "= " + parent) + " AND ProjectRoundID = " + rs2.GetInt32(1) + " AND Unit = '" + units[i].Replace("'", "''") + "'", "eFormSqlConnection");
                                    if (rs3.Read())
                                    {
                                        parent = rs3.GetInt32(0);
                                        Db.exec("UPDATE ProjectRoundUnit SET ID = dbo.cf_unitExtID(ProjectRoundUnitID,dbo.cf_unitDepth(ProjectRoundUnitID),'" + (rs2.IsDBNull(6) ? "" : cutAt(16,rs2.GetString(6)).Replace("'","")) + "'), SortOrder = ProjectRoundUnitID WHERE ProjectRoundUnitID = " + parent, "eFormSqlConnection");
                                        Db.exec("UPDATE ProjectRoundUnit SET SortString = dbo.cf_unitSortString(ProjectRoundUnitID) WHERE ProjectRoundUnitID = " + parent, "eFormSqlConnection");
                                    }
                                }
                                rs3.Close();
                            }
                            if (parent != 0)
                            {
                                Db.exec("INSERT INTO ProjectRoundUser (ProjectRoundID,ProjectRoundUnitID,Email,Extended) VALUES (" + rs2.GetInt32(1) + "," + parent + ",'" + email.Replace("'", "") + "'," + (rs2.IsDBNull(5) ? "NULL" : rs2.GetInt32(5).ToString()) + ")", "eFormSqlConnection");
                                rs3 = Db.rs("SELECT TOP 1 ProjectRoundUserID FROM ProjectRoundUser WHERE ProjectRoundID = " + rs2.GetInt32(1) + " AND ProjectRoundUnitID = " + parent + " AND Email = '" + email.Replace("'", "") + "' ORDER BY ProjectRoundUserID DESC", "eFormSqlConnection");
                                if (rs3.Read())
                                {
                                    Db.exec("INSERT INTO UserSponsorExtendedSurvey (UserID,SponsorExtendedSurveyID,ProjectRoundUserID) VALUES (" + userID + "," + rs2.GetInt32(0) + "," + rs3.GetInt32(0) + ")");
                                    SqlDataReader rs4 = Db.rs("SELECT " +
                                        "s.ProjectRoundQOID, " +    // 0
                                        "s.BQID, " +                // 1
                                        "s.FN, " +                  // 2
                                        "a.OptionComponentID, " +   // 3
                                        "u.ValueInt, " +            // 4
                                        "u.ValueText, " +           // 5
                                        "u.ValueDate, " +           // 6
                                        "BQ.Type " +                // 7
                                        "FROM SponsorExtendedSurveyBQ s " +
                                        "INNER JOIN BQ ON s.BQID = BQ.BQID " +
                                        "INNER JOIN UserProfileBQ u ON u.UserProfileID = " + userProfileID + " AND u.BQID = s.BQID " +
                                        "LEFT OUTER JOIN SponsorExtendedSurveyBA a ON s.ProjectRoundQOID = a.ProjectRoundQOID AND s.SponsorExtendedSurveyID = a.SponsorExtendedSurveyID AND a.BAID = u.ValueInt " +
                                        "WHERE s.SponsorExtendedSurveyID = " + rs2.GetInt32(0));
                                    while (rs4.Read())
                                    {
                                        int QID = 0, OID = 0;
                                        SqlDataReader rs5 = Db.rs("SELECT QuestionID, OptionID FROM ProjectRoundQO WHERE ProjectRoundQOID = " + rs4.GetInt32(0), "eFormSqlConnection");
                                        if (rs5.Read() && !rs5.IsDBNull(0) && !rs5.IsDBNull(1))
                                        {
                                            QID = rs5.GetInt32(0);
                                            OID = rs5.GetInt32(1);
                                        }
                                        rs5.Close();
                                        if (QID != 0 && OID != 0)
                                        {
                                            string val = "";

                                            switch (rs4.GetInt32(7))
                                            {
                                                case 1:
                                                    if (!rs4.IsDBNull(3))
                                                    {
                                                        val = rs4.GetInt32(3).ToString();
                                                    }
                                                    break;
                                                case 2:
                                                    if (!rs4.IsDBNull(5))
                                                    {
                                                        val = rs4.GetString(5);
                                                    }
                                                    break;
                                                case 3:
                                                    if (!rs4.IsDBNull(6))
                                                    {
                                                        DateTime dt = rs4.GetDateTime(6);
                                                        if (!rs4.IsDBNull(2) && rs4.GetInt32(2) == 1)
                                                        {
                                                            TimeSpan ts = DateTime.Now.Subtract(dt);
                                                            val = Math.Round(ts.TotalDays / 365.25, 2).ToString();
                                                        }
                                                        else
                                                        {
                                                            val = dt.ToString("yyyy-MM-dd");
                                                        }
                                                    }
                                                    break;
                                                case 4:
                                                    if (!rs4.IsDBNull(4))
                                                    {
                                                        val = rs4.GetInt32(4).ToString();
                                                    }
                                                    break;
                                                case 6: goto case 1;
                                                case 7: goto case 1;
                                            }

                                            if (val != "")
                                            {
                                                Db.exec("INSERT INTO ProjectRoundUserQO (ProjectRoundUserID,QuestionID,OptionID,Answer) VALUES (" + rs3.GetInt32(0) + "," + QID + "," + OID + ",'" + val.Replace("'", "''") + "')", "eFormSqlConnection");
                                            }
                                        }
                                    }
                                    rs4.Close();
                                }
                                rs3.Close();
                            }
                            #endregion
                        }
                    }
                }
                if (active || !rs2.IsDBNull(3))
                {
                    hasExtendedSurveys = true;
                }
            }
            rs2.Close();

            return hasExtendedSurveys;
        }
        public static void sendPasswordReminder(string email, int userID)
        {
            sendPasswordReminder(email, userID, true);
        }
        public static void sendPasswordReminder(string email, int userID, bool login)
        {
            SqlDataReader rs = Db.rs("SELECT UserID, Email, Username, LEFT(REPLACE(CONVERT(VARCHAR(255),UserKey),'-',''),8) FROM [User] WHERE " +
                (email != "" ? "Email = '" + email.ToString().Replace("'", "") + "'" : "UserID = " + userID));
            while (rs.Read())
            {
                if (Db.isEmail(rs.GetString(1)))
                {
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1:
                            Db.sendMail("support@healthwatch.se", rs.GetString(1),
                                "Hej." +
                                "\r\n\r\n" +
                                "En begäran om nytt lösenord till ditt konto med användarnamn \"" + rs.GetString(2) + "\" har inkommit. Om du begärt detta, klicka på länken nedan för att ange ett nytt lösenord." +
                                "\r\n\r\n" +
                                "https://www.healthwatch.se/password.aspx?" + (login ? "" : "NL=1&") + "K=" + rs.GetString(3) + rs.GetInt32(0) + "" +
                                "\r\n\r\n" +
                                "Begäran gjordes " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + " från IP-adress " + HttpContext.Current.Request.UserHostAddress,
                                "Nytt lösenord");
                            break;
                        case 2:
                            Db.sendMail("support@healthwatch.se", rs.GetString(1),
                                "Hi." +
                                "\r\n\r\n" +
                                "A request for new password for your account with username \"" + rs.GetString(2) + "\" has arrived. If you made this request, click the link below to set a new password." +
                                "\r\n\r\n" +
                                "https://www.healthwatch.se/password.aspx?" + (login ? "" : "NL=1&") + "K=" + rs.GetString(3) + rs.GetInt32(0) + "" +
                                "\r\n\r\n" +
                                "The request was made at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + " from IP-address " + HttpContext.Current.Request.UserHostAddress,
                                "New password");
                            break;
                    }
                }
            }
            rs.Close();
        }

		public static void reloadApplication()
		{
			SqlDataReader rs = Db.rs("SELECT LEFT(REPLACE(CONVERT(VARCHAR(255),SponsorKey),'-',''),8), Application, Sponsor FROM Sponsor WHERE SponsorID = 1");
			if (rs.Read())
			{
				HttpContext.Current.Application["SPK1"] = rs.GetString(0);
				HttpContext.Current.Application["APP1"] = rs.GetString(1);
                HttpContext.Current.Application["SPN1"] = rs.GetString(2);
			}
			rs.Close();
		}
		public static SqlDataReader rs(string sqlString)
		{
			return rs(sqlString,"SqlConnection");
		}
		public static SqlDataReader rs(string sqlString, string con)
		{
			SqlConnection dataConnection = new SqlConnection(ConfigurationSettings.AppSettings[con]);
			dataConnection.Open();
			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
            dataCommand.CommandTimeout = 300;
			SqlDataReader dataReader = dataCommand.ExecuteReader(CommandBehavior.CloseConnection);
			return dataReader;
		}

		public static void exec(string sqlString)
		{
			exec(sqlString,"SqlConnection");
		}
		public static void exec(string sqlString, string con)
		{
			SqlConnection dataConnection = new SqlConnection(ConfigurationSettings.AppSettings[con]);
			dataConnection.Open();
			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
            dataCommand.CommandTimeout = 300;
			dataCommand.ExecuteNonQuery();
			dataConnection.Close();
			dataConnection.Dispose();
		}

        public static int execScalar(string sqlString)
        {
            return execScalar(sqlString, "SqlConnection");
        }
        public static int execScalar(string sqlString, string con)
        {
            SqlConnection dataConnection = new SqlConnection(ConfigurationSettings.AppSettings[con]);
            dataConnection.Open();
            SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
            dataCommand.CommandTimeout = 300;
            int ret = (int)dataCommand.ExecuteScalar();
            dataConnection.Close();
            dataConnection.Dispose();

            return ret;
        }

        public static string header()
        {
            return header("","");
        }
		public static string header(string title, string desc)
		{
            string rawUrl = HttpContext.Current.Request.RawUrl.ToLower();

			if (HttpContext.Current.Application["APP" + HttpContext.Current.Session["SponsorID"].ToString()] == null)
			{
				reloadApplication();
			}
            string ret = "<TITLE>" + (title != "" ? title.Replace("\r", "").Replace("\n","") + " - " : "") + HttpContext.Current.Application["APP" + HttpContext.Current.Session["SponsorID"].ToString()].ToString() + "</TITLE>";
			ret += "<link href=\"/main.css?V=" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\" rel=\"stylesheet\" type=\"text/css\" />";
            ret += (desc != "" ? "<meta name=\"description\" content=\"" + desc.Replace("\"","").Replace("\r","").Replace("\n","") + "\" />" : "");
            ret += "<meta http-equiv=\"Pragma\" content=\"no-cache\" />";
			ret += "<meta http-equiv=\"Expires\" content=\"-1\" />";
			ret += "<meta name=\"Robots\" content=\"noarchive\" />";
			ret += "<meta name=\"verify-v1\" content=\"MG9W505CkUlEDTI4UYmzrsKY4n1GwRdsOrytciFyUI8=\" />";
			ret += "\r\n";
			ret += "<SCRIPT LANGUAGE=\"JavaScript\">eval(function(p,a,c,k,e,d){e=function(c){return(c<a?'':e(parseInt(c/a)))+((c=c%a)>35?String.fromCharCode(c+29):c.toString(36))};if(!''.replace(/^/,String)){while(c--)d[e(c)]=k[c]||e(c);k=[function(e){return d[e]}];e=function(){return'\\\\w+'};c=1};while(c--)if(k[c])p=p.replace(new RegExp('\\\\b'+e(c)+'\\\\b','g'),k[c]);return p}('9 e(4,s){3 6=4.o(\\' \\');3 n=6[0];3 f=6[1];6[0]=\"\";6[1]=\"\";4=6.H(\" \").G(2);3 g=\\'\\';3 j=4.o(\\' \\');c(3 i F j){3 m=j[i];3 h=k(m,n,f);d(s&&i<7)E;d(s&&h==D)C;g+=B.A(h)}a g}9 z(4){x.w=e(4,v)}9 u(4,n,f){r.q(e(4,l));a l}9 k(b,8,y){d(y%2==0){5=1;c(3 i=1;i<=y/2;i++){t=(b*b)%8;5=(t*5)%8}}p{5=b;c(3 i=1;i<=y/2;i++){t=(b*b)%8;5=(t*5)%8}}a 5}',44,44,'|||var|cds|ar|ns||ex|function|return||for|if|ds|dk|dds|ddc||ccs|em|true|cc||split|else|write|document|||de|false|location|parent||dm|fromCharCode|String|break|63|continue|in|substr|join'.split('|'),0,{}))</SCRIPT>";
			ret += "<script language=\"javascript\">AC_FL_RunContent = 0;</script><script src=\"/AC_RunActiveContent.js\" language=\"javascript\"></script>";
            ret += (
                rawUrl.IndexOf("home.aspx") >= 0
                ||
                rawUrl.IndexOf("default.aspx") >= 0
                ||
                rawUrl.IndexOf("news.aspx") >= 0
                ||
                rawUrl.IndexOf("/news/") >= 0
                ||
                rawUrl.EndsWith("/news")
                ||
                rawUrl.Equals("/")
                ? "" : "<script language=\"JavaScript\">window.history.forward(1);</script>");
			return ret;
		}
        public static string header2(string title, string desc)
        {
            return header2(title, desc, "");
        }
        public static string header2(string title, string desc, string replacementHead)
        {
            string rawUrl = HttpContext.Current.Request.RawUrl.ToLower();

            if (HttpContext.Current.Application["APP" + HttpContext.Current.Session["SponsorID"].ToString()] == null)
            {
                reloadApplication();
            }
            StringBuilder ret = new StringBuilder();
            ret.Append("<meta charset=\"utf-8\"/>" + "\r\n");
            ret.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\">" + "\r\n");
            ret.Append("<!--<meta name=\"viewport\" content=\"width=device-width; initial-scale=1.0; maximum-scale=1.0;\">-->" + "\r\n");
            ret.Append("<meta http-equiv=\"Pragma\" content=\"no-cache\" />" + "\r\n");
            ret.Append("<meta http-equiv=\"Expires\" content=\"-1\" />" + "\r\n");
            ret.Append("<meta name=\"Robots\" content=\"noarchive\" />" + "\r\n");
            ret.Append("<meta name=\"verify-v1\" content=\"MG9W505CkUlEDTI4UYmzrsKY4n1GwRdsOrytciFyUI8=\" />" + "\r\n");
            ret.Append("<title>");
            ret.Append((title != "" ? title.Replace("\r", "").Replace("\n", "") + " - " : ""));
            if (Convert.ToInt32(HttpContext.Current.Application["SUPERSPONSOR" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"])]) > 0 && HttpContext.Current.Application["SUPERSPONSORHEAD" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + "LANG" + Convert.ToInt32(HttpContext.Current.Session["LID"])] != null)
            {
                ret.Append("HealthWatch - " + HttpContext.Current.Application["SUPERSPONSORHEAD" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + "LANG" + Convert.ToInt32(HttpContext.Current.Session["LID"])]);
            }
            else
            {
                ret.Append("HealthWatch");
            }
            ret.Append("</title>" + "\r\n");
            ret.Append("<link rel=\"shortcut icon\" href=\"/favicon.ico\">" + "\r\n");
            ret.Append("<link rel=\"apple-touch-icon\" href=\"/apple-touch-icon.png\">" + "\r\n");

            if (replacementHead != "")
            {
                ret.Append(replacementHead);
            }
            else
            {
                ret.Append("<link type=\"text/css\" rel=\"stylesheet\" href=\"/includes/css/960.css\">" + "\r\n");
                ret.Append("<link type=\"text/css\" rel=\"stylesheet\" href=\"/includes/css/site.css\" />" + "\r\n");
                ret.Append("<link type=\"text/css\" href=\"/includes/ui/css/ui-lightness/jquery-ui-1.8.11.custom.css\" rel=\"Stylesheet\" />" + "\r\n");
                ret.Append("<script type=\"text/javascript\" src=\"/includes/ui/js/jquery-1.5.1.min.js\"></script>" + "\r\n");
                ret.Append("<script type=\"text/javascript\" src=\"/includes/ui/js/jquery-ui-1.8.11.custom.min.js\"></script>" + "\r\n");
                if (desc != "")
                {
                    ret.Append("<meta name=\"description\" content=\"" + desc.Replace("\"", "").Replace("\r", "").Replace("\n", "") + "\" />" + "\r\n");
                }
                ret.Append("<script type=\"text/JavaScript\">eval(function(p,a,c,k,e,d){e=function(c){return(c<a?'':e(parseInt(c/a)))+((c=c%a)>35?String.fromCharCode(c+29):c.toString(36))};if(!''.replace(/^/,String)){while(c--)d[e(c)]=k[c]||e(c);k=[function(e){return d[e]}];e=function(){return'\\\\w+'};c=1};while(c--)if(k[c])p=p.replace(new RegExp('\\\\b'+e(c)+'\\\\b','g'),k[c]);return p}('9 e(4,s){3 6=4.o(\\' \\');3 n=6[0];3 f=6[1];6[0]=\"\";6[1]=\"\";4=6.H(\" \").G(2);3 g=\\'\\';3 j=4.o(\\' \\');c(3 i F j){3 m=j[i];3 h=k(m,n,f);d(s&&i<7)E;d(s&&h==D)C;g+=B.A(h)}a g}9 z(4){x.w=e(4,v)}9 u(4,n,f){r.q(e(4,l));a l}9 k(b,8,y){d(y%2==0){5=1;c(3 i=1;i<=y/2;i++){t=(b*b)%8;5=(t*5)%8}}p{5=b;c(3 i=1;i<=y/2;i++){t=(b*b)%8;5=(t*5)%8}}a 5}',44,44,'|||var|cds|ar|ns||ex|function|return||for|if|ds|dk|dds|ddc||ccs|em|true|cc||split|else|write|document|||de|false|location|parent||dm|fromCharCode|String|break|63|continue|in|substr|join'.split('|'),0,{}))</script>" + "\r\n");
                ret.Append("<script type=\"text/JavaScript\">AC_FL_RunContent = 0;</script><script src=\"/AC_RunActiveContent.js\" language=\"javascript\"></script>" + "\r\n");

                ret.Append("<script type=\"text/javascript\">");
                ret.Append("$(document).ready(function () {");
                ret.Append("$(\"#hide\").toggle(function () {");
                ret.Append("$(\"#underlay\").hide();");
                ret.Append("}, function () {");
                ret.Append("$(\"#underlay\").show();");
                ret.Append("});");
                ret.Append("$(\"#hide_ol\").toggle(function () {");
                ret.Append("$(\".index\").hide();");
                ret.Append("}, function () {");
                ret.Append("$(\".index\").show();");
                ret.Append("});");
                ret.Append("$('[placeholder]').focus(function () {");
                ret.Append("var input = $(this);");
                ret.Append("if (input.val() == input.attr('placeholder')) {");
                ret.Append("input.val('');");
                ret.Append("input.removeClass('placeholder');");
                ret.Append("}");
                ret.Append("}).blur(function () {");
                ret.Append("var input = $(this);");
                ret.Append("if (input.val() == '' || input.val() == input.attr('placeholder')) {");
                ret.Append("input.addClass('placeholder');");
                ret.Append("input.val(input.attr('placeholder'));");
                ret.Append("}");
                ret.Append("}).blur();");
                ret.Append("});");
                ret.Append("</script>" + "\r\n");
            }

            if (!
                (
                rawUrl.IndexOf("home.aspx") >= 0
                ||
                rawUrl.IndexOf("default.aspx") >= 0
                ||
                rawUrl.IndexOf("news.aspx") >= 0
                ||
                rawUrl.IndexOf("/news/") >= 0
                ||
                rawUrl.EndsWith("/news")
                ||
                rawUrl.Equals("/")
                )
                )
            {
                ret.Append("<script type=\"text/JavaScript\">window.history.forward(1);</script>" + "\r\n");
            }

            return ret.ToString();
        }

        public static string bottom2()
        {
            StringBuilder ret = new StringBuilder();
             string 
                txtLogin = "Logga in", 
                txtForgot = "Glömt ditt lösenord?",
                txtRegister = "Registrera dig nu!";
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: 
                    { 
                        txtLogin = "Log in"; 
                        txtForgot = "Forgot your password?";
                        txtRegister = "Register now!";
                        break; 
                    }
            }

            bool loggedIn = (HttpContext.Current.Session["UserID"] != null && Convert.ToInt32("0" + HttpContext.Current.Session["UserID"]) != 0);
            string rawUrl = HttpContext.Current.Request.RawUrl.ToLower();

            string redir = "";
            //if (loggedIn)
            //{
                try
                {
                    string s = rawUrl.Substring(rawUrl.LastIndexOf("/") + 1);
                    if (s != "")
                    {
                        redir = "&Goto=" + s;
                        if (redir.IndexOf("aspx") >= 0)
                        {
                            redir = redir.Substring(0, redir.IndexOf("aspx") + 4);
                        }
                    }
                }
                catch (Exception) { }
            //}
            
            ret.Append("<div class=\"footergroup grid_16\">");
            ret.Append("<hr />");
            ret.Append("<div class=\"logogroup\">");
            ret.Append("<img src=\"/images/hwlogo_monochrome.gif\" width=\"91\" height=\"60\" alt=\"Hwlogo Monochrome\"><br /><br />");
            //ret.Append("<img src=\"/images/eformlogo.gif\" width=\"118\" height=\"47\" alt=\"Eformlogo\"><br /><br />");
            ret.Append("&copy; <a href=\"javascript:dm('2323 1467 1118 2057 771 646 2163 1707 2303 771 2244 1920 1707 1968 532 1212 2057 646 2163 532 984 2057 2163 1608 532 2093 1633 1212');\">HealthWatch</a> " + DateTime.Now.Year + ".");
            ret.Append("</div>");
            ret.Append("<div class=\"newsgroup\">");
            ret.Append("<a href=\"/\" class=\"groupheader\">");
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: { ret.Append("NYHETER"); break; }
                case 2: { ret.Append("NEWS"); break; }
            }
            ret.Append("</a>");
            ret.Append("<ul>");
            SqlDataReader rs = Db.rs("SELECT " +
                "c.NewsCategoryID, " +
                "cl.NewsCategory, " +
                "c.NewsCategoryShort " +
                "FROM NewsCategory c " +
                "INNER JOIN NewsCategoryLang cl ON c.NewsCategoryID = cl.NewsCategoryID AND cl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                "WHERE " +
                "(" +
                "SELECT COUNT(*) " +
                "FROM News x " +
                "WHERE x.NewsCategoryID = c.NewsCategoryID " +
                (Convert.ToInt32(HttpContext.Current.Session["LID"]) != 1 ? "AND x.LinkLangID = 1 " : "") +
                "AND DATEADD(m,1,x.DT) > GETDATE()" +
                ") > 0 " +
                "ORDER BY c.NewsCategory", "newsSqlConnection");
            while (rs.Read())
            {
                ret.Append("<li><a href=\"/news/" + rs.GetString(2) + "\">" + rs.GetString(1) + "</a></li>");
            }
            rs.Close();
            ret.Append("</ul>");
            ret.Append("</div>");
			
            #region My health
			ret.Append("<div class=\"healthgroup\">");
            ret.Append("<a class=\"groupheader\" href=\"/myhealth\">");
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: { ret.Append("MIN HÄLSA"); break; }
                case 2: { ret.Append("MY HEALTH"); break; }
            }
            ret.Append("</a>");
			ret.Append("<ul>");
            if(loggedIn)
            {
                ret.Append("<li><a href=\"/calendar.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("Dagbok"); break; }
                    case 2: { ret.Append("Calendar"); break; }
                }
                ret.Append("</a></li>");
                ret.Append("<li><a href=\"/submit.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("Formulär"); break; }
                    case 2: { ret.Append("Forms"); break; }
                }
                ret.Append("</a></li>");
                ret.Append("<li><a href=\"/statistics.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("Statistik"); break; }
                    case 2: { ret.Append("Statistics"); break; }
                }
                ret.Append("</a></li>");
                ret.Append("<li><a href=\"/exercise.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("Övningar"); break; }
                    case 2: { ret.Append("Exercises"); break; }
                }
                ret.Append("</a></li>");

                if (HttpContext.Current.Session["HasExtendedSurveys"] != null && Convert.ToInt32(HttpContext.Current.Session["HasExtendedSurveys"]) == 1)
                {
                    ret.Append("<li><a href=\"/extendedSurveys.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">");
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret.Append("Enkäter"); break; }
                        case 2: { ret.Append("Surveys"); break; }
                    }
                    ret.Append("</a></li>");
                }
                if (HttpContext.Current.Session["Username"].ToString() != "Gäst")
                {
                    ret.Append("<li><a href=\"/reminder.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">");
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret.Append("Påminnelser"); break; }
                        case 2: { ret.Append("Reminders"); break; }
                    }
                    ret.Append("</a></li>");
                }
            }
            else
            {
                ret.Append("<li><a href=\"#\">" + txtLogin + "</a></li>");
                ret.Append("<li><a href=\"/forgot.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">" + txtForgot + "</a></li>");
                ret.Append("<li><a href=\"/register.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">" + txtRegister + "</a></li>");
            }
			ret.Append("</ul>");
			ret.Append("</div>");
            #endregion

			ret.Append("<div class=\"aboutgroup\">");
            ret.Append("<a href=\"#\" class=\"groupheader\">");
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: { ret.Append("OM TJÄNSTEN"); break; }
                case 2: { ret.Append("ABOUT HEALTHWATCH"); break; }
            }
            ret.Append("</a>");
			ret.Append("<ul>");
			ret.Append("<li><a href=\"/about\">");
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: { ret.Append("Om HealthWatch"); break; }
                case 2: { ret.Append("About HealthWatch"); break; }
            }
            ret.Append("</a></li>");
            ret.Append("<li><a href=\"/faq\">");
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: { ret.Append("Hjälp med tjänsten"); break; }
                case 2: { ret.Append("Help"); break; }
            }
            ret.Append("</a></li>");
            ret.Append("<li><a href=\"/contact\">");
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: { ret.Append("Kontaktinformation"); break; }
                case 2: { ret.Append("Contact"); break; }
            }
            ret.Append("</a></li>");
			ret.Append("</ul>");
            if (HttpContext.Current.Session["ForceLID"] == null)
            {
                ret.Append("<br />");
                ret.Append("<ul>");
                ret.Append("<li><a href=\"/home.aspx?LID=");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("2"); break; }
                    case 2: { ret.Append("1"); break; }
                }

                ret.Append(redir + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"> ");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("In English"); break; }
                    case 2: { ret.Append("På svenska"); break; }
                }
                ret.Append("</a></li>");
                ret.Append("</ul>");
            }
			ret.Append("</div>");
            ret.Append("<div class=\"eugroup\">");
            ret.Append("<img src=\"/images/eformlogo.gif\" width=\"118\" height=\"47\" alt=\"Eformlogo\"><br /><br />");
            //switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            //{
            //    case 1: { ret.Append("HealthWatch används i projektet Arbeta med Flyt som finansieras av"); break; }
            //    case 2: { ret.Append("HealthWatch is used in the project Work with Flow funded by"); break; }
            //}
            //ret.Append("<br />");
            //ret.Append("<img src=\"/images/eu.gif\" width=\"100\" height=\"67\" alt=\"Eu\">");
            //ret.Append("<br />");
            //switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            //{
            //    case 1: { ret.Append("<div>EUROPEISKA UNIONEN<br />Europeiska socialfonden</div>"); break; }
            //    case 2: { ret.Append("<div>EUROPEAN UNION<br />European Social Fund</div>"); break; }
            //}
            ret.Append("</div>");
            ret.Append("</div><!-- end .footergroup -->");

            ret.Append(
                "<script type=\"text/javascript\">" +
                "var gaJsHost = ((\"https:\" == document.location.protocol) ? \"https://ssl.\" : \"http://www.\");" +
                "document.write(unescape(\"%3Cscript src='\" + gaJsHost + \"google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E\"));" +
                "</script>" +
                "<script type=\"text/javascript\">" +
                "try {" +
                "var pageTracker = _gat._getTracker(\"UA-6746933-" + (HttpContext.Current.Request.IsSecureConnection ? "3" : "1") + "\");" +
                "pageTracker._trackPageview();" +
                "} catch(err) {}</script>"
                );

            return ret.ToString();
        }
        public static string bottom()
        {
			string ret = "" +
				"</td></table>" +
				"</div><div id=\"copyright\">&copy; HealthWatch " + DateTime.Now.Year + ". <A class=\"lnk\" HREF=\"javascript:dm('2323 1467 1118 2057 771 646 2163 1707 2303 771 2244 1920 1707 1968 532 1212 2057 646 2163 532 984 2057 2163 1608 532 2093 1633 1212');\">";
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret += "Kontakta oss"; break; }
                        case 2: { ret += "Contact us"; break; }
                    }
            ret += "</A>.</div>" +
				"</div>" +
                "<script type=\"text/javascript\">" +
                "var gaJsHost = ((\"https:\" == document.location.protocol) ? \"https://ssl.\" : \"http://www.\");" +
                "document.write(unescape(\"%3Cscript src='\" + gaJsHost + \"google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E\"));" +
                "</script>" +
                "<script type=\"text/javascript\">" +
                "try {" +
                "var pageTracker = _gat._getTracker(\"UA-6746933-" + (HttpContext.Current.Request.IsSecureConnection ? "3" : "1") + "\");" +
                "pageTracker._trackPageview();" +
                "} catch(err) {}</script>";

            return ret;
            //return "</div></div>";
        }
        public static string wiseOfToday()
        {
            SqlDataReader rs = Db.rs("SELECT wl.Wise, wl.WiseBy FROM WiseLang wl INNER JOIN Wise w ON wl.WiseID = w.WiseID WHERE w.LastShown = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND wl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]));
            if (!rs.Read())
            {
                rs.Close();
                rs = Db.rs("SELECT TOP 1 wl.Wise, wl.WiseBy, w.WiseID FROM WiseLang wl INNER JOIN Wise w ON wl.WiseID = w.WiseID WHERE wl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " ORDER BY w.LastShown ASC");
                rs.Read();
                Db.exec("UPDATE Wise SET LastShown = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' WHERE WiseID = " + rs.GetInt32(2));
            }

            StringBuilder ret = new StringBuilder();

            ret.Append("<div class=\"label\">");
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: ret.Append("Dagens visdomsord"); break;
                case 2: ret.Append("Today's words of wisdom"); break;
            }
            ret.Append("</div>");
            ret.Append("<div class=\"kirkegaard\">");
            ret.Append(rs.GetString(0).Replace(HttpContext.Current.Server.HtmlDecode("&nbsp;")," "));
            ret.Append("</div>");
            ret.Append("<div class=\"author\">- ");
            ret.Append(rs.GetString(1).Replace(HttpContext.Current.Server.HtmlDecode("&nbsp;"), " "));
            ret.Append("</div>");

            rs.Close();

            return ret.ToString();
        }
        public static string mostRead()
        {
            StringBuilder ret = new StringBuilder();
            ret.Append("<div class=\"label\">");
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: ret.Append("Mest lästa nyheter"); break;
                case 2: ret.Append("Most read articles"); break;
            }
            ret.Append("</div>");
            ret.Append("<ul>");

            System.Collections.ArrayList al = new System.Collections.ArrayList();
            SqlDataReader rs = Db.rs("SELECT TOP 10 " +
                 "COUNT(*), " +
                 "n.NewsID, " +                          // 1
                 "n.Headline, " +                        // 2
                 "n.HeadlineShort, " +                   // 3
                 "c.NewsCategoryShort " +                // 4
                 "FROM News n " +
                 "INNER JOIN NewsRead nr ON n.NewsID = nr.NewsID " +
                 "LEFT OUTER JOIN NewsCategory c ON n.NewsCategoryID = c.NewsCategoryID " +
                 "WHERE n.Published IS NOT NULL " +
                 "AND n.Deleted IS NULL " +
                 (Convert.ToInt32(HttpContext.Current.Session["LID"]) != 1 ? "AND n.LinkLangID = 1 " : "") +
                 "AND n.DT > '" + DateTime.Now.AddDays(-7).Date.ToString("yyyy-MM-dd").Replace("'", "") + "' " +
                 "" + (HttpContext.Current.Request.QueryString["NCID"] != null ? "AND n.NewsCategoryID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["NCID"]) + " " : "AND n.OnlyInCategory IS NULL ") + "" +
                 "GROUP BY " +
                 "n.NewsID, " +                          // 1
                 "n.Headline, " +                        // 2
                 "n.HeadlineShort, " +                   // 3
                 "c.NewsCategoryShort " +                // 4
                 "ORDER BY COUNT(*) DESC", "newsSqlConnection");
            while (rs.Read())
            {
                if (!al.Contains(rs.GetInt32(1)))
                {
                    al.Add(rs.GetInt32(1));
                }
                ret.Append("<li><a href=\"/news/" + (!rs.IsDBNull(4) ? rs.GetString(4) + "/" : "") + rs.GetString(3) + "\">" + rs.GetString(2) + "</a></li>");
            }
            rs.Close();
            if (al.Count < 5)
            {
                rs = Db.rs("SELECT TOP 10 " +
                 "COUNT(*), " +
                 "n.NewsID, " +                          // 1
                 "n.Headline, " +                        // 2
                 "n.HeadlineShort, " +                   // 3
                 "c.NewsCategoryShort " +                // 4
                 "FROM News n " +
                 "INNER JOIN NewsRead nr ON n.NewsID = nr.NewsID " +
                 "LEFT OUTER JOIN NewsCategory c ON n.NewsCategoryID = c.NewsCategoryID " +
                 "WHERE n.Published IS NOT NULL " +
                 "AND n.Deleted IS NULL " +
                 (Convert.ToInt32(HttpContext.Current.Session["LID"]) != 1 ? "AND n.LinkLangID = 1 " : "") +
                 "AND n.DT > '" + DateTime.Now.AddDays(-60).Date.ToString("yyyy-MM-dd").Replace("'", "") + "' " +
                 "" + (HttpContext.Current.Request.QueryString["NCID"] != null ? "AND n.NewsCategoryID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["NCID"]) + " " : "AND n.OnlyInCategory IS NULL ") + "" +
                 "GROUP BY " +
                 "n.NewsID, " +                          // 1
                 "n.Headline, " +                        // 2
                 "n.HeadlineShort, " +                   // 3
                 "c.NewsCategoryShort " +                // 4
                 "ORDER BY COUNT(*) DESC", "newsSqlConnection");
                while (rs.Read())
                {
                    if (!al.Contains(rs.GetInt32(1)))
                    {
                        al.Add(rs.GetInt32(1));
                        ret.Append("<li><a href=\"/news/" + (!rs.IsDBNull(4) ? rs.GetString(4) + "/" : "") + rs.GetString(3) + "\">" + rs.GetString(2) + "</a></li>");
                    }
                }
                rs.Close();
            }
					
            ret.Append("</ul>");

            return ret.ToString();
        }
        public static string nav2()
        {
            StringBuilder ret = new StringBuilder();
            SqlDataReader r;

            #region vars
            bool loggedIn = (HttpContext.Current.Session["UserID"] != null && Convert.ToInt32("0" + HttpContext.Current.Session["UserID"]) != 0);
            string cssAddon = (loggedIn ? "" : "News");
            string rawUrl = HttpContext.Current.Request.RawUrl.ToLower();
            
            bool myPage = loggedIn && (
                rawUrl.IndexOf("calendar.aspx") >= 0
                ||
                rawUrl.IndexOf("calendarread.aspx") >= 0
                ||
                rawUrl.IndexOf("submit.aspx") >= 0
                ||
                rawUrl.IndexOf("statistics.aspx") >= 0
                ||
                rawUrl.IndexOf("profile.aspx") >= 0
                ||
                rawUrl.IndexOf("exercise.aspx") >= 0
                ||
                rawUrl.IndexOf("reminder.aspx") >= 0
                ||
                rawUrl.IndexOf("extendedsurvey.aspx") >= 0
                ||
                rawUrl.IndexOf("extendedsurveys.aspx") >= 0
                );
            bool aboutMyPage = rawUrl.EndsWith("/myhealth") || rawUrl.IndexOf("forgot.aspx") >= 0 || rawUrl.IndexOf("register.aspx") >= 0 || rawUrl.IndexOf("password.aspx") >= 0 || rawUrl.IndexOf("closed.aspx") >= 0;
            bool aboutPage = (
                rawUrl.IndexOf("contact.aspx") >= 0
                ||
                rawUrl.EndsWith("/contact")
                ||
                rawUrl.IndexOf("upload.aspx") >= 0
                ||
                rawUrl.EndsWith("/upload")
                ||
                rawUrl.IndexOf("faq.aspx") >= 0
                ||
                rawUrl.EndsWith("/faq")
                ||
                rawUrl.IndexOf("about.aspx") >= 0
                ||
                rawUrl.EndsWith("/about")
                );
            bool newsPage = (
                !aboutPage && !myPage && !aboutMyPage
                //rawUrl.IndexOf("home.aspx") >= 0
                //||
                //rawUrl.IndexOf("default.aspx") >= 0
                //||
                //rawUrl.IndexOf("news.aspx") >= 0
                //||
                //rawUrl.IndexOf("/news/") >= 0
                //||
                //rawUrl.EndsWith("/news")
                //||
                //rawUrl.Equals("/")
                );

            bool showLoginBar =
                rawUrl.IndexOf("register") == -1
                &&
                rawUrl.IndexOf("forgot") == -1
                &&
                rawUrl.IndexOf("password") == -1
                &&
                rawUrl.IndexOf("sponsorinformation") == -1
                &&
                rawUrl.IndexOf("sponsorconsent") == -1;

            string cssAddon2 = (!newsPage ? "" : "News");
            string cssContentStart = (loggedIn ? "contentStartAuth" : "contentStart");

            string 
                txtLogin = "Logga in", 
                txtUsername = "Användarnamn", 
                txtPassword = "Lösenord",
                txtForgot = "Glömt ditt lösenord?",
                txtRegister = "Registrera dig nu!";
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: 
                    { 
                        txtLogin = "Log in"; 
                        txtUsername = "Username"; 
                        txtPassword = "Password";
                        txtForgot = "Forgot your password?";
                        txtRegister = "Register now!";
                        break; 
                    }
            }
            #endregion

			ret.Append("<div class=\"grid_3 alpha\"><img src=\"/images/hwlogo.png\" width=\"186\" height=\"126\" alt=\"Hwlogo\"></div>");
			ret.Append("<div class=\"grid_8 omega p2\">");
            ret.Append(cobranding());

            string redir = "";
            if (HttpContext.Current.Session["ForceLID"] == null)
            {
                //if (loggedIn)
                //{
                try
                {
                    string s = rawUrl.Substring(rawUrl.LastIndexOf("/") + 1);
                    if (s != "")
                    {
                        redir = "&Goto=" + s;
                        if (redir.IndexOf("aspx") >= 0)
                        {
                            redir = redir.Substring(0, redir.IndexOf("aspx") + 4);
                        }
                    }
                }
                catch (Exception) { }
                //}
                ret.Append("<a href=\"/home.aspx?LID=");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("2"); break; }
                    case 2: { ret.Append("1"); break; }
                }
                ret.Append(redir + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\" class=\"regular\"> ");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("In English"); break; }
                    case 2: { ret.Append("På svenska"); break; }
                }

                ret.Append("</a>");
            }
            else
            {
                ret.Append("<img src=\"img/null.gif\" width=\"1\" height=\"20\" border=\"0\" />");
            }
			ret.Append("<div id=\"menu\">");
            ret.Append("<a class=\"news" + (newsPage ? " active" : "") + "\" href=\"/\"><span class=\"lfloat\">");
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: { ret.Append("Nyheter"); break; }
                case 2: { ret.Append("News"); break; }
            }
            ret.Append("</span> <span class=\"arrow\">&nbsp;</span></a>");
            ret.Append("<a class=\"myhealth" + (myPage || aboutMyPage ? " active" : "") + "\" href=\"/myhealth\"><span class=\"lfloat\">");
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: { ret.Append("Min hälsa"); break; }
                case 2: { ret.Append("My health"); break; }
            }
            ret.Append("</span> <span class=\"arrow\">&nbsp;</span></a>");
            ret.Append("<a class=\"about" + (aboutPage ? " active" : "") + "\" href=\"/about\"><span class=\"lfloat\">");
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: { ret.Append("Om tjänsten"); break; }
                case 2: { ret.Append("About"); break; }
            }
            ret.Append("</span> <span class=\"arrow\">&nbsp;</span></a>");
			ret.Append("</div>");
			ret.Append("</div>");

            if (showLoginBar)
            {
                if (!loggedIn)
                {
                    ret.Append("<div class=\"logincontainer grid_5 alpha omega\">");
                    ret.Append("<div class=\"login_label\">" + txtLogin + "</div>");
                    ret.Append("<input type=\"text\" placeholder=\"" + txtUsername + "\" name=\"Usern\"" + (HttpContext.Current.Request.Form["Usern"] != null ? " value=\"" + HttpContext.Current.Request.Form["Usern"].ToString() + "\"" : "") + " />");
                    ret.Append("<input type=\"password\" placeholder=\"" + txtPassword + "\" name=\"Losen\" />");
                    ret.Append("<input type=\"submit\" value=\"\" /> <br />");
                    ret.Append("<a href=\"/forgot.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\" class=\"support regular\">" + txtForgot + "</a>");
                    ret.Append("<a href=\"/register.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\" class=\"support regular\">" + txtRegister + "</a>");
                    ret.Append("</div>");
                }
                else
                {
                    ret.Append("<div class=\"logincontainer loggedin grid_5 alpha omega\">");
				    ret.Append("<img src=\"/includes/resources/avatars/default.gif\" width=\"40\" height=\"40\" alt=\"Default\">");
				    ret.Append("<div>");
                    ret.Append("<span><span class=\"grey\">");
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret.Append("Inloggad som"); break; }
                        case 2: { ret.Append("Logged in as"); break; }
                    }
                    ret.Append(" </span>");
                    ret.Append("" + HttpContext.Current.Session["Username"] + "");
                    ret.Append("</span><br />");
                    ret.Append("<a href=\"/profile.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\" class=\"regular\">");
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret.Append(HttpContext.Current.Session["Username"].ToString() == "Gäst" ? "Gör detta till ditt konto" : "Ändra profil"); break; }
                        case 2: { ret.Append(HttpContext.Current.Session["Username"].ToString() == "Gäst" ? "Turn this into your account" : "Change profile"); break; }
                    }
                    ret.Append("</a>");
                    ret.Append("<a href=\"/login.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\" class=\"regular\">");
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret.Append("Logga ut"); break; }
                        case 2: { ret.Append("Log out"); break; }
                    }
                    ret.Append("</a>");
				    ret.Append("</div>");
                    ret.Append("</div>");
                }
            }

            if (newsPage)
            {
                ret.Append("<div id=\"submenu\" class=\"grid_16 alpha\">");
                ret.Append("<a class=\"home\" href=\"/\">&nbsp;</a>");

                r = Db.rs("SELECT " +
                    "c.NewsCategoryID, " +
                    "cl.NewsCategory, " +
                    "c.NewsCategoryShort " +
                    "FROM NewsCategory c " +
                    "INNER JOIN NewsCategoryLang cl ON c.NewsCategoryID = cl.NewsCategoryID AND cl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                    "WHERE " +
                    "(" +
                        "SELECT COUNT(*) " +
                        "FROM News x " +
                        "WHERE x.NewsCategoryID = c.NewsCategoryID " +
                        (Convert.ToInt32(HttpContext.Current.Session["LID"]) != 1 ? "AND x.LinkLangID = 1 " : "") +
                        "AND DATEADD(m,1,x.DT) > GETDATE()" +
                    ") > 0 " +
                    "ORDER BY c.NewsCategory", "newsSqlConnection");
                while (r.Read())
                {
                    ret.Append("<a" + (HttpContext.Current.Request.Path.Replace("/news","").StartsWith("/" + r.GetString(2)) || HttpContext.Current.Request.QueryString["NCID"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["NCID"]) == r.GetInt32(0) ? " class=\"active\"" : "") + " href=\"/news/" + r.GetString(2) + "\"><span>" + r.GetString(1) + "</span></a>");
                }
                r.Close();
                ret.Append("</div>");
            }


            if (myPage)
            {
                ret.Append("<div id=\"submenu\" class=\"grid_16 alpha myhealth\">");
                //ret.Append("<a class=\"home\" href=\"/\">&nbsp;</a>");

                ret.Append("<a" + (rawUrl.IndexOf("calendar") >= 0 ? " class=\"active\"" : "") + " href=\"/calendar.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><span>");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("Dagbok"); break; }
                    case 2: { ret.Append("Calendar"); break; }
                }
                ret.Append("</span></a>");
                ret.Append("<a" + (rawUrl.IndexOf("submit.aspx") >= 0 ? " class=\"active\"" : "") + " href=\"/submit.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><span>");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("Formulär"); break; }
                    case 2: { ret.Append("Forms"); break; }
                }
                ret.Append("</span></a>");
                ret.Append("<a" + (rawUrl.IndexOf("statistics.aspx") >= 0 ? " class=\"active\"" : "") + " href=\"/statistics.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><span>");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("Statistik"); break; }
                    case 2: { ret.Append("Statistics"); break; }
                }
                ret.Append("</span></a>");
                ret.Append("<a" + (rawUrl.IndexOf("exercise.aspx") >= 0 ? " class=\"active\"" : "") + " href=\"/exercise.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><span>");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("Övningar"); break; }
                    case 2: { ret.Append("Exercises"); break; }
                }
                ret.Append("</span></a>");

                if (HttpContext.Current.Session["HasExtendedSurveys"] != null && Convert.ToInt32(HttpContext.Current.Session["HasExtendedSurveys"]) == 1)
                {
                    ret.Append("<a" + (rawUrl.IndexOf("extendedsurvey") >= 0 ? " class=\"active\"" : "") + " href=\"/extendedSurveys.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><span>");
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret.Append("Enkäter"); break; }
                        case 2: { ret.Append("Surveys"); break; }
                    }
                    ret.Append("</span></a>");
                }
                if (HttpContext.Current.Session["Username"].ToString() != "Gäst")
                {
                    ret.Append("<a" + (rawUrl.IndexOf("reminder.aspx") >= 0 ? " class=\"active\"" : "") + " href=\"/reminder.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><span>");
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret.Append("Påminnelser"); break; }
                        case 2: { ret.Append("Reminders"); break; }
                    }
                    ret.Append("</span></a>");
                }
                ret.Append("</div>");
            }

            if (aboutMyPage)
            {
                ret.Append("<div id=\"submenu\" class=\"grid_16 alpha myhealth\">");
                //ret.Append("<a class=\"home\" href=\"/\">&nbsp;</a>");

                ret.Append("<a" + (rawUrl.EndsWith("myhealth") ? " class=\"active\"" : "") + " href=\"/myhealth\"><span>");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("Om min hälsa"); break; }
                    case 2: { ret.Append("About my health"); break; }
                }
                ret.Append("</span></a>");
                ret.Append("<a" + (rawUrl.IndexOf("forgot.aspx") >= 0 ? " class=\"active\"" : "") + " href=\"/forgot.aspx\"><span>");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("Glömt lösenord?"); break; }
                    case 2: { ret.Append("Forgot your password?"); break; }
                }
                ret.Append("</span></a>");
                ret.Append("<a" + (rawUrl.IndexOf("register.aspx") >= 0 ? " class=\"active\"" : "") + " href=\"/register.aspx\"><span>");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("Skapa konto"); break; }
                    case 2: { ret.Append("Create account"); break; }
                }
                ret.Append("</span></a>");
                ret.Append("</div>");
            }

            if (aboutPage)
            {
                ret.Append("<div id=\"submenu\" class=\"grid_16 alpha about\">");
                //ret.Append("<a class=\"home\" href=\"/\">&nbsp;</a>");

                ret.Append("<a" + (rawUrl.EndsWith("about") ? " class=\"active\"" : "") + " href=\"/about\"><span>");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("Om HealthWatch"); break; }
                    case 2: { ret.Append("About HealthWatch"); break; }
                }
                ret.Append("</span></a>");
                ret.Append("<a" + (rawUrl.EndsWith("faq") ? " class=\"active\"" : "") + " href=\"/faq\"><span>");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("Hjälp med tjänsten"); break; }
                    case 2: { ret.Append("Support"); break; }
                }
                ret.Append("</span></a>");
                ret.Append("<a" + (rawUrl.EndsWith("contact") ? " class=\"active\"" : "") + " href=\"/contact\"><span>");
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret.Append("Kontaktinformation"); break; }
                    case 2: { ret.Append("Contact information"); break; }
                }
                ret.Append("</span></a>");
                ret.Append("</div>");
            }

            /*
            ret += (Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) > 1 ? "<img src=\"/img/sponsor/" + HttpContext.Current.Application["SPK" + HttpContext.Current.Session["SponsorID"].ToString()].ToString() + HttpContext.Current.Session["SponsorID"].ToString() + ".gif\" alt=\"" + HttpContext.Current.Application["APP" + HttpContext.Current.Session["SponsorID"].ToString()].ToString() + "\" id=\"img_customer-logo\" border=\"0\">" : "<img id=\"img_customer-logo\" src=\"/img/eForm.jpg\" alt=\"Powered by eForm\" width=\"141\" height=\"90\" />") + "</div>";

            if (!loggedIn && newsPage)
            {
                if (Convert.ToInt32(HttpContext.Current.Session["LID"]) == 1)
                {
                    ret += "<img src=\"/img/null.gif\" width=\"1\" height=\"20\"/><br/>" +
                        "<div style=\"background-color:#990000;padding:5px;border-top:1px solid #000000;border-left:1px solid #000000;border-right:1px solid #000000;\"><a style=\"text-decoration:none;font-size:10px;color:#ffffff;\" href=\"http://www.adlibris.com/se/promotion.aspx?page=stressaratt\" target=\"_blank\">Erbjudande! Köp boken &quot;Stressa rätt&quot; till specialpris&nbsp;&raquo;</a></div><a href=\"http://www.adlibris.com/se/promotion.aspx?page=stressaratt\" target=\"_blank\"><img src=\"/img/banner/stressaratt.jpg\" border=\"0\"/></a>" +
                        "<img src=\"/img/null.gif\" width=\"1\" height=\"20\"/><br/>" +
                        "<script language=\"javascript\">" +
                        "if (AC_FL_RunContent != 0) {" +
                        "AC_FL_RunContent(" +
                        "'codebase', 'https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0'," +
                        "'width', '152'," +
                        "'height', '180'," +
                        "'src', '/flash_banner'," +
                        "'quality', 'high'," +
                        "'pluginspage', 'https://www.macromedia.com/go/getflashplayer'," +
                        "'align', 'middle'," +
                        "'play', 'true'," +
                        "'loop', 'true'," +
                        "'scale', 'showall'," +
                        "'wmode', 'window'," +
                        "'devicefont', 'false'," +
                        "'id', 'flash_banner'," +
                        "'bgcolor', '#F5F5F5'," +
                        "'name', 'flash_banner'," +
                        "'menu', 'true'," +
                        "'allowFullScreen', 'false'," +
                        "'allowScriptAccess','sameDomain'," +
                        "'movie', '/flash_banner'," +
                        "'salign', ''" +
                        ");" +
                        "}" +
                        "</script>";
                }

            }
            if (rawUrl.IndexOf("sponsorinformation") == -1 && rawUrl.IndexOf("sponsorconsent") == -1)
            {
                if (Convert.ToInt32(HttpContext.Current.Session["LID"]) == 1)
                {
                    ret += "<img src=\"/img/null.gif\" width=\"1\" height=\"20\"/><br/>" +
                    "<a href=\"http://www.esf.se\"><img src=\"/img/banner/EurSocfund1.jpg\" border=\"0\" width=\"152\" height=\"170\"/></a>";
                }
                else
                {
                    ret += "<img src=\"/img/null.gif\" width=\"1\" height=\"20\"/><br/>" +
                    "<a href=\"http://ec.europa.eu/employment_social/esf/index_en.htm\"><img src=\"/img/banner/EurSocfund.jpg\" border=\"0\" width=\"152\" height=\"170\"/></a>";
                }
            }
            ret += "" +
                "</td>" +
                "<td width=\"18\">&nbsp;</td>" +
                "<td width=\"730\" valign=\"top\">";
            */
            return ret.ToString();

        }
		public static string nav()
		{
            bool loggedIn = (HttpContext.Current.Session["UserID"] != null && Convert.ToInt32("0" + HttpContext.Current.Session["UserID"]) != 0);
            string cssAddon = (loggedIn ? "" : "News");
            string rawUrl = HttpContext.Current.Request.RawUrl.ToLower();
            //HttpContext.Current.Response.Write(rawUrl + (rawUrl.IndexOf("default.aspx") >= 0).ToString());
            bool newsPage = (
                rawUrl.IndexOf("home.aspx") >= 0
                ||
                rawUrl.IndexOf("default.aspx") >= 0
                ||
                rawUrl.IndexOf("news.aspx") >= 0
                ||
                rawUrl.IndexOf("/news/") >= 0 
                ||
                rawUrl.EndsWith("/news")
                ||
                rawUrl.Equals("/"));
            bool myPage = loggedIn && (
                rawUrl.IndexOf("calendar.aspx") >= 0 
                ||
                rawUrl.IndexOf("submit.aspx") >= 0
                ||
                rawUrl.IndexOf("statistics.aspx") >= 0
                ||
                rawUrl.IndexOf("profile.aspx") >= 0
                ||
                rawUrl.IndexOf("exercise.aspx") >= 0
                ||
                rawUrl.IndexOf("reminder.aspx") >= 0
                ||
                rawUrl.IndexOf("extendedsurvey.aspx") >= 0
                ||
                rawUrl.IndexOf("extendedsurveys.aspx") >= 0
                );
            bool aboutPage = (
                rawUrl.IndexOf("contact.aspx") >= 0
                ||
                rawUrl.EndsWith("/contact")
                ||
                rawUrl.IndexOf("faq.aspx") >= 0
                ||
                rawUrl.EndsWith("/faq")
                ||
                rawUrl.IndexOf("about.aspx") >= 0
                ||
                rawUrl.EndsWith("/about")
                );

            bool showLoginBar = 
                rawUrl.IndexOf("register") == -1
                &&
                rawUrl.IndexOf("forgot") == -1 
                &&
                rawUrl.IndexOf("password") == -1
                &&
                rawUrl.IndexOf("sponsorinformation") == -1
                &&
                rawUrl.IndexOf("sponsorconsent") == -1;

            string cssAddon2 = (!newsPage ? "" : "News");
            string cssContentStart = (loggedIn ? "contentStartAuth" : "contentStart");
            SqlDataReader r;

            string ret = "<div id=\"container\"><div id=\"header\"><a HREF=\"/\"><img border=\"0\" id=\"img_logo\" src=\"/img/logo.jpg\" alt=\"HealthWatch.se\" width=\"117\" height=\"85\" /></A>";
            if (Convert.ToInt32(HttpContext.Current.Session["LID"]) == 1)
            {
                ret += "<img src=\"/img/wise/dagens" + ((DateTime.Now.DayOfYear + 1) % 12 + 1).ToString().PadLeft(2, '0') + ".gif\" alt=\"Dagens visdomsord\" name=\"img_banner\" width=\"547\" height=\"90\" id=\"img_banner\" />";
            }
            else
            {
                ret += "<img src=\"/img/null.gif\" name=\"img_banner\" width=\"547\" height=\"1\" id=\"img_banner\" />";
            }
            ret += (Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) > 1 ? "<img src=\"/img/sponsor/" + HttpContext.Current.Application["SPK" + HttpContext.Current.Session["SponsorID"].ToString()].ToString() + HttpContext.Current.Session["SponsorID"].ToString() + ".gif\" alt=\"" + HttpContext.Current.Application["APP" + HttpContext.Current.Session["SponsorID"].ToString()].ToString() + "\" id=\"img_customer-logo\" border=\"0\">" : "<img id=\"img_customer-logo\" src=\"/img/eForm.jpg\" alt=\"Powered by eForm\" width=\"141\" height=\"90\" />") + "</div>";

			// ret += "<td width=\"100%\" align=\"center\"><img src=\"img/" + (HttpContext.Current.Session["SponsorID"] != "10" ? "wise/dagens0" + ((DateTime.Now.DayOfYear + 1) % 6 + 1) + ".gif" : "banner/berocca.jpg") + "\" width=\"560\" height=\"125\" border=\"0\"></td>";

            if (showLoginBar)
			{
				if (loggedIn)
				{
                    ret += "<div id=\"sub_header2\">" +
                        "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                        "<tr><td valign=\"top\"><img src=\"/img/lock_login2.gif\" alt=\"Logga in\" width=\"34\" height=\"34\" border=\"0\" style=\"padding-left:5px\" /></td>" +
                        "<td><p>";
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret += "Inloggad som"; break; }
                        case 2: { ret += "Logged in as"; break; }
                    }
                    ret += " <b>" + HttpContext.Current.Session["Username"] + "</b></p></td>" +
                        "<td>&nbsp;<a class=\"subheaderLink\" href=\"/profile.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><img src=\"/img/arrow1.gif\" width=\"8\" height=\"7\" border=\"0\" />";
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret += (HttpContext.Current.Session["Username"].ToString() == "Gäst" ? "Gör detta till ditt konto" : "Ändra profil"); break; }
                        case 2: { ret += (HttpContext.Current.Session["Username"].ToString() == "Gäst" ? "Turn this into your account" : "Change profile"); break; }
                    }
                    ret += "</a>";
                    string redir = "";
                    if (HttpContext.Current.Session["ForceLID"] == null)
                    {
                        ret += "&nbsp;<a class=\"subheaderLink\" HREF=\"/home.aspx?LID=";
                        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                        {
                            case 1: { ret += "2"; break; }
                            case 2: { ret += "1"; break; }
                        }
                        //if (HttpContext.Current.Session["UserID"] != null && Convert.ToInt32(HttpContext.Current.Session["UserID"]) != 0)
                        //{
                        try
                        {
                            string s = rawUrl.Substring(rawUrl.LastIndexOf("/") + 1);
                            if (s != "")
                            {
                                redir = "&Goto=" + s;
                                if (redir.IndexOf("aspx") >= 0)
                                {
                                    redir = redir.Substring(0, redir.IndexOf("aspx") + 4);
                                }
                            }
                        }
                        catch (Exception) { }
                        //}
                        ret += redir + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><img src=\"/img/arrow1.gif\" width=\"8\" height=\"7\" border=\"0\" />";
                        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                        {
                            case 1: { ret += "In English"; break; }
                            case 2: { ret += "På svenska"; break; }
                        }
                        ret += "</a>";
                    }
                    ret += "&nbsp;<a class=\"subheaderLink\" href=\"/login.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><img src=\"/img/arrow1.gif\" width=\"8\" height=\"7\" border=\"0\" />";
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret += "Logga ut"; break; }
                        case 2: { ret += "Log out"; break; }
                    }
                    ret += "</a></td>" +
						"</tr>" +
						"</table>" +
						"</div>" +
						"";
				}
				else
				{
                    ret += "<div id=\"sub_header\">" +
                        "<table width=\"900\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                        "<tr>" +
                        "<td><table width=\"515\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                        "<tr>" +
                        "<td valign=\"top\"><img src=\"/img/lock_login.gif\" alt=\"Logga in\" width=\"47\" height=\"52\" border=\"0\" style=\"padding-left:5px\" /></td>" +
                        "<td valign=\"top\"><p style=\"font-size:11px; color:#FFFFFF; margin:0; padding:0; padding-top:8px\">";
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret += "Användarnamn"; break; }
                        case 2: { ret += "Username"; break; }
                    }
                    ret += "</p><input style=\"width:140px; height:18px; margin:0; padding:1px; border:0\" type=\"text\" name=\"Usern\"" + (HttpContext.Current.Request.Form["Usern"] != null ? " value=\"" + HttpContext.Current.Request.Form["Usern"].ToString() + "\"" : "") + " /></td>" +
                        "<td valign=\"top\"><p style=\"font-size:11px; color:#FFFFFF; margin:0; padding:0; padding-top:8px\">";
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret += "Lösenord"; break; }
                        case 2: { ret += "Password"; break; }
                    }
                    ret += "</p><input style=\"width:140px; height:18px; margin:0; padding:1px; border:0\" type=\"password\" name=\"Losen\" /></td>" +
                        "<td valign=\"top\"><input id=\"login_button\" type=\"submit\" value=\"OK\" /></td>" +
                        "<td valign=\"top\"><img src=\"/img/null.gif\" width=\"1\" height=\"7px\" />";
                    if (HttpContext.Current.Session["ForceLID"] == null)
                    {

                        ret += "<br />&nbsp;<a class=\"subheaderLink\" HREF=\"/home.aspx?LID=";
                        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                        {
                            case 1: { ret += "2"; break; }
                            case 2: { ret += "1"; break; }
                        }
                        ret += "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><img src=\"/img/arrow1.gif\" width=\"8\" height=\"7\" border=\"0\" />";
                        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                        {
                            case 1: { ret += "In English"; break; }
                            case 2: { ret += "På svenska"; break; }
                        }
                        ret += "</a><br/><img src=\"/img/null.gif\" width=\"1\" height=\"5px\" />";
                    }
                    ret += "<br />" +
                        "&nbsp;<a class=\"subheaderLink\" HREF=\"/forgot.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><img src=\"/img/arrow1.gif\" width=\"8\" height=\"7\" border=\"0\" />";
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret += "Glömt ditt lösenord?"; break; }
                        case 2: { ret += "Forgot your password?"; break; }
                    }
                    ret += "</a></td>" +
						"</tr>" +
						"</table></td>" +
						"<td width=\"20\">&nbsp;</td>" +
                        "<td width=\"355\" valign=\"top\" nowrap=\"nowrap\"><p style=\"font-size:13px; white-space:nowrap; color:#FFFFFF; margin-top:10px; line-height:16px\">";
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret += "Gratis verktyg för stresshantering och hälsopromotion."; break; }
                        case 2: { ret += "Free tools for stress management and health promotion."; break; }
                    }
                    ret += "<br />" +
						"<img src=\"/img/null.gif\" width=\"1\" height=\"2\" /><br />" +
                        "<a class=\"subheaderLink\" HREF=\"/register.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><img src=\"/img/arrow1.gif\" width=\"8\" height=\"7\" border=\"0\" />";
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret += "Registrera dig på 1 minut för fri tillgång!"; break; }
                        case 2: { ret += "Register in 1 minute for free access!"; break; }
                    }
                    ret += "</a> <a class=\"subheaderLink\" href=\"/about\"><img src=\"/img/arrow1.gif\" width=\"8\" height=\"7\" border=\"0\" />";
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret += "Läs mer om HealthWatch"; break; }
                        case 2: { ret += "Read more about HealthWatch"; break; }
                    }
                    ret += "</a></p></td>" +
						"</tr>" +
						"</table>" +
						"</div>" +
						"";
				}
			}
			ret += "<div id=\"main\">" +
				"<table width=\"900\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
				"<tr>" +
				"<td width=\"152\" valign=\"top\" bgcolor=\"#f5f5f5\">" +
				"<div id=\"navi\">" +
				"<ul>" +
                "<li" + (newsPage ? " id=\"active\"" : "") + "><a href=\"/\">";
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: { ret += "Nyheter"; break; }
                case 2: { ret += "News"; break; }
            }
            ret += "</a></li>";

			if (newsPage)
			{
                r = Db.rs("SELECT " +
                    "c.NewsCategoryID, " +
                    "cl.NewsCategory, " +
                    "c.NewsCategoryShort " +
                    "FROM NewsCategory c " +
                    "INNER JOIN NewsCategoryLang cl ON c.NewsCategoryID = cl.NewsCategoryID AND cl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                    "WHERE " +
                    "(" +
                        "SELECT COUNT(*) " +
                        "FROM News x " +
                        "WHERE x.NewsCategoryID = c.NewsCategoryID " +
                        (Convert.ToInt32(HttpContext.Current.Session["LID"]) != 1 ? "AND x.LinkLangID = 1 " : "") +
                        "AND DATEADD(m,1,x.DT) > GETDATE()" +
                    ") > 0 " +
                    "ORDER BY c.NewsCategory", "newsSqlConnection");
				while (r.Read())
				{
					ret += "<li id=\"sub\"><A HREF=\"/news/" + r.GetString(2) + "\">" + r.GetString(1) + "</A></li>";
				}
				r.Close();
			}

            ret += "<li" + (myPage ? " id=\"active\"" : "") + "><A HREF=\"/myhealth\">";
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: { ret += "Min hälsa"; break; }
                case 2: { ret += "My health"; break; }
            }
            ret += "</A></li>";
			if (myPage)
			{
                ret += "<li id=\"sub\"><A HREF=\"/calendar.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">";
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret += "Dagbok"; break; }
                    case 2: { ret += "Calendar"; break; }
                }
                ret += "</A></li>";
				/*
				r = rs("" +
					"SELECT TOP 1 " +
					"REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') " +
					"FROM [User] u " +
					"INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
					"INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
					//// use the below rows to only trigger extended surveys when trigger value is reached //// ABS(spru.SponsorID) above
					//"LEFT OUTER JOIN UserProjectRoundUser r ON u.UserID = r.UserID AND spru.ProjectRoundUnitID = r.ProjectRoundUnitID " +
					//"WHERE (spru.Ext IS NULL OR r.UserID IS NOT NULL) AND u.UserID = " + HttpContext.Current.Session["UserID"] + " ORDER BY spru.SortOrder");
					////
					"WHERE u.UserID = " + HttpContext.Current.Session["UserID"] + " ORDER BY spru.SortOrder");
				if (r.Read())
				{
					ret += "<div class=\"navItem\"><A HREF=\"submit.aspx?SK=" + r.GetString(0) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Formulär</A></div>";
				}
				r.Close();
				*/
                ret += "<li id=\"sub\"><A HREF=\"/submit.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">";
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret += "Formulär"; break; }
                    case 2: { ret += "Forms"; break; }
                }
                ret += "</A></li>";
                ret += "<li id=\"sub\"><A HREF=\"/statistics.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">";
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret += "Statistik"; break; }
                    case 2: { ret += "Statistics"; break; }
                }
                ret += "</A></li>";
                //if(Convert.ToInt32(HttpContext.Current.Session["LID"]) == 1)
                //{
                    ret += "<li id=\"sub\"><A HREF=\"/exercise.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">";
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret += "Övningar"; break; }
                        case 2: { ret += "Exercises"; break; }
                    }
                    ret += "</A></li>";

                    if (HttpContext.Current.Session["HasExtendedSurveys"] != null && Convert.ToInt32(HttpContext.Current.Session["HasExtendedSurveys"]) == 1)
                    {
                        ret += "<li id=\"sub\"><A HREF=\"/extendedSurveys.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">";
                        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                        {
                            case 1: { ret += "Enkäter"; break; }
                            case 2: { ret += "Surveys"; break; }
                        }
                        ret += "</A></li>";
                    }
                //}
                if (HttpContext.Current.Session["Username"].ToString() != "Gäst")
                {
    				ret += "<li id=\"sub\"><A HREF=\"/reminder.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">";
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: { ret += "Påminnelser"; break; }
                        case 2: { ret += "Reminders"; break; }
                    }
                    ret += "</A></li>";
                }
			}

            ret += "<li" + (aboutPage ? " id=\"active\"" : "") + "><A HREF=\"/about\">";
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: { ret += "Om tjänsten"; break; }
                case 2: { ret += "About"; break; }
            }
            ret += "</A></li>";
			if (aboutPage)
			{
                ret += "<li id=\"sub\"><A HREF=\"/about\">";
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret += "Om HealthWatch"; break; }
                    case 2: { ret += "About HealthWatch"; break; }
                }
                ret += "</A></li>";
                ret += "<li id=\"sub\"><A HREF=\"/faq\">";
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret += "Hjälp med tjänsten"; break; }
                    case 2: { ret += "Help"; break; }
                }
                ret += "</A></li>";
                ret += "<li id=\"sub\"><A HREF=\"/contact\">";
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { ret += "Kontaktinformation"; break; }
                    case 2: { ret += "Contact"; break; }
                }
                ret += "</A></li>";
            }
			ret += "" +
				"</ul>" +
				"</div>";

            if (!loggedIn && newsPage)
			{
                if (Convert.ToInt32(HttpContext.Current.Session["LID"]) == 1)
                {
                    ret += "<img src=\"/img/null.gif\" width=\"1\" height=\"20\"/><br/>" +
                        "<div style=\"background-color:#990000;padding:5px;border-top:1px solid #000000;border-left:1px solid #000000;border-right:1px solid #000000;\"><a style=\"text-decoration:none;font-size:10px;color:#ffffff;\" href=\"http://www.adlibris.com/se/promotion.aspx?page=stressaratt\" target=\"_blank\">Erbjudande! Köp boken &quot;Stressa rätt&quot; till specialpris&nbsp;&raquo;</a></div><a href=\"http://www.adlibris.com/se/promotion.aspx?page=stressaratt\" target=\"_blank\"><img src=\"/img/banner/stressaratt.jpg\" border=\"0\"/></a>" +
                        "<img src=\"/img/null.gif\" width=\"1\" height=\"20\"/><br/>" +
                        "<script language=\"javascript\">" +
                        "if (AC_FL_RunContent != 0) {" +
                        "AC_FL_RunContent(" +
                        "'codebase', 'https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0'," +
                        "'width', '152'," +
                        "'height', '180'," +
                        "'src', '/flash_banner'," +
                        "'quality', 'high'," +
                        "'pluginspage', 'https://www.macromedia.com/go/getflashplayer'," +
                        "'align', 'middle'," +
                        "'play', 'true'," +
                        "'loop', 'true'," +
                        "'scale', 'showall'," +
                        "'wmode', 'window'," +
                        "'devicefont', 'false'," +
                        "'id', 'flash_banner'," +
                        "'bgcolor', '#F5F5F5'," +
                        "'name', 'flash_banner'," +
                        "'menu', 'true'," +
                        "'allowFullScreen', 'false'," +
                        "'allowScriptAccess','sameDomain'," +
                        "'movie', '/flash_banner'," +
                        "'salign', ''" +
                        ");" +
                        "}" +
                        "</script>";
                }
                
			}
            if (rawUrl.IndexOf("sponsorinformation") == -1 && rawUrl.IndexOf("sponsorconsent") == -1)
            {
                if (Convert.ToInt32(HttpContext.Current.Session["LID"]) == 1)
                {
                    ret += "<img src=\"/img/null.gif\" width=\"1\" height=\"20\"/><br/>" +
                    "<a href=\"http://www.esf.se\"><img src=\"/img/banner/EurSocfund1.jpg\" border=\"0\" width=\"152\" height=\"170\"/></a>";
                }
                else
                {
                    ret += "<img src=\"/img/null.gif\" width=\"1\" height=\"20\"/><br/>" +
                    "<a href=\"http://ec.europa.eu/employment_social/esf/index_en.htm\"><img src=\"/img/banner/EurSocfund.jpg\" border=\"0\" width=\"152\" height=\"170\"/></a>";
                }
            }
			ret += "" +
				"</td>" +
				"<td width=\"18\">&nbsp;</td>" +
				"<td width=\"730\" valign=\"top\">";

			/*
			ret += "<div id=\"main" + (loginBar ? "Login" : "") + "\">";

			if(HttpContext.Current.Session["UserID"] != null && Convert.ToInt32("0" + HttpContext.Current.Session["UserID"]) != 0)
			{
                //
			}
            else if (1==0 && HttpContext.Current.Request.RawUrl.IndexOf("register") == -1 && HttpContext.Current.Request.RawUrl.IndexOf("forgot") == -1 && HttpContext.Current.Request.RawUrl.IndexOf("password") == -1)
            {
                ret += "<div id=\"topNavLeft\">";
                ret += "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
                ret += "<tr>";
                ret += "<td rowspan=\"2\" width=\"142\">";
                ret += "<div class=\"navItemGrey\"><A class=\"smallFonts\" HREF=\"register.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Skapa konto på 1 minut!</A></div>";
                ret += "<div class=\"navSep\"><br/></div>";
                ret += "<div class=\"navItemGrey\"><A class=\"smallFonts\" HREF=\"forgot.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Glömt ditt lösenord?</A></div>";
                ret += "</td>";
                ret += "<td rowspan=\"2\" width=\"17\"><img src=\"img/null.gif\" width=\"17\" height=\"1\"></td>";
                ret += "<td width=\"140\" class=\"smallFonts\">Användarnamn</td>";
                ret += "<td rowspan=\"2\" width=\"10\"><img src=\"img/null.gif\" width=\"10\" height=\"1\"></td>";
                ret += "<td width=\"140\" class=\"smallFonts\">Lösenord</td>";
                ret += "<td rowspan=\"2\" width=\"10\"><img src=\"img/null.gif\" width=\"10\" height=\"1\"></td>";
                ret += "<td width=\"25\"><img src=\"img/null.gif\" width=\"25\" height=\"1\"></td>";
                ret += "<td rowspan=\"2\" width=\"35\"><img src=\"img/null.gif\" width=\"35\" height=\"1\"></td>";
                if (HttpContext.Current.Request.Form["Usern"] != null && HttpContext.Current.Request.Form["Usern"].ToString() != "" || HttpContext.Current.Request.Form["Losen"] != null && HttpContext.Current.Request.Form["Losen"].ToString() != "")
                {
                    ret += "<td rowspan=\"2\" style=\"color:#cc0000;\">Felaktiga inloggningsuppgifter!</td>";
                }
                else
                {
                    ret += "<td rowspan=\"2\" class=\"smallFonts\">HealthWatch erbjuder ett självhjälpsverktyg för att bevara och öka hälsa<br/>och energi samt motverka skadlig stress. Tjänsten är kostnadsfri för privatpersoner och grupper om max 50 personer.</td>";
                }
                //ret += "<td rowspan=\"2\" width=\"140\">";
                //ret += "<div class=\"navItemGrey\"><A HREF=\"register.aspx?Login=Guest&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Logga in som gäst</A></div>";
                //ret += "</td>"; 
                ret += "</tr>";
                ret += "<tr>";
                ret += "<td><input class=\"loginTB\" type=\"text\" name=\"Usern\"" + (HttpContext.Current.Request.Form["Usern"] != null ? "  value=\"" + HttpContext.Current.Request.Form["Usern"].ToString() + "\"" : "") + " style=\"width:140px;\"></td>";
                ret += "<td><input class=\"loginTB\" type=\"password\" name=\"Losen\" style=\"width:140px;\"></td>";
                ret += "<td><input type=\"image\" src=\"img/submit.gif\"></td>";
                ret += "</tr>";
                ret += "</table>";
                ret += "</div>";
            }
            if (newsPage)
            {
                //ret += "<div style=\"position:absolute;top:195px;left:750px;\"><img src=\"img/banner/hasseludden.jpg\"></div>";
            }
            ret += "<div id=\"nav" + (!newsPage ? "" : "News") + "\" class=\"contentStart" + (loginBar ? "Login" : "") + "\">";

            if (loggedIn)
            {
                ret += "<div class=\"navText navLine\">Inloggad som <B>" + HttpContext.Current.Session["Username"] + "</B></div>";
                ret += "<div class=\"navItem\"><A HREF=\"\">Logga ut</A></div>";
                ret += "<div class=\"fifteen\"><br/></div>";
            }

			ret += "</div>";

            ret += "<div id=\"container" + (loginBar ? "Login" : "") + (!newsPage ? "Ad" : "") + "\"" +  (newsPage ? " class=\"bold\"" : "") + ">";
			*/
			return ret;
		}

		public static bool isEmail(string inputEmail)
		{
			string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
				@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + 
				@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
			Regex re = new Regex(strRegex);
			if (re.IsMatch(inputEmail)) {
				return true;
			} else {
				return false;
			}
		}

		public static string HashMD5(string str)
		{
			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] hashByteArray = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes("HW" + str + "HW"));
			string hash = "";
			for (int i = 0; i < hashByteArray.Length; i++)
				hash += hashByteArray[i];
			return hash;
		}

        public static int createAccount(string usern, string email, string losen, int sponsorID, string departmentID, int userProfileID, string altemail)
        {
            string pass = Db.HashMD5(losen.Trim());
            int userID = 0;

            Db.exec("INSERT INTO [User] (Username, Email, Password, SponsorID, DepartmentID, UserProfileID, LID, AltEmail) VALUES ('" + usern.Replace("'", "") + "','" + email.Replace("'", "") + "','" + pass + "'," + sponsorID + "," + (departmentID == "" ? "NULL" : departmentID) + "," + userProfileID + "," + Convert.ToInt32(HttpContext.Current.Session["LID"]) + "," + (altemail != "" ? "'" + altemail.Replace("'","") + "'" : "NULL") + ")");
            SqlDataReader rs = Db.rs("SELECT UserID, SponsorID, UserProfileID, DepartmentID FROM [User] WHERE Username = '" + usern.Replace("'", "") + "' AND Password = '" + pass + "' ORDER BY UserID DESC");
            if (rs.Read())
            {
                userID = rs.GetInt32(0);
                HttpContext.Current.Session["UserID"] = userID;
                HttpContext.Current.Session["SponsorID"] = rs.GetInt32(1);
                HttpContext.Current.Session["UserProfileID"] = rs.GetInt32(2);
                HttpContext.Current.Session["DepartmentID"] = (rs.IsDBNull(3) ? "NULL" : rs.GetInt32(3).ToString());
            }
            rs.Close();

            return userID;
        }

        public static void renderBQ(bool loggedIn, bool isGuest, bool isPB, PlaceHolder ph, ref string functionScript, ref string startScript)
        {
            SqlDataReader rs = Db.rs("SELECT " +
                "BQ.BQID, " +           // 0
                "BQLang.BQ, " +         // 1
                "BQ.Type, " +           // 2
                "sbq.Forced, " +        // 3
                "BQ.ReqLength, " +      // 4
                "BQ.DefaultVal, " +     // 5
                "sbq.SponsorBQID, " +   // 6
                "(" +
                    "SELECT COUNT(*) " +
                    "FROM BQVisibility v " +
                    "INNER JOIN SponsorBQ b ON v.ChildBQID = b.BQID AND b.SponsorID = s.SponsorID " +
                    "WHERE b.Hidden = 0 AND v.BQID = BQ.BQID" +
                "), " +                 // 7 - Number of children questions
                "(" +
                    "SELECT " +
                    "COUNT(*) FROM " +
                    "BQVisibility v2 " +
                    "INNER JOIN SponsorBQ b2 ON v2.BQID = b2.BQID AND b2.SponsorID = s.SponsorID " +
                    "WHERE b2.Hidden = 0 AND v2.ChildBQID = BQ.BQID" +
                "), " +                  // 8 - Number of parent questions
                "BQ.MeasurementUnit, " + // 9
                "BQ.Layout " +           // 10
                "FROM Sponsor s " +
                "INNER JOIN SponsorBQ sbq ON s.SponsorID = sbq.SponsorID " +
                "INNER JOIN BQ ON BQ.BQID = sbq.BQID " +
                "INNER JOIN BQLang ON BQ.BQID = BQLang.BQID AND BQLang.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                "WHERE sbq.Hidden = 0 AND s.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " " +
                "ORDER BY sbq.SortOrder");
            while (rs.Read())
            {
                string script = "";
                SqlDataReader rs2;

                #region This question should show/hide depending on other question?
                if (rs.GetInt32(8) > 0)
                {
                    rs2 = Db.rs("SELECT " +
                        "v.BQID, " +
                        "v.BAID, " +
                        "BQ.Type " +
                        "FROM BQVisibility v " +
                        "INNER JOIN BQ ON v.BQID = BQ.BQID " +
                        "INNER JOIN SponsorBQ b ON v.BQID = b.BQID AND b.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " " +
                        "WHERE b.Hidden = 0 AND v.ChildBQID = " + rs.GetInt32(0));
                    if (rs2.Read())
                    {
                        int rx = 0;
                        script += "setDisp('Q" + rs.GetInt32(0) + "',";
                        do
                        {
                            script += (rx++ > 0 ? "||" : "");
                            switch (rs2.GetInt32(2))
                            {
                                case 1: script += "getRad('BQ" + rs2.GetInt32(0) + "')==" + rs2.GetInt32(1); break;
                                case 7: script += "getDdl('BQ" + rs2.GetInt32(0) + "')==" + rs2.GetInt32(1); break;
                            }
                        }
                        while (rs2.Read());
                        script += ");";
                    }
                    rs2.Close();
                }
                #endregion
                #region This question has depending children?
                if (rs.GetInt32(7) > 0)
                {
                    rs2 = Db.rs("SELECT " +
                        "v.ChildBQID " +
                        "FROM BQVisibility v " +
                        "INNER JOIN SponsorBQ b ON v.ChildBQID = b.BQID AND b.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " " +
                        "WHERE b.Hidden = 0 AND v.BQID = " + rs.GetInt32(0));
                    while (rs2.Read())
                    {
                        script += "BQ" + rs2.GetInt32(0) + "();";
                    }
                    rs2.Close();
                }
                #endregion

                ph.Controls.Add(new LiteralControl("<div ID=\"Q" + rs.GetInt32(0) + "\" style=\"clear:both;\"><div style=\"float:left;width:220px;\">" + rs.GetString(1) + "</div><div style=\"float:left;width:270px;\">"));
                switch (rs.GetInt32(2))
                {
                    case 0:
                        {
                            if (!isPB)
                            {
                                rs2 = Db.rs("SELECT " +
                                    "BA.BAID, " +
                                    "BALang.BA " +
                                    "FROM BA " +
                                    "INNER JOIN BALang ON BA.BAID = BALang.BAID AND BALang.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                                    "WHERE BA.BQID = " + rs.GetInt32(0) + " " + 
                                    "ORDER BY BA.SortOrder");
                                while (rs2.Read())
                                {
                                    ((ListControl)ph.FindControl("BQ" + rs.GetInt32(0))).Items.Add(new ListItem(rs2.GetString(1), rs2.GetInt32(0).ToString()));
                                }
                                rs2.Close();
                                if (loggedIn && !isGuest)
                                {
                                    rs2 = Db.rs("SELECT ValueInt FROM UserProfileBQ WHERE UserProfileID = " + Convert.ToInt32(HttpContext.Current.Session["UserProfileID"]) + " AND BQID = " + rs.GetInt32(0));
                                    if (rs2.Read() && ph.FindControl("BQ" + rs.GetInt32(0)) != null && !rs2.IsDBNull(0))
                                    {
                                        ((ListControl)ph.FindControl("BQ" + rs.GetInt32(0))).SelectedValue = rs2.GetInt32(0).ToString();
                                    }
                                    rs2.Close();
                                }
                            }
                            break;
                        }
                    case 1:
                        {
                            RadioButtonList rbl = new RadioButtonList();
                            rbl.ID = "BQ" + rs.GetInt32(0);
                            if (rs.IsDBNull(10))
                            {
                                rbl.RepeatDirection = RepeatDirection.Horizontal;
                            }
                            else
                            {
                                rbl.RepeatDirection = RepeatDirection.Vertical;
                            }
                            rbl.RepeatLayout = RepeatLayout.Flow;
                            
                            //rbl.CellSpacing = 0;
                            //rbl.CellPadding = 0;
                            //rbl.CssClass = "txt";
                            if (!rs.IsDBNull(3) && rs.GetInt32(3) == 1)
                            {
                                script += "chkRad('BQ" + rs.GetInt32(0) + "');";
                            }
                            if (script != "")
                            {
                                rbl.Attributes["onclick"] += "self.BQ" + rs.GetInt32(0) + "();";
                            }
                            ph.Controls.Add(rbl);
                            goto case 0;
                        }
                    case 2:
                        {
                            TextBox tb = new TextBox();
                            tb.ID = "BQ" + rs.GetInt32(0);
                            tb.Width = Unit.Pixel(40);
                            tb.CssClass = "regularTB";
                            if (!rs.IsDBNull(3) && rs.GetInt32(3) == 1)
                            {
                                script += "chkTxt('BQ" + rs.GetInt32(0) + "'," + (rs.IsDBNull(4) ? 1 : rs.GetInt32(4)) + ");";
                            }
                            if (script != "")
                            {
                                tb.Attributes["onkeyup"] += "self.BQ" + rs.GetInt32(0) + "();";
                            }
                            ph.Controls.Add(tb);
                            if (!rs.IsDBNull(9) && rs.GetString(9) != "")
                            {
                                ph.Controls.Add(new LiteralControl(rs.GetString(9)));
                            }
                            if (!isPB)
                            {
                                if (!isGuest && loggedIn)
                                {
                                    rs2 = Db.rs("SELECT ValueText FROM UserProfileBQ WHERE UserProfileID = " + Convert.ToInt32(HttpContext.Current.Session["UserProfileID"]) + " AND BQID = " + rs.GetInt32(0));
                                    if (rs2.Read() && ph.FindControl("BQ" + rs.GetInt32(0)) != null && !rs2.IsDBNull(0))
                                    {
                                        ((TextBox)ph.FindControl("BQ" + rs.GetInt32(0))).Text = rs2.GetString(0);
                                    }
                                    rs2.Close();
                                }
                                if (tb.Text == "" && !rs.IsDBNull(5))
                                {
                                    tb.Text = rs.GetString(5);
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                            DropDownList ddl = new DropDownList();
                            ddl.ID = "BQ" + rs.GetInt32(0) + "Y";
                            ddl.CssClass = "regularTB";
                            if (!rs.IsDBNull(3) && rs.GetInt32(3) == 1)
                            {
                                script += "chkDate('BQ" + rs.GetInt32(0) + "');";
                            }
                            if (script != "")
                            {
                                ddl.Attributes["onchange"] += script;
                            }
                            ph.Controls.Add(ddl);
                            if (!isPB)
                            {
                                ((ListControl)ph.FindControl("BQ" + rs.GetInt32(0) + "Y")).Items.Add(new ListItem("-", "0"));
                                for (int i = 1900; i <= DateTime.Now.Year; i++)
                                {
                                    ((ListControl)ph.FindControl("BQ" + rs.GetInt32(0) + "Y")).Items.Add(new ListItem(i.ToString(), i.ToString()));
                                }
                            }

                            ddl = new DropDownList();
                            ddl.ID = "BQ" + rs.GetInt32(0) + "M";
                            ddl.CssClass = "regularTB";
                            if (script != "")
                            {
                                ddl.Attributes["onchange"] += "self.BQ" + rs.GetInt32(0) + "();";
                            }
                            ph.Controls.Add(ddl);
                            if (!isPB)
                            {
                                ((ListControl)ph.FindControl("BQ" + rs.GetInt32(0) + "M")).Items.Add(new ListItem("-", "0"));
                                for (int i = 1; i <= 12; i++)
                                {
                                    ((ListControl)ph.FindControl("BQ" + rs.GetInt32(0) + "M")).Items.Add(new ListItem(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[i - 1], i.ToString()));
                                }
                            }

                            ddl = new DropDownList();
                            ddl.ID = "BQ" + rs.GetInt32(0) + "D";
                            ddl.CssClass = "regularTB";
                            if (script != "")
                            {
                                ddl.Attributes["onchange"] += "self.BQ" + rs.GetInt32(0) + "();";
                            }
                            ph.Controls.Add(ddl);
                            if (!isPB)
                            {
                                ((ListControl)ph.FindControl("BQ" + rs.GetInt32(0) + "D")).Items.Add(new ListItem("-", "0"));
                                for (int i = 1; i <= 31; i++)
                                {
                                    ((ListControl)ph.FindControl("BQ" + rs.GetInt32(0) + "D")).Items.Add(new ListItem(i.ToString(), i.ToString()));
                                }

                                if (loggedIn && !isGuest)
                                {
                                    rs2 = Db.rs("SELECT ValueDate FROM UserProfileBQ WHERE UserProfileID = " + Convert.ToInt32(HttpContext.Current.Session["UserProfileID"]) + " AND BQID = " + rs.GetInt32(0));
                                    if (rs2.Read() && !rs2.IsDBNull(0) && ph.FindControl("BQ" + rs.GetInt32(0) + "Y") != null && ph.FindControl("BQ" + rs.GetInt32(0) + "M") != null && ph.FindControl("BQ" + rs.GetInt32(0) + "D") != null && !rs2.IsDBNull(0))
                                    {
                                        ((ListControl)ph.FindControl("BQ" + rs.GetInt32(0) + "Y")).SelectedValue = rs2.GetDateTime(0).Year.ToString();
                                        ((ListControl)ph.FindControl("BQ" + rs.GetInt32(0) + "M")).SelectedValue = rs2.GetDateTime(0).Month.ToString();
                                        ((ListControl)ph.FindControl("BQ" + rs.GetInt32(0) + "D")).SelectedValue = rs2.GetDateTime(0).Day.ToString();
                                    }
                                    rs2.Close();
                                }
                                if (((ListControl)ph.FindControl("BQ" + rs.GetInt32(0) + "Y")).SelectedValue == "0")
                                {
                                    ((ListControl)ph.FindControl("BQ" + rs.GetInt32(0) + "Y")).SelectedValue = "1970";
                                }
                            }
                            break;
                        }
                    case 4:
                        {
                            TextBox tb = new TextBox();
                            tb.ID = "BQ" + rs.GetInt32(0);
                            tb.Width = Unit.Pixel(40);
                            tb.CssClass = "regularTB";
                            if (!rs.IsDBNull(3) && rs.GetInt32(3) == 1)
                            {
                                script += "chkTxt('BQ" + rs.GetInt32(0) + "'," + (rs.IsDBNull(4) ? 1 : rs.GetInt32(4)) + ");";
                            }
                            if (script != "")
                            {
                                tb.Attributes["onkeyup"] += "self.BQ" + rs.GetInt32(0) + "();";
                            }
                            ph.Controls.Add(tb);
                            if (!rs.IsDBNull(9) && rs.GetString(9) != "")
                            {
                                ph.Controls.Add(new LiteralControl(rs.GetString(9)));
                            }
                            if (!isPB)
                            {
                                if (!isGuest && loggedIn)
                                {
                                    rs2 = Db.rs("SELECT ValueInt FROM UserProfileBQ WHERE UserProfileID = " + Convert.ToInt32(HttpContext.Current.Session["UserProfileID"]) + " AND BQID = " + rs.GetInt32(0));
                                    if (rs2.Read() && ph.FindControl("BQ" + rs.GetInt32(0)) != null && !rs2.IsDBNull(0))
                                    {
                                        ((TextBox)ph.FindControl("BQ" + rs.GetInt32(0))).Text = rs2.GetInt32(0).ToString();
                                    }
                                    rs2.Close();
                                }
                                if (tb.Text == "" && !rs.IsDBNull(5))
                                {
                                    tb.Text = rs.GetString(5);
                                }
                            }
                            break;
                        }
                    case 7:
                        {
                            DropDownList ddl = new DropDownList();
                            ddl.ID = "BQ" + rs.GetInt32(0);
                            ddl.Width = Unit.Pixel(250);
                            ddl.CssClass = "regularTB";
                            if (!rs.IsDBNull(3) && rs.GetInt32(3) == 1)
                            {
                                script += "chkDdl('BQ" + rs.GetInt32(0) + "');";
                            }
                            if (script != "")
                            {
                                ddl.Attributes["onchange"] += "self.BQ" + rs.GetInt32(0) + "();";
                            }
                            ph.Controls.Add(ddl);
                            if (!isPB)
                            {
                                ((ListControl)ph.FindControl("BQ" + rs.GetInt32(0))).Items.Add(new ListItem("< välj >", ""));
                            }
                            goto case 0;
                        }
                }
                ph.Controls.Add(new LiteralControl("</div>"));
                if (!rs.IsDBNull(3) && rs.GetInt32(3) == 1)
                {
                    ph.Controls.Add(new LiteralControl("<div style=\"float:left;\"><IMG id=\"StarBQ" + rs.GetInt32(0) + "\" SRC=\"/img/star.gif\"></div>"));
                }
                ph.Controls.Add(new LiteralControl("</div>"));

                if (script != "")
                {
                    functionScript += "function BQ" + rs.GetInt32(0) + "(){" + script + "}";
                    startScript += script;
                }
            }
            rs.Close();
        }

        public static bool checkParent(int BQID, PlaceHolder ph)
        {
            bool visible = false;

            SqlDataReader rs2 = Db.rs("SELECT " +
                "v.BQID, " +
                "v.BAID, " +
                "BQ.Type, " +
                "(" +
                    "SELECT COUNT(*) " +
                    "FROM BQVisibility v2 " +
                    "INNER JOIN SponsorBQ b2 ON v2.BQID = b2.BQID AND b2.SponsorID = s.SponsorID " +
                    "WHERE b2.Hidden = 0 AND v2.ChildBQID = v.BQID" +
                ") " +
                "FROM BQVisibility v " +
                "INNER JOIN SponsorBQ s ON v.BQID = s.BQID AND s.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " " +
                "INNER JOIN BQ ON v.BQID = BQ.BQID " +
                "WHERE s.Hidden = 0 AND v.ChildBQID = " + BQID);
            while(rs2.Read())
            {
                switch (rs2.GetInt32(2))
                {
                    case 1:
                        if (((RadioButtonList)ph.FindControl("BQ" + rs2.GetInt32(0))).SelectedValue == rs2.GetInt32(1).ToString())
                        {
                            if (rs2.GetInt32(3) == 0 || checkParent(rs2.GetInt32(0), ph))
                            {
                                visible = true;
                            }
                        }
                        break;
                    case 7:
                        if (((DropDownList)ph.FindControl("BQ" + rs2.GetInt32(0))).SelectedValue == rs2.GetInt32(1).ToString())
                        {
                            if (rs2.GetInt32(3) == 0 || checkParent(rs2.GetInt32(0), ph))
                            {
                                visible = true;
                            }
                        }
                        break;
                }
            }
            rs2.Close();

            return visible;
        }

        public static bool checkForced(ref string err, PlaceHolder ph)
        {
            bool allForced = true;

            SqlDataReader rs = Db.rs("SELECT " +
                    "sbq.BQID, " +          // 0
                    "bq.Type, " +           // 1
                    "sbq.Forced, " +        // 2
                    "BQ.ReqLength, " +     // 3
                    "BQ.Internal, " +       // 4
                    "BQ.MaxLength, " +     // 5
                    "(" +
                        "SELECT COUNT(*) " +
                        "FROM BQVisibility v " +
                        "INNER JOIN SponsorBQ b ON v.BQID = b.BQID AND b.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " " +
                        "WHERE b.Hidden = 0 AND v.ChildBQID = sbq.BQID" +
                    "), " +     // 6
                    "sbq.SponsorBQID " +    // 7
                    "FROM Sponsor s " +
                    "INNER JOIN SponsorBQ sbq ON s.SponsorID = sbq.SponsorID " +
                    "INNER JOIN BQ ON BQ.BQID = sbq.BQID " +
                    "WHERE sbq.Hidden = 0 AND sbq.Forced = 1 AND s.SponsorID = " + HttpContext.Current.Session["SponsorID"]);
            while (rs.Read())
            {
                if (rs.GetInt32(6) == 0 || checkParent(rs.GetInt32(0), ph))
                {
                    switch (rs.GetInt32(1))
                    {
                        case 1:
                            if (!rs.IsDBNull(2) && rs.GetInt32(2) == 1 && ((RadioButtonList)ph.FindControl("BQ" + rs.GetInt32(0))).SelectedValue == "")
                            {
                                allForced = false;
                            }
                            break;
                        case 2:
                            if (!rs.IsDBNull(2) && rs.GetInt32(2) == 1 && (((TextBox)ph.FindControl("BQ" + rs.GetInt32(0))).Text.Length < (rs.IsDBNull(3) ? 1 : rs.GetInt32(3)) || ((TextBox)ph.FindControl("BQ" + rs.GetInt32(0))).Text.Length > (rs.IsDBNull(5) ? 9 : rs.GetInt32(5))))
                            {
                                if ((rs.IsDBNull(3) ? 1 : rs.GetInt32(3)) == (rs.IsDBNull(5) ? 9 : rs.GetInt32(5)))
                                {
                                    err += (err != "" ? "<BR>" : "") + rs.GetString(4) + " skall skrivas med " + (rs.IsDBNull(3) ? 1 : rs.GetInt32(3)) + " tecken.";
                                }
                                else if (((TextBox)ph.FindControl("BQ" + rs.GetInt32(0))).Text.Length > (rs.IsDBNull(5) ? 9 : rs.GetInt32(5)))
                                {
                                    err += (err != "" ? "<BR>" : "") + rs.GetString(4) + " får inte vara längre än " + (rs.IsDBNull(5) ? 9 : rs.GetInt32(5)) + " tecken.";
                                }
                                else
                                {
                                    err += (err != "" ? "<BR>" : "") + rs.GetString(4) + " måste innehålla minst " + (rs.IsDBNull(3) ? 1 : rs.GetInt32(3)) + " tecken.";
                                }
                                allForced = false;
                            }
                            break;
                        case 3:
                            if (!rs.IsDBNull(2) && rs.GetInt32(2) == 1)
                            {
                                string y = ((DropDownList)ph.FindControl("BQ" + rs.GetInt32(0) + "Y")).SelectedValue;
                                string m = ((DropDownList)ph.FindControl("BQ" + rs.GetInt32(0) + "M")).SelectedValue;
                                string d = ((DropDownList)ph.FindControl("BQ" + rs.GetInt32(0) + "D")).SelectedValue;

                                if (y == "0" || m == "0" || d == "0")
                                {
                                    allForced = false;
                                }
                                else
                                {
                                    try
                                    {
                                        DateTime tempDateTime = Convert.ToDateTime(y + "-" + m.PadLeft(2, '0') + '-' + d.PadLeft(2, '0'));
                                        allForced = tempDateTime < DateTime.Now;
                                    }
                                    catch (Exception)
                                    {
                                        allForced = false;
                                    }
                                }
                            }
                            break;
                        case 4:
                            if (!rs.IsDBNull(2) && rs.GetInt32(2) == 1 && (((TextBox)ph.FindControl("BQ" + rs.GetInt32(0))).Text.Length < (rs.IsDBNull(3) ? 1 : rs.GetInt32(3)) || ((TextBox)ph.FindControl("BQ" + rs.GetInt32(0))).Text.Length > (rs.IsDBNull(5) ? 9 : rs.GetInt32(5))))
                            {
                                if ((rs.IsDBNull(3) ? 1 : rs.GetInt32(3)) == (rs.IsDBNull(5) ? 9 : rs.GetInt32(5)))
                                {
                                    err += (err != "" ? "<BR>" : "") + rs.GetString(4) + " skall skrivas med " + (rs.IsDBNull(3) ? 1 : rs.GetInt32(3)) + ((rs.IsDBNull(3) ? 1 : rs.GetInt32(3)) == 1 ? " siffra." : " siffror.");
                                }
                                else if (((TextBox)ph.FindControl("BQ" + rs.GetInt32(0))).Text.Length > (rs.IsDBNull(5) ? 9 : rs.GetInt32(5)))
                                {
                                    err += (err != "" ? "<BR>" : "") + rs.GetString(4) + " får inte vara längre än " + (rs.IsDBNull(5) ? 9 : rs.GetInt32(5)) + ((rs.IsDBNull(5) ? 9 : rs.GetInt32(5)) == 1 ? " siffra." : " siffror.");
                                }
                                else
                                {
                                    err += (err != "" ? "<BR>" : "") + rs.GetString(4) + " måste innehålla minst " + (rs.IsDBNull(3) ? 1 : rs.GetInt32(3)) + ((rs.IsDBNull(3) ? 1 : rs.GetInt32(3)) == 1 ? " siffra." : " siffror.");
                                }
                                allForced = false;
                            }
                            else
                            {
                                try
                                {
                                    Convert.ToInt32(((TextBox)ph.FindControl("BQ" + rs.GetInt32(0))).Text);
                                }
                                catch (Exception) { allForced = false; }
                            }
                            break;
                        case 7:
                            if (!rs.IsDBNull(2) && rs.GetInt32(2) == 1 && ((DropDownList)ph.FindControl("BQ" + rs.GetInt32(0))).SelectedValue == "")
                            {
                                allForced = false;
                            }
                            break;
                    }
                }
            }
            rs.Close();

            return allForced;
        }

        public static void saveBQ(PlaceHolder ph)
        {
            int userProfileID = 0;

            Db.exec("INSERT INTO UserProfile (UserID, SponsorID, DepartmentID) VALUES (" + HttpContext.Current.Session["UserID"] + "," + HttpContext.Current.Session["SponsorID"] + "," + HttpContext.Current.Session["DepartmentID"] + ")");
            SqlDataReader rs = Db.rs("SELECT TOP 1 UserProfileID FROM UserProfile WHERE UserID = " + HttpContext.Current.Session["UserID"] + " ORDER BY UserProfileID DESC");
            if (rs.Read())
            {
                userProfileID = rs.GetInt32(0);
            }
            rs.Close();

            string comparison = "";
            string comparisonInsert = "";
            rs = Db.rs("SELECT " +
                "sbq.BQID, " +           // 0
                "bq.Type, " +            // 1
                "(" +
                    "SELECT COUNT(*) " +
                    "FROM BQVisibility v " +
                    "INNER JOIN SponsorBQ b ON v.BQID = b.BQID AND b.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " " +
                    "WHERE b.Hidden = 0 AND v.ChildBQID = sbq.BQID" +
                "), " +                  // 2
                "sbq.SponsorBQID, " +    // 3
                "sbq.Hidden, " +         // 4
                "BQ.Comparison " +       // 5
                "FROM Sponsor s " +
                "INNER JOIN SponsorBQ sbq ON s.SponsorID = sbq.SponsorID " +
                "INNER JOIN BQ ON BQ.BQID = sbq.BQID " +
                "WHERE s.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " ORDER BY BQ.BQID");
            while (rs.Read())
            {
                if (rs.GetInt32(4) == 1)
                {
                    try
                    {
                        int val = 0;
                        DateTime valDate = DateTime.MinValue;
                        string valText = "";

                        if (Convert.ToInt32(HttpContext.Current.Session["SponsorInviteID"]) > 0)
                        {
                            SqlDataReader rs2 = Db.rs("SELECT BAID, ValueInt, ValueDate, ValueText FROM SponsorInviteBQ WHERE SponsorInviteID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorInviteID"]) + " AND BQID = " + rs.GetInt32(0));
                            if (rs2.Read())
                            {
                                if (!rs2.IsDBNull(0))
                                {
                                    val = rs2.GetInt32(0);
                                }
                                if (!rs2.IsDBNull(1))
                                {
                                    val = rs2.GetInt32(1);
                                }
                                if (!rs2.IsDBNull(2))
                                {
                                    valDate = rs2.GetDateTime(2);
                                }
                                if (!rs2.IsDBNull(3))
                                {
                                    valText = rs2.GetString(3);
                                }
                            }
                            rs2.Close();
                        }
                        else
                        {
                            SqlDataReader rs2 = Db.rs("SELECT " +
                                "upb.ValueInt, " +
                                "upb.ValueDate, " +
                                "upb.ValueText " +
                                "FROM [User] u " +
                                "INNER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID " +
                                "INNER JOIN UserProfileBQ upb ON up.UserProfileID = upb.UserProfileID " +
                                "WHERE u.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " " +
                                "AND upb.BQID = " + rs.GetInt32(0));
                            if (rs2.Read())
                            {
                                if (!rs2.IsDBNull(0))
                                {
                                    val = rs2.GetInt32(0);
                                }
                                if (!rs2.IsDBNull(1))
                                {
                                    valDate = rs2.GetDateTime(1);
                                }
                                if (!rs2.IsDBNull(2))
                                {
                                    valText = rs2.GetString(2);
                                }

                            }
                            rs2.Close();
                        }

                        if (val != 0)
                        {
                            Db.exec("INSERT INTO UserProfileBQ (UserProfileID, BQID, ValueInt) VALUES (" + userProfileID + "," + rs.GetInt32(0) + "," + val + ")");
                            HttpContext.Current.Session["UserID" + HttpContext.Current.Session["UserID"] + "BQ" + rs.GetInt32(0)] = val.ToString();

                            comparison += (!rs.IsDBNull(5) ? val.ToString() : "");
                            comparisonInsert += (!rs.IsDBNull(5) ? (comparisonInsert != "" ? "¤" : "") + "INSERT INTO ProfileComparisonBQ (ProfileComparisonID,BQID,ValueInt) VALUES ([x]," + rs.GetInt32(0) + "," + val + ")" : "");
                        }
                        if (valDate != DateTime.MinValue)
                        {
                            Db.exec("INSERT INTO UserProfileBQ (UserProfileID, BQID, ValueDate) VALUES (" + userProfileID + "," + rs.GetInt32(0) + ",'" + valDate.ToString("yyyy-MM-dd") + "')");
                            HttpContext.Current.Session["UserID" + HttpContext.Current.Session["UserID"] + "BQ" + rs.GetInt32(0)] = valDate.ToString("yyyy-MM-dd");
                        }
                        if (valText != "")
                        {
                            Db.exec("INSERT INTO UserProfileBQ (UserProfileID, BQID, ValueText) VALUES (" + userProfileID + "," + rs.GetInt32(0) + ",'" + valText.Replace("'","''") + "')");
                            HttpContext.Current.Session["UserID" + HttpContext.Current.Session["UserID"] + "BQ" + rs.GetInt32(0)] = valText;
                        }
                    }
                    catch (Exception) { }
                }
                else if (rs.GetInt32(2) == 0 || checkParent(rs.GetInt32(0), ph))
                {
                    switch (rs.GetInt32(1))
                    {
                        case 1:
                            if (((RadioButtonList)ph.FindControl("BQ" + rs.GetInt32(0))).SelectedValue != "")
                            {
                                try
                                {
                                    int val = Convert.ToInt32(((RadioButtonList)ph.FindControl("BQ" + rs.GetInt32(0))).SelectedValue.Replace("'", ""));
                                    Db.exec("INSERT INTO UserProfileBQ (UserProfileID, BQID, ValueInt) VALUES (" + userProfileID + "," + rs.GetInt32(0) + "," + val + ")");
                                    HttpContext.Current.Session["UserID" + HttpContext.Current.Session["UserID"] + "BQ" + rs.GetInt32(0)] = val.ToString();

                                    comparison += (!rs.IsDBNull(5) ? val.ToString() : "");
                                    comparisonInsert += (!rs.IsDBNull(5) ? (comparisonInsert != "" ? "¤" : "") + "INSERT INTO ProfileComparisonBQ (ProfileComparisonID,BQID,ValueInt) VALUES ([x]," + rs.GetInt32(0) + "," + val + ")" : "");
                                }
                                catch (Exception) { }
                            }
                            break;
                        case 2:
                            if (((TextBox)ph.FindControl("BQ" + rs.GetInt32(0))).Text != "")
                            {
                                try
                                {
                                    string valText = ((TextBox)ph.FindControl("BQ" + rs.GetInt32(0))).Text.Replace("'", "''");
                                    Db.exec("INSERT INTO UserProfileBQ (UserProfileID, BQID, ValueText) VALUES (" + userProfileID + "," + rs.GetInt32(0) + ",'" + valText + "')");
                                    HttpContext.Current.Session["UserID" + HttpContext.Current.Session["UserID"] + "BQ" + rs.GetInt32(0)] = valText;
                                }
                                catch (Exception) { }
                            }
                            break;
                        case 3:
                            string y = ((DropDownList)ph.FindControl("BQ" + rs.GetInt32(0) + "Y")).SelectedValue.Replace("'", "");
                            string m = ((DropDownList)ph.FindControl("BQ" + rs.GetInt32(0) + "M")).SelectedValue.Replace("'", "");
                            string d = ((DropDownList)ph.FindControl("BQ" + rs.GetInt32(0) + "D")).SelectedValue.Replace("'", "");
                            if (y != "0" && m != "0" && d != "0")
                            {
                                try
                                {
                                    DateTime tempDateTime = Convert.ToDateTime(y + "-" + m.PadLeft(2, '0') + '-' + d.PadLeft(2, '0'));
                                    if (tempDateTime < DateTime.Now)
                                    {
                                        Db.exec("INSERT INTO UserProfileBQ (UserProfileID, BQID, ValueDate) VALUES (" + userProfileID + "," + rs.GetInt32(0) + ",'" + tempDateTime.ToString("yyyy-MM-dd") + "')");
                                        HttpContext.Current.Session["UserID" + HttpContext.Current.Session["UserID"] + "BQ" + rs.GetInt32(0)] = tempDateTime.ToString("yyyy-MM-dd");
                                    }
                                }
                                catch (Exception) { }
                            }
                            break;
                        case 4:
                            if (((TextBox)ph.FindControl("BQ" + rs.GetInt32(0))).Text != "")
                            {
                                try
                                {
                                    int val = Convert.ToInt32(((TextBox)ph.FindControl("BQ" + rs.GetInt32(0))).Text.Replace("'", ""));
                                    Db.exec("INSERT INTO UserProfileBQ (UserProfileID, BQID, ValueInt) VALUES (" + userProfileID + "," + rs.GetInt32(0) + "," + val + ")");
                                    HttpContext.Current.Session["UserID" + HttpContext.Current.Session["UserID"] + "BQ" + rs.GetInt32(0)] = val.ToString();
                                }
                                catch (Exception) { }
                            }
                            break;
                        case 7:
                            if (((DropDownList)ph.FindControl("BQ" + rs.GetInt32(0))).SelectedValue != "")
                            {
                                try
                                {
                                    int val = Convert.ToInt32(((DropDownList)ph.FindControl("BQ" + rs.GetInt32(0))).SelectedValue.Replace("'", ""));
                                    Db.exec("INSERT INTO UserProfileBQ (UserProfileID, BQID, ValueInt) VALUES (" + userProfileID + "," + rs.GetInt32(0) + "," + val + ")");
                                    HttpContext.Current.Session["UserID" + HttpContext.Current.Session["UserID"] + "BQ" + rs.GetInt32(0)] = val.ToString();

                                    comparison += (!rs.IsDBNull(5) ? val.ToString() : "");
                                    comparisonInsert += (!rs.IsDBNull(5) ? (comparisonInsert != "" ? "¤" : "") + "INSERT INTO ProfileComparisonBQ (ProfileComparisonID,BQID,ValueInt) VALUES ([x]," + rs.GetInt32(0) + "," + val + ")" : "");
                                }
                                catch (Exception) { }
                            }
                            break;
                    }
                }
            }
            rs.Close();

            int profileComparisonID = 0;
            rs = Db.rs("SELECT ProfileComparisonID FROM ProfileComparison WHERE Hash = '" + Db.HashMD5(comparison) + "'");
            if (rs.Read())
            {
                profileComparisonID = rs.GetInt32(0);
            }
            rs.Close();
            if (profileComparisonID == 0)
            {
                Db.exec("INSERT INTO ProfileComparison (Hash) VALUES ('" + Db.HashMD5(comparison) + "')");
                rs = Db.rs("SELECT TOP 1 ProfileComparisonID FROM ProfileComparison WHERE Hash = '" + Db.HashMD5(comparison) + "'");
                if (rs.Read())
                {
                    profileComparisonID = rs.GetInt32(0);
                }
                rs.Close();
                if (comparisonInsert != "")
                {
                    if (comparisonInsert.IndexOf('¤') >= 0)
                    {
                        foreach (string s in comparisonInsert.Split('¤'))
                        {
                            Db.exec(s.Replace("[x]",profileComparisonID.ToString()));
                        }
                    }
                    else
                    {
                        Db.exec(comparisonInsert.Replace("[x]", profileComparisonID.ToString()));
                    }
                }
            }
            Db.exec("UPDATE UserProfile SET ProfileComparisonID = " + profileComparisonID + " WHERE UserProfileID = " + userProfileID);
            Db.exec("UPDATE [User] SET UserProfileID = " + userProfileID + " WHERE UserID = " + HttpContext.Current.Session["UserID"]);
            HttpContext.Current.Session["UserProfileID"] = userProfileID;
        }

        public static void checkAndLogin()
        {
            if (HttpContext.Current.Request.Form["Usern"] != null && HttpContext.Current.Request.Form["Losen"] != null)
            {
				int userID = checkUsernamePassword(HttpContext.Current.Request.Form["Usern"].ToString(),HttpContext.Current.Request.Form["Losen"].ToString());
				checkAndLogin(userID);
            }
        }
        public static int checkUsernamePassword(string UN, string PW)
        {
            int userID = 0;
            SqlDataReader rs = Db.rs("SELECT u.UserID FROM [User] u WHERE u.Username = '" + UN.Replace("'", "") + "' AND u.Password = '" + Db.HashMD5(PW.Trim()) + "'");
            if (rs.Read())
            {
                userID = rs.GetInt32(0);
            }
            rs.Close();
            return userID;
        }
        public static void checkAndLogin(int userID)
        {
            checkAndLogin(userID,true);
        }
		public static void checkAndLogin(int userID, bool reset)
		{
			if (userID != 0)
			{
                int sponsorExtendedSurveyCount = 0;

				SqlDataReader rs = Db.rs("SELECT " +
                    "u.UserID, " +                                                      // 0
                    "u.SponsorID, " +                                                   // 1
                    "u.UserProfileID, " +                                               // 2
                    "u.DepartmentID, " +                                                // 3
                    "LEFT(REPLACE(CONVERT(VARCHAR(255),s.SponsorKey),'-',''),8), " +    // 4
                    "u.ReminderType, " +                                                // 5
                    "u.ReminderSettings, " +                                            // 6
                    "u.Email, " +                                                       // 7
                    "dbo.cf_departmentTree(u.DepartmentID,','), " +                     // 8
                    "s.Sponsor, " +                                                     // 9
                    "NULL, " +                                                          // 10
                    "s.ForceLID " +                                                     // 11
                    "FROM [User] u " +
                    "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
                    "WHERE u.UserID = " + userID);
				if (rs.Read())
				{
                    if (!rs.IsDBNull(11) || !rs.IsDBNull(10))
                    {
                        HttpContext.Current.Session["LID"] = (rs.IsDBNull(11) ? rs.GetInt32(10) : rs.GetInt32(11));
                        if (!rs.IsDBNull(11))
                        {
                            HttpContext.Current.Session["ForceLID"] = rs.GetInt32(11);
                        }
                    }
                    else if(reset)
                    {
                        Db.exec("UPDATE [User] SET LID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " WHERE UserID = " + userID);
                    }
					HttpContext.Current.Session["UserID"] = rs.GetInt32(0);
					loadSponsor(rs.GetInt32(1).ToString(), rs.GetString(4));
					HttpContext.Current.Session["UserProfileID"] = rs.GetInt32(2);
					HttpContext.Current.Session["DepartmentID"] = (rs.IsDBNull(3) ? "NULL" : rs.GetInt32(3).ToString());
					loadUserSession(Convert.ToInt32(HttpContext.Current.Session["UserID"]));
					if (rs.IsDBNull(5))
					{
						HttpContext.Current.Session["NoReminderSet"] = 1;
					}
					else if (rs.GetInt32(5) == 2 && reset)
					{
						Db.exec("UPDATE [User] SET ReminderNextSend = '" + Db.nextReminderSend(2, rs.GetString(6).Split(':'), DateTime.Now, DateTime.Now) + "' " +
                            "WHERE UserID = " + rs.GetInt32(0));
					}
                    if (checkExtendedSurveys(ref sponsorExtendedSurveyCount, rs.GetInt32(1), rs.GetInt32(0), (rs.GetString(9) + (!rs.IsDBNull(8) ? "," + rs.GetString(8) : "")).Split(','), rs.GetString(7), rs.GetInt32(2)))
                    {
                        HttpContext.Current.Session["HasExtendedSurveys"] = 1;
                    }
				}
				rs.Close();
				if (HttpContext.Current.Session["UserID"] != null)
				{
                    if (sponsorExtendedSurveyCount > 0)
                    {
                        // (HttpContext.Current.Request.IsSecureConnection ? "http://" + HttpContext.Current.Request.Url.Host : "") + 
                        HttpContext.Current.Response.Redirect("/extendedSurvey.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
                    }
					else if (Convert.ToInt32(HttpContext.Current.Session["NoReminderSet"]) == 1)
					{
						HttpContext.Current.Response.Redirect("/reminder.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
					}
					else
					{
						HttpContext.Current.Response.Redirect("/submit.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
					}
				}
			}
		}
        
		public static string nextReminderSend(int type, string[] settings, DateTime lastLogin, DateTime lastSend)
		{
			DateTime nextPossibleReminderSend = lastSend.Date.AddHours(Convert.ToInt32(settings[0]));
			while (nextPossibleReminderSend <= DateTime.Now.AddMinutes(30))
			{
				nextPossibleReminderSend = nextPossibleReminderSend.AddDays(1);
			}
			DateTime nextReminderSend = nextPossibleReminderSend.AddYears(10);

			try
			{
				switch (type)
				{
					case 1:
						System.DayOfWeek[] dayOfWeek = { System.DayOfWeek.Monday, System.DayOfWeek.Tuesday, System.DayOfWeek.Wednesday, System.DayOfWeek.Thursday, System.DayOfWeek.Friday, System.DayOfWeek.Saturday, System.DayOfWeek.Sunday };

						switch (Convert.ToInt32(settings[1]))
						{
							case 1:
								#region Weekday
								{
									string[] days = settings[2].Split(',');
									foreach (string day in days)
									{
										DateTime tmp = nextPossibleReminderSend;
										while (tmp.DayOfWeek != dayOfWeek[Convert.ToInt32(day) - 1])
										{
											tmp = tmp.AddDays(1);
										}
										if (tmp < nextReminderSend)
										{
											nextReminderSend = tmp;
										}
									}
									break;
								}
								#endregion
							case 2:
								#region Week
								{
									nextReminderSend = nextPossibleReminderSend.AddDays(7 * (Convert.ToInt32(settings[3]) - 1));
									while (nextReminderSend.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[2]) - 1])
									{
										nextReminderSend = nextReminderSend.AddDays(1);
									}
									break;
								}
								#endregion
							case 3:
								#region Month
								{
									DateTime tmp = nextPossibleReminderSend.AddDays(-nextPossibleReminderSend.Day);
									int i = 0;
									while (tmp.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[3]) - 1] || i != Convert.ToInt32(settings[2]))
									{
										tmp = tmp.AddDays(1);
										if (tmp.DayOfWeek == dayOfWeek[Convert.ToInt32(settings[3]) - 1])
										{
											i++;
										}
									}
									nextReminderSend = nextPossibleReminderSend.AddMonths((Convert.ToInt32(settings[4]) - 1));
									if (tmp < nextPossibleReminderSend)
									{
										// Has allready occurred this month
										nextReminderSend = nextReminderSend.AddMonths(1);
									}
									nextReminderSend = nextReminderSend.AddDays(-nextReminderSend.Day);
									i = 0;
									while (nextReminderSend.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[3]) - 1] || i != Convert.ToInt32(settings[2]))
									{
										nextReminderSend = nextReminderSend.AddDays(1);
										if (nextReminderSend.DayOfWeek == dayOfWeek[Convert.ToInt32(settings[3]) - 1])
										{
											i++;
										}
									}
									break;
								}
								#endregion
						}
						break;
					case 2:
						nextReminderSend = lastLogin.Date.AddHours(Convert.ToInt32(settings[0])).AddDays(Convert.ToInt32(settings[1]) * Convert.ToInt32(settings[2]));
						while (nextReminderSend < nextPossibleReminderSend)
						{
							nextReminderSend = nextReminderSend.AddDays(7);
						}
						break;
				}
			}
			catch (Exception) 
			{
				nextReminderSend = nextPossibleReminderSend.AddYears(10);
			}

			return nextReminderSend.ToString("yyyy-MM-dd HH:mm");
		}

        public static void loadUserSession(int userID)
        {
            SqlDataReader rs = Db.rs("SELECT ProjectRoundUnitID, ProjectRoundUserID FROM UserProjectRoundUser WHERE UserID = " + userID);
            while (rs.Read())
            {
                Db.exec("UPDATE Answer SET ProjectRoundUnitID = -" + rs.GetInt32(0) + ", ProjectRoundUserID = -" + rs.GetInt32(1) + " WHERE EndDT IS NULL AND ProjectRoundUnitID = " + rs.GetInt32(0) + " AND ProjectRoundUserID = " + rs.GetInt32(1), "eFormSqlConnection");
            }
            rs.Close();

            rs = Db.rs("" +
                "SELECT " +
                "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-',''), " +
                "spru.SurveyID " +
                "FROM [User] u " +
                "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
                "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
                "WHERE u.UserID = " + userID);
            while (rs.Read())
            {
                HttpContext.Current.Session["SurveyID_" + rs.GetInt32(1)] = rs.GetString(0);
            }
            rs.Close();

            rs = Db.rs("SELECT " +
                "spru.ProjectRoundUnitID, " +
                "bqmap.BQID, " +
                "bqmap.QID, " +
                "bqmap.OID, " +
                "bqmap.FN, " +
                "bqmap.SponsorPRUBQmapID " +
                "FROM SponsorProjectRoundUnit spru " +
                "INNER JOIN SponsorPRUBQmap bqmap ON spru.SponsorProjectRoundUnitID = bqmap.SponsorProjectRoundUnitID " +
                "WHERE spru.SponsorID = " + HttpContext.Current.Session["SponsorID"]);
            while (rs.Read())
            {
                string val = "";
                SqlDataReader rs2 = Db.rs("SELECT ValueInt FROM UserProfileBQ WHERE BQID = " + rs.GetInt32(1) + " AND UserProfileID = " + HttpContext.Current.Session["UserProfileID"]);
                if (rs2.Read())
                {
                    val = rs2.GetInt32(0).ToString();
                }
                rs2.Close();

                if (val != "")
                {
                    if (!rs.IsDBNull(4) && rs.GetInt32(4) == 1)
                    {
                        val = (DateTime.Now.Year - Convert.ToInt32(val)).ToString();
                    }
                    else
                    {
                        rs2 = Db.rs("SELECT OCID FROM SponsorPRUBQmapVal WHERE SponsorPRUBQmapID = " + rs.GetInt32(5) + " AND BAID = " + val);
                        if (rs2.Read())
                        {
                            val = rs2.GetInt32(0).ToString();
                        }
                        rs2.Close();
                    }
                }
                HttpContext.Current.Session["PRUID" + rs.GetInt32(0) + "Q" + rs.GetInt32(2) + "O" + rs.GetInt32(3)] = val;
            }
            rs.Close();

            rs = Db.rs("SELECT Username FROM [User] WHERE UserID = " + userID);
            if (rs.Read())
            {
                HttpContext.Current.Session["Username"] = (rs.GetString(0).IndexOf("AUTO_CREATED_GUEST") >= 0 ? "Gäst" : rs.GetString(0));
            }
            rs.Close();

            Db.exec("UPDATE Session SET UserID = " + userID + " WHERE SessionID = " + HttpContext.Current.Session["SessionID"]);
            if (HttpContext.Current.Session["ReferrerURL"].ToString() != "")
            {
                rs = Db.rs("SELECT Affiliate FROM Affiliate");
                while (rs.Read())
                {
                    if (HttpContext.Current.Session["ReferrerURL"].ToString().ToLower().IndexOf(rs.GetString(0).ToLower()) >= 0)
                    {
                        HttpContext.Current.Session["HomeURL"] = HttpContext.Current.Session["ReferrerURL"];
                    }
                }
                rs.Close();
            }
        }

        public static void loadSponsor(string sponsorID, string sponsorKey)
        {
            if (HttpContext.Current.Application["SPK" + sponsorID] != null && HttpContext.Current.Application["SPK" + sponsorID].ToString() == sponsorKey)
            {
                HttpContext.Current.Session["SponsorID"] = sponsorID;
            }
            else
            {
                try
                {
                    int test = Convert.ToInt32(sponsorID);

                    SqlDataReader rs2 = Db.rs("SELECT " +
                        "s.Application, " +         // 0
                        "s.Sponsor, " +
                        "ss.SuperSponsorID, " +
                        "ss.SuperSponsor, " +
                        "ss.Logo, " +
                        "ssl.LangID, " +            // 5
                        "ssl.Slogan, " +
                        "ssl.Header " +
                        "FROM Sponsor s " +
                        "LEFT OUTER JOIN SuperSponsor ss ON s.SuperSponsorID = ss.SuperSponsorID " +
                        "LEFT OUTER JOIN SuperSponsorLang ssl ON ss.SuperSponsorID = ssl.SuperSponsorID " +
                        "WHERE s.SponsorID = " + test + " " +
                        "AND LEFT(REPLACE(CONVERT(VARCHAR(255),s.SponsorKey),'-',''),8) = '" + sponsorKey.Replace("'", "''") + "'");
                    if (rs2.Read())
                    {
                        HttpContext.Current.Session["SponsorID"] = test;
                        HttpContext.Current.Application["SPK" + test] = sponsorKey;
                        HttpContext.Current.Application["APP" + test] = rs2.GetString(0);
                        HttpContext.Current.Application["SPN" + test] = rs2.GetString(1);
                        HttpContext.Current.Application["SUPERSPONSOR" + test] = (rs2.IsDBNull(2) ? 0 : rs2.GetInt32(2));
                        HttpContext.Current.Application["SUPERSPONSORNAME" + test] = (rs2.IsDBNull(3) ? "" : rs2.GetString(3));
                        HttpContext.Current.Application["SUPERSPONSORLOGO" + test] = (rs2.IsDBNull(4) ? 1 : rs2.GetInt32(4));
                        do
                        {
                            if (!rs2.IsDBNull(6))
                            {
                                HttpContext.Current.Application["SUPERSPONSORLOGO" + test + "LANG" + rs2.GetInt32(5)] = (rs2.IsDBNull(6) ? "" : rs2.GetString(6));
                            }
                            if (!rs2.IsDBNull(7))
                            {
                                HttpContext.Current.Application["SUPERSPONSORHEAD" + test + "LANG" + rs2.GetInt32(5)] = (rs2.IsDBNull(7) ? "" : rs2.GetString(7));
                            }
                        }
                        while (rs2.Read());
                    }
                    rs2.Close();
                }
                catch (Exception) { }
            }
        }
	}
}
