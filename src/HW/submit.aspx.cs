//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_submit_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'submit.aspx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace healthWatch
{
    public partial class submit : System.Web.UI.Page
    {
        int cCX = 0;
        int surveyID = 0;
        int langID = 0;
        int projectRoundID = 0;
        int projectRoundUnitID = 0;
        int projectRoundUserID = 0;
        int answerID = 0;
        bool finished = false;
        bool newAnswer = true;
        string answerKey = "";
        bool first = true;
        string nextSK = "";

        System.Collections.ArrayList missingValues = new System.Collections.ArrayList();
        System.Collections.Hashtable oldValues = new System.Collections.Hashtable();

        int sessionID = 0;

        string surveyIntroX = "";

        public static int createSurveyUser(int untID, string eml)
        {
            int usrID = 0;

            SqlDataReader rs = Db.rs("SELECT ProjectRoundID FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + untID, "eFormSqlConnection");
            if (rs.Read())
            {
                Db.exec("INSERT INTO ProjectRoundUser (ProjectRoundID,ProjectRoundUnitID,Email) VALUES (" + rs.GetInt32(0) + "," + untID + ",'" + eml.Replace("'", "") + "')", "eFormSqlConnection");
                rs.Close();
                rs = Db.rs("SELECT ProjectRoundUserID FROM [ProjectRoundUser] WHERE ProjectRoundUnitID=" + untID + " AND Email = '" + eml.Replace("'", "") + "' ORDER BY ProjectRoundUserID DESC", "eFormSqlConnection");
                if (rs.Read())
                {
                    usrID = rs.GetInt32(0);
                    Db.exec("INSERT INTO UserProjectRoundUser (UserID, ProjectRoundUnitID, ProjectRoundUserID) VALUES (" + HttpContext.Current.Session["UserID"] + "," + untID + "," + usrID + ")");
                }
            }
            rs.Close();

            return usrID;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (HttpContext.Current.Session["UserID"] == null)
            {
                HttpContext.Current.Response.Redirect("home.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }

            HttpContext.Current.Response.ExpiresAbsolute = DateTime.UtcNow.Subtract(new TimeSpan(1, 0, 0, 0));
            HttpContext.Current.Response.Expires = -1;
            HttpContext.Current.Response.CacheControl = "no-cache";

            HttpContext.Current.Session.Timeout = 120;

            SqlDataReader rs;

            string sk = (HttpContext.Current.Request.QueryString["SK"] == null ? "" : HttpContext.Current.Request.QueryString["SK"].ToString());

            if (sk == "")
            {
                #region Fetch first SK for sponsor and redirect back here with SK
                rs = Db.rs("SELECT TOP 1 " +
                    "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') " +
                    "FROM SponsorProjectRoundUnit spru " +
                    "WHERE spru.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " " +
                    "AND (spru.OnlyEveryDays IS NULL OR DATEADD(d,spru.OnlyEveryDays,dbo.cf_lastSubmission(spru.ProjectRoundUnitID," + HttpContext.Current.Session["UserID"] + ")) < GETDATE()) " +
                    "ORDER BY spru.SortOrder");
                if (rs.Read())
                {
                    sk = rs.GetString(0);
                }
                rs.Close();
                if (sk != "")
                {
                    HttpContext.Current.Response.Redirect("submit.aspx?SK=" + sk + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
                }
                else
                {
                    HttpContext.Current.Response.Redirect("calendar.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
                }
                #endregion
            }

            int formCount = 0; bool found = false, goNext = false;
            string sql = "SELECT " +
                "spru.ProjectRoundUnitID, " +
                "upru.ProjectRoundUserID, " +
                "u.Email, " +
                "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-',''), " +
                "ISNULL(sprul.Nav,spru.Nav) AS Nav, " +
                "spru.GoToStatistics " +
                "FROM [User] u " +
                "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
                "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
                //"FROM SponsorProjectRoundUnit spru " +
                //"INNER JOIN [User] u ON u.UserID = " + HttpContext.Current.Session["UserID"] + " " +
                "LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID AND sprul.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                "LEFT OUTER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
                //"WHERE REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') = '" + sk.Replace("'","") + "'");
                "WHERE u.UserID = " + HttpContext.Current.Session["UserID"] + " " +
                "AND (spru.OnlyEveryDays IS NULL OR DATEADD(d,spru.OnlyEveryDays,dbo.cf_lastSubmission(spru.ProjectRoundUnitID,u.UserID)) < GETDATE()) " +
                "ORDER BY spru.SortOrder";
            //HttpContext.Current.Response.Write(sql);
            rs = Db.rs(sql);
            while (rs.Read())
            {
                // include some sort of AfterSaveGoToSurvey <int> here

                formCount++;

                if (sk == rs.GetString(3))
                {
                    found = true;
                    goNext = rs.IsDBNull(5);
                    projectRoundUnitID = rs.GetInt32(0);
                    if (!rs.IsDBNull(1))
                    {
                        projectRoundUserID = rs.GetInt32(1);
                    }
                    else
                    {
                        projectRoundUserID = createSurveyUser(projectRoundUnitID, rs.GetString(2));
                    }

                    if (!IsPostBack)
                    {
                        SurveyName.Text = rs.GetString(4);
                    }
                }
                else if (found && nextSK == "" && goNext)
                {
                    nextSK = rs.GetString(3);
                }
                if (formCount < 5)
                {
                    shortCutForms.InnerHtml += "<a" + (sk == rs.GetString(3) ? " class=\"active\"" : "") + " href=\"submit.aspx?SK=" + rs.GetString(3) + "\"><span>" + rs.GetString(4) + "</span></a>";
                }
            }
            rs.Close();
            formchoser.Visible = (formCount > 0);

            //if (!IsPostBack)
            //{
            //    //Forms.Text = "<div class=\"boxTitle\" style=\"width:127px;\">";
            //    //switch (langID)
            //    //{
            //    //    case 1: { Forms.Text += "Formulär"; break; }
            //    //    case 2: { Forms.Text += "Forms"; break; }
            //    //}
            //    //Forms.Text += "</div><div class=\"box\" style=\"width:127px\">";
            //    rs = Db.rs("" +
            //        "SELECT " +
            //        "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-',''), " +
            //        "ISNULL(sprul.Nav,spru.Nav) AS Nav " +
            //        "FROM [User] u " +
            //        "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
            //        "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
            //        "LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID AND sprul.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
            //        //// use the below rows to only trigger extended surveys when trigger value is reached //// ABS(spru.SponsorID) above
            //        //"LEFT OUTER JOIN UserProjectRoundUser r ON u.UserID = r.UserID AND spru.ProjectRoundUnitID = r.ProjectRoundUnitID " +
            //        //"WHERE (spru.Ext IS NULL OR r.UserID IS NOT NULL) AND u.UserID = " + HttpContext.Current.Session["UserID"] + " ORDER BY spru.SortOrder");
            //        ////
            //        "WHERE u.UserID = " + HttpContext.Current.Session["UserID"] + " ORDER BY spru.SortOrder");
            //    while (rs.Read())
            //    {
            //        if (HttpContext.Current.Request.QueryString["SK"] == rs.GetString(0))
            //        {
            //            //Forms.Text += "<input type=\"radio\" name=\"FormSelection\" checked />" + rs.GetString(1) + "<br/>";
            //            SurveyName.Text = rs.GetString(1);
            //        }
            //        else
            //        {
            //            //Forms.Text += "<input type=\"radio\" name=\"FormSelection\" onclick=\"location.href='submit.aspx?SK=" + rs.GetString(0) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "';\">" + rs.GetString(1) + "<br/>";
            //        }
            //    }
            //    rs.Close();
            //    //Forms.Text += "</div>";
            //}

            if (projectRoundUserID == 0)
            {
                HttpContext.Current.Response.Redirect("calendar.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }

            #region Fetch SurveyID, LangID
            rs = Db.rs("SELECT " +
                "dbo.cf_unitSurveyID(u.ProjectRoundUnitID), " +
                "dbo.cf_unitLangID(u.ProjectRoundUnitID), " +
                "pru.ProjectRoundID, " +
                "(SELECT COUNT(*) FROM Answer a WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = u.ProjectRoundUserID) " +
                "FROM ProjectRoundUser u " +
                "INNER JOIN ProjectRoundUnit pru ON u.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
                "WHERE u.ProjectRoundUserID = " + projectRoundUserID, "eFormSqlConnection");
            if (rs.Read())
            {
                surveyID = rs.GetInt32(0);
                //langID = rs.GetInt32(1);
                langID = Convert.ToInt32(HttpContext.Current.Session["LID"]);
                projectRoundID = rs.GetInt32(2);
                first = false;// (rs.GetInt32(3) == 0);
            }
            rs.Close();
            #endregion

            SurveyIntro.Text = "";

            #region Fetch AnswerID and check for Finished
            rs = Db.rs("SELECT TOP 1 AnswerID, EndDT, REPLACE(CONVERT(VARCHAR(255),AnswerKey),'-','') FROM Answer WHERE ProjectRoundUserID = " + projectRoundUserID + " ORDER BY AnswerID DESC", "eFormSqlConnection");
            if (rs.Read())
            {
                answerID = rs.GetInt32(0);
                answerKey = rs.GetString(2);
                if (!rs.IsDBNull(1))
                {
                    if (!IsPostBack)
                    {
                        answerID = 0;
                        answerKey = "";
                    }
                    else
                    {
                        finished = true;
                    }
                }
            }
            rs.Close();
            #endregion

            if (projectRoundID != 0 && projectRoundUnitID != 0)
            {
                if (answerID == 0)
                {
                    #region Create new AnswerID
                    Db.exec("INSERT INTO Answer (ProjectRoundID, ProjectRoundUnitID, ProjectRoundUserID, ExtendedFirst) VALUES (" + projectRoundID + "," + projectRoundUnitID + "," + projectRoundUserID + "," + (first ? "1" : "NULL") + ")", "eFormSqlConnection");
                    rs = Db.rs("SELECT TOP 1 AnswerID, REPLACE(CONVERT(VARCHAR(255),AnswerKey),'-','') FROM Answer WHERE EndDT IS NULL AND ProjectRoundUserID = " + projectRoundUserID + " ORDER BY AnswerID DESC", "eFormSqlConnection");
                    if (rs.Read())
                    {
                        answerID = rs.GetInt32(0);
                        answerKey = rs.GetString(1);
                    }
                    rs.Close();
                    #endregion
                }
                else
                {
                    #region Fetch response and store/update
                    newAnswer = false;

                    rs = Db.rs("SELECT " +
                        "sq.QuestionID, " +	// 0
                        "qo.OptionID, " +	// 1
                        "o.OptionType, " +	// 2
                        "sqo.Forced " +		// 3
                        "FROM SurveyQuestion sq " +
                        "INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " +
                        "INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " +
                        "INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
                        "WHERE qo.Hide IS NULL AND sq.SurveyID = " + surveyID + " " +
                        (!first ? "AND (sq.ExtendedFirst IS NULL OR sq.ExtendedFirst = 0) " : "") +
                        "ORDER BY sq.SortOrder", "eFormSqlConnection");
                    while (rs.Read())
                    {
                        string val = "", newVal = "";

                        if (IsPostBack)
                        {
                            newVal = (HttpContext.Current.Request.Form["Q" + rs.GetInt32(0) + "O" + rs.GetInt32(1)] != null ? HttpContext.Current.Request.Form["Q" + rs.GetInt32(0) + "O" + rs.GetInt32(1)].ToString() : "");

                            switch (rs.GetInt32(2))
                            {
                                case 9:
                                    {
                                        //if(newVal != "")
                                        //{
                                        //    newVal = (HttpContext.Current.Request.Form["VASvalue" + newVal] != null ? HttpContext.Current.Request.Form["VASvalue" + newVal].ToString() : "");
                                        //}
                                        newVal = newVal.Replace("NULL", "");
                                    }
                                    break;
                            }
                        }

                        bool hasOldVal = false;
                        SqlDataReader rs2 = Db.rs("SELECT ValueInt, ValueText, ValueDecimal FROM AnswerValue WHERE AnswerID = " + answerID + " AND QuestionID = " + rs.GetInt32(0) + " AND OptionID = " + rs.GetInt32(1) + " AND DeletedSessionID IS NULL", "eFormSqlConnection");
                        if (rs2.Read())
                        {
                            hasOldVal = true;

                            #region Fetch previously stored value
                            switch (rs.GetInt32(2))
                            {
                                case 1:
                                    {
                                        if (!rs.IsDBNull(0))
                                        {
                                            val = rs2.GetInt32(0).ToString();
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        if (!rs.IsDBNull(1))
                                        {
                                            val = rs2.GetString(1);
                                        }
                                        break;
                                    }
                                case 3:
                                    {
                                        goto case 1;
                                    }
                                case 4:
                                    {
                                        if (!rs.IsDBNull(2))
                                        {
                                            val = rs2.GetDecimal(2).ToString();
                                        }
                                        break;
                                    }
                                case 9:
                                    {
                                        goto case 1;
                                    }
                            }
                            #endregion
                        }
                        rs2.Close();

                        if (IsPostBack && newVal != "" || !IsPostBack && hasOldVal)
                        {
                            cCX++;
                        }

                        if (IsPostBack)
                        {
                            if (newVal != val)
                            {
                                if (hasOldVal)
                                {
                                    Db.exec("UPDATE AnswerValue SET DeletedSessionID = " + sessionID + " WHERE AnswerID = " + answerID + " AND QuestionID = " + rs.GetInt32(0) + " AND OptionID = " + rs.GetInt32(1) + " AND DeletedSessionID IS NULL", "eFormSqlConnection");
                                }

                                if (newVal != "")
                                {
                                    #region Save new value
                                    switch (rs.GetInt32(2))
                                    {
                                        case 1:
                                            {
                                                Db.exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueInt,CreatedSessionID) VALUES (" + answerID + "," + rs.GetInt32(0) + "," + rs.GetInt32(1) + "," + newVal.Replace("'", "") + "," + sessionID + ")", "eFormSqlConnection");
                                                break;
                                            }
                                        case 2:
                                            {
                                                Db.exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueText,CreatedSessionID) VALUES (" + answerID + "," + rs.GetInt32(0) + "," + rs.GetInt32(1) + ",'" + newVal.Replace("'", "''") + "'," + sessionID + ")", "eFormSqlConnection");
                                                break;
                                            }
                                        case 3:
                                            {
                                                goto case 1;
                                            }
                                        case 4:
                                            {
                                                try
                                                {
                                                    decimal newValIns = Convert.ToDecimal(newVal);
                                                    Db.exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueDecimal,CreatedSessionID) VALUES (" + answerID + "," + rs.GetInt32(0) + "," + rs.GetInt32(1) + "," + newValIns.ToString().Replace(",", ".") + "," + sessionID + ")", "eFormSqlConnection");
                                                }
                                                catch (Exception) { }
                                                break;
                                            }
                                        case 9:
                                            {
                                                goto case 1;
                                            }
                                    }
                                    #endregion
                                }
                            }

                            val = newVal;
                        }


                        oldValues.Add("Q" + rs.GetInt32(0) + "O" + rs.GetInt32(1), HttpUtility.HtmlEncode(val));
                        if (val == "" && rs.GetInt32(3) == 1)
                        {
                            missingValues.Add("Q" + rs.GetInt32(0));
                        }
                    }
                    rs.Close();
                    #endregion
                }
            }

            switch (langID)
            {
                case 1: { submitBtn.Text = "Spara"; break; }
                case 2: { submitBtn.Text = "Save"; break; }
            }
            submitBtn.Click += new EventHandler(SubmitButton_Click);
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (missingValues.Count == 0)
            {
                Db.exec("UPDATE Answer SET EndDT = GETDATE() WHERE AnswerID = " + answerID, "eFormSqlConnection");
                Db.exec("INSERT INTO UserProjectRoundUserAnswer (ProjectRoundUserID, AnswerKey, UserProfileID, AnswerID) VALUES (" + projectRoundUserID + ",'" + answerKey + "'," + Convert.ToInt32(HttpContext.Current.Session["UserProfileID"]) + "," + answerID + ")");
                finished = true;
            }
            else
            {
                SurveyIntro.Text = "<SPAN STYLE=\"color:#CC0000;\">Du har lämnat " + missingValues.Count + " " + (missingValues.Count > 1 ? "obligatoriska frågor obesvarade. Dessa är markerade" : "obligatorisk fråga obesvarad. Denna är markerad") + " i rött nedan.</SPAN>";
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (surveyID == 0 || langID == 0 || finished)
            {
                if (nextSK != "")
                {
                    HttpContext.Current.Response.Redirect("submit.aspx?SK=" + nextSK + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
                }
                else
                {
                    HttpContext.Current.Response.Redirect("statistics.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&AK=" + answerKey, true);
                }
            }

            int totalCX = 0;
            //int vasCX = 0;
            //int mandatoryCX = 0;
            //string vasDict = "";
            //string globalDependency = "";

            SqlDataReader rs = Db.rs("SELECT " +
                "dbo.cf_unitSurveyIntro(" + projectRoundUnitID + "), " +
                "prl.SurveyName, " +
                "(" +
                    "SELECT " +
                    "COUNT(DISTINCT sq.SurveyQuestionID) " +
                    "FROM SurveyQuestion sq " +
                //"INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " +
                    "INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
                    "WHERE sq.NoCount IS NULL AND q.Box = 0 AND sq.SurveyID = " + surveyID + "" +
                    (!first ? " AND (sq.ExtendedFirst IS NULL OR sq.ExtendedFirst = 0)" : "") +
                ") FROM ProjectRound pr " +
                "LEFT OUTER JOIN ProjectRoundLang prl ON pr.ProjectRoundID = prl.ProjectRoundID AND prl.LangID = " + langID + " " +
                "WHERE pr.ProjectRoundID = " + projectRoundID, "eFormSqlConnection");
            if (rs.Read())
            {
                surveyIntroX = (rs.IsDBNull(0) || rs.GetString(0) == "" ? surveyIntroX : rs.GetString(0).Replace("\r\n", "<br/>").Replace("\n", "<br/>"));
                //surveyName = (rs.IsDBNull(1) ? surveyName : rs.GetString(1));
                totalCX = rs.GetInt32(2);
            }
            rs.Close();

            if (SurveyIntro.Text == "")
            {
                SurveyIntro.Text = surveyIntroX;
            }

            #region Render survey
            bool twoCol = false;
            string[] sbarr = new string[totalCX];
            int cx = 0, qCX = 0;//, oCX = 0, questionsInBox = 0, questionsHidInBox = 0;
            //bool firstInBox = false;
            //bool flipFlopBg = false;
            //bool hideIt = false;
            //bool previousHideIt = false;

            //string boxBuffer = "";

            rs = Db.rs("SELECT " +
                "sq.SurveyQuestionID, " +	// 0
                "sq.OptionsPlacement, " +	// 1
                "q.FontFamily, " +			// 2
                "ISNULL(sq.FontSize,q.FontSize), " +			// 3
                "q.FontDecoration, " +		// 4
                "q.FontColor, " +			// 5
                "q.Underlined, " +			// 6
                "ISNULL(sql.Question,ql.Question) AS Question, " +			// 7
                "q.QuestionID, " +			// 8
                "q.Box, " +					// 9
                "sq.NoCount, " +			// 10
                "sq.RestartCount, " +		// 11
                "(SELECT COUNT(*) FROM SurveyQuestionOption sqo WHERE sqo.OptionPlacement = 1 AND sqo.SurveyQuestionID = sq.SurveyQuestionID), " +	// 12
                "(SELECT COUNT(*) FROM SurveyQuestionOption sqo INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID WHERE qo.Hide = 1 AND sqo.SurveyQuestionID = sq.SurveyQuestionID), " +	// 13
                "sq.NoBreak, " +			// 14
                "sq.BreakAfterQuestion, " +	// 15
                "s.FlipFlopBg, " +			// 16
                "s.TwoColumns, " +          // 17
                "(SELECT COUNT(*) FROM SurveyQuestionOption sqo WHERE sqo.SurveyQuestionID = sq.SurveyQuestionID) " +	// 18
                "FROM Survey s " +
                "INNER JOIN SurveyQuestion sq ON s.SurveyID = sq.SurveyID " +
                "INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
                "INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + langID + " " +
                "LEFT OUTER JOIN SurveyQuestionLang sql ON sq.SurveyQuestionID = sql.SurveyQuestionID AND sql.LangID = ql.LangID " +
                "WHERE sq.NoCount IS NULL AND q.Box = 0 AND s.SurveyID = " + surveyID + " " +
                (!first ? "AND (sq.ExtendedFirst IS NULL OR sq.ExtendedFirst = 0) " : "") +
                "ORDER BY sq.SortOrder", "eFormSqlConnection");
            while (rs.Read())
            {
                twoCol = !rs.IsDBNull(17);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                cx++;

                if (rs.GetInt32(18) > 0)
                {
                    #region Option
                    SqlDataReader rs2 = Db.rs("SELECT " +
                        "qo.QuestionID, " +			// 0
                        "qo.OptionID, " +			// 1
                        "sqo.OptionPlacement, " +	// 2
                        "o.OptionType, " +			// 3
                        "o.Width, " +				// 4
                        "ISNULL(sqo.Height,o.Height), " +				// 5
                        "sqo.Forced, " +			// 6
                        "o.InnerWidth, " +			// 7
                        "qo.Hide, " +				// 8
                        "o.BgColor, " +				// 9
                        "(SELECT COUNT(*) FROM OptionComponents ocs WHERE ocs.OptionID = o.OptionID) " +    // 10
                        "FROM SurveyQuestionOption sqo " +
                        "INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " +
                        "INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
                        "WHERE sqo.SurveyQuestionID = " + rs.GetInt32(0) + " " +
                        "ORDER BY sqo.SortOrder", "eFormSqlConnection");
                    if (rs2.Read())
                    {
                        bool vas = (rs2.GetInt32(3) == 9);

                        sb.Append("<div class=\"question" + (!twoCol ? " questionOneCol" : " questionTwoCol") + (vas ? " questionVas" : "") + "\" id=\"Q" + rs.GetInt32(8) + "\"" + (!rs2.IsDBNull(5) && rs2.GetInt32(5) > 0 ? " style=\"height:" + rs2.GetInt32(5) + "px;\"" : "") + ">");
                        sb.Append("<h3 style=\"display:inline;float:left;\">");
                        if (!rs.IsDBNull(11))
                        {
                            // Reset count
                            qCX = 0;
                        }
                        if (rs.IsDBNull(10))
                        {
                            // Not no count
                            qCX++;
                            sb.Append("<span class=\"numeral\">" + qCX + "</span> ");
                        }
                        sb.Append("<span" + (!rs.IsDBNull(3) && rs.GetInt32(3) > 0 ? " style=\"font-size:" + rs.GetInt32(3) + "px;\"" : "") + ">");
                        sb.Append(rs.GetString(7).Replace("\r\n", "<br/>"));
                        sb.Append("</span></h3>");

                        do
                        {
                            switch (rs2.GetInt32(3))
                            {
                                case 1:
                                    {
                                        System.Text.StringBuilder header = new System.Text.StringBuilder();
                                        System.Text.StringBuilder input = new System.Text.StringBuilder();

                                        SqlDataReader rs3 = Db.rs("SELECT " +
                                            "ocs.OptionComponentID, " +
                                            "ocl.Text " +
                                            "FROM OptionComponents ocs " +
                                            "INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
                                            "INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + langID + " " +
                                            "WHERE ocs.OptionID = " + rs2.GetInt32(1) + " " +
                                            "ORDER BY ocs.SortOrder", "eFormSqlConnection");
                                        while (rs3.Read())
                                        {
                                            if (!rs2.IsDBNull(2) && rs2.GetInt32(2) == 1)
                                            {
                                                header.Append("<div style=\"float:left;text-align:center;width:" + rs2.GetInt32(4) + "px;\">" + rs3.GetString(1) + "</div>");
                                                input.Append("<div style=\"float:left;text-align:center;width:" + rs2.GetInt32(4) + "px;\">");
                                            }
                                            input.Append("<input type=\"radio\" value=\"" + rs3.GetInt32(0) + "\" name=\"Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1) + "\">");
                                            if (!rs2.IsDBNull(2) && rs2.GetInt32(2) == 1)
                                            {
                                                input.Append("</div>");
                                            }
                                            else
                                            {
                                                input.Append(rs3.GetString(1) + " ");
                                            }
                                            switch (rs2.GetInt32(2))
                                            {
                                                case 8: input.Append("<br/>"); break;
                                            }
                                        }
                                        rs3.Close();

                                        if (!rs.IsDBNull(1) && rs.GetInt32(1) == 2) { sb.Append("<div style=\"clear:both;\"></div>"); } // OptionsPlacement = Below

                                        if (!rs2.IsDBNull(2) && rs2.GetInt32(2) == 1)
                                        {
                                            sb.Append("<div style=\"float:left; margin-left:20px;\">" + header + "</div>");
                                            sb.Append("<div style=\"clear:both;\"></div>");
                                            sb.Append("<div style=\"float:left; margin-bottom:10px;margin-left:20px;\">" + input + "</div>");
                                        }
                                        else
                                        {
                                            sb.Append("<div style=\"float:left; margin-bottom:10px;margin-left:20px;\">" + input + "</div>");
                                        }
                                        break;
                                    }
                                case 9:
                                    {
                                        if (cx == 1)
                                        {
                                            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                                            {
                                                case 1: Help.InnerText = "Klicka på linjen under respektive fråga."; break;
                                                case 2: Help.InnerText = "Click the line below each question."; break;
                                            }
                                            Help.Visible = true;
                                        }
                                        sb.Append("<div class=\"questionTwoCol\">");
                                        sb.Append("<div id=\"Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1) + "\" class=\"slider lfloat\"></div>");
                                        sb.Append("<div class=\"hundrfix\"></div>");
                                        sb.Append(" ");
                                        sb.Append("<div class=\"empty\"></div>");
                                        sb.Append("<br />");
                                        sb.Append("<div class=\"labels\">");
                                        int dx = 0;
                                        SqlDataReader rs3 = Db.rs("SELECT " +
                                            "ocs.OptionComponentID, " +
                                            "ocl.Text " +
                                            "FROM OptionComponents ocs " +
                                            "INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
                                            "INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + langID + " " +
                                            "WHERE ocs.OptionID = " + rs2.GetInt32(1) + " " +
                                            "ORDER BY ocs.SortOrder", "eFormSqlConnection");
                                        while (rs3.Read())
                                        {
                                            if (dx == 1 && rs2.GetInt32(10) == 2)
                                            {
                                                sb.Append("<div class=\"label l25\">&nbsp;</div>");
                                                sb.Append("<div class=\"label l50\">&nbsp;</div>");
                                                sb.Append("<div class=\"label l75\">&nbsp;</div>");
                                            }
                                            sb.Append("<div class=\"label l" + (dx * (100 / (rs2.GetInt32(10) - 1))) + "\">" + rs3.GetString(1) + "</div>");
                                            dx++;
                                        }
                                        rs3.Close();
                                        sb.Append("</div>");
                                        sb.Append("</div>");
                                        sb.Append("<input type=\"hidden\" name=\"Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1) + "\" value=\"NULL\" />");

                                        break;
                                    }
                            }
                        }
                        while (rs2.Read());
                        sb.Append("</div><!-- end .question #" + cx + " -->");
                    }
                    else
                    {
                        sb.Append("<div class=\"question" + (!twoCol ? " questionOneCol" : " questionTwoCol") + "\" id=\"Q" + rs.GetInt32(8) + "\">");
                        sb.Append(rs.GetString(7).Replace("\r\n", "<br/>"));
                        sb.Append("</div><!-- end .question #" + cx + " -->");
                    }
                    rs2.Close();
                    #endregion
                }
                else
                {
                    sb.Append("<div class=\"question" + (!twoCol ? " questionOneCol" : " questionTwoCol") + "\">");
                    sb.Append("<h3 style=\"display:inline;float:left;\">");
                    sb.Append("<span" + (!rs.IsDBNull(3) && rs.GetInt32(3) > 0 ? " style=\"font-size:" + rs.GetInt32(3) + "px;\"" : "") + ">");
                    sb.Append(rs.GetString(7).Replace("\r\n", "<br/>"));
                    sb.Append("</span></h3>");
                    sb.Append("</div>");
                }
                sbarr[cx - 1] = sb.ToString();

                #region old
                /*
                if (!rs.IsDBNull(11) && rs.GetInt32(11) == 1)
				{
					cx = 0;
				}

				qCX++;

                if (qCX == 1 || rs.GetInt32(9) == 1)
				{
					flipFlopBg = false;

					if(qCX > 1)
					{
						boxBuffer += "</table>";
						boxBuffer += "</div>";
						
						if(questionsHidInBox > 1 && questionsInBox == questionsHidInBox)
						{
							boxBuffer += "<div id=\"footer\" style=\"display:none;\">&nbsp;</div>";
							boxBuffer = boxBuffer.Replace(" class=\"eform_area\""," style=\"display:none;\" class=\"eform_area\"").Replace(" class=\"eform_ques\""," style=\"display:none;\" class=\"eform_ques\"");
						}
						else
						{
							boxBuffer += "<div id=\"footer\">&nbsp;</div>";
						}
						Survey.Controls.Add(new LiteralControl(boxBuffer));

						questionsInBox = 0;
						questionsHidInBox = 0;
						boxBuffer = "";
					}
					boxBuffer += "<div id=\"Q" + rs.GetInt32(0) + "A\" class=\"eform_area\"><a href=\"JavaScript:showAll();\"><img align=\"right\" src=\"submitImages/null.gif\" width=\"1\" height=\"1\" border=\"0\"></a><p>" + rs.GetString(7) + "</p></div>";
					boxBuffer += "<div id=\"Q" + rs.GetInt32(0) + "Q\" class=\"eform_ques\">";
					boxBuffer += "<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">";

					firstInBox = true;
				}
				else
				{
					string bg = (flipFlopBg ? " BGCOLOR=\"#DEDEDE\"" : "");

					if(!firstInBox)
					{
						if(1==0 && rs.GetInt32(6) == 1)
						{
							boxBuffer += "<TR id=\"Q" + rs.GetInt32(0) + "S1\"" + (previousHideIt ? " style=\"display:none;\"" : "") + "><TD colspan=\"3\"" + bg + "><img src=\"submitImages/null.gif\" width=\"1\" height=\"10\"></td></tr>";
							boxBuffer += "<TR id=\"Q" + rs.GetInt32(0) + "S2\"" + (previousHideIt ? " style=\"display:none;\"" : "") + "><TD colspan=\"3\" bgcolor=\"#CECECE\"><img src=\"submitImages/null.gif\" width=\"1\" height=\"1\"></td></tr>";
							boxBuffer += "<TR id=\"Q" + rs.GetInt32(0) + "S3\"" + (previousHideIt ? " style=\"display:none;\"" : "") + "><TD colspan=\"3\"" + bg + "><img src=\"submitImages/null.gif\" width=\"1\" height=\"10\"></td></tr>";

							flipFlopBg = (!rs.IsDBNull(16) ? !flipFlopBg : false);
							bg = (flipFlopBg ? " BGCOLOR=\"#DEDEDE\"" : "");
						}
						if(rs.IsDBNull(14))
						{
							boxBuffer += "<TR id=\"Q" + rs.GetInt32(0) + "S1\"" + (previousHideIt ? " style=\"display:none;\"" : "") + "><TD colspan=\"3\"" + bg + "><img src=\"submitImages/null.gif\" width=\"1\" height=\"10\"></td></tr>";
						}
					}
					firstInBox = false;

					#region Fetch option/header

					string o = "";
					string h = "";
					int w = 0;

					bool showHeaderRow = (0 < rs.GetInt32(12));

					SqlDataReader rs2 = Db.rs("SELECT " + 
						"qo.QuestionID, " +			// 0
						"qo.OptionID, " +			// 1
						"sqo.OptionPlacement, " +	// 2
						"o.OptionType, " +			// 3
						"o.Width, " +				// 4
						"o.Height, " +				// 5
						"sqo.Forced, " +			// 6
						"o.InnerWidth, " +			// 7
						"qo.Hide, " +				// 8
						"o.BgColor " +				// 9
						"FROM SurveyQuestionOption sqo " + 
						"INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " + 
						"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " + 
						"WHERE sqo.SurveyQuestionID = " + rs.GetInt32(0) + " " + 
						"ORDER BY sqo.SortOrder","eFormSqlConnection");
					while(rs2.Read())
					{
						string dependency = "";

						SqlDataReader rs3 = Db.rs("SELECT " +
							"SurveyQuestionID, " +
							"OptionComponentID " +
							"FROM SurveyQuestionIf " +
							"WHERE QuestionID = " + rs2.GetInt32(0) + " " +
							"AND OptionID = " + rs2.GetInt32(1) + " " +
							"AND SurveyID = " + surveyID + " " +
							"ORDER BY SurveyQuestionID","eFormSqlConnection");
						if(rs3.Read())
						{
							int SQID = 0;
							int nextSQID = 0;
							string tmpDependency = "";
							do
							{
								nextSQID = rs3.GetInt32(0);

								if(SQID != 0 && SQID != nextSQID)
								{
									dependency += "vi('" + SQID + "',(" + tmpDependency + "));";
									tmpDependency = "";
								}
								else if(SQID == nextSQID)
								{
									tmpDependency += "||";
								}
								tmpDependency += "isVal(document.forms[0].Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1) + "," + rs3.GetInt32(1) + ")";

								SQID = rs3.GetInt32(0);
							}
							while(rs3.Read());

							dependency += "vi('" + SQID + "',(" + tmpDependency + "));";

							globalDependency += dependency;
						}
						rs3.Close();

						oCX++;

						if(!rs2.IsDBNull(6) && rs2.GetInt32(6) == 1)
						{
							mandatoryCX++;
						}

						o += "<TD " + (!rs2.IsDBNull(9) ? "BGCOLOR=\"#" + rs2.GetString(9) + "\" " : "") + "VALIGN=\"TOP\" ALIGN=\"RIGHT\"><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">";
						if(showHeaderRow)
						{
							h += "<TD " + (!rs2.IsDBNull(9) ? "BGCOLOR=\"#" + rs2.GetString(9) + "\" " : "") + "VALIGN=\"TOP\" ALIGN=\"RIGHT\"><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\"><TR>";
						}
						if(rs2.GetInt32(2) <= 5)
						{
							o += "<TR>";
						}
						else
						{
							w += rs2.GetInt32(4);
							if(showHeaderRow)
							{
								h += "<TD>&nbsp;</TD>";
							}
						}
						
						bool firstComponent = true;
						int compCX = 0;

						rs3 = Db.rs("SELECT ocs.OptionComponentID, ocl.Text FROM OptionComponents ocs INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + langID + " WHERE ocs.OptionID = " + rs2.GetInt32(1) + " ORDER BY ocs.SortOrder","eFormSqlConnection");
						while(rs3.Read())
						{
							compCX++;

							if(showHeaderRow)
							{
								if(rs2.GetInt32(2) == 1)
								{
									h += "<TD ALIGN=\"CENTER\" WIDTH=\"" + rs2.GetInt32(4) + "\">";
									if(!rs.IsDBNull(13) && rs.GetInt32(13) > 0)
									{
										// When hide in some option, put this in to stretch
										h += "<img src=\"img/null.gif\" width=\"" + rs2.GetInt32(4) + "\" height=\"1\"><br>";
									}
									h += rs3.GetString(1);
									h += "</TD>";
								}
							}
							if(rs2.GetInt32(2) > 5)
							{
								o += "<TR><TD WIDTH=\"" + rs2.GetInt32(4) + "\">";
							}
							else if(rs2.GetInt32(3) == 2 || rs2.GetInt32(3) == 4)
							{
								w += rs2.GetInt32(4);
								o += "<TD ALIGN=\"LEFT\" WIDTH=\"" + rs2.GetInt32(4) + "\">";
							}
							else
							{
								w += rs2.GetInt32(4);
								o += "<TD ALIGN=\"CENTER\" WIDTH=\"" + rs2.GetInt32(4) + "\">";
							}

							if(rs2.IsDBNull(8))
							{
								switch(rs2.GetInt32(3))
								{
									case 1:
									{
										if(!rs.IsDBNull(13) && rs.GetInt32(13) > 0)
										{
											o += "<img src=\"img/null.gif\" width=\"" + rs2.GetInt32(4) + "\" height=\"1\"><br>";
										}
										if(HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)] != null && HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)].ToString() != "")
										{
											hideIt = true;
										}
										bool chk = false;
										if(!IsPostBack && HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)] != null && HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)].ToString() != "")
										{
											if(HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)].ToString() == rs3.GetInt32(0).ToString())
											{
												chk = true;
											}
										}
										else if(oldValues.Contains("Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)) && oldValues["Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)].ToString() == rs3.GetInt32(0).ToString())
										{
											chk = true;
										}
										o += "<INPUT" + (chk ? " CHECKED" : "") + " TYPE=\"radio\" ONFOCUS=\"setRad(document.forms[0].Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1) + ");\" ONCLICK=\"" + dependency + "chkRad(document.forms[0].Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1) + "," + (!rs2.IsDBNull(6) && rs2.GetInt32(6) == 1 ? "true" : "false") + ");\" NAME=\"Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1) + "\" VALUE=\"" + rs3.GetInt32(0) + "\">";
										break;
									}
									case 2:
									{
										if(HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)] != null && HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)].ToString() != "")
										{
											hideIt = true;
										}
										string val = "";
										if(!IsPostBack && HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)] != null && HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)].ToString() != "")
										{
											val = HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)].ToString();
										}
										else if(oldValues.Contains("Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)))
										{
											val = oldValues["Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)].ToString();
										}

										if(rs2.GetInt32(5) <= 1)
										{
											o += "<INPUT VALUE=\"" + val + "\" TYPE=\"text\" ONFOCUS=\"setTxt(this.value);\" ONKEYUP=\"chkTxt(this.value," + (!rs2.IsDBNull(6) && rs2.GetInt32(6) == 1 ? "true" : "false") + ");\" NAME=\"Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1) + "\" STYLE=\"width:" + (!rs2.IsDBNull(7) && rs2.GetInt32(7) > 0 ? rs2.GetInt32(7) : rs2.GetInt32(4)) + "px;\">";
										}
										else
										{
											o += "<TEXTAREA NAME=\"Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1) + "\" ONFOCUS=\"setTxt(this.value);\" ONKEYUP=\"chkTxt(this.value," + (!rs2.IsDBNull(6) && rs2.GetInt32(6) == 1 ? "true" : "false") + ");\" STYLE=\"height:" + rs2.GetInt32(5) + "px;width:" + (!rs2.IsDBNull(7) && rs2.GetInt32(7) > 0 ? rs2.GetInt32(7) : rs2.GetInt32(4)) + "px;\">" + val + "</TEXTAREA>";
										}
										break;
									}
									case 3:
									{
										if(HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)] != null && HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)].ToString() != "")
										{
											hideIt = true;
										}
										bool chk = false;
										if(!IsPostBack && HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)] != null && HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)].ToString() != "")
										{
											if(HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)].ToString() == rs3.GetInt32(0).ToString())
											{
												chk = true;
											}
										}
										else if(oldValues.Contains("Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)) && oldValues["Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)].ToString() == rs3.GetInt32(0).ToString())
										{
											chk = true;
										}
										o += "<INPUT" + (chk ? " CHECKED" : "") + " TYPE=\"checkbox\" ONFOCUS=\"setRad(document.forms[0].Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1) + ");\" ONCLICK=\"" + dependency + "chkRad(document.forms[0].Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1) + "," + (!rs2.IsDBNull(6) && rs2.GetInt32(6) == 1 ? "true" : "false") + ");\" NAME=\"Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1) + "\" VALUE=\"" + rs3.GetInt32(0) + "\">";
										break;
									}
									case 4:
									{
										goto case 2;
									}
									case 9:
									{
										if(firstComponent)
										{
											if(HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)] != null && HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)].ToString() != "")
											{
												hideIt = true;
											}

											string val = "NULL";
											if(!IsPostBack && HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)] != null && HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)].ToString() != "")
											{
												val = HttpContext.Current.Session["PRUID" + projectRoundUnitID + "Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)].ToString();
											}
											else if(oldValues.Contains("Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)))
											{
												val = oldValues["Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1)].ToString();
											}

											vasCX++;
											vasDict += "<input type=\"hidden\" name=\"Q" + rs2.GetInt32(0) + "O" + rs2.GetInt32(1) + "\" value=\"" + vasCX + "\">";

											o += "" +
												"<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
												"<tr><td colspan=\"3\"><img src=\"submitImages/null.gif\" width=\"1\" height=\"8\"></td></tr>" +
												"<tr>" +
												"<td><img src=\"submitImages/VASdot_l.gif\" width=\"6\" height=\"15\"></td>" +
												"<td><img id=\"VASline" + vasCX + "\" src=\"submitImages/VASline.gif\" width=\"330\" height=\"15\" border=\"0\"><img style=\"position:absolute;visibility:hidden;\" id=\"VASknob" + vasCX + "\" src=\"submitImages/VASknob4.gif\" width=\"4\" height=\"15\"></td>" +
												"<td><img src=\"submitImages/VASdot_r.gif\" width=\"6\" height=\"15\"></td>" +
												"</tr>" +
												"<tr>" +
												"<td colspan=\"3\">" +
												"<input type=\"hidden\" name=\"VASforced" + vasCX + "\" value=\"" + (!rs2.IsDBNull(6) && rs2.GetInt32(6) == 1 ? "1" : "0") + "\">" +
												"<input type=\"hidden\" name=\"VASvalue" + vasCX + "\" value=\"" + val + "\">" +
												"<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
												"<tr>" +
												"<td align=\"left\" style=\"width:[VAS_LEFT_SIZE]px;font-size:11px;\">" + rs3.GetString(1) + "</td>" +
												"[VAS_DESC]" +
												"</tr>" +
												"</table>" +
												"</td>" +
												"</tr>" +
												"</table>";
										}
										else
										{
											o = o.Replace("[VAS_ALIGN]","center").Replace("[VAS_RIGHT_SIZE]","[VAS_MIDDLE_SIZE]").Replace("[VAS_DESC]","<td align=\"[VAS_ALIGN]\" style=\"width:[VAS_RIGHT_SIZE]px;font-size:11px;\">" + rs3.GetString(1) + "</td>[VAS_DESC]");
										}
									}
										break;
								}
								if(rs2.GetInt32(2) == 3 || rs2.GetInt32(2) == 8)
								{
									o += rs3.GetString(1);
								}
							}
							else
							{
								o += "<img src=\"img/null.gif\" width=\"" + rs2.GetInt32(4) + "\" height=\"1\">";
							}
				
							o += "</TD>";

							if(rs2.GetInt32(2) > 5)
							{
								o += "</TR>\r\n";
							}

							firstComponent = false;
						}
						rs3.Close();

						o = o.Replace("[VAS_ALIGN]","right").Replace("[VAS_DESC]","");
						switch(compCX)
						{
							case 2:	o = o.Replace("[VAS_LEFT_SIZE]","168").Replace("[VAS_RIGHT_SIZE]","168"); break;
							case 3:	o = o.Replace("[VAS_LEFT_SIZE]","112").Replace("[VAS_RIGHT_SIZE]","112").Replace("[VAS_MIDDLE_SIZE]","112"); break;
							case 5:	o = o.Replace("[VAS_LEFT_SIZE]","58").Replace("[VAS_RIGHT_SIZE]","58").Replace("[VAS_MIDDLE_SIZE]","75"); break;
						}

						if(showHeaderRow)
						{
							h += "</TR></TABLE></TD>";
						}
						if(rs2.GetInt32(2) <= 5)
						{
							o += "</TR>\r\n";
						}
						o += "</TABLE></TD>";
					}
					rs2.Close();

					#endregion

					#region Render question

					questionsInBox++;
					questionsHidInBox += (hideIt ? 1 : 0);

					string q = "<SPAN STYLE=\"";
					switch(rs.GetInt32(2))
					{
						case 1:	q += "font-family: Tahoma;"; break;
						case 2:	q += "font-family: Verdana;"; break;
						case 3:	q += "font-family: Arial;"; break;
						case 4:	q += "font-family: Courier New;"; break;
						case 5:	q += "font-family: Times New Roman;"; break;
					}
					if(rs.GetInt32(3) != 0)
					{
						q += "font-size: " + rs.GetInt32(3) + "px;";
					}
					switch(rs.GetInt32(4))
					{
						case 2:	q += "font-weight: bold;"; break;
						case 3:	q += "font-weight: italic;"; break;
						case 4:	q += "text-decoration: underline;"; break;
						case 5:	q += "text-decoration: line-through;"; break;
					}
					if(missingValues.Contains("Q" + rs.GetInt32(8)))
					{
						q += "color:#CC0000;";
					}
					else if(rs.GetString(5) != "")
					{
						q += "color: " + rs.GetString(5) + ";";
					}
					q += "\">" + rs.GetString(7).Replace("\n","<BR/>") + (!rs.IsDBNull(15) ? "<br>&nbsp;" : "") + "</span>";

					if(rs.GetInt32(6) == 1)
					{
					}

					if(o == "")
					{
						boxBuffer += "<TR id=\"Q" + rs.GetInt32(0) + "Q\"><TD COLSPAN=\"2\"" + bg + ">" + q + "</TD></TR>\r\n";
					}
					else
					{
						if(h != "")
						{
							boxBuffer += "<TR id=\"Q" + rs.GetInt32(0) + "H\"" + (hideIt ? " style=\"display:none;\"" : "") + ">";
							boxBuffer += "<TD" + bg + ">&nbsp;</TD>";
							boxBuffer += "<TD" + bg + ">";
				
							boxBuffer += "<TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">";
							boxBuffer += "<TR>";
							boxBuffer += "<TD WIDTH=\"" + (555-w) + "\">&nbsp;</TD>";
							boxBuffer += "" + h + "";
							boxBuffer += "</TR>";
							boxBuffer += "</TABLE>";
				
							boxBuffer += "</TD>";
							boxBuffer += "</TR>\r\n";
						}

						boxBuffer += "<TR id=\"Q" + rs.GetInt32(0) + "Q\"" + (hideIt ? " style=\"display:none;\"" : "") + ">";
						boxBuffer += "<TD VALIGN=\"TOP\" ALIGN=\"RIGHT\"" + bg + ">" + (rs.IsDBNull(10) || rs.GetInt32(10) == 0 ? (++cx) + "." : "") + "&nbsp;</TD>";
						boxBuffer += "<TD" + bg + ">";
			
						if(rs.GetInt32(1) == 1)
						{
							boxBuffer += "<TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">";
							boxBuffer += "<TR>";
							boxBuffer += "<TD WIDTH=\"" + (555-w) + "\" VALIGN=\"TOP\">" + q + "</TD>";
							boxBuffer += "" + o + "";
							boxBuffer += "</TR>";
							boxBuffer += "</TABLE>";
						}
						else
						{
							boxBuffer += q;
							boxBuffer += "</TD>";
							boxBuffer += "</TR>\r\n";
							boxBuffer += "<TR id=\"Q" + rs.GetInt32(0) + "A\"" + (hideIt ? " style=\"display:none;\"" : "") + ">";
							boxBuffer += "<TD" + bg + ">&nbsp;</TD>";
							boxBuffer += "<TD" + bg + ">";
							boxBuffer += "<TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">";
							boxBuffer += "<TR>";
							boxBuffer += "" + o + "";
							boxBuffer += "</TR>";
							boxBuffer += "</TABLE>";
						}
				
						boxBuffer += "</TD>";
						boxBuffer += "</TR>\r\n";
					}
					if(rs.IsDBNull(14))
					{
						boxBuffer += "<TR id=\"Q" + rs.GetInt32(0) + "S1\"" + (hideIt ? " style=\"display:none;\"" : "") + "><TD colspan=\"3\"" + bg + "><img src=\"submitImages/null.gif\" width=\"1\" height=\"10\"></td></tr>";
					}

					flipFlopBg = (!rs.IsDBNull(16) ? !flipFlopBg : false);

					previousHideIt = hideIt;
					hideIt = false;
					#endregion
                }
                */
                #endregion
            }
            rs.Close();
            if (twoCol)
            {
                int hop = Convert.ToInt32(Math.Ceiling((double)totalCX / 2));
                for (int i = 0; i < hop; i++)
                {
                    Survey.Controls.Add(new LiteralControl(sbarr[i]));
                    if (i + hop < totalCX)
                    {
                        Survey.Controls.Add(new LiteralControl(sbarr[i + hop]));
                    }
                }
            }
            else
            {
                for (int i = 0; i < totalCX; i++)
                {
                    Survey.Controls.Add(new LiteralControl(sbarr[i]));
                }
            }

            //boxBuffer += "</table>";
            //boxBuffer += "</div>";

            //if(questionsHidInBox > 1 && questionsInBox == questionsHidInBox)
            //{
            //    boxBuffer = boxBuffer.Replace(" class=\"eform_area\""," style=\"display:none;\" class=\"eform_area\"").Replace(" class=\"eform_ques\""," style=\"display:none;\" class=\"eform_ques\"");
            //}

            //Survey.Controls.Add(new LiteralControl(boxBuffer));

            //Buttons.Controls.Add(new LiteralControl("<div id=\"footer\">&nbsp;"));
            //rs = Db.rs("SELECT Copyright FROM Survey WHERE SurveyID = " + surveyID,"eFormSqlConnection");
            //if(rs.Read() && !rs.IsDBNull(0))
            //{
            //    Buttons.Controls.Add(new LiteralControl("<br/>" + rs.GetString(0) + "<br/>&nbsp;"));
            //}
            //rs.Close();
            //Buttons.Controls.Add(new LiteralControl("</div>"));

            //ClientScript.RegisterStartupScript(this.GetType(),"DEPENDENCY","<script language=\"JavaScript\">" + globalDependency + "</SCRIPT>");

            //Survey.Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"COMPLETEDcount\" value=\"" + cCX + "\">"));
            //Survey.Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"AID\" value=\"" + answerID + "\">"));
            //Survey.Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"VAScount\" value=\"" + vasCX + "\">"));
            //Survey.Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"FORCEDcount\" value=\"" + mandatoryCX + "\">"));
            //Survey.Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"FORCEDcompleted\" value=\"" + (!newAnswer ? mandatoryCX - missingValues.Count : 0) + "\">"));
            //Survey.Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"VASinactive\" value=\"0\">"));
            //Survey.Controls.Add(new LiteralControl(vasDict));

            #endregion

            //if(SurveyIntro.Text != "")
            //{
            //    SurveyIntro.Text = "" +
            //        "<div class=\"eform_area\"><p>Information</p></div>" + 
            //        "<div class=\"eform_ques\"><TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\"><TR><TD>" +
            //        SurveyIntro.Text +
            //        "</TD></TR></TABLE></div>" +
            //        "<div id=\"footer\">&nbsp;</div>";
            //}
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
    }
}