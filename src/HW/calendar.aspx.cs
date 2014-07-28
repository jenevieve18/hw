using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data.SqlClient;

namespace HW
{
    public partial class calendar : System.Web.UI.Page
    {
        private bool updateActs = false;
        private string UMIDs = "";
        public DateTime dt = DateTime.Now;

        public string selectDay()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "Select day";
                default: return "Välj dag";
            }
        }
        public string showCalendar()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "Show calendar";
                default: return "Visa kalender";
            }
        }
        public string orCancel()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "Or cancel";
                default: return "Eller avbryt";
            }
        }
        public string selectActivity()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "Select activity/measurement";
                default: return "Välj aktivitet/mätning";
            }
        }
        public string add()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "Add";
                default: return "Lägg till";
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
        public string notes()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "Notes";
                default: return "Anteckningar";
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
        public string todaysMood()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "Todays mood";
                default: return "Dagsform";
            }
        }
        public string menu()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "<a class=\"active\" href=\"#\"><span>Add or edit</span></a><a href=\"calendarRead.aspx\"><span>Read</span></a>";
                default: return "<a class=\"active\" href=\"#\"><span>Lägg till eller redigera</span></a><a href=\"calendarRead.aspx\"><span>Läs dagboken</span></a>";
            }
        }
        public string shortDays()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa']";
                default: return "['Sö', 'M&aring;', 'Ti', 'On', 'To', 'Fr', 'L&ouml;']";
            }
        }
        public string days()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday']";
                default: return "['Söndag', 'Måndag', 'Tisdag', 'Onsdag', 'Torsdag', 'Fredag', 'Lördag']";
            }
        }
        public string months()
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 2: return "['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December']";
                default: return "['Januari', 'Februari', 'Mars', 'April', 'Maj', 'Juni', 'Juli', 'Augusti', 'September', 'Oktober', 'November', 'December']";
            }
        }
        private string uniqueStringFromLists(SortedList r, string s, string lastSeparator)
        {
            string ret = "";

            while (s.Replace("#", "") != "")
            {
                int i = Convert.ToInt32(s.Substring(1, s.Length - 2).Split('#')[0]);

                ret = ret.Replace("#", ", ");
                ret += (ret != "" ? "#" : "") + r[i];
                s = s.Replace("#" + i + "#", "#");
            }

            return ret.Replace("#", lastSeparator);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1:
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");
                    break;
                case 2:
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    submit1.Text = "<span>Save</span>";
                    submit2.Text = "Save";
                    break;
            }

            if (HttpContext.Current.Session["UserID"] == null)
            {
                HttpContext.Current.Response.Redirect("inactivity.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }
            if (HttpContext.Current.Request.Form["DeleteUMID"] != null && Convert.ToInt32(HttpContext.Current.Request.Form["DeleteUMID"]) != 0)
            {
                Db.exec("UPDATE UserMeasure SET DeletedDT = GETDATE() WHERE UserMeasureID = " + Convert.ToInt32(HttpContext.Current.Request.Form["DeleteUMID"]) + " AND UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]));
                updateActs = true;
            }
            if (HttpContext.Current.Request.Form["DeleteUPRUA"] != null && HttpContext.Current.Request.Form["DeleteUPRUA"].ToString() != "0")
            {
                string[] uprua = HttpContext.Current.Request.Form["DeleteUPRUA"].ToString().Split(':');
                Db.exec("UPDATE UserProjectRoundUserAnswer SET " +
                    "ProjectRoundUserID = -ABS(ProjectRoundUserID), " +
                    "UserProfileID = -ABS(UserProfileID) " +
                    "WHERE AnswerKey = '" + uprua[1].Replace("'", "") + "' " +
                    "AND UserProjectRoundUserAnswerID = " + Convert.ToInt32(uprua[0]));
                updateActs = true;
            }

            //if (!IsPostBack)
            //{
            //    updateActs = true;

            //    PersonalCalendar.Width = Unit.Pixel(250);
            //    PersonalCalendar.Height = Unit.Pixel(185);

            //    PersonalCalendar.BorderColor = System.Drawing.ColorTranslator.FromHtml("#D6D6D6");
            //    PersonalCalendar.BorderStyle = BorderStyle.Solid;
            //    PersonalCalendar.BorderWidth = Unit.Pixel(1);

            //    PersonalCalendar.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

            //    PersonalCalendar.OtherMonthDayStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D6D6D6");
            //    PersonalCalendar.DayHeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

            //    PersonalCalendar.SelectedDayStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //    PersonalCalendar.SelectedDayStyle.BorderColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            //    PersonalCalendar.SelectedDayStyle.BorderStyle = BorderStyle.Solid;
            //    PersonalCalendar.SelectedDayStyle.BorderWidth = Unit.Pixel(1);

            //    PersonalCalendar.NextPrevStyle.CssClass = "calendarTitle";
            //    PersonalCalendar.PrevMonthText = "&lt;&lt;";
            //    PersonalCalendar.NextMonthText = "&gt;&gt;";

            //    PersonalCalendar.TitleStyle.CssClass = "calendarTitle";
            //    PersonalCalendar.TitleStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#26a9df");

            //    PersonalCalendar.CssClass = "txt";

            //dt = DateTime.Now;
            if (HttpContext.Current.Request.QueryString["D"] != null)
            {
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
                dt = DateTime.ParseExact(HttpContext.Current.Request.QueryString["D"].ToString(), "yyyyMMdd", ci);
            }

            //PersonalCalendar.VisibleDate = (HttpContext.Current.Request.QueryString["D"] != null ? Convert.ToDateTime(HttpContext.Current.Request.QueryString["D"]) : DateTime.Now.Date);
            //PersonalCalendar.SelectedDate = (HttpContext.Current.Request.QueryString["D"] != null ? Convert.ToDateTime(HttpContext.Current.Request.QueryString["D"]) : DateTime.Now.Date);
            //}

            submit1.Click += new EventHandler(submit_Click);
            submit2.Click += new EventHandler(submit_Click);
            //PersonalCalendar.SelectionChanged += new EventHandler(PersonalCalendar_SelectionChanged);

            /*
            SqlDataReader rs = Db.rs("SELECT ExerciseVariantID,ExerciseContent,Lang FROM ExerciseVariantLang", "pqlSqlConnection");
            while (rs.Read())
            {
                Db.exec("UPDATE ExerciseVariantLang SET ExerciseContent = " + (rs.IsDBNull(1) ? "NULL" : "'" + rs.GetString(1).Replace("'", "''") + "'") + " WHERE ExerciseVariantID = " + rs.GetValue(0) + " AND Lang = " + rs.GetValue(2));
            }
            rs.Close();
            */
        }

        //void PersonalCalendar_SelectionChanged(object sender, EventArgs e)
        //{
        //    updateActs = true;
        //if (HttpContext.Current.Request.Form["a1"] == "3")
        //{
        //    a1.Value = "0";
        //    a2.Value = "0";
        //    a3.Value = "0";
        //}
        //}

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
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //PersonalCalendar.BoldDates = "";

            SqlDataReader rs;

            #region Stats
            /*
            int surveyID = (!IsPostBack && HttpContext.Current.Request.QueryString["SID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["SID"]) : 0);
			if(surveyID != 0 || HttpContext.Current.Request.QueryString["AK"] != null)
			{
				Page.RegisterStartupScript("GOTO","<script language=\"JavaScript\">location.href += '#stats';</script>");
			}
			string answerKey = "";
			statsHeader.Text = "";

			int cx = 0;
			DateTime showDate = DateTime.MinValue;
			int showPos = 0;
			string surveys = "0";
			string surveyKeys = "0";
			string uprus = "0";
			bool today = false;

			System.Collections.Hashtable toActivate = new System.Collections.Hashtable();

			string email = "";

			rs = Db.rs("SELECT " +
				"spru.ProjectRoundUnitID, " +								// 0
				"spru.Feedback, " +											// 1
				"upru.ProjectRoundUserID, " +								// 2
				"REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-',''), " +	// 3
				//"spru.Ext, " +											// 4
				"NULL, " +													// 4
				"upru.UserID, " +											// 5
				"u.Email " +												// 6
				"FROM [User] u " +
				"INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
				"INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = ABS(spru.SponsorID) " +
				"LEFT OUTER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
				"WHERE u.UserID = " + HttpContext.Current.Session["UserID"] + " ORDER BY spru.SortOrder");
			while(rs.Read())
			{
				if(!(rs.IsDBNull(4) || !rs.IsDBNull(5)))
				{
					SqlDataReader rs2 = Db.rs("SELECT " +
						"dbo.cf_unitSurveyID(ProjectRoundUnitID) " +
						"FROM ProjectRoundUnit " +
						"WHERE ProjectRoundUnitID = " + rs.GetInt32(4),"eFormSqlConnection");
					if(rs2.Read())
					{
						email = rs.GetString(6);
						toActivate.Add(rs2.GetInt32(0),rs.GetInt32(0));
					}
					rs2.Close();
				}
				else
				{
					surveyKeys += "," + rs.GetString(3);
					if(!rs.IsDBNull(2))
					{
						uprus += "," + rs.GetInt32(2);
					}
					SqlDataReader rs2 = Db.rs("SELECT " +
						"dbo.cf_unitSurveyID(ProjectRoundUnitID) " +
						"FROM ProjectRoundUnit " +
						"WHERE ProjectRoundUnitID = " + rs.GetInt32(0),"eFormSqlConnection");
					if(rs2.Read())
					{
						surveys += "," + rs2.GetInt32(0);
						if(!rs.IsDBNull(2))
						{
							SqlDataReader rs3 = Db.rs("SELECT TOP 1 " +
								"REPLACE(CONVERT(VARCHAR(255),AnswerKey),'-',''), " +
								"EndDT " +
								"FROM Answer " +
								"WHERE ProjectRoundUserID = " + rs.GetInt32(2) + " " +
								"AND EndDT IS NOT NULL " +
								"AND EndDT <= '" + PersonalCalendar.SelectedDate.AddDays(1).ToString("yyyy-MM-dd") + "' " +
								"ORDER BY AnswerID DESC","eFormSqlConnection");
							if(rs3.Read())
							{
								if(PersonalCalendar.SelectedDate.ToString("yyyy-MM-dd") == rs3.GetDateTime(1).ToString("yyyy-MM-dd"))
									today = true;

								if(surveyID == rs2.GetInt32(0) || surveyID == 0 && rs3.GetDateTime(1) > showDate)
								{
									answerKey = rs3.GetString(0);
									showPos = cx;
									showDate = rs3.GetDateTime(1);
								}
							}
							else if(rs2.GetInt32(0) == surveyID)
							{
								showPos = cx;
							}
							rs3.Close();
						}
						else if(rs2.GetInt32(0) == surveyID)
						{
							showPos = cx;
						}
					}
					rs2.Close();

                    statsHeader.Text += "<span onclick=\"location.href='calendar.aspx?SID=" + (surveys.Split(',').Length > 1 ? surveys.Split(',')[cx + 1] : surveys) + "&D=" + PersonalCalendar.SelectedDate.ToString("yyyy-MM-dd") + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "';\" class=\"statsHead" + (cx == 0 ? "First" : "") + "[" + cx + "]\">" + rs.GetString(1) + "</span>";
					cx++;
				}
			}
			rs.Close();

			surveyID = Convert.ToInt32((surveys.Split(',').Length > 1 ? surveys.Split(',')[showPos+1] : surveys));

			for(int i = 0; i < cx; i++)
				statsHeader.Text = statsHeader.Text.Replace("[" + i + "]",(showPos == i ? "A" : ""));

			stats.Text = "";
			if(answerKey != "")
			{
				if(surveyID == 9 || surveyID == 10 || surveyID == 13 || surveyID == 15)
				{
					//int age = 18;
					//rs = Db.rs("SELECT ValueInt FROM UserBQ WHERE BQID = 1 AND UserID = " + HttpContext.Current.Session["UserID"]);
					//if(rs.Read() && !rs.IsDBNull(0))
					//{
					//	age = DateTime.Now.Year - rs.GetInt32(0);
					//}
					//rs.Close();
					int age = 18, ageQ = 0, ageO = 0;
					switch(surveyID)
					{
						case 9: ageQ = 151; ageO = 33; break;
						case 10: ageQ = 201; ageO = 43; break;
						case 13: ageQ = 291; ageO = 43; break;
						case 15: ageQ = 532; ageO = 127; break;
					}
					rs = Db.rs("SELECT " +
						"av.ValueDecimal " +
						"FROM Answer a " +
						"INNER JOIN AnswerValue av ON av.AnswerID = a.AnswerID " +
						"WHERE REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-','') = '" + answerKey + "' AND av.QuestionID = " + ageQ + " AND av.OptionID = " + ageO,"eFormSqlConnection");
					if(rs.Read() && !rs.IsDBNull(0))
					{
						age = Convert.ToInt32(Math.Round(rs.GetDecimal(0),0));
					}
					rs.Close();

					stats.Text += "<img src=\"reportImageStatic.aspx?SID=" + surveyID + "&T=" + (today ? 1 : 0) + "&AK=" + answerKey + "\"/><br/>";
				}
			}
			else
			{
				stats.Text += "<BR><BR><span style=\"width:580px;text-align:center;\">Ingen statistik" + (uprus == "0" ? ". <A STYLE=\"text-decoration:underline;\" HREF=\"submit.aspx?SK=" + surveyKeys.Split(',')[showPos+1] + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Till enkäten.</A>" : "") + "</span><BR><BR><BR>";
            }
            rs = Db.rs("SELECT DISTINCT(EndDT) " +
				"FROM Answer " +
				"WHERE ProjectRoundUserID IN (" + uprus + ") " +
				"AND EndDT IS NOT NULL","eFormSqlConnection");
			while(rs.Read())
			{
				PersonalCalendar.BoldDates += rs.GetDateTime(0).ToString("yyyy-MM-dd");
			}
			rs.Close();
            */
            #endregion

            //string script = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //if (!IsPostBack)
            //{
            //a1.Value = "0";
            //a2.Value = "0";
            //a3.Value = "0";

            //switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            //{
            //    case 1:
            //        actHeader.Text += "<span onclick=\"c(0);\" id=\"act0\" class=\"actHeadFirstA\">Aktivitet/mätning</span>";
            //        actHeader.Text += "<span onclick=\"c(1);\" id=\"act1\" class=\"actHead\">Kategori</span>";
            //        actHeader.Text += "<span onclick=\"c(2);\" id=\"act2\" class=\"actHead\">Registrera värden</span>";
            //        actHeader.Text += "<span onclick=\"c(3);\" id=\"act3\" class=\"actHead\">Diagram</span>"; 
            //        break;
            //    case 2:
            //        actHeader.Text += "<span onclick=\"c(0);\" id=\"act0\" class=\"actHeadFirstA\">Activity/measurement</span>";
            //        actHeader.Text += "<span onclick=\"c(1);\" id=\"act1\" class=\"actHead\">Category</span>";
            //        actHeader.Text += "<span onclick=\"c(2);\" id=\"act2\" class=\"actHead\">Register values</span>";
            //        actHeader.Text += "<span onclick=\"c(3);\" id=\"act3\" class=\"actHead\">Diagram</span>";
            //        break;
            //}

            sb.Append("<li><label>");
            //actInnerBoxTop.InnerHtml += "<TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">" +
            //    "<TR><TD width=\"190\">";
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1:
                    sb.Append("Klockslag");
                    break;
                case 2:
                    sb.Append("Time");
                    break;
            }
            //Längd (cm)<input type="text" style="width: 113px; float: right"></label></li>
            //actInnerBoxTop.InnerHtml += "&nbsp;</TD><TD><SELECT CLASS=\"txt\" NAME=\"hour\">";
            sb.Append("</label><div><select name=\"hour\">");
            for (int i = 0; i <= 23; i++)
            {
                sb.Append("<OPTION VALUE=\"" + i + "\"" + (DateTime.Now.Hour == i ? " SELECTED" : "") + ">" + i.ToString().PadLeft(2, '0') + "</OPTION>");
            }
            sb.Append("</select><select name=\"minute\">");
            for (int i = 0; i <= 55; i += 5)
            {
                sb.Append("<OPTION VALUE=\"" + i + "\"" + (DateTime.Now.Minute - DateTime.Now.Minute % 5 == i ? " SELECTED" : "") + ">" + i.ToString().PadLeft(2, '0') + "</OPTION>");
            }
            sb.Append("</select></div></li>");
            //}
            //else
            //{
            //    script += "" +
            //        "setDDL('hour'," + Convert.ToInt32(HttpContext.Current.Request.Form["hour"]) + ");" +
            //        "setDDL('minute'," + Convert.ToInt32(HttpContext.Current.Request.Form["minute"]) / 5 + ");";
            //}
            //script += "" +
            //    "act" + (a1.Value == "3" && UMIDs != "" ? "G('" + UMIDs + "'" : "S(document.forms[0].a1.value") + ",document.forms[0].a2.value,document.forms[0].a3.value);";
            //ClientScript.RegisterStartupScript(this.GetType(),"ACT", "<script type=\"text/JavaScript\">" + script + "</script>");

            //int x2 = 0;
            //HtmlGenericControl g1 = ((HtmlGenericControl)actBox.FindControl("act0_0_0"));
            //if (!IsPostBack)
            //{
            //    g1.InnerHtml += "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td>";
            //}

            System.Collections.Specialized.ListDictionary ld = new System.Collections.Specialized.ListDictionary();
            System.Collections.ArrayList al = new ArrayList();

            rs = Db.rs("SELECT " +
                "mt.MeasureTypeID, " +       // 0
                "ISNULL(mtl.MeasureType,mt.MeasureType) " +
                "FROM MeasureType mt " +
                "LEFT OUTER JOIN MeasureTypeLang mtl ON mt.MeasureTypeID = mtl.MeasureTypeID AND mtl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                "WHERE Active = 1 " +
                "ORDER BY mt.SortOrder");
            while (rs.Read())
            {
                //if (!IsPostBack)
                //{
                //    if (x2 > 0)
                //    {
                //        g1.InnerHtml += (x2 % 2 == 1 ? "</td><td><img src=\"img/null.gif\" width=\"20\" height=\"0\"/></td><td>" : "</td></tr><tr><td colspan=\"3\"><td><img src=\"img/null.gif\" width=\"1\" height=\"5\"/></td></tr><tr><td>");
                //    }
                //    g1.InnerHtml += "<A HREF=\"JavaScript:actS(1," + x2 + ",0);\" class=\"lnk\">" + rs.GetString(1) + " &raquo;</A>";
                //}

                //int x3 = 0;
                //HtmlGenericControl g2 = new HtmlGenericControl("DIV");
                //g2.ID = "act1_" + x2 + "_0";
                //actBox.Controls.Add(g2);
                //if (!IsPostBack)
                //{
                //    g2.Attributes["style"] += "display:none;";
                //    g2.Attributes["class"] += "actInnerBox";
                //    g2.InnerHtml += "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td>";
                //}

                SqlDataReader rs2 = Db.rs("SELECT " +
                "mc.MeasureCategoryID, " +       // 0
                "ISNULL(mcl.MeasureCategory,mc.MeasureCategory), " +
                "NULL, " +
                "mc.SortOrder AS SO " +
                "FROM MeasureCategory mc " +
                "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                "WHERE mc.SPRUID IS NULL AND mc.MeasureTypeID = " + rs.GetInt32(0) + " " +
                "UNION ALL " +
                "SELECT " +
                "mc.MeasureCategoryID, " +       // 0
                "ISNULL(mcl.MeasureCategory,mc.MeasureCategory), " +
                "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-',''), " +
                "mc.SortOrder AS SO " +
                "FROM MeasureCategory mc " +
                "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                "INNER JOIN SponsorProjectRoundUnit spru ON mc.SPRUID = spru.SponsorProjectRoundUnitID AND spru.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " " +
                "WHERE mc.MeasureTypeID = " + rs.GetInt32(0) + " " +
                "AND (spru.OnlyEveryDays IS NULL OR DATEADD(d,spru.OnlyEveryDays,dbo.cf_lastSubmission(spru.ProjectRoundUnitID," + HttpContext.Current.Session["UserID"] + ")) < GETDATE()) " +
                "ORDER BY SO");
                while (rs2.Read())
                {
                    //if (!IsPostBack)
                    //{
                    //if (x3 > 0)
                    //{
                    //    g2.InnerHtml += (x3 % 2 == 1 ? "</td><td><img src=\"img/null.gif\" width=\"20\" height=\"0\"/></td><td>" : "</td></tr><tr><td colspan=\"3\"><td><img src=\"img/null.gif\" width=\"1\" height=\"5\"/></td></tr><tr><td>");
                    //}
                    if (!rs2.IsDBNull(2))
                    {
                        al.Add("><A HREF=\"submit.aspx?SK=" + rs2.GetString(2) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">" + rs2.GetString(1) + "</A></li>");
                    }
                    else
                    {
                        al.Add("id=\"C" + rs2.GetInt32(0) + "\"><A HREF=\"javascript:;\">" + rs2.GetString(1) + "</A></li>");
                    }
                    //}

                    //HtmlGenericControl g3 = new HtmlGenericControl("DIV");
                    //g3.Attributes["style"] += "display:none;";
                    //g3.Attributes["class"] += "actInnerBoxBottom";
                    //g3.ID = "act2_" + x2 + "_" + x3;
                    //actBox.Controls.Add(g3);

                    //if (!IsPostBack)
                    //{
                    //    g3.InnerHtml += "<INPUT TYPE=\"hidden\" NAME=\"MT" + x2 + "MC" + x3 + "\" VALUE=\"" + rs2.GetInt32(0) + "\">";
                    //    g3.InnerHtml += "<TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">";
                    //}

                    SqlDataReader rs3 = Db.rs("SELECT " +
                    "m.MeasureID, " +
                    "ISNULL(ml.Measure,m.Measure), " +
                    "(SELECT COUNT(*) FROM MeasureComponent mc WHERE mc.MeasureID = m.MeasureID), " +
                    "m.MoreInfo " +
                    "FROM Measure m " +
                    "LEFT OUTER JOIN MeasureLang ml ON m.MeasureID = ml.MeasureID AND ml.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                    "WHERE m.MeasureCategoryID = " + rs2.GetInt32(0) + " " +
                    "ORDER BY m.MeasureCategoryID, m.SortOrder");
                    while (rs3.Read())
                    {
                        //if(!ld.Contains(rs3.GetInt32(0)))
                        //{
                        //    ld.Add(rs3.GetInt32(0),x2+","+x3);
                        //}
                        //if(!IsPostBack)
                        //{
                        //    g3.InnerHtml += "<TR><TD colspan=\"3\" class=\"sep\">&nbsp;</td></tr>";
                        //    g3.InnerHtml += "<TR>";
                        //    g3.InnerHtml += "<TD width=\"120\" valign=\"top\" rowspan=\"" + rs3.GetInt32(2) + "\">" + (!rs3.IsDBNull(3) && rs3.GetString(3) != "" && Convert.ToInt32(HttpContext.Current.Session["LID"]) == 1 ? "<div style=\"float:right;\"><A HREF=\"JavaScript:void(window.open('measureInfo.aspx?MID=" + rs3.GetInt32(0) + "','','width=300,height=200,scrollbars=1'));\" class=\"lnk\">Info &raquo;</A></div>" : "") + "" + rs3.GetString(1) + "&nbsp;</TD>";
                        //}

                        int x = 0;

                        SqlDataReader rs4 = Db.rs("SELECT " +
                        "mc.MeasureComponentID, " +
                        "ISNULL(mcl.MeasureComponent,mc.MeasureComponent), " +
                        "mc.Type, " +
                        "ISNULL(mcl.Unit,mc.Unit), " +
                        "mc.Inherit, " +
                        "mc.AutoScript, " +            // 5
                        "(SELECT COUNT(*) FROM MeasureComponentPart mcp WHERE mcp.MeasureComponentPart = mc.MeasureComponentID), " +
                        "mc.Decimals " +
                        "FROM MeasureComponent mc " +
                        "LEFT OUTER JOIN MeasureComponentLang mcl ON mc.MeasureComponentID = mcl.MeasureComponentID AND mcl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                        "WHERE mc.MeasureID = " + rs3.GetInt32(0) + " " +
                        "ORDER BY mc.SortOrder");
                        while (rs4.Read())
                        {
                            //if(!IsPostBack)
                            //{
                            sb.Append("<li id=\"MC" + rs2.GetInt32(0) + "_" + x + "\"><label>" + rs3.GetString(1));
                            if (rs3.GetInt32(2) > 1)
                            {
                                sb.Append("<div>" + rs4.GetString(1) + "</div>");
                            }
                            sb.Append("</label><div>");

                            string val = "";
                            if (rs4.GetInt32(4) == 1)
                            {
                                SqlDataReader rs5 = Db.rs("SELECT TOP 1 c.ValDec FROM UserMeasureComponent c INNER JOIN UserMeasure m ON c.UserMeasureID = m.UserMeasureID WHERE c.MeasureComponentID = " + rs4.GetInt32(0) + " AND m.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " AND m.DeletedDT IS NULL ORDER BY m.DT DESC");
                                if (rs5.Read())
                                {
                                    val += " VALUE=\"" + Math.Round(rs5.GetDecimal(0), rs4.GetInt32(7)) + "\"";
                                }
                                rs5.Close();
                            }
                            string auto = "";
                            if (rs4.GetInt32(6) != 0)
                            {
                                SqlDataReader rs5 = Db.rs("SELECT MeasureComponentID FROM MeasureComponentPart WHERE MeasureComponentPart = " + rs4.GetInt32(0));
                                while (rs5.Read())
                                {
                                    auto += "MCP" + rs5.GetInt32(0) + "();";
                                }
                                rs5.Close();
                            }
                            if (!rs4.IsDBNull(5))
                            {
                                auto += "MCP" + rs4.GetInt32(0) + "();";
                            }
                            //g3.InnerHtml += (x > 0 ? "<TR>" : "") + "<TD WIDTH=\"70\">";
                            //if (rs3.GetInt32(2) > 1)
                            //{
                            //    g3.InnerHtml += rs4.GetString(1) + "&nbsp;";
                            //}
                            //g3.InnerHtml += "</TD><TD>";
                            switch (rs4.GetInt32(2))
                            {
                                case 4: //g3.InnerHtml += "<INPUT" + val + (auto != "" ? " ONKEYUP=\"" + auto + "\"" : "") + " TYPE=\"text\" CLASS=\"txt\" STYLE=\"WIDTH:50px;\" NAME=\"M" + rs3.GetInt32(0) + "C" + rs4.GetInt32(0) + "\">&nbsp;" + rs4.GetString(3);
                                    sb.Append("<input" + val + (auto != "" ? " onkeyup=\"" + auto + "\"" : "") + " type=\"text\" style=\"width:50px;\" name=\"M" + rs3.GetInt32(0) + "C" + rs4.GetInt32(0) + "\" /> " + rs4.GetString(3));
                                    break;
                            }
                            //g3.InnerHtml += "</TD></TR>";
                            //}
                            if (!rs4.IsDBNull(5))
                            {
                                string scr = "";
                                SqlDataReader rs5 = Db.rs("SELECT p.MeasureComponentPart, c.MeasureID FROM MeasureComponentPart p INNER JOIN MeasureComponent c ON p.MeasureComponentPart = c.MeasureComponentID WHERE p.MeasureComponentID = " + rs4.GetInt32(0) + " ORDER BY p.SortOrder");
                                while (rs5.Read())
                                {
                                    scr += (scr != "" ? "," : "") + "'M" + rs5.GetInt32(1) + "C" + rs5.GetInt32(0) + "'";
                                }
                                rs5.Close();
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "MCP" + rs4.GetInt32(0), "<script language=\"JavaScript\">function MCPS" + rs4.GetInt32(0) + "(){" + rs4.GetString(5) + "}function MCP" + rs4.GetInt32(0) + "(){document.forms[0].M" + rs3.GetInt32(0) + "C" + rs4.GetInt32(0) + ".value=MCPS" + rs4.GetInt32(0) + "(" + scr + ");}</script>");
                            }
                            //if (!IsPostBack)
                            //{
                            sb.Append("</div></li>");
                            //}
                            x++;
                        }
                        rs4.Close();
                    }
                    rs3.Close();

                    //if(!IsPostBack)
                    //{
                    //    g3.InnerHtml += "</TABLE>";
                    //}

                    //x3++;
                }
                rs2.Close();

                //if (!IsPostBack)
                //{
                //    g2.InnerHtml += "</td></tr></table>";
                //}

                //x2++;
            }
            rs.Close();
            //if (!IsPostBack)
            //{
            for (int i = 0; i < al.Count; i++)
            {
                formlinks.Controls.Add(new LiteralControl("<li " + (i == al.Count - 1 ? "class=\"last\" " : "") + (string)al[i]));
            }
            subform.Controls.Add(new LiteralControl(sb.ToString()));

            //switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            //{
            //    case 1:
            //        g1.InnerHtml += "</td></tr></table><br/>Formulär om kost, fysisk aktivitet, sömn, självkänsla, depression, m.fl. kommer inom kort.";
            //        break;
            //    case 2:
            //        g1.InnerHtml += "</td></tr></table><br/>Forms about nutrition, physical activity, sleep, self-esteem, depression, etc. will be added shortly.";
            //        break;
            //}
            //}

            messagetext.Text = "";
            rs = Db.rs("SELECT DiaryNote, Mood FROM Diary WHERE DeletedDT IS NULL AND DiaryDate = '" + dt.ToString("yyyy-MM-dd") + "' AND UserID = " + HttpContext.Current.Session["UserID"]);
            if (rs.Read())
            {
                messagetext.Text = rs.GetString(0);
                mood.Controls.Add(new LiteralControl(renderMood((rs.IsDBNull(1) ? 0 : rs.GetInt32(1)))));
            }
            else
            {
                mood.Controls.Add(new LiteralControl(renderMood(0)));
            }
            rs.Close();

            //rs = Db.rs("SELECT " +
            //    "DiaryDate " +
            //    "FROM Diary " +
            //    "WHERE DeletedDT IS NULL " +
            //    "AND UserID = " + HttpContext.Current.Session["UserID"] + " " +
            //    "UNION ALL " +
            //    "SELECT " +
            //    "DISTINCT dbo.cf_yearMonthDay(um.DT) " +
            //    "FROM UserMeasure um " +
            //    "WHERE um.UserID = " + HttpContext.Current.Session["UserID"] + " " +
            //    "UNION ALL " +
            //    "SELECT " +
            //    "DISTINCT dbo.cf_yearMonthDay(es.DateTime) " +
            //    "FROM ExerciseStats es " +
            //    "WHERE es.UserID = " + HttpContext.Current.Session["UserID"]);
            //while (rs.Read())
            //{
            //    PersonalCalendar.BoldDates += rs.GetDateTime(0).ToString("yyyy-MM-dd");
            //}
            //rs.Close();

            //if (updateActs)
            //{
            //actsBox.InnerHtml = "<TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">";
            TodaysActivities.Controls.Add(new LiteralControl(Db.fetchActs(dt.ToString("yyyy-MM-dd").Replace("'", ""), Convert.ToInt32(HttpContext.Current.Session["LID"]), Convert.ToInt32(HttpContext.Current.Session["UserID"]), true)));
            //}
        }

        private string renderMood(int today)
        {
            string s = "";
            s += "<li><label><input type=\"radio\"" + (today == 1 ? " checked" : "") + " value=\"1\" name=\"mood\" /><span>";
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: s += "Vet inte"; break;
                case 2: s += "Don't know"; break;
            }
            s += "</span></label></li>";

            s += "<li><label class=\"happy\"><input type=\"radio\"" + (today == 2 ? " checked" : "") + " value=\"2\" name=\"mood\" /><span>";
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: s += "Glad"; break;
                case 2: s += "Happy"; break;
            }
            s += "</span></label></li>";

            s += "<li><label class=\"neutral\"><input type=\"radio\"" + (today == 3 ? " checked" : "") + " value=\"3\" name=\"mood\" /><span>";
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: s += "Neutral"; break;
                case 2: s += "Neutral"; break;
            }
            s += "</span></label></li>";

            s += "<li><label class=\"unhappy\"><input type=\"radio\"" + (today == 4 ? " checked" : "") + " value=\"4\" name=\"mood\" /><span>";
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: s += "Missnöjd"; break;
                case 2: s += "Unhappy"; break;
            }
            s += "</span></label></li>";

            return s;
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

        private void submit_Click(object sender, EventArgs e)
        {
            bool oldNoteIdentical = false;
            SqlDataReader rs = Db.rs("SELECT " +
                "DiaryID, " +
                "DiaryNote, " +
                "Mood " +
                "FROM Diary " +
                "WHERE DeletedDT IS NULL " +
                "AND DiaryDate = '" + dt.ToString("yyyy-MM-dd") + "' " +
                "AND UserID = " + HttpContext.Current.Session["UserID"]);
            if (rs.Read())
            {
                if (rs.GetString(1) != messagetext.Text || (rs.IsDBNull(2) ? 0 : rs.GetInt32(2)) != (HttpContext.Current.Request.Form["mood"] != null && HttpContext.Current.Request.Form["mood"].ToString() != "" ? Convert.ToInt32(HttpContext.Current.Request.Form["mood"].ToString()) : 0))
                {
                    Db.exec("UPDATE Diary SET DeletedDT = GETDATE() WHERE DiaryID = " + rs.GetInt32(0));
                }
                else
                {
                    oldNoteIdentical = true;
                }
            }
            rs.Close();
            if ((messagetext.Text != "" || HttpContext.Current.Request.Form["mood"] != null && HttpContext.Current.Request.Form["mood"].ToString() != "") && !oldNoteIdentical)
            {
                Db.exec("INSERT INTO Diary (DiaryNote, DiaryDate, UserID, Mood) VALUES ('" + messagetext.Text.Replace("'", "''") + "','" + dt.ToString("yyyy-MM-dd") + "'," + HttpContext.Current.Session["UserID"] + "," + (HttpContext.Current.Request.Form["mood"] != null && HttpContext.Current.Request.Form["mood"].ToString() != "" ? Convert.ToInt32(HttpContext.Current.Request.Form["mood"].ToString()) : 0) + ")");
            }

            if (HttpContext.Current.Request.Form["MCID"] != null && HttpContext.Current.Request.Form["MCID"].ToString() != "0")
            {
                int measureID = 0, userMeasureID = 0;
                rs = Db.rs("SELECT " +
                    "m.MeasureID " +
                    "FROM Measure m " +
                    "WHERE m.MeasureCategoryID = " + Convert.ToInt32(HttpContext.Current.Request.Form["MCID"].ToString()));
                while (rs.Read())
                {
                    bool allReqFilledIn = true;
                    #region Check if all req are filled in
                    SqlDataReader rs2 = Db.rs("SELECT " +
                        "mc.MeasureComponentID, " +
                        "mc.Type " +
                        "FROM MeasureComponent mc " +
                        "WHERE mc.Required = 1 " +
                        "AND mc.MeasureID = " + rs.GetInt32(0));
                    while (rs2.Read())
                    {
                        if (HttpContext.Current.Request.Form["M" + rs.GetInt32(0) + "C" + rs2.GetInt32(0)] == null || HttpContext.Current.Request.Form["M" + rs.GetInt32(0) + "C" + rs2.GetInt32(0)].ToString() == "")
                        {
                            #region Not found
                            allReqFilledIn = false;
                            #endregion
                        }
                        else
                        {
                            #region Check if valid
                            switch (rs2.GetInt32(1))
                            {
                                case 4:
                                    try
                                    {
                                        Convert.ToDecimal(HttpContext.Current.Request.Form["M" + rs.GetInt32(0) + "C" + rs2.GetInt32(0)].ToString().Replace("'", "").Replace(".", ","));
                                    }
                                    catch (Exception)
                                    {
                                        allReqFilledIn = false;
                                    }
                                    break;
                            }
                            #endregion
                        }
                    }
                    rs2.Close();
                    #endregion

                    if (allReqFilledIn)
                    {
                        if (measureID != rs.GetInt32(0))
                        {
                            #region Create new UserMeasure
                            Db.exec("INSERT INTO UserMeasure (" +
                                "UserID, " +
                                "CreatedDT, " +
                                "DT," +
                                "UserProfileID" +
                                ") VALUES (" +
                                "" + HttpContext.Current.Session["UserID"] + "," +
                                "GETDATE()," +
                                "'" + dt.ToString("yyyy-MM-dd") + " " + Convert.ToInt32(HttpContext.Current.Request.Form["hour"]) + ":" + Convert.ToInt32(HttpContext.Current.Request.Form["minute"]) + "'," +
                                "" + Convert.ToInt32(HttpContext.Current.Session["UserProfileID"]) + "" +
                                ")");
                            #endregion
                            #region Fetch new UserMeasure
                            rs2 = Db.rs("SELECT TOP 1 " +
                                "UserMeasureID " +
                                "FROM UserMeasure " +
                                "WHERE UserID = " + HttpContext.Current.Session["UserID"] + " " +
                                "ORDER BY UserMeasureID DESC");
                            if (rs2.Read())
                            {
                                userMeasureID = rs2.GetInt32(0);
                            }
                            rs2.Close();
                            #endregion

                            UMIDs += (UMIDs != "" ? ":" : "") + userMeasureID;
                        }

                        rs2 = Db.rs("SELECT " +
                        "mc.MeasureComponentID, " +
                        "mc.Type " +
                        "FROM MeasureComponent mc " +
                        "WHERE mc.MeasureID = " + rs.GetInt32(0));
                        while (rs2.Read())
                        {
                            if (HttpContext.Current.Request.Form["M" + rs.GetInt32(0) + "C" + rs2.GetInt32(0)] != null && HttpContext.Current.Request.Form["M" + rs.GetInt32(0) + "C" + rs2.GetInt32(0)].ToString() != "")
                            {
                                bool valid = false;
                                #region Check if valid
                                switch (rs2.GetInt32(1))
                                {
                                    case 4:
                                        try
                                        {
                                            Convert.ToDecimal(HttpContext.Current.Request.Form["M" + rs.GetInt32(0) + "C" + rs2.GetInt32(0)].ToString().Replace("'", "").Replace(".", ","));
                                            valid = true;
                                        }
                                        catch (Exception) { }
                                        break;
                                }
                                #endregion

                                if (valid)
                                {
                                    updateActs = true;
                                    a1.Value = "3";
                                    #region Insert value
                                    switch (rs2.GetInt32(1))
                                    {
                                        case 4: Db.exec("INSERT INTO UserMeasureComponent (UserMeasureID, MeasureComponentID, ValDec) VALUES (" + userMeasureID + "," + rs2.GetInt32(0) + "," + HttpContext.Current.Request.Form["M" + rs.GetInt32(0) + "C" + rs2.GetInt32(0)].ToString().Replace("'", "").Replace(",", ".") + ")"); break;
                                    }
                                    #endregion
                                }
                            }
                        }
                        rs2.Close();
                    }
                    measureID = rs.GetInt32(0);
                }
                rs.Close();
            }
        }
    }
}