using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace HW
{
    public partial class calendarRead : System.Web.UI.Page
    {
        public DateTime fromDT = DateTime.Now.AddMonths(-1);
        public DateTime toDT = DateTime.Now;

        private string renderMood(int today)
        {
            switch (today)
            {
                default: return "";
                case 2: return "happy";
                case 3: return "neutral";
                case 4: return "unhappy";
            }
        }
        public string todaysActivities()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "Todays activities/measurements";
                default: return "Dagens aktiviteter/mätningar";
            }
        }

        public string timeSpace()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "<span>From </span>" +
                           "<div id=\"\" class=\"var from year\"><dl class=\"dropdown\"><dt><a><span>" + fromDT.Year + "</span></a></dt></dl></div>" +
                           "<div id=\"\" class=\"var from month\"><dl class=\"dropdown\"><dt><a><span>" + fromDT.Month.ToString().PadLeft(2, '0') + "</span></a></dt></dl></div>" +
                           "<div id=\"\" class=\"var from day\"><dl class=\"dropdown\"><dt><a><span>" + fromDT.Day.ToString().PadLeft(2, '0') + "</span></a></dt></dl></div>" +
                           "<span> To </span>" +
                           "<div id=\"\" class=\"var to year\"><dl class=\"dropdown\"><dt><a><span>" + toDT.Year + "</span></a></dt></dl></div>" +
                           "<div id=\"\" class=\"var to month\"><dl class=\"dropdown\"><dt><a><span>" + toDT.Month.ToString().PadLeft(2, '0') + "</span></a></dt></dl></div>" +
                           "<div id=\"\" class=\"var to day\"><dl class=\"dropdown\"><dt><a><span>" + toDT.Day.ToString().PadLeft(2, '0') + "</span></a></dt></dl></div>" +
                           "<a class=\"button\" id=\"updateDate\" href=\"#\"><span>Update</span></a>";
                default: return "<span>Fr&aring;n </span>" +
                           "<div id=\"\" class=\"var from year\"><dl class=\"dropdown\"><dt><a><span>" + fromDT.Year + "</span></a></dt></dl></div>" +
                           "<div id=\"\" class=\"var from month\"><dl class=\"dropdown\"><dt><a><span>" + fromDT.Month.ToString().PadLeft(2, '0') + "</span></a></dt></dl></div>" +
                           "<div id=\"\" class=\"var from day\"><dl class=\"dropdown\"><dt><a><span>" + fromDT.Day.ToString().PadLeft(2, '0') + "</span></a></dt></dl></div>" +
                           "<span> Till </span>" +
                           "<div id=\"\" class=\"var to year\"><dl class=\"dropdown\"><dt><a><span>" + toDT.Year + "</span></a></dt></dl></div>" +
                           "<div id=\"\" class=\"var to month\"><dl class=\"dropdown\"><dt><a><span>" + toDT.Month.ToString().PadLeft(2, '0') + "</span></a></dt></dl></div>" +
                           "<div id=\"\" class=\"var to day\"><dl class=\"dropdown\"><dt><a><span>" + toDT.Day.ToString().PadLeft(2, '0') + "</span></a></dt></dl></div>" +
                           "<a class=\"button\" id=\"updateDate\" href=\"#\"><span>Uppdatera</span></a>";
            }
        }
        public string print()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "Print";
                default: return "Skriv ut";
            }
        }
        public string showLess()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "Show less";
                default: return "Visa färre";
            }
        }
        public string showMore()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "Show more";
                default: return "Visa mer";
            }
        }
        public string timePeriod()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "Select timeframe";
                default: return "Välj tidsperiod du vill läsa";
            }
        }

        public string menu()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "<a href=\"calendar.aspx\"><span>Add or edit</span></a><a class=\"active\" href=\"#\"><span>Read</span></a>";
                default: return "<a href=\"calendar.aspx\"><span>Lägg till eller redigera</span></a><a class=\"active\" href=\"#\"><span>Läs dagboken</span></a>";
            }
        }
        public static string pageHeader()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2:
                    return "Calendar";
                default:
                    return "Dagbok";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2:
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    break;
                default:
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");
                    break;
            }

            if (HttpContext.Current.Request.QueryString["FD"] != null)
            {
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
                fromDT = DateTime.ParseExact(HttpContext.Current.Request.QueryString["FD"].ToString(), "yyyyMMdd", ci);
            }
            if (HttpContext.Current.Request.QueryString["TD"] != null)
            {
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
                toDT = DateTime.ParseExact(HttpContext.Current.Request.QueryString["TD"].ToString(), "yyyyMMdd", ci);
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            DateTime oldDT = DateTime.MinValue;
            string sql = "SELECT DISTINCT " +
                "dbo.cf_yearMonthDay(um.DT) AS M " +
                "FROM UserMeasure um " +
                "INNER JOIN UserMeasureComponent umc ON um.UserMeasureID = umc.UserMeasureID " +
                "INNER JOIN MeasureComponent mc ON umc.MeasureComponentID = mc.MeasureComponentID " +
                "INNER JOIN Measure m ON mc.MeasureID = m.MeasureID " +
                "LEFT OUTER JOIN MeasureLang ml ON m.MeasureID = ml.MeasureID AND ml.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                "WHERE mc.ShowInList = 1 AND um.DeletedDT IS NULL AND um.UserID = " + HttpContext.Current.Session["UserID"] + " " +
                "AND dbo.cf_yearMonthDay(um.DT) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
                "AND dbo.cf_yearMonthDay(um.DT) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +

                "UNION ALL " +

                "SELECT DISTINCT " +
                "dbo.cf_yearMonthDay(es.DateTime) AS M " +
                "FROM ExerciseStats es " +
                "INNER JOIN ExerciseVariantLang evl ON es.ExerciseVariantLangID = evl.ExerciseVariantLangID " +
                "INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID " +
                "INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID " +
                "LEFT OUTER JOIN ExerciseLang el ON e.ExerciseID = el.ExerciseID AND el.Lang = " + (Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1) + " " +
                "WHERE es.UserID = " + HttpContext.Current.Session["UserID"] + " " +
                "AND dbo.cf_yearMonthDay(es.DateTime) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
                "AND dbo.cf_yearMonthDay(es.DateTime) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +

                "UNION ALL " +

                "SELECT DISTINCT " +
                "dbo.cf_yearMonthDay(uprua.DT) AS M " +
                "FROM UserProjectRoundUser upru " +
                "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
                "INNER JOIN [User] u ON upru.UserID = u.UserID " +
                "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
                "INNER JOIN SponsorProjectRoundUnit spru ON upru.ProjectRoundUnitID = spru.ProjectRoundUnitID AND s.SponsorID = spru.SponsorID " +
                "INNER JOIN MeasureCategory mc ON spru.SponsorProjectRoundUnitID = mc.SPRUID " +
                "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                "WHERE upru.UserID = " + HttpContext.Current.Session["UserID"] + " " +
                "AND dbo.cf_yearMonthDay(uprua.DT) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
                "AND dbo.cf_yearMonthDay(uprua.DT) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +

                "UNION ALL " +

                "SELECT DISTINCT " +
                "dbo.cf_yearMonthDay(d.DiaryDate) AS M " +
                "FROM Diary d " +
                "WHERE d.UserID = " + HttpContext.Current.Session["UserID"] + " " +
                "AND dbo.cf_yearMonthDay(d.DiaryDate) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
                "AND dbo.cf_yearMonthDay(d.DiaryDate) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +

                "ORDER BY M DESC";
            //HttpContext.Current.Response.Write(sql);
            SqlDataReader rs = Db.rs(sql);
            while (rs.Read())
            {
                DateTime dt = DateTime.ParseExact(rs.GetString(0), "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                if (dt != oldDT)
                {
                    sb.Append("<div class=\"post\">" +
                              "<div class=\"top\"></div>" +
                              "<div class=\"dategroup\">" +
                              "<div class=\"date\">" +
                              "<div class=\"small\">" + dt.DayOfWeek + "</div>" +
                              "<div class=\"large\">" + dt.Day + "</div>" +
                              "<div class=\"small monthyear\">" + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[Convert.ToInt32(dt.Month) - 1] + " " + dt.Year + "</div>" +
                              "</div>" +
                              "<a href=\"calendar.aspx?D=" + rs.GetString(0).Replace("-", "") + "\" class=\"edit\">");
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 2:
                            sb.Append("Edit");
                            break;
                        default:
                            sb.Append("Redigera");
                            break;
                    }
                    sb.Append("</a>" +
                              "</div>" +
                              "<div class=\"messagegroup\">" +
                              "<div class=\"message\">");
                    SqlDataReader rs2 = Db.rs("SELECT DiaryNote, Mood FROM Diary WHERE DeletedDT IS NULL AND UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " AND DiaryDate = '" + dt.ToString("yyyy-MM-dd") + "'");
                    while (rs2.Read())
                    {
                        sb.Append("" +
                            (!rs2.IsDBNull(1) && rs2.GetInt32(1) != 0 ? "<div class=\"mood " + renderMood(rs2.GetInt32(1)) + "\"></div>" : "") +
                            (!rs2.IsDBNull(0) && rs2.GetString(0) != "" ? "<p>" + rs2.GetString(0) + "</p>" : ""));
                    }
                    rs2.Close();
                    sb.Append("" +
                          "</div>" +
                          "<a href=\"#!\" class=\"2 moretoggle\">" + showMore() + "</a>" +
                          "</div>" +
                          "<div class=\"activities\">" +
                          "<div class=\"title\">" + todaysActivities() + "</div>" +
                          Db.fetchActs(dt.ToString("yyyy-MM-dd"), Convert.ToInt32(HttpContext.Current.Session["LID"]), Convert.ToInt32(HttpContext.Current.Session["UserID"]), false) +
                          "</div>" +
                          "<span>" +
                          "<a href=\"#!\" class=\"2 moretoggle\">" + showMore() + "</a>" +
                          "<span>" +
                          "<div class=\"bottomcontainer\"></div>" +
                          "<div class=\"bottom\"></div>" +
                          "</div><!-- end .post -->");
                }
                oldDT = dt;
            }
            rs.Close();

            posts.Controls.Add(new LiteralControl(sb.ToString()));
        }
    }
}